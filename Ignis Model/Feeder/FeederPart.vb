Public Class FeederPart
    Private targetPercentage As Double
    Private realPercentage As Double
    Private material As Material

    Sub New(targetPercentage As Double, realPercentage As Double, material As Material)
        Me.targetPercentage = targetPercentage
        Me.realPercentage = realPercentage 
        Me.material = material
    End Sub

    Public ReadOnly Property getTargetPercentage As Double
        Get
            Return targetPercentage
        End Get
    End Property

    Public ReadOnly Property getRealPercentage As Double
        Get
            Return realPercentage
        End Get
    End Property


End Class
