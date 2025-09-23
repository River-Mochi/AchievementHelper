using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// German locale entries (de-DE)
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
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "Haupt" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "Einstellungen" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "Info" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Erfolge aktivieren" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Wenn EIN (Standard), werden Erfolge bei Verwendung von Mods wieder aktiviert und während des Ladens geschützt." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Anzeigename des Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "Aktuelle Mod-Version." },
            };
        }

        public void Unload() { }
    }
}
