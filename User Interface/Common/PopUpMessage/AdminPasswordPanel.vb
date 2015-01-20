Namespace UI.Common

    Public Class AdminPasswordPanel
        Inherits PopUpMessage

        ' Constants
        Public Shared ReadOnly SIZE_WITH_PARAMETERS_BUTTON As Size = New Size(400, 180)
        Public Shared ReadOnly SIZE_WITHOUT_PARAMETERS_BUTTON As Size = New Size(400, 160)
        Private Shared ReadOnly TITLE_LABEL_HEIGHT As Integer = 25
        Private Shared ReadOnly BUTTONS_SIZE As Size = New Size(90, 30)

        ' Components
        Private WithEvents titleLabel As Label
        Private WithEvents passwordField As TextBox
        Private WithEvents iconPanel As Panel
        Private WithEvents succesMessageLabel As Label

        Private WithEvents okButton As Button
        Private WithEvents adminSettingsButton As Button

        ' Attributes
        Private _adminSettingsController As AdminSettingsController

        Private _authenticated As Boolean = False

        Private _iconPanelIsShowing As Boolean = False

        Private _showSettingsButton As Boolean = True

        ' Events
        Public Event SuccessfulAuthentication()

        Public Sub New()
            MyBase.New()

            Me._adminSettingsController = ProgramController.SettingsControllers.AdminSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.titleLabel = New Label
            Me.titleLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.titleLabel.Text = "Permissions : Mot de passe"
            Me.titleLabel.ForeColor = FORE_COLOR
            Me.titleLabel.Font = Constants.UI.Fonts.BIGGER_DEFAULT_FONT_BOLD

            Me.passwordField = New TextBox
            Me.passwordField.AutoSize = False
            Me.passwordField.UseSystemPasswordChar = True
            Me.passwordField.TextAlign = HorizontalAlignment.Center

            Me.iconPanel = New Panel
            Me.iconPanel.BackColor = BACK_COLOR
            Me.iconPanel.BackgroundImageLayout = ImageLayout.Center
            Me.iconPanel.Size = New Size(30, 30)

            Me.succesMessageLabel = New Label
            Me.succesMessageLabel.AutoSize = False
            Me.succesMessageLabel.BackColor = BACK_COLOR
            Me.succesMessageLabel.ForeColor = FORE_COLOR
            Me.succesMessageLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.succesMessageLabel.BorderStyle = Windows.Forms.BorderStyle.None
            Me.ShowSettingsButton = Me._showSettingsButton

            Me.adminSettingsButton = New Button
            Me.adminSettingsButton.TextAlign = ContentAlignment.MiddleRight
            Me.adminSettingsButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.adminSettingsButton.Image = Constants.UI.Images._24x24.SETTINGS
            Me.adminSettingsButton.Size = New Size(115, BUTTONS_SIZE.Height)
            Me.adminSettingsButton.BackColor = FORE_COLOR
            Me.adminSettingsButton.ForeColor = BACK_COLOR
            Me.adminSettingsButton.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.adminSettingsButton.FlatStyle = FlatStyle.Flat
            Me.adminSettingsButton.Text = "Paramètres"

            Me.okButton = New Button
            Me.okButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.okButton.Image = Constants.UI.Images._24x24.GOOD
            Me.okButton.FlatStyle = FlatStyle.Flat
            Me.okButton.Size = BUTTONS_SIZE
            Me.okButton.BackColor = FORE_COLOR
            Me.okButton.ForeColor = BACK_COLOR
            Me.okButton.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.okButton.TextAlign = ContentAlignment.MiddleCenter
            Me.okButton.Text = "Ok"

            Me.Controls.Add(Me.titleLabel)
            Me.Controls.Add(Me.passwordField)
            Me.Controls.Add(Me.okButton)

            AddHandler Me.okButton.Click, AddressOf Me.authenticate

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Me.titleLabel.Location = New Point(0, 5)

            If (Me.ShowCloseButton) Then
                Me.titleLabel.Size = New Size(Me.Width - (Me.Width - Me.closeButton.Location.X), 25)
            Else
                Me.titleLabel.Size = New Size(Me.Width, 25)
            End If

            Me.okButton.Location = New Point(Me.Width - okButton.Width - 1, Me.Height - okButton.Height - 1)
            Me.adminSettingsButton.Location = New Point(5, okButton.Location.Y)

            Me.iconPanel.Location = New Point(5, Me.Height / 2 - BUTTONS_SIZE.Height / 2)

            If (Me._iconPanelIsShowing) Then

                Me.passwordField.Location = New Point(Me.iconPanel.Location.X + Me.iconPanel.Width + 5, Me.iconPanel.Location.Y - 2)
                Me.passwordField.Size = New Size(Me.Width - 20 - iconPanel.Width - iconPanel.Location.X, 35)
            Else

                Me.passwordField.Location = New Point(15, Me.iconPanel.Location.Y - 2)
                Me.passwordField.Size = New Size(Me.Width - 30, 35)
            End If

            Me.succesMessageLabel.Location = New Point(Me.iconPanel.Location.X + Me.iconPanel.Width + 5, TITLE_LABEL_HEIGHT)
            Me.succesMessageLabel.Size = New Size(Me.Width - 20 - iconPanel.Width, Me.Height - TITLE_LABEL_HEIGHT - BUTTONS_SIZE.Height)

        End Sub

        Public Property ShowSettingsButton As Boolean
            Get
                Return Me._showSettingsButton
            End Get
            Set(value As Boolean)
                Me._showSettingsButton = value

                If (value) Then

                    Me.succesMessageLabel.Text = "Appuyer sur les boutons 'Ok' ou 'X' pour cacher ce message. Vous pouvez également appuyer sur le bouton 'Paramètres' pour accèder aux paramètres des permissions."

                Else
                    Me.succesMessageLabel.Text = "Appuyer sur les boutons 'Ok' ou 'X' pour cacher ce message."
                End If
            End Set
        End Property

        Private Property Authenticated As Boolean
            Get
                Return Me._authenticated
            End Get
            Set(value As Boolean)

                Me._authenticated = value

                If (Not _iconPanelIsShowing) Then
                    Me.Controls.Add(Me.iconPanel)
                    Me._iconPanelIsShowing = True
                End If

                If (value) Then

                    Me.Controls.Remove(passwordField)
                    Me.Controls.Add(Me.succesMessageLabel)
                    Me.iconPanel.BackgroundImage = Constants.UI.Images._24x24.GOOD

                    If (Me._showSettingsButton) Then
                        Me.Controls.Add(Me.adminSettingsButton)
                    End If

                    Me.titleLabel.Text = "Valide!"

                    RemoveHandler Me.okButton.Click, AddressOf Me.authenticate
                    AddHandler Me.okButton.Click, Sub()
                                                      Me.raiseCloseEvent(ClosingStatus.Ok)
                                                  End Sub

                    RaiseEvent SuccessfulAuthentication()

                    Me.Focus()

                Else

                    Me.iconPanel.BackgroundImage = Constants.UI.Images._24x24.DELETE
                    Beep()

                End If

                Me.ajustLayout()
            End Set
        End Property

        Private Sub authenticate(sender As Object, e As EventArgs)

            Me.Authenticated = Me._adminSettingsController.getAdminRights(Me.passwordField.Text)

        End Sub

        Private Sub goToSettings() Handles adminSettingsButton.Click

            ProgramController.UIController.SettingsFrame.selectView(ProgramController.UIController.AdminSettingsView)

        End Sub

        Private Sub onEnterKey(sender As Object, e As KeyEventArgs) Handles passwordField.KeyDown

            If (e.KeyCode = Keys.Enter) Then

                e.SuppressKeyPress = True

                Me.authenticate(Nothing, Nothing)
            End If
        End Sub

        Private Sub onEnterKey2(sender As Object, e As PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown

            If (e.KeyCode = Keys.Enter) Then

                Me.raiseCloseEvent(ClosingStatus.Ok)
            End If
        End Sub

        Private Sub _onFocus() Handles Me.GotFocus

            If (Not Me.Authenticated) Then
                Me.passwordField.Focus()
            End If
        End Sub

    End Class
End Namespace
