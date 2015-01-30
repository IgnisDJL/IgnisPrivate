Public Class RecipePart
    Private percentage As Double
    Private material As Material

    Sub New(percentage As Double, material As Material)
        Me.percentage = percentage
        Me.material = material
    End Sub

    Public ReadOnly Property getPercentage As Double
        Get
            Return percentage
        End Get
    End Property

    Public ReadOnly Property getMaterial As Material
        Get
            Return material
        End Get
    End Property

End Class
