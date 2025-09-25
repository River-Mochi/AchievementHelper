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
        private const int kAssertFrames = 600;        // ~10s @60 FPS
        private const int kStableFramesToExit = 60;   // early-exit once TRUE for this many frames

        // ---- State ----
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

            if (!(Mod.Settings?.EnableAchievements ?? true))
            {
                Mod.log.Info("EnableAchievements = false; leaving achievements disabled for this session.");
                m_FramesLeft = 0;
                m_StableTrueFrames = 0;
                return;
            }

            // Start a new assert window at load-complete; flips often occur here.
            m_FramesLeft = kAssertFrames;
            m_StableTrueFrames = 0;

            ForceEnableIfNeeded("OnGameLoadingComplete");
            Mod.log.Info($"Assert window started: {kAssertFrames} frames; early-exit after {kStableFramesToExit} stable frames.");
        }

        protected override void OnUpdate()
        {
            if (!(Mod.Settings?.EnableAchievements ?? true))
                return;

            if (m_FramesLeft <= 0)
                return;

            bool flipped = ForceEnableIfNeeded("OnUpdate");
            if (flipped) m_StableTrueFrames = 0;
            else if (m_StableTrueFrames < kStableFramesToExit) m_StableTrueFrames++;

            if (m_StableTrueFrames >= kStableFramesToExit)
            {
                Mod.log.Info($"Early-exit: achievementsEnabled stable for {kStableFramesToExit} frames.");
                m_FramesLeft = 0;
                return;
            }

            m_FramesLeft--;
            if (m_FramesLeft % 60 == 0)
                Mod.log.Info($"Asserting… {m_FramesLeft} frames left (stable={m_StableTrueFrames})");
        }

        /// <summary>Ensures PlatformManager.achievementsEnabled is TRUE. Returns true if we had to flip it.</summary>
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
                Mod.log.Warn($"{source}: Detected achievementsEnabled == FALSE. Forcing TRUE.");
                pm.achievementsEnabled = true;
                return true;
            }

            return false; // already true
        }
    }
}
