
Public Class PerMille
    Inherits Unit

    Public Shared ReadOnly UNIT As New PerMille()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(PerOne)
                Return value / 1000

            Case GetType(Percent)
                Return value / 10

            Case GetType(PerMille)
                Return value

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.PER_MILLE
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.PER_MILLE
        End Get
    End Property
End Class
