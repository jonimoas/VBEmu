Imports SharpDX.XInput

Module InputTranslator
    Public Function translateXinput(ByVal controllerState)
        Select Case controllerState.Gamepad.Buttons
            Case SharpDX.XInput.GamepadButtonFlags.A
                Return Keys.Enter
            Case SharpDX.XInput.GamepadButtonFlags.LeftShoulder
                Return Keys.Left
            Case SharpDX.XInput.GamepadButtonFlags.RightShoulder
                Return Keys.Right
            Case SharpDX.XInput.GamepadButtonFlags.X
                Return Keys.OemPeriod
            Case SharpDX.XInput.GamepadButtonFlags.Y
                Return Keys.Oemcomma
            Case SharpDX.XInput.GamepadButtonFlags.DPadUp
                Return Keys.Up
            Case SharpDX.XInput.GamepadButtonFlags.DPadDown
                Return Keys.Down
        End Select
        Return Nothing
    End Function

    Public Function translateDinput(ByVal newstring)
        Select Case newstring
            Case My.Settings.joystick_start
                Return Keys.Enter
            Case My.Settings.joystick_previous_system
                Return Keys.Left
            Case My.Settings.joystick_next_system
                Return Keys.Right
            Case My.Settings.joystick_next_genre
                Return Keys.Left
            Case My.Settings.joystick_previous_genre
                Return Keys.Oemcomma
            Case My.Settings.joystick_previous_game
                Return Keys.Up
            Case My.Settings.joystick_next_game
                Return Keys.Down
        End Select
        Return Nothing
    End Function
End Module
