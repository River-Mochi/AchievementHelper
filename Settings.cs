using System.Collections.Generic;
using System.Linq;                    // ToArray
using Colossal.IO.AssetDatabase;      // FileLocation
using Game.Modding;                   // IMod
using Game.Settings;                  // ModSetting + UI attributes

namespace AchievementHelper
{
    [FileLocation("AchievementHelper")]
    [SettingsUIGroupOrder(MainGroup, NotCompleteGroup, CompletedGroup)]
    [SettingsUIShowGroupName(MainGroup, NotCompleteGroup, CompletedGroup)]
    public class Settings : ModSetting
    {
        // ---- Tabs ----
        public const string Section = "Settings"; // main tab
        public const string AboutSection = "About";    // second tab
        public const string DebugSection = "Debug";    // (placeholder) third tab

        // ---- Groups shown on the "Settings" tab ----
        public const string MainGroup = "Settings";
        public const string NotCompleteGroup = "Not Complete";
        public const string CompletedGroup = "Completed";

        public Settings(IMod mod) : base(mod) { }

        // Toggle at the top of the Settings tab
        [SettingsUISection(Section, MainGroup)]
        public bool EnableAchievements { get; set; } = true;

        // Read-only lists (one achievement per line with • bullets).
        private string m_NotCompleteText = "—";
        private string m_CompletedText = "—";

        [SettingsUISection(Section, NotCompleteGroup)]
        public string NotCompleteList => m_NotCompleteText;

        [SettingsUISection(Section, CompletedGroup)]
        public string CompletedList => m_CompletedText;

        /// <summary>Called by <c>Mod.cs</c> after it builds the lists.</summary>
        public void SetAchievementLists(IEnumerable<string> notComplete, IEnumerable<string> completed)
        {
            static string BulletLines(IEnumerable<string> lines)
            {
                var arr = lines as string[] ?? lines.ToArray();
                // Show one name per line with a bullet.
                return arr.Length == 0 ? "—" : "• " + string.Join("\r\n• ", arr);
            }

            m_NotCompleteText = BulletLines(notComplete);
            m_CompletedText = BulletLines(completed);
        }

        // ---- About tab (read-only info) ----
        [SettingsUISection(AboutSection, MainGroup)]
        public string NameDisplay => Mod.Name;

        [SettingsUISection(AboutSection, MainGroup)]
        public string VersionDisplay => Mod.VersionShort;

        // Simple text “link” for now (buttons vary by SDK version; we’ll add the real button once confirmed)
        [SettingsUISection(AboutSection, MainGroup)]
        public string AchievementsWiki => "https://cs2.paradoxwikis.com/Achievements";

        // ---- Debug tab (placeholder; interactive controls come next) ----
        [SettingsUISection(DebugSection, MainGroup)]
        public string DebugNote => "Reset controls will appear here in a later build.";

        public override void SetDefaults()
        {
            EnableAchievements = true;
            m_NotCompleteText = "—";
            m_CompletedText = "—";
        }
    }
}
