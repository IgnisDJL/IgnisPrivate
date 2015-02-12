
Public Class SourceFile
    Inherits DataFile
    Public sourceFileAdapter As SourceFileAdapter
    Public importConstant As GlobalImportConstant


    Public Sub New(filePath As String, sourceFileAdapter As SourceFileAdapter)
        MyBase.New(filePath)
        Me.sourceFileAdapter = sourceFileAdapter
        sourceFileAdapter.setImportConstantForLanguage(Me)
    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return sourceFileAdapter.getDate(Me)
        End Get
    End Property

End Class
