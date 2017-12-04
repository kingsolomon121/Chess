Public Class Form1
    Dim board(64) As Button
    Dim col As Integer = 0

    Public Sub Colorer()
        For count As Integer = 1 To 64
            If ((count Mod 16) Mod 2 = 0 Xor count Mod 16 > 8) Then
                board(count).BackColor = Color.Empty
            Else
                board(count).BackColor = Color.Maroon
            End If
        Next
        For count As Integer = 16 To 64 Step 16
            board(count).BackColor = Color.Maroon
        Next
    End Sub
    Dim clicked As Boolean = False
    Dim player As Integer = 1
    Dim piece As Integer
    Public Sub CheckSpace(num As Integer)
        If Not (clicked) Then
            If Not (board(num).BackgroundImage Is Nothing) Then
                If (player = 1 And board(num).ForeColor = Color.White) Or ((player = 2 And board(num).ForeColor = Color.Black)) Then
                    clicked = True
                    piece = num
                    Button2.Enabled = True
                End If
            End If
        Else
            clicked = False
            MovePiece(num)
            Button2.Enabled = False
            Colorer()
        End If
    End Sub

    Dim wp As Image = My.Resources.white_pawn
    Dim wr As Image = My.Resources.white_rook
    Dim wn As Image = My.Resources.white_knight
    Dim wb As Image = My.Resources.white_bishop
    Dim wk As Image = My.Resources.white_king
    Dim wq As Image = My.Resources.white_queen

    Dim bp As Image = My.Resources.black_pawn
    Dim br As Image = My.Resources.black_rook
    Dim bn As Image = My.Resources.black_knight
    Dim bb As Image = My.Resources.black_bishop
    Dim bk As Image = My.Resources.black_king
    Dim bq As Image = My.Resources.black_queen
    Dim moveMade() As Integer
    Dim num As Integer

    Public Sub Moved()
        moveMade = {num, piece}
        Button1.Enabled = True
    End Sub

    Public Sub WinKing()
        Dim king As Integer
        For counter As Integer = 1 To 64
            If (board(counter).BackgroundImage Is wk Or board(counter).BackgroundImage Is bk) Then
                king += 1
            End If
        Next
        If (king = 2) Then
            Exit Sub
        End If
        If (player = 1) Then
            MsgBox("Player 2 Wins!!")
            End
        Else
            MsgBox("Player 1 Wins!!")
            End
        End If
    End Sub

    Public Sub PawnQueen()
        For counter As Integer = 1 To 8
            If (board(counter).BackgroundImage Is wp) Then
                board(counter).BackgroundImage = wq
            End If
        Next
        For counter As Integer = 57 To 64
            If (board(counter).BackgroundImage Is bp) Then
                board(counter).BackgroundImage = bq
            End If
        Next
    End Sub

    Public Sub MovePiece(numb As Integer)
        num = numb
        If (player = 1) Then
            If (board(num).BackgroundImage Is Nothing Or board(num).ForeColor = Color.Black) Then
                If (board(piece).BackgroundImage Is wp) Then
                    If (piece - 8 = num And board(num).BackgroundImage Is Nothing) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                        PawnQueen()
                    ElseIf (piece - 9 = num Or piece - 7 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                        PawnQueen()
                    End If

                ElseIf (board(piece).BackgroundImage Is wr) Then
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next

                ElseIf (board(piece).BackgroundImage Is wn) Then
                    If (piece - 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    End If

                ElseIf (board(piece).BackgroundImage Is wb) Then
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next

                ElseIf (board(piece).BackgroundImage Is wq) Then
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.White
                            board(piece).ForeColor = Color.Empty
                            player = 2
                            Label1.Text = "Player 2's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next

                ElseIf (board(piece).BackgroundImage Is wk) Then
                    If (piece - 1 = num) And (Math.Ceiling((piece - 1) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 1 = num) And (Math.Ceiling((piece + 1) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 8 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 8 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 9 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 9 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 7 = num) And Not (Math.Ceiling((piece - 7) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 7 = num) And Not (Math.Ceiling((piece + 7) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.White
                        board(piece).ForeColor = Color.Empty
                        player = 2
                        Label1.Text = "Player 2's Turn"
                        WinKing()
                        Moved()
                    End If
                End If
            End If



        Else
            If (board(num).BackgroundImage Is Nothing Or board(num).ForeColor = Color.White) Then
                If (board(piece).BackgroundImage Is bp) Then
                    If (piece + 8 = num And board(num).BackgroundImage Is Nothing) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                        PawnQueen()
                    ElseIf (piece + 9 = num Or piece + 7 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                        PawnQueen()
                    End If

                ElseIf (board(piece).BackgroundImage Is br) Then
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next

                ElseIf (board(piece).BackgroundImage Is bn) Then
                    If (piece - 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    End If

                ElseIf (board(piece).BackgroundImage Is bb) Then
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next

                ElseIf (board(piece).BackgroundImage Is bq) Then
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            board(num).BackgroundImage = board(piece).BackgroundImage
                            board(piece).BackgroundImage = Nothing
                            board(num).ForeColor = Color.Black
                            board(piece).ForeColor = Color.Empty
                            player = 1
                            Label1.Text = "Player 1's Turn"
                            WinKing()
                            Moved()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next

                ElseIf (board(piece).BackgroundImage Is bk) Then
                    If (piece - 1 = num) And (Math.Ceiling((piece - 1) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 1 = num) And (Math.Ceiling((piece + 1) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 8 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 8 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 9 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 9 = num) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece - 7 = num) And Not (Math.Ceiling((piece - 7) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    ElseIf (piece + 7 = num) And Not (Math.Ceiling((piece + 7) / 8) = Math.Ceiling(piece / 8)) Then
                        board(num).BackgroundImage = board(piece).BackgroundImage
                        board(piece).BackgroundImage = Nothing
                        board(num).ForeColor = Color.Black
                        board(piece).ForeColor = Color.Empty
                        player = 1
                        Label1.Text = "Player 1's Turn"
                        WinKing()
                        Moved()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 1 To 64

            board(i) = New Button
            Me.Controls.Add(board(i))
            board(i).Size = New System.Drawing.Size(64, 55)
            board(i).Text = ""
            board(i).BackgroundImageLayout = ImageLayout.Stretch
            board(i).Tag = i
            AddHandler board(i).Click, AddressOf Me.Button_Click
        Next i

        For i = 1 To 8
            board(i).Location = New Point(64 * (i - 1), 0)
        Next i

        For i = 9 To 16
            board(i).Location = New Point(64 * (i - 9), 55)
            board(i).BackgroundImage = bp
            board(i).ForeColor = Color.Black
        Next i

        For i = 17 To 24
            board(i).Location = New Point(64 * (i - 17), 110)
        Next i

        For i = 25 To 32
            board(i).Location = New Point(64 * (i - 25), 165)
        Next i

        For i = 33 To 40
            board(i).Location = New Point(64 * (i - 33), 220)
        Next i

        For i = 41 To 48
            board(i).Location = New Point(64 * (i - 41), 275)
        Next i

        For i = 49 To 56
            board(i).Location = New Point(64 * (i - 49), 330)
            board(i).BackgroundImage = wp
            board(i).ForeColor = Color.White
        Next i

        For i = 57 To 64
            board(i).Location = New Point(64 * (i - 57), 385)
        Next i

        Colorer()

        board(1).BackgroundImage = br
        board(8).BackgroundImage = br
        board(2).BackgroundImage = bn
        board(7).BackgroundImage = bn
        board(3).BackgroundImage = bb
        board(6).BackgroundImage = bb
        board(4).BackgroundImage = bk
        board(5).BackgroundImage = bq
        board(1).ForeColor = Color.Black
        board(8).ForeColor = Color.Black
        board(2).ForeColor = Color.Black
        board(7).ForeColor = Color.Black
        board(3).ForeColor = Color.Black
        board(6).ForeColor = Color.Black
        board(4).ForeColor = Color.Black
        board(5).ForeColor = Color.Black

        board(57).BackgroundImage = wr
        board(64).BackgroundImage = wr
        board(63).BackgroundImage = wn
        board(58).BackgroundImage = wn
        board(59).BackgroundImage = wb
        board(62).BackgroundImage = wb
        board(60).BackgroundImage = wk
        board(61).BackgroundImage = wq
        board(57).ForeColor = Color.White
        board(58).ForeColor = Color.White
        board(59).ForeColor = Color.White
        board(60).ForeColor = Color.White
        board(61).ForeColor = Color.White
        board(62).ForeColor = Color.White
        board(63).ForeColor = Color.White
        board(64).ForeColor = Color.White
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs)
        CheckSpace(CType(CType(sender,
    System.Windows.Forms.Button).Tag, Integer))
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (player = 1) Then
            board(moveMade(1)).BackgroundImage = board(moveMade(0)).BackgroundImage
            board(moveMade(0)).BackgroundImage = Nothing
            board(moveMade(0)).ForeColor = Color.Empty
            board(moveMade(1)).ForeColor = Color.Black
            player = 2
            Label1.Text = "Player 2's Turn"
            Button1.Enabled = False
        Else
            board(moveMade(1)).BackgroundImage = board(moveMade(0)).BackgroundImage
            board(moveMade(0)).BackgroundImage = Nothing
            board(moveMade(0)).ForeColor = Color.Empty
            board(moveMade(1)).ForeColor = Color.White
            player = 1
            Label1.Text = "Player 1's Turn"
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim startR As Integer = (piece + 1) - (piece Mod 8)
        Dim i As Integer

        If (board(piece).BackgroundImage Is wp) Then
            If (piece - 8 > 0) And (board(piece - 8).BackgroundImage Is Nothing) Then
                board(piece - 8).BackColor = Color.Yellow
            End If
            If (piece - 7 > 0) And (board(piece - 7).ForeColor = Color.Black) Then
                board(piece - 7).BackColor = Color.Yellow
            End If
            If (piece - 9 > 0) And (board(piece - 9).ForeColor = Color.Black) Then
                board(piece - 9).BackColor = Color.Yellow
            End If


        ElseIf (board(piece).BackgroundImage Is bp) Then
            If (piece + 8 < 65) And (board(piece + 8).BackgroundImage Is Nothing) Then
                board(piece + 8).BackColor = Color.Yellow
            End If
            If (piece + 7 < 65) And (board(piece + 7).ForeColor = Color.White) Then
                board(piece + 7).BackColor = Color.Yellow
            End If
            If (piece + 9 < 65) And (board(piece + 9).ForeColor = Color.White) Then
                board(piece + 9).BackColor = Color.Yellow
            End If


        ElseIf (board(piece).BackgroundImage Is wr) Then
            For i = 1 To 7
                If (piece - i * 8 > 0) Then
                    If (board(piece - 8 * i).BackgroundImage Is Nothing) Then
                        board(piece - 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 8 * i).ForeColor = Color.Black) Then
                            board(piece - 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + i * 8 < 65) Then
                    If (board(piece + 8 * i).BackgroundImage Is Nothing) Then
                        board(piece + 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 8 * i).ForeColor = Color.Black) Then
                            board(piece + 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + 1 * i < 65) Then
                    If ((piece + 1 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 1 * i).BackgroundImage Is Nothing) Then
                        board(piece + 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 1 * i).ForeColor = Color.Black) Then
                            board(piece + 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece - 1 * i > 0) Then
                    If ((piece - 1 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 1 * i).BackgroundImage Is Nothing) Then
                        board(piece - 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 1 * i).ForeColor = Color.Black) Then
                            board(piece - 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i


        ElseIf (board(piece).BackgroundImage Is br) Then
            For i = 1 To 7
                If (piece - i * 8 > 0) Then
                    If (board(piece - 8 * i).BackgroundImage Is Nothing) Then
                        board(piece - 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 8 * i).ForeColor = Color.White) Then
                            board(piece - 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + i * 8 < 65) Then
                    If (board(piece + 8 * i).BackgroundImage Is Nothing) Then
                        board(piece + 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 8 * i).ForeColor = Color.White) Then
                            board(piece + 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + 1 * i < 65) Then
                    If ((piece + 1 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 1 * i).BackgroundImage Is Nothing) Then
                        board(piece + 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 1 * i).ForeColor = Color.White) Then
                            board(piece + 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece - 1 * i > 0) Then
                    If ((piece - 1 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 1 * i).BackgroundImage Is Nothing) Then
                        board(piece - 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 1 * i).ForeColor = Color.White) Then
                            board(piece - 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i


        ElseIf (board(piece).BackgroundImage Is wn) Then
            If (piece - 17 > 0) And (Math.Ceiling((piece - 17) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                If (board(piece - 17).BackgroundImage Is Nothing) Or (board(piece - 17).ForeColor = Color.Black) Then
                    board(piece - 17).BackColor = Color.Yellow
                End If
            End If
            If (piece - 15 > 0) And (Math.Ceiling((piece - 15) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                If (board(piece - 15).BackgroundImage Is Nothing) Or (board(piece - 15).ForeColor = Color.Black) Then
                    board(piece - 15).BackColor = Color.Yellow
                End If
            End If
            If (piece + 17 < 65) And (Math.Ceiling((piece + 17) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                If (board(piece + 17).BackgroundImage Is Nothing) Or (board(piece + 17).ForeColor = Color.Black) Then
                    board(piece + 17).BackColor = Color.Yellow
                End If
            End If
            If (piece + 15 < 65) And (Math.Ceiling((piece + 15) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                If (board(piece + 15).BackgroundImage Is Nothing) Or (board(piece + 15).ForeColor = Color.Black) Then
                    board(piece + 15).BackColor = Color.Yellow
                End If
            End If
            If (piece - 10 > 0) And (Math.Ceiling((piece - 10) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                If (board(piece - 10).BackgroundImage Is Nothing) Or (board(piece - 10).ForeColor = Color.Black) Then
                    board(piece - 10).BackColor = Color.Yellow
                End If
            End If
            If (piece - 6 > 0) And (Math.Ceiling((piece - 6) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                If (board(piece - 6).BackgroundImage Is Nothing) Or (board(piece - 6).ForeColor = Color.Black) Then
                    board(piece - 6).BackColor = Color.Yellow
                End If
            End If
            If (piece + 10 < 65) And (Math.Ceiling((piece + 10) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                If (board(piece + 10).BackgroundImage Is Nothing) Or (board(piece + 10).ForeColor = Color.Black) Then
                    board(piece + 10).BackColor = Color.Yellow
                End If
            End If
            If (piece + 6 < 65) And (Math.Ceiling((piece + 6) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                If (board(piece + 6).BackgroundImage Is Nothing) Or (board(piece + 6).ForeColor = Color.Black) Then
                    board(piece + 6).BackColor = Color.Yellow
                End If
            End If


        ElseIf (board(piece).BackgroundImage Is bn) Then
            If (piece - 17 > 0) And (Math.Ceiling((piece - 17) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                If (board(piece - 17).BackgroundImage Is Nothing) Or (board(piece - 17).ForeColor = Color.White) Then
                    board(piece - 17).BackColor = Color.Yellow
                End If
            End If
            If (piece - 15 > 0) And (Math.Ceiling((piece - 15) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                If (board(piece - 15).BackgroundImage Is Nothing) Or (board(piece - 15).ForeColor = Color.White) Then
                    board(piece - 15).BackColor = Color.Yellow
                End If
            End If
            If (piece + 17 < 65) And (Math.Ceiling((piece + 17) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                If (board(piece + 17).BackgroundImage Is Nothing) Or (board(piece + 17).ForeColor = Color.White) Then
                    board(piece + 17).BackColor = Color.Yellow
                End If
            End If
            If (piece + 15 < 65) And (Math.Ceiling((piece + 15) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                If (board(piece + 15).BackgroundImage Is Nothing) Or (board(piece + 15).ForeColor = Color.White) Then
                    board(piece + 15).BackColor = Color.Yellow
                End If
            End If
            If (piece - 10 > 0) And (Math.Ceiling((piece - 10) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                If (board(piece - 10).BackgroundImage Is Nothing) Or (board(piece - 10).ForeColor = Color.White) Then
                    board(piece - 10).BackColor = Color.Yellow
                End If
            End If
            If (piece - 6 > 0) And (Math.Ceiling((piece - 6) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                If (board(piece - 6).BackgroundImage Is Nothing) Or (board(piece - 6).ForeColor = Color.White) Then
                    board(piece - 6).BackColor = Color.Yellow
                End If
            End If
            If (piece + 10 < 65) And (Math.Ceiling((piece + 10) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                If (board(piece + 10).BackgroundImage Is Nothing) Or (board(piece + 10).ForeColor = Color.White) Then
                    board(piece + 10).BackColor = Color.Yellow
                End If
            End If
            If (piece + 6 < 65) And (Math.Ceiling((piece + 6) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                If (board(piece + 6).BackgroundImage Is Nothing) Or (board(piece + 6).ForeColor = Color.White) Then
                    board(piece + 6).BackColor = Color.Yellow
                End If
            End If


        ElseIf (board(piece).BackgroundImage Is wb) Then
            For i = 1 To 7
                If (piece - 7 * i > 0) Then
                    If ((piece - 7 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece - 7 * i).BackgroundImage Is Nothing) Then
                        board(piece - 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 7 * i).ForeColor = Color.Black) Then
                            board(piece - 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece - 9 * i > 0) Then
                    If ((piece - 9 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 9 * i).BackgroundImage Is Nothing) Then
                        board(piece - 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 9 * i).ForeColor = Color.Black) Then
                            board(piece - 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 7 * i < 65) Then
                    If ((piece + 7 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece + 7 * i).BackgroundImage Is Nothing) Then
                        board(piece + 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 7 * i).ForeColor = Color.Black) Then
                            board(piece + 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 9 * i < 65) Then
                    If ((piece + 9 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 9 * i).BackgroundImage Is Nothing) Then
                        board(piece + 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 9 * i).ForeColor = Color.Black) Then
                            board(piece + 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next


        ElseIf (board(piece).BackgroundImage Is bb) Then
            For i = 1 To 7
                If (piece - 7 * i > 0) Then
                    If ((piece - 7 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece - 7 * i).BackgroundImage Is Nothing) Then
                        board(piece - 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 7 * i).ForeColor = Color.White) Then
                            board(piece - 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece - 9 * i > 0) Then
                    If ((piece - 9 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 9 * i).BackgroundImage Is Nothing) Then
                        board(piece - 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 9 * i).ForeColor = Color.White) Then
                            board(piece - 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 7 * i < 65) Then
                    If ((piece + 7 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece + 7 * i).BackgroundImage Is Nothing) Then
                        board(piece + 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 7 * i).ForeColor = Color.White) Then
                            board(piece + 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 9 * i < 65) Then
                    If ((piece + 9 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 9 * i).BackgroundImage Is Nothing) Then
                        board(piece + 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 9 * i).ForeColor = Color.White) Then
                            board(piece + 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next


        ElseIf (board(piece).BackgroundImage Is wq) Then
            For i = 1 To 7
                If (piece - 7 * i > 0) Then
                    If ((piece - 7 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece - 7 * i).BackgroundImage Is Nothing) Then
                        board(piece - 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 7 * i).ForeColor = Color.Black) Then
                            board(piece - 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece - 9 * i > 0) Then
                    If ((piece - 9 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 9 * i).BackgroundImage Is Nothing) Then
                        board(piece - 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 9 * i).ForeColor = Color.Black) Then
                            board(piece - 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 7 * i < 65) Then
                    If ((piece + 7 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece + 7 * i).BackgroundImage Is Nothing) Then
                        board(piece + 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 7 * i).ForeColor = Color.Black) Then
                            board(piece + 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 9 * i < 65) Then
                    If ((piece + 9 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 9 * i).BackgroundImage Is Nothing) Then
                        board(piece + 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 9 * i).ForeColor = Color.Black) Then
                            board(piece + 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece - i * 8 > 0) Then
                    If (board(piece - 8 * i).BackgroundImage Is Nothing) Then
                        board(piece - 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 8 * i).ForeColor = Color.Black) Then
                            board(piece - 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + i * 8 < 65) Then
                    If (board(piece + 8 * i).BackgroundImage Is Nothing) Then
                        board(piece + 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 8 * i).ForeColor = Color.Black) Then
                            board(piece + 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + 1 * i < 65) Then
                    If ((piece + 1 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 1 * i).BackgroundImage Is Nothing) Then
                        board(piece + 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 1 * i).ForeColor = Color.Black) Then
                            board(piece + 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece - 1 * i > 0) Then
                    If ((piece - 1 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 1 * i).BackgroundImage Is Nothing) Then
                        board(piece - 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 1 * i).ForeColor = Color.Black) Then
                            board(piece - 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i


        ElseIf (board(piece).BackgroundImage Is bq) Then
            For i = 1 To 7
                If (piece - 7 * i > 0) Then
                    If ((piece - 7 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece - 7 * i).BackgroundImage Is Nothing) Then
                        board(piece - 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 7 * i).ForeColor = Color.White) Then
                            board(piece - 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece - 9 * i > 0) Then
                    If ((piece - 9 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 9 * i).BackgroundImage Is Nothing) Then
                        board(piece - 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 9 * i).ForeColor = Color.White) Then
                            board(piece - 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 7 * i < 65) Then
                    If ((piece + 7 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece + 7 * i).BackgroundImage Is Nothing) Then
                        board(piece + 7 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 7 * i).ForeColor = Color.White) Then
                            board(piece + 7 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece + 9 * i < 65) Then
                    If ((piece + 9 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 9 * i).BackgroundImage Is Nothing) Then
                        board(piece + 9 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 9 * i).ForeColor = Color.White) Then
                            board(piece + 9 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next
            For i = 1 To 7
                If (piece - i * 8 > 0) Then
                    If (board(piece - 8 * i).BackgroundImage Is Nothing) Then
                        board(piece - 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 8 * i).ForeColor = Color.White) Then
                            board(piece - 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + i * 8 < 65) Then
                    If (board(piece + 8 * i).BackgroundImage Is Nothing) Then
                        board(piece + 8 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 8 * i).ForeColor = Color.White) Then
                            board(piece + 8 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece + 1 * i < 65) Then
                    If ((piece + 1 * i) Mod 8 = 1) Then
                        Exit For
                    End If
                    If (board(piece + 1 * i).BackgroundImage Is Nothing) Then
                        board(piece + 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece + 1 * i).ForeColor = Color.White) Then
                            board(piece + 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i
            For i = 1 To 7
                If (piece - 1 * i > 0) Then
                    If ((piece - 1 * i) Mod 8 = 0) Then
                        Exit For
                    End If
                    If (board(piece - 1 * i).BackgroundImage Is Nothing) Then
                        board(piece - 1 * i).BackColor = Color.Yellow
                    Else
                        If (board(piece - 1 * i).ForeColor = Color.White) Then
                            board(piece - 1 * i).BackColor = Color.Yellow
                        End If
                        Exit For
                    End If
                End If
            Next i


        ElseIf (board(piece).BackgroundImage Is wk) Then
            If (Math.Ceiling((piece - 1) / 8) = Math.Ceiling(piece / 8)) Then
                If (board(piece - 1).BackgroundImage Is Nothing) Then
                    board(piece - 1).BackColor = Color.Yellow
                ElseIf (board(piece - 1).ForeColor = Color.Black) Then
                    board(piece - 1).BackColor = Color.Yellow
                End If
            End If
            If (Math.Ceiling((piece + 1) / 8) = Math.Ceiling(piece / 8)) Then
                If (board(piece + 1).BackgroundImage Is Nothing) Then
                    board(piece + 1).BackColor = Color.Yellow
                ElseIf (board(piece + 1).ForeColor = Color.Black) Then
                    board(piece + 1).BackColor = Color.Yellow
                End If
            End If
            If (piece - 8 > 0) Then
                If (board(piece - 8).BackgroundImage Is Nothing) Then
                    board(piece - 8).BackColor = Color.Yellow
                ElseIf (board(piece - 8).ForeColor = Color.Black) Then
                    board(piece - 8).BackColor = Color.Yellow
                End If
            End If
            If (piece + 8 < 65) Then
                If (board(piece + 8).BackgroundImage Is Nothing) Then
                    board(piece + 8).BackColor = Color.Yellow
                ElseIf (board(piece + 8).ForeColor = Color.Black) Then
                    board(piece + 8).BackColor = Color.Yellow
                End If
            End If
            If (Math.Ceiling((piece - 9) / 8) = Math.Ceiling((piece - 8) / 8)) And (piece - 9 > 0) Then
                If (board(piece - 9).BackgroundImage Is Nothing) Then
                    board(piece - 9).BackColor = Color.Yellow
                ElseIf (board(piece - 9).ForeColor = Color.Black) Then
                    board(piece - 9).BackColor = Color.Yellow
                End If
            End If
            If Not (Math.Ceiling((piece - 7) / 8) = Math.Ceiling((piece) / 8)) And (piece - 7 > 0) Then
                If (board(piece - 7).BackgroundImage Is Nothing) Then
                    board(piece - 7).BackColor = Color.Yellow
                ElseIf (board(piece - 7).ForeColor = Color.Black) Then
                    board(piece - 7).BackColor = Color.Yellow
                End If
            End If
            If (Math.Ceiling((piece + 9) / 8) = Math.Ceiling((piece + 8) / 8)) And (piece + 9 < 65) Then
                If (board(piece + 9).BackgroundImage Is Nothing) Then
                    board(piece + 9).BackColor = Color.Yellow
                ElseIf (board(piece + 9).ForeColor = Color.Black) Then
                    board(piece + 9).BackColor = Color.Yellow
                End If
            End If
            If Not (Math.Ceiling((piece + 7) / 8) = Math.Ceiling((piece) / 8)) And (piece + 7 < 65) Then
                If (board(piece + 7).BackgroundImage Is Nothing) Then
                    board(piece + 7).BackColor = Color.Yellow
                ElseIf (board(piece + 7).ForeColor = Color.Black) Then
                    board(piece + 7).BackColor = Color.Yellow
                End If
            End If


        ElseIf (board(piece).BackgroundImage Is bk) Then
            If (Math.Ceiling((piece - 1) / 8) = Math.Ceiling(piece / 8)) Then
                If (board(piece - 1).BackgroundImage Is Nothing) Then
                    board(piece - 1).BackColor = Color.Yellow
                ElseIf (board(piece - 1).ForeColor = Color.White) Then
                    board(piece - 1).BackColor = Color.Yellow
                End If
            End If
            If (Math.Ceiling((piece + 1) / 8) = Math.Ceiling(piece / 8)) Then
                If (board(piece + 1).BackgroundImage Is Nothing) Then
                    board(piece + 1).BackColor = Color.Yellow
                ElseIf (board(piece + 1).ForeColor = Color.White) Then
                    board(piece + 1).BackColor = Color.Yellow
                End If
            End If
            If (piece - 8 > 0) Then
                If (board(piece - 8).BackgroundImage Is Nothing) Then
                    board(piece - 8).BackColor = Color.Yellow
                ElseIf (board(piece - 8).ForeColor = Color.White) Then
                    board(piece - 8).BackColor = Color.Yellow
                End If
            End If
            If (piece + 8 < 65) Then
                If (board(piece + 8).BackgroundImage Is Nothing) Then
                    board(piece + 8).BackColor = Color.Yellow
                ElseIf (board(piece + 8).ForeColor = Color.White) Then
                    board(piece + 8).BackColor = Color.Yellow
                End If
            End If
            If (Math.Ceiling((piece - 9) / 8) = Math.Ceiling((piece - 8) / 8)) And (piece - 9 > 0) Then
                If (board(piece - 9).BackgroundImage Is Nothing) Then
                    board(piece - 9).BackColor = Color.Yellow
                ElseIf (board(piece - 9).ForeColor = Color.White) Then
                    board(piece - 9).BackColor = Color.Yellow
                End If
            End If
            If Not (Math.Ceiling((piece - 7) / 8) = Math.Ceiling((piece) / 8)) And (piece - 7 > 0) Then
                If (board(piece - 7).BackgroundImage Is Nothing) Then
                    board(piece - 7).BackColor = Color.Yellow
                ElseIf (board(piece - 7).ForeColor = Color.White) Then
                    board(piece - 7).BackColor = Color.Yellow
                End If
            End If
            If (Math.Ceiling((piece + 9) / 8) = Math.Ceiling((piece + 8) / 8)) And (piece + 9 < 65) Then
                If (board(piece + 9).BackgroundImage Is Nothing) Then
                    board(piece + 9).BackColor = Color.Yellow
                ElseIf (board(piece + 9).ForeColor = Color.White) Then
                    board(piece + 9).BackColor = Color.Yellow
                End If
            End If
            If Not (Math.Ceiling((piece + 7) / 8) = Math.Ceiling((piece) / 8)) And (piece + 7 < 65) Then
                If (board(piece + 7).BackgroundImage Is Nothing) Then
                    board(piece + 7).BackColor = Color.Yellow
                ElseIf (board(piece + 7).ForeColor = Color.White) Then
                    board(piece + 7).BackColor = Color.Yellow
                End If
            End If

        End If
    End Sub
End Class