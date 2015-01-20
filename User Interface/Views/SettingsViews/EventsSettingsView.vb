Imports IGNIS.UI.Common

Namespace UI

    Public Class EventsSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Événements"

        Public Shared ReadOnly ADD_NEW_DELAY_TYPE As String = "+ Nouveau type"

        ' Components
        ' !LAYOUT!
        Private WithEvents eventsEnabledCheckBox As CheckBox

        Private WithEvents newEventNameField As TextField
        Private WithEvents newEventReplaceField As TextField
        Private WithEvents newEventStartCheckBox As CheckBox
        Private WithEvents newEventStopCheckBox As CheckBox
        Private WithEvents addNewEventButton As Button

        Private WithEvents eventsListView As EventsListView

        Private delaysJustificationTimeLabel As Label
        Private WithEvents delaysJustificationTimeField As TextField
        Private delaysJustificationTimeBuffer As String

        Private WithEvents newDelayCodeField As TextField
        Private WithEvents newDelayDescriptionField As TextField
        Private newDelayTypeField As ComboBox
        Private WithEvents addNewDelayButton As Button

        Private delaysListView As DelaysListView

        ' #todo Add icon
        Private WithEvents cantSeeDelayCodesManagementControlsLabel As Label

        Private WithEvents adminPasswordPanel As Common.AdminPasswordPanel
        ' !LAYOUT!

        ' Attributes
        Private _eventsSettings As EventsSettingsController

        Private _delayCodesManagementControlsAreShowing As Boolean = False

        Public Sub New()
            MyBase.New()

            Me.layout = New EventsSettingsViewLayout

            Me._eventsSettings = ProgramController.SettingsControllers.EventsSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.eventsEnabledCheckBox = New CheckBox
            Me.eventsEnabledCheckBox.CheckAlign = ContentAlignment.MiddleLeft
            Me.eventsEnabledCheckBox.TextAlign = ContentAlignment.MiddleLeft
            Me.eventsEnabledCheckBox.Text = "Fichier d'évèmements séparé"
            Me.eventsEnabledCheckBox.Cursor = Cursors.Hand

            Me.newEventNameField = New TextField
            Me.newEventNameField.PlaceHolder = "Nom"
            Me.newEventNameField.CanBeEmpty = False

            Me.newEventReplaceField = New TextField
            Me.newEventReplaceField.PlaceHolder = "Remplacé par"
            Me.newEventReplaceField.ValidationType = TextField.ValidationTypes.Text
            Me.newEventReplaceField.CanBeEmpty = False

            Me.newEventStartCheckBox = New CheckBox
            Me.newEventStartCheckBox.Text = "Départ"
            Me.newEventStartCheckBox.TextAlign = ContentAlignment.TopCenter
            Me.newEventStartCheckBox.CheckAlign = ContentAlignment.BottomCenter
            Me.newEventStartCheckBox.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.newEventStartCheckBox.Cursor = Cursors.Hand

            Me.newEventStopCheckBox = New CheckBox
            Me.newEventStopCheckBox.Text = "Arrêt"
            Me.newEventStopCheckBox.TextAlign = ContentAlignment.TopCenter
            Me.newEventStopCheckBox.CheckAlign = ContentAlignment.BottomCenter
            Me.newEventStopCheckBox.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.newEventStopCheckBox.Cursor = Cursors.Hand

            Me.addNewEventButton = New Button
            Me.addNewEventButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewEventButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewEventButton.Enabled = False

            Me.eventsListView = New EventsListView
            AddHandler Me.eventsListView.DeleteEventInfo, AddressOf Me._eventsSettings.removeEventInfo
            AddHandler Me.eventsListView.DeleteEventInfo, AddressOf Me.raiseSettingChangedEvent


            Me.delaysJustificationTimeLabel = New Label
            Me.delaysJustificationTimeLabel.Text = "Durée minimum d'un délai justifiable"
            AddHandler Me.delaysJustificationTimeLabel.Click, AddressOf Me.Focus

            Me.delaysJustificationTimeField = New TextField
            Me.delaysJustificationTimeField.TextAlign = HorizontalAlignment.Center
            Me.delaysJustificationTimeField.MaxLength = 4
            Me.delaysJustificationTimeField.ValidationType = TextField.ValidationTypes.Numbers
            Me.delaysJustificationTimeField.CanBeEmpty = False


            Me.newDelayCodeField = New TextField
            Me.newDelayCodeField.PlaceHolder = "Code"
            Me.newDelayCodeField.ValidationType = TextField.ValidationTypes.Text
            Me.newDelayCodeField.CanBeEmpty = False

            Me.newDelayDescriptionField = New TextField
            Me.newDelayDescriptionField.PlaceHolder = "Description"
            Me.newDelayDescriptionField.ValidationType = TextField.ValidationTypes.Text
            Me.newDelayDescriptionField.CanBeEmpty = False

            Me.newDelayTypeField = New ComboBox
            Me.newDelayTypeField.DropDownStyle = ComboBoxStyle.DropDownList
            Me.newDelayTypeField.DrawMode = DrawMode.OwnerDrawFixed
            AddHandler Me.newDelayTypeField.DrawItem, AddressOf Me.setTypeItemsBackColor

            Me.addNewDelayButton = New Button
            Me.addNewDelayButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewDelayButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewDelayButton.Enabled = False


            Me.delaysListView = New DelaysListView(Me._eventsSettings)
            AddHandler Me.delaysListView.DeleteDelayCode, AddressOf Me._eventsSettings.removeDelayCode
            AddHandler Me.delaysListView.DeleteDelayCode, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.delaysListView.UpdateDelayCode, AddressOf Me._eventsSettings.updateDelayCode
            AddHandler Me.delaysListView.UpdateDelayCode, AddressOf Me.raiseSettingChangedEvent

            Me.cantSeeDelayCodesManagementControlsLabel = New Label
            Me.cantSeeDelayCodesManagementControlsLabel.Font = New Font(Constants.UI.Fonts.SMALL_DEFAULT_FONT, FontStyle.Underline)
            Me.cantSeeDelayCodesManagementControlsLabel.ForeColor = Color.Blue
            Me.cantSeeDelayCodesManagementControlsLabel.Text = "*Cliquez ici pour avoir accès au gestionnaire des codes de délais."
            Me.cantSeeDelayCodesManagementControlsLabel.Cursor = Cursors.Hand

            Me.adminPasswordPanel = New AdminPasswordPanel
            Me.adminPasswordPanel.IsDraggable = False

            Me.Controls.Add(Me.eventsEnabledCheckBox)
            Me.Controls.Add(Me.newEventNameField)
            Me.Controls.Add(Me.newEventReplaceField)
            Me.Controls.Add(Me.newEventStartCheckBox)
            Me.Controls.Add(Me.newEventStopCheckBox)
            Me.Controls.Add(Me.addNewEventButton)
            Me.Controls.Add(Me.eventsListView)
            Me.Controls.Add(Me.delaysJustificationTimeLabel)
            Me.Controls.Add(Me.delaysJustificationTimeField)
            Me.Controls.Add(Me.cantSeeDelayCodesManagementControlsLabel)

            AddHandler Me.newEventNameField.ValidationOccured, AddressOf Me.enableAddNewEventButton
            AddHandler Me.newEventReplaceField.ValidationOccured, AddressOf Me.enableAddNewEventButton

            AddHandler Me.newDelayCodeField.ValidationOccured, AddressOf Me.enableAddNewDelayButton
            AddHandler Me.newDelayDescriptionField.ValidationOccured, AddressOf Me.enableAddNewDelayButton

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, EventsSettingsViewLayout)

            Me.adminPasswordPanel.ajustLayout(Common.AdminPasswordPanel.SIZE_WITH_PARAMETERS_BUTTON)

            Me.eventsEnabledCheckBox.Location = layout.EventsEnabledCheckBox_Location
            Me.eventsEnabledCheckBox.Size = layout.EventsEnabledCheckBox_Size

            Me.newEventNameField.Location = layout.NewEventNameField_Location
            Me.newEventNameField.Size = layout.NewEventNameField_Size

            Me.newEventReplaceField.Location = layout.NewEventReplaceField_Location
            Me.newEventReplaceField.Size = layout.NewEventReplaceField_Size

            Me.newEventStartCheckBox.Location = layout.NewEventStartCheckBox_Location
            Me.newEventStartCheckBox.Size = layout.NewEventStartCheckBox_Size

            Me.newEventStopCheckBox.Location = layout.NewEventStopCheckBox_Location
            Me.newEventStopCheckBox.Size = layout.NewEventStopCheckBox_Size

            Me.addNewEventButton.Location = layout.AddNewEventButton_Location
            Me.addNewEventButton.Size = layout.AddNewEventButton_Size


            Me.eventsListView.Location = layout.EventsListView_Location
            Me.eventsListView.ajustLayout(layout.EventsListView_Size)


            Me.delaysJustificationTimeLabel.Location = layout.DelaysJustificationTimeLabel_Location
            Me.delaysJustificationTimeLabel.Size = layout.DelaysJustificationTimeLabel_Size

            Me.delaysJustificationTimeField.Location = layout.DelaysJustificationTimeField_Location
            Me.delaysJustificationTimeField.Size = layout.DelaysJustificationTimeField_Size


            Me.newDelayCodeField.Location = layout.NewDelayCodeField_Location
            Me.newDelayCodeField.Size = layout.NewDelayCodeField_Size

            Me.newDelayDescriptionField.Location = layout.NewDelayDescriptionField_Location
            Me.newDelayDescriptionField.Size = layout.NewDelayDescriptionField_Size

            Me.newDelayTypeField.Location = layout.NewDelayTypeField_Location
            Me.newDelayTypeField.Size = layout.NewDelayTypeField_Size

            Me.addNewDelayButton.Location = layout.AddNewDelayButton_Location
            Me.addNewDelayButton.Size = layout.AddNewDelayButton_Size

            Me.delaysListView.Location = layout.DelaysListView_Location
            Me.delaysListView.ajustLayout(layout.DelaysListView_Size)

            Me.cantSeeDelayCodesManagementControlsLabel.Location = layout.CantSeeDelayCodesManagementControlsLabel_Location
            Me.cantSeeDelayCodesManagementControlsLabel.Size = layout.CantSeeDelayCodesManagementControlsLabel_Size

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim layout = DirectCast(Me.layout, EventsSettingsViewLayout)

            Me.eventsListView.ajustLayoutFinal(layout.EventsListView_Size)
            Me.delaysListView.ajustLayoutFinal(layout.DelaysListView_Size)

            Me.adminPasswordPanel.Location = New Point(Me.Width / 2 - adminPasswordPanel.Width / 2, Me.Height / 2 - adminPasswordPanel.Height / 2)

        End Sub

        Public Overrides Sub updateFields()
            Me.updatingFields = True

            Me.eventsEnabledCheckBox.Checked = Me._eventsSettings.EventsEnabled
            Me.enableEventFields()

            Me.eventsListView.clear()

            If (Me.eventsEnabledCheckBox.Checked) Then

                For Each _eventInfo As XmlSettings.EventsNode.EventInfo In Me._eventsSettings.Events
                    Me.eventsListView.addObject(_eventInfo)
                Next

                Me.eventsListView.refreshList()
            End If

            Me.delaysJustificationTimeField.DefaultText = Me._eventsSettings.JustifiableDelaysDuration.TotalSeconds.ToString("N0")
            Me.delaysJustificationTimeBuffer = Me.delaysJustificationTimeField.Text

            Me.newDelayTypeField.Items.Clear()
            Me.delaysListView.clear()

            For Each _delayType As DelayType In Me._eventsSettings.DelayTypes

                Me.newDelayTypeField.Items.Add(_delayType)

                For Each _code As DelayCode In _delayType.Codes

                    Me.delaysListView.addObject(_code)
                Next
            Next
            Me.delaysListView.refreshList()


            Me.newDelayTypeField.SelectedIndex = 0
            'Me.newDelayTypeField.Items.Add(ADD_NEW_DELAY_TYPE)

            Me.updatingFields = False
        End Sub

        Protected Overloads Overrides Sub beforeShow()

            If (ProgramController.SettingsControllers.AdminSettingsController.UserIsAdmin OrElse _
                ProgramController.SettingsControllers.AdminSettingsController.UserCanModifyDelayCodes) Then

                Me.showDelayCodeManagementControls()
            Else
                Me.hideDelayCodeManagementControls()
            End If

        End Sub

        Public Overrides Sub afterShow()

            Me.hideAdminPasswordPanel()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub showDelayCodeManagementControls()

            If (Not Me._delayCodesManagementControlsAreShowing) Then

                Me.Controls.Remove(Me.cantSeeDelayCodesManagementControlsLabel)

                Me.Controls.Add(Me.newDelayCodeField)
                Me.Controls.Add(Me.newDelayDescriptionField)
                Me.Controls.Add(Me.newDelayTypeField)
                Me.Controls.Add(Me.addNewDelayButton)
                Me.Controls.Add(Me.delaysListView)

                Me._delayCodesManagementControlsAreShowing = True
            End If
        End Sub

        Private Sub hideDelayCodeManagementControls()

            If (Me._delayCodesManagementControlsAreShowing) Then

                Me.Controls.Remove(Me.newDelayCodeField)
                Me.Controls.Remove(Me.newDelayDescriptionField)
                Me.Controls.Remove(Me.newDelayTypeField)
                Me.Controls.Remove(Me.addNewDelayButton)
                Me.Controls.Remove(Me.delaysListView)

                Me.Controls.Add(Me.cantSeeDelayCodesManagementControlsLabel)

                Me._delayCodesManagementControlsAreShowing = False
            End If
        End Sub

        Private Sub showAdminPasswordPanel() Handles cantSeeDelayCodesManagementControlsLabel.Click

            Me.Controls.Add(Me.adminPasswordPanel)
            Me.adminPasswordPanel.BringToFront()
            Me.adminPasswordPanel.Focus()
        End Sub

        Private Sub hideAdminPasswordPanel()

            Me.Controls.Remove(Me.adminPasswordPanel)

        End Sub

        Private Sub onAdminPanelClose(status As Common.PopUpMessage.ClosingStatus) Handles adminPasswordPanel.CloseEvent

            Me.hideAdminPasswordPanel()
        End Sub

        Private Sub onAuthentication() Handles adminPasswordPanel.SuccessfulAuthentication
            Me.showDelayCodeManagementControls()
        End Sub

        Private Sub enableEventFields()

            Me.newEventNameField.Enabled = Me.eventsEnabledCheckBox.Checked
            Me.newEventReplaceField.Enabled = Me.eventsEnabledCheckBox.Checked
            Me.newEventStartCheckBox.Enabled = Me.eventsEnabledCheckBox.Checked
            Me.newEventStopCheckBox.Enabled = Me.eventsEnabledCheckBox.Checked
            Me.addNewEventButton.Enabled = Me.eventsEnabledCheckBox.Checked
            Me.eventsListView.Enabled = Me.eventsEnabledCheckBox.Checked

            Me.enableAddNewEventButton()
        End Sub

        Private Sub onEventsEnabled() Handles eventsEnabledCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me._eventsSettings.EventsEnabled = eventsEnabledCheckBox.Checked
                Me.raiseSettingChangedEvent()
            End If

        End Sub

        Private Sub addEventInfo() Handles addNewEventButton.Click

            Dim isDuplicate As Boolean = False

            For Each _item As XmlSettings.EventsNode.EventInfo In Me.eventsListView.DisplayedObjectList
                If (_item.MESSAGE.Equals(newEventNameField.Text)) Then
                    isDuplicate = True

                    ' @todo - select item
                    Exit For
                End If
            Next

            If (isDuplicate) Then

                Beep()
            Else

                Me._eventsSettings.addNewEventInfo(newEventNameField.Text, newEventReplaceField.Text, Me.newEventStartCheckBox.Checked, Me.newEventStopCheckBox.Checked)
                Me.raiseSettingChangedEvent()

                newEventNameField.Text = ""
                newEventReplaceField.Text = ""

                Me.eventsListView.selectLastItem()
            End If

        End Sub

        Private Sub enableAddNewEventButton()

            If (Me.newEventNameField.IsValid AndAlso _
                Me.newEventReplaceField.IsValid) Then

                Me.addNewEventButton.Enabled = True

            Else
                Me.addNewEventButton.Enabled = False
            End If
        End Sub

        Private Sub updateEventInfo(eventInfoToUpdate As XmlSettings.EventsNode.EventInfo, newName As String, newReplace As String, isStart As Boolean, isStop As Boolean) Handles eventsListView.UpdateEventInfo
            If (isStart) Then
                Me._eventsSettings.updateEventInfo(eventInfoToUpdate, newName, newReplace, Constants.Input.Events.EventType.START)
            ElseIf (isStop) Then
                Me._eventsSettings.updateEventInfo(eventInfoToUpdate, newName, newReplace, Constants.Input.Events.EventType.STOP_)
            Else
                Me._eventsSettings.updateEventInfo(eventInfoToUpdate, newName, newReplace, Constants.Input.Events.EventType.IMPORTANT)
            End If

            Me.raiseSettingChangedEvent()
        End Sub

        Private Sub setJustificationDuration() Handles delaysJustificationTimeField.LostFocus

            If (Not Me.delaysJustificationTimeField.Text.Equals(Me.delaysJustificationTimeBuffer) AndAlso _
                Me.delaysJustificationTimeField.IsValid) Then

                Me._eventsSettings.JustifiableDelaysDuration = TimeSpan.FromSeconds(CInt(delaysJustificationTimeField.Text))
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub addDelayCode() Handles addNewDelayButton.Click

            If (newDelayCodeField.Text = "" OrElse _
                newDelayDescriptionField.Text = "" OrElse _
                IsNothing(newDelayTypeField.SelectedItem) OrElse _
                newDelayCodeField.ForeColor = Color.Gray OrElse _
                newDelayDescriptionField.ForeColor = Color.Gray) Then

                Beep()
            Else

                Me._eventsSettings.addDelayCode(Me.newDelayCodeField.Text, Me.newDelayDescriptionField.Text, Me.newDelayTypeField.SelectedItem)
                Me.raiseSettingChangedEvent()

                newDelayCodeField.Text = ""
                newDelayDescriptionField.Text = ""

                Me.delaysListView.selectFirstItem()
            End If

        End Sub

        Private Sub enableAddNewDelayButton()

            If (Me.newDelayCodeField.IsValid AndAlso _
                Me.newDelayDescriptionField.IsValid) Then

                Me.addNewDelayButton.Enabled = True
            Else
                Me.addNewDelayButton.Enabled = False
            End If
        End Sub

        Private Sub toggleStartStop(sender As Object, e As EventArgs) Handles newEventStartCheckBox.CheckedChanged, newEventStopCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me.updatingFields = True

                If (sender.Equals(Me.newEventStartCheckBox) AndAlso Me.newEventStartCheckBox.Checked) Then
                    Me.newEventStopCheckBox.Checked = False
                ElseIf (sender.Equals(Me.newEventStopCheckBox) AndAlso Me.newEventStopCheckBox.Checked) Then
                    Me.newEventStartCheckBox.Checked = False
                End If

                Me.updatingFields = False
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

            If (Me.newDelayTypeField.Items(e.Index).Equals(ADD_NEW_DELAY_TYPE)) Then

                backColor = Color.White
                itemsText = ADD_NEW_DELAY_TYPE
            Else

                backColor = DirectCast(Me.newDelayTypeField.Items(e.Index), DelayType).Color
                itemsText = DirectCast(Me.newDelayTypeField.Items(e.Index), DelayType).Name
            End If

            e.Graphics.DrawRectangle(New Pen(backColor), itemsRectangle)
            e.Graphics.FillRectangle(New SolidBrush(backColor), itemsRectangle)

            If ((e.State And DrawItemState.Selected) = DrawItemState.Selected) OrElse ((e.State And DrawItemState.ComboBoxEdit) = DrawItemState.ComboBoxEdit) Then
                e.Graphics.DrawString(itemsText, Constants.UI.Fonts.DEFAULT_FONT_BOLD, Brushes.Black, 10, ((e.Bounds.Height - Constants.UI.Fonts.DEFAULT_FONT_BOLD.Height) / 2) + e.Bounds.Top)
            Else
                e.Graphics.DrawString(itemsText, Me.Font, Brushes.Black, 5, ((e.Bounds.Height - Me.Font.Height) / 2) + e.Bounds.Top)
            End If
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._eventsSettings
            End Get
        End Property
    End Class
End Namespace

