Namespace Commands.Settings

    Public Class SetUserCanChangeEmailSettings
        Inherits SettingsCommand

        Private _newState As Boolean
        Private _node As XmlSettings.PermissionNode

        Public Sub New(enabled As Boolean)
            MyBase.New()

            Me._newState = enabled
        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me._node)) Then

                For Each permission As XmlSettings.PermissionNode In Me.Settings.Admin.PERMISSION_NODES

                    If (permission.Name = XmlSettings.PermissionNode.CAN_CHANGE_EMAIL_SETTINGS) Then

                        Me._node = permission
                    End If
                Next
            End If

            Me._node.Value = Me._newState

        End Sub

        Public Overrides Sub undo()

            Me._node.Value = Not _newState
        End Sub

    End Class
End Namespace

