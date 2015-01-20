Imports IGNIS.XmlSettings

Public Class MDBReport
    Inherits XLSReport

    Protected cycleList As List(Of MDBCycle)

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
    Public Sub New(MDBCycleList As List(Of MDBCycle))

        Me.cycleList = MDBCycleList

        ' Progress Bar
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Génération du model)"
        ReportGenerationControl.instance.addStep(5)

        ' Replace for settings.needtogeneratemodel so you dont have to regenerate the model when there is no need to
        If (ProductionDay.generateModel OrElse Not MDBModel.fileExists) Then

            Try
                Dim modelGenerator As New MDBModel(XLSReport.ExcelApp)
                Me.xlsWorkbook = modelGenerator.generateModel()

            Catch ex As Threading.ThreadAbortException

                Me.Dispose()

            End Try

        Else
            Me.xlsWorkbook = XLSReport.ExcelApp.Workbooks.Open(Constants.Paths.MDB_MODEL)

        End If

        XLSReport.ExcelApp.DisplayAlerts = False
        XLSReport.ExcelApp.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

    End Sub

    
    Public Overrides Sub loadData()

        ' Select the XLS table (Sheet)
        Me.xlsDataSheet = Me.xlsWorkbook.Worksheets(XmlSettings.Settings.LANGUAGE.ExcelReport.DataSheetName)

        ' Set the starting cell
        Dim startingCell = Me.xlsDataSheet.Range(Constants.Output.Excel.DataSheet.DATA_TABLE_TOP_LEFT_CELL_NAME)

        ' Load the .log files and arrange it
        Me.organizeData()

        startingCell.Resize(Me.organizedData.GetLength(0), Me.organizedData.GetLength(1)).Value = Me.organizedData

        Me.decorateDataSheet(Constants.Output.Excel.DataSheet.SHEET_NAME, ORGANIZED_DATA)

    End Sub


    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub organizeData()

        Dim nbCycles = Me.cycleList.Count

        ' Calculate the number of cells the table header will occupy
        Dim nbColsReport = XmlSettings.Settings.instance.Report.Excel.MDB_SHEET.NB_COLUMNS

        ReDim Me.organizedData(nbCycles - 1, nbColsReport - 1)

        ' Progress Bar
        Dim progressBarStep As Double = 1 / nbColsReport * 15
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Organisation des données)"


        For i = 0 To nbColsReport - 1

            ReportGenerationControl.instance.addStep(progressBarStep)

            Dim reportColumn = XmlSettings.Settings.instance.Report.Excel.MDB_SHEET.getColumnByIndex(i)

            If (TypeOf reportColumn Is DataColumnInfo) Then

                Dim excelDataColumn = DirectCast(reportColumn, DataColumnInfo)

                Dim mdbDataInfo = XmlSettings.Settings.instance.Usine.DataFiles.MDB.getDataInfoByTag(excelDataColumn.TAG)

                For j = 0 To cycleList.Count - 1

                    Try
                        Dim value = cycleList.ElementAt(j).getData(mdbDataInfo.TAG)
                        Me.organizedData(j, i) = formatData(mdbDataInfo.UNIT.convert(value, excelDataColumn.UNIT))
                    Catch ex As Exception
                        UIExceptionHandler.instance.handle(ex)
                    End Try

                Next

            ElseIf (TypeOf reportColumn Is SubColumnInfo) Then

                Dim excelSubColumnInfo = DirectCast(reportColumn, SubColumnInfo)

                Dim mdbSuperColumnInfo = XmlSettings.Settings.instance.Usine.DataFiles.MDB.getFeedInfoByIndex(excelSubColumnInfo.SUPER_COLUMN.TAG, excelSubColumnInfo.SUPER_COLUMN.INDEX)

                Dim mdbSubColumn As DataInfo = Nothing

                For Each mdbSubColInfo In XmlSettings.Settings.instance.Usine.DataFiles.MDB.SUB_COLUMNS

                    If (mdbSubColInfo.TAG.Equals(excelSubColumnInfo.TAG)) Then

                        mdbSubColumn = mdbSubColInfo

                        Exit For
                    End If

                Next

                For j = 0 To cycleList.Count - 1

                    Try
                        Dim value = cycleList(j).getData(mdbSuperColumnInfo.TAG, mdbSuperColumnInfo.INDEX, mdbSubColumn.TAG)
                        Me.organizedData(j, i) = formatData(mdbSubColumn.UNIT.convert(value, excelSubColumnInfo.UNIT))
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
        Return XmlSettings.Settings.instance.Usine.DataFiles.MDB
    End Function

    Public Overrides Function modelFileExist() As Boolean
        Return New IO.FileInfo(Constants.Paths.MDB_MODEL).Exists
    End Function
End Class
