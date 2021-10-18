@echo off
@setlocal enableextensions
@cd /d "%~dp0"

IF "%~1" == "createLink" goto createLink

CALL :checkPrivileges
:createLink
ECHO CREATING LINKS...
FOR /F "tokens=1,2 delims=," %%G IN (LinkList.txt) DO (
  ECHO * Creating symbolink link: %%G
  RMDIR /S /Q "%~dp0%%G"
  MKLINK /d "%~dp0%%G" "%~dp0%%H"
  ECHO.
)
goto eof

:checkPrivileges 
ECHO CHECK PRIVILEGES...
NET FILE 1>NUL 2>NUL
if ERRORLEVEL 1 ( ECHO [91mYou must run as Administrator rights.[0m & ECHO Exiting... & ECHO. & PAUSE & EXIT /D)
exit /b

:eof
IF "%1"=="" PAUSE