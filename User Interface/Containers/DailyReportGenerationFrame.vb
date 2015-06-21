Namespace UI

    Public Class DailyReportGenerationFrame
        Inherits View

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "Génération - Rapports journaliers"

        ' Components

        ' --- Progression Panel
        Private progressionPanel As Panel
        Private progressionTitleLabel As Label
        Private overAllProgressBar As ProgressBar
        Private analysisStepLabel As Label
        Private manualDataStepLabel As Label
        Private delaysJustificationStepLabel As Label
        Private commentsStepLabel As Label
        Private finishingGenerationStepLabel As Label


        Private generationStepView As GenerationStepView


        Private buttonsPanel As Panel


        ' Attributes
        Private progressCompleted As Integer = 0

        Public Sub New()
            MyBase.New()

            Me.layout = New ReportGenerationFrameLayout

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            ' Progression panel
            Me.progressionPanel = New Panel

            Me.progressionTitleLabel = New Label
            Me.progressionTitleLabel.AutoSize = False
            Me.progressionTitleLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.progressionTitleLabel.Text = "Progression"


            Me.overAllProgressBar = New ProgressBar
            Me.overAllProgressBar.Style = ProgressBarStyle.Continuous
            Me.overAllProgressBar.Value = 0

            Me.analysisStepLabel = New Label
            Me.analysisStepLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.analysisStepLabel.AutoSize = False
            Me.analysisStepLabel.Text = "Analyse"
            Me.analysisStepLabel.ImageAlign = ContentAlignment.MiddleRight

            Me.manualDataStepLabel = New Label
            Me.manualDataStepLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.manualDataStepLabel.AutoSize = False
            Me.manualDataStepLabel.Text = "Données complémentaires"
            Me.manualDataStepLabel.ImageAlign = ContentAlignment.MiddleRight

            Me.delaysJustificationStepLabel = New Label
            Me.delaysJustificationStepLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.delaysJustificationStepLabel.AutoSize = False
            Me.delaysJustificationStepLabel.Text = "Délais"
            Me.delaysJustificationStepLabel.ImageAlign = ContentAlignment.MiddleRight

            Me.commentsStepLabel = New Label
            Me.commentsStepLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.commentsStepLabel.AutoSize = False
            Me.commentsStepLabel.Text = "Commentaires"
            Me.commentsStepLabel.ImageAlign = ContentAlignment.MiddleRight

            Me.finishingGenerationStepLabel = New Label
            Me.finishingGenerationStepLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.finishingGenerationStepLabel.AutoSize = False
            Me.finishingGenerationStepLabel.Text = "Génération"
            Me.finishingGenerationStepLabel.ImageAlign = ContentAlignment.MiddleRight

            Me.progressionPanel.Controls.Add(progressionTitleLabel)
            Me.progressionPanel.Controls.Add(overAllProgressBar)
            Me.progressionPanel.Controls.Add(analysisStepLabel)
            Me.progressionPanel.Controls.Add(manualDataStepLabel)
            Me.progressionPanel.Controls.Add(delaysJustificationStepLabel)
            Me.progressionPanel.Controls.Add(commentsStepLabel)
            Me.progressionPanel.Controls.Add(finishingGenerationStepLabel)

            ' Buttons panel
            Me.buttonsPanel = New Panel

            Me.Controls.Add(Me.progressionPanel)
            Me.Controls.Add(Me.generationStepView)
            Me.Controls.Add(Me.buttonsPanel)



        End Sub

        Protected Overloads Overrides Sub ajustLayout(newSize As Size)

            Dim layout As ReportGenerationFrameLayout = DirectCast(Me.layout, ReportGenerationFrameLayout)

            ' Progression Panel
            Me.progressionPanel.Location = layout.ProgressionPanel_Location
            Me.progressionPanel.Size = layout.ProgressionPanel_Size

            ' Progression title Label (Inside Progression Panel)
            Me.progressionTitleLabel.Location = layout.ProgressionTitleLabel_Location
            Me.progressionTitleLabel.Size = layout.ProgressionTitleLabel_Size

            ' Progression Bar (Inside Progression Panel)
            Me.overAllProgressBar.Location = layout.ProgressionBar_Location
            Me.overAllProgressBar.Size = layout.ProgressionBar_Size

            ' Analysis Step Label (Inside Progression Panel)
            Me.analysisStepLabel.Location = layout.AnalysisStepLabel_Location
            Me.analysisStepLabel.Size = layout.AnalysisStepLabel_Size

            ' Manual Data Step Label (Inside Progression Panel)
            Me.manualDataStepLabel.Location = layout.ManualDataStepLabel_Location
            Me.manualDataStepLabel.Size = layout.ManualDataStepLabel_Size

            ' Events Justification Step Label (Inside Progression Panel)
            Me.delaysJustificationStepLabel.Location = layout.DelaysJustificationStepLabel_Location
            Me.delaysJustificationStepLabel.Size = layout.DelaysJustificationStepLabel_Size

            ' KA01_Comments Step Label (Inside Progression Panel)
            Me.commentsStepLabel.Location = layout.CommentsStepLabel_Location
            Me.commentsStepLabel.Size = layout.CommentsStepLabel_Size

            ' Finishing Generation Step Label (Inside Progression Panel)
            Me.finishingGenerationStepLabel.Location = layout.FinishingGenerationStepLabel_Location
            Me.finishingGenerationStepLabel.Size = layout.FinishingGenerationStepLabel_Size

            ' Buttons Panel
            Me.buttonsPanel.Location = layout.ButtonsPanel_Location
            Me.buttonsPanel.Size = layout.buttonsPanel_Size

            ' Generation Step View
            If (Not IsNothing(Me.generationStepView)) Then
                Me.generationStepView.Location = layout.GenerationStepView_Location
                Me.generationStepView.ajustLayout(layout.GenerationStepView_Size)

                Me.generationStepView.getBackButton.Location = layout.BackButton_Location
                Me.generationStepView.getBackButton.Size = layout.BackButton_Size

                Me.generationStepView.getCancelButton.Location = layout.CancelButton_Location
                Me.generationStepView.getCancelButton.Size = layout.CancelButton_Size
            End If
        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal(newSize As Size)
            If (Not IsNothing(Me.generationStepView)) Then
                Me.generationStepView.ajustLayoutFinal(DirectCast(Me.layout, ReportGenerationFrameLayout).GenerationStepView_Size())
            End If
        End Sub

        Public Sub changeStep(newGenerationStepView As GenerationStepView, valueToAddToProgressBar As Integer)

            Me.removeGenerationStepView()

            Me.progressCompleted += valueToAddToProgressBar
            Me.overAllProgressBar.Value = Me.progressCompleted

            Me.generationStepView = newGenerationStepView

            Me.generationStepView.Visible = False

            Me.generationStepView.beforeShow(DirectCast(Me.layout, ReportGenerationFrameLayout).GenerationStepView_Size)

            Me.Controls.Add(newGenerationStepView)

            Me.generationStepView.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.buttonsPanel.Controls.Add(Me.generationStepView.getBackButton)
            Me.buttonsPanel.Controls.Add(Me.generationStepView.getCancelButton)

            For Each otherButtons As Button In Me.generationStepView.OtherButtons
                Me.buttonsPanel.Controls.Add(otherButtons)
            Next

            AddHandler Me.generationStepView.progressEvent, AddressOf Me.updateOverallProgressBar

            Me.progressionTitleLabel.Text = Me.generationStepView.Name

            Me.ajustLayout(Me.Size)
            Me.ajustLayoutFinal(Me.Size)

            Me.Refresh()

            Me.generationStepView.Visible = True

            Me.generationStepView.afterShow()

        End Sub

        Private Sub updateOverallProgressBar(currentViewProgress As Integer)

            Dim currentProgress As Integer = Me.progressCompleted + (currentViewProgress / 100) * Me.generationStepView.OverallProgressValue

            If (Not Me.overAllProgressBar.Value = currentProgress) Then
                Me.overAllProgressBar.Value = currentProgress
            End If

        End Sub

        Protected Overloads Overrides Sub beforeShow()
            reset()
        End Sub

        Public Overrides Sub afterShow()
            Me.Refresh()
        End Sub

        Public Overrides Sub onHide()

        End Sub

        Public WriteOnly Property AnalsysisStepFinished As Boolean
            Set(isFinished As Boolean)

                If (isFinished) Then
                    applyFinishedStyle(Me.analysisStepLabel)
                Else
                    applyUnfinishedStyle(Me.analysisStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property ManualDataStepFinished As Boolean
            Set(isFinished As Boolean)

                If (isFinished) Then
                    applyFinishedStyle(Me.manualDataStepLabel)
                Else
                    applyUnfinishedStyle(Me.manualDataStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property ManualDataStepSkipped As Boolean
            Set(wasSkipped As Boolean)

                If (wasSkipped) Then
                    applySkippedStyle(Me.manualDataStepLabel)
                Else
                    applyUnfinishedStyle(Me.manualDataStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property DelaysJustificationStepFinished As Boolean
            Set(isFinished As Boolean)

                If (isFinished) Then
                    applyFinishedStyle(Me.delaysJustificationStepLabel)
                Else
                    applyUnfinishedStyle(Me.delaysJustificationStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property DelaysJustificationStepSkipped As Boolean
            Set(wasSkipped As Boolean)

                If (wasSkipped) Then
                    applySkippedStyle(Me.delaysJustificationStepLabel)
                Else
                    applyUnfinishedStyle(Me.delaysJustificationStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property CommentsStepFinished As Boolean
            Set(isFinished As Boolean)

                If (isFinished) Then
                    applyFinishedStyle(Me.commentsStepLabel)
                Else
                    applyUnfinishedStyle(Me.commentsStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property CommentsStepWasSkipped As Boolean
            Set(wasSkipped As Boolean)
                If (wasSkipped) Then
                    applySkippedStyle(Me.commentsStepLabel)
                Else
                    applyUnfinishedStyle(Me.commentsStepLabel)
                End If
            End Set
        End Property

        Public WriteOnly Property FinishingGenerationStepFinished As Boolean
            Set(isFinished As Boolean)
                If (isFinished) Then
                    applyFinishedStyle(Me.finishingGenerationStepLabel)
                Else
                    applyUnfinishedStyle(Me.finishingGenerationStepLabel)
                End If
            End Set
        End Property

        Private Sub applyFinishedStyle(label As Label)
            label.Font = New Font(Constants.UI.Fonts.DEFAULT_FONT, FontStyle.Bold)
            label.ForeColor = Color.Green
            label.Image = Constants.UI.Images._24x24.GOOD
        End Sub

        Private Sub applyUnfinishedStyle(label As Label)
            label.Enabled = True
            label.Font = Constants.UI.Fonts.DEFAULT_FONT
            label.ForeColor = Color.Black
            label.Image = Nothing
        End Sub

        Private Sub applySkippedStyle(label As Label)
            applyUnfinishedStyle(label)
            label.Enabled = False
        End Sub

        Private Sub reset()

            Me.removeGenerationStepView()

            Me.progressCompleted = 0

            Me.AnalsysisStepFinished = False
            Me.ManualDataStepFinished = False
            Me.DelaysJustificationStepFinished = False
            Me.CommentsStepFinished = False
            Me.FinishingGenerationStepFinished = False

            Me.overAllProgressBar.Value = 0
        End Sub

        Private Sub removeGenerationStepView()
            If (Not IsNothing(Me.generationStepView)) Then

                Me.buttonsPanel.Controls.Remove(Me.generationStepView.getBackButton)
                Me.buttonsPanel.Controls.Remove(Me.generationStepView.getCancelButton)

                For Each otherButtons As Button In Me.generationStepView.OtherButtons
                    Me.buttonsPanel.Controls.Remove(otherButtons)
                Next

                Me.Controls.Remove(Me.generationStepView)

                Me.generationStepView.onHide()

                RemoveHandler Me.generationStepView.progressEvent, AddressOf updateOverallProgressBar
            End If
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property
    End Class

End Namespace
