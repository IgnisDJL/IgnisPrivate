Imports Microsoft.Office.Interop.Word

Namespace Constants.Reports.BookMarks

    Public Class SummaryDailyReportBookMarks

        Public AA01_HeaderPlantName As Microsoft.Office.Interop.Word.Range
        Public AA02_HeaderPlantID As Microsoft.Office.Interop.Word.Range

        Public CA01_ProductionDayDate As Microsoft.Office.Interop.Word.Range

        Public CT01_OperationStartTime As Microsoft.Office.Interop.Word.Range
        Public CT01_OperationEndTime As Microsoft.Office.Interop.Word.Range
        Public CT01_OperationDuration As Microsoft.Office.Interop.Word.Range
        Public CT01_ProductionStartTime As Microsoft.Office.Interop.Word.Range
        Public CT01_ProductionEndTime As Microsoft.Office.Interop.Word.Range
        Public CT01_ProductionDuration As Microsoft.Office.Interop.Word.Range
        Public CT01_LoadingStartTime As Microsoft.Office.Interop.Word.Range
        Public CT01_LoadingEndTime As Microsoft.Office.Interop.Word.Range
        Public CT01_LoadingDuration As Microsoft.Office.Interop.Word.Range
        Public CT01_PausesDuration As Microsoft.Office.Interop.Word.Range
        Public CT01_MaintenanceDuration As Microsoft.Office.Interop.Word.Range

        Public CT02_FirstMixName As Microsoft.Office.Interop.Word.Range
        Public CT02_FirstMixVirginAsphaltConcreteGrade As Microsoft.Office.Interop.Word.Range
        Public CT02_FirstMixQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_FirstMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public CT02_FirstMixProductionMode As Microsoft.Office.Interop.Word.Range
        Public CT02_SecondMixName As Microsoft.Office.Interop.Word.Range
        Public CT02_SecondMixVirginAsphaltConcreteGrade As Microsoft.Office.Interop.Word.Range
        Public CT02_SecondMixQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_SecondMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public CT02_SecondMixProductionMode As Microsoft.Office.Interop.Word.Range
        Public CT02_ThirdMixName As Microsoft.Office.Interop.Word.Range
        Public CT02_ThirdMixVirginAsphaltConcreteGrade As Microsoft.Office.Interop.Word.Range
        Public CT02_ThirdMixQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_ThirdMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public CT02_ThirdMixProductionMode As Microsoft.Office.Interop.Word.Range

        Public CT02_OtherMixesNumberOfMixes As Microsoft.Office.Interop.Word.Range
        Public CT02_OtherMixesQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_OtherMixesProductionRate As Microsoft.Office.Interop.Word.Range
        Public CT02_OtherMixesProductionMode As Microsoft.Office.Interop.Word.Range
        Public CT02_TotalMixQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_TotalMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public CT02_SiloStartQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_SiloEndQuantity As Microsoft.Office.Interop.Word.Range

        Public CT02_SaleableQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_RejectedMixQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_RejectedMixPercentage As Microsoft.Office.Interop.Word.Range
        Public CT02_PayableQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_SoldQuantity As Microsoft.Office.Interop.Word.Range
        Public CT02_SoldQuantityDifferencePercentage As Microsoft.Office.Interop.Word.Range

        Public CG01_ProductionQuantityGraphic As Microsoft.Office.Interop.Word.Range
        Public CG02_ProductionRateGraphic As Microsoft.Office.Interop.Word.Range

        Public DT01_ContinuousDuration As Microsoft.Office.Interop.Word.Range
        Public DT01_DiscontinuousDuration As Microsoft.Office.Interop.Word.Range
        Public DT01_DelaysDuration As Microsoft.Office.Interop.Word.Range
        Public DT01_ContinuousPercentage As Microsoft.Office.Interop.Word.Range
        Public DT01_DiscontinuousPercentage As Microsoft.Office.Interop.Word.Range
        Public DT01_DelaysPercentage As Microsoft.Office.Interop.Word.Range
        Public DT01_ContinuousMixChange As Microsoft.Office.Interop.Word.Range
        Public DT01_DisontinuousMixChange As Microsoft.Office.Interop.Word.Range
        Public DT01_DelaysNumber As Microsoft.Office.Interop.Word.Range
        Public DT01_ContinuousQuantity As Microsoft.Office.Interop.Word.Range
        Public DT01_DiscontinuousQuantity As Microsoft.Office.Interop.Word.Range
        Public DT01_ContinuousProductionRate As Microsoft.Office.Interop.Word.Range
        Public DT01_DiscontinuousProductionRate As Microsoft.Office.Interop.Word.Range

        Public DT02_TotalOperationDuration As Microsoft.Office.Interop.Word.Range
        Public DT02_NetOperationDuration As Microsoft.Office.Interop.Word.Range
        Public DT02_NetProductionDuration As Microsoft.Office.Interop.Word.Range
        Public DT02_EffectiveProductionDuration As Microsoft.Office.Interop.Word.Range
        Public DT02_EffectiveInternalDuration As Microsoft.Office.Interop.Word.Range
        Public DT02_DelaysDuration As Microsoft.Office.Interop.Word.Range

        Public DT03_BreakageNumber As Microsoft.Office.Interop.Word.Range
        Public DT03_DisponibilityPercentage As Microsoft.Office.Interop.Word.Range
        Public DT03_UtilisationPercentage As Microsoft.Office.Interop.Word.Range
        Public DT03_TempsEntrePannes As Microsoft.Office.Interop.Word.Range
        Public DT03_TempsPourReparer As Microsoft.Office.Interop.Word.Range

        Public DG01_ProductionDistributionGraphic As Microsoft.Office.Interop.Word.Range
        Public DG02_DelaysDistributionGraphic As Microsoft.Office.Interop.Word.Range

        Public ET01_FirstVirginAsphaltConcreteTankId As Microsoft.Office.Interop.Word.Range
        Public ET01_FirstVirginAsphaltConcreteGrade As Microsoft.Office.Interop.Word.Range
        Public ET01_FirstVirginAsphaltConcreteQuantity As Microsoft.Office.Interop.Word.Range
        Public ET01_TotalVirginAsphaltConcreteQuantity As Microsoft.Office.Interop.Word.Range

        Public ET02_VirginAsphaltConcreteDifferencePerc As Microsoft.Office.Interop.Word.Range
        Public ET02_AverageTemperatureDifference As Microsoft.Office.Interop.Word.Range
        Public ET03_TemperatureVariation As Microsoft.Office.Interop.Word.Range
        Public ET04_VirginAsphaltConcreteAberrancePerc As Microsoft.Office.Interop.Word.Range
        Public ET04_TempratureAberrancePercentage As Microsoft.Office.Interop.Word.Range

        Public EG01_TemperatureVariationGraphic As Microsoft.Office.Interop.Word.Range

        Public FT01_FirstFuelName As Microsoft.Office.Interop.Word.Range
        Public FT01_FirstFuelQuantity As Microsoft.Office.Interop.Word.Range
        Public FT01_FirstFuelConsumptionRate As Microsoft.Office.Interop.Word.Range
        Public FT01_SecondFuelName As Microsoft.Office.Interop.Word.Range
        Public FT01_SecondFuelQuantity As Microsoft.Office.Interop.Word.Range
        Public FT01_SecondFuelConsumptionRate As Microsoft.Office.Interop.Word.Range

        Public GT01_RejectedAggregatesQuantity As Microsoft.Office.Interop.Word.Range
        Public GT01_RejectedAggregatesPercentage As Microsoft.Office.Interop.Word.Range
        Public GT01_RejectedFillerQuantity As Microsoft.Office.Interop.Word.Range
        Public GT01_RejectedFillerPercentage As Microsoft.Office.Interop.Word.Range
        Public GT01_RejectedRecycledQuantity As Microsoft.Office.Interop.Word.Range
        Public GT01_RejectedRecycledPercentage As Microsoft.Office.Interop.Word.Range

        Public HT01_FirstDelayStartTime As Microsoft.Office.Interop.Word.Range
        Public HT01_FirstDelayEndTime As Microsoft.Office.Interop.Word.Range
        Public HT01_FirstDelayDuration As Microsoft.Office.Interop.Word.Range
        Public HT01_FirstDelayCode As Microsoft.Office.Interop.Word.Range
        Public HT01_FirstDelayDescription As Microsoft.Office.Interop.Word.Range
        Public HT01_FirstDelayComments As Microsoft.Office.Interop.Word.Range
        Public HT01_MinimalDurationForJustification As Microsoft.Office.Interop.Word.Range
        Public HT01_DelaysNumberUnderMinimalDuration As Microsoft.Office.Interop.Word.Range
        Public HT01_DelaysUnderMinimalTimeDuration As Microsoft.Office.Interop.Word.Range

        Public JA01_ContinuousProductionSummarySection As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousMixNumber As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousMixName As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousVirginACGrade As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousRecycledQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousVirginACQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousFeederQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_FirstContinuousFeederDescription As Microsoft.Office.Interop.Word.Range
        Public JT01_ContinuousTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_ContinuousTotalVirginACQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_ContinuousFeederTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public JT01_ContinuousTotalCellsToMerge As Microsoft.Office.Interop.Word.Range
        Public JT01_ContinuousWithRAPPercentage As Microsoft.Office.Interop.Word.Range

        Public JA02_DiscontinuousProductionSummarySect As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousMixNumber As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousMixName As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousVirginACGrade As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousRecycledQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousVirginACQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousFeederQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_FirstDiscontinuousFeederDescription As Microsoft.Office.Interop.Word.Range
        Public JT02_DiscontinuousTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_DiscontinuousTotalVirginACQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_DiscontinuousFeederTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public JT02_DiscontinuousTotalCellsToMerge As Microsoft.Office.Interop.Word.Range
        Public JT02_DiscontinuousWithRAPPercentage As Microsoft.Office.Interop.Word.Range

        Public KA01_Comments As Microsoft.Office.Interop.Word.Range

        Public LA01_OperatorName As Microsoft.Office.Interop.Word.Range

        Public BA01_FooterDate As Microsoft.Office.Interop.Word.Range
        Public LA02_SignatureDate As Microsoft.Office.Interop.Word.Range


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="wordDoc"></param>
        ''' <remarks>
        ''' Use the same variable name as the bookmark string
        ''' Use regex powered search and replace.
        ''' </remarks>
        Sub initialize(wordDoc As Document)

            Me.AA01_HeaderPlantName = wordDoc.Bookmarks("AA01_HeaderPlantName").Range
            Me.AA02_HeaderPlantID = wordDoc.Bookmarks("AA02_HeaderPlantID").Range

            Me.CA01_ProductionDayDate = wordDoc.Bookmarks("CA01_ProductionDayDate").Range

            Me.CT01_OperationStartTime = wordDoc.Bookmarks("CT01_OperationStartTime").Range
            Me.CT01_OperationEndTime = wordDoc.Bookmarks("CT01_OperationEndTime").Range
            Me.CT01_OperationDuration = wordDoc.Bookmarks("CT01_OperationDuration").Range
            Me.CT01_ProductionStartTime = wordDoc.Bookmarks("CT01_ProductionStartTime").Range
            Me.CT01_ProductionEndTime = wordDoc.Bookmarks("CT01_ProductionEndTime").Range
            Me.CT01_ProductionDuration = wordDoc.Bookmarks("CT01_ProductionDuration").Range
            Me.CT01_LoadingStartTime = wordDoc.Bookmarks("CT01_LoadingStartTime").Range
            Me.CT01_LoadingEndTime = wordDoc.Bookmarks("CT01_LoadingEndTime").Range
            Me.CT01_LoadingDuration = wordDoc.Bookmarks("CT01_LoadingDuration").Range
            Me.CT01_PausesDuration = wordDoc.Bookmarks("CT01_PausesDuration").Range
            Me.CT01_MaintenanceDuration = wordDoc.Bookmarks("CT01_MaintenanceDuration").Range

            Me.CT02_FirstMixName = wordDoc.Bookmarks("CT02_FirstMixName").Range
            Me.CT02_FirstMixVirginAsphaltConcreteGrade = wordDoc.Bookmarks("CT02_FirstMixVirginAsphaltConcreteGrade").Range
            Me.CT02_FirstMixQuantity = wordDoc.Bookmarks("CT02_FirstMixQuantity").Range
            Me.CT02_FirstMixProductionRate = wordDoc.Bookmarks("CT02_FirstMixProductionRate").Range
            Me.CT02_FirstMixProductionMode = wordDoc.Bookmarks("CT02_FirstMixProductionMode").Range
            Me.CT02_SecondMixName = wordDoc.Bookmarks("CT02_SecondMixName").Range
            Me.CT02_SecondMixVirginAsphaltConcreteGrade = wordDoc.Bookmarks("CT02_SecondMixVirginAsphaltConcreteGrade").Range
            Me.CT02_SecondMixQuantity = wordDoc.Bookmarks("CT02_SecondMixQuantity").Range
            Me.CT02_SecondMixProductionRate = wordDoc.Bookmarks("CT02_SecondMixProductionRate").Range
            Me.CT02_SecondMixProductionMode = wordDoc.Bookmarks("CT02_SecondMixProductionMode").Range
            Me.CT02_ThirdMixName = wordDoc.Bookmarks("CT02_ThirdMixName").Range
            Me.CT02_ThirdMixVirginAsphaltConcreteGrade = wordDoc.Bookmarks("CT02_ThirdMixVirginAsphaltConcreteGrade").Range
            Me.CT02_ThirdMixQuantity = wordDoc.Bookmarks("CT02_ThirdMixQuantity").Range
            Me.CT02_ThirdMixProductionRate = wordDoc.Bookmarks("CT02_ThirdMixProductionRate").Range
            Me.CT02_ThirdMixProductionMode = wordDoc.Bookmarks("CT02_ThirdMixProductionMode").Range

            Me.CT02_OtherMixesNumberOfMixes = wordDoc.Bookmarks("CT02_OtherMixesNumberOfMixes").Range
            Me.CT02_OtherMixesQuantity = wordDoc.Bookmarks("CT02_OtherMixesQuantity").Range
            Me.CT02_OtherMixesProductionRate = wordDoc.Bookmarks("CT02_OtherMixesProductionRate").Range
            Me.CT02_OtherMixesProductionMode = wordDoc.Bookmarks("CT02_OtherMixesProductionMode").Range
            Me.CT02_TotalMixQuantity = wordDoc.Bookmarks("CT02_TotalMixQuantity").Range
            Me.CT02_TotalMixProductionRate = wordDoc.Bookmarks("CT02_TotalMixProductionRate").Range
            Me.CT02_SiloStartQuantity = wordDoc.Bookmarks("CT02_SiloStartQuantity").Range
            Me.CT02_SiloEndQuantity = wordDoc.Bookmarks("CT02_SiloEndQuantity").Range

            Me.CT02_SaleableQuantity = wordDoc.Bookmarks("CT02_SaleableQuantity").Range
            Me.CT02_RejectedMixQuantity = wordDoc.Bookmarks("CT02_RejectedMixQuantity").Range
            Me.CT02_RejectedMixPercentage = wordDoc.Bookmarks("CT02_RejectedMixPercentage").Range
            Me.CT02_PayableQuantity = wordDoc.Bookmarks("CT02_PayableQuantity").Range
            Me.CT02_SoldQuantity = wordDoc.Bookmarks("CT02_SoldQuantity").Range
            Me.CT02_SoldQuantityDifferencePercentage = wordDoc.Bookmarks("CT02_SoldQuantityDifferencePercentage").Range

            Me.CG01_ProductionQuantityGraphic = wordDoc.Bookmarks("CG01_ProductionQuantityGraphic").Range
            Me.CG02_ProductionRateGraphic = wordDoc.Bookmarks("CG02_ProductionRateGraphic").Range

            Me.DT01_ContinuousDuration = wordDoc.Bookmarks("DT01_ContinuousDuration").Range
            Me.DT01_DiscontinuousDuration = wordDoc.Bookmarks("DT01_DiscontinuousDuration").Range
            Me.DT01_DelaysDuration = wordDoc.Bookmarks("DT01_DelaysDuration").Range
            Me.DT01_ContinuousPercentage = wordDoc.Bookmarks("DT01_ContinuousPercentage").Range
            Me.DT01_DiscontinuousPercentage = wordDoc.Bookmarks("DT01_DiscontinuousPercentage").Range
            Me.DT01_DelaysPercentage = wordDoc.Bookmarks("DT01_DelaysPercentage").Range
            Me.DT01_ContinuousMixChange = wordDoc.Bookmarks("DT01_ContinuousMixChange").Range
            Me.DT01_DisontinuousMixChange = wordDoc.Bookmarks("DT01_DisontinuousMixChange").Range
            Me.DT01_DelaysNumber = wordDoc.Bookmarks("DT01_DelaysNumber").Range
            Me.DT01_ContinuousQuantity = wordDoc.Bookmarks("DT01_ContinuousQuantity").Range
            Me.DT01_DiscontinuousQuantity = wordDoc.Bookmarks("DT01_DiscontinuousQuantity").Range
            Me.DT01_ContinuousProductionRate = wordDoc.Bookmarks("DT01_ContinuousProductionRate").Range
            Me.DT01_DiscontinuousProductionRate = wordDoc.Bookmarks("DT01_DiscontinuousProductionRate").Range

            Me.DT02_TotalOperationDuration = wordDoc.Bookmarks("DT02_TotalOperationDuration").Range
            Me.DT02_NetOperationDuration = wordDoc.Bookmarks("DT02_NetOperationDuration").Range
            Me.DT02_NetProductionDuration = wordDoc.Bookmarks("DT02_NetProductionDuration").Range
            Me.DT02_EffectiveProductionDuration = wordDoc.Bookmarks("DT02_EffectiveProductionDuration").Range
            Me.DT02_EffectiveInternalDuration = wordDoc.Bookmarks("DT02_EffectiveInternalDuration").Range
            Me.DT02_DelaysDuration = wordDoc.Bookmarks("DT02_DelaysDuration").Range

            Me.DT03_BreakageNumber = wordDoc.Bookmarks("DT03_BreakageNumber").Range
            Me.DT03_DisponibilityPercentage = wordDoc.Bookmarks("DT03_DisponibilityPercentage").Range
            Me.DT03_UtilisationPercentage = wordDoc.Bookmarks("DT03_UtilisationPercentage").Range
            Me.DT03_TempsEntrePannes = wordDoc.Bookmarks("DT03_TempsEntrePannes").Range
            Me.DT03_TempsPourReparer = wordDoc.Bookmarks("DT03_TempsPourReparer").Range

            Me.DG01_ProductionDistributionGraphic = wordDoc.Bookmarks("DG01_ProductionDistributionGraphic").Range
            Me.DG02_DelaysDistributionGraphic = wordDoc.Bookmarks("DG02_DelaysDistributionGraphic").Range

            Me.ET01_FirstVirginAsphaltConcreteTankId = wordDoc.Bookmarks("ET01_FirstVirginAsphaltConcreteTankId").Range
            Me.ET01_FirstVirginAsphaltConcreteGrade = wordDoc.Bookmarks("ET01_FirstVirginAsphaltConcreteGrade").Range
            Me.ET01_FirstVirginAsphaltConcreteQuantity = wordDoc.Bookmarks("ET01_FirstVirginAsphaltConcreteQuantity").Range
            Me.ET01_TotalVirginAsphaltConcreteQuantity = wordDoc.Bookmarks("ET01_TotalVirginAsphaltConcreteQuantity").Range

            Me.ET02_VirginAsphaltConcreteDifferencePerc = wordDoc.Bookmarks("ET02_VirginAsphaltConcreteDifferencePerc").Range
            Me.ET02_AverageTemperatureDifference = wordDoc.Bookmarks("ET02_AverageTemperatureDifference").Range
            Me.ET03_TemperatureVariation = wordDoc.Bookmarks("ET03_TemperatureVariation").Range
            Me.ET04_VirginAsphaltConcreteAberrancePerc = wordDoc.Bookmarks("ET04_VirginAsphaltConcreteAberrancePerc").Range
            Me.ET04_TempratureAberrancePercentage = wordDoc.Bookmarks("ET04_TempratureAberrancePercentage").Range

            Me.EG01_TemperatureVariationGraphic = wordDoc.Bookmarks("EG01_TemperatureVariationGraphic").Range

            Me.FT01_FirstFuelName = wordDoc.Bookmarks("FT01_FirstFuelName").Range
            Me.FT01_FirstFuelQuantity = wordDoc.Bookmarks("FT01_FirstFuelQuantity").Range
            Me.FT01_FirstFuelConsumptionRate = wordDoc.Bookmarks("FT01_FirstFuelConsumptionRate").Range
            Me.FT01_SecondFuelName = wordDoc.Bookmarks("FT01_SecondFuelName").Range
            Me.FT01_SecondFuelQuantity = wordDoc.Bookmarks("FT01_SecondFuelQuantity").Range
            Me.FT01_SecondFuelConsumptionRate = wordDoc.Bookmarks("FT01_SecondFuelConsumptionRate").Range

            Me.GT01_RejectedAggregatesQuantity = wordDoc.Bookmarks("GT01_RejectedAggregatesQuantity").Range
            Me.GT01_RejectedAggregatesPercentage = wordDoc.Bookmarks("GT01_RejectedAggregatesPercentage").Range
            Me.GT01_RejectedFillerQuantity = wordDoc.Bookmarks("GT01_RejectedFillerQuantity").Range
            Me.GT01_RejectedFillerPercentage = wordDoc.Bookmarks("GT01_RejectedFillerPercentage").Range
            Me.GT01_RejectedRecycledQuantity = wordDoc.Bookmarks("GT01_RejectedRecycledQuantity").Range
            Me.GT01_RejectedRecycledPercentage = wordDoc.Bookmarks("GT01_RejectedRecycledPercentage").Range

            Me.HT01_FirstDelayStartTime = wordDoc.Bookmarks("HT01_FirstDelayStartTime").Range
            Me.HT01_FirstDelayEndTime = wordDoc.Bookmarks("HT01_FirstDelayEndTime").Range
            Me.HT01_FirstDelayDuration = wordDoc.Bookmarks("HT01_FirstDelayDuration").Range
            Me.HT01_FirstDelayCode = wordDoc.Bookmarks("HT01_FirstDelayCode").Range
            Me.HT01_FirstDelayDescription = wordDoc.Bookmarks("HT01_FirstDelayDescription").Range
            Me.HT01_FirstDelayComments = wordDoc.Bookmarks("HT01_FirstDelayComments").Range
            Me.HT01_MinimalDurationForJustification = wordDoc.Bookmarks("HT01_MinimalDurationForJustification").Range
            Me.HT01_DelaysNumberUnderMinimalDuration = wordDoc.Bookmarks("HT01_DelaysNumberUnderMinimalDuration").Range
            Me.HT01_DelaysUnderMinimalTimeDuration = wordDoc.Bookmarks("HT01_DelaysUnderMinimalTimeDuration").Range

            Me.JA01_ContinuousProductionSummarySection = wordDoc.Bookmarks("JA01_ContinuousProductionSummarySection").Range
            Me.JT01_FirstContinuousMixNumber = wordDoc.Bookmarks("JT01_FirstContinuousMixNumber").Range
            Me.JT01_FirstContinuousMixName = wordDoc.Bookmarks("JT01_FirstContinuousMixName").Range
            Me.JT01_FirstContinuousVirginACGrade = wordDoc.Bookmarks("JT01_FirstContinuousVirginACGrade").Range
            Me.JT01_FirstContinuousRecycledQuantity = wordDoc.Bookmarks("JT01_FirstContinuousRecycledQuantity").Range
            Me.JT01_FirstContinuousQuantity = wordDoc.Bookmarks("JT01_FirstContinuousQuantity").Range
            Me.JT01_FirstContinuousVirginACQuantity = wordDoc.Bookmarks("JT01_FirstContinuousVirginACQuantity").Range
            Me.JT01_FirstContinuousFeederDescription = wordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range
            Me.JT01_FirstContinuousFeederQuantity = wordDoc.Bookmarks("JT01_FirstContinuousFeederQuantity").Range
            Me.JT01_ContinuousTotalQuantity = wordDoc.Bookmarks("JT01_ContinuousTotalQuantity").Range
            Me.JT01_ContinuousTotalVirginACQuantity = wordDoc.Bookmarks("JT01_ContinuousTotalVirginACQuantity").Range
            Me.JT01_ContinuousFeederTotalQuantity = wordDoc.Bookmarks("FirstContinuousProductionFeederTotalQty").Range
            Me.JT01_ContinuousTotalCellsToMerge = wordDoc.Bookmarks("JT01_ContinuousTotalCellsToMerge").Range
            Me.JT01_ContinuousWithRAPPercentage = wordDoc.Bookmarks("ContinuousProductionMixWithGBRPercentage").Range()

            Me.JA02_DiscontinuousProductionSummarySect = wordDoc.Bookmarks("JA02_DiscontinuousProductionSummarySect").Range
            Me.JT02_FirstDiscontinuousMixNumber = wordDoc.Bookmarks("JT02_FirstDiscontinuousMixNumber").Range
            Me.JT02_FirstDiscontinuousMixName = wordDoc.Bookmarks("JT02_FirstDiscontinuousMixName").Range
            Me.JT02_FirstDiscontinuousVirginACGrade = wordDoc.Bookmarks("JT02_FirstDiscontinuousVirginACGrade").Range
            Me.JT02_FirstDiscontinuousRecycledQuantity = wordDoc.Bookmarks("JT02_FirstDiscontinuousRecycledQuantity").Range
            Me.JT02_FirstDiscontinuousQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionTotalQty").Range
            Me.JT02_FirstDiscontinuousVirginACQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionAsphaltQty").Range
            Me.JT02_FirstDiscontinuousFeederDescription = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range
            Me.JT02_FirstDiscontinuousFeederQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederQty").Range
            Me.JT02_DiscontinuousTotalQuantity = wordDoc.Bookmarks("JT02_DiscontinuousTotalQuantity").Range
            Me.JT02_DiscontinuousTotalVirginACQuantity = wordDoc.Bookmarks("DiscontinuousProductionTotalAsphaltQty").Range
            Me.JT02_DiscontinuousFeederTotalQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFdrTotalQty").Range
            Me.JT02_DiscontinuousTotalCellsToMerge = wordDoc.Bookmarks("JT02_DiscontinuousTotalCellsToMerge").Range
            Me.JT02_DiscontinuousWithRAPPercentage = wordDoc.Bookmarks("DiscontinuousProdMixWithGBRPercentage").Range()

            Me.KA01_Comments = wordDoc.Bookmarks("KA01_Comments").Range

            Me.LA01_OperatorName = wordDoc.Bookmarks("LA01_OperatorName").Range

            Me.BA01_FooterDate = wordDoc.Bookmarks("BA01_FooterDate").Range
            Me.LA02_SignatureDate = wordDoc.Bookmarks("LA02_SignatureDate").Range

        End Sub

        Public Sub reinitializeContinuousProductionSummaryBookMarks(wordDoc As Document)

            Me.JA01_ContinuousProductionSummarySection = wordDoc.Bookmarks("JA01_ContinuousProductionSummarySection").Range
            Me.JT01_FirstContinuousMixNumber = wordDoc.Bookmarks("JT01_FirstContinuousMixNumber").Range
            Me.JT01_FirstContinuousMixName = wordDoc.Bookmarks("JT01_FirstContinuousMixName").Range
            Me.JT01_FirstContinuousVirginACGrade = wordDoc.Bookmarks("JT01_FirstContinuousVirginACGrade").Range
            Me.JT01_FirstContinuousRecycledQuantity = wordDoc.Bookmarks("JT01_FirstContinuousRecycledQuantity").Range
            Me.JT01_FirstContinuousQuantity = wordDoc.Bookmarks("JT01_FirstContinuousQuantity").Range
            Me.JT01_FirstContinuousVirginACQuantity = wordDoc.Bookmarks("JT01_FirstContinuousVirginACQuantity").Range
            Me.JT01_FirstContinuousFeederDescription = wordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range
            Me.JT01_FirstContinuousFeederQuantity = wordDoc.Bookmarks("JT01_FirstContinuousFeederQuantity").Range
            Me.JT01_ContinuousTotalQuantity = wordDoc.Bookmarks("JT01_ContinuousTotalQuantity").Range
            Me.JT01_ContinuousTotalVirginACQuantity = wordDoc.Bookmarks("JT01_ContinuousTotalVirginACQuantity").Range
            Me.JT01_ContinuousFeederTotalQuantity = wordDoc.Bookmarks("FirstContinuousProductionFeederTotalQty").Range
            Me.JT01_ContinuousTotalCellsToMerge = wordDoc.Bookmarks("JT01_ContinuousTotalCellsToMerge").Range
            Me.JT01_ContinuousWithRAPPercentage = wordDoc.Bookmarks("ContinuousProductionMixWithGBRPercentage").Range()

        End Sub

        Public Sub reinitializeDiscontinuousProductionSummaryBookMarks(wordDoc As Document)

            Me.JA02_DiscontinuousProductionSummarySect = wordDoc.Bookmarks("JA02_DiscontinuousProductionSummarySect").Range
            Me.JT02_FirstDiscontinuousMixNumber = wordDoc.Bookmarks("JT02_FirstDiscontinuousMixNumber").Range
            Me.JT02_FirstDiscontinuousMixName = wordDoc.Bookmarks("JT02_FirstDiscontinuousMixName").Range
            Me.JT02_FirstDiscontinuousVirginACGrade = wordDoc.Bookmarks("JT02_FirstDiscontinuousVirginACGrade").Range
            Me.JT02_FirstDiscontinuousRecycledQuantity = wordDoc.Bookmarks("JT02_FirstDiscontinuousRecycledQuantity").Range
            Me.JT02_FirstDiscontinuousQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionTotalQty").Range
            Me.JT02_FirstDiscontinuousVirginACQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionAsphaltQty").Range
            Me.JT02_FirstDiscontinuousFeederDescription = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range
            Me.JT02_FirstDiscontinuousFeederQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederQty").Range
            Me.JT02_DiscontinuousTotalQuantity = wordDoc.Bookmarks("JT02_DiscontinuousTotalQuantity").Range
            Me.JT02_DiscontinuousTotalVirginACQuantity = wordDoc.Bookmarks("DiscontinuousProductionTotalAsphaltQty").Range
            Me.JT02_DiscontinuousFeederTotalQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFdrTotalQty").Range
            Me.JT02_DiscontinuousTotalCellsToMerge = wordDoc.Bookmarks("JT02_DiscontinuousTotalCellsToMerge").Range
            Me.JT02_DiscontinuousWithRAPPercentage = wordDoc.Bookmarks("DiscontinuousProdMixWithGBRPercentage").Range()

        End Sub

    End Class

End Namespace
