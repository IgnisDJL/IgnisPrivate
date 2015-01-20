﻿Public Class HybridStatistics
    Inherits Statistics

    Public Overrides Sub compute(cycles As List(Of Cycle), ByRef events As EventsCollection)

        Dim firstCSVCycle As CSVCycle = Nothing
        Dim lastCSVCycle As CSVCycle = Nothing

        Dim firstLOGCycle As LOGCycle = Nothing
        Dim lastLOGCycle As LOGCycle = Nothing

        Dim currentMixStats As MixStatistics = Nothing      ' The statistics on the mix that is currently analysed
        Dim currentACStats As AsphaltStatistics = Nothing   ' The statistics on the asphalt that is currently analysed

        For Each currentCycle As Cycle In cycles

            If (TypeOf currentCycle Is CSVCycle) Then

                If (IsNothing(firstCSVCycle) OrElse firstCSVCycle.TIME.CompareTo(currentCycle.TIME) < 0) Then
                    firstCSVCycle = currentCycle
                End If

                analyseCSVCycle(currentCycle, currentMixStats, currentACStats, events)

                If (IsNothing(lastCSVCycle) OrElse lastCSVCycle.TIME.CompareTo(currentCycle.TIME) > 0) Then
                    lastCSVCycle = currentCycle
                End If

            ElseIf (TypeOf currentCycle Is LOGCycle) Then

                If (IsNothing(firstLOGCycle) OrElse firstLOGCycle.TIME.CompareTo(currentCycle.TIME) < 0) Then
                    firstLOGCycle = currentCycle
                End If

                analyseLOGCycle(currentCycle, currentMixStats, currentACStats, events)

                If (IsNothing(lastLOGCycle) OrElse lastLOGCycle.TIME.CompareTo(currentCycle.TIME) > 0) Then
                    lastLOGCycle = currentCycle
                End If

            End If

        Next

        computeStartTime(firstCSVCycle, firstLOGCycle, events.START_EVENTS.First)
        computeEndTime(lastCSVCycle, lastLOGCycle, events.STOP_EVENTS.Last)

        Me.AllMixes.Sort()
        Me.AllAsphalts.Sort()

        computerOtherAndTotalMix(lastCSVCycle, lastLOGCycle)

    End Sub

    Private Sub analyseCSVCycle(ByRef currentCycle As CSVCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics, ByRef events As EventsCollection)

        analyseCSVMixAndAC(currentCycle, currentMixStats, currentACStats)

        analyseCSVHotFeeds(currentCycle, currentMixStats)

        ' Add the cycle's information to the statistics
        currentMixStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
        currentACStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

        ' Increment the duration and mass of the batch production
        Me.DiscontinuousProduction.Duration = Me.DiscontinuousProduction.Duration.Add(currentCycle.DURATION)
        Me.DiscontinuousProduction.Quantity += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(currentCycle.MIX_MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

    End Sub

    Private Sub analyseCSVMixAndAC(ByRef currentCycle As CSVCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics)

        Dim isNewMix As Boolean                      ' Represents wether or not the cycle's mix has been analysed that day
        Dim isNewAC As Boolean                       ' Represents wether or not the cycle's asphalt has been analysed that day

        ' If the cycle's mix or asphalt has been analysed before
        If (IsNothing(currentMixStats) OrElse _
            Not currentCycle.FORMULA_NAME.Equals(currentMixStats.FORMULA_NAME) OrElse _
            Not currentCycle.ASPHALT_NAME.Equals(currentMixStats.ASPHALT_STATS.NAME)) Then

            isNewMix = True     ' By default
            isNewAC = True      ' By default

            For Each mix In Me.AllMixes

                ' #refactor - use allmixes.contains
                ' If the cycle's mix is the same than a previously analysed mix
                If (currentCycle.FORMULA_NAME.Equals(mix.FORMULA_NAME) And currentCycle.ASPHALT_NAME.Equals(mix.ASPHALT_STATS.NAME)) Then

                    currentMixStats = mix   ' Set the current mix

                    isNewMix = False
                    Exit For ' No need to loop more

                End If

            Next

            For Each asphalt In Me.AllAsphalts

                ' If the cycle's asphalt is the same than a previously analysed asphalt
                If (asphalt.NAME.Equals(currentCycle.ASPHALT_NAME)) Then

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

                    .NAME = currentCycle.MIX_NAME
                    .PRODUCTION_TYPE = XmlSettings.Settings.LANGUAGE.General.WordFor_Batch
                    .FORMULA_NAME = currentCycle.FORMULA_NAME

                    With .ASPHALT_STATS

                        .NAME = currentCycle.ASPHALT_NAME
                        ' #refactor - settings
                        .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                    End With

                End With

                ' Add the mix to the list of mixes
                Me.AllMixes.Add(currentMixStats)

            End If ' End if is new mix

            ' If the cycle's asphalt hasn't been analysed before
            If (isNewAC) Then

                currentACStats = New AsphaltStatistics()

                ' Set the asphalt's basic information
                With currentACStats

                    .TANK = currentCycle.ASPHALT_TANK
                    .NAME = currentCycle.ASPHALT_NAME
                    .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                End With

                ' Add the asphalt to the list of asphalts
                Me.AllAsphalts.Add(currentACStats)

            End If ' End if new asphalt

        End If ' End if different mix or asphalt

    End Sub

    Private Sub analyseCSVHotFeeds(ByRef currentCycle As CSVCycle, ByRef currentMixStats As MixStatistics)

        Dim addToStats As Boolean ' Whether or not the feeder is unknown

        For Each feed In currentCycle.HOT_FEEDS

            addToStats = True ' True by default

            ' For each batch feeder
            For Each feedStats In currentMixStats.BATCH_FEEDERS_STATS

                ' If the feed is known
                If (feed.INDEX.Equals(feedStats.INDEX)) Then

                    ' Increment the total mass of that feed
                    feedStats.TOTAL_MASS += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(currentCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

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
                                                              .TOTAL_MASS = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(currentCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)})
            End If

        Next ' End for each cycle's hot feeds

    End Sub

    Private Sub analyseCSVEvents(ByRef currentCycle As CSVCycle, ByRef events As EventsCollection)

        ' If the cycle is not the first cycle of the day
        If (Not IsNothing(currentCycle.PREVIOUS_CYCLE)) Then

            ' If there was NO mix change.
            If (currentCycle.FORMULA_NAME.Equals(currentCycle.PREVIOUS_CYCLE.FORMULA_NAME)) Then

                ' Check if the hot feeds Set Point Percentage has changed
                For Each feed In currentCycle.HOT_FEEDS

                    For Each previousFeed In currentCycle.PREVIOUS_CYCLE.HOT_FEEDS

                        If (feed.INDEX.Equals(previousFeed.INDEX) AndAlso Not feed.SET_POINT_PERCENTAGE.Equals(previousFeed.SET_POINT_PERCENTAGE)) Then

                            ' Need to adapt this beacuse of material name...
                            ' Make new Mix Recipe Change event
                            events.addMixRecipeChangeEvent(currentCycle.TIME, "Changement au " & feed.MATERIAL_NAME & ": " & previousFeed.SET_POINT_PERCENTAGE & "% à " & feed.SET_POINT_PERCENTAGE & "%")

                        End If

                    Next ' End for each previous cycle's hot feeds

                Next ' End for each cycle's hot feed

            Else ' The formula has been changed

                ' Make new Mix Change Event
                events.addMixChangeEvent(currentCycle.TIME, "Changement de formule : " & currentCycle.PREVIOUS_CYCLE.FORMULA_NAME & " à " & currentCycle.FORMULA_NAME)

                Me.DiscontinuousProduction.NbMixSwitch += 1

            End If ' End if there is a mix change

        End If ' End if 

    End Sub

    Private Sub analyseLOGCycle(ByRef currentCycle As LOGCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics, ByRef events As EventsCollection)

        analyseLOGMixAndAC(currentCycle, currentMixStats, currentACStats)

        analyseLOGColdFeeds(currentCycle, currentMixStats)

        ' Add the cycle's information to the statistics
        currentMixStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
        currentACStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)

        ' If the cycle is not a "dry" cycle
        If (currentCycle.PRODUCTION_SPEED > 0) Then

            ' Increment the duration and mass of the batch production
            Me.ContinuousProduction.Duration = Me.ContinuousProduction.Duration.Add(currentCycle.DURATION)
            Me.ContinuousProduction.Quantity += XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(currentCycle.MIX_MASS, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

        End If

        analyseLOGEvents(currentCycle, events)

    End Sub

    Private Sub analyseLOGMixAndAC(ByRef currentCycle As LOGCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics)

        Dim isNewMix As Boolean                      ' Represents wether or not the cycle's mix has been analysed that day
        Dim isNewAC As Boolean                       ' Represents wether or not the cycle's asphalt has been analysed that day

        ' If the cycle's mix or asphalt has been analysed before
        If (IsNothing(currentMixStats) OrElse _
            Not currentCycle.FORMULA_NAME.Equals(currentMixStats.FORMULA_NAME) OrElse _
            Not currentCycle.ASPHALT_NAME.Equals(currentMixStats.ASPHALT_STATS.NAME)) Then

            isNewMix = True     ' By default
            isNewAC = True      ' By default

            ' If the cycle's mix is the same than a previously analysed mix
            For Each mix In Me.AllMixes

                If (currentCycle.FORMULA_NAME.Equals(mix.FORMULA_NAME) And currentCycle.ASPHALT_NAME.Equals(mix.ASPHALT_STATS.NAME)) Then

                    currentMixStats = mix   ' Set the current mix

                    isNewMix = False
                    Exit For ' No need to loop more

                End If

            Next

            ' If the cycle's asphalt is the same than a previously analysed asphalt
            For Each asphalt In Me.AllAsphalts

                If (asphalt.NAME.Equals(currentCycle.ASPHALT_NAME)) Then

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

                    .NAME = currentCycle.MIX_NAME
                    .PRODUCTION_TYPE = XmlSettings.Settings.LANGUAGE.General.WordFor_Continuous
                    .FORMULA_NAME = currentCycle.FORMULA_NAME

                    With .ASPHALT_STATS

                        .NAME = currentCycle.ASPHALT_NAME
                        .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                    End With

                End With

                ' Add the mix to the list of mixes
                Me.AllMixes.Add(currentMixStats)


            End If ' End if new mix

            ' If the cycle's asphalt hasn't been analysed 
            If (isNewAC) Then

                currentACStats = New AsphaltStatistics() ' Set the current asphalt

                ' Set the asphalt's basic information
                With currentACStats

                    .NAME = currentCycle.ASPHALT_NAME
                    .TANK = currentCycle.ASPHALT_TANK
                    .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT)

                End With

                ' If the cycle is not a "dry" cycle
                If (currentCycle.PRODUCTION_SPEED > 0) Then

                    ' Add the new asphalt to the asphalts list
                    Me.AllAsphalts.Add(currentACStats)

                End If

            End If ' End if new asphalt

        End If ' End if different mix or asphalt

    End Sub

    Private Sub analyseLOGColdFeeds(ByRef currentCycle As LOGCycle, ByRef currentMixStats As MixStatistics)

        Dim addToStats As Boolean       ' Wheter or not the feeder is unknown

        For Each feed In currentCycle.COLD_FEEDS

            addToStats = True       ' True by default

            ' Set the feed's mass WITHOUT THE MOISTURE'S MASS
            Dim feedMass = feed.MASS - feed.MASS * XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(LOGFeeder.MOISTURE_PERCENTAGE_TAG).convert(DirectCast(feed, LOGFeeder).MOISTURE_PERCENTAGE, PerOne.UNIT)

            ' For each continuous feeder
            For Each feedStats In currentMixStats.CONTINUOUS_FEEDERS_STATS

                ' If the feed is known
                If (feed.INDEX.Equals(feedStats.INDEX)) Then

                    ' Increment the feeds total mass
                    feedStats.TOTAL_MASS += XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(LOGFeeder.MASS_TAG).convert(feedMass, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)

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
                                                                  .TOTAL_MASS = XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(LOGFeeder.MASS_TAG).convert(feedMass, XmlSettings.Settings.instance.Report.Word.MASS_UNIT)})
            End If

        Next ' End for each cold feed

    End Sub

    Private Sub analyseLOGEvents(ByRef currentCycle As LOGCycle, ByRef events As EventsCollection)

        ' If the cycle is not the first cycle of the day
        If (Not IsNothing(currentCycle.PREVIOUS_CYCLE)) Then

            ' If there was NO mix change.
            If (currentCycle.FORMULA_NAME.Equals(currentCycle.PREVIOUS_CYCLE.FORMULA_NAME)) Then

                ' Check if the cold feeds Set Point Percentage, Moisture or Asphalt Set Point Percentage has changed
                For i = 0 To currentCycle.COLD_FEEDS.Count - 1

                    If (Not currentCycle.COLD_FEEDS(i).SET_POINT_PERCENTAGE.Equals(currentCycle.PREVIOUS_CYCLE.COLD_FEEDS(i).SET_POINT_PERCENTAGE)) Then

                        ' Make new Mix Recipe Change event
                        events.addMixRecipeChangeEvent(currentCycle.TIME, "Changement au " & currentCycle.COLD_FEEDS(i).LOCATION & ": " & currentCycle.PREVIOUS_CYCLE.COLD_FEEDS(i).SET_POINT_PERCENTAGE & "% à " & currentCycle.COLD_FEEDS(i).SET_POINT_PERCENTAGE & "%")

                    End If

                    If (Not DirectCast(currentCycle.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE.Equals(DirectCast(currentCycle.PREVIOUS_CYCLE.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE)) Then

                        ' Make new Mix Recipe Change event
                        events.addMixRecipeChangeEvent(currentCycle.TIME, "Changement de % HUM au " & currentCycle.COLD_FEEDS(i).LOCATION & " : " & DirectCast(currentCycle.PREVIOUS_CYCLE.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE & "% à " & DirectCast(currentCycle.COLD_FEEDS(i), LOGFeeder).MOISTURE_PERCENTAGE & "%")

                    End If

                    If (Not currentCycle.ASPHALT_SET_POINT_PERCENTAGE.Equals(currentCycle.PREVIOUS_CYCLE.ASPHALT_SET_POINT_PERCENTAGE)) Then

                        ' Make new Mix Recipe Change event
                        events.addMixRecipeChangeEvent(currentCycle.TIME, "Changement de % bitume : " & currentCycle.PREVIOUS_CYCLE.ASPHALT_SET_POINT_PERCENTAGE & "% à " & currentCycle.ASPHALT_SET_POINT_PERCENTAGE & "%")

                    End If

                Next

            Else ' If there was a mix change

                ' Make new Mix Change event
                events.addMixChangeEvent(currentCycle.TIME, "Changement de formule : " & currentCycle.PREVIOUS_CYCLE.FORMULA_NAME & " à " & currentCycle.FORMULA_NAME)

                Me.ContinuousProduction.NbMixSwitch += 1

            End If

        End If ' End if is not first cycle

    End Sub

    Private Sub computeStartTime(firstCSVCycle As CSVCycle, firstLOGCycle As LOGCycle, firstStartEvent As SingleEvent)

        If (Not IsNothing(firstCSVCycle)) Then

            Me.DiscontinuousProduction.StartTime = firstCSVCycle.TIME

            Me._startTime = firstCSVCycle.TIME

        End If

        If (Not IsNothing(firstLOGCycle)) Then

            Me.ContinuousProduction.StartTime = firstLOGCycle.TIME

            If (Me._startTime.CompareTo(firstLOGCycle.TIME) > 0) Then

                Me._startTime = firstLOGCycle.TIME

            End If

        End If

        If (Not IsNothing(firstStartEvent) AndAlso _
           Me._startTime.CompareTo(firstStartEvent.TIME) > 0) Then

            Me._startTime = firstStartEvent.TIME

        End If

    End Sub

    Private Sub computeEndTime(lastCSVCycle As CSVCycle, lastLOGCycle As LOGCycle, lastStopEvent As SingleEvent)

        If (Not IsNothing(lastCSVCycle)) Then

            Me.DiscontinuousProduction.EndTime = lastCSVCycle.TIME

            Me._endTime = lastCSVCycle.TIME

        End If

        If (Not IsNothing(lastLOGCycle)) Then

            Me.ContinuousProduction.EndTime = lastLOGCycle.TIME

            If (Me._endTime.CompareTo(lastLOGCycle.TIME) < 0) Then

                Me._endTime = lastLOGCycle.TIME

            End If

        End If

        If (Not IsNothing(lastStopEvent) AndAlso _
            Me._endTime.CompareTo(lastStopEvent.TIME) < 0) Then

            Me._endTime = lastStopEvent.TIME

        End If

    End Sub

    Private Sub computerOtherAndTotalMix(lastCSVCycle As CSVCycle, lastLOGCycle As LOGCycle)

        If (Not IsNothing(lastCSVCycle)) Then

            ' For each feeder of the last batch cycle
            For i = 0 To lastCSVCycle.HOT_FEEDS.Count - 1

                ' Add new feeder to total batch feeders list - with basic information
                Me.MixesTotal.BATCH_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                           With {.LOCATION = lastCSVCycle.HOT_FEEDS(i).LOCATION, _
                                                                 .MATERIAL_NAME = lastCSVCycle.HOT_FEEDS(i).MATERIAL_NAME, _
                                                                 .INDEX = lastCSVCycle.HOT_FEEDS(i).INDEX})
            Next

        End If

        If (Not IsNothing(lastLOGCycle)) Then

            ' For each feeder of the last continuous cycle
            For i = 0 To lastLOGCycle.COLD_FEEDS.Count - 1

                ' Add new feeder to total continuous feeders list - with basic information
                Me.MixesTotal.CONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                                With {.LOCATION = lastLOGCycle.COLD_FEEDS(i).LOCATION, _
                                                                      .MATERIAL_NAME = lastLOGCycle.COLD_FEEDS(i).MATERIAL_NAME, _
                                                                      .INDEX = lastLOGCycle.COLD_FEEDS(i).INDEX})
            Next

        End If

        ' For each mix statistics
        For i = 0 To Me.AllMixes.Count - 1

            ' With current mix statistics
            With Me.AllMixes(i)

                ' If 4th biggest mix or higher
                If (i > 2) Then

                    ' Increment Other mix stats
                    Me.OtherMixes.CYCLE_MASS = .TOTAL_MASS
                    Me.OtherMixes.CYCLE_TIME = .TOTAL_TIME
                    Me.OtherMixes.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE
                    Me.OtherMixes.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS

                End If ' End if 4th biggest

                ' If current mix is Continuous
                If (.PRODUCTION_TYPE = XmlSettings.Settings.LANGUAGE.General.WordFor_Continuous) Then

                    ' For each of the mix's feeder
                    For j = 0 To .CONTINUOUS_FEEDERS_STATS.Count - 1

                        ' If the mix is not a "dry" mix
                        If (.TOTAL_MASS > 0) Then

                            ' Increment the feeder's total mass
                            Me.MixesTotal.CONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS += .CONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS

                        End If

                    Next

                Else ' then the current mix is Batch

                    ' For each of the mix's feeder
                    For j = 0 To .BATCH_FEEDERS_STATS.Count - 1

                        ' Increment the feeder's total mass
                        Me.MixesTotal.BATCH_FEEDERS_STATS(j).TOTAL_MASS += .BATCH_FEEDERS_STATS(j).TOTAL_MASS

                    Next

                End If ' end if continuous mix or batch mix

                ' Increment total mix statistics
                Me.MixesTotal.CYCLE_MASS = .TOTAL_MASS
                Me.MixesTotal.CYCLE_TIME = .TOTAL_TIME
                Me.MixesTotal.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE

                ' If the mix recycled mass is available
                If (Not Double.IsNaN(.TOTAL_RECYCLED_MASS)) Then
                    Me.MixesTotal.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS
                End If

                Me.MixesTotal.ASPHALT_STATS.CYCLE_ASPHALT_MASS = .ASPHALT_STATS.TOTAL_MASS

            End With ' End with current mix statistics

        Next ' End for each mix statistics

    End Sub


End Class
