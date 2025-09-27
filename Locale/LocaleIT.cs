using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// Italian locale (it-IT)
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
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "Home"     },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "Avanzate" },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "Info"  },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "Link"  },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "Note"  },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "Azioni" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG"  },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Nome visualizzato del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Versione corrente del mod." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Wiki degli obiettivi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Apri il wiki degli obiettivi nel browser." },

                 // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "Gli obiettivi sono attivi; completa le attività richieste per ottenerli in modo naturale.\nBuon divertimento! :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "Nota: a volte, dopo aver soddisfatto i requisiti, l’obiettivo può apparire solo dopo il riavvio del gioco." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "Seleziona obiettivo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "Scegli un obiettivo su cui operare." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "Sblocca selezionato" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**Sblocca e completa** l’obiettivo selezionato." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "Cancella selezionato" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "Imposta l’obiettivo selezionato come **non completato**." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "CANCELLARE / REIMPOSTARE questo obiettivo.\n\nContinuare?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "Questa mod abilita già gli obiettivi (predefinito) senza usare i pulsanti della scheda Avanzate.\nSe vuoi fare prima, usa [Sblocca selezionato]." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "FAI ATTENZIONE a [Reimposta tutto]. Se lo premi per sbaglio, puoi recuperare gli obiettivi con [Sblocca selezionato]." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)), "REIMPOSTA TUTTI GLI OBIETTIVI" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)), "**ATTENZIONE**: cancella/reimposta TUTTI gli obiettivi. Utile per test.\nSe sbagli, recuperali con [Sblocca selezionato]." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)), "REIMPOSTARE / CANCELLARE tutti gli obiettivi allo stato INIZIALE (non completato). Continuare?" },
            };
        }

        public void Unload() { }
    }
}
