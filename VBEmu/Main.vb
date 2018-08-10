Imports System.Collections.Specialized

Public Class Main
    Dim gamelist
    Dim folder As String
    Dim consolelist As Collection
    Dim gamelistavailable As Boolean
    Dim selectedgame As Game
    Dim genrelist
    Dim developerlist
    Dim filteredgames
    Dim genrefilter As Boolean
    Dim developerfilter As Boolean
    Dim firstrun

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.es_settings_loc = "" Then
            OpenFileDialog1.ShowDialog()
            folder = OpenFileDialog1.FileName
            My.Settings.es_settings_loc = OpenFileDialog1.FileName
        Else
            folder = My.Settings.es_settings_loc
        End If
        consolelist = XML.readGamesystem(folder)
        For Each c In consolelist
            systemBox.Items.Add(c.getfullName())
        Next
        systemBox.SelectedIndex = 0
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gameBox.SelectedIndexChanged
        If gamelistavailable Then
            For Each g In gamelist
                If gameBox.SelectedItem = g.getName() Then
                    selectedgame = g
                    Me.Text = "(" + systemBox.SelectedItem + ") " + g.getName() + " (" + g.getDeveloper + ") " + g.getGenre
                    cover.ImageLocation = consolelist.Item(systemBox.SelectedIndex + 1).getPath() + g.getImage()
                    description.Text = g.getDescription()
                End If
            Next
        Else
            description.Text = vbNullString
            cover.Image = cover.ErrorImage
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub reloadGames(ByVal romdir)
        genreBox.Enabled = True
        devBox.Enabled = True
        gameBox.Items.Clear()
        gamelist = New Collection
        genrelist = New StringCollection()
        developerlist = New StringCollection()
        genrelist.Add("All")
        developerlist.Add("All")
        If System.IO.File.Exists(romdir + "\gamelist.xml") Then
            gamelist = XML.readGame(romdir + "\gamelist.xml", consolelist.Item(systemBox.SelectedIndex + 1))
            For Each g In gamelist
                If g.getGenre().trim <> "" And Not genrelist.Contains(g.getGenre().trim) Then
                    genrelist.Add(g.getGenre().trim)
                End If
                If g.getDeveloper().trim <> "" And Not developerlist.Contains(g.getDeveloper().trim) Then
                    developerlist.Add(g.getDeveloper())
                End If
                gameBox.Items.Add(g.getName())
                ProgressBar1.PerformStep()
            Next
            genreBox.DataSource = genrelist
            devBox.DataSource = developerlist
            gamelistavailable = True
            filteredgames = gamelist
            gameBox.SelectedIndex = 0
            ProgressBar1.Value = ProgressBar1.Maximum
        Else
            genreBox.Enabled = False
            devBox.Enabled = False
            Dim files() As String = IO.Directory.GetFiles(romdir)
            For Each file As String In files
                gameBox.Items.Add(IO.Path.GetFileName(file))
            Next
            gamelistavailable = False
            Me.Text = systemBox.SelectedItem
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles systemBox.SelectedIndexChanged
        reloadGames(consolelist.Item(systemBox.SelectedIndex + 1).getPath())
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles gameBox.DoubleClick
        consolelist.Item(systemBox.SelectedIndex + 1).run(selectedgame.getpath())
    End Sub

    Private Sub filter(ByVal genref, ByVal developerf)
        If gamelistavailable Then
            gameBox.Items.Clear()
            filteredgames = New Collection
            If genreBox.SelectedItem <> "All" And devBox.SelectedItem <> "All" Then
                For Each g In gamelist
                    If g.getGenre().trim = genref.trim And g.getDeveloper().trim = developerf.trim Then
                        filteredgames.add(g)
                    End If
                Next
            ElseIf genreBox.SelectedItem <> "All" Then
                For Each g In gamelist
                    If g.getGenre().trim = genref.trim Then
                        filteredgames.add(g)
                    End If
                Next
            ElseIf devBox.SelectedItem <> "All" Then
                For Each g In gamelist
                    If g.getDeveloper().trim = developerf.trim Then
                        filteredgames.add(g)
                    End If
                Next
            Else
                filteredgames = gamelist
            End If
            For Each g In filteredgames
                gameBox.Items.Add(g.getName())
            Next
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles genreBox.SelectedIndexChanged
        filteredgames = New Collection
        filter(genreBox.SelectedItem.trim, devBox.SelectedItem.trim)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles devBox.SelectedIndexChanged
        filteredgames = New Collection
        filter(genreBox.SelectedItem.trim, devBox.SelectedItem.trim)
    End Sub
End Class
