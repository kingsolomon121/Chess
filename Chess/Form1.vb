'Spencer Solomon
'Computer Science 1 Mods 15-16
'12/7/17
'Chess Program

Public Class Form1
    'Array of buttons for all the board spaces
    Dim board(64) As Button
    'color variable to decide if the board starts with a white of black space
    Dim col As Integer = 0
    'Used to decide if a piece is currently selected
    Dim clicked As Boolean = False
    'Used to store whos turn it is
    Dim player As Integer = 1
    'Used to store the space number of the currently selected piece
    Dim piece As Integer
    'Used to store the space number of where the player desires to move
    Dim num As Integer
    'Stores all of the images
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
    'Array which stores the "piece" and "num" variables whenever a move is made
    Dim moveMade(2) As Integer
    'Stores the image of the piece that was taken (or the blank space that was "taken") after 
    'every move
    Dim pict As Image
    'Array which stores how many times each white piece has been moved
    Dim white(6) As Integer
    'Array which stores how many times each black piece has been moved
    Dim black(6) As Integer
    'Color of the default color of whatever space is currently being selected
    Dim coler As Color
    'Stores how many secs the game has been going on
    Dim sec As Integer
    'Stores how many minutes the game has been going on
    Dim min As Integer
    'Stores the name of the first player
    Dim firstplayer As String
    'Stores the name of the second player
    Dim secplayer As String
    'Stores the total number of moves that have been made
    Dim moves As Integer = 0
    Dim length As Integer = 64

    'Creates All the Buttons and assigns them with the correct event handlers, images and forecolors
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 1 To 64

            board(i) = New Button
            Me.Controls.Add(board(i))
            board(i).Size = New System.Drawing.Size(64, 55)
            board(i).Text = ""
            board(i).BackgroundImageLayout = ImageLayout.Stretch
            board(i).Tag = i
            AddHandler board(i).Click, AddressOf Me.Button_Click
            AddHandler board(i).MouseEnter, AddressOf Me.MouseEnterShadows
            AddHandler board(i).MouseLeave, AddressOf Me.MouseLeaveShadows
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

        firstplayer = InputBox("What is Player 1's name?")
        secplayer = InputBox("What is Player 2's name?")

        MsgBox("This program abides by all of the rules of chess, with the exclusion of en passant. " & firstplayer & " will play white and " & secplayer & " will play black.")
        Label1.Text = firstplayer & "'s Turn!"

        Pawnval = {0, 0, 0, 0, 0, 0, 0, 0, 5, 10, 10, -20, -20, 10, 10, 5, 5, -5, -10, 0, 0, -10, -5, 5, 0, 0, 0, 20, 20, 0, 0, 0, 5, 5, 10, 25, 25, 10, 5, 5, 10, 10, 20, 30, 30, 20, 10, 10, 50, 50, 50, 50, 50, 50, 50, 50, 90, 90, 90, 90, 90, 90, 90, 90}
        Knightval = {-50, -40, -30, -30, -30, -30, -40, -50, -40, -20, 0, 5, 5, 0, -20, -40, -30, 5, 10, 15, 15, 10, 5, -30, -30, 0, 15, 20, 20, 15, 0, -30, -30, 5, 15, 20, 20, 15, 5, -30, -30, 0, 10, 15, 15, 10, 0, -30, -40, -20, 0, 0, 0, 0, -20, -40, -50, -40, -30, -30, -30, -30, -40, -50}
        Bishopval = {-20, -10, -10, -10, -10, -10, -10, -20, -10, 0, 0, 0, 0, 0, 0, -10, -10, 0, 5, 10, 10, 5, 0, -10, -10, 5, 5, 10, 10, 5, 5, -10, -10, 0, 10, 10, 10, 10, 0, -10, -10, 10, 10, 10, 10, 10, 10, -10, -10, 5, 0, 0, 0, 0, 5, -10, -20, -10, -10, -10, -10, -10, -10, -20}
        Rookval = {0, 0.5, 0, 5, 6, 0, 0.5, 0, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, 5, 10, 10, 10, 10, 10, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0}
        Queenval = {-20, -10, -10, -5, -5, -10, -10, -20, -10, 0, 5, 0, 0, 0, 0, -10, -10, 5, 5, 5, 5, 5, 0, -10, 0, 0, 5, 5, 5, 5, 0, -5, -5, 0, 5, 5, 5, 5, 0, -5, -10, 0, 5, 5, 5, 5, 0, -10, -10, 0, 0, 0, 0, 0, 0, -10, -20, -10, -10, -5, -5, -10, -10, -20}
        Kingval = {20, 30, 10, 0, 0, 10, 30, 20, 20, 20, 0, 0, 0, 0, 20, 20, -10, -20, -20, -20, -20, -20, -20, -10, -30, -40, -40, -50, -50, -40, -40, -30, -20, -30, -30, -40, -40, -30, -30, -20, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30}
    End Sub

    'Runs whenever a square on the board is clicked
    Private Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        CheckSpace(CType(CType(sender, System.Windows.Forms.Button).Tag, Integer))
        Colorer()
    End Sub

    'Runs when the Undo Button is clicked
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        moves -= 1
        Label3.Text = "Total Moves: " & moves
        If (player = 1) Then
            board(moveMade(1)).BackgroundImage = board(moveMade(0)).BackgroundImage
            board(moveMade(0)).BackgroundImage = pict
            If (pict Is Nothing) Then
                board(moveMade(0)).ForeColor = Color.Empty
            Else
                board(moveMade(0)).ForeColor = Color.White
            End If
            board(moveMade(1)).ForeColor = Color.Black
            player = 2
            Label1.Text = "Player 2's Turn"
            Button1.Enabled = False
        ElseIf (player = 2) Then
            board(moveMade(1)).BackgroundImage = board(moveMade(0)).BackgroundImage
            board(moveMade(0)).BackgroundImage = pict
            If (pict Is Nothing) Then
                board(moveMade(0)).ForeColor = Color.Empty
            Else
                board(moveMade(0)).ForeColor = Color.Black
            End If
            board(moveMade(1)).ForeColor = Color.White
            player = 1
            Label1.Text = "Player 1's Turn"
            Button1.Enabled = False
        End If
    End Sub

    'Runs when the hint button is clicked
    Private Sub Button2_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
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
            If (board(piece - 16).BackgroundImage Is Nothing) And (Math.Ceiling(piece / 8) = 7) Then
                board(piece - 16).BackColor = Color.Yellow
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
            If (board(piece + 16).BackgroundImage Is Nothing) And (Math.Ceiling(piece / 8) = 2) Then
                board(piece + 16).BackColor = Color.Yellow
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
            Next i
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
            Next i
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
            Next i
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
            Next i


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
            Next i
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
            Next i
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
            Next i
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
            Next i


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
            Next i
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
            Next i
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
            Next i
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
            Next i
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
            Next i
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
            Next i
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
            Next i
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
            Next i
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
        Button2.Enabled = False
    End Sub

    'Colors the board with the correct pattern
    Public Sub Colorer()
        For count As Integer = 1 To 64
            If ((count Mod 16) Mod 2 = 0 Xor count Mod 16 > 8) Then
                board(count).BackColor = Color.Empty
            Else
                board(count).BackColor = Color.Maroon
            End If
        Next count
        For count As Integer = 16 To 64 Step 16
            board(count).BackColor = Color.Maroon
        Next count
    End Sub

    'Checks if the space that is clicked on either contains a piece associated with the player
    'whose turn it is, or if a piece is selected already
    Public Sub CheckSpace(ByVal num As Integer)
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

    'Runs after a piece has been movedxxxxxxxxxxxxxx
    Public Sub Moved()
        moveMade(0) = num
        moveMade(1) = piece
        If Not (AI = 1) Then
            Button1.Enabled = True
        End If
        If (BlackCheck() = True) Then
            MsgBox("Black is in Check!")
        End If
        If (WhiteCheck() = True) Then
            MsgBox("White is in Check!")
        End If
    End Sub

    'Declares that a player has won the game
    Public Sub WinKing()
        Dim king As Integer
        For counter As Integer = 1 To 64
            If (board(counter).BackgroundImage Is wk Or board(counter).BackgroundImage Is bk) Then
                king += 1
            End If
        Next counter
        If (king = 2) Then
            Exit Sub
        End If
        If (player = 1) Then
            MsgBox(secplayer & " Wins!!")
            MostUsed()
        Else
            MsgBox(firstplayer & " Wins!!")
            MostUsed()
        End If
    End Sub

    'Runs when a pawn has reached the opposite end of the board and is promoted to a queen
    Public Sub PawnQueen()
        For counter As Integer = 1 To 8
            If (board(counter).BackgroundImage Is wp) Then
                board(counter).BackgroundImage = wq
            End If
        Next counter
        For counter As Integer = 57 To 64
            If (board(counter).BackgroundImage Is bp) Then
                board(counter).BackgroundImage = bq
            End If
        Next counter
    End Sub

    'Decides if a move is a valid move
    Public Sub MovePiece(ByVal numb As Integer)
        num = numb
        If (player = 1) Then
            If (board(num).BackgroundImage Is Nothing Or board(num).ForeColor = Color.Black) Then
                If (board(piece).BackgroundImage Is wp) Then
                    If (piece - 8 = num And board(num).BackgroundImage Is Nothing) Then
                        white(0) += 1
                        WhiteMover()
                        PawnQueen()
                    ElseIf ((piece - 9 = num) And board(piece - 9).ForeColor = Color.Black) Or ((piece - 7 = num) And board(piece - 7).ForeColor = Color.Black) Then
                        white(0) += 1
                        WhiteMover()
                        PawnQueen()
                    ElseIf (piece - 16 = num) And (board(piece - 16).BackgroundImage Is Nothing) And (Math.Ceiling(piece / 8) = 7) And (board(piece - 8).BackgroundImage Is Nothing) Then
                        white(0) += 1
                        WhiteMover()
                    End If

                ElseIf (board(piece).BackgroundImage Is wr) Then
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            white(1) += 1
                            WhiteMover()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            white(1) += 1
                            WhiteMover()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            white(1) += 1
                            WhiteMover()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            white(1) += 1
                            WhiteMover()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter

                ElseIf (board(piece).BackgroundImage Is wn) Then
                    If (piece - 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece - 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece + 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece + 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece - 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece + 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece - 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    ElseIf (piece + 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        white(2) += 1
                        WhiteMover()
                    End If

                ElseIf (board(piece).BackgroundImage Is wb) Then
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            white(3) += 1
                            WhiteMover()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            white(3) += 1
                            WhiteMover()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            white(3) += 1
                            WhiteMover()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            white(3) += 1
                            WhiteMover()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter

                ElseIf (board(piece).BackgroundImage Is wq) Then
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            white(4) += 1
                            WhiteMover()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter

                ElseIf (board(piece).BackgroundImage Is wk) Then
                    Castle()
                    If (piece - 1 = num) And (Math.Ceiling((piece - 1) / 8) = Math.Ceiling(piece / 8)) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece + 1 = num) And (Math.Ceiling((piece + 1) / 8) = Math.Ceiling(piece / 8)) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece - 8 = num) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece + 8 = num) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece - 9 = num) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece + 9 = num) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece - 7 = num) And Not (Math.Ceiling((piece - 7) / 8) = Math.Ceiling(piece / 8)) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    ElseIf (piece + 7 = num) And Not (Math.Ceiling((piece + 7) / 8) = Math.Ceiling(piece / 8)) Then
                        white(5) += 1
                        wkingmove = True
                        WhiteMover()
                    End If
                End If
            End If



        Else
            If (board(num).BackgroundImage Is Nothing Or board(num).ForeColor = Color.White) Then
                If (board(piece).BackgroundImage Is bp) Then
                    If (piece + 8 = num And board(num).BackgroundImage Is Nothing) Then
                        black(0) += 1
                        BlackMover()
                        PawnQueen()
                    ElseIf ((piece + 9 = num) And board(piece + 9).ForeColor = Color.White) Or ((piece + 7 = num) And board(piece + 7).ForeColor = Color.White) Then
                        black(0) += 1
                        BlackMover()
                        PawnQueen()
                    ElseIf (piece + 16 = num) And (board(piece + 16).BackgroundImage Is Nothing) And (Math.Ceiling(piece / 8) = 2) And (board(piece + 8).BackgroundImage Is Nothing) Then
                        black(0) += 1
                        BlackMover()
                    End If

                ElseIf (board(piece).BackgroundImage Is br) Then
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            black(1) += 1
                            BlackMover()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            black(1) += 1
                            BlackMover()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            black(1) += 1
                            BlackMover()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            black(1) += 1
                            BlackMover()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter

                ElseIf (board(piece).BackgroundImage Is bn) Then
                    If (piece - 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece - 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 16) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece + 17 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece + 15 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 16) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece - 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece + 10 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece - 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece - 8) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    ElseIf (piece + 6 = num) And (Math.Ceiling((num) / 8) = Math.Ceiling((piece + 8) / 8)) Then
                        black(2) += 1
                        BlackMover()
                    End If

                ElseIf (board(piece).BackgroundImage Is bb) Then
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            black(3) += 1
                            BlackMover()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            black(3) += 1
                            BlackMover()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            black(3) += 1
                            BlackMover()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            black(3) += 1
                            BlackMover()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter

                ElseIf (board(piece).BackgroundImage Is bq) Then
                    For counter As Integer = 1 To 7
                        If (piece - (8 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece - (8 * counter) > 0 And piece - (8 * counter) < 65) Then
                            If Not (board(piece - (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If (piece + (8 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece + (8 * counter) > 0 And piece + (8 * counter) < 65) Then
                            If Not (board(piece + (8 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 1 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (1 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece - (1 * counter) > 0 And piece - (1 * counter) < 65) Then
                            If Not (board(piece - (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 1 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (1 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece + (1 * counter) > 0 And piece + (1 * counter) < 65) Then
                            If Not (board(piece + (1 * counter)).BackgroundImage Is Nothing) Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 7 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece - (7 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece - (7 * counter) > 0 And piece - (7 * counter) < 65) Then
                            If Not (board(piece - (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 7 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece + (7 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece + (7 * counter) > 0 And piece + (7 * counter) < 65) Then
                            If Not (board(piece + (7 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece - 9 * counter) Mod 8 = 0) Then
                            Exit For
                        End If
                        If (piece - (9 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece - (9 * counter) > 0 And piece - (9 * counter) < 65) Then
                            If Not (board(piece - (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter
                    For counter As Integer = 1 To 7
                        If ((piece + 9 * counter) Mod 8 = 1) Then
                            Exit For
                        End If
                        If (piece + (9 * counter) = num) Then
                            black(4) += 1
                            BlackMover()
                        End If
                        If (piece + (9 * counter) > 0 And piece + (9 * counter) < 65) Then
                            If Not (board(piece + (9 * counter))).BackgroundImage Is Nothing Then
                                Exit For
                            End If
                        End If
                    Next counter

                ElseIf (board(piece).BackgroundImage Is bk) Then
                    Castle()
                    If (piece - 1 = num) And (Math.Ceiling((piece - 1) / 8) = Math.Ceiling(piece / 8)) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece + 1 = num) And (Math.Ceiling((piece + 1) / 8) = Math.Ceiling(piece / 8)) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece - 8 = num) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece + 8 = num) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece - 9 = num) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece + 9 = num) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece - 7 = num) And Not (Math.Ceiling((piece - 7) / 8) = Math.Ceiling(piece / 8)) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    ElseIf (piece + 7 = num) And Not (Math.Ceiling((piece + 7) / 8) = Math.Ceiling(piece / 8)) Then
                        black(5) += 1
                        bkingmove = True
                        BlackMover()
                    End If
                End If
            End If
        End If
    End Sub

    'Keeps track of the most used piece for each player
    Public Sub MostUsed()
        Dim bpiece As Integer
        Dim wpiece As Integer
        Dim bpieceS As String = ""
        Dim wpieceS As String = ""
        Dim wmax As Integer = 0
        Dim bmax As Integer = 0

        For i As Integer = 0 To 4
            If white(i) > wmax Then
                wmax = white(i)
                wpiece = i
            End If
        Next i
        For i As Integer = 0 To 4
            If black(i) > bmax Then
                bmax = black(i)
                bpiece = i
            End If
        Next i

        If (wpiece = 0) Then
            wpieceS = "Pawn"
        ElseIf (wpiece = 1) Then
            wpieceS = "Rook"
        ElseIf (wpiece = 2) Then
            wpieceS = "Knight"
        ElseIf (wpiece = 3) Then
            wpieceS = "Bishop"
        ElseIf (wpiece = 4) Then
            wpieceS = "Queen"
        ElseIf (wpiece = 5) Then
            wpieceS = "King"
        End If

        If (bpiece = 0) Then
            bpieceS = "Pawn"
        ElseIf (bpiece = 1) Then
            bpieceS = "Rook"
        ElseIf (bpiece = 2) Then
            bpieceS = "Knight"
        ElseIf (bpiece = 3) Then
            bpieceS = "Bishop"
        ElseIf (bpiece = 4) Then
            bpieceS = "Queen"
        ElseIf (bpiece = 5) Then
            bpieceS = "King"
        End If

        MsgBox("White's most used piece was the " & wpieceS & " and they moved it " & wmax & " times.")
        If (AI = 0) Then
            MsgBox("Black's most used piece was the " & bpieceS & " and they moved it " & bmax & " times.")
        End If
        Close()
    End Sub

    'Event Handler for all board squares which changes the color of whichever space you are on
    Public Sub MouseEnterShadows(ByVal sender As Object, ByVal e As EventArgs)
        coler = (CType(CType(sender, System.Windows.Forms.Button).BackColor, Color))
        board(CType(CType(sender, System.Windows.Forms.Button).Tag, Integer)).BackColor = Color.GreenYellow
        Timer1.Enabled = True
    End Sub

    'Event Handler for all board squares which changes the color back to the default
    Public Sub MouseLeaveShadows(ByVal sender As Object, ByVal e As EventArgs)
        board(CType(CType(sender, System.Windows.Forms.Button).Tag, Integer)).BackColor = coler
        If (coler = Color.Yellow) And Not (board(CType(CType(sender, System.Windows.Forms.Button).Tag, Integer)).BackgroundImage Is Nothing) Then
            If (player = 2) And (board(CType(CType(sender, System.Windows.Forms.Button).Tag, Integer)).ForeColor = Color.White) Then
                Colorer()
            ElseIf (player = 1) And (board(CType(CType(sender, System.Windows.Forms.Button).Tag, Integer)).ForeColor = Color.Black) Then
                Colorer()
            End If
        End If
    End Sub

    'Timer in charge of the game time
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        sec += 1
        If (sec > 59) Then
            sec = 0
            min += 1
        End If

        If (sec < 10) Then
            Label2.Text = (min & ":0" & sec)
        Else
            Label2.Text = (min & ":" & sec)
        End If
    End Sub

    'Runs whenever a white piece must be moved
    Public Sub WhiteMover()
        pict = board(num).BackgroundImage
        board(num).BackgroundImage = board(piece).BackgroundImage
        board(piece).BackgroundImage = Nothing
        board(num).ForeColor = Color.White
        board(piece).ForeColor = Color.Empty
        player = 2
        Label1.Text = secplayer & "'s Turn"
        WinKing()
        Moved()
        moves += 1
        Label3.Text = "Total Moves: " & moves
        If (AI = 1) Then
            Button1.Enabled = False
            AImove()
        End If
    End Sub

    'Runs whenever a black piece must be moved
    Public Sub BlackMover()
        pict = board(num).BackgroundImage
        board(num).BackgroundImage = board(piece).BackgroundImage
        board(piece).BackgroundImage = Nothing
        board(num).ForeColor = Color.Black
        board(piece).ForeColor = Color.Empty
        player = 1
        Label1.Text = firstplayer & "'s Turn"
        WinKing()
        Moved()
        moves += 1
        Label3.Text = "Total Moves: " & moves
    End Sub

    'Decides if black is in check
    Public Function BlackCheck() As Boolean
        Dim i As Integer
        Dim king As Integer
        For i = 1 To 64
            If (board(i).BackgroundImage Is bk) Then
                king = i
            End If
        Next i

        For i = 1 To 7
            If (king + 8 * i < 65) Then
                If (board(king + 8 * i).ForeColor = Color.White) And ((board(king + 8 * i).BackgroundImage Is wr) Or (board(king + 8 * i).BackgroundImage Is wq)) Then

                    Return True
                    Exit Function
                End If
                If Not (board(king + 8 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king - 8 * i > 0) Then
                If (board(king - 8 * i).ForeColor = Color.White) And ((board(king - 8 * i).BackgroundImage Is wr) Or (board(king - 8 * i).BackgroundImage Is wq)) Then

                    Return True
                    Exit Function
                End If
                If Not (board(king - 8 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If ((king - 1 * i) Mod 8 = 0) Then
                Exit For
            End If
            If (board(king - 1 * i).ForeColor = Color.White) And ((board(king - 1 * i).BackgroundImage Is wr) Or (board(king - 1 * i).BackgroundImage Is wq)) Then
                Return True
                Exit Function
            End If
            If Not (board(king - 1 * i).BackgroundImage Is Nothing) Then
                Exit For
            End If
        Next i

        For i = 1 To 7
            If ((king + 1 * i) Mod 8 = 1) Then
                Exit For
            End If
            If (board(king + 1 * i).ForeColor = Color.White) And ((board(king + 1 * i).BackgroundImage Is wr) Or (board(king + 1 * i).BackgroundImage Is wq)) Then

                Return True
                Exit Function
            End If
            If Not (board(king + 1 * i).BackgroundImage Is Nothing) Then
                Exit For
            End If
        Next i

        For i = 1 To 7
            If (king - 9 * i > 0) Then
                If ((king - 9 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(king - 9 * i).ForeColor = Color.White) And ((board(king - 9 * i).BackgroundImage Is wb) Or (board(king - 9 * i).BackgroundImage Is wq)) Then

                    Return True
                    Exit Function
                End If
                If Not (board(king - 9 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king + 9 * i < 65) Then
                If ((king + 9 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(king + 9 * i).ForeColor = Color.White) And ((board(king + 9 * i).BackgroundImage Is wb) Or (board(king + 9 * i).BackgroundImage Is wq)) Then

                    Return True
                    Exit Function
                End If
                If Not (board(king + 9 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king - 7 * i > 0) Then
                If ((king - 7 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(king - 7 * i).ForeColor = Color.White) And ((board(king - 7 * i).BackgroundImage Is wb) Or (board(king - 7 * i).BackgroundImage Is wq)) Then

                    Return True
                    Exit Function
                End If
                If Not (board(king - 7 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king + 7 * i < 65) Then
                If ((king + 7 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(king + 7 * i).ForeColor = Color.White) And ((board(king + 7 * i).BackgroundImage Is wb) Or (board(king + 7 * i).BackgroundImage Is wq)) Then

                    Return True
                    Exit Function
                End If
                If Not (board(king + 7 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        If (king + 9 < 65) Then
            If (board(king + 9).BackgroundImage Is wp) Then

                Return True
                Exit Function
            End If
        End If

        If (king + 7 < 65) Then
            If (board(king + 7).BackgroundImage Is wp) Then

                Return True
                Exit Function
            End If
        End If

        If (king + 17 < 65) Then
            If (board(king + 17).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king - 17 > 0) Then
            If (board(king - 17).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king + 15 < 65) Then
            If (board(king + 15).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king - 15 > 0) Then
            If (board(king - 15).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king + 10 < 65) Then
            If (board(king + 10).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king - 10 > 0) Then
            If (board(king - 10).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king + 6 < 65) Then
            If (board(king + 6).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If

        If (king - 6 > 0) Then
            If (board(king - 6).BackgroundImage Is wn) Then

                Return True
                Exit Function
            End If
        End If
    End Function

    'Decides if white is in check
    Public Function WhiteCheck() As Boolean
        Dim i As Integer
        Dim king As Integer

        For i = 1 To 64
            If (board(i).BackgroundImage Is wk) Then
                king = i
            End If
        Next i

        For i = 1 To 7
            If (king + 8 * i < 65) Then
                If (board(king + 8 * i).ForeColor = Color.Black) And ((board(king + 8 * i).BackgroundImage Is br) Or (board(king + 8 * i).BackgroundImage Is bq)) Then
                    Return True
                    Exit Function
                End If
                If Not (board(king + 8 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king - 8 * i > 0) Then
                If (board(king - 8 * i).ForeColor = Color.Black) And ((board(king - 8 * i).BackgroundImage Is br) Or (board(king - 8 * i).BackgroundImage Is bq)) Then
                    Return True
                    Exit Function
                End If
                If Not (board(king - 8 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If ((king - 1 * i) Mod 8 = 0) Then
                Exit For
            End If
            If (board(king - 1 * i).ForeColor = Color.Black) And ((board(king - 1 * i).BackgroundImage Is br) Or (board(king - 1 * i).BackgroundImage Is bq)) Then
                Return True
                Exit Function
            End If
            If Not (board(king - 1 * i).BackgroundImage Is Nothing) Then
                Exit For
            End If
        Next i

        For i = 1 To 7
            If ((king + 1 * i) Mod 8 = 1) Then
                Exit For
            End If
            If (board(king + 1 * i).ForeColor = Color.Black) And ((board(king + 1 * i).BackgroundImage Is br) Or (board(king + 1 * i).BackgroundImage Is bq)) Then
                Return True
                Exit Function
            End If
            If Not (board(king + 1 * i).BackgroundImage Is Nothing) Then
                Exit For
            End If
        Next i

        For i = 1 To 7
            If (king - 9 * i > 0) Then
                If ((king - 9 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(king - 9 * i).ForeColor = Color.Black) And ((board(king - 9 * i).BackgroundImage Is bb) Or (board(king - 9 * i).BackgroundImage Is bq)) Then
                    Return True
                    Exit Function
                End If
                If Not (board(king - 9 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king + 9 * i < 65) Then
                If ((king + 9 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(king + 9 * i).ForeColor = Color.Black) And ((board(king + 9 * i).BackgroundImage Is bb) Or (board(king + 9 * i).BackgroundImage Is bq)) Then
                    Return True
                    Exit Function
                End If
                If Not (board(king + 9 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king - 7 * i > 0) Then
                If ((king - 7 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(king - 7 * i).ForeColor = Color.Black) And ((board(king - 7 * i).BackgroundImage Is bb) Or (board(king - 7 * i).BackgroundImage Is bq)) Then
                    Return True
                    Exit Function
                End If
                If Not (board(king - 7 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (king + 7 * i < 65) Then
                If ((king + 7 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(king + 7 * i).ForeColor = Color.Black) And ((board(king + 7 * i).BackgroundImage Is bb) Or (board(king + 7 * i).BackgroundImage Is bq)) Then
                    Return True
                    Exit Function
                End If
                If Not (board(king + 7 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        If (king - 9 > 0) Then
            If (board(king - 9).BackgroundImage Is bp) Then
                Return True
                Exit Function
            End If
        End If

        If (king - 7 > 0) Then
            If (board(king - 7).BackgroundImage Is bp) Then
                Return True
                Exit Function
            End If
        End If

        If (king + 17 < 65) Then
            If (board(king + 17).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king - 17 > 0) Then
            If (board(king - 17).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king + 15 < 65) Then
            If (board(king + 15).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king - 15 > 0) Then
            If (board(king - 15).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king + 10 < 65) Then
            If (board(king + 10).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king - 10 > 0) Then
            If (board(king - 10).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king + 6 < 65) Then
            If (board(king + 6).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If

        If (king - 6 > 0) Then
            If (board(king - 6).BackgroundImage Is bn) Then
                Return True
                Exit Function
            End If
        End If
    End Function

    Dim wkingmove As Boolean = False
    Dim bkingmove As Boolean = False

    Public Sub Castle()
        If (player = 1) And (wkingmove = False) Then
            If (num = 58) And (board(59).BackgroundImage Is Nothing) And (board(58).BackgroundImage Is Nothing) Then
                board(57).BackgroundImage = Nothing
                board(58).BackgroundImage = wk
                board(59).BackgroundImage = wr
                board(60).BackgroundImage = Nothing
                board(57).ForeColor = Nothing
                board(58).ForeColor = Color.White
                board(59).ForeColor = Color.White
                board(60).ForeColor = Nothing
                player = 2
                Label1.Text = secplayer & "'s Turn"
                WinKing()
                Button1.Enabled = False
                moves += 1
                Label3.Text = "Total Moves: " & moves
                wkingmove = True
                If (AI = 1) Then
                    AImove()
                End If
            ElseIf (num = 62) And (board(63).BackgroundImage Is Nothing) And (board(62).BackgroundImage Is Nothing) And (board(61).BackgroundImage Is Nothing) Then
                board(60).BackgroundImage = Nothing
                board(60).ForeColor = Nothing
                board(61).BackgroundImage = wr
                board(61).ForeColor = Color.White
                board(62).BackgroundImage = wk
                board(62).ForeColor = Color.White
                board(63).BackgroundImage = Nothing
                board(63).ForeColor = Nothing
                board(64).BackgroundImage = Nothing
                board(64).ForeColor = Nothing
                player = 2
                Label1.Text = secplayer & "'s Turn"
                WinKing()
                Button1.Enabled = False
                moves += 1
                Label3.Text = "Total Moves: " & moves
                wkingmove = True
                If (AI = 1) Then
                    AImove()
                End If
            End If
        ElseIf (player = 2) And (bkingmove = False) Then
            If (num = 2) And (board(2).BackgroundImage Is Nothing) And (board(3).BackgroundImage Is Nothing) Then
                board(1).BackgroundImage = Nothing
                board(1).ForeColor = Nothing
                board(2).BackgroundImage = bk
                board(2).ForeColor = Color.Black
                board(3).BackgroundImage = br
                board(3).ForeColor = Color.Black
                board(4).BackgroundImage = Nothing
                board(4).ForeColor = Nothing
                player = 1
                Label1.Text = firstplayer & "'s Turn"
                WinKing()
                Button1.Enabled = False
                moves += 1
                Label3.Text = "Total Moves: " & moves
                bkingmove = True
            ElseIf (num = 6) And (board(5).BackgroundImage Is Nothing) And (board(6).BackgroundImage Is Nothing) And (board(7).BackgroundImage Is Nothing) Then
                board(4).BackgroundImage = Nothing
                board(4).ForeColor = Nothing
                board(5).BackgroundImage = br
                board(5).ForeColor = Color.Black
                board(6).BackgroundImage = bk
                board(6).ForeColor = Color.Black
                board(7).BackgroundImage = Nothing
                board(7).ForeColor = Nothing
                board(8).BackgroundImage = Nothing
                board(8).ForeColor = Nothing
                player = 1
                Label1.Text = firstplayer & "'s Turn"
                WinKing()
                Button1.Enabled = False
                moves += 1
                Label3.Text = "Total Moves: " & moves
                bkingmove = True
            End If
        End If
    End Sub

    Dim AI As Integer
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (AI = 0) Then
            Button3.Text = "Switch to 2 Player"
            AI = 1
        ElseIf (AI = 1) Then
            Button3.Text = "Switch to AI"
            AI = 0
        End If
    End Sub
    Dim space As Integer
    Dim boardpos(64) As Integer
    Dim testboardpos(64) As Integer
    Dim movetospace As Integer
    Dim spacef As Integer
    Dim movetospacef As Integer
    Dim whitepawn As Integer
    Dim whiterook As Integer
    Dim whitemin As Integer
    Dim whitequeen As Integer
    Dim testwhitepawn As Integer
    Dim testwhiterook As Integer
    Dim testwhitemin As Integer
    Dim testwhitequeen As Integer
    Public Sub AImove()
        boardpos(0) = 52
        whitepawn = 0
        whitequeen = 0
        whitemin = 0
        whiterook = 0
        For counter As Integer = 1 To 64
            If (board(counter).BackgroundImage Is bp) Then
                boardpos(counter) = 0
            ElseIf (board(counter).BackgroundImage Is br) Then
                boardpos(counter) = 1
            ElseIf (board(counter).BackgroundImage Is bn) Then
                boardpos(counter) = 2
            ElseIf (board(counter).BackgroundImage Is bb) Then
                boardpos(counter) = 3
            ElseIf (board(counter).BackgroundImage Is bq) Then
                boardpos(counter) = 4
            ElseIf (board(counter).BackgroundImage Is bk) Then
                boardpos(counter) = 5
            ElseIf (board(counter).BackgroundImage Is wp) Then
                boardpos(counter) = 6
                whitepawn += 1
            ElseIf (board(counter).BackgroundImage Is wr) Then
                boardpos(counter) = 7
                whiterook += 1
            ElseIf (board(counter).BackgroundImage Is wn) Then
                boardpos(counter) = 8
                whitemin += 1
            ElseIf (board(counter).BackgroundImage Is wb) Then
                boardpos(counter) = 9
                whitemin += 1
            ElseIf (board(counter).BackgroundImage Is wq) Then
                boardpos(counter) = 10
                whitequeen += 1
            ElseIf (board(counter).BackgroundImage Is wk) Then
                boardpos(counter) = 11
            Else
                boardpos(counter) = 12
            End If
        Next counter

        For j As Integer = 1 To 64
            testboardpos(j) = boardpos(j)
        Next j

        For space = 1 To 64
            If (board(space).ForeColor = Color.Black) Then
                If (board(space).BackgroundImage Is bp) Then
                    AIpawn()
                ElseIf (board(space).BackgroundImage Is br) Then
                    AIrook()
                ElseIf (board(space).BackgroundImage Is bn) Then
                    AIknight()
                ElseIf (board(space).BackgroundImage Is bb) Then
                    AIbishop()
                ElseIf (board(space).BackgroundImage Is bq) Then
                    AIqueen()
                ElseIf (board(space).BackgroundImage Is bk) Then
                    AIking()
                End If
            End If
        Next space
        AiMover()
    End Sub

    Public Sub AIpawn()
        If (space + 8 < 65) Then
            If (board(space + 8).BackgroundImage Is Nothing) Then
                testboardpos(space + 8) = 0
                testboardpos(space) = 12
                movetospace = space + 8
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If

        If (space + 9 < 65) Then
            If (board(space + 9).ForeColor = Color.White) And (Math.Ceiling((space + 9) / 8) = Math.Ceiling((space) / 8) + 1) Then
                testboardpos(space + 9) = 0
                testboardpos(space) = 12
                movetospace = space + 9
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space + 7 < 65) Then
            If (board(space + 7).ForeColor = Color.White) And (Math.Ceiling((space + 9) / 7) = Math.Ceiling((space) / 7) + 1) Then
                testboardpos(space + 7) = 0
                testboardpos(space) = 12
                movetospace = space + 7
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If

        If (space + 16 < 65) Then
            If (Math.Ceiling((space + 16) / 8) = 4) And (board(space + 16).BackgroundImage Is Nothing) And (board(space + 8).BackgroundImage Is Nothing) Then
                testboardpos(space + 16) = 0
                testboardpos(space) = 12
                movetospace = space + 16
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
    End Sub

    Public Sub AIrook()
        Dim counter As Integer

        For counter = 1 To 7
            If (space - counter * 8 > 0) Then
                If (board(space - counter * 8).BackgroundImage Is Nothing) Then
                    testboardpos(space - counter * 8) = 1
                    testboardpos(space) = 12
                    movetospace = space - counter * 8
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - counter * 8).ForeColor = Color.White) Then
                        testboardpos(space - counter * 8) = 1
                        testboardpos(space) = 12
                        movetospace = space - counter * 8
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For counter = 1 To 7
            If (space + counter * 8 < 65) Then
                If (board(space + counter * 8).BackgroundImage Is Nothing) Then
                    testboardpos(space + counter * 8) = 1
                    testboardpos(space) = 12
                    movetospace = space + counter * 8
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + counter * 8).ForeColor = Color.White) Then
                        testboardpos(space + counter * 8) = 1
                        testboardpos(space) = 12
                        movetospace = space + counter * 8
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For counter = 1 To 7
            If (space + counter < 65) Then
                If ((space + counter) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(space + counter).BackgroundImage Is Nothing) Then
                    testboardpos(space + counter) = 1
                    testboardpos(space) = 12
                    movetospace = space + counter
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + counter).ForeColor = Color.White) Then
                        testboardpos(space + counter) = 1
                        testboardpos(space) = 12
                        movetospace = space + counter
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For counter = 1 To 7
            If (space - counter > 0) Then
                If ((space - counter) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(space - counter).BackgroundImage Is Nothing) Then
                    testboardpos(space - counter) = 1
                    testboardpos(space) = 12
                    movetospace = space - counter
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - counter).ForeColor = Color.White) Then
                        testboardpos(space - counter) = 1
                        testboardpos(space) = 12
                        movetospace = space - counter
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
    End Sub

    Public Sub AIknight()
        If (space + 17 < 65) Then
            If (Math.Ceiling((space + 17) / 8) = Math.Ceiling(space / 8) + 2) And ((board(space + 17).BackgroundImage Is Nothing) Or (board(space + 17).ForeColor = Color.White)) Then
                testboardpos(space + 17) = 2
                testboardpos(space) = 12
                movetospace = space + 17
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space + 15 < 65) Then
            If (Math.Ceiling((space + 15) / 8) = Math.Ceiling(space / 8) + 2) And ((board(space + 15).BackgroundImage Is Nothing) Or (board(space + 15).ForeColor = Color.White)) Then
                testboardpos(space + 15) = 2
                testboardpos(space) = 12
                movetospace = space + 15
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 17 > 0) Then
            If (Math.Ceiling((space - 17) / 8) = Math.Ceiling(space / 8) - 2) And ((board(space - 17).BackgroundImage Is Nothing) Or (board(space - 17).ForeColor = Color.White)) Then
                testboardpos(space - 17) = 2
                testboardpos(space) = 12
                movetospace = space - 17
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 15 > 0) Then
            If (Math.Ceiling((space - 15) / 8) = Math.Ceiling(space / 8) - 2) And ((board(space - 15).BackgroundImage Is Nothing) Or (board(space - 15).ForeColor = Color.White)) Then
                testboardpos(space - 15) = 2
                testboardpos(space) = 12
                movetospace = space - 15
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space + 6 < 65) Then
            If (Math.Ceiling((space + 6) / 8) = Math.Ceiling(space / 8) + 1) And ((board(space + 6).BackgroundImage Is Nothing) Or (board(space + 6).ForeColor = Color.White)) Then
                testboardpos(space + 6) = 2
                testboardpos(space) = 12
                movetospace = space + 6
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space + 10 < 65) Then
            If (Math.Ceiling((space + 10) / 8) = Math.Ceiling(space / 8) + 1) And ((board(space + 10).BackgroundImage Is Nothing) Or (board(space + 10).ForeColor = Color.White)) Then
                testboardpos(space + 10) = 2
                testboardpos(space) = 12
                movetospace = space + 10
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 6 > 0) Then
            If (Math.Ceiling((space - 6) / 8) = Math.Ceiling(space / 8) - 1) And ((board(space - 6).BackgroundImage Is Nothing) Or (board(space - 6).ForeColor = Color.White)) Then
                testboardpos(space - 6) = 2
                testboardpos(space) = 12
                movetospace = space - 6
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 10 > 0) Then
            If (Math.Ceiling((space - 10) / 8) = Math.Ceiling(space / 8) - 1) And ((board(space - 10).BackgroundImage Is Nothing) Or (board(space - 10).ForeColor = Color.White)) Then
                testboardpos(space - 10) = 2
                testboardpos(space) = 12
                movetospace = space - 10
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
    End Sub

    Public Sub AIbishop()
        Dim i As Integer

        For i = 1 To 64
            If (space + 7 * i < 65) Then
                If ((space + 7 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(space + 7 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space + 7 * i) = 3
                    testboardpos(space) = 12
                    movetospace = space + 7 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + 7 * i).ForeColor = Color.White) Then
                        testboardpos(space + 7 * i) = 3
                        testboardpos(space) = 12
                        movetospace = space + 7 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
        For i = 1 To 64
            If (space - 7 * i > 0) Then
                If ((space - 7 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(space - 7 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space - 7 * i) = 3
                    testboardpos(space) = 12
                    movetospace = space - 7 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - 7 * i).ForeColor = Color.White) Then
                        testboardpos(space - 7 * i) = 3
                        testboardpos(space) = 12
                        movetospace = space - 7 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
        For i = 1 To 64
            If (space + 9 * i < 65) Then
                If ((space + 9 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(space + 9 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space + 9 * i) = 3
                    testboardpos(space) = 12
                    movetospace = space + 9 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + 9 * i).ForeColor = Color.White) Then
                        testboardpos(space + 9 * i) = 3
                        testboardpos(space) = 12
                        movetospace = space + 9 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
        For i = 1 To 64
            If (space - 9 * i > 0) Then
                If ((space - 9 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(space - 9 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space - 9 * i) = 3
                    testboardpos(space) = 12
                    movetospace = space - 9 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - 9 * i).ForeColor = Color.White) Then
                        testboardpos(space - 9 * i) = 3
                        testboardpos(space) = 12
                        movetospace = space - 9 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
    End Sub

    Public Sub AIking()
        If (space + 8 < 65) Then
            If (board(space + 8).BackgroundImage Is Nothing) Or (board(space + 8).ForeColor = Color.White) Then
                testboardpos(space + 8) = 5
                testboardpos(space) = 12
                movetospace = space + 8
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 8 > 0) Then
            If (board(space - 8).BackgroundImage Is Nothing) Or (board(space - 8).ForeColor = Color.White) Then
                testboardpos(space - 8) = 5
                testboardpos(space) = 12
                movetospace = space - 8
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space + 7 < 65) Then
            If ((board(space + 7).BackgroundImage Is Nothing) Or (board(space + 7).ForeColor = Color.White)) And ((space + 7) Mod 8 = 0) Then
                testboardpos(space + 7) = 5
                testboardpos(space) = 12
                movetospace = space + 7
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 7 > 0) Then
            If ((board(space - 7).BackgroundImage Is Nothing) Or (board(space - 7).ForeColor = Color.White)) And ((space - 7) Mod 8 = 1) Then
                testboardpos(space - 7) = 5
                testboardpos(space) = 12
                movetospace = space - 7
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space + 9 < 65) Then
            If ((board(space + 9).BackgroundImage Is Nothing) Or (board(space + 9).ForeColor = Color.White)) And ((space + 9) Mod 8 = 1) Then
                testboardpos(space + 9) = 5
                testboardpos(space) = 12
                movetospace = space + 9
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
        If (space - 9 > 0) Then
            If ((board(space - 9).BackgroundImage Is Nothing) Or (board(space - 9).ForeColor = Color.White)) And ((space - 9) Mod 8 = 0) Then
                testboardpos(space - 9) = 5
                testboardpos(space) = 12
                movetospace = space - 9
                If (BlackCheck() = True) Then
                    AiinCheck()
                Else
                    Calculate()
                End If
            End If
        End If
    End Sub

    Public Sub AIqueen()
        Dim i As Integer
        Dim counter As Integer

        For counter = 1 To 7
            If (space - counter * 8 > 0) Then
                If (board(space - counter * 8).BackgroundImage Is Nothing) Then
                    testboardpos(space - counter * 8) = 4
                    testboardpos(space) = 12
                    movetospace = space - counter * 8
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - counter * 8).ForeColor = Color.White) Then
                        testboardpos(space - counter * 8) = 4
                        testboardpos(space) = 12
                        movetospace = space - counter * 8
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For counter = 1 To 7
            If (space + counter * 8 < 65) Then
                If (board(space + counter * 8).BackgroundImage Is Nothing) Then
                    testboardpos(space + counter * 8) = 4
                    testboardpos(space) = 12
                    movetospace = space + counter * 8
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + counter * 8).ForeColor = Color.White) Then
                        testboardpos(space + counter * 8) = 4
                        testboardpos(space) = 12
                        movetospace = space + counter * 8
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For counter = 1 To 7
            If (space + counter < 65) Then
                If ((space + counter) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(space + counter).BackgroundImage Is Nothing) Then
                    testboardpos(space + counter) = 4
                    testboardpos(space) = 12
                    movetospace = space + counter
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + counter).ForeColor = Color.White) Then
                        testboardpos(space + counter) = 4
                        testboardpos(space) = 12
                        movetospace = space + counter
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For counter = 1 To 7
            If (space - counter > 0) Then
                If ((space - counter) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(space - counter).BackgroundImage Is Nothing) Then
                    testboardpos(space - counter) = 4
                    testboardpos(space) = 12
                    movetospace = space - counter
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - counter).ForeColor = Color.White) Then
                        testboardpos(space - counter) = 4
                        testboardpos(space) = 12
                        movetospace = space - counter
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next counter
        For i = 1 To 64
            If (space + 7 * i < 65) Then
                If ((space + 7 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(space + 7 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space + 7 * i) = 4
                    testboardpos(space) = 12
                    movetospace = space + 7 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + 7 * i).ForeColor = Color.White) Then
                        testboardpos(space + 7 * i) = 4
                        testboardpos(space) = 12
                        movetospace = space + 7 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
        For i = 1 To 64
            If (space - 7 * i > 0) Then
                If ((space - 7 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(space - 7 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space - 7 * i) = 4
                    testboardpos(space) = 12
                    movetospace = space - 7 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - 7 * i).ForeColor = Color.White) Then
                        testboardpos(space - 7 * i) = 4
                        testboardpos(space) = 12
                        movetospace = space - 7 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
        For i = 1 To 64
            If (space + 9 * i < 65) Then
                If ((space + 9 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(space + 9 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space + 9 * i) = 4
                    testboardpos(space) = 12
                    movetospace = space + 9 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space + 9 * i).ForeColor = Color.White) Then
                        testboardpos(space + 9 * i) = 4
                        testboardpos(space) = 12
                        movetospace = space + 9 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
        For i = 1 To 64
            If (space - 9 * i > 0) Then
                If ((space - 9 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(space - 9 * i).BackgroundImage Is Nothing) Then
                    testboardpos(space - 9 * i) = 4
                    testboardpos(space) = 12
                    movetospace = space - 9 * i
                    If (BlackCheck() = True) Then
                        AiinCheck()
                    Else
                        Calculate()
                    End If
                Else
                    If (board(space - 9 * i).ForeColor = Color.White) Then
                        testboardpos(space - 9 * i) = 4
                        testboardpos(space) = 12
                        movetospace = space - 9 * i
                        If (BlackCheck() = True) Then
                            AiinCheck()
                        Else
                            Calculate()
                        End If
                    End If
                    Exit For
                End If
            End If
        Next i
    End Sub

    Dim Pawnval(63) As Integer
    Dim Rookval(63) As Integer
    Dim Knightval(63) As Integer
    Dim Bishopval(63) As Integer
    Dim Queenval(63) As Integer
    Dim Kingval(63) As Integer
    Dim whitetot As Integer
    Dim blacktot As Integer
    Dim eval As Integer
    Dim best As Integer = -1000000

    Public Sub Calculate()
        Dim kings As Integer
        For counter As Integer = 1 To 64
            If (testboardpos(counter) = 0) Then
                blacktot += Pawnval(counter - 1)
            End If
            If (testboardpos(counter) = 1) Then
                blacktot += Rookval(counter - 1)
            End If
            If (testboardpos(counter) = 2) Then
                blacktot += Knightval(counter - 1)
            End If
            If (testboardpos(counter) = 3) Then
                blacktot += Bishopval(counter - 1)
            End If
            If (testboardpos(counter) = 4) Then
                blacktot += Queenval(counter - 1)
            End If
            If (testboardpos(counter) = 5) Then
                blacktot += Kingval(counter - 1)
            End If
            If (testboardpos(counter) = 6) Then
                whitetot += Pawnval(65 - counter - 1)
                testwhitepawn += 1
            End If
            If (testboardpos(counter) = 7) Then
                whitetot += Rookval(65 - counter - 1)
                testwhiterook += 1
            End If
            If (testboardpos(counter) = 8) Then
                whitetot += Knightval(65 - counter - 1)
                testwhitemin += 1
            End If
            If (testboardpos(counter) = 9) Then
                whitetot += Bishopval(65 - counter - 1)
                testwhitemin += 1
            End If
            If (testboardpos(counter) = 10) Then
                whitetot += Queenval(65 - counter - 1)
                testwhitequeen += 1
            End If
            If (testboardpos(counter) = 11) Then
                whitetot += Kingval(65 - counter - 1)
            End If
        Next
        eval = blacktot - whitetot

        If (whitepawn <> testwhitepawn) Then
            eval += 11
        ElseIf (whitemin <> testwhitemin) Then
            eval += 31
        ElseIf (whiterook <> testwhiterook) Then
            eval += 51
        ElseIf (whitequeen <> testwhitequeen) Then
            eval += 96
        End If

        If (AIinDanger() = True) Then
            If (board(space).BackgroundImage Is bp) Then
                eval -= 10
            ElseIf (board(space).BackgroundImage Is bb) Or (board(space).BackgroundImage Is bn) Then
                eval -= 30
            ElseIf (board(space).BackgroundImage Is br) Then
                eval -= 50
            ElseIf (board(space).BackgroundImage Is bq) Then
                eval -= 95
            ElseIf (board(space).BackgroundImage Is bk) Then
                eval -= 100000
            End If
        End If

        If (WhiteCheck() = True) Then
            eval += 12
        End If
        If (eval > best) Then
            best = eval
            spacef = space
            movetospacef = movetospace
        End If

        For counter As Integer = 1 To 64
            If (testboardpos(counter) = 11) Then
                kings = 1
            End If
        Next
        If (kings = 0) Then
            best = 100000000
            spacef = space
            movetospacef = movetospace
        End If

        whitetot = 0
        blacktot = 0
        eval = 0
        kings = 0
        testwhitepawn = 0
        testwhitequeen = 0
        testwhitemin = 0
        testwhiterook = 0
        For j As Integer = 1 To 64
            testboardpos(j) = boardpos(j)
        Next j
    End Sub

    Public Sub AiMover()
        board(movetospacef).BackgroundImage = board(spacef).BackgroundImage
        board(spacef).BackgroundImage = Nothing
        board(spacef).ForeColor = Nothing
        board(movetospacef).ForeColor = Color.Black
        '  If (BlackCheck() = True) Then
        ' If (picturea Is Nothing) Then
        'board(space).BackgroundImage = board(movetospace).BackgroundImage
        'board(movetospace).BackgroundImage = Nothing
        'board(movetospace).ForeColor = Nothing
        'board(space).ForeColor = Color.Black
        'End If
        'If Not (picturea Is Nothing) Then
        'board(space).BackgroundImage = board(movetospace).BackgroundImage
        'board(movetospace).BackgroundImage = picturea
        'board(movetospace).ForeColor = Color.White
        'board(space).ForeColor = Color.Black
        'End If
        'AImove()
        'Exit Sub
        'End If
        Button1.Enabled = False
        player = 1
        Label1.Text = firstplayer & "'s Turn"
        best = -10000000
        moves += 1
        Label3.Text = "Total Moves: " & moves
        Moved()
        PawnQueen()
        WinKing()
    End Sub

    Public Sub AiinCheck()
        Dim picture As Image

        picture = board(movetospace).BackgroundImage
        board(movetospace).BackgroundImage = board(space).BackgroundImage
        board(movetospace).ForeColor = Color.Black
        board(space).BackgroundImage = Nothing
        board(space).ForeColor = Nothing


        If (BlackCheck() = True) Then
            If (picture Is Nothing) Then
                board(space).BackgroundImage = board(movetospace).BackgroundImage
                board(movetospace).BackgroundImage = picture
                board(movetospace).ForeColor = Nothing
                board(space).ForeColor = Color.Black
            Else
                board(space).BackgroundImage = board(movetospace).BackgroundImage
                board(movetospace).BackgroundImage = picture
                board(movetospace).ForeColor = Color.White
                board(space).ForeColor = Color.Black
            End If
            Exit Sub
        End If
        If (picture Is Nothing) Then
            board(space).BackgroundImage = board(movetospace).BackgroundImage
            board(movetospace).BackgroundImage = picture
            board(movetospace).ForeColor = Nothing
            board(space).ForeColor = Color.Black
        Else
            board(space).BackgroundImage = board(movetospace).BackgroundImage
            board(movetospace).BackgroundImage = picture
            board(movetospace).ForeColor = Color.White
            board(space).ForeColor = Color.Black
        End If
        Calculate()
    End Sub

    Dim picturea As Image
    Public Function AIinDanger() As Boolean
        picturea = board(movetospace).BackgroundImage
        board(movetospace).BackgroundImage = board(space).BackgroundImage
        board(movetospace).ForeColor = Color.Black
        board(space).BackgroundImage = Nothing
        board(space).ForeColor = Nothing



        For i = 1 To 7
            If (movetospace + 8 * i < 65) Then
                If (board(movetospace + 8 * i).ForeColor = Color.White) And ((board(movetospace + 8 * i).BackgroundImage Is wr) Or (board(movetospace + 8 * i).BackgroundImage Is wq)) Then
                    DangerBack()
                    Return True
                End If
                If Not (board(movetospace + 8 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (movetospace - 8 * i > 0) Then
                If (board(movetospace - 8 * i).ForeColor = Color.White) And ((board(movetospace - 8 * i).BackgroundImage Is wr) Or (board(movetospace - 8 * i).BackgroundImage Is wq)) Then
                    DangerBack()
                    Return True
                End If
                If Not (board(movetospace - 8 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If ((movetospace - 1 * i) Mod 8 = 0) Then
                Exit For
            End If
            If (board(movetospace - 1 * i).ForeColor = Color.White) And ((board(movetospace - 1 * i).BackgroundImage Is wr) Or (board(movetospace - 1 * i).BackgroundImage Is wq)) Then
                DangerBack()
                Return True

            End If
            If Not (board(movetospace - 1 * i).BackgroundImage Is Nothing) Then
                Exit For
            End If
        Next i

        For i = 1 To 7
            If ((movetospace + 1 * i) Mod 8 = 1) Then
                Exit For
            End If
            If (board(movetospace + 1 * i).ForeColor = Color.White) And ((board(movetospace + 1 * i).BackgroundImage Is wr) Or (board(movetospace + 1 * i).BackgroundImage Is wq)) Then
                DangerBack()
                Return True
            End If
            If Not (board(movetospace + 1 * i).BackgroundImage Is Nothing) Then
                Exit For
            End If
        Next i

        For i = 1 To 7
            If (movetospace - 9 * i > 0) Then
                If ((movetospace - 9 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(movetospace - 9 * i).ForeColor = Color.White) And ((board(movetospace - 9 * i).BackgroundImage Is wb) Or (board(movetospace - 9 * i).BackgroundImage Is wq)) Then
                    DangerBack()
                    Return True

                End If
                If Not (board(movetospace - 9 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (movetospace + 9 * i < 65) Then
                If ((movetospace + 9 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(movetospace + 9 * i).ForeColor = Color.White) And ((board(movetospace + 9 * i).BackgroundImage Is wb) Or (board(movetospace + 9 * i).BackgroundImage Is wq)) Then
                    DangerBack()
                    Return True

                End If
                If Not (board(movetospace + 9 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (movetospace - 7 * i > 0) Then
                If ((movetospace - 7 * i) Mod 8 = 1) Then
                    Exit For
                End If
                If (board(movetospace - 7 * i).ForeColor = Color.White) And ((board(movetospace - 7 * i).BackgroundImage Is wb) Or (board(movetospace - 7 * i).BackgroundImage Is wq)) Then
                    DangerBack()
                    Return True

                End If
                If Not (board(movetospace - 7 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        For i = 1 To 7
            If (movetospace + 7 * i < 65) Then
                If ((movetospace + 7 * i) Mod 8 = 0) Then
                    Exit For
                End If
                If (board(movetospace + 7 * i).ForeColor = Color.White) And ((board(movetospace + 7 * i).BackgroundImage Is wb) Or (board(movetospace + 7 * i).BackgroundImage Is wq)) Then
                    DangerBack()
                    Return True

                End If
                If Not (board(movetospace + 7 * i).BackgroundImage Is Nothing) Then
                    Exit For
                End If
            End If
        Next i

        If (movetospace + 9 < 65) Then
            If (board(movetospace + 9).BackgroundImage Is wp) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace + 7 < 65) Then
            If (board(movetospace + 7).BackgroundImage Is wp) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace + 17 < 65) Then
            If (board(movetospace + 17).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace - 17 > 0) Then
            If (board(movetospace - 17).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace + 15 < 65) Then
            If (board(movetospace + 15).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace - 15 > 0) Then
            If (board(movetospace - 15).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace + 10 < 65) Then
            If (board(movetospace + 10).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace - 10 > 0) Then
            If (board(movetospace - 10).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace + 6 < 65) Then
            If (board(movetospace + 6).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If

        If (movetospace - 6 > 0) Then
            If (board(movetospace - 6).BackgroundImage Is wn) Then
                DangerBack()
                Return True
            End If
        End If
        DangerBack()
    End Function

    Public Sub DangerBack()
        If (picturea Is Nothing) Then
            board(space).BackgroundImage = board(movetospace).BackgroundImage
            board(movetospace).BackgroundImage = Nothing
            board(movetospace).ForeColor = Nothing
            board(space).ForeColor = Color.Black
        End If
        If Not (picturea Is Nothing) Then
            board(space).BackgroundImage = board(movetospace).BackgroundImage
            board(movetospace).BackgroundImage = picturea
            board(movetospace).ForeColor = Color.White
            board(space).ForeColor = Color.Black
        End If
    End Sub
End Class