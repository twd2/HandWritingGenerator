Imports System.Runtime.InteropServices

Public Class ImageProcessor

    Public Shared Function Gray(r As Integer, g As Integer, b As Integer) As Double
        Return r * 0.299 + g * 0.587 + b * 0.114
    End Function

    Public Shared Function ToGray(id As ImageData) As Integer(,)
        Dim gr(id.Height - 1, id.Width - 1) As Integer
        For y = 0 To id.Height - 1
            For x = 0 To id.Width - 1
                gr(y, x) = Gray(id(y, x, 0), id(y, x, 1), id(y, x, 2))
            Next
        Next
        Return gr
    End Function

    Public Shared Function ToGray(i As Bitmap) As Integer(,)
        Return ToGray(ImageData.FromBitmap(i))
    End Function

    Public Shared Function Binarization(gr As Integer(,), Optional T As Double = 128) As Boolean(,)
        'Dim T = FindThreshold(gr, acc)
        Dim bin(gr.GetUpperBound(0), gr.GetUpperBound(1)) As Boolean
        For y = 0 To gr.GetUpperBound(0)
            For x = 0 To gr.GetUpperBound(1)
                bin(y, x) = gr(y, x) < T
            Next
        Next
        Return bin
    End Function

    Public Shared Function BinarizationBitmap(bin As Boolean(,)) As Bitmap
        Dim bmp As New Bitmap(bin.GetUpperBound(1) + 1, bin.GetUpperBound(0) + 1)
        Dim id = ImageData.FromBitmap(bmp)
        For y = 0 To bmp.Height - 1
            For x = 0 To bmp.Width - 1
                Dim a = IIf(bin(y, x), 0, 255)
                id(y, x, 0) = a
                id(y, x, 1) = a
                id(y, x, 2) = a
            Next
        Next
        Dim bmpdata = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format24bppRgb)
        Marshal.Copy(id.bmpdata, 0, bmpdata.Scan0, id.Size())
        bmp.UnlockBits(bmpdata)
        Return bmp
    End Function

    Public Shared Function GrayBitmap(gr As Integer(,)) As Bitmap
        Dim bmp As New Bitmap(gr.GetUpperBound(1) + 1, gr.GetUpperBound(0) + 1)
        Dim id = ImageData.FromBitmap(bmp)
        For y = 0 To bmp.Height - 1
            For x = 0 To bmp.Width - 1
                Dim a = gr(y, x)
                id(y, x, 0) = a
                id(y, x, 1) = a
                id(y, x, 2) = a
            Next
        Next
        Dim bmpdata = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format24bppRgb)
        Marshal.Copy(id.bmpdata, 0, bmpdata.Scan0, id.Size())
        bmp.UnlockBits(bmpdata)
        Return bmp
    End Function

    '寻找二值化的阈值
    Public Shared Function FindThreshold(gr As Integer(,), Optional accuracy As Double = 0.1) As Double
        Dim lastT = 0.0, T = 127.5
        Do While Math.Abs(T - lastT) > accuracy
            lastT = T
            Dim sumW = 0L, sumB = 0L
            Dim countW = 0, countB = 0
            For y = 0 To gr.GetUpperBound(0)
                For x = 0 To gr.GetUpperBound(1)
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

    Public Shared Function CutImg(src As Image, rect As Rectangle) As Bitmap
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
        Dim raw = ImageData.FromBitmap(bmp, New Rectangle(startX, top, endX - startX + 1, bottom - top + 1))

        'Binarization
        Dim gr = ToGray(raw)
        Dim T = FindThreshold(gr)
        Dim bin = Binarization(gr, T)

        For x = 0 To bin.GetUpperBound(1)
            result(x) = 0
            For y = 0 To bin.GetUpperBound(0)
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
        Dim raw = ImageData.FromBitmap(bmp, New Rectangle(left, startY, right - left, endY - startY))

        'Binarization
        Dim gr = ToGray(raw)
        Dim T = FindThreshold(gr)
        Dim bin = Binarization(gr, T)

        For y = 0 To bin.GetUpperBound(0)
            result(y) = 0
            For x = 0 To bin.GetUpperBound(1)
                result(y) += IIf(bin(y, x), 1, 0)
            Next
        Next
        Return result
    End Function

End Class
