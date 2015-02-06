Public MustInherit Class Feeder_1
    Private feederId As String

    Public Sub New(feederId As String)
        Me.feederId = feederId
    End Sub

    Public Function getFeedertName(productionDate As Date) As String
        Return Plant.feederCatalog.getDescriptionFromContainer(feederId, productionDate)
    End Function

    Public MustOverride Function isRecycled() As Boolean
End Class
