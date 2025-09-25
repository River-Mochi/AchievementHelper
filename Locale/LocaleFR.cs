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
                { m_Setting.GetSettingsLocaleID(), "Achievement Helper" },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "Paramètres" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "À propos"   },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "Debug"      },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Paramètres"         },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Non terminés"       },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Terminés"           },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Activer les succès" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "Quand activé (par défaut), permet d'obtenir des succès avec des mods et maintient l’option active au chargement." },

                // Lists
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Non terminés" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Succès encore disponibles." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Terminés" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Succès déjà débloqués." },

                // About
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Nom affiché du mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Version actuelle du mod." },

          { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
{ m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),  "Open the CS2 achievements wiki in your browser." },


                // Debug
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "Notes" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "Des outils de développement seront ajoutés ici." },
            };
        }

        public void Unload() { }
    }
}
