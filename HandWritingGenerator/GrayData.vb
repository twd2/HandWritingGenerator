Public Class GrayData

    Public data As Integer(,)
    Public xDPI, yDPI As Integer

    Public Sub New(width As Integer, height As Integer, xDPI As Integer, yDPI As Integer)
        ReDim data(height - 1, width - 1)
        Me.xDPI = xDPI
        Me.yDPI = yDPI
    End Sub

    Default Public Property raw(y As Integer, x As Integer) As Integer
        Get
            Return data(y, x)
        End Get
        Set(value As Integer)
            data(y, x) = value
        End Set
    End Property

    Public ReadOnly Property Width As Integer
        Get
            Return data.GetUpperBound(1) + 1
        End Get
    End Property

    Public ReadOnly Property Height As Integer
        Get
            Return data.GetUpperBound(0) + 1
        End Get
    End Property

    Public Shared Function FromBitmap(bmp As Bitmap) As GrayData
        Dim rd = RawData.FromBitmap(bmp)
        Return FromRawData(rd)
    End Function

    Public Shared Function FromRawData(rd As RawData) As GrayData
        Dim gr As New GrayData(rd.Width, rd.Height, rd.xDPI, rd.yDPI)
        For y = 0 To rd.Height - 1
            For x = 0 To rd.Width - 1
                gr(y, x) = ImageProcessor.Gray(rd(y, x, 0), rd(y, x, 1), rd(y, x, 2))
            Next
        Next
        Return gr
    End Function

    Public Function ToBitmap() As Bitmap
        Dim rd = RawData.Create(Width, Height, xDPI, yDPI)
        For y = 0 To rd.Height - 1
            For x = 0 To rd.Width - 1
                Dim a = data(y, x)
                rd(y, x, 0) = a
                rd(y, x, 1) = a
                rd(y, x, 2) = a
            Next
        Next
        Return rd.ToBitmap()
    End Function

End Class
