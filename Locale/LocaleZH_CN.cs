using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Simplified Chinese locale entries (zh-Hans / zh-CN)
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
                { m_Setting.GetSettingsLocaleID(), "Achievement Fixer" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "设置" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "关于" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "调试" },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "设置" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "未完成" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "已完成" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "启用成就" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "开启（默认）后，使用模组时仍可获得成就，并在加载期间保持开启。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "未完成" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "仍可获得的成就。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "已完成" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "已解锁的成就。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Mod 的显示名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "当前 Mod 版本。" },

{ m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "说明" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "将来会添加开发工具。" },
            };
        }

        public void Unload() { }
    }
}
