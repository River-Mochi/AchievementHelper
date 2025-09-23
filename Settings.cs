using System.Collections.Generic;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace AchievementHelper
{
    [FileLocation("AchievementHelper")]
    [SettingsUIGroupOrder(MainGroup, AchievementsAvailableGroup, AchievementsCompletedGroup, AboutGroup)]
    [SettingsUIShowGroupName(MainGroup, AchievementsAvailableGroup, AchievementsCompletedGroup, AboutGroup)]
    public class Settings : ModSetting
    {
        public const string Section = "Main";
        public const string MainGroup = "Settings";
        public const string AchievementsAvailableGroup = "Available";
        public const string AchievementsCompletedGroup = "Completed";
        public const string AboutGroup = "About";

        public Settings(IMod mod) : base(mod) { }

        // Toggle at top
        [SettingsUISection(Section, MainGroup)]
        public bool EnableAchievements { get; set; } = true;

        // --- Achievements lists (read-only text blocks) ---
        private string m_AvailableText = "—";
        private string m_CompletedText = "—";

        [SettingsUISection(Section, AchievementsAvailableGroup)]
        public string AvailableList => m_AvailableText;

        [SettingsUISection(Section, AchievementsCompletedGroup)]
        public string CompletedList => m_CompletedText;

        // Called by our code to (re)populate the two lists.
        public void SetAchievementLists(IEnumerable<string> available, IEnumerable<string> completed)
        {
            static string JoinLines(IEnumerable<string> lines)
            {
                // bullet list; Settings UI renders plain text
                var arr = lines as string[] ?? lines.ToArray();
                return arr.Length == 0 ? "—" : "• " + string.Join("\n• ", arr);
            }

            m_AvailableText = JoinLines(available);
            m_CompletedText = JoinLines(completed);
        }

        // About — read-only displays
        [SettingsUISection(Section, AboutGroup)]
        public string NameDisplay => Mod.Name;

        [SettingsUISection(Section, AboutGroup)]
        public string VersionDisplay => Mod.VersionShort;

        public override void SetDefaults()
        {
            EnableAchievements = true;
            m_AvailableText = "—";
            m_CompletedText = "—";
        }
    }
}
