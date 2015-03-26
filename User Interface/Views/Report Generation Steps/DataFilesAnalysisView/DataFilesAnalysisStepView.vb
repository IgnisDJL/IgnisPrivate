Namespace UI

    Public Class DataFilesAnalysisStepView
        Inherits GenerationStepView

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "Analyse des données"

        ' Components | #refactor - make it dynamic (the labels and what not) - extract class to mustinherit and use it for this and finishing report generation view
        Private csvLabel As Label
        Private csvProgressBar As ProgressBar

        Private logLabel As Label
        Private logProgressBar As ProgressBar

        Private mdbLabel As Label
        Private mdbProgressBar As ProgressBar

        Private eventsLabel As Label
        Private eventsProgressBar As ProgressBar

        ' Attributes
        Private usineSettings As XmlSettings.UsineNode

        Private label1 As Label
        Private progressBar1 As ProgressBar
        Private label2 As Label
        Private progressBar2 As ProgressBar
        Private label3 As Label
        Private progressBar3 As ProgressBar

        ' #refactor - Controller should be the only one that knows this
        Private nbProductionDaysToAnalyse As Integer
        Private nbCSVFilesCompleted As Integer
        Private nbLOGFilesCompleted As Integer
        Private nbMDBFilesCompleted As Integer
        Private nbEventsFilesCompleted As Integer

        Public Sub New(usineSettings As XmlSettings.UsineNode)
            MyBase.New()

            Me.usineSettings = usineSettings

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.csvLabel = New Label
            Me.csvLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.csvLabel.AutoSize = False
            Me.csvLabel.Text = "Données discontinues (.csv)"

            Me.csvProgressBar = New ProgressBar
            Me.csvProgressBar.Style = ProgressBarStyle.Continuous


            Me.logLabel = New Label
            Me.logLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.logLabel.AutoSize = False
            Me.logLabel.Text = "Données continues (.log)"

            Me.logProgressBar = New ProgressBar
            Me.logProgressBar.Style = ProgressBarStyle.Continuous


            Me.mdbLabel = New Label
            Me.mdbLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.mdbLabel.AutoSize = False
            Me.mdbLabel.Text = "Production discontinue (.mdb)"

            Me.mdbProgressBar = New ProgressBar
            Me.mdbProgressBar.Style = ProgressBarStyle.Continuous


            Me.eventsLabel = New Label
            Me.eventsLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.eventsLabel.AutoSize = False
            Me.eventsLabel.Text = "Évènements (.log)"

            Me.eventsProgressBar = New ProgressBar
            Me.eventsProgressBar.Style = ProgressBarStyle.Continuous

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, DataFilesAnalysisStepLayout)

            If (Not IsNothing(Me.label1)) Then
                Me.label1.Location = layout.FirstLabel_Location
                Me.label1.Size = layout.FirstLabel_Size
                Me.progressBar1.Location = layout.FirstProgressBar_Location
                Me.progressBar1.Size = layout.FirstProgressBar_Size
            End If

            If (Not IsNothing(Me.label2)) Then
                Me.label2.Location = layout.SecondLabel_Location
                Me.label2.Size = layout.SecondLabel_Size
                Me.progressBar2.Location = layout.SecondProgressBar_Location
                Me.progressBar2.Size = layout.SecondProgressBar_Size
            End If

            If (Not IsNothing(Me.label3)) Then
                Me.label3.Location = layout.ThirdLabel_Location
                Me.label3.Size = layout.ThirdLabel_Size
                Me.progressBar3.Location = layout.ThirdProgressBar_Location
                Me.progressBar3.Size = layout.ThirdProgressBar_Size
            End If

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

        End Sub

        Private Sub newCSVFileAnalysisStartedEventsHandler(file As CSVFile)

            Console.WriteLine("New csv file being analysed : " & file.getFileInfo.Name & file.Date_.ToString(" (dd-MMM-yyyy)"))

        End Sub

        Private Sub newLOGFileAnalysisStartedEventHandler(file As LOGFile)

            Console.WriteLine("New log file being analysed : " & file.getFileInfo.Name & file.Date_.ToString(" (dd-MMM-yyyy)"))

        End Sub

        Private Sub newMDBFileAnalysisStartedEventHandler(file As MDBFile)

            Console.WriteLine("New mdb file being analysed : " & file.getFileInfo.Name & file.Date_.ToString(" (dd-MMM-yyyy)"))

        End Sub

        Private Sub newEventsFileAnalysisStartedEventHandler(file As EventsFile)

            Console.WriteLine("New event file being analysed : " & file.getFileInfo.Name & file.Date_.ToString(" (dd-MMM-yyyy)"))

        End Sub

        Private Sub csvFileProgressEventHandler(current As Integer, total As Integer)

            Dim value As Double = nbCSVFilesCompleted * (100 / nbProductionDaysToAnalyse) + (current / total) * (100 / nbProductionDaysToAnalyse)
            If (CInt(value) > Me.csvProgressBar.Value) Then
                Me.Invoke(Sub() setCSVProgressBarValue(value))
            End If

        End Sub

        Private Sub logFileProgressEventHandler(current As Integer, total As Integer)

            Dim value As Double = nbLOGFilesCompleted * (100 / nbProductionDaysToAnalyse) + (current / total) * (100 / nbProductionDaysToAnalyse)
            If (CInt(value) > Me.logProgressBar.Value) Then

                Me.Invoke(Sub() setLOGProgressBarValue(value))
            End If
        End Sub

        Private Sub mdbFileProgressEventHandler(current As Integer, total As Integer)

            Dim value As Double = nbMDBFilesCompleted * (100 / nbProductionDaysToAnalyse) + (current / total) * (100 / nbProductionDaysToAnalyse)
            If (CInt(value) > Me.mdbProgressBar.Value) Then

                Me.Invoke(Sub() setMDBProgressBarValue(value))
            End If
        End Sub

        Private Sub eventsFileProgressEventHandler(current As Integer, total As Integer)

            Dim value As Double = nbEventsFilesCompleted * (100 / nbProductionDaysToAnalyse) + (current / total) * (100 / nbProductionDaysToAnalyse)
            If (CInt(value) > Me.eventsProgressBar.Value) Then
                Me.Invoke(Sub() setEventsProgressBarValue(value))
            End If
        End Sub

        Private Sub csvFileAnalysisFinishedEventHandler(file As CSVFile)

            Me.nbCSVFilesCompleted += 1

            Dim value As Double = nbCSVFilesCompleted * 100 / nbProductionDaysToAnalyse

            Me.Invoke(Sub() setCSVProgressBarValue(value))

            RemoveHandler file.AnalysisStartedEvent, AddressOf newCSVFileAnalysisStartedEventsHandler
            RemoveHandler file.AnalysisProgress, AddressOf csvFileProgressEventHandler
            RemoveHandler file.AnalysisStopedEvent, AddressOf csvFileAnalysisFinishedEventHandler

        End Sub

        Private Sub logFileAnalysisFinishedEventHandler(file As LOGFile)

            Me.nbLOGFilesCompleted += 1

            Dim value As Double = nbLOGFilesCompleted * 100 / nbProductionDaysToAnalyse

            Me.Invoke(Sub() setLOGProgressBarValue(value))

            RemoveHandler file.AnalysisStartedEvent, AddressOf newLOGFileAnalysisStartedEventHandler
            RemoveHandler file.AnalysisProgress, AddressOf logFileProgressEventHandler
            RemoveHandler file.AnalysisStopedEvent, AddressOf logFileAnalysisFinishedEventHandler

        End Sub

        Private Sub mdbFileAnalysisFinishedEventHandler(file As MDBFile)

            Me.nbMDBFilesCompleted += 1

            Dim value As Double = nbMDBFilesCompleted * 100 / nbProductionDaysToAnalyse

            Me.Invoke(Sub() setMDBProgressBarValue(value))

            RemoveHandler file.AnalysisStartedEvent, AddressOf newMDBFileAnalysisStartedEventHandler
            RemoveHandler file.AnalysisProgress, AddressOf mdbFileProgressEventHandler
            RemoveHandler file.AnalysisStopedEvent, AddressOf mdbFileAnalysisFinishedEventHandler

        End Sub

        Private Sub eventsFileAnalysisFinishedEventHandler(file As EventsFile)

            Me.nbEventsFilesCompleted += 1

            Dim value As Double = nbEventsFilesCompleted * 100 / nbProductionDaysToAnalyse
            Me.Invoke(Sub() setEventsProgressBarValue(value))

            RemoveHandler file.AnalysisStartedEvent, AddressOf newEventsFileAnalysisStartedEventHandler
            RemoveHandler file.AnalysisProgress, AddressOf eventsFileProgressEventHandler
            RemoveHandler file.AnalysisStopedEvent, AddressOf eventsFileAnalysisFinishedEventHandler

        End Sub

        Public WriteOnly Property ProductionDays As List(Of ProductionDay_1)
            Set(days As List(Of ProductionDay_1))

                Me.reset()

                Dim csvIsActive As Boolean = Me.Controls.Contains(Me.csvLabel)
                Dim logIsActive As Boolean = Me.Controls.Contains(Me.logLabel)
                Dim mdbIsActive As Boolean = Me.Controls.Contains(Me.mdbLabel)
                Dim eventsIsActive As Boolean = Me.Controls.Contains(Me.eventsLabel)

                For Each productionDay As ProductionDay_1 In days

                    nbProductionDaysToAnalyse += 1

                    'Dim datafilesInfo As DataFilesInformation = productionDay.DataFilesInfo

                    'If (datafilesInfo.HasCSVFile AndAlso csvIsActive) Then

                    '    AddHandler datafilesInfo.CSVFile.AnalysisStartedEvent, AddressOf newCSVFileAnalysisStartedEventsHandler
                    '    AddHandler datafilesInfo.CSVFile.AnalysisProgress, AddressOf csvFileProgressEventHandler
                    '    AddHandler datafilesInfo.CSVFile.AnalysisStopedEvent, AddressOf csvFileAnalysisFinishedEventHandler

                    'End If

                    'If (datafilesInfo.HasLOGFile AndAlso logIsActive) Then

                    '    AddHandler datafilesInfo.LOGFile.AnalysisStartedEvent, AddressOf newLOGFileAnalysisStartedEventHandler
                    '    AddHandler datafilesInfo.LOGFile.AnalysisProgress, AddressOf logFileProgressEventHandler
                    '    AddHandler datafilesInfo.LOGFile.AnalysisStopedEvent, AddressOf logFileAnalysisFinishedEventHandler

                    'End If

                    'If (datafilesInfo.HasMDBFile AndAlso mdbIsActive) Then

                    '    AddHandler datafilesInfo.MDBFile.AnalysisStartedEvent, AddressOf newMDBFileAnalysisStartedEventHandler
                    '    AddHandler datafilesInfo.MDBFile.AnalysisProgress, AddressOf mdbFileProgressEventHandler
                    '    AddHandler datafilesInfo.MDBFile.AnalysisStopedEvent, AddressOf mdbFileAnalysisFinishedEventHandler

                    'End If

                    'If (datafilesInfo.HasEventsFile AndAlso eventsIsActive) Then

                    '    AddHandler datafilesInfo.EventsFile.AnalysisStartedEvent, AddressOf newEventsFileAnalysisStartedEventHandler
                    '    AddHandler datafilesInfo.EventsFile.AnalysisProgress, AddressOf eventsFileProgressEventHandler
                    '    AddHandler datafilesInfo.EventsFile.AnalysisStopedEvent, AddressOf eventsFileAnalysisFinishedEventHandler

                    'End If

                Next

            End Set
        End Property

        Protected Overloads Overrides Sub beforeShow()


        End Sub

        Public Overrides Sub afterShow()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub reset()

            Dim nbProgressBars As Integer = 0

            Me.Controls.Remove(Me.csvLabel)
            Me.Controls.Remove(Me.csvProgressBar)
            Me.Controls.Remove(Me.logLabel)
            Me.Controls.Remove(Me.logProgressBar)
            Me.Controls.Remove(Me.mdbLabel)
            Me.Controls.Remove(Me.mdbProgressBar)
            Me.Controls.Remove(Me.eventsLabel)
            Me.Controls.Remove(Me.eventsProgressBar)

            Me.label1 = Nothing
            Me.label2 = Nothing
            Me.label3 = Nothing

            Me.progressBar1 = Nothing
            Me.progressBar2 = Nothing
            Me.progressBar3 = Nothing

            Select Case Me.usineSettings.TYPE

                Case Constants.Settings.UsineType.HYBRID

                    Me.Controls.Add(Me.csvLabel)
                    Me.Controls.Add(Me.csvProgressBar)
                    Me.label1 = Me.csvLabel
                    Me.progressBar1 = Me.csvProgressBar

                    Me.Controls.Add(Me.logLabel)
                    Me.Controls.Add(Me.logProgressBar)
                    Me.label2 = Me.logLabel
                    Me.progressBar2 = Me.logProgressBar

                    nbProgressBars = 2

                    If (Me.usineSettings.Events.ACTIVE) Then

                        Me.Controls.Add(Me.eventsLabel)
                        Me.Controls.Add(Me.eventsProgressBar)
                        Me.label3 = Me.eventsLabel
                        Me.progressBar3 = Me.eventsProgressBar

                        nbProgressBars += 1
                    End If

                Case Constants.Settings.UsineType.CSV

                    Me.Controls.Add(Me.csvLabel)
                    Me.Controls.Add(Me.csvProgressBar)
                    Me.label1 = Me.csvLabel
                    Me.progressBar1 = Me.csvProgressBar

                    nbProgressBars = 1

                Case Constants.Settings.UsineType.LOG
                    Me.Controls.Add(Me.logLabel)
                    Me.Controls.Add(Me.logProgressBar)
                    Me.label1 = Me.logLabel
                    Me.progressBar1 = Me.logProgressBar

                    nbProgressBars = 1

                    If (Me.usineSettings.Events.ACTIVE) Then

                        Me.Controls.Add(Me.eventsLabel)
                        Me.Controls.Add(Me.eventsProgressBar)
                        Me.label2 = Me.eventsLabel
                        Me.progressBar2 = Me.eventsProgressBar

                        nbProgressBars += 1
                    End If

                Case Constants.Settings.UsineType.MDB

                    Me.Controls.Add(Me.mdbLabel)
                    Me.Controls.Add(Me.mdbProgressBar)
                    Me.label1 = Me.mdbLabel
                    Me.progressBar1 = Me.mdbProgressBar

                    nbProgressBars = 1
            End Select

            ' Initialize layout with nb of progress bars
            Me.layout = New DataFilesAnalysisStepLayout(nbProgressBars)

            Me.nbProductionDaysToAnalyse = 0
            Me.nbCSVFilesCompleted = 0
            Me.nbLOGFilesCompleted = 0
            Me.nbMDBFilesCompleted = 0
            Me.nbEventsFilesCompleted = 0

            Me.csvProgressBar.Value = 0
            Me.logProgressBar.Value = 0
            Me.mdbProgressBar.Value = 0
            Me.eventsProgressBar.Value = 0

        End Sub

        Private Sub setCSVProgressBarValue(value As Integer)

            Me.csvProgressBar.Value = value

            raiseProgressEvent(MinProgressBarValue)
        End Sub

        Private Sub setLOGProgressBarValue(value As Integer)
            Me.logProgressBar.Value = value

            raiseProgressEvent(MinProgressBarValue)
        End Sub

        Private Sub setMDBProgressBarValue(value As Integer)
            Me.mdbProgressBar.Value = value

            raiseProgressEvent(MinProgressBarValue)
        End Sub

        Private Sub setEventsProgressBarValue(value As Integer)
            Me.eventsProgressBar.Value = value

            raiseProgressEvent(MinProgressBarValue)
        End Sub

        Protected Overrides Sub goBack()
            ProgramController.ReportGenerationController.cancelGeneration()
        End Sub

        Protected Overrides Sub cancel()
            ProgramController.ReportGenerationController.cancelGeneration()
        End Sub

        Private ReadOnly Property MinProgressBarValue As Integer
            Get
                Dim minVal As Integer = 100

                If (Not IsNothing(Me.csvProgressBar)) Then
                    minVal = Math.Min(minVal, Me.csvProgressBar.Value)
                End If

                If (Not IsNothing(Me.logProgressBar)) Then
                    minVal = Math.Min(minVal, Me.logProgressBar.Value)
                End If

                If (Not IsNothing(Me.mdbProgressBar)) Then
                    minVal = Math.Min(minVal, Me.mdbProgressBar.Value)
                End If

                If (Not IsNothing(Me.eventsProgressBar)) Then
                    minVal = Math.Min(minVal, Me.eventsProgressBar.Value)
                End If

                Return minVal
            End Get
        End Property

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Public Overrides ReadOnly Property OverallProgressValue As Integer
            Get
                Return 10
            End Get
        End Property
    End Class
End Namespace
