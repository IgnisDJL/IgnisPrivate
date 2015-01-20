Namespace Commands.Settings

    Public Class SetUsineID
        Inherits SettingsCommand

        Private _newID As String
        Private _oldID As String

        Public Sub New(newID As String)
            MyBase.New()

            Me._oldID = Me.Settings.Usine.PLANT_ID

            Me._newID = newID

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.PLANT_ID = _newID

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.PLANT_ID = _oldID

        End Sub

    End Class
End Namespace
