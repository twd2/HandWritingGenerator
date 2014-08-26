Public Class Block
    Implements IComparable(Of Block)

    Public Left As Integer = Integer.MaxValue
    Public Right As Integer = 0
    Public Top As Integer = Integer.MaxValue
    Public Bottom As Integer = 0
    Public dots As New List(Of Point)
    Private _centre As Point = Nothing

    Public Sub Add(x As Integer, y As Integer)
        dots.Add(New Point(x, y))
        If x < Left Then
            Left = x
        End If
        If x > Right Then
            Right = x
        End If
        If y < Top Then
            Top = y
        End If
        If y > Bottom Then
            Bottom = y
        End If
    End Sub

    ReadOnly Property Acreage As Integer
        Get
            Return dots.Count
        End Get
    End Property

    ReadOnly Property Width As Integer
        Get
            Return Right - Left + 1
        End Get
    End Property

    ReadOnly Property Height As Integer
        Get
            Return Bottom - Top + 1
        End Get
    End Property

    ReadOnly Property MaxAcreage As Integer
        Get
            Return Width * Height
        End Get
    End Property

    ReadOnly Property Centre As Point
        Get
            If _centre = Nothing Then
                Dim sumX = 0, sumY = 0
                For Each dot In dots
                    sumX += dot.X
                    sumY += dot.Y
                Next
                _centre = New Point(sumX / Acreage, sumY / Acreage)
            End If
            Return _centre
        End Get
    End Property

    Public Function CompareTo(other As Block) As Integer Implements IComparable(Of Block).CompareTo
        Return Acreage - other.Acreage
    End Function
End Class
