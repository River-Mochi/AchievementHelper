using System;
using System.Collections.Generic;
using Colossal.PSI.Common;  // PlatformManager, AchievementsHelper, AchievementId, AchievementAttribute

namespace AchievementHelper
{
    /// <summary>
    /// Tiny helper that asks the game for its achievements, splits them into
    /// Available vs Completed, and returns human-ish names (best-effort).
    /// </summary>
    internal static class AchievementsBridge
    {
        public static bool TryBuildLists(out List<string> available, out List<string> completed)
        {
            available = new List<string>();
            completed = new List<string>();

            try
            {
                var pm = PlatformManager.instance;
                if (pm == null)
                {
                    Mod.log.Warn("AchievementsBridge: PlatformManager.instance is null.");
                    return false;
                }

                // Static map id -> metadata (includes internalName)
                var map = AchievementsHelper.InitializeAchievements(); // may be null in rare cases

                foreach (var a in pm.EnumerateAchievements())
                {
                    string label = ResolveName(a.id, map);
                    if (a.achieved) completed.Add(label);
                    else available.Add(label);
                }

                available.Sort(StringComparer.OrdinalIgnoreCase);
                completed.Sort(StringComparer.OrdinalIgnoreCase);
                return true;
            }
            catch (Exception ex)
            {
                Mod.log.Warn($"AchievementsBridge: {ex.GetType().Name}: {ex.Message}");
                return false;
            }
        }

        private static string ResolveName(AchievementId id, Dictionary<AchievementId, AchievementAttribute> map)
        {
            if (map != null && map.TryGetValue(id, out var attr) && !string.IsNullOrEmpty(attr.internalName))
                return attr.internalName;

            // Fallback—show id as string if no metadata
            return id.ToString();
        }
    }
}
