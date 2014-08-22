Imports System.Runtime.InteropServices

Public Class ImageData

    Public Width, Height, Stride As Integer
    Public bmpdata As Byte()

    Public Function Size() As Integer
        Return bmpdata.Length
    End Function

    Default Public Property raw(Index As Integer) As Byte
        Get
            Return bmpdata(Index)
        End Get
        Set(ByVal Value As Byte)
            bmpdata(Index) = Value
        End Set
    End Property

    Default Public Property raw(y As Integer, x As Integer, n As Integer) As Byte
        Get
            Return bmpdata(Stride * y + 3 * x + n)
        End Get
        Set(value As Byte)
            bmpdata(Stride * y + 3 * x + n) = value
        End Set
    End Property

    Public Shared Function FromBitmap(bmp As Bitmap, rect As Rectangle) As ImageData
        Using tmpbmp = ImageProcessor.CutImg(bmp, rect)
            Dim id As New ImageData
            id.Width = rect.Width
            id.Height = rect.Height
            Dim bmpdata = tmpbmp.LockBits(New Rectangle(0, 0, tmpbmp.Width, tmpbmp.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format24bppRgb)
            id.Stride = bmpdata.Stride
            Dim bmpsize = bmpdata.Stride * bmpdata.Height
            ReDim id.bmpdata(bmpsize - 1)
            Marshal.Copy(bmpdata.Scan0, id.bmpdata, 0, id.bmpdata.Length)
            tmpbmp.UnlockBits(bmpdata)
            Return id
        End Using
    End Function

    Public Shared Function FromBitmap(bmp As Bitmap) As ImageData
        Return FromBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
    End Function

End Class
