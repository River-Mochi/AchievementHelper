using System;
using System.Collections.Generic;
using Colossal.PSI.Common;   // PlatformManager
using Game.SceneFlow;        // GameManager (for localization if you later resolve names)

namespace AchievementHelper
{
    internal static class AchievementsBridge
    {
        /// <summary>
        /// Fill 'available' (locked) and 'completed' (unlocked) with names.
        /// Returns true if real data was gathered, false if not available on this build.
        /// </summary>
        public static bool TryBuildLists(out List<string> available, out List<string> completed)
        {
            available = new List<string>();
            completed = new List<string>();

            try
            {
                var pm = PlatformManager.instance;
                if (pm == null)
                    return false;

                // --- TODO: Wire to the real APIs you see in dnSpyEX ---
                // Typical pattern you’ll find:
                //   foreach (var ach in pm.achievementsBackend.GetAll()) {
                //       bool unlocked = pm.achievementsBackend.IsUnlocked(ach.Id);
                //       string displayName = Localize(ach.NameKey) or ach.DisplayName;
                //       (unlocked ? completed : available).Add(displayName);
                //   }
                //
                // Keep it defensive: if you only get IDs, add IDs. If you only have keys, localize via
                // GameManager.instance.localizationManager?.Get(...)
                //
                // For now we return false so the caller knows it’s not wired yet.
                return false;
            }
            catch (Exception ex)
            {
                Mod.log.Warn($"Achievements enumeration failed: {ex.Message}");
                return false;
            }
        }
    }
}
