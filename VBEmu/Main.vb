Imports System.Collections.Specialized
Imports System.Threading
Imports System.Runtime.InteropServices


Public Class Main
    Public joyConf = False
    Dim gamelist
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
    Dim t As Thread
    Dim joyThread As Thread
    Declare Function joyGetPosEx Lib "winmm.dll" (ByVal uJoyID As Integer, ByRef pji As JOYINFOEX) As Integer

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

    Private Sub JoyPoll()
        While True
            If Not joyConf Then


                Call joyGetPosEx(0, myjoyEX)
                With myjoyEX
                    Dim newString = .dwXpos.ToString + "DEL" + .dwYpos.ToString + "DEL" + .dwZpos.ToString + "DEL" + .dwRpos.ToString + "DEL" + .dwUpos.ToString + "DEL" + .dwVpos.ToString + "DEL" + .dwButtons.ToString("X") + "DEL" + .dwButtonNumber.ToString + "DEL" + (.dwPOV / 100).ToString + "DEL" + (.dwPOV / 100).ToString
                    Select Case newString
                        Case My.Settings.joystick_start
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.Enter)
                                                End Sub)
                        Case My.Settings.joystick_previous_system
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.Left)
                                                End Sub)
                        Case My.Settings.joystick_next_system
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.Right)
                                                End Sub)
                        Case My.Settings.joystick_next_genre
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.OemPeriod)
                                                End Sub)
                        Case My.Settings.joystick_previous_genre
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.Oemcomma)
                                                End Sub)
                        Case My.Settings.joystick_previous_game
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.Up)
                                                End Sub)
                        Case My.Settings.joystick_next_game
                            Me.InvokeIfRequired(Sub()
                                                    ProcessCmdKey(New Message(), Keys.Down)
                                                End Sub)
                    End Select
                End With
            End If
            Thread.Sleep(100)
        End While
    End Sub

    Private Function updateColors()
        systemBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        gameBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        description.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        Me.BackColor = ColorTranslator.FromHtml("#080708")
        save.BackColor = ColorTranslator.FromHtml("#FDCA40")
        quit.BackColor = ColorTranslator.FromHtml("#DF2935")
        save.ForeColor = ColorTranslator.FromHtml("#080708")
        genreBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        devBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        ProgressBar1.ForeColor = ColorTranslator.FromHtml("#0066CC")
        ProgressBar1.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        cover.BackColor = ColorTranslator.FromHtml("#080708")
        TextBox1.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        Return 0
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateColors()
        myjoyEX.dwSize = 64
        myjoyEX.dwFlags = &HFF ' All information

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
        consolelist = XML.readGamesystem(folder)
        For Each c In consolelist
            systemBox.Items.Add(c.getfullName())
        Next
        systemBox.SelectedIndex = 0
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Dim rs As New Resizer
        rs.FindAllControls(Me)
        Me.Bounds = Screen.GetWorkingArea(Me)
        rs.ResizeAllControls(Me)
        joyThread = New Threading.Thread(AddressOf JoyPoll)
        joyThread.Start()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gameBox.SelectedIndexChanged
        If gamelistavailable Then
            For Each g In gamelist
                If gameBox.SelectedItem = g.getName() Then
                    selectedgame = g
                    cover.ImageLocation = consolelist.Item(systemBox.SelectedIndex + 1).getPath() + g.getImage()
                    description.Text = g.getDescription()
                End If
            Next
        Else
            For Each g In gamelist
                If gameBox.SelectedItem = g.getName() Then
                    selectedgame = g
                End If
            Next
            description.Text = vbNullString

            cover.Image = cover.ErrorImage
        End If
        If description.Text = "" Then
            description.ScrollBars = ScrollBars.None
        Else
            description.ScrollBars = ScrollBars.Vertical
        End If
        updateColors()
    End Sub

    Private Sub reloadGames(ByVal romdir)
        Try
            t.Abort()
            t = New Threading.Thread(AddressOf updateGames)
        Catch
            t = New Threading.Thread(AddressOf updateGames)
        End Try
        t.Start(New Object() {romdir, systemBox.SelectedIndex + 1, systemBox.SelectedItem})
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles systemBox.SelectedIndexChanged
        TextBox1.Text = ""
        reloadGames(consolelist.Item(systemBox.SelectedIndex + 1).getPath())
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles gameBox.DoubleClick
        consolelist.Item(systemBox.SelectedIndex + 1).run(selectedgame.getpath())
        TextBox1.Text = ""
    End Sub

    Private Sub filter(ByVal genref, ByVal developerf)
        If gamelistavailable Then
            gameControlList.Clear()
            filteredgames = New Collection
            gameControlList = New Collection
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
                gameControlList.Add(g.getName())
            Next
            gameBox.DataSource = gameControlList
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles genreBox.SelectedIndexChanged
        filteredgames = New Collection
        Try
            TextBox1.Text = ""
            filter(genreBox.SelectedItem.trim, devBox.SelectedItem.trim)
        Catch
            Return
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles devBox.SelectedIndexChanged
        filteredgames = New Collection
        Try
            TextBox1.Text = ""
            filter(genreBox.SelectedItem.trim, devBox.SelectedItem.trim)
        Catch
            Return
        End Try

    End Sub

    Private Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        saveScript.FileName = selectedgame.getName + ".bat"
        saveScript.ShowDialog()
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

    Private Function updateGames(ByVal params)
        Dim romdir = params(0)
        Dim system = params(1)
        Dim headText = params(2)
        gamelist = New Collection
        gameControlList = New Collection
        genrelist = New StringCollection()
        developerlist = New StringCollection()
        genrelist.Add("All")
        developerlist.Add("All")
        If IO.File.Exists(consolelist.Item(system).getGamelist) Then
            gamelist = XML.readGame(consolelist.Item(system).getGamelist, consolelist.Item(system))
            ProgressBar1.InvokeIfRequired(Sub()
                                              ProgressBar1.Maximum = gamelist.Count
                                          End Sub)
            For Each g In gamelist
                If g.getGenre().trim <> "" And Not genrelist.Contains(g.getGenre().trim) Then
                    genrelist.Add(g.getGenre().trim)
                End If
                If g.getDeveloper().trim <> "" And Not developerlist.Contains(g.getDeveloper().trim) Then
                    developerlist.Add(g.getDeveloper())
                End If
                gameControlList.Add(g.getName())
                ProgressBar1.InvokeIfRequired(Sub()
                                                  ProgressBar1.PerformStep()
                                              End Sub)
            Next
            genreBox.InvokeIfRequired(Sub()
                                          genreBox.Enabled = True
                                          genreBox.DataSource = genrelist
                                      End Sub)
            devBox.InvokeIfRequired(Sub()
                                        devBox.Enabled = True
                                        devBox.DataSource = developerlist
                                    End Sub)
            gamelistavailable = True
            filteredgames = gamelist
            gameBox.InvokeIfRequired(Sub()
                                         gameBox.DataSource = gameControlList
                                     End Sub)
            gameBox.InvokeIfRequired(Sub()
                                         gameBox.SelectedIndex = 0
                                     End Sub)
        Else
            genreBox.InvokeIfRequired(Sub()
                                          genreBox.Enabled = False
                                      End Sub)
            devBox.InvokeIfRequired(Sub()
                                        devBox.Enabled = False
                                    End Sub)
            Try
                Dim files() As String = IO.Directory.GetFiles(romdir)


                Dim id = 1
                For Each file As String In files
                    gameControlList.Add(IO.Path.GetFileName(file))
                    gamelist.Add(New Game("./" + IO.Path.GetFileName(file), IO.Path.GetFileName(file), "", "", "", "", id))
                    id = id + 1
                Next
                gamelistavailable = False
                cover.InvokeIfRequired(Sub()
                                           cover.ImageLocation = vbNull
                                       End Sub)
                description.InvokeIfRequired(Sub()
                                                 description.Text = ""
                                             End Sub)
                gameBox.InvokeIfRequired(Sub()
                                             gameBox.DataSource = gameControlList
                                         End Sub)
            Catch
            End Try
        End If
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

    Private Sub Main_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Joysticks.Show()
    End Sub
End Class
