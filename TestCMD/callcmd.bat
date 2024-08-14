@echo off
:: Command Line mode
TestCMD -cmd
if ERRORLEVEL 1 (
    echo The command failed with errorlevel %ERRORLEVEL%.
) else (
    echo The command succeeded with errorlevel %ERRORLEVEL%.
)
pause

:: Wrong argument
TestCMD -xyz
if ERRORLEVEL 1 (
    echo The command failed with errorlevel %ERRORLEVEL%.
) else (
    echo The command succeeded with errorlevel %ERRORLEVEL%.
)
pause

:: Startup with data in window mode
TestCMD -info "Hello Batchfile"
pause

:: Startup with data in window mode
TestCMD
pause