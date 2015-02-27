Namespace Constants.Input

    Public Class MDB

        Public Const CONNECTION_STRING = "Microsoft.Jet.OLEDB.4.0;" & _
                                         "Persist Security Info=False;"

        Public Const DEFAULT_LOCATION = "BENNE FROIDE #3"

        Public Shared ReadOnly FILE_NAME_REGEX As String = "\.mdb$"

        ' Change the name of this
        Public Shared ReadOnly STOP_OFFSET = TimeSpan.FromMinutes(0.5)

        ' Comment and sort that shit
        Public Shared ReadOnly AVAILABLE_DATA As DataInfoConstant() = {New DataInfoConstant(Cycle.CYCLE_ID_1_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.TEMPERATURE_TAG, Celsius.UNIT), _
                                                                        New DataInfoConstant(Cycle.SET_POINT_TEMPERATURE_TAG, Celsius.UNIT), _
                                                                        New DataInfoConstant(Cycle.TEMPERATURE_VARIATION_TAG, Celsius.UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_MASS_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(Cycle.DATE_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.TIME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.DURATION_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.PRODUCTION_SPEED_TAG, KgPerHour.UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_FORMULA_NAME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_NAME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.MIX_ACCUMULATED_MASS_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(Cycle.AGGREGATES_MASS_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_NAME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_TANK_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_MASS_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_ACCUMULATED_MASS_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_SET_POINT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.ASPHALT_PERCENTAGE_VARIATION_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.RECYCLED_MASS_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(Cycle.RECYCLED_SET_POINT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(Cycle.RECYCLED_PERCENTAGE_TAG, Percent.UNIT), _
                                                                        New DataInfoConstant(MDBCycle.CYCLE_ID_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(MDBCycle.COMMAND_ID_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(MDBCycle.TRUCK_ID_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(MDBCycle.TOTAL_MALAXING_TIME_TAG, Unit.NO_UNIT), _
                                                                        New DataInfoConstant(MDBCycle.RECIPE_QUANTITY_TAG, KiloGrams.UNIT), _
                                                                        New DataInfoConstant(MDBCycle.DRY_MALAXING_TIME_TAG, Unit.NO_UNIT), _
                                                                       New DataInfoConstant(MDBCycle.SET_POINT_MASS_TAG, KiloGrams.UNIT)}


        Public Shared ReadOnly AVAILABLE_SUBCOLUMNS As DataInfoConstant() = {New DataInfoConstant(Feeder.MATERIAL_NAME_TAG, Unit.NO_UNIT), _
                                                                             New DataInfoConstant(Feeder.LOCATION_TAG, Unit.NO_UNIT), _
                                                                             New DataInfoConstant(Feeder.SET_POINT_MASS_TAG, KiloGrams.UNIT), _
                                                                             New DataInfoConstant(Feeder.MASS_TAG, KiloGrams.UNIT), _
                                                                             New DataInfoConstant(Feeder.ACCUMULATED_MASS_TAG, KiloGrams.UNIT), _
                                                                             New DataInfoConstant(Feeder.SET_POINT_PERCENTAGE_TAG, Percent.UNIT), _
                                                                            New DataInfoConstant(Feeder.PERCENTAGE_TAG, Percent.UNIT), _
                                                                            New DataInfoConstant(MDBFeeder.RECIPE_MASS_TAG, KiloGrams.UNIT), _
                                                                             New DataInfoConstant(MDBFeeder.MANUAL_MODE_TAG, Unit.NO_UNIT)}
        ' Maybe add aggregates...
        Public Shared ReadOnly AVAILABLE_FEEDINFO As FeedInfo() = {New FeedInfoConstant(Cycle.ASPHALT_SUMMARY_FEED_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(Cycle.FILLER_SUMMARY_FEED_TAG, AVAILABLE_SUBCOLUMNS), _
                                                                   New FeedInfoConstant(Cycle.RECYCLE_SUMMARY_FEED_TAG, AVAILABLE_SUBCOLUMNS)}

        Public Class Columns

            ' Cycle table
            Public Const CYCLE_ID = "CycleID"
            Public Const COMMAND_ID = "CommandeID"
            Public Const QUANTITY = "Quantite"
            Public Const FINAL_TEMP = "TemperatureBeton"
            Public Const DATE_TIME = "Date"
            Public Const TOTAL_MALAX_TIME = "TempsMelangeFinalReel"
            Public Const DRY_MALAX_TIME = "TempsMelangeSecReel"
            Public Const MIX_TEMPERATURE = "TemperatureBeton"

            ' Command table
            Public Const FORMULA_NAME_ID = "NomFormuleID"
            Public Const TRUCK_ID = "SiloExpedition"

            ' String Cache
            Public Const STRING_ID = "StringCacheID"
            Public Const STRING_CONTENT = "Str"

            ' Recipe
            Public Const RECIPE_ID = "RecetteID"
            Public Const RECIPE_NAME = "Nom"
            Public Const RECIPE_DESC = "Description"
            Public Const COLD_FEEDS_RECIPE_ID = "ColdFeedRecipeID"

            ' Cold Feed Recipe Details
            Public Const CFRD_TABLE_RECIPE_ID = "RecipeID"
            Public Const MATERIAL_ID = "MateriauID"
            Public Const MATERIAL_PERCENTAGE = "Percentage"

            ' Materials table
            Public Const MATERIAL_NAME = "Nom"

            ' Cycle Details Table
            Public Const MATERIAL_NAME_ID = "NomMateriauID"
            Public Const FORMULA_QUANTITY = "QuantiteFormule"
            Public Const DOSAGE_QUANTITY = "QuantiteDosage"
            Public Const REAL_QUANTITY = "QuantiteReel"
            Public Const LOCATION = "Emplacement"
            Public Const MANUEL_MODE = "Manuelle"

            ' Location table
            Public Const LOCATION_ID = "NoEmplacement"
            Public Const LOCATION_NAME = "Nom"



        End Class

        Public Class Tables

            Public Const CYCLE = "Cycle"

            Public Const COMMAND = "Commande"

            Public Const STRING_CACHE = "StringCache"

            Public Const RECIPES = "Recettes"

            Public Const COLD_FEEDS_RECIPES_DETAILS = "ColdFeedsRecipesDetails"

            Public Const MATERIALS = "Materiau"

            Public Const CYCLE_DETAILS = "[Details Cycle]"

            Public Const LOCATION = "Emplacement"

        End Class

        Public Class CycleQuery

            Public Const QUERY = "SELECT " & Tables.CYCLE & "." & Columns.CYCLE_ID & ", " & _
                                            Tables.CYCLE & "." & Columns.COMMAND_ID & ", " & _
                                            Tables.CYCLE & "." & Columns.QUANTITY & ", " & _
                                            Tables.CYCLE & "." & Columns.FINAL_TEMP & ", " & _
                                            Tables.CYCLE & "." & Columns.DATE_TIME & ", " & _
                                            Tables.CYCLE & "." & Columns.DRY_MALAX_TIME & ", " & _
                                            Tables.CYCLE & "." & Columns.TOTAL_MALAX_TIME & ", " & _
                                            Tables.COMMAND & "." & Columns.TRUCK_ID & ", " & _
                                            Tables.STRING_CACHE & "." & Columns.STRING_CONTENT & ", " & _
                                            Tables.RECIPES & "." & Columns.RECIPE_DESC & ", " & _
                                            Tables.RECIPES & "." & Columns.QUANTITY & ", " & _
                                            Tables.COLD_FEEDS_RECIPES_DETAILS & "." & Columns.MATERIAL_PERCENTAGE & ", " & _
                                            Tables.LOCATION & "." & Columns.LOCATION_NAME & ", " & _
                                            Tables.MATERIALS & "." & Columns.MATERIAL_NAME & _
                                " FROM ((((((" & Tables.CYCLE & ")" & _
                                " LEFT JOIN " & Tables.COMMAND & _
                                " ON " & Tables.CYCLE & "." & Columns.COMMAND_ID & _
                                " = " & Tables.COMMAND & "." & Columns.COMMAND_ID & ")" & _
                                " LEFT JOIN " & Tables.STRING_CACHE & _
                                " ON " & Tables.COMMAND & "." & Columns.FORMULA_NAME_ID & _
                                " = " & Tables.STRING_CACHE & "." & Columns.STRING_ID & ")" & _
                                " LEFT JOIN " & Tables.RECIPES & _
                                " ON " & Tables.STRING_CACHE & "." & Columns.STRING_CONTENT & _
                                " = " & Tables.RECIPES & "." & Columns.RECIPE_NAME & ")" & _
                                " LEFT JOIN " & Tables.COLD_FEEDS_RECIPES_DETAILS & _
                                " ON " & Tables.RECIPES & "." & Columns.COLD_FEEDS_RECIPE_ID & _
                                " = " & Tables.COLD_FEEDS_RECIPES_DETAILS & "." & Columns.CFRD_TABLE_RECIPE_ID & ")" & _
                                " LEFT JOIN " & Tables.LOCATION & _
                                " ON " & Tables.COLD_FEEDS_RECIPES_DETAILS & "." & Columns.MATERIAL_ID & _
                                " = " & Tables.LOCATION & "." & Columns.MATERIAL_ID & ")" & _
                                " LEFT JOIN " & Tables.MATERIALS & _
                                " ON " & Tables.COLD_FEEDS_RECIPES_DETAILS & "." & Columns.MATERIAL_ID & _
                                " = " & Tables.MATERIALS & "." & Columns.MATERIAL_ID & _
                                " WHERE " & Tables.CYCLE & "." & Columns.DATE_TIME & " "

            Public Enum RESULTS

                CYCLE_ID = 0
                COMMAND_ID = 1
                SET_POINT_MASS = 2
                TEMPERATURE = 3
                DATE_TIME = 4
                DRY_MALAXING_TIME = 5
                TOTAL_MALAXING_TIME = 6
                TRUCK_ID = 7
                FORMULA_NAME = 8
                MIX_NAME = 9
                RECIPE_QUANTITY = 10

            End Enum


        End Class

        Public Class FeedsQuery

            Public Const QUERY = "SELECT " & Tables.CYCLE_DETAILS & "." & Columns.FORMULA_QUANTITY & ", " & _
                            Tables.CYCLE_DETAILS & "." & Columns.DOSAGE_QUANTITY & ", " & _
                            Tables.CYCLE_DETAILS & "." & Columns.REAL_QUANTITY & ", " & _
                            Tables.CYCLE_DETAILS & "." & Columns.MANUEL_MODE & ", " & _
                            Tables.STRING_CACHE & "." & Columns.STRING_CONTENT & ", " & _
                            Tables.LOCATION & "." & Columns.LOCATION_NAME & _
                " FROM ((" & Tables.CYCLE_DETAILS & ")" & _
                " LEFT JOIN " & Tables.STRING_CACHE & _
                " ON " & Tables.CYCLE_DETAILS & "." & Columns.MATERIAL_NAME_ID & _
                " = " & Tables.STRING_CACHE & "." & Columns.STRING_ID & ")" & _
                " LEFT JOIN " & Tables.LOCATION & _
                " ON " & Tables.CYCLE_DETAILS & "." & Columns.LOCATION & _
                " = " & Tables.LOCATION & "." & Columns.LOCATION_ID & _
                " WHERE " & Tables.CYCLE_DETAILS & "." & Columns.CYCLE_ID & " = "

            Public Enum RESULTS

                RECIPE_MASS = 0
                SET_POINT_MASS = 1
                MASS = 2
                MANUAL_MODE = 3
                MATERIAL_NAME = 4
                LOCATION = 5

            End Enum

        End Class

    End Class

End Namespace