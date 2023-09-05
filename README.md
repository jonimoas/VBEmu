# VBEmu
Simple emulator frontend using the file formats of EmulationStation focused on simplicity and speed. Good for users with huge collections.

## Features

It includes all the usual features such as:

* Ability to search games by typing the name
* Ability to filter games by genre/publisher (requires gamelist xmls)
* Pure keyboard navigation
* Fully customizable controller input (limited to the first controller for now)

Some unique features are:

* The ability to create scripts for a game, and launch it without needing the frontend
* Heavy use of threads, helping to avoid any freezes etc
* Optimized gamelist parsing (thousands of roms in seconds)
* Focus on big rom collections

## USAGE:

The application uses es_systems.cfg and gamelist.xml files like emulationstation
you need to locate your es_systems file on first run and the gamelist xmls and image folders need to be in the directory of your collections.
see samples.txt for more information on the files used

To run a rom simply double click on it or press the enter button.

## NAVIGATION:

|button|function|
| --- | --- |
|up-down|change game|
|left-right|change system|
|enter|run|
|comma-dot|change genre|
|escape|close|
|space|search|

## SEARCH:

Pressing the space button, or clicking above the gamelist and typing, allows you to search 
games by name

## CONTROLLER:

You can double click in a black area, and the joystick configuration window will show. 
Click on each command, press the button on the joystick and wait until the textbox fills.

## SCRIPTS:

You can also use the save function to create a script and simply double click on it and run the selected game

## CACHING:

You can toggle the caching from the joystick config screen. Live caching enables saves the parsed xml data 
on the first run and pre caching makes the app load everything on startup, obviously costing in time.

![Screenshot](/screenshot.png)
