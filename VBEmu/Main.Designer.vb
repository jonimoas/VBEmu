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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.gameBox = New System.Windows.Forms.ListBox()
        Me.description = New System.Windows.Forms.TextBox()
        Me.systemBox = New System.Windows.Forms.ListBox()
        Me.genreBox = New System.Windows.Forms.ComboBox()
        Me.devBox = New System.Windows.Forms.ComboBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.openSystemsCfg = New System.Windows.Forms.OpenFileDialog()
        Me.save = New System.Windows.Forms.Button()
        Me.quit = New System.Windows.Forms.Button()
        Me.saveScript = New System.Windows.Forms.SaveFileDialog()
        Me.cover = New System.Windows.Forms.PictureBox()
        Me.openGamelist = New System.Windows.Forms.OpenFileDialog()
        CType(Me.cover, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gameBox
        '
        Me.gameBox.BackColor = System.Drawing.Color.Beige
        Me.gameBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.gameBox.FormattingEnabled = True
        Me.gameBox.ItemHeight = 20
        Me.gameBox.Location = New System.Drawing.Point(1, 138)
        Me.gameBox.Name = "gameBox"
        Me.gameBox.Size = New System.Drawing.Size(346, 364)
        Me.gameBox.Sorted = True
        Me.gameBox.TabIndex = 0
        '
        'description
        '
        Me.description.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.description.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.description.Location = New System.Drawing.Point(389, 296)
        Me.description.Multiline = True
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.description.Size = New System.Drawing.Size(233, 206)
        Me.description.TabIndex = 5
        '
        'systemBox
        '
        Me.systemBox.BackColor = System.Drawing.Color.White
        Me.systemBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.systemBox.FormattingEnabled = True
        Me.systemBox.ItemHeight = 20
        Me.systemBox.Location = New System.Drawing.Point(1, 2)
        Me.systemBox.Name = "systemBox"
        Me.systemBox.Size = New System.Drawing.Size(346, 124)
        Me.systemBox.TabIndex = 7
        '
        'genreBox
        '
        Me.genreBox.BackColor = System.Drawing.Color.Beige
        Me.genreBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.genreBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.genreBox.FormattingEnabled = True
        Me.genreBox.Location = New System.Drawing.Point(389, 2)
        Me.genreBox.Name = "genreBox"
        Me.genreBox.Size = New System.Drawing.Size(233, 28)
        Me.genreBox.TabIndex = 8
        '
        'devBox
        '
        Me.devBox.BackColor = System.Drawing.Color.Beige
        Me.devBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.devBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.devBox.FormattingEnabled = True
        Me.devBox.Location = New System.Drawing.Point(389, 36)
        Me.devBox.Name = "devBox"
        Me.devBox.Size = New System.Drawing.Size(233, 28)
        Me.devBox.TabIndex = 9
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.Beige
        Me.ProgressBar1.ForeColor = System.Drawing.Color.Black
        Me.ProgressBar1.Location = New System.Drawing.Point(1, 508)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ProgressBar1.Size = New System.Drawing.Size(346, 40)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 10
        '
        'openSystemsCfg
        '
        Me.openSystemsCfg.FileName = "es_systems.cfg"
        '
        'save
        '
        Me.save.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.save.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.save.Location = New System.Drawing.Point(389, 508)
        Me.save.Name = "save"
        Me.save.Size = New System.Drawing.Size(110, 40)
        Me.save.TabIndex = 11
        Me.save.Text = "Save"
        Me.save.UseVisualStyleBackColor = True
        '
        'quit
        '
        Me.quit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.quit.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.quit.Location = New System.Drawing.Point(512, 508)
        Me.quit.Name = "quit"
        Me.quit.Size = New System.Drawing.Size(110, 40)
        Me.quit.TabIndex = 12
        Me.quit.Text = "Exit"
        Me.quit.UseVisualStyleBackColor = True
        '
        'saveScript
        '
        '
        'cover
        '
        Me.cover.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.cover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cover.ErrorImage = CType(resources.GetObject("cover.ErrorImage"), System.Drawing.Image)
        Me.cover.Image = CType(resources.GetObject("cover.Image"), System.Drawing.Image)
        Me.cover.InitialImage = CType(resources.GetObject("cover.InitialImage"), System.Drawing.Image)
        Me.cover.Location = New System.Drawing.Point(389, 70)
        Me.cover.Name = "cover"
        Me.cover.Size = New System.Drawing.Size(233, 220)
        Me.cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.cover.TabIndex = 4
        Me.cover.TabStop = False
        '
        'openGamelist
        '
        Me.openGamelist.FileName = "gamelist.xml"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.ClientSize = New System.Drawing.Size(634, 550)
        Me.ControlBox = False
        Me.Controls.Add(Me.quit)
        Me.Controls.Add(Me.save)
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
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
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
    Friend WithEvents openSystemsCfg As System.Windows.Forms.OpenFileDialog
    Friend WithEvents save As System.Windows.Forms.Button
    Friend WithEvents quit As System.Windows.Forms.Button
    Friend WithEvents saveScript As System.Windows.Forms.SaveFileDialog
    Friend WithEvents openGamelist As System.Windows.Forms.OpenFileDialog

End Class
