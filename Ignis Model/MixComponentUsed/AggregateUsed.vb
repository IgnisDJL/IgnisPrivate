Public Class AggregateUsed
    Inherits MixComponentUsed
    Private moisturePercentage As Double

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, moisturePercentage As Double)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)
        Me.moisturePercentage = moisturePercentage

    End Sub

    Public ReadOnly Property getMoisturePercentage() As Double
        Get
            Return moisturePercentage
        End Get
    End Property
End Class
