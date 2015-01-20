Public Class OtherUnit
    Inherits Unit

    Public Shared ReadOnly UNIT As New OtherUnit()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double
        Return MyBase.convert(value, toUnit)
    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.OTHER_UNIT
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return ""
        End Get
    End Property
End Class
