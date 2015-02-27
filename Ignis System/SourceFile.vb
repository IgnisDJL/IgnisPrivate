
Public Class SourceFile
    Inherits DataFile
    Public sourceFileAdapter As SourceFileAdapter
    Public importConstant As GlobalImportConstant

    Private productionDate As Date

    Public Sub New(filePath As String, sourceFileAdapter As SourceFileAdapter)
        MyBase.New(filePath)
        Me.sourceFileAdapter = sourceFileAdapter
        sourceFileAdapter.setImportConstantForLanguage(Me)
        Me.productionDate = sourceFileAdapter.getDate(Me)

    End Sub

    Public Sub New(filePath As String, sourceFileAdapter As SourceFileAdapter, productionDate As Date)
        MyBase.New(filePath)
        Me.sourceFileAdapter = sourceFileAdapter
        sourceFileAdapter.setImportConstantForLanguage(Me)
        Me.productionDate = productionDate

    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return productionDate
        End Get
    End Property

End Class
