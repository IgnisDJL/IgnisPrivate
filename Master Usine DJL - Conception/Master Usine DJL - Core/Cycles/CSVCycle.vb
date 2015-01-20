''' <summary>
''' A cycle object that corresponds to a single cycle from a .csv file produced by a Minds system.
''' </summary>
Public Class CSVCycle
    Inherits Cycle

    ''' <summary>The csv cycle that comes before this cycle</summary>
    Private previousCycle As CSVCycle

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="previousCycle">The csv cycle that comes right before this cycle</param>
    ''' <remarks></remarks>
    Public Sub New(previousCycle As CSVCycle)
        MyBase.New(previousCycle)

        Me.previousCycle = previousCycle

    End Sub

    ''' <summary>
    ''' Gets the cycle's property that corresponds with the tag and the index.
    ''' </summary>
    ''' <param name="tag">The tag that determines which property is returned</param>
    ''' <param name="index">The property's index, if it is indexed</param>
    ''' <returns>The cycle's corresponding property as a System.Double, as a System.String or as a System.TimeSpan</returns>
    ''' <remarks>If the tag doesn't correspond to a property of the CSVCycle class, the Cycle.getData(tag) is called</remarks>
    Public Overloads Function getData(tag As Tag, index As Integer) As Object

        Select Case tag

            Case ID_TAG
                Return ID

            Case TRUCK_ID_TAG
                Return TRUCK_ID

            Case COMMAND_ID_TAG
                Return COMMAND_ID

            Case AGGREGATE_MASS_TAG
                If (index > 0) Then

                    Return Me.AGGREGATE_MASS(index - 1)

                End If

                Return Nothing

            Case AGGREGATE_PERCENTAGE_TAG
                If (index > 0) Then

                    Return Me.AGGREGATE_PERCENTAGE(index - 1)

                End If

                Return Nothing

            Case DOPE_MASS_TAG
                Return DOPE_MASS

            Case FILLER_APPORT_TAG
                Return FILLER_APPORT

            Case FILLER_RECUP_TAG
                Return FILLER_RECUP

            Case COLD_FEED_PERCENTAGE_TAG
                If (index > 0) Then

                    Return Me.COLDFEED_PERCENTAGE(index - 1)

                End If

                Return Nothing

            Case DOPE_PERCENTAGE_TAG
                Return DOPE_PERCENTAGE

            Case ASPHALT_TEMPERATURE_TAG
                Return ASPHALT_TEMPERATURE

            Case SILO_TAG
                Return SILO

            Case WET_MALAXING_TIME_TAG
                Return WET_MALAXING_TIME

            Case Else
                Return MyBase.getData(tag)

        End Select

    End Function ' End getData

    ''' <summary>The cycle's identifyer</summary>
    Public Property ID As String

    ''' <summary>The truck in which the cycle belongs</summary>
    Public Property TRUCK_ID As String

    ''' <summary>The command to which the cycle belongs</summary>
    Public Property COMMAND_ID As String

    Public Property AGGREGATE_MASS As Double() = Nothing

    Public Property AGGREGATE_PERCENTAGE As Double() = Nothing

    Public Property COLDFEED_PERCENTAGE As Double() = Nothing

    ''' <summary>The cycle's dope mass</summary>
    Public Property DOPE_MASS As Double

    ''' <summary>The cycle's additive material mass</summary>
    Public Property ADDITIVE_MASS As Double

    ''' <summary>The cycle's filler mass</summary>
    Public Property FILLER_MASS As Double

    ''' <summary>The cycle's filler apport</summary>
    Public Property FILLER_APPORT As Double

    ''' <summary>The cycle's filler recup</summary>
    Public Property FILLER_RECUP As Double

    ''' <summary>The cycle's dope percentage</summary>
    Public Property DOPE_PERCENTAGE As Double

    ''' <summary>The cycle's asphalt temperature</summary>
    Public Property ASPHALT_TEMPERATURE As Double

    ''' <summary>The silo in which the cycle belongs</summary>
    Public Property SILO As String

    ''' <summary>The cycle's wet malaxing duration</summary>
    Public Property WET_MALAXING_TIME As TimeSpan

    ''' <summary>The cycle's mix set point temperature</summary>
    Public Overrides Property SET_POINT_TEMPERATURE As Double

    ' --------Constants----------- '

    ''' <summary>The tag corresponding to the cycle's ID property</summary>
    Public Shared ReadOnly ID_TAG As Tag = New Tag("#CycleID", "Numéro de cycle", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's TRUCK_ID property</summary>
    Public Shared ReadOnly TRUCK_ID_TAG As Tag = New Tag("#TruckID", "Camion", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's COMMAND_ID property</summary>
    Public Shared ReadOnly COMMAND_ID_TAG As Tag = New Tag("#CommandID", "Contrat", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's AGGREGATE_MASS property</summary>
    Public Shared ReadOnly AGGREGATE_MASS_TAG As Tag = New Tag("#AggregateMass", "Masse granulat", Unit.DEFAULT_MASS_UNIT, True)

    ''' <summary>The tag corresponding to the cycle's AGGREGATE_PERCENTAGE property</summary>
    Public Shared ReadOnly AGGREGATE_PERCENTAGE_TAG As Tag = New Tag("#AggregatePercentage", "Pourcentage granulat", Unit.DEFAULT_PERCENT_UNIT, True)

    ''' <summary>The tag corresponding to the cycle's DOPE_MASS property</summary>
    Public Shared ReadOnly DOPE_MASS_TAG As Tag = New Tag("#DopeMass", "Masse dope", Unit.DEFAULT_MASS_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's FILLER_APPORT property</summary>
    Public Shared ReadOnly FILLER_APPORT_TAG As Tag = New Tag("#FillerApport", "Filler apport.", Unit.DEFAULT_MASS_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's FILLER_RECUP property</summary>
    Public Shared ReadOnly FILLER_RECUP_TAG As Tag = New Tag("#FillerRecup", "Filler récup.", Unit.DEFAULT_MASS_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's COLD_FEED_PERCENTAGE property</summary>
    Public Shared ReadOnly COLD_FEED_PERCENTAGE_TAG As Tag = New Tag("#ColdFeedPercentage", "Pourcentage benne froide", Unit.DEFAULT_PERCENT_UNIT, True)

    ''' <summary>The tag corresponding to the cycle's DOPE_PERCENTAGE property</summary>
    Public Shared ReadOnly DOPE_PERCENTAGE_TAG As Tag = New Tag("#DopePercentage", "Pourcentage dope", Unit.DEFAULT_PERCENT_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's ASPHALT_TEMPERATURE property</summary>
    Public Shared ReadOnly ASPHALT_TEMPERATURE_TAG As Tag = New Tag("#AsphaltTemperature", "Température bitume", Unit.DEFAULT_TEMPERATURE_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's SILO property</summary>
    Public Shared ReadOnly SILO_TAG As Tag = New Tag("#Silo", "Silo", Unit.NO_UNIT, False)

    ''' <summary>The tag corresponding to the cycle's WET_MALAXING_TIME property</summary>
    Public Shared ReadOnly WET_MALAXING_TIME_TAG As Tag = New Tag("#WetMalaxTime", "Malaxage humide", Unit.NO_UNIT, False)

    ''' <summary>An array containing all of the CSVCycle's tags</summary>
    ''' <remarks>
    ''' Pre-condition : All the Tag objects this array contains have to be initialized before the array.
    ''' </remarks>
    Public Shared Shadows ReadOnly TAGS As Tag() = {ID_TAG, _
                                                    TRUCK_ID_TAG, _
                                                    COMMAND_ID_TAG, _
                                                    AGGREGATE_MASS_TAG, _
                                                    AGGREGATE_PERCENTAGE_TAG, _
                                                    DOPE_MASS_TAG, _
                                                    FILLER_APPORT_TAG, _
                                                    FILLER_RECUP_TAG, _
                                                    COLD_FEED_PERCENTAGE_TAG, _
                                                    DOPE_PERCENTAGE_TAG, _
                                                    ASPHALT_TEMPERATURE_TAG, _
                                                    SILO_TAG, _
                                                    WET_MALAXING_TIME_TAG}
End Class
