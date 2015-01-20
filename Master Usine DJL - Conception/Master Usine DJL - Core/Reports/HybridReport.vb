Imports Microsoft.Office.Interop

Public Class HybridReport
    Inherits XLSReport

    Private csvReport As CSVReport
    Private logReport As LOGReport

    ''' <summary>
    ''' Constructor without parameters
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(CSVCycleList As List(Of CSVCycle), LOGCycleList As List(Of LOGCycle))

        Me.csvReport = New CSVReport(CSVCycleList, False)
        Me.logReport = New LOGReport(LOGCycleList, False)

        ' Progress Bar
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Génération du model)"
        ReportGenerationControl.instance.addStep(5)

        If (ProductionDay.generateModel OrElse Not HybridModel.fileExists) Then

            Try
                Dim modelGenerator As New HybridModel(XLSReport.ExcelApp)
                Me.xlsWorkbook = modelGenerator.generateModel()

            Catch ex As Threading.ThreadAbortException

                Me.dispose()

            End Try

        Else
            Me.xlsWorkbook = XLSReport.ExcelApp.Workbooks.Open(Constants.Paths.HYBRID_MODEL)
        End If

        XLSReport.ExcelApp.DisplayAlerts = False
        XLSReport.ExcelApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

    End Sub

    Public Overrides Sub loadData()

        Me.organizeData()

        ' Select the XLS table (Sheet)
        Dim csvDataSheet = Me.xlsWorkbook.Worksheets(XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName_Csv)

        ' Set the starting cell <- remove
        Dim startingCell = csvDataSheet.Range(Constants.Output.Excel.DataSheet.CSV_DATA_TABLE_TOP_LEFT_CELL)

        ' Import data in excel sheet
        startingCell.Resize(Me.csvReport.ORGANIZED_DATA.GetLength(0), Me.csvReport.ORGANIZED_DATA.GetLength(1)).Value = Me.csvReport.ORGANIZED_DATA


        Dim logDataSheet = Me.xlsWorkbook.Worksheets(XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName_Log)

        startingCell = logDataSheet.Range(Constants.Output.Excel.DataSheet.LOG_DATA_TABLE_TOP_LEFT_CELL)

        ' Import data in excel sheet
        startingCell.Resize(Me.logReport.ORGANIZED_DATA.GetLength(0), Me.logReport.ORGANIZED_DATA.GetLength(1)).Value = Me.logReport.ORGANIZED_DATA

        Me.decorateDataSheet(XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName_Csv, Me.csvReport.ORGANIZED_DATA)
        Me.decorateDataSheet(XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName_Log, Me.logReport.ORGANIZED_DATA)

    End Sub

    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub organizeData()

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (organisation des données batch)"

        Me.csvReport.organizeData()

        ReportGenerationControl.instance.addStep(5)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (organisation des données continue)"

        Me.logReport.organizeData()

        ReportGenerationControl.instance.addStep(5)

    End Sub

    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub dispose()

        Me.logReport.Dispose()
        Me.csvReport.Dispose()

        MyBase.Dispose()

    End Sub

    Public Overrides Function getDataFileNode() As DataFileNode
        Return Nothing
    End Function

    Public Overrides Function modelFileExist() As Boolean
        Return New IO.FileInfo(Constants.Paths.HYBRID_MODEL).Exists
    End Function
End Class
