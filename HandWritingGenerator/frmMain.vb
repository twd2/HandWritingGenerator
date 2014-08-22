Imports System.IO

Public Class frmMain

    Dim _TplPath As String = ""
    Dim _CharTemplate As CharTemplate = Nothing

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmCreate.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function CutImg(src As Image, rect As Rectangle) As Image
        Dim result As New Bitmap(rect.Width, rect.Height)
        Using g = Graphics.FromImage(result)
            g.DrawImage(src, -rect.X, -rect.Y)
        End Using
        Return result
    End Function

    Private Function MagicImage(src As Bitmap) As Bitmap
        Dim res As New Bitmap(src.Width, src.Height)
        Dim a, b, c, d, e, f As Integer
        Dim r As New Random
        a = r.Next(500, 1000)
        b = r.Next(0, 500)
        c = 0 ' r.Next(0, 10)
        d = r.Next(0, 500)
        e = r.Next(500, 1000)
        f = 0 'r.Next(0, 10)
        For y = 0 To src.Height - 1
            For x = 0 To src.Width - 1
                Dim newp = MagicPoint(a, b, c, d, e, f, New Point(x, y))
                Try
                    res.SetPixel(newp.X, newp.Y, src.GetPixel(x, y))
                Catch ex As Exception

                End Try
            Next
        Next
        Return res
    End Function

    Private Function MagicPoint(a As Integer, b As Integer, c As Integer, d As Integer, e As Integer, f As Integer, src As Point) As Point
        Dim x, y As Integer
        'a *= 1000
        'b *= 1000
        'c *= 1000
        'd *= 1000
        'e *= 1000
        'f *= 1000
        x = a * src.X / 1000 + b * src.Y / 1000 + c
        y = d * src.X / 1000 + e * src.Y / 1000 + f
        Return New Point(x, y)
    End Function

    Private Function ToImgByTemplate(data As String) As Image
        If _CharTemplate Is Nothing Then
            LoadTemplate()
        End If
        Return _CharTemplate.GenerateImage(data)
    End Function

    Private Function ToImgByTemplate(data() As String) As Image
        If _CharTemplate Is Nothing Then
            LoadTemplate()
        End If
        Return _CharTemplate.GenerateImage(data)
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PictureBox1.Image = ToImgByTemplate({"he"})
        'PictureBox1.Image = ToImgByTemplate({"hello, world",
        '                                     "abcde9f8g7h6i5j4k3l2m1n0",
        '                                     "The quick brown fox jumps over the lazy dog."})
        PictureBox1.Refresh()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim PageSizeW = 100 '每行字符数
        Dim PageSizeH = 39 '行数
        Using OFD As New OpenFileDialog
            OFD.Title = "文字"
            OFD.Filter = "文字|*.txt"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            Dim lns = File.ReadAllLines(OFD.FileName)
            Dim lstPage As New List(Of List(Of String))
            Dim currPage As New List(Of String)

            For i = 0 To lns.Count - 1
                If (i + 1) Mod PageSizeH = 0 Then
                    lstPage.Add(currPage)
                    currPage = New List(Of String)
                End If
                currPage.Add(lns(i))
            Next
            If currPage.Count > 0 Then
                lstPage.Add(currPage)
            End If
            For i = 0 To lstPage.Count - 1
                ToImgByTemplate(lstPage(i).ToArray()).Save("gen\" & i.ToString() & ".jpg")
            Next

        End Using

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadTemplate()
    End Sub

    Private Sub LoadTemplate()
        Using OFD As New OpenFileDialog
            OFD.Title = "手写体模板"
            OFD.Filter = "模板|*.tpl"
            If OFD.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Return
            End If
            _CharTemplate = CharTemplate.Load(OFD.FileName)
        End Using
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        PictureBox1.Image = ToImgByTemplate(TextBox1.Text)
    End Sub
End Class
