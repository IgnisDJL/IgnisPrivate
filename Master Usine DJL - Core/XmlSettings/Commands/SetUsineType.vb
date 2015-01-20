Namespace Commands.Settings

    Public Class SetUsineType
        Inherits SettingsCommand

        Private _newType As Constants.Settings.UsineType
        Private _oldType As Constants.Settings.UsineType

        Public Sub New(newType As Constants.Settings.UsineType)
            MyBase.New()

            Me._newType = newType

            Me._oldType = Me.Settings.Usine.TYPE

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.TYPE = Me._newType

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.TYPE = Me._oldType

        End Sub
    End Class
End Namespace
