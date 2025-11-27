# Sonic Heroes Archipelago Changelog


===================================================
===================================================

## Version 2.0.1 (APWorld and Mod)

## APWorld

### Additions
- Added Item and Location Groups
- Added back Trap Fill Option to enable trap items

### Changes
- Renamed a few choices for options THIS IS A BREAKING CHANGE FOR YAMLS MAKE SURE TO REGENERATE

### Fixes
- Rocket Accel now correctly requires a second character logically

### Refactors
- Changed Normal Location type to Level for Location Group Support (this does not impact generation)

## Mod

### Additions
- Updated README with information about the Version and Music Shuffle

### Changes
- Level Completions now checks for if the location was sent instead of locally saving it. (also attempt to handle the possible race condition) (please let me know if there are issues unlocking the final boss with this)
- Team Blast Filler now Gives 1 Ring (increased to 10 if Team Blast is not possible)

### Improvements
- Level Select UI for Final Boss now no longer displays Emeralds if they are not needed for goal (and therefore not in the itempool)
- Certain Items (Character, Ability, and Obj) no longer flip bool value in save when received and instead always set to true (debug flag)

### Fixes
- Final Boss Now Requires All Characters to unlock (this was already logically required in APWorld)
- Fix for forward slashes is Music Shuffle Folder file paths (Linux) Make sure to use the folder picker button to properly select the file path
- BugFix for SECRET Showing in Level Select UI when not supposed to

### Refactors
- Mod Save now defaults SpecialStage, Boss, and FinalBoss Abilities to true (this does not impact gameplay as these regions ignore unlock status)

===================================================
===================================================

## Version 2.0.0 (APWorld and Mod) (Sonic Story Only)

## Both APWorld and Mod

### Additions
- Character and Ability Lockout via items (with extensive logic routing)

### Removals
- Removed Old Level Gate design

## APWorld

### Removals
- Removed old options as the new design does not support them yet 

### Refactors
- Extensive Refactor of entire codebase to support new design (>15000 lines)


## Mod

### Additions
- Music Randomizer (supports Sonic Heroes, Sonic Adventure DX, Sonic Adventure 2 Battle, and Custom ADX files) (extra setup required)

### Improvements
- Level Select UI Now Works In-Game (while paused)
- Save File is now a json for easier reading
- Deathlink/Ringlink and Music Shuffle Options are allowed to be changed while connected and will update automatically (make sure to close the config window)

### Fixes
- Should no longer drop items on reconnect (Off By 1 Error)
- Hopefully no longer crashes on disconnect (moved some initializers out of reconnect loop) 
- Deathlink should no longer double send in specific race conditions 
- Lower Priority Checkpoints are allowed to be gotten after a higher priority one

===================================================
===================================================
## Version 1.5.1 (Mod)

## Mod

### Additions
- Added Mod Option to disable playing sounds when receiving items or Ringlink Packets


### Fixes
- Various Crashes around Dark, Rose, and Chaotix sanity checks are now fixed
- SuperHard Egg Fleet Checkpoint 5 now has the correct priority

===================================================
===================================================

## Version 1.5.0 (APWorld and Mod)


## Both APWorld and Mod

### Fixes
- Chaotix Rail Canyon Bonus Key 3 removed (out of bounds)
- Chaotix Bullet Station Bous Keys 2 and 3 swapped (wrong order)
- Rose Lost Jungle Checkpoint 1 removed (out of bounds)
- Rose Hang Castle Checkpoint 3 removed (out of bounds)
- Rose Egg Fleet Checkpoint 1 added (missing)
- Chaotix Lost Jungle Checkpoints 1 and 4 removed (out of bounds)
- Chaotix Mystic Mansion Checkpoint 3 removed (out of bounds)


## Mod

### Fixes
- Required Rank now correctly requires the rank (not the rank below the selected one)

===================================================
===================================================

## Version 1.4.0 (APWorld and Mod)


## Both APWorld and Mod

### Additions
- Checkpoint Sanity

### Fixes
- Rose Grand Metropolis Bous Key 2 added (missing)
- Chaotix Seaside Hill Bonus Key 3 added (missing)


## APWorld

### Removals
- Removed Extra Emblems option (pending rework)

### Changes
- SuperHard Mode uses different location names (instead of Sonic Act 2)

## Mod

### Additions
- Level Select UI Checkpoint Sanity
- Level Select UI now displays on both sides for Key and Checkpoint Sanity when generated with Only 1 Set


===================================================
===================================================

## Version 1.3.1 (Mod)

## Mod

### Fixes
- Version Checker should now correctly work

===================================================
===================================================

## Version 1.3.0 (APWorld and Mod)


## Both APWorld and Mod

### Additions
- Key Sanity
- SuperHard Mode (accessed via Sonic Act B when enabled)

## APWorld

### Changes
- Normal Goal Unlock Condition renamed to Both

## Mod

### Additions
- Level Select UI Key Sanity
- Version Checker (checks that Mod and APWorld versions are compatible)

### Fixes
- Level Select UI for Metal Madness should now correctly show completion status for Metal Madness
- Metal Madness should now correctly open in Level Select on reload (hopefully)



===================================================
===================================================

## Version 1.2.1 (APWorld and Mod)

## Known Issues
- Extra Emblems values aboove 0 can cause generation to fail when the new Sanity Excluded Percent option is high

## Apworld

### Additions
- New Option to Exclude a percentage of sanity locations (to help with balancing a large number of checks in syncs)

### Fixes
- 0 level gates should now properly generate
- Extended hint info for Metal Overlord location when set to emerald only and more than 0 level gates should now work

