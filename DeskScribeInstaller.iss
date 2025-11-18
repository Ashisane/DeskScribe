; DeskScribe Inno Setup script - simple installer (per-user, no admin required)
[Setup]
AppName=DeskScribe
AppVersion=1.0.0
DefaultDirName={userappdata}\DeskScribe
DisableProgramGroupPage=yes
OutputDir=.
OutputBaseFilename=DeskScribe-Setup
Compression=lzma
SolidCompression=yes
DefaultGroupName=DeskScribe
PrivilegesRequired=lowest
SetupIconFile="C:\Users\UTKARSH\Desktop\desk-scribe\src\DeskScribe.App\Resources\app.ico"
WizardSmallImageFile="C:\Users\UTKARSH\Desktop\desk-scribe\src\DeskScribe.App\Resources\app_banner.bmp"

[Files]
; Adjust Source to the folder where you published your app
Source: "C:\Users\UTKARSH\Desktop\desk-scribe\publish\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{userdesktop}\DeskScribe"; Filename: "{app}\DeskScribe.exe"

[Run]
Filename: "{app}\DeskScribe.exe"; Description: "Launch DeskScribe"; Flags: nowait postinstall skipifsilent

[Registry]
; Add per-user Run shortcut to start DeskScribe on login with --startup
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "DeskScribe"; ValueData: """{app}\DeskScribe.exe"" --startup"; Flags: preserveStringType

[UninstallDelete]
Type: files; Name: "{app}\*.*"