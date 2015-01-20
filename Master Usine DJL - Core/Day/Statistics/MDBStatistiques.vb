Public Class MDBStatistiques
    Inherits Statistics


    Public Overrides Sub compute(cycles As List(Of Cycle), ByRef events As EventsCollection)

        Dim firstMDBCycle As MDBCycle = Nothing
        Dim lastMDBCycle As MDBCycle = Nothing

        Dim currentMixStats As MixStatistics = Nothing      ' The statistics on the mix that is currently analysed
        Dim currentACStats As AsphaltStatistics = Nothing   ' The statistics on the asphalt that is currently analysed

        ' #refactor - Move to after the loop when we have sortable start events
        events.addStartEvent(cycles.First.TIME)

        For Each currentCycle As Cycle In cycles

            If (TypeOf currentCycle Is MDBCycle) Then

                If (IsNothing(firstMDBCycle) OrElse firstMDBCycle.TIME.CompareTo(currentCycle.TIME) > 0) Then
                    firstMDBCycle = currentCycle
                End If

                analyseMDBCycle(currentCycle, currentMixStats, currentACStats, events)
                analyseMDBEvents(currentCycle, events)

                If (IsNothing(lastMDBCycle) OrElse lastMDBCycle.TIME.CompareTo(currentCycle.TIME) < 0) Then
                    lastMDBCycle = currentCycle
                End If

            End If

            If (currentCycle.PRODUCTION_SPEED > 0) Then
                Me._nbProductiveCycles += 1
            End If

        Next

        events.addStopEvent(lastMDBCycle.TIME)

        computeStartTime(firstMDBCycle, If(events.START_EVENTS.Count = 0, Nothing, events.START_EVENTS.First))
        computeEndTime(lastMDBCycle, If(events.STOP_EVENTS.Count = 0, Nothing, events.STOP_EVENTS.First))

        Me.AllMixes.Sort()
        Me.AllAsphalts.Sort()

        computeOtherAndTotalMix(lastMDBCycle)

    End Sub

    Private Sub analyseMDBCycle(ByRef currentCycle As MDBCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics, ByRef events As EventsCollection)

        analyseMDBMixAndAC(currentCycle, currentMixStats, currentACStats)

        analyseMDBHotFeeds(currentCycle, currentMixStats)


        If (Not IsNothing(currentMixStats) AndAlso Not IsNothing(currentACStats)) Then
            ' Add the cycle's information to the statistics
            currentMixStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
            currentACStats.addCycle(currentCycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
        End If


        ' Increment the duration and mass of the batch production
        Me.DiscontinuousProduction.Duration = Me.DiscontinuousProduction.Duration.Add(currentCycle.DURATION)
        Me.DiscontinuousProduction.Quantity += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(IGNIS.Cycle.MIX_MASS_TAG).convert(currentCycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        Me.DiscontinuousProduction.Cycles.Add(currentCycle)

    End Sub

    Private Sub analyseMDBMixAndAC(ByRef currentCycle As MDBCycle, ByRef currentMixStats As MixStatistics, ByRef currentACStats As AsphaltStatistics)


        Dim isNewMix As Boolean = True  ' Represents wether or not the cycle's mix has been analysed that day
        Dim isNewAC As Boolean          ' Represents wether or not the cycle's asphalt has been analysed that day

        If (Not IsNothing(currentCycle.ASPHALT_NAME)) Then

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

                        currentMixStats = mix ' Set the current mix

                        isNewMix = False
                        Exit For ' No need to loop more

                    End If

                Next

                For Each asphalt In Me.AllAsphalts

                    ' If the cycle's asphalt is the same than a previously analysed asphalt
                    If (asphalt.NAME.Equals(currentCycle.ASPHALT_NAME)) Then

                        currentACStats = asphalt ' Set the current asphalt

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
                            .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Reports.TEMPERATURE_UNIT)

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

                        .NAME = currentCycle.ASPHALT_NAME
                        .TANK = currentCycle.ASPHALT_TANK
                        .SET_POINT_TEMPERATURE = XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(IGNIS.Cycle.SET_POINT_TEMPERATURE_TAG).convert(currentCycle.SET_POINT_TEMPERATURE, XmlSettings.Settings.instance.Reports.TEMPERATURE_UNIT)

                    End With

                    ' Add the asphalt to the list of asphalts
                    Me.AllAsphalts.Add(currentACStats)

                End If ' End if new asphalt

            End If ' End if different mix or asphalt
        End If

    End Sub

    Private Sub analyseMDBHotFeeds(ByRef currentCycle As MDBCycle, ByRef currentMixStats As MixStatistics)



    End Sub

    Private Sub analyseMDBEvents(ByRef currentCycle As MDBCycle, ByRef events As EventsCollection)

        ' If the cycle is not the first cycle of the day
        If (Not IsNothing(currentCycle.PREVIOUS_CYCLE)) Then

            Dim timeDiff = currentCycle.TIME.Subtract(currentCycle.PREVIOUS_CYCLE.TIME)

            If (timeDiff.Subtract(currentCycle.DURATION).CompareTo(TimeSpan.FromSeconds(XmlSettings.Settings.instance.Usine.DataFiles.MDB.STOP_OFFSET)) > 0) Then

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

                    If (DirectCast(feed, MDBFeeder).MANUAL_MODE) Then

                        events.addMixRecipeChangeEvent(currentCycle.TIME, "Addition de " & feed.MATERIAL_NAME & " manuelle : " & XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(MDBFeeder.MASS_TAG).convert(feed.MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT) & "(" & XmlSettings.Settings.instance.Reports.MASS_UNIT & ")")

                    End If
                Next ' End for each cycle's hot feed

                If (Not currentCycle.ASPHALT_SET_POINT_PERCENTAGE.Equals(currentCycle.PREVIOUS_CYCLE.ASPHALT_SET_POINT_PERCENTAGE)) Then

                    events.addMixRecipeChangeEvent(currentCycle.TIME, "Changement de % Bitume : " & currentCycle.PREVIOUS_CYCLE.ASPHALT_SET_POINT_PERCENTAGE & "% à " & currentCycle.ASPHALT_SET_POINT_PERCENTAGE & "%")

                End If

                For Each asphaltFeed As MDBFeeder In currentCycle.ASPHALT_FEEDS

                    If (asphaltFeed.MANUAL_MODE) Then

                        events.addMixRecipeChangeEvent(currentCycle.TIME, "Addition de " & currentCycle.ASPHALT_SUMMARY_FEED.MATERIAL_NAME & " manuelle : " & XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(MDBFeeder.MASS_TAG).convert(currentCycle.ASPHALT_SUMMARY_FEED.MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT) & "(" & XmlSettings.Settings.instance.Reports.MASS_UNIT & ")")

                    End If

                Next


            Else ' The formula has been changed

                ' Make new Mix Change Event
                events.addMixChangeEvent(currentCycle.TIME, "Changement de formule : " & currentCycle.PREVIOUS_CYCLE.FORMULA_NAME & " à " & currentCycle.FORMULA_NAME)

                Me.DiscontinuousProduction.NbMixSwitch += 1

            End If ' End if there is a mix change

        End If

    End Sub

    Private Sub computeStartTime(firstMDBCycle As MDBCycle, firstStartEvent As SingleEvent)

        If (Not IsNothing(firstMDBCycle)) Then

            Me.DiscontinuousProduction.StartTime = firstMDBCycle.TIME

            Me._startTime = firstMDBCycle.TIME

        End If

        If (Not IsNothing(firstStartEvent) AndAlso _
           Me._startTime.CompareTo(firstStartEvent.TIME) > 0) Then

            Me._startTime = firstStartEvent.TIME

        End If

    End Sub

    Private Sub computeEndTime(lastMDBCycle As MDBCycle, lastStopEvent As SingleEvent)

        If (Not IsNothing(lastMDBCycle)) Then

            Me.DiscontinuousProduction.EndTime = lastMDBCycle.TIME

            Me._endTime = lastMDBCycle.TIME

        End If

        If (Not IsNothing(lastStopEvent) AndAlso _
            Me._endTime.CompareTo(lastStopEvent.TIME) < 0) Then

            Me._endTime = lastStopEvent.TIME

        End If

    End Sub

    Private Sub computeOtherAndTotalMix(lastMDBCycle As MDBCycle)

        For i = 0 To Me.AllMixes.Count - 1

            With Me.AllMixes(i)

                If (i > 2) Then

                    Me.OtherMixes.CYCLE_MASS = .TOTAL_MASS
                    Me.OtherMixes.CYCLE_TIME = .TOTAL_TIME
                    Me.OtherMixes.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE

                    ' #remove - remove if???
                    If (Not Double.IsNaN(.TOTAL_RECYCLED_MASS)) Then
                        Me.OtherMixes.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS
                    End If

                End If

                Dim lastCycle = DirectCast(.CYCLES.Last, MDBCycle)
                Dim addToList As Boolean

                For j = 0 To 7 ' <- Feed index - where's the 7 from? #refactor

                    addToList = True

                    Dim feedStats As New FeedersStatistics() With {.INDEX = j}
                    For Each cycle In .CYCLES

                        For Each cycleHotFeed In cycle.HOT_FEEDS

                            If (cycleHotFeed.INDEX = feedStats.INDEX) Then

                                feedStats.LOCATION = cycleHotFeed.LOCATION
                                feedStats.MATERIAL_NAME = cycleHotFeed.MATERIAL_NAME
                                feedStats.TOTAL_MASS += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Feeder.MASS_TAG).convert(cycleHotFeed.MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)

                            End If

                        Next

                    Next

                    .DISCONTINUOUS_FEEDERS_STATS.Add(feedStats)

                    If (feedStats.TOTAL_MASS > 0) Then

                        For Each totalFeed In Me.MixesTotal.DISCONTINUOUS_FEEDERS_STATS

                            If (totalFeed.INDEX.Equals(feedStats.INDEX)) Then
                                totalFeed.TOTAL_MASS += feedStats.TOTAL_MASS
                                addToList = False
                            End If
                        Next

                        If (addToList) Then
                            Me.MixesTotal.DISCONTINUOUS_FEEDERS_STATS.Add(New FeedersStatistics() With { _
                                                                       .INDEX = feedStats.INDEX, _
                                                                       .LOCATION = feedStats.LOCATION, _
                                                                       .MATERIAL_NAME = feedStats.MATERIAL_NAME, _
                                                                       .TOTAL_MASS = feedStats.TOTAL_MASS})
                        End If
                    End If
                Next


                Me.MixesTotal.CYCLE_MASS = .TOTAL_MASS
                Me.MixesTotal.CYCLE_TIME = .TOTAL_TIME
                Me.MixesTotal.CYCLE_RECYCLED_PERCENTAGE = .AVERAGE_RECYCLED_PERCENTAGE

                If (Not Double.IsNaN(.TOTAL_RECYCLED_MASS)) Then
                    Me.MixesTotal.CYCLE_RECYCLED_MASS = .TOTAL_RECYCLED_MASS
                End If

                Me.MixesTotal.ASPHALT_STATS.CYCLE_ASPHALT_MASS = .ASPHALT_STATS.TOTAL_MASS

            End With

        Next

    End Sub
End Class
