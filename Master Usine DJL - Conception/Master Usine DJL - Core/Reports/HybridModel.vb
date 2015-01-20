Imports IGNIS.XmlSettings

Public Class HybridModel
    Inherits XLSModel

    Private xlsApp As Microsoft.Office.Interop.Excel.Application

    Public Sub New(ByRef app As Microsoft.Office.Interop.Excel.Application)

        Me.xlsApp = app

    End Sub

    Public Overrides Function generateModel() As Microsoft.Office.Interop.Excel.Workbook

        ' Delete the file if it already existed
        If (HybridModel.fileExists) Then

            Dim retryOpeningFile As Boolean
            Do
                retryOpeningFile = False
                Try

                    System.IO.File.Delete(Constants.Paths.HYBRID_MODEL)

                Catch ex As IO.IOException

                    ' Throw model is opened exception
                    If (UIExceptionHandler.instance.handle(New OpenedFileException(OpenedFileException.FileType.Model_XLS, ex))) Then
                        retryOpeningFile = True
                    Else

                        Dim del As ReportGenerationControl.CancelReportFromInsideDelegateMethod = AddressOf ReportGenerationControl.instance.cancelReportMaking
                        MainWindow.instance.Invoke(del)

                    End If

                End Try

            Loop While (retryOpeningFile)

        End If

        ' Create new file
        Dim xlsWorkbook = xlsApp.Workbooks.Add

        ' Retreive default worksheets
        Dim defaultSheets As New ArrayList
        For Each sheet In xlsWorkbook.Worksheets
            defaultSheets.Add(sheet)
        Next

        Dim graphicsSheet = DirectCast(xlsWorkbook.Worksheets.Add, Microsoft.Office.Interop.Excel.Worksheet)

        graphicsSheet.Name = XmlSettings.Settings.LANGUAGE.ExcelReport.GraphicsSheetName

        ' Create the log data sheet before deleting the others because you have to have at least 1 sheet open
        Dim xlsDataSheet_LOG = DirectCast(xlsWorkbook.Worksheets.Add, Microsoft.Office.Interop.Excel.Worksheet)

        xlsDataSheet_LOG.Name = XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName_Log

        ' Create the csv data sheet before deleting the others because you have to have at least 1 sheet open
        Dim xlsDataSheet_CSV = DirectCast(xlsWorkbook.Worksheets.Add, Microsoft.Office.Interop.Excel.Worksheet)

        xlsDataSheet_CSV.Name = XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName_Csv

        ' Delete the default worksheets
        For Each sheet As Microsoft.Office.Interop.Excel.Worksheet In defaultSheets
            sheet.Delete()
        Next

        'CSV
        Dim csvSheetSettings = XmlSettings.Settings.instance.Report.Excel.CSV_SHEET

        ' Calculate the number of cells the table header will occupy
        Dim nbTotalColumns = csvSheetSettings.NB_COLUMNS

        Dim tableHeader(2, nbTotalColumns) As String

        'Dim column As XmlSettings.ExcelNode.ColumnInfo
        'For i = 0 To nbTotalColumns - 1

        '    column = csvSheetSettings.getColumnByIndex(i)

        '    If (TypeOf column Is ExcelNode.SuperColumnInfo) Then

        '        tableHeader(0, i) = column.DISPLAY_NAME
        '        tableHeader(1, i) = column.DISPLAY_NAME

        '    ElseIf (TypeOf column Is ExcelNode.SubColumnInfo) Then

        '        Dim subcolumn = DirectCast(column, ExcelNode.SubColumnInfo)

        '        tableHeader(0, i) = subcolumn.SUPER_COLUMN.DISPLAY_NAME
        '        tableHeader(1, i) = subcolumn.DISPLAY_NAME

        '    End If

        'Next

        'xlsDataSheet_CSV.Range("B2").Resize(2, nbTotalColumns).Value = tableHeader

        '' LOG
        'Dim logSheetSettings = XmlSettings.Settings.instance.Report.Excel.LOG_SHEET

        'nbTotalColumns = logSheetSettings.NB_COLUMNS

        'ReDim tableHeader(2, nbTotalColumns)

        'For i = 0 To nbTotalColumns - 1

        '    column = logSheetSettings.getColumnByIndex(i)

        '    If (TypeOf column Is XmlSettings.ExcelNode.SuperColumnInfo) Then

        '        tableHeader(0, i) = column.DISPLAY_NAME
        '        tableHeader(1, i) = column.DISPLAY_NAME

        '    ElseIf (TypeOf column Is ExcelNode.SubColumnInfo) Then

        '        Dim subcolumn = DirectCast(column, ExcelNode.SubColumnInfo)

        '        tableHeader(0, i) = subcolumn.SUPER_COLUMN.DISPLAY_NAME
        '        tableHeader(1, i) = subcolumn.DISPLAY_NAME

        '    End If

        'Next

        xlsDataSheet_LOG.Range("B2").Resize(2, nbTotalColumns).Value = tableHeader

        ' Set the data table top left cell name for the csv sheet
        xlsDataSheet_CSV.Range("B4").Name = Constants.Output.Excel.DataSheet.CSV_DATA_TABLE_TOP_LEFT_CELL
        ' Set the data table top left cell name for the log sheet
        xlsDataSheet_LOG.Range("B4").Name = Constants.Output.Excel.DataSheet.LOG_DATA_TABLE_TOP_LEFT_CELL

        Me.saveAs(xlsWorkbook, Constants.Paths.HYBRID_MODEL, Constants.Output.Excel.REPORT_EXTENSION)

        Return xlsWorkbook

    End Function

    Public Shared Function fileExists() As Boolean
        Return System.IO.File.Exists(Constants.Paths.HYBRID_MODEL)
    End Function

End Class
