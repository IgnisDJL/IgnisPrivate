Public Class AdminSettingsController
    Inherits SettingsController

    Private adminNode As XmlSettings.AdminNode

    Private userHasAdminRights As Boolean = False

    Public Sub New()

        Me.adminNode = XmlSettings.Settings.instance.Admin
    End Sub

    Public ReadOnly Property UserIsAdmin As Boolean
        Get
            Return Me.userHasAdminRights
        End Get
    End Property

    Public Function getAdminRights(password As String) As Boolean

        If (adminNode.PASSWORD = password) Then
            Me.userHasAdminRights = True
        End If

        Return Me.userHasAdminRights
    End Function

    Public Property UserCanOpenDataFiles As Boolean
        Get

            For Each permission As XmlSettings.PermissionNode In Me.adminNode.PERMISSION_NODES
                If (permission.Name = XmlSettings.PermissionNode.CAN_OPEN_DATA_FILES) Then
                    Return permission.Value
                End If
            Next

            Return False
        End Get
        Set(value As Boolean)

            If (Me.userHasAdminRights) Then
                Me.executeCommand(New Commands.Settings.SetUserCanOpenDataFiles(value))
            Else

                Throw New Exception("L'utilisateur n'a pas les droits.")
            End If
        End Set
    End Property

    Public Property UserCanModifyDelayCodes As Boolean
        Get

            For Each permission As XmlSettings.PermissionNode In Me.adminNode.PERMISSION_NODES
                If (permission.Name = XmlSettings.PermissionNode.CAN_MODIFY_DELAY_CODES) Then
                    Return permission.Value
                End If
            Next

            Return False
        End Get
        Set(value As Boolean)

            If (Me.userHasAdminRights) Then
                Me.executeCommand(New Commands.Settings.SetUserCanModifyDelayCodes(value))
            Else

                Throw New Exception("L'utilisateur n'a pas les droits.")
            End If
        End Set
    End Property

    Public Property UserCanChangeEmailSettings As Boolean
        Get

            For Each permission As XmlSettings.PermissionNode In Me.adminNode.PERMISSION_NODES
                If (permission.Name = XmlSettings.PermissionNode.CAN_CHANGE_EMAIL_SETTINGS) Then
                    Return permission.Value
                End If
            Next

            Return False
        End Get
        Set(value As Boolean)

            If (Me.userHasAdminRights) Then
                Me.executeCommand(New Commands.Settings.SetUserCanChangeEmailSettings(value))
            Else

                Throw New Exception("L'utilisateur n'a pas les droits.")
            End If
        End Set
    End Property

    Public Property UserCanResetDatabase As Boolean
        Get

            For Each permission As XmlSettings.PermissionNode In Me.adminNode.PERMISSION_NODES
                If (permission.Name = XmlSettings.PermissionNode.CAN_RESET_DATABASE) Then
                    Return permission.Value
                End If
            Next

            Return False
        End Get
        Set(value As Boolean)

            If (Me.userHasAdminRights) Then
                Me.executeCommand(New Commands.Settings.SetUserCanResetDataBase(value))
            Else

                Throw New Exception("L'utilisateur n'a pas les droits.")
            End If
        End Set
    End Property
End Class
