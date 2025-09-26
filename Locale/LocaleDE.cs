using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
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
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "Haupt"     },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "Erweitert" },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "Info"     },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "Links"    },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "Hinweise" },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "Aktionen" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG"    },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Anzeigename des Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Aktuelle Mod-Version." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements-Wiki" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Öffnet das Wiki im Browser." },

               // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "Erfolge sind jetzt aktiviert; erledige einfach die Aufgaben, um sie ganz normal freizuschalten.\nViel Spaß! :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "Hinweis: Manchmal erscheint ein Erfolg erst nach einem Neustart des Spiels, obwohl die Bedingungen erfüllt wurden." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "Erfolg auswählen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "Wählen Sie einen Erfolg, auf den eine Aktion angewendet werden soll." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "Ausgewählten freischalten" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**Schaltet den ausgewählten Erfolg frei und schließt ihn ab.**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "Ausgewählten zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "Markiert den ausgewählten Erfolg als **nicht abgeschlossen**." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "DIESEN ERFOLG LÖSCHEN / ZURÜCKSETZEN.\n\nFortfahren?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "Dieses Mod aktiviert Erfolge bereits standardmäßig – ohne die Schaltflächen im Reiter „Erweitert“.\nWenn du es schneller willst, nutze die Schaltfläche [Ausgewählten freischalten]." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "VORSICHT mit [Alle zurücksetzen]. Falls versehentlich gedrückt, kannst du Erfolge mit [Ausgewählten freischalten] wiederherstellen." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearAllAchievements)), "ALLE ERFOLGE ZURÜCKSETZEN" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearAllAchievements)), "**WARNUNG**: Setzt ALLE Erfolge zurück (zum Testen nützlich).\nBei Fehlbedienung kannst du Erfolge mit [Ausgewählten freischalten] zurückholen." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearAllAchievements)), "ALLE ERFOLGE auf „nicht abgeschlossen“ zurücksetzen. Fortfahren?" },
            };
        }

        public void Unload() { }
    }
}
