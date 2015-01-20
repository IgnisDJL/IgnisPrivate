Public Class LOGDay
    Inherits ProductionDay

    Private cycleList As New List(Of LOGCycle)

    Public Sub New(date_ As Date, cycleList As List(Of LOGCycle))
        MyBase.New(date_)

        Me.cycleList = cycleList

        Me.currentDataFile = XmlSettings.Settings.instance.Usine.DataFiles.LOG

    End Sub

    Public Overrides Sub gatherData()

        Me._date = cycleList.First.DATE_

        Me.startTime = cycleList.First.TIME
        Me.endTime = cycleList.Last.TIME

        Me.batchProduction_duration = TimeSpan.Zero

        Dim currentMixStats As MixStatistics = Nothing
        Dim currentACStats As AsphaltStatistics = Nothing

        Dim isNewMix As Boolean = True
        Dim isNewAC As Boolean = True

        ' For each LOG cycles
        For Each cycle In cycleList

            ' If the cycle's mix or asphalt has been analysed before
            If (IsNothing(currentMixStats) OrElse Not cycle.FORMULA_NAME.Equals(currentMixStats.FORMULA_NAME) OrElse Not cycle.ASPHALT_NAME.Equals(currentMixStats.ASPHALT_STATS.NAME)) Then

                isNewMix = True     ' By default
                isNewAC = True      ' By default

                ' If the cycle's mix is the same than a previously analysed mix
                For Each mix In Me.MIX_STATS

                    If (cycle.FORMULA_NAME.Equals(mix.FORMULA_NAME) And cycle.ASPHALT_NAME.Equals(mix.ASPHALT_STATS.NAME)) Then

                        currentMixStats = mix   ' Set the current mix

                        isNewMix = False
                        Exit For ' No need to loop more

                    End If

                Next

                ' If the cycle's asphalt is the same than a previously analysed asphalt
                For Each asphalt In Me.ASPHALT_STATS

                    If (asphalt.NAME.Equals(cycle.ASPHALT_NAME)) Then

                        currentACStats = asphalt    ' Set the current asphalt

                        isNewAC = False
                        Exit For ' No need to loop more

                    End If

                Next

                ' If the cycle's mix hasn't been analysed before
                If (isNewMix) Then

                    currentMixStats = New MixStatistics() ' Set the current mix

                    ' Set the mix's basic information
                    With currentMixStats

                        .NAME = cycle.MIX_NAME
                        .PRODUCTION_TYPE = XmlSettings.Settings.LANGUAGE.General.WordFor_Continuous
                        .FORMULA_NAME = cycle.FORMULA_NAME

                        With .ASPHALT_STATS

                            .NAME = cycle.ASPHALT_NAME
                            .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                        End With

                    End With

                    ' Add the mix to the list of mixes
                    Me.MIX_STATS.Add(currentMixStats)


                End If ' End if new mix

                ' If the cycle's asphalt hasn't been analysed 
                If (isNewAC) Then

                    currentACStats = New AsphaltStatistics() ' Set the current asphalt

                    ' Set the asphalt's basic information
                    With currentACStats

                        .NAME = cycle.ASPHALT_NAME
                        .TANK = cycle.ASPHALT_TANK
                        .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                    End With

                    ' Add the new asphalt to the asphalts list
                    Me.ASPHALT_STATS.Add(currentACStats)

                End If ' End if new asphalt

            End If ' End if different mix or asphalt


            ' --------------------LOG COLD FEEDS -------------------- '

            For Each feed In cycle.COLD_FEEDS

                Dim addToStats = True       ' Wheter or not the feeder is unknown

                ' Set the feed's mass WITHOUT THE MOISTURE'S MASS
                Dim feedMass = feed.MASS - feed.MASS * currentDataFile.getUnitByTag(LOGFeeder.MOISTURE_PERCENTAGE_TAG).convert(DirectCast(feed, LOGFeeder).MOISTURE_PERCENTAGE, PerOne.UNIT)

                ' For each continuous feeder
                For Each feedStats In currentMixStats.CONTINUOUS_FEEDERS_STATS

                    ' If the feed is known
                    If (feed.INDEX.Equals(feedStats.INDEX)) Then

                        ' Increment the feeds total mass
                        feedStats.TOTAL_MASS += currentDataFile.getUnitByTag(LOGFeeder.MASS_TAG).convert(feedMass, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

                        addToStats = False
                        Exit For ' No need to loop more

                    End If

                Next

                ' If the feed is unknown
                If (addToStats) Then

                    ' Add to continuous feeders list - with basic info
                    currentMixStats.CONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                                 With {.INDEX = feed.INDEX, _
                                                                      .LOCATION = feed.LOCATION, _
                                                                      .MATERIAL_NAME = feed.MATERIAL_NAME, _
                                                                      .TOTAL_MASS = currentDataFile.getUnitByTag(LOGFeeder.MASS_TAG).convert(feedMass, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)})
                End If

            Next ' End for each cold feed

            ' Add the cycle's information to the statistics
            currentMixStats.addCycle(cycle, Me.currentDataFile)
            currentACStats.addCycle(cycle, Me.currentDataFile)

            ' If the cycle is not a "dry" cycle
            If (cycle.PRODUCTION_SPEED > 0) Then

                ' Increment the duration and mass of the batch production
                Me.continuousProduction_duration = Me.continuousProduction_duration.Add(cycle.DURATION)
                Me.continuousProduction_totalMass += Me.currentDataFile.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

            End If


            ' -------------------- LOG EVENTS -------------------- '

            ' If the cycle is not the first cycle of the day
            If (Not IsNothing(cycle.PREVIOUS_CYCLE)) Then

                ' If there was NO mix change.
                If (cycle.FORMULA_NAME.Equals(cycle.PREVIOUS_CYCLE.FORMULA_NAME)) Then

                    ' Check if the cold feeds Set Point Percentage, Moisture or Asphalt Set Point Percentage has changed
                    For i = 0 To cycle.COLD_FEEDS.Count - 1

                        If (Not cycle.COLD_FEEDS(i).SET_POINT_PERCENTAGE.Equals(cycle.PREVIOUS_CYCLE.COLD_FEEDS(i).SET_POINT_PERCENTAGE)) Then

                            ' Make new Mix Recipe Change event
                            Events.addMixRecipeChangeEvent(cycle.TIME, "Changement au " & cycle.COLD_FEEDS(i).LOCATION & ": " & cycle.PREVIOUS_CYCLE.COLD_FEEDS(i).SET_POINT_PERCENTAGE & "% à " & cycle.COLD_FEEDS(i).SET_POINT_PERCENTAGE & "%")

                        End If

                        If (Not DirectCast(cycle.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE.Equals(DirectCast(cycle.PREVIOUS_CYCLE.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE)) Then

                            ' Make new Mix Recipe Change event
                            Events.addMixRecipeChangeEvent(cycle.TIME, "Changement de % HUM au " & cycle.COLD_FEEDS(i).LOCATION & " : " & DirectCast(cycle.PREVIOUS_CYCLE.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE & "% à " & DirectCast(cycle.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE & "%")

                        End If

                        If (Not cycle.ASPHALT_SET_POINT_PERCENTAGE.Equals(cycle.PREVIOUS_CYCLE.ASPHALT_SET_POINT_PERCENTAGE)) Then

                            ' Make new Mix Recipe Change event
                            Events.addMixRecipeChangeEvent(cycle.TIME, "Changement de % bitume : " & cycle.PREVIOUS_CYCLE.ASPHALT_SET_POINT_PERCENTAGE & "% à " & cycle.ASPHALT_SET_POINT_PERCENTAGE & "%")

                        End If

                    Next

                Else ' If there was a mix change

                    ' Make new Mix Change event
                    Events.addMixChangeEvent(cycle.TIME, "Changement de formule : " & cycle.PREVIOUS_CYCLE.FORMULA_NAME & " à " & cycle.FORMULA_NAME)

                    Me.continuousProduction_nbMixSwitch += 1

                End If

            End If ' End if is not first cycle

        Next

        Me.MIX_STATS.Sort()
        Me.ASPHALT_STATS.Sort()

        ' For each feeder of the last continuous cycle
        For i = 0 To cycleList.Last.COLD_FEEDS.Count - 1

            ' Add new feeder to total continuous feeders list - with basic information
            Me.TOTAL_MIX_STATS.CONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                            With {.LOCATION = Me.cycleList.Last.COLD_FEEDS(i).LOCATION, _
                                                                  .MATERIAL_NAME = Me.cycleList.Last.COLD_FEEDS(i).MATERIAL_NAME, _
                                                                  .INDEX = Me.cycleList.Last.COLD_FEEDS(i).INDEX})
        Next

        For i = 0 To Me.MIX_STATS.Count - 1

            With Me.MIX_STATS(i)

                If (i > 2) Then

                    Me.OTHER_MIX_STATS.CYCLE_MASS = .TOTAL_MASS
                    Me.OTHER_MIX_STATS.CYCLE_TIME = .TOTAL_TIME
                    Me.OTHER_MIX_STATS.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE
                    Me.OTHER_MIX_STATS.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS

                End If

                For j = 0 To .CONTINUOUS_FEEDERS_STATS.Count - 1

                    If (.TOTAL_MASS > 0) Then

                        ' Increment the feeder's total mass
                        Me.TOTAL_MIX_STATS.CONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS += .CONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS

                    End If

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

    End Sub

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
        Dim asphaltPercentag As New AsphaltPercentageGraphic()
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

            If (MIX_STATS(i).TOTAL_MASS > 0) Then

                For Each cycle In MIX_STATS(i).CYCLES
                    asphaltPercentag.addCycle(cycle, Me.currentDataFile)
                    asphaltPercentageVariation.addCycle(cycle, Me.currentDataFile)
                    recycledPercentage.addCycle(cycle, Me.currentDataFile)
                Next

            End If

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
        asphaltPercentag.save()
        mixTemperature.save()
        productionSpeed.save()
        mixTemperatureVariation.save()
        asphaltPercentageVariation.save()
        recycledPercentage.save()
        fuelConsumption.save()

        Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.CreateSpecificCulture("en-US")

        ' Progress Bar
        ReportGenerationControl.instance.addStep(2)

    End Sub


    Public Overrides Sub generateReports()

        Dim xls As LOGReport = Nothing
        Dim docx As DOCXReport = Nothing

        ProductionDay.generateModel = XmlSettings.Settings.instance.wasUpdated

        Try
            Me.gatherData()

            Me.generateGraphics()

            ' Progress Bar
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport excel (Ouverture de excel)"

            xls = New LOGReport(Me.cycleList)
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
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport word (Ouverture de word)"

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
