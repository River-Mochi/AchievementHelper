using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Simplified Chinese locale (zh-HANS)
    /// </summary>
    public class LocaleZH_CN : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleZH_CN(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "主页"  },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "高级"  },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "信息" },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "链接" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "说明" },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "操作" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG" },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "模组" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "此模组的显示名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "当前模组版本。" },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "成就 Wiki" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "在浏览器中打开成就 Wiki。" },

                // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "已启用成就；按正常方式完成任务即可自然解锁。\n玩得开心！:)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "注意：有时在满足条件后，需重启游戏成就才会显示。" },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "选择成就" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "选择一个要操作的成就。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "解锁所选" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**解锁并完成**所选成就。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "清除所选" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "将所选成就标记为**未完成**。" },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "清除/重置此成就。\n\n继续？" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "本模组已默认启用成就，无需使用“高级”选项卡中的按钮。\n如果想更快，请使用 [解锁所选] 按钮。" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "使用 [重置全部] 时请小心。如果误触，可通过 [解锁所选] 取回成就。" },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearAllAchievements)), "重置所有成就" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearAllAchievements)), "**警告**：清除/重置所有成就（用于调试/测试）。\n如果误操作，可用 [解锁所选] 取回成就。" },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearAllAchievements)), "将所有成就重置为**未完成**的初始状态。继续？" },
            };
        }

        public void Unload() { }
    }
}
