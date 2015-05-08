Imports System.Threading

Public Class ReportGenerationController_1

    Private report As DailyReport
    Private dailyReportFactory As DailyReportFactory

    Private summaryDailyReportsGenerator As SummaryDailyReportGenerator_1
    Private _generationCancelled As Boolean
    Private manualDataStepWasSkipped As Boolean
    Private delayJustificationStepWasSkipped As Boolean
    Private cancelGenerationHandler As FormClosingEventHandler
    Private currentDelayIndex As Integer = 0
    Private reportToSend As SummaryDailyReport

    Public Sub New()
        dailyReportFactory = New DailyReportFactory
        manualDataStepWasSkipped = False
        delayJustificationStepWasSkipped = False
        currentDelayIndex = 0
        Me.cancelGenerationHandler = New FormClosingEventHandler(AddressOf cancelGeneration)

    End Sub

    Public Sub startDailyReportGenerationSequence(dateDebut As Date, dateFin As Date)

        ProgramController.UIController.changeView(ProgramController.UIController.ReportGenerationFrame)
        Me.createReport(dateDebut, dateFin)

    End Sub

    Private Sub createReport(dateDebut As Date, dateFin As Date)

        Me._generationCancelled = False
        AddHandler ProgramController.UIController.MainFrame.FormClosing, Me.cancelGenerationHandler

        report = dailyReportFactory.createDailyReport(dateDebut, dateFin)
        startManualDataStep(report)
    End Sub

    ' ----------------
    ' Manual Data
    ' ----------------
    Private Sub startManualDataStep(report As DailyReport)

        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.ManualDataStepView, 0)
        ProgramController.UIController.ManualDataStepView.showManualData(report.getDonneeManuel())

    End Sub

    Public Sub showNextManualData()

        finishManualDataStep()

    End Sub

    Public Sub showPreviousManualData()

        cancelGeneration()
    End Sub

    Private Sub finishManualDataStep()

        ProgramController.UIController.ReportGenerationFrame.ManualDataStepFinished = True
        saveManualData()
        startDelayJustificationStep()
    End Sub

    Private Sub saveManualData()

        If (report.getDonneeManuel.isComplete) Then

            ProgramController.ManualDataPersistence.addData(report.getDonneeManuel)

        End If

        Console.WriteLine("Done saving manual data")
    End Sub

    Public Sub skipManualDataStep()

        Me.manualDataStepWasSkipped = True

        ProgramController.UIController.ReportGenerationFrame.ManualDataStepSkipped = True

        startDelayJustificationStep()
    End Sub

    ' --------------------
    ' Delays Justification
    ' --------------------
    Private Sub startDelayJustificationStep()

        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.DelaysJustificationStepView, 0)


        If (report.getHybridDelayList.Count = 0) Then
            finishDelayJustificationStep()
        Else
            currentDelayIndex = 0
            ProgramController.UIController.DelaysJustificationStepView.showDelay(report.getHybridDelayList.Item(0), 1, report.getHybridDelayList.Count)
        End If


    End Sub

    Public Sub showNextDelay()

        If (currentDelayIndex = report.getHybridDelayList.Count - 1) Then

            finishDelayJustificationStep()

        Else
            Me.currentDelayIndex += 1
            ProgramController.UIController.DelaysJustificationStepView.showDelay(report.getHybridDelayList.Item(currentDelayIndex), Me.currentDelayIndex + 1, report.getHybridDelayList.Count)
        End If

    End Sub

    Public Sub showPreviousDelay()
        If (Me.currentDelayIndex = 0) Then

            ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.ManualDataStepView, 0)

            ProgramController.UIController.ReportGenerationFrame.ManualDataStepFinished = False

            ProgramController.UIController.ManualDataStepView.showManualData(report.getDonneeManuel)

        Else
            Me.currentDelayIndex -= 1
            ProgramController.UIController.DelaysJustificationStepView.showDelay(report.getHybridDelayList.Item(currentDelayIndex), Me.currentDelayIndex + 1, report.getHybridDelayList.Count)

        End If

    End Sub

    Public Sub splitDelay(delay As Delay_1, splitTime As Date)
        report.splitDelay(delay, splitTime)
        ProgramController.UIController.DelaysJustificationStepView.showDelay(report.getHybridDelayList.Item(currentDelayIndex), Me.currentDelayIndex + 1, report.getHybridDelayList.Count)
    End Sub

    Public Sub mergeDelays(delay As Delay_1)

        report.mergeDelays(delay, report.getHybridDelayList.Item(report.getHybridDelayList.IndexOf(delay) + 1))

        ProgramController.UIController.DelaysJustificationStepView.showDelay(report.getHybridDelayList.Item(currentDelayIndex), Me.currentDelayIndex + 1, report.getHybridDelayList.Count)
    End Sub

    Private Sub finishDelayJustificationStep()

        ProgramController.UIController.ReportGenerationFrame.DelaysJustificationStepFinished = True
        startCommentsStep()

    End Sub

    Public Sub skipDelayJustificationStep()

        Me.delayJustificationStepWasSkipped = True

        Me.currentDelayIndex = 0

        ProgramController.UIController.ReportGenerationFrame.DelaysJustificationStepSkipped = True

        For Each delay As Delay_1 In report.getHybridDelayList
            delay.setEmpty()
        Next

        startCommentsStep()

    End Sub

    ' ----------------
    ' Comments Step
    ' ----------------
    Public Sub startCommentsStep()

        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.CommentsStepView, 0)
        ProgramController.UIController.CommentsStepView.setReport(Me.report)
        ProgramController.UIController.CommentsStepView.showReport()

    End Sub

    Public Sub showNextComment()
        finishCommentsStep()
    End Sub

    Public Sub showPreviousComment()

        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.DelaysJustificationStepView, 0)

        ProgramController.UIController.ReportGenerationFrame.DelaysJustificationStepFinished = False

        Me.currentDelayIndex = Me.report.getHybridDelayList.Count
        Me.showPreviousDelay()

    End Sub

    Public Sub finishCommentsStep()

        ProgramController.UIController.ReportGenerationFrame.CommentsStepFinished = True

        startFinishingGenerationStep()

    End Sub

    Public Sub skipCommentsStep()

        ProgramController.UIController.ReportGenerationFrame.CommentsStepWasSkipped = True

        report.setReportComment(String.Empty)
        startFinishingGenerationStep()

    End Sub

    ' ----------------
    ' Finishing generation Step
    ' ----------------
    Private Sub startFinishingGenerationStep()

        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.FinishingGenerationStepView, 0)
        generateSummaryDailyReports()

    End Sub

    Private Sub generateSummaryDailyReports()

        Me.summaryDailyReportsGenerator = New SummaryDailyReportGenerator_1(report)

        AddHandler Me.summaryDailyReportsGenerator.ProcessComplete, AddressOf onReportFinished
        AddHandler Me.summaryDailyReportsGenerator.CurrentProgress, AddressOf monitorReportGenerationProgress

        Me.reportToSend = Me.summaryDailyReportsGenerator.generateReport()

        RemoveHandler Me.summaryDailyReportsGenerator.ProcessComplete, AddressOf onReportFinished
        RemoveHandler Me.summaryDailyReportsGenerator.CurrentProgress, AddressOf monitorReportGenerationProgress

        ProgramController.UIController.ReportGenerationFrame.FinishingGenerationStepFinished = True

        Me.summaryDailyReportsGenerator.disposeOfRessources()

        Thread.Sleep(1000)

        ProgramController.UIController.invokeFromUIThread(Sub() finalizeGenrationStep())

    End Sub

    Public Sub monitorReportGenerationProgress(currentReportProgress As Object)

        ProgramController.UIController.invokeFromUIThread(Sub() ProgramController.UIController.FinishingGenerationStepView.showProgress(1, 100))
    End Sub

    Private Sub onReportFinished(sender As Object)
        'nbReportsFinished += 1
    End Sub

    Private Sub finalizeGenrationStep()

        ProgramController.UIController.changeView(ProgramController.UIController.DailyReportView)

        If (XmlSettings.Settings.instance.Usine.EmailsInfo.SHOW_POPUP_AFTER_GENERATION) Then
            ProgramController.UIController.DailyReportView.showSendReportsByEmailPanel(1)
        End If
    End Sub

    Public Sub goBackFromFinishingGenerationStep()

        ProgramController.UIController.ReportGenerationFrame.FinishingGenerationStepFinished = False

        ProgramController.UIController.ReportGenerationFrame.changeStep(ProgramController.UIController.CommentsStepView, 0)

        ProgramController.UIController.ReportGenerationFrame.CommentsStepFinished = False

        ProgramController.UIController.CommentsStepView.showReport( )
    End Sub

    Public Sub emailLastGeneratedReports()

        ProgramController.FileExportationController.FilesToExport.Clear()

        If (Not IsNothing(reportToSend)) Then

            ProgramController.FileExportationController.FilesToExport.Add(Me.reportToSend)

        End If
        ProgramController.UIController.changeView(ProgramController.UIController.EmailExportationView)
        
    End Sub

    Public Sub cancelGeneration()

        Me._generationCancelled = True

        '' Kill data files analysis threads
        'If (Not IsNothing(Me.csvAnalysisThread)) Then
        '    Me.csvAnalysisThread.Abort()
        'End If

        'If (Not IsNothing(Me.logAnalysisThread)) Then
        '    Me.logAnalysisThread.Abort()
        'End If

        'If (Not IsNothing(Me.mdbAnalysisThread)) Then
        '    Me.mdbAnalysisThread.Abort()
        'End If

        'If (Not IsNothing(Me.eventsAnalysisThread)) Then
        '    Me.eventsAnalysisThread.Abort()
        'End If

        'If (Not IsNothing(Me.manualDataSavingThread)) Then
        '    Me.manualDataSavingThread.Abort()
        'End If

        'If (Not IsNothing(Me.summaryDailyReportGenerationThread)) Then
        '    Me.summaryDailyReportGenerationThread.Abort()
        'End If

        'Me.selectedProductionDays = Nothing
        'Me.selectedReportReadyProductionDays.Clear()
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
