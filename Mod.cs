using System.Reflection;            // Assembly attributes
using Colossal;                     // IDictionarySource
using Colossal.IO.AssetDatabase;    // AssetDatabase
using Colossal.Logging;             // ILog
using Game;                         // UpdateSystem, SystemUpdatePhase
using Game.Achievements;            // AchievementTriggerSystem
using Game.Modding;                 // IMod
using Game.SceneFlow;               // GameManager

namespace AchievementHelper
{
    public sealed class Mod : IMod
    {
        // ---- Logging ----
        // Writes to Logs/AchievementHelper.log (no UI popups)
        public static readonly ILog log =
            LogManager.GetLogger("AchievementHelper").SetShowsErrorsInUI(false);

        // ---- Settings (shared) ----
        public static Settings? Settings { get; private set; }

        // ---- Version / name (from .csproj assembly attributes) ----
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

            m_Settings = new Settings(this);
            Settings = m_Settings;

            // --- ADD LOCALES ---
            IDictionarySource en = new LocaleEN(m_Settings);
            IDictionarySource fr = new LocaleFR(m_Settings);
            IDictionarySource es = new LocaleES(m_Settings);
            IDictionarySource de = new LocaleDE(m_Settings);
            IDictionarySource it = new LocaleIT(m_Settings);
            IDictionarySource zhCN = new LocaleZH_CN(m_Settings);   // Simplified Chinese
            IDictionarySource ja = new LocaleJA(m_Settings);        // Japanese
            IDictionarySource ko = new LocaleKO(m_Settings);        // Korean
            IDictionarySource vi = new LocaleVI(m_Settings);        // Vietnamese

            // Register BEFORE showing Options UI
            AddLocale("en-US", en);
            AddLocale("fr-FR", fr);
            AddLocale("es-ES", es);
            AddLocale("de-DE", de);
            AddLocale("it-IT", it);
            AddLocale("ja-JP", ja);
            AddLocale("ko-KR", ko);
            AddLocale("vi-VN", vi);

            // Register ZH under common ids so LocalizationManager can find match
            AddLocale("zh-HANS", zhCN);     // log shows this is used
            AddLocale("zh-CN", zhCN);       // common Steam locale Simplified Chinese
            AddLocale("zh-Hans", zhCN);     // common alias (case variant)
            AddLocale("zh-Hans-CN", zhCN);  // common alias (region variant)

            // Load saved settings (or defaults on first run)
            AssetDatabase.global.LoadSettings("AchievementHelper", m_Settings, new Settings(this));

            // Expose Options UI (after locales are registered)
            m_Settings.RegisterInOptionsUI();

            // Run our system after the game’s achievement trigger system
            updateSystem.UpdateAfter<AchievementHelperSystem, AchievementTriggerSystem>(SystemUpdatePhase.MainLoop);

            // Optional: log active locale at load
            var lm = GameManager.instance?.localizationManager;
            if (lm != null)
                log.Info($"[Locale] Active: {lm.activeLocaleId}");
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
