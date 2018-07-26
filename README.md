# New Mod Manager for Total War
The new mod manager for Total War is a rework of the Rome 2 mod manager by MitchTWC. Featuring new file parsing, conflict resolution, data file assessment, profile management, and other rewrites this tool is on it's way to be being rewritten from the ground up in a 2.0 version which will feature Steam integration.

## Changes and Bug Fixes for v1.7.1:

- [x] Fixed date sorting bug that broke profiles

## Changes and Bug Fixes for v1.7.1:

- [x] Extended timeout on version check and wrapped it in an exception check. 

## Changes and Bug Fixes for v1.7:

### New Functionality:

Right clicking on list of mods has the following new options:
- [x] Show Contents
- [x] Open with PFM
- [x] Open with RPFM
- [x] Save to Archive
- [x] Restore from Archive
- [x] Delete from Data (Delete does not remove from backup)
- [x] Copy (appends an  _ copy to the end of the pack name without the space between the _ and copy) 
- [x] Rename

New Menu Options
- [x] Clean Data
- [x] Option to Highlight Recent Mods
- [x] Option to Backup Mods
- [x] Option to Choose PFM Location
- [x] Option to Choose RPFM Location
- [x] Option to Choose Archive Location
- [x] Detect Game Installations

- [x] Mod Manager now displays what the latest version available is

