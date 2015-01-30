Public MustInherit Class MixComponentUsed

    Private targetPercentage As Double
    Private actualPercentage As Double
    Private debit As Double
    Private mass As Double

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        Me.targetPercentage = targetPercentage
        Me.actualPercentage = actualPercentage
        Me.debit = debit
        Me.mass = mass

    End Sub

    Public ReadOnly Property getTargetPercentage() As Double
        Get
            Return targetPercentage
        End Get
    End Property

    Public ReadOnly Property getActualPercentage() As Double
        Get
            Return actualPercentage
        End Get
    End Property

    Public ReadOnly Property getDebit() As Double
        Get
            Return debit
        End Get
    End Property

    Public ReadOnly Property getMass() As Double
        Get
            Return mass
        End Get
    End Property

End Class
