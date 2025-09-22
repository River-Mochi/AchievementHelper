# Achievement Helper — Phase 1 Design

**Goal:** Keep achievements working in Cities Skylines II even when the game is started with mods.

**Scope:** Minimal, reliable, and easy to read. One setting (on/off), a short “assert window” after load to guard against late flips, optional watchdog for rare reports, and clear logging.

---

## TL;DR (behavior)

- When a save finishes loading, then **force** `PlatformManager.instance.achievementsEnabled = true`.
- For a short period (default **~10 seconds** ≈ 600 frames), then **assert once per frame** to keep the flag `true`.
- If the flag has been `true` for **30 consecutive frames** (stable), then **end early**.
- Optional **watchdog** can continue to enforce afterwards (internal, off by default, can be enabled if anyone reports a problem).
- **Settings** toggle - [ ] lets players disable the behavior entirely.

---

## Non-goals (Phase 1)

- Changing the in-game UI text that claims “Achievements disabled…” (that’s a separate UI-side issue).
- Touching the built-in **Achievements catalog** or unlocking achievements directly.
- DLC ownership rules—`AchievementsHelper` skips DLC achievements you don’t own; we do not meddle with that.

---

## Public surface

- **Namespace:** `AchievementHelper`
- **Files:**
  - `Mod.cs` — mod entry, logs, loads settings, adds `LocaleEN`, and registers our system.
  - `Settings.cs` — `EnableAchievements` toggle (default ON) + a simple **About** group (version, GitHub button).
  - `Locale/LocaleEN.cs` — English strings for the Settings UI.
  - `AchievementHelperSystem.cs` — the assert-window logic (inherits `GameSystemBase`).

---

## Key types & APIs (from dnSpyEX notes)

| Type / Member | Kind | Why we use it |
|---|---|---|
| `GameSystemBase` | Base class | Lets us hook game lifecycle and `OnUpdate()`. |
| `GameSystemBase.OnGameLoadingComplete(Purpose, GameMode)` | Method | Best moment to start our short assert window. |
| `GameSystemBase.OnUpdate()` | Method | Runs every frame; we enforce during the window. |
| `Colossal.PSI.Common.PlatformManager.instance` | Singleton | Holds `achievementsEnabled`. |
| `PlatformManager.achievementsEnabled : bool` | Field/prop | The single flag that disables/enables achievements backends. |
| `LogManager.GetLogger(...).SetShowsErrorsInUI(false)` | Logging | Traceable, but no popup spam for users. |
| `GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(...))` | Localization | Register our English strings. |
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

