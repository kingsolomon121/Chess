Public Class Form1
    Dim board(64) As Button
    Dim col As Integer = 0

    Public Sub Colorer(i As Integer)
        If (col Mod 2 = 0) Then
            board(i).BackColor = Color.Maroon
        End If
        col += 1
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 1 To 64

            board(i) = New Button
            Me.Controls.Add(board(i))
            board(i).Size = New System.Drawing.Size(64, 55)
            board(i).Text = i
            board(i).BackgroundImageLayout = ImageLayout.Tile
            board(i).FlatStyle = FlatStyle.Standard

        Next i

        For i = 1 To 8
            board(i).Location = New Point(64 * (i - 1), 0)
            Colorer(i)
        Next i

        col = 1
        For i = 9 To 16
            board(i).Location = New Point(64 * (i - 9), 55)
            board(i).BackgroundImage = My.Resources.black_pawn
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
            Colorer(i)
        Next i

        col = 1
        For i = 57 To 64
            board(i).Location = New Point(64 * (i - 57), 385)
            Colorer(i)
        Next i

    End Sub
End Class
