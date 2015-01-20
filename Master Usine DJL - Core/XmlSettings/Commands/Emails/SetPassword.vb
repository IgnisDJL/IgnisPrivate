Namespace Commands.Settings

    Public Class SetPassword
        Inherits SettingsCommand

        Private _newPassword As String
        Private _oldPassword As String

        Public Sub New(newPassword As String)
            MyBase.New()

            Me._oldPassword = Me.Settings.Usine.EmailsInfo.PASSWORD

            Me._newPassword = newPassword

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.EmailsInfo.PASSWORD = _newPassword

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.PASSWORD = _oldPassword

        End Sub
    End Class
End Namespace