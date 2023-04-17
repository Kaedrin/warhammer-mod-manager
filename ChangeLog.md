## Changes and Bug Fixes for v1.8.15:

v1.8.15 is a major update with a raft of changes, more to come:

1. Out of date check for Warhammer 2 updated for the Silence and the Fury DLC
2. New option to force trigger downloads when a date discrepancy is detected between the Steam API and Content folder timestamps (Options -> Check Steam for Out of Date Mods). A message will pop up indicating if any are found letting you know that you need to refresh KMM after they are done downloading (check Steam and wait until they are finished downloading before clicking Refresh)
3. New Option to delete Content files for out of date check (to ensure maximum ability to get the latest, not relying on steam to replace out of date mods with the download but deleting the Content version then letting the download get a fresh copy)
4. Troubleshooting Guide URL updated
5. Profiles menu now has a new option for "Export Current Profile to Clipboard" 
6. Profiles menu now has a new option for "Export Current Profile to HTML" which includes URLs to their steam page(s)
7. Profiles menu now has a new option for "Export All Mods to HTML" which includes URLs to their steam page(s)
8. Dark mode options has been added. Changing to dark mode will require a restart of KMM (as will changing back).
9. Delete from data now correctly whacks the PNG file in all cases
10. A warning is now given when a Webp malformed image is detected. .WEBP images renamed as .PNG do not work with KMM and .Net programs
11. Message informs the user when user preferences are reset due to new options being added. Next version will load from old preferences, sorry.
12. New Right Click option "Delete from Data (Multiple, Any Checked)"
13. New Right Click option "Unsubscribe from Steam and Delete (Multiple, Any Checked)"
14. Right Click option "Unsubscribe (Steam) and Delete" renamed "Unsubscribe from Steam and Delete"
15. RPFM specific files "extra_packfile.rpfm_reserved", "settings.rpfm_reserved" and "notes.rpfm_reserved" are now excluded from conflict checks
16. Open Data folder option
17. Open Content folder option
18. Clean Data and Exit

## Changes and Bug Fixes for v1.8.14:

v1.8.13 is a minor update with a few fixes, more to come:

1. Warhammer 2 out of date check corrected to support the new "Rakarth" FLC
2. TLS Support added for getting latest version information
3. Steamworks updated to version 15.0 to support Steam
4. Under the hood changes in preparation  of Dark Theme and Content loading for the 1.9 Update which will be going into Alpha soon
5. Fixed a debug message and incorrect steam upload left in the development build

## Changes and Bug Fixes for v1.8.12: 

v1.8.12 is a minor update with a few fixes, more to come:

1. Warhammer 2 out of date check corrected to support the new "The Twisted and Twilight" DLC
2. Three Kingdoms now correctly has support for VFX mods
3. Feature branch for Content loading is moved back to development and will resume after this release

## Changes and Bug Fixes for v1.8.11: 

v1.8.11 is a minor update with a few fixes, more to come:

1. Warhammer 2 out of date check corrected
2. ThreeKingdoms exe name corrected
3. New GameData xml file version, manager will detect settings for the first time on running
4. Title now set to File name when not a subscribed mod (no long shifts the pack type to the the title column)
5. Updated Steamworks version

## Changes and Bug Fixes for v1.8.10: 

v1.8.10 is a minor update with some fixes for date sorting, a date update for the new Shadow and Blade patch, and under the hood changes for the 2.0 update.

1. Date Sorting should be corrected
2. Out of date now shows based on the new patch for Shadow and Blade
3. A fix was put in for invalid table names in pack files (they will no longer crash the manager)
4. Under the hood changes were done in preparation of the 2.0 update and the uploader tool.

## Changes and Bug Fixes for v1.8.9:

v1.8.9 is fix to the sorting and profile problems with the mod manager.

1. Profiles should be cleaned up and fixed
2. Conflicts shouldn't show up when using bypass conflicts option
3. Launch game only complains when you use descending sort or a sort on one of the other columns and lets you cancel. 
4. Whole thing shouldn't be annoying and should preserve everything. 
5. Activating a profile should bubble everything to the top finally. 
6. Sorting alpha with a profile won't screw a profile up. 
7. Refreshing when you have a blank in the profile name box will clear the mods from the top and do a full alpha sort of the list and put it back into a "pristine state"
8. Switching between bypass conflict options will no longer break the titles in the list
9. Switching between use last profile options will no break the list sorting


## Changes and Bug Fixes for v1.8.7:

