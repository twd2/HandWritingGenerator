<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.保存图片SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.加载模板ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.从文本文件TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.从文本文件黑白BToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.测试TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelloWorldHToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.排版测试TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.其它OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(412, 485)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.保存图片SToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(158, 28)
        '
        '保存图片SToolStripMenuItem
        '
        Me.保存图片SToolStripMenuItem.Name = "保存图片SToolStripMenuItem"
        Me.保存图片SToolStripMenuItem.Size = New System.Drawing.Size(157, 24)
        Me.保存图片SToolStripMenuItem.Text = "保存图片(&S)"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 51)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PictureBox1)
        Me.SplitContainer1.Size = New System.Drawing.Size(834, 485)
        Me.SplitContainer1.SplitterDistance = 418
        Me.SplitContainer1.TabIndex = 5
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(4, 3)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(411, 479)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "The quick brown fox jumps over the lazy dog."
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.加载模板ToolStripMenuItem, Me.ToolStripMenuItem2, Me.ToolStripMenuItem1, Me.ToolStripMenuItem4, Me.测试TToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(858, 28)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '加载模板ToolStripMenuItem
        '
        Me.加载模板ToolStripMenuItem.Name = "加载模板ToolStripMenuItem"
        Me.加载模板ToolStripMenuItem.Size = New System.Drawing.Size(99, 24)
        Me.加载模板ToolStripMenuItem.Text = "加载模板(&L)"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.从文本文件TToolStripMenuItem, Me.从文本文件黑白BToolStripMenuItem})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(72, 24)
        Me.ToolStripMenuItem2.Text = "生成(&G)"
        '
        '从文本文件TToolStripMenuItem
        '
        Me.从文本文件TToolStripMenuItem.Name = "从文本文件TToolStripMenuItem"
        Me.从文本文件TToolStripMenuItem.Size = New System.Drawing.Size(212, 24)
        Me.从文本文件TToolStripMenuItem.Text = "从文本文件(&T)"
        '
        '从文本文件黑白BToolStripMenuItem
        '
        Me.从文本文件黑白BToolStripMenuItem.Name = "从文本文件黑白BToolStripMenuItem"
        Me.从文本文件黑白BToolStripMenuItem.Size = New System.Drawing.Size(212, 24)
        Me.从文本文件黑白BToolStripMenuItem.Text = "从文本文件(黑白)(&B)"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(114, 24)
        Me.ToolStripMenuItem1.Text = "模板编辑器(&E)"
        '
        '测试TToolStripMenuItem
        '
        Me.测试TToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelloWorldHToolStripMenuItem, Me.排版测试TToolStripMenuItem, Me.其它OToolStripMenuItem})
        Me.测试TToolStripMenuItem.Name = "测试TToolStripMenuItem"
        Me.测试TToolStripMenuItem.Size = New System.Drawing.Size(70, 24)
        Me.测试TToolStripMenuItem.Text = "测试(&T)"
        '
        'HelloWorldHToolStripMenuItem
        '
        Me.HelloWorldHToolStripMenuItem.Name = "HelloWorldHToolStripMenuItem"
        Me.HelloWorldHToolStripMenuItem.Size = New System.Drawing.Size(186, 24)
        Me.HelloWorldHToolStripMenuItem.Text = "hello, world(&H)"
        '
        '排版测试TToolStripMenuItem
        '
        Me.排版测试TToolStripMenuItem.Name = "排版测试TToolStripMenuItem"
        Me.排版测试TToolStripMenuItem.Size = New System.Drawing.Size(186, 24)
        Me.排版测试TToolStripMenuItem.Text = "排版测试(&T)"
        '
        '其它OToolStripMenuItem
        '
        Me.其它OToolStripMenuItem.Name = "其它OToolStripMenuItem"
        Me.其它OToolStripMenuItem.Size = New System.Drawing.Size(186, 24)
        Me.其它OToolStripMenuItem.Text = "其它(&O)"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(115, 24)
        Me.ToolStripMenuItem4.Text = "图片预处理(&P)"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(858, 548)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMain"
        Me.Text = "手写体生成器"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 保存图片SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 加载模板ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 测试TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelloWorldHToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 排版测试TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 从文本文件TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 从文本文件黑白BToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 其它OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem

End Class
