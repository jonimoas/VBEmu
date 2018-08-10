<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.gameBox = New System.Windows.Forms.ListBox()
        Me.description = New System.Windows.Forms.TextBox()
        Me.systemBox = New System.Windows.Forms.ListBox()
        Me.genreBox = New System.Windows.Forms.ComboBox()
        Me.devBox = New System.Windows.Forms.ComboBox()
        Me.cover = New System.Windows.Forms.PictureBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.cover, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gameBox
        '
        Me.gameBox.FormattingEnabled = True
        Me.gameBox.Location = New System.Drawing.Point(2, 144)
        Me.gameBox.Name = "gameBox"
        Me.gameBox.Size = New System.Drawing.Size(346, 381)
        Me.gameBox.Sorted = True
        Me.gameBox.TabIndex = 0
        '
        'description
        '
        Me.description.Location = New System.Drawing.Point(354, 270)
        Me.description.Multiline = True
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.description.Size = New System.Drawing.Size(233, 255)
        Me.description.TabIndex = 5
        '
        'systemBox
        '
        Me.systemBox.FormattingEnabled = True
        Me.systemBox.Location = New System.Drawing.Point(2, 8)
        Me.systemBox.Name = "systemBox"
        Me.systemBox.Size = New System.Drawing.Size(346, 134)
        Me.systemBox.TabIndex = 7
        '
        'genreBox
        '
        Me.genreBox.FormattingEnabled = True
        Me.genreBox.Location = New System.Drawing.Point(354, 1)
        Me.genreBox.Name = "genreBox"
        Me.genreBox.Size = New System.Drawing.Size(187, 21)
        Me.genreBox.TabIndex = 8
        '
        'devBox
        '
        Me.devBox.FormattingEnabled = True
        Me.devBox.Location = New System.Drawing.Point(354, 31)
        Me.devBox.Name = "devBox"
        Me.devBox.Size = New System.Drawing.Size(187, 21)
        Me.devBox.TabIndex = 9
        '
        'cover
        '
        Me.cover.ErrorImage = Global.VBEmu.My.Resources.Resources.gamepadpic
        Me.cover.Location = New System.Drawing.Point(354, 58)
        Me.cover.Name = "cover"
        Me.cover.Size = New System.Drawing.Size(220, 206)
        Me.cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.cover.TabIndex = 4
        Me.cover.TabStop = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(2, 531)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(599, 23)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 10
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 566)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.devBox)
        Me.Controls.Add(Me.genreBox)
        Me.Controls.Add(Me.systemBox)
        Me.Controls.Add(Me.description)
        Me.Controls.Add(Me.cover)
        Me.Controls.Add(Me.gameBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.Text = "VBEmu"
        CType(Me.cover, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gameBox As System.Windows.Forms.ListBox
    Friend WithEvents cover As System.Windows.Forms.PictureBox
    Friend WithEvents description As System.Windows.Forms.TextBox
    Friend WithEvents systemBox As System.Windows.Forms.ListBox
    Friend WithEvents genreBox As System.Windows.Forms.ComboBox
    Friend WithEvents devBox As System.Windows.Forms.ComboBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

End Class
