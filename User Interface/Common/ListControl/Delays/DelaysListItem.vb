Imports IGNIS.UI.Common

Namespace UI

    Public Class DelaysListItem
        Inherits Common.ListItem(Of DelayCode)

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25)

        ' Components
        Private WithEvents codeLabel As Label
        Private WithEvents codeField As TextField

        Private WithEvents descriptionLabel As Label
        Private WithEvents descriptionField As TextField

        Private WithEvents typeLabel As Label
        Private WithEvents typeField As ComboBox

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        ' Attributes


        ' Events
        Public Event DeleteDelayCode(code As DelayCode)
        Public Event UpdateDelayCode(code As DelayCode, newDelayCode As String, newDelayDescription As String, newDelayType As DelayType)


        Public Sub New(delayCode As DelayCode, settingsController As EventsSettingsController)
            MyBase.New(delayCode)

            Me.initializeComponents()

            For Each _delayType As DelayType In settingsController.DelayTypes

                Me.typeField.Items.Add(_delayType)
            Next

            Me.typeField.SelectedItem = Me.ItemObject.Type
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.codeLabel = New Label
            Me.codeLabel.Text = Me.ItemObject.Code
            Me.codeLabel.AutoSize = False
            Me.codeLabel.TextAlign = ContentAlignment.MiddleCenter

            Me.codeField = New TextField
            Me.codeField.TextAlign = HorizontalAlignment.Center
            Me.codeField.AutoSize = False
            Me.codeField.ValidationType = TextField.ValidationTypes.Text
            Me.codeField.CanBeEmpty = False

            Me.descriptionLabel = New Label
            Me.descriptionLabel.Text = Me.ItemObject.Description
            Me.descriptionLabel.AutoSize = False
            Me.descriptionLabel.TextAlign = ContentAlignment.MiddleCenter

            Me.descriptionField = New TextField
            Me.descriptionField.AutoSize = False
            Me.descriptionField.ValidationType = TextField.ValidationTypes.Text
            Me.descriptionField.CanBeEmpty = False

            Me.typeLabel = New Label
            Me.typeLabel.Text = ItemObject.Type.Name
            Me.typeLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.typeField = New ComboBox
            Me.typeField.AutoSize = False
            Me.typeField.Font = Constants.UI.Fonts.DEFAULT_FONT
            Me.typeField.DropDownStyle = ComboBoxStyle.DropDownList
            Me.typeField.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler Me.typeField.DrawItem, AddressOf Me.setTypeItemsBackColor


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


            Me.Controls.Add(Me.codeLabel)
            Me.Controls.Add(Me.descriptionLabel)
            Me.Controls.Add(Me.typeLabel)

            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            AddHandler Me.codeField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.descriptionField.ValidationOccured, AddressOf Me.enableConfirmEditButton

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.codeLabel.Location = New Point(0, 0)
            Me.codeLabel.Size = New Size(70, Me.Height)

            Me.codeField.Location = Me.codeLabel.Location
            Me.codeField.Size = Me.codeLabel.Size

            Me.descriptionLabel.Location = New Point(Me.codeLabel.Location.X + Me.codeLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.descriptionLabel.Size = New Size(Me.Width - 70 - 180 - 2 * BUTTONS_SIZE.Width - 4 * SPACE_BETWEEN_CONTROLS_X, Me.Height)

            Me.descriptionField.Location = Me.descriptionLabel.Location
            Me.descriptionField.Size = Me.descriptionLabel.Size

            Me.typeLabel.Location = New Point(Me.descriptionLabel.Location.X + Me.descriptionLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.typeLabel.Size = New Size(180, Me.Height - 5)

            Me.typeField.Location = Me.typeLabel.Location
            Me.typeField.Size = Me.typeLabel.Size

            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location
        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, codeLabel.Click, descriptionLabel.Click, typeLabel.Click

            Me.raiseClickEvent()
        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent DeleteDelayCode(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent UpdateDelayCode(Me.ItemObject, Me.codeField.Text, Me.descriptionField.Text, Me.typeField.SelectedItem)

        End Sub

        Private Sub cancelEditing() Handles cancelEditButton.Click

            enterReadMode()
        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.codeField.IsValid AndAlso _
                Me.descriptionField.IsValid) Then

                Me.confirmEditButton.Enabled = True
            Else
                Me.confirmEditButton.Enabled = False
            End If
        End Sub

        Private Sub enterWriteMode() Handles editButton.Click

            Me._currentMode = Mode.WRITE

            Me.Controls.Remove(Me.codeLabel)
            Me.Controls.Remove(Me.descriptionLabel)
            Me.Controls.Remove(Me.typeLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.codeField)
            Me.Controls.Add(Me.descriptionField)
            Me.Controls.Add(Me.typeField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.codeField.DefaultText = Me.codeLabel.Text
            Me.descriptionField.DefaultText = Me.descriptionLabel.Text

        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.codeField)
            Me.Controls.Remove(Me.descriptionField)
            Me.Controls.Remove(Me.typeField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.codeLabel)
            Me.Controls.Add(Me.descriptionLabel)
            Me.Controls.Add(Me.typeLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles codeField.KeyDown, descriptionField.KeyDown, typeField.KeyDown

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

        Private Sub setTypeItemsBackColor(sender As Object, e As DrawItemEventArgs)

            If (e.Index < 0) Then
                Exit Sub
            End If

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Dim backColor As Color
            Dim itemsText As String
            Dim itemsRectangle As Rectangle = New Rectangle(1, e.Bounds.Top + 1, e.Bounds.Width - 1, e.Bounds.Height - 1)

            backColor = DirectCast(Me.typeField.Items(e.Index), DelayType).Color
            itemsText = DirectCast(Me.typeField.Items(e.Index), DelayType).Name

            e.Graphics.DrawRectangle(New Pen(backColor), itemsRectangle)
            e.Graphics.FillRectangle(New SolidBrush(backColor), itemsRectangle)

            If ((e.State And DrawItemState.Selected) = DrawItemState.Selected) OrElse ((e.State And DrawItemState.ComboBoxEdit) = DrawItemState.ComboBoxEdit) Then
                e.Graphics.DrawString(itemsText, Constants.UI.Fonts.DEFAULT_FONT_BOLD, Brushes.Black, 10, ((e.Bounds.Height - Constants.UI.Fonts.DEFAULT_FONT_BOLD.Height) / 2) + e.Bounds.Top)
            Else
                e.Graphics.DrawString(itemsText, Me.Font, Brushes.Black, 5, ((e.Bounds.Height - Me.Font.Height) / 2) + e.Bounds.Top)
            End If
        End Sub
    End Class
End Namespace
