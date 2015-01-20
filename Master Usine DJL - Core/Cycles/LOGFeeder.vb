Public Class LOGFeeder
    Inherits Feeder

    Private belongingCycle As LOGCycle

    Public Sub New(belongingCycle As LOGCycle)
        MyBase.New(belongingCycle)

        Me.belongingCycle = belongingCycle

    End Sub

    Public Overrides Function getData(tagName As Tag) As Object

        Select Case tagName

            Case PRODUCTION_SPEED_TAG
                Return Me.PRODUCTION_SPEED

            Case MOISTURE_PERCENTAGE_TAG
                Return Me.MOISTURE_PERCENTAGE

            Case RECYCLED_ASPHALT_PERCENTAGE_TAG
                Return Me.RECYCLED_ASPHALT_PERCENTAGE

            Case Else
                Return MyBase.getData(tagName)

        End Select

    End Function

    Public Property PRODUCTION_SPEED As Double = Double.NaN

    Public Property MOISTURE_PERCENTAGE As Double = Double.NaN

    Public Property RECYCLED_ASPHALT_PERCENTAGE As Double = Double.NaN

    ' --------Constants----------- '

    Public Shared ReadOnly PRODUCTION_SPEED_TAG As Tag = New Tag("#ProductionSpeed", "Taux de production", Unit.DEFAULT_PRODUCTION_SPEED_UNIT, False)

    Public Shared ReadOnly MOISTURE_PERCENTAGE_TAG As Tag = New Tag("#Moisture", "Humidité", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly RECYCLED_ASPHALT_PERCENTAGE_TAG As Tag = New Tag("#RecycledAphaltPercentage", "Pourcentage de bitume recyclé", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared Shadows ReadOnly TAGS As Tag() = {PRODUCTION_SPEED_TAG, _
                                                    MOISTURE_PERCENTAGE_TAG, _
                                                    RECYCLED_ASPHALT_PERCENTAGE_TAG}

End Class
