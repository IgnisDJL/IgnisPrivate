Namespace Commands.Settings

    Public Class SetUSBPath
        Inherits SettingsCommand

        Private _newPath As String
        Private _oldPath As String

        Public Sub New(newPath As String)
            MyBase.New()

            Me._oldPath = Me.Settings.Usine.USB_DIRECTORY

            Me._newPath = newPath

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.USB_DIRECTORY = _newPath

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.USB_DIRECTORY = _oldPath

        End Sub

    End Class
End Namespace
