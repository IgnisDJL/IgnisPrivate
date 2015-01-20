''' <summary>
''' Contains the data corresponding to a single cycle of the production.
''' </summary>
''' <remarks>
''' Every cycle is linked to it's previous cycle through the PREVIOUS_CYCLE attribute.
''' The only cycle without a previous cycle is the first cycle of the day.
''' </remarks>
Public Class Cycle

    ''' <summary>The cycle that comes right before this cycle in chronogical order</summary>
    Private previousCycle As Cycle

    ''' <summary>
    ''' Constructs and return a Cycle object
    ''' </summary>
    ''' <param name="previousCycle">The cycle that comes right before this cycle in chronological order. Can be nothing.</param>
    ''' <remarks></remarks>
    Public Sub New(previousCycle As Cycle)
        Me.previousCycle = previousCycle
    End Sub

    ''' <summary>
    ''' Return's the cycle's property that corresponds with the given tag.
    ''' </summary>
    ''' <param name="tagName">The tag that determinates wich property/attribute of the cycle to return.</param>
    ''' <returns>The value of this cycle's property that corresponds to the given tag. Can be of type System.Double, System.String or System.Timespan</returns>
    ''' <remarks>If no property matches the tag, an exception is thrown</remarks>
    Public Overridable Overloads Function getData(tagName As Tag) As Object

        Select Case tagName

            Case Cycle.CYCLE_ID_1_TAG
                Return CYCLE_ID_1

            Case Cycle.DATE_TAG
                Return DATE_

            Case Cycle.TIME_TAG
                Return TIME

            Case Cycle.DURATION_TAG
                Return DURATION

            Case Cycle.MIX_MASS_TAG
                Return MIX_MASS

            Case Cycle.MIX_ACCUMULATED_MASS_TAG
                Return MIX_ACCUMULATED_MASS

            Case Cycle.PRODUCTION_SPEED_TAG
                Return PRODUCTION_SPEED

            Case Cycle.ASPHALT_MASS_TAG
                Return ASPHALT_MASS

            Case Cycle.ASPHALT_ACCUMULATED_MASS_TAG
                Return ASPHALT_ACCUMULATED_MASS

            Case Cycle.RECYCLED_MASS_TAG
                Return RECYCLED_MASS

            Case Cycle.AGGREGATES_MASS_TAG
                Return AGGREGATES_MASS

            Case Cycle.FUEL_QUANTITY_TAG
                Return FUEL_QUANTITY

            Case Cycle.MIX_NAME_TAG
                Return MIX_NAME

            Case Cycle.MIX_FORMULA_NAME_TAG
                Return FORMULA_NAME

            Case Cycle.ASPHALT_NAME_TAG
                Return ASPHALT_NAME

            Case Cycle.ASPHALT_TANK_TAG
                Return ASPHALT_TANK

            Case Cycle.TEMPERATURE_TAG
                Return TEMPERATURE

            Case Cycle.SET_POINT_TEMPERATURE_TAG
                Return SET_POINT_TEMPERATURE

            Case Cycle.TEMPERATURE_VARIATION_TAG
                Return TEMPERATURE_VARIATION

            Case Cycle.ASPHALT_PERCENTAGE_TAG
                Return ASPHALT_PERCENTAGE

            Case Cycle.ASPHALT_SET_POINT_PERCENTAGE_TAG
                Return ASPHALT_SET_POINT_PERCENTAGE

            Case Cycle.ASPHALT_PERCENTAGE_VARIATION_TAG
                Return ASPHALT_PERCENTAGE_VARIATION

            Case Cycle.RECYCLED_PERCENTAGE_TAG
                Return RECYCLED_PERCENTAGE

            Case Cycle.RECYCLED_SET_POINT_PERCENTAGE_TAG
                Return RECYCLED_SET_POINT_PERCENTAGE

            Case Else
                Throw New InvalidTagException("This tag is invalid : " & tagName.TAG_NAME)

        End Select

    End Function

    ''' <summary>
    ''' Gets the cycle's property that corresponds to the given sub column tag from the feed corresponding to the given super column tag and the given index.
    ''' </summary>
    ''' <param name="superColumnTag">The tag that corresponds to the desired value superColumn</param>
    ''' <param name="columnIndex">The index of the desired value's superColumn</param>
    ''' <param name="subColumnTag">The tag that corresponds to the desired value</param>
    ''' <returns>The value of the desired property as a System.Double or System.String</returns>
    ''' <remarks></remarks>
    Public Overridable Overloads Function getData(superColumnTag As Tag, columnIndex As Integer, subColumnTag As Tag) As Object


        Select Case superColumnTag

            Case Cycle.HOT_FEED_TAG
                For Each feed In Me.HOT_FEEDS
                    If (feed.INDEX = columnIndex) Then
                        Return feed.getData(subColumnTag)
                    End If
                Next

                For Each feed In Me.ASPHALT_FEEDS
                    If (feed.INDEX = columnIndex) Then
                        Return feed.getData(subColumnTag)
                    End If
                Next

            Case Cycle.ASPHALT_SUMMARY_FEED_TAG
                Return Me.ASPHALT_SUMMARY_FEED.getData(subColumnTag)

            Case Cycle.RECYCLE_SUMMARY_FEED_TAG
                Return Me.RECYCLE_SUMMARY_FEED.getData(subColumnTag)

            Case Cycle.FILLER_SUMMARY_FEED_TAG
                Return Me.FILLER_SUMMARY_FEED.getData(subColumnTag)

            Case Cycle.COLD_FEED_TAG
                For Each feed In Me.COLD_FEEDS
                    If (feed.INDEX = columnIndex) Then
                        Return feed.getData(subColumnTag)
                    End If
                Next

            Case Else

                Throw New InvalidTagException("This tag is invalid : " & superColumnTag.TAG_NAME)

        End Select

        ' Invalid index or no coldFeed/hotfeed with this particular index in the cycle

        Return Nothing
    End Function

    ''' <summary>This cycle's previous cycle following the chronological order of prodution</summary>
    Public ReadOnly Property PREVIOUS_CYCLE As Cycle
        Get
            Return Me.previousCycle
        End Get
    End Property

    ''' <summary>The tag corresponding to the cycle's hotfeeds list</summary>
    Public Shared ReadOnly HOT_FEED_TAG As Tag = New Tag("#HotFeed", "Benne chaude", Unit.NO_UNIT, True)

    ''' <summary>This cycle's hotfeeds list</summary>
    Private hotFeeds As New List(Of Feeder)

    ''' <summary>Gets this cycle's hotfeeds list</summary>
    Public ReadOnly Property HOT_FEEDS As List(Of Feeder)
        Get
            Return Me.hotFeeds
        End Get
    End Property

    ' Contains all the asphalt feeds (should only be accessed to store and recover asphalt feeds)
    Private asphaltFeeds As New List(Of Feeder)
    Public ReadOnly Property ASPHALT_FEEDS As List(Of Feeder)
        Get
            Return Me.asphaltFeeds
        End Get
    End Property

    ''' <summary>The tag corresponding to the cycle's ASPHALT_SUMMARY_FEED property</summary>
    Public Shared ReadOnly ASPHALT_SUMMARY_FEED_TAG As Tag = New Tag("#AsphaltFeed", "Réservoir bitume sommaire", Unit.NO_UNIT, False)

    ''' <summary>This cycle's asphalt feeds represented as one single feed</summary>
    ''' <remarks>Instanciate in own cycle constructor</remarks>
    Public Property ASPHALT_SUMMARY_FEED As Feeder

    ''' <summary>The tag corresponding to the cycle's RECYCLE_SUMMARY_FEED proprety</summary>
    Public Shared ReadOnly RECYCLE_SUMMARY_FEED_TAG As Tag = New Tag("#RecycleFeed", "Réservoir recyclé sommaire", Unit.NO_UNIT, False)

    ''' <summary>This cycle's recycle feeds represented as one single feed</summary>
    ''' <remarks>Instanciate in own cycle constructor</remarks>
    Public Property RECYCLE_SUMMARY_FEED As Feeder

    ''' <summary>The tag corresponding to the cycle's FILLER_SUMMARY_FEED property</summary>
    Public Shared ReadOnly FILLER_SUMMARY_FEED_TAG As Tag = New Tag("#FillerFeed", "Réservoir filler sommaire", Unit.NO_UNIT, False)

    ''' <summary>This cycle's filler feeds represented as one single feed</summary>
    ''' <remarks>Instanciate in own cycle constructor</remarks>
    Public Property FILLER_SUMMARY_FEED As Feeder

    ''' <summary>The tag corresponding to the cycle's cold feeds list</summary>
    Public Shared ReadOnly COLD_FEED_TAG As Tag = New Tag("#ColdFeed", "Benne froide", Unit.NO_UNIT, True)

    ''' <summary>This cycle's coldfeeds list</summary>
    Private coldFeeds As New List(Of Feeder)

    ''' <summary>Gets this cycle's coldfeeds list</summary>
    Public ReadOnly Property COLD_FEEDS As List(Of Feeder)
        Get
            Return Me.coldFeeds
        End Get
    End Property

    '====================================================================='
    ' Time Related '
    '====================================================================='
    ''' <summary>The date of the cycle's production day</summary>
    Public Property DATE_ As Date

    Public Property TIME As Date

    Public Overridable Property DURATION As TimeSpan

    ' --------Constants----------- '
    Public Shared ReadOnly DATE_TAG As Tag = New Tag("#Date", "Date", Unit.NO_UNIT, False)

    Public Shared ReadOnly TIME_TAG As Tag = New Tag("#Time", "Heure", Unit.NO_UNIT, False)

    Public Shared ReadOnly DURATION_TAG As Tag = New Tag("#Duration", "Durée", Unit.NO_UNIT, False)

    '====================================================================='
    ' Quantity Related '
    '====================================================================='
    Public Property MIX_MASS As Double

    Public Overridable Property MIX_ACCUMULATED_MASS As Double = Double.NaN

    Public Overridable Property PRODUCTION_SPEED As Double

    Public Property ASPHALT_MASS As Double

    Public Property ASPHALT_ACCUMULATED_MASS As Double

    Public Property RECYCLED_MASS As Double = Double.NaN

    Public Property AGGREGATES_MASS As Double

    Public Property FUEL_QUANTITY As Double = Double.NaN

    ' --------Shared ReadOnlyants----------- '
    Public Shared ReadOnly MIX_MASS_TAG As Tag = New Tag("#MixMass", "Masse totale", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly MIX_ACCUMULATED_MASS_TAG As Tag = New Tag("#MixAccumulatedMass", "Masse totale accumulée", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly PRODUCTION_SPEED_TAG As Tag = New Tag("#ProductionSpeed", "Taux de production", Unit.DEFAULT_PRODUCTION_SPEED_UNIT, False)

    Public Shared ReadOnly ASPHALT_MASS_TAG As Tag = New Tag("#AsphaltMass", "Masse bitume", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly ASPHALT_ACCUMULATED_MASS_TAG As Tag = New Tag("#AsphaltAccumulatedMass", "Masse bitume accumulé", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly RECYCLED_MASS_TAG As Tag = New Tag("#RecycledMass", "Masse de recyclé", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly AGGREGATES_MASS_TAG As Tag = New Tag("#AggregatesTotalMass", "Masse granulats", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly FUEL_QUANTITY_TAG As Tag = New Tag("#FuelQuantity", "Quantité carburant", Unit.NO_UNIT, False) ' No fuel unit implemented yet

    '====================================================================='
    ' Description Related '
    '====================================================================='
    Public ReadOnly Property CYCLE_ID_1
        Get
            Return If(IsNothing(Me.PREVIOUS_CYCLE), 1, Me.PREVIOUS_CYCLE.CYCLE_ID_1 + 1)
        End Get
    End Property

    Private mixName As String
    Public Property MIX_NAME As String
        Get
            Return If(IsNothing(mixName), Me.FORMULA_NAME, mixName)
        End Get
        Set(value As String)
            Me.mixName = value
        End Set
    End Property

    Public Overridable Property FORMULA_NAME As String

    Public Property ASPHALT_NAME As String

    Public Overridable Property ASPHALT_TANK As String

    ' --------Constants----------- '
    Public Shared ReadOnly CYCLE_ID_1_TAG As Tag = New Tag("#CycleID_1", "Numéro de cycle (base 1)", Unit.NO_UNIT, False)

    Public Shared ReadOnly MIX_NAME_TAG As Tag = New Tag("#MixName", "Enrobé", Unit.NO_UNIT, False)

    Public Shared ReadOnly MIX_FORMULA_NAME_TAG As Tag = New Tag("#FormulaName", "Formule", Unit.NO_UNIT, False)

    Public Shared ReadOnly ASPHALT_NAME_TAG As Tag = New Tag("#AsphaltName", "Grade bitume", Unit.NO_UNIT, False)

    Public Shared ReadOnly ASPHALT_TANK_TAG As Tag = New Tag("#AsphaltTank", "Réservoir bitume", Unit.NO_UNIT, False)

    '====================================================================='
    ' Temperature Related '
    '====================================================================='
    Public Property TEMPERATURE As Double = Double.NaN

    Public Overridable Property SET_POINT_TEMPERATURE As Double = Double.NaN

    ' --------Calculated----------- '
    Public ReadOnly Property TEMPERATURE_VARIATION As Double
        Get
            If (Not Double.IsNaN(Me.SET_POINT_TEMPERATURE) AndAlso Not Double.IsNaN(Me.TEMPERATURE)) Then
                Return Me.TEMPERATURE - Me.SET_POINT_TEMPERATURE
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    ' --------Constants----------- '
    Public Shared ReadOnly TEMPERATURE_TAG As Tag = New Tag("#MixTemperature", "Température", Unit.DEFAULT_TEMPERATURE_UNIT, False)

    Public Shared ReadOnly SET_POINT_TEMPERATURE_TAG As Tag = New Tag("#MixSetPointTemperature", "Température visée", Unit.DEFAULT_TEMPERATURE_UNIT, False)

    Public Shared ReadOnly TEMPERATURE_VARIATION_TAG As Tag = New Tag("#MixTemperatureVariation", "Variation de température", Unit.DEFAULT_TEMPERATURE_UNIT, False)

    '====================================================================='
    ' Percentage Related '
    '====================================================================='
    Public Overridable Property ASPHALT_PERCENTAGE As Double = Double.NaN

    Public Property ASPHALT_SET_POINT_PERCENTAGE As Double = Double.NaN

    Public Overridable Property RECYCLED_PERCENTAGE As Double = Double.NaN

    Public Property RECYCLED_SET_POINT_PERCENTAGE As Double = Double.NaN

    ' --------Calculated----------- '
    Public ReadOnly Property ASPHALT_PERCENTAGE_VARIATION As Double
        Get
            If (Not Double.IsNaN(Me.ASPHALT_SET_POINT_PERCENTAGE) AndAlso Not Double.IsNaN(Me.ASPHALT_PERCENTAGE)) Then
                Return Me.ASPHALT_PERCENTAGE - Me.ASPHALT_SET_POINT_PERCENTAGE
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    ' --------Constants----------- '
    Public Shared ReadOnly ASPHALT_PERCENTAGE_TAG As Tag = New Tag("#AsphaltPercentage", "Pourcentage bitume", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly ASPHALT_SET_POINT_PERCENTAGE_TAG As Tag = New Tag("#AsphaltSetPointPercentage", "Pourcentage bitume visé", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly ASPHALT_PERCENTAGE_VARIATION_TAG As Tag = New Tag("#AsphaltPercentageVariation", "Variation pourcentage bitume", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly RECYCLED_PERCENTAGE_TAG As Tag = New Tag("#RecycledPercentage", "Pourcentage recyclé", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly RECYCLED_SET_POINT_PERCENTAGE_TAG As Tag = New Tag("#RecycledSetPointPercentage", "Pourcentage recyclé visé", Unit.DEFAULT_PERCENT_UNIT, False)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Keep in alphabetical order
    ''' </remarks>
    Public Shared ReadOnly TAGS As Tag() = {CYCLE_ID_1_TAG, _
                                            HOT_FEED_TAG, _
                                            ASPHALT_SUMMARY_FEED_TAG, _
                                            RECYCLE_SUMMARY_FEED_TAG, _
                                            FILLER_SUMMARY_FEED_TAG, _
                                            COLD_FEED_TAG, _
                                            DATE_TAG, _
                                            TIME_TAG, _
                                            DURATION_TAG, _
                                            MIX_MASS_TAG, _
                                            MIX_ACCUMULATED_MASS_TAG, _
                                            PRODUCTION_SPEED_TAG, _
                                            ASPHALT_MASS_TAG, _
                                            ASPHALT_ACCUMULATED_MASS_TAG, _
                                            RECYCLED_MASS_TAG, _
                                            AGGREGATES_MASS_TAG, _
                                            FUEL_QUANTITY_TAG, _
                                            MIX_NAME_TAG, _
                                            MIX_FORMULA_NAME_TAG, _
                                            ASPHALT_NAME_TAG, _
                                            ASPHALT_TANK_TAG, _
                                            TEMPERATURE_TAG, _
                                            SET_POINT_TEMPERATURE_TAG, _
                                            TEMPERATURE_VARIATION_TAG, _
                                            ASPHALT_PERCENTAGE_TAG, _
                                            ASPHALT_SET_POINT_PERCENTAGE_TAG, _
                                            ASPHALT_PERCENTAGE_VARIATION_TAG, _
                                            RECYCLED_PERCENTAGE_TAG, _
                                            RECYCLED_SET_POINT_PERCENTAGE_TAG}


    Public Overrides Function ToString() As String
        Return "Cycle # " & Me.CYCLE_ID_1
    End Function

    Public Shared Function isset(cycleValue As Double) As Boolean
        Return Not Double.IsNaN(cycleValue)
    End Function
End Class
