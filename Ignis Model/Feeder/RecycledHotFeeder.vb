Public Class RecycledHotFeeder
    Inherits HotFeeder

    Private asphaltPercentage As Double

    Public Sub New(feederId As String, materialID As String, targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(feederId, materialID, targetPercentage, actualPercentage, debit, mass)

    End Sub

    Public Overrides Function isRecycled() As Boolean
        Return True
    End Function
End Class
