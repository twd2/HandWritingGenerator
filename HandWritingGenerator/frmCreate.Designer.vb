<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreate
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

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnOpenImage = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.DisplayOffsetX = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.LOffset = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TOffset = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ROffset = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.BOffset = New System.Windows.Forms.NumericUpDown()
        Me.btnAddChar = New System.Windows.Forms.Button()
        Me.BOffsetLock = New System.Windows.Forms.CheckBox()
        Me.TOffsetLock = New System.Windows.Forms.CheckBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSaveTemplate = New System.Windows.Forms.Button()
        Me.btnNewTemplate = New System.Windows.Forms.Button()
        Me.btnOpenTemplate = New System.Windows.Forms.Button()
        Me.DisplayOffsetY = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SaveImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisplayOffsetX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ROffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BOffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisplayOffsetY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpenImage
        '
        Me.btnOpenImage.Location = New System.Drawing.Point(16, 132)
        Me.btnOpenImage.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenImage.Name = "btnOpenImage"
        Me.btnOpenImage.Size = New System.Drawing.Size(147, 29)
        Me.btnOpenImage.TabIndex = 0
        Me.btnOpenImage.Text = "打开图片"
        Me.btnOpenImage.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox1.Location = New System.Drawing.Point(16, 640)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(635, 82)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'DisplayOffsetX
        '
        Me.DisplayOffsetX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOffsetX.Location = New System.Drawing.Point(198, 15)
        Me.DisplayOffsetX.Margin = New System.Windows.Forms.Padding(4)
        Me.DisplayOffsetX.Name = "DisplayOffsetX"
        Me.DisplayOffsetX.Size = New System.Drawing.Size(668, 56)
        Me.DisplayOffsetX.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(171, 140)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "字符"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(218, 137)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.MaxLength = 1
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(39, 25)
        Me.TextBox1.TabIndex = 4
        '
        'LOffset
        '
        Me.LOffset.Location = New System.Drawing.Point(360, 137)
        Me.LOffset.Margin = New System.Windows.Forms.Padding(4)
        Me.LOffset.Name = "LOffset"
        Me.LOffset.Size = New System.Drawing.Size(75, 25)
        Me.LOffset.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(266, 140)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 15)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "左边偏移量"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(620, 140)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "上边偏移量"
        '
        'TOffset
        '
        Me.TOffset.Location = New System.Drawing.Point(715, 138)
        Me.TOffset.Margin = New System.Windows.Forms.Padding(4)
        Me.TOffset.Name = "TOffset"
        Me.TOffset.Size = New System.Drawing.Size(75, 25)
        Me.TOffset.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Chocolate
        Me.Label4.Location = New System.Drawing.Point(266, 173)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 15)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "右边偏移量"
        '
        'ROffset
        '
        Me.ROffset.Location = New System.Drawing.Point(360, 170)
        Me.ROffset.Margin = New System.Windows.Forms.Padding(4)
        Me.ROffset.Name = "ROffset"
        Me.ROffset.Size = New System.Drawing.Size(75, 25)
        Me.ROffset.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(443, 140)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 15)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "下边偏移量"
        '
        'BOffset
        '
        Me.BOffset.Location = New System.Drawing.Point(538, 137)
        Me.BOffset.Margin = New System.Windows.Forms.Padding(4)
        Me.BOffset.Name = "BOffset"
        Me.BOffset.Size = New System.Drawing.Size(75, 25)
        Me.BOffset.TabIndex = 11
        '
        'btnAddChar
        '
        Me.btnAddChar.Location = New System.Drawing.Point(171, 169)
        Me.btnAddChar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAddChar.Name = "btnAddChar"
        Me.btnAddChar.Size = New System.Drawing.Size(86, 28)
        Me.btnAddChar.TabIndex = 13
        Me.btnAddChar.Text = "添加"
        Me.btnAddChar.UseVisualStyleBackColor = True
        '
        'BOffsetLock
        '
        Me.BOffsetLock.AutoSize = True
        Me.BOffsetLock.Location = New System.Drawing.Point(446, 173)
        Me.BOffsetLock.Margin = New System.Windows.Forms.Padding(4)
        Me.BOffsetLock.Name = "BOffsetLock"
        Me.BOffsetLock.Size = New System.Drawing.Size(134, 19)
        Me.BOffsetLock.TabIndex = 14
        Me.BOffsetLock.Text = "锁定下边偏移量"
        Me.BOffsetLock.UseVisualStyleBackColor = True
        '
        'TOffsetLock
        '
        Me.TOffsetLock.AutoSize = True
        Me.TOffsetLock.Location = New System.Drawing.Point(623, 172)
        Me.TOffsetLock.Margin = New System.Windows.Forms.Padding(4)
        Me.TOffsetLock.Name = "TOffsetLock"
        Me.TOffsetLock.Size = New System.Drawing.Size(134, 19)
        Me.TOffsetLock.TabIndex = 15
        Me.TOffsetLock.Text = "锁定上边偏移量"
        Me.TOffsetLock.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(16, 205)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(850, 426)
        Me.ListView1.TabIndex = 16
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "字符"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "x1"
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "y1"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "x2"
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "y2"
        '
        'btnSaveTemplate
        '
        Me.btnSaveTemplate.Location = New System.Drawing.Point(16, 169)
        Me.btnSaveTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveTemplate.Name = "btnSaveTemplate"
        Me.btnSaveTemplate.Size = New System.Drawing.Size(147, 28)
        Me.btnSaveTemplate.TabIndex = 17
        Me.btnSaveTemplate.Text = "保存模板"
        Me.btnSaveTemplate.UseVisualStyleBackColor = True
        '
        'btnNewTemplate
        '
        Me.btnNewTemplate.Location = New System.Drawing.Point(16, 12)
        Me.btnNewTemplate.Name = "btnNewTemplate"
        Me.btnNewTemplate.Size = New System.Drawing.Size(102, 59)
        Me.btnNewTemplate.TabIndex = 18
        Me.btnNewTemplate.Text = "新建模板"
        Me.btnNewTemplate.UseVisualStyleBackColor = True
        '
        'btnOpenTemplate
        '
        Me.btnOpenTemplate.Location = New System.Drawing.Point(16, 77)
        Me.btnOpenTemplate.Name = "btnOpenTemplate"
        Me.btnOpenTemplate.Size = New System.Drawing.Size(102, 47)
        Me.btnOpenTemplate.TabIndex = 19
        Me.btnOpenTemplate.Text = "打开模板"
        Me.btnOpenTemplate.UseVisualStyleBackColor = True
        '
        'DisplayOffsetY
        '
        Me.DisplayOffsetY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayOffsetY.Location = New System.Drawing.Point(198, 68)
        Me.DisplayOffsetY.Margin = New System.Windows.Forms.Padding(4)
        Me.DisplayOffsetY.Name = "DisplayOffsetY"
        Me.DisplayOffsetY.Size = New System.Drawing.Size(668, 56)
        Me.DisplayOffsetY.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(124, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 15)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "水平移动"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(124, 89)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 15)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "垂直移动"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveImageToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(158, 28)
        '
        'SaveImageToolStripMenuItem
        '
        Me.SaveImageToolStripMenuItem.Name = "SaveImageToolStripMenuItem"
        Me.SaveImageToolStripMenuItem.Size = New System.Drawing.Size(157, 24)
        Me.SaveImageToolStripMenuItem.Text = "保存图片(&S)"
        '
        'frmCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 809)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DisplayOffsetY)
        Me.Controls.Add(Me.btnOpenTemplate)
        Me.Controls.Add(Me.btnNewTemplate)
        Me.Controls.Add(Me.btnSaveTemplate)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.TOffsetLock)
        Me.Controls.Add(Me.BOffsetLock)
        Me.Controls.Add(Me.btnAddChar)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.BOffset)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ROffset)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TOffset)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LOffset)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DisplayOffsetX)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnOpenImage)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCreate"
        Me.Text = "模版编辑器"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisplayOffsetX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LOffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TOffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ROffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BOffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisplayOffsetY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOpenImage As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents DisplayOffsetX As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents LOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ROffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BOffset As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnAddChar As System.Windows.Forms.Button
    Friend WithEvents BOffsetLock As System.Windows.Forms.CheckBox
    Friend WithEvents TOffsetLock As System.Windows.Forms.CheckBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnSaveTemplate As System.Windows.Forms.Button
    Friend WithEvents btnNewTemplate As System.Windows.Forms.Button
    Friend WithEvents btnOpenTemplate As System.Windows.Forms.Button
    Friend WithEvents DisplayOffsetY As System.Windows.Forms.TrackBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SaveImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
