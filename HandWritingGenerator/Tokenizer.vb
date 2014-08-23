Imports System.IO

Public Class Tokenizer

    Private _Str As String
    Private _Tokens As List(Of String) = Nothing
    Private _i As Integer = 0
    Public Const WordChar As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
    Public Const SplitChar As String = " `~!@#$%^&*()_+-={}|[]\:"";'<>?,./" + vbCrLf
    Public Const WithSpaceChar As String = ",.?!;:"
    Public Const SpaceChar As Char = " "c
    Public Const Newline As String = vbCrLf

    Public Sub New(str As String)
        _Str = str
    End Sub

    Public Function HaveNext() As Boolean
        If _Tokens Is Nothing Then
            Scan()
        End If
        Return _i <= _Tokens.Count - 1
    End Function

    Public Function Take() As String
        If _Tokens Is Nothing Then
            Scan()
        End If
        Dim ret = _Tokens(_i)
        _i += 1
        Return ret
    End Function

    Public Sub Untake()
        _i -= 1
        If _i < 0 Then
            _i = 0
        End If
    End Sub

    Public Sub Scan()
        _Tokens = New List(Of String)
        _i = 0
        Dim currString = ""
        For i = 0 To _Str.Length - 1
            Dim currChar = _Str(i)
            Dim isWord = WordChar.IndexOf(currChar) >= 0
            If isWord Then
                currString += currChar
            Else
                If currString <> "" Then
                    _Tokens.Add(currString)
                End If
                _Tokens.Add(currChar)
                currString = ""
                End If
        Next
        If currString <> "" Then
            _Tokens.Add(currString)
        End If
        '_Tokens.RemoveAll(Function(s As String)
        '                      Return s.Trim() = ""
        '                  End Function)
    End Sub

End Class
