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
            openSystemsCfg.ShowDialog()
            folder = openSystemsCfg.FileName
            My.Settings.es_settings_loc = openSystemsCfg.FileName
        Else
            folder = My.Settings.es_settings_loc
        End If
        If Not System.IO.File.Exists(My.Settings.es_settings_loc) Then
            MsgBox("es_systems.cfg not selected!")
            End
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
        If description.Text = "" Then
            description.BackColor = Color.Black
            description.ScrollBars = ScrollBars.None
        Else
            description.BackColor = Color.Beige
            description.ScrollBars = ScrollBars.Vertical
        End If
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
        If System.IO.File.Exists(consolelist.Item(systemBox.SelectedIndex + 1).getGamelist) Then
            gamelist = XML.readGame(consolelist.Item(systemBox.SelectedIndex + 1).getGamelist, consolelist.Item(systemBox.SelectedIndex + 1))
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
            cover.ImageLocation = vbNull
            description.Text = ""
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

    Private Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        saveScript.FileName = selectedgame.getName + ".bat"
        saveScript.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles saveScript.FileOk
        IO.File.WriteAllText(saveScript.FileName, consolelist.Item(systemBox.SelectedIndex + 1).makeScript(selectedgame.getpath()))
    End Sub

    Private Sub quit_Click(sender As Object, e As EventArgs) Handles quit.Click
        My.Settings.Save()
        End
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        Select Case keyData
            Case Keys.Up
                If gameBox.SelectedIndex = 0 Then
                    gameBox.SelectedIndex = gameBox.Items.Count - 1
                Else
                    gameBox.SelectedIndex = gameBox.SelectedIndex - 1
                End If
                Return True
            Case Keys.Right
                If systemBox.SelectedIndex = systemBox.Items.Count - 1 Then
                    systemBox.SelectedIndex = 0
                Else
                    systemBox.SelectedIndex = systemBox.SelectedIndex + 1
                End If
                Return True
            Case Keys.Down
                If gameBox.SelectedIndex = gameBox.Items.Count - 1 Then
                    gameBox.SelectedIndex = 0
                Else
                    gameBox.SelectedIndex = gameBox.SelectedIndex + 1
                End If
                Return True
            Case Keys.Left
                If systemBox.SelectedIndex = 0 Then
                    systemBox.SelectedIndex = systemBox.Items.Count - 1
                Else
                    systemBox.SelectedIndex = systemBox.SelectedIndex - 1
                End If
                Return True
            Case Keys.Oemcomma
                If genreBox.SelectedIndex = 0 Then
                    genreBox.SelectedIndex = genreBox.Items.Count - 1
                Else
                    genreBox.SelectedIndex = genreBox.SelectedIndex - 1
                End If
                Return True
            Case Keys.OemPeriod
                If genreBox.SelectedIndex = genreBox.Items.Count - 1 Then
                    genreBox.SelectedIndex = 0
                Else
                    genreBox.SelectedIndex = genreBox.SelectedIndex + 1
                End If
                Return True
            Case Keys.Enter
                consolelist.Item(systemBox.SelectedIndex + 1).run(selectedgame.getpath())
                Return True
            Case Keys.Escape
                End
                Return True
            Case Keys.Space
                saveScript.FileName = selectedgame.getName + ".bat"
                saveScript.ShowDialog()
                Return True
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

End Class
