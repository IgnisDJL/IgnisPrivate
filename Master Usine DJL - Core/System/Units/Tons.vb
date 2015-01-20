Public Class Tons
    Inherits Unit

    Public Shared ReadOnly UNIT As New Tons()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(KiloGrams)
                Return value * 1000

            Case GetType(Tons)
                Return value

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.TON
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.TON
        End Get
    End Property
End Class
