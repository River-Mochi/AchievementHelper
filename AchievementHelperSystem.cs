using Colossal.PSI.Common;              // PlatformManager
using Colossal.Serialization.Entities;  // Purpose enum
using Game;                             // GameSystemBase, GameMode
using Unity.Entities;                   // WorldSystemFilter

namespace AchievementHelper
{
    /// <summary>
    /// Re-enables achievements after a city finishes loading and holds them TRUE
    /// for a short assert window. The window auto-ends early after N stable frames.
    /// </summary>
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
    public partial class AchievementHelperSystem : GameSystemBase
    {
        // ---- Tunables ----
        private const int kAssertFrames = 600; // ~10s @60 FPS (scales with FPS)
        private const int kStableFramesToExit = 90;  // early-exit once TRUE for this many frames
        private const bool kWatchdogAfterWindow = false; // set true only if late flips are observed

        // ---- State ----
        private int m_FramesLeft;
        private int m_StableTrueFrames;

        // ---- Lifecycle ----
        protected override void OnCreate()
        {
            base.OnCreate();
            m_FramesLeft = 0;
            m_StableTrueFrames = 0;
            Mod.log.Info("AchievementHelperSystem created");
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            // If settings exist and the user disabled us, do nothing for this session.
            if (Mod.Settings != null && !Mod.Settings.EnableAchievements)
            {
                Mod.log.Info("Settings.EnableAchievements = false; leaving achievements disabled for this session.");
                m_FramesLeft = 0;
                m_StableTrueFrames = 0;
                return;
            }

            // Start a new assert window at load-complete; flips often occur here.
            m_FramesLeft = kAssertFrames;
            m_StableTrueFrames = 0;

            ForceEnableIfNeeded("OnGameLoadingComplete");
            Mod.log.Info($"Assert window started: {kAssertFrames} frames; early-exit after {kStableFramesToExit} stable frames.");

            // If you added the achievements list UI, you can refresh it here:
            // AchievementsBridge.Invalidate();
        }

        protected override void OnUpdate()
        {
            // Respect the toggle if settings are present.
            if (Mod.Settings != null && !Mod.Settings.EnableAchievements)
                return;

            if (m_FramesLeft > 0)
            {
                bool flipped = ForceEnableIfNeeded("OnUpdate");

                if (flipped)
                {
                    // We corrected a late OFF->ON flip; restart stability count.
                    m_StableTrueFrames = 0;
                }
                else if (m_StableTrueFrames < kStableFramesToExit)
                {
                    m_StableTrueFrames++;
                }

                if (m_StableTrueFrames >= kStableFramesToExit)
                {
                    Mod.log.Info($"Early-exit: achievementsEnabled stable for {kStableFramesToExit} frames.");
                    m_FramesLeft = 0;
                    return;
                }

                m_FramesLeft--;

                // Low-noise heartbeat ~once per second at 60 FPS.
                if (m_FramesLeft % 60 == 0)
                    Mod.log.Info($"Asserting… {m_FramesLeft} frames left (stable={m_StableTrueFrames})");
            }
            else if (kWatchdogAfterWindow)
            {
                // Optional long-tail protection if you ever see very-late flips in the wild.
                ForceEnableIfNeeded("Watchdog");
            }
        }

        // ---- Helpers ----
        /// <summary>
        /// Ensures PlatformManager.achievementsEnabled is TRUE.
        /// Returns true if we had to flip it.
        /// </summary>
        private static bool ForceEnableIfNeeded(string source)
        {
            var pm = PlatformManager.instance;
            if (pm == null)
            {
                Mod.log.Info($"{source}: PlatformManager.instance == null; skip");
                return false;
            }

            if (!pm.achievementsEnabled)
            {
                // Something (game or another mod) flipped it OFF. Put it back.
                Mod.log.Warn($"{source}: Detected achievementsEnabled == FALSE (this disables Steam/PDX achievements). Forcing TRUE.");
                pm.achievementsEnabled = true;
                return true;
            }

            return false; // already true
        }
    }
}
