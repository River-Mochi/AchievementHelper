using System;
using System.Collections.Generic;
using System.Reflection;               // Assembly attributes
using Colossal;                        // IDictionarySource
using Colossal.IO.AssetDatabase;       // AssetDatabase
using Colossal.Logging;                // ILog, LogManager
using Colossal.PSI.Common;             // PlatformManager, AchievementsHelper
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

        // Version/name  from assembly (.csproj)
        private static readonly Assembly s_Asm = Assembly.GetExecutingAssembly();
        public static readonly string Name =
            s_Asm.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "Achievement Helper";

        private static readonly string s_InfoRaw =
            s_Asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "1.0.0";

        public static readonly string VersionShort = s_InfoRaw.Split(' ', '+')[0];
        public static readonly string VersionInformational = s_InfoRaw;

        private static bool s_BannerLogged;

        // Locales to attach the override source to
        private static readonly string[] s_LocaleIds =
        {
            "en-US","fr-FR","de-DE","es-ES","it-IT","ja-JP","ko-KR","vi-VN","zh-HANS"
        };

        // ---- Lifecycle ----
        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (!s_BannerLogged)
            {
                log.Info($"{Name} {VersionShort} (info: {VersionInformational})");
                s_BannerLogged = true;
            }

            var settings = new Settings(this);
            Settings = settings;

            // Locales (register BEFORE Options UI)
            AddLocale("en-US", new LocaleEN(settings));
            //  AddLocale("fr-FR", new LocaleFR(settings));
            //  AddLocale("de-DE", new LocaleDE(settings));
            //  AddLocale("es-ES", new LocaleES(settings));
            //  AddLocale("it-IT", new LocaleIT(settings));
            //  AddLocale("ja-JP", new LocaleJA(settings));
            //  AddLocale("ko-KR", new LocaleKO(settings));
            //  AddLocale("vi-VN", new LocaleVI(settings));
            //  AddLocale("zh-HANS", new LocaleZH_CN(settings));

            // Hide/replace in game Achievements tab warning via locale override
            TryInstallWarningOverrideSource();

            // Load saved settings
            AssetDatabase.global.LoadSettings("AchievementHelper", settings, new Settings(this));

            // Build lists once at load
            if (TryBuildAchievementLists(out var notComplete, out var completed))
                settings.SetAchievementLists(notComplete, completed);

            // Options UI
            settings.RegisterInOptionsUI();

            // Keep achievements enabled after triggers run
            updateSystem.UpdateAfter<AchievementHelperSystem, AchievementTriggerSystem>(SystemUpdatePhase.MainLoop);

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

        // ---- Helpers ----
        private static void AddLocale(string localeId, IDictionarySource source)
        {
            var lm = GameManager.instance?.localizationManager;
            if (lm != null) lm.AddSource(localeId, source);
            else log.Warn($"LocalizationManager null; cannot add locale '{localeId}'.");
        }

        /// <summary>
        /// Tiny in-memory localization source that hides the Achievements warnings.
        /// </summary>
        private static void TryInstallWarningOverrideSource()
        {
            try
            {
                var lm = GameManager.instance?.localizationManager;
                if (lm == null) { log.Warn("No LocalizationManager; cannot add warning override."); return; }

                var entries = new Dictionary<string, string>
                {
                    // Primary warnings -> hidden (empty)
                    { "_c.Menu.ACHIEVEMENTS_WARNING_MODS",           "" },
                    { "_c.Menu.ACHIEVEMENTS_WARNING_GAME_OPTIONS",   "" },
                    { "_c.Menu.ACHIEVEMENTS_WARNING_DEBUGMENU",      "" },
                    // Past / readonly variants -> also hidden
                    { "_c.Menu.ACHIEVEMENTS_WARNING_PAST_MODS",      "" },
                    { "_c.Menu.ACHIEVEMENTS_WARNING_PAST_OPTIONS",   "" },
                    { "_c.Menu.ACHIEVEMENTS_WARNING_PAST_DEBUGMENU", "" },
                    { "_c.Menu.ACHIEVEMENTS_WARNING_READONLY",       "" },
                };

                // Add for active and common locales (last-added source wins)
                var active = lm.activeLocaleId;
                if (!string.IsNullOrEmpty(active))
                    lm.AddSource(active, new MemoryLocalizationSource(entries));

                foreach (var id in s_LocaleIds)
                    lm.AddSource(id, new MemoryLocalizationSource(entries));

                log.Info("Installed localization override for Achievements warnings.");
            }
            catch (Exception ex)
            {
                log.Warn($"Warning override failed: {ex.GetType().Name}: {ex.Message}");
            }
        }

        /// <summary>
        /// Split into NotComplete / Complete names
        /// </summary>
        private static bool TryBuildAchievementLists(out List<string> notComplete, out List<string> completed)
        {
            notComplete = new();
            completed = new();

            try
            {
                var pm = PlatformManager.instance;
                if (pm == null)
                {
                    log.Warn("PlatformManager.instance is null.");
                    return false;
                }

                var meta = AchievementsHelper.InitializeAchievements(); // id -> AchievementAttribute (may be null)

                foreach (var a in pm.EnumerateAchievements())
                {
                    var internalName =
                        (meta != null && meta.TryGetValue(a.id, out var attr) && !string.IsNullOrEmpty(attr.internalName))
                            ? attr.internalName
                            : a.id.ToString();

                    var display = Settings.Titleize(internalName);

                    if (a.achieved) completed.Add(display);
                    else notComplete.Add(display);
                }

                notComplete.Sort(StringComparer.OrdinalIgnoreCase);
                completed.Sort(StringComparer.OrdinalIgnoreCase);
                return true;
            }
            catch (Exception ex)
            {
                log.Warn($"TryBuildAchievementLists: {ex.GetType().Name}: {ex.Message}");
                return false;
            }
        }
    }

    /// <summary>Minimal in-memory localization source to overriding specific keys.</summary>
    internal sealed class MemoryLocalizationSource : IDictionarySource
    {
        private readonly Dictionary<string, string> m_Entries;
        public MemoryLocalizationSource(Dictionary<string, string> entries) { m_Entries = entries; }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts) => m_Entries;
        public void Unload() { }
    }
}
