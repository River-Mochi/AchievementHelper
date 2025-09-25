using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Colossal.IO.AssetDatabase;
using Colossal.PSI.Common;
using Game.Modding;
using Game.Settings;
using UnityEngine;

namespace AchievementHelper
{
    [FileLocation("AchievementHelper")]
    [SettingsUIGroupOrder(MainGroup, NotCompleteGroup, CompletedGroup)]
    [SettingsUIShowGroupName(MainGroup, NotCompleteGroup, CompletedGroup)]
    public class Settings : ModSetting
    {
        // Tabs / groups
        public const string MainTab = "Main";
        public const string AboutTab = "About";
        public const string DebugTab = "Debug";

        public const string MainGroup = "Settings";
        public const string NotCompleteGroup = "Not Complete";
        public const string CompletedGroup = "Completed";

        public const string InfoGroup = "Info";
        public const string ButtonGroup = "Links";
        public const string FiltersGroup = "Filters";
        public const string DebugButtons = "Actions";

        private const string UrlAchievementsWiki = "https://cs2.paradoxwikis.com/Achievements";

        public Settings(IMod mod) : base(mod) { }

        // Main toggle
        [SettingsUISection(MainTab, MainGroup)]
        public bool EnableAchievements { get; set; } = true;

        // Read-only lists
        private string m_NotCompleteText = "—";
        private string m_CompletedText = "—";

        [SettingsUISection(MainTab, NotCompleteGroup)]
        public string NotCompleteList => m_NotCompleteText;

        [SettingsUISection(MainTab, CompletedGroup)]
        public string CompletedList => m_CompletedText;

        public void SetAchievementLists(IEnumerable<string> notComplete, IEnumerable<string> completed)
        {
            static string Bullets(IEnumerable<string> lines) =>
                lines == null ? "—" :
                string.Join("\r\n", lines.Select(Titleize)
                                         .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                                         .Select(x => $"• {x}"));

            m_NotCompleteText = Bullets(notComplete);
            m_CompletedText = Bullets(completed);
        }

        // About tab
        [SettingsUISection(AboutTab, InfoGroup)]
        public string NameDisplay => Mod.Name;

        [SettingsUISection(AboutTab, InfoGroup)]
        public string VersionDisplay => Mod.VersionShort;

        [SettingsUIButtonGroup(ButtonGroup)]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, ButtonGroup)]
        public bool OpenAchievementsWikiButton
        {
            set
            {
                try { Application.OpenURL(UrlAchievementsWiki); }
                catch (Exception ex) { Mod.log.Warn($"Failed to open wiki: {ex.Message}"); }
            }
        }

        // Debug tab — dropdown + actions
        [SettingsUISection(DebugTab, DebugButtons)]
        [SettingsUIDropdown(typeof(Settings), nameof(GetAchievementChoices))]
        public string SelectedAchievement { get; set; } = "";

        [SettingsUIButton]
        [SettingsUISection(DebugTab, DebugButtons)]
        public bool ClearSelectedAchievement
        {
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(SelectedAchievement))
                    {
                        Mod.log.Warn("ClearSelectedAchievement: nothing selected.");
                        return;
                    }
                    if (!TryResolveAchievementId(SelectedAchievement, out var id))
                    {
                        Mod.log.Warn($"ClearSelectedAchievement: could not resolve '{SelectedAchievement}'.");
                        return;
                    }

                    var pm = PlatformManager.instance;
                    if (pm == null)
                    {
                        Mod.log.Warn("ClearSelectedAchievement: PlatformManager.instance is null.");
                        return;
                    }

                    pm.ClearAchievement(id);
                    Mod.log.Info($"Requested clear of achievement: {SelectedAchievement} ({id}).");
                }
                catch (Exception ex)
                {
                    Mod.log.Warn($"ClearSelectedAchievement failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        [SettingsUIButton]
        [SettingsUISection(DebugTab, DebugButtons)]
        public bool ResetAllAchievements
        {
            set
            {
                try
                {
                    var pm = PlatformManager.instance;
                    if (pm == null)
                    {
                        Mod.log.Warn("ResetAllAchievements: PlatformManager.instance is null.");
                        return;
                    }

                    pm.ResetAchievements();
                    Mod.log.Info("Requested RESET of ALL platform achievements.");
                }
                catch (Exception ex)
                {
                    Mod.log.Warn($"ResetAllAchievements failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        public static IEnumerable<string> GetAchievementChoices()
        {
            var pm = PlatformManager.instance;
            if (pm == null) yield break;

            var meta = AchievementsHelper.InitializeAchievements();
            var set = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var a in pm.EnumerateAchievements())
            {
                var internalName =
                    (meta != null && meta.TryGetValue(a.id, out var attr) && !string.IsNullOrEmpty(attr.internalName))
                        ? attr.internalName
                        : a.id.ToString();

                set.Add(Titleize(internalName));
            }

            foreach (var display in set)
                yield return display;
        }

        private static bool TryResolveAchievementId(string display, out AchievementId id)
        {
            id = default;
            var pm = PlatformManager.instance;
            if (pm == null) return false;

            var meta = AchievementsHelper.InitializeAchievements();

            foreach (var a in pm.EnumerateAchievements())
            {
                var internalName =
                    (meta != null && meta.TryGetValue(a.id, out var attr) && !string.IsNullOrEmpty(attr.internalName))
                        ? attr.internalName
                        : a.id.ToString();

                if (string.Equals(Titleize(internalName), display, StringComparison.OrdinalIgnoreCase))
                {
                    id = a.id;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Insert spaces at CamelCase / digit boundaries and normalize gaps.
        /// Keeps original casing (simple and predictable).
        /// </summary>
        internal static string Titleize(string internalName)
        {
            if (string.IsNullOrWhiteSpace(internalName)) return internalName ?? "";
            var spaced = Regex.Replace(internalName, "(?<!^)(?=[A-Z0-9])", " ").Trim();
            return Regex.Replace(spaced, "\\s{2,}", " ");
        }

        public override void SetDefaults()
        {
            EnableAchievements = true;
            m_NotCompleteText = "—";
            m_CompletedText = "—";
            SelectedAchievement = "";
        }
    }
}
