Public Class DailyReportFactory

    Public Sub New()

    End Sub

    Public Function createDailyReport(dateDebut As Date, dateFin As Date) As DailyReport

        Dim report As DailyReport

        report = New DailyReport(dateDebut, dateFin)

        Return report

    End Function


End Class
