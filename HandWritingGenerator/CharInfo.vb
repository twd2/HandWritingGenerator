Public Class CharInfo
    Public c As Char
    Public rect As Rectangle
    Public img As Image = Nothing

    Public Sub New(c As Char, rect As Rectangle)
        Me.c = c
        Me.rect = rect
        'Me.img = CutImg(mainimg, rect)
    End Sub

    Public Sub New(c As Char, x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer)
        Me.c = c
        Me.rect = New Rectangle(Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x2 - x1), Math.Abs(y2 - y1))
        'Me.img = CutImg(mainimg, rect)
    End Sub

    Private Function CutImg(src As Image, rect As Rectangle) As Image
        Dim result As New Bitmap(rect.Width, rect.Height)
        Using g = Graphics.FromImage(result)
            g.DrawImage(src, -rect.X, -rect.Y)
        End Using
        Return result
    End Function

    Public Sub MakeCache(mainimg As Image)
        img = CutImg(mainimg, rect)
    End Sub

End Class
