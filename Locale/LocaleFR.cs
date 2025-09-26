using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// French locale (fr-FR)
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
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "Principal" },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "Avancé"    },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "Infos"  },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "Liens"  },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "Notes"  },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "Actions" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG"   },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Nom affiché du mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Version actuelle du mod." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Wiki des succès" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Ouvre le wiki des succès dans votre navigateur." },

                // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "Les succès sont activés ; il suffit d’accomplir les tâches requises pour les obtenir naturellement.\nAmusez-vous ! :)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)), "Remarque : parfois, après avoir rempli les conditions d’un succès, il peut n’apparaître qu’après un redémarrage du jeu." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)), "Choisir un succès" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)), "Choisissez un succès sur lequel agir." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "Déverrouiller la sélection" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)), "**Déverrouille et valide** le succès sélectionné." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "Réinitialiser la sélection" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)), "Marque le succès sélectionné comme **non accompli**." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "EFFACER / RÉINITIALISER ce succès.\n\nContinuer ?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)), "Ce mod active déjà les succès (par défaut) sans utiliser les boutons de l’onglet Avancé.\nSi vous voulez aller plus vite, utilisez le bouton [Déverrouiller la sélection]." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)), "FAITES ATTENTION avec le bouton [Tout réinitialiser]. En cas d’erreur, vous pouvez récupérer des succès avec [Déverrouiller la sélection]." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearAllAchievements)), "RÉINITIALISER TOUS LES SUCCÈS" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearAllAchievements)), "**AVERTISSEMENT** : efface/réinitialise TOUS les succès. Utile pour le test.\nSi vous l’utilisez par erreur, vous pouvez récupérer les succès via [Déverrouiller la sélection]." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearAllAchievements)), "RÉINITIALISER / EFFACER tous les succès vers l’état d’origine (non accompli). Continuer ?" },
            };
        }

        public void Unload() { }
    }
}
