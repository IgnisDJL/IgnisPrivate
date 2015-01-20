Namespace Commands.Settings

    Public Class SetUsineName
        Inherits SettingsCommand

        Private _newName As String
        Private _oldName As String

        Public Sub New(newName As String)
            MyBase.New()

            Me._oldName = Me.Settings.Usine.PLANT_NAME

            Me._newName = newName

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.PLANT_NAME = _newName

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.PLANT_NAME = _oldName

        End Sub
    End Class
End Namespace
