Namespace Commands.Settings

    Public Class SetSettingsFileEnabled
        Inherits SettingsCommand

        Private _newState As Boolean

        Public Sub New(enabled As Boolean)
            MyBase.New()

            Me._newState = enabled
        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.Events.ACTIVE = _newState
        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.Events.ACTIVE = Not _newState
        End Sub

    End Class
End Namespace
