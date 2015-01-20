Namespace Constants.Input

    Public Class LOG

        ''' <summary>Regex to check if a value is empty in the log files (only in the tables)</summary>
        Public Shared EMPTY_DATA_REGEX = "^[\s]+$"

        ''' <summary>
        ''' Regex to match and extract information from the name of log files
        ''' </summary>
        ''' <remarks>Example : 09142012.log</remarks>
        Public Shared ReadOnly FILE_NAME_REGEX As String = "([\d]{2})([\d]{2})([\d]{4})\.log"

        ''' <summary>Index of date components in the name regex of log files</summary>
        Public Enum LOGFileNameRegexDateIndex
            DAY = 2
            MONTH = 1
            YEAR = 3
        End Enum

        Public Enum HotFeedsIndexes
            VIRGIN_AGGREGATES = 0
            RECYCLED_AGGREGATES = 1
            RECYCLED_ASPHALT = 2
            VIRGIN_ASPHALT = 3
            TOTAL_ASPHALT = 4
            FILLER = 5
            ADDITIVE = 6
        End Enum

        Public Const NUMBER_OF_HOT_FEEDS = 7

        ' Check units
        Public Shared ReadOnly AVAILABLE_DATA As DataInfoConstant() = {New DataInfoConstant(Cycle.CYCLE_ID_1_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.TEMPERATURE_TAG, Celsius.UNIT), _
                                                                        New DataInfoConstant(Cycle.SET_POINT_TEMPERATURE_TAG, Celsius.UNIT), _
                                                                        New DataInfoConstant(Cycle.TEMPERATURE_VARIATION_TAG, Celsius.UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_MASS_TAG, Tons.UNIT), _
                                                                        New DataInfoConstant(Cycle.DATE_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.TIME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.DURATION_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.PRODUCTION_SPEED_TAG, TonsPerHour.UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_FORMULA_NAME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_NAME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_ACCUMULATED_MASS_TAG, Tons.UNIT), _
                                                                        New DataInfoConstant(Cycle.AGGREGATES_MASS_TAG, Tons.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_NAME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_TANK_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_MASS_TAG, Tons.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_ACCUMULATED_MASS_TAG, Tons.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_SET_POINT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_PERCENTAGE_VARIATION_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.RECYCLED_MASS_TAG, Tons.UNIT), _
                                                                        New DataInfoConstant(Cycle.RECYCLED_SET_POINT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.RECYCLED_PERCENTAGE_TAG, Percent.UNIT), _
                                                                       New DataInfoConstant(LOGCycle.DENSITY_TAG, Unit.NO_UNIT), _
                                                                       New DataInfoConstant(LOGCycle.BAG_HOUSE_DIFF_TAG, Unit.NO_UNIT), _
                                                                       New DataInfoConstant(LOGCycle.TOTAL_ACCUMULATED_TONS_TAG, Tons.UNIT), _
                                                                       New DataInfoConstant(LOGCycle.ASPHALT_TEMPERATURE_TAG, Celsius.UNIT), _
                                                                       New DataInfoConstant(LOGCycle.SILO_FILLING_TAG, Unit.NO_UNIT), _
                                                                      New DataInfoConstant(LOGCycle.DUST_REMOVAL_TAG, Unit.NO_UNIT)}

        Public Shared ReadOnly AVAILABLE_SUBCOLUMNS As DataInfoConstant() = {New DataInfoConstant(Feeder.MATERIAL_NAME_TAG, Unit.NO_UNIT), _
                                                                             New DataInfoConstant(Feeder.LOCATION_TAG, Unit.NO_UNIT), _
                                                                             New DataInfoConstant(Feeder.SET_POINT_MASS_TAG, Tons.UNIT), _
                                                                             New DataInfoConstant(Feeder.MASS_TAG, Tons.UNIT), _
                                                                             New DataInfoConstant(Feeder.ACCUMULATED_MASS_TAG, Tons.UNIT), _
                                                                             New DataInfoConstant(Feeder.SET_POINT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                            New DataInfoConstant(LOGFeeder.PRODUCTION_SPEED_TAG, TonsPerHour.UNIT), _
                                                                            New DataInfoConstant(LOGFeeder.MOISTURE_PERCENTAGE_TAG, Percent.UNIT), _
                                                                            New DataInfoConstant(LOGFeeder.RECYCLED_ASPHALT_PERCENTAGE_TAG, Percent.UNIT)}

        ' You'll probably need to add the hot feeds here
        Public Shared ReadOnly AVAILABLE_FEEDINFO As FeedInfo() = {New FeedInfoConstant(LOGCycle.VIRGIN_AGGREGATES_FEEDER_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(LOGCycle.RAP_ASPHALT_FEEDER_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(LOGCycle.VIRGIN_ASPHALT_FEEDER_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(LOGCycle.ADDITIVE_FEEDER_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(Cycle.ASPHALT_SUMMARY_FEED_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(Cycle.FILLER_SUMMARY_FEED_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(Cycle.RECYCLE_SUMMARY_FEED_TAG, AVAILABLE_SUBCOLUMNS)}

    End Class

End Namespace
