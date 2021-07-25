Revised .15 draft change list

x1. Out of date check for Warhammer 2 updated for the Silence and the Fury DLC
2. New option to force trigger downloads when a date discrepancy is detected between the Steam API and Content folder timestamps (Options -> Check Steam for Out of Date Mods). A message will pop up indicating if any are found letting you know that you need to refresh KMM after they are done downloading (check Steam and wait until they are finished downloading before clicking Refresh)
x3. Troubleshooting Guide URL updated
x4. Profiles menu now has a new option for "Export Current Profile to Clipboard" 
x5. Profiles menu now has a new option for "Export Current Profile to HTML" which includes URLs to their steam page(s)
6. Profiles menu now has a new option for "Export All Mods to HTML" which includes URLs to their steam page(s)
7. Profiles menu now has a new option for "Export Selected Mods to HTML" which includes URLs to their steam page(s)
8. Dark mode options has been added. Changing to dark mode will require a restart of KMM (as will changing back).
x9. Delete from data now correctly whacks the PNG file in all cases
x10. A warning is now given when a Webp malformed image is detected. .WEBP images renamed as .PNG do not work with KMM and .Net programs
11. Steamworks has been rolled back for greater compatibility at this time
12. Pack files that have a % in the pack file name now properly give a warning instead of just erroring out
13. A VirusTotal scan report from https://www.virustotal.com/ has been added to the .zip for each release.
14. Pack type doesn't show when Conflict detect is on?
15. New Right Click option "Delete from Data (Multiple)"
16. New Right Click option "Unsubscribe from Steam and Delete (Multiple)"
x17. Right Click option "Unsubscribe (Steam) and Delete" renamed "Unsubscribe from Steam and Delete"
x18. RPFM specific files "extra_packfile.rpfm_reserved", "settings.rpfm_reserved" and "notes.rpfm_reserved" are now excluded from conflict checks
19. Open Data folder option
20. Open Content folder option
21. Clean Data and Exit
22. New Option to delete Content files for out of date check (to ensure maximum ability to get the latest, not relying on steam to replace out of date mods with the download but deleting the Content version then letting the download get a fresh copy)
x23. Blah Blah Not For You

Those without an x in front still need verification/their feature branch checked in/will change as I constantly update the .15 line.


Moved to 1.8.16
1. New option to disable generals for custom battles which can be found in the Options menu with the name "Disable Generals for Custom Battles." Please note that this setting affects the user script which affects multiplayer compatibility. All users must have this setting enabled or disabled equally.
2. Full support for all of the settings in the User Script Modification document. Included in the .zip is a .TXT file named "UserScriptModification.txt" which has all of the options option added (Beta) for War2 with their defaults already set. Changing the true/false value will cause KMM to set the value the next time KMM starts and the Launch button is pressed. 
3. New option to disable loading of mod contents (Alpha, FAST performance but some options may not work, ~4x)
4. Test two mods with identical names
5. Add option to delete CA appdata folder
6. Add Troy Support
7. "When sorting by number of conflicts it still lists by order of 1st character rather than number order ie 1, 10, 100, 2, 23, 240 rather than 001. 002. 010, 023, 100, 240." Sort order will be fixed for number of conflicts.
Support for two new RPFM features added
8. New Right-Click option for "Re-Subscribe to Mod" added which will unsubscribe to the mod, delete it from Content and Data, Subscribe to it, and trigger a download
9. Support for two new features in RPFM
10. Error message improved when the game hasn't been run yet (CA AppData directory does not exist)
11. New game detection system detects game location moves
12. Auto-unsub deleted/hidden mods (that are not yours) option
13. Add mergedmodslist.txt to merged mods to display which packs were sourced, including date
14. New options for Open folders in windows explorer for mod author section
15. Merge mod capability will only allow “movie only” and “non-movie” groups to be merged
16. Check for downloads option added
17. Profiles library xml added. On load profile, check if alpha and pop a message if not. Store decisions to preserve non-alpha. 
18. Try to fix adding mods to profiles being added out of alpha order (whatever the sequence is that causes this) so that profiles are always alpha unless a user chooses. When a priority change is done, disable the alpha bark
19. New option -> Deep Clean for Data (all user hak/png files scrubbed, not just loaded ones)
20. Move all new menu code to Menu.cs

Moved to 1.9:
1. Out of Date check potentially added for 3K
2. Readme option/link at the top added (disabled option moved out of Information, will be incredibly barebones for now as it's going to be more of a FAQ till 1.9 and Content loading get completed)
3. Export Profile to Steam Collection (or Update)
4. Import Profile from Steam Collection 
5. Validate and Clean Data option added which will compare Content and Data and remove anything from Data that is not in Content. WARNING: This is meant for maximum multiplayer compatibility and is NOT meant for mod developers using the Data folder. Be careful when you use this option!
6. Uploader now available
7. Add set alternate path option for Uploads to Author section
8. Add set alternate path option for Mods to Author section
9. Option to add new pack to auto merge
10. Add new option to update auto-merge with modlist.txt file
11. Add option to add steam item to favorites?
12. Backup path can now be set separately for each game
13.  Ability for Mod Author's to have a readme.txt displayed via Right Click option
14. Mods missing from profile on load will be reported to the user
15. Napoleon Support
16. Global search Steam Data, File name, and file contents (if enabled for the last)
17. Review Shogun 2 vs Saga: Fall of the Samurai
