Public Class Main
    Dim gamelist As Collection
    Dim folder As String
    Dim consolelist As Collection
    Dim gamelistavailable As Boolean
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        folder = "c:\users\moas\es_systems.cfg"
        consolelist = XML.readGamesystem(folder)
        For Each c In consolelist
            ListBox2.Items.Add(c.getfullName())
        Next
        ListBox2.SelectedIndex = 0
        reloadGames(consolelist.Item(1).getPath())
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If gamelistavailable Then
            genre.Text = gamelist.Item(ListBox1.SelectedIndex + 1).getGenre()
            developer.Text = gamelist.Item(ListBox1.SelectedIndex + 1).getDeveloper()
            cover.ImageLocation = consolelist.Item(ListBox2.SelectedIndex + 1).getPath() + gamelist.Item(ListBox1.SelectedIndex + 1).getImage()
            description.Text = gamelist.Item(ListBox1.SelectedIndex + 1).getDescription()
        Else
            genre.Text = vbNullString
            developer.Text = vbNullString
            description.Text = vbNullString
            cover.Image = cover.ErrorImage
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub reloadGames(ByVal romdir)
        ListBox1.Items.Clear()
        If System.IO.File.Exists(romdir + "\gamelist.xml") Then
            ListBox1.Items.Clear()
            gamelist = XML.readGame(romdir + "\gamelist.xml")
            For Each g In gamelist
                ListBox1.Items.Add(g.getName())
            Next
            ListBox1.SelectedIndex = 0
            gamelistavailable = True
        Else
            Dim files() As String = IO.Directory.GetFiles(romdir)
            For Each file As String In files
                ListBox1.Items.Add(IO.Path.GetFileName(file))
            Next
            gamelistavailable = False
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        reloadGames(consolelist.Item(ListBox2.SelectedIndex + 1).getPath())
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        consolelist.Item(ListBox2.SelectedIndex + 1).run(gamelist.Item(ListBox1.SelectedIndex + 1).getpath())
    End Sub
End Class
