Imports System.Runtime.InteropServices

Public Class RawData

    Public Width, Height, Stride As Integer
    Public xDPI, yDPI As Integer
    Public data As Byte()

    Public Function Size() As Integer
        Return data.Length
    End Function

    Default Public Property raw(Index As Integer) As Byte
        Get
            Return data(Index)
        End Get
        Set(ByVal Value As Byte)
            data(Index) = Value
        End Set
    End Property

    Default Public Property raw(y As Integer, x As Integer, n As Integer) As Byte
        Get
            Return data(Stride * y + 3 * x + n)
        End Get
        Set(value As Byte)
            data(Stride * y + 3 * x + n) = value
        End Set
    End Property

    Public Shared Function FromBitmap(bmp As Bitmap, rect As Rectangle) As RawData
        Using tmpbmp = ImageProcessor.CutImage(bmp, rect)
            Dim rd As New RawData
            rd.Width = rect.Width
            rd.Height = rect.Height
            rd.xDPI = tmpbmp.HorizontalResolution
            rd.yDPI = tmpbmp.VerticalResolution
            Dim bmpdata = tmpbmp.LockBits(New Rectangle(0, 0, tmpbmp.Width, tmpbmp.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format24bppRgb)
            rd.Stride = bmpdata.Stride
            Dim bmpsize = bmpdata.Stride * bmpdata.Height
            ReDim rd.data(bmpsize - 1)
            Marshal.Copy(bmpdata.Scan0, rd.data, 0, rd.data.Length)
            tmpbmp.UnlockBits(bmpdata)
            Return rd
        End Using
    End Function

    Public Shared Function FromBitmap(bmp As Bitmap) As RawData
        Return FromBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
    End Function

    Public Shared Function Create(Width As Integer, Height As Integer, xDPI As Integer, yDPI As Integer) As RawData
        Dim bmp As New Bitmap(Width, Height)
        bmp.SetResolution(xDPI, yDPI)
        Return FromBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
    End Function

    Public Function ToBitmap() As Bitmap
        Dim bmp As New Bitmap(Width, Height)
        bmp.SetResolution(xDPI, yDPI)
        Dim bmpdata = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.WriteOnly, Imaging.PixelFormat.Format24bppRgb)
        Marshal.Copy(data, 0, bmpdata.Scan0, Size())
        bmp.UnlockBits(bmpdata)
        Return bmp
    End Function

End Class
