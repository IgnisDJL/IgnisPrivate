''' <summary>
''' A cycle object that corresponds to a single cycle from a .log data file produced by a Minds system.
''' </summary>
Public Class LOGCycle
    Inherits Cycle

    ''' <summary>The log cycle that comes before this cycle</summary>
    Private previousCycle As LOGCycle

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="previousCycle">The log cycle that comes right before this cycle</param>
    ''' <remarks></remarks>
    Public Sub New(previousCycle As LOGCycle)
        MyBase.New(previousCycle)

        Me.previousCycle = previousCycle

    End Sub

    ''' <summary>
    ''' Gets the cycle's property that corresponds with the tag and the index.
    ''' </summary>
    ''' <param name="tag">The tag that determines which property is returned</param>
    ''' <returns>The cycle's corresponding property as a System.Double, as a System.String or as a System.TimeSpan</returns>
    ''' <remarks>If the tag doesn't correspond to a property of the LOGCycle class, the Cycle.getData(tag) is called</remarks>
    Public Overrides Function getData(tag As Tag) As Object

        Select Case tag

            Case DENSITY_TAG
                Return DENSITY

            Case BAG_HOUSE_DIFF_TAG
                Return BAG_HOUSE_DIFF

            Case TOTAL_ACCUMULATED_TONS_TAG
                Return TOTAL_ACCUMULATED_TONS

            Case ASPHALT_TEMPERATURE_TAG
                Return ASPHALT_TEMPERATURE

            Case SILO_FILLING_TAG
                Return SILO_FILLING

            Case DUST_REMOVAL_TAG
                Return DUST_REMOVAL

            Case Else
                Return MyBase.getData(tag)

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
    Public Overrides Function getData(superColumnTag As Tag, columnIndex As Integer, subColumnTag As Tag) As Object

        Select Case superColumnTag

            Case LOGCycle.VIRGIN_AGGREGATES_FEEDER_TAG
                Return Me.VIRGIN_AGGREGATES_FEEDER.getData(subColumnTag)

            Case LOGCycle.RAP_ASPHALT_FEEDER_TAG
                Return Me.RAP_ASPHALT_FEEDER.getData(subColumnTag)

            Case LOGCycle.VIRGIN_ASPHALT_FEEDER_TAG
                Return Me.VIRGIN_ASPHALT_FEEDER.getData(subColumnTag)

            Case LOGCycle.ADDITIVE_FEEDER_TAG
                Return Me.ADDITIVE_FEEDER.getData(subColumnTag)

            Case Else

                Return MyBase.getData(superColumnTag, columnIndex, subColumnTag)

        End Select

        ' Invalid index or no coldFeed/hotfeed with this particular index in the cycle

        Return Nothing
    End Function

    ''' <summary>The cycle's mix density</summary>
    Public Property DENSITY As Double

    ''' <summary>The cycle's bag house temperature differential</summary>
    Public Property BAG_HOUSE_DIFF As Double

    ''' <summary>The cycle's accumulated tons logged by the asphalt plant's system since it's start</summary>
    ''' <remarks>
    ''' DO NOT MISTAKE FOR THE ACCUMULATED MASS PROPERTY
    ''' This value is not accurate for an accumulated mass because it reset's randomly in the data files
    ''' </remarks>
    Public Property TOTAL_ACCUMULATED_TONS As Double

    ''' <summary>The cycle's asphalt temperature</summary>
    Public Property ASPHALT_TEMPERATURE As Double

    ''' <summary>The silo in which the cycle belongs</summary>
    Public Property SILO_FILLING As String

    ''' <summary>The cycle's the speed of the cycle's dust removal. Usually in T/h</summary>
    Public Property DUST_REMOVAL As Double

    ' --------Constants----------- '

    ''' <summary>The tag corresponding to the cycle's DENSITY property</summary>
    Public Shared ReadOnly DENSITY_TAG As Tag = New Tag("#Density", "Densité", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's BAG_HOUSE_DIFF property</summary>
    Public Shared ReadOnly BAG_HOUSE_DIFF_TAG As Tag = New Tag("#BagHouseDiff", "Diff. temp. bag house", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's TOTAL_ACCUMULATED_TONS property</summary>
    Public Shared ReadOnly TOTAL_ACCUMULATED_TONS_TAG As Tag = New Tag("#TotalAccumulatedTons", "Masse accumulée totale", Unit.DEFAULT_MASS_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's ASPHALT_TEMPERATURE property</summary>
    Public Shared ReadOnly ASPHALT_TEMPERATURE_TAG As Tag = New Tag("#AsphaltTemperature", "Température bitume", Unit.DEFAULT_TEMPERATURE_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's SILO_FILLING property</summary>
    Public Shared ReadOnly SILO_FILLING_TAG As Tag = New Tag("#Silo", "Silo", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's DUST_REMOVAL property</summary>
    Public Shared ReadOnly DUST_REMOVAL_TAG As Tag = New Tag("#DustRemoval", "Dépoussiérage", Unit.NO_UNIT, False)

    Public Shared ReadOnly VIRGIN_AGGREGATES_FEEDER_TAG As Tag = New Tag("#VirginAggFeed", "Aggrégats vièrges", Unit.NO_UNIT, True)

    Public Shared ReadOnly RAP_ASPHALT_FEEDER_TAG As Tag = New Tag("#RAPACFeed", "Bitume recyclé", Unit.NO_UNIT, True)

    Public Shared ReadOnly VIRGIN_ASPHALT_FEEDER_TAG As Tag = New Tag("#VirginACFeed", "Bitume vièrges", Unit.NO_UNIT, True)

    Public Shared ReadOnly ADDITIVE_FEEDER_TAG As Tag = New Tag("#AddFeed", "Additifs", Unit.NO_UNIT, True)

    ' Explain how rapAgg = recycle_summ_feed and totalAc = asph_summ_feed and filler = ... (talking about the column names of the hotfeeds in the datafiles)
    Public Property VIRGIN_AGGREGATES_FEEDER As Feeder

    Public Property RAP_ASPHALT_FEEDER As Feeder

    Public Property VIRGIN_ASPHALT_FEEDER As Feeder

    Public Property ADDITIVE_FEEDER As Feeder


    ''' <summary>An array containing all of the CSVCycle's tags</summary>
    ''' <remarks>
    ''' Pre-condition : All the Tag objects this array contains have to be initialized before the array.
    ''' </remarks>
    Public Shared Shadows TAGS As Tag() = {DENSITY_TAG, _
                                           BAG_HOUSE_DIFF_TAG, _
                                           TOTAL_ACCUMULATED_TONS_TAG, _
                                           ASPHALT_TEMPERATURE_TAG, _
                                           SILO_FILLING_TAG, _
                                           DUST_REMOVAL_TAG, _
                                           VIRGIN_AGGREGATES_FEEDER_TAG, _
                                           RAP_ASPHALT_FEEDER_TAG, _
                                           VIRGIN_ASPHALT_FEEDER_TAG, _
                                           ADDITIVE_FEEDER_TAG}
End Class
