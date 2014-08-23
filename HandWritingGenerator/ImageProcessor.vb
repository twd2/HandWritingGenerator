Imports System.Runtime.InteropServices

Public Class ImageProcessor

    Public Shared Function Gray(r As Integer, g As Integer, b As Integer) As Double
        Return r * 0.299 + g * 0.587 + b * 0.114
    End Function

    '寻找二值化的阈值
    Public Shared Function FindBinarizationThreshold(gr As GrayData, Optional accuracy As Double = 0.1) As Double
        Dim lastT = 0.0, T = 127.5
        Do While Math.Abs(T - lastT) > accuracy
            lastT = T
            Dim sumW = 0L, sumB = 0L
            Dim countW = 0, countB = 0
            For y = 0 To gr.Height - 1
                For x = 0 To gr.Width - 1
                    If gr(y, x) >= T Then 'white
                        countW += 1
                        sumW += gr(y, x)
                    Else 'black
                        countB += 1
                        sumB += gr(y, x)
                    End If
                Next
            Next
            If countW = 0 Then
                countW = 1
            End If
            If countB = 0 Then
                countB = 1
            End If
            Dim avrW = sumW / countW
            Dim avrB = sumB / countB
            T = (avrW + avrB) / 2
        Loop
        Return T
    End Function

    Public Shared Function CutImage(src As Image, rect As Rectangle) As Bitmap
        Dim result As New Bitmap(rect.Width, rect.Height)
        result.SetResolution(src.HorizontalResolution, src.VerticalResolution)
        Using g = Graphics.FromImage(result)
            g.DrawImage(src, -rect.X, -rect.Y)
        End Using
        Return result
    End Function

    Public Shared Function CalcVerticalBlackCount(bmp As Bitmap,
                                           startX As Integer,
                                           endX As Integer,
                                           top As Integer,
                                           bottom As Integer) As Integer()
        Dim result(endX - startX - 1 + 1) As Integer

        'to raw
        Dim raw = RawData.FromBitmap(bmp, New Rectangle(startX, top, endX - startX + 1, bottom - top + 1))

        'Binarization
        Dim bin = BinaryData.FromRawData(raw)

        For x = 0 To bin.Width - 1
            result(x) = 0
            For y = 0 To bin.Height - 1
                result(x) += IIf(bin(y, x), 1, 0)
            Next
        Next
        Return result
    End Function

    '寻找每列黑点数的阈值, 使得大于等于阈值的为字符的一部分
    Public Shared Function FindCountThreshold(count As Integer(), Optional accuracy As Double = 0.1) As Double
        Dim lastT = -1.0, T = 1.0
        Do While Math.Abs(T - lastT) > accuracy
            lastT = T
            Dim sumChar = 0L, sumSpace = 0L
            Dim countChar = 0, countSpace = 0
            For i = 0 To count.Length - 1
                If count(i) >= T Then 'Char
                    countChar += 1
                    sumChar += count(i)
                Else 'Space
                    countSpace += 1
                    sumSpace += count(i)
                End If
            Next
            If countChar = 0 Then
                countChar = 1
            End If
            If countSpace = 0 Then
                countSpace = 1
            End If
            Dim avrChar = sumChar / countChar
            Dim avrSpace = sumSpace / countSpace
            T = (avrChar + avrSpace) / 2
        Loop
        Return T
    End Function

    '计算连续为字符/空格的个数也就是字符/空格的宽度
    Public Shared Function SplitCount(count As Integer(), Optional T As Integer = 0) As List(Of CharRegion)
        Dim Regions As New List(Of CharRegion)
        Dim currRegion As CharRegion
        Dim start = 0
        Do While start <= count.Length - 1
            currRegion = CharRegion.Read(count, start, T)
            Regions.Add(currRegion)
            start = currRegion.RightOffset + 1
        Loop
        Return Regions
    End Function

    Public Shared Function FindFragmentThreshold(cr As List(Of CharRegion), Optional accuracy As Double = 0.1) As Double
        Dim lastT = -1.0, T = 1.0
        Do While Math.Abs(T - lastT) > accuracy
            lastT = T

            Dim sumB = 0L, sumF = 0L
            Dim countB = 0, countF = 0
            For i = 0 To cr.Count - 1
                If cr(i).Length() >= T Then 'block
                    countB += 1
                    sumB += cr(i).Length()
                Else 'fragment
                    countF += 1
                    sumF += cr(i).Length()
                End If
            Next
            If countF = 0 Then
                countF = 1
            End If
            If countB = 0 Then
                countB = 1
            End If
            Dim avrB = sumB / countB
            Dim avrF = sumF / countF
            T = (avrB + avrF) / 2
        Loop
        Return T
    End Function

    Public Shared Sub CombineFragment(cr As List(Of CharRegion), Optional T As Integer = 1)
        '合并碎片
        Dim start = 0
        Do While start <= cr.Count - 1
            Dim i = start + 1
            If cr(start).Type = CharRegionType.Space Then
                start = i
                Continue Do
            End If
            Do While (cr(start).Length() < T) AndAlso (i <= cr.Count - 1 - 1) '是fragment并且有的合并就合并
                cr(start).Combine(cr(i)) '下一个不同种的
                cr(start).Combine(cr(i + 1)) '下一个同种的
                i += 2
            Loop
            If cr(start).Length() < T Then '还是碎片
                cr(start).deleted = True
            End If
            start = i
            cr.RemoveAll(Function(r As CharRegion)
                             Return r.deleted
                         End Function)
        Loop
    End Sub


    Public Shared Function CalcHorizontalBlackCount(bmp As Bitmap,
                                           startY As Integer,
                                           endY As Integer,
                                           left As Integer,
                                           right As Integer) As Integer()
        Dim result(endY - startY - 1 + 1) As Integer

        'to raw
        Dim raw = RawData.FromBitmap(bmp, New Rectangle(left, startY, right - left, endY - startY))

        'Binarization
        Dim bin = BinaryData.FromRawData(raw)

        For y = 0 To bin.Height - 1
            result(y) = 0
            For x = 0 To bin.Width - 1
                result(y) += IIf(bin(y, x), 1, 0)
            Next
        Next
        Return result
    End Function

    'Private Function MagicImage(src As Bitmap) As Bitmap
    '    Dim res As New Bitmap(src.Width, src.Height)
    '    Dim a, b, c, d, e, f As Integer
    '    Dim r As New Random
    '    a = r.Next(500, 1000)
    '    b = r.Next(0, 500)
    '    c = 0 ' r.Next(0, 10)
    '    d = r.Next(0, 500)
    '    e = r.Next(500, 1000)
    '    f = 0 'r.Next(0, 10)
    '    For y = 0 To src.Height - 1
    '        For x = 0 To src.Width - 1
    '            Dim newp = MagicPoint(a, b, c, d, e, f, New Point(x, y))
    '            Try
    '                res.SetPixel(newp.X, newp.Y, src.GetPixel(x, y))
    '            Catch ex As Exception

    '            End Try
    '        Next
    '    Next
    '    Return res
    'End Function

    'Private Function MagicPoint(a As Integer, b As Integer, c As Integer, d As Integer, e As Integer, f As Integer, src As Point) As Point
    '    Dim x, y As Integer
    '    'a *= 1000
    '    'b *= 1000
    '    'c *= 1000
    '    'd *= 1000
    '    'e *= 1000
    '    'f *= 1000
    '    x = a * src.X / 1000 + b * src.Y / 1000 + c
    '    y = d * src.X / 1000 + e * src.Y / 1000 + f
    '    Return New Point(x, y)
    'End Function

End Class
