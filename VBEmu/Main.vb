﻿Imports System.Collections.Specialized
Imports System.Threading
Imports System.Runtime.InteropServices


Public Class Main
    Public joyConf = False
    Dim gamelist As new Collection
    Dim folder As String
    Dim consolelist As Collection
    Dim gamelistavailable As Boolean
    Dim selectedgame As Game
    Dim genrelist
    Dim developerlist
    Dim gameControlList
    Dim filteredgames
    Dim genrefilter As Boolean
    Dim developerfilter As Boolean
    Dim firstrun
    Dim windowFocus = True
    Dim t As Thread
    Dim joyThread As Thread
    Dim globalgamelist = New Collection
    Dim globalgenrelist = New Collection
    Dim globaldevlist = New Collection
    Dim globalgamenames = New Collection
    Dim globalextensions = New Collection
    Dim metadataDownloaded = False
    Dim lock = New Object
    Declare Function joyGetPosEx Lib "winmm.dll" (ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer
    Dim controller As SharpDX.XInput.Controller = New SharpDX.XInput.Controller(SharpDX.XInput.UserIndex.One)
    <StructLayout(LayoutKind.Sequential)>
    Public Structure JOYINFOEX
        Public dwSize As Integer
        Public dwFlags As Integer
        Public dwXpos As Integer
        Public dwYpos As Integer
        Public dwZpos As Integer
        Public dwRpos As Integer
        Public dwUpos As Integer
        Public dwVpos As Integer
        Public dwButtons As Integer
        Public dwButtonNumber As Integer
        Public dwPOV As Integer
        Public dwReserved1 As Integer
        Public dwReserved2 As Integer
    End Structure

    Dim myjoyEX As JOYINFOEX

    Private Sub DInputHandler()
        While True
            If Not joyConf And windowFocus Then
                Call joyGetPosEx(0, myjoyEX)
                With myjoyEX
                    Dim newString = .dwXpos.ToString + "DEL" + .dwYpos.ToString + "DEL" + .dwZpos.ToString + "DEL" + .dwRpos.ToString + "DEL" + .dwUpos.ToString + "DEL" + .dwVpos.ToString + "DEL" + .dwButtons.ToString("X") + "DEL" + .dwButtonNumber.ToString + "DEL" + (.dwPOV / 100).ToString + "DEL" + (.dwPOV / 100).ToString
                    Me.InvokeIfRequired(Sub()
                                            ProcessCmdKey(New Message(), InputTranslator.translateDinput(newString))
                                        End Sub)
                End With
            End If
            Thread.Sleep(100)
        End While
    End Sub

    Private Sub XInputHandler()
        While True And controller.IsConnected
            If Not joyConf And windowFocus Then
                Me.InvokeIfRequired(Sub()
                                        ProcessCmdKey(New Message(), InputTranslator.translateXinput(controller.GetState()))
                                    End Sub)
            End If
            Thread.Sleep(100)
        End While
    End Sub

    Private Function updateColors()
        systemBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        gameBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        description.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        Me.BackColor = ColorTranslator.FromHtml("#080708")
        quit.BackColor = ColorTranslator.FromHtml("#DF2935")
        genreBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        devBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        ProgressBar1.ForeColor = ColorTranslator.FromHtml("#0066CC")
        ProgressBar1.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        cover.BackColor = ColorTranslator.FromHtml("#080708")
        TextBox1.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        Return 0
    End Function

    Private Sub checkGetSettings()
        If My.Settings.es_settings_loc = "" Then
            openSystemsCfg.ShowDialog()
            folder = openSystemsCfg.FileName
            My.Settings.es_settings_loc = openSystemsCfg.FileName
        Else
            folder = My.Settings.es_settings_loc
        End If
        If Not System.IO.File.Exists(My.Settings.es_settings_loc) Then
            openSystemsCfg.ShowDialog()
            folder = openSystemsCfg.FileName
            My.Settings.es_settings_loc = openSystemsCfg.FileName
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateColors()
        checkGetSettings()
        consolelist = XML.readGamesystem(folder)
        For Each c In consolelist
            systemBox.Items.Add(c.getfullName())
        Next
        Dim cacheThreads = New Collection
        Dim i = 1
        If My.Settings.precache Then
            While i <= consolelist.Count
                Dim readThread = New Threading.Thread(AddressOf buildCacheBackground)
                readThread.Start((New Object() {folder, i, "", False}))
                i += 1
            End While
        End If
        systemBox.SelectedIndex = 0
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Dim rs As New Resizer
        rs.FindAllControls(Me)
        Me.Bounds = Screen.GetWorkingArea(Me)
        rs.ResizeAllControls(Me)
        storeGlobalExtensions()
        If My.Settings.useXInput Then
            joyThread = New Threading.Thread(AddressOf XInputHandler)
        Else
            myjoyEX.dwSize = 64
            myjoyEX.dwFlags = &HFF ' All information
            joyThread = New Threading.Thread(AddressOf DInputHandler)
        End If
        joyThread.Start()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gameBox.SelectedIndexChanged
        metadataDownloaded = False
        For Each g In gamelist
            If gameBox.SelectedItem = g.getName() Then
                selectedgame = g
                If gamelistavailable Then
                    If Not g.getDescription() Is Nothing Then
                        description.Text = g.getDescription()
                        description.ScrollBars = ScrollBars.Vertical
                    Else
                        description.Text = Nothing
                        description.ScrollBars = ScrollBars.None
                    End If
                    cover.Image = cover.ErrorImage
                    If Not g.getImage() Is Nothing Then
                        cover.ImageLocation = consolelist.Item(systemBox.SelectedIndex + 1).getPath() + g.getImage()
                    Else
                        cover.Image = cover.ErrorImage
                    End If
                End If
                Exit For
            End If
        Next
        updateColors()
    End Sub

    Private Sub reloadGames(ByVal romdir)
        If My.Settings.freeze Then
            updateGames({romdir, systemBox.SelectedIndex + 1, systemBox.SelectedItem, My.Settings.livecache})
        Else
            Try
                t.Abort()
            Catch
            End Try
            t = New Threading.Thread(AddressOf updateGames)
            t.Start(New Object() {romdir, systemBox.SelectedIndex + 1, systemBox.SelectedItem, My.Settings.livecache})
        End If

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles systemBox.SelectedIndexChanged
        metadataDownloaded = False
        TextBox1.Text = ""
        gameBox.DataSource = New StringCollection
        reloadGames(consolelist.Item(systemBox.SelectedIndex + 1).getPath())
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles gameBox.DoubleClick
        consolelist.Item(systemBox.SelectedIndex + 1).run(selectedgame.getpath())
        TextBox1.Text = ""
    End Sub

    Private Sub FilterChange(sender As Object, e As EventArgs) Handles genreBox.SelectedIndexChanged, devBox.SelectedIndexChanged
        metadataDownloaded = False
        Try
            TextBox1.Text = ""
            gameBox.DataSource = UIHelpers.filter(genreBox.SelectedItem.trim, devBox.SelectedItem.trim, gamelist)
        Catch
            Return
        End Try
    End Sub


    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles saveScript.FileOk
        Try
            IO.File.WriteAllText(saveScript.FileName, consolelist.Item(systemBox.SelectedIndex + 1).makeScript(selectedgame.getpath()))
        Catch
            Return
        End Try
    End Sub

    Private Sub quit_Click(sender As Object, e As EventArgs) Handles quit.Click
        My.Settings.Save()
        End
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        metadataDownloaded = False
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
                TextBox1.Text = ""
                Return True
            Case Keys.Escape
                End
                Return True
            Case Keys.Space
                If Me.ActiveControl.Name = "TextBox1" Then
                    TextBox1.AppendText(" ")
                    Return True
                End If
                TextBox1.Select()
                Return True
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub updateGamesCached(ByVal params)
        Dim romdir = params(0)
        Dim system = params(1)
        Dim headText = params(2)
        Dim reparseXML = Not params(3)
        gamelist = globalgamelist.item(consolelist.Item(system).getName())
        gameControlList = globalgamenames.item(consolelist.Item(system).getName())
        If IO.File.Exists(consolelist.Item(system).getGamelist) Then
            gamelistavailable = True
            developerlist = globaldevlist.item(consolelist.Item(system).getName())
            genrelist = globalgenrelist.item(consolelist.Item(system).getName())
            genreBox.InvokeIfRequired(Sub()
                                          genreBox.Enabled = True
                                          genreBox.DataSource = genrelist
                                          genreBox.SelectedIndex = 0
                                      End Sub)
            devBox.InvokeIfRequired(Sub()
                                        devBox.Enabled = True
                                        devBox.DataSource = developerlist
                                        devBox.SelectedIndex = 0
                                    End Sub)
            filteredgames = gamelist
        Else
            gamelistavailable = False
            cover.InvokeIfRequired(Sub()
                                       cover.ImageLocation = vbNull
                                   End Sub)
            description.InvokeIfRequired(Sub()
                                             description.Text = ""
                                         End Sub)
            genreBox.InvokeIfRequired(Sub()
                                          genreBox.Enabled = False
                                      End Sub)
            devBox.InvokeIfRequired(Sub()
                                        devBox.Enabled = False
                                    End Sub)
        End If
        gameBox.InvokeIfRequired(Sub()
                                     gameBox.DataSource = gameControlList
                                 End Sub)
        If gamelistavailable Then
            Try
                gameBox.InvokeIfRequired(Sub()
                                             gameBox.SelectedIndex = 0
                                         End Sub)
            Catch
                If globalgamelist.Contains(consolelist.Item(system).getName()) Then
                    globalgamelist.Remove(consolelist.Item(system).getName())
                    globalgamenames.Remove(consolelist.Item(system).getName())
                    If gamelistavailable Then
                        globalgenrelist.Remove(consolelist.Item(system).getName())
                        globaldevlist.Remove(consolelist.Item(system).getName())
                    End If
                End If
                updateGames(params)
            End Try
        End If
    End Sub

    Private Sub buildCacheBackground(ByVal params)
        Dim romdir = params(0)
        Dim system = params(1)
        Dim headText = params(2)
        Dim reparseXML = Not params(3)
        Dim threadGameControlList = New Collection
        Dim threadGenreList = New StringCollection()
        Dim threadDeveloperList = New StringCollection()
        threadGenreList.Add("All")
        threadDeveloperList.Add("All")
        If IO.File.Exists(consolelist.Item(system).getGamelist) Then
            Dim threadGamelist = XML.readGame(consolelist.Item(system).getGamelist, consolelist.Item(system))
            For Each g In threadGamelist
                If Not g.getGenre() Is Nothing Then
                    If Not threadGenreList.Contains(g.getGenre().trim) Then
                        threadGenreList.Add(g.getGenre().trim)
                    End If
                End If
                If Not g.getDeveloper() Is Nothing Then
                    If Not threadDeveloperList.Contains(g.getDeveloper().trim) Then
                        threadDeveloperList.Add(g.getDeveloper())
                    End If
                End If
                threadGameControlList.Add(g.getName())
            Next
            SyncLock lock
                globalgamelist.Add(threadGamelist, consolelist.Item(system).getName())
                globalgamenames.Add(threadGameControlList, consolelist.Item(system).getName())
                globalgenrelist.Add(threadGenreList, consolelist.Item(system).getName())
                globaldevlist.Add(threadDeveloperList, consolelist.Item(system).getName())
            End SyncLock
        Else
            If IO.Directory.Exists(consolelist.Item(system).getPath()) Then
                Dim threadGamelist = New Collection
                Dim files() As String = IO.Directory.GetFiles(consolelist.Item(system).getPath())
                Dim id = 1
                For Each file As String In files
                    threadGameControlList.Add(IO.Path.GetFileName(file))
                    threadGamelist.Add(New Game("./" + IO.Path.GetFileName(file), IO.Path.GetFileName(file), "", "", "", "", id))
                    id = id + 1
                Next
                SyncLock lock
                    globalgamelist.Add(threadGamelist, consolelist.Item(system).getName())
                    globalgamenames.Add(threadGameControlList, consolelist.Item(system).getName())
                End SyncLock
            End If
        End If
    End Sub
    Private Sub updateGamesUncached(ByVal params)
        Dim romdir = params(0)
        Dim system = params(1)
        Dim headText = params(2)
        Dim reparseXML = Not params(3)
        If IO.File.Exists(consolelist.Item(System).getGamelist) Then
            gamelist = XML.readGame(consolelist.Item(System).getGamelist, consolelist.Item(System))
            ProgressBar1.InvokeIfRequired(Sub()
                                              ProgressBar1.Maximum = gamelist.Count
                                          End Sub)
            For Each g In gamelist
                If Not g.getGenre() Is Nothing Then
                    If Not genrelist.Contains(g.getGenre().trim) Then
                        genrelist.Add(g.getGenre().trim)
                    End If
                End If
                If Not g.getDeveloper() Is Nothing Then
                    If Not developerlist.Contains(g.getDeveloper().trim) Then
                        developerlist.Add(g.getDeveloper())
                    End If
                End If
                gameControlList.Add(g.getName())
                ProgressBar1.InvokeIfRequired(Sub()
                                                  ProgressBar1.PerformStep()
                                              End Sub)
            Next
            gamelistavailable = True
            filteredgames = gamelist
        Else
            generatePlaceHolders(romdir)
        End If
        performCache(system)
    End Sub

    Private Sub generatePlaceHolders(ByVal romdir)
        gamelistavailable = False
        Try
            Dim files() As String = IO.Directory.GetFiles(romdir)
            Dim id = 1
            For Each file As String In files
                gameControlList.Add(IO.Path.GetFileName(file))
                gamelist.Add(New Game("./" + IO.Path.GetFileName(file), IO.Path.GetFileName(file), "", "", "", "", id))
                id = id + 1
            Next
        Catch
            Return
        End Try
    End Sub

    Private Sub performCache(ByVal system)
        If globalgamelist.Contains(consolelist.Item(system).getName()) Then
            globalgamelist.Remove(consolelist.Item(system).getName())
            globalgamenames.Remove(consolelist.Item(system).getName())
            If gamelistavailable Then
                globalgenrelist.Remove(consolelist.Item(system).getName())
                globaldevlist.Remove(consolelist.Item(system).getName())
            End If
        End If
        globalgamelist.Add(gamelist, consolelist.Item(system).getName())
        globalgamenames.Add(gameControlList, consolelist.Item(system).getName())
        If gamelistavailable Then
            globalgenrelist.Add(genrelist, consolelist.Item(system).getName())
            globaldevlist.Add(developerlist, consolelist.Item(system).getName())
        End If
    End Sub

    Private Function updateGames(ByVal params)
        Me.SuspendLayout()
        metadataDownloaded = False
        ProgressBar1.InvokeIfRequired(Sub()
                                          ProgressBar1.Value = 0
                                      End Sub)
        Dim romdir = params(0)
        Dim system = params(1)
        Dim headText = params(2)
        Dim reparseXML = Not params(3)
        gamelist = New Collection
        gameControlList = New Collection
        genrelist = New StringCollection()
        developerlist = New StringCollection()
        genrelist.Add("All")
        developerlist.Add("All")
        If Not globalgamelist.Contains(consolelist.Item(system).getName()) Or reparseXML Then
            updateGamesUncached(params)
        End If
        updateGamesCached(params)
        Me.ResumeLayout()
        Return 0
    End Function

    Public Shared Sub CenterForm(ByVal frm As Form, Optional ByVal parent As Form = Nothing)
        '' Note: call this from frm's Load event!
        Dim r As Rectangle
        If parent IsNot Nothing Then
            r = parent.RectangleToScreen(parent.ClientRectangle)
        Else
            r = Screen.FromPoint(frm.Location).WorkingArea
        End If

        Dim x = r.Left + (r.Width - frm.Width) \ 2
        Dim y = r.Top + (r.Height - frm.Height) \ 2
        frm.Location = New Point(x, y)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        metadataDownloaded = False
        If TextBox1.Text = "" Then
            gameBox.DataSource = gameControlList
        Else
            Dim finList As New Collection
            For Each g In gameControlList
                If g.ToLower().Contains(TextBox1.Text.ToLower()) Then
                    finList.Add(g)
                End If
            Next
            gameBox.DataSource = finList
        End If

    End Sub

    Private Sub Main_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        windowFocus = True
    End Sub

    Private Sub Main_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        windowFocus = False
    End Sub

    Private Sub storeGlobalExtensions()
        For Each c In consolelist
            For Each e In c.getExtensions()
                If Not globalextensions.contains(e.Trim()) And e.Trim().length > 0 Then
                    globalextensions.Add(e.Trim(), e.Trim())
                End If
            Next
        Next
    End Sub

    Private Sub buildWholeGameList(ByVal params)
        ProgressBar1.InvokeIfRequired(Sub()
                                          ProgressBar1.Maximum = gamelist.Count
                                          ProgressBar1.Value = 0
                                      End Sub)
        Dim localgamelist = params(0)
        Dim localsystem = params(1)
        For Each g In localgamelist
            Try
                Dim currentgame As Game
                If g.getDescription() = Nothing Then
                    If My.Settings.useTGDB Then
                        currentgame = updateMetaDataTheGamesDB(g, globalextensions)
                    Else
                        currentgame = updateMetaDataRAWG(g, globalextensions)
                    End If
                    If Not currentgame.getDescription() = Nothing Then
                        If Not IO.File.Exists(localsystem.getGamelist) Then
                            createGameList(localsystem, currentgame)
                        Else
                            updateGamelist(localsystem, currentgame)
                        End If
                    End If
                End If
                currentgame = Nothing
                ProgressBar1.InvokeIfRequired(Sub()
                                                  ProgressBar1.PerformStep()
                                              End Sub)
            Catch
                ProgressBar1.InvokeIfRequired(Sub()
                                                  ProgressBar1.PerformStep()
                                              End Sub)
            End Try
        Next
        If My.Settings.AutoReload Then
            gamelistavailable = True
            TextBox1.Text = ""
            gameBox.DataSource = New StringCollection
            reloadGames(consolelist.Item(systemBox.SelectedIndex + 1).getPath())
        End If
    End Sub

    Private Sub DownloadMetadataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DownloadMetadataToolStripMenuItem.Click
        Try
            If selectedgame.getDescription() = Nothing Then
                description.Text = "DOWNLOADING"
                If My.Settings.useTGDB Then
                    selectedgame = updateMetaDataTheGamesDB(selectedgame, globalextensions)
                Else
                    selectedgame = updateMetaDataRAWG(selectedgame, globalextensions)
                End If
                description.Text = selectedgame.getDescription()
                cover.ImageLocation = selectedgame.getImage()
                description.ScrollBars = ScrollBars.Vertical
                metadataDownloaded = True
            Else
                MsgBox("Game already has metadata!")
                metadataDownloaded = False
            End If
        Catch ex As Exception
            description.Text = Nothing
            cover.Image = cover.ErrorImage
            description.ScrollBars = ScrollBars.None
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveMetadataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMetadataToolStripMenuItem.Click
        If metadataDownloaded Then
            If gamelistavailable Then
                updateGamelist(consolelist.Item(systemBox.SelectedIndex + 1), selectedgame)
            Else
                createGameList(consolelist.Item(systemBox.SelectedIndex + 1), selectedgame)
            End If
            metadataDownloaded = False
            If My.Settings.AutoReload Then
                gamelistavailable = True
                TextBox1.Text = ""
                gameBox.DataSource = New StringCollection
                reloadGames(consolelist.Item(systemBox.SelectedIndex + 1).getPath())
            End If
        Else
            MsgBox("This metadata is already stored!")
        End If
    End Sub

    Private Sub UpdateWholeSystemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateWholeSystemToolStripMenuItem.Click
        If My.Settings.freeze Then
            buildWholeGameList({gamelist, consolelist.Item(systemBox.SelectedIndex + 1)})
        Else
            t = New Threading.Thread(AddressOf buildWholeGameList)
            t.Start((New Object() {gamelist, consolelist.Item(systemBox.SelectedIndex + 1)}))
        End If
    End Sub

    Private Sub ShowOptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowOptionsToolStripMenuItem.Click
        Joysticks.Show()
    End Sub

    Private Sub GenrateScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenrateScriptToolStripMenuItem.Click
        saveScript.FileName = selectedgame.getName + ".bat"
        saveScript.ShowDialog()
    End Sub
End Class
