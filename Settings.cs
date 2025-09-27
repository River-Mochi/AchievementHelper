using System;
using System.Collections.Generic;
using System.Linq;
using Colossal.IO.AssetDatabase;  // [FileLocation]
using Colossal.PSI.Common;        // PlatformManager, AchievementId, AchievementsHelper
using Game.Modding;
using Game.Settings;
using Game.UI.Widgets;            // DropdownItem<T>
using UnityEngine;

namespace AchievementFixer
{
    [FileLocation("ModsSettings/AchievementFixer/AchievementFixer")]    // location of persistent settings
    [SettingsUIGroupOrder(
        MainInfoGroup, ButtonGroup, NotesGroup,
        AdvRowActions, AdvRowDebug
    )]
    [SettingsUIShowGroupName(
        NotesGroup,     // show NOTES on Main tab
        AdvRowDebug     // show DEBUG on Advanced tab
    )]

    public class Settings : ModSetting
    {
        // ---- Tabs / Groups ----
        public const string MainTab = "Main";
        public const string AdvancedTab = "Advanced";

        // Main tab groups
        public const string MainInfoGroup = "Info";
        public const string ButtonGroup = "Links";
        public const string NotesGroup = "Notes";

        // Advanced tab groups
        public const string AdvRowActions = "Actions";
        public const string AdvRowDebug = "Debug";

        private const string UrlAchievementsWiki = "https://cs2.paradoxwikis.com/Achievements";

        public Settings(IMod mod) : base(mod) { }

        // Main: Name / Version
        [SettingsUISection(MainTab, MainInfoGroup)]
        public string NameDisplay => Mod.Name;

        [SettingsUISection(MainTab, MainInfoGroup)]
        public string VersionDisplay => Mod.VersionShort;

        // Main: Wiki button
        [SettingsUIButtonGroup(ButtonGroup)]
        [SettingsUIButton]
        [SettingsUISection(MainTab, ButtonGroup)]
        public bool OpenAchievementsWikiButton
        {
            set
            {
                if (!value) return;
                try { Application.OpenURL(UrlAchievementsWiki); }
                catch (Exception ex) { Mod.log.Warn($"Failed to open wiki: {ex.Message}"); }
            }
        }

        // Main tab: Notes (multiline; content by Locale)
        [SettingsUIMultilineText]
        [SettingsUISection(MainTab, NotesGroup)]
        public string MainNotes => string.Empty;

        // Advanced tab: dropdown & Unlock + Clear in same row
        [SettingsUISection(AdvancedTab, AdvRowActions)]
        [SettingsUIDropdown(typeof(Settings), nameof(GetAchievementChoices))]
        public string SelectedAchievement { get; set; } = "";

        //Unlock Selected
        [SettingsUIButton]
        [SettingsUIButtonGroup(AdvRowActions)]
        [SettingsUISection(AdvancedTab, AdvRowActions)]
        public bool UnlockSelectedAchievement
        {
            set
            {
                if (!value) return;
                try
                {
                    if (!TryResolveAchievementId(SelectedAchievement, out var id))
                    {
                        Mod.log.Warn($"UnlockSelectedAchievement: could not resolve '{SelectedAchievement}'.");
                        return;
                    }

                    var pm = PlatformManager.instance;
                    if (pm == null)
                    {
                        Mod.log.Warn("UnlockSelectedAchievement: PlatformManager.instance is null.");
                        return;
                    }

                    pm.UnlockAchievement(id);
                    Mod.log.Info($"Requested UNLOCK of achievement: {SelectedAchievement} ({id}).");
                }
                catch (Exception ex)
                {
                    Mod.log.Warn($"UnlockSelectedAchievement failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        // Clear Selected (single)
        [SettingsUIButtonGroup(AdvRowActions)]
        [SettingsUIButton]
        [SettingsUIConfirmation] // Yes/No modal
        [SettingsUISection(AdvancedTab, AdvRowActions)]
        public bool ClearSelectedAchievement
        {
            set
            {
                if (!value) return; // user clicked "No"
                try
                {
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
                    Mod.log.Info($"Requested CLEAR (reset) of single achievement: {SelectedAchievement} ({id}).");
                }
                catch (Exception ex)
                {
                    Mod.log.Warn($"ClearSelectedAchievement failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        // Advanced: text under the two buttons
        [SettingsUIMultilineText]
        [SettingsUISection(AdvancedTab, AdvRowActions)]
        public string AdvancedAdvisory => string.Empty;

        // Advanced: DEBUG section (Reset All)
        [SettingsUIButton]
        [SettingsUIConfirmation]    // Yes/No Modal
        [SettingsUIButtonGroup(AdvRowDebug)]
        [SettingsUISection(AdvancedTab, AdvRowDebug)]
        public bool ResetAllAchievements
        {
            set
            {
                if (!value) return;
                try
                {
                    var pm = PlatformManager.instance;
                    if (pm == null)
                    {
                        Mod.log.Warn("ResetAllAchievements: PlatformManager.instance is null.");
                        return;
                    }

                    pm.ResetAchievements();
                    Mod.log.Info("Requested Reset of ALL platform achievements.");
                }
                catch (Exception ex)
                {
                    Mod.log.Warn($"ResetAllAchievements failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        // ---- Helpers ----

        /// <summary> Dropdown: value = internalName, display = friendly name.</summary>
        public static DropdownItem<string>[] GetAchievementChoices()
        {
            var pm = PlatformManager.instance;
            if (pm == null) return Array.Empty<DropdownItem<string>>();

            // Get metadata (internal names)
            var meta = AchievementsHelper.InitializeAchievements();
            var items = new List<DropdownItem<string>>();

            foreach (var a in pm.EnumerateAchievements())
            {
                var internalName =
                    (meta != null && meta.TryGetValue(a.id, out var attr) && !string.IsNullOrEmpty(attr.internalName))
                        ? attr.internalName
                        : a.id.ToString();

                items.Add(new DropdownItem<string>
                {
                    value = internalName,
                    displayName = AchievementDisplay.Get(internalName)
                });
            }

            return items.OrderBy(i => i.displayName.id, StringComparer.OrdinalIgnoreCase).ToArray();
        }

        private static bool TryResolveAchievementId(string selectedValue, out AchievementId id)
        {
            id = default;
            var pm = PlatformManager.instance;
            if (pm == null) return false;

            foreach (var a in pm.EnumerateAchievements())
            {
                if (string.Equals(a.id.ToString(), selectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    id = a.id;
                    return true;
                }
            }

            var meta = AchievementsHelper.InitializeAchievements();
            if (meta != null)
            {
                foreach (var kv in meta)
                {
                    if (string.Equals(kv.Value.internalName, selectedValue, StringComparison.OrdinalIgnoreCase))
                    {
                        id = kv.Key;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void SetDefaults()
        {
            SelectedAchievement = "";
        }
    }
}
