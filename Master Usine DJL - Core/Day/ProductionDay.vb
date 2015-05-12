''' <summary>
''' Represents a production day at the asphalt plant. Provides access to methods to analyse datafiles and compute statistics for reports.
''' </summary>
''' <remarks>
''' Daily report generation sequence:
'''  1. Analyse data files
'''  2. Analyse event files (if needed)
'''  3. Pre-compile production statistics (they will be partially complete)
'''  4. Gather user input on production (currently known as manual data)
'''  5. Finalize compilation of production statistics
'''  6. Pre-compile events statistics
'''  7. Gather user input on delays
'''  8. Finalize compilation of event statistics
'''  9. Gather user input on report
'''  10. Generate report (compile report)
''' 
''' </remarks>
Public Class ProductionDay

    ' Attributes
    Private _date As Date
    Private _cycles As List(Of Cycle)
    Private _events As EventsCollection
    Private _statistics As Statistics ' Only thing that should be public (with date obviously) | should be production stats
    Private _manualData As ManualData
    Private _dataFileInfo As DataFilesInformation
    Private _reportFilesInfo As ReportFilesInformation

    Public Sub New(_date As Date)

        Me._date = _date

        Me._cycles = New List(Of Cycle)

        Me._reportFilesInfo = New ReportFilesInformation

    End Sub

    Public Sub analyseCSV()
        If (Me.DataFilesInfo.HasCSVFile) Then
            Me._cycles.AddRange(Me.DataFilesInfo.CSVFile.getCycles(Nothing, Nothing))
        End If
    End Sub

    Public Sub analyseLOG()
        If (Me.DataFilesInfo.HasLOGFile) Then
            Me._cycles.AddRange(Me.DataFilesInfo.LOGFile.getCycles(Nothing, Nothing))
        End If
    End Sub

    Public Sub analyseMDB()
        If (Me.DataFilesInfo.HasMDBFile) Then
            Me._cycles.AddRange(Me.DataFilesInfo.MDBFile.getCycles(Me._date, Me._date.AddDays(1)))
        End If
    End Sub

    Public Sub analyseEvents()

        If (Me.DataFilesInfo.HasEventsFile) Then

            Me._events = Me.DataFilesInfo.EventsFile.getEvents
        Else
            Me._events = New EventsCollection()
        End If
    End Sub

    ''' <remarks>
    ''' Precondition : DataFiles need to have been analysed
    ''' </remarks>
    Public Sub computeStatistics()

        Me._statistics = StatisticsFactory.getStatistics(XmlSettings.Settings.instance.Usine.DataFiles)

        Me._statistics.compute(Me._cycles, Me._events)

    End Sub

    ''' <summary>
    ''' Pre-computes delays statistics using analysed events and computed production statistics
    ''' </summary>
    ''' <remarks>
    ''' Precondition : Events have to have been analysed
    ''' Precondition : Production statistics have to have been fully computed
    ''' Sequence : After production statistics are fully computed. Before manual user input on delays
    ''' </remarks>
    Public Sub preComputeDelaysStatistics()

        Me.Statistics.EventsStatistics.preCompile(Me.ManualData.OPERATION_START_TIME, Me.ManualData.OPERATION_END_TIME, Me.Events.START_EVENTS, Me.Events.STOP_EVENTS)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Be sure this can be called twice. So things have to be resettable without having to call preComputeDelays (if possible)
    ''' </remarks>
    Public Sub finalizeDelayStatistics()
        Me.Statistics.EventsStatistics.finalizeCompilation()
    End Sub

    Public ReadOnly Property IsReportReady As Boolean
        Get

            Dim is_reportReady As Boolean = False

            With XmlSettings.Settings.instance.Usine.DataFiles

                If (.CSV.ACTIVE AndAlso .LOG.ACTIVE) Then
                    is_reportReady = Me.DataFilesInfo.HasCSVFile AndAlso Me.DataFilesInfo.HasLOGFile
                ElseIf (.CSV.ACTIVE) Then
                    is_reportReady = Me.DataFilesInfo.HasCSVFile
                ElseIf (.LOG.ACTIVE) Then
                    is_reportReady = Me.DataFilesInfo.HasLOGFile
                ElseIf (.MDB.ACTIVE) Then
                    is_reportReady = Me.DataFilesInfo.HasMDBFile
                End If

            End With

            If (is_reportReady AndAlso XmlSettings.Settings.instance.Usine.Events.ACTIVE) Then
                is_reportReady = Me.DataFilesInfo.HasEventsFile
            End If

            Return is_reportReady
        End Get
    End Property

    Public Property DataFilesInfo As DataFilesInformation
        Get
            Return Me._dataFileInfo
        End Get
        Set(value As DataFilesInformation)

            Me._dataFileInfo = value

            If (Not IsNothing(Me._dataFileInfo.MDBFile)) Then
                Me._dataFileInfo.MDBFile.setDate(Me.Date_)
            End If

        End Set
    End Property

    Public ReadOnly Property ReportFilesInfo As ReportFilesInformation
        Get
            Return Me._reportFilesInfo
        End Get
    End Property

    Public ReadOnly Property ManualData As ManualData
        Get
            If (IsNothing(_manualData)) Then
                _manualData = ProgramController.ManualDataPersistence.getData(Me.Date_)

                If (IsNothing(_manualData)) Then

                    _manualData = New ManualData(Me.Date_, Me.Statistics.CT01_ProductionStartTime, Me.Statistics.CT01_ProductionEndTime, Me.Statistics.ContinuousProduction.Quantity + Me.Statistics.DiscontinuousProduction.Quantity)

                End If
            End If

            Return _manualData
        End Get
    End Property

    Public ReadOnly Property Statistics As Statistics
        Get
            Return Me._statistics
        End Get
    End Property

    Public ReadOnly Property Events As EventsCollection
        Get
            Return _events
        End Get
    End Property

    Public Property KA01_Comments As String

    Public ReadOnly Property Date_ As Date
        Get
            Return Me._date
        End Get
    End Property
End Class
