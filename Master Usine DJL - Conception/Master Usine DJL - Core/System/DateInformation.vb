Public Class DateInformation

    Private _date As Date
    Private _csvFile As CSVFile
    Private _logFile As LOGFile
    Private _mdbFile As MDBFile
    Private _eventsFile As EventsFile
    Private _summaryDailyReport As SummaryDailyReport
    Private _completeDailyReport As CompleteDailyReport
    Private _summaryPeriodicReport As SummaryPeriodicReport
    Private _completePeriodicReport As CompletePeriodicReport
    Private _manualData As ManualData

    Public Sub New(_date As Date, _
                   csvFile As CSVFile, _
                   logFile As LOGFile, _
                   mdbFile As MDBFile, _
                   eventsFile As EventsFile, _
                   summaryDailyReport As SummaryDailyReport, _
                   completeDailyReport As CompleteDailyReport, _
                   summaryPeriodicReport As SummaryPeriodicReport, _
                   completePeriodicReport As CompletePeriodicReport, _
                   manualData As ManualData)

        Me._date = _date
        Me._csvFile = csvFile
        Me._logFile = logFile
        Me._mdbFile = mdbFile
        Me._eventsFile = eventsFile
        Me._summaryDailyReport = summaryDailyReport
        Me._completeDailyReport = completeDailyReport
        Me._summaryPeriodicReport = summaryPeriodicReport
        Me._completePeriodicReport = completePeriodicReport
        Me._manualData = manualData

    End Sub

    Public ReadOnly Property IsReportReady As Boolean
        Get

            Dim is_reportReady As Boolean = False

            With XmlSettings.Settings.instance.Usine.DataFiles

                If (.CSV.ACTIVE AndAlso .LOG.ACTIVE) Then
                    is_reportReady = Me.HasCSVFile AndAlso Me.HasLOGFile
                ElseIf (.CSV.ACTIVE) Then
                    is_reportReady = Me.HasCSVFile
                ElseIf (.LOG.ACTIVE) Then
                    is_reportReady = Me.HasLOGFile
                ElseIf (.MDB.ACTIVE) Then
                    is_reportReady = Me.HasMDBFile
                End If

            End With

            If (is_reportReady AndAlso XmlSettings.Settings.instance.Usine.Events.ACTIVE) Then
                is_reportReady = Me.HasEventsFile
            End If

            Return is_reportReady
        End Get
    End Property

    Public ReadOnly Property Date_ As Date
        Get
            Return Me._date
        End Get
    End Property

    Public ReadOnly Property CSVFile As CSVFile
        Get
            Return _csvFile
        End Get
    End Property

    Public ReadOnly Property LOGFile As LOGFile
        Get
            Return _logFile
        End Get
    End Property

    Public ReadOnly Property MDBFile As MDBFile
        Get
            Return _mdbFile
        End Get
    End Property

    Public ReadOnly Property EventsFile As EventsFile
        Get
            Return _eventsFile
        End Get
    End Property

    Public ReadOnly Property SummaryDailyReport As SummaryDailyReport
        Get
            Return _summaryDailyReport
        End Get
    End Property

    Public ReadOnly Property CompleteDailyReport As CompleteDailyReport
        Get
            Return _completeDailyReport
        End Get
    End Property

    Public ReadOnly Property SummaryPeriodicReport As SummaryPeriodicReport
        Get
            Return _summaryPeriodicReport
        End Get
    End Property

    Public ReadOnly Property CompletePeriodicReport As CompletePeriodicReport
        Get
            Return _completePeriodicReport
        End Get
    End Property

    Public ReadOnly Property ManualData As ManualData
        Get
            Return _manualData
        End Get
    End Property


    Public ReadOnly Property HasCSVFile As Boolean
        Get
            Return Not IsNothing(Me._csvFile)
        End Get
    End Property

    Public ReadOnly Property HasLOGFile As Boolean
        Get
            Return Not IsNothing(Me._logFile)
        End Get
    End Property

    Public ReadOnly Property HasMDBFile As Boolean
        Get
            ' #Todo
            Return False ' Not IsNothing(Me._mdbFile) andAlso me.mdbFile.HasCyclesFor(date)
        End Get
    End Property

    Public ReadOnly Property HasEventsFile As Boolean
        Get
            Return Not IsNothing(Me._eventsFile)
        End Get
    End Property

    Public ReadOnly Property HasSummaryDailyReport As Boolean
        Get
            Return Not IsNothing(Me._summaryDailyReport)
        End Get
    End Property

    Public ReadOnly Property HasCompleteDailyReport As Boolean
        Get
            Return Not IsNothing(Me._completeDailyReport)
        End Get
    End Property

    Public ReadOnly Property HasSummaryPeriodicReport As Boolean
        Get
            Return Not IsNothing(Me._summaryPeriodicReport)
        End Get
    End Property

    Public ReadOnly Property HasCompletePeriodicReport As Boolean
        Get
            Return Not IsNothing(Me._completePeriodicReport)
        End Get
    End Property

    Public ReadOnly Property HasCompleteManualData As Boolean
        Get
            ' #Todo
            Return False ' ManualData.iscomplete
        End Get
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean

        If (TypeOf obj Is DateInformation) Then

            Return DirectCast(obj, DateInformation).Date_.Equals(Me.Date_)

        Else
            Return False
        End If

    End Function

End Class
