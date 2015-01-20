''' <summary>
''' A cycle object that corresponds to a single cycle from a .mdb database produced by a Marcotte system.
''' </summary>
Public Class MDBCycle
    Inherits Cycle
    Implements ICloneable

    ''' <summary>The mdb cycle that comes before this cycle</summary>
    Private previousCycle As MDBCycle

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="previousCycle">The mdb cycle that comes right before this cycle</param>
    ''' <remarks></remarks>
    Public Sub New(previousCycle As MDBCycle)
        MyBase.New(previousCycle)

        Me.ASPHALT_SUMMARY_FEED = New MDBFeeder(Me)
        Me.RECYCLE_SUMMARY_FEED = New MDBFeeder(Me)
        Me.FILLER_SUMMARY_FEED = New MDBFeeder(Me)

        Me.previousCycle = previousCycle

    End Sub

    ''' <summary>
    ''' Gets the cycle's property that corresponds with the tag and the index.
    ''' </summary>
    ''' <param name="tag">The tag that determines which property is returned</param>
    ''' <returns>The cycle's corresponding property as a System.Double, as a System.String or as a System.TimeSpan</returns>
    ''' <remarks>If the tag doesn't correspond to a property of the MDBCycle class, the Cycle.getData(tag) is called</remarks>
    Public Overrides Function getData(tag As Tag) As Object

        Select Case tag

            Case CYCLE_ID_TAG
                Return CYCLE_ID

            Case COMMAND_ID_TAG
                Return COMMAND_ID

            Case TRUCK_ID_TAG
                Return TRUCK_ID

            Case DRY_MALAXING_TIME_TAG
                Return DRY_MALAXING_TIME

            Case TOTAL_MALAXING_TIME_TAG
                Return TOTAL_MALAXING_TIME

            Case SET_POINT_MASS_TAG
                Return SET_POINT_MASS

            Case RECIPE_QUANTITY_TAG
                Return RECIPE_QUANTITY

            Case Else
                Return MyBase.getData(tag)

        End Select

    End Function

    ''' <summary>
    ''' Adds a hot feed to the cycle's hot feeds list
    ''' </summary>
    ''' <param name="hotFeed">The hot feed to be added.</param>
    Public Sub addHotFeed(hotFeed As MDBFeeder)


        Dim keepLooping As Boolean = True   ' Indicated when to stop looping through this cycle's previous cycles
        Dim cycle As MDBCycle               ' Stores the previous cycle's while looping through them


        With hotFeed

            ' Add the feed's mass to this cycle's total mass
            Me.MIX_MASS += .MASS

            ' If the hot feed is a recycled feed
            If (.IS_RECYCLED) Then

                ' Add it's mass to the cycle's aggregates mass
                Me.AGGREGATES_MASS += .MASS

                ' Set this cycle's recycled related properties
                Me.RECYCLED_MASS = .MASS
                Me.RECYCLED_SET_POINT_PERCENTAGE = .RECIPE_MASS / Me.RECIPE_QUANTITY * 100

                ' Set the hotfeed's set point percentage
                .SET_POINT_PERCENTAGE = .RECIPE_MASS / Me.RECIPE_QUANTITY * 100

                ' --------------------------------------------------- '
                ' --------------------------------------------------- '

                Me.RECYCLE_SUMMARY_FEED.MASS = If(Double.IsNaN(Me.RECYCLE_SUMMARY_FEED.MASS), .MASS, Me.RECYCLE_SUMMARY_FEED.MASS + .MASS)

                If (IsNothing(Me.RECYCLE_SUMMARY_FEED.LOCATION)) Then

                    Me.RECYCLE_SUMMARY_FEED.LOCATION = .LOCATION

                    Me.RECYCLE_SUMMARY_FEED.MATERIAL_NAME = .MATERIAL_NAME

                    Me.RECYCLE_SUMMARY_FEED.MASS = .MASS

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                    DirectCast(Me.RECYCLE_SUMMARY_FEED, MDBFeeder).RECIPE_MASS = .RECIPE_MASS

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_PERCENTAGE = .SET_POINT_PERCENTAGE

                    Me.RECYCLE_SUMMARY_FEED.ACCUMULATED_MASS = If(IsNothing(Me.PREVIOUS_CYCLE), .MASS, Me.PREVIOUS_CYCLE.ASPHALT_SUMMARY_FEED.ACCUMULATED_MASS + .MASS)


                Else

                    Me.RECYCLE_SUMMARY_FEED.LOCATION = Me.RECYCLE_SUMMARY_FEED.LOCATION & " + " & .LOCATION

                    Me.RECYCLE_SUMMARY_FEED.MATERIAL_NAME = Me.RECYCLE_SUMMARY_FEED.MATERIAL_NAME & " + " & .MATERIAL_NAME

                    Me.RECYCLE_SUMMARY_FEED.MASS += .MASS

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                    DirectCast(Me.RECYCLE_SUMMARY_FEED, MDBFeeder).RECIPE_MASS += .RECIPE_MASS

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_PERCENTAGE += .SET_POINT_PERCENTAGE

                    Me.RECYCLE_SUMMARY_FEED.ACCUMULATED_MASS += .MASS

                End If

                ' --------------------------------------------------- '
                ' --------------------------------------------------- '


                ' Loop through this cycle's previous cycles because it is not sure that all the cycles contain this particular hot feed
                cycle = Me
                keepLooping = True

                While (keepLooping) ' #refactor - keep a feedsummary list as a static attr (or singleton with date (getInstance(date))

                    ' If no previous cycle contains this particular hot feed
                    If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

                        .ACCUMULATED_MASS = .MASS
                        keepLooping = False

                    Else

                        cycle = DirectCast(cycle.PREVIOUS_CYCLE, MDBCycle)

                        ' Check the previous cycle's hot feeds to find a the same hot feed
                        For Each feed In cycle.HOT_FEEDS

                            If (.INDEX.Equals(feed.INDEX)) Then

                                .ACCUMULATED_MASS = feed.ACCUMULATED_MASS + .MASS   ' Increment the feed's accumulated mass

                                keepLooping = False
                                Exit For
                            End If

                        Next

                    End If

                End While

                ' Add the hot feed to the list
                Me.HOT_FEEDS.Add(hotFeed)

                ' The hot feed contain's asphalt
            ElseIf (.IS_ASPHALT) Then

                Me.ASPHALT_NAME = .MATERIAL_NAME
                Me.ASPHALT_TANK = .LOCATION
                Me.ASPHALT_MASS = .MASS

                .SET_POINT_PERCENTAGE = .RECIPE_MASS / Me.RECIPE_QUANTITY * 100
                Me.ASPHALT_SET_POINT_PERCENTAGE = .SET_POINT_PERCENTAGE

                ' Increment this cycle's asphalt accumulated mass
                If (IsNothing(Me.PREVIOUS_CYCLE)) Then
                    Me.ASPHALT_ACCUMULATED_MASS = .MASS
                Else
                    Me.ASPHALT_ACCUMULATED_MASS = Me.PREVIOUS_CYCLE.ASPHALT_ACCUMULATED_MASS + .MASS
                End If

                ' Set the cycle's asphalt feed
                'Me.ASPHALT_SUMMARY_FEED = hotFeed

                ' Replace the lines at the top with

                ' If it is the first asphalt feed of the cycle
                If (IsNothing(Me.ASPHALT_SUMMARY_FEED.LOCATION)) Then

                    Me.ASPHALT_SUMMARY_FEED.LOCATION = .LOCATION

                    Me.ASPHALT_SUMMARY_FEED.MATERIAL_NAME = .MATERIAL_NAME

                    Me.ASPHALT_SUMMARY_FEED.MASS = .MASS

                    Me.ASPHALT_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                    DirectCast(Me.ASPHALT_SUMMARY_FEED, MDBFeeder).RECIPE_MASS = .RECIPE_MASS

                    Me.ASPHALT_SUMMARY_FEED.SET_POINT_PERCENTAGE = .SET_POINT_PERCENTAGE
                  
                    Me.ASPHALT_SUMMARY_FEED.ACCUMULATED_MASS = If(IsNothing(Me.PREVIOUS_CYCLE), .MASS, Me.PREVIOUS_CYCLE.ASPHALT_SUMMARY_FEED.ACCUMULATED_MASS + .MASS)


                Else ' This is not the first asphalt feed of the cycle (multiple asphalt feeds)

                    Me.ASPHALT_SUMMARY_FEED.LOCATION = Me.ASPHALT_SUMMARY_FEED.LOCATION & " + " & .LOCATION

                    Me.ASPHALT_SUMMARY_FEED.MATERIAL_NAME = Me.ASPHALT_SUMMARY_FEED.MATERIAL_NAME & " + " & .MATERIAL_NAME

                    Me.ASPHALT_SUMMARY_FEED.MASS += .MASS

                    Me.ASPHALT_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                    DirectCast(Me.ASPHALT_SUMMARY_FEED, MDBFeeder).RECIPE_MASS += .RECIPE_MASS

                    Me.ASPHALT_SUMMARY_FEED.SET_POINT_PERCENTAGE += .SET_POINT_PERCENTAGE

                    Me.ASPHALT_SUMMARY_FEED.ACCUMULATED_MASS += .MASS

                End If

                ' Loop through this cycle's previous cycles because it is not sure that all the cycles contain this particular asphalt feed
                cycle = Me
                keepLooping = True

                While (keepLooping)

                    ' If no previous cycle contains this particular hot feed
                    If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

                        .ACCUMULATED_MASS = .MASS
                        keepLooping = False

                    Else

                        cycle = DirectCast(cycle.PREVIOUS_CYCLE, MDBCycle)

                        ' Check the previous cycle's hot feeds to find a the same hot feed
                        For Each feed In cycle.ASPHALT_FEEDS

                            If (.INDEX.Equals(feed.INDEX)) Then

                                .ACCUMULATED_MASS = feed.ACCUMULATED_MASS + .MASS   ' Increment the feed's accumulated mass

                                keepLooping = False
                                Exit For
                            End If

                        Next

                    End If

                End While


                Me.ASPHALT_FEEDS.Add(hotFeed)


            Else ' The hot feed's contain's some sort of aggregate (aggregate, filler or surplus)

                Me.AGGREGATES_MASS += .MASS

                .SET_POINT_PERCENTAGE = .RECIPE_MASS / Me.RECIPE_QUANTITY * 100

                ' Loop through this cycle's previous cycles because it is not sure that all the cycles contain this particular hot feed
                cycle = Me
                While (keepLooping)

                    ' If no previous cycle contains this particular hot feed
                    If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

                        .ACCUMULATED_MASS = .MASS
                        keepLooping = False

                    Else

                        cycle = DirectCast(cycle.PREVIOUS_CYCLE, MDBCycle)

                        ' Check the previous cycle's hot feeds to find a the same hot feed
                        For Each feed In cycle.HOT_FEEDS

                            If (.INDEX.Equals(feed.INDEX)) Then

                                .ACCUMULATED_MASS = feed.ACCUMULATED_MASS + .MASS

                                keepLooping = False
                                Exit For

                            End If
                        Next

                    End If

                End While

                If (.IS_FILLER) Then ' Filler's contant index

                    With hotFeed

                        ' If it is the first asphalt feed of the cycle
                        If (IsNothing(Me.FILLER_SUMMARY_FEED.LOCATION)) Then

                            Me.FILLER_SUMMARY_FEED.LOCATION = .LOCATION

                            Me.FILLER_SUMMARY_FEED.MATERIAL_NAME = .MATERIAL_NAME

                            Me.FILLER_SUMMARY_FEED.MASS = .MASS

                            Me.FILLER_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                            DirectCast(Me.FILLER_SUMMARY_FEED, MDBFeeder).RECIPE_MASS = .RECIPE_MASS

                            Me.FILLER_SUMMARY_FEED.SET_POINT_PERCENTAGE = .SET_POINT_PERCENTAGE

                            Me.FILLER_SUMMARY_FEED.ACCUMULATED_MASS = If(IsNothing(Me.PREVIOUS_CYCLE), .MASS, Me.PREVIOUS_CYCLE.FILLER_SUMMARY_FEED.ACCUMULATED_MASS + .MASS)

                        Else ' This is not the first asphalt feed of the cycle (multiple asphalt feeds)

                            Me.FILLER_SUMMARY_FEED.LOCATION = Me.FILLER_SUMMARY_FEED.LOCATION & " + " & .LOCATION

                            Me.FILLER_SUMMARY_FEED.MATERIAL_NAME = Me.FILLER_SUMMARY_FEED.MATERIAL_NAME & " + " & .MATERIAL_NAME

                            Me.FILLER_SUMMARY_FEED.MASS += .MASS

                            Me.FILLER_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                            DirectCast(Me.FILLER_SUMMARY_FEED, MDBFeeder).RECIPE_MASS += .RECIPE_MASS

                            Me.FILLER_SUMMARY_FEED.SET_POINT_PERCENTAGE += .SET_POINT_PERCENTAGE

                            Me.FILLER_SUMMARY_FEED.ACCUMULATED_MASS += .MASS

                        End If

                    End With

                ElseIf (Not .IS_AGGREGATE) Then ' Surplus constant index

                    '.INDEX = XmlSettings.Settings.instance.Usine.DataFiles.MDB.NUMBER_HOT_FEEDS

                End If

                ' Add the hot feed to the list
                Me.HOT_FEEDS.Add(hotFeed)

            End If

        End With

    End Sub

    ''' <summary>
    ''' Add's a cold feed to this cycle's cold feeds list.
    ''' </summary>
    ''' <param name="coldFeed">The cold feed to be added.</param>
    ''' <remarks></remarks>
    Public Sub addColdFeed(coldFeed As MDBFeeder)

        If (coldFeed.IS_RECYCLED) Then

            ' Set the feed's index to the last feed's index
            'coldFeed.INDEX = XmlSettings.Settings.instance.Usine.DataFiles.MDB.NUMBER_COLD_FEEDS


            ' Set this cycle's Recycled set point percentage

            With coldFeed

                Me.RECYCLED_SET_POINT_PERCENTAGE = .SET_POINT_PERCENTAGE

                ' If it is the first asphalt feed of the cycle
                If (IsNothing(Me.RECYCLE_SUMMARY_FEED.LOCATION)) Then

                    Me.RECYCLE_SUMMARY_FEED.LOCATION = .LOCATION

                    Me.RECYCLE_SUMMARY_FEED.MATERIAL_NAME = .MATERIAL_NAME

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_PERCENTAGE = .SET_POINT_PERCENTAGE

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS


                Else ' This is not the first asphalt feed of the cycle (multiple asphalt feeds)

                    Me.RECYCLE_SUMMARY_FEED.LOCATION = Me.RECYCLE_SUMMARY_FEED.LOCATION & " + " & .LOCATION

                    Me.RECYCLE_SUMMARY_FEED.MATERIAL_NAME = Me.RECYCLE_SUMMARY_FEED.MATERIAL_NAME & " + " & .MATERIAL_NAME

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_PERCENTAGE += .SET_POINT_PERCENTAGE

                    Me.RECYCLE_SUMMARY_FEED.SET_POINT_MASS = .SET_POINT_MASS

                End If

            End With

        End If

        ' Add the cold feed to the list
        Me.COLD_FEEDS.Add(coldFeed)

    End Sub

    Public Property CYCLE_ID As String

    Public Property COMMAND_ID As String

    Public Property TRUCK_ID As String

    Public Property DRY_MALAXING_TIME As TimeSpan

    Public Property TOTAL_MALAXING_TIME As TimeSpan

    Public Property SET_POINT_MASS As Double

    ' Default value = 1000, in case it is not found. 1000 is the only value we have seen this variable yet
    Public Property RECIPE_QUANTITY As Double = 1000

    ' --------Constants----------- '

    Public Shared ReadOnly CYCLE_ID_TAG As Tag = New Tag("#CycleID", "Numéro de cycle", Unit.NO_UNIT, False)

    Public Shared ReadOnly COMMAND_ID_TAG As Tag = New Tag("#CommandID", "Contrat", Unit.NO_UNIT, False)

    Public Shared ReadOnly TRUCK_ID_TAG As Tag = New Tag("#TruckID", "Camion", Unit.NO_UNIT, False)

    Public Shared ReadOnly DRY_MALAXING_TIME_TAG As Tag = New Tag("#DryMalaxTime", "Malaxage sec", Unit.NO_UNIT, False)

    Public Shared ReadOnly TOTAL_MALAXING_TIME_TAG As Tag = New Tag("#TotalMalaxTime", "Malaxage total", Unit.NO_UNIT, False)

    Public Shared ReadOnly SET_POINT_MASS_TAG As Tag = New Tag("#MixSetPointMass", "Masse visée", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly RECIPE_QUANTITY_TAG As Tag = New Tag("#RecipeQuantity", "Masse recette", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared Shadows ReadOnly TAGS As Tag() = {CYCLE_ID_TAG, _
                                                    COMMAND_ID_TAG, _
                                                    TRUCK_ID_TAG, _
                                                    DRY_MALAXING_TIME_TAG, _
                                                    TOTAL_MALAXING_TIME_TAG, _
                                                    SET_POINT_MASS_TAG, _
                                                    RECIPE_QUANTITY_TAG}
    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Clone() As Object Implements ICloneable.Clone
        Return Me.MemberwiseClone
    End Function

End Class
