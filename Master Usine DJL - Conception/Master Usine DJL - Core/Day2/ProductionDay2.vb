Public Class ProductionDay2

    ' Attributes
    Private _date As Date
    Private _cycles As List(Of Cycle)
    Private _events As EventsCollection
    Private _statistics As Statistics
    Private _manualData As ManualData

    Public Sub New(_date As Date)

        Me._date = _date

        Me._cycles = New List(Of Cycle)

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

    Public Sub analyseEvents()
        If (Me.DataFilesInfo.HasEventsFile) Then
            Me._events = Me.DataFilesInfo.EventsFile.getEvents
        End If
    End Sub

    ''' <remarks>
    ''' Precondition : DataFiles need to have been analysed
    ''' </remarks>
    Public Sub computeStatistics()

        Me._statistics = StatisticsFactory.getStatistics(XmlSettings.Settings.instance.Usine.DataFiles)

        Me._statistics.compute(Me._cycles, Me._events)

    End Sub


    ''' <remarks>
    ''' Precondition : Events have to have been computed
    ''' </remarks>
    Public Sub computeDelays()

        If (Me.Events.DELAYS.Count = 0) Then
            Me._events.computeDelays(ManualData.OPERATION_START_TIME, ManualData.OPERATION_END_TIME)
        End If

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

    Public Property ReportFilesInfo As ReportFilesInformation

    Public ReadOnly Property ManualData As ManualData
        Get
            If (IsNothing(_manualData)) Then
                _manualData = ProgramController.ManualDataPersistence.getData(Me.Date_)

                If (IsNothing(_manualData)) Then

                    _manualData = New ManualData(Me.Date_, Me.Statistics.ProductionStartTime, Me.Statistics.ProductionEndTime, Me.Statistics.ContinuousProduction.Quantity + Me.Statistics.DiscontinuousProduction.Quantity)

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

    Public ReadOnly Property Date_ As Date
        Get
            Return Me._date
        End Get
    End Property
End Class
