# New Mod Manager for Warhammer 2
The new mod manager for Warhammer 2 is a rework of the Rome 2 mod manager by MitchTWC. Featuring new file parsing, conflict resolution, data file assessment, profile management, and other rewrites this tool is on it's way to be being rewritten from the ground up in a 2.0 version which will allow support for Warhammer 3 and potentially other Total War games as well as limited Steam integration.

## New Version 1.2 Features:
- [ ] New path system parsed from XML 
- [ ] Application preferences moved to an XML based system
- [ ] Last Used Profile application preference enabled when it's not ready yet
- [ ] Data XML expanded to include new information needed (file exe name, script path name, etc)
- [ ] Menu dropdown for games a dynamic pickup from XML data files
- [ ] Application preferences are no longer redone every time the application starts and closes
- [ ] Menu item for changing game directory will now reset file paths allowing the user to rechoose exe locations
- [ ] Evaluate PackFileManager approach to Total War file paths and pack files (script path, user script file path, appdata root, data path, exe path, exe name, etc)
- [ ] Remove all legacy pack file implementations
- [ ] Importing a profile with a mod you don't have subscribed will pop a custom form displaying which mods are missing (pack file names for now, steam URLs in the future)
- [ ] Generate data file for Rome 2
- [ ] Rome 2 support added
- [ ] Warhammer 1 support added
- [ ] Switch to 100% XML driven data files (remove dependency on internal WH2 data file list) - **Required before Tomb Kings arrive**
- [ ] Remove stopwatches 
- [ ] Create new GUI/form for choosing which Games you want to enable
- [ ] Mod Manager is auto-aware of War2, will prompt if it is not there if you want to scan for other games. Otherwise you need to use the menu option to enable other games.
- [ ] Limited Steam Integration (Title, Author, Required Mods) and a checkbox to enable/disable
- [ ] Text box to enter search text to filter the list view down, empty the box to restore the list view
- [ ] Detect any unsubscribed/deleted Mod and make a local backup before Launch

## Current Mod Manager Features:

- [x] Ability to load, save, and delete profiles of activated Mods
- [x] Ability to change the priority order of Mods
- [x] Ability to show which Mods are in conflict with each other
- [x] Ability to refresh the list of Mods in case new ones were downloaded using the CA mod launcher
- [x] Ability to revert back to vanilla (no custom priority defined, return to using the CA mod launcher)
- [x] Ability to select a row/Mod and see the Mod's image
- [x] Improved file loading using parallel processing
- [x] Move Data files and AppIds to XML files enabling the Mod Manager to support other Total War games (will need to rework for more than Warhammer 1 and 2)
- [x] Enable/Disable all mods
- [x] Add menu options for Importing and Exporting profiles (to any other location on the machine than the usual scripts folder for sharing)
- [x] Fix for profiles not loading correctly
- [x] Fix for column sorting incorrectly interacting with profiles
- [x] New menu option to use the last profile used at startup
- [x] New menu option to use Warhammer 1 or 2 (which is restored at application startup)
- [x] New menu option to export a profile for sharing with others
- [x] New menu option to import a profile (will silently ignore any pack files that are missing at this time, more work will be done on this in the future)
- [x] Right clicking a row will launch a new form which will show the file contents of the Mod file
- [x] Move data file path to menu option
- [x] Move contact message to menu option
- [x] Profiles now only save the Mods that are enabled for speed/efficiency 
- [x] Improved sizing/resizing elements of the GUI to better support 16x9 resolutions and large fonts.

## Requirements:

- DotNet v4.5 Framework installed
- Resolution at least 1400x850 or better
- Either Warhammer 2 or Warhammer 1 installed
- **_You MUST disable ALL mods in the CA Mod Manager BEFORE Launch in the New Mod Manager for Warhammer 2 - This applies to Warhammer 1 as well_**
- **_You MUST REVERT TO VANILLA in order to go back to use the CA Mod Manager after using the New Mod Manager for Warhammer 2 - This applies to Warhammer 1 as well_**

## Upcoming Features:
- [ ] New column added indicated the priority of the activated mods
- [ ] Drag and drop capability for the ListView
- [ ] Better support for smaller resolutions with resizing and adjusted placement
- [ ] Conflict GUI could show more information on what it is conflicting with for each Mod
- [ ] Checking an item should move it up to the top with the rest of the checked items (auto-bump)
- [ ] Add help menu item
- [ ] Full source code check in
- [ ] WPF-based ground up rewrite of the GUI
- [ ] Number of Mods activated listed
- [ ] Rework Data files and AppIds to XML files enabling the Mod Manager to support other Total War games 
- [ ] Thumbnail mode where each row is larger but shows an inline thumbnail for the Mod (probably a dozen rows at once)
- [ ] Ctrl/shift (keys TBD) select to allow moving multiple mods at a time
- [ ] Ability to categorize rows using the steam categories
- [ ] Add preference to remember last UI setting
  
  ![alt text](https://github.com/Kaedrin/warhammer-mod-manager/blob/master/WarhammerModManager.JPG "Warhammer 2 Mod Manager Screenshot")
