Public Class KiloGrams
    Inherits Unit

    Public Shared ReadOnly UNIT As New KiloGrams()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(KiloGrams)
                Return value

            Case GetType(Tons)
                Return value / 1000

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.KILLOGRAM
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.KILLOGRAM
        End Get
    End Property
End Class
