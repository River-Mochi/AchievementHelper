using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;

namespace AchievementHelper
{
    [FileLocation(nameof(AchievementHelper))]
    [SettingsUIGroupOrder(MainGroup, AboutGroup)]
    [SettingsUIShowGroupName(MainGroup, AboutGroup)]
    public class Settings : ModSetting
    {
        public const string Section = "Main";
        public const string MainGroup = "Settings";
        public const string AboutGroup = "About";

        public Settings(IMod mod) : base(mod) { }

        // --- Options ---

        [SettingsUISection(Section, MainGroup)]
        public bool EnableAchievements { get; set; } = true;

        // Read-only “about” fields
        [SettingsUISection(Section, AboutGroup)]
        public string NameDisplay => "Achievement Helper";

        [SettingsUISection(Section, AboutGroup)]
        public string VersionDisplay => typeof(Settings).Assembly
            .GetName().Version?.ToString() ?? "1.0.0";

        [SettingsUISection(Section, AboutGroup)]
        [SettingsUIButton]
        public bool OpenGithub
        {
            set
            {
                Mod.Log.Info("OpenGithub clicked");
                // optional: System.Diagnostics.Process.Start(new ProcessStartInfo { ... });
            }
        }

        public override void SetDefaults()
        {
            EnableAchievements = true;
        }
    }
}
