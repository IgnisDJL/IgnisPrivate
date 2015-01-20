Namespace UI

    Public Class CommentsStepView
        Inherits GenerationStepView

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "Commentaires supplémentaires"
        Public Shared ReadOnly LABELS_HEIGHT As Integer = 30

        Private Shared ReadOnly SKIP_WARNING_MESSAGE_SIZE As Size = New Size(400, 145)

        ' Components
        Private dateLabel As Label

        Private commentLabel As Label

        Private WithEvents commentTextBox As TextBox

        Private WithEvents nextButton As Common.NextButton
        Private WithEvents skipButton As Button

        Private WithEvents skipWarningMessagePanel As Common.UserMessagePanel

        ' Attributes
        Private currentDay As ProductionDay


        Public Sub New()
            MyBase.New()

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.dateLabel = New Label
            Me.dateLabel.AutoSize = False
            Me.dateLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.dateLabel.Font = Constants.UI.Fonts.BIGGER_DEFAULT_FONT_BOLD

            Me.commentLabel = New Label
            Me.commentLabel.AutoSize = False
            Me.commentLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.commentLabel.Text = "Commentaires :"

            Me.commentTextBox = New TextBox
            Me.commentTextBox.Multiline = True

            Me.nextButton = New Common.NextButton

            Me.skipButton = New Button
            Me.skipButton.TextAlign = ContentAlignment.MiddleCenter
            Me.skipButton.Text = "Étape suivante"
            Me.skipButton.Font = Constants.UI.Fonts.SMALLER_DEFAULT_FONT

            Me.OtherButtons.Add(Me.nextButton)
            Me.OtherButtons.Add(Me.skipButton)

            Me.Controls.Add(dateLabel)
            Me.Controls.Add(commentLabel)
            Me.Controls.Add(commentTextBox)

            Me.commentTextBox.TabIndex = 1
            Me.nextButton.TabIndex = 2
            Me.skipButton.TabIndex = 3
            Me.cancelButton.TabIndex = 4
            Me.backButton.TabIndex = 5

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Me.dateLabel.Location = New Point(0, 0)
            Me.dateLabel.Size = New Size(Me.Width, LABELS_HEIGHT)

            Me.commentLabel.Location = New Point(ReportGenerationFrameLayout.LOCATION_START_X, Me.dateLabel.Height + 2 * ReportGenerationFrameLayout.SPACE_BETWEEN_CONTROLS_Y)
            Me.commentLabel.Size = New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, LABELS_HEIGHT)

            Me.commentTextBox.Location = New Point(ReportGenerationFrameLayout.LOCATION_START_X, Me.commentLabel.Location.Y + Me.commentLabel.Height)
            Me.commentTextBox.Size = New Size(Me.commentLabel.Width, Me.Height / 2)

            ' Next Button (In buttons panel)
            Me.nextButton.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me.nextButton.Size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

            ' Skip Button (In buttons panel)
            Me.skipButton.Location = New Point(nextButton.Location.X - ReportGenerationFrameLayout.SPACE_BETWEEN_CONTROLS_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me.skipButton.Size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

            If (Not IsNothing(Me.skipWarningMessagePanel)) Then
                Me.skipWarningMessagePanel.Location = New Point(Me.Width / 2 - SKIP_WARNING_MESSAGE_SIZE.Width / 2, Me.Height / 2 - SKIP_WARNING_MESSAGE_SIZE.Height / 2)
            End If

        End Sub

        Public Sub showDay(day As ProductionDay, currentDayNumber As Integer, totalNumberOfDays As Integer)

            Me.currentDay = day

            raiseProgressEvent((currentDayNumber - 1) / totalNumberOfDays * 100)

            Me.dateLabel.Text = StrConv(Me.currentDay.Date_.ToString("dddd d MMMM"), VbStrConv.ProperCase)

            Me.commentTextBox.Text = Me.currentDay.Comments

            Me.commentTextBox.Focus()
        End Sub

        Private Sub saveComment() Handles commentTextBox.TextChanged
            Me.currentDay.Comments = Me.commentTextBox.Text
        End Sub

        Public Overrides Sub afterShow()

            If (Not IsNothing(Me.skipWarningMessagePanel)) Then
                Me.Controls.Remove(Me.skipWarningMessagePanel)
            End If
        End Sub

        Protected Overloads Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Protected Overrides Sub cancel()

            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.cancelGeneration()

        End Sub

        Private Sub goNext() Handles nextButton.Click

            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.showNextComment()

        End Sub

        Protected Overrides Sub goBack()

            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.showPreviousComment()

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

                ProgramController.ReportGenerationController.skipCommentsStep()

            End If

            Me.Controls.Remove(Me.skipWarningMessagePanel)
        End Sub

        Private Sub initializeSkipWarningMessage()

            Me.skipWarningMessagePanel = New Common.UserMessagePanel("Avertissement!", "Aucun commentaire ne sera sauvegardé." & Environment.NewLine & "Changer d'étape quand même?", Constants.UI.Images._64x64.WARNING, True)

            Me.skipWarningMessagePanel.ajustLayout(SKIP_WARNING_MESSAGE_SIZE)
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Public Overrides ReadOnly Property OverallProgressValue As Integer
            Get
                Return 15
            End Get
        End Property
    End Class
End Namespace
