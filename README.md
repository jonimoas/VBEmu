# VBEmu

Simple emulator frontend using the file formats of EmulationStation and connected to rawg.io, focused on simplicity and speed.
Good for users with huge collections.

## Features

It includes all the usual features such as:

- Ability to search games by typing the name
- Ability to filter games by genre/publisher (requires gamelist xmls)
- Pure keyboard navigation
- Fully customizable controller input (limited to the first controller for now)

Some unique features are:

- The ability to create scripts for a game, and launch it without needing the frontend
- Heavy use of threads, helping to avoid any freezes etc
- Optimized gamelist parsing (thousands of roms in seconds)
- Focus on big rom collections
- Connectivity to RAWG.io
- Connectivity to thegamesdb.net
- Ability to directly download and store images and metadata

## USAGE:

The application uses es_systems.cfg and gamelist.xml files like emulationstation
you need to locate your es_systems file on first run and the gamelist xmls and image folders need to be in the directory of your collections.
see samples.txt for more information on the files used

To run a rom simply double click on it or press the enter button.

## NAVIGATION:

| button     | function      |
| ---------- | ------------- |
| up-down    | change game   |
| left-right | change system |
| enter      | run           |
| comma-dot  | change genre  |
| escape     | close         |
| space      | search        |

## SEARCH:

Pressing the space button, or clicking above the gamelist and typing, allows you to search
games by name

## CONTROLLER:

You can double click in a black area, and the joystick configuration window will show.
Click on each command, press the button on the joystick and wait until the textbox fills.

## CACHING:

You can toggle the caching from the joystick config screen. Live caching enables saves the parsed xml data
on the first run and pre caching makes the app load everything on startup, obviously costing in time.
The freeze UI option, if enabled, removes any threading, so the UI will freeze on any loading.

## RAWG.io:

In order to configure usage of the rawg.io rest API, you can get a token here:
https://rawg.io/apidocs
Then, paste the token in the joystick configuration screen, choose RAWG as your provider and click Save.

## thegamesdb.net:

In order to configure usage of thegamesdb.net rest API, you must have a dev token.
Paste it on the config screen, save and choose TGDB as your provider.

## Context Menu:

When right clicking anywhere on the form, the context menu will appear.
It includes the following functions:

- Download metadata from the selected provider, if they don't exist.
- Save the newly downloaded metadata.
- Create a script to run the game without needing the frontend.
- Show the options dialog.
- Download missing metadata for the whole system.

![Screenshot](/screenshot.png)
