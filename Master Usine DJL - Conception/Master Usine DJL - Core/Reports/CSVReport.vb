Imports Microsoft.Office.Interop

''' <summary>
''' Provides the methods to create the excel report based on the .csv data files
''' </summary>
''' <remarks></remarks>
Public Class CSVReport
    Inherits XLSReport

    Public cycleList As List(Of CSVCycle)

    Private organizedData As Object(,)
    Public ReadOnly Property ORGANIZED_DATA As Object(,)
        Get
            Return Me.organizedData
        End Get
    End Property


    ''' <summary>
    ''' Constructor without parameters
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(CSVCycleList As List(Of CSVCycle), Optional createDocument As Boolean = True)

        Me.cycleList = CSVCycleList

        If (createDocument) Then

            ' Progress Bar
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Génération du model)"
            ReportGenerationControl.instance.addStep(10)

            If (ProductionDay.generateModel OrElse Not CSVModel.fileExists) Then

                Try
                    Dim modelGenerator As New CSVModel(XLSReport.ExcelApp)
                    Me.xlsWorkbook = modelGenerator.generateModel()

                Catch ex As Threading.ThreadAbortException

                    Me.Dispose()

                End Try

            Else
                Me.xlsWorkbook = XLSReport.ExcelApp.Workbooks.Open(Constants.Paths.CSV_MODEL)

            End If

            XLSReport.ExcelApp.DisplayAlerts = False
            XLSReport.ExcelApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

        End If

    End Sub

    ''' <summary>
    ''' Loads the data from the .csv files gathered by the
    ''' asphalt plant into the excel report.
    ''' </summary>
    ''' <remarks>
    ''' Each files in the folder are read line by line.
    ''' To save processing time, we have to reduce the 
    ''' calls to the excel object.
    ''' </remarks>
    Public Overrides Sub loadData()

        ' Select the XLS table (Sheet)
        Me.xlsDataSheet = DirectCast(Me.xlsWorkbook.Worksheets(Constants.Output.Excel.DataSheet.SHEET_NAME), Excel.Worksheet)

        ' Set the starting cell
        Dim startingCell = Me.xlsDataSheet.Range(Constants.Output.Excel.DataSheet.DATA_TABLE_TOP_LEFT_CELL_NAME)

        ' Load the .cvs files and arrange it
        Me.organizeData()

        ' Set the range
        Dim dataTableRange = startingCell.Resize(Me.organizedData.GetLength(0), Me.organizedData.GetLength(1))

        ' Import values
        dataTableRange.Value = Me.organizedData

        decorateDataSheet(Constants.Output.Excel.DataSheet.SHEET_NAME, ORGANIZED_DATA)

    End Sub

    ''' <summary>
    ''' Organizes the raw data from the .csv files into data that will fit in the excel report.
    ''' </summary>
    ''' <remarks>
    ''' ProgressBar : 15 %
    ''' </remarks>
    Public Overrides Sub organizeData()

        Dim nbCycles = Me.cycleList.Count

        ' Calculate the number of cells the table header will occupy
        Dim nbColsReport = XmlSettings.Settings.instance.Report.Excel.CSV_SHEET.NB_COLUMNS

        ReDim Me.organizedData(nbCycles - 1, nbColsReport - 1)


        ' Progress Bar
        Dim progressBarStep As Double = 1 / nbColsReport * 15
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Organisation des données)"


        For i = 0 To nbColsReport - 1

            ReportGenerationControl.instance.addStep(progressBarStep)

            Dim reportColumn = XmlSettings.Settings.instance.Report.Excel.CSV_SHEET.getColumnByIndex(i)

            If (TypeOf reportColumn Is DataColumnInfo) Then

                Dim excelDataColumn = DirectCast(reportColumn, DataColumnInfo)

                Dim csvDataInfo = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getDataInfoByTag(excelDataColumn.TAG, excelDataColumn.index)

                Dim index As Integer = If(TypeOf csvDataInfo Is XmlSettings.DataInfoNode, DirectCast(csvDataInfo, XmlSettings.DataInfoNode).index, 0)

                For j = 0 To cycleList.Count - 1

                    Try
                        Dim value = cycleList.ElementAt(j).getData(csvDataInfo.TAG, index)
                        Me.organizedData(j, i) = formatData(csvDataInfo.UNIT.convert(value, excelDataColumn.UNIT))
                    Catch ex As Exception
                        UIExceptionHandler.instance.handle(ex)
                    End Try

                Next

            ElseIf (TypeOf reportColumn Is SubColumnInfo) Then

                Dim excelSubColumnInfo = DirectCast(reportColumn, SubColumnInfo)

                Dim csvSuperColumnInfo = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getFeedInfoByIndex(excelSubColumnInfo.SUPER_COLUMN.TAG, excelSubColumnInfo.SUPER_COLUMN.INDEX)

                Dim csvSubColumn As DataInfo = Nothing

                For Each csvSubColInfo In XmlSettings.Settings.instance.Usine.DataFiles.CSV.SUB_COLUMNS

                    If (csvSubColInfo.TAG.Equals(excelSubColumnInfo.TAG)) Then

                        csvSubColumn = csvSubColInfo

                        Exit For
                    End If

                Next

                For j = 0 To cycleList.Count - 1

                    Try
                        Dim value = cycleList(j).getData(csvSuperColumnInfo.TAG, csvSuperColumnInfo.INDEX, csvSubColumn.TAG)
                        Me.organizedData(j, i) = formatData(csvSubColumn.UNIT.convert(value, excelSubColumnInfo.UNIT))
                    Catch ex As Exception
                        UIExceptionHandler.instance.handle(ex)
                    End Try

                Next

            End If
        Next

    End Sub

    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function getDataFileNode() As DataFileNode
        Return XmlSettings.Settings.instance.Usine.DataFiles.CSV
    End Function


    Public Overrides Function modelFileExist() As Boolean
        Return New IO.FileInfo(Constants.Paths.CSV_MODEL).Exists
    End Function
End Class
