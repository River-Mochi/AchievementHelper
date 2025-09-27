using System.Collections.Generic;
using Colossal;

namespace AchievementFixer
{
    /// <summary>
    /// English locale (en-US)
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
                { m_Setting.GetOptionTabLocaleID(Settings.MainTab),     "Main"     },
                { m_Setting.GetOptionTabLocaleID(Settings.AdvancedTab), "Advanced" },

                // Groups (Main tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.MainInfoGroup), "Info"   },
                { m_Setting.GetOptionGroupLocaleID(Settings.ButtonGroup),   "Links"  },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotesGroup),    "Notes"  },

                // Groups (Advanced tab)
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowActions), "Actions" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AdvRowDebug),   "DEBUG"   },

                // Main >> Info
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Display name of this mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Current mod version." },

                // Main >> Links
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.OpenAchievementsWikiButton)), "Achievements wiki" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.OpenAchievementsWikiButton)),
                  "Open the achievements wiki in your browser." },

                // Main >> Notes
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.MainNotes)), "Achievements are enabled now - just do the required tasks to naturally complete achievements.\n" +
                "Enjoy! :)\n" +
                "Advanced tab has additional options.\n\n" +
                "There are 6 achievements in Steam that are not available until the Bridges & Ports DLC is released" },

                { m_Setting.GetOptionDescLocaleID(nameof(Settings.MainNotes)),
                    "Note: sometimes after completing required tasks for an achievement, it might not appear until reboot of game." },

                // --- Advanced tab ---
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.SelectedAchievement)),   "Select achievement" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.SelectedAchievement)),    "Choose an achievement to operate on." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.UnlockSelectedAchievement)), "Unlock selected" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.UnlockSelectedAchievement)),  "**Unlocks & Completes** the selected achievement." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ClearSelectedAchievement)),  "Clear selected" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ClearSelectedAchievement)),   "Marks the selected achievement as **not completed**." },
                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ClearSelectedAchievement)), "CLEAR / RESET this achievement.\n\nContinue?" },

                // Advanced >> advisory text
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AdvancedAdvisory)),
                  "This mod already enables achievements (default)\n" +
                  "without using any buttons in the Advanced tab.\n" +
                  "If you want something faster, then try the [Unlock Selected] button." },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AdvancedAdvisory)),
                    "BE CAREFUL using the [Clear ALL] button. If you accidentally use it, you can recover completed achievements with the [Unlock Selected] button." },

                // Advanced >> DEBUG (Clear All)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.ResetAllAchievements)),  "RESET ALL ACHIEVEMENTS" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.ResetAllAchievements)),
                    "**WARNING**: clears/resets ALL achievements. Useful to debug or for testers.\n" +
                    "If you accidentally use this button, you can get achievements back by using the [Unlock Selected] button." },

                { m_Setting.GetOptionWarningLocaleID(nameof(Settings.ResetAllAchievements)), "RESET / CLEAR all achievements to the NOT complete, original status. Continue?" },
            };
        }

        public void Unload() { }
    }
}
