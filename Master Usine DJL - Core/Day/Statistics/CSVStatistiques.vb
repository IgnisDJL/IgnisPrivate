Public Class CSVStatistiques
    Inherits Statistics


    Public Overrides Sub compute(cycles As List(Of Cycle), ByRef events As EventsCollection)

        Dim firstCSVCycle As CSVCycle = Nothing
        Dim lastCSVCycle As CSVCycle = Nothing

        Dim currentMixStats As MixStatistics = Nothing      ' The statistics on the mix that is currently analysed
        Dim currentACStats As AsphaltStatistics = Nothing   ' The statistics on the asphalt that is currently analysed

        ' #refactor - Move to after the loop when we have sortable start events
        events.addStartEvent(cycles.First.TIME)

        For Each currentCycle As Cycle In cycles

            If (TypeOf currentCycle Is CSVCycle) Then

                If (IsNothing(firstCSVCycle) OrElse firstCSVCycle.TIME.CompareTo(currentCycle.TIME) > 0) Then
                    firstCSVCycle = currentCycle
                End If

                analyseCSVCycle(currentCycle, currentMixStats, currentACStats, events)
                analyseCSVEvents(currentCycle, events)

                If (IsNothing(lastCSVCycle) OrElse lastCSVCycle.TIME.CompareTo(currentCycle.TIME) < 0) Then
                    lastCSVCycle = currentCycle
                End If

            End If

            If (currentCycle.PRODUCTION_SPEED > 0) Then
                Me._nbProductiveCycles += 1
            End If

        Next

        events.addStopEvent(lastCSVCycle.TIME)

        computeStartTime(firstCSVCycle, If(events.START_EVENTS.Count = 0, Nothing, events.START_EVENTS.First))
        computeEndTime(lastCSVCycle, If(events.STOP_EVENTS.Count = 0, Nothing, events.STOP_EVENTS.First))

        Me.AllMixes.Sort()
        Me.AllAsphalts.Sort()

        computeOtherAndTotalMix(lastCSVCycle)

    End Sub

    Private Sub analyseCSVCycle(ByRef currentCycle As CSVCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics, ByRef events As EventsCollection)

        analyseCSVMixAndAC(currentCycle, currentMixStats, currentACStats)

        analyseCSVHotFeeds(currentCycle, currentMixStats)

        ' Add the cycle's information to the statistics
        currentMixStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
        currentACStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

        ' Increment the duration and mass of the batch production
        Me.DiscontinuousProduction.Duration = Me.DiscontinuousProduction.Duration.Add(currentCycle.DURATION)
        Me.DiscontinuousProduction.Quantity += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(currentCycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        Me.DiscontinuousProduction.Cycles.Add(currentCycle)
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
                    .PRODUCTION_TYPE = MixStatistics.ProductionTypes.Discontinuous
                    .FORMULA_NAME = currentCycle.FORMULA_NAME

                    With .ASPHALT_STATS

                        .NAME = currentCycle.ASPHALT_NAME
                        ' #refactor - settings
                        .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Reports.TEMPERATURE_UNIT)

                    End With

                End With

                Me.DiscontinuousProduction.Mixes.Add(currentMixStats)

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
                    .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Reports.TEMPERATURE_UNIT)

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
            For Each feedStats In currentMixStats.DISCONTINUOUS_FEEDERS_STATS

                ' If the feed is known
                If (feed.INDEX.Equals(feedStats.INDEX)) Then

                    ' Increment the total mass of that feed
                    feedStats.TOTAL_MASS += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(CSVCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)

                    addToStats = False
                    Exit For ' No need to loop more

                End If

            Next ' End for each batch feeder

            ' If the feeder is unknown
            If (addToStats) Then

                ' Add to batch feeders list - with basic info
                currentMixStats.DISCONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                        With {.INDEX = feed.INDEX, _
                                                              .LOCATION = feed.LOCATION, _
                                                              .MATERIAL_NAME = feed.MATERIAL_NAME, _
                                                              .TOTAL_MASS = XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(CSVCycle.AGGREGATE_MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)})
            End If

        Next ' End for each cycle's hot feeds

    End Sub

    Private Sub analyseCSVEvents(ByRef currentCycle As CSVCycle, ByRef events As EventsCollection)

        ' If the cycle is not the first cycle of the day
        If (Not IsNothing(currentCycle.PREVIOUS_CYCLE)) Then

            Dim timeDiff = currentCycle.TIME.Subtract(currentCycle.PREVIOUS_CYCLE.TIME)

            If (timeDiff.Subtract(currentCycle.DURATION).CompareTo(TimeSpan.FromSeconds(XmlSettings.Settings.instance.Usine.DataFiles.CSV.STOP_OFFSET)) > 0) Then

                events.addStopEvent(currentCycle.PREVIOUS_CYCLE.TIME.Add(currentCycle.DURATION))
                events.addStartEvent(currentCycle.TIME)

            End If

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

        End If ' End if ...

    End Sub

    Private Sub computeStartTime(firstCSVCycle As CSVCycle, firstStartEvent As SingleEvent)

        If (Not IsNothing(firstCSVCycle)) Then

            Me.DiscontinuousProduction.StartTime = firstCSVCycle.TIME

            Me._startTime = firstCSVCycle.TIME

        End If

        If (Not IsNothing(firstStartEvent) AndAlso _
           Me._startTime.CompareTo(firstStartEvent.TIME) > 0) Then

            Me._startTime = firstStartEvent.TIME

        End If

    End Sub

    Private Sub computeEndTime(lastCSVCycle As CSVCycle, lastStopEvent As SingleEvent)

        If (Not IsNothing(lastCSVCycle)) Then

            Me.DiscontinuousProduction.EndTime = lastCSVCycle.TIME

            Me._endTime = lastCSVCycle.TIME

        End If

        If (Not IsNothing(lastStopEvent) AndAlso _
            Me._endTime.CompareTo(lastStopEvent.TIME) < 0) Then

            Me._endTime = lastStopEvent.TIME

        End If

    End Sub

    Private Sub computeOtherAndTotalMix(lastCSVCycle As CSVCycle)

        If (Not IsNothing(lastCSVCycle)) Then

            ' For each feeder of the last batch cycle
            For i = 0 To lastCSVCycle.HOT_FEEDS.Count - 1

                ' Add new feeder to total batch feeders list - with basic information
                Me.MixesTotal.DISCONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() _
                                                           With {.LOCATION = lastCSVCycle.HOT_FEEDS(i).LOCATION, _
                                                                 .MATERIAL_NAME = lastCSVCycle.HOT_FEEDS(i).MATERIAL_NAME, _
                                                                 .INDEX = lastCSVCycle.HOT_FEEDS(i).INDEX})
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
                If (.PRODUCTION_TYPE = MixStatistics.ProductionTypes.Continuous) Then

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
                    For j = 0 To .DISCONTINUOUS_FEEDERS_STATS.Count - 1

                        ' Increment the feeder's total mass
                        Me.MixesTotal.DISCONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS += .DISCONTINUOUS_FEEDERS_STATS(j).TOTAL_MASS

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
