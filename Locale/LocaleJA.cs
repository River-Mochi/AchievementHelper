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
                { m_Setting.GetSettingsLocaleID(), "Achievement Helper" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "設定" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "情報" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "デバッグ" },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "設定" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "未達成" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "達成済み" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "実績を有効化" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "ON（既定）の場合、MOD使用時でも実績を有効にし、読み込み中も維持します。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "未達成" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "まだ獲得可能な実績。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "達成済み" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "すでに解除した実績。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "このModの名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "現在のModバージョン。" },

           { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "備考" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "開発用ツールは後日追加予定。" },
            };
        }

        public void Unload() { }
    }
}
