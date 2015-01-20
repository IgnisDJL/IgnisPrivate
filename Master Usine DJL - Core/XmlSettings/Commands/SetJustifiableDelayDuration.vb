Namespace Commands.Settings

    Public Class SetJustifiableDelayDuration
        Inherits SettingsCommand

        Private _newValue As TimeSpan
        Private _oldValue As TimeSpan

        Public Sub New(newValue As TimeSpan)
            MyBase.New()

            Me._oldValue = Me.Settings.Usine.Events.Delays.JUSTIFIABLE_DURATION

            Me._newValue = newValue

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.Events.Delays.JUSTIFIABLE_DURATION = _newValue

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.Events.Delays.JUSTIFIABLE_DURATION = _oldValue

        End Sub
    End Class
End Namespace
