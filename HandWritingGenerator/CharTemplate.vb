Imports System.IO
Imports System.Text
Imports System.Drawing.Imaging

Public Class CharTemplate

    Public charMap As New Dictionary(Of Char, CharInfo)
    Public MainImg As Bitmap = Nothing

    Public Sub New()

    End Sub

    Public Shared Function Load(filename As String) As CharTemplate
        Dim dpi = GetScreenDPI()
        'We need this to deal with DPI<>96
        Return Load(filename, dpi.X / 96, dpi.Y / 96)
    End Function

    Public Shared Function Load(filename As String, modulusX As Double, modulusY As Double) As CharTemplate
        Dim ct As New CharTemplate

        Using sr As New StreamReader(filename, Encoding.UTF8)
            Using ms As New MemoryStream(Convert.FromBase64String(sr.ReadLine()))
                ct.setMainImgFromStream(ms)
            End Using
            Do While Not sr.EndOfStream
                Dim strdata = sr.ReadLine()
                Dim data = strdata.Substring(1).Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
                Dim x1 As Integer = modulusX * Int32.Parse(data(0)),
                    y1 As Integer = modulusY * Int32.Parse(data(1)),
                    x2 As Integer = modulusX * Int32.Parse(data(2)),
                    y2 As Integer = modulusY * Int32.Parse(data(3))
                Dim rect As New Rectangle(Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x1 - x2), Math.Abs(y1 - y2))
                Dim currChar As New CharInfo(strdata(0), rect)
                currChar.MakeCache(ct.MainImg)
                ct.charMap.Add(currChar.c, currChar)
            Loop
        End Using
        Return ct
    End Function

    Public Sub setMainImgFromStream(s As Stream)
        Dim bmp = Bitmap.FromStream(s)
        MainImg = New Bitmap(bmp.Width, bmp.Height)
        MainImg.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution)
        Using g = Graphics.FromImage(MainImg)
            g.DrawImage(bmp, 0, 0)
        End Using
        bmp.Dispose()
    End Sub

    Public Sub setMainImgFromFile(filename As String)
        Dim bmp = Bitmap.FromFile(filename)
        MainImg = New Bitmap(bmp.Width, bmp.Height)
        MainImg.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution)
        Using g = Graphics.FromImage(MainImg)
            g.DrawImage(bmp, 0, 0)
        End Using
        bmp.Dispose()
    End Sub

    Public Function GetCharImg(c As Char) As Image
        Return charMap(c).img
    End Function

    Public Sub Save(filename As String, modulusX As Double, modulusY As Double)
        Using sw As New StreamWriter(filename, False, Encoding.UTF8)
            Dim imgdata As Byte()
            Using ms As New MemoryStream()
                MainImg.Save(ms, ImageFormat.Jpeg)
                imgdata = ms.ToArray()
            End Using
            sw.WriteLine(Convert.ToBase64String(imgdata))
            For Each ci In charMap.Values
                sw.Write(ci.c)
                sw.Write(" ")
                sw.Write(CInt(ci.rect.Left * modulusX)) '左上x
                sw.Write(" ")
                sw.Write(CInt(ci.rect.Top * modulusY)) '左上y
                sw.Write(" ")
                sw.Write(CInt(ci.rect.Right * modulusX)) '右上x
                sw.Write(" ")
                sw.Write(CInt(ci.rect.Bottom * modulusY)) '右上y
                sw.WriteLine()
            Next
            sw.Flush()
        End Using
    End Sub

    Public Sub Save(filename As String)
        Dim dpi = GetScreenDPI()
        'We need this to deal with DPI<>96
        Save(filename, 96 / dpi.X, 96 / dpi.Y)
    End Sub

    Public Shared Function GetScreenDPI() As PointF
        Using g = Graphics.FromHwnd(IntPtr.Zero)
            Return New PointF(g.DpiX, g.DpiY)
        End Using
    End Function

    Public Function GenerateImage(lines() As String) As Image
        '预处理计算大小~
        Dim maxw = 0 '最宽行的宽度
        Dim maxchar = 0 '最多字符数
        For Each l In lines
            Dim myw = 0, mychar = 0
            For Each ch In l
                Try
                    If Not charMap.ContainsKey(ch) Then
                        Continue For
                    End If
                    myw += charMap(ch).img.Width
                Catch ex As Exception
                    'Debug.Print(ex.ToString())
                    Debug.Print(ch)
                End Try
                mychar += 1
            Next
            If myw > maxw Then
                maxw = myw
            End If
            If mychar > maxchar Then
                maxchar = mychar
            End If
        Next
        Dim maxh = 0 '单行最高(按理说一样的)
        For Each ci In charMap.Values
            If ci.img.Height > maxh Then
                maxh = ci.img.Height
            End If
        Next

        If maxw <= 0 OrElse maxh * lines.Count <= 0 Then
            Return Nothing
        End If

        Dim result As New Bitmap(maxw, maxh * lines.Count)
        Using g = Graphics.FromImage(result)
            For y = 0 To lines.Count - 1
                Dim line = lines(y)
                Dim offset = 0
                For x = 0 To lines(y).Count - 1
                    Dim currch = line(x)
                    If Not charMap.ContainsKey(currch) Then
                        Continue For
                    End If
                    Dim currImg = charMap(currch).img
                    '仿射变换
                    'currImg = MagicImage(currImg)
                    g.DrawImage(currImg, offset, y * maxh)
                    offset += currImg.Width
                Next
                '空格补齐
                If charMap.ContainsKey(" ") Then
                    Dim spImg = charMap(" ").img
                    Dim padding = maxw - offset
                    Dim paddingCount = padding / spImg.Width
                    For i = 1 To paddingCount
                        g.DrawImage(spImg, offset, y * maxh)
                        offset += spImg.Width
                    Next
                End If
            Next
        End Using
        Return result
    End Function

    Public Function GenerateImage(data As String) As Image
        Dim lines = data.Split({vbCrLf, vbCr, vbLf}, StringSplitOptions.None)
        Return GenerateImage(lines)
    End Function


End Class
