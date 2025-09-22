# Achievement Helper

## Goal — Phase 1
- Keep achievements working in Cities Skylines II even when the game is started with mods.
- Active mods (not assets) will normally disqualify a save game for acheivement completion.

## Option menu
- Toggle ☑ lets players disable the mod even if it's installed.

### Non-goals (Phase 1)
- This mod does not automatically give you the achievement, you still need to do the natural things required, e.g., build 10 parks in a single city to get "Groundskeeper".
- Changing the in-game UI text that claims “Achievements disabled…” (that’s a separate UI-side issue).
- Touching the built-in Achievements tab or unlocking achievements directly.
- This mod skips any possible DLC achievements for DLCs the player does not own. We don't meddle with that.
<br><br>
### Method
- One setting (on/off), a short “assert window” after load to guard against late flips, optional watchdog for rare reports, and clear logging.
- When a save finishes loading, then **enable** `PlatformManager.instance.achievementsEnabled = true`.
- For a short period (default **~10 seconds** ≈ 600 frames), then **assert once per frame** to keep the flag `true`.
- If the flag has been `true` for **30 consecutive frames** (stable), then **end early**.

---


