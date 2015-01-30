Public Class RecycledAggregateUsed
    Inherits AggregateUsed

    Private asphaltPercentage As Double

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, moisturePercentage As Double, asphaltPercentage As Double)
        MyBase.New(targetPercentage, actualPercentage, debit, mass, moisturePercentage)

        Me.asphaltPercentage = asphaltPercentage

    End Sub

    Public ReadOnly Property getAsphaltPercentage() As Double
        Get
            Return asphaltPercentage
        End Get
    End Property
End Class
