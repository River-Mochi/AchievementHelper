// LocaleEN.cs
using System.Collections.Generic;
using Colossal;

namespace AchievementHelper
{
    /// <summary> English strings for the Options UI. </summary>
    public class LocaleEN : IDictionarySource
    {
        private readonly Settings m_Setting;
        public LocaleEN(Settings setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // One section/tab
                { m_Setting.GetOptionTabLocaleID(Settings.Section), "Main" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Settings.MainGroup),  "Settings" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AchievementsAvailableGroup), "Available" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AchievementsCompletedGroup), "Completed" },
                { m_Setting.GetOptionGroupLocaleID(Settings.AboutGroup), "About" },

                // Toggle
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Enable achievements" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "When ON (default), re-enables achievements while using mods and protects the flag during loading." },

                // Available list (read-only)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.AvailableList)), "Available achievements" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.AvailableList)),
                  "Locked achievements you can still earn in this save." },

                // Completed list (read-only)
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.CompletedList)), "Completed achievements" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.CompletedList)),
                  "Achievements you’ve already earned." },

                // About fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Display name of this mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)), "Current mod version." },
            };
        }

        public void Unload() { }
    }
}
