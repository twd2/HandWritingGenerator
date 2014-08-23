Imports System.IO
Imports System.Drawing.Imaging

Public Class frmMain

    Dim _TplPath As String = ""
    Dim _CharTemplate As CharTemplate = Nothing

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmEditor.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function ToImgByTemplate(data As String) As Image
        If Not RequireTemplate() Then
            Return Nothing
        End If
        Return _CharTemplate.GenerateImage(data)
    End Function

    Private Function ToImgByTemplate(data() As String) As Image
        If Not RequireTemplate() Then
            Return Nothing
        End If
        Return _CharTemplate.GenerateImage(data)
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'PictureBox1.Image = ToImgByTemplate({"he"})
        PictureBox1.Image = ToImgByTemplate({"hello, world",
                                             "abcde9f8g7h6i5j4k3l2m1n0",
                                             "The quick brown fox jumps over the lazy dog."})
        PictureBox1.Refresh()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not RequireTemplate() Then
            Return
        End If
        Dim A4Width = 7 'inch
        Dim A4Height = 11 'inch
        Dim PageWidthPx As Integer = A4Width * _CharTemplate.MainImg.HorizontalResolution
        Dim PageHeightPx As Integer = A4Height * _CharTemplate.MainImg.VerticalResolution
        Dim PageHeightCount As Integer = PageHeightPx / _CharTemplate.MinHeight
        Using OFD As New OpenFileDialog
            OFD.Title = "文字"
            OFD.Filter = "文字|*.txt"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            Dim tokens As New Tokenizer(File.ReadAllText(OFD.FileName))
            tokens.Scan()
            Dim typeset As New Typesetter(tokens, _CharTemplate)
            typeset.PageWidth = PageWidthPx
            typeset.Typeset(False)
            Dim lns = typeset.ToString().Split({vbCrLf}, StringSplitOptions.None)
            Dim lstPage As New List(Of List(Of String))
            Dim currPage As New List(Of String)

            '分页
            For i = 0 To lns.Count - 1
                If (i + 1) Mod PageHeightCount = 0 Then
                    lstPage.Add(currPage)
                    currPage = New List(Of String)
                End If
                currPage.Add(lns(i))
            Next
            If currPage.Count > 0 Then
                lstPage.Add(currPage)
            End If

            Dim SaveTo = OFD.FileName + ".gen"

            If Not Directory.Exists(SaveTo) Then
                Directory.CreateDirectory(SaveTo)
            End If

            For i = 0 To lstPage.Count - 1
                ToImgByTemplate(lstPage(i).ToArray()).Save(SaveTo + "\P" & i.ToString() & ".jpg")
            Next
            MsgBox("完成")
        End Using

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadTemplate()
    End Sub

    Private Function LoadTemplate() As Boolean
        Using OFD As New OpenFileDialog
            OFD.Title = "手写体模板"
            OFD.Filter = "模板|*.tpl"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return False
            End If
            _CharTemplate = CharTemplate.Load(OFD.FileName)
        End Using
        Return True
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            Dim token As New Tokenizer(TextBox1.Text)
            token.Scan()
            Dim ts As New Typesetter(token, _CharTemplate)
            ts.PageWidth = PictureBox1.Width
            'ts.PageMaxLineCount = Integer.MaxValue
            ts.Typeset(False)
            PictureBox1.Image = ToImgByTemplate(ts.ToString()) 'BinaryData.FromBitmap(ToImgByTemplate(ts.ToString())).ToBitmap()
        Catch ex As Exception
            Debug.Print(ex.ToString())
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Not RequireTemplate() Then
            Return
        End If
        Dim token As New Tokenizer(File.ReadAllText("test.txt"))
        token.Scan()
        Dim ts As New Typesetter(token, _CharTemplate)
        ts.PageWidth = 30
        'ts.PageMaxLineCount = Integer.MaxValue
        ts.Typeset(False)
        ts.ToString()
    End Sub

    Private Function RequireTemplate() As Boolean
        If _CharTemplate Is Nothing Then
            Return LoadTemplate()
        End If
        Return True
    End Function

    Private Sub 保存图片SToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存图片SToolStripMenuItem.Click
        If PictureBox1.Image Is Nothing Then
            Return
        End If
        Using SFD As New SaveFileDialog
            SFD.Title = "保存图片"
            SFD.Filter = "PNG|*.png"
            If SFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            PictureBox1.Image.Save(SFD.FileName, ImageFormat.Png)
        End Using
    End Sub
End Class
