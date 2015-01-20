Namespace Commands.Settings

    Public Class SetCSVUnits
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

            With XmlSettings.Settings.instance.Usine.DataFiles.CSV

                Me._oldMassUnit = .MassUnit
                Me._oldTemperatureUnit = .TemperatureUnit
                Me._oldPercentageUnit = .PercentageUnit
                Me._oldProductionRateUnit = .ProductionRateUnit
            End With
        End Sub

        Public Overrides Sub execute()

            With XmlSettings.Settings.instance.Usine.DataFiles.CSV

                .MassUnit = Me._newMassUnit
                .TemperatureUnit = Me._newTemperatureUnit
                .PercentageUnit = Me._newPercentageUnit
                .ProductionRateUnit = Me._newProductionRateUnit
            End With
        End Sub

        Public Overrides Sub undo()

            With XmlSettings.Settings.instance.Usine.DataFiles.CSV

                .MassUnit = Me._oldMassUnit
                .TemperatureUnit = Me._oldTemperatureUnit
                .PercentageUnit = Me._oldPercentageUnit
                .ProductionRateUnit = Me._oldProductionRateUnit
            End With
        End Sub
    End Class
End Namespace
