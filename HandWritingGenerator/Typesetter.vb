Imports System.Text

Public Class Typesetter

    Private _Tokens As Tokenizer
    Private _CharInfo As Dictionary(Of Char, CharInfo)
    Private _Template As CharTemplate
    Public PageWidth As Integer = 0
    'Public PageMaxLineCount As Integer = 0
    Private _sb As StringBuilder = Nothing

    Public Sub New(tokens As Tokenizer, temp As CharTemplate)
        _Tokens = tokens
        _Template = temp
    End Sub

    Public Function GetCharWidth(ch As Char) As Integer
        If _Template.charMap.ContainsKey(ch) Then
            Return _Template.charMap(ch).Width
        Else
            Return 0
        End If
    End Function

    Public Function GetTokenWidth(token As String) As Integer
        Dim width = 0
        For i = 0 To token.Length - 1
            width += GetCharWidth(token(i))
        Next
        Return width
    End Function

    Public Sub Typeset(PunctuationWithSpace As Boolean)
        _sb = New StringBuilder()
        Dim currLineWidth = 0
        Do While _Tokens.HaveNext()
            Dim token = _Tokens.Take()
            Dim tryAppend As Boolean = False
            If token.Length = 1 AndAlso Tokenizer.SplitChar.IndexOf(token(0)) >= 0 Then 'split token
                If token = Tokenizer.SpaceChar.ToString() Then '空格直接添加
                    _sb.Append(token)
                    currLineWidth += GetTokenWidth(token)
                Else
                    If Tokenizer.WithSpaceChar.IndexOf(token(0)) >= 0 Then '后面需要一个空格
                        _sb.Append(token) '直接添加
                        currLineWidth += GetTokenWidth(token)
                        If PunctuationWithSpace Then '后面跟空格的字符自动添加空格
                            Dim haveSpace = False
                            If _Tokens.HaveNext() Then
                                haveSpace = (_Tokens.Take() = Tokenizer.SpaceChar.ToString())
                                _Tokens.Untake()
                            End If

                            If Not haveSpace Then
                                _sb.Append(Tokenizer.SpaceChar)
                                currLineWidth += GetTokenWidth(token)
                            End If
                        End If
                    Else
                        If Tokenizer.Newline.IndexOf(token(0)) >= 0 Then '换行符
                            currLineWidth = 0
                            _sb.Append(token)
                        Else
                            tryAppend = True
                        End If
                    End If
                End If
            Else
                tryAppend = True
            End If
            If tryAppend Then
                If currLineWidth + GetTokenWidth(token) <= PageWidth Then
                    _sb.Append(token)
                    currLineWidth += GetTokenWidth(token)
                ElseIf GetTokenWidth(token) > PageWidth Then
                    Throw New Exception("WOW such token so long")
                Else
                    '该行满了
                    _sb.AppendLine() '新行
                    currLineWidth = 0
                    _Tokens.Untake() '恢复
                End If
            End If
            ''该行满了
            'If currLineWidth >= PageWidth Then
            '    _sb.AppendLine()
            '    currLineWidth = 0
            'End If
        Loop
        _sb.AppendLine() '新行
        currLineWidth = 0
    End Sub

    Public Shadows Function ToString() As String
        Return _sb.ToString()
    End Function

End Class
