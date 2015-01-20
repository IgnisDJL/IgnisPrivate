Namespace Commands.Settings

    Public Class SetPort
        Inherits SettingsCommand

        Private _newPort As String
        Private _oldPort As String

        Public Sub New(newPort As String)
            MyBase.New()

            Me._oldPort = Me.Settings.Usine.EmailsInfo.PORT

            Me._newPort = newPort

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.EmailsInfo.PORT = _newPort

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.PORT = _oldPort

        End Sub
    End Class
End Namespace