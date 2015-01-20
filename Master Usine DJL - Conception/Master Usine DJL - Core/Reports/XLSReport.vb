Imports Microsoft.Office.Interop
Imports IGNIS.XmlSettings

''' <summary>
''' Class representing the Excel document in wich
''' the content of the files gathered by the asphalt
''' plant will be writen in.
''' </summary>
Public MustInherit Class XLSReport
    Implements IDisposable

    Public Shared ExcelApp As Excel.Application
    Protected xlsWorkbook As Excel.Workbook
    Protected xlsDataSheet As Excel.Worksheet

    ''' <summary>
    ''' Saves the new excel document.
    ''' </summary>
    Public Sub saveAs(fullPath As String)

        Try

            Me.xlsWorkbook.SaveAs(fullPath)

        Catch ex As System.Runtime.InteropServices.COMException

            If (UIExceptionHandler.instance.handle(New OpenedFileException(OpenedFileException.FileType.XLS, ex))) Then

                Me.saveAs(fullPath)

            End If

        End Try

    End Sub

    Protected Overridable Function formatData(value As Object) As Object

        If (IsNothing(value)) Then
            Return "-"

        ElseIf (TypeOf value Is TimeSpan) Then

            Return DirectCast(value, TimeSpan).TotalSeconds

        ElseIf (TypeOf value Is Date) Then

            Dim date_ = DirectCast(value, Date)

            If (date_.Hour = 0 And date_.Minute = 0 And date_.Second = 0) Then

                Return date_.ToString("yyyy-MM-dd")

            Else

                Return date_.ToString("HH:mm:ss")

            End If

        ElseIf (TypeOf value Is Double) Then

            If (Double.IsNaN(value)) Then
                Return Nothing
            Else
                ' 2 decimales dans le excel!
                Return Math.Round(value, 2)
            End If

        ElseIf (TypeOf value Is Boolean) Then

            If (value) Then
                Return XmlSettings.Settings.LANGUAGE.General.WordFor_Yes
            Else
                Return XmlSettings.Settings.LANGUAGE.General.WordFor_No
            End If

        ElseIf (TypeOf value Is String) Then

            If (Date.TryParse(value, New Date())) Then

                Return """" & value & """"
            Else
                Return value
            End If

        Else

            Return value

        End If

    End Function

    Protected Sub decorateDataSheet(workSheetName As String, organizedData As Object(,))

        Dim workSheet As Excel.Worksheet = xlsWorkbook.Worksheets(workSheetName)

        If (IsNothing(workSheet)) Then
            ' throw exc : sheet not found
            Debugger.Break()

        Else

            workSheet.Select()

        End If

        XLSReport.ExcelApp.Range("A4").Select()
        XLSReport.ExcelApp.ActiveWindow.FreezePanes = True

        Dim cell = ExcelApp.Range("B2")

        Dim offsetIndex As Integer
        Dim columnIndex = 0

        ExcelApp.Range(ExcelApp.Range("B2"), ExcelApp.Range("B2").Offset(1, organizedData.GetLength(1) - 1)).Select()
        With DirectCast(ExcelApp.Selection, Excel.Range)

            With .DisplayFormat.Font

                .Name = "Arial"
                .Size = 12
                .Bold = True

            End With

            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter

            With .Borders

                .LineStyle = Excel.XlLineStyle.xlContinuous
                .Weight = Excel.XlBorderWeight.xlMedium

            End With

        End With

        With ExcelApp.Range(ExcelApp.Range("B2"), ExcelApp.Range("B2").Offset(0, organizedData.GetLength(1) - 1))

            .DisplayFormat.Font.Name = "Arial"


            .Columns.ColumnWidth = 30 ' Explain that it is for the autofit to work better
            .Columns.EntireColumn.AutoFit()
            .Columns.EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

        End With

        XLSReport.ExcelApp.Cells.Select()

        With DirectCast(XLSReport.ExcelApp.Selection, Excel.Range)

            .Interior.Color = Drawing.Color.FromArgb(220, 220, 220)

        End With

        ' No -2 for the rows because of the merge
        With ExcelApp.Range(ExcelApp.Range("B2"), ExcelApp.Range("B2").Offset(organizedData.GetLength(0), organizedData.GetLength(1) - 1))

            .Interior.Color = Drawing.Color.White

        End With

        ExcelApp.Range(ExcelApp.Range("B4"), ExcelApp.Range("B4").Offset(organizedData.GetLength(0) - 1, organizedData.GetLength(1) - 1)).Select()

        With DirectCast(ExcelApp.Selection, Excel.Range)

            .Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            .Borders.Weight = Excel.XlBorderWeight.xlThin

            '.NumberFormat = "General"

        End With

        ' Merge top cells
        While (columnIndex < organizedData.GetLength(1))

            offsetIndex = 1

            ' While the column name in this cell is equal to the column name in the cell next to it
            While (cell.Value.Equals(cell.Offset(0, offsetIndex).Value))
                offsetIndex += 1
            End While

            If (offsetIndex = 1) Then

                ExcelApp.Range(cell, cell.Offset(1, 0)).Merge()

            Else

                ExcelApp.Range(cell, cell.Offset(0, offsetIndex - 1)).Merge()

            End If

            cell = cell.Offset(0, 1)

            columnIndex += offsetIndex

        End While

        DirectCast(workSheet.Rows(1), Excel.Range).RowHeight = 15
        DirectCast(workSheet.Rows(2), Excel.Range).RowHeight = 35
        DirectCast(workSheet.Rows(3), Excel.Range).RowHeight = 25

    End Sub

    Public Overridable Sub loadGraphics()

        Dim workSheet As Excel.Worksheet = xlsWorkbook.Worksheets(Settings.LANGUAGE.ExcelReport.GraphicsSheetName)

        If (IsNothing(workSheet)) Then
            ' throw exc : sheet not found
            Debugger.Break()
        Else

            workSheet.Select()

        End If

        Dim topMargin = ExcelApp.Range("B2").Top
        Dim leftMargin = ExcelApp.Range("B2").Left
        '                                                                                                                                                                                                                                               Original size divided by 2, to give a nice size
        Dim g1 = DirectCast(XLSReport.ExcelApp.ActiveSheet, Excel.Worksheet).Shapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 1200 / 2, 690 / 2)

        g1.Left = leftMargin
        g1.Top = topMargin

        Dim g2 = DirectCast(XLSReport.ExcelApp.ActiveSheet, Excel.Worksheet).Shapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ASPHALT_PERCENTAGE_GRAPHIC, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 1200 / 2, 690 / 2)

        g2.Top = g1.Top + g1.Height + topMargin
        g2.Left = leftMargin

        Dim g3 = DirectCast(XLSReport.ExcelApp.ActiveSheet, Excel.Worksheet).Shapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_GRAPHIC, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 1200 / 2, 690 / 2)

        g3.Top = g2.Top + g2.Height + topMargin
        g3.Left = leftMargin

        Me.decorateGraphicSheet()

    End Sub

    Protected Sub decorateGraphicSheet()

        DirectCast(xlsWorkbook.Worksheets(Settings.LANGUAGE.ExcelReport.GraphicsSheetName), Excel.Worksheet).Select()

        XLSReport.ExcelApp.Cells.Select()

        With DirectCast(XLSReport.ExcelApp.Selection, Excel.Range)

            .Interior.Color = Drawing.Color.FromArgb(220, 220, 220)

        End With

    End Sub

    ''' <summary>
    ''' Must absolutely dispose of the Excel documents or
    ''' they will stay open even after the program is closed.
    ''' </summary>
    ''' <remarks>
    ''' Be carefull, the False in the workbook.close() method
    ''' means it will not save it. This method is purely for
    ''' prevention, no saving or document modification should
    ''' be done here.
    ''' </remarks>
    Public Overridable Sub Dispose() Implements System.IDisposable.Dispose

        If (Not IsNothing(XLSReport.ExcelApp)) Then

            XLSReport.ExcelApp.Visible = False

            For Each workBook As Excel.Workbook In ExcelApp.Workbooks

                For Each workSheet As Excel.Worksheet In workBook.Worksheets

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet)
                    workSheet = Nothing
                Next

                workBook.Close(False)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook)
                workBook = Nothing

            Next

            Me.xlsWorkbook = Nothing

        End If

    End Sub

    Public Shared Sub killApp()
        If (Not IsNothing(ExcelApp)) Then
            XLSReport.ExcelApp.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(XLSReport.ExcelApp)
            XLSReport.ExcelApp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End If
    End Sub

    Public MustOverride Sub loadData()
    Public MustOverride Sub organizeData()
    Public MustOverride Function getDataFileNode() As DataFileNode

    Public MustOverride Function modelFileExist() As Boolean

End Class
