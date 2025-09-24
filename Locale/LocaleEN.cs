using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary>English strings for the Options UI (en-US).</summary>
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
                { m_Setting.GetOptionTabLocaleID(Settings.Section),      "Settings" },
                { m_Setting.GetOptionTabLocaleID(Settings.AboutSection), "About"   },
                { m_Setting.GetOptionTabLocaleID(Settings.DebugSection), "Debug"   },

                // Groups on Settings tab
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),        "Settings"     },
                { m_Setting.GetOptionGroupLocaleID(Settings.NotCompleteGroup), "Not Complete" },
                { m_Setting.GetOptionGroupLocaleID(Settings.CompletedGroup),   "Completed"    },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Enable achievements" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "When ON (default), enables achievements while using mods and keeps the flag on during loading." },

                // Lists
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NotCompleteList)), "Not Complete" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NotCompleteList)),  "Achievements you can still earn." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Completed" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),  "Achievements already unlocked." },

                // About
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)),    "Mod"     },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),     "Display name of this mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Current mod version." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AchievementsWiki)), "Achievements wiki" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AchievementsWiki)),  "Open the CS2 achievements wiki." },

                // Debug tab
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.DebugNote)), "Notes" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.DebugNote)),  "Developer utilities will be added here." },
            };
        }

        public void Unload() { }
    }
}
