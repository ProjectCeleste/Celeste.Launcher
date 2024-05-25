; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Age of Empires Online"
#define MyAppVersion "1.0"
#define MyAppPublisher "Celeste"
#define MyAppURL "https://projectceleste.com/"
#define MyAppExeName "Celeste Launcher.exe"
#define MyAppScannerName "Celeste Game Scanner UI.exe"
#define MyAppCopyright "Copyright (C) 2017-2024 Celeste Project"

#define NO_COMPRESSION 0

#include "CodeDependencies.iss"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{310CECDC-62C3-440B-BD34-053582C22A04}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
AppCopyright={#MyAppCopyright}
DefaultDirName={userappdata}\ProjectCeleste
DisableProgramGroupPage=yes
SetupIconFile=icon.ico
; In case of dependencies - just in case
; AlwaysRestart=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=CelesteInstaller
#if NO_COMPRESSION
Compression=none
#else
Compression=lzma
#endif
SolidCompression=yes
WizardStyle=modern
DisableWelcomePage=no
WizardImageFile=Left.bmp
WizardSmallImageFile=NewLauncherIcon.bmp

; DiskSpanning=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Source: "aoeo-install\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
Source: "{#GetEnv('CELESTE_LAUNCHER_OUTPUT')}\*"; DestDir: {app}; Flags: ignoreversion recursesubdirs createallsubdirs

[Code]

function InitializeSetup: Boolean;
begin
  Dependency_AddDotNet35;
  Dependency_AddVC2010;
  Dependency_AddVC2015To2022;
  Dependency_AddDirectX;

  Result := True;
end;

// DIRECT PLAY!
function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  ResultCode: Integer; 
begin
  Exec('Dism', ' /online /enable-feature /featurename:DirectPlay /All /NoRestart', '', SW_HIDE, ewWaitUntilTerminated, ResultCode)
end;

procedure InitializeWizard;
begin
  Dependency_Set_Additional_Task_Memo('%1Activating DirectPlay');
end;


[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppScannerName}"; Parameters: "{app} enUS false true"; StatusMsg: "Running Game Scanner..."; Flags: skipifsilent
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange("Celeste Launcher", '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Messages]
UninstalledAll=%1 was successfully removed from your computer. You may now delete the rest of the game folder to remove the game files.




