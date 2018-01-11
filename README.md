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
- [ ] Parallel.Foreach to speed up the file loading
- [ ] Limited Steam Integration (Title, Author, Required Mods) and a checkbox to enable/disable
- [ ] Better support for smaller resolutions with resizing and adjusted placement
- [ ] Conflict GUI could show more information on what it is conflicting with for each Mod
- [ ] Checking an item should move it up to the top with the rest of the checked items (auto-bump)
- [ ] Add help button
- [ ] Full source code check in
- [ ] WPF-based ground up rewrite of the GUI
- [ ] Number of Mods activated listed
- [ ] Enable/Disable all mods
- [ ] Move Data files and AppIds to XML files enabling the Mod Manager to support other Total War games
- [ ] Thumbnail mode where each row is larger but shows an inline thumbnail for the Mod (probably a dozen rows at once)
  
  ![alt text](https://github.com/Kaedrin/warhammer-mod-manager/blob/master/WarhammerModManager.JPG "Warhammer 2 Mod Manager Screenshot")
