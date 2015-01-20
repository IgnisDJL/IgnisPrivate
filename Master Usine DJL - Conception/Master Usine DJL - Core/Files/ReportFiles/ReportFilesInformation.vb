Public Class ReportFilesInformation

    Private _summaryDailyReport As SummaryDailyReport
    Private _completeDailyReport As CompleteDailyReport
    Private _summaryPeriodicReport As SummaryPeriodicReport
    Private _completePeriodicReport As CompletePeriodicReport
    ' #todo
    ' Manual data report

    Public Sub New(summaryDailyReport As SummaryDailyReport, _
                   completeDailyReport As CompleteDailyReport, _
                   summaryPeriodicReport As SummaryPeriodicReport, _
                   completePeriodicReport As CompletePeriodicReport)


        Me._summaryDailyReport = summaryDailyReport
        Me._completeDailyReport = completeDailyReport
        Me._summaryPeriodicReport = summaryPeriodicReport
        Me._completePeriodicReport = completePeriodicReport

    End Sub

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
End Class