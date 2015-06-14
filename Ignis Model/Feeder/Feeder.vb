Public MustInherit Class Feeder_1
    Implements IEquatable(Of Feeder_1)
    Private feederId As String

    Public Sub New(feederId As String)
        Me.feederId = feederId
    End Sub

    Public Function getFeederDescription(productionDate As Date) As String
        Dim description As String = Plant.feederCatalog.getDescriptionFromContainer(feederId, productionDate)

        If (String.IsNullOrEmpty(description)) Then
            Return String.Empty
        Else
            Return description
        End If

    End Function

    Public Function getFeederID() As String
        Return feederId
    End Function

    Public MustOverride Function isRecycled() As Boolean


    Public Overloads Function Equals(ByVal feeder As Feeder_1) As Boolean Implements IEquatable(Of Feeder_1).Equals
        If Me.feederId = feeder.getFeederID Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
