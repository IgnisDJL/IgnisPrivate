Public Class RecycledHotFeeder
    Inherits HotFeeder

    Private asphaltPercentage As Double

    Public Sub New(feederId As String, targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, moisturePercentage As Double)
        MyBase.New(feederId, targetPercentage, actualPercentage, debit, mass, moisturePercentage)
    End Sub

    Public ReadOnly Property getAsphaltPercentage() As Double
        Get
            Return asphaltPercentage
        End Get
    End Property


    Public Overrides Function isRecycled() As Boolean
        Return True
    End Function
End Class