===================================================
===================================================

## Version 1.2.0 (APWorld and Mod)

## Both APWorld and Mod

### Versioning Changes
- APWorld and Mod now increment minor versions together (so 1.2.x now matches 1.2.x for each other)
- Patch number increases signify backwards compatible changes while minor version increases signify breaking changes (THIS IS DIFFERENT FROM SEMVAR)

### Changes
- Option Rework (now each Act can be enabled or disabled per story)


## APWorld

### Additions
- Added a single unit test

### Changes
- Additional Levels are added to early gates if needed to prevent Fill Errors


## Mod

### Additions
- UI for active traps

### Fixes
- Complete Level hook no longer has an invalid call
- Level Select UI bosses should now show completed correctly


===================================================
===================================================

## Version 1.1.9 (APWorld) (goes with Mod Version 1.1.3)

## APWorld

### Additions
- New Option Emblem Pool Size (how many Emblems are added to item pool per Act enabled per story)

### Changes
- Extra Emblems are no longer used in calculating gate costs (was causing issues with Chaotix Sanity)

### Fixes
- Generating with multiple players should now work correctly due to location refactor

### Refactors
- Full rewrite of location handling


===================================================
===================================================

## Version 1.1.8 (APWorld) (goes with Mod Version 1.1.3)

## APWorld

### Additions
- New Option to change Location Type for emerald stages (Priority, Normal, or Excluded)

### Changes
- Level Gate cap for 1 story only removed
- Update ModVersion in SlotData for Mod (from 1.1.2 to 1.1.3)

===================================================
===================================================

## Version 1.1.7 (APWorld) (goes with Mod Version 1.1.2)

## APWorld

### Changes
- Update ModVersion in SlotData for Mod (from 1.1.1 to 1.1.2)

===================================================
===================================================

## Version 1.1.6 (APWorld) (goes with Mod Version 1.1.1)

## APWorld

### Changes
- Update ModVersion in SlotData for Mod (from 1.1.0 to 1.1.1)

===================================================
===================================================

## Version 1.1.5 (APWorld) (goes with Mod Version 1.1.0)

## APWorld

### Changes
- Max Level Gates increased to 7
- Max Extra Emblems increased to 900
- Update ModVersion in SlotData for Mod (from 100 to 1.1.0)

===================================================
===================================================

## Version 1.1.4 (APWorld) (goes with Mod Version 1.0.2)

## APWorld

### Changes
- Limit Max Extra Emblems to 150 until Mod supports it

===================================================
===================================================

## Version 1.1.3 (APWorld and Mod) (APWorld 1.1.3 goes with Mod 1.0.2)

## APWorld

### Fixes
- Shuffleable Boss list now truncates properly


## Mod

### Additions
- Various funny death messages for deathlink
- New Option to control the In-game log (how long messages appear)

### Fixes
- In-game log now scrolls fluidly
- Sending many location checks in a short time should no longer cause freezing (thanks to refactor)

### Refactors
- Refactor of location sending (uses separate thread)

===================================================
===================================================

## Version 1.1.2 (APWorld and Mod) (APWorld 1.1.2 goes with Mod 1.0.2)

## APWorld

### Changes
- Max Level Gates lowered to 5


## Mod

### Fixes
- Hinting with incorrect case should no longer crash

===================================================
===================================================

## Version 1.1.1 (APWorld and Mod) (APWorld 1.1.1 goes with Mod 1.0.2)

## APWorld

### Fixes
- Generation error with team indexes should now be fixed

## Mod

### Additions
- new Options for In-game log (how many max messages, how much delay between each message)

### Fixes
- Level Select UI now correctly displays Act 2 completion
- Off by one error for sanity checks in Level Select UI is now fixed
- Log messages should no longer disappear too fast if cached

===================================================
===================================================

## Version 1.1.0 (APWorld and Mod) (APWorld 1.1.0 goes with Mod 1.0.2)

## APWorld

### Additions
- Extended Hint Info (extra info when something is hinted)
- Extra Emblems option (this adds extra emblems to the item pool)

### Refactors
- Locations refactored to support these changes


## Mod

### Additions
- Level Select UI

### Changes
- Mod now supports up to 7 level gates
- Mod now supports emblems above 255 (ushort to int)

### Fixes
- Items should no longer be dropped
- Metal Madness should now unlock correctly on emeralds only unlock condition

===================================================
===================================================

## Version 1.0.2 (APWorld and Mod)

## APWorld

### Fixes
- Stealth trap should no longer be in the item pool when trap fill is set to 0


## Mod

### Additions
- Freeze trap, No Swap trap, and Stealth trap now play a sound when they start (and stealth trap plays a sound when it ends)

### Fixes
- Dark, Rose, and Chaotix enemy and ring checks should no longer be skipped
- Chaotix Rail Canyon should now properly send completion check
- No Swap Trap should now properly prevent leader swapping
- Playing a sound during a transition should no longer crash

===================================================
===================================================

## Version 1.0.1 (APWorld and Mod)

## APWorld

### Additions
- New Options for individual trap weights

## Mod

### Removals
- Various debug prints are now removed

### Changes
- SHAP Native Caller is now in the mod (SHAP Native Caller is a helper library used to call specific game functions)

### Fixes
- Dont Lose Bonus Key option set to false should no longer crash
- Dark Sanity should now correctly send checks on certain enemies

===================================================
===================================================

## Version 1.0.0 (APWorld and Mod)

## Initial Release

===================================================
===================================================