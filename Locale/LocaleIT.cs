using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// Italian locale entries (it-IT)
    /// </summary>
    public class LocaleIT : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleIT(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "Principale" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "Impostazioni" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "Informazioni" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Abilita obiettivi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Quando ATTIVO (predefinito), riabilita gli obiettivi con le mod e protegge lo stato durante il caricamento." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Nome visualizzato della mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "Versione attuale della mod." },
            };
        }

        public void Unload() { }
    }
}

