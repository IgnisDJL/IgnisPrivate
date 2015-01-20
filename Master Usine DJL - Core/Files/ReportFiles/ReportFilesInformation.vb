Public Class ReportFilesInformation

    Private _summaryDailyReport As SummaryDailyReport
    Private _summaryReadOnlyDailyReport As SummaryDailyReport
    Private _completeDailyReport As CompleteDailyReport
    Private _summaryPeriodicReports As List(Of SummaryPeriodicReport)
    Private _completePeriodicReports As List(Of CompletePeriodicReport)
    ' #todo
    ' Manual data report

    Public Sub addReport(report As ReportFile)

        If (Not IsNothing(report)) Then

            If (TypeOf report Is SummaryDailyReport) Then

                If (DirectCast(report, SummaryDailyReport).IS_READ_ONLY) Then
                    Me._summaryReadOnlyDailyReport = report
                Else
                    Me._summaryDailyReport = report
                End If

            ElseIf (TypeOf report Is CompleteDailyReport) Then
                ' Just set it
                Throw New NotImplementedException
            ElseIf (TypeOf report Is SummaryPeriodicReport) Then

                ' Check replace the one with the same start/end date
                Throw New NotImplementedException
            ElseIf (TypeOf report Is CompletePeriodicReport) Then
                ' Check replace the one with the same start/end date
                Throw New NotImplementedException
            End If
        End If

    End Sub

    Public ReadOnly Property SummaryDailyReport As SummaryDailyReport
        Get
            Return _summaryDailyReport
        End Get
    End Property

    Public ReadOnly Property SummaryReadOnlyDailyReport As SummaryDailyReport
        Get
            Return _summaryReadOnlyDailyReport
        End Get
    End Property

    Public ReadOnly Property CompleteDailyReport As CompleteDailyReport
        Get
            Return _completeDailyReport
        End Get
    End Property

    Public ReadOnly Property SummaryPeriodicReports As List(Of SummaryPeriodicReport)
        Get
            Return _summaryPeriodicReports
        End Get
    End Property

    Public ReadOnly Property CompletePeriodicReport As List(Of CompletePeriodicReport)
        Get
            Return _completePeriodicReports
        End Get
    End Property
End Class