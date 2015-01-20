Imports IGNIS.UI.Common

Namespace UI

    Public Class EmailSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Courriels"

        ' Components
        ' !LAYOUT!
        Private newRecipientField As Common.TextField
        Private WithEvents addNewRecipientButton As Button
        Private recipientsListView As Common.RecipientListView

        Private newDefaultRecipientField As Common.TextField
        Private WithEvents addNewDefaultRecipientButton As Button
        Private defaultRecipientsListView As Common.RecipientListView

        Private credentialsLabel As Label
        Private WithEvents credentialsField As Common.TextField
        Private credentialsBuffer As String
        Private credentialsToolTip As ToolTip

        Private passwordLabel As Label
        Private WithEvents passwordField As TextBox
        Private passwordBuffer As String
        Private passwordToolTip As ToolTip

        Private hostLabel As Label
        Private WithEvents hostField As Common.TextField
        Private hostBuffer As String
        Private hostToolTip As ToolTip

        Private portLabel As Label
        Private WithEvents portField As Common.TextField
        Private portBuffer As String
        Private portToolTip As ToolTip

        Private WithEvents cantSeeProtectedEmailSettingsLabel As Label

        Private WithEvents adminPasswordPanel As Common.AdminPasswordPanel
        ' !LAYOUT!

        ' Attributes
        Private _emailSettings As EmailSettingsController

        Private _protectedEmailSettingsAreShowing As Boolean = False

        Public Sub New()
            MyBase.New()

            Me.layout = New EmailSettingsViewLayout

            Me._emailSettings = ProgramController.SettingsControllers.EmailSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.newRecipientField = New TextField
            Me.newRecipientField.PlaceHolder = "Adresse"
            Me.newRecipientField.ValidationType = TextField.ValidationTypes.Email
            Me.newRecipientField.CanBeEmpty = False

            Me.addNewRecipientButton = New Button
            Me.addNewRecipientButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewRecipientButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewRecipientButton.Enabled = False

            Me.recipientsListView = New Common.RecipientListView("Destinataires")
            AddHandler Me.recipientsListView.deleteRecipient, AddressOf Me._emailSettings.removeRecipient
            AddHandler Me.recipientsListView.deleteRecipient, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.recipientsListView.updateRecipient, AddressOf Me._emailSettings.updateRecipient
            AddHandler Me.recipientsListView.updateRecipient, AddressOf Me.raiseSettingChangedEvent


            Me.newDefaultRecipientField = New TextField
            Me.newDefaultRecipientField.PlaceHolder = "Adresse"
            Me.newDefaultRecipientField.ValidationType = TextField.ValidationTypes.Email
            Me.newDefaultRecipientField.CanBeEmpty = False

            Me.addNewDefaultRecipientButton = New Button
            Me.addNewDefaultRecipientButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewDefaultRecipientButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewDefaultRecipientButton.Enabled = False

            Me.defaultRecipientsListView = New Common.RecipientListView("Destinataires par defaut")
            AddHandler Me.defaultRecipientsListView.deleteRecipient, AddressOf Me._emailSettings.removeDefaultRecipient
            AddHandler Me.defaultRecipientsListView.deleteRecipient, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.defaultRecipientsListView.updateRecipient, AddressOf Me._emailSettings.updateDefaultRecipient
            AddHandler Me.defaultRecipientsListView.updateRecipient, AddressOf Me.raiseSettingChangedEvent

            Me.credentialsLabel = New Label
            Me.credentialsLabel.AutoSize = False
            Me.credentialsLabel.TextAlign = ContentAlignment.BottomLeft
            Me.credentialsLabel.Text = "Nom d'usager"

            Me.credentialsField = New TextField
            Me.credentialsField.ValidationType = TextField.ValidationTypes.Email
            Me.credentialsField.CanBeEmpty = False

            Me.passwordLabel = New Label
            Me.passwordLabel.AutoSize = False
            Me.passwordLabel.TextAlign = ContentAlignment.BottomLeft
            Me.passwordLabel.Text = "Mot de passe"

            Me.passwordField = New TextBox
            Me.passwordField.UseSystemPasswordChar = True

            Me.hostLabel = New Label
            Me.hostLabel.AutoSize = False
            Me.hostLabel.TextAlign = ContentAlignment.BottomLeft
            Me.hostLabel.Text = "Serveur d'envoi"

            Me.hostField = New TextField
            Me.hostField.CanBeEmpty = False

            Me.portLabel = New Label
            Me.portLabel.AutoSize = False
            Me.portLabel.TextAlign = ContentAlignment.BottomLeft
            Me.portLabel.Text = "Port d'envoi"

            Me.portField = New TextField
            Me.portField.MaxLength = 5
            Me.portField.ValidationType = TextField.ValidationTypes.Numbers
            Me.portField.CanBeEmpty = False

            Me.cantSeeProtectedEmailSettingsLabel = New Label
            Me.cantSeeProtectedEmailSettingsLabel.Font = New Font(Constants.UI.Fonts.SMALL_DEFAULT_FONT, FontStyle.Underline)
            Me.cantSeeProtectedEmailSettingsLabel.ForeColor = Color.Blue
            Me.cantSeeProtectedEmailSettingsLabel.Text = "*Cliquez ici pour avoir accès au gestionnaire de destinataires par défaut et aux paramètres d'envoi."
            Me.cantSeeProtectedEmailSettingsLabel.Cursor = Cursors.Hand

            Me.adminPasswordPanel = New AdminPasswordPanel
            Me.adminPasswordPanel.IsDraggable = False

            Me.Controls.Add(Me.newRecipientField)
            Me.Controls.Add(Me.addNewRecipientButton)
            Me.Controls.Add(Me.recipientsListView)
            Me.Controls.Add(Me.cantSeeProtectedEmailSettingsLabel)

            AddHandler Me.newRecipientField.ValidationOccured, AddressOf Me.enableAddNewRecipientButton
            AddHandler Me.newDefaultRecipientField.ValidationOccured, AddressOf Me.enableAddNewDefaultRecipientButton

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, EmailSettingsViewLayout)

            Me.newRecipientField.Location = layout.NewRecipientField_Location
            Me.newRecipientField.Size = layout.NewRecipientField_Size

            Me.addNewRecipientButton.Location = layout.AddNewRecipientButton_Location
            Me.addNewRecipientButton.Size = layout.AddNewRecipientButton_Size

            Me.recipientsListView.Location = layout.RecipientsListView_Location
            Me.recipientsListView.ajustLayout(layout.RecipientsListView_Size)

            Me.newDefaultRecipientField.Location = layout.NewDefaultRecipientField_Location
            Me.newDefaultRecipientField.Size = layout.NewDefaultRecipientField_Size

            Me.addNewDefaultRecipientButton.Location = layout.AddNewDefaultRecipientButton_Location
            Me.addNewDefaultRecipientButton.Size = layout.AddNewDefaultRecipientButton_Size

            Me.defaultRecipientsListView.Location = layout.DefaultRecipientsListView_Location
            Me.defaultRecipientsListView.ajustLayout(layout.DefaultRecipientsListView_Size)

            Me.credentialsLabel.Location = layout.CredentialsLabel_Location
            Me.credentialsLabel.Size = layout.CredentialsLabel_Size

            Me.credentialsField.Location = layout.CredentialsField_Location
            Me.credentialsField.Size = layout.CredentialsField_Size

            Me.passwordLabel.Location = layout.PasswordLabel_Location
            Me.passwordLabel.Size = layout.PasswordLabel_Size

            Me.passwordField.Location = layout.PasswordField_Location
            Me.passwordField.Size = layout.PasswordField_Size

            Me.hostLabel.Location = layout.HostLabel_Location
            Me.hostLabel.Size = layout.HostLabel_Size

            Me.hostField.Location = layout.HostField_Location
            Me.hostField.Size = layout.HostField_Size

            Me.portLabel.Location = layout.PortLabel_Location
            Me.portLabel.Size = layout.PortLabel_Size

            Me.portField.Location = layout.PortField_Location
            Me.portField.Size = layout.PortField_Size

            Me.adminPasswordPanel.ajustLayout(Common.AdminPasswordPanel.SIZE_WITH_PARAMETERS_BUTTON)

            Me.cantSeeProtectedEmailSettingsLabel.Location = layout.CantSeeProtectedEmailSettingsLabel_Location
            Me.cantSeeProtectedEmailSettingsLabel.Size = layout.CantSeeProtectedEmailSettingsLabel_Size

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim layout = DirectCast(Me.layout, EmailSettingsViewLayout)

            Me.recipientsListView.ajustLayoutFinal(layout.RecipientsListView_Size)
            Me.defaultRecipientsListView.ajustLayoutFinal(layout.DefaultRecipientsListView_Size)

            Me.adminPasswordPanel.Location = New Point(Me.Width / 2 - adminPasswordPanel.Width / 2, Me.Height / 2 - adminPasswordPanel.Height / 2)

        End Sub

        Public Overrides Sub updateFields()
            Me.updatingFields = True

            Me.recipientsListView.clear()

            For Each _recipient As EmailRecipient In Me._emailSettings.Recipients

                Me.recipientsListView.addObject(_recipient)
            Next
            Me.recipientsListView.refreshList()

            Me.defaultRecipientsListView.clear()

            For Each _recipient As EmailRecipient In Me._emailSettings.DefaultRecipients

                Me.defaultRecipientsListView.addObject(_recipient)
            Next
            Me.defaultRecipientsListView.refreshList()

            Me.credentialsField.DefaultText = Me._emailSettings.Credentials
            Me.credentialsBuffer = Me._emailSettings.Credentials

            Me.passwordField.Text = Me._emailSettings.Password
            Me.passwordBuffer = Me._emailSettings.Password

            Me.hostField.DefaultText = Me._emailSettings.Host
            Me.hostBuffer = Me._emailSettings.Host

            Me.portField.DefaultText = Me._emailSettings.Port
            Me.portBuffer = Me._emailSettings.Port

            Me.Focus()

            Me.updatingFields = False
        End Sub

        Protected Overloads Overrides Sub beforeShow()

            If (ProgramController.SettingsControllers.AdminSettingsController.UserIsAdmin OrElse _
                ProgramController.SettingsControllers.AdminSettingsController.UserCanChangeEmailSettings) Then

                Me.showProtectedEmailSettings()
            Else
                Me.hideProtectedEmailSettings()
            End If

        End Sub

        Public Overrides Sub afterShow()

            Me.hideAdminPasswordPanel()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub showProtectedEmailSettings()

            If (Not Me._protectedEmailSettingsAreShowing) Then

                Me.Controls.Remove(Me.cantSeeProtectedEmailSettingsLabel)

                Me.Controls.Add(Me.newDefaultRecipientField)
                Me.Controls.Add(Me.addNewDefaultRecipientButton)
                Me.Controls.Add(Me.defaultRecipientsListView)
                Me.Controls.Add(Me.credentialsLabel)
                Me.Controls.Add(Me.credentialsField)
                Me.Controls.Add(Me.passwordLabel)
                Me.Controls.Add(Me.passwordField)
                Me.Controls.Add(Me.hostLabel)
                Me.Controls.Add(Me.hostField)
                Me.Controls.Add(Me.portLabel)
                Me.Controls.Add(Me.portField)

                Me._protectedEmailSettingsAreShowing = True
            End If
        End Sub

        Private Sub hideProtectedEmailSettings()

            If (Me._protectedEmailSettingsAreShowing) Then

                Me.Controls.Add(Me.cantSeeProtectedEmailSettingsLabel)

                Me.Controls.Remove(Me.newDefaultRecipientField)
                Me.Controls.Remove(Me.addNewDefaultRecipientButton)
                Me.Controls.Remove(Me.defaultRecipientsListView)
                Me.Controls.Remove(Me.credentialsLabel)
                Me.Controls.Remove(Me.credentialsField)
                Me.Controls.Remove(Me.passwordLabel)
                Me.Controls.Remove(Me.passwordField)
                Me.Controls.Remove(Me.hostLabel)
                Me.Controls.Remove(Me.hostField)
                Me.Controls.Remove(Me.portLabel)
                Me.Controls.Remove(Me.portField)

                Me._protectedEmailSettingsAreShowing = False
            End If
        End Sub

        Private Sub showAdminPasswordPanel() Handles cantSeeProtectedEmailSettingsLabel.Click

            Me.Controls.Add(Me.adminPasswordPanel)
            Me.adminPasswordPanel.BringToFront()
            Me.adminPasswordPanel.Focus()
        End Sub

        Private Sub hideAdminPasswordPanel()

            Me.Controls.Remove(Me.adminPasswordPanel)

        End Sub

        Private Sub onAdminPanelClose(status As Common.PopUpMessage.ClosingStatus) Handles AdminPasswordPanel.CloseEvent

            Me.hideAdminPasswordPanel()
        End Sub

        Private Sub onAuthentication() Handles AdminPasswordPanel.SuccessfulAuthentication
            Me.showProtectedEmailSettings()
        End Sub

        Public Sub enableAddNewRecipientButton()

            If (Me.newRecipientField.IsValid) Then

                Me.addNewRecipientButton.Enabled = True
            Else
                Me.addNewRecipientButton.Enabled = False
            End If
        End Sub

        Public Sub enableAddNewDefaultRecipientButton()

            If (Me.newDefaultRecipientField.IsValid) Then

                Me.addNewDefaultRecipientButton.Enabled = True
            Else
                Me.addNewDefaultRecipientButton.Enabled = False
            End If
        End Sub

        Private Sub addRecipient() Handles addNewRecipientButton.Click

            Me._emailSettings.addRecipient(Me.newRecipientField.Text)
            Me.raiseSettingChangedEvent()

            Me.newRecipientField.DefaultText = ""
            Me.recipientsListView.selectLastItem()
        End Sub

        Private Sub addDefaultRecipient() Handles addNewDefaultRecipientButton.Click

            Me._emailSettings.addDefaultRecipient(Me.newDefaultRecipientField.Text)
            Me.raiseSettingChangedEvent()

            Me.newDefaultRecipientField.DefaultText = ""
            Me.defaultRecipientsListView.selectLastItem()
        End Sub

        Private Sub updateCredentials() Handles credentialsField.LostFocus

            If (Me.credentialsField.IsValid AndAlso _
                Not credentialsField.Text.Equals(credentialsBuffer)) Then

                Me._emailSettings.Credentials = Me.credentialsField.Text
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub updatePassword() Handles passwordField.LostFocus

            If (Not passwordField.Text.Equals(passwordBuffer)) Then

                Me._emailSettings.Password = Me.passwordField.Text
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub updateHost() Handles hostField.LostFocus

            If (Me.hostField.IsValid AndAlso _
                Not hostField.Text.Equals(hostBuffer)) Then

                Me._emailSettings.Host = Me.hostField.Text
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub updatePort() Handles portField.LostFocus

            If (Me.portField.IsValid AndAlso _
                Not portField.Text.Equals(portBuffer)) Then

                Me._emailSettings.Port = Me.portField.Text
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._emailSettings
            End Get
        End Property
    End Class
End Namespace

