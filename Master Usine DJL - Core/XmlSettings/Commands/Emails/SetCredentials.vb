Namespace Commands.Settings

    Public Class SetCredentials
        Inherits SettingsCommand

        Private _newCreds As String
        Private _oldCreds As String

        Public Sub New(newCreds As String)
            MyBase.New()

            Me._oldCreds = Me.Settings.Usine.EmailsInfo.CREDENTIALS

            Me._newCreds = newCreds

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.EmailsInfo.CREDENTIALS = _newCreds

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.CREDENTIALS = _oldCreds

        End Sub
    End Class
End Namespace