
Public Class TonsPerMinute
    Inherits Unit

    Public Shared ReadOnly UNIT As New TonsPerMinute()

    Private Sub New()

    End Sub

    Public Overrides Function convert(value As Double, toUnit As Unit) As Double

        Select Case toUnit.GetType

            Case GetType(KgPerMinute)
                Return value * 1000

            Case GetType(KgPerHour)
                Return value * 1000 * 60

            Case GetType(TonsPerHour)
                Return value * 60

            Case GetType(TonsPerMinute)
                Return value

            Case Else
                Return MyBase.convert(value, toUnit)

        End Select

    End Function

    Public Overrides Function ToString() As String
        Return Constants.Units.StringRepresentation.TONS_PER_MINUTE
    End Function

    Public Overrides ReadOnly Property SYMBOL As String
        Get
            Return Constants.Units.Symbols.TONS_PER_MINUTE
        End Get
    End Property
End Class
