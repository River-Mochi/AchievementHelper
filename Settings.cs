using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace AchievementHelper
{
    [FileLocation("AchievementHelper")]
    [SettingsUIGroupOrder(MainGroup, AboutGroup)]
    [SettingsUIShowGroupName(MainGroup, AboutGroup)]
    public class Settings : ModSetting
    {
        public const string Section = "Main";
        public const string MainGroup = "Settings";
        public const string AboutGroup = "About";

        public Settings(IMod mod) : base(mod) { }

        // Toggle at top
        [SettingsUISection(Section, MainGroup)]
        public bool EnableAchievements { get; set; } = true;

        // About — read-only displays
        [SettingsUISection(Section, AboutGroup)]
        public string NameDisplay => Mod.Name;

        [SettingsUISection(Section, AboutGroup)]
        public string VersionDisplay => Mod.VersionShort;

        public override void SetDefaults()
        {
            EnableAchievements = true;
        }
    }
}
