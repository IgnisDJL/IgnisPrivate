Public MustInherit Class ReportFile
    Inherits File

    Private isReadOnly As Boolean

    Public Sub New(filePath As String, isReadOnly As Boolean)
        MyBase.New(filePath)

        Me.isReadOnly = isReadOnly

    End Sub

    Public ReadOnly Property IS_READ_ONLY As Boolean
        Get
            Return Me.isReadOnly
        End Get
    End Property

    Public MustOverride Overrides ReadOnly Property Date_ As Date

End Class
