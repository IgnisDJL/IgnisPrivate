Namespace UI

    Public Class DelayJustificationStepView
        Inherits GenerationStepView

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "Justification des délais"

        Private Shared ReadOnly ALL_DELAYS As String = "Tous"
        Private Shared ReadOnly UNKNOWN_DELAY As String = "Inconnu"

        Private Shared ReadOnly DARK_GREY As Color = Color.FromArgb(50, 50, 50)

        Private Shared ReadOnly SPLITTER_PANEL_SIZE As Size = New Size(320, 280)

        Private Shared ReadOnly SKIP_WARNING_MESSAGE_SIZE As Size = New Size(400, 145)

        ' Components
        Private dateLabel As Label

        Private startTextLabel As Label
        Private startTimeLabel As Label

        Private endTextLabel As Label
        Private endTimeLabel As Label

        Private delayNumberTextLabel As Label
        Private delayNumberValueLabel As Label

        Private durationTextLabel As Label
        Private durationValueLabel As Label

        Private delayTypeLabel As Label
        Private delayTypeCombobox As ComboBox

        Private delayCodeLabel As Label
        Private WithEvents delayCodeCombobox As ComboBox

        Private delayJustificationLabel As Label
        Private WithEvents delayJustificationTextbox As TextBox

        ' --- Buttons
        Private WithEvents nextButton As Common.NextButton
        Private WithEvents skipButton As Button
        Private WithEvents splitDelayButton As Button

        Private spliterPanel As SplitDelayPanel
        Private WithEvents undoLabel As Label

        Private WithEvents skipWarningMessagePanel As Common.UserMessagePanel

        ' Attributes
        Private currentDelay As Delay_1


        Public Sub New()
            MyBase.New()

            Me.layout = New DelaysJustificationStepLayout

            Me.initializeComponents()

            ' Add event handlers
            AddHandler Me.delayTypeCombobox.DrawItem, AddressOf Me.setTypeItemsBackColor
            AddHandler Me.delayTypeCombobox.SelectedValueChanged, AddressOf Me.updateDelayCode
            AddHandler Me.delayCodeCombobox.DrawItem, AddressOf Me.setCodeItemsBackColor
            AddHandler Me.delayCodeCombobox.DropDownClosed, AddressOf Me.delayJustificationTextbox.Focus
            AddHandler Me.delayCodeCombobox.SelectedValueChanged, AddressOf Me.onSelectedCode

            AddHandler Me.spliterPanel.CloseEvent, AddressOf Me.finalizeDelaySplitting

            ' Get delay types
            Me.delayTypeCombobox.Items.Add(ALL_DELAYS)

            For Each type As DelayType In ProgramController.SettingsControllers.EventsSettingsController.DelayTypes

                Me.delayTypeCombobox.Items.Add(type)
            Next

            Me.delayTypeCombobox.Items.Add(UNKNOWN_DELAY)
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.dateLabel = New Label
            Me.dateLabel.AutoSize = False
            Me.dateLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.dateLabel.Font = Constants.UI.Fonts.BIGGER_DEFAULT_FONT_BOLD

            Me.startTextLabel = New Label
            Me.startTextLabel.AutoSize = False
            Me.startTextLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.startTextLabel.Text = "Début :"
            Me.startTextLabel.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.startTimeLabel = New Label
            Me.startTimeLabel.AutoSize = False
            Me.startTimeLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.startTimeLabel.Font = Constants.UI.Fonts.DEFAULT_FONT

            Me.endTextLabel = New Label
            Me.endTextLabel.AutoSize = False
            Me.endTextLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.endTextLabel.Text = "Fin :"
            Me.endTextLabel.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.endTimeLabel = New Label
            Me.endTimeLabel.AutoSize = False
            Me.endTimeLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.endTimeLabel.Font = Constants.UI.Fonts.DEFAULT_FONT

            Me.delayNumberTextLabel = New Label
            Me.delayNumberTextLabel.AutoSize = False
            Me.delayNumberTextLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.delayNumberTextLabel.Text = "Progression"
            Me.delayNumberTextLabel.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.delayNumberValueLabel = New Label
            Me.delayNumberValueLabel.AutoSize = False
            Me.delayNumberValueLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.delayNumberValueLabel.Font = Constants.UI.Fonts.DEFAULT_FONT

            Me.durationTextLabel = New Label
            Me.durationTextLabel.AutoSize = False
            Me.durationTextLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.durationTextLabel.Text = "Durée : "
            Me.durationTextLabel.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.durationValueLabel = New Label
            Me.durationValueLabel.AutoSize = False
            Me.durationValueLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.durationValueLabel.Font = Constants.UI.Fonts.DEFAULT_FONT

            Me.delayTypeLabel = New Label
            Me.delayTypeLabel.AutoSize = False
            Me.delayTypeLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.delayTypeLabel.Text = "Type de délai : "

            Me.delayTypeCombobox = New ComboBox
            Me.delayTypeCombobox.DropDownStyle = ComboBoxStyle.DropDownList
            Me.delayTypeCombobox.DrawMode = DrawMode.OwnerDrawFixed

            Me.delayCodeLabel = New Label
            Me.delayCodeLabel.AutoSize = False
            Me.delayCodeLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.delayCodeLabel.Text = "Code du délai : "

            Me.delayCodeCombobox = New ComboBox
            Me.delayCodeCombobox.DropDownStyle = ComboBoxStyle.DropDownList
            Me.delayCodeCombobox.DrawMode = DrawMode.OwnerDrawFixed

            Me.delayJustificationLabel = New Label
            Me.delayJustificationLabel.AutoSize = False
            Me.delayJustificationLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.delayJustificationLabel.Text = "Commentaire : "

            Me.delayJustificationTextbox = New TextBox

            Me.nextButton = New Common.NextButton

            Me.skipButton = New Button
            Me.skipButton.TextAlign = ContentAlignment.MiddleCenter
            Me.skipButton.Text = "Étape suivante"
            Me.skipButton.Font = Constants.UI.Fonts.SMALLER_DEFAULT_FONT

            Me.splitDelayButton = New Button
            Me.splitDelayButton.TextAlign = ContentAlignment.MiddleCenter
            Me.splitDelayButton.Text = "  Diviser"
            Me.splitDelayButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.splitDelayButton.Image = Constants.UI.Images._32x32.SPLIT

            Me.undoLabel = New Label
            Me.undoLabel.Font = New Font(Constants.UI.Fonts.SMALL_DEFAULT_FONT, FontStyle.Underline)
            Me.undoLabel.ForeColor = Color.Blue
            Me.undoLabel.Text = "Annuler division (Ctrl + Z)"
            Me.undoLabel.Size = DelaysJustificationStepLayout.UNDO_LABEL_SIZE
            Me.undoLabel.Cursor = Cursors.Hand

            Me.OtherButtons.Add(Me.nextButton)
            Me.OtherButtons.Add(Me.skipButton)
            Me.OtherButtons.Add(Me.splitDelayButton)

            Me.Controls.Add(dateLabel)
            Me.Controls.Add(startTextLabel)
            Me.Controls.Add(startTimeLabel)
            Me.Controls.Add(endTextLabel)
            Me.Controls.Add(endTimeLabel)
            Me.Controls.Add(delayNumberTextLabel)
            Me.Controls.Add(delayNumberValueLabel)
            Me.Controls.Add(durationTextLabel)
            Me.Controls.Add(durationValueLabel)
            Me.Controls.Add(delayTypeLabel)
            Me.Controls.Add(delayTypeCombobox)
            Me.Controls.Add(delayCodeLabel)
            Me.Controls.Add(delayCodeCombobox)
            Me.Controls.Add(delayJustificationLabel)
            Me.Controls.Add(delayJustificationTextbox)

            Me.spliterPanel = New SplitDelayPanel

            Me.delayTypeCombobox.TabStop = False
            Me.delayCodeCombobox.TabStop = False
            Me.delayJustificationTextbox.TabIndex = 1
            Me.nextButton.TabIndex = 2
            Me.skipButton.TabIndex = 3
            Me.splitDelayButton.TabIndex = 4
            Me.cancelButton.TabIndex = 5
            Me.backButton.TabIndex = 6
        End Sub

        Public Sub onSelectedCode()

            Me.delayCodeCombobox.BackColor = DirectCast(Me.delayCodeCombobox.SelectedItem, DelayCode).Type.Color

            If (Not IsNothing(Me.currentDelay)) Then
                'Me.currentDelay.Code = Me.delayCodeCombobox.SelectedItem
            End If
        End Sub

        Public Sub onJustificationChanged() Handles delayJustificationTextbox.TextChanged
            'Me.currentDelay.Justification = delayJustificationTextbox.Text
        End Sub


        Private Sub updateDelayCode()

            Me.delayCodeCombobox.Items.Clear()

            If (Me.delayTypeCombobox.SelectedItem.Equals(ALL_DELAYS)) Then

                Me.delayCodeCombobox.Enabled = True

                For Each delayType As DelayType In ProgramController.SettingsControllers.EventsSettingsController.DelayTypes
                    Me.delayCodeCombobox.Items.AddRange(delayType.Codes.ToArray)
                Next

            ElseIf (Me.delayTypeCombobox.SelectedItem.Equals(UNKNOWN_DELAY)) Then

                Me.delayCodeCombobox.BackColor = Color.White

                Me.delayCodeCombobox.Enabled = False

                Me.delayJustificationTextbox.Focus()

                'Me.currentDelay.Code = Nothing
                'Me.currentDelay.IsUnknown = True

                Exit Sub
            Else

                Me.delayCodeCombobox.Enabled = True

                Dim delayType = DirectCast(Me.delayTypeCombobox.SelectedItem, DelayType)

                Me.delayCodeCombobox.Items.AddRange(delayType.Codes.ToArray)

            End If

            'If (IsNothing(Me.currentDelay)) OrElse _
            '    'IsNothing(Me.currentDelay.Code) OrElse _
            '    Not Me.delayCodeCombobox.Items.Contains(Me.currentDelay.Code) Then

            Me.delayCodeCombobox.SelectedIndex = 0

            'Else
            'Me.delayCodeCombobox.SelectedItem = Me.currentDelay.Code
            'End If

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout As DelaysJustificationStepLayout = DirectCast(Me.layout, DelaysJustificationStepLayout)

            Me.dateLabel.Location = layout.DateLabel_Location
            Me.dateLabel.Size = layout.DateLabel_Size

            Me.startTextLabel.Location = layout.StartTextLabel_Location
            Me.startTextLabel.Size = layout.StartTextLabel_Size
            Me.startTimeLabel.Location = layout.StartTimeLabel_Location
            Me.startTimeLabel.Size = layout.StartTimeLabel_Size

            Me.endTextLabel.Location = layout.EndTextLabel_Location
            Me.endTextLabel.Size = layout.EndTextLabel_Size
            Me.endTimeLabel.Location = layout.EndTimeLabel_Location
            Me.endTimeLabel.Size = layout.EndTimeLabel_Size

            Me.delayNumberTextLabel.Location = layout.DelayNumberTextLabel_Location
            Me.delayNumberTextLabel.Size = layout.DelayNumberTextLabel_Size
            Me.delayNumberValueLabel.Location = layout.DelayNumberValueLabel_Location
            Me.delayNumberValueLabel.Size = layout.DelayNumberValueLabel_Size

            Me.durationTextLabel.Location = layout.DurationTextLabel_Location
            Me.durationTextLabel.Size = layout.DurationTextLabel_Size
            Me.durationValueLabel.Location = layout.DurationValueLabel_Location
            Me.durationValueLabel.Size = layout.DurationValueLabel_Size

            Me.delayTypeLabel.Location = layout.DelayTypeLabel_Location
            Me.delayTypeLabel.Size = layout.DelayTypeLabel_Size
            Me.delayTypeCombobox.Location = layout.DelayTypeCombobox_Location
            Me.delayTypeCombobox.Size = layout.DelayTypeCombobox_Size

            Me.delayCodeLabel.Location = layout.DelayCodeLabel_Location
            Me.delayCodeLabel.Size = layout.DelayCodeLabel_Size
            Me.delayCodeCombobox.Location = layout.DelayCodeCombobox_Location
            Me.delayCodeCombobox.Size = layout.DelayCodeCombobox_Size

            Me.delayJustificationLabel.Location = layout.DelayJustificationLabel_Location
            Me.delayJustificationLabel.Size = layout.DelayJustificationLabel_Size
            Me.delayJustificationTextbox.Location = layout.DelayJustificationTextbox_Location
            Me.delayJustificationTextbox.Size = layout.DelayJustificationTextbox_Size

            Me.nextButton.Location = layout.NextButton_Location
            Me.nextButton.Size = layout.NextButton_Size

            Me.skipButton.Location = layout.SkipButton_Location
            Me.skipButton.Size = layout.SkipButton_Size

            Me.splitDelayButton.Location = layout.SplitButton_Location
            Me.splitDelayButton.Size = layout.SplitButton_Size

            Me.undoLabel.Location = layout.UndoLabel_Location

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

            If (Not IsNothing(Me.skipWarningMessagePanel)) Then
                Me.skipWarningMessagePanel.Location = New Point(Me.Width / 2 - SKIP_WARNING_MESSAGE_SIZE.Width / 2, Me.Height / 2 - SKIP_WARNING_MESSAGE_SIZE.Height / 2)
            End If

        End Sub

        Public Sub showDelay(delay As Delay_1, currentDelayNumber As Integer, totalNumberOfDelays As Integer)

            Me.currentDelay = delay

            raiseProgressEvent((currentDelayNumber - 1) / totalNumberOfDelays * 100)

            updateLabels(currentDelayNumber, totalNumberOfDelays)

            Me.delayJustificationTextbox.Focus()

        End Sub

        Public Sub updateLabels(currentDelayNumber As Integer, totalNumberOfDelays As Integer)

            Me.dateLabel.Text = StrConv(Me.currentDelay.getStartDelay.ToString("dddd d MMMM"), VbStrConv.ProperCase)

            Me.startTimeLabel.Text = Me.currentDelay.getStartDelay.ToString("HH:mm")
            Me.endTimeLabel.Text = Me.currentDelay.getEndDelay.ToString("HH:mm")
            Me.durationValueLabel.Text = Me.currentDelay.getDuration.ToString("h\hmm")
            Me.delayNumberValueLabel.Text = currentDelayNumber & " / " & totalNumberOfDelays

            'If (Not IsNothing(Me.currentDelay.Type) AndAlso _
            '    Me.delayTypeCombobox.Items.Contains(Me.currentDelay.Type)) Then

            '    Me.delayTypeCombobox.SelectedItem = ALL_DELAYS

            '    If (Me.delayCodeCombobox.Items.Contains(Me.currentDelay.Code)) Then
            '        Me.delayCodeCombobox.SelectedItem = Me.currentDelay.Code
            '    End If

            'ElseIf (Me.currentDelay.IsUnknown) Then

            '    Me.delayTypeCombobox.SelectedItem = UNKNOWN_DELAY

            'Else

            Me.delayTypeCombobox.SelectedItem = ALL_DELAYS
            Me.delayCodeCombobox.SelectedIndex = 0

            'Me.currentDelay.Code = Me.delayCodeCombobox.SelectedItem

            'End If

            Me.delayJustificationTextbox.Text = Me.currentDelay.getDelayDescription.ToString

        End Sub

        Protected Overloads Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub afterShow()

            If (Not IsNothing(Me.skipWarningMessagePanel)) Then
                Me.Controls.Remove(Me.skipWarningMessagePanel)
            End If
        End Sub

        Public Overrides Sub onHide()

            Me.Controls.Remove(Me.spliterPanel)

            Me.currentDelay = Nothing

        End Sub

        Protected Overrides Sub cancel()

            Me.Controls.Remove(Me.spliterPanel)
            Me.Controls.Remove(Me.undoLabel)
            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.cancelGeneration()

        End Sub

        Private Sub goNext() Handles nextButton.Click

            Me.Controls.Remove(Me.spliterPanel)
            Me.Controls.Remove(Me.undoLabel)
            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.showNextDelay()

        End Sub

        Protected Overrides Sub goBack()

            Me.Controls.Remove(Me.spliterPanel)
            Me.Controls.Remove(Me.undoLabel)
            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.showPreviousDelay()

        End Sub

        Private Sub nextOnEnter(sender As Object, e As KeyEventArgs) Handles delayJustificationTextbox.KeyDown
            If (e.KeyCode = Keys.Enter AndAlso Me.nextButton.Enabled) Then
                goNext()
                e.SuppressKeyPress = True
            End If
        End Sub

        '
        ' Delay Splitting 
        '
        Private Sub toggleSplitDelayPanel() Handles splitDelayButton.Click

            If (Me.Controls.Contains(Me.spliterPanel)) Then

                Me.Controls.Remove(Me.spliterPanel)

            Else
                Me.Controls.Remove(Me.skipWarningMessagePanel)

                Me.spliterPanel.Location = New Point(Me.ClientSize.Width - SPLITTER_PANEL_SIZE.Width - 2, Me.ClientSize.Height - SPLITTER_PANEL_SIZE.Height - 2)
                Me.spliterPanel.ajustLayout(SPLITTER_PANEL_SIZE)

                Me.spliterPanel.beforeShow(Me.currentDelay.getStartDelay, Me.currentDelay.getEndDelay)
                Me.Controls.Add(Me.spliterPanel)

                Me.spliterPanel.BringToFront()

                Me.spliterPanel.Focus()
            End If

        End Sub

        Private Sub finalizeDelaySplitting(messageClosingStatus As SplitDelayPanel.ClosingStatus)

            Me.Controls.Remove(Me.spliterPanel)

            If (messageClosingStatus = Common.PopUpMessage.ClosingStatus.Ok) Then

                If (Me.spliterPanel.SplitTime.Subtract(Me.currentDelay.getStartDelay).TotalMinutes >= 1 AndAlso Me.currentDelay.getEndDelay.Subtract(Me.spliterPanel.SplitTime).TotalMinutes >= 1) Then

                    ProgramController.ReportGenerationController.splitDelay(Me.currentDelay, Me.spliterPanel.SplitTime)

                    Me.Controls.Add(undoLabel)
                    Me.Focus()
                End If
            End If
        End Sub

        Private Sub undoDelaySplitting() Handles undoLabel.Click
            ProgramController.ReportGenerationController.mergeDelays(Me.currentDelay)
            Me.Controls.Remove(undoLabel)
        End Sub

        Private Sub listenToCtrlZ(sender As Object, e As PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown

            If (Me.Controls.Contains(Me.undoLabel)) Then

                If (e.Control AndAlso e.KeyCode = Keys.Z) Then

                    Me.undoDelaySplitting()

                End If

            End If

        End Sub

        Private Sub focusOnClick() Handles Me.Click
            Me.Focus()
        End Sub

        '
        ' Skip Warning
        '
        Private Sub showSkipWarning() Handles skipButton.Click

            If (IsNothing(Me.skipWarningMessagePanel)) Then
                Me.initializeSkipWarningMessage()
            End If

            Me.ajustLayoutFinal()

            Me.Controls.Add(Me.skipWarningMessagePanel)
            Me.skipWarningMessagePanel.BringToFront()

            Me.skipWarningMessagePanel.Focus()
        End Sub

        Private Sub skipStep(closeStatus As Common.PopUpMessage.ClosingStatus) Handles skipWarningMessagePanel.CloseEvent

            If (closeStatus = Common.PopUpMessage.ClosingStatus.Ok) Then

                Me.Controls.Remove(undoLabel)

                ProgramController.ReportGenerationController.skipDelayJustificationStep()

            End If

            Me.Controls.Remove(Me.skipWarningMessagePanel)
        End Sub

        Private Sub initializeSkipWarningMessage()

            Me.skipWarningMessagePanel = New Common.UserMessagePanel("Avertissement!", "Aucun délai ne sera sauvegardé." & Environment.NewLine & "Changer d'étape quand même?", Constants.UI.Images._64x64.WARNING, True)

            Me.skipWarningMessagePanel.ajustLayout(SKIP_WARNING_MESSAGE_SIZE)
        End Sub

        '
        ' Combobox Drawing
        '
        Private Sub setTypeItemsBackColor(sender As Object, e As DrawItemEventArgs)

            If (e.Index < 0) Then
                Exit Sub
            End If

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Dim backColor As Color
            Dim itemsText As String
            Dim itemsRectangle As Rectangle = New Rectangle(1, e.Bounds.Top + 1, e.Bounds.Width - 1, e.Bounds.Height - 1)

            If (Me.delayTypeCombobox.Items(e.Index).Equals(ALL_DELAYS)) Then

                backColor = Color.White
                itemsText = ALL_DELAYS

            ElseIf (Me.delayTypeCombobox.Items(e.Index).Equals(UNKNOWN_DELAY)) Then

                backColor = Color.White
                itemsText = UNKNOWN_DELAY
            Else

                backColor = DirectCast(Me.delayTypeCombobox.Items(e.Index), DelayType).Color
                itemsText = DirectCast(Me.delayTypeCombobox.Items(e.Index), DelayType).Name
            End If

            e.Graphics.DrawRectangle(New Pen(backColor), itemsRectangle)
            e.Graphics.FillRectangle(New SolidBrush(backColor), itemsRectangle)

            If ((e.State And DrawItemState.Selected) = DrawItemState.Selected) OrElse ((e.State And DrawItemState.ComboBoxEdit) = DrawItemState.ComboBoxEdit) Then
                e.Graphics.DrawString(itemsText, Constants.UI.Fonts.DEFAULT_FONT_BOLD, Brushes.Black, e.Bounds.Height + 5, ((e.Bounds.Height - Constants.UI.Fonts.DEFAULT_FONT_BOLD.Height) / 2) + e.Bounds.Top)
            Else
                e.Graphics.DrawString(itemsText, Me.Font, Brushes.Black, e.Bounds.Height + 1, ((e.Bounds.Height - Me.Font.Height) / 2) + e.Bounds.Top)
            End If
        End Sub

        Private Sub setCodeItemsBackColor(sender As Object, e As DrawItemEventArgs)

            If (e.Index < 0) Then
                Exit Sub
            End If

            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

            Dim backColor As Color
            Dim itemsText As String
            Dim itemsRectangle As Rectangle = New Rectangle(1, e.Bounds.Top + 1, e.Bounds.Width - 1, e.Bounds.Height - 1)

            backColor = DirectCast(Me.delayCodeCombobox.Items(e.Index), DelayCode).Type.Color
            itemsText = DirectCast(Me.delayCodeCombobox.Items(e.Index), DelayCode).ToString

            e.Graphics.DrawRectangle(New Pen(backColor), itemsRectangle)
            e.Graphics.FillRectangle(New SolidBrush(backColor), itemsRectangle)

            If ((e.State And DrawItemState.Selected) = DrawItemState.Selected) OrElse ((e.State And DrawItemState.ComboBoxEdit) = DrawItemState.ComboBoxEdit) Then
                e.Graphics.DrawString(itemsText, Constants.UI.Fonts.DEFAULT_FONT_BOLD, Brushes.Black, e.Bounds.Height + 5, ((e.Bounds.Height - Constants.UI.Fonts.DEFAULT_FONT_BOLD.Height) / 2) + e.Bounds.Top)
            Else
                e.Graphics.DrawString(itemsText, Me.Font, Brushes.Black, e.Bounds.Height + 1, ((e.Bounds.Height - Me.Font.Height) / 2) + e.Bounds.Top)
            End If

        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Public Overrides ReadOnly Property OverallProgressValue As Integer
            Get
                Return 30
            End Get
        End Property
    End Class

End Namespace