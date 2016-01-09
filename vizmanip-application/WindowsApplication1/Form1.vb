Imports System
Imports System.Text
Public Class Form1
    Dim tableData As New Dictionary(Of Integer, String())
    Dim rowCount As Integer = 0
    Dim timer_has_ticked As Boolean = False
    Dim x_man_cor As Integer
    Dim y_man_cor As Integer
    Dim openFilename As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            PictureBox1.Load(OpenFileDialog1.FileName)
        End If

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName <> "" Then
            Dim sw As New System.IO.StreamWriter(SaveFileDialog1.FileName)
            Dim outputString As New String("")
            Dim currentRow As String()
            rowCount = tableData.Count()
            For i As Integer = 0 To rowCount - 1
                currentRow = tableData.Values(i)
                currentRow(x_man_cor) = NumericUpDown2.Value
                currentRow(y_man_cor) = NumericUpDown1.Value

                For Each currentField In currentRow
                    outputString = outputString & currentField
                    outputString = outputString & ","
                Next
                Dim sb As New StringBuilder()
                sb.AppendLine()
                outputString = outputString & sb.ToString()
            Next
            sw.Write(outputString)
            sw.Close()
            openFile(openFilename)
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub OpenFileDialog2_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog2.FileOk

    End Sub
    Private Sub openFile(filename)
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(CStr(filename))
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            tableData.Clear()
            rowCount = 0
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    tableData.Add(rowCount, currentRow)
                    rowCount += 1
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
            rowCount = tableData.Count
            NumericUpDown3.Minimum = 0
            NumericUpDown3.Maximum = rowCount
            NumericUpDown4.Minimum = 0
            NumericUpDown4.Maximum = rowCount
            display_points(tableData, rowCount)
        End Using
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click REM load Data button
        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(OpenFileDialog2.FileName)
                openFilename = CStr(OpenFileDialog2.FileName)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim currentRow As String()
                REM Dim tableData As New Dictionary(Of Integer, String())
                REM Dim rowCount As Integer = 0
                tableData.Clear()
                rowCount = 0
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        REM Dim currentField As String
                        tableData.Add(rowCount, currentRow)
                        rowCount += 1
                        REM For Each currentField In currentRow
                        REM MsgBox(currentField)
                        REM Next
                    Catch ex As Microsoft.VisualBasic.
                                FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message &
                        "is not valid and will be skipped.")
                    End Try
                End While
                REM MsgBox(rowCount)
                rowCount = tableData.Count
                REM MsgBox(rowCount)
                NumericUpDown3.Minimum = 0
                NumericUpDown3.Maximum = rowCount
                NumericUpDown4.Minimum = 0
                NumericUpDown4.Maximum = rowCount
                display_points(tableData, rowCount)
            End Using
        End If
    End Sub
    Private Sub redraw_points(sender As Object, e As EventArgs) Handles PictureBox1.Paint
        display_points(tableData, rowCount)

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PictureBox1.Refresh()
    End Sub
    Private Sub display_points(tableData As Dictionary(Of Integer, String()), rowCount As Integer)
        Dim currentRow As String()
        Dim nextRow As String()
        Dim x As Integer
        Dim y As Integer
        Dim x_auto_cor As Integer
        Dim y_auto_cor As Integer
        Dim p As New System.Drawing.Pen(Color.Red, 4)
        Dim p2 As New System.Drawing.Pen(Color.Red, 1)
        Dim p_man As New System.Drawing.Pen(Color.Blue, 4)
        Dim p2_man As New System.Drawing.Pen(Color.Blue, 1)
        Dim p_auto As New System.Drawing.Pen(Color.Green, 4)
        Dim p2_auto As New System.Drawing.Pen(Color.Green, 1)
        Dim g As System.Drawing.Graphics
        Dim f As New System.Drawing.Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold)
        Dim mybrush As New SolidBrush(Color.Black)
        Dim pathStart As Integer
        Dim pathEnd As Integer
        g = PictureBox1.CreateGraphics
        If tableData.Count > 0 Then
            currentRow = tableData.Values(0)
            REM find header data
            Dim fieldCounter As Integer = 0
            For Each currentField In currentRow
                If currentField = "x" Then
                    x = fieldCounter
                ElseIf currentField = "y" Then
                    y = fieldCounter
                ElseIf currentField = "x-man-cor" Then
                    x_man_cor = fieldCounter
                ElseIf currentField = "y-man-cor" Then
                    y_man_cor = fieldCounter
                ElseIf currentField = "x-auto-cor" Then
                    x_auto_cor = fieldCounter
                ElseIf currentField = "y-auto-cor" Then
                    y_auto_cor = fieldCounter
                End If
                fieldCounter += 1
            Next
            If NumericUpDown3.Value = 0 Or NumericUpDown4.Value = 0 Or NumericUpDown3.Value > NumericUpDown4.Value Then
                pathStart = 1
                pathEnd = rowCount - 1
            Else
                pathStart = NumericUpDown3.Value
                pathEnd = NumericUpDown4.Value
            End If
            For counter As Integer = pathStart To pathEnd
                currentRow = tableData.Values(counter)
                If counter < rowCount - 1 And counter > 0 Then
                    nextRow = tableData.Values(counter + 1)
                    REM Original
                    If CheckBox1.Checked = True Then
                        g.DrawEllipse(p, CInt(currentRow(x) - 2), CInt(currentRow(y) - 2), 4, 4)
                        g.DrawLine(p2, CSng(currentRow(x)), CSng(currentRow(y)), CSng(nextRow(x)), CSng(nextRow(y)))
                        If CheckBox4.Checked = True Then
                            g.DrawString(CStr(counter), f, mybrush, CSng(currentRow(x)), CSng(currentRow(y)))
                        End If
                    End If
                    REM Manual Corrections
                    If CheckBox2.Checked = True Then
                        g.DrawEllipse(p_man, CInt(currentRow(x_man_cor) - 2) + NumericUpDown2.Value, CInt(currentRow(y_man_cor) - 2) + NumericUpDown1.Value, 4, 4)
                        g.DrawLine(p2_man, CSng(currentRow(x_man_cor)) + NumericUpDown2.Value, CSng(currentRow(y_man_cor)) + NumericUpDown1.Value, CSng(nextRow(x_man_cor)) + NumericUpDown2.Value, CSng(nextRow(y_man_cor)) + NumericUpDown1.Value)
                        If CheckBox4.Checked = True Then
                            g.DrawString(CStr(counter), f, mybrush, CSng(currentRow(x_man_cor)) + NumericUpDown2.Value, CSng(currentRow(y_man_cor)) + NumericUpDown1.Value)

                        End If
                    End If
                    REM Auto Corrections
                    If CheckBox3.Checked = True Then
                        g.DrawEllipse(p_auto, CInt(currentRow(x_auto_cor) - 2), CInt(currentRow(y_auto_cor) - 2), 4, 4)
                        g.DrawLine(p2_auto, CSng(currentRow(x_auto_cor)), CSng(currentRow(y_auto_cor)), CSng(nextRow(x_auto_cor)), CSng(nextRow(y_auto_cor)))
                        If CheckBox4.Checked = True Then
                            g.DrawString(CStr(counter), f, mybrush, CSng(currentRow(x_auto_cor)), CSng(currentRow(y_auto_cor)))
                        End If
                    End If
                End If
            Next
        End If
        REM PictureBox1.Refresh()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
    Private Sub button4_click(sender As Object, e As EventArgs) Handles Button4.Click
        PictureBox1.Refresh()
        display_points(tableData, rowCount)
    End Sub
    Private Sub timer_tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        timer_has_ticked = True
    End Sub
    Private Sub numericUpDown3_click(sender As Object, e As EventArgs) Handles NumericUpDown3.Click
        PictureBox1.Refresh()
        display_points(tableData, rowCount)
    End Sub
    Private Sub numericUpDown4_click(sender As Object, e As EventArgs) Handles NumericUpDown4.Click
        PictureBox1.Refresh()
        display_points(tableData, rowCount)
    End Sub
    Private Sub numericUpDown2_click(sender As Object, e As EventArgs) Handles NumericUpDown2.Click
        PictureBox1.Refresh()
        display_points(tableData, rowCount)
    End Sub
    Private Sub numericUpDown1_click(sender As Object, e As EventArgs) Handles NumericUpDown1.Click
        PictureBox1.Refresh()
        display_points(tableData, rowCount)
    End Sub
End Class
