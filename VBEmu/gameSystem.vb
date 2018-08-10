Public Class gameSystem
    Private fullname As String
    Private name As String
    Private path As String
    Private command As String
    Private extensions As String()
    Dim quote As String = "\"""
    Public Sub New(ByVal path, ByVal name, ByVal fullname, ByVal command, ByVal extension)
        Me.path = path
        Me.name = name
        Me.fullname = fullname
        Me.command = command
        Me.extensions = extension.split(".")
    End Sub
    Public Function getName()
        Return name
    End Function
    Public Function getPath()
        Return path
    End Function
    Public Function getFullname()
        Return fullname
    End Function
    Public Function getExtensions()
        Return extensions
    End Function
    Public Sub run(game)
        Dim torun = command.Replace("%ROM%", Chr(34) + path + "\" + game + Chr(34)).Replace("\.", "").Replace("/", "\").Replace(" &", "&").Replace("\Q", "/Q").Replace("\F", "/F")
        IO.File.WriteAllText("run.bat", torun)
        Clipboard.SetText(torun)
        Process.Start("run.bat")
    End Sub
End Class
