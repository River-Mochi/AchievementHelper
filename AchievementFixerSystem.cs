using Colossal.PSI.Common;              // PlatformManager
using Colossal.Serialization.Entities;  // Purpose enum
using Game;                             // GameSystemBase, GameMode
using Unity.Entities;                   // WorldSystemFilter

namespace AchievementFixer
{
    /// <summary>
    /// Keeps achievements enabled after load with a short assert window.
    /// </summary>
    [WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
    public partial class AchievementFixerSystem : GameSystemBase
    {

        private int m_FramesLeft;
        private int m_StableTrueFrames;
        private const int kAssertFrames = 180;      // ~3s @60 FPS
        private const int kStableFramesToExit = 60; // early-exit once TRUE for this many frames

        protected override void OnCreate()
        {
            base.OnCreate();
            m_FramesLeft = 0;
            m_StableTrueFrames = 0;
            Mod.log.Info("AchievementFixerSystem created");
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            // Start a new assert window at load-complete.
            m_FramesLeft = kAssertFrames;
            m_StableTrueFrames = 0;

            ForceEnableIfNeeded("OnGameLoadingComplete");
#if DEBUG
            Mod.log.Info($"Assert window started: {kAssertFrames} frames; early-exit after {kStableFramesToExit} stable frames.");
#endif
        }

        protected override void OnUpdate()
        {

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
#if DEBUG
            if (m_FramesLeft % 60 == 0)
                Mod.log.Info($"Asserting… {m_FramesLeft} frames left (stable={m_StableTrueFrames})");
#endif
        }

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
                Mod.log.Info($"{source}: ATTENTION: detected game flipped achievementsEnabled == FALSE. Forcing TRUE.");
                pm.achievementsEnabled = true;
                return true;
            }

            return false;
        }
    }
}
