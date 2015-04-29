Public Class SummaryDailyReportFormater
    Inherits ReportFormater

    Private Shared ReadOnly TIME_FORMAT As String = "HH:mm"
    Private Shared ReadOnly DURATION_FORMAT As String = "hh\hmm"
    Private Shared ReadOnly SHORT_DATE_FORMAT As String = "d MMMM yyyy"
    Private Shared ReadOnly FULL_DATE_FORMAT As String = "dd MMMM yyyy"
    Private Shared ReadOnly DATE_TIME_FORMAT As String = FULL_DATE_FORMAT & " " & TIME_FORMAT


    Public Sub New()
        MyBase.New(TIME_FORMAT, _
                   DURATION_FORMAT, _
                   SHORT_DATE_FORMAT, _
                   FULL_DATE_FORMAT, _
                   DATE_TIME_FORMAT, _
                   UNKNOWN_VALUE_CHARACTER, _
                   INVALID_VALUE_CHARACTER)

    End Sub

End Class
