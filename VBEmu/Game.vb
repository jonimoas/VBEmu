Public Class Game
    Private path As String
    Private name As String
    Private image As String
    Private genre As String
    Private developer As String
    Private description As String
    Public Sub New(ByVal path, ByVal name, ByVal image, ByVal genre, ByVal developer, ByVal description)
        Me.path = path
        Me.name = name
        Me.image = image
        Me.genre = genre
        Me.developer = developer
        Me.description = description
    End Sub
    Public Function getName()
        Return name
    End Function
    Public Function getDeveloper()
        Return developer
    End Function
    Public Function getImage()
        Return image
    End Function
    Public Function getGenre()
        Return genre
    End Function
    Public Function getDescription()
        Return description
    End Function
    Public Function getpath()
        Return path
    End Function

End Class