1. Out of date mods are now flagged correctly for the 17th of April, 2019 *except* for mods which are not on steam (local mods will stay white as I have no way to know if they are updated or not)
2. The number of movie mods active will now show up above the Refresh button when the number is higher than zero
3. An additional column has been added to the grid showing the mod type. If you scroll to the right you will see the Mod Type and the Author columns. The Mod Type is useful for tracking down Movie mods, since those are currently loaded up all the time with KMM until the 1.9 udpate. The only way to remove them before the 1.9 update is the unsubscribe to the mod and make sure the file doesn't show up in KMM.
4. A warning has been added when you try to launch and the grid is not sorted by the File Name column in Alphabetical order. 

## Known Issue - Napoleon will not work at this time as it doesn't not support the new Content folder scheme and I need to set the manager up to support it strictly from Data. I will add this to a future version.  

## Changes and Bug Fixes for v1.8.6:
Changes and Bug Fixes for v1.8.6:
v1.8.6 is a minor update to show out of date mods for the new "The Prophet and the Warlock" patch while I'm wrapping up the v1.9 update. I'll do a major cleanup of the readme and add a lot more FAQ/instructions soon.
Out of date mods are now flagged correctly for the 17th of April, 2019 except for mods which are not on steam (local mods will stay white as I have no way to know if they are updated or not)
Removed the capability to handle moving around game installs as this was leading the DLC missing issue for some users. Will revisit this again in v1.9. For now, you can use the "Option - Detect Game Installations" to update pathing or manually delete the GameData.xml from the "Kaedrin Mod Manager" folder in your Roaming Application Data (%appdata% in File Explorer).
File loading has been rewritten to handle the DLC missing for some users with more changes.

## Changes and Bug Fixes for v1.8.2:

v1.8.2 is a minor update to show out of date mods for the new Vampire Coast patch while I'm still working on the new features for v1.9. One of them that is currently available as a Beta is the Merge Mods capability with the following features:
1. All mods that you have checked will be merged together.
2. The mod you right click to choose the option for will be the one that is chosen for creating a new file with _merge appended at the end.  For example, right clicking on "test.pack" will merge all the checked mods in the grid into a single pack named "test_merge.pack"
3. Picking the same pack again for the option will currently overwrite the file without warning.
4. Conflicts are NOT HANDLED AT THIS TIME! Merge modules with conflicts at your own risk of crashing. You have been warned.

## Changes and Bug Fixes for v1.8:

### Steam is here!!
- [x] Steam Information is now pulled! (Title and Author, though Author needs to be scrolled to the right to see)
- [x] Right Click option to open a subscribed module's Steam page via Web Browser has been added
- [x] Right Click option to open a subscribed module's Steam page via Steam client has been added
- [x] Upon refresh, a backup is now done
- [x] 0.0 sized pack files found in Content now deleted and a message informs the user of which one
- [x] 0.0 sized pack files are removed from the data folder
- [x] Date column fixed to be a consistent format that is sort friendly
- [x] Date column is now cultural friendly (including sort capability)
- [x] Revert to Vanilla File Menu option renamed to Reset Program Settings
- [x] Fixed Rename Bug
- [x] Upon specifying a location for backups, a backup is now immediately done
- [x] Upon enabling the option to do backups (and a location is already set), a backup is now immediately done
- [x] Out of date mods are colored orange
- [x] Recently changed mods are now colored light blue (instead of yellow)
- [x] Removed Revert to Vanilla button
- [x] Added bypass CA Launcher option
- [x] Error message added for when steam isn't available when KMM is started
- [x] Link to Total War Mod Troubleshooting Guide added
- [x] Priority Change tooltip updated with new text

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

## Changes due to the new Creative Assembly Launcher/Mod Manager:

Due to a change in the Creative Assembly Launcher/Mod Manager and how it handles .pack files, I've added new code to handle this and make it mod friendly again. The CA Launcher now uses both the \Steam\steamapps\workshop\content and \Steam\steamapps\common\Total War WARHAMMER II\data folder for mods. Any mods that have been added or updated since the new CA Launcher was released have been switched to use the new Content folder and the .pack file will ONLY be found there by default. So mod developers/users who are looking for their files and can't find them... that's where they dissapeared to. 

In order to keep it easy for developers to open mods with PFM/RPFM and for the KMM to see all of the mods in a single place, I've added code to copy all of the mods out of the content folder and in to the data folder. The files are left in the content folder once this happens (and the old format .bin files are also removed to save space and speed up the process). Any time a mod is updated it will be downloaded like normal. If the "last modified" timestamp on your data folder is newer than the one in content, the file will not be updated so users who modify the works of a mod developer should rename the work in order to ensure getting future updates. I'll be working to cache this out in an xml file to speed the process up more for the v1.6 update.
