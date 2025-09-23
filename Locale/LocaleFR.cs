using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// French locale entries (fr-FR)
    /// </summary>
    public class LocaleFR : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleFR(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "Principal" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "Paramètres" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "À propos" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Activer les succès" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Quand ACTIVÉ (par défaut), réactive les succès avec des mods et protège l’état pendant le chargement." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Nom du mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "Version actuelle du mod." },
            };
        }

        public void Unload() { }
    }
}
