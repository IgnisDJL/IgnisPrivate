Namespace UI

    Public Class FinishingGenerationStepView
        Inherits GenerationStepView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Génération du rapport"

        ' Components | #refactor - make it dynamic (the labels and what not)
        Private summaryDailyLabel As Label
        Private summaryDailyProgressBar As ProgressBar

        ' Attributes

        Public Sub New()
            MyBase.New()

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Dim nbProgressBars As Integer = 0

            Me.summaryDailyLabel = New Label
            Me.summaryDailyLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.summaryDailyLabel.AutoSize = False
            Me.summaryDailyLabel.Text = "Rapports journaliers sommaires (Word | PDF)"

            Me.summaryDailyProgressBar = New ProgressBar
            Me.summaryDailyProgressBar.Style = ProgressBarStyle.Continuous
            Me.summaryDailyProgressBar.Value = 0

            ' #refactor - When you get the list of reports to generate, then add the elements to the control list
            Me.Controls.Add(Me.summaryDailyLabel)
            Me.Controls.Add(Me.summaryDailyProgressBar)

            nbProgressBars += 1

            ' Initialize layout with nb of progress bars
            ' #refactor - move to addProgressBar method
            Me.layout = New DataFilesAnalysisStepLayout(nbProgressBars)

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, DataFilesAnalysisStepLayout)

            If (Not IsNothing(Me.summaryDailyLabel)) Then

                Me.summaryDailyLabel.Location = layout.FirstLabel_Location
                Me.summaryDailyLabel.Size = layout.FirstLabel_Size

                Me.summaryDailyProgressBar.Location = layout.FirstProgressBar_Location
                Me.summaryDailyProgressBar.Size = layout.FirstProgressBar_Size

            End If

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

        End Sub

        Public Sub showProgress(currentProgress As Integer, overallProgress As Integer)

            Me.summaryDailyProgressBar.Value = Math.Floor(currentProgress / overallProgress * 100)

            raiseProgressEvent(currentProgress / overallProgress * 100)
        End Sub

        Public Overrides Sub afterShow()

        End Sub

        Protected Overloads Overrides Sub beforeShow()

            If (Not IsNothing(Me.summaryDailyProgressBar)) Then
                Me.summaryDailyProgressBar.Value = 0
            End If

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Protected Overrides Sub cancel()
            ProgramController.ReportGenerationController.cancelGeneration()
        End Sub

        Protected Overrides Sub goBack()
            ProgramController.ReportGenerationController.goBackFromFinishingGenerationStep()
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