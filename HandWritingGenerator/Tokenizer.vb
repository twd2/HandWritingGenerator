Imports System.IO

Public Class Tokenizer

    Private _Str As String
    'Private _Tokens As List(Of String) = Nothing
    Private _TokenStack As Stack(Of String) = Nothing
    Private _LastTake As String = Nothing
    Public Const WordChar As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
    Public Const SplitChar As String = " `~!@#$%^&*()_+-={}|[]\:"";'<>?,./" + vbCrLf
    Public Const WithSpaceChar As String = ",.?!;:"
    Public Const SpaceChar As Char = " "c
    Public Const Newline As String = vbCrLf
    Public Const WordCat As Char = "-"c

    Public Sub New(str As String)
        _Str = str
    End Sub

    Public Function HaveNext() As Boolean
        If _TokenStack Is Nothing Then
            Scan()
        End If
        Return _TokenStack.Count > 0
    End Function

    Public Function Take() As String
        If _TokenStack Is Nothing Then
            Scan()
        End If
        _LastTake = _TokenStack.Pop()
        Return _LastTake
        'Dim ret = _Tokens(_i)
        '_i += 1
        'Return ret
    End Function

    Public Sub Untake()
        If _LastTake Is Nothing Then
            Throw New Exception("Cannot untake")
        End If
        _TokenStack.Push(_LastTake)
        '_i -= 1
        'If _i < 0 Then
        '    _i = 0
        'End If
    End Sub

    Public Sub Push(s As String)
        _TokenStack.Push(s)
    End Sub

    Public Sub Scan()
        _TokenStack = New Stack(Of String)
        Dim Tokens As New List(Of String)

        Dim currString = ""
        For i = 0 To _Str.Length - 1
            Dim currChar = _Str(i)
            Dim isWord = WordChar.IndexOf(currChar) >= 0
            If isWord Then
                currString += currChar
            Else
                If currString <> "" Then
                    Tokens.Add(currString)
                End If
                Tokens.Add(currChar)
                currString = ""
            End If
        Next
        If currString <> "" Then
            Tokens.Add(currString)
        End If
        '_Tokens.RemoveAll(Function(s As String)
        '                      Return s.Trim() = ""
        '                  End Function)

        For i = Tokens.Count - 1 To 0 Step -1
            _TokenStack.Push(Tokens(i))
        Next
    End Sub

End Class
