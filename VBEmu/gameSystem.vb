Public Class gameSystem
    Private fullname As String
    Private name As String
    Private path As String
    Private command As String
    Private extensions As String()
    Dim quote As String = "\"""
    Private gamelist As String
    Public Sub New(ByVal path, ByVal name, ByVal fullname, ByVal command, ByVal extension)
        Me.path = path
        Me.name = name
        Me.fullname = fullname
        Me.command = command
        Me.extensions = extension.split(".")
        Me.gamelist = path + "\gamelist.xml"
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
        IO.File.WriteAllText("run.bat", makeScript(game))
        Process.Start("run.bat")
    End Sub
    Public Function makeScript(game)
        Return command.Replace("%ROM%", Chr(34) + path + "\" + game + Chr(34)).Replace("\.", "").Replace("/", "\").Replace(" &", "&").Replace("\Q", "/Q").Replace("\F", "/F")
    End Function
    Public Sub setGamelist(gamelistloc)
        Me.gamelist = gamelistloc
    End Sub
    Public Function getGamelist()
        Return gamelist
    End Function
End Class
