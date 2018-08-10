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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.genre = New System.Windows.Forms.Label()
        Me.developer = New System.Windows.Forms.Label()
        Me.cover = New System.Windows.Forms.PictureBox()
        Me.description = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        CType(Me.cover, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(2, 133)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(249, 394)
        Me.ListBox1.TabIndex = 0
        '
        'genre
        '
        Me.genre.AutoSize = True
        Me.genre.Location = New System.Drawing.Point(269, 16)
        Me.genre.Name = "genre"
        Me.genre.Size = New System.Drawing.Size(34, 13)
        Me.genre.TabIndex = 1
        Me.genre.Text = "genre"
        '
        'developer
        '
        Me.developer.AutoSize = True
        Me.developer.Location = New System.Drawing.Point(269, 3)
        Me.developer.Name = "developer"
        Me.developer.Size = New System.Drawing.Size(54, 13)
        Me.developer.TabIndex = 3
        Me.developer.Text = "developer"
        '
        'cover
        '
        Me.cover.Location = New System.Drawing.Point(302, 48)
        Me.cover.Name = "cover"
        Me.cover.Size = New System.Drawing.Size(189, 192)
        Me.cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.cover.TabIndex = 4
        Me.cover.TabStop = False
        '
        'description
        '
        Me.description.Location = New System.Drawing.Point(272, 267)
        Me.description.Multiline = True
        Me.description.Name = "description"
        Me.description.ReadOnly = True
        Me.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.description.Size = New System.Drawing.Size(247, 255)
        Me.description.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(471, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(48, 34)
        Me.Button1.TabIndex = 6
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(2, 8)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(248, 108)
        Me.ListBox2.TabIndex = 7
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 534)
        Me.ControlBox = False
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.description)
        Me.Controls.Add(Me.cover)
        Me.Controls.Add(Me.developer)
        Me.Controls.Add(Me.genre)
        Me.Controls.Add(Me.ListBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "Main"
        Me.Text = "Atari"
        CType(Me.cover, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents genre As System.Windows.Forms.Label
    Friend WithEvents developer As System.Windows.Forms.Label
    Friend WithEvents cover As System.Windows.Forms.PictureBox
    Friend WithEvents description As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox

End Class
