
Public Class Percent
    Inherits Unit

    Public Shared ReadOnly UNIT As New Percent()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(PerOne)
                Return value / 100

            Case GetType(Percent)
                Return value

            Case GetType(PerMille)
                Return value * 10

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.PERCENT
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.PERCENT
        End Get
    End Property
End Class
