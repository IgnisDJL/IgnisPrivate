
Public Class SummaryDailyReport
    Inherits ReportFile

    ' #language
    Public Shared ReadOnly GENERIC_NAME As String = "Rapport journalier sommaire"
    Public Shared ReadOnly WRITABLE_EXTENSION As String = " (Word)"
    Public Shared ReadOnly READONLY_EXTENSION As String = " (PDF)"

    Private day As Date

    Public Sub New(day As Date, filePath As String, isReadOnly As Boolean)
        MyBase.New(filePath, isReadOnly)

        Me.day = day
    End Sub

    Public Overrides Function ToString() As String
        Return GENERIC_NAME & If(IS_READ_ONLY, READONLY_EXTENSION, WRITABLE_EXTENSION)
    End Function

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return Me.day
        End Get
    End Property
End Class
