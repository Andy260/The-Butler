Latest Build as of 9/6/2015...

======================
Description
======================
Mostly level creation and minor bug fixes since last build.

Changelog:
- Started development of Levels 1 & 2
- Added Level selection scene
- Began development of first level
- Player will now be aware of health through dimming of game affect
- Implemented flashing lights (not shown within this build)
- Game now has a lose state, where game will reset (health not visible in-game yet)
- Implemented moving lights
- Implemented detection of player within influence of light sources

Known issues
- Player is always considered to be in light within flashing lights' influence, 
  regardless of whether or not the light is currently active or not.
- Screen doesn't stay completely dim after level failure