Imports IGNIS.UI.Common

Namespace UI

    Public Class AsphaltTankListItem
        Inherits Common.ListItem(Of XmlSettings.AsphaltNode.TankInfo)

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25) ' #Refactor - extract

        ' Components
        Private WithEvents tankNameLabel As Label
        Private WithEvents asphaltNameLabel As Label
        Private WithEvents mixTargetTemperatureLabel As Label

        Private WithEvents tankNameField As TextField
        Private WithEvents asphaltNameField As TextField
        Private WithEvents mixTargetTemperatureField As TextField

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        ' Attributes

        ' Events
        Public Event deleteTankInfo(asphaltTank As XmlSettings.AsphaltNode.TankInfo)
        Public Event updateTankInfo(asphaltTank As XmlSettings.AsphaltNode.TankInfo, tankName As String, asphaltName As String, mixTargetTemperature As Double)


        Public Sub New(asphaltTank As XmlSettings.AsphaltNode.TankInfo)
            MyBase.New(asphaltTank)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.tankNameLabel = New Label
            Me.tankNameLabel.Text = Me.ItemObject.TANK_NAME
            Me.tankNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.asphaltNameLabel = New Label
            Me.asphaltNameLabel.Text = Me.ItemObject.ASPHALT_NAME
            Me.asphaltNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.mixTargetTemperatureLabel = New Label
            Me.mixTargetTemperatureLabel.Text = Me.ItemObject.SET_POINT_TEMP & Constants.Units.StringRepresentation.CELSIUS
            Me.mixTargetTemperatureLabel.TextAlign = ContentAlignment.MiddleCenter

            Me.tankNameField = New TextField
            Me.tankNameField.AutoSize = False
            Me.tankNameField.ValidationType = TextField.ValidationTypes.Text
            Me.tankNameField.CanBeEmpty = False

            Me.asphaltNameField = New TextField
            Me.asphaltNameField.AutoSize = False
            Me.asphaltNameField.ValidationType = TextField.ValidationTypes.Text
            Me.asphaltNameField.CanBeEmpty = False

            Me.mixTargetTemperatureField = New TextField
            Me.mixTargetTemperatureField.AutoSize = False
            Me.mixTargetTemperatureField.TextAlign = HorizontalAlignment.Center
            Me.mixTargetTemperatureField.ValidationType = TextField.ValidationTypes.Numbers
            Me.mixTargetTemperatureField.AcceptsTemperature = True
            Me.mixTargetTemperatureField.CanBeEmpty = False

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

            Me.Controls.Add(Me.tankNameLabel)
            Me.Controls.Add(Me.asphaltNameLabel)
            Me.Controls.Add(Me.mixTargetTemperatureLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            AddHandler Me.asphaltNameField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.tankNameField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.mixTargetTemperatureField.ValidationOccured, AddressOf Me.enableConfirmEditButton


        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.tankNameLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            Me.tankNameLabel.Size = New Size((newSize.Width - 2 * BUTTONS_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) * 3 / 8, newSize.Height)

            Me.tankNameField.Location = Me.tankNameLabel.Location
            Me.tankNameField.Size = Me.tankNameLabel.Size

            Me.asphaltNameLabel.Location = New Point(Me.tankNameField.Location.X + Me.tankNameField.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.asphaltNameLabel.Size = Me.tankNameField.Size

            Me.asphaltNameField.Location = Me.asphaltNameLabel.Location
            Me.asphaltNameField.Size = Me.asphaltNameLabel.Size

            Me.mixTargetTemperatureLabel.Location = New Point(Me.asphaltNameLabel.Location.X + Me.asphaltNameLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.mixTargetTemperatureLabel.Size = New Size((newSize.Width - 2 * BUTTONS_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) * 2 / 8, newSize.Height)

            Me.mixTargetTemperatureField.Location = Me.mixTargetTemperatureLabel.Location
            Me.mixTargetTemperatureField.Size = Me.mixTargetTemperatureLabel.Size

            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, tankNameLabel.Click, asphaltNameLabel.Click, mixTargetTemperatureLabel.Click

            raiseClickEvent()

        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent deleteTankInfo(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent updateTankInfo(Me.ItemObject, Me.tankNameField.Text, Me.asphaltNameField.Text, CDbl(Me.mixTargetTemperatureField.Text.Replace(Constants.Units.StringRepresentation.CELSIUS, "")))

        End Sub

        Private Sub cancelEditing() Handles cancelEditButton.Click

            enterReadMode()

        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.asphaltNameField.IsValid AndAlso _
                Me.tankNameField.IsValid AndAlso _
                Me.mixTargetTemperatureField.IsValid) Then

                Me.confirmEditButton.Enabled = True
            Else
                Me.confirmEditButton.Enabled = False
            End If
        End Sub

        Private Sub enterWriteMode() Handles editButton.Click

            Me._currentMode = Mode.WRITE

            Me.Controls.Remove(Me.tankNameLabel)
            Me.Controls.Remove(Me.asphaltNameLabel)
            Me.Controls.Remove(Me.mixTargetTemperatureLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.tankNameField)
            Me.Controls.Add(Me.asphaltNameField)
            Me.Controls.Add(Me.mixTargetTemperatureField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.tankNameField.DefaultText = Me.tankNameLabel.Text
            Me.asphaltNameField.DefaultText = Me.asphaltNameLabel.Text
            Me.mixTargetTemperatureField.DefaultText = Me.mixTargetTemperatureLabel.Text

        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.tankNameField)
            Me.Controls.Remove(Me.asphaltNameField)
            Me.Controls.Remove(Me.mixTargetTemperatureField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.tankNameLabel)
            Me.Controls.Add(Me.asphaltNameLabel)
            Me.Controls.Add(Me.mixTargetTemperatureLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles tankNameField.KeyDown, asphaltNameField.KeyDown, mixTargetTemperatureField.KeyDown

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

