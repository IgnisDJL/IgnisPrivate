﻿
Public Class CSVDay
    Inherits ProductionDay

    Private cycleList As New List(Of CSVCycle)

    Public Sub New(date_ As Date, cycleList As List(Of CSVCycle))
        MyBase.New(date_)

        Me.cycleList = cycleList

        Me.currentDataFile = XmlSettings.Settings.instance.Usine.DataFiles.CSV

    End Sub

    Public Overrides Sub gatherData()

        Me._date = cycleList.First.DATE_

        Me.startTime = cycleList.First.TIME
        Me.endTime = cycleList.Last.TIME

        Dim currentMixStats As MixStatistics = Nothing
        Dim currentACStats As AsphaltStatistics = Nothing

        Dim isNewMix As Boolean = True
        Dim isNewAC As Boolean = True

        Events.addStartEvent(Me.START_TIME)

        For Each cycle In cycleList

            If (IsNothing(currentMixStats) OrElse Not cycle.FORMULA_NAME.Equals(currentMixStats.FORMULA_NAME) OrElse Not cycle.ASPHALT_NAME.Equals(currentMixStats.ASPHALT_STATS.NAME)) Then

                isNewMix = True
                isNewAC = True

                For Each mix In Me.MIX_STATS

                    If (cycle.FORMULA_NAME.Equals(mix.FORMULA_NAME) And cycle.ASPHALT_NAME.Equals(mix.ASPHALT_STATS.NAME)) Then

                        currentMixStats = mix
                        isNewMix = False

                        Exit For
                    End If

                Next

                For Each asphalt In Me.ASPHALT_STATS

                    If (asphalt.NAME.Equals(cycle.ASPHALT_NAME)) Then

                        currentACStats = asphalt
                        isNewAC = False

                        Exit For
                    End If

                Next

                If (isNewMix) Then

                    currentMixStats = New MixStatistics()

                    With currentMixStats

                        .NAME = cycle.MIX_NAME
                        .PRODUCTION_TYPE = XmlSettings.Settings.LANGUAGE.General.WordFor_Batch
                        .FORMULA_NAME = cycle.FORMULA_NAME

                        With .ASPHALT_STATS

                            .NAME = cycle.ASPHALT_NAME
                            .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                        End With

                    End With

                    Me.MIX_STATS.Add(currentMixStats)

                End If

                If (isNewAC) Then

                    currentACStats = New AsphaltStatistics()

                    With currentACStats

                        .TANK = cycle.ASPHALT_TANK
                        .NAME = cycle.ASPHALT_NAME
                        .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                    End With

                    Me.ASPHALT_STATS.Add(currentACStats)

                End If

            End If

            For Each feed In cycle.HOT_FEEDS

                Dim addToStats = True

                For Each feedStats In currentMixStats.BATCH_FEEDERS_STATS

                    If (feed.INDEX.Equals(feedStats.INDEX)) Then

                        feedStats.TOTAL_MASS += currentDataFile.getUnitByTag(CSVCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)
                        addToStats = False

                        Exit For
                    End If

                Next

                If (addToStats) Then
                    currentMixStats.BATCH_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                            With {.INDEX = feed.INDEX, _
                                                                  .LOCATION = feed.LOCATION, _
                                                                  .MATERIAL_NAME = feed.MATERIAL_NAME, _
                                                                  .TOTAL_MASS = currentDataFile.getUnitByTag(CSVCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)})
                End If

            Next

            currentMixStats.addCycle(cycle, Me.currentDataFile)
            currentACStats.addCycle(cycle, Me.currentDataFile)

            Me.batchProduction_duration = Me.batchProduction_duration.Add(cycle.DURATION)
            Me.batchProduction_totalMass += Me.currentDataFile.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

            ' events
            If (Not IsNothing(cycle.PREVIOUS_CYCLE)) Then

                Dim timeDiff = cycle.TIME.Subtract(cycle.PREVIOUS_CYCLE.TIME)

                If (timeDiff.Subtract(cycle.DURATION).CompareTo(TimeSpan.FromSeconds(XmlSettings.Settings.instance.Usine.DataFiles.CSV.STOP_OFFSET)) > 0) Then

                    Events.addStopEvent(cycle.PREVIOUS_CYCLE.TIME.Add(cycle.DURATION))
                    Events.addStartEvent(cycle.TIME)

                End If

                If (cycle.FORMULA_NAME.Equals(cycle.PREVIOUS_CYCLE.FORMULA_NAME)) Then

                    For Each feed In cycle.HOT_FEEDS

                        For Each previousFeed In cycle.PREVIOUS_CYCLE.HOT_FEEDS

                            If (feed.INDEX.Equals(previousFeed.INDEX) AndAlso Not feed.SET_POINT_PERCENTAGE.Equals(previousFeed.SET_POINT_PERCENTAGE)) Then

                                ' Need to adapt this beacuse of material name...
                                Events.addMixRecipeChangeEvent(cycle.TIME, "Changement au " & feed.MATERIAL_NAME & ": " & previousFeed.SET_POINT_PERCENTAGE & "% à " & feed.SET_POINT_PERCENTAGE & "%")

                            End If

                        Next

                    Next

                Else ' Formula change

                    Events.addMixChangeEvent(cycle.TIME, "Changement de formule : " & cycle.PREVIOUS_CYCLE.FORMULA_NAME & " à " & cycle.FORMULA_NAME)

                    Me.batchProduction_nbMixSwitch += 1

                End If

            End If

        Next

        ' Last stop
        Events.addStopEvent(Me.END_TIME)

        Me.MIX_STATS.Sort()
        Me.ASPHALT_STATS.Sort()

        For i = 0 To Me.cycleList.Last.HOT_FEEDS.Count - 1

            ' Add new feeder to total batch feeders list - with basic information
            Me.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                       With {.LOCATION = Me.cycleList.Last.HOT_FEEDS(i).LOCATION, _
                                                             .MATERIAL_NAME = Me.cycleList.Last.HOT_FEEDS(i).MATERIAL_NAME, _
                                                             .INDEX = Me.cycleList.Last.HOT_FEEDS(i).INDEX})
        Next

        For i = 0 To Me.MIX_STATS.Count - 1

            With Me.MIX_STATS(i)

                If (i > 2) Then

                    Me.OTHER_MIX_STATS.CYCLE_MASS = .TOTAL_MASS
                    Me.OTHER_MIX_STATS.CYCLE_TIME = .TOTAL_TIME
                    Me.OTHER_MIX_STATS.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE

                    'Try to remove if
                    If (Not Double.IsNaN(.TOTAL_RECYCLED_MASS)) Then
                        Me.OTHER_MIX_STATS.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS
                    End If

                End If

                For j = 0 To .BATCH_FEEDERS_STATS.Count - 1
                    Me.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS(j).TOTAL_MASS += .BATCH_FEEDERS_STATS(j).TOTAL_MASS
                Next

                Me.TOTAL_MIX_STATS.CYCLE_MASS = .TOTAL_MASS
                Me.TOTAL_MIX_STATS.CYCLE_TIME = .TOTAL_TIME
                Me.TOTAL_MIX_STATS.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE

                If (Not Double.IsNaN(.TOTAL_RECYCLED_MASS)) Then
                    Me.TOTAL_MIX_STATS.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS
                End If

                Me.TOTAL_MIX_STATS.ASPHALT_STATS.CYCLE_ASPHALT_MASS = .ASPHALT_STATS.TOTAL_MASS

            End With

        Next

    End Sub ' End GatherData()

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' ProgressBar = 10%
    ''' </remarks>
    Public Overrides Sub generateGraphics()

        ' Progress Bar
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération des graphiques"
        ReportGenerationControl.instance.addStep(2)

        Threading.Thread.CurrentThread.CurrentCulture = XmlSettings.Settings.LANGUAGE.Culture

        Graphic.pointFormatList_asphalt = New PointFormatList
        Graphic.pointFormatList_mix = New PointFormatList

        Dim accumulatedMass As New AccumulatedMassGraphic()
        Dim asphaltPercentage As New AsphaltPercentageGraphic()
        Dim mixTemperature As New MixTemperatureGraphic()
        Dim productionSpeed As New ProductionSpeedGraphic()
        Dim mixTemperatureVariation As New MixTemperatureVariationGraphic()
        Dim asphaltPercentageVariation As New AsphaltPercentageVariationGraphic()
        Dim recycledPercentage As New RecycledPercentageGraphic()
        Dim fuelConsumption As New FuelConsumptionGraphic

        For Each cycle In Me.cycleList

            accumulatedMass.addCycle(cycle, Me.currentDataFile)
            productionSpeed.addCycle(cycle, Me.currentDataFile)

        Next

        ' Progress Bar
        ReportGenerationControl.instance.addStep(2)

        For i = 0 To MIX_STATS.Count - 1
            For Each cycle In MIX_STATS(i).CYCLES
                asphaltPercentage.addCycle(cycle, Me.currentDataFile)
                asphaltPercentageVariation.addCycle(cycle, Me.currentDataFile)
                recycledPercentage.addCycle(cycle, Me.currentDataFile)
            Next
        Next

        ' Progress Bar
        ReportGenerationControl.instance.addStep(2)

        For i = 0 To ASPHALT_STATS.Count - 1
            For Each cycle In ASPHALT_STATS(i).CYCLES
                mixTemperature.addCycle(cycle, Me.currentDataFile)
                mixTemperatureVariation.addCycle(cycle, Me.currentDataFile)
            Next
        Next

        ' Progress Bar
        ReportGenerationControl.instance.addStep(2)

        accumulatedMass.save()
        asphaltPercentage.save()
        mixTemperature.save()
        productionSpeed.save()
        mixTemperatureVariation.save()
        asphaltPercentageVariation.save()
        recycledPercentage.save()
        fuelConsumption.save()

        ' Progress Bar
        ReportGenerationControl.instance.addStep(2)

        Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.CreateSpecificCulture("en-US")

    End Sub


    Public Overrides Sub generateReports()

        Dim xls As CSVReport = Nothing
        Dim docx As DOCXReport = Nothing

        ProductionDay.generateModel = XmlSettings.Settings.instance.wasUpdated

        Try

            Me.gatherData()

            Me.generateGraphics()

            ' Progress Bar
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (ouverture de excel)"

            xls = New CSVReport(Me.cycleList)
            xls.loadData()
            xls.loadGraphics()

            Dim savePath = Constants.Paths.EXTENDED_REPORTS_ARCHIVES_DIRECTORY & XmlSettings.Settings.LANGUAGE.ExcelReport.FileName & " (" & Me.DATE_.ToString("yyyy-MM-dd") & ").xlsx"
            xls.saveAs(savePath)

            If (XmlSettings.Settings.instance.Report.Excel.OPEN_WHEN_DONE) Then
                XLSReport.ExcelApp.Visible = True
            Else
                xls.dispose()
                xls = Nothing
            End If

            ' Progress Bar
            ReportGenerationControl.instance.addStep(10)
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport word (ouverture de word)"

            docx = New DOCXReport(Me)
            docx.generateReport()
            docx.saveAs(Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & XmlSettings.Settings.LANGUAGE.WordReport.FileName & " (" & Me.DATE_.ToString("yyyy-MM-dd") & ").docx")
            docx.saveAsPDF(Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & XmlSettings.Settings.LANGUAGE.WordReport.FileName & " (" & Me.DATE_.ToString("yyyy-MM-dd") & ").pdf")

            ' Progress Bar
            ReportGenerationControl.instance.addStep(15)

            docx.dispose()
            docx = Nothing

            ProductionDay.generateModel = False

        Catch ex As Threading.ThreadAbortException

            If (Not IsNothing(xls)) Then
                xls.dispose()
            End If

            If (Not IsNothing(docx)) Then
                docx.dispose()
            End If

        End Try

    End Sub

End Class
