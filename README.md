# Reasonably Sheltered

## What is this mod
Reasonably Sheltered, or Sheltered for short, is a small mod that adds a key combination to make tamed/friend creatures stay/follow.

## How to use
### Telling your creature to stay
After installing and enabling the mod in Remix, you should be good to go. Ingame, hold **down** while pressing **grab** and the creature should stay where they are or follow.
The mod will show a popup when the creature switches from follow to staying and vice-versa. If you dont see any popup, try moving closer to the creature.

### Distractions (mechanic)
Some things, like for lizards when they see prey and for slugpups when they see predators, will cause the creature to be distracted. Distractions will make the creature follow its
normal AI and switch back to follow when its finished. Make sure to be in a empty shelter as else your creatures will wander off!

## Steam Workshop
The mod can be found in the workshop here: https://steamcommunity.com/sharedfiles/filedetails/?id=2927358839

<br/>

# Building the mod

## Initial setup
To build the mod you will need to prepare some things first:
1. Copy your Rain World installation to a folder named 'game', after copying it should look like this:
   -- game<br/>
   ---- RainWorld_Data<br/>
   ---- ..<br/>
   ---- RainWorld.exe<br/>
   ---- ...
2. Install the .NET SDK
3. You should be good to go

## Building
The mod can be built using the dotnet CLI or visual studio.

Run `dotnet build` in the project root and it should build the full mod.

## Finding the output binaries
By default, the mod will be saved in `build/remix`, the contents of the folder can then be copied directly to Rain World as described below.

<br/>

# Installing the mod without the Steam Workshop
After building (or downloading) the mod package, go to your Rain World folder and copy the mod to the following path:
`[Rain World]/RainWorld_Data/StreamingAssets/mods/reasonablysheltered`, after copying it should show up in Remix.
