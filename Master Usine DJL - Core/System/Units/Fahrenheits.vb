
Public Class Fahrenheits
    Inherits Unit
    Implements TemperatureUnit

    Public Shared ReadOnly UNIT As New Fahrenheits()

    Private Sub New()

    End Sub

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.FAHRENHEIGHT
    End Function

    Public Overloads Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(Celsius)
                Return (value - 32) * 5 / 9

            Case GetType(Fahrenheits)
                Return value

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Function unitConvert(value As Double, toUnit As Unit) As Double Implements TemperatureUnit.unitConvert

        Select Case toUnit.GetType

            Case GetType(Celsius)
                Return value * 5 / 9

            Case Else
                Return value

        End Select

    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.FAHRENHEIGHT
        End Get
    End Property

End Class
