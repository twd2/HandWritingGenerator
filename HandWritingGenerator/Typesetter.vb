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

    Private Function SubToken(token As String, maxwidth As Integer) As String
        If maxwidth >= GetTokenWidth(token) Then
            Return token
        End If
        If maxwidth < 0 Then
            Throw New ArgumentException("maxwidth")
        End If
        If maxwidth = 0 Then
            Return ""
        End If
        Dim width = 0
        Dim st = ""
        For i = 0 To token.Length - 1
            If width + GetCharWidth(token(i)) <= maxwidth Then
                width += GetCharWidth(token(i))
                st += token(i)
            Else
                Exit For
            End If
        Next
        Return st
    End Function

    Public Sub Typeset(PunctuationWithSpace As Boolean)
        If PageWidth < _Template.MaxWidth + GetCharWidth(Tokenizer.WordCat) Then
            Throw New Exception("PageWidth too small")
        End If
        _sb = New StringBuilder()
        Dim currentLineWidth = 0
        Do While _Tokens.HaveNext()
            Dim token = _Tokens.Take()
            Dim tokenWidth = GetTokenWidth(token)
            Dim tryAppend As Boolean = False
            If token.Length = 1 AndAlso Tokenizer.SplitChar.IndexOf(token(0)) >= 0 Then 'split token
                If token = Tokenizer.SpaceChar.ToString() Then '空格直接添加
                    _sb.Append(token)
                    currentLineWidth += tokenWidth
                Else
                    If Tokenizer.WithSpaceChar.IndexOf(token(0)) >= 0 Then '后面需要一个空格
                        _sb.Append(token) '直接添加
                        currentLineWidth += tokenWidth
                        If PunctuationWithSpace Then '后面跟空格的字符自动添加空格
                            Dim haveSpace = False
                            If _Tokens.HaveNext() Then
                                haveSpace = (_Tokens.Take() = Tokenizer.SpaceChar.ToString())
                                _Tokens.Untake()
                            End If

                            If Not haveSpace Then
                                _sb.Append(Tokenizer.SpaceChar)
                                currentLineWidth += tokenWidth
                            End If
                        End If
                    Else
                        If Tokenizer.Newline.IndexOf(token(0)) >= 0 Then '换行符
                            currentLineWidth = 0
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
                If currentLineWidth + tokenWidth <= PageWidth Then '还可以添加
                    _sb.Append(token)
                    currentLineWidth += tokenWidth
                ElseIf tokenWidth > PageWidth Then '超长单词
                    Dim WordCatWidth = GetCharWidth(Tokenizer.WordCat)
                    '把当前行空下的用完，然后再用下一行
                    Dim CutMaxWidth = Math.Max(WordCatWidth, PageWidth - currentLineWidth)
                    Dim tokens As New List(Of String)
                    '切短
                    Do While GetTokenWidth(token) > CutMaxWidth
                        Dim st = SubToken(token, CutMaxWidth - WordCatWidth)
                        If st <> "" Then
                            tokens.Add(st + Tokenizer.WordCat)
                            token = token.Substring(st.Length)
                        End If
                        CutMaxWidth = PageWidth
                    Loop
                    If token <> "" Then '还剩下
                        tokens.Add(token)
                    Else '恰好切完
                        Dim last = tokens(tokens.Count - 1)
                        tokens(tokens.Count - 1) = last.Substring(0, last.Length)
                    End If
                    For i = tokens.Count - 1 To 0 Step -1
                        _Tokens.Push(tokens(i))
                    Next
                Else '该行满了
                    _sb.AppendLine() '新行
                    currentLineWidth = 0
                    _Tokens.Untake() '恢复
                End If
            End If
        Loop
        _sb.AppendLine() '新行
        currentLineWidth = 0
    End Sub

    Public Shadows Function ToString() As String
        Return _sb.ToString()
    End Function

End Class
