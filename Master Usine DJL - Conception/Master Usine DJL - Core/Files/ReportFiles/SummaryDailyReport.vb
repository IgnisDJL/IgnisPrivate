Public Class SummaryDailyReport
    Inherits ReportFile

    ' #language
    Public Shared ReadOnly GENERIC_NAME As String = "Rapport journalier sommaire"
    Public Shared ReadOnly WRITABLE_EXTENSION As String = " (Word)"
    Public Shared ReadOnly READONLY_EXTENSION As String = " (PDF)"

    Public Sub New(filePath As String, isReadOnly As Boolean)
        MyBase.New(filePath, isReadOnly)
    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            ' Regex with name...
            Return Nothing
        End Get
    End Property
End Class
