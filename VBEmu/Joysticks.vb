Imports System.Threading
Imports System.Runtime.InteropServices
Public Class Joysticks
    Dim joyThread As Thread
    Dim changeNextGame = False
    Dim changePreviousGame = False
    Dim changeNextGenre = False
    Dim changePreviousGenre = False
    Dim changeNextSystem = False
    Dim changePreviousSystem = False
    Dim changeStart = False

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
        Dim last = ""
        While True
            Call joyGetPosEx(0, myjoyEX)
            With myjoyEX
                Dim newString = .dwXpos.ToString + "DEL" + .dwYpos.ToString + "DEL" + .dwZpos.ToString + "DEL" + .dwRpos.ToString + "DEL" + .dwUpos.ToString + "DEL" + .dwVpos.ToString + "DEL" + .dwButtons.ToString("X") + "DEL" + .dwButtonNumber.ToString + "DEL" + (.dwPOV / 100).ToString + "DEL" + (.dwPOV / 100).ToString
                If last = "" Then
                    last = newString
                ElseIf newString <> last Then
                    If changeNextGame Then
                        changeNextGame = False
                        My.Settings.joystick_next_game = newString
                        NextGameText.InvokeIfRequired(Sub()
                                                          NextGameText.Text = newString
                                                      End Sub)
                        NextGameButton.InvokeIfRequired(Sub()
                                                            NextGameButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                        End Sub)

                    End If
                    If changePreviousGame Then
                        changePreviousGame = False
                        My.Settings.joystick_previous_game = newString
                        PreviousGameText.InvokeIfRequired(Sub()
                                                              PreviousGameText.Text = newString
                                                          End Sub)
                        PreviousGameButton.InvokeIfRequired(Sub()
                                                                PreviousGameButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                            End Sub)
                    End If
                    If changeNextGenre Then
                        changeNextGenre = False
                        My.Settings.joystick_next_genre = newString
                        NextGenreText.InvokeIfRequired(Sub()
                                                           NextGenreText.Text = newString
                                                       End Sub)
                        NextGenreButton.InvokeIfRequired(Sub()
                                                             NextGenreButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                         End Sub)
                    End If
                    If changePreviousGenre Then
                        changePreviousGenre = False
                        My.Settings.joystick_previous_genre = newString
                        PreviousGenreText.InvokeIfRequired(Sub()
                                                               PreviousGenreText.Text = newString
                                                           End Sub)
                        PreviousGenreButton.InvokeIfRequired(Sub()
                                                                 PreviousGenreButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                             End Sub)
                    End If
                    If changeNextSystem Then
                        changeNextSystem = False
                        My.Settings.joystick_next_system = newString
                        NextSystemText.InvokeIfRequired(Sub()
                                                            NextSystemText.Text = newString
                                                        End Sub)
                        NextSystemButton.InvokeIfRequired(Sub()
                                                              NextSystemButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                          End Sub)
                    End If
                    If changePreviousSystem Then
                        changePreviousSystem = False
                        My.Settings.joystick_previous_system = newString
                        PreviousSystemText.InvokeIfRequired(Sub()
                                                                PreviousSystemText.Text = newString
                                                            End Sub)
                        PreviousSystemButton.InvokeIfRequired(Sub()
                                                                  PreviousSystemButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                              End Sub)
                    End If
                    If changeStart Then
                        changeStart = False
                        My.Settings.joystick_start = newString
                        StartText.InvokeIfRequired(Sub()
                                                       StartText.Text = newString
                                                   End Sub)
                        StartButton.InvokeIfRequired(Sub()
                                                         StartButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
                                                     End Sub)
                    End If
                End If
            End With
            Thread.Sleep(1000)
        End While
    End Sub


    Private Function updateColors()
        NextGameText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        NextGenreText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        NextSystemText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        PreviousGameText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        PreviousGenreText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        PreviousSystemText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        StartText.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        TokenBox.BackColor = ColorTranslator.FromHtml("#E6E8E6")
        Me.BackColor = ColorTranslator.FromHtml("#080708")
        NextGameButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        NextGenreButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        NextSystemButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        PreviousGameButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        PreviousGenreButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        PreviousSystemButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        StartButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        OKButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        TokenButton.BackColor = ColorTranslator.FromHtml("#FDCA40")
        Return 0
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Main.joyConf = True
        myjoyEX.dwSize = 64
        myjoyEX.dwFlags = &HFF
        joyThread = New Threading.Thread(AddressOf JoyPoll)
        joyThread.Start()
        StartText.Text = My.Settings.joystick_start
        PreviousSystemText.Text = My.Settings.joystick_previous_system
        NextSystemText.Text = My.Settings.joystick_next_system
        NextGenreText.Text = My.Settings.joystick_next_genre
        PreviousGenreText.Text = My.Settings.joystick_previous_genre
        PreviousGameText.Text = My.Settings.joystick_previous_game
        NextGameText.Text = My.Settings.joystick_next_game
        CheckBox1.Checked = My.Settings.livecache
        CheckBox2.Checked = My.Settings.precache
        TokenBox.Text = My.Settings.RAWGToken
        updateColors()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles NextGameButton.Click
        changeNextGame = True
        NextGameButton.BackColor = ColorTranslator.FromHtml("#DF2935")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles PreviousGameButton.Click
        changePreviousGame = True
        PreviousGameButton.BackColor = ColorTranslator.FromHtml("#DF2935")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles NextGenreButton.Click
        changeNextGenre = True
        NextGenreButton.BackColor = ColorTranslator.FromHtml("#DF2935")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles PreviousGenreButton.Click
        changePreviousGenre = True
        PreviousGenreButton.BackColor = ColorTranslator.FromHtml("#DF2935")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles NextSystemButton.Click
        changeNextSystem = True
        NextSystemButton.BackColor = ColorTranslator.FromHtml("#DF2935")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles PreviousSystemButton.Click
        changePreviousSystem = True
        PreviousSystemButton.BackColor = ColorTranslator.FromHtml("#DF2935")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        changeStart = True
        StartButton.BackColor = ColorTranslator.FromHtml("#DF2935")
    End Sub

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        joyThread.Abort()
        Main.joyConf = False
        Me.Hide()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        My.Settings.livecache = CheckBox1.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        My.Settings.precache = CheckBox2.Checked
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        My.Settings.freeze = CheckBox3.Checked
    End Sub

    Private Sub TokenButton_Click(sender As Object, e As EventArgs) Handles TokenButton.Click
        My.Settings.RAWGToken = TokenBox.Text
    End Sub
End Class