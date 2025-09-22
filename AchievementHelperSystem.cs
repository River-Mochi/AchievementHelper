using Colossal.PSI.Common;        // PlatformManager
using Game;                       // GameSystemBase, Purpose, GameMode
using Unity.Entities;             // WorldSystemFilter
using Colossal.Serialization.Entities; // Purpose enum

namespace Mod.AchievementHelper
{
    /// <summary>
    /// Re-enables achievements after a load and keeps them TRUE during a short assert window.
    /// Window auto-ends early after some consecutive stable frames.
    /// </summary>
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
    public partial class AchievementHelperSystem : GameSystemBase
    {
        // --- Tunables ---
        private const int kAssertFrames = 600; // ~10s @ 60 FPS; scales with FPS
        private const int kStableFramesToExit = 30;  // early-exit once we see this many frames already TRUE
        private const bool kWatchdogAfterWindow = false; // set true only if you see very late flips in the wild

        // --- State ---
        private int m_FramesLeft;
        private int m_StableTrueFrames;

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

            // If settings aren’t ready, act as if enabled (defensive)
            if (Mod.Settings != null && !Mod.Settings.EnableAchievements)
            {
                Mod.log.Info("Settings.EnableAchievements = false; leaving achievements disabled for this session.");
                m_FramesLeft = 0;
                m_StableTrueFrames = 0;
                return;
            }

            m_FramesLeft = kAssertFrames;
            m_StableTrueFrames = 0;

            // Flip on immediately at load-complete (late flips happen around here)
            ForceEnableIfNeeded("OnGameLoadingComplete");
            Mod.log.Info($"Assert window started: {kAssertFrames} frames; early-exit after {kStableFramesToExit} stable frames.");
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings != null && !Mod.Settings.EnableAchievements)
                return;

            if (m_FramesLeft > 0)
            {
                bool hadToFlip = ForceEnableIfNeeded("OnUpdate");

                if (hadToFlip)
                {
                    // We caught an off->on event; reset stability
                    m_StableTrueFrames = 0;
                }
                else
                {
                    if (m_StableTrueFrames < kStableFramesToExit)
                        m_StableTrueFrames++;
                }

                if (m_StableTrueFrames >= kStableFramesToExit)
                {
                    Mod.log.Info($"Early-exit: achievementsEnabled stable for {kStableFramesToExit} frames.");
                    m_FramesLeft = 0;
                    return;
                }

                m_FramesLeft--;

                // Low-noise heartbeat ~once per second at 60 FPS
                if (m_FramesLeft % 60 == 0)
                    Mod.log.Debug($"Asserting… {m_FramesLeft} frames left (stable={m_StableTrueFrames})");
            }
            else if (kWatchdogAfterWindow)
            {
                // Optional long-tail protection
                ForceEnableIfNeeded("Watchdog");
            }
        }

        /// <summary>
        /// Ensures achievementsEnabled is true. Returns true if we had to flip it.
        /// Also logs clearly when we detect a BAD flip to false.
        /// </summary>
        private static bool ForceEnableIfNeeded(string source)
        {
            var pm = PlatformManager.instance;
            if (pm == null)
            {
                Mod.log.Debug($"{source}: PlatformManager.instance == null; skip");
                return false;
            }

            if (!pm.achievementsEnabled)
            {
                // This indicates the game or another mod flipped it OFF.
                Mod.log.Warn($"{source}: Detected achievementsEnabled == FALSE (this disables Steam/PDX achievements). Forcing TRUE.");
                pm.achievementsEnabled = true;
                return true;
            }

            return false; // already true
        }
    }
}
