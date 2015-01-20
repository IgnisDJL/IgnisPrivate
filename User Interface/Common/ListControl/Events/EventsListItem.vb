Imports IGNIS.UI.Common

Namespace UI

    Public Class EventsListItem
        Inherits Common.ListItem(Of XmlSettings.EventsNode.EventInfo)

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25)

        ' Components
        Private WithEvents nameLabel As Label
        Private WithEvents replaceLabel As Label
        Private WithEvents nameField As TextField
        Private WithEvents replaceField As TextField
        Private WithEvents startCheckBox As CheckBox
        Private WithEvents stopCheckBox As CheckBox

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        Private nameToolTip As ToolTip
        Private replaceToolTip As ToolTip

        ' Attributes

        ' Events
        Public Event DeleteEventInfo(eventInfo As XmlSettings.EventsNode.EventInfo)
        Public Event UpdateEventInfo(eventInfo As XmlSettings.EventsNode.EventInfo, newName As String, newReplace As String, newIsStart As Boolean, newIsStop As Boolean)


        Public Sub New(eventInfo As XmlSettings.EventsNode.EventInfo)
            MyBase.New(eventInfo)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.nameLabel = New Label
            Me.nameLabel.AutoSize = False
            Me.nameLabel.Text = Me.ItemObject.MESSAGE
            Me.nameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.replaceLabel = New Label
            Me.replaceLabel.AutoSize = False
            Me.replaceLabel.Text = Me.ItemObject.ALT_MESSAGE
            Me.replaceLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.nameField = New TextField
            Me.nameField.AutoSize = False
            Me.nameField.TextAlign = HorizontalAlignment.Left
            Me.nameField.CanBeEmpty = False

            Me.replaceField = New TextField
            Me.replaceField.AutoSize = False
            Me.replaceField.TextAlign = HorizontalAlignment.Left
            Me.replaceField.ValidationType = TextField.ValidationTypes.Text
            Me.replaceField.CanBeEmpty = False

            Me.startCheckBox = New CheckBox
            Me.startCheckBox.Enabled = False
            Me.startCheckBox.Checked = XmlSettings.Settings.instance.Usine.Events.Start_.START_EVENTS.Contains(Me.ItemObject)

            Me.stopCheckBox = New CheckBox
            Me.stopCheckBox.Enabled = False
            Me.stopCheckBox.Checked = XmlSettings.Settings.instance.Usine.Events.Stop_.STOP_EVENTS.Contains(Me.ItemObject)


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

            Me.Controls.Add(Me.nameLabel)
            Me.Controls.Add(Me.replaceLabel)
            Me.Controls.Add(Me.startCheckBox)
            Me.Controls.Add(Me.stopCheckBox)

            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            Me.nameToolTip = New ToolTip()
            Me.nameToolTip.BackColor = Color.White
            Me.nameToolTip.InitialDelay = 300
            Me.nameToolTip.AutoPopDelay = 5000
            Me.nameToolTip.SetToolTip(Me.nameLabel, Me.nameLabel.Text)

            Me.replaceToolTip = New ToolTip()
            Me.replaceToolTip.BackColor = Color.White
            Me.replaceToolTip.InitialDelay = 300
            Me.replaceToolTip.AutoPopDelay = 5000
            Me.replaceToolTip.SetToolTip(Me.replaceLabel, Me.replaceLabel.Text)

            AddHandler Me.replaceField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.nameField.ValidationOccured, AddressOf Me.enableConfirmEditButton

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            ' New Event Name Field
            Me.nameLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            Me.nameLabel.Size = New Size(200, Me.Height)

            Me.nameField.Location = Me.nameLabel.Location
            Me.nameField.Size = Me.nameLabel.Size

            ' New Event Replace Field
            Me.replaceLabel.Location = New Point(nameLabel.Location.X + Me.nameLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.replaceLabel.Size = New Size(200, Me.Height)

            Me.replaceField.Location = Me.replaceLabel.Location
            Me.replaceField.Size = Me.replaceLabel.Size

            ' New Event Start Check Box
            Me.startCheckBox.Location = New Point(Me.replaceLabel.Location.X + Me.replaceLabel.Width, 0)
            Me.startCheckBox.Size = New Size((Me.Width - nameLabel.Width - replaceLabel.Width - 2 * BUTTONS_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) / 2, Me.Height)

            ' New Event Stop Check Box
            Me.stopCheckBox.Location = New Point(Me.startCheckBox.Location.X + Me.startCheckBox.Width, 0)
            Me.stopCheckBox.Size = Me.startCheckBox.Size


            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location
        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, nameLabel.Click, replaceLabel.Click

            Me.raiseClickEvent()
        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent DeleteEventInfo(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent UpdateEventInfo(Me.ItemObject, Me.nameField.Text, Me.replaceField.Text, Me.startCheckBox.Checked, Me.stopCheckBox.Checked)

        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.nameField.IsValid AndAlso _
                Me.replaceField.IsValid) Then

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

            Me.Controls.Remove(Me.nameLabel)
            Me.Controls.Remove(Me.replaceLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.nameField)
            Me.Controls.Add(Me.replaceField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.startCheckBox.Enabled = True
            Me.stopCheckBox.Enabled = True

            Me.nameField.DefaultText = Me.nameLabel.Text
            Me.replaceField.DefaultText = Me.replaceLabel.Text

        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.nameField)
            Me.Controls.Remove(Me.replaceField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.nameLabel)
            Me.Controls.Add(Me.replaceLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            Me.startCheckBox.Enabled = False
            Me.stopCheckBox.Enabled = False

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles nameField.KeyDown, replaceField.KeyDown

            If (e.KeyCode = Keys.Enter) Then

                If (Me.confirmEditButton.Enabled = True) Then
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
