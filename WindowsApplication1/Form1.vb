Imports System
Imports System.Text

Public Class Form1

    Dim rowCount As Integer = 0
    Dim timer_has_ticked As Boolean = False
    Dim x_man_cor As Integer
    Dim y_man_cor As Integer
    Dim openFilename As String

    Private DataPoints As New List(Of DataPoint)
    Private DownOnPoint As DataPoint = Nothing

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            PictureBox1.Load(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click REM load Data button
        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(OpenFileDialog2.FileName)
                openFilename = CStr(OpenFileDialog2.FileName)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim currentRow As Integer
                DataPoints.Clear()
                rowCount = 0
                While Not MyReader.EndOfData
                    Try
                        DataPoints.Add(New DataPoint(rowCount, currentRow, 10))
                        rowCount += 1
                    Catch ex As Microsoft.VisualBasic.
                                FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message &
                        "is not valid and will be skipped.")
                    End Try
                End While
                rowCount = DataPoints.Count
                NumericUpDown3.Minimum = 0
                NumericUpDown3.Maximum = rowCount
                NumericUpDown4.Minimum = 0
                NumericUpDown4.Maximum = rowCount
            End Using
        End If
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown

        If e.Button = MouseButtons.Left Then
            For Each dp As DataPoint In DataPoints
                If dp.Bounds.Contains(e.Location) Then
                    DownOnPoint = dp
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If DownOnPoint IsNot Nothing Then
            DownOnPoint.Location = e.Location
            PictureBox1.Refresh()
        End If
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        DownOnPoint = Nothing
        PictureBox1.Refresh()
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        For Each dp As DataPoint In DataPoints
            e.Graphics.FillEllipse(Brushes.Red, dp.Bounds)
        Next
    End Sub
End Class


Public Class DataPoint
    Public Property Location As Point

    Public ReadOnly Property Bounds As Rectangle
        Get
            Return New Rectangle(Location.X - (Size \ 2), Location.Y - (Size \ 2), Size, Size)
        End Get
    End Property

    Public ReadOnly Property Size As Integer

    Public Sub New(x As Integer, y As Integer, elpsSize As Integer)
        If elpsSize < 2 Then elpsSize = 2
        Size = elpsSize
        Location = New Point(x, y)
    End Sub
End Class

