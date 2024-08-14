Imports System
Imports System.IO
Imports System.Runtime.InteropServices

Class Application
    <DllImport("kernel32.dll")>
    Private Shared Function AllocConsole() As Boolean
    End Function

    <DllImport("kernel32.dll")>
    Private Shared Function AttachConsole(dwProcessId As Integer) As Boolean
    End Function

    Private Const ATTACH_PARENT_PROCESS As Integer = -1
    Private Shared Sub AttachConsoleToParent()




        If Debugger.IsAttached Then
            ' allocate Console
            AllocConsole()
        Else
            ' Attach to the existing console (the parent process's console)
            AttachConsole(ATTACH_PARENT_PROCESS)
        End If

        ' Redirect standard output and error to the attached console
        Console.SetIn(New StreamReader(Console.OpenStandardInput()))
        Console.SetOut(New StreamWriter(Console.OpenStandardOutput()) With {.AutoFlush = True})
        Console.SetError(New StreamWriter(Console.OpenStandardError()) With {.AutoFlush = True})

        Try
            ' Clear input buffer
            While Console.KeyAvailable
                Console.ReadKey(True)
            End While
            ' Flush the console buffer
            Console.Out.Flush()
            Console.Error.Flush()
        Catch ex As Exception

        End Try

        'Console.WriteLine()
    End Sub
    Private Shared Sub ProcessCommandLineArgs(args As String())
        ' Example: Check for a specific argument and perform an action
        If args(0).ToLower() = "-cmd" Then
            ' Allocate a console for the application
            AttachConsoleToParent()

            ' Execute the command-line functionality
            Console.WriteLine("Command-line mode activated. Exit code 0")
            ' Example of functionality: Write to a file, perform a calculation, etc.
            ' After finishing, you can exit the application.
            Environment.Exit(0)
            ' Optionally exit if this is purely a command-line tool usage

            Application.Current.Shutdown()
        ElseIf args(0).ToLower() = "-info" Then
            Dim info As String = args(1)
            ' Load the main window for normal WPF usage
            Dim mainWindow As New MainWindow(info)
            mainWindow.Show()
        Else
            ' Allocate a console for the application
            AttachConsoleToParent()

            ' Handle other command-line arguments or show usage information
            Console.WriteLine("Invalid argument. Usage: -cmd")
            Console.WriteLine("Usage:")
            Console.WriteLine("   -cmd                   to run in command-line mode and exit")
            Console.WriteLine("   -info ""Hello World""  to start wpf app with given Text")
            Console.WriteLine("                          No argument starts default WPF Application")
            Environment.Exit(1)
            ' Optionally exit if this is purely a command-line tool usage
            Application.Current.Shutdown()
        End If
    End Sub

    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup

        ' Get command-line arguments
        Dim args As String() = e.Args

        ' Check if any arguments were passed
        If args.Length > 0 Then
            ' Process the arguments here
            ProcessCommandLineArgs(args)
        Else
            ' Load the main window for normal WPF usage
            Dim mainWindow As New MainWindow()
            mainWindow.Show()
        End If
    End Sub
End Class
