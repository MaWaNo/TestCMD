# WPF&Console Test Application

A solution for using WPF apps as command line tool and also start depending on command line arguments.

## Background

The application output stays a window project (WPF) but attaches to the Console in order to Console.Write.
For command line usage in applications, that are always used in window mode, the use of `Environment.GetCommandLineArgs` at startup would be enough.

## Issues
There is a strange behaviour in direct usage from console window.
The console output should be
```
D:\Repos\testing\TestCMD\TestCMD\bin\Release\net6.0-windows>testcmd.bat -cmd
Command-line mode activated. Exit code 0
```
but is
```
D:\Repos\testing\TestCMD\TestCMD\bin\Release\net6.0-windows>Command-line mode activated. Exit code 0

```

It works normally, when the exe is called form a batch file and error levels are set correctly.

## Usage

Here is an example batch file:

```batch
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
```
