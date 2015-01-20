Public Class PersistenceController

    Private dataFilesPersistence As DataFilesPersistence
    Private reportsPersistence As ReportsPersistence
    Private manualDataPersistence As ManualDataPersistence


    Public Sub New(dataFilesPersistence As DataFilesPersistence, reportsPersistence As ReportsPersistence, manualDataPersistence As ManualDataPersistence, settings As XmlSettings.Settings)

        Me.dataFilesPersistence = dataFilesPersistence
        Me.reportsPersistence = reportsPersistence
        Me.manualDataPersistence = manualDataPersistence

    End Sub

    Public Function selectProductionDays(startDate As Date, endDate As Date) As List(Of ProductionDay)

        Dim days As New List(Of ProductionDay)

        Dim _date As Date

        For nbDaysToSubstract = 0 To endDate.Subtract(startDate).Days

            _date = endDate.Subtract(TimeSpan.FromDays(nbDaysToSubstract))

            Dim productionDay As New ProductionDay(_date)

            productionDay.DataFilesInfo = New DataFilesInformation(dataFilesPersistence.getCSVFile(_date), _
                                                                   dataFilesPersistence.getLOGFile(_date), _
                                                                   dataFilesPersistence.getMDBFile(), _
                                                                   dataFilesPersistence.getEventsFile(_date))

            For Each report As ReportFile In reportsPersistence.getDailyReports(_date)
                productionDay.ReportFilesInfo.addReport(report)
            Next

            days.Add(productionDay)

        Next

        Return days
    End Function

    ''' <summary>
    ''' Return's the date of the last report ready day
    ''' </summary>
    ''' <returns>The date of the last report ready day or Date.maxValue if no report ready day was found</returns>
    Public Function findLastReportReadyDate() As Date

        Dim lastReportReadyDate = Date.MaxValue

        Select Case ProgramController.SettingsControllers.DataFilesSettingsController.UsineType

            Case Constants.Settings.UsineType.HYBRID

                Dim eventsActive As Boolean = ProgramController.SettingsControllers.EventsSettingsController.EventsEnabled

                For Each _dataFiles As DataFile() In dataFilesPersistence.getAllCSVLOGAndEventsFiles

                    If (Not IsNothing(_dataFiles(0)) AndAlso Not IsNothing(_dataFiles(1)) AndAlso _
                       (Not eventsActive OrElse Not IsNothing(_dataFiles(2)))) Then

                        lastReportReadyDate = _dataFiles(0).Date_

                        Exit For
                    End If
                Next

            Case Constants.Settings.UsineType.CSV

                Dim csvFiles = Me.dataFilesPersistence.getAllCSVFiles

                If (csvFiles.Count > 0) Then
                    Return csvFiles.First.Date_
                End If

            Case Constants.Settings.UsineType.LOG

                Dim logFiles = Me.dataFilesPersistence.getAllLOGFiles

                If (ProgramController.SettingsControllers.EventsSettingsController.EventsEnabled) Then

                    Dim evtFiles = Me.dataFilesPersistence.getAllEventsFiles

                    For Each _logFile As LOGFile In logFiles

                        For Each _eventFile As EventsFile In evtFiles

                            If (_logFile.Date_.CompareTo(_eventFile.Date_) = 0) Then
                                Return _logFile.Date_

                            ElseIf (_logFile.Date_.CompareTo(_eventFile.Date_) > 0) Then

                                Exit For
                            End If

                        Next
                    Next

                Else
                    If (logFiles.Count > 0) Then
                        Return logFiles.First.Date_
                    End If
                End If



            Case Constants.Settings.UsineType.MDB

                Dim mdbFile As MDBFile = Me.dataFilesPersistence.getMDBFile()

                If (Not IsNothing(mdbFile)) Then

                    lastReportReadyDate = mdbFile.getLastCycleDate

                End If

            Case Else
                Throw New NotImplementedException

        End Select

        Return lastReportReadyDate
    End Function

End Class
