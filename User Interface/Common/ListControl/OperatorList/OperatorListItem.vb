Imports IGNIS.UI.Common

Namespace UI

    Public Class OperatorListItem
        Inherits Common.ListItem(Of FactoryOperator)

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25)

        ' Components
        Private WithEvents firstNameLabel As Label
        Private WithEvents lastNameLabel As Label

        Private WithEvents firstNameField As TextField
        Private WithEvents lastNameField As TextField

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        Private instructionToolTip As ToolTip

        ' Attributes

        ' Events
        Public Event deleteOperator(_operator As FactoryOperator)
        Public Event updateOperator(_operator As FactoryOperator, newFirstName As String, newLastName As String)


        Public Sub New(_operator As FactoryOperator)
            MyBase.New(_operator)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.firstNameLabel = New Label
            Me.firstNameLabel.Text = Me.ItemObject.FirstName
            Me.firstNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.lastNameLabel = New Label
            Me.lastNameLabel.Text = Me.ItemObject.LastName
            Me.lastNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.firstNameField = New TextField
            Me.firstNameField.AutoSize = False
            Me.firstNameField.ValidationType = TextField.ValidationTypes.Text
            Me.firstNameField.CanBeEmpty = False

            Me.lastNameField = New TextField
            Me.lastNameField.AutoSize = False
            Me.lastNameField.ValidationType = TextField.ValidationTypes.Text
            Me.lastNameField.CanBeEmpty = False

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

            Me.Controls.Add(Me.firstNameLabel)
            Me.Controls.Add(Me.lastNameLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            AddHandler Me.firstNameField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.lastNameField.ValidationOccured, AddressOf Me.enableConfirmEditButton

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.firstNameLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            Me.firstNameLabel.Size = New Size((newSize.Width - 2 * BUTTONS_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) / 2, newSize.Height)

            Me.firstNameField.Location = Me.firstNameLabel.Location
            Me.firstNameField.Size = Me.firstNameLabel.Size

            Me.lastNameLabel.Location = New Point(Me.firstNameLabel.Location.X + Me.firstNameLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.lastNameLabel.Size = New Size(Me.firstNameLabel.Width, newSize.Height)

            Me.lastNameField.Location = Me.lastNameLabel.Location
            Me.lastNameField.Size = Me.lastNameLabel.Size

            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, firstNameLabel.Click, lastNameLabel.Click

            raiseClickEvent()

        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent deleteOperator(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent updateOperator(Me.ItemObject, Me.firstNameField.Text, Me.lastNameField.Text)

        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.firstNameField.IsValid AndAlso _
                Me.lastNameField.IsValid) Then

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

            Me.Controls.Remove(Me.firstNameLabel)
            Me.Controls.Remove(Me.lastNameLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.firstNameField)
            Me.Controls.Add(Me.lastNameField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.firstNameField.DefaultText = Me.firstNameLabel.Text
            Me.lastNameField.DefaultText = Me.lastNameLabel.Text

            Me.enableConfirmEditButton()
        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.firstNameField)
            Me.Controls.Remove(Me.lastNameField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.firstNameLabel)
            Me.Controls.Add(Me.lastNameLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles firstNameField.KeyDown, lastNameField.KeyDown

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
