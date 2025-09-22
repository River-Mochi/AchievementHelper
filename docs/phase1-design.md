# Achievement Helper

## Goal - Phase 1
- Keep achievements working in Cities Skylines II even when the game is started with mods.
- Active mods (not assets) will normally disqualify a save game for acheivement completion.

## Options Menu
- Toggle ☑ lets players disable the mod even if it's still installed.

## Non-goals Phase 1
- This mod does not automatically give you the achievement, you still need to do the natural things required, e.g., build 10 parks in a single city to get "Groundskeeper".
- Changing the in-game UI text that claims “Achievements disabled…” (that’s a separate UI-side issue).
- Touching the built-in Achievements tab or unlocking achievements directly.
- This mod skips DLC achievements for DLCs the player does not own. We don't meddle with that.

---
<br><br>
## Methods
- Minimal, reliable, and easy to read. One setting (on/off), a short “assert window” after load to guard against late flips, optional watchdog for rare reports, and clear logging.
- When a save finishes loading, then **enable** `PlatformManager.instance.achievementsEnabled = true`.
- For a short period (default **~10 seconds** ≈ 600 frames), then **assert once per frame** to keep the flag `true`.
- If the flag has been `true` for **30 consecutive frames** (stable), then **end early**.
- Optional **watchdog** can continue to enforce afterwards (internal, off by default, can be enabled if anyone reports a problem).


---

## Public surface

- **Namespace:** `AchievementHelper`
- **Files:**
  - `Mod.cs` — mod entry, logs, loads settings, adds `LocaleEN`, and registers our system.
  - `Settings.cs` — `EnableAchievements` toggle (default ON)
  - `Locale/LocaleEN.cs` — English strings for the Settings UI.
  - `AchievementHelperSystem.cs` — assert-window logic (inherits `GameSystemBase`).

---

## Key types & APIs (dnSpyEX research)

| Type / Member | Kind | Why we use it |
|---|---|---|
| `GameSystemBase` | Base class | to hook game lifecycle and `OnUpdate()`. |
| `GameSystemBase.OnGameLoadingComplete(Purpose, GameMode)` | Method | Best moment to start short assert window. |
| `GameSystemBase.OnUpdate()` | Method | Runs every frame; we enforce during the window. |
| `Colossal.PSI.Common.PlatformManager.instance` | Singleton | Holds `achievementsEnabled` |
| `PlatformManager.achievementsEnabled : bool` | Field/prop | single flag that disables/enables achievements backends. |
| `LogManager.GetLogger(...).SetShowsErrorsInUI(false)` | Logging | Traceable uses \Logs\modName.log |
| `GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(...))` | Localization | Register English strings. |
| `ModSetting` + `SettingsUI*` attributes | Settings UI | Build the toggle & About info without custom UI. |
| `AssetDatabase.global.LoadSettings(modName, setting, new Setting(mod))` | Settings | Persist user settings between sessions. |

> **Note:** Game code includes `Colossal.PSI.Common.AchievementsHelper` (plural). Our namespace `AchievementHelper` (singular) is distinct; no conflict.

---

## Algorithm

Let:
- `kAssertFrames = 600` (≈10s @ 60 FPS)
- `kStableFramesToExit = 30`
- `kWatchdogAfterWindow = false`

```text
OnGameLoadingComplete:
  if !Settings.EnableAchievements -> return
  framesLeft = kAssertFrames
  stableTrueFrames = 0
  ForceEnableIfNeeded("OnGameLoadingComplete")
  log: "Assert window started..."

OnUpdate:
  if !Settings.EnableAchievements -> return
  if framesLeft > 0:
    hadToFlip = ForceEnableIfNeeded("OnUpdate")
    if hadToFlip: stableTrueFrames = 0
    else: stableTrueFrames = min(stableTrueFrames + 1, kStableFramesToExit)
    if stableTrueFrames >= kStableFramesToExit:
      log: "Early-exit: stable TRUE for 30 frames"
      framesLeft = 0
      return
    framesLeft--
    if framesLeft % 60 == 0: log.debug heartbeat
  else if kWatchdogAfterWindow:
    ForceEnableIfNeeded("Watchdog")

ForceEnableIfNeeded(source):
  if PlatformManager.instance == null:
    log.debug($"{source}: PlatformManager null; skip")
    return false
  if !PlatformManager.instance.achievementsEnabled:
    log.warn($"{source}: Detected FALSE; forcing TRUE")
    PlatformManager.instance.achievementsEnabled = true
    return true
  return false
