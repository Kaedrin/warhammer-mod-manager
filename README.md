# New Mod Manager for Total War
The new mod manager for Total War is a rework of the Rome 2 mod manager by MitchTWC. Featuring new file parsing, conflict resolution, data file assessment, profile management, and other rewrites this tool is on it's way to be being rewritten from the ground up in a 2.0 version which will feature Steam integration.

## Changes and Bug Fixes for v1.9.0:

v1.9.0 adds support for Warhammer 3 and Troy, with more to come:

1. Support for Troy Added
2. Support for PFH6 Added
3. UI Support for War3 Added
4. Out of Date Check Updated/Added For Warhammer 3, Warhammer 2, Three Kingdoms, and Troy
5. Initial support for moving Date Updated string to an XML file instead of requiring code changes

**WARNING If you see the CA launcher download any files, once it is finished downloading you need to click the refresh button in the KMM in order for the KMM to move those new and/or updated mods to the data folder. The refresh button will scan the content folder again and display any newly subscribed mods in the list as usual.**

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
- [x] Added menu options for Importing and Exporting profiles (to any other location on the machine than the usual scripts folder for sharing)
- [x] New menu option to use the last profile used at startup
- [x] New menu option to use Warhammer 1 or 2 (which is restored at application startup)
- [x] New menu option to export a profile for sharing with others
- [x] New menu option to import a profile (will silently ignore any pack files that are missing at this time, more work will be done on this in the future)
- [x] Right clicking a row will launch a new form which will show the file contents of the Mod file
- [x] Moved data file path to menu option
- [x] Moved contact message to menu option
- [x] Profiles now only save the Mods that are enabled for speed/efficiency 
- [x] Improved sizing/resizing elements of the GUI to better support 16x9 resolutions and large fonts.
- [x] Solved how to get which Total War games are installed for next version
- [x] Found how to import CA Mod Launcher settings for first time users, will include in a future version
- [x] Improved file loading using parallel processing
- [x] Enable/Disable all mods option added
- [x] Added menu options for Importing and Exporting profiles (to any other location on the machine than the usual scripts folder for sharing)
- [x] New menu option to use the last profile used at startup
- [x] New menu option to export a profile for sharing with others
- [x] New menu option to import a profile (will silently ignore any pack files that are missing at this time, more work will be done on this in the future)
- [x] Right clicking a row will launch a new form which will show the file contents of the Mod file
- [x] Moved data file path to menu option
- [x] Moved contact message to menu option
- [x] Profiles now only save the Mods that are enabled for speed/efficiency 
- [x] Improved sizing/resizing elements of the GUI to better support 16x9 resolutions and large fonts.
- [x] Number of Mods activated listed
- [x] Settings file for old versions of the mod manager deleted at startup to reduce conflicts
- [x] Moving mods/up down with the arrows show their PNG
- [x] Settings file for old versions of the mod manager deleted at startup to reduce conflicts
- [x] Moving mods/up down with the arrows show their PNG
- [x] Issues with mods being missing due to the new CA Launcher
- [x] Data path option under information now mentions that revert will delete the application preferences so that you can restart the application and have it scan for a new install location.
- [x] Revert now deletes the application preference and the user.script.txt file
- [x] New way of handling settings implemented which should improve their reliability
- [x] Bug in old settings handling fixed which should take care of the issue where it couldn't find the .exe and you had to manually delete the application preferences at times. 
- [x] Steam files added but integration disabled for now, waiting for the update on the 31st.
- [x] "Last Used Mods" Profile now works again
- [x] Content folder now keeps the .pack file for steam integration but a copy to data unless a newer filestamp is found (ie, the user has altered the file on their own). Version 1.5 will flag this file is being altered and an update being available from the author.
- [x] Closing KMM deletes the user.script.txt file (assists mod authors with developing scripts)
- [x] Clean Data button added (with confirmation) - this will remove all mods from your Data folder. Mods that have no been updated yet will have to be re-downloaded
- [x] Warhammer 1 support added
- [x] Thrones of Britannia support added
- [x] KMM now checks to see if War2, War1, and Throb are installed at start and enables/disables their menu item appropriately
- [x] Application settings have been moved to \AppData\Roaming\Kaedrin Mod Manager
- [x] Profiles are now saved by game
- [x] Importing a profile now shows up in the Profiles selection list without having to click Refresh
- [x] Content folder now keeps the .pack file for steam integration but a copy to data unless a newer filestamp is found (ie, the user has altered the file on their own). Version 1.6 will flag this file is being altered and an update being available from the author.
- [x] Issue with Steam being installed on one drive and a game being installed on another drive fixed
- [x] Error Creating Profile when switching to a game fixed (directory doesn't exist bug)
- [x] Clean Data wrapped in a try/catch so if a process is playing with your files, you won't see an error. The files remain.
- [x] Delete user.script.txt on startup
- [x] AppId (Steam Integration text file) deleted at startup 
- [x] Add clean data to the File menu
- [x] Install detection routine will now use Steam for the first check. This will prompt the user that the application will exit after writing out the install paths. You will need to restart the mod manager after this one time detection of install location happens.
- [x] Movie packs will now show up in the List (they were loading but not displaying)
- [x] Batch file method of calling the game exe removed (this should reduce antivirus reports)
- [x] Install folder scan now checks if folder exists that steam reports (since apparently steam reports folders that don't exist or users don't have anymore)
- [x] Warhammer 2 installation is no longer required for Warhammer 1 or Thrones of Britannia
- [x] Steam install detection issues when you don't have Warhammer 2 installion fixed
- [x] Copy files wrapped in an access violation check (should prevent issues with copying files that are being downloaded)

## Requirements:

- DotNet v4.5 Framework installed
- Resolution at least 1400x850 or better
- **_You MUST disable ALL mods in the CA Mod Manager BEFORE Launching in this Mod Manager_**
- **_You MUST REVERT TO VANILLA in order to go back to use the CA Mod Manager after using the New Mod Manager for Warhammer 2_**

## Upcoming Features:

- Being cleaned up and will be back soon

  ![alt text](https://github.com/Kaedrin/warhammer-mod-manager/blob/master/TotalWarModManager.JPG "Total War Mod Manager Screenshot")