Changes and Fixes
- [x] Movie packs will now show up in the List (they were loading but not displaying)
- [x] Batch file method of calling the game exe removed (this should reduce antivirus reports)
- [x] Install folder scan now checks if folder exists that steam reports (since apparently steam reports folders that don't exist or users don't have anymore)
- [x] Warhammer 2 installation is no longer required for Warhammer 1 or Thrones of Britannia
- [x] Steam install detection issues when you don't have Warhammer 2 installion fixed
- [x] Copy files wrapped in an access violation check (should prevent issues with copying files that are being downloaded)
- [x] Issue with Steam being installed on one drive and a game being installed on another drive fixed
- [x] Error Creating Profile when switching to a game fixed (directory doesn't exist bug)
- [x] Clean Data wrapped in a try/catch so if a process is playing with your files, you won't see an error. The files remain.
- [x] user.script.txt file deleted at startup
- [x] AppId (Steam Integration text file) deleted at startup
- [x] AppId (Steam Integration text file) written/deleted on the fly so only used when needed
- [x] Clean data moved to the File menu
- [x] Install detection routine will now use Steam for the first check. This will prompt the user that the application will exit after writing out the install paths. You will need to restart the mod manager after this one time detection of install location happens.
- [x] Warhammer 2 installation is no longer required for Warhammer 1 or Thrones of Britannia
- [x] Steam install detection issues when you don't have Warhammer 2 installion fixed
- [x] Movie packs will now show up in the List (they were loading but not displaying)
- [x] Batch file method of calling the warhammer exe removed (this should reduce antivirus reports)
- [x] Install folder scan now checks if folder exists that steam reports (since apparently steam reports folders that don't exist or users don't have anymore)
- [x] Image files (.png) are no longer locked by the GUI when displayed allowing the image to be updated while in use
- [x] Copy files wrapped in an access violation check (should prevent issues with copying files that are being downloaded)
- [x] XML data files are no longer needed for each game meaning Patch Day updates are no longer required
- [x] .Movie pack file format is only supported for Warhammer 1 and Warhammer 2
- [x] Title bar for Conflicts GUI no longer mentions .pack .pack files
- [x] All Total War games are now supported
- [x] Date display in the mod list has been redone and should now sort correctly


## Known Issue: Existing Warhammer 2 profiles will need to be imported manually from the \AppData\Roaming\The Creative Assembly\Warhammer2\scripts folder

You can manually copy them from the \AppData\Roaming\The Creative Assembly\Warhammer2\scripts folder to the \AppData\Roaming\Kaedrin Mod Manager\Profiles\Warhammer2 folder. The easiest way to get to appdata is to type %appdata% in windows explorer.

## Changes due to the new Creative Assembly Launcher/Mod Manager:

Due to a change in the Creative Assembly Launcher/Mod Manager and how it handles .pack files, I've added new code to handle this and make it mod friendly again. The CA Launcher now uses both the \Steam\steamapps\workshop\content and \Steam\steamapps\common\Total War WARHAMMER II\data folder for mods. Any mods that have been added or updated since the new CA Launcher was released have been switched to use the new Content folder and the .pack file will ONLY be found there by default. So mod developers/users who are looking for their files and can't find them... that's where they dissapeared to. 

In order to keep it easy for developers to open mods with PFM/RPFM and for the KMM to see all of the mods in a single place, I've added code to copy all of the mods out of the content folder and in to the data folder. The files are left in the content folder once this happens (and the old format .bin files are also removed to save space and speed up the process). Any time a mod is updated it will be downloaded like normal. If the "last modified" timestamp on your data folder is newer than the one in content, the file will not be updated so users who modify the works of a mod developer should rename the work in order to ensure getting future updates. I'll be working to cache this out in an xml file to speed the process up more for the v1.6 update.

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
- [x] Switched to 100% XML driven data files (remove dependency on internal WH2 data file list)
- [x] Remove all legacy pack file implementations
- [x] Solved how to get which Total War games are installed for next version
- [x] Data XML expanded to include new information needed (file exe name, etc)
- [x] Found how to import CA Mod Launcher settings for first time users, will include in a future version
- [x] Improved file loading using parallel processing
- [x] Moved Data files and AppIds to XML files preparing the Mod Manager to support other Total War games (needs more work to support Warhammer 1 and other games)
- [x] Enable/Disable all mods option added
- [x] Added menu options for Importing and Exporting profiles (to any other location on the machine than the usual scripts folder for sharing)
- [x] Fix for profiles not loading correctly
- [x] Fix for column sorting incorrectly interacting with profiles
- [x] New menu option to use the last profile used at startup
- [x] New menu option to use Warhammer 1 or 2 (which is restored at application startup) (disabled for now)
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
- Either Warhammer 2, Warhammer 1, or Thrones of Britannia installed
- **_You MUST disable ALL mods in the CA Mod Manager BEFORE Launch in the New Mod Manager for Warhammer 2 - This applies to Warhammer 1 and Thrones of Britannia as well_**
- **_You MUST REVERT TO VANILLA in order to go back to use the CA Mod Manager after using the New Mod Manager for Warhammer 2 - This applies to Warhammer 1 and Thrones of Britannia as well_**

## Upcoming Features:
- [ ] Application preferences are no longer redone every time the application starts and closes
- [ ] Better support for smaller resolutions with resizing and adjusted placement
- [ ] Checking an item should move it up to the top with the rest of the checked items (auto-bump)
- [ ] Add help menu item
- [ ] New column added indicated the priority of the activated mods
- [ ] Drag and drop capability for the ListView
- [ ] Better support for smaller resolutions with resizing and adjusted placement
- [ ] Full source code check in
- [ ] WPF-based ground up rewrite of the GUI
- [ ] Thumbnail mode where each row is larger but shows an inline thumbnail for the Mod (probably a dozen rows at once)
- [ ] Ctrl/shift (keys TBD) select to allow moving multiple mods at a time
- [ ] Ability to categorize rows using the steam categories
- [ ] Menu item for changing game directory will now reset file paths allowing the user to rechoose exe locations
- [ ] Importing a profile with a mod you don't have subscribed will pop a custom form displaying which mods are missing (pack file names for now, steam URLs in the future)
- [ ]  Steam Integration (Title, Author, Required Mods,etc) and a checkbox to enable/disable
- [ ] Text box to enter search text to filter the list view down, empty the box to restore the list view
- [ ] Detect any unsubscribed/deleted Mod and make a local backup before Launch
- [ ] Include ability to import CA Mod Launcher settings for first time users

  ![alt text](https://github.com/Kaedrin/warhammer-mod-manager/blob/master/TotalWarModManager.JPG "Total War Mod Manager Screenshot")
