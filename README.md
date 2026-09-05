![AP Banner.png](AP%20Banner.png)


# Getting Started

This implementation is for the PC version of Sonic Heroes. There are 2 official releases of the PC release. One of these (CD version) has SafeDisc DRM (which requries the game disc to be inserted to launch). The other one (NoCD version) does not have this DRM but is much rarer. If you have the CD version, there is an extra setup step but should otherwise have no issues running.

## Required Software

* [Archipelago](https://github.com/ArchipelagoMW/Archipelago)
* A legally obtained copy of Sonic Heroes (PC)
* [Reloaded-II Mod Loader](https://github.com/Reloaded-Project/Reloaded-II)
* [D3D8to9](https://github.com/crosire/d3d8to9/releases)
* The [APWorld](https://github.com/Ethicallogic-Archipelago/SonicHeroesArchipelago)
* [Universal Tracker ApWorld](https://github.com/FarisTheAncient/Archipelago)
* [SafeDiscShim](https://github.com/RibShark/SafeDiscShim/releases) \[CD RELEASE\]


## Setup

### CD Version
If you have the NoCD version, you can skip this step.

SafeDisc DRM requires [SafeDiscShim](https://github.com/RibShark/SafeDiscShim/releases) to be installed. SafeDiscShim does **NOT** bypass the check for having the game disc inserted and will require the game disc to be inserted to launch.

Also, the injection of mods needs to be delayed a small amount of time in order for the DRM check to safely complete and the game memory to be unprotected. This can be accomplished by manually injecting the mods through Reloaded after the game has launched see [Delaying Mod Injection](#delaying-mod-injection).


### Reloaded

First, follow the setup for [Reloaded-II Mod Loader](https://github.com/Reloaded-Project/Reloaded-II) making sure that the game properly launches vanilla. 

This game does work well with controller but requires proper configuration for that specific controller.

### D3D8to9

In order for the in-game UI to show, add [D3D8to9](https://github.com/crosire/d3d8to9/releases) to the game directory right next to the game executable.

If on Linux, make sure to a dll override for this dll.



### Archipelago, APWorld, and Universal Tracker
Install [Archipelago](https://github.com/ArchipelagoMW/Archipelago) by following the setup guide.

After Archipelago is installed, install the The [APWorld](https://github.com/Ethicallogic-Archipelago/SonicHeroesArchipelago) and [Universal Tracker APWorld](https://github.com/FarisTheAncient/Archipelago) by adding them to the worlds folder in the Archipelago install and relaunching Archipelago at least once.

Universal Tracker is highly recommended due to the very complex logic involved in the randomization. It shows exactly what is possible at any given point during the randomizer

Explaining Archipelago and Universal Tracker is beyond scope for this setup guide. Refer to their setup guides as needed.


### Installing and Configuring Mod

After Reloaded can launch vanilla, add the AP Client Mod to the mods folder. This can also be done through Reloaded's mod browser.

In Reloaded, before launching the modded game, use the configure mod option to open a window with various options to configure. Make sure that the Host IP, Password, Port, and Slot match exactly before launching the game. 

Any non-connection related options can be edited mid-game and will take effect after the window is closed.


### Other Mods
This mod currently has a single other mod dependency:
* SH Essentials: Controller Hook by Sewer56



The list of mods that I personally use are as follows:

* TONERR by Sewer56 (for expanded One File size limit)
* Heroes Transparency Blending Fixes by Brandondorf9999
* LOD Ring Removal by DonutStopGaming
* HD Rings by Raphael Drew Boltman, SoloSlacker, and DonutStopGaming
* CRI Filesystem Hook by Sewer56
* SH Essentials: Controller Hook by Sewer56
* SH Essentials: Widescreen Revamp by Raphael Drew Boltman
* Heroes Freecam by Sewer56
* Custom Mapping for Heroes Controller Hook by Sewer56
* Definitive Character Visual Overhaul by LunaAlex64
* SH Essentials: Graphics by Sewer56
* SH: Fixed Edition by DonutStopGaming and Kell
* This Mod

Many OOL or cosmetic mods should work with this mod but avoid mods that change base game functionality.


### Delaying Mod Injection

If you have NoCD version and don't want to delay injection of mods, this step can be skipped.


Reloaded by default automatically injects the mods when launching the game. If you have CD Version or wish to delay injection, go to Edit Application and Advanced Tools & Options and check "Don't Inject Loader". This will prevent the injection of mods and launch the game vanilla. After the game has laucnhed, find the game process in the Processes list in Reloaded to manually inject the mods.


# Playing the Rando

Once you have the game booting, check the log that appears to ensure you're connected. If you're not, check your mod configuration and relaunch the game.

If you connect successfully, you should then create a new save file. The mod will unlock stages as needed but will not lock already unlocked stages in the save file so creating a new save every Archipelago run is required. Given that the game has 99 save files, just reserve one for AP and delete it when finished.

The game is played entirely from the Level Select (Challenge) screen.


### Spawn Positions

The mod allows for enetering levels spawning at any unlocked spawn position from level select. The Start of Level, any checkpoint, and the Bonus Stage are valid spawn positions. Touching any checkpoint in game will unlock that spawn position as well.

* In order to select a specific spawn position, select the level but before selecting the act press up or down to change the selection. Confirm by selecting the act which will enter the level.

* Bonus and Emerald Stages are entered from level select by choosing the "Bonus Stage" spawn position after it is unlocked. The vanilla way of entering bonus and emerald stages is removed.



## Music Shuffle

The new version has music shuffle. First, change the Music Shuffle Option to true and use the folder picker option Heroes BGM folder to point to the BGM folder in the Sonic Heroes game directory. Enabling the Heroes Shuffle option will add all of the Sonic Heroes songs to the pool to be selected from to replace the vanilla songs.

Music is either a song (which loops) or a jingle (which doesnt loop).

The following options help change how the music is shuffled. These will separate their respective songs into a separate pool to then shuffle only with themselves when enabled.
- Separate Boss Music (Boss themes)
- Separate Long Jingles (The intro theme and a theme for each team. These do not loop.)
- Separate Menu Music (The 4 menu themes)
- Separate Short Music (2P battle themes)


### SA2, SADX, and Shadow the Hedgehog Music Shuffle

There is support for Sonic Adventure 2 (with the Battle DLC), Sonic Adventure DX, and Shadow the Hedgehog music builtin. Just use the folder picker option for that specific game to point to the correct folder (ADX folder for SA2 and WMA folder for SADX) and enable the option to add that game's music to the pool.

### Custom Music Shuffle (must be ADX file)

Custom Music requires a specific setup. First have a folder to store all of the custom music and use the folder picker option to point to that folder. Make sure that the file path is under 200 characters and that no individual folder has too long of a name.


This music must be in a ADX format (with loops set for music in order to have the game loop that song). Then, create the following folders (with this exact name) in that folder:
- ShortMusic
- LongJingle
- Jingle
- Music
- MenuMusic
- BossMusic


Place the desired ADX files in the folder corresponding to the type of music. If not enabling the specific Separate option for that type, then that folder is combined with either the Music folder or Jingle pool by the mod.


### Final Note about Music Shuffle

If the pool is not large enough to fill all of the Heroes songs, then the vanilla song is placed there (this happens with SA2 only with Separate Boss Themes as Heroes has more boss themes than SA2).




# Special Thanks
Thanks to EthicalLogic for developing the APWorld and taking over development of the Mod, xMcacutt for initially developing the Mod, Sewer56 for Reloaded-II, and Mayo and Seri for help playtesting.

[![Game Banana](https://gamebanana.com/mods/embeddables/582396?type=large)](https://gamebanana.com/mods/582396)
