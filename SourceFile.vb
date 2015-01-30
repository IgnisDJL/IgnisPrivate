
Public Class SourceFile
    Inherits DataFile
    Public sourceFileAdapter As SourceFileAdapter

    Public Sub New(filePath As String)
        MyBase.New(filePath)
        sourceFileAdapter = New SourceFileLogAdapter()

    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return sourceFileAdapter.getDate(Me)
        End Get
    End Property

End Class
