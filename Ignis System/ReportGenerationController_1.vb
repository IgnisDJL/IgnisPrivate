Imports System.Threading

Public Class ReportGenerationController_1
    ' Takes care of all the report generation sychronizing and such

    ' Attributes
    Private report As Report
    Private selectedProductionDays As List(Of ProductionDay_1)
    Private selectedReportReadyProductionDays As List(Of ProductionDay_1)
    Private dailyReportFactory As DailyReportFactory

    Private _generationCancelled As Boolean
    Private currentManualDataIndex As Integer

    Private cancelGenerationHandler As FormClosingEventHandler

    Public Sub New()

        selectedProductionDays = New List(Of ProductionDay_1)
        selectedReportReadyProductionDays = New List(Of ProductionDay_1)
        dailyReportFactory = New DailyReportFactory
        Me.cancelGenerationHandler = New FormClosingEventHandler(AddressOf cancelGeneration)



        'Me.manualDataStepWasSkipped = False
        'Me.delayJustificationStepWasSkipped = False

    End Sub

    Public Sub setSelectedProductionDays(productionDays As List(Of ProductionDay_1))

        Me.selectedProductionDays = productionDays
        Me.selectedReportReadyProductionDays.Clear()

        For Each _day As ProductionDay_1 In Me.selectedProductionDays
            Me.selectedReportReadyProductionDays.Add(_day)
        Next

    

    End Sub

    Public Sub startDailyReportGenerationSequence(dateDebut As Date, dateFin As Date)

        ProgramController.UIController.changeView(ProgramController.UIController.ReportGenerationFrame)
        Me.createReport(dateDebut, dateFin)

    End Sub

    ' -------------------
    ' Data files analysis
    ' -------------------
    Private Sub createReport(dateDebut As Date, dateFin As Date)

        Me._generationCancelled = False
        AddHandler ProgramController.UIController.MainFrame.FormClosing, Me.cancelGenerationHandler

        'ProgramController.UIController.DataFilesAnalysisStepView.ProductionDays = Me.selectedReportReadyProductionDays
        'ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.DataFilesAnalysisStepView, 0)

        report = dailyReportFactory.createDailyReport(dateDebut, dateFin)

        TryCast(report, DailyReport).getTableauHoraire()
        TryCast(report, DailyReport).getTableauEnrobes()
        TryCast(report, DailyReport).getTableauModeProduction()
        TryCast(report, DailyReport).getTableauDelais()
        TryCast(report, DailyReport).getTableauBitumeConsommes()
        TryCast(report, DailyReport).getTableauEcartValeurVisee()
        TryCast(report, DailyReport).getTableauVariationEnProduction()
        TryCast(report, DailyReport).getTableauValeursAberrantes()
        TryCast(report, DailyReport).getTableauCarburants()
        TryCast(report, DailyReport).getTableauRejets()
        TryCast(report, DailyReport).getTableauDelay()

        startManualDataStep(report)
    End Sub


    ' ----------------
    ' Manual Data
    ' ----------------
    Private Sub startManualDataStep(report As DailyReport)

        Me.currentManualDataIndex = 0
        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.ManualDataStepView, ProgramController.UIController.DataFilesAnalysisStepView.OverallProgressValue)
        ProgramController.UIController.ManualDataStepView.showManualData(report.getDonneeManuel(), 1)

    End Sub

    Public Sub showNextManualData()

        'If (currentManualDataIndex = Me.selectedReportReadyProductionDays.Count - 1) Then

        '    finishManualDataStep()

        'Else

        '    Me.currentManualDataIndex += 1

        '    'ProgramController.UIController.ManualDataStepView.showManualData(selectedReportReadyProductionDays(currentManualDataIndex).ManualData, currentManualDataIndex / selectedReportReadyProductionDays.Count * 100)

        'End If

    End Sub

    Public Sub showPreviousManualData()

        'If (currentManualDataIndex = 0) Then

        '    cancelGeneration()

        'Else

        '    Me.currentManualDataIndex -= 1

        '    'ProgramController.UIController.ManualDataStepView.showManualData(selectedReportReadyProductionDays(currentManualDataIndex).ManualData, currentManualDataIndex / selectedReportReadyProductionDays.Count * 100)

        'End If

    End Sub

    Private Sub finishManualDataStep()

        'ProgramController.UIController.ReportGenerationFrame.ManualDataStepFinished = True


        'Me.manualDataSavingThread = New Thread(AddressOf saveManualData)
        'manualDataSavingThread.Start()

        'startDelayJustificationStep()

    End Sub

    Private Sub saveManualData()

        'For Each day As ProductionDay_1 In Me.selectedReportReadyProductionDays

        '    'If (day.ManualData.isComplete) Then

        '    '    ProgramController.ManualDataPersistence.addData(day.ManualData)

        '    'End If

        'Next

        'Console.WriteLine("Done saving manual data")
    End Sub

    Public Sub skipManualDataStep()

        'Me.manualDataStepWasSkipped = True

        'Me.currentManualDataIndex = 0

        'ProgramController.UIController.ReportGenerationFrame.ManualDataStepSkipped = True

        'startDelayJustificationStep()

    End Sub

    ' --------------------
    ' Delays Justification
    ' --------------------
    Private Sub startDelayJustificationStep()

        'ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.DelaysJustificationStepView, ProgramController.UIController.ManualDataStepView.OverallProgressValue)

        'allDelays = New List(Of Delay_1)

        'For Each day As ProductionDay_1 In Me.selectedReportReadyProductionDays

        '    'day.preComputeDelaysStatistics()

        '    'allDelays.AddRange(day.Statistics.EventsStatistics.JustifiableDelays)

        'Next

        'If (allDelays.Count = 0) Then
        '    finishDelayJustificationStep()
        'Else
        '    Me.currentDelayIndex = 0
        '    ProgramController.UIController.DelaysJustificationStepView.showDelay(allDelays(Me.currentDelayIndex), Me.currentDelayIndex + 1, allDelays.Count)
        'End If


    End Sub

    Public Sub showNextDelay()

        'If (currentDelayIndex = allDelays.Count - 1) Then

        '    finishDelayJustificationStep()

        'Else
        '    Me.currentDelayIndex += 1
        '    ProgramController.UIController.DelaysJustificationStepView.showDelay(allDelays(Me.currentDelayIndex), Me.currentDelayIndex + 1, allDelays.Count)
        'End If

    End Sub

    Public Sub showPreviousDelay()

        'If (Me.currentDelayIndex = 0) Then

        '    ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.ManualDataStepView, -ProgramController.UIController.ManualDataStepView.OverallProgressValue)

        '    ProgramController.UIController.ReportGenerationFrame.ManualDataStepFinished = False

        '    'ProgramController.UIController.ManualDataStepView.showManualData(selectedReportReadyProductionDays(currentManualDataIndex).ManualData, currentManualDataIndex / (selectedReportReadyProductionDays.Count) * 100)

        'Else

        '    Me.currentDelayIndex -= 1
        '    ProgramController.UIController.DelaysJustificationStepView.showDelay(allDelays(Me.currentDelayIndex), Me.currentDelayIndex + 1, allDelays.Count)

        'End If

    End Sub

    Public Sub splitDelay(delay As Delay_1, splitTime As Date)

        'For Each day As ProductionDay_1 In Me.selectedReportReadyProductionDays

        '    'If (day.Statistics.EventsStatistics.Delays.Contains(delay)) Then

        '    '    Dim newDelays As Delay_1() = day.Statistics.EventsStatistics.splitDelay(delay, splitTime)

        '    '    Me.allDelays.Remove(delay)
        '    '    Me.allDelays.Insert(Me.currentDelayIndex, newDelays(0))
        '    '    Me.allDelays.Insert(Me.currentDelayIndex + 1, newDelays(1))

        '    '    ProgramController.UIController.DelaysJustificationStepView.showDelay(allDelays(Me.currentDelayIndex), Me.currentDelayIndex + 1, allDelays.Count)

        '    '    Exit Sub
        '    'End If
        'Next

    End Sub

    Public Sub mergeDelays(delay As Delay_1)

        'For Each day As ProductionDay_1 In Me.selectedReportReadyProductionDays

        '    'If (day.Statistics.EventsStatistics.Delays.Contains(delay)) Then

        '    '    Dim newDelay = day.Statistics.EventsStatistics.mergeDelays(delay)

        '    '    Dim indexOfDelay = Me.allDelays.IndexOf(delay)

        '    '    Me.allDelays.RemoveAt(indexOfDelay)
        '    '    Me.allDelays.RemoveAt(indexOfDelay)
        '    '    Me.allDelays.Insert(indexOfDelay, newDelay)

        '    '    ProgramController.UIController.DelaysJustificationStepView.showDelay(allDelays(Me.currentDelayIndex), Me.currentDelayIndex + 1, allDelays.Count)

        '    '    Exit Sub
        '    'End If

        'Next

    End Sub

    Private Sub finishDelayJustificationStep()

        ' '' Section à analyser plus tard, utilse à comprendre pour pouvoir terminer la section des délais
        ' '' Break point à ajouter si, la durée du délais est inférieur à l'écart possible entre deux cycles consécutif.
        ' '' Cas particulié avec des fichiers logs.


        'For Each day As ProductionDay_1 In Me.selectedReportReadyProductionDays

        '    'day.finalizeDelayStatistics()

        'Next

        'ProgramController.UIController.ReportGenerationFrame.DelaysJustificationStepFinished = True

        'startCommentsStep()

    End Sub

    Public Sub skipDelayJustificationStep()

        'Me.delayJustificationStepWasSkipped = True

        'Me.currentDelayIndex = 0

        'ProgramController.UIController.ReportGenerationFrame.DelaysJustificationStepSkipped = True

        'For Each delay In allDelays
        '    delay.setIdCategorie(Nothing)
        '    delay.setIdJustification(Nothing)
        'Next

        'startCommentsStep()

    End Sub

    ' ----------------
    ' Comments Step
    ' ----------------
    Public Sub startCommentsStep()

        'Me.currentCommentIndex = 0

        'ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.CommentsStepView, ProgramController.UIController.DelaysJustificationStepView.OverallProgressValue)

        'ProgramController.UIController.CommentsStepView.showDay(Me.selectedReportReadyProductionDays.First, Me.currentCommentIndex + 1, Me.selectedReportReadyProductionDays.Count)

    End Sub

    Public Sub showNextComment()

        'If (currentCommentIndex = Me.selectedReportReadyProductionDays.Count - 1) Then

        '    finishCommentsStep()

        'Else
        '    Me.currentCommentIndex += 1
        '    ProgramController.UIController.CommentsStepView.showDay(Me.selectedReportReadyProductionDays(Me.currentCommentIndex), Me.currentCommentIndex + 1, Me.selectedReportReadyProductionDays.Count)
        'End If

    End Sub

    Public Sub showPreviousComment()

        'If (Me.currentCommentIndex = 0) Then

        '    ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.DelaysJustificationStepView, -ProgramController.UIController.DelaysJustificationStepView.OverallProgressValue)

        '    ProgramController.UIController.ReportGenerationFrame.DelaysJustificationStepFinished = False

        '    If (Me.allDelays.Count > 0) Then

        '        ProgramController.UIController.DelaysJustificationStepView.showDelay(allDelays(Me.currentDelayIndex), Me.currentDelayIndex + 1, allDelays.Count)
        '    Else

        '        Me.showPreviousDelay()
        '    End If

        'Else

        '    Me.currentCommentIndex -= 1
        '    ProgramController.UIController.CommentsStepView.showDay(Me.selectedReportReadyProductionDays(Me.currentCommentIndex), Me.currentCommentIndex + 1, Me.selectedReportReadyProductionDays.Count)

        'End If

    End Sub

    Public Sub finishCommentsStep()

        'ProgramController.UIController.ReportGenerationFrame.CommentsStepFinished = True

        'startFinishingGenerationStep()

    End Sub

    Public Sub skipCommentsStep()

        'ProgramController.UIController.ReportGenerationFrame.CommentsStepWasSkipped = True

        'Me.currentCommentIndex = 0

        'For Each day In Me.selectedReportReadyProductionDays
        '    'day.Comments = Nothing
        'Next

        'startFinishingGenerationStep()

    End Sub

    ' ----------------
    ' Finishing generation Step
    ' ----------------
    Public Sub startFinishingGenerationStep()

        'ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.FinishingGenerationStepView, ProgramController.UIController.CommentsStepView.OverallProgressValue)


        'Me.summaryDailyReportGenerationThread = New Thread(Sub() generateSummaryDailyReports(Me.selectedReportReadyProductionDays))

        'Me.summaryDailyReportGenerationThread.Start()

    End Sub

    Private Sub generateSummaryDailyReports(productionDays As List(Of ProductionDay_1))

        'Me.summaryDailyReportsGenerator = New SummaryDailyReportGenerator()

        'nbReportsFinished = 0

        'For Each _productionDay In productionDays

        '    AddHandler Me.summaryDailyReportsGenerator.ProcessComplete, AddressOf onReportFinished
        '    AddHandler Me.summaryDailyReportsGenerator.CurrentProgress, AddressOf monitorReportGenerationProgress

        '    Me.summaryDailyReportsGenerator.generateReport(_productionDay)

        '    RemoveHandler Me.summaryDailyReportsGenerator.ProcessComplete, AddressOf onReportFinished
        '    RemoveHandler Me.summaryDailyReportsGenerator.CurrentProgress, AddressOf monitorReportGenerationProgress
        'Next

        'ProgramController.UIController.ReportGenerationFrame.FinishingGenerationStepFinished = True

        'Me.summaryDailyReportsGenerator.disposeOfRessources()

        'Thread.Sleep(1000)

        'ProgramController.UIController.invokeFromUIThread(Sub() finalizeGenrationStep())

    End Sub

    'Private nbReportsFinished As Integer = 0
    Public Sub monitorReportGenerationProgress(currentReportProgress As Object)

        'ProgramController.UIController.invokeFromUIThread(Sub() ProgramController.UIController.FinishingGenerationStepView.showProgress((nbReportsFinished + CInt(currentReportProgress) / 100) * 100, Me.selectedReportReadyProductionDays.Count * 100))
    End Sub

    Private Sub onReportFinished(sender As Object)
        'nbReportsFinished += 1
    End Sub

    Private Sub finalizeGenrationStep()

        'ProgramController.UIController.changeView(ProgramController.UIController.DailyReportView)

        'If (XmlSettings.Settings.instance.Usine.EmailsInfo.SHOW_POPUP_AFTER_GENERATION) Then
        '    ProgramController.UIController.DailyReportView.showSendReportsByEmailPanel(Me.selectedReportReadyProductionDays.Count)
        'End If
    End Sub

    Public Sub goBackFromFinishingGenerationStep()

        'ProgramController.UIController.ReportGenerationFrame.FinishingGenerationStepFinished = False

        'ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.CommentsStepView, -ProgramController.UIController.CommentsStepView.OverallProgressValue)

        'ProgramController.UIController.ReportGenerationFrame.CommentsStepFinished = False

        'ProgramController.UIController.CommentsStepView.showDay(Me.selectedReportReadyProductionDays(Me.currentCommentIndex), Me.currentCommentIndex + 1, Me.selectedReportReadyProductionDays.Count)
    End Sub

    Public Sub emailLastGeneratedReports()

        'ProgramController.FileExportationController.FilesToExport.Clear()

        'For Each _productionDay In Me.selectedReportReadyProductionDays

        '    'ProgramController.FileExportationController.FilesToExport.Add(_productionDay.ReportFilesInfo.SummaryReadOnlyDailyReport)
        'Next

        'ProgramController.UIController.changeView(ProgramController.UIController.EmailExportationView)
    End Sub

    Public Sub cancelGeneration()

        'Me._generationCancelled = True

        '' Kill data files analysis threads
        ''If (Not IsNothing(Me.csvAnalysisThread)) Then
        ''    Me.csvAnalysisThread.Abort()
        ''End If

        ''If (Not IsNothing(Me.logAnalysisThread)) Then
        ''    Me.logAnalysisThread.Abort()
        ''End If

        ''If (Not IsNothing(Me.mdbAnalysisThread)) Then
        ''    Me.mdbAnalysisThread.Abort()
        ''End If

        ''If (Not IsNothing(Me.eventsAnalysisThread)) Then
        ''    Me.eventsAnalysisThread.Abort()
        ''End If

        'If (Not IsNothing(Me.manualDataSavingThread)) Then
        '    Me.manualDataSavingThread.Abort()
        'End If

        'If (Not IsNothing(Me.summaryDailyReportGenerationThread)) Then
        '    Me.summaryDailyReportGenerationThread.Abort()
        'End If

        Me.selectedProductionDays = Nothing
        Me.selectedReportReadyProductionDays.Clear()
        ProgramController.UIController.changeView(ProgramController.UIController.DailyReportView)
        RemoveHandler ProgramController.UIController.MainFrame.FormClosing, Me.cancelGenerationHandler

    End Sub

    Public ReadOnly Property GenerationCancelled As Boolean
        Get
            Return Me._generationCancelled
        End Get
    End Property

    'Public ReadOnly Property ReportsToGenerate As List(Of ReportFile.ReportTypes)
    '    Get
    '        Return Me._reportsToGenerate
    '    End Get
    'End Property

    'Public ReadOnly Property AvailableReportsToGenerate As List(Of ReportFile.ReportTypes)
    '    Get
    '        Return Me._availableReportsToGenerate
    '    End Get
    'End Property
End Class
