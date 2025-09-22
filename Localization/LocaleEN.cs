using System.Collections.Generic;
using Colossal;
using Colossal.IO.AssetDatabase;

namespace AchievementHelper
{
    /// <summary>Static labels and tooltips for the Settings UI.</summary>
    public class LocaleEN : IDictionarySource
    {
        private readonly Settings m_Settings;
        public LocaleEN(Settings settings) { m_Settings = settings; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // One tab
                { m_Settings.GetOptionTabLocaleID(Settings.Section), "Achievement Helper" },

                // Group titles
                { m_Settings.GetOptionGroupLocaleID(Settings.MainGroup),  "Settings" },
                { m_Settings.GetOptionGroupLocaleID(Settings.AboutGroup), "About" },

                // Toggle
                { m_Settings.GetOptionLabelLocaleID(nameof(Settings.EnableAchievements)), "Enable achievements" },
                { m_Settings.GetOptionDescLocaleID(nameof(Settings.EnableAchievements)),
                  "When ON (default), re-enables achievements while using mods and protects the flag during loading." },

                // About
                { m_Settings.GetOptionLabelLocaleID(nameof(Settings.NameDisplay)), "Mod" },
                { m_Settings.GetOptionDescLocaleID(nameof(Settings.NameDisplay)),  "Display name of this mod." },

                { m_Settings.GetOptionLabelLocaleID(nameof(Settings.VersionDisplay)), "Version" },
                { m_Settings.GetOptionDescLocaleID(nameof(Settings.VersionDisplay)),  "Current mod version." },

                { m_Settings.GetOptionLabelLocaleID(nameof(Settings.OpenGithub)), "GitHub" },
                { m_Settings.GetOptionDescLocaleID(nameof(Settings.OpenGithub)),  "Open the mod’s GitHub page in your browser." },
            };
        }

        public void Unload() { }
    }
}
