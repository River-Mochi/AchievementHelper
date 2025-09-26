using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
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
                { m_Setting.GetSettingsLocaleID(), "Achievement Fixer" },

                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "Impostazioni" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "Informazioni" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "Debug" },

                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Impostazioni" },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Non completati" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Completati" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Abilita obiettivi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Quando ATTIVO (predefinito), consente gli obiettivi con mod e li mantiene attivi durante il caricamento." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Non completati" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Obiettivi ancora conseguibili." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Completati" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Obiettivi già sbloccati." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Nome visualizzato del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Versione attuale del mod." },

        { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "Note" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "Strumenti di sviluppo saranno aggiunti." },
            };
        }

        public void Unload() { }
    }
}
