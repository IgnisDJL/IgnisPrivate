Imports IGNIS.XmlSettings

''' <summary>
''' Provides the methods to create the excel report based on the .log data files
''' </summary>
''' <remarks></remarks>
Public Class LOGReport
    Inherits XLSReport

    Public cycleList As List(Of LOGCycle)

    Private organizedData As Object(,)
    Public ReadOnly Property ORGANIZED_DATA As Object(,)
        Get
            Return Me.organizedData
        End Get
    End Property


    Public Sub New(LOGCycleList As List(Of LOGCycle), Optional createDocument As Boolean = True)

        Me.cycleList = LOGCycleList

        If (createDocument) Then
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Génération du model)"
            ReportGenerationControl.instance.addStep(10)

            If (ProductionDay.generateModel OrElse Not LOGModel.fileExists) Then

                Try
                    Dim modelGenerator As New LOGModel(XLSReport.ExcelApp)
                    Me.xlsWorkbook = modelGenerator.generateModel()

                Catch ex As Threading.ThreadAbortException

                    Me.Dispose()

                End Try

            Else
                Me.xlsWorkbook = XLSReport.ExcelApp.Workbooks.Open(Constants.Paths.LOG_MODEL)
            End If

            XLSReport.ExcelApp.DisplayAlerts = False
            XLSReport.ExcelApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        End If

    End Sub

    ''' <summary>
    ''' Loads the data from the .log files gathered by the
    ''' asphalt plant into the excel report.
    ''' </summary>
    ''' <remarks>
    ''' Each files in the folder are read line by line.
    ''' To save processing time, we have to reduce the 
    ''' calls to the excel object.
    ''' </remarks>
    Public Overrides Sub loadData()

        ' Select the XLS table (Sheet)
        Me.xlsDataSheet = Me.xlsWorkbook.Worksheets(Constants.Output.Excel.DataSheet.SHEET_NAME)

        ' Set the starting cell
        Dim startingCell = Me.xlsDataSheet.Range(Constants.Output.Excel.DataSheet.DATA_TABLE_TOP_LEFT_CELL_NAME)

        ' Load the .log files and arrange it
        Me.organizeData()

        ' Import data in excel sheet
        startingCell.Resize(Me.organizedData.GetLength(0), Me.organizedData.GetLength(1)).Value = Me.organizedData


        Me.decorateDataSheet(XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName, ORGANIZED_DATA)

    End Sub

    ''' <summary>
    ''' Organizes the raw data from the .log files into data that will fit in the excel report
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub organizeData()

        Dim nbCycles = Me.cycleList.Count

        ' Calculate the number of cells the table header will occupy
        Dim nbColsReport = XmlSettings.Settings.instance.Report.Excel.LOG_SHEET.NB_COLUMNS

        ReDim Me.organizedData(nbCycles - 1, nbColsReport - 1)

        For i = 0 To nbColsReport - 1

            Dim reportColumn = XmlSettings.Settings.instance.Report.Excel.LOG_SHEET.getColumnByIndex(i)

            If (TypeOf reportColumn Is DataColumnInfo) Then

                Dim excelDataColumn = DirectCast(reportColumn, DataColumnInfo)

                Dim logDataInfo = XmlSettings.Settings.instance.Usine.DataFiles.LOG.getDataInfoByTag(excelDataColumn.TAG)

                For j = 0 To cycleList.Count - 1

                    Try
                        Dim value = cycleList.ElementAt(j).getData(logDataInfo.TAG)
                        Me.organizedData(j, i) = formatData(logDataInfo.UNIT.convert(value, excelDataColumn.UNIT))
                    Catch ex As Exception
                        UIExceptionHandler.instance.handle(ex)
                    End Try

                Next

            ElseIf (TypeOf reportColumn Is SubColumnInfo) Then

                Dim excelSubColumnInfo = DirectCast(reportColumn, SubColumnInfo)

                Dim logSuperColumnInfo = XmlSettings.Settings.instance.Usine.DataFiles.LOG.getFeedInfoByIndex(excelSubColumnInfo.SUPER_COLUMN.TAG, excelSubColumnInfo.SUPER_COLUMN.INDEX)

                Dim logSubColumn As DataInfo = Nothing

                For Each logSubColInfo In XmlSettings.Settings.instance.Usine.DataFiles.LOG.SUB_COLUMNS

                    If (logSubColInfo.TAG.Equals(excelSubColumnInfo.TAG)) Then

                        logSubColumn = logSubColInfo

                        Exit For
                    End If

                Next

                For j = 0 To cycleList.Count - 1

                    Try
                        Dim value = cycleList(j).getData(logSuperColumnInfo.TAG, logSuperColumnInfo.INDEX, logSubColumn.TAG)
                        Me.organizedData(j, i) = formatData(logSubColumn.UNIT.convert(value, excelSubColumnInfo.UNIT))
                    Catch ex As Exception
                        UIExceptionHandler.instance.handle(ex)
                    End Try

                Next

            End If
        Next

    End Sub ' End organizeData

    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getDataFileNode() As DataFileNode
        Return XmlSettings.Settings.instance.Usine.DataFiles.LOG
    End Function

    Public Overrides Function modelFileExist() As Boolean
        Return New IO.FileInfo(Constants.Paths.LOG_MODEL).Exists
    End Function
End Class
