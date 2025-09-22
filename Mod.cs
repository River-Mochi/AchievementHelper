using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Mod.AchievementHelper;

namespace AchievementHelper
{
    public class Mod : IMod
    {
        public static readonly ILog Log =
            LogManager.GetLogger("AchievementHelper").SetShowsErrorsInUI(false);

        public static Settings Settings { get; private set; } = null!;

        private Settings m_SettingsInstance;

        public void OnLoad(UpdateSystem updateSystem)
        {
            Log.Info(nameof(OnLoad));

            // Settings + localization
            m_SettingsInstance = new Settings(this);
            m_SettingsInstance.RegisterInOptionsUI();
            Settings = m_SettingsInstance;

            GameManager.instance.localizationManager.AddSource("en-US",
                new Locale.LocaleEN(m_SettingsInstance));

            // Load saved values (if present)
            AssetDatabase.global.LoadSettings(nameof(AchievementHelper),
                m_SettingsInstance, new Settings(this));

            // Schedule our system; make its OnUpdate run after AchievementTriggerSystem
            updateSystem.UpdateAfter<AchievementHelperSystem, Game.Achievements.AchievementTriggerSystem>
                (SystemUpdatePhase.MainLoop);
        }

        public void OnDispose()
        {
            Log.Info(nameof(OnDispose));
            if (m_SettingsInstance != null)
            {
                m_SettingsInstance.UnregisterInOptionsUI();
                m_SettingsInstance = null!;
            }
        }
    }
}
