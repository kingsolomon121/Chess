Public Class Form1
    Dim board(64) As Button
    Dim col As Integer = 0

    Public Sub Colorer(i As Integer)
        If (col Mod 2 = 0) Then
            board(i).BackColor = Color.Maroon
        End If
        col += 1
    End Sub
    Dim clicked As Boolean = False
    Dim player As Integer = 1
    Public Sub CheckSpace(num As Integer)

        If Not (clicked) Then
            If Not (board(num).BackgroundImage Is Nothing) Then
                If (player = 1 And board(num).ForeColor = Color.White) Then
                    clicked = True
                    'player = 2
                    'Label1.Text = "Player 2's Turn"
                ElseIf (player = 2 And board(num).ForeColor = Color.Black) Then
                    'player = 1
                    clicked = True
                    'Label1.Text = "Player 1's Turn"
                End If
            End If
        Else
            clicked = False
            MovePiece(num)
        End If
    End Sub

    Public Sub MovePiece(num As Integer)
        If (board(num).BackgroundImage Is Nothing) Then
            MsgBox("Move Made")
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
            Colorer(i)
        Next i

        col = 1
        For i = 9 To 16
            board(i).Location = New Point(64 * (i - 9), 55)
            board(i).BackgroundImage = My.Resources.black_pawn
            board(i).ForeColor = Color.Black
            Colorer(i)
        Next i

        col = 0
        For i = 17 To 24
            board(i).Location = New Point(64 * (i - 17), 110)
            Colorer(i)
        Next i

        col = 1
        For i = 25 To 32
            board(i).Location = New Point(64 * (i - 25), 165)
            Colorer(i)
        Next i

        col = 0
        For i = 33 To 40
            board(i).Location = New Point(64 * (i - 33), 220)
            Colorer(i)
        Next i

        col = 1
        For i = 41 To 48
            board(i).Location = New Point(64 * (i - 41), 275)
            Colorer(i)
        Next i

        col = 0
        For i = 49 To 56
            board(i).Location = New Point(64 * (i - 49), 330)
            board(i).BackgroundImage = My.Resources.white_pawn
            board(i).ForeColor = Color.White
            Colorer(i)
        Next i

        col = 1
        For i = 57 To 64
            board(i).Location = New Point(64 * (i - 57), 385)
            Colorer(i)
        Next i

        board(1).BackgroundImage = My.Resources.black_rook
        board(8).BackgroundImage = My.Resources.black_rook
        board(2).BackgroundImage = My.Resources.black_knight
        board(7).BackgroundImage = My.Resources.black_knight
        board(3).BackgroundImage = My.Resources.black_bishop
        board(6).BackgroundImage = My.Resources.black_bishop
        board(4).BackgroundImage = My.Resources.black_king
        board(5).BackgroundImage = My.Resources.black_queen
        board(1).ForeColor = Color.Black
        board(8).ForeColor = Color.Black
        board(2).ForeColor = Color.Black
        board(7).ForeColor = Color.Black
        board(3).ForeColor = Color.Black
        board(6).ForeColor = Color.Black
        board(4).ForeColor = Color.Black
        board(5).ForeColor = Color.Black

        board(57).BackgroundImage = My.Resources.white_rook
        board(64).BackgroundImage = My.Resources.white_rook
        board(63).BackgroundImage = My.Resources.white_knight
        board(58).BackgroundImage = My.Resources.white_knight
        board(59).BackgroundImage = My.Resources.white_bishop
        board(62).BackgroundImage = My.Resources.white_bishop
        board(60).BackgroundImage = My.Resources.white_king
        board(61).BackgroundImage = My.Resources.white_queen
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
End Class
