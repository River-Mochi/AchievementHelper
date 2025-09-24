# Achievement Helper

## Goal - Phase 1
- Keep achievements working in Cities Skylines II even when the game is started with mods.
- Active mods (not assets) will normally disqualify a save game for achievement completion.

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
- Optional **watchdog** can continue to enforce afterwards (internal code, off by default, can be enabled if anyone reports a problem).

---

## Project Layout (files & classes)

- **Namespace:** `AchievementHelper`
- **Files:**
  - `Mod.cs` — main mod entry point, logs, loads settings, adds `LocaleEN`, and registers the System to run.
  - `Settings.cs` — `EnableAchievements` toggle (default ON)
  - `Locale/LocaleEN.cs` — English strings for Settings UI.
  - `AchievementHelperSystem.cs` — after loading, checks again to ensure achievements are still enabled (inherits `GameSystemBase`).

---

## Key types & APIs (dnSpyEX research)

| Type / Member | Kind | Why we use it |
|---|---|---|
| `GameSystemBase` | Base class | to hook game lifecycle and `OnUpdate()`. |
| `GameSystemBase.OnGameLoadingComplete(Purpose, GameMode)` | Method | Best moment to start short assert window. |
| `GameSystemBase.OnUpdate()` | Method | Runs every frame; we enforce during the window. |
| `Colossal.PSI.Common.PlatformManager.instance` | Singleton | Holds `achievementsEnabled`. |
| `PlatformManager.achievementsEnabled : bool` | Field/prop | single flag that disables/enables achievements backends. |
| `LogManager.GetLogger(...).SetShowsErrorsInUI(false)` | Logging | Traceable uses \Logs\modName.log |
| `GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(...))` | Localization | Register English strings. |
| `ModSetting` + `SettingsUI*` attributes | Settings UI | build checkbox toggles & Options menu without a custom UI. |
| `AssetDatabase.global.LoadSettings(modName, setting, new Setting(mod))` | Settings | Persist user settings between sessions. |
| `Game.UI.InGame.AchievementsUISystem` | UISystemBase | Builds the Achievements tab UI, wires bindings. |
| `AchievementsUISystem.GetAchievementTabStatus()` | Method | Decides which warning state (Available, Hidden, ModsDisabled, OptionsDisabled) is shown. |
| `GetterValueBinding<int>("achievements", "achievementTabStatus", ...)` | Binding | Exposes tab status to the UI. |
| `IAchievement` / `PlatformManager.instance.EnumerateAchievements()` | API | Enumerates names, descriptions, progress, and icons. |
| `_c.Menu.ACHIEVEMENTS_WARNING_*` keys | Localization keys | Text for warning messages (“disabled because mods…”, etc.). |
| `Media/Game/Achievements/*.png` | Assets | Icons used in the Achievements tab. Color = achieved; `_locked` = grayscale locked state. |
| `CityConfigurationSystem.usedMods.Count` | Field | Used by `AchievementsUISystem.GetAchievementTabStatus()` to decide ModsDisabled status. |
| `Achievements` (static IDs) | Data | Contains all achievement IDs. |
| `AchievementTriggerSystem` | System | Enforces progress, also ANDs `achievementsEnabled` with mod/option flags on load. |

> **Note:** Game code includes `Colossal.PSI.Common.AchievementsHelper` (plural). Our namespace `AchievementHelper` (singular) is distinct; no conflict.

---

## UI / Localization Hooks

- Warning messages in the Achievements tab are **not hard-coded strings**; they’re pulled from localization keys:
  - `_c.Menu.ACHIEVEMENTS_WARNING_MODS`
  - `_c.Menu.ACHIEVEMENTS_WARNING_OPTIONS`
  - `_c.Menu.ACHIEVEMENTS_WARNING_DEBUGMENU`
  - (plus PlayStation variants, not relevant on PC)

- **Override strategy:** Provide a replacement localization source (e.g., via `MemoryLocalizationSource`) that redefines those keys. This replaces the in-game warning without patching `AchievementsUISystem` directly.
  - Example override text: `“Achievements are enabled by Achievement Helper.”`
  - To hide entirely, set the override value to an empty string `""`.

- **Optional hide strategy:** Add a tiny CSS asset to hide the warning element if desired. This avoids Harmony but removes the banner visually.

- **Future extension:** Enumerating `IAchievement` objects lets the mod display a custom Achievements panel if needed (names, descriptions, progress, icons are all available).

---

**Performance**
- While active: 1–2 property checks per frame for at most ~10s (or until early exit).
- After that: zero cost (unless watchdog is enabled).

**Compatibility & Risks**
- We only set a single engine flag; extremely low chance of conflict.
- If another mod continually sets the flag to false after our short window, then enabling optional `watchdog` can catch it (hardcoded and off by default).
- We do not touch saves, achievement definitions, or DLC logic.

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
