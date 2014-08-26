Public Class RectangleWithOffset

    Public Rectangle As Rectangle
    Public XOffset As Integer = 0, YOffset As Integer = 0

    Public Sub New(rect As Rectangle)
        Rectangle = rect
    End Sub

    Public Sub New(rect As Rectangle, xoffset As Integer, yoffset As Integer)
        Rectangle = rect
        Me.XOffset = xoffset
        Me.YOffset = yoffset
    End Sub

    ReadOnly Property NewRectangle As Rectangle
        Get
            Return New Rectangle(Rectangle.X + XOffset, Rectangle.Y + YOffset, Rectangle.Width, Rectangle.Height)
        End Get
    End Property

    ReadOnly Property NewPostion As Point
        Get
            Return New Point(Rectangle.X + XOffset, Rectangle.Y + YOffset)
        End Get
    End Property

End Class
