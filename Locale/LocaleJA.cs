using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// Japanese locale entries (ja-JP)
    /// </summary>
    public class LocaleJA : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleJA(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "メイン" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "設定" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "概要" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "実績を有効にする" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "ON（既定）の場合、Mod使用中でも実績を再度有効化し、読み込み中に状態を保護します。" },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "このModの表示名。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "現在のModバージョン。" },
            };
        }

        public void Unload() { }
    }
}
