
Public Class SourceFileComplement
    Inherits DataFile

    Private productionDate As Date

    Public Sub New(filePath As String, productionDate As Date)
        MyBase.New(filePath)
        Me.productionDate = productionDate
    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return productionDate
        End Get
    End Property

End Class
