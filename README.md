# Reasonably Sheltered

## What is this mod
Reasonably Sheltered, or Sheltered for short, is a small mod that adds a key combination to make tamed lizards stay/follow.

## How to use
After installing and enabling the mod in Remix, you should be good to go. Ingame, hold **down** while pressing **grab** and the lizard should stay where they are or follow.
The mod will show a popup when the lizard switches from follow to staying and vice-versa. If you dont see any popup, try moving closer to the lizard.

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
