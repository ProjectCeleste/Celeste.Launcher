# Change Log

## [v3.1.0]

**New features:**

- Complete redesign of the user interface in WPF
- A new friend list
- Translated launcher to German, Spanish, French, Italian, Portugese, and Chinese
- Game scanner will show progress status on the taskbar
- Can start the game without using the mouse
- Fast download (optional) available when starting a game scan
- Log files that makes it easier to diagnose problems

**Changed:**
- Fixed CPU and memory leaks in game scanner
- More responsive UI
- The launcher runs on .NET Framework 4.8
- Passwords are stored using Windows Credentials Manager
- Improved UI, and clearer text and dialog messages
- Many small code-cleanups, tweaks, and fixes

## [v2.1.2.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.1.2.0) (2019-10-08)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.1.1.0...v2.1.2.0)

**Bug Fix:**

- Fix delay due to "Quick Scan" when "Play Button" is pressed.

## [v2.1.1.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.1.1.0) (2019-10-05)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.1.0.0...v2.1.1.0)

**Bug Fix:**

- Fix issue with "Quick Scan" not beeing initialized.

## [v2.1.0.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.1.0.0) (2019-10-05)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.8.0...v2.1.0.0)

**Celeste Launcher will now require .NETFramework version 4.7 instead of version 4.5!**

**Game Scanner Enhancements:**

- Add "Download Boost" (you need to check the box to enable it before run an scan and enjoy faster game update).
- File check execution speed has been improved (avg. 3x faster).

## [v2.0.8.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.8.0) (2019-05-20)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.7.0...v2.0.8.0)

- Add support for "NatPunchtrough".
- Remove support for "old beta editor".

## [v2.0.7.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.7.0) (2018-06-23)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.6.0...v2.0.7.0)

**New Features:**

- "Scenario Manager" : Manage your scenario for offline playing.

**Enhancements:**

- UI Improvement.
- Support for xLive Legacy "removed".

## [v2.0.6.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.6.0) (2018-06-12)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.5.0...v2.0.6.0)

**Enhancements:**

- Scenario from "Game Editor" can now be easilly acces in "Offline Mode" using folder "/Scenario/CustomScn/".
- Ai script are now available in "Game Editor".

**Bug Fix:**

- Fix issue with "Game Editor install state".
- Fix issue where "Game Editor" use an incorrect "user folder" when started from launcher.

## [v2.0.5.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.5.0) (2018-05-04)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.4.0...v2.0.5.0)

**New Features:**

- "Offline Mode" : You can now play custom scenario made by the community using "Game Editor".

**Enhancements:**

- Necessary file for "Diagnostic Mode" are now automatically downloaded if missing.
- "Game Editor" can now be launched without an internet connection available.

## [v2.0.4.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.4.0) (2018-03-27)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.3.2...v2.0.4.0)

**New Features:**

- "Game Editor" : Now you can install and use the "Game Editor" (Tools Menu) to start designing map and maybe see them include in "Celeste Fan Project".
- Windows Firewall Helper : Easily configure in few click the default windows firewall for "Celeste Fan Project Launcher" and the game.

**Enhancements:**

- Windows Features Helper is now disabled on Windows 7 to avoid confusion (not needed).

## [v2.0.3.2](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.3.2) (2018-02-26)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.3.1...v2.0.3.2)

**Bug Fix:**

- Fix issue with auto-updater.

## [v2.0.3.1](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.3.1) (2018-02-20)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.3.0...v2.0.3.1)

**Enhancements:**

- Add support for "procdump" to help debug game issue.
- Add fallback server for "GameScan".

## [v2.0.3.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.3.0) (2018-02-13)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.2.0...v2.0.3.0)

**New Features:**

- Friends: You can now add and see your friends activity.

**Bug Fix:**

- Fix issue with change password.

## [v2.0.2.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.2.0) (2018-01-31)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.1.4...v2.0.2.0)

**Enhancements:**

- "GameScan" will now use as temporary folder an folder who will be create at the same location has the launcher instead of using windows temporary folder.
- "Quick GameScan" who was running before the game start has been improved its now a lot faster than before (and will no more display an UI).
- "GameScan" will no more close if an error occur in order to be able to read the "progress log".
- "Beta Update" in "GameScan" has been removed and replaced by two radio box in order to select the xLive version you want to use.

Note: "Sparta XLive" is now the default xLive version, if you have issue with it use "Legacy XLive" version.

## [v2.0.1.4](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.1.4) (2018-01-08)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.1.3...v2.0.1.4)

**New Features:**

- Steam Helper: Allow to easily make "Celeste Fan Project" compatible with "Steam" (can be found in "Tools/Helpers" menu).

Note: Now steam_api.dll will be auto removed if your game installation is not "Steam" compatible.

## [v2.0.1.3](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.1.3) (2017-12-23)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.1.2...v2.0.1.3)

**Bug Fix:**

- Fix issue with game scan.

## [v2.0.1.2](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.1.2) (2017-12-22)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.1.1...v2.0.1.2)

**Bug Fix:**

- Various minor fix.

## [v2.0.1.1](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.1.1) (2017-11-24)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.1.0...v2.0.1.1)

**Bug Fix:**

- Fix login issue with new server update.

## [v2.0.1.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.1.0) (2017-11-21)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.0.1...v2.0.1.0)

**New Features:**

- Windows Features Helper: Enable and install in one click "Direct Play" and ".Net 3.5" on Windows 8 and more (can be found in "Tools" menu).

**Bug Fix:**

- Fix issue with "change password".
- Fix issue where login and register button can be disabled when application start.

**Enhancements:**

- Launcher and "Game File" update can now be ignored (not recommended, and you can maybe not connect if its an forced update).

## [v2.0.0.1](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.0.1) (2017-11-16)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v2.0.0.0...v2.0.0.1)

**Bug Fix:**

- Beta update setting will now be remembered.
- Fix issue with registration.
- Fix issue with auto-updater.


## [v2.0.0.0](https://github.com/ProjectCeleste/Celeste_Launcher/tree/v2.0.0.0) (2017-11-14)

[Full Changelog](https://github.com/ProjectCeleste/Celeste_Launcher/compare/v1.x.x.x...v2.0.0.0)

**New Features:**

- News Block: See latest news about Celeste directly from the launcher (WIP).
- Game Scanner: Allow to easily install/update/repair game files (An quick scan will be run each time you launch the game using the launcher).
- Updater: The launcher will now auto-update on start if a new version is available.

Note: Game update and launcher update will now be separate with thoose last two new features available to handle them.

**Enhancements:**

- Common: UI Redesigned.
- Login: Add Auto-Login option.
- MP Settings: Add an option to set manually an IP ("Connection Type = Other").
- MP Settings: Improve option "LAN Only".
- ...

