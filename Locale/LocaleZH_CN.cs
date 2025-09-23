using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// Simplified Chinese locale entries (zh-CN)
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
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "主选项" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "设置" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "关于" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "启用成就" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "开启（默认）时，在使用模组时重新启用成就，并在加载期间保护该状态。" },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "模组" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "此模组的显示名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "当前模组版本。" },
            };
        }

        public void Unload() { }
    }
}
