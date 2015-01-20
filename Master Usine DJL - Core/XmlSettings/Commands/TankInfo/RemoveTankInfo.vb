Namespace Commands.Settings

    Public Class RemoveTankInfo
        Inherits SettingsCommand

        Private tankToRemove As XmlSettings.AsphaltNode.TankInfo

        Public Sub New(tankToRemove As XmlSettings.AsphaltNode.TankInfo)

            Me.tankToRemove = tankToRemove
        End Sub

        Public Overrides Sub execute()

            XmlSettings.Settings.instance.Usine.AsphaltInfo.removeTankInfo(Me.tankToRemove)

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.AsphaltInfo.addTankInfo(Me.tankToRemove)

        End Sub
    End Class
End Namespace

