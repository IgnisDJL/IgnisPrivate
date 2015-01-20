Namespace Commands.Settings

    Public Class AddAsphaltTankInfo
        Inherits SettingsCommand

        Private tankName As String
        Private asphaltName As String
        Private mixTargetTemperature As Double

        Private newTankNode As XmlSettings.AsphaltNode.TankInfo

        Public Sub New(tankName As String, asphaltName As String, mixTargetTemperature As Double)

            Me.tankName = tankName
            Me.asphaltName = asphaltName
            Me.mixTargetTemperature = mixTargetTemperature

        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me.newTankNode)) Then
                Me.newTankNode = XmlSettings.Settings.instance.Usine.AsphaltInfo.addTankInfo(Me.tankName, Me.asphaltName, Me.mixTargetTemperature)
            Else
                XmlSettings.Settings.instance.Usine.AsphaltInfo.addTankInfo(Me.newTankNode)
            End If

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.AsphaltInfo.removeTankInfo(Me.newTankNode)

        End Sub
    End Class
End Namespace

