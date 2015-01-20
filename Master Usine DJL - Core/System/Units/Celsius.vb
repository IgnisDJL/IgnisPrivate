
Public Class Celsius
    Inherits Unit
    Implements TemperatureUnit

    Public Shared ReadOnly UNIT As New Celsius()

    Private Sub New()

    End Sub

    Public Function unitConvert(value As Double, toUnit As Unit) As Double Implements TemperatureUnit.unitConvert

        Select Case toUnit.GetType

            Case GetType(Fahrenheits)
                Return value * 9 / 5

            Case Else
                Return value

        End Select
    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.CELSIUS
    End Function

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(Fahrenheits)
                Return value * 9 / 5 + 32

            Case GetType(Celsius)
                Return value

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.CELSIUS
        End Get
    End Property

End Class
