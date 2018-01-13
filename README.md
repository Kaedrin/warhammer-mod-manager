# New Mod Manager for Warhammer 2
The new mod manager for Warhammer 2 is a rework of the Rome 2 mod manager by MitchTWC. Featuring new file parsing, conflict resolution, data file assessment, profile management, and other rewrites this tool is on it's way to be being rewritten from the ground up in a 2.0 version which will allow support for Warhammer 3 and potentially other Total War games as well as limited Steam integration.

## Initial Release Features:

- [x] Ability to load, save, and delete profiles of activated Mods
- [x] Ability to change the priority order of Mods
- [x] Ability to show which Mods are in conflict with each other
- [x] Ability to refresh the list of Mods in case new ones were downloaded using the CA mod launcher
- [x] Ability to revert back to vanilla (no custom priority defined, return to using the CA mod launcher)
- [x] Ability to select a row/Mod and see the Mod's image

## Requirements:

- DotNet v4.5 Framework installed
- Resolution at least 1400x850 or better 
- **_You MUST disable ALL mods in the CA Mod Manager BEFORE Launch in the New Mod Manager for Warhammer 2_**
- **_You MUST REVERT TO VANILLA in order to go back to use the CA Mod Manager after using the New Mod Manager for Warhammer 2_**

## Upcoming Features:
- [ ] New column added indicated the priority of the activated mods
- [ ] Drag and drop capability for the ListView
- [x] Parallel.Foreach to speed up the file loading
- [ ] Limited Steam Integration (Title, Author, Required Mods) and a checkbox to enable/disable
- [ ] Better support for smaller resolutions with resizing and adjusted placement
- [ ] Conflict GUI could show more information on what it is conflicting with for each Mod
- [ ] Checking an item should move it up to the top with the rest of the checked items (auto-bump)
- [x] Add help menu item
- [ ] Full source code check in
- [ ] WPF-based ground up rewrite of the GUI
- [ ] Number of Mods activated listed
- [x] Enable/Disable all mods
- [x] Move Data files and AppIds to XML files enabling the Mod Manager to support other Total War games
- [x] Add menu options for Importing and Exporting profiles (to any other location on the machine than the usual scripts folder for sharing)
- [ ] Thumbnail mode where each row is larger but shows an inline thumbnail for the Mod (probably a dozen rows at once)
- [x] Importing a profile with a mod you don't have subscribed will pop an error message detailing which mods are missing (pack file names for now, steam URLs in the future)
- [x] Generate a data XML file for Warhammer 1 (since I have the game installed)
- [ ] Install Rome 2 and generate a data XML file (since I own the game)
- [ ] Ctrl/shift (keys TBD) select to allow moving multiple mods at a time
- [ ] Ability to categorize rows using the steam categories
- [x] Fix for profiles not loading correctly
- [x] Profiles now only save the Mods that are enabled for speed/efficiency 
- [x] Add preference to start with last profile used - v1.1
- [ ] Add preference to remember last UI setting - v1.1
- [x] Move data file path to menu option - v1.1
- [x] Move contact message to menu option - v1.1
- [x] Dock list view to bottom of GUI to help support resize and GUI issues with WinForms GUI - v1.1
- [x] Right clicking a row will launch a form which will show the file contents of the Mod file
  
  ![alt text](https://github.com/Kaedrin/warhammer-mod-manager/blob/master/WarhammerModManager.JPG "Warhammer 2 Mod Manager Screenshot")
