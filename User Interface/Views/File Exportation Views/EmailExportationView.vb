Namespace UI

    ''' <remarks>
    ''' Contains the input fields for email sending (from, to, comments...)
    ''' Also maybe the progressbar of the sending
    ''' </remarks>
    Public Class EmailExportationView
        Inherits View

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Courriel"

        ' Components
        Private attachmentInformationLabel As Label

        Private operatorsLabel As Label
        Private WithEvents operatorsComboBox As ComboBox

        Private WithEvents recipientsListView As Common.RecipientListView

        Private commentsLabel As Label
        Private WithEvents commentsTextField As TextBox

        Private buttonsPanel As Panel

        Private WithEvents backButton As Common.BackButton
        Private WithEvents sendButton As Button

        Private WithEvents sendingEmailPanel As Common.UserMessagePanel
        Private WithEvents emailSentSuccessfullyPanel As Common.UserMessagePanel
        Private WithEvents errorSendingEmailPanel As Common.UserMessagePanel

        ' Attributes
        Private emailSettings As EmailSettingsController
        Private exportationController As FileExportationController

        Public Sub New()
            MyBase.New()

            Me.layout = New EmailExportationViewLayout

            Me.emailSettings = ProgramController.SettingsControllers.EmailSettingsController
            Me.exportationController = ProgramController.FileExportationController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.attachmentInformationLabel = New Label
            Me.attachmentInformationLabel.AutoSize = False
            Me.attachmentInformationLabel.TextAlign = ContentAlignment.TopCenter
            Me.attachmentInformationLabel.ForeColor = Constants.UI.Colors.LIGHT_GREY
            Me.attachmentInformationLabel.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.operatorsLabel = New Label
            Me.operatorsLabel.AutoSize = False
            Me.operatorsLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.operatorsLabel.Text = "Opérateur"

            Me.operatorsComboBox = New ComboBox
            Me.operatorsComboBox.DropDownStyle = ComboBoxStyle.DropDownList

            Me.recipientsListView = New Common.RecipientListView("Destinataires")
            Me.recipientsListView.CheckableItems = True

            Me.commentsLabel = New Label
            Me.commentsLabel.AutoSize = False
            Me.commentsLabel.Text = "Commentaires"

            Me.commentsTextField = New TextBox
            Me.commentsTextField.Multiline = True

            Me.buttonsPanel = New Panel

            Me.backButton = New Common.BackButton
            Me.backButton.Size = New Size(Common.BackButton.BUTTON_WIDTH, EmailExportationViewLayout.CONTROL_BUTTONS_HEIGHT)

            Me.sendButton = New Button
            Me.sendButton.TextAlign = ContentAlignment.MiddleCenter
            Me.sendButton.ImageAlign = ContentAlignment.MiddleRight
            Me.sendButton.Image = Constants.UI.Images._32x32.GOOD
            Me.sendButton.Size = EmailExportationViewLayout.SEND_BUTTON_SIZE
            Me.sendButton.Font = Constants.UI.Fonts.DEFAULT_FONT_BOLD
            Me.sendButton.Text = "Envoyer"

            Me.buttonsPanel.Controls.Add(Me.backButton)
            Me.buttonsPanel.Controls.Add(Me.sendButton)

            Me.Controls.Add(Me.attachmentInformationLabel)
            Me.Controls.Add(Me.operatorsLabel)
            Me.Controls.Add(Me.operatorsComboBox)
            Me.Controls.Add(Me.recipientsListView)
            Me.Controls.Add(Me.commentsLabel)
            Me.Controls.Add(Me.commentsTextField)
            Me.Controls.Add(Me.buttonsPanel)

        End Sub

        Protected Overloads Overrides Sub ajustLayout(newSize As Size)

            Dim layout = DirectCast(Me.layout, EmailExportationViewLayout)

            Me.attachmentInformationLabel.Location = layout.AttachmentInformationLabel_Location
            Me.attachmentInformationLabel.Size = layout.AttachmentInformationLabel_Size

            Me.operatorsLabel.Location = layout.OperatorsLabel_Location
            Me.operatorsLabel.Size = layout.OperatorsLabel_Size

            Me.operatorsComboBox.Location = layout.OperatorsComboBox_Location
            Me.operatorsComboBox.Size = layout.OperatorsComboBox_Size

            Me.recipientsListView.Location = layout.RecipientsListView_Location
            Me.recipientsListView.ajustLayout(layout.RecipientsListView_Size)

            Me.commentsLabel.Location = layout.CommentsLabel_Location
            Me.commentsLabel.Size = layout.CommentsLabel_Size

            Me.commentsTextField.Location = layout.CommentsTextField_Location
            Me.commentsTextField.Size = layout.CommentsTextField_Size

            Me.buttonsPanel.Location = layout.ButtonsPanel_Location
            Me.buttonsPanel.Size = layout.ButtonsPanel_Size

            Me.backButton.Location = layout.BackButton_Location

            Me.sendButton.Location = layout.SendButton_Location

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal(newSize As Size)

            Dim layout = DirectCast(Me.layout, EmailExportationViewLayout)

            Me.recipientsListView.ajustLayoutFinal(layout.RecipientsListView_Size)

        End Sub

        Protected Overloads Overrides Sub beforeShow()

            Dim filesSize As Double = 0

            For Each _file As File In Me.exportationController.FilesToExport

                filesSize += _file.getFileInfo.Length

            Next

            Dim filesSizeString = (filesSize / 1000000).ToString("N1")

            Me.attachmentInformationLabel.Text = Me.exportationController.FilesToExport.Count & " fichiers en pièce jointe (" & filesSizeString & " Mo)"

            Me.operatorsComboBox.Items.Clear()
            Me.operatorsComboBox.Items.Add(FactoryOperator.DEFAULT_OPERATOR)
            For Each operatorInfo As XmlSettings.OperatorsNode.OperatorInfo In XmlSettings.Settings.instance.Usine.OperatorsInfo.OPERATORS

                Me.operatorsComboBox.Items.Add(New FactoryOperator(operatorInfo.FIRST_NAME, operatorInfo.LAST_NAME))

            Next
            Me.operatorsComboBox.SelectedItem = Me.exportationController.Sender

            Me.recipientsListView.clear()

            For Each _recipient As EmailRecipient In Me.emailSettings.Recipients
                Me.recipientsListView.addObject(_recipient)
            Next
            Me.recipientsListView.refreshList()

            Me.enableSendButton()
        End Sub

        Private Sub onRecipientChecked(recipient As EmailRecipient, checked As Boolean) Handles recipientsListView.ItemChecked
            Me.emailSettings.setRecipientSelection(recipient.Address, checked)
            Me.enableSendButton()
        End Sub

        Public Overrides Sub afterShow()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub showSendingEmailPanel()

            If (IsNothing(Me.sendingEmailPanel)) Then

                Me.sendingEmailPanel = New Common.UserMessagePanel("Statut de l'envoi", "En attente ...", Nothing)
                Me.sendingEmailPanel.ajustLayout(New Size(300, 100))
            End If

            Me.sendingEmailPanel.Location = New Point((Me.Width - Me.sendingEmailPanel.Width) / 2, (Me.Height - Me.sendingEmailPanel.Height) / 2)
            Me.Controls.Add(Me.sendingEmailPanel)
            Me.sendingEmailPanel.BringToFront()
        End Sub

        Private Sub hideSendingEmailPanel()
            Me.Controls.Remove(Me.sendingEmailPanel)
        End Sub

        Public Sub showEmailSentSuccessfullyPanel()

            Me.hideSendingEmailPanel()

            If (IsNothing(Me.emailSentSuccessfullyPanel)) Then

                Me.emailSentSuccessfullyPanel = New Common.UserMessagePanel("Statut de l'envoi", "Succès!", Constants.UI.Images._64x64.GOOD)
                Me.emailSentSuccessfullyPanel.ajustLayout(New Size(300, 100))
            End If

            Me.emailSentSuccessfullyPanel.Location = New Point((Me.Width - Me.emailSentSuccessfullyPanel.Width) / 2, (Me.Height - Me.emailSentSuccessfullyPanel.Height) / 2)
            Me.Controls.Add(Me.emailSentSuccessfullyPanel)
            Me.emailSentSuccessfullyPanel.BringToFront()

            Me.enableSendButton()
        End Sub

        Private Sub hideEmailSentSuccessfullyPanel() Handles emailSentSuccessfullyPanel.CloseEvent
            Me.Controls.Remove(Me.emailSentSuccessfullyPanel)
        End Sub

        Public Sub showErrorSendingEmailPanel()

            Me.hideSendingEmailPanel()

            If (IsNothing(Me.errorSendingEmailPanel)) Then

                Me.errorSendingEmailPanel = New Common.UserMessagePanel("Statut de l'envoi", "Erreur!", Constants.UI.Images._64x64.WARNING)
                Me.errorSendingEmailPanel.ajustLayout(New Size(300, 100))
            End If

            Me.errorSendingEmailPanel.Location = New Point((Me.Width - Me.errorSendingEmailPanel.Width) / 2, (Me.Height - Me.errorSendingEmailPanel.Height) / 2)
            Me.Controls.Add(Me.errorSendingEmailPanel)
            Me.errorSendingEmailPanel.BringToFront()

            Me.enableSendButton()
        End Sub

        Private Sub hideErrorSendingEmailPanel() Handles errorSendingEmailPanel.CloseEvent
            Me.Controls.Remove(Me.errorSendingEmailPanel)
        End Sub

        Private Sub enableSendButton()

            Dim atLeast1Recipient As Boolean = Me.emailSettings.DefaultRecipients.Count > 1

            If (Not atLeast1Recipient) Then

                For Each recipients In Me.emailSettings.Recipients

                    If (recipients.Selected) Then
                        atLeast1Recipient = True
                        Exit For
                    End If
                Next
            End If

            If (Not Me.exportationController.SendingInProgress AndAlso _
                Me.exportationController.FilesToExport.Count > 0 AndAlso _
                atLeast1Recipient) Then

                Me.sendButton.Enabled = True
            Else
                Me.sendButton.Enabled = False
            End If
        End Sub

        Private Sub cancelSend(status As Common.PopUpMessage.ClosingStatus) Handles sendingEmailPanel.CloseEvent

            If (status = Common.PopUpMessage.ClosingStatus.Cancel) Then

                Me.exportationController.cancelEmailSending()
                Me.hideSendingEmailPanel()

                Me.enableSendButton()
            End If
        End Sub

        Private Sub send() Handles sendButton.Click

            Me.exportationController.sendFiles(commentsTextField.Text)

            If (Me.exportationController.SendingInProgress) Then

                Me.hideEmailSentSuccessfullyPanel()
                Me.hideErrorSendingEmailPanel()

                Me.showSendingEmailPanel()

                Me.sendButton.Enabled = False
            End If
        End Sub

        Private Sub goBack() Handles backButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.FileExportationView)
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property
    End Class
End Namespace
