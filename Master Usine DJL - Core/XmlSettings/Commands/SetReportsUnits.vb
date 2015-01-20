Namespace Commands.Settings

    Public Class SetReportsUnits
        Inherits SettingsCommand

        Private _oldMassUnit As Unit
        Private _oldTemperatureUnit As Unit
        Private _oldPercentageUnit As Unit
        Private _oldProductionRateUnit As Unit

        Private _newMassUnit As Unit
        Private _newTemperatureUnit As Unit
        Private _newPercentageUnit As Unit
        Private _newProductionRateUnit As Unit

        Public Sub New(newMassUnit As Unit, newTemperatureUnit As Unit, newPercentageUnit As Unit, newProductionRateUnit As Unit)
            MyBase.New()

            Me._newMassUnit = newMassUnit
            Me._newTemperatureUnit = newTemperatureUnit
            Me._newPercentageUnit = newPercentageUnit
            Me._newProductionRateUnit = newProductionRateUnit

            With XmlSettings.Settings.instance.Reports

                Me._oldMassUnit = .MASS_UNIT
                Me._oldTemperatureUnit = .TEMPERATURE_UNIT
                Me._oldPercentageUnit = .PERCENT_UNIT
                Me._oldProductionRateUnit = .PRODUCTION_SPEED_UNIT
            End With
        End Sub

        Public Overrides Sub execute()

            With XmlSettings.Settings.instance.Reports

                .MASS_UNIT = Me._newMassUnit
                .TEMPERATURE_UNIT = Me._newTemperatureUnit
                .PERCENT_UNIT = Me._newPercentageUnit
                .PRODUCTION_SPEED_UNIT = Me._newProductionRateUnit
            End With
        End Sub

        Public Overrides Sub undo()

            With XmlSettings.Settings.instance.Reports

                .MASS_UNIT = Me._oldMassUnit
                .TEMPERATURE_UNIT = Me._oldTemperatureUnit
                .PERCENT_UNIT = Me._oldPercentageUnit
                .PRODUCTION_SPEED_UNIT = Me._oldProductionRateUnit
            End With
        End Sub
    End Class
End Namespace
