Namespace Commands.Settings

    Public Class SetHost
        Inherits SettingsCommand

        Private _newHost As String
        Private _oldHost As String

        Public Sub New(newHost As String)
            MyBase.New()

            Me._oldHost = Me.Settings.Usine.EmailsInfo.HOST

            Me._newHost = newHost

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.EmailsInfo.HOST = _newHost

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.HOST = _oldHost

        End Sub
    End Class
End Namespace