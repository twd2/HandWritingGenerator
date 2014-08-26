Imports System.IO
Imports System.Drawing.Imaging

Public Class frmEditor

    Enum LRTB
        Left
        Right
        Top
        Bottom
    End Enum

    Dim _Clicked As Boolean = False

    Dim _DisplayImg As Bitmap = Nothing
    Dim _DisplayG As Graphics
    Dim _offsetX As Integer = 0, _offsetY As Integer = 0
    Dim _TOffsetValue As Integer = 0, _BOffsetValue As Integer = 10,
        _LOffsetValue As Integer = 0, _ROffsetValue As Integer = 0
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
            _DisplayG.ResetTransform()
            _DisplayG.TranslateTransform(_offsetX, _offsetY)
            _DisplayG.DrawImage(_CharTemplate.MainImg, 0, 0)
            '左
            _DisplayG.DrawLine(Pens.Blue, _LOffsetValue, 0, _LOffsetValue, _CharTemplate.MainImg.Height)
            '右
            _DisplayG.DrawLine(Pens.Chocolate, _ROffsetValue, 0, _ROffsetValue, _CharTemplate.MainImg.Height)
            '上
            _DisplayG.DrawLine(Pens.Red, 0, _TOffsetValue, _CharTemplate.MainImg.Width, _TOffsetValue)
            '下
            _DisplayG.DrawLine(Pens.Green, 0, _BOffsetValue, _CharTemplate.MainImg.Width, _BOffsetValue)
            Try
                PictureBox1.Refresh()
            Catch ex As Exception

            End Try
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
            Dim absPoint As New Point(e.X - _offsetX, e.Y - _offsetY)
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
        If _EnabledAutoCalc Then
            If _LOffsetValue <> _LastCalcLOffset Then
                _LAdjustCount += 1
                _LAdjustSum += _LOffsetValue - _LastCalcLOffset
            End If
            If _ROffsetValue <> _LastCalcROffset Then
                _RAdjustCount += 1
                _RAdjustSum += _ROffsetValue - _LastCalcROffset
            End If
        End If
        '_lstChars.Add(New myChar With {.c = TextBox1.Text, .x1 = LOffset.Value, .x2 = ROffset.Value, .y1 = TOffset.Value, .y2 = BOffset.Value})
        RefreshTable()

        TextBox1.Text = ChrW(AscW(TextBox1.Text) + 1)
    End Sub

    Private Sub RefreshTable()
        'Dim selectId = -1
        'If ListView1.SelectedIndices.Count > 0 Then
        '    selectId = ListView1.SelectedIndices(0)
        'End If

        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        For Each ci In _CharTemplate.charMap.Values
            Dim lvi As New ListViewItem(ci.c)
            lvi.SubItems.Add(ci.rect.Left)
            lvi.SubItems.Add(ci.rect.Top)
            lvi.SubItems.Add(ci.rect.Right)
            lvi.SubItems.Add(ci.rect.Bottom)
            ListView1.Items.Add(lvi)
        Next
        ListView1.EndUpdate()
        'If selectId >= 0 AndAlso selectId <= ListView1.Items.Count - 1 Then

        'End If
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
        AutoPostion()
        RefreshImg()
    End Sub

    Private Sub AutoPostion()
        DisplayOffsetX.Value = Math.Max(Math.Min(LOffset.Value - 10, DisplayOffsetX.Value), DisplayOffsetX.Minimum)
        DisplayOffsetX.Value = Math.Min(Math.Max(10 + ROffset.Value - PictureBox1.Width, DisplayOffsetX.Value), DisplayOffsetX.Maximum)
        _offsetX = -DisplayOffsetX.Value
        DisplayOffsetY.Value = Math.Max(Math.Min(TOffset.Value - 10, DisplayOffsetY.Value), DisplayOffsetY.Minimum)
        DisplayOffsetY.Value = Math.Min(Math.Max(10 + BOffset.Value - PictureBox1.Height, DisplayOffsetY.Value), DisplayOffsetY.Maximum)
        _offsetY = -DisplayOffsetY.Value
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
        _DisplayImg.SetResolution(_CharTemplate.MainImg.HorizontalResolution,
                                _CharTemplate.MainImg.VerticalResolution)
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
            SFD.Filter = "PNG|*.png"
            If SFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            _CharTemplate.MainImg.Save(SFD.FileName, ImageFormat.Png)
        End Using
    End Sub


    Private Sub 生成图线GToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 生成图线GToolStripMenuItem.Click
        'If _CharTemplate Is Nothing Then
        '    Return
        'End If
        'Using sw As New StreamWriter("xx.csv", False)
        '    Dim data = ImageProcessor.CalcVerticalBlackCount(_CharTemplate.MainImg, _LOffsetValue, _CharTemplate.MainImg.Width - 1, _TOffsetValue, _BOffsetValue)
        '    Dim T = ImageProcessor.FindCountThreshold(data)
        '    Dim lst = ImageProcessor.SplitCount(data, T)
        '    Dim minlength = ImageProcessor.FindFragmentThreshold(lst)
        '    ImageProcessor.CombineFragment(lst, minlength)
        '    For i = 0 To data.Length - 1
        '        sw.WriteLine("{0},{1}", i, data(i))
        '    Next
        'End Using
    End Sub

    Private Sub frmCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Dim _EnabledAutoCalc As Boolean = False
    'Dim _CharRegionsOffset As Integer = 0
    'Dim _CharRegions As List(Of CharRegion) = Nothing
    'Dim _CharRegionsIndex As Integer = 0
    Dim _LAdjustCount As Integer = 0
    Dim _LAdjustSum As Long = 0
    Dim _RAdjustCount As Integer = 0
    Dim _RAdjustSum As Long = 0
    Dim _LastCalcLOffset As Integer
    Dim _LastCalcROffset As Integer

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'If _CharRegions Is Nothing Then

        'test
        'Dim rd = RawData.FromBitmap(_CharTemplate.MainImg)
        'Dim bin = BinaryData.FromRawData(rd)
        '_CharTemplate.MainImg = bin.ToBitmap()
        ''_CharTemplate.MainImg = ImageProcessor.GrayBitmap(gr)
        'PictureBox1.Refresh()
        Dim data = ImageProcessor.CalcVerticalBlackCount(_CharTemplate.MainImg, _ROffsetValue + 1, _CharTemplate.MainImg.Width - 1, _TOffsetValue, _BOffsetValue)
        Dim T = 5 'ImageProcessor.FindCountThreshold(data)
        Dim CharRegions = ImageProcessor.SplitBlackCount(data, T)
        Dim CharRegionsOffset = _ROffsetValue
        Dim minlength = ImageProcessor.FindFragmentThreshold(CharRegions)
        ImageProcessor.CombineFragment(CharRegions, minlength)
        Dim CharRegionsIndex = 0
        Do While CharRegionsIndex <= CharRegions.Count - 1 AndAlso CharRegions(CharRegionsIndex).Type <> CharRegionType.Char
            CharRegionsIndex += 1
        Loop

        'End If
        If CharRegionsIndex > CharRegions.Count - 1 Then
            MsgBox("找不到了")
            Return
        End If


        LOffset.Value = CharRegionsOffset + CharRegions(CharRegionsIndex).LeftOffset
        If _LAdjustCount > 0 Then
            Dim ladjoff = _LAdjustSum / _LAdjustCount
            LOffset.Value += ladjoff
        End If
        ROffset.Value = CharRegionsOffset + CharRegions(CharRegionsIndex).RightOffset
        If _RAdjustCount > 0 Then
            Dim radjoff = _RAdjustSum / _RAdjustCount
            ROffset.Value += radjoff
        End If

        _LOffsetValue = LOffset.Value
        _LastCalcLOffset = LOffset.Value
        _ROffsetValue = ROffset.Value
        _LastCalcROffset = ROffset.Value
        AutoPostion()

        _EnabledAutoCalc = True
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub
End Class