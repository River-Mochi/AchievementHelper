using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// Spanish locale entries (es-ES)
    /// </summary>
    public class LocaleES : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleES(Settings setting) { m_Setting = setting; }

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
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "Ajustes" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "Acerca de" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Activar logros" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Cuando está ACTIVADO (por defecto), vuelve a habilitar los logros al usar mods y protege el estado durante la carga." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Nombre del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "Versión actual del mod." },
            };
        }

        public void Unload() { }
    }
}
