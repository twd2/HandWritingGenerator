<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreprocess
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.labTheta = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DisplayOffsetY = New System.Windows.Forms.TrackBar()
        Me.DisplayOffsetX = New System.Windows.Forms.TrackBar()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.YOffset = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisplayOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisplayOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.YOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 338)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(982, 295)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(96, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "倾斜校正"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'labTheta
        '
        Me.labTheta.AutoSize = True
        Me.labTheta.Location = New System.Drawing.Point(12, 9)
        Me.labTheta.Name = "labTheta"
        Me.labTheta.Size = New System.Drawing.Size(106, 15)
        Me.labTheta.TabIndex = 2
        Me.labTheta.Text = "倾斜矫正: 0°"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(252, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 15)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "垂直移动"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(252, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 15)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "水平移动"
        '
        'DisplayOffsetY
        '
        Me.DisplayOffsetY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOffsetY.Location = New System.Drawing.Point(326, 66)
        Me.DisplayOffsetY.Margin = New System.Windows.Forms.Padding(4)
        Me.DisplayOffsetY.Name = "DisplayOffsetY"
        Me.DisplayOffsetY.Size = New System.Drawing.Size(668, 56)
        Me.DisplayOffsetY.TabIndex = 24
        '
        'DisplayOffsetX
        '
        Me.DisplayOffsetX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOffsetX.Location = New System.Drawing.Point(326, 13)
        Me.DisplayOffsetX.Margin = New System.Windows.Forms.Padding(4)
        Me.DisplayOffsetX.Name = "DisplayOffsetX"
        Me.DisplayOffsetX.Size = New System.Drawing.Size(668, 56)
        Me.DisplayOffsetX.TabIndex = 23
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(177, 28)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 27
        Me.Button2.Text = "获取矩形"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(15, 28)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 28
        Me.Button3.Text = "打开图片"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(12, 129)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(982, 203)
        Me.ListView1.TabIndex = 29
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "编号"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "纵坐标偏移量"
        Me.ColumnHeader2.Width = 108
        '
        'YOffset
        '
        Me.YOffset.Location = New System.Drawing.Point(116, 87)
        Me.YOffset.Margin = New System.Windows.Forms.Padding(4)
        Me.YOffset.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
        Me.YOffset.Minimum = New Decimal(New Integer() {-2147483648, 0, 0, -2147483648})
        Me.YOffset.Name = "YOffset"
        Me.YOffset.Size = New System.Drawing.Size(129, 25)
        Me.YOffset.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 15)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "纵坐标偏移量"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(15, 57)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(237, 23)
        Me.Button4.TabIndex = 32
        Me.Button4.Text = "保存"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmPreprocess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 645)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.YOffset)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DisplayOffsetY)
        Me.Controls.Add(Me.DisplayOffsetX)
        Me.Controls.Add(Me.labTheta)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "frmPreprocess"
        Me.Text = "图片预处理"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisplayOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisplayOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.YOffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents labTheta As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DisplayOffsetY As System.Windows.Forms.TrackBar
    Friend WithEvents DisplayOffsetX As System.Windows.Forms.TrackBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents YOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
End Class
