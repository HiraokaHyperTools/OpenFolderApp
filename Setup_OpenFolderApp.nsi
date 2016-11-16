; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

!define APP "OpenFolderApp"
!system 'DefineAsmVer.exe "bin\DEBUG\${APP}.exe" "!define VER ""[SVER]"" " > Tmpver.nsh'
!include "Tmpver.nsh"

; The name of the installer
Name "${APP}"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\NSIS_Example2" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page directory
;Page components
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section ""

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File /r /x "*.vshost.*" "bin\DEBUG\*.*"
  
  WriteRegStr HKCR ".ofa" "" "OpenFolderApp"
  WriteRegStr HKCR ".ofa\ShellNew" "NullFile" ""

  WriteRegStr HKCR "OpenFolderApp" "" "フォルダー ショートカット"
  WriteRegStr HKCR "OpenFolderApp" "NeverShowExt" ""
  WriteRegStr HKCR "OpenFolderApp\DefaultIcon" "" "$INSTDIR\${APP}.exe"
  WriteRegStr HKCR "OpenFolderApp\shell\open\command" "" '"$INSTDIR\${APP}.exe" /o "%1"'
  WriteRegStr HKCR "OpenFolderApp\shell\edit\command" "" '"$INSTDIR\${APP}.exe" /e "%1"'

  ; Write the installation path into the registry
  WriteRegStr HKLM "SOFTWARE\${APP}" "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${APP}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
  System::Call 'Shell32::SHChangeNotify(i 0x8000000, i 0, i 0, i 0)'
  
SectionEnd

; Optional section (can be disabled by the user)

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove files and uninstaller
  Delete "$INSTDIR\${APP}.exe"

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKLM "SOFTWARE\${APP}"

  ; Remove files and uninstaller
  Delete "$INSTDIR\uninstall.exe"

  ; Remove shortcuts, if any

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
