
Public Class SourceFile
    Inherits DataFile
    Implements IEquatable(Of SourceFile)

    Public sourceFileAdapter As SourceFileAdapter
    Public importConstant As GlobalImportConstant
    Private eventFilePath As String
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

    Public Overloads Function Equals(sourceFile As SourceFile) As Boolean Implements IEquatable(Of SourceFile).Equals
        If Me.productionDate = sourceFile.Date_ Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub setEventFilePath(eventFilePath As String)
        Me.eventFilePath = eventFilePath
    End Sub

    Public Function getEventFilePath() As String
        Return eventFilePath
    End Function
End Class
