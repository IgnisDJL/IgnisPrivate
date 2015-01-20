Namespace Commands.Settings

    Public Class UpdateTankInfo
        Inherits SettingsCommand

        Private newTankName As String
        Private newAsphaltName As String
        Private newMixTargetTemperature As Double

        Private oldTankName As String
        Private oldAsphaltName As String
        Private oldMixTargetTemperature As Double

        Private tankInfoToUpdate As XmlSettings.AsphaltNode.TankInfo

        Public Sub New(tankInfoToUpdate As XmlSettings.AsphaltNode.TankInfo, newTankName As String, newAsphaltName As String, newMixTargetTemperature As Double)

            Me.newTankName = newTankName
            Me.newAsphaltName = newAsphaltName
            Me.newMixTargetTemperature = newMixTargetTemperature

            Me.tankInfoToUpdate = tankInfoToUpdate

            Me.oldTankName = tankInfoToUpdate.TANK_NAME
            Me.oldAsphaltName = tankInfoToUpdate.ASPHALT_NAME
            Me.oldMixTargetTemperature = tankInfoToUpdate.SET_POINT_TEMP
        End Sub

        Public Overrides Sub execute()

            With Me.tankInfoToUpdate
                .TANK_NAME = newTankName
                .ASPHALT_NAME = newAsphaltName
                .SET_POINT_TEMP = newMixTargetTemperature
            End With

        End Sub

        Public Overrides Sub undo()

            With Me.tankInfoToUpdate
                .TANK_NAME = oldTankName
                .ASPHALT_NAME = oldAsphaltName
                .SET_POINT_TEMP = oldMixTargetTemperature
            End With
        End Sub
    End Class
End Namespace

