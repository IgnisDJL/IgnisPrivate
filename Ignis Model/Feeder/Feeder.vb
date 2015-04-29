Public MustInherit Class Feeder_1
    Implements IEquatable(Of Feeder_1)
    Private feederId As String

    Public Sub New(feederId As String)
        Me.feederId = feederId
    End Sub

    Public Function getFeederName(productionDate As Date) As String
        Dim name As String = Plant.feederCatalog.getDescriptionFromContainer(feederId, productionDate)

        If (String.IsNullOrEmpty(name)) Then
            Return feederId
        Else
            Return name
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
