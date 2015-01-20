
Namespace UI.Common

    Public Class EditableRecipientListItem
        Inherits RecipientListItem

        ' Constants
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25)

        ' Components
        Private WithEvents addressField As TextField

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        ' Attributes

        ' Events
        Public Event deleteRecipient(recipient As EmailRecipient)
        Public Event updateRecipient(recipient As EmailRecipient, newAddress As String)


        Public Sub New(recipient As EmailRecipient)
            MyBase.New(recipient)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.addressLabel = New Label
            Me.addressLabel.AutoSize = False
            Me.addressLabel.Text = Me.ItemObject.Address
            Me.addressLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.addressField = New TextField
            Me.addressField.ValidationType = TextField.ValidationTypes.Email
            Me.addressField.CanBeEmpty = False

            Me.deleteButton = New Button
            Me.deleteButton.Image = Constants.UI.Images._16x16.DELETE
            Me.deleteButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.deleteButton.Size = BUTTONS_SIZE
            Me.deleteButton.BackColor = Me.BackColor

            Me.editButton = New Button
            Me.editButton.Image = Constants.UI.Images._16x16.EDIT
            Me.editButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.editButton.Size = BUTTONS_SIZE
            Me.editButton.BackColor = Me.BackColor

            Me.cancelEditButton = New Button
            Me.cancelEditButton.Image = Constants.UI.Images._16x16.WRONG
            Me.cancelEditButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.cancelEditButton.Size = BUTTONS_SIZE
            Me.cancelEditButton.BackColor = Me.BackColor

            Me.confirmEditButton = New Button
            Me.confirmEditButton.Image = Constants.UI.Images._16x16.GOOD
            Me.confirmEditButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.confirmEditButton.Size = BUTTONS_SIZE
            Me.confirmEditButton.BackColor = Me.BackColor

            Me.Controls.Add(Me.addressLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            AddHandler Me.addressField.ValidationOccured, AddressOf Me.enableConfirmEditButton

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.addressLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            Me.addressLabel.Size = New Size(newSize.Width - 2 * BUTTONS_SIZE.Width - 4 * SPACE_BETWEEN_CONTROLS_X, newSize.Height)

            Me.addressField.Location = Me.addressLabel.Location
            Me.addressField.Size = Me.addressLabel.Size

            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, addressLabel.Click

            raiseClickEvent()

        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent deleteRecipient(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent updateRecipient(Me.ItemObject, Me.addressField.Text)

        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.addressField.IsValid) Then

                Me.confirmEditButton.Enabled = True
            Else

                Me.confirmEditButton.Enabled = False
            End If
        End Sub

        Private Sub cancelEditing() Handles cancelEditButton.Click

            enterReadMode()

        End Sub

        Private Sub enterWriteMode() Handles editButton.Click

            Me._currentMode = Mode.WRITE

            Me.Controls.Remove(Me.addressLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.addressField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.addressField.DefaultText = Me.addressLabel.Text

            Me.enableConfirmEditButton()
        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.addressField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.addressLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles addressField.KeyDown

            If (e.KeyCode = Keys.Enter) Then

                If (Me.confirmEditButton.Enabled) Then

                    raiseUpdateEvent()
                Else
                    Beep()
                End If

            ElseIf (e.KeyCode = Keys.Escape) Then

                cancelEditing()
            End If
        End Sub

    End Class
End Namespace
