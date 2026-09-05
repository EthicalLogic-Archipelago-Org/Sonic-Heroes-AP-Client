
# FAQ

### The game crashed on launch with a small popup window saying "Unknown Hard Error"

This error is SafeDisc DRM protecting a spot in memory that the mod is trying to read. Make sure to follow the CD Verison instructions in the setup guide.


### The game crashes on connection or on receiving a ring, shield, emblem, or level up item, or receving a ringlink with an unknown fatal error

The current (2.2.3) version of the mod has a race condition with the function used to play a sound on receiving an item. Disable Play Item and RingLink sounds in the mod config until the next release fixes this issue.


### I cant open the Mod Config Window after updating

I have changed a few config options over a few updates.
Go to {Reloaded Directory}/User/Mods/sonicheroes.archipelago.client and delete the Config.json
Relaunch Reloaded and it will work.

Notice the User folder here as many have mistaken the folder required.



### I can't enter a bonus/emerald stage after bringing the key to the goal

The vanilla way of entering bonus/emerald stages was removed. Enter the Bonus/Emerald stage from level select by choosing the regular stage and changing the spawn position to Bonus Stage after it has been unlocked.

The current (Mod Version 2.2.3) way of unlocking the bonus stage is to pick up any 1 key in the level and goal the level (you do **NOT** need have to have key when you goal)


### Why cant I Fly / Triangle Jump / Combo Finisher in ability rando

Some abiliies require another ability to use.
* Triangle Jump Requires Homing Attack
* Flight Requires Thundershoot (but not a second character despite Thundershoot needing one)
* Combo Finisher Requires Power Attack
* Many abilties may also require Jump as well if you have Jump rando enabled



### Music Rando is on but only vanilla music is playing

If you have followed the directions correctly then make sure that your file paths are not too long (aim for less than 100 characters)
If your pool of music is smaller than vanilla, then vanilla is forced as a failsafe



## Currently Known Crash Addresses

### Address 64A33B after receiving a shield

This is a race condition in the current version (2.2.3) that will be fixed in the next mod version