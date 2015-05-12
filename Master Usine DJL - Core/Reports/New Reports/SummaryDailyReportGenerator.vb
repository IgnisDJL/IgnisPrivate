﻿Imports Microsoft.Office.Interop.Word

''' <summary>
''' 
''' </summary>
''' <remarks>
''' Find first and iterate from last method prooved to be the fastest (after comparing with using table objects).
''' I know it's a lot of duplicated code but it's worth it performance wise. Using bookmarks is also very fast.
''' </remarks>
Public Class SummaryDailyReportGenerator
    Inherits ReportGenerator
    Implements IGNIS.Eventing.TrackableProcess

    Private bookMarks As Constants.Reports.BookMarks.SummaryDailyReportBookMarks

    Public Sub New()
        'MyBase.New(ReportType.SummaryDailyReport, New SummaryDailyReportFormater)
        MyBase.New(New SummaryDailyReportFormater)
        initializeWordApplication()

        Me.bookMarks = New Constants.Reports.BookMarks.SummaryDailyReportBookMarks

    End Sub


    Public Sub generateReport(productionDay As ProductionDay_1)

        'RaiseEvent ProcessStarting(Me)

        'Try

        '    Me.WordDoc = WordApp.Documents.Open(Constants.Paths.SUMMARY_DAILY_REPORT_TEMPLATE, False, True)

        '    Me.bookMarks.initialize(Me.WordDoc)

        '    ' Factory Info
        '    bookMarks.AA01_HeaderPlantName.Text = XmlSettings.Settings.instance.Usine.PLANT_NAME
        '    bookMarks.AA02_HeaderPlantID.Text = XmlSettings.Settings.instance.Usine.PLANT_ID

        '    ' Date
        '    bookMarks.BA01_FooterDate.Text = productionDay.Date_.ToString(Me.Formater.ShortDateFormat)

        '    ' Add er when first of month
        '    ajustDateString(productionDay.Date_, bookMarks.BA01_FooterDate)

        '    ' --------
        '    ' TABLE 1
        '    ' --------

        '    ' Operation
        '    bookMarks.CT01_OperationStartTime.Text = productionDay.ManualData.OPERATION_START_TIME.ToString(Me.Formater.TimeFormat)
        '    bookMarks.CT01_OperationEndTime.Text = productionDay.ManualData.OPERATION_END_TIME.ToString(Me.Formater.TimeFormat)
        '    bookMarks.CT01_OperationDuration.Text = productionDay.ManualData.OPERATION_END_TIME.Subtract(productionDay.ManualData.OPERATION_START_TIME).ToString(Me.Formater.DurationFormat)

        '    ' Production
        '    bookMarks.CT01_ProductionStartTime.Text = productionDay.ManualData.PRODUCTION_START_TIME.ToString(Me.Formater.TimeFormat)
        '    bookMarks.CT01_ProductionEndTime.Text = productionDay.ManualData.PRODUCTION_END_TIME.ToString(Me.Formater.TimeFormat)
        '    bookMarks.CT01_ProductionDuration.Text = productionDay.ManualData.PRODUCTION_END_TIME.Subtract(productionDay.ManualData.PRODUCTION_START_TIME).ToString(Me.Formater.DurationFormat)

        '    ' Loading / weight station
        '    ' #refactor (if was set)
        '    bookMarks.CT01_LoadingStartTime.Text = productionDay.ManualData.FIRST_LOADING_TIME.ToString(Me.Formater.TimeFormat)
        '    bookMarks.CT01_LoadingEndTime.Text = productionDay.ManualData.LAST_LOADING_TIME.ToString(Me.Formater.TimeFormat)
        '    bookMarks.CT01_LoadingDuration.Text = productionDay.ManualData.LAST_LOADING_TIME.Subtract(productionDay.ManualData.FIRST_LOADING_TIME).ToString(Me.Formater.DurationFormat)


        '    bookMarks.CT01_PausesDuration.Text = productionDay.Statistics.EventsStatistics.CT01_PausesDuration.ToString(Me.Formater.DurationFormat)

        '    bookMarks.CT01_MaintenanceDuration.Text = productionDay.Statistics.EventsStatistics.CT01_MaintenanceDuration.ToString(Me.Formater.DurationFormat)

        '    RaiseEvent CurrentProgress(12) ' 12 % Progress

        '    ' --------
        '    ' TABLE 2 
        '    ' --------
        '    Dim firstMix As MixStatistics = productionDay.Statistics.AllMixes(0)

        '    bookMarks.CT02_FirstMixName.Text = firstMix.NAME

        '    ' Asphalt temp span  '#refactor - name of bookmark
        '    bookMarks.CT02_FirstMixVirginAsphaltConcreteGrade.Text = firstMix.ASPHALT_STATS.NAME

        '    bookMarks.CT02_FirstMixQuantity.Text = firstMix.TOTAL_MASS.ToString("N0")
        '    bookMarks.CT02_FirstMixProductionRate.Text = firstMix.AVERAGE_PRODUCTION_SPEED.ToString("N0")

        '    bookMarks.CT02_FirstMixProductionMode.Text = Me.Formater.getProductionTypeString(firstMix.PRODUCTION_TYPE)

        '    If (productionDay.Statistics.AllMixes.Count > 1) Then

        '        Dim secondMix As MixStatistics = productionDay.Statistics.AllMixes(1)

        '        bookMarks.CT02_SecondMixName.Text = secondMix.NAME

        '        ' Asphalt temp span
        '        bookMarks.CT02_SecondMixVirginAsphaltConcreteGrade.Text = secondMix.ASPHALT_STATS.NAME

        '        bookMarks.CT02_SecondMixQuantity.Text = secondMix.TOTAL_MASS.ToString("N0")
        '        bookMarks.CT02_SecondMixProductionRate.Text = secondMix.AVERAGE_PRODUCTION_SPEED.ToString("N0")

        '        bookMarks.CT02_SecondMixProductionMode.Text = Me.Formater.getProductionTypeString(secondMix.PRODUCTION_TYPE)
        '    Else
        '        bookMarks.CT02_SecondMixName.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_SecondMixVirginAsphaltConcreteGrade.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_SecondMixQuantity.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_SecondMixProductionRate.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_SecondMixProductionMode.Text = Me.Formater.InvalidValueCharacter
        '    End If

        '    If (productionDay.Statistics.AllMixes.Count > 2) Then

        '        Dim thirdMix As MixStatistics = productionDay.Statistics.AllMixes(2)

        '        bookMarks.CT02_ThirdMixName.Text = thirdMix.NAME

        '        ' Asphalt temp span
        '        bookMarks.CT02_ThirdMixVirginAsphaltConcreteGrade.Text = thirdMix.ASPHALT_STATS.NAME

        '        bookMarks.CT02_ThirdMixQuantity.Text = thirdMix.TOTAL_MASS.ToString("N0")
        '        bookMarks.CT02_ThirdMixProductionRate.Text = thirdMix.AVERAGE_PRODUCTION_SPEED.ToString("N0")

        '        bookMarks.CT02_ThirdMixProductionMode.Text = Me.Formater.getProductionTypeString(thirdMix.PRODUCTION_TYPE)

        '    Else

        '        bookMarks.CT02_ThirdMixName.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_ThirdMixVirginAsphaltConcreteGrade.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_ThirdMixQuantity.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_ThirdMixProductionRate.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.CT02_ThirdMixProductionMode.Text = Me.Formater.InvalidValueCharacter
        '    End If


        '    bookMarks.CT02_OtherMixesNumberOfMixes.Text = If(productionDay.Statistics.AllMixes.Count <= 3, "0", productionDay.Statistics.AllMixes.Count - 3)
        '    bookMarks.CT02_OtherMixesQuantity.Text = productionDay.Statistics.OtherMixes.TOTAL_MASS.ToString("N0")
        '    bookMarks.CT02_OtherMixesProductionRate.Text = productionDay.Statistics.OtherMixes.AVERAGE_PRODUCTION_SPEED.ToString("N0")

        '    ' Todo production type other mixes

        '    bookMarks.CT02_TotalMixQuantity.Text = productionDay.Statistics.MixesTotal.TOTAL_MASS.ToString("N0")
        '    bookMarks.CT02_TotalMixProductionRate.Text = productionDay.Statistics.MixesTotal.AVERAGE_PRODUCTION_SPEED.ToString("N0")

        '    ' Silo at start
        '    bookMarks.CT02_SiloStartQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.SILO_QUANTITY_AT_START)

        '    RaiseEvent CurrentProgress(24) ' 24 % Progress

        '    ' Silo at end
        '    bookMarks.CT02_SiloEndQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.SILO_QUANTITY_AT_END)

        '    ' Salable qty
        '    bookMarks.CT02_SaleableQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.SALABLE_QUANTITY, "N0")

        '    ' Rejected mix

        '    bookMarks.CT02_RejectedMixQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_MIX_QUANTITY, "N0")
        '    bookMarks.CT02_RejectedMixPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_MIX_PERCENTAGE, "N1")

        '    ' Payable qty
        '    bookMarks.CT02_PayableQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.PAYABLE_QUANTITY, "N0")

        '    ' Sold (weighted) qty

        '    bookMarks.CT02_SoldQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.WEIGHTED_QUANTITY, "N0")
        '    bookMarks.CT02_SoldQuantityDifferencePercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.WEIGHTED_QUANTITY_DIFFERENCE_PERCENTAGE, "N1")

        '    ' --------------------
        '    ' Graphic 1 et 2 #refactor generate before graphs and store them
        '    ' --------------------
        '    XYScatterGraphic.pointFormatList_asphalt = New PointFormatList
        '    XYScatterGraphic.pointFormatList_mix = New PointFormatList

        '    Dim isHybrid As Boolean = XmlSettings.Settings.instance.Usine.DataFiles.CSV.ACTIVE AndAlso XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE

        '    Dim accumulatedMass As New AccumulatedMassGraphic(productionDay.Date_, isHybrid)
        '    Dim productionSpeed As New ProductionSpeedGraphic(productionDay.Date_)

        '    For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

        '        If (TypeOf _cycle Is CSVCycle) Then

        '            accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
        '            productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

        '        ElseIf (TypeOf _cycle Is MDBCycle) Then

        '            accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
        '            productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
        '        End If

        '    Next

        '    accumulatedMass.toggleMarkerColor()
        '    productionSpeed.toggleMarkerColor()

        '    For Each _cycle In productionDay.Statistics.ContinuousProduction.Cycles

        '        accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
        '        productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
        '    Next

        '    accumulatedMass.save()
        '    productionSpeed.save()

        '    Dim g1 = bookMarks.CG01_ProductionQuantityGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC, False, True)
        '    Dim g2 = bookMarks.CG02_ProductionRateGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_SPEED_GRAPHIC, False, True)

        '    g1.Width = bookMarks.CG01_ProductionQuantityGraphic.Cells(1).Width
        '    g2.Width = bookMarks.CG01_ProductionQuantityGraphic.Cells(1).Width


        '    ' --------------------
        '    ' PRODUCTION ET DELAIS
        '    ' --------------------

        '    bookMarks.DT01_ContinuousDuration.Text = productionDay.Statistics.ContinuousProduction.Duration.ToString(Me.Formater.DurationFormat)
        '    bookMarks.DT01_DiscontinuousDuration.Text = productionDay.Statistics.DiscontinuousProduction.Duration.ToString(Me.Formater.DurationFormat)


        '    bookMarks.DT01_DelaysDuration.Text = productionDay.Statistics.EventsStatistics.DT01_DelaysDuration.ToString(Me.Formater.DurationFormat)

        '    bookMarks.DT01_ContinuousPercentage.Text = (productionDay.Statistics.ContinuousProduction.Duration.TotalSeconds / productionDay.ManualData.PRODUCTION_END_TIME.Subtract(productionDay.ManualData.PRODUCTION_START_TIME).TotalSeconds * 100).ToString("N0")
        '    bookMarks.DT01_DiscontinuousPercentage.Text = (productionDay.Statistics.DiscontinuousProduction.Duration.TotalSeconds / productionDay.ManualData.PRODUCTION_END_TIME.Subtract(productionDay.ManualData.PRODUCTION_START_TIME).TotalSeconds * 100).ToString("N0")
        '    bookMarks.DT01_DelaysPercentage.Text = (productionDay.Statistics.EventsStatistics.DT01_DelaysDuration.TotalSeconds / productionDay.ManualData.OPERATION_END_TIME.Subtract(productionDay.ManualData.OPERATION_START_TIME).TotalSeconds * 100).ToString("N0")

        '    bookMarks.DT01_ContinuousMixChange.Text = productionDay.Statistics.ContinuousProduction.NbMixSwitch
        '    bookMarks.DT01_DiscontinuousMixChange.Text = productionDay.Statistics.DiscontinuousProduction.NbMixSwitch
        '    bookMarks.DT01_DelaysNumber.Text = productionDay.Statistics.EventsStatistics.Delays.Count

        '    bookMarks.DT01_ContinuousQuantity.Text = productionDay.Statistics.ContinuousProduction.Quantity.ToString("N0")
        '    bookMarks.DT01_DiscontinuousQuantity.Text = productionDay.Statistics.DiscontinuousProduction.Quantity.ToString("N0")

        '    Dim avgProdRate As Double = 0
        '    Dim nbCyclesAnalysedForProdRate As Integer = 0

        '    For Each _cycle As Cycle In productionDay.Statistics.ContinuousProduction.Cycles
        '        If (_cycle.PRODUCTION_SPEED > 0) Then
        '            avgProdRate += XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.PRODUCTION_SPEED, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '            nbCyclesAnalysedForProdRate += 1
        '        End If
        '    Next

        '    avgProdRate = If(nbCyclesAnalysedForProdRate = 0, 0, avgProdRate / nbCyclesAnalysedForProdRate)

        '    bookMarks.DT01_ContinuousProductionRate.Text = avgProdRate.ToString("N0")

        '    RaiseEvent CurrentProgress(36) ' 36 % Progress

        '    avgProdRate = 0
        '    nbCyclesAnalysedForProdRate = 0

        '    For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles
        '        If (_cycle.PRODUCTION_SPEED > 0) Then

        '            If (TypeOf _cycle Is CSVCycle) Then

        '                avgProdRate += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.PRODUCTION_SPEED, XmlSettings.Settings.instance.Reports.MASS_UNIT)

        '            ElseIf (TypeOf _cycle Is MDBCycle) Then
        '                avgProdRate += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.PRODUCTION_SPEED, XmlSettings.Settings.instance.Reports.MASS_UNIT)

        '            End If

        '            nbCyclesAnalysedForProdRate += 1
        '        End If
        '    Next

        '    avgProdRate = If(nbCyclesAnalysedForProdRate = 0, 0, avgProdRate / nbCyclesAnalysedForProdRate)

        '    bookMarks.DT01_DiscontinuousProductionRate.Text = avgProdRate.ToString("N0")

        '    ' -------------------
        '    ' TEMPS DE PRODUCTION
        '    ' -------------------

        '    ' Temps total d’opération = Heure fin d’opération – Heure début d’opération
        '    Dim grossOperationDuration As TimeSpan = productionDay.ManualData.OPERATION_END_TIME.Subtract(productionDay.ManualData.OPERATION_START_TIME)
        '    bookMarks.DT02_TotalOperationDuration.Text = grossOperationDuration.ToString(Me.Formater.DurationFormat)

        '    ' Temps net d’opération = Temps total d’opération – temps Pause(s)
        '    Dim netOperationDuration = grossOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.CT01_PausesDuration)
        '    bookMarks.DT02_NetOperationDuration.Text = netOperationDuration.ToString(Me.Formater.DurationFormat)

        '    ' Production nette = Temps net d’opération – temps Entretien(s)
        '    Dim netProductionDuration As TimeSpan = netOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.CT01_MaintenanceDuration)
        '    bookMarks.DT02_NetProductionDuration.Text = netProductionDuration.ToString(Me.Formater.DurationFormat)

        '    'Production efficace = Temps total d’opération – (temps Pause(s) + temps Entretien(s) + temps Délais)
        '    Dim effectiveProductionDuration As TimeSpan = grossOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.CT01_PausesDuration).Subtract(productionDay.Statistics.EventsStatistics.CT01_MaintenanceDuration).Subtract(productionDay.Statistics.EventsStatistics.DT01_DelaysDuration)
        '    bookMarks.DT02_EffectiveProductionDuration.Text = effectiveProductionDuration.ToString(Me.Formater.DurationFormat)

        '    ' Production efficace interne = Temps total d’opération – (temps Pause(s) + temps Entretien(s) + temps Délais interne (code 1 à 46))
        '    Dim effectiveInternProductionDuration As TimeSpan = grossOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.CT01_PausesDuration).Subtract(productionDay.Statistics.EventsStatistics.CT01_MaintenanceDuration).Subtract(productionDay.Statistics.EventsStatistics.InternDelaysDuration)
        '    bookMarks.DT02_EffectiveInternalDuration.Text = effectiveInternProductionDuration.ToString(Me.Formater.DurationFormat)

        '    bookMarks.DT02_DelaysDuration.Text = productionDay.Statistics.EventsStatistics.DT01_DelaysDuration.ToString(Me.Formater.DurationFormat)

        '    ' -------
        '    ' DELAIS
        '    ' -------

        '    bookMarks.DT03_BreakageNumber.Text = productionDay.Statistics.EventsStatistics.DT03_BreakageNumber

        '    ' Disponibilité (%) = Production efficace interne / Production nette * 100
        '    bookMarks.DT03_DisponibilityPercentage.Text = (effectiveInternProductionDuration.TotalSeconds / netProductionDuration.TotalSeconds * 100).ToString("N0")
        '    ' Utilisation (%) = Production efficace / Temps total d’opération
        '    bookMarks.DT03_UtilisationPercentage.Text = (effectiveProductionDuration.TotalSeconds / grossOperationDuration.TotalSeconds * 100).ToString("N0")

        '    If (productionDay.Statistics.EventsStatistics.DT03_BreakageNumber > 0) Then

        '        bookMarks.DT03_TempsEntrePannes.Text = TimeSpan.FromSeconds(effectiveProductionDuration.TotalSeconds / productionDay.Statistics.EventsStatistics.DT03_BreakageNumber).ToString(Me.Formater.DurationFormat)
        '        bookMarks.DT03_TempsPourReparer.Text = TimeSpan.FromSeconds(productionDay.Statistics.EventsStatistics.InternBreakagesDuration.TotalSeconds / productionDay.Statistics.EventsStatistics.DT03_BreakageNumber).ToString(Me.Formater.DurationFormat)

        '    Else

        '        bookMarks.DT03_TempsEntrePannes.Text = Me.Formater.InvalidValueCharacter
        '        bookMarks.DT03_TempsPourReparer.Text = Me.Formater.InvalidValueCharacter

        '    End If

        '    ' ----------------------
        '    ' DISTRIBUTION GRAPHICS
        '    ' ----------------------

        '    Dim pdg = New DG01_ProductionDistributionGraphic(productionDay.Statistics.CT01_ProductionEndTime.Subtract(productionDay.Statistics.CT01_ProductionStartTime), _
        '                                                productionDay.Statistics.EventsStatistics.CT01_PausesDuration, _
        '                                                productionDay.Statistics.EventsStatistics.CT01_MaintenanceDuration, _
        '                                                productionDay.Statistics.EventsStatistics.DT01_DelaysDuration)

        '    pdg.save()

        '    Dim ddg = New DG02_DelaysDistributionGraphic(productionDay.Statistics.EventsStatistics.InternWithBreakageDuration, _
        '                                            productionDay.Statistics.EventsStatistics.InternWithoutBreakageDuration, _
        '                                            productionDay.Statistics.EventsStatistics.ExternDuration, _
        '                                            productionDay.Statistics.EventsStatistics.OtherDelaysDuration)

        '    ddg.save()

        '    Dim g3 = bookMarks.DG01_ProductionDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_DISTRIBUTION_GRAPHIC, False, True)

        '    g3.Width = bookMarks.DG01_ProductionDistributionGraphic.Cells(1).Width

        '    Dim g4 = bookMarks.DG02_DelaysDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.DELAYS_DISTRIBUTION_GRAPHIC, False, True)

        '    g4.Width = bookMarks.DG02_DelaysDistributionGraphic.Cells(1).Width

        '    ' -----------------
        '    ' BITUMES CONSOMMÉ
        '    ' -----------------

        '    If (productionDay.Statistics.AllAsphalts.Count > 0) Then

        '        Dim _acStat As AsphaltStatistics = productionDay.Statistics.AllAsphalts(0)

        '        Dim totalAsphaltQuantity As Double

        '        bookMarks.ET01_FirstVirginAsphaltConcreteTankId.Text = _acStat.TANK
        '        bookMarks.ET01_FirstVirginAsphaltConcreteGrade.Text = _acStat.NAME
        '        bookMarks.ET01_FirstVirginAsphaltConcreteQuantity.Text = _acStat.TOTAL_MASS.ToString("N1")

        '        totalAsphaltQuantity += productionDay.Statistics.AllAsphalts(0).TOTAL_MASS

        '        For i = productionDay.Statistics.AllAsphalts.Count - 1 To 1 Step -1

        '            _acStat = productionDay.Statistics.AllAsphalts(i)

        '            bookMarks.ET01_FirstVirginAsphaltConcreteTankId.Select()
        '            WordApp.Selection.InsertRowsBelow(1)

        '            ' Number (cursor already in position
        '            WordApp.Selection.Text = _acStat.TANK

        '            ' Name
        '            moveSelectionToCellBelow(bookMarks.ET01_FirstVirginAsphaltConcreteGrade)
        '            WordApp.Selection.Text = _acStat.NAME

        '            ' Quantity
        '            moveSelectionToCellBelow(bookMarks.ET01_FirstVirginAsphaltConcreteQuantity)
        '            WordApp.Selection.Text = _acStat.TOTAL_MASS.ToString("N1")

        '            totalAsphaltQuantity += _acStat.TOTAL_MASS

        '            WordApp.Selection.SelectRow()
        '            WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone
        '            If (i Mod 2 = 1) Then
        '                WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
        '            End If

        '        Next

        '        bookMarks.ET01_TotalVirginAsphaltConcreteQuantity.Text = totalAsphaltQuantity.ToString("N1")

        '    Else

        '        ' TODO
        '        Throw New NotImplementedException

        '    End If

        '    RaiseEvent CurrentProgress(48) ' 48 % Progress

        '    ' -----------------------------------
        '    ' ECART PAR RAPPORT A LA VALEUR VISÉE
        '    ' -----------------------------------

        '    Dim totalTemperatureVariationPerQuantitySum As Double = 0
        '    Dim totalACPercentageVariationPerQuantitySum As Double = 0
        '    Dim overallTempVariation As Double = 0

        '    Dim analysedCyclesMixQuantitySumForTempDiff As Double = 0
        '    Dim analysedCyclesMixQuantitySumForACPercDiff As Double = 0
        '    Dim analysedCyclesMixQuantitySumForTempVar As Double = 0


        '    For Each _cycle As Cycle In productionDay.Statistics.ContinuousProduction.Cycles

        '        If (_cycle.PRODUCTION_SPEED > 0 AndAlso Not IsNothing(_cycle.PREVIOUS_CYCLE)) Then

        '            If (Not Double.IsNaN(_cycle.TEMPERATURE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then
        '                totalTemperatureVariationPerQuantitySum += (_cycle.TEMPERATURE_VARIATION + _cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION) / 2 * _cycle.MIX_MASS
        '                analysedCyclesMixQuantitySumForTempDiff += _cycle.MIX_MASS
        '            End If

        '            If (Not Double.IsNaN(_cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION)) Then
        '                totalACPercentageVariationPerQuantitySum += (_cycle.ASPHALT_PERCENTAGE_VARIATION + _cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION) / 2 * _cycle.MIX_MASS
        '                analysedCyclesMixQuantitySumForACPercDiff += _cycle.MIX_MASS
        '            End If

        '            If (Not Double.IsNaN(_cycle.TEMPERATURE) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then

        '                overallTempVariation += Math.Abs(_cycle.TEMPERATURE - _cycle.PREVIOUS_CYCLE.TEMPERATURE)
        '                analysedCyclesMixQuantitySumForTempVar += _cycle.MIX_MASS
        '            End If
        '        End If
        '    Next

        '    For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

        '        If (_cycle.PRODUCTION_SPEED > 0 AndAlso Not IsNothing(_cycle.PREVIOUS_CYCLE)) Then


        '            If (TypeOf _cycle Is CSVCycle) Then

        '                If (Not Double.IsNaN(_cycle.TEMPERATURE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then
        '                    totalTemperatureVariationPerQuantitySum += (_cycle.TEMPERATURE_VARIATION + _cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                    analysedCyclesMixQuantitySumForTempDiff += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                End If

        '                If (Not Double.IsNaN(_cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION)) Then
        '                    totalACPercentageVariationPerQuantitySum += (_cycle.ASPHALT_PERCENTAGE_VARIATION + _cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                    analysedCyclesMixQuantitySumForACPercDiff += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                End If

        '                If (Not Double.IsNaN(_cycle.TEMPERATURE) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then

        '                    overallTempVariation += Math.Abs(_cycle.TEMPERATURE - _cycle.PREVIOUS_CYCLE.TEMPERATURE)
        '                    analysedCyclesMixQuantitySumForTempVar += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                End If

        '            ElseIf (TypeOf _cycle Is MDBCycle) Then

        '                If (Not Double.IsNaN(_cycle.TEMPERATURE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then
        '                    totalTemperatureVariationPerQuantitySum += (_cycle.TEMPERATURE_VARIATION + _cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                    analysedCyclesMixQuantitySumForTempDiff += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                End If

        '                If (Not Double.IsNaN(_cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION)) Then
        '                    totalACPercentageVariationPerQuantitySum += (_cycle.ASPHALT_PERCENTAGE_VARIATION + _cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                    analysedCyclesMixQuantitySumForACPercDiff += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                End If

        '                If (Not Double.IsNaN(_cycle.TEMPERATURE) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then

        '                    overallTempVariation += Math.Abs(_cycle.TEMPERATURE - _cycle.PREVIOUS_CYCLE.TEMPERATURE)
        '                    analysedCyclesMixQuantitySumForTempVar += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
        '                End If
        '            End If


        '        End If
        '    Next

        '    bookMarks.ET02_AverageTemperatureDifference.Text = If(analysedCyclesMixQuantitySumForTempDiff = 0, "-", (totalTemperatureVariationPerQuantitySum / analysedCyclesMixQuantitySumForTempDiff).ToString("N1"))
        '    bookMarks.ET02_VirginAsphaltConcreteDifferencePerc.Text = If(analysedCyclesMixQuantitySumForACPercDiff = 0, "-", (totalACPercentageVariationPerQuantitySum / analysedCyclesMixQuantitySumForACPercDiff).ToString("N3"))
        '    bookMarks.ET03_TemperatureVariation.Text = If(analysedCyclesMixQuantitySumForTempVar = 0, "-", (overallTempVariation / analysedCyclesMixQuantitySumForTempVar).ToString("N1"))

        '    RaiseEvent CurrentProgress(60) ' 60 % Progress

        '    Dim nbCyclesWithAbberantTemperature As Integer = 0

        '    For Each _mixStat As MixStatistics In productionDay.Statistics.AllMixes

        '        nbCyclesWithAbberantTemperature += _mixStat.NB_CYCLES_WITH_ABBERANTE_TEMPERATURE

        '    Next

        '    bookMarks.ET04_TempratureAberrancePercentage.Text = (nbCyclesWithAbberantTemperature / productionDay.Statistics.NB_PRODUCTIVE_CYLES * 100).ToString("N1")

        '    Dim nbCyclesWithAberrantACPercentage As Integer = 0

        '    For Each _acStat As AsphaltStatistics In productionDay.Statistics.AllAsphalts
        '        nbCyclesWithAberrantACPercentage += _acStat.NB_CYCLE_WITH_ABERRANT_PERCENTAGE
        '    Next

        '    bookMarks.ET04_TempratureAberrancePercentage.Text = (nbCyclesWithAberrantACPercentage / productionDay.Statistics.NB_PRODUCTIVE_CYLES * 100).ToString("N1")

        '    ' -------------------------------
        '    ' Temperature difference graphic
        '    ' -------------------------------
        '    Dim mixTemperatureVariation As New MixTemperatureVariationGraphic(productionDay.Date_)

        '    For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

        '        If (TypeOf _cycle Is CSVCycle) Then

        '            mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

        '        ElseIf (TypeOf _cycle Is MDBCycle) Then

        '            mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)

        '        End If

        '    Next

        '    For Each _cycle As LOGCycle In productionDay.Statistics.ContinuousProduction.Cycles

        '        mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)

        '    Next

        '    mixTemperatureVariation.save()

        '    Dim g5 = bookMarks.EG01_TemperatureVariationGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC, False, True)

        '    g5.Width = bookMarks.CG01_ProductionQuantityGraphic.Cells(1).Width


        '    ' -----------------------------------
        '    ' CARBURANTS
        '    ' -----------------------------------

        '    With XmlSettings.Settings.instance.Usine.FuelsInfo

        '        bookMarks.FT01_FirstFuelName.Text = .FUEL_1_NAME
        '        bookMarks.FT01_SecondFuelName.Text = .FUEL_2_NAME

        '        bookMarks.FT01_FirstFuelQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMED_QUANTITY_1)
        '        bookMarks.FT01_FirstFuelConsumptionRate.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMPTION_RATE_1, "N1") & " " & .FUEL_1_UNIT & "/T"

        '        bookMarks.FT01_SecondFuelQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMED_QUANTITY_2)
        '        bookMarks.FT01_SecondFuelConsumptionRate.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMPTION_RATE_2, "N1") & " " & .FUEL_2_UNIT & "/T"

        '    End With


        '    ' -------
        '    ' REJETS
        '    ' -------
        '    bookMarks.GT01_RejectedAggregatesQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_AGGREGATES_QUANTITY)
        '    bookMarks.GT01_RejectedAggregatesPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_AGGREGATES_PERCENTAGE, "N1")

        '    bookMarks.GT01_RejectedFillerQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_FILLER_QUANTITY)
        '    bookMarks.GT01_RejectedFillerPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_FILLER_PERCENTAGE, "N1")

        '    bookMarks.GT01_RejectedRecycledQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_RECYCLED_QUANTITY)
        '    bookMarks.GT01_RejectedRecycledPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_RECYCLED_PERCENTAGE, "N1")

        '    RaiseEvent CurrentProgress(72) ' 72 % Progress

        '    ' Using the with statement is faster by a couple seconds with the way I fill the next tables
        '    With WordApp.Selection

        '        ' ---------------------
        '        ' DELAYS SUMMARY TABLE
        '        ' ---------------------

        '        If (productionDay.Statistics.EventsStatistics.JustifiableDelays.Count > 0) Then

        '            Dim delays = productionDay.Statistics.EventsStatistics.JustifiableDelays

        '            Dim _delay As Delay

        '            ' All delays except first, starting from last
        '            For i = delays.Count - 1 To 1 Step -1

        '                _delay = delays(i)

        '                ' Add new row
        '                bookMarks.HT01_FirstDelayStartTime.Select()
        '                .InsertRowsBelow(1)

        '                ' Start time (already selected after insertRowBelow()
        '                .Text = _delay.StartTime.ToString(Me.Formater.TimeFormat)


        '                .SelectRow()
        '                .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

        '                ' Alternate white rows
        '                If (i Mod 2 = 1) Then
        '                    .Shading.BackgroundPatternColor = WdColor.wdColorWhite
        '                End If

        '                ' End time

        '                moveSelectionToCellBelow(bookMarks.HT01_FirstDelayEndTime)
        '                .Text = _delay.EndTime.ToString(Me.Formater.TimeFormat)

        '                ' Duration
        '                moveSelectionToCellBelow(bookMarks.HT01_FirstDelayDuration)
        '                .Text = _delay.Duration.ToString(Me.Formater.DurationFormat)

        '                ' Select cell for delay code
        '                moveSelectionToCellBelow(bookMarks.HT01_FirstDelayCode)

        '                If (_delay.IsUnknown) Then

        '                    ' Delay Code (unknown)
        '                    .Text = Me.Formater.UnknownValueCharacter
        '                    .Shading.BackgroundPatternColor = WdColor.wdColorWhite

        '                    ' Delay justification
        '                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayComments)
        '                    .Text = _delay.Justification

        '                ElseIf (IsNothing(_delay.Code)) Then

        '                    ' Delay Code
        '                    .Text = "-"
        '                    .Shading.BackgroundPatternColor = WdColor.wdColorWhite

        '                    ' Delay code description
        '                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayDescription)
        '                    .Text = "-"

        '                    ' Delay justification
        '                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayComments)
        '                    .Text = "-"

        '                Else

        '                    ' Delay Code
        '                    .Text = _delay.Code.Code
        '                    .Shading.BackgroundPatternColor = RGB(_delay.Type.Color.R, _delay.Type.Color.G, _delay.Type.Color.B)

        '                    ' Delay code description
        '                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayDescription)
        '                    .Text = _delay.Code.Description

        '                    ' Delay justification
        '                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayComments)
        '                    .Text = _delay.Justification
        '                End If

        '            Next

        '            _delay = delays(0)

        '            ' First delay
        '            bookMarks.HT01_FirstDelayStartTime.Text = _delay.StartTime.ToString(Me.Formater.TimeFormat)
        '            bookMarks.HT01_FirstDelayEndTime.Text = _delay.EndTime.ToString(Me.Formater.TimeFormat)
        '            bookMarks.HT01_FirstDelayDuration.Text = _delay.Duration.ToString(Me.Formater.DurationFormat)

        '            If (_delay.IsUnknown) Then

        '                bookMarks.HT01_FirstDelayCode.Text = Me.Formater.UnknownValueCharacter
        '                bookMarks.HT01_FirstDelayCode.Shading.BackgroundPatternColor = WdColor.wdColorWhite
        '                bookMarks.HT01_FirstDelayComments.Text = _delay.Justification

        '            ElseIf (IsNothing(_delay.Code)) Then

        '                bookMarks.HT01_FirstDelayCode.Text = Me.Formater.InvalidValueCharacter
        '                bookMarks.HT01_FirstDelayCode.Shading.BackgroundPatternColor = WdColor.wdColorWhite
        '                bookMarks.HT01_FirstDelayDescription.Text = Me.Formater.InvalidValueCharacter
        '                bookMarks.HT01_FirstDelayComments.Text = Me.Formater.InvalidValueCharacter

        '            Else

        '                bookMarks.HT01_FirstDelayCode.Text = _delay.Code.Code
        '                bookMarks.HT01_FirstDelayCode.Shading.BackgroundPatternColor = RGB(_delay.Type.Color.R, _delay.Type.Color.G, _delay.Type.Color.B)

        '                bookMarks.HT01_FirstDelayDescription.Text = _delay.Code.Description

        '                bookMarks.HT01_FirstDelayComments.Text = _delay.Justification

        '            End If

        '        Else

        '            bookMarks.HT01_FirstDelayStartTime.Select()
        '            .Rows.Delete()

        '        End If

        '        bookMarks.HT01_MinimalDurationForJustification.Text = XmlSettings.Settings.instance.Usine.Events.Delays.JUSTIFIABLE_DURATION.TotalMinutes.ToString("N0")
        '        bookMarks.HT01_DelaysNumberUnderMinimalDuration.Text = productionDay.Statistics.EventsStatistics.Delays.Count - productionDay.Statistics.EventsStatistics.JustifiableDelays.Count
        '        bookMarks.HT01_DelaysUnderMinimalTimeDuration.Text = productionDay.Statistics.EventsStatistics.DT01_DelaysDuration.Subtract(productionDay.Statistics.EventsStatistics.JustifiableDelaysDuration).TotalMinutes.ToString("N0")

        '        RaiseEvent CurrentProgress(80) ' 80 % Progress


        '        ' ------------------------------------
        '        ' CONTINUOUS PRODUCTION SUMMARY TABLE
        '        ' ------------------------------------


        '        Dim nonNullFeeds As New List(Of FeedersStatistics)

        '        ' Find non null feeder
        '        For Each _feedStat As FeedersStatistics In productionDay.Statistics.MixesTotal.CONTINUOUS_FEEDERS_STATS

        '            If (_feedStat.TOTAL_MASS > 0) Then

        '                nonNullFeeds.Add(_feedStat)

        '            End If
        '        Next

        '        If (nonNullFeeds.Count > 0) Then

        '            Dim columnsWidth = bookMarks.JT01_FirstContinuousFeederDescription.Columns.Width / nonNullFeeds.Count

        '            ' First non null feeder
        '            bookMarks.JT01_FirstContinuousFeederDescription.Select()

        '            ' Feeder description
        '            If (IsNothing(nonNullFeeds.First().MATERIAL_NAME)) Then
        '                .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & "(T)"
        '            Else
        '                .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & nonNullFeeds.First().MATERIAL_NAME & " (T)"
        '            End If

        '            bookMarks.JT01_ContinuousFeederTotalQuantity.Select()
        '            .Text = nonNullFeeds.First().TOTAL_MASS.ToString("N1")

        '            With .Columns.Last
        '                .Width = columnsWidth
        '                .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
        '            End With

        '            ' Other non null feeders
        '            For feedStatIndex = nonNullFeeds.Count - 1 To 1 Step -1

        '                WordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range.Select()

        '                .InsertColumnsRight()

        '                ' Feeder description
        '                If (IsNothing(nonNullFeeds(feedStatIndex).MATERIAL_NAME)) Then
        '                    .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & "(T)"
        '                Else
        '                    .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & nonNullFeeds(feedStatIndex).MATERIAL_NAME & " (T)"
        '                End If

        '                With .Columns.Last
        '                    .Width = columnsWidth
        '                    .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
        '                End With

        '                ' Total quantity
        '                .MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
        '                .Text = nonNullFeeds(feedStatIndex).TOTAL_MASS.ToString("N1")

        '            Next

        '            bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

        '            ' Find first non null mixStat
        '            Dim _mixStat As MixStatistics
        '            Dim _nonNullFeed As FeedersStatistics
        '            Dim nbNonNullMix As Integer = 0
        '            Dim mixWithRecycledQuantitySum As Double = 0

        '            Dim totalAsphaltQuantity As Double = 0

        '            Dim firstMixStatIndex As Integer = -1

        '            productionDay.Statistics.ContinuousProduction.Mixes.Sort()

        '            For _firstMixStatIndex = 0 To productionDay.Statistics.ContinuousProduction.Mixes.Count - 1

        '                _mixStat = productionDay.Statistics.ContinuousProduction.Mixes(_firstMixStatIndex)

        '                If (_mixStat.TOTAL_MASS > 0) Then

        '                    firstMixStatIndex = _firstMixStatIndex

        '                    bookMarks.JT01_FirstContinuousMixNumber.Text = _mixStat.FORMULA_NAME

        '                    bookMarks.JT01_FirstContinuousMixName.Text = _mixStat.NAME

        '                    bookMarks.JT01_FirstContinuousVirginACGrade.Text = _mixStat.ASPHALT_STATS.NAME

        '                    bookMarks.JT01_FirstContinuousRAPPercentage.Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

        '                    bookMarks.JT01_FirstContinuousQuantity.Text = _mixStat.TOTAL_MASS.ToString("N1")

        '                    bookMarks.JT01_FirstContinuousVirginACQuantity.Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

        '                    totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

        '                    For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

        '                        _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

        '                        bookMarks.JT01_FirstContinuousFeederQuantity.Select()

        '                        For Each _currentMixFeed As FeedersStatistics In _mixStat.CONTINUOUS_FEEDERS_STATS

        '                            If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

        '                                .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

        '                                .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

        '                                Exit For ' Corresponding Feeder was found
        '                            End If

        '                        Next

        '                    Next

        '                    If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
        '                        mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
        '                    End If

        '                    nbNonNullMix += 1

        '                    Exit For ' First non null mixStat was found
        '                End If

        '            Next

        '            ' Used for alternate white rows
        '            Dim nbRows As Integer = 1

        '            ' Other non null mixstats
        '            For mixStatIndex = productionDay.Statistics.ContinuousProduction.Mixes.Count - 1 To firstMixStatIndex + 1 Step -1

        '                _mixStat = productionDay.Statistics.ContinuousProduction.Mixes(mixStatIndex)

        '                If (_mixStat.TOTAL_MASS > 0) Then

        '                    bookMarks.JT01_FirstContinuousMixNumber.Select()

        '                    .InsertRowsBelow()
        '                    nbRows += 1

        '                    ' Formula Name
        '                    .Text = _mixStat.FORMULA_NAME

        '                    ' Mix Name
        '                    moveSelectionToCellBelow(bookMarks.JT01_FirstContinuousMixName)
        '                    .Text = _mixStat.NAME

        '                    ' Asphalt Name
        '                    moveSelectionToCellBelow(bookMarks.JT01_FirstContinuousVirginACGrade)
        '                    .Text = _mixStat.ASPHALT_STATS.NAME

        '                    ' target recycled percentage
        '                    moveSelectionToCellBelow(bookMarks.JT01_FirstContinuousRAPPercentage)
        '                    .Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

        '                    ' mix quantity
        '                    moveSelectionToCellBelow(bookMarks.JT01_FirstContinuousQuantity)
        '                    .Text = _mixStat.TOTAL_MASS.ToString("N1")

        '                    ' Asphalt Quantity
        '                    moveSelectionToCellBelow(bookMarks.JT01_FirstContinuousVirginACQuantity)
        '                    .Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

        '                    totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

        '                    For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

        '                        _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

        '                        moveSelectionToCellBelow(bookMarks.JT01_FirstContinuousFeederQuantity)

        '                        For Each _currentMixFeed As FeedersStatistics In _mixStat.CONTINUOUS_FEEDERS_STATS

        '                            If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

        '                                .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

        '                                .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

        '                                Exit For ' Corresponding Feeder was found
        '                            End If
        '                        Next
        '                    Next

        '                    If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
        '                        mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
        '                    End If

        '                    nbNonNullMix += 1

        '                End If
        '            Next

        '            ' Alternate white rows and remove borders
        '            For i = 1 To nbRows - 1

        '                bookMarks.JT01_FirstContinuousMixNumber.Select()
        '                .MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
        '                .SelectRow()

        '                .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

        '                If (i Mod 2 = 1) Then
        '                    .Shading.BackgroundPatternColor = WdColor.wdColorWhite
        '                End If
        '            Next

        '            bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

        '            bookMarks.JT01_ContinuousTotalQuantity.Text = productionDay.Statistics.ContinuousProduction.Quantity.ToString("N1")
        '            bookMarks.JT01_ContinuousTotalVirginACQuantity.Text = totalAsphaltQuantity.ToString("N1")
        '            bookMarks.JT01_ContinuousWithRAPPercentage.Text = (mixWithRecycledQuantitySum / productionDay.Statistics.ContinuousProduction.Quantity * 100).ToString("N0")

        '            bookMarks.JT01_ContinuousTotalCellsToMerge.Cells.Merge()

        '        Else

        '            bookMarks.JA01_ContinuousProductionSummarySection.Delete()

        '        End If

        '        RaiseEvent CurrentProgress(85) ' 85 % Progress

        '        ' ------------------------------------
        '        ' DISCONTINUOUS PRODUCTION SUMMARY TABLE
        '        ' ------------------------------------

        '        nonNullFeeds.Clear()

        '        ' Find non null feeder
        '        For Each _feedStat As FeedersStatistics In productionDay.Statistics.MixesTotal.DISCONTINUOUS_FEEDERS_STATS

        '            If (_feedStat.TOTAL_MASS > 0) Then

        '                nonNullFeeds.Add(_feedStat)

        '            End If
        '        Next

        '        If (nonNullFeeds.Count > 0) Then

        '            Dim columnsWidth = bookMarks.JT02_FirstDiscontinuousFeederDescription.Columns.Width / nonNullFeeds.Count

        '            ' First non null feeder
        '            bookMarks.JT02_FirstDiscontinuousFeederDescription.Select()

        '            ' Feeder= description
        '            If (IsNothing(nonNullFeeds.First().MATERIAL_NAME)) Then
        '                .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & "(T)"
        '            Else
        '                .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & nonNullFeeds.First().MATERIAL_NAME & " (T)"
        '            End If

        '            bookMarks.JT02_FirstDiscontinuousFeederQuantity.Select()
        '            .Text = nonNullFeeds.First().TOTAL_MASS.ToString("N1")

        '            With .Columns.Last
        '                .Width = columnsWidth
        '                .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
        '            End With

        '            ' Other non null feeders
        '            For feedStatIndex = nonNullFeeds.Count - 1 To 1 Step -1

        '                WordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range.Select()

        '                .InsertColumnsRight()

        '                ' Feeder description
        '                If (IsNothing(nonNullFeeds(feedStatIndex).MATERIAL_NAME)) Then
        '                    .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & "(T)"
        '                Else
        '                    .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & nonNullFeeds(feedStatIndex).MATERIAL_NAME & " (T)"
        '                End If

        '                With .Columns.Last
        '                    .Width = columnsWidth
        '                    .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
        '                End With

        '                ' Total quantity
        '                .MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
        '                .Text = nonNullFeeds(feedStatIndex).TOTAL_MASS.ToString("N1")

        '            Next

        '            bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

        '            ' Find first non null mixStat
        '            Dim _mixStat As MixStatistics
        '            Dim _nonNullFeed As FeedersStatistics
        '            Dim nbNonNullMix As Integer = 0
        '            Dim mixWithRecycledQuantitySum As Double = 0

        '            Dim totalAsphaltQuantity As Double = 0

        '            Dim firstMixStatIndex As Integer = -1

        '            productionDay.Statistics.DiscontinuousProduction.Mixes.Sort()

        '            For _firstMixStatIndex = 0 To productionDay.Statistics.DiscontinuousProduction.Mixes.Count - 1

        '                _mixStat = productionDay.Statistics.DiscontinuousProduction.Mixes(_firstMixStatIndex)

        '                If (_mixStat.TOTAL_MASS > 0) Then

        '                    firstMixStatIndex = _firstMixStatIndex

        '                    bookMarks.JT02_FirstDiscontinuousMixNumber.Text = _mixStat.FORMULA_NAME

        '                    bookMarks.JT02_FirstDiscontinuousMixName.Text = _mixStat.NAME

        '                    bookMarks.JT02_FirstDiscontinuousVirginACGrade.Text = _mixStat.ASPHALT_STATS.NAME

        '                    bookMarks.JT02_FirstDiscontinuousRecycledQuantity.Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

        '                    bookMarks.JT02_FirstDiscontinuousQuantity.Text = _mixStat.TOTAL_MASS.ToString("N1")

        '                    bookMarks.JT02_FirstDiscontinuousQuantity.Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

        '                    totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

        '                    For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

        '                        _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

        '                        bookMarks.JT02_FirstDiscontinuousFeederQuantity.Select()

        '                        For Each _currentMixFeed As FeedersStatistics In _mixStat.DISCONTINUOUS_FEEDERS_STATS

        '                            If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

        '                                .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

        '                                .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

        '                                Exit For ' Corresponding Feeder was found
        '                            End If

        '                        Next

        '                    Next

        '                    If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
        '                        mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
        '                    End If

        '                    nbNonNullMix += 1

        '                    Exit For ' First non null mixStat was found
        '                End If

        '            Next

        '            ' Used for alternate white rows
        '            Dim nbRows As Integer = 1

        '            ' Other non null mixstats
        '            For mixStatIndex = productionDay.Statistics.DiscontinuousProduction.Mixes.Count - 1 To firstMixStatIndex + 1 Step -1

        '                _mixStat = productionDay.Statistics.DiscontinuousProduction.Mixes(mixStatIndex)

        '                If (_mixStat.TOTAL_MASS > 0) Then

        '                    bookMarks.JT02_FirstDiscontinuousMixNumber.Select()

        '                    .InsertRowsBelow()
        '                    nbRows += 1

        '                    ' Formula Name
        '                    .Text = _mixStat.FORMULA_NAME

        '                    ' Mix Name
        '                    moveSelectionToCellBelow(bookMarks.JT02_FirstDiscontinuousMixName)
        '                    .Text = _mixStat.NAME

        '                    ' Asphalt Name
        '                    moveSelectionToCellBelow(bookMarks.JT02_FirstDiscontinuousVirginACGrade)
        '                    .Text = _mixStat.ASPHALT_STATS.NAME

        '                    ' target recycled percentage
        '                    moveSelectionToCellBelow(bookMarks.JT02_FirstDiscontinuousRecycledQuantity)
        '                    .Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

        '                    ' mix quantity
        '                    moveSelectionToCellBelow(bookMarks.JT02_FirstDiscontinuousQuantity)
        '                    .Text = _mixStat.TOTAL_MASS.ToString("N1")

        '                    ' Asphalt Quantity
        '                    moveSelectionToCellBelow(bookMarks.JT02_FirstDiscontinuousQuantity)
        '                    .Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

        '                    totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

        '                    For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

        '                        _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

        '                        moveSelectionToCellBelow(bookMarks.JT02_FirstDiscontinuousFeederQuantity)

        '                        For Each _currentMixFeed As FeedersStatistics In _mixStat.DISCONTINUOUS_FEEDERS_STATS

        '                            If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

        '                                .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

        '                                .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

        '                                Exit For ' Corresponding Feeder was found
        '                            End If
        '                        Next
        '                    Next

        '                    If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
        '                        mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
        '                    End If

        '                    nbNonNullMix += 1

        '                End If
        '            Next

        '            ' Alternate white rows and remove borders
        '            For i = 1 To nbRows - 1

        '                bookMarks.JT02_FirstDiscontinuousMixNumber.Select()
        '                .MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
        '                .SelectRow()

        '                .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

        '                If (i Mod 2 = 1) Then
        '                    .Shading.BackgroundPatternColor = WdColor.wdColorWhite
        '                End If
        '            Next

        '            bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

        '            bookMarks.JT02_FirstDiscontinuousQuantity.Text = productionDay.Statistics.DiscontinuousProduction.Quantity.ToString("N1")
        '            bookMarks.JT02_DiscontinuousTotalVirginACQuantity.Text = totalAsphaltQuantity.ToString("N1")
        '            bookMarks.JT02_DiscontinuousWithRAPPercentage.Text = (mixWithRecycledQuantitySum / productionDay.Statistics.DiscontinuousProduction.Quantity * 100).ToString("N0")

        '            bookMarks.JT02_DiscontinuousTotalCellsToMerge.Cells.Merge()


        '        Else

        '            bookMarks.JA02_DiscontinuousProductionSummarySect.Delete()

        '        End If
        '    End With

        '    RaiseEvent CurrentProgress(90) ' 90 % Progress

        '    ' ---------
        '    ' KA01_Comments
        '    ' ---------

        '    bookMarks.KA01_Comments.Text = productionDay.KA01_Comments

        '    ' ----------
        '    ' Signature
        '    ' ----------

        '    If (Not productionDay.ManualData.FACTORY_OPERATOR = FactoryOperator.DEFAULT_OPERATOR) Then

        '        bookMarks.LA01_OperatorName.Text = productionDay.ManualData.FACTORY_OPERATOR.ToString()
        '    End If

        '    bookMarks.BA01_FooterDate.Text = Date.Today.ToString(Me.Formater.FullDateFormat)
        '    bookMarks.LA02_SignatureDate.Text = Date.Now.ToString(Me.Formater.DateTimeFormat)

        '    ' -----
        '    ' SAVE N QUIT
        '    ' -----
        '    Dim savePath = Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & "Rapport Journalier Sommaire - " & XmlSettings.Settings.instance.Usine.PLANT_NAME & productionDay.Date_.ToString(" - (yyyy-MM-dd)")

        '    Me.WordDoc.SaveAs2(savePath)
        '    Dim writableReport As New SummaryDailyReport(productionDay.Date_, savePath & ".docx", False)
        '    productionDay.ReportFilesInfo.addReport(writableReport)

        '    Me.WordDoc.SaveAs2(savePath, WdSaveFormat.wdFormatPDF)
        '    Dim readOnlyReport As New SummaryDailyReport(productionDay.Date_, savePath & ".pdf", True)
        '    productionDay.ReportFilesInfo.addReport(readOnlyReport)

        '    ProgramController.ReportsPersistence.addDailyReports(productionDay.Date_, writableReport.getFileInfo.FullName, readOnlyReport.getFileInfo.FullName, Nothing)

        '    Me.killDocumentObjects()

        '    If (XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_READ_ONLY) Then
        '        readOnlyReport.open()
        '    End If

        '    If (XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_WRITABLE) Then
        '        writableReport.open()
        '    End If

        '    RaiseEvent CurrentProgress(100) ' 100 % Progress


        'Catch ex As Threading.ThreadAbortException

        '    Me.disposeOfRessources()

        '    RaiseEvent ProcessInterrupted(Me, ex)
        'End Try

        'RaiseEvent ProcessComplete(Me)

    End Sub

    ''' <summary>
    ''' Moves the selection to the table cell beneith.
    ''' </summary>
    ''' <param name="bookMark">The bookmark in the cell above</param>
    ''' <remarks></remarks>
    Private Sub moveSelectionToCellBelow(bookMark As Range)

        bookMark.Cells(1).Select()

        WordApp.Selection.MoveDown()
    End Sub

    ''' <summary>
    ''' Adds a 'er' in superscript next to the 1 so the date looks like 1er octobre 2012 and not 1 octobre 2012
    ''' </summary>
    ''' <param name="_date">The date</param>
    ''' <param name="bookMark">The bookmark where the date is written</param>
    ''' <remarks>
    ''' Will be used a lot more when english version comes out. #language
    ''' </remarks>
    Private Sub ajustDateString(_date As Date, bookMark As Range)

        If (_date.Day = 1) Then

            bookMark.Select()
            WordApp.Selection.MoveLeft(WdUnits.wdCharacter, 1)
            WordApp.Selection.MoveRight(WdUnits.wdCharacter, 1)
            WordApp.Selection.Font.Superscript = True
            WordApp.Selection.TypeText("er") ' #language
        End If
    End Sub

    Public Event CurrentProgress(progressPercentage As Object) Implements Eventing.TrackableProcess.CurrentProgress

    Public Event ProcessComplete(sender As Object) Implements Eventing.TrackableProcess.ProcessComplete

    Public Event ProcessInterrupted(sender As Object, exception As Exception) Implements Eventing.TrackableProcess.ProcessInterrupted

    Public Event ProcessStarting(sender As Object) Implements Eventing.TrackableProcess.ProcessStarting
End Class
