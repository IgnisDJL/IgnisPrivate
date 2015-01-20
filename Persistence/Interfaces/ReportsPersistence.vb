Public MustInherit Class ReportsPersistence

    ''' <summary>Verifies that the format of the reports storage is correct.</summary>
    Public MustOverride Function verifyFormat() As Boolean

    ''' <summary>Removes all reports and resets the storage information in the correct format</summary>
    Public MustOverride Sub reset()

    Public MustOverride Sub addDailyReports(day As Date, summaryReportPath As String, summaryReadOnlyReportPath As String, completeReportPath As String)

    Public MustOverride Sub addPeriodicReports(startDate As Date, endDate As Date, summaryReportPath As String, summaryReadOnlyReportPath As String, completeReportPath As String)

    ''' <summary>Return's all the daily reports for that day</summary>
    Public MustOverride Function getDailyReports(day As Date) As List(Of ReportFile)

    ''' <summary>Return's all the summary daily reports between the startDate and the endDate</summary>
    Public MustOverride Function getSummaryDailyReports(startDate As Date, endDate As Date) As List(Of SummaryDailyReport)

    ''' <summary>Return's all the complete daily reports between the startDate and the endDate</summary>
    Public MustOverride Function getCompleteDailyReports(startDate As Date, endDate As Date) As List(Of CompleteDailyReport)

    ''' <summary>Return's all the periodic reports that contain that date in their interval</summary>
    Public MustOverride Function getPeriodicReports(day As Date) As List(Of ReportFile)

    ''' <summary>Return's all the summary periodic reports that match exactly the given interval</summary>
    Public MustOverride Function getSummaryPeriodicReport(startDate As Date, endDate As Date) As List(Of SummaryPeriodicReport)

    ''' <summary>Return's all the complete periodic reports that match exactly the given interval</summary>
    Public MustOverride Function getCompletePeriodicReport(startDate As Date, endDate As Date) As List(Of CompletePeriodicReport)

End Class
