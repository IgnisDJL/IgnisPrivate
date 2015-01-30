
Public Class SourceFile
    Inherits DataFile
    Public sourceFileAdapter As SourceFileAdapter

    Public Sub New(filePath As String, sourceFileAdapter As SourceFileAdapter)
        MyBase.New(filePath)
        Me.sourceFileAdapter = sourceFileAdapter

    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return sourceFileAdapter.getDate(Me)
        End Get
    End Property

End Class
