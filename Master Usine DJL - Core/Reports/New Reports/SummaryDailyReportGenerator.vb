Imports Microsoft.Office.Interop.Word

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
        MyBase.New(ReportType.SummaryDailyReport, _
                   New SummaryDailyReportFormater)

        initializeWordApplication()

        Me.bookMarks = New Constants.Reports.BookMarks.SummaryDailyReportBookMarks

    End Sub


    Public Sub generateReport(productionDay As ProductionDay)

        RaiseEvent ProcessStarting(Me)

        Try

            Me.WordDoc = WordApp.Documents.Open(Constants.Paths.SUMMARY_DAILY_REPORT_TEMPLATE, False, True)

            Me.bookMarks.initialize(Me.WordDoc)

            ' Factory Info
            bookMarks.FactoryName.Text = XmlSettings.Settings.instance.Usine.PLANT_NAME
            bookMarks.FactoryId.Text = XmlSettings.Settings.instance.Usine.PLANT_ID

            ' Date
            bookMarks.ProductionDayDate.Text = productionDay.Date_.ToString(Me.Formater.ShortDateFormat)

            ' Add er when first of month
            ajustDateString(productionDay.Date_, bookMarks.ProductionDayDate)

            ' --------
            ' TABLE 1
            ' --------

            ' Operation
            bookMarks.OperationStartTime.Text = productionDay.ManualData.OPERATION_START_TIME.ToString(Me.Formater.TimeFormat)
            bookMarks.OperationEndTime.Text = productionDay.ManualData.OPERATION_END_TIME.ToString(Me.Formater.TimeFormat)
            bookMarks.OperationDuration.Text = productionDay.ManualData.OPERATION_END_TIME.Subtract(productionDay.ManualData.OPERATION_START_TIME).ToString(Me.Formater.DurationFormat)

            ' Production
            bookMarks.ProductionStartTime.Text = productionDay.ManualData.PRODUCTION_START_TIME.ToString(Me.Formater.TimeFormat)
            bookMarks.ProductionEndTime.Text = productionDay.ManualData.PRODUCTION_END_TIME.ToString(Me.Formater.TimeFormat)
            bookMarks.ProductionDuration.Text = productionDay.ManualData.PRODUCTION_END_TIME.Subtract(productionDay.ManualData.PRODUCTION_START_TIME).ToString(Me.Formater.DurationFormat)

            ' Loading / weight station
            ' #refactor (if was set)
            bookMarks.FirstLoadingTime.Text = productionDay.ManualData.FIRST_LOADING_TIME.ToString(Me.Formater.TimeFormat)
            bookMarks.LastLoadingTime.Text = productionDay.ManualData.LAST_LOADING_TIME.ToString(Me.Formater.TimeFormat)
            bookMarks.LoadingDuration.Text = productionDay.ManualData.LAST_LOADING_TIME.Subtract(productionDay.ManualData.FIRST_LOADING_TIME).ToString(Me.Formater.DurationFormat)


            bookMarks.PausesDuration.Text = productionDay.Statistics.EventsStatistics.PausesDuration.ToString(Me.Formater.DurationFormat)

            bookMarks.MaintenanceDuration.Text = productionDay.Statistics.EventsStatistics.MaintenanceDuration.ToString(Me.Formater.DurationFormat)

            RaiseEvent CurrentProgress(12) ' 12 % Progress

            ' --------
            ' TABLE 2 
            ' --------
            Dim firstMix As MixStatistics = productionDay.Statistics.AllMixes(0)

            bookMarks.FirstMixName.Text = firstMix.NAME

            ' Asphalt temp span  '#refactor - name of bookmark
            bookMarks.FirstMixAsphaltTemperatureSpan.Text = firstMix.ASPHALT_STATS.NAME

            bookMarks.FirstMixQuantity.Text = firstMix.TOTAL_MASS.ToString("N0")
            bookMarks.FirstMixProductionRate.Text = firstMix.AVERAGE_PRODUCTION_SPEED.ToString("N0")

            bookMarks.FirstMixProductionType.Text = Me.Formater.getProductionTypeString(firstMix.PRODUCTION_TYPE)

            If (productionDay.Statistics.AllMixes.Count > 1) Then

                Dim secondMix As MixStatistics = productionDay.Statistics.AllMixes(1)

                bookMarks.SecondMixName.Text = secondMix.NAME

                ' Asphalt temp span
                bookMarks.SecondMixAsphaltTemperatureSpan.Text = secondMix.ASPHALT_STATS.NAME

                bookMarks.SecondMixQuantity.Text = secondMix.TOTAL_MASS.ToString("N0")
                bookMarks.SecondMixProductionRate.Text = secondMix.AVERAGE_PRODUCTION_SPEED.ToString("N0")

                bookMarks.SecondMixProductionType.Text = Me.Formater.getProductionTypeString(secondMix.PRODUCTION_TYPE)
            Else
                bookMarks.SecondMixName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixAsphaltTemperatureSpan.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixProductionType.Text = Me.Formater.InvalidValueCharacter
            End If

            If (productionDay.Statistics.AllMixes.Count > 2) Then

                Dim thirdMix As MixStatistics = productionDay.Statistics.AllMixes(2)

                bookMarks.ThirdMixName.Text = thirdMix.NAME

                ' Asphalt temp span
                bookMarks.ThirdMixAsphaltTemperatureSpan.Text = thirdMix.ASPHALT_STATS.NAME

                bookMarks.ThirdMixQuantity.Text = thirdMix.TOTAL_MASS.ToString("N0")
                bookMarks.ThirdMixProductionRate.Text = thirdMix.AVERAGE_PRODUCTION_SPEED.ToString("N0")

                bookMarks.ThirdMixProductionType.Text = Me.Formater.getProductionTypeString(thirdMix.PRODUCTION_TYPE)

            Else

                bookMarks.ThirdMixName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixAsphaltTemperatureSpan.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixProductionType.Text = Me.Formater.InvalidValueCharacter
            End If


            bookMarks.NumberOfOtherMixes.Text = If(productionDay.Statistics.AllMixes.Count <= 3, "0", productionDay.Statistics.AllMixes.Count - 3)
            bookMarks.OtherMixesQuantity.Text = productionDay.Statistics.OtherMixes.TOTAL_MASS.ToString("N0")
            bookMarks.OtherMixesProductionRate.Text = productionDay.Statistics.OtherMixes.AVERAGE_PRODUCTION_SPEED.ToString("N0")

            ' Todo production type other mixes

            bookMarks.TotalQuantityProduced.Text = productionDay.Statistics.MixesTotal.TOTAL_MASS.ToString("N0")
            bookMarks.TotalProductionRate.Text = productionDay.Statistics.MixesTotal.AVERAGE_PRODUCTION_SPEED.ToString("N0")

            ' Silo at start
            bookMarks.SiloQuantityAtStart.Text = Me.Formater.getManualDataString(productionDay.ManualData.SILO_QUANTITY_AT_START)

            RaiseEvent CurrentProgress(24) ' 24 % Progress

            ' Silo at end
            bookMarks.SiloQuantityAtEnd.Text = Me.Formater.getManualDataString(productionDay.ManualData.SILO_QUANTITY_AT_END)

            ' Salable qty
            bookMarks.SalableQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.SALABLE_QUANTITY, "N0")

            ' Rejected mix

            bookMarks.RejectedMixQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_MIX_QUANTITY, "N0")
            bookMarks.RejectedMixPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_MIX_PERCENTAGE, "N1")

            ' Payable qty
            bookMarks.TotalPayableQuantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.PAYABLE_QUANTITY, "N0")

            ' Sold (weighted) qty

            bookMarks.TotalQuantitySold.Text = Me.Formater.getManualDataString(productionDay.ManualData.WEIGHTED_QUANTITY, "N0")
            bookMarks.TotalQuantitySoldDifferencePercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.WEIGHTED_QUANTITY_DIFFERENCE_PERCENTAGE, "N1")

            ' --------------------
            ' Graphic 1 et 2 #refactor generate before graphs and store them
            ' --------------------
            XYScatterGraphic.pointFormatList_asphalt = New PointFormatList
            XYScatterGraphic.pointFormatList_mix = New PointFormatList

            Dim isHybrid As Boolean = XmlSettings.Settings.instance.Usine.DataFiles.CSV.ACTIVE AndAlso XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE

            Dim accumulatedMass As New AccumulatedMassGraphic(productionDay.Date_, isHybrid)
            Dim productionSpeed As New ProductionSpeedGraphic(productionDay.Date_)

            For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

                If (TypeOf _cycle Is CSVCycle) Then

                    accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
                    productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

                ElseIf (TypeOf _cycle Is MDBCycle) Then

                    accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
                    productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
                End If

            Next

            accumulatedMass.toggleMarkerColor()
            productionSpeed.toggleMarkerColor()

            For Each _cycle In productionDay.Statistics.ContinuousProduction.Cycles

                accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
                productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
            Next

            accumulatedMass.save()
            productionSpeed.save()

            Dim g1 = bookMarks.ProductionQuantityGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC, False, True)
            Dim g2 = bookMarks.ProductionRateGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_SPEED_GRAPHIC, False, True)

            g1.Width = bookMarks.ProductionQuantityGraphic.Cells(1).Width
            g2.Width = bookMarks.ProductionQuantityGraphic.Cells(1).Width


            ' --------------------
            ' PRODUCTION ET DELAIS
            ' --------------------

            bookMarks.ContinuousProductionDuration.Text = productionDay.Statistics.ContinuousProduction.Duration.ToString(Me.Formater.DurationFormat)
            bookMarks.DiscontinuousProductionDuration.Text = productionDay.Statistics.DiscontinuousProduction.Duration.ToString(Me.Formater.DurationFormat)


            bookMarks.DelaysDuration.Text = productionDay.Statistics.EventsStatistics.DelaysDuration.ToString(Me.Formater.DurationFormat)

            bookMarks.ContinuousProductionPercentage.Text = (productionDay.Statistics.ContinuousProduction.Duration.TotalSeconds / productionDay.ManualData.PRODUCTION_END_TIME.Subtract(productionDay.ManualData.PRODUCTION_START_TIME).TotalSeconds * 100).ToString("N0")
            bookMarks.DiscontinuousProductionPercentage.Text = (productionDay.Statistics.DiscontinuousProduction.Duration.TotalSeconds / productionDay.ManualData.PRODUCTION_END_TIME.Subtract(productionDay.ManualData.PRODUCTION_START_TIME).TotalSeconds * 100).ToString("N0")
            bookMarks.DelaysPercentage.Text = (productionDay.Statistics.EventsStatistics.DelaysDuration.TotalSeconds / productionDay.ManualData.OPERATION_END_TIME.Subtract(productionDay.ManualData.OPERATION_START_TIME).TotalSeconds * 100).ToString("N0")

            bookMarks.NbSwitchContinuous.Text = productionDay.Statistics.ContinuousProduction.NbMixSwitch
            bookMarks.NbMixSwitchDiscontinuous.Text = productionDay.Statistics.DiscontinuousProduction.NbMixSwitch
            bookMarks.NumberOfDelays.Text = productionDay.Statistics.EventsStatistics.Delays.Count

            bookMarks.ContinuousProductionQuantity.Text = productionDay.Statistics.ContinuousProduction.Quantity.ToString("N0")
            bookMarks.DiscontinuousProductionQuantity.Text = productionDay.Statistics.DiscontinuousProduction.Quantity.ToString("N0")

            Dim avgProdRate As Double = 0
            Dim nbCyclesAnalysedForProdRate As Integer = 0

            For Each _cycle As Cycle In productionDay.Statistics.ContinuousProduction.Cycles
                If (_cycle.PRODUCTION_SPEED > 0) Then
                    avgProdRate += XmlSettings.Settings.instance.Usine.DataFiles.LOG.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.PRODUCTION_SPEED, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                    nbCyclesAnalysedForProdRate += 1
                End If
            Next

            avgProdRate = If(nbCyclesAnalysedForProdRate = 0, 0, avgProdRate / nbCyclesAnalysedForProdRate)

            bookMarks.ContinuousProductionRate.Text = avgProdRate.ToString("N0")

            RaiseEvent CurrentProgress(36) ' 36 % Progress

            avgProdRate = 0
            nbCyclesAnalysedForProdRate = 0

            For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles
                If (_cycle.PRODUCTION_SPEED > 0) Then

                    If (TypeOf _cycle Is CSVCycle) Then

                        avgProdRate += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.PRODUCTION_SPEED, XmlSettings.Settings.instance.Reports.MASS_UNIT)

                    ElseIf (TypeOf _cycle Is MDBCycle) Then
                        avgProdRate += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.PRODUCTION_SPEED, XmlSettings.Settings.instance.Reports.MASS_UNIT)

                    End If

                    nbCyclesAnalysedForProdRate += 1
                End If
            Next

            avgProdRate = If(nbCyclesAnalysedForProdRate = 0, 0, avgProdRate / nbCyclesAnalysedForProdRate)

            bookMarks.DiscontinuousProductionRate.Text = avgProdRate.ToString("N0")

            ' -------------------
            ' TEMPS DE PRODUCTION
            ' -------------------

            ' Temps total d’opération = Heure fin d’opération – Heure début d’opération
            Dim grossOperationDuration As TimeSpan = productionDay.ManualData.OPERATION_END_TIME.Subtract(productionDay.ManualData.OPERATION_START_TIME)
            bookMarks.GrossOperationDuration.Text = grossOperationDuration.ToString(Me.Formater.DurationFormat)

            ' Temps net d’opération = Temps total d’opération – temps Pause(s)
            Dim netOperationDuration = grossOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.PausesDuration)
            bookMarks.NetOperationDuration.Text = netOperationDuration.ToString(Me.Formater.DurationFormat)

            ' Production nette = Temps net d’opération – temps Entretien(s)
            Dim netProductionDuration As TimeSpan = netOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.MaintenanceDuration)
            bookMarks.NetProductionDuration.Text = netProductionDuration.ToString(Me.Formater.DurationFormat)

            'Production efficace = Temps total d’opération – (temps Pause(s) + temps Entretien(s) + temps Délais)
            Dim effectiveProductionDuration As TimeSpan = grossOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.PausesDuration).Subtract(productionDay.Statistics.EventsStatistics.MaintenanceDuration).Subtract(productionDay.Statistics.EventsStatistics.DelaysDuration)
            bookMarks.EffectiveProductionDuration.Text = effectiveProductionDuration.ToString(Me.Formater.DurationFormat)

            ' Production efficace interne = Temps total d’opération – (temps Pause(s) + temps Entretien(s) + temps Délais interne (code 1 à 46))
            Dim effectiveInternProductionDuration As TimeSpan = grossOperationDuration.Subtract(productionDay.Statistics.EventsStatistics.PausesDuration).Subtract(productionDay.Statistics.EventsStatistics.MaintenanceDuration).Subtract(productionDay.Statistics.EventsStatistics.InternDelaysDuration)
            bookMarks.EffectiveInternProductionDuration.Text = effectiveInternProductionDuration.ToString(Me.Formater.DurationFormat)

            bookMarks.AllDelaysDuration.Text = productionDay.Statistics.EventsStatistics.DelaysDuration.ToString(Me.Formater.DurationFormat)

            ' -------
            ' DELAIS
            ' -------

            bookMarks.NbOfBreakages.Text = productionDay.Statistics.EventsStatistics.NbOfBreakages

            ' Disponibilité (%) = Production efficace interne / Production nette * 100
            bookMarks.DisponibilityPercentage.Text = (effectiveInternProductionDuration.TotalSeconds / netProductionDuration.TotalSeconds * 100).ToString("N0")
            ' Utilisation (%) = Production efficace / Temps total d’opération
            bookMarks.UtilisationPercentage.Text = (effectiveProductionDuration.TotalSeconds / grossOperationDuration.TotalSeconds * 100).ToString("N0")

            If (productionDay.Statistics.EventsStatistics.NbOfBreakages > 0) Then

                bookMarks.TimeBetweenBreakDowns.Text = TimeSpan.FromSeconds(effectiveProductionDuration.TotalSeconds / productionDay.Statistics.EventsStatistics.NbOfBreakages).ToString(Me.Formater.DurationFormat)
                bookMarks.ReparationsDuration.Text = TimeSpan.FromSeconds(productionDay.Statistics.EventsStatistics.InternBreakagesDuration.TotalSeconds / productionDay.Statistics.EventsStatistics.NbOfBreakages).ToString(Me.Formater.DurationFormat)

            Else

                bookMarks.TimeBetweenBreakDowns.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ReparationsDuration.Text = Me.Formater.InvalidValueCharacter

            End If

            ' ----------------------
            ' DISTRIBUTION GRAPHICS
            ' ----------------------

            Dim pdg = New ProductionDistributionGraphic(productionDay.Statistics.ProductionEndTime.Subtract(productionDay.Statistics.ProductionStartTime), _
                                                        productionDay.Statistics.EventsStatistics.PausesDuration, _
                                                        productionDay.Statistics.EventsStatistics.MaintenanceDuration, _
                                                        productionDay.Statistics.EventsStatistics.DelaysDuration)

            pdg.save()

            Dim ddg = New DelaysDistributionGraphic(productionDay.Statistics.EventsStatistics.InternWithBreakageDuration, _
                                                    productionDay.Statistics.EventsStatistics.InternWithoutBreakageDuration, _
                                                    productionDay.Statistics.EventsStatistics.ExternDuration, _
                                                    productionDay.Statistics.EventsStatistics.OtherDelaysDuration)

            ddg.save()

            Dim g3 = bookMarks.ProductionDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_DISTRIBUTION_GRAPHIC, False, True)

            g3.Width = bookMarks.ProductionDistributionGraphic.Cells(1).Width

            Dim g4 = bookMarks.DelaysDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.DELAYS_DISTRIBUTION_GRAPHIC, False, True)

            g4.Width = bookMarks.DelaysDistributionGraphic.Cells(1).Width

            ' -----------------
            ' BITUMES CONSOMMÉ
            ' -----------------

            If (productionDay.Statistics.AllAsphalts.Count > 0) Then

                Dim _acStat As AsphaltStatistics = productionDay.Statistics.AllAsphalts(0)

                Dim totalAsphaltQuantity As Double

                bookMarks.FirstAsphaltNumber.Text = _acStat.TANK
                bookMarks.FirstAsphaltName.Text = _acStat.NAME
                bookMarks.FirstAsphaltQuantity.Text = _acStat.TOTAL_MASS.ToString("N1")

                totalAsphaltQuantity += productionDay.Statistics.AllAsphalts(0).TOTAL_MASS

                For i = productionDay.Statistics.AllAsphalts.Count - 1 To 1 Step -1

                    _acStat = productionDay.Statistics.AllAsphalts(i)

                    bookMarks.FirstAsphaltNumber.Select()
                    WordApp.Selection.InsertRowsBelow(1)

                    ' Number (cursor already in position
                    WordApp.Selection.Text = _acStat.TANK

                    ' Name
                    moveSelectionToCellBelow(bookMarks.FirstAsphaltName)
                    WordApp.Selection.Text = _acStat.NAME

                    ' Quantity
                    moveSelectionToCellBelow(bookMarks.FirstAsphaltQuantity)
                    WordApp.Selection.Text = _acStat.TOTAL_MASS.ToString("N1")

                    totalAsphaltQuantity += _acStat.TOTAL_MASS

                    WordApp.Selection.SelectRow()
                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone
                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If

                Next

                bookMarks.TotalAsphaltQuantity.Text = totalAsphaltQuantity.ToString("N1")

            Else

                ' TODO
                Throw New NotImplementedException

            End If

            RaiseEvent CurrentProgress(48) ' 48 % Progress

            ' -----------------------------------
            ' ECART PAR RAPPORT A LA VALEUR VISÉE
            ' -----------------------------------

            Dim totalTemperatureVariationPerQuantitySum As Double = 0
            Dim totalACPercentageVariationPerQuantitySum As Double = 0
            Dim overallTempVariation As Double = 0

            Dim analysedCyclesMixQuantitySumForTempDiff As Double = 0
            Dim analysedCyclesMixQuantitySumForACPercDiff As Double = 0
            Dim analysedCyclesMixQuantitySumForTempVar As Double = 0


            For Each _cycle As Cycle In productionDay.Statistics.ContinuousProduction.Cycles

                If (_cycle.PRODUCTION_SPEED > 0 AndAlso Not IsNothing(_cycle.PREVIOUS_CYCLE)) Then

                    If (Not Double.IsNaN(_cycle.TEMPERATURE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then
                        totalTemperatureVariationPerQuantitySum += (_cycle.TEMPERATURE_VARIATION + _cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION) / 2 * _cycle.MIX_MASS
                        analysedCyclesMixQuantitySumForTempDiff += _cycle.MIX_MASS
                    End If

                    If (Not Double.IsNaN(_cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION)) Then
                        totalACPercentageVariationPerQuantitySum += (_cycle.ASPHALT_PERCENTAGE_VARIATION + _cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION) / 2 * _cycle.MIX_MASS
                        analysedCyclesMixQuantitySumForACPercDiff += _cycle.MIX_MASS
                    End If

                    If (Not Double.IsNaN(_cycle.TEMPERATURE) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then

                        overallTempVariation += Math.Abs(_cycle.TEMPERATURE - _cycle.PREVIOUS_CYCLE.TEMPERATURE)
                        analysedCyclesMixQuantitySumForTempVar += _cycle.MIX_MASS
                    End If
                End If
            Next

            For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

                If (_cycle.PRODUCTION_SPEED > 0 AndAlso Not IsNothing(_cycle.PREVIOUS_CYCLE)) Then


                    If (TypeOf _cycle Is CSVCycle) Then

                        If (Not Double.IsNaN(_cycle.TEMPERATURE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then
                            totalTemperatureVariationPerQuantitySum += (_cycle.TEMPERATURE_VARIATION + _cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                            analysedCyclesMixQuantitySumForTempDiff += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                        End If

                        If (Not Double.IsNaN(_cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION)) Then
                            totalACPercentageVariationPerQuantitySum += (_cycle.ASPHALT_PERCENTAGE_VARIATION + _cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                            analysedCyclesMixQuantitySumForACPercDiff += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                        End If

                        If (Not Double.IsNaN(_cycle.TEMPERATURE) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then

                            overallTempVariation += Math.Abs(_cycle.TEMPERATURE - _cycle.PREVIOUS_CYCLE.TEMPERATURE)
                            analysedCyclesMixQuantitySumForTempVar += XmlSettings.Settings.instance.Usine.DataFiles.CSV.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                        End If

                    ElseIf (TypeOf _cycle Is MDBCycle) Then

                        If (Not Double.IsNaN(_cycle.TEMPERATURE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then
                            totalTemperatureVariationPerQuantitySum += (_cycle.TEMPERATURE_VARIATION + _cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                            analysedCyclesMixQuantitySumForTempDiff += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                        End If

                        If (Not Double.IsNaN(_cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION)) Then
                            totalACPercentageVariationPerQuantitySum += (_cycle.ASPHALT_PERCENTAGE_VARIATION + _cycle.PREVIOUS_CYCLE.ASPHALT_PERCENTAGE_VARIATION) / 2 * XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                            analysedCyclesMixQuantitySumForACPercDiff += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                        End If

                        If (Not Double.IsNaN(_cycle.TEMPERATURE) AndAlso Not Double.IsNaN(_cycle.PREVIOUS_CYCLE.TEMPERATURE_VARIATION)) Then

                            overallTempVariation += Math.Abs(_cycle.TEMPERATURE - _cycle.PREVIOUS_CYCLE.TEMPERATURE)
                            analysedCyclesMixQuantitySumForTempVar += XmlSettings.Settings.instance.Usine.DataFiles.MDB.getUnitByTag(Cycle.MIX_MASS_TAG).convert(_cycle.MIX_MASS, XmlSettings.Settings.instance.Reports.MASS_UNIT)
                        End If
                    End If


                End If
            Next

            bookMarks.OverallTemperatureDifference.Text = If(analysedCyclesMixQuantitySumForTempDiff = 0, "-", (totalTemperatureVariationPerQuantitySum / analysedCyclesMixQuantitySumForTempDiff).ToString("N1"))
            bookMarks.AsphaltDifferencePercentage.Text = If(analysedCyclesMixQuantitySumForACPercDiff = 0, "-", (totalACPercentageVariationPerQuantitySum / analysedCyclesMixQuantitySumForACPercDiff).ToString("N3"))
            bookMarks.OverallTemperatureVariation.Text = If(analysedCyclesMixQuantitySumForTempVar = 0, "-", (overallTempVariation / analysedCyclesMixQuantitySumForTempVar).ToString("N1"))

            RaiseEvent CurrentProgress(60) ' 60 % Progress

            Dim nbCyclesWithAbberantTemperature As Integer = 0

            For Each _mixStat As MixStatistics In productionDay.Statistics.AllMixes

                nbCyclesWithAbberantTemperature += _mixStat.NB_CYCLES_WITH_ABBERANTE_TEMPERATURE

            Next

            bookMarks.TemperatureAberrancePercentage.Text = (nbCyclesWithAbberantTemperature / productionDay.Statistics.NB_PRODUCTIVE_CYLES * 100).ToString("N1")

            Dim nbCyclesWithAberrantACPercentage As Integer = 0

            For Each _acStat As AsphaltStatistics In productionDay.Statistics.AllAsphalts
                nbCyclesWithAberrantACPercentage += _acStat.NB_CYCLE_WITH_ABERRANT_PERCENTAGE
            Next

            bookMarks.AsphaltAberrancePercentage.Text = (nbCyclesWithAberrantACPercentage / productionDay.Statistics.NB_PRODUCTIVE_CYLES * 100).ToString("N1")

            ' -------------------------------
            ' Temperature difference graphic
            ' -------------------------------
            Dim mixTemperatureVariation As New MixTemperatureVariationGraphic(productionDay.Date_)

            For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

                If (TypeOf _cycle Is CSVCycle) Then

                    mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

                ElseIf (TypeOf _cycle Is MDBCycle) Then

                    mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)

                End If

            Next

            For Each _cycle As LOGCycle In productionDay.Statistics.ContinuousProduction.Cycles

                mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)

            Next

            mixTemperatureVariation.save()

            Dim g5 = bookMarks.TemperatureVariationGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC, False, True)

            g5.Width = bookMarks.ProductionQuantityGraphic.Cells(1).Width


            ' -----------------------------------
            ' CARBURANTS
            ' -----------------------------------

            With XmlSettings.Settings.instance.Usine.FuelsInfo

                bookMarks.Fuel1Name.Text = .FUEL_1_NAME
                bookMarks.Fuel2Name.Text = .FUEL_2_NAME

                bookMarks.Fuel1Quantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMED_QUANTITY_1)
                bookMarks.Fuel1ConsumptionRate.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMPTION_RATE_1, "N1") & " " & .FUEL_1_UNIT & "/T"

                bookMarks.Fuel2Quantity.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMED_QUANTITY_2)
                bookMarks.Fuel2ConsumptionRate.Text = Me.Formater.getManualDataString(productionDay.ManualData.FUEL_CONSUMPTION_RATE_2, "N1") & " " & .FUEL_2_UNIT & "/T"

            End With


            ' -------
            ' REJETS
            ' -------
            bookMarks.RejectedAggregates.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_AGGREGATES_QUANTITY)
            bookMarks.RejectedAggregatesPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_AGGREGATES_PERCENTAGE, "N1")

            bookMarks.RejectedFiller.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_FILLER_QUANTITY)
            bookMarks.RejectedFillerPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_FILLER_PERCENTAGE, "N1")

            bookMarks.RejectedRecycled.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_RECYCLED_QUANTITY)
            bookMarks.RejectedRecycledPercentage.Text = Me.Formater.getManualDataString(productionDay.ManualData.REJECTED_RECYCLED_PERCENTAGE, "N1")

            RaiseEvent CurrentProgress(72) ' 72 % Progress

            ' Using the with statement is faster by a couple seconds with the way I fill the next tables
            With WordApp.Selection

                ' ---------------------
                ' DELAYS SUMMARY TABLE
                ' ---------------------

                If (productionDay.Statistics.EventsStatistics.JustifiableDelays.Count > 0) Then

                    Dim delays = productionDay.Statistics.EventsStatistics.JustifiableDelays

                    Dim _delay As Delay

                    ' All delays except first, starting from last
                    For i = delays.Count - 1 To 1 Step -1

                        _delay = delays(i)

                        ' Add new row
                        bookMarks.FirstDelayStartTime.Select()
                        .InsertRowsBelow(1)

                        ' Start time (already selected after insertRowBelow()
                        .Text = _delay.StartTime.ToString(Me.Formater.TimeFormat)


                        .SelectRow()
                        .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                        ' Alternate white rows
                        If (i Mod 2 = 1) Then
                            .Shading.BackgroundPatternColor = WdColor.wdColorWhite
                        End If

                        ' End time

                        moveSelectionToCellBelow(bookMarks.FirstDelayEndTime)
                        .Text = _delay.EndTime.ToString(Me.Formater.TimeFormat)

                        ' Duration
                        moveSelectionToCellBelow(bookMarks.FirstDelayDuration)
                        .Text = _delay.Duration.ToString(Me.Formater.DurationFormat)

                        ' Select cell for delay code
                        moveSelectionToCellBelow(bookMarks.FirstDelayCode)

                        If (_delay.IsUnknown) Then

                            ' Delay Code (unknown)
                            .Text = Me.Formater.UnknownValueCharacter
                            .Shading.BackgroundPatternColor = WdColor.wdColorWhite

                            ' Delay justification
                            moveSelectionToCellBelow(bookMarks.FirstDelayJustification)
                            .Text = _delay.Justification

                        ElseIf (IsNothing(_delay.Code)) Then

                            ' Delay Code
                            .Text = "-"
                            .Shading.BackgroundPatternColor = WdColor.wdColorWhite

                            ' Delay code description
                            moveSelectionToCellBelow(bookMarks.FirstDelayDescription)
                            .Text = "-"

                            ' Delay justification
                            moveSelectionToCellBelow(bookMarks.FirstDelayJustification)
                            .Text = "-"

                        Else

                            ' Delay Code
                            .Text = _delay.Code.Code
                            .Shading.BackgroundPatternColor = RGB(_delay.Type.Color.R, _delay.Type.Color.G, _delay.Type.Color.B)

                            ' Delay code description
                            moveSelectionToCellBelow(bookMarks.FirstDelayDescription)
                            .Text = _delay.Code.Description

                            ' Delay justification
                            moveSelectionToCellBelow(bookMarks.FirstDelayJustification)
                            .Text = _delay.Justification
                        End If

                    Next

                    _delay = delays(0)

                    ' First delay
                    bookMarks.FirstDelayStartTime.Text = _delay.StartTime.ToString(Me.Formater.TimeFormat)
                    bookMarks.FirstDelayEndTime.Text = _delay.EndTime.ToString(Me.Formater.TimeFormat)
                    bookMarks.FirstDelayDuration.Text = _delay.Duration.ToString(Me.Formater.DurationFormat)

                    If (_delay.IsUnknown) Then

                        bookMarks.FirstDelayCode.Text = Me.Formater.UnknownValueCharacter
                        bookMarks.FirstDelayCode.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                        bookMarks.FirstDelayJustification.Text = _delay.Justification

                    ElseIf (IsNothing(_delay.Code)) Then

                        bookMarks.FirstDelayCode.Text = Me.Formater.InvalidValueCharacter
                        bookMarks.FirstDelayCode.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                        bookMarks.FirstDelayDescription.Text = Me.Formater.InvalidValueCharacter
                        bookMarks.FirstDelayJustification.Text = Me.Formater.InvalidValueCharacter

                    Else

                        bookMarks.FirstDelayCode.Text = _delay.Code.Code
                        bookMarks.FirstDelayCode.Shading.BackgroundPatternColor = RGB(_delay.Type.Color.R, _delay.Type.Color.G, _delay.Type.Color.B)

                        bookMarks.FirstDelayDescription.Text = _delay.Code.Description

                        bookMarks.FirstDelayJustification.Text = _delay.Justification

                    End If

                Else

                    bookMarks.FirstDelayStartTime.Select()
                    .Rows.Delete()

                End If

                bookMarks.JustificationDuration.Text = XmlSettings.Settings.instance.Usine.Events.Delays.JUSTIFIABLE_DURATION.TotalMinutes.ToString("N0")
                bookMarks.NbDelaysNotJustified.Text = productionDay.Statistics.EventsStatistics.Delays.Count - productionDay.Statistics.EventsStatistics.JustifiableDelays.Count
                bookMarks.DelaysNotJustifiedDuration.Text = productionDay.Statistics.EventsStatistics.DelaysDuration.Subtract(productionDay.Statistics.EventsStatistics.JustifiableDelaysDuration).TotalMinutes.ToString("N0")

                RaiseEvent CurrentProgress(80) ' 80 % Progress


                ' ------------------------------------
                ' CONTINUOUS PRODUCTION SUMMARY TABLE
                ' ------------------------------------


                Dim nonNullFeeds As New List(Of FeedersStatistics)

                ' Find non null feeder
                For Each _feedStat As FeedersStatistics In productionDay.Statistics.MixesTotal.CONTINUOUS_FEEDERS_STATS

                    If (_feedStat.TOTAL_MASS > 0) Then

                        nonNullFeeds.Add(_feedStat)

                    End If
                Next

                If (nonNullFeeds.Count > 0) Then

                    Dim columnsWidth = bookMarks.FirstContinuousProductionFeederDescription.Columns.Width / nonNullFeeds.Count

                    ' First non null feeder
                    bookMarks.FirstContinuousProductionFeederDescription.Select()

                    ' Feeder description
                    If (IsNothing(nonNullFeeds.First().MATERIAL_NAME)) Then
                        .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & "(T)"
                    Else
                        .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & nonNullFeeds.First().MATERIAL_NAME & " (T)"
                    End If

                    bookMarks.FirstContinuousProductionFeederTotalQuantity.Select()
                    .Text = nonNullFeeds.First().TOTAL_MASS.ToString("N1")

                    With .Columns.Last
                        .Width = columnsWidth
                        .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                    End With

                    ' Other non null feeders
                    For feedStatIndex = nonNullFeeds.Count - 1 To 1 Step -1

                        WordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range.Select()

                        .InsertColumnsRight()

                        ' Feeder description
                        If (IsNothing(nonNullFeeds(feedStatIndex).MATERIAL_NAME)) Then
                            .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & "(T)"
                        Else
                            .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & nonNullFeeds(feedStatIndex).MATERIAL_NAME & " (T)"
                        End If

                        With .Columns.Last
                            .Width = columnsWidth
                            .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                        End With

                        ' Total quantity
                        .MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
                        .Text = nonNullFeeds(feedStatIndex).TOTAL_MASS.ToString("N1")

                    Next

                    bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

                    ' Find first non null mixStat
                    Dim _mixStat As MixStatistics
                    Dim _nonNullFeed As FeedersStatistics
                    Dim nbNonNullMix As Integer = 0
                    Dim mixWithRecycledQuantitySum As Double = 0

                    Dim totalAsphaltQuantity As Double = 0

                    Dim firstMixStatIndex As Integer = -1

                    productionDay.Statistics.ContinuousProduction.Mixes.Sort()

                    For _firstMixStatIndex = 0 To productionDay.Statistics.ContinuousProduction.Mixes.Count - 1

                        _mixStat = productionDay.Statistics.ContinuousProduction.Mixes(_firstMixStatIndex)

                        If (_mixStat.TOTAL_MASS > 0) Then

                            firstMixStatIndex = _firstMixStatIndex

                            bookMarks.FirstContinuousProductionFormulaName.Text = _mixStat.FORMULA_NAME

                            bookMarks.FirstContinuousProductionMixName.Text = _mixStat.NAME

                            bookMarks.FirstContinuousProductionAsphaltName.Text = _mixStat.ASPHALT_STATS.NAME

                            bookMarks.FirstContinuousProductionRAP.Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

                            bookMarks.FirstContinuousProductionTotalQuantity.Text = _mixStat.TOTAL_MASS.ToString("N1")

                            bookMarks.FirstContinuousProductionAsphaltQuantity.Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

                            totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

                            For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

                                _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

                                bookMarks.FirstContinuousProductionFeederQuantity.Select()

                                For Each _currentMixFeed As FeedersStatistics In _mixStat.CONTINUOUS_FEEDERS_STATS

                                    If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

                                        .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

                                        .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

                                        Exit For ' Corresponding Feeder was found
                                    End If

                                Next

                            Next

                            If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
                                mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
                            End If

                            nbNonNullMix += 1

                            Exit For ' First non null mixStat was found
                        End If

                    Next

                    ' Used for alternate white rows
                    Dim nbRows As Integer = 1

                    ' Other non null mixstats
                    For mixStatIndex = productionDay.Statistics.ContinuousProduction.Mixes.Count - 1 To firstMixStatIndex + 1 Step -1

                        _mixStat = productionDay.Statistics.ContinuousProduction.Mixes(mixStatIndex)

                        If (_mixStat.TOTAL_MASS > 0) Then

                            bookMarks.FirstContinuousProductionFormulaName.Select()

                            .InsertRowsBelow()
                            nbRows += 1

                            ' Formula Name
                            .Text = _mixStat.FORMULA_NAME

                            ' Mix Name
                            moveSelectionToCellBelow(bookMarks.FirstContinuousProductionMixName)
                            .Text = _mixStat.NAME

                            ' Asphalt Name
                            moveSelectionToCellBelow(bookMarks.FirstContinuousProductionAsphaltName)
                            .Text = _mixStat.ASPHALT_STATS.NAME

                            ' target recycled percentage
                            moveSelectionToCellBelow(bookMarks.FirstContinuousProductionRAP)
                            .Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

                            ' mix quantity
                            moveSelectionToCellBelow(bookMarks.FirstContinuousProductionTotalQuantity)
                            .Text = _mixStat.TOTAL_MASS.ToString("N1")

                            ' Asphalt Quantity
                            moveSelectionToCellBelow(bookMarks.FirstContinuousProductionAsphaltQuantity)
                            .Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

                            totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

                            For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

                                _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

                                moveSelectionToCellBelow(bookMarks.FirstContinuousProductionFeederQuantity)

                                For Each _currentMixFeed As FeedersStatistics In _mixStat.CONTINUOUS_FEEDERS_STATS

                                    If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

                                        .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

                                        .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

                                        Exit For ' Corresponding Feeder was found
                                    End If
                                Next
                            Next

                            If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
                                mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
                            End If

                            nbNonNullMix += 1

                        End If
                    Next

                    ' Alternate white rows and remove borders
                    For i = 1 To nbRows - 1

                        bookMarks.FirstContinuousProductionFormulaName.Select()
                        .MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                        .SelectRow()

                        .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                        If (i Mod 2 = 1) Then
                            .Shading.BackgroundPatternColor = WdColor.wdColorWhite
                        End If
                    Next

                    bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

                    bookMarks.ContinuousProductionTotalQuantity.Text = productionDay.Statistics.ContinuousProduction.Quantity.ToString("N1")
                    bookMarks.ContinuousProductionTotalAsphaltQuantity.Text = totalAsphaltQuantity.ToString("N1")
                    bookMarks.ContinuousProductionMixWithRecycledPercentage.Text = (mixWithRecycledQuantitySum / productionDay.Statistics.ContinuousProduction.Quantity * 100).ToString("N0")

                    bookMarks.ContinuousProductionTotalCellsToMerge.Cells.Merge()

                Else

                    bookMarks.ContinuousProductionSummarySection.Delete()

                End If

                RaiseEvent CurrentProgress(85) ' 85 % Progress

                ' ------------------------------------
                ' DISCONTINUOUS PRODUCTION SUMMARY TABLE
                ' ------------------------------------

                nonNullFeeds.Clear()

                ' Find non null feeder
                For Each _feedStat As FeedersStatistics In productionDay.Statistics.MixesTotal.DISCONTINUOUS_FEEDERS_STATS

                    If (_feedStat.TOTAL_MASS > 0) Then

                        nonNullFeeds.Add(_feedStat)

                    End If
                Next

                If (nonNullFeeds.Count > 0) Then

                    Dim columnsWidth = bookMarks.FirstDiscontinuousProductionFeederDescription.Columns.Width / nonNullFeeds.Count

                    ' First non null feeder
                    bookMarks.FirstDiscontinuousProductionFeederDescription.Select()

                    ' Feeder= description
                    If (IsNothing(nonNullFeeds.First().MATERIAL_NAME)) Then
                        .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & "(T)"
                    Else
                        .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & nonNullFeeds.First().MATERIAL_NAME & " (T)"
                    End If

                    bookMarks.FirstDiscontinuousProductionFeederTotalQuantity.Select()
                    .Text = nonNullFeeds.First().TOTAL_MASS.ToString("N1")

                    With .Columns.Last
                        .Width = columnsWidth
                        .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                    End With

                    ' Other non null feeders
                    For feedStatIndex = nonNullFeeds.Count - 1 To 1 Step -1

                        WordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range.Select()

                        .InsertColumnsRight()

                        ' Feeder description
                        If (IsNothing(nonNullFeeds(feedStatIndex).MATERIAL_NAME)) Then
                            .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & "(T)"
                        Else
                            .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & nonNullFeeds(feedStatIndex).MATERIAL_NAME & " (T)"
                        End If

                        With .Columns.Last
                            .Width = columnsWidth
                            .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                        End With

                        ' Total quantity
                        .MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
                        .Text = nonNullFeeds(feedStatIndex).TOTAL_MASS.ToString("N1")

                    Next

                    bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

                    ' Find first non null mixStat
                    Dim _mixStat As MixStatistics
                    Dim _nonNullFeed As FeedersStatistics
                    Dim nbNonNullMix As Integer = 0
                    Dim mixWithRecycledQuantitySum As Double = 0

                    Dim totalAsphaltQuantity As Double = 0

                    Dim firstMixStatIndex As Integer = -1

                    productionDay.Statistics.DiscontinuousProduction.Mixes.Sort()

                    For _firstMixStatIndex = 0 To productionDay.Statistics.DiscontinuousProduction.Mixes.Count - 1

                        _mixStat = productionDay.Statistics.DiscontinuousProduction.Mixes(_firstMixStatIndex)

                        If (_mixStat.TOTAL_MASS > 0) Then

                            firstMixStatIndex = _firstMixStatIndex

                            bookMarks.FirstDiscontinuousProductionFormulaName.Text = _mixStat.FORMULA_NAME

                            bookMarks.FirstDiscontinuousProductionMixName.Text = _mixStat.NAME

                            bookMarks.FirstDiscontinuousProductionAsphaltName.Text = _mixStat.ASPHALT_STATS.NAME

                            bookMarks.FirstDiscontinuousProductionRAP.Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

                            bookMarks.FirstDiscontinuousProductionTotalQuantity.Text = _mixStat.TOTAL_MASS.ToString("N1")

                            bookMarks.FirstDiscontinuousProductionAsphaltQuantity.Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

                            totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

                            For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

                                _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

                                bookMarks.FirstDiscontinuousProductionFeederQuantity.Select()

                                For Each _currentMixFeed As FeedersStatistics In _mixStat.DISCONTINUOUS_FEEDERS_STATS

                                    If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

                                        .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

                                        .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

                                        Exit For ' Corresponding Feeder was found
                                    End If

                                Next

                            Next

                            If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
                                mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
                            End If

                            nbNonNullMix += 1

                            Exit For ' First non null mixStat was found
                        End If

                    Next

                    ' Used for alternate white rows
                    Dim nbRows As Integer = 1

                    ' Other non null mixstats
                    For mixStatIndex = productionDay.Statistics.DiscontinuousProduction.Mixes.Count - 1 To firstMixStatIndex + 1 Step -1

                        _mixStat = productionDay.Statistics.DiscontinuousProduction.Mixes(mixStatIndex)

                        If (_mixStat.TOTAL_MASS > 0) Then

                            bookMarks.FirstDiscontinuousProductionFormulaName.Select()

                            .InsertRowsBelow()
                            nbRows += 1

                            ' Formula Name
                            .Text = _mixStat.FORMULA_NAME

                            ' Mix Name
                            moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionMixName)
                            .Text = _mixStat.NAME

                            ' Asphalt Name
                            moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionAsphaltName)
                            .Text = _mixStat.ASPHALT_STATS.NAME

                            ' target recycled percentage
                            moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionRAP)
                            .Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

                            ' mix quantity
                            moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionTotalQuantity)
                            .Text = _mixStat.TOTAL_MASS.ToString("N1")

                            ' Asphalt Quantity
                            moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionAsphaltQuantity)
                            .Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

                            totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

                            For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

                                _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

                                moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionFeederQuantity)

                                For Each _currentMixFeed As FeedersStatistics In _mixStat.DISCONTINUOUS_FEEDERS_STATS

                                    If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

                                        .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

                                        .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

                                        Exit For ' Corresponding Feeder was found
                                    End If
                                Next
                            Next

                            If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
                                mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
                            End If

                            nbNonNullMix += 1

                        End If
                    Next

                    ' Alternate white rows and remove borders
                    For i = 1 To nbRows - 1

                        bookMarks.FirstDiscontinuousProductionFormulaName.Select()
                        .MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                        .SelectRow()

                        .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                        If (i Mod 2 = 1) Then
                            .Shading.BackgroundPatternColor = WdColor.wdColorWhite
                        End If
                    Next

                    bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

                    bookMarks.DiscontinuousProductionTotalQuantity.Text = productionDay.Statistics.DiscontinuousProduction.Quantity.ToString("N1")
                    bookMarks.DiscontinuousProductionTotalAsphaltQuantity.Text = totalAsphaltQuantity.ToString("N1")
                    bookMarks.DiscontinuousProductionMixWithRecycledPercentage.Text = (mixWithRecycledQuantitySum / productionDay.Statistics.DiscontinuousProduction.Quantity * 100).ToString("N0")

                    bookMarks.DiscontinuousProductionTotalCellsToMerge.Cells.Merge()


                Else

                    bookMarks.DiscontinuousProductionSummarySection.Delete()

                End If
            End With

            RaiseEvent CurrentProgress(90) ' 90 % Progress

            ' ---------
            ' Comments
            ' ---------

            bookMarks.Comments.Text = productionDay.Comments

            ' ----------
            ' Signature
            ' ----------

            If (Not productionDay.ManualData.FACTORY_OPERATOR = FactoryOperator.DEFAULT_OPERATOR) Then

                bookMarks.OperatorName.Text = productionDay.ManualData.FACTORY_OPERATOR.ToString()
            End If

            bookMarks.CurrentDate1.Text = Date.Today.ToString(Me.Formater.FullDateFormat)
            bookMarks.CurrentDate2.Text = Date.Now.ToString(Me.Formater.DateTimeFormat)

            ' -----
            ' SAVE N QUIT
            ' -----
            Dim savePath = Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & "Rapport Journalier Sommaire - " & XmlSettings.Settings.instance.Usine.PLANT_NAME & productionDay.Date_.ToString(" - (yyyy-MM-dd)")

            Me.WordDoc.SaveAs2(savePath)
            Dim writableReport As New SummaryDailyReport(productionDay.Date_, savePath & ".docx", False)
            productionDay.ReportFilesInfo.addReport(writableReport)

            Me.WordDoc.SaveAs2(savePath, WdSaveFormat.wdFormatPDF)
            Dim readOnlyReport As New SummaryDailyReport(productionDay.Date_, savePath & ".pdf", True)
            productionDay.ReportFilesInfo.addReport(readOnlyReport)

            ProgramController.ReportsPersistence.addDailyReports(productionDay.Date_, writableReport.getFileInfo.FullName, readOnlyReport.getFileInfo.FullName, Nothing)

            Me.killDocumentObjects()

            If (XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_READ_ONLY) Then
                readOnlyReport.open()
            End If

            If (XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_WRITABLE) Then
                writableReport.open()
            End If

            RaiseEvent CurrentProgress(100) ' 100 % Progress


        Catch ex As Threading.ThreadAbortException

            Me.disposeOfRessources()

            RaiseEvent ProcessInterrupted(Me, ex)
        End Try

        RaiseEvent ProcessComplete(Me)

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
