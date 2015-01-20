Public Class CompleteDailyReport
    Inherits ReportFile

    ' #language
    Public Shared ReadOnly GENERIC_NAME As String = "Rapport journalier complet"
    Public Shared ReadOnly EXTENSION As String = " (Excel)"

    Public Sub New(filePath As String)
        MyBase.New(filePath, False)
    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            ' Regex with name...
            Return Nothing
        End Get
    End Property
End Class
