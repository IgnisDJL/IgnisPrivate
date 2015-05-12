Public Class RecycledHotFeeder
    Inherits HotFeeder

    Private asphaltPercentage As Double

    Public Sub New(feederId As String, materialID As String, targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(feederId, materialID, targetPercentage, actualPercentage, debit, mass)

        asphaltPercentage = 0
    End Sub

    Public Overrides Function isRecycled() As Boolean
        Return True
    End Function

    Public Function getAsphaltPercentage() As Boolean
        Return asphaltPercentage
    End Function


End Class
