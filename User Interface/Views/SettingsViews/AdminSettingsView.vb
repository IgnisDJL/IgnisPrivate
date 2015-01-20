Imports IGNIS.UI.Common

Namespace UI

    Public Class AdminSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Administrateur"

        ' Components
        Private passwordPopUp As Common.AdminPasswordPanel
        Private passwordInformationToolTip As ToolTip

        Private permissionsLabel As Label
        Private permissionsPanel As Panel
        Private WithEvents canOpenDataFilesCheckBox As CheckBox
        Private WithEvents canModifyDelayCodesCheckBox As CheckBox
        Private WithEvents canChangeEmailSettingsCheckBox As CheckBox
        Private WithEvents canResetDatabaseCheckBox As CheckBox

        Private canOpenDataFilesToolTip As ToolTip
        Private canModifyDelayCodesToolTip As ToolTip
        Private canChangeEmailSettingsToolTip As ToolTip
        Private canResetDatabaseToolTip As ToolTip


        ' Attributes
        Private _adminSettingsController As AdminSettingsController

        Private _passwordPopUpIsShowing As Boolean = False
        Private _settingsControlsAreShowing As Boolean = False

        Public Sub New()
            MyBase.New()

            Me.layout = New AdminSettingsViewLayout()

            Me._adminSettingsController = ProgramController.SettingsControllers.AdminSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.permissionsLabel = New Label
            Me.permissionsLabel.AutoSize = False
            Me.permissionsLabel.Text = "Permissions"

            Me.permissionsPanel = New Panel
            Me.permissionsPanel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.canOpenDataFilesCheckBox = New CheckBox
            Me.canOpenDataFilesCheckBox.Text = "Ouvrir les fichiers de données :"
            Me.canOpenDataFilesCheckBox.TextAlign = ContentAlignment.MiddleLeft
            Me.canOpenDataFilesCheckBox.CheckAlign = ContentAlignment.MiddleRight

            Me.canModifyDelayCodesCheckBox = New CheckBox
            Me.canModifyDelayCodesCheckBox.Text = "Modifier les codes des délais :"
            Me.canModifyDelayCodesCheckBox.TextAlign = ContentAlignment.MiddleLeft
            Me.canModifyDelayCodesCheckBox.CheckAlign = ContentAlignment.MiddleRight

            Me.canChangeEmailSettingsCheckBox = New CheckBox
            Me.canChangeEmailSettingsCheckBox.Text = "Modifier les paramètres d'envoi par courriel :"
            Me.canChangeEmailSettingsCheckBox.TextAlign = ContentAlignment.MiddleLeft
            Me.canChangeEmailSettingsCheckBox.CheckAlign = ContentAlignment.MiddleRight

            Me.canResetDatabaseCheckBox = New CheckBox
            Me.canResetDatabaseCheckBox.Text = "Réinitialiser la base de données du programme :"
            Me.canResetDatabaseCheckBox.TextAlign = ContentAlignment.MiddleLeft
            Me.canResetDatabaseCheckBox.CheckAlign = ContentAlignment.MiddleRight
            Me.canResetDatabaseCheckBox.Enabled = False

            Me.permissionsPanel.Controls.Add(Me.canOpenDataFilesCheckBox)
            Me.permissionsPanel.Controls.Add(Me.canModifyDelayCodesCheckBox)
            Me.permissionsPanel.Controls.Add(Me.canChangeEmailSettingsCheckBox)
            Me.permissionsPanel.Controls.Add(Me.canResetDatabaseCheckBox)

            Me.canOpenDataFilesToolTip = New ToolTip
            Me.canOpenDataFilesToolTip.SetToolTip(Me.canOpenDataFilesCheckBox, "Permet à l'utilisateur d'ouvrir les fichiers de données (e.g. les .csv ou .mdb) à partir" & Environment.NewLine & "de l'interface du programme en double-cliquant sur les items des listes de fichiers.")
            Me.canOpenDataFilesToolTip.AutoPopDelay = 30000

            Me.canModifyDelayCodesToolTip = New ToolTip
            Me.canModifyDelayCodesToolTip.SetToolTip(Me.canModifyDelayCodesCheckBox, "Autorise l'accès au gestionnaire des codes de délais dans la section 'Événements' des paramètres.")
            Me.canModifyDelayCodesToolTip.AutoPopDelay = 30000

            Me.canChangeEmailSettingsToolTip = New ToolTip
            Me.canChangeEmailSettingsToolTip.SetToolTip(Me.canChangeEmailSettingsCheckBox, "Autorise l'accès aux paramètres de configuration de l'envoi par courriel et à la gestion" & Environment.NewLine & "des destinataires par défaut dans la section 'Courriels et Sauvegarde' des paramètres.")
            Me.canChangeEmailSettingsToolTip.AutoPopDelay = 30000

            Me.canResetDatabaseToolTip = New ToolTip
            Me.canResetDatabaseToolTip.SetToolTip(Me.canResetDatabaseCheckBox, "Autorise l'accès au fonctions de gestion de la base de données et des archives" & Environment.NewLine & "du programme dans la section 'Fichiers de données' des paramètres.")
            Me.canResetDatabaseToolTip.AutoPopDelay = 30000

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            If (Me._passwordPopUpIsShowing) Then

                Me.passwordPopUp.ajustLayout(Common.AdminPasswordPanel.SIZE_WITHOUT_PARAMETERS_BUTTON)
            End If

            If (Me._settingsControlsAreShowing) Then

                Dim layout = DirectCast(Me.layout, AdminSettingsViewLayout)

                Me.permissionsLabel.Location = layout.PermissionsLabel_Location
                Me.permissionsLabel.Size = layout.PermissionsLabel_Size

                Me.permissionsPanel.Location = layout.PermissionsPanel_Location
                Me.permissionsPanel.Size = layout.PermissionsPanel_Size

                Me.canOpenDataFilesCheckBox.Location = layout.CanOpenDataFilesCheckBox_Location
                Me.canOpenDataFilesCheckBox.Size = layout.CanOpenDataFilesCheckBox_Size

                Me.canModifyDelayCodesCheckBox.Location = layout.CanModifyDelayCodesCheckBox_Location
                Me.canModifyDelayCodesCheckBox.Size = layout.CanModifyDelayCodesCheckBox_Size

                Me.canChangeEmailSettingsCheckBox.Location = layout.CanChangeEmailSettingsCheckBox_Location
                Me.canChangeEmailSettingsCheckBox.Size = layout.CanChangeEmailSettingsCheckBox_Size

                Me.canResetDatabaseCheckBox.Location = layout.CanResetDatabaseCheckBox_Location
                Me.canResetDatabaseCheckBox.Size = layout.CanResetDatabaseCheckBox_Size
            End If
        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

            If (Me._passwordPopUpIsShowing) Then
                Me.passwordPopUp.Location = New Point(Me.Width / 2 - Me.passwordPopUp.Width / 2, Me.Height / 2 - Me.passwordPopUp.Height / 2)
            End If

        End Sub

        Public Overrides Sub afterShow()

            If (Me._passwordPopUpIsShowing) Then
                Me.passwordPopUp.Focus()
            End If

        End Sub

        Protected Overloads Overrides Sub beforeShow()

            If (Me._adminSettingsController.UserIsAdmin) Then

                Me.showSettingsControls()
            Else

                Me.showPasswordPopup()
            End If
        End Sub

        Public Overrides Sub onHide()

        End Sub

        Public Overrides Sub updateFields()
            Me.updatingFields = True

            Me.canOpenDataFilesCheckBox.Checked = Me._adminSettingsController.UserCanOpenDataFiles
            Me.canChangeEmailSettingsCheckBox.Checked = Me._adminSettingsController.UserCanChangeEmailSettings
            Me.canModifyDelayCodesCheckBox.Checked = Me._adminSettingsController.UserCanModifyDelayCodes
            Me.canResetDatabaseCheckBox.Checked = Me._adminSettingsController.UserCanResetDatabase

            Me.updatingFields = False
        End Sub

        Private Sub showSettingsControls()

            If (Me._passwordPopUpIsShowing) Then
                Me.Controls.Remove(Me.passwordPopUp)
                Me._passwordPopUpIsShowing = False
            End If

            If (Not Me._settingsControlsAreShowing) Then

                Me.Controls.Add(Me.permissionsLabel)
                Me.Controls.Add(Me.permissionsPanel)

                Me._settingsControlsAreShowing = True

                ajustLayout()
            End If
        End Sub

        Private Sub showPasswordPopup()

            If (Not Me._passwordPopUpIsShowing) Then

                If (IsNothing(Me.passwordPopUp)) Then
                    Me.passwordPopUp = New Common.AdminPasswordPanel
                    Me.passwordPopUp.ShowSettingsButton = False
                    Me.passwordPopUp.ShowCloseButton = False
                    Me.passwordPopUp.IsDraggable = False

                    AddHandler Me.passwordPopUp.SuccessfulAuthentication, AddressOf Me.showSettingsControls

                    Me.passwordInformationToolTip = New ToolTip
                    Me.passwordInformationToolTip.SetToolTip(Me.passwordPopUp, "Entrez le mot de passe pour avoir accès aux paramètres administrateurs et à la gestion des permissions.")

                End If

                Me.Controls.Add(Me.passwordPopUp)
                Me._passwordPopUpIsShowing = True

                Me.passwordPopUp.Location = New Point(100, 100)

            End If
        End Sub

        Private Sub canOpenDataFilesCheckBox_listener(sender As Object, e As EventArgs) Handles canOpenDataFilesCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me._adminSettingsController.UserCanOpenDataFiles = canOpenDataFilesCheckBox.Checked
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub canModifyDelayCodesCheckBox_listener(sender As Object, e As EventArgs) Handles canModifyDelayCodesCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me._adminSettingsController.UserCanModifyDelayCodes = canModifyDelayCodesCheckBox.Checked
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub canChangeEmailSettingsCheckBox_listener(sender As Object, e As EventArgs) Handles canChangeEmailSettingsCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me._adminSettingsController.UserCanChangeEmailSettings = canChangeEmailSettingsCheckBox.Checked
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub canResetDatabaseCheckBox_listener(sender As Object, e As EventArgs) Handles canResetDatabaseCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me._adminSettingsController.UserCanResetDatabase = canResetDatabaseCheckBox.Checked
                Me.raiseSettingChangedEvent()
            End If
        End Sub


        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._adminSettingsController
            End Get
        End Property

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property
    End Class
End Namespace