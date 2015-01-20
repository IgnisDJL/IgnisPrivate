Namespace UI.Common

    Public Class UserMessagePanel
        Inherits PopUpMessage

        ' Constants
        Private Shared ReadOnly TITLE_LABEL_HEIGHT As Integer = 30
        Private Shared ReadOnly BUTTONS_SIZE As Size = New Size(90, 30)

        ' Components
        Private WithEvents titleLabel As TextBox
        Private WithEvents messageLabel As TextBox
        Private WithEvents iconPanel As Panel

        Private WithEvents okButton As Button
        Private WithEvents cancelButton As Common.CancelButton

        ' Attributes
        Private title As String
        Private message As String
        Private image As Image

        Private okCancelEnabled As Boolean

        Public Sub New(title As String, message As String, icon As Image, Optional withOkCancelButtons As Boolean = False)

            Me.title = title
            Me.message = message
            Me.image = icon

            Me.okCancelEnabled = withOkCancelButtons

            Me.initializeComponents()

            AddHandler iconPanel.MouseDown, AddressOf Me._onMouseDown
            AddHandler iconPanel.MouseMove, AddressOf Me.dragLocation
            AddHandler iconPanel.MouseUp, AddressOf Me._onMouseUp

        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.iconPanel = New Panel

            Me.iconPanel.BackColor = BACK_COLOR
            Me.iconPanel.BackgroundImageLayout = ImageLayout.Center
            Me.iconPanel.BackgroundImage = Me.image

            If (Not IsNothing(Me.title)) Then

                Me.titleLabel = New TextBox
                Me.titleLabel.Text = title
                Me.titleLabel.BackColor = BACK_COLOR
                Me.titleLabel.ForeColor = FORE_COLOR
                Me.titleLabel.ReadOnly = True
                Me.titleLabel.BorderStyle = Windows.Forms.BorderStyle.None
                Me.titleLabel.TextAlign = HorizontalAlignment.Center
                Me.titleLabel.Font = Constants.UI.Fonts.BIGGER_DEFAULT_FONT_BOLD
                Me.titleLabel.Size = New Size(0, TITLE_LABEL_HEIGHT)
            End If

            Me.messageLabel = New TextBox
            Me.messageLabel.Multiline = True
            Me.messageLabel.ReadOnly = True
            Me.messageLabel.BackColor = BACK_COLOR
            Me.messageLabel.ForeColor = FORE_COLOR
            Me.messageLabel.WordWrap = True
            Me.messageLabel.TextAlign = HorizontalAlignment.Left
            Me.messageLabel.BorderStyle = Windows.Forms.BorderStyle.None
            Me.messageLabel.Font = Constants.UI.Fonts.DEFAULT_FONT
            Me.messageLabel.Text = Me.message

            Me.Controls.Add(iconPanel)
            Me.Controls.Add(messageLabel)
            Me.Controls.Add(titleLabel)

            If (Me.okCancelEnabled) Then

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

                Me.cancelButton = New Common.CancelButton
                Me.cancelButton.FlatStyle = FlatStyle.Flat
                Me.cancelButton.Size = BUTTONS_SIZE
                Me.cancelButton.BackColor = FORE_COLOR
                Me.cancelButton.ForeColor = BACK_COLOR
                Me.cancelButton.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
                Me.cancelButton.Text = "Annuler"
                Me.cancelButton.TextAlign = ContentAlignment.MiddleRight
                Me.cancelButton.UseVisualStyleBackColor = False

                Me.Controls.Add(okButton)
                Me.Controls.Add(cancelButton)

                Me.okButton.TabIndex = 1

            Else
                Me.closeButton.TabIndex = 1
            End If

        End Sub

        Protected Overrides Sub ajustLayout()

            Me.iconPanel.Location = New Point(0, 0)
            Me.iconPanel.Size = New Size(Me.Width / 4, Me.Height)

            If (Not IsNothing(Me.titleLabel)) Then
                Me.titleLabel.Location = New Point(Me.iconPanel.Width - closeButton.Width, 5)
                Me.titleLabel.Size = New Size(Me.Width * 3 / 4, TITLE_LABEL_HEIGHT)
            End If

            Me.messageLabel.Location = New Point(Me.iconPanel.Width, TITLE_LABEL_HEIGHT + 5)
            Me.messageLabel.Size = New Size(Me.Width * 3 / 4, Me.Height - TITLE_LABEL_HEIGHT - 5)

            If (okCancelEnabled) Then
                Me.messageLabel.Height -= BUTTONS_SIZE.Height + 5
                Me.okButton.Location = New Point(Me.Width - okButton.Width - 1, Me.Height - okButton.Height - 1)
                Me.cancelButton.Location = New Point(Me.okButton.Location.X - cancelButton.Width - 5, okButton.Location.Y)
            End If

        End Sub

        Private Sub _onKeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown

            If (okCancelEnabled) Then

                If (e.KeyCode = Keys.Enter) Then
                    ok()
                ElseIf (e.KeyCode = Keys.Escape) Then
                    cancel()
                End If
            End If

        End Sub

        Public Sub ajustLayoutFinal(newSize As Size)

            'ajustLayout()
        End Sub

        Private Sub ok() Handles okButton.Click
            Me.raiseCloseEvent(ClosingStatus.Ok)
        End Sub

        Private Sub cancel() Handles cancelButton.Click
            Me.raiseCloseEvent(ClosingStatus.Cancel)
        End Sub

        Private Sub x() Handles titleLabel.GotFocus

        End Sub

    End Class

End Namespace
