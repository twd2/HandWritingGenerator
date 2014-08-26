Public Enum CharRegionType
    [Char]
    Space
End Enum

Public Class CharRegion

    Public deleted As Boolean = False
    Public LeftOffset = 0, RightOffset = 0
    Public Type As CharRegionType

    Public Function Length() As Integer
        Return RightOffset - LeftOffset + 1
    End Function

    Public Sub Combine(b As CharRegion)
        If RightOffset + 1 <> b.LeftOffset Then
            Throw New Exception("Cannot combine")
        End If
        RightOffset = b.RightOffset
        b.deleted = True
    End Sub

    Public Shared Function Read(blackcount As Integer(), start As Integer, Optional T As Integer = 0) As CharRegion
        Dim cr As New CharRegion
        Dim counting = False
        If blackcount(start) >= T Then
            cr.Type = CharRegionType.Char
            For i = start To blackcount.Length - 1
                If Not counting AndAlso blackcount(i) >= T Then 'Char
                    counting = True
                    cr.LeftOffset = i
                End If
                If counting Then
                    If blackcount(i) < T Then
                        cr.RightOffset = i - 1
                        Exit For
                    End If
                End If
            Next
        Else
            cr.Type = CharRegionType.Space
            For i = start To blackcount.Length - 1
                If Not counting AndAlso blackcount(i) < T Then 'Space
                    counting = True
                    cr.LeftOffset = i
                End If
                If counting Then
                    If blackcount(i) >= T Then
                        cr.RightOffset = i - 1
                        Exit For
                    End If
                End If
            Next
        End If
        If cr.RightOffset <= 0 Then
            cr.RightOffset = cr.LeftOffset
        End If
        Return cr
    End Function

End Class
