Imports System.IO

Public Class frmCreate

    Enum LRTB
        Left
        Right
        Top
        Bottom
    End Enum

    Dim _Clicked As Boolean = False

    Dim _DisplayImg As Image = Nothing
    Dim _DisplayG As Graphics
    Dim _offsetX = 0, _offsetY = 0
    Dim _TOffsetValue = 0, _BOffsetValue = 10, _LOffsetValue = 0, _ROffsetValue = 0
    Dim _LRTB As LRTB = LRTB.Top
    Dim _CharTemplate As CharTemplate

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnOpenImage.Click
        If _CharTemplate Is Nothing Then
            MsgBox("请先新建一个模板")
            Return
        End If
        Using OFD As New OpenFileDialog
            OFD.Title = "手写体扫描副本"
            OFD.Filter = "图片文件|*.jpg;*.gif;*.bmp;*.png"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            _CharTemplate.setMainImgFromFile(OFD.FileName)
            UpdateLimits()
        End Using
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles DisplayOffsetX.Scroll
        _offsetX = -DisplayOffsetX.Value
        RefreshImg()
    End Sub

    Private Sub RefreshImg()
        Try
            _DisplayG.Clear(Color.White)
            _DisplayG.DrawImage(_CharTemplate.MainImg, 0 + _offsetX, 0 + _offsetY)
            '左
            _DisplayG.DrawLine(Pens.Blue, _LOffsetValue + _offsetX, 0 + _offsetY, _LOffsetValue + _offsetX, _CharTemplate.MainImg.Height + _offsetY)
            '右
            _DisplayG.DrawLine(Pens.Chocolate, _ROffsetValue + _offsetX, 0 + _offsetY, _ROffsetValue + _offsetX, _CharTemplate.MainImg.Height + _offsetY)
            '上
            _DisplayG.DrawLine(Pens.Red, 0 + _offsetX, _TOffsetValue + _offsetY, _CharTemplate.MainImg.Width + _offsetX, _TOffsetValue + _offsetY)
            '下
            _DisplayG.DrawLine(Pens.Green, 0 + _offsetX, _BOffsetValue + _offsetY, _CharTemplate.MainImg.Width + _offsetX, _BOffsetValue + _offsetY)
            PictureBox1.Refresh()
        Catch ex As Exception
            Debug.Print(ex.ToString())
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick

    End Sub

    Private Sub LOffset_GotFocus(sender As Object, e As EventArgs) Handles LOffset.GotFocus
        _LRTB = LRTB.Left
    End Sub

    Private Sub ROffset_GotFocus(sender As Object, e As EventArgs) Handles ROffset.GotFocus
        _LRTB = LRTB.Right
    End Sub

    Private Sub TOffset_GotFocus(sender As Object, e As EventArgs) Handles TOffset.GotFocus
        _LRTB = LRTB.Top
    End Sub

    Private Sub DOffset_GotFocus(sender As Object, e As EventArgs) Handles BOffset.GotFocus
        _LRTB = LRTB.Bottom
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        _Clicked = True
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If Not _Clicked Then
            Return
        End If
        Try
            Dim absPoint As New Point(e.X - _offsetX, e.Y)
            Select Case _LRTB
                Case LRTB.Left
                    LOffset.Value = absPoint.X
                Case LRTB.Right
                    ROffset.Value = absPoint.X
                Case LRTB.Top
                    If TOffset.Enabled Then
                        TOffset.Value = absPoint.Y
                    End If
                Case LRTB.Bottom
                    If BOffset.Enabled Then
                        BOffset.Value = absPoint.Y
                    End If
                Case Else
                    Return
            End Select
        Catch ex As Exception

        End Try
        'RefreshImg()
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        _Clicked = False
    End Sub

    Private Sub BOffset_ValueChanged(sender As Object, e As EventArgs) Handles BOffset.ValueChanged
        _BOffsetValue = BOffset.Value
        RefreshImg()
    End Sub

    Private Sub LOffset_ValueChanged(sender As Object, e As EventArgs) Handles LOffset.ValueChanged
        _LOffsetValue = LOffset.Value
        RefreshImg()
    End Sub

    Private Sub ROffset_ValueChanged(sender As Object, e As EventArgs) Handles ROffset.ValueChanged
        _ROffsetValue = ROffset.Value
        RefreshImg()
    End Sub

    Private Sub TOffset_ValueChanged(sender As Object, e As EventArgs) Handles TOffset.ValueChanged
        _TOffsetValue = TOffset.Value
        RefreshImg()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles BOffsetLock.CheckedChanged
        BOffset.Enabled = Not BOffsetLock.Checked
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles TOffsetLock.CheckedChanged
        TOffset.Enabled = Not TOffsetLock.Checked
    End Sub

    Private Sub btnAddChar_Click(sender As Object, e As EventArgs) Handles btnAddChar.Click
        If _CharTemplate Is Nothing Then
            MsgBox("请先新建一个模板")
            Return
        End If
        BOffsetLock.Checked = True
        TOffsetLock.Checked = True
        If TextBox1.Text.Length < 1 Then
            MsgBox("请输入字符")
            Return
        End If
        Dim ci As New CharInfo(TextBox1.Text(0), LOffset.Value, TOffset.Value, ROffset.Value, BOffset.Value)
        If _CharTemplate.charMap.ContainsKey(ci.c) Then
            If MsgBox("已经存在该字符, 是否要替换", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                _CharTemplate.charMap(ci.c) = ci
            End If
        Else
            _CharTemplate.charMap.Add(ci.c, ci)
        End If
        '_lstChars.Add(New myChar With {.c = TextBox1.Text, .x1 = LOffset.Value, .x2 = ROffset.Value, .y1 = TOffset.Value, .y2 = BOffset.Value})
        RefreshTable()
    End Sub

    Private Sub RefreshTable()
        ListView1.Items.Clear()
        For Each ci In _CharTemplate.charMap.Values
            Dim lvi As New ListViewItem(ci.c)
            lvi.SubItems.Add(ci.rect.Left)
            lvi.SubItems.Add(ci.rect.Top)
            lvi.SubItems.Add(ci.rect.Right)
            lvi.SubItems.Add(ci.rect.Bottom)
            ListView1.Items.Add(lvi)
        Next
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedIndices.Count < 1 Then
            Return
        End If
        '_lstChars.RemoveAt(ListView1.SelectedIndices(0))
        _CharTemplate.charMap.Remove(ListView1.SelectedItems(0).Text)
        RefreshTable()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedIndices.Count < 1 Then
            Return
        End If
        Dim ci = _CharTemplate.charMap(ListView1.SelectedItems(0).Text)
        TextBox1.Text = ci.c
        LOffset.Value = ci.rect.Left
        ROffset.Value = ci.rect.Right
        TOffset.Value = ci.rect.Top
        BOffset.Value = ci.rect.Bottom
        DisplayOffsetX.Value = Math.Min(LOffset.Value, DisplayOffsetX.Value)
        _offsetX = -DisplayOffsetX.Value
        RefreshImg()
    End Sub

    Private Sub btnSaveTemplate_Click(sender As Object, e As EventArgs) Handles btnSaveTemplate.Click
        If _CharTemplate Is Nothing Then
            MsgBox("请先新建一个模板")
            Return
        End If
        Using SFD As New SaveFileDialog
            SFD.Title = "保存模板"
            SFD.Filter = "模板|*.tpl"
            If SFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            _CharTemplate.Save(SFD.FileName)
        End Using
    End Sub

    Private Sub btnNewTemplate_Click(sender As Object, e As EventArgs) Handles btnNewTemplate.Click
        _CharTemplate = New CharTemplate()
    End Sub

    Private Sub UpdateLimits()
        If _DisplayImg IsNot Nothing Then
            _DisplayImg.Dispose()
            _DisplayImg = Nothing
        End If
        _DisplayImg = New Bitmap(_CharTemplate.MainImg.Width, _CharTemplate.MainImg.Height)
        DisplayOffsetX.Maximum = _CharTemplate.MainImg.Width
        DisplayOffsetY.Maximum = _CharTemplate.MainImg.Height
        LOffset.Maximum = _CharTemplate.MainImg.Width
        ROffset.Maximum = _CharTemplate.MainImg.Width
        TOffset.Maximum = _CharTemplate.MainImg.Height
        BOffset.Maximum = _CharTemplate.MainImg.Height
        PictureBox1.Image = _DisplayImg
        _DisplayG = Graphics.FromImage(_DisplayImg)
        _DisplayG.DrawImage(_CharTemplate.MainImg, 0, 0)
        PictureBox1.Refresh()
        RefreshImg()
    End Sub

    Private Sub btnOpenTemplate_Click(sender As Object, e As EventArgs) Handles btnOpenTemplate.Click
        Using OFD As New OpenFileDialog
            OFD.Title = "手写体模板"
            OFD.Filter = "模板|*.tpl"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            _CharTemplate = CharTemplate.Load(OFD.FileName)
            UpdateLimits()
            RefreshTable()
        End Using
    End Sub

    Private Sub TrackBar1_Scroll_1(sender As Object, e As EventArgs) Handles DisplayOffsetY.Scroll
        _offsetY = -DisplayOffsetY.Value
        RefreshImg()
    End Sub

    Private Sub SaveImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveImageToolStripMenuItem.Click
        If _CharTemplate Is Nothing Then
            Return
        End If
        Using SFD As New SaveFileDialog
            SFD.Title = "保存图片"
            SFD.Filter = "JPEG|*.jpg"
            If SFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            _CharTemplate.MainImg.Save(SFD.FileName)
        End Using
    End Sub
End Class