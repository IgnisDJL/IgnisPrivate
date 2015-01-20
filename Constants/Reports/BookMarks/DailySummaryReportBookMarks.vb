Imports Microsoft.Office.Interop.Word

Namespace Constants.Reports.BookMarks

    Public Class SummaryDailyReportBookMarks

        Public FactoryName As Microsoft.Office.Interop.Word.Range
        Public FactoryId As Microsoft.Office.Interop.Word.Range

        Public ProductionDayDate As Microsoft.Office.Interop.Word.Range

        Public OperationStartTime As Microsoft.Office.Interop.Word.Range
        Public OperationEndTime As Microsoft.Office.Interop.Word.Range
        Public OperationDuration As Microsoft.Office.Interop.Word.Range
        Public ProductionStartTime As Microsoft.Office.Interop.Word.Range
        Public ProductionEndTime As Microsoft.Office.Interop.Word.Range
        Public ProductionDuration As Microsoft.Office.Interop.Word.Range
        Public FirstLoadingTime As Microsoft.Office.Interop.Word.Range
        Public LastLoadingTime As Microsoft.Office.Interop.Word.Range
        Public LoadingDuration As Microsoft.Office.Interop.Word.Range
        Public PausesDuration As Microsoft.Office.Interop.Word.Range
        Public MaintenanceDuration As Microsoft.Office.Interop.Word.Range

        Public FirstMixName As Microsoft.Office.Interop.Word.Range
        Public FirstMixAsphaltTemperatureSpan As Microsoft.Office.Interop.Word.Range
        Public FirstMixQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public FirstMixProductionType As Microsoft.Office.Interop.Word.Range
        Public SecondMixName As Microsoft.Office.Interop.Word.Range
        Public SecondMixAsphaltTemperatureSpan As Microsoft.Office.Interop.Word.Range
        Public SecondMixQuantity As Microsoft.Office.Interop.Word.Range
        Public SecondMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public SecondMixProductionType As Microsoft.Office.Interop.Word.Range
        Public ThirdMixName As Microsoft.Office.Interop.Word.Range
        Public ThirdMixAsphaltTemperatureSpan As Microsoft.Office.Interop.Word.Range
        Public ThirdMixQuantity As Microsoft.Office.Interop.Word.Range
        Public ThirdMixProductionRate As Microsoft.Office.Interop.Word.Range
        Public ThirdMixProductionType As Microsoft.Office.Interop.Word.Range

        Public NumberOfOtherMixes As Microsoft.Office.Interop.Word.Range
        Public OtherMixesQuantity As Microsoft.Office.Interop.Word.Range
        Public OtherMixesProductionRate As Microsoft.Office.Interop.Word.Range
        Public OtherMixesProductionType As Microsoft.Office.Interop.Word.Range
        Public TotalQuantityProduced As Microsoft.Office.Interop.Word.Range
        Public TotalProductionRate As Microsoft.Office.Interop.Word.Range
        Public SiloQuantityAtStart As Microsoft.Office.Interop.Word.Range
        Public SiloQuantityAtEnd As Microsoft.Office.Interop.Word.Range

        Public SalableQuantity As Microsoft.Office.Interop.Word.Range
        Public RejectedMixQuantity As Microsoft.Office.Interop.Word.Range
        Public RejectedMixPercentage As Microsoft.Office.Interop.Word.Range
        Public TotalPayableQuantity As Microsoft.Office.Interop.Word.Range
        Public TotalQuantitySold As Microsoft.Office.Interop.Word.Range
        Public TotalQuantitySoldDifferencePercentage As Microsoft.Office.Interop.Word.Range

        Public ProductionQuantityGraphic As Microsoft.Office.Interop.Word.Range
        Public ProductionRateGraphic As Microsoft.Office.Interop.Word.Range

        Public ContinuousProductionDuration As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionDuration As Microsoft.Office.Interop.Word.Range
        Public DelaysDuration As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionPercentage As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionPercentage As Microsoft.Office.Interop.Word.Range
        Public DelaysPercentage As Microsoft.Office.Interop.Word.Range
        Public NbSwitchContinuous As Microsoft.Office.Interop.Word.Range
        Public NbMixSwitchDiscontinuous As Microsoft.Office.Interop.Word.Range
        Public NumberOfDelays As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionQuantity As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionQuantity As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionRate As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionRate As Microsoft.Office.Interop.Word.Range

        Public GrossOperationDuration As Microsoft.Office.Interop.Word.Range
        Public NetOperationDuration As Microsoft.Office.Interop.Word.Range
        Public NetProductionDuration As Microsoft.Office.Interop.Word.Range
        Public EffectiveProductionDuration As Microsoft.Office.Interop.Word.Range
        Public EffectiveInternProductionDuration As Microsoft.Office.Interop.Word.Range
        Public AllDelaysDuration As Microsoft.Office.Interop.Word.Range

        Public NbOfBreakages As Microsoft.Office.Interop.Word.Range
        Public DisponibilityPercentage As Microsoft.Office.Interop.Word.Range
        Public UtilisationPercentage As Microsoft.Office.Interop.Word.Range
        Public TimeBetweenBreakDowns As Microsoft.Office.Interop.Word.Range
        Public ReparationsDuration As Microsoft.Office.Interop.Word.Range

        Public ProductionDistributionGraphic As Microsoft.Office.Interop.Word.Range
        Public DelaysDistributionGraphic As Microsoft.Office.Interop.Word.Range

        Public FirstAsphaltNumber As Microsoft.Office.Interop.Word.Range
        Public FirstAsphaltName As Microsoft.Office.Interop.Word.Range
        Public FirstAsphaltQuantity As Microsoft.Office.Interop.Word.Range
        Public TotalAsphaltQuantity As Microsoft.Office.Interop.Word.Range

        Public AsphaltDifferencePercentage As Microsoft.Office.Interop.Word.Range
        Public OverallTemperatureDifference As Microsoft.Office.Interop.Word.Range
        Public OverallTemperatureVariation As Microsoft.Office.Interop.Word.Range
        Public AsphaltAberrancePercentage As Microsoft.Office.Interop.Word.Range
        Public TemperatureAberrancePercentage As Microsoft.Office.Interop.Word.Range

        Public TemperatureVariationGraphic As Microsoft.Office.Interop.Word.Range

        Public Fuel1Name As Microsoft.Office.Interop.Word.Range
        Public Fuel1Quantity As Microsoft.Office.Interop.Word.Range
        Public Fuel1ConsumptionRate As Microsoft.Office.Interop.Word.Range
        Public Fuel2Name As Microsoft.Office.Interop.Word.Range
        Public Fuel2Quantity As Microsoft.Office.Interop.Word.Range
        Public Fuel2ConsumptionRate As Microsoft.Office.Interop.Word.Range

        Public RejectedAggregates As Microsoft.Office.Interop.Word.Range
        Public RejectedAggregatesPercentage As Microsoft.Office.Interop.Word.Range
        Public RejectedFiller As Microsoft.Office.Interop.Word.Range
        Public RejectedFillerPercentage As Microsoft.Office.Interop.Word.Range
        Public RejectedRecycled As Microsoft.Office.Interop.Word.Range
        Public RejectedRecycledPercentage As Microsoft.Office.Interop.Word.Range

        Public FirstDelayStartTime As Microsoft.Office.Interop.Word.Range
        Public FirstDelayEndTime As Microsoft.Office.Interop.Word.Range
        Public FirstDelayDuration As Microsoft.Office.Interop.Word.Range
        Public FirstDelayCode As Microsoft.Office.Interop.Word.Range
        Public FirstDelayDescription As Microsoft.Office.Interop.Word.Range
        Public FirstDelayJustification As Microsoft.Office.Interop.Word.Range
        Public JustificationDuration As Microsoft.Office.Interop.Word.Range
        Public NbDelaysNotJustified As Microsoft.Office.Interop.Word.Range
        Public DelaysNotJustifiedDuration As Microsoft.Office.Interop.Word.Range

        Public ContinuousProductionSummarySection As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionFormulaName As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionMixName As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionAsphaltName As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionRAP As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionAsphaltQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionFeederQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionFeederDescription As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionTotalAsphaltQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstContinuousProductionFeederTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionTotalCellsToMerge As Microsoft.Office.Interop.Word.Range
        Public ContinuousProductionMixWithRecycledPercentage As Microsoft.Office.Interop.Word.Range

        Public DiscontinuousProductionSummarySection As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionFormulaName As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionMixName As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionAsphaltName As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionRAP As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionAsphaltQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionFeederQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionFeederDescription As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionTotalAsphaltQuantity As Microsoft.Office.Interop.Word.Range
        Public FirstDiscontinuousProductionFeederTotalQuantity As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionTotalCellsToMerge As Microsoft.Office.Interop.Word.Range
        Public DiscontinuousProductionMixWithRecycledPercentage As Microsoft.Office.Interop.Word.Range

        Public Comments As Microsoft.Office.Interop.Word.Range

        Public OperatorName As Microsoft.Office.Interop.Word.Range

        Public CurrentDate1 As Microsoft.Office.Interop.Word.Range
        Public CurrentDate2 As Microsoft.Office.Interop.Word.Range


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="wordDoc"></param>
        ''' <remarks>
        ''' Use the same variable name as the bookmark string
        ''' Use regex powered search and replace.
        ''' </remarks>
        Sub initialize(wordDoc As Document)

            Me.FactoryName = wordDoc.Bookmarks("FactoryName").Range
            Me.FactoryId = wordDoc.Bookmarks("FactoryId").Range

            Me.ProductionDayDate = wordDoc.Bookmarks("ProductionDayDate").Range

            Me.OperationStartTime = wordDoc.Bookmarks("OperationStartTime").Range
            Me.OperationEndTime = wordDoc.Bookmarks("OperationEndTime").Range
            Me.OperationDuration = wordDoc.Bookmarks("OperationDuration").Range
            Me.ProductionStartTime = wordDoc.Bookmarks("ProductionStartTime").Range
            Me.ProductionEndTime = wordDoc.Bookmarks("ProductionEndTime").Range
            Me.ProductionDuration = wordDoc.Bookmarks("ProductionDuration").Range
            Me.FirstLoadingTime = wordDoc.Bookmarks("FirstLoadingTime").Range
            Me.LastLoadingTime = wordDoc.Bookmarks("LastLoadingTime").Range
            Me.LoadingDuration = wordDoc.Bookmarks("LoadingDuration").Range
            Me.PausesDuration = wordDoc.Bookmarks("PausesDuration").Range
            Me.MaintenanceDuration = wordDoc.Bookmarks("MaintenanceDuration").Range

            Me.FirstMixName = wordDoc.Bookmarks("FirstMixName").Range
            Me.FirstMixAsphaltTemperatureSpan = wordDoc.Bookmarks("FirstMixAsphaltTemperatureSpan").Range
            Me.FirstMixQuantity = wordDoc.Bookmarks("FirstMixQuantity").Range
            Me.FirstMixProductionRate = wordDoc.Bookmarks("FirstMixProductionRate").Range
            Me.FirstMixProductionType = wordDoc.Bookmarks("FirstMixProductionType").Range
            Me.SecondMixName = wordDoc.Bookmarks("SecondMixName").Range
            Me.SecondMixAsphaltTemperatureSpan = wordDoc.Bookmarks("SecondMixAsphaltTemperatureSpan").Range
            Me.SecondMixQuantity = wordDoc.Bookmarks("SecondMixQuantity").Range
            Me.SecondMixProductionRate = wordDoc.Bookmarks("SecondMixProductionRate").Range
            Me.SecondMixProductionType = wordDoc.Bookmarks("SecondMixProductionType").Range
            Me.ThirdMixName = wordDoc.Bookmarks("ThirdMixName").Range
            Me.ThirdMixAsphaltTemperatureSpan = wordDoc.Bookmarks("ThirdMixAsphaltTemperatureSpan").Range
            Me.ThirdMixQuantity = wordDoc.Bookmarks("ThirdMixQuantity").Range
            Me.ThirdMixProductionRate = wordDoc.Bookmarks("ThirdMixProductionRate").Range
            Me.ThirdMixProductionType = wordDoc.Bookmarks("ThirdMixProductionType").Range

            Me.NumberOfOtherMixes = wordDoc.Bookmarks("NumberOfOtherMixes").Range
            Me.OtherMixesQuantity = wordDoc.Bookmarks("OtherMixesQuantity").Range
            Me.OtherMixesProductionRate = wordDoc.Bookmarks("OtherMixesProductionRate").Range
            Me.OtherMixesProductionType = wordDoc.Bookmarks("OtherMixesProductionType").Range
            Me.TotalQuantityProduced = wordDoc.Bookmarks("TotalQuantityProduced").Range
            Me.TotalProductionRate = wordDoc.Bookmarks("TotalProductionRate").Range
            Me.SiloQuantityAtStart = wordDoc.Bookmarks("SiloQuantityAtStart").Range
            Me.SiloQuantityAtEnd = wordDoc.Bookmarks("SiloQuantityAtEnd").Range

            Me.SalableQuantity = wordDoc.Bookmarks("SalableQuantity").Range
            Me.RejectedMixQuantity = wordDoc.Bookmarks("RejectedMixQuantity").Range
            Me.RejectedMixPercentage = wordDoc.Bookmarks("RejectedMixPercentage").Range
            Me.TotalPayableQuantity = wordDoc.Bookmarks("TotalPayableQuantity").Range
            Me.TotalQuantitySold = wordDoc.Bookmarks("TotalQuantitySold").Range
            Me.TotalQuantitySoldDifferencePercentage = wordDoc.Bookmarks("TotalQuantitySoldDifferencePercentage").Range

            Me.ProductionQuantityGraphic = wordDoc.Bookmarks("ProductionQuantityGraphic").Range
            Me.ProductionRateGraphic = wordDoc.Bookmarks("ProductionRateGraphic").Range

            Me.ContinuousProductionDuration = wordDoc.Bookmarks("ContinuousProductionDuration").Range
            Me.DiscontinuousProductionDuration = wordDoc.Bookmarks("DiscontinuousProductionDuration").Range
            Me.DelaysDuration = wordDoc.Bookmarks("DelaysDuration").Range
            Me.ContinuousProductionPercentage = wordDoc.Bookmarks("ContinuousProductionPercentage").Range
            Me.DiscontinuousProductionPercentage = wordDoc.Bookmarks("DiscontinuousProductionPercentage").Range
            Me.DelaysPercentage = wordDoc.Bookmarks("DelaysPercentage").Range
            Me.NbSwitchContinuous = wordDoc.Bookmarks("NbSwitchContinuous").Range
            Me.NbMixSwitchDiscontinuous = wordDoc.Bookmarks("NbMixSwitchDiscontinuous").Range
            Me.NumberOfDelays = wordDoc.Bookmarks("NumberOfDelays").Range
            Me.ContinuousProductionQuantity = wordDoc.Bookmarks("ContinuousProductionQuantity").Range
            Me.DiscontinuousProductionQuantity = wordDoc.Bookmarks("DiscontinuousProductionQuantity").Range
            Me.ContinuousProductionRate = wordDoc.Bookmarks("ContinuousProductionRate").Range
            Me.DiscontinuousProductionRate = wordDoc.Bookmarks("DiscontinuousProductionRate").Range

            Me.GrossOperationDuration = wordDoc.Bookmarks("GrossOperationDuration").Range
            Me.NetOperationDuration = wordDoc.Bookmarks("NetOperationDuration").Range
            Me.NetProductionDuration = wordDoc.Bookmarks("NetProductionDuration").Range
            Me.EffectiveProductionDuration = wordDoc.Bookmarks("EffectiveProductionDuration").Range
            Me.EffectiveInternProductionDuration = wordDoc.Bookmarks("EffectiveInternProductionDuration").Range
            Me.AllDelaysDuration = wordDoc.Bookmarks("AllDelaysDuration").Range

            Me.NbOfBreakages = wordDoc.Bookmarks("NbOfBreakages").Range
            Me.DisponibilityPercentage = wordDoc.Bookmarks("DisponibilityPercentage").Range
            Me.UtilisationPercentage = wordDoc.Bookmarks("UtilisationPercentage").Range
            Me.TimeBetweenBreakDowns = wordDoc.Bookmarks("TimeBetweenBreakDowns").Range
            Me.ReparationsDuration = wordDoc.Bookmarks("ReparationsDuration").Range

            Me.ProductionDistributionGraphic = wordDoc.Bookmarks("ProductionDistributionGraphic").Range
            Me.DelaysDistributionGraphic = wordDoc.Bookmarks("DelaysDistributionGraphic").Range

            Me.FirstAsphaltNumber = wordDoc.Bookmarks("FirstAsphaltNumber").Range
            Me.FirstAsphaltName = wordDoc.Bookmarks("FirstAsphaltName").Range
            Me.FirstAsphaltQuantity = wordDoc.Bookmarks("FirstAsphaltQuantity").Range
            Me.TotalAsphaltQuantity = wordDoc.Bookmarks("TotalAsphaltQuantity").Range

            Me.AsphaltDifferencePercentage = wordDoc.Bookmarks("AsphaltDifferencePercentage").Range
            Me.OverallTemperatureDifference = wordDoc.Bookmarks("OverallTemperatureDifference").Range
            Me.OverallTemperatureVariation = wordDoc.Bookmarks("OverallTemperatureVariation").Range
            Me.AsphaltAberrancePercentage = wordDoc.Bookmarks("AsphaltAberrancePercentage").Range
            Me.TemperatureAberrancePercentage = wordDoc.Bookmarks("TemperatureAberrancePercentage").Range

            Me.TemperatureVariationGraphic = wordDoc.Bookmarks("TemperatureVariationGraphic").Range

            Me.Fuel1Name = wordDoc.Bookmarks("Fuel1Name").Range
            Me.Fuel1Quantity = wordDoc.Bookmarks("Fuel1Quantity").Range
            Me.Fuel1ConsumptionRate = wordDoc.Bookmarks("Fuel1ConsumptionRate").Range
            Me.Fuel2Name = wordDoc.Bookmarks("Fuel2Name").Range
            Me.Fuel2Quantity = wordDoc.Bookmarks("Fuel2Quantity").Range
            Me.Fuel2ConsumptionRate = wordDoc.Bookmarks("Fuel2ConsumptionRate").Range

            Me.RejectedAggregates = wordDoc.Bookmarks("RejectedAggregates").Range
            Me.RejectedAggregatesPercentage = wordDoc.Bookmarks("RejectedAggregatesPercentage").Range
            Me.RejectedFiller = wordDoc.Bookmarks("RejectedFiller").Range
            Me.RejectedFillerPercentage = wordDoc.Bookmarks("RejectedFillerPercentage").Range
            Me.RejectedRecycled = wordDoc.Bookmarks("RejectedRecycled").Range
            Me.RejectedRecycledPercentage = wordDoc.Bookmarks("RejectedRecycledPercentage").Range

            Me.FirstDelayStartTime = wordDoc.Bookmarks("FirstDelayStartTime").Range
            Me.FirstDelayEndTime = wordDoc.Bookmarks("FirstDelayEndTime").Range
            Me.FirstDelayDuration = wordDoc.Bookmarks("FirstDelayDuration").Range
            Me.FirstDelayCode = wordDoc.Bookmarks("FirstDelayCode").Range
            Me.FirstDelayDescription = wordDoc.Bookmarks("FirstDelayDescription").Range
            Me.FirstDelayJustification = wordDoc.Bookmarks("FirstDelayJustification").Range
            Me.JustificationDuration = wordDoc.Bookmarks("JustificationDuration").Range
            Me.NbDelaysNotJustified = wordDoc.Bookmarks("NbDelaysNotJustified").Range
            Me.DelaysNotJustifiedDuration = wordDoc.Bookmarks("DelaysNotJustifiedDuration").Range

            Me.ContinuousProductionSummarySection = wordDoc.Bookmarks("ContinuousProductionSummarySection").Range
            Me.FirstContinuousProductionFormulaName = wordDoc.Bookmarks("FirstContinuousProductionFormulaName").Range
            Me.FirstContinuousProductionMixName = wordDoc.Bookmarks("FirstContinuousProductionMixName").Range
            Me.FirstContinuousProductionAsphaltName = wordDoc.Bookmarks("FirstContinuousProductionAsphaltName").Range
            Me.FirstContinuousProductionRAP = wordDoc.Bookmarks("FirstContinuousProductionRAP").Range
            Me.FirstContinuousProductionTotalQuantity = wordDoc.Bookmarks("FirstContinuousProductionTotalQuantity").Range
            Me.FirstContinuousProductionAsphaltQuantity = wordDoc.Bookmarks("FirstContinuousProductionAsphaltQuantity").Range
            Me.FirstContinuousProductionFeederDescription = wordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range
            Me.FirstContinuousProductionFeederQuantity = wordDoc.Bookmarks("FirstContinuousProductionFeederQuantity").Range
            Me.ContinuousProductionTotalQuantity = wordDoc.Bookmarks("ContinuousProductionTotalQuantity").Range
            Me.ContinuousProductionTotalAsphaltQuantity = wordDoc.Bookmarks("ContinuousProductionTotalAsphaltQuantity").Range
            Me.FirstContinuousProductionFeederTotalQuantity = wordDoc.Bookmarks("FirstContinuousProductionFeederTotalQty").Range
            Me.ContinuousProductionTotalCellsToMerge = wordDoc.Bookmarks("ContinuousProductionTotalCellsToMerge").Range
            Me.ContinuousProductionMixWithRecycledPercentage = wordDoc.Bookmarks("ContinuousProductionMixWithGBRPercentage").Range()

            Me.DiscontinuousProductionSummarySection = wordDoc.Bookmarks("DiscontinuousProductionSummarySection").Range
            Me.FirstDiscontinuousProductionFormulaName = wordDoc.Bookmarks("FirstDiscontinuousProductionFormulaName").Range
            Me.FirstDiscontinuousProductionMixName = wordDoc.Bookmarks("FirstDiscontinuousProductionMixName").Range
            Me.FirstDiscontinuousProductionAsphaltName = wordDoc.Bookmarks("FirstDiscontinuousProductionAsphaltName").Range
            Me.FirstDiscontinuousProductionRAP = wordDoc.Bookmarks("FirstDiscontinuousProductionRAP").Range
            Me.FirstDiscontinuousProductionTotalQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionTotalQty").Range
            Me.FirstDiscontinuousProductionAsphaltQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionAsphaltQty").Range
            Me.FirstDiscontinuousProductionFeederDescription = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range
            Me.FirstDiscontinuousProductionFeederQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederQty").Range
            Me.DiscontinuousProductionTotalQuantity = wordDoc.Bookmarks("DiscontinuousProductionTotalQuantity").Range
            Me.DiscontinuousProductionTotalAsphaltQuantity = wordDoc.Bookmarks("DiscontinuousProductionTotalAsphaltQty").Range
            Me.FirstDiscontinuousProductionFeederTotalQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFdrTotalQty").Range
            Me.DiscontinuousProductionTotalCellsToMerge = wordDoc.Bookmarks("DiscontinuousProductionTotalCellsToMerge").Range
            Me.DiscontinuousProductionMixWithRecycledPercentage = wordDoc.Bookmarks("DiscontinuousProdMixWithGBRPercentage").Range()

            Me.Comments = wordDoc.Bookmarks("Comments").Range

            Me.OperatorName = wordDoc.Bookmarks("OperatorName").Range

            Me.CurrentDate1 = wordDoc.Bookmarks("CurrentDate1").Range
            Me.CurrentDate2 = wordDoc.Bookmarks("CurrentDate2").Range

        End Sub

        Public Sub reinitializeContinuousProductionSummaryBookMarks(wordDoc As Document)

            Me.ContinuousProductionSummarySection = wordDoc.Bookmarks("ContinuousProductionSummarySection").Range
            Me.FirstContinuousProductionFormulaName = wordDoc.Bookmarks("FirstContinuousProductionFormulaName").Range
            Me.FirstContinuousProductionMixName = wordDoc.Bookmarks("FirstContinuousProductionMixName").Range
            Me.FirstContinuousProductionAsphaltName = wordDoc.Bookmarks("FirstContinuousProductionAsphaltName").Range
            Me.FirstContinuousProductionRAP = wordDoc.Bookmarks("FirstContinuousProductionRAP").Range
            Me.FirstContinuousProductionTotalQuantity = wordDoc.Bookmarks("FirstContinuousProductionTotalQuantity").Range
            Me.FirstContinuousProductionAsphaltQuantity = wordDoc.Bookmarks("FirstContinuousProductionAsphaltQuantity").Range
            Me.FirstContinuousProductionFeederDescription = wordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range
            Me.FirstContinuousProductionFeederQuantity = wordDoc.Bookmarks("FirstContinuousProductionFeederQuantity").Range
            Me.ContinuousProductionTotalQuantity = wordDoc.Bookmarks("ContinuousProductionTotalQuantity").Range
            Me.ContinuousProductionTotalAsphaltQuantity = wordDoc.Bookmarks("ContinuousProductionTotalAsphaltQuantity").Range
            Me.FirstContinuousProductionFeederTotalQuantity = wordDoc.Bookmarks("FirstContinuousProductionFeederTotalQty").Range
            Me.ContinuousProductionTotalCellsToMerge = wordDoc.Bookmarks("ContinuousProductionTotalCellsToMerge").Range
            Me.ContinuousProductionMixWithRecycledPercentage = wordDoc.Bookmarks("ContinuousProductionMixWithGBRPercentage").Range()

        End Sub

        Public Sub reinitializeDiscontinuousProductionSummaryBookMarks(wordDoc As Document)

            Me.DiscontinuousProductionSummarySection = wordDoc.Bookmarks("DiscontinuousProductionSummarySection").Range
            Me.FirstDiscontinuousProductionFormulaName = wordDoc.Bookmarks("FirstDiscontinuousProductionFormulaName").Range
            Me.FirstDiscontinuousProductionMixName = wordDoc.Bookmarks("FirstDiscontinuousProductionMixName").Range
            Me.FirstDiscontinuousProductionAsphaltName = wordDoc.Bookmarks("FirstDiscontinuousProductionAsphaltName").Range
            Me.FirstDiscontinuousProductionRAP = wordDoc.Bookmarks("FirstDiscontinuousProductionRAP").Range
            Me.FirstDiscontinuousProductionTotalQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionTotalQty").Range
            Me.FirstDiscontinuousProductionAsphaltQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionAsphaltQty").Range
            Me.FirstDiscontinuousProductionFeederDescription = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range
            Me.FirstDiscontinuousProductionFeederQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFeederQty").Range
            Me.DiscontinuousProductionTotalQuantity = wordDoc.Bookmarks("DiscontinuousProductionTotalQuantity").Range
            Me.DiscontinuousProductionTotalAsphaltQuantity = wordDoc.Bookmarks("DiscontinuousProductionTotalAsphaltQty").Range
            Me.FirstDiscontinuousProductionFeederTotalQuantity = wordDoc.Bookmarks("FirstDiscontinuousProductionFdrTotalQty").Range
            Me.DiscontinuousProductionTotalCellsToMerge = wordDoc.Bookmarks("DiscontinuousProductionTotalCellsToMerge").Range
            Me.DiscontinuousProductionMixWithRecycledPercentage = wordDoc.Bookmarks("DiscontinuousProdMixWithGBRPercentage").Range()

        End Sub

    End Class

End Namespace
