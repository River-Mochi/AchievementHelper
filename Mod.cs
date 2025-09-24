using System.Reflection;               // Assembly attributes
using Colossal;                        // IDictionarySource
using Colossal.IO.AssetDatabase;       // AssetDatabase
using Colossal.Logging;                // ILog, LogManager
using Game;                            // UpdateSystem
using Game.Achievements;               // AchievementTriggerSystem
using Game.Modding;                    // IMod
using Game.SceneFlow;                  // GameManager

namespace AchievementHelper
{
    public sealed class Mod : IMod
    {
        // Logs/AchievementHelper.log
        public static readonly ILog log =
            LogManager.GetLogger("AchievementHelper").SetShowsErrorsInUI(false);

        public static Settings? Settings { get; private set; }

        // Version/name pulled from assembly (.csproj)
        private static readonly Assembly s_Asm = Assembly.GetExecutingAssembly();
        public static readonly string Name =
            s_Asm.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "Achievement Helper";

        private static readonly string s_InfoRaw =
            s_Asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "1.0.0";

        public static readonly string VersionShort = s_InfoRaw.Split(' ', '+')[0];
        public static readonly string VersionInformational = s_InfoRaw;

        // ---- State ----
        private static bool s_BannerLogged;
        private Settings? m_Settings;

        // ---- Lifecycle ----
        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (!s_BannerLogged)
            {
                log.Info($"{Name} {VersionShort} (info: {VersionInformational})");
                s_BannerLogged = true;
            }

            // Settings instance (also stashed for easy access by other classes)
            m_Settings = new Settings(this);
            Settings = m_Settings;

            // --- Locales (register BEFORE showing Options UI) ---
            IDictionarySource en = new LocaleEN(m_Settings);
            AddLocale("en-US", en);

            // If you added other locale classes, you can add them here later:
            // AddLocale("fr-FR", new LocaleFR(m_Settings));
            // AddLocale("de-DE", new LocaleDE(m_Settings));
            // AddLocale("es-ES", new LocaleES(m_Settings));
            // AddLocale("it-IT", new LocaleIT(m_Settings));
            // AddLocale("zh-HANS", new LocaleZH_CN(m_Settings));
            // AddLocale("zh-CN",   new LocaleZH_CN(m_Settings));
            // AddLocale("ja-JP",   new LocaleJA(m_Settings));
            // AddLocale("ko-KR",   new LocaleKO(m_Settings));
            // AddLocale("vi-VN",   new LocaleVI(m_Settings));

            // Load saved settings (or defaults on first run)
            AssetDatabase.global.LoadSettings("AchievementHelper", m_Settings, new Settings(this));

            // Build the Available/Completed lists once at load
            if (AchievementsBridge.TryBuildLists(out var available, out var completed))
            {
                m_Settings.SetAchievementLists(available, completed);
            }

            // Expose Options UI (after locales are registered)
            m_Settings.RegisterInOptionsUI();

            // Run our system after the game’s achievement trigger system
            updateSystem.UpdateAfter<AchievementHelperSystem, AchievementTriggerSystem>(SystemUpdatePhase.MainLoop);

            var lm = GameManager.instance?.localizationManager;
            if (lm != null) log.Info($"[Locale] Active: {lm.activeLocaleId}");
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));

            if (m_Settings != null)
            {
                m_Settings.UnregisterInOptionsUI();
                m_Settings = null;
            }

            Settings = null;
        }

        // ---- Helpers ----
        private static void AddLocale(string localeId, IDictionarySource source)
        {
            var lm = GameManager.instance?.localizationManager;
            if (lm != null) lm.AddSource(localeId, source);
            else log.Warn($"LocalizationManager null; cannot add locale '{localeId}'.");
        }
    }
}
