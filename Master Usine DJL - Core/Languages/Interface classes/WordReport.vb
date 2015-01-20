Public Interface WordReport

    ReadOnly Property FileName As String

    ' Header
    ReadOnly Property Header As String

    ' Footer
    ReadOnly Property Footer_Middle As String

    ReadOnly Property Footer_Right As String

    ' Production section
    ReadOnly Property ProductionSection_Title As String

    ReadOnly Property ProductionSection_openingHoursText As String

    ReadOnly Property ProductionSection_productionHoursText As String

    ReadOnly Property ProductionSection_Table1_ProductionMode As String

    ReadOnly Property ProductionSection_Table1_Duration As String

    ReadOnly Property ProductionSection_Table1_TimePercentage As String

    ReadOnly Property ProductionSection_Table1_MixSwitchAndStops As String

    ReadOnly Property ProductionSection_Table1_ProductionSpeed As String

    ReadOnly Property ProductionSection_Table2_TotalProduction As String

    ReadOnly Property ProductionSection_Table2_ProductionSpeed As String

    ReadOnly Property ProductionSection_Table2_ProductionMode As String

    ReadOnly Property ProductionSection_Table2_MassSold As String

    ReadOnly Property ProductionSection_Table2_MassLeft As String

    ' Temperature Section
    ReadOnly Property TemperatureSection_Title As String

    ReadOnly Property TemperatureSection_Note As String

    ReadOnly Property TemperatureSection_Table_SetPointTemperature As String

    ReadOnly Property TemperatureSection_Table_UnderLimitPercentage As String

    ReadOnly Property TemperatureSection_Table_OverLimitPercentage As String

    ReadOnly Property TemperatureSection_Table_OutLimitsPercentage As String

    ReadOnly Property TemperatureSection_Table_OutLimitsMass As String

    ' Asphalt Percentage section
    ReadOnly Property AsphaltPercentageSection_Title As String

    ReadOnly Property AsphaltPercentageSection_Note As String

    ReadOnly Property AsphaltPercentageSection_Table_SetPointPercentage As String

    ReadOnly Property AsphaltPercentageSection_Table_OutTolerancePercentage As String

    ReadOnly Property AsphaltPercentageSection_Table_OutControlePercentage As String

    ReadOnly Property AsphaltPercentageSection_Table_OutControleMass As String

    ' Recycling section
    ReadOnly Property RecyclingSection_Title As String

    ReadOnly Property RecyclingSection_Table_SetPointRAP As String

    ReadOnly Property RecyclingSection_Table_AverageRAP As String

    ReadOnly Property RecyclingSection_Table_RAPMass As String

    ' Fuel consumption section
    ReadOnly Property FuelConsumptionSection_Title As String

    ReadOnly Property FuelConsumptionSection_FuelConsumption As String

    ReadOnly Property FuelConsumptionSection_AverageConsumption As String

    ' Mix summary section
    ReadOnly Property MixSummarySection_ContinuousTitle As String

    ReadOnly Property MixSummarySection_BatchTitle As String

    ReadOnly Property MixSummarySection_SetPointRAP As String

    ReadOnly Property MixSummarySection_NoContinuousMix As String

    ReadOnly Property MixSummarySection_NoBatchMix As String

    ' Asphalt summary section
    ReadOnly Property AsphaltSummarySection_Title As String

    ReadOnly Property AsphaltSummarySection_Table_Tanks As String

    ReadOnly Property AsphaltSummarySection_Table_AsphaltName As String

    ' Rejects Summary section
    ReadOnly Property RejectsSummarySection_Title As String

    ReadOnly Property RejectsSummarySection_Table_Materials As String

    ReadOnly Property RejectsSummarySection_Table_RejectedQuantity As String

    ' Stops summary
    ReadOnly Property StopsSummarySection_Title As String

    ReadOnly Property StopsSummarySection_Table_Start As String

    ReadOnly Property StopsSummarySection_Table_End As String

    ReadOnly Property StopsSummarySection_Table_Duration As String

    ReadOnly Property StopsSummarySection_Table_Code As String

    ReadOnly Property StopsSummarySection_Table_Description As String

    ReadOnly Property StopsSummarySection_Table_Cause As String

    ReadOnly Property StopsSummarySection_Codes_1 As String

    ReadOnly Property StopsSummarySection_Codes_2 As String

    ReadOnly Property StopsSummarySection_Codes_3 As String

    ReadOnly Property StopsSummarySection_Codes_4 As String

    ReadOnly Property StopsSummarySection_Codes_5 As String

    ReadOnly Property StopsSummarySection_Codes_6 As String

    ReadOnly Property StopsSummarySection_Codes_7 As String

    ReadOnly Property StopsSummarySection_Codes_8 As String

    ReadOnly Property StopsSummarySection_Codes_9 As String

    ReadOnly Property StopsSummarySection_Codes_10 As String

    ReadOnly Property StopsSummarySection_Codes_11 As String

    ReadOnly Property StopsSummarySection_Codes_12 As String

    ReadOnly Property StopsSummarySection_Codes_13 As String

    ReadOnly Property StopsSummarySection_Codes_14 As String

    ReadOnly Property StopsSummarySection_Codes_15 As String

    ReadOnly Property StopsSummarySection_Codes_16 As String

    ReadOnly Property StopsSummarySection_Codes_17 As String

    ReadOnly Property StopsSummarySection_Codes_18 As String

    ReadOnly Property StopsSummarySection_Codes_19 As String

    ReadOnly Property StopsSummarySection_Codes_20 As String

    ReadOnly Property StopsSummarySection_Codes_21 As String

    ReadOnly Property StopsSummarySection_Codes_22 As String

    ReadOnly Property StopsSummarySection_Codes_23 As String

    ReadOnly Property StopsSummarySection_Codes_24 As String

    ReadOnly Property StopsSummarySection_Codes_25 As String

    ' Events Summary section
    ReadOnly Property EventsSummarySection_Title As String

    ReadOnly Property EventsSummarySection_Table_EventNumber As String

    ReadOnly Property EventsSummarySection_Table_MixRecipeChange As String

    ReadOnly Property EventsSummarySection_Table_MixChange As String

    ReadOnly Property EventsSummarySection_Table_Start As String

    ReadOnly Property EventsSummarySection_Table_End As String

    ReadOnly Property EventsSummarySection_Table_Duration As String

    ReadOnly Property EventsSummarySection_Table_Comments As String

    ReadOnly Property EventsSummarySection_Table_StopsDuration As String

    ' Signature section
    ReadOnly Property SignatureSection_Signature As String

    ReadOnly Property SignatureSection_Date As String

    ReadOnly Property SignatureSection_Operator As String

    ReadOnly Property SignatureSection_Supervisor As String

End Interface
