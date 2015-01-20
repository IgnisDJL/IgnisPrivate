Public MustInherit Class ReportFile
    Inherits File

    Private isReadOnly As Boolean

    Protected Sub New(filePath As String, isReadOnly As Boolean)
        MyBase.New(filePath)

        Me.isReadOnly = isReadOnly

    End Sub

    Public ReadOnly Property IS_READ_ONLY As Boolean
        Get
            Return Me.isReadOnly
        End Get
    End Property

    Public MustOverride Overrides ReadOnly Property Date_ As Date

    Public Enum ReportTypes
        SummaryDailyReport = 1
        SummaryNightShiftReport = 2
    End Enum
End Class
