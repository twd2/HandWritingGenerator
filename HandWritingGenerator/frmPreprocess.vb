Imports System.Drawing.Imaging

Public Class frmPreprocess

    Private _DisplayImg As Bitmap = Nothing
    Private _DisplayG As Graphics
    Private _imgFilename As String = Nothing
    Private _srcimg As Bitmap
    Private _offsetX As Integer = 0, _offsetY As Integer = 0
    Private physicsAcreageModulus As Double = 0.0
    Private lstRect As New List(Of RectangleWithOffset)

    Public Sub InitImg()
        _DisplayImg = New Bitmap(_srcimg.Width, _srcimg.Height)
        _DisplayImg.SetResolution(_srcimg.HorizontalResolution,
                                _srcimg.VerticalResolution)
        physicsAcreageModulus = 1 / (_srcimg.HorizontalResolution * _srcimg.VerticalResolution)
        DisplayOffsetX.Maximum = _srcimg.Width
        DisplayOffsetY.Maximum = _srcimg.Height
        PictureBox1.Image = _DisplayImg
        _DisplayG = Graphics.FromImage(_DisplayImg)
        _DisplayG.DrawImage(_srcimg, 0, 0)
        PictureBox1.Refresh()
    End Sub

    Private Sub RefreshImg()
        Try
            _DisplayG.Clear(Color.White)
            _DisplayG.ResetTransform()
            _DisplayG.TranslateTransform(_offsetX, _offsetY)
            If ListView1.SelectedIndices.Count <= 0 Then
                _DisplayG.DrawImage(_srcimg, 0, 0)
            Else
                Dim id = ListView1.SelectedIndices(0)
                If id <= 0 Then
                    _DisplayG.DrawImage(ImageProcessor.CutImage(_srcimg, lstRect(id).Rectangle), lstRect(id).NewPostion)
                ElseIf id <= lstRect.Count - 1 Then
                    _DisplayG.DrawImage(ImageProcessor.CutImage(_srcimg, lstRect(id - 1).Rectangle), lstRect(id - 1).NewPostion.X - lstRect(id - 1).Rectangle.Width \ 2, lstRect(id - 1).NewPostion.Y)
                    _DisplayG.DrawImage(ImageProcessor.CutImage(_srcimg, lstRect(id).Rectangle), lstRect(id - 1).NewPostion.X + lstRect(id).Rectangle.Width \ 2, lstRect(id).NewPostion.Y)
                Else
                    '惊悚惊悚
                End If
            End If
            PictureBox1.Refresh()
        Catch ex As Exception

        End Try
    End Sub

    Private Function OpenImage() As Boolean
        Using OFD As New OpenFileDialog
            OFD.Title = "手写体扫描副本"
            OFD.Filter = "图片文件|*.jpg;*.gif;*.bmp;*.png"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return False
            End If
            _imgFilename = OFD.FileName
            _srcimg = Bitmap.FromFile(_imgFilename)
            InitImg()
            RefreshImg()
        End Using
        Return True
    End Function

    Private Function CheckImage() As Boolean
        If _imgFilename Is Nothing Then
            Return OpenImage()
        End If
        Return True
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not CheckImage() Then
            Return
        End If
        TC()
    End Sub

    Private Sub TC()
        Dim sw As New Stopwatch
        sw.Start()
        Dim theta As Double
        _srcimg = TiltCorrection.Do(_srcimg, theta)
        sw.Stop()
        Debug.Print(sw.ElapsedMilliseconds)
        labTheta.Text = "倾斜矫正: " & theta / Math.PI * 180 & "°"
        RefreshImg()
    End Sub

    Private Function CompBlockByY(b1 As Block, b2 As Block) As Integer
        Return b1.Top - b2.Top
    End Function

    Private Sub FindRect()
        Const physicsTopOffset = 1 / 3 'inch
        Const physicsRectHeight = 2 / 3 'inch
        Dim leftBlock As New List(Of Block),
            rightBlock As New List(Of Block)
        Dim bd = BinaryData.FromBitmap(_srcimg)
        Dim InchPerPixelY = 1 / bd.yDPI
        Dim TopOffset = physicsTopOffset / InchPerPixelY
        Dim RectHeight = physicsRectHeight / InchPerPixelY

        Dim blks = ImageProcessor.FindBlocks(bd)
        Dim sqs = ImageProcessor.FindSquares(blks, TiltCorrection.physicsAcreage / physicsAcreageModulus, 0.25)
        Dim halfWidth = _srcimg.Width / 2

        '分左右
        For Each sq In sqs
            If sq.Centre.X > halfWidth Then
                rightBlock.Add(sq)
            Else
                leftBlock.Add(sq)
            End If
        Next
        If leftBlock.Count <> rightBlock.Count Then
            Throw New Exception("leftBlock.Count <> rightBlock.Count")
        End If

        '取出矩形
        lstRect.Clear()
        For i = 0 To leftBlock.Count - 1
            Dim lblk = leftBlock(i),
                rblk = rightBlock(i)
            Dim rect As New Rectangle(lblk.Centre.X, lblk.Centre.Y - TopOffset, rblk.Centre.X - lblk.Centre.X, RectHeight)
            '_DisplayG.DrawRectangle(Pens.Blue, rect)
            lstRect.Add(New RectangleWithOffset(rect, 0, -rect.Y))
        Next
        'PictureBox1.Refresh()
        'Return lstrect
        RefreshList()
    End Sub

    Private Sub DisplayOffsetX_Scroll(sender As Object, e As EventArgs) Handles DisplayOffsetX.Scroll
        _offsetX = -DisplayOffsetX.Value
        RefreshImg()
    End Sub

    Private Sub DisplayOffsetY_Scroll(sender As Object, e As EventArgs) Handles DisplayOffsetY.Scroll
        _offsetY = -DisplayOffsetY.Value
        RefreshImg()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not CheckImage() Then
            Return
        End If
        FindRect()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenImage()
    End Sub

    Private Sub frmPreprocess_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedIndices.Count > 0 Then
            YOffset.Value = lstRect(ListView1.SelectedIndices(0)).YOffset
        End If
        RefreshImg()
    End Sub

    Private Sub RefreshList()
        ListView1.Items.Clear()
        For i = 0 To lstRect.Count - 1
            Dim li As New ListViewItem(i.ToString())
            li.SubItems.Add(lstRect(i).YOffset)
            ListView1.Items.Add(li)
        Next
    End Sub

    Private Sub YOffset_LostFocus(sender As Object, e As EventArgs) Handles YOffset.LostFocus
        RefreshList()
    End Sub

    Private Sub YOffset_ValueChanged(sender As Object, e As EventArgs) Handles YOffset.ValueChanged
        If ListView1.SelectedIndices.Count > 0 Then
            lstRect(ListView1.SelectedIndices(0)).YOffset = YOffset.Value
        End If
        RefreshImg()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If lstRect.Count = 0 Then
            Return
        End If
        Using SFD As New SaveFileDialog
            SFD.Title = "保存图片"
            SFD.Filter = "图片|*.png"
            If SFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If

            Dim sumWidth = 0, maxHeight = 0
            For i = 0 To lstRect.Count - 1
                Dim rect = lstRect(i)
                If rect.Rectangle.Height > maxHeight Then
                    maxHeight = rect.Rectangle.Height
                End If
                sumWidth += rect.Rectangle.Width
            Next
            Dim bmp As New Bitmap(sumWidth, maxHeight)
            bmp.SetResolution(_srcimg.HorizontalResolution,
                               _srcimg.VerticalResolution)
            Using g = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                Dim xoffset = 0
                For i = 0 To lstRect.Count - 1
                    Dim rect = lstRect(i)
                    g.DrawImage(ImageProcessor.CutImage(_srcimg, rect.Rectangle), xoffset, rect.NewPostion.Y)
                    xoffset += rect.Rectangle.Width
                Next
            End Using
            bmp.Save(SFD.FileName, ImageFormat.Png)
            '_CharTemplate.Save(SFD.FileName)
        End Using
    End Sub
End Class