Public Class CSVFeeder
    Inherits Feeder

    Private belongingCycle As CSVCycle

    Public Sub New(belongingCycle As CSVCycle)
        MyBase.New(belongingCycle)

        Me.belongingCycle = belongingCycle

    End Sub

    Public Overrides Function getData(tagName As Tag) As Object

        Select Case tagName

            Case PERCENTAGE_TAG
                Return PERCENTAGE

            Case Else
                Return MyBase.getData(tagName)

        End Select
    End Function

    Public Shadows Property PERCENTAGE As Double = 0

    ' --------Constants----------- '
    Public Shared Shadows ReadOnly PERCENTAGE_TAG = New Tag("#Percentage", "Pourcentage", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared Shadows ReadOnly TAGS As Tag() = {PERCENTAGE_TAG}

End Class
