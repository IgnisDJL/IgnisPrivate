Namespace Constants.Input

    Public Class CSV

        ''' <summary>Regex to check if a value is empty in the csv files</summary>
        Public Const EMPTY_VALUE_REGEX As String = "^[""|\.]{0,2}$"

        ''' <summary>
        ''' Regex to match and extract information from the name of csv files
        ''' </summary>
        ''' <remarks>Exemple : 01-06-2011_CtrlInt.csv</remarks>
        Public Shared ReadOnly FILE_NAME_REGEX As String = "([\d]{2})-([\d]{2})-([\d]{4})_CtrlInt\.csv"

        ''' <summary>Index of date components in the name regex of csv files</summary>
        Public Enum CSVFileNameRegexDateIndex
            DAY = 1
            MONTH = 2
            YEAR = 3
        End Enum

        ''' <summary>
        ''' Represents the derived data that the program calculated or deduced from the data in the file.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly AVAILABLE_DATA As DataInfoConstant() = {New DataInfoConstant(Cycle.DATE_TAG, Unit.NO_UNIT), _
                                                                       New DataInfoConstant(Cycle.ASPHALT_ACCUMULATED_MASS_TAG, KiloGrams.UNIT), _
                                                                       New DataInfoConstant(Cycle.SET_POINT_TEMPERATURE_TAG, Celsius.UNIT), _
                                                                       New DataInfoConstant(Cycle.TEMPERATURE_VARIATION_TAG, Celsius.UNIT), _
                                                                       New DataInfoConstant(Cycle.PRODUCTION_SPEED_TAG, KgPerHour.UNIT), _
                                                                       New DataInfoConstant(Cycle.RECYCLED_MASS_TAG, KiloGrams.UNIT), _
                                                                       New DataInfoConstant(Cycle.RECYCLED_PERCENTAGE_TAG, Percent.UNIT)}

        ''' <summary>
        ''' Represents the derived data calculated or deduced by the program for each feeder
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly AVAILABLE_SUBCOLUMNS As DataInfoConstant() = {New DataInfoConstant(Feeder.MATERIAL_NAME_TAG, Unit.NO_UNIT), _
                                                                             New DataInfoConstant(Feeder.LOCATION_TAG, Unit.NO_UNIT), _
                                                                             New DataInfoConstant(Feeder.MASS_TAG, KiloGrams.UNIT), _
                                                                             New DataInfoConstant(Feeder.ACCUMULATED_MASS_TAG, KiloGrams.UNIT), _
                                                                            New DataInfoConstant(Feeder.PERCENTAGE_TAG, Percent.UNIT)}
    End Class

End Namespace
