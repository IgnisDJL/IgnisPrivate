Imports IGNIS.XmlSettings

Public Class HybridDay
    Inherits ProductionDay

    Private CSVCycleList As New List(Of CSVCycle)
    Private LOGCycleList As New List(Of LOGCycle)


    Public Sub New(date_ As Date, CSVCycleList As List(Of CSVCycle), LOGCycleList As List(Of LOGCycle))
        MyBase.New(date_)

        Me.CSVCycleList = CSVCycleList
        Me.LOGCycleList = LOGCycleList

    End Sub

    Public Overrides Sub gatherData()

        ' ------------------------------------------------------------           ------------------------------------------------------------ '
        ' ------------------------------------------------------------CSV CYCLES ------------------------------------------------------------ '
        ' ------------------------------------------------------------           ------------------------------------------------------------ '

        ' Set the correct datafile xml node from the settings
        Me.currentDataFile = XmlSettings.Settings.instance.Usine.DataFiles.CSV

        ' Set the production day's date  !!!! Try using usine.current date
        Me._date = CSVCycleList.First.DATE_

        ' Set the production's start time and end time
        If (CSVCycleList.First.TIME.CompareTo(Events.START_EVENTS.First.TIME) < 0) Then
            Me.startTime = CSVCycleList.First.TIME
        Else
            Me.startTime = Events.START_EVENTS.First.TIME
        End If

        If (CSVCycleList.Last.TIME.CompareTo(Events.STOP_EVENTS.Last.TIME) > 0) Then
            Me.endTime = CSVCycleList.Last.TIME
        Else
            Me.endTime = Events.STOP_EVENTS.Last.TIME
        End If


        Dim currentMixStats As MixStatistics = Nothing      ' The statistics on the mix that is currently analysed
        Dim currentACStats As AsphaltStatistics = Nothing   ' The statistics on the asphalt that is currently analysed

        Dim isNewMix As Boolean = True                      ' Represents wether or not the cycle's mix has been analysed that day
        Dim isNewAC As Boolean = True                       ' Represents wether or not the cycle's asphalt has been analysed that day

        ' For each CSV cycles
        For Each cycle In CSVCycleList

            ' If the cycle's mix or asphalt has been analysed before
            If (IsNothing(currentMixStats) OrElse Not cycle.FORMULA_NAME.Equals(currentMixStats.FORMULA_NAME) OrElse Not cycle.ASPHALT_NAME.Equals(currentMixStats.ASPHALT_STATS.NAME)) Then

                isNewMix = True     ' By default
                isNewAC = True      ' By default

                For Each mix In Me.MIX_STATS

                    ' If the cycle's mix is the same than a previously analysed mix
                    If (cycle.FORMULA_NAME.Equals(mix.FORMULA_NAME) And cycle.ASPHALT_NAME.Equals(mix.ASPHALT_STATS.NAME)) Then

                        currentMixStats = mix   ' Set the current mix

                        isNewMix = False
                        Exit For ' No need to loop more

                    End If

                Next

                For Each asphalt In Me.ASPHALT_STATS

                    ' If the cycle's asphalt is the same than a previously analysed asphalt
                    If (asphalt.NAME.Equals(cycle.ASPHALT_NAME)) Then

                        currentACStats = asphalt    ' Set the current asphalt

                        isNewAC = False
                        Exit For ' No need to loop more

                    End If

                Next

                ' If the cycle's mix hasn't been analysed before
                If (isNewMix) Then

                    currentMixStats = New MixStatistics ' Set the current mix

                    ' Set the mix's basic information
                    With currentMixStats

                        .NAME = cycle.MIX_NAME
                        .PRODUCTION_TYPE = XmlSettings.Settings.LANGUAGE.General.WordFor_Batch
                        .FORMULA_NAME = cycle.FORMULA_NAME

                        With .ASPHALT_STATS

                            .NAME = cycle.ASPHALT_NAME
                            .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, Settings.instance.Report.Word.TEMPERATURE_UNIT)

                        End With

                    End With

                    ' Add the mix to the list of mixes
                    Me.MIX_STATS.Add(currentMixStats)

                End If ' End if is new mix

                ' If the cycle's asphalt hasn't been analysed before
                If (isNewAC) Then

                    currentACStats = New AsphaltStatistics()

                    ' Set the asphalt's basic information
                    With currentACStats

                        .TANK = cycle.ASPHALT_TANK
                        .NAME = cycle.ASPHALT_NAME
                        .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, Settings.instance.Report.Word.TEMPERATURE_UNIT)

                    End With

                    ' Add the asphalt to the list of asphalts
                    Me.ASPHALT_STATS.Add(currentACStats)

                End If ' End if new asphalt

            End If ' End if different mix or asphalt


            ' --------------------CSV HOT FEEDS -------------------- '

            For Each feed In cycle.HOT_FEEDS

                Dim addToStats = True   ' Wheter or not the feeder is unknown; True by default

                ' For each batch feeder
                For Each feedStats In currentMixStats.BATCH_FEEDERS_STATS

                    ' If the feed is known
                    If (feed.INDEX.Equals(feedStats.INDEX)) Then

                        ' Increment the total mass of that feed
                        feedStats.TOTAL_MASS += currentDataFile.getUnitByTag(CSVCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, Settings.instance.Report.Word.MASS_UNIT)

                        addToStats = False
                        Exit For ' No need to loop more

                    End If

                Next ' End for each batch feeder

                ' If the feeder is unknown
                If (addToStats) Then

                    ' Add to batch feeders list - with basic info
                    currentMixStats.BATCH_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                            With {.INDEX = feed.INDEX, _
                                                                  .LOCATION = feed.LOCATION, _
                                                                  .MATERIAL_NAME = feed.MATERIAL_NAME, _
                                                                  .TOTAL_MASS = currentDataFile.getUnitByTag(CSVCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, Settings.instance.Report.Word.MASS_UNIT)})
                End If

            Next ' End for each cycle's hot feeds

            ' Add the cycle's information to the statistics
            currentMixStats.addCycle(cycle, Me.currentDataFile)
            currentACStats.addCycle(cycle, Me.currentDataFile)

            ' Increment the duration and mass of the batch production
            Me.batchProduction_duration = Me.batchProduction_duration.Add(cycle.DURATION)
            Me.batchProduction_totalMass += Me.currentDataFile.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, Settings.instance.Report.Word.MASS_UNIT)


            ' --------------------CSV EVENTS --------------------'

            ' If the cycle is not the first cycle of the day
            If (Not IsNothing(cycle.PREVIOUS_CYCLE)) Then

                ' If there was NO mix change.
                If (cycle.FORMULA_NAME.Equals(cycle.PREVIOUS_CYCLE.FORMULA_NAME)) Then

                    ' Check if the hot feeds Set Point Percentage has changed
                    For Each feed In cycle.HOT_FEEDS

                        For Each previousFeed In cycle.PREVIOUS_CYCLE.HOT_FEEDS

                            If (feed.INDEX.Equals(previousFeed.INDEX) AndAlso Not feed.SET_POINT_PERCENTAGE.Equals(previousFeed.SET_POINT_PERCENTAGE)) Then

                                ' Need to adapt this beacuse of material name...
                                ' Make new Mix Recipe Change event
                                Events.addMixRecipeChangeEvent(cycle.TIME, "Changement au " & feed.MATERIAL_NAME & ": " & previousFeed.SET_POINT_PERCENTAGE & "% à " & feed.SET_POINT_PERCENTAGE & "%")

                            End If

                        Next ' End for each previous cycle's hot feeds

                    Next ' End for each cycle's hot feed

                Else ' The formula has been changed

                    ' Make new Mix Change Event
                    Events.addMixChangeEvent(cycle.TIME, "Changement de formule : " & cycle.PREVIOUS_CYCLE.FORMULA_NAME & " à " & cycle.FORMULA_NAME)

                    Me.batchProduction_nbMixSwitch += 1

                End If ' End if there is a mix change

            End If ' End if 

        Next ' End for each CSV cycles


        ' ------------------------------------------------------------           ------------------------------------------------------------ '
        ' ------------------------------------------------------------LOG CYCLES ------------------------------------------------------------ '
        ' ------------------------------------------------------------           ------------------------------------------------------------ '

        ' Set the correct datafile xml node from the settings
        Me.currentDataFile = XmlSettings.Settings.instance.Usine.DataFiles.LOG

        ' For each LOG cycles
        For Each cycle In LOGCycleList

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
                        .PRODUCTION_TYPE = Settings.LANGUAGE.General.WordFor_Continuous
                        .FORMULA_NAME = cycle.FORMULA_NAME

                        With .ASPHALT_STATS

                            .NAME = cycle.ASPHALT_NAME
                            .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, Settings.instance.Report.Word.TEMPERATURE_UNIT)

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
                        .SET_POINT_TEMPERATURE = Me.currentDataFile.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(cycle.SET_POINT_TEMPERATURE, Settings.instance.Report.Word.TEMPERATURE_UNIT)

                    End With

                    ' If the cycle is not a "dry" cycle
                    If (cycle.PRODUCTION_SPEED > 0) Then

                        ' Add the new asphalt to the asphalts list
                        Me.ASPHALT_STATS.Add(currentACStats)

                    End If

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
                        feedStats.TOTAL_MASS += currentDataFile.getUnitByTag(LOGFeeder.MASS_TAG).convert(feedMass, Settings.instance.Report.Word.MASS_UNIT)

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
                                                                      .TOTAL_MASS = currentDataFile.getUnitByTag(LOGFeeder.MASS_TAG).convert(feedMass, Settings.instance.Report.Word.MASS_UNIT)})
                End If

            Next ' End for each cold feed

            ' Add the cycle's information to the statistics
            currentMixStats.addCycle(cycle, Me.currentDataFile)
            currentACStats.addCycle(cycle, Me.currentDataFile)

            ' If the cycle is not a "dry" cycle
            If (cycle.PRODUCTION_SPEED > 0) Then

                ' Increment the duration and mass of the batch production
                Me.continuousProduction_duration = Me.continuousProduction_duration.Add(cycle.DURATION)
                Me.continuousProduction_totalMass += Me.currentDataFile.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, Settings.instance.Report.Word.MASS_UNIT)

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

        Next ' End for each log cycle

        ' Sort by total mass from biggest to smallest
        Me.MIX_STATS.Sort()
        Me.ASPHALT_STATS.Sort()

        ' ------------------- Total and Other feeders statistics ------------------- '

        ' For each feeder of the last batch cycle
        For i = 0 To CSVCycleList.Last.HOT_FEEDS.Count - 1

            ' Add new feeder to total batch feeders list - with basic information
            Me.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                       With {.LOCATION = Me.CSVCycleList.Last.HOT_FEEDS(i).LOCATION, _
                                                             .MATERIAL_NAME = Me.CSVCycleList.Last.HOT_FEEDS(i).MATERIAL_NAME, _
                                                             .INDEX = Me.CSVCycleList.Last.HOT_FEEDS(i).INDEX})
        Next

        ' For each feeder of the last continuous cycle
        For i = 0 To LOGCycleList.Last.COLD_FEEDS.Count - 1

            ' Add new feeder to total continuous feeders list - with basic information
            Me.TOTAL_MIX_STATS.CONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                            With {.LOCATION = Me.LOGCycleList.Last.COLD_FEEDS(i).LOCATION, _
                                                                  .MATERIAL_NAME = Me.LOGCycleList.Last.COLD_FEEDS(i).MATERIAL_NAME, _
                                                                  .INDEX = Me.LOGCycleList.Last.COLD_FEEDS(i).INDEX})
        Next

        ' For each mix statistics
        For i = 0 To Me.MIX_STATS.Count - 1

            ' With current mix statistics
            With Me.MIX_STATS(i)

                ' If 4th biggest mix or higher
                If (i > 2) Then

                    ' Increment Other mix stats
                    Me.OTHER_MIX_STATS.CYCLE_MASS = .TOTAL_MASS
                    Me.OTHER_MIX_STATS.CYCLE_TIME = .TOTAL_TIME
                    Me.OTHER_MIX_STATS.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE
                    Me.OTHER_MIX_STATS.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS

                End If ' End if 4th biggest

                ' If current mix is Continuous
                If (.PRODUCTION_TYPE = Settings.LANGUAGE.General.WordFor_Continuous) Then

                    ' For each of the mix's feeder
                    For j = 0 To .CONTINUOUS_FEEDERS_STATS.Count - 1

                        ' If the mix is not a "dry" mix
                        If (.TOTAL_MASS > 0) Then

                            ' Increment the feeder's total mass
                            Me.TOTAL_MIX_STATS.CONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS += .CONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS

                        End If

                    Next

                Else ' then the current mix is Batch

                    ' For each of the mix's feeder
                    For j = 0 To .BATCH_FEEDERS_STATS.Count - 1

                        ' Increment the feeder's total mass
                        Me.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS(j).TOTAL_MASS += .BATCH_FEEDERS_STATS(j).TOTAL_MASS

                    Next

                End If ' end if continuous mix or batch mix

                ' Increment total mix statistics
                Me.TOTAL_MIX_STATS.CYCLE_MASS = .TOTAL_MASS
                Me.TOTAL_MIX_STATS.CYCLE_TIME = .TOTAL_TIME
                Me.TOTAL_MIX_STATS.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE

                ' If the mix recycled mass is available
                If (Not Double.IsNaN(.TOTAL_RECYCLED_MASS)) Then
                    Me.TOTAL_MIX_STATS.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS
                End If

                Me.TOTAL_MIX_STATS.ASPHALT_STATS.CYCLE_ASPHALT_MASS = .ASPHALT_STATS.TOTAL_MASS

            End With ' End with current mix statistics

        Next ' End for each mix statistics

    End Sub ' End gatherData()

    Public Overrides Sub generateGraphics()

        ' Progress Bar
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération des graphiques"
        ReportGenerationControl.instance.addStep(1)

        ' Set the culture for the graphics
        Threading.Thread.CurrentThread.CurrentCulture = XmlSettings.Settings.LANGUAGE.Culture

        Graphic.pointFormatList_asphalt = New PointFormatList
        Graphic.pointFormatList_mix = New PointFormatList

        Dim accumulatedMass As New AccumulatedMassGraphic(True)
        Dim asphaltPercentage As New AsphaltPercentageGraphic()
        Dim mixTemperature As New MixTemperatureGraphic()
        Dim productionSpeed As New ProductionSpeedGraphic()
        Dim mixTemperatureVariation As New MixTemperatureVariationGraphic()
        Dim asphaltPercentageVariation As New AsphaltPercentageVariationGraphic()
        Dim recycledPercentage As New RecycledPercentageGraphic()
        Dim fuelConsumption As New FuelConsumptionGraphic

        For Each cycle In Me.CSVCycleList

            accumulatedMass.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
            productionSpeed.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

        Next

        accumulatedMass.toggleMarkerColor()
        productionSpeed.toggleMarkerColor()

        For Each cycle In Me.LOGCycleList

            accumulatedMass.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
            productionSpeed.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)

        Next
        ' Progress Bar
        ReportGenerationControl.instance.addStep(1)

        For i = 0 To MIX_STATS.Count - 1
            For Each cycle In MIX_STATS(i).CYCLES

                If (TypeOf cycle Is CSVCycle) Then
                    asphaltPercentage.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
                    asphaltPercentageVariation.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
                    recycledPercentage.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
                Else
                    asphaltPercentage.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
                    asphaltPercentageVariation.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
                    recycledPercentage.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
                End If

            Next
        Next
        ' Progress Bar
        ReportGenerationControl.instance.addStep(1)

        For i = 0 To ASPHALT_STATS.Count - 1
            For Each cycle In ASPHALT_STATS(i).CYCLES
                If (TypeOf cycle Is CSVCycle) Then
                    mixTemperature.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
                    mixTemperatureVariation.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
                ElseIf (TypeOf cycle Is LOGCycle) Then
                    mixTemperature.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
                    mixTemperatureVariation.addCycle(cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
                End If
            Next
        Next
        ' Progress Bar
        ReportGenerationControl.instance.addStep(1)

        accumulatedMass.save()
        asphaltPercentage.save()
        mixTemperature.save()
        productionSpeed.save()
        mixTemperatureVariation.save()
        asphaltPercentageVariation.save()
        recycledPercentage.save()
        fuelConsumption.save()

        ' Progress Bar
        ReportGenerationControl.instance.addStep(1)

        Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.CreateSpecificCulture("en-US")

    End Sub

    Public Overrides Sub generateReports()

        Dim xls As HybridReport = Nothing
        Dim docx As DOCXReport = Nothing

        ProductionDay.generateModel = XmlSettings.Settings.instance.wasUpdated

        Try
            Me.gatherData()

            Me.generateGraphics()

            ' Progress Bar
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Excel (Ouverture de Excel)"

            xls = New HybridReport(Me.CSVCycleList, Me.LOGCycleList)
            xls.loadData()
            xls.loadGraphics()

            Dim savePath = Constants.Paths.EXTENDED_REPORTS_ARCHIVES_DIRECTORY & Settings.LANGUAGE.ExcelReport.FileName & " (" & Me.DATE_.ToString("yyyy-MM-dd") & ").xlsx"
            xls.saveAs(savePath)

            If (XmlSettings.Settings.instance.Report.Excel.OPEN_WHEN_DONE) Then
                XLSReport.ExcelApp.Visible = True
            Else
                xls.dispose()
                xls = Nothing
            End If

            ' Progress Bar
            ReportGenerationControl.instance.addStep(5)
            ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Ouverture de Word)"

            docx = New DOCXReport(Me)
            docx.generateReport()
            docx.saveAs(Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & Settings.LANGUAGE.WordReport.FileName & " (" & Me.DATE_.ToString("yyyy-MM-dd") & ").docx")
            docx.saveAsPDF(Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & Settings.LANGUAGE.WordReport.FileName & " (" & Me.DATE_.ToString("yyyy-MM-dd") & ").pdf")

            ' Progress Bar
            ReportGenerationControl.instance.addStep(10)

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
