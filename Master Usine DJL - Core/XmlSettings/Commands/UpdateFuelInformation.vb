Namespace Commands.Settings

    Public Class UpdateFuelInformation
        Inherits SettingsCommand

        Private _newFuel1Name As String
        Private _newFuel1Unit As String

        Private _newFuel2Name As String
        Private _newFuel2Unit As String

        Private _oldFuel1Name As String
        Private _oldFuel1Unit As String

        Private _oldFuel2Name As String
        Private _oldFuel2Unit As String

        Public Sub New(newFuel1Name As String, newFuel1Unit As String, newFuel2Name As String, newFuel2Unit As String)

            Me._newFuel1Name = newFuel1Name
            Me._newFuel1Unit = newFuel1Unit
            Me._newFuel2Name = newFuel2Name
            Me._newFuel2Unit = newFuel2Unit

            With XmlSettings.Settings.instance.Usine.FuelsInfo

                Me._oldFuel1Name = .FUEL_1_NAME
                Me._oldFuel1Unit = .FUEL_1_UNIT
                Me._oldFuel2Name = .FUEL_2_NAME
                Me._oldFuel2Unit = .FUEL_2_UNIT

            End With

        End Sub

        Public Overrides Sub execute()

            With XmlSettings.Settings.instance.Usine.FuelsInfo

                .FUEL_1_NAME = Me._newFuel1Name
                .FUEL_1_UNIT = Me._newFuel1Unit
                .FUEL_2_NAME = Me._newFuel2Name
                .FUEL_2_UNIT = Me._newFuel2Unit

            End With

        End Sub

        Public Overrides Sub undo()

            With XmlSettings.Settings.instance.Usine.FuelsInfo

                .FUEL_1_NAME = Me._oldFuel1Name
                .FUEL_1_UNIT = Me._oldFuel1Unit
                .FUEL_2_NAME = Me._oldFuel2Name
                .FUEL_2_UNIT = Me._oldFuel2Unit

            End With

        End Sub
    End Class
End Namespace
