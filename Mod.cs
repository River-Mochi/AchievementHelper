using System.Collections.Generic;
using System.Reflection;
using Colossal;                        // IDictionarySource
using Colossal.IO.AssetDatabase;       // AssetDatabase
using Colossal.Logging;                // ILog, LogManager
using Game;                            // UpdateSystem
using Game.Achievements;               // AchievementTriggerSystem
using Game.Modding;                    // IMod
using Game.SceneFlow;                  // GameManager

namespace AchievementFixer
{
    public sealed class Mod : IMod
    {
        public static readonly ILog log =
            LogManager.GetLogger("AchievementFixer").SetShowsErrorsInUI(false);

        public static Settings? Settings { get; private set; }

        private static readonly Assembly s_Asm = Assembly.GetExecutingAssembly();
        public static readonly string Name =
            s_Asm.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "Achievement Fixer";

        private static readonly string s_InfoRaw =
            s_Asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "1.0.0";

        public static readonly string VersionShort = s_InfoRaw.Split(' ', '+')[0];

        private static bool s_BannerLogged;

        // Add common locale variants
        internal static readonly string[] s_LocaleIds =
        {
            "en-US","fr-FR","de-DE","es-ES","it-IT","ja-JP","ko-KR","vi-VN","zh-HANS",
        };

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));
            if (!s_BannerLogged)
            {
                log.Info($"{Name} {VersionShort}");
                s_BannerLogged = true;
            }

            var settings = new Settings(this);
            Settings = settings;

            // Locales (register BEFORE Options UI)
            AddLocale("en-US", new LocaleEN(settings));
            AddLocale("fr-FR", new LocaleFR(settings));
            AddLocale("de-DE", new LocaleDE(settings));
            AddLocale("es-ES", new LocaleES(settings));
            AddLocale("it-IT", new LocaleIT(settings));
            AddLocale("ja-JP", new LocaleJA(settings));
            AddLocale("ko-KR", new LocaleKO(settings));
            AddLocale("vi-VN", new LocaleVI(settings));
            AddLocale("zh-HANS", new LocaleZH_CN(settings));


            // Load any saved settings (none currently)
            AssetDatabase.global.LoadSettings("AchievementFixer", settings, new Settings(this));

            // Options UI
            settings.RegisterInOptionsUI();

            // Hide Achievement warning @ mods strings
            TryInstallWarningOverrideSource();

            // Keep enabled: run after achievement trigger system
            updateSystem.UpdateAfter<AchievementFixerSystem, AchievementTriggerSystem>(SystemUpdatePhase.MainLoop);

            var lm = GameManager.instance?.localizationManager;
            if (lm != null) log.Info($"[Locale] Active: {lm.activeLocaleId}");
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (Settings != null)
            {
                Settings.UnregisterInOptionsUI();
                Settings = null;
            }
        }

        private static void AddLocale(string localeId, IDictionarySource source)
        {
            var lm = GameManager.instance?.localizationManager;
            if (lm != null) lm.AddSource(localeId, source);
            else log.Warn($"LocalizationManager null; cannot add locale '{localeId}'.");
        }

        /// <summary>
        /// Blank the Achievement warning strings used on map menu and Progression/Achievements panel.
        /// </summary>
        private static void TryInstallWarningOverrideSource()
        {
            var lm = GameManager.instance?.localizationManager;
            if (lm == null) { Mod.log.Warn("No LocalizationManager; cannot add warning override."); return; }


            const string key = "Menu.ACHIEVEMENTS_WARNING_MODS";              // Confirmed key for string
            const string text = "Achievements Enabled by Achievement Fixer."; // or "" to fully hide

            var entries = new Dictionary<string, string> { [key] = text };

            var active = lm.activeLocaleId;
            if (!string.IsNullOrEmpty(active))
                lm.AddSource(active, new MemoryLocalizationSource(entries));

            Mod.log.Info("Installed override for Menu.ACHIEVEMENTS_WARNING_MODS.");
        }

    }
    /// <summary>In-memory localization source.</summary>
    internal sealed class MemoryLocalizationSource : IDictionarySource
    {
        private readonly Dictionary<string, string> m_Entries;
        public MemoryLocalizationSource(Dictionary<string, string> entries) { m_Entries = entries; }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts) => m_Entries;
        public void Unload() { }
    }
}
