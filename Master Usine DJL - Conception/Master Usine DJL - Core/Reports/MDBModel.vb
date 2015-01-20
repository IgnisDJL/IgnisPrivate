Imports IGNIS.XmlSettings

Public Class MDBModel
    Inherits XLSModel

    Private xlsApp As Microsoft.Office.Interop.Excel.Application

    Public Sub New(ByRef app As Microsoft.Office.Interop.Excel.Application)

        Me.xlsApp = app

    End Sub

    Public Overrides Function generateModel() As Microsoft.Office.Interop.Excel.Workbook

        ' Delete the file if it already existed
        If (MDBModel.fileExists) Then

            Dim retryOpeningFile As Boolean
            Do
                retryOpeningFile = False
                Try

                    System.IO.File.Delete(Constants.Paths.MDB_MODEL)

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
        Dim xlsWorkbook = Me.xlsApp.Workbooks.Add

        ' Retreive default worksheets
        Dim defaultSheets As New ArrayList
        For Each sheet In xlsWorkbook.Worksheets
            defaultSheets.Add(sheet)
        Next

        ' Create my sheet before deleting the others because you have to have at least 1 sheet open
        Dim GraphicsSheet = DirectCast(xlsWorkbook.Worksheets.Add, Microsoft.Office.Interop.Excel.Worksheet)

        GraphicsSheet.Name = XmlSettings.Settings.LANGUAGE.ExcelReport.GraphicsSheetName

        Dim xlsDataSheet = DirectCast(xlsWorkbook.Worksheets.Add, Microsoft.Office.Interop.Excel.Worksheet)

        xlsDataSheet.Name = Constants.Output.Excel.DataSheet.SHEET_NAME

        ' Delete the default worksheets
        For Each sheet As Microsoft.Office.Interop.Excel.Worksheet In defaultSheets
            sheet.Delete()
        Next

        Dim sheetSettings = XmlSettings.Settings.instance.Report.Excel.MDB_SHEET

        ' Calculate the number of cells the table header will occupy
        Dim nbTotalColumns = sheetSettings.NB_COLUMNS

        Dim tableHeader(2, nbTotalColumns) As String

        Dim column As ExcelColumn
        For i = 0 To nbTotalColumns - 1

            column = sheetSettings.getColumnByIndex(i)

            If (TypeOf column Is DataColumnInfo) Then

                Dim dataColumn = DirectCast(column, DataColumnInfo)

                tableHeader(0, i) = dataColumn.COLUMN_NAME
                tableHeader(1, i) = dataColumn.COLUMN_NAME

            ElseIf (TypeOf column Is SubColumnInfo) Then

                Dim subcolumn = DirectCast(column, SubColumnInfo)

                tableHeader(0, i) = subcolumn.SUPER_COLUMN.COLUMN_NAME
                tableHeader(1, i) = subcolumn.COLUMN_NAME

            End If

        Next

        ' Insert the data table header
        xlsDataSheet.Range("B2").Resize(2, nbTotalColumns).Value = tableHeader

        ' Set the data table top left cell name
        xlsDataSheet.Range("B4").Name = Constants.Output.Excel.DataSheet.DATA_TABLE_TOP_LEFT_CELL_NAME

        Me.saveAs(xlsWorkbook, Constants.Paths.MDB_MODEL, Constants.Output.Excel.REPORT_EXTENSION)

        Return xlsWorkbook

    End Function

    Public Shared Function fileExists() As Boolean
        Return System.IO.File.Exists(Constants.Paths.MDB_MODEL)
    End Function

End Class
