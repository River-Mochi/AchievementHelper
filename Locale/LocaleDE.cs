using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// German locale (de-DE)
    /// </summary>
    public class LocaleDE : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleDE(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Achievement Helper" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "Einstellungen" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "Info"          },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "Debug"         },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Einstellungen"    },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Nicht abgeschlossen" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Abgeschlossen"    },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Erfolge aktivieren" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Wenn EIN (Standard), ermöglicht Erfolge mit Mods und hält die Einstellung während des Ladens aktiv." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Nicht abgeschlossen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Noch erreichbare Erfolge." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Abgeschlossen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Bereits freigeschaltete Erfolge." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Anzeigename des Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Aktuelle Mod-Version." },

               { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "Hinweise" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "Entwicklertools werden später hinzugefügt." },
            };
        }

        public void Unload() { }
    }
}
