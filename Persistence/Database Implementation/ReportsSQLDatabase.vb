Imports System.IO
Imports IGNIS.Constants.Database.ReportsDB

Public Class ReportsSQLDatabase
    Inherits ReportsPersistence

    Private database As SQLiteAdapter

    Public Sub New(databaseAdapter As SQLiteAdapter)

        Me.database = databaseAdapter

    End Sub

    Public Overrides Sub addDailyReports(day As Date, summaryReportPath As String, summaryReadOnlyReportPath As String, completeReportPath As String)

        Dim row As New Dictionary(Of String, String)
        row.Add(Columns.DATE_, day.ToString(Constants.Database.SQL.DATE_FORMAT))

        If (Not IsNothing(summaryReportPath)) Then
            summaryReportPath = summaryReportPath.Replace(Constants.Paths.PROGRAM_ROOT, "")
            row.Add(Columns.SUMMARY_DAILY_REPORT_PATH, database.preventSQLInjection(summaryReportPath))
        End If

        If (Not IsNothing(summaryReadOnlyReportPath)) Then
            summaryReadOnlyReportPath = summaryReadOnlyReportPath.Replace(Constants.Paths.PROGRAM_ROOT, "")
            row.Add(Columns.SUMMARY_READONLY_DAILY_REPORT_PATH, database.preventSQLInjection(summaryReadOnlyReportPath))
        End If

        If (Not IsNothing(completeReportPath)) Then
            completeReportPath = completeReportPath.Replace(Constants.Paths.PROGRAM_ROOT, "")
            row.Add(Columns.COMPLETE_DAILY_REPORT_PATH, database.preventSQLInjection(completeReportPath))
        End If

        If (CInt(database.ExecuteScalar("SELECT COUNT(*) FROM " & TableNames.DAILY_REPORTS & " WHERE " & Columns.DATE_ & "='" & day.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")) > 0) Then
            database.Update(TableNames.DAILY_REPORTS, row, Columns.DATE_ & "='" & day & "'")
        Else
            database.Insert(TableNames.DAILY_REPORTS, row)
        End If

    End Sub

    Public Overrides Sub addPeriodicReports(startDate As Date, endDate As Date, summaryReportPath As String, summaryReadOnlyReportPath As String, completeReportPath As String)
        Throw New NotImplementedException
    End Sub

    Public Overrides Function getCompleteDailyReports(startDate As Date, endDate As Date) As List(Of CompleteDailyReport)

        'Dim allReports As New List(Of CompleteDailyReport)

        'Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.DAILY_REPORTS & " WHERE " & Columns.DATE_ & "BETWEEN '" & startDate.ToString(Constants.Database.SQL.DATE_FORMAT) & "' AND '" & startDate.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")
        Throw New NotImplementedException

        Return Nothing
    End Function

    Public Overrides Function getCompletePeriodicReport(startDate As Date, endDate As Date) As List(Of CompletePeriodicReport)
        Throw New NotImplementedException
    End Function

    Public Overrides Function getDailyReports(day As Date) As List(Of ReportFile)

        Dim allReports As New List(Of ReportFile)

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.DAILY_REPORTS & " WHERE " & Columns.DATE_ & "='" & day.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")

        If (dataTable.Rows.Count > 0) Then

            If (Not DBNull.Value.Equals(dataTable.Rows(0)(Columns.SUMMARY_DAILY_REPORT_PATH))) Then
                allReports.Add(New SummaryDailyReport(day, Constants.Paths.PROGRAM_ROOT & dataTable.Rows(0)(Columns.SUMMARY_DAILY_REPORT_PATH), False))
            End If

            If (Not DBNull.Value.Equals(dataTable.Rows(0)(Columns.SUMMARY_READONLY_DAILY_REPORT_PATH))) Then
                allReports.Add(New SummaryDailyReport(day, Constants.Paths.PROGRAM_ROOT & dataTable.Rows(0)(Columns.SUMMARY_READONLY_DAILY_REPORT_PATH), True))
            End If

        End If

        Return allReports
    End Function

    Public Overrides Function getPeriodicReports(day As Date) As List(Of ReportFile)
        Throw New NotImplementedException
    End Function

    Public Overrides Function getSummaryDailyReports(startDate As Date, endDate As Date) As List(Of SummaryDailyReport)
        Throw New NotImplementedException
    End Function

    Public Overrides Function getSummaryPeriodicReport(startDate As Date, endDate As Date) As List(Of SummaryPeriodicReport)
        Throw New NotImplementedException
    End Function

    Public Overrides Sub reset()

        database.ClearTable(TableNames.DAILY_REPORTS)

        verifyFormat()
    End Sub

    Public Overrides Function verifyFormat() As Boolean

        Try

            Dim summaryReportsDirectory As New DirectoryInfo(Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY)

            If (Not summaryReportsDirectory.Exists) Then
                summaryReportsDirectory.Create()
            End If

            Dim tableColumns As Dictionary(Of String, String)

            If (Not database.tableExists(TableNames.DAILY_REPORTS)) Then

                tableColumns = New Dictionary(Of String, String)
                tableColumns.Add(Columns.DATE_, "DATE")
                tableColumns.Add(Columns.SUMMARY_DAILY_REPORT_PATH, "VARCHAR(63)")
                tableColumns.Add(Columns.SUMMARY_READONLY_DAILY_REPORT_PATH, "VARCHAR(63)")
                tableColumns.Add(Columns.COMPLETE_DAILY_REPORT_PATH, "VARCHAR(63)")

                database.createTable(TableNames.DAILY_REPORTS, tableColumns)

                Console.WriteLine(TableNames.DAILY_REPORTS & " table was created : ")
                Me.database.printTable(TableNames.DAILY_REPORTS)
            End If

        Catch ex As Exception

            UIExceptionHandler.instance.handle(ex)

            Return False
        End Try

        Return True

    End Function

End Class
