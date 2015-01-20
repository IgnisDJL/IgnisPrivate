
Public Class PerOne
    Inherits Unit

    Public Shared ReadOnly UNIT As New PerOne()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(PerOne)
                Return value

            Case GetType(Percent)
                Return value * 100

            Case GetType(PerMille)
                Return value * 1000

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.PER_ONE
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.PER_ONE
        End Get
    End Property
End Class
