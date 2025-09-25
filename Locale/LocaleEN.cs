using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>
    /// English Locale (en-US)
    /// </summary>
    public class LocaleEN : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleEN(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Options menu entry
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),  "Main"  },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutTab), "About" },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugTab), "Debug" },

                // Groups (Main)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Settings"     },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Not Complete" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Complete"     },

                // Groups (About)
                { m_Setting.GetOptionGroupLocaleID(Settings.InfoGroup),    "Info"    },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),  "Links"   },
                { m_Setting.GetOptionGroupLocaleID(Settings.FiltersGroup), "Filters" },

                // Groups (Debug)
                { m_Setting.GetOptionGroupLocaleID(Settings.DebugButtons), "Actions" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Enable achievements" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "When ON (default), enables achievements while using mods and keeps the flag on during loading." },

                // Lists
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Not Complete" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Achievements you can still earn." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Complete" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Achievements already unlocked." },

                // About — info fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Display name of this mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Current mod version." },

                // About — link button
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Open the CS2 achievements wiki in your browser." },

                // Debug tab
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)),   "Select achievement" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)),    "Choose an achievement to clear." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)), "Clear selected achievement" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)),  "Sets the selected achievement to not completed (platform backend)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)),  "RESET ALL" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)),   "WARNING: resets ALL achievements to not completed. Useful for testing but understand it will clear out existing completed achievements." },

                // Confirmation for RESET ALL
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)),
                  "Permanently clear all completed achievements.\nContinue?" },
            };
        }

        public void Unload() { }
    }
}
