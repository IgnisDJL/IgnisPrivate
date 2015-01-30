Public Class Feeder_1
    Private aggregateUsed As AggregateUsed
    Private feederId As String

    Public Sub New(feederId As String)
        Me.feederId = feederId
    End Sub

    Public Sub setAggregateUsed(aggregateUsed As AggregateUsed)
        Me.aggregateUsed = aggregateUsed
    End Sub

    Public Function getAggregateUsed() As AggregateUsed
        Return aggregateUsed
    End Function

    Public Function getFeedertName(productionDate As Date) As String
        Return Plant.feederCatalog.getDescriptionFromContainer(feederId, productionDate)
    End Function

    Public Function isRecycled() As Boolean
        If Not IsNothing(aggregateUsed) Then
            Return TypeOf aggregateUsed Is RecycledAggregateUsed
        Else
            Return Nothing
        End If

    End Function
End Class
