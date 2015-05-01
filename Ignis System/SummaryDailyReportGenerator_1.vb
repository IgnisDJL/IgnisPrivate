Imports Microsoft.Office.Interop.Word

''' <summary>
''' 
''' </summary>
''' <remarks>
''' Find first and iterate from last method prooved to be the fastest (after comparing with using table objects).
''' I know it's a lot of duplicated code but it's worth it performance wise. Using bookmarks is also very fast.
''' </remarks>
Public Class SummaryDailyReportGenerator_1
    Inherits ReportGenerator
    Implements IGNIS.Eventing.TrackableProcess

    Private dailyReport As DailyReport

    Private bookMarks As Constants.Reports.BookMarks.SummaryDailyReportBookMarks

    Public Sub New(dailyReport As DailyReport)
        MyBase.New(New SummaryDailyReportFormater)

        Me.dailyReport = dailyReport
        initializeWordApplication()

        Me.bookMarks = New Constants.Reports.BookMarks.SummaryDailyReportBookMarks

    End Sub

    Public Function generateReport() As SummaryDailyReport

        Try

            Me.WordDoc = WordApp.Documents.Open(Constants.Paths.SUMMARY_DAILY_REPORT_TEMPLATE, False, True)

            Me.bookMarks.initialize(Me.WordDoc)


            '*****************************************************************************************************************************************
            '*                                          Section des informations extraites du fichier XML 
            '*****************************************************************************************************************************************


            ' Information de l'usine
            bookMarks.FactoryName.Text = XmlSettings.Settings.instance.Usine.PLANT_NAME
            bookMarks.FactoryId.Text = XmlSettings.Settings.instance.Usine.PLANT_ID


            '*****************************************************************************************************************************************
            '*                                          Section des informations extraites du rapport 
            '*****************************************************************************************************************************************

            ' Date
            bookMarks.ProductionDayDate.Text = New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day).ToString(Me.Formater.ShortDateFormat)

            ' Add er when first of month

            ajustDateString(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day), bookMarks.ProductionDayDate)

            ' --------
            ' TABLE 1
            ' --------

            ' Operation
            Dim tableauHoraire As List(Of ArrayList) = dailyReport.getTableauHoraire

            Dim ligneOpperation As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_Operation)
            bookMarks.OperationStartTime.Text = CType(ligneOpperation.Item(EnumDailyReportTableauIndex.colonne_OpperationDebut), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.OperationEndTime.Text = CType(ligneOpperation.Item(EnumDailyReportTableauIndex.colonne_OpperationFin), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.OperationDuration.Text = CType(ligneOpperation.Item(EnumDailyReportTableauIndex.colonne_OpperationDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            ' Production

            Dim ligneProduction As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_Production)

            bookMarks.ProductionStartTime.Text = CType(ligneProduction.Item(EnumDailyReportTableauIndex.colonne_ProductionDebut), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.ProductionEndTime.Text = CType(ligneProduction.Item(EnumDailyReportTableauIndex.colonne_ProductionFin), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.ProductionDuration.Text = CType(ligneProduction.Item(EnumDailyReportTableauIndex.colonne_ProductionDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            Dim ligneDelaisPauses As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_DelaisPauses)

            bookMarks.PausesDuration.Text = CType(ligneDelaisPauses.Item(EnumDailyReportTableauIndex.colonne_PausesDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            Dim ligneDelaisEntretiens As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_DelaisEntretiens)

            bookMarks.MaintenanceDuration.Text = CType(ligneDelaisEntretiens.Item(EnumDailyReportTableauIndex.colonne_Entretiens), TimeSpan).ToString(Me.Formater.DurationFormat)

            RaiseEvent CurrentProgress(12) ' 12 % Progress

            ' --------
            ' TABLE 2 
            ' --------

            Dim tableauEnrobes As List(Of ArrayList) = dailyReport.getTableauEnrobes

            Dim ligneEnrobe1 As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_Enrobe1)

            If (ligneEnrobe1.Count > 0) Then
                bookMarks.FirstMixName.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1NoFormule)
                bookMarks.FirstMixAsphaltTemperatureSpan.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1NomEnrobe)
                bookMarks.FirstMixQuantity.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1Quantite)
                bookMarks.FirstMixProductionRate.Text = CType(ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1Production), Double).ToString("N0")
                bookMarks.FirstMixProductionType.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1ProductionMode)
            Else
                bookMarks.FirstMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.FirstMixAsphaltTemperatureSpan.Text = Me.Formater.InvalidValueCharacter
                bookMarks.FirstMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.FirstMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.FirstMixProductionType.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneEnrobe2 As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_Enrobe2)

            If (ligneEnrobe2.Count > 0) Then

                bookMarks.SecondMixName.Text = ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2NoFormule)
                bookMarks.SecondMixAsphaltTemperatureSpan.Text = ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2NomEnrobe)
                bookMarks.SecondMixQuantity.Text = CType(ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2Quantite), Double).ToString("N0")
                bookMarks.SecondMixProductionRate.Text = CType(ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2Production), Double).ToString("N0")
                bookMarks.SecondMixProductionType.Text = ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2ProductionMode)

            Else
                bookMarks.SecondMixName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixAsphaltTemperatureSpan.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.SecondMixProductionType.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneEnrobe3 As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_Enrobe3)

            If (ligneEnrobe3.Count > 0) Then

                bookMarks.ThirdMixName.Text = ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3NoFormule)
                bookMarks.ThirdMixAsphaltTemperatureSpan.Text = ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3NomEnrobe)
                bookMarks.ThirdMixQuantity.Text = CType(ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3Quantite), Double).ToString("N0")
                bookMarks.ThirdMixProductionRate.Text = CType(ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3Production), Double).ToString("N0")
                bookMarks.ThirdMixProductionType.Text = ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3ProductionMode)

            Else
                bookMarks.ThirdMixName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixAsphaltTemperatureSpan.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ThirdMixProductionType.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneEnrobeAutres As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_EnrobeAutres)

            If (ligneEnrobeAutres.Count > 0) Then

                bookMarks.NumberOfOtherMixes.Text = ligneEnrobeAutres.Item(EnumDailyReportTableauIndex.colonne_EnrobeAutreNombre)
                bookMarks.OtherMixesQuantity.Text = CType(ligneEnrobeAutres.Item(EnumDailyReportTableauIndex.colonne_EnrobeAutreQuantite), Double).ToString("N0")
                bookMarks.OtherMixesProductionRate.Text = CType(ligneEnrobeAutres.Item(EnumDailyReportTableauIndex.colonne_EnrobeAutreProduction), Double).ToString("N0")
                bookMarks.OtherMixesProductionType.Text = Me.Formater.InvalidValueCharacter
            Else
                bookMarks.NumberOfOtherMixes.Text = Me.Formater.InvalidValueCharacter
                bookMarks.OtherMixesQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.OtherMixesProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.OtherMixesProductionType.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneQuantiteTotaleProduite As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotaleProduite)

            bookMarks.TotalQuantityProduced.Text = CType(ligneQuantiteTotaleProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleProduiteQuantite), Double).ToString("N0")
            bookMarks.TotalProductionRate.Text = CType(ligneQuantiteTotaleProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleProduiteProduction), Double).ToString("N0")


            Dim ligneQuantiteEnSiloDebut As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteEnSiloDebut)

            ' Silo at start
            bookMarks.SiloQuantityAtStart.Text = ligneQuantiteEnSiloDebut.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloDebutQuantite)

            RaiseEvent CurrentProgress(24) ' 24 % Progress

            Dim ligneQuantiteEnSiloFin As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteEnSiloFin)

            ' Silo at end
            bookMarks.SiloQuantityAtEnd.Text = ligneQuantiteEnSiloFin.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloFinQuantite)

            Dim ligneQuantiteTotaleVendable As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotaleVendable)
            ' Salable qty
            bookMarks.SalableQuantity.Text = CType(ligneQuantiteTotaleVendable.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendableQuantite), Double).ToString("N0")

            Dim ligneRejetsEnrobes As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_RejetsEnrobes)
            ' Rejected mix
            bookMarks.RejectedMixQuantity.Text = CType(ligneRejetsEnrobes.Item(EnumDailyReportTableauIndex.colonne_RejetsEnrobesQuantite), Double).ToString("N0")
            bookMarks.RejectedMixPercentage.Text = CType(ligneRejetsEnrobes.Item(EnumDailyReportTableauIndex.colonne_RejetsEnrobesPourcentageRejet), Double).ToString("N1")


            Dim ligneQuantiteTotalePayable As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotalePayable)

            ' Payable qty
            bookMarks.TotalPayableQuantity.Text = CType(ligneQuantiteTotalePayable.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotalePayableQuantite), Double).ToString("N0")

            Dim ligneQuantiteTotaleVendue As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotaleVendue)

            ' Sold (weighted) qty
            bookMarks.TotalQuantitySold.Text = CType(ligneQuantiteTotaleVendue.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendueQuantite), Double).ToString("N0")
            bookMarks.TotalQuantitySoldDifferencePercentage.Text = CType(ligneQuantiteTotaleVendue.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVenduePourcentageEcart), Double).ToString("N1")

            ' --------------------
            ' Graphic 1 et 2 #refactor generate before graphs and store them
            ' --------------------
            XYScatterGraphic.pointFormatList_asphalt = New PointFormatList
            XYScatterGraphic.pointFormatList_mix = New PointFormatList

            Dim isHybrid As Boolean = XmlSettings.Settings.instance.Usine.DataFiles.CSV.ACTIVE AndAlso XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE

            Dim accumulatedMass As New AccumulatedMassGraphic(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day), isHybrid)
            Dim productionSpeed As New ProductionSpeedGraphic(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day))

            'For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

            '    If (TypeOf _cycle Is CSVCycle) Then

            '        accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)
            '        productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

            '    ElseIf (TypeOf _cycle Is MDBCycle) Then

            '        accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
            '        productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)
            '    End If

            'Next

            'accumulatedMass.toggleMarkerColor()
            'productionSpeed.toggleMarkerColor()

            'For Each _cycle In productionDay.Statistics.ContinuousProduction.Cycles

            '    accumulatedMass.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
            '    productionSpeed.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)
            'Next

            'accumulatedMass.save()
            'productionSpeed.save()

            'Dim g1 = bookMarks.ProductionQuantityGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC, False, True)
            'Dim g2 = bookMarks.ProductionRateGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_SPEED_GRAPHIC, False, True)

            'g1.Width = bookMarks.ProductionQuantityGraphic.Cells(1).Width
            'g2.Width = bookMarks.ProductionQuantityGraphic.Cells(1).Width


            ' --------------------
            ' PRODUCTION ET DELAIS
            ' --------------------

            Dim tableauModeDeProduction As List(Of ArrayList) = dailyReport.getTableauModeProduction

            ' Durée
            Dim ligneDuree As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_Duree)
            bookMarks.ContinuousProductionDuration.Text = CType(ligneDuree.Item(EnumDailyReportTableauIndex.colonne_DureeContinu), TimeSpan).ToString(Me.Formater.DurationFormat)
            bookMarks.DiscontinuousProductionDuration.Text = CType(ligneDuree.Item(EnumDailyReportTableauIndex.colonne_DureeDiscontinu), TimeSpan).ToString(Me.Formater.DurationFormat)
            bookMarks.DelaysDuration.Text = CType(ligneDuree.Item(EnumDailyReportTableauIndex.colonne_DureeDelais), TimeSpan).ToString(Me.Formater.DurationFormat)

            'Pourcentage du temps
            Dim lignePourcentageDuTemps As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_PourcentageDuTemps)
            bookMarks.ContinuousProductionPercentage.Text = CType(lignePourcentageDuTemps.Item(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsContinu), Double).ToString("N0")
            bookMarks.DiscontinuousProductionPercentage.Text = CType(lignePourcentageDuTemps.Item(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsDiscontinu), Double).ToString("N0")
            bookMarks.DelaysPercentage.Text = CType(lignePourcentageDuTemps.Item(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsDelais), Double).ToString("N0")

            'Nombre de changement de mélanges / délais
            Dim ligneNombreDeChangements As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_NombreDeChangements)
            bookMarks.NbSwitchContinuous.Text = ligneNombreDeChangements.Item(EnumDailyReportTableauIndex.colonne_NombreDeChangementsContinu)
            bookMarks.NbMixSwitchDiscontinuous.Text = ligneNombreDeChangements.Item(EnumDailyReportTableauIndex.colonne_NombreDeChangementsDiscontinu)
            bookMarks.NumberOfDelays.Text = ligneNombreDeChangements.Item(EnumDailyReportTableauIndex.colonne_NombreDeChangementsDelais)

            'Quantite produite
            Dim ligneQuantiteProduite As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_QuantiteProduite)
            bookMarks.ContinuousProductionQuantity.Text = CType(ligneQuantiteProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteProduiteContinu), Double).ToString("N0")
            bookMarks.DiscontinuousProductionQuantity.Text = CType(ligneQuantiteProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteProduiteDiscontinu), Double).ToString("N0")

            'Taux de production
            Dim ligneTauxDeProduction As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_TauxDeProduction)
            bookMarks.ContinuousProductionRate.Text = CType(ligneTauxDeProduction.Item(EnumDailyReportTableauIndex.colonne_TauxDeProductionContinu), Double).ToString("N0")
            bookMarks.DiscontinuousProductionRate.Text = CType(ligneTauxDeProduction.Item(EnumDailyReportTableauIndex.colonne_TauxDeProductionDiscontinu), Double).ToString("N0")

            RaiseEvent CurrentProgress(36) ' 36 % Progress

            ' -------------------
            ' TEMPS DE PRODUCTION
            ' -------------------

            Dim tableauTempsDeProduction As List(Of ArrayList) = dailyReport.getTableauTempsDeProduction

            ' Temps total d’opération 
            Dim ligneTempsTotalOperations As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_TempsTotalOperations)
            bookMarks.GrossOperationDuration.Text = CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_TempsTotalOperationsDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            ' Temps net d’opération 
            Dim ligneTempsNetOperations As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_TempsNetOperations)
            bookMarks.NetOperationDuration.Text = CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_TempsNetOperationsDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            ' Production nette 
            Dim ligneProductionNette As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_ProductionNette)
            bookMarks.NetProductionDuration.Text = CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_ProductionNetteDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            'Production efficace 
            Dim ligneProductionEfficace As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_ProductionEfficace)
            bookMarks.EffectiveProductionDuration.Text = CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_ProductionEfficaceDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            ' Production efficace interne 
            Dim ligneProductionEfficaceInterne As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_ProductionEfficaceInterne)
            bookMarks.EffectiveInternProductionDuration.Text = CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_ProductionEfficaceInterneDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            Dim ligneDelais As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_Delais)
            bookMarks.AllDelaysDuration.Text = CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_DelaisDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            ' -------
            ' DELAIS
            ' -------

            Dim tableauDelais As List(Of ArrayList) = dailyReport.getTableauDelais

            Dim ligneNombreDeBris As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_NombreDeBris)
            bookMarks.NbOfBreakages.Text = ligneNombreDeBris.Item(EnumDailyReportTableauIndex.colonne_NombreDeBris)

            ' Disponibilité (%) = Production efficace interne / Production nette * 100
            Dim ligneDisponibilite As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_Disponibilite)
            bookMarks.DisponibilityPercentage.Text = CType(ligneDisponibilite.Item(EnumDailyReportTableauIndex.colonne_Disponibilite), Double).ToString("N0")

            ' Utilisation (%) = Production efficace / Temps total d’opération
            Dim ligneUtilisation As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_Utilisation)
            bookMarks.UtilisationPercentage.Text = CType(ligneUtilisation.Item(EnumDailyReportTableauIndex.colonne_Utilisation), Double).ToString("N0")

            If ligneNombreDeBris.Item(EnumDailyReportTableauIndex.colonne_NombreDeBris) > 0 Then
                Dim ligneTempsEntrePannes As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_TempsEntrePannes)
                bookMarks.TimeBetweenBreakDowns.Text = CType(ligneTempsEntrePannes.Item(EnumDailyReportTableauIndex.colonne_TempsEntrePannes), TimeSpan).ToString(Me.Formater.DurationFormat)

                Dim ligneTempsPourReparer As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_TempsPourReparer)
                bookMarks.ReparationsDuration.Text = CType(ligneTempsPourReparer.Item(EnumDailyReportTableauIndex.colonne_TempsPourReparer), TimeSpan).ToString(Me.Formater.DurationFormat)
            Else

                bookMarks.TimeBetweenBreakDowns.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ReparationsDuration.Text = Me.Formater.InvalidValueCharacter
            End If

            ' ----------------------
            ' DISTRIBUTION GRAPHICS
            ' ----------------------

            'Dim pdg = New ProductionDistributionGraphic(productionDay.Statistics.ProductionEndTime.Subtract(productionDay.Statistics.ProductionStartTime), _
            '                                            productionDay.Statistics.EventsStatistics.PausesDuration, _
            '                                            productionDay.Statistics.EventsStatistics.MaintenanceDuration, _
            '                                            productionDay.Statistics.EventsStatistics.DelaysDuration)

            'pdg.save()

            'Dim ddg = New DelaysDistributionGraphic(productionDay.Statistics.EventsStatistics.InternWithBreakageDuration, _
            '                                        productionDay.Statistics.EventsStatistics.InternWithoutBreakageDuration, _
            '                                        productionDay.Statistics.EventsStatistics.ExternDuration, _
            '                                        productionDay.Statistics.EventsStatistics.OtherDelaysDuration)

            'ddg.save()

            'Dim g3 = bookMarks.ProductionDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_DISTRIBUTION_GRAPHIC, False, True)

            'g3.Width = bookMarks.ProductionDistributionGraphic.Cells(1).Width

            'Dim g4 = bookMarks.DelaysDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.DELAYS_DISTRIBUTION_GRAPHIC, False, True)

            'g4.Width = bookMarks.DelaysDistributionGraphic.Cells(1).Width

            ' -----------------
            ' BITUMES CONSOMMÉ
            ' -----------------

            Dim tableauBitumeConsommes As List(Of ArrayList) = dailyReport.getTableauBitumeConsommes

            If tableauBitumeConsommes.Count > 1 Then

                Dim ligneVirginAsphaltConcrete As ArrayList

                ligneVirginAsphaltConcrete = tableauBitumeConsommes.Item(EnumDailyReportTableauIndex.ligne_VirginAsphaltConcrete)

                bookMarks.FirstAsphaltNumber.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteReservoir)
                bookMarks.FirstAsphaltName.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteGrade)
                bookMarks.FirstAsphaltQuantity.Text = CType(ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteQuantite), Double).ToString("N1")

                For i = tableauBitumeConsommes.Count - 2 To 1 Step -1

                    bookMarks.FirstAsphaltNumber.Select()
                    WordApp.Selection.InsertRowsBelow(1)

                    ligneVirginAsphaltConcrete = tableauBitumeConsommes.Item(i)

                    ' Number (cursor already in position
                    WordApp.Selection.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteReservoir)

                    ' Name
                    moveSelectionToCellBelow(bookMarks.FirstAsphaltName)
                    WordApp.Selection.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteGrade)

                    ' Quantity
                    moveSelectionToCellBelow(bookMarks.FirstAsphaltQuantity)
                    WordApp.Selection.Text = CType(ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteQuantite), Double).ToString("N1")

                    WordApp.Selection.SelectRow()
                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone
                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If

                Next

                Dim ligneTotalBitumeConsommes As ArrayList = tableauBitumeConsommes.Item(tableauBitumeConsommes.Count - 1)
                bookMarks.TotalAsphaltQuantity.Text = CType(ligneTotalBitumeConsommes.Item(EnumDailyReportTableauIndex.colonne_TotalBitumeConsommesQuantite), Double).ToString("N1")
            Else

                bookMarks.FirstAsphaltNumber.Text = Me.Formater.InvalidValueCharacter
                bookMarks.FirstAsphaltName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.FirstAsphaltQuantity.Text = Me.Formater.InvalidValueCharacter

            End If


            RaiseEvent CurrentProgress(48) ' 48 % Progress

            ' -----------------------------------
            ' ECART PAR RAPPORT A LA VALEUR VISÉE
            ' -----------------------------------

            Dim tableauEcartValeurVisee As List(Of ArrayList) = dailyReport.getTableauEcartValeurVisee

            Dim ligneBitumeEcart As ArrayList = tableauEcartValeurVisee.Item(EnumDailyReportTableauIndex.ligne_BitumeEcart)
            Dim ligneTemperatureEcart As ArrayList = tableauEcartValeurVisee.Item(EnumDailyReportTableauIndex.ligne_TemperatureEcart)

            bookMarks.OverallTemperatureDifference.Text = CType(ligneBitumeEcart.Item(EnumDailyReportTableauIndex.colonne_BitumeEcartPourcentage), Double).ToString("N1")
            bookMarks.AsphaltDifferencePercentage.Text = CType(ligneTemperatureEcart.Item(EnumDailyReportTableauIndex.colonne_TemperatureEcart), Double).ToString("N3")

            ' -----------------------------------
            ' VARIATION EN PRODUCTION
            ' -----------------------------------
            Dim tableauVariationEnProduction As List(Of ArrayList) = dailyReport.getTableauVariationEnProduction

            Dim ligneVariationTemperature As ArrayList = tableauVariationEnProduction.Item(EnumDailyReportTableauIndex.ligne_VariationTemperature)

            bookMarks.OverallTemperatureVariation.Text = CType(ligneVariationTemperature.Item(EnumDailyReportTableauIndex.colonne_VariationTemperature), Double).ToString("N1")

            ' -----------------------------------
            ' TAUX DE VALEURS ABERRANTES
            ' -----------------------------------
            Dim tableauValeursAberrantes As List(Of ArrayList) = dailyReport.getTableauValeursAberrantes

            Dim lignePourcentageBitume As ArrayList = tableauValeursAberrantes.Item(EnumDailyReportTableauIndex.ligne_PourcentageBitume)
            Dim lignePourcentageTemperature As ArrayList = tableauValeursAberrantes.Item(EnumDailyReportTableauIndex.ligne_PourcentageTemperature)

            bookMarks.TemperatureAberrancePercentage.Text = CType(lignePourcentageBitume.Item(EnumDailyReportTableauIndex.colonne_PourcentageBitume), Double).ToString("N1")
            bookMarks.AsphaltAberrancePercentage.Text = CType(lignePourcentageTemperature.Item(EnumDailyReportTableauIndex.colonne_PourcentageTemperature), Double).ToString("N1")

            ' -------------------------------
            ' Temperature difference graphic
            ' -------------------------------
            'Dim mixTemperatureVariation As New MixTemperatureVariationGraphic(productionDay.Date_)

            'For Each _cycle As Cycle In productionDay.Statistics.DiscontinuousProduction.Cycles

            '    If (TypeOf _cycle Is CSVCycle) Then

            '        mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.CSV)

            '    ElseIf (TypeOf _cycle Is MDBCycle) Then

            '        mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.MDB)

            '    End If

            'Next

            'For Each _cycle As LOGCycle In productionDay.Statistics.ContinuousProduction.Cycles

            '    mixTemperatureVariation.addCycle(_cycle, XmlSettings.Settings.instance.Usine.DataFiles.LOG)

            'Next

            'mixTemperatureVariation.save()

            'Dim g5 = bookMarks.TemperatureVariationGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC, False, True)

            'g5.Width = bookMarks.ProductionQuantityGraphic.Cells(1).Width


            ' -----------------------------------
            ' CARBURANTS
            ' -----------------------------------

            Dim tableauCarburants As List(Of ArrayList) = dailyReport.getTableauCarburants

            Dim ligneCarburantPrincipal As ArrayList = tableauCarburants.Item(EnumDailyReportTableauIndex.ligne_CarburantPrincipal)
            Dim ligneCarburantGazNatutel As ArrayList = tableauCarburants.Item(EnumDailyReportTableauIndex.ligne_CarburantGazNatutel)

            bookMarks.Fuel1Name.Text = ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_NomCarburant)
            bookMarks.Fuel2Name.Text = ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_NomCarburant)

            bookMarks.Fuel1Quantity.Text = ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_QuantiteConsomme)
            bookMarks.Fuel1ConsumptionRate.Text = CType(ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_TauxDeConsommation), Double).ToString("N1") & " " & ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_UniteTauxDeConsommation)

            bookMarks.Fuel2Quantity.Text = ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_QuantiteConsomme)
            bookMarks.Fuel2ConsumptionRate.Text = CType(ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_TauxDeConsommation), Double).ToString("N1") & " " & ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_UniteTauxDeConsommation)

            ' -------
            ' REJETS
            ' -------
            Dim tableauRejets As List(Of ArrayList) = dailyReport.getTableauRejets

            Dim ligneQuantiteRejete As ArrayList = tableauRejets.Item(EnumDailyReportTableauIndex.ligne_QuantiteRejete)
            Dim ligneTauxDeRejet As ArrayList = tableauRejets.Item(EnumDailyReportTableauIndex.ligne_TauxDeRejet)

            bookMarks.RejectedAggregates.Text = ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetGranulats)
            bookMarks.RejectedFiller.Text = ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetFiller)
            bookMarks.RejectedRecycled.Text = ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetGBR)

            bookMarks.RejectedAggregatesPercentage.Text = CType(ligneTauxDeRejet.Item(EnumDailyReportTableauIndex.colonne_RejetGranulats), Double).ToString("N1")
            bookMarks.RejectedFillerPercentage.Text = CType(ligneTauxDeRejet.Item(EnumDailyReportTableauIndex.colonne_RejetFiller), Double).ToString("N1")
            bookMarks.RejectedRecycledPercentage.Text = CType(ligneTauxDeRejet.Item(EnumDailyReportTableauIndex.colonne_RejetGBR), Double).ToString("N1")

            RaiseEvent CurrentProgress(72) ' 72 % Progress

            ' Using the with statement is faster by a couple seconds with the way I fill the next tables

            ' ---------------------
            ' DELAYS SUMMARY TABLE
            ' ---------------------

            Dim tableauDelay As List(Of ArrayList) = dailyReport.getTableauDelay

            Dim ligneDelayTotal As ArrayList = tableauDelay.Item(tableauDelay.Count - 1)

            Dim ligneDelay As ArrayList
            Dim color As Color
            If (tableauDelay.Count > 2) Then

                ' All delays except first, starting from last
                For i = tableauDelay.Count - 3 To 1 Step -1

                    ligneDelay = tableauDelay.Item(i)

                    ' Add new row
                    bookMarks.FirstDelayStartTime.Select()
                    WordApp.Selection.InsertRowsBelow(1)

                    ' Start time (already selected after insertRowBelow()
                    WordApp.Selection.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDebut), Date).ToString(Me.Formater.TimeFormat)


                    WordApp.Selection.SelectRow()
                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                    ' Alternate white rows
                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If

                    ' End time

                    moveSelectionToCellBelow(bookMarks.FirstDelayEndTime)
                    WordApp.Selection.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableFin), Date).ToString(Me.Formater.TimeFormat)

                    ' Duration
                    moveSelectionToCellBelow(bookMarks.FirstDelayDuration)
                    WordApp.Selection.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

                    ' Select cell for delay code
                    moveSelectionToCellBelow(bookMarks.FirstDelayCode)

                    ' Delay Code
                    WordApp.Selection.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableName)

                    color = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisColor)

                    WordApp.Selection.Shading.BackgroundPatternColor = RGB(color.R, color.G, color.B)

                    ' Delay code description
                    moveSelectionToCellBelow(bookMarks.FirstDelayDescription)
                    WordApp.Selection.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDescription)

                    ' Delay justification
                    moveSelectionToCellBelow(bookMarks.FirstDelayJustification)
                    WordApp.Selection.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableCommentaire)


                    'If (_delay.IsUnknown) Then

                    '    ' Delay Code (unknown)
                    '    WordApp.Selection.Text = Me.Formater.UnknownValueCharacter
                    '    WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite

                    '    ' Delay justification
                    '    moveSelectionToCellBelow(bookMarks.FirstDelayJustification)
                    '    WordApp.Selection.Text = _delay.Justification

                    'ElseIf (IsNothing(_delay.Code)) Then

                    '    ' Delay Code
                    '    WordApp.Selection.Text = "-"
                    '    WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite

                    '    ' Delay code description
                    '    moveSelectionToCellBelow(bookMarks.FirstDelayDescription)
                    '    WordApp.Selection.Text = "-"

                    '    ' Delay justification
                    '    moveSelectionToCellBelow(bookMarks.FirstDelayJustification)
                    '    WordApp.Selection.Text = "-"

                    'Else

                    'End If

                Next

                ligneDelay = tableauDelay.Item(0)

                ' First delay
                bookMarks.FirstDelayStartTime.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDebut), Date).ToString(Me.Formater.TimeFormat)
                bookMarks.FirstDelayEndTime.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableFin), Date).ToString(Me.Formater.TimeFormat)
                bookMarks.FirstDelayDuration.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

                'If (_delay.IsUnknown) Then

                '    bookMarks.FirstDelayCode.Text = Me.Formater.UnknownValueCharacter
                '    bookMarks.FirstDelayCode.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                '    bookMarks.FirstDelayJustification.Text = _delay.Justification

                'ElseIf (IsNothing(_delay.Code)) Then

                '    bookMarks.FirstDelayCode.Text = Me.Formater.InvalidValueCharacter
                '    bookMarks.FirstDelayCode.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                '    bookMarks.FirstDelayDescription.Text = Me.Formater.InvalidValueCharacter
                '    bookMarks.FirstDelayJustification.Text = Me.Formater.InvalidValueCharacter

                'Else

                color = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisColor)

                bookMarks.FirstDelayCode.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableName)
                bookMarks.FirstDelayCode.Shading.BackgroundPatternColor = RGB(color.R, color.G, color.B)
                bookMarks.FirstDelayDescription.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDescription)
                bookMarks.FirstDelayJustification.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableCommentaire)

                'End If

            Else

                bookMarks.FirstDelayStartTime.Select()
                WordApp.Selection.Rows.Delete()

            End If

            Dim ligneDelayNonJustifiable As ArrayList = tableauDelay.Item(tableauDelay.Count - 2)

            bookMarks.JustificationDuration.Text = ligneDelayNonJustifiable.Item(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableLimite)
            bookMarks.NbDelaysNotJustified.Text = ligneDelayNonJustifiable.Item(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableNombre)
            bookMarks.DelaysNotJustifiedDuration.Text = CType(ligneDelayNonJustifiable.Item(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableDuree), TimeSpan).ToString(Me.Formater.DurationFormat)

            ' TODO
            ' Ajouter la ligne Durée totale des délais

            RaiseEvent CurrentProgress(80) ' 80 % Progress


            ' ------------------------------------
            ' CONTINUOUS PRODUCTION SUMMARY TABLE
            ' ------------------------------------

            '' Creation des entêtes des bennes chaudes

            ' Créeation de la première entête de benne chaude
            Dim tableauProductionContinu As List(Of ArrayList) = dailyReport.getTableauProductionContinu
            If tableauProductionContinu.Count > 4 Then

                Dim ligneSommaireEntete As ArrayList = tableauProductionContinu.Item(EnumDailyReportTableauIndex.ligne_SommaireEntete)

                If ligneSommaireEntete.Count > 0 Then

                    Dim columnsWidth = bookMarks.FirstContinuousProductionFeederDescription.Columns.Width / ligneSommaireEntete.Count
                    bookMarks.FirstContinuousProductionFeederDescription.Select()

                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete.Item(0)
                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"


                    bookMarks.FirstContinuousProductionFeederTotalQuantity.Select()
                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                Else

                End If

                ' Création de autres entête de benne chaude
                For feederEnteteIndex = ligneSommaireEntete.Count - 1 To 1 Step -1

                    WordDoc.Bookmarks("FirstContinuousProductionFeederDesc").Range.Select()

                    WordApp.Selection.InsertColumnsRight()

                    Dim columnsWidth = bookMarks.FirstContinuousProductionFeederDescription.Columns.Width / ligneSommaireEntete.Count
                    bookMarks.FirstContinuousProductionFeederDescription.Select()

                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete.Item(feederEnteteIndex)
                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"


                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter

                    ' Total quantity
                    WordApp.Selection.MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)

                Next

                bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

                '' Céeation des lignes du tableau

                ' Création de la première ligne

                Dim ligneEnrobe As ArrayList

                If tableauProductionContinu.Count > 4 Then

                    ligneEnrobe = tableauProductionContinu.Item(EnumDailyReportTableauIndex.ligne_SommaireFirstLigneEnrobe)

                    bookMarks.FirstContinuousProductionFormulaName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFormule)

                    bookMarks.FirstContinuousProductionMixName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeName)

                    bookMarks.FirstContinuousProductionAsphaltName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeGrade)

                    bookMarks.FirstContinuousProductionRAP.Text = If(Double.IsNaN(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise)), "-", CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise), Double).ToString("N0"))

                    bookMarks.FirstContinuousProductionTotalQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasse), Double).ToString("N1")

                    bookMarks.FirstContinuousProductionAsphaltQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume), Double).ToString("N1")

                    bookMarks.FirstContinuousProductionFeederQuantity.Select()

                    WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse), Double).ToString("N1")

                    For feedIndex = 1 To ligneSommaireEntete.Count - 1

                        WordApp.Selection.MoveRight(WdUnits.wdCell, WdMovementType.wdMove)

                        WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse + feedIndex), Double).ToString("N1")

                    Next

                Else

                End If

                ' Used for alternate white rows
                Dim nbRows As Integer = 1

                ' Other non null mixstats
                For indexLigneEnrobe = tableauProductionContinu.Count - 4 To 2 Step -1

                    bookMarks.FirstContinuousProductionFormulaName.Select()

                    WordApp.Selection.InsertRowsBelow()
                    nbRows += 1

                    ligneEnrobe = tableauProductionContinu.Item(indexLigneEnrobe)

                    bookMarks.FirstContinuousProductionFormulaName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFormule)

                    bookMarks.FirstContinuousProductionMixName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeName)

                    bookMarks.FirstContinuousProductionAsphaltName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeGrade)

                    bookMarks.FirstContinuousProductionRAP.Text = If(Double.IsNaN(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise)), "-", CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise), Double).ToString("N0"))

                    bookMarks.FirstContinuousProductionTotalQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasse), Double).ToString("N1")

                    bookMarks.FirstContinuousProductionAsphaltQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume), Double).ToString("N1")

                    bookMarks.FirstContinuousProductionFeederQuantity.Select()

                    WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse), Double).ToString("N1")

                    For feedIndex = 1 To ligneSommaireEntete.Count - 1

                        WordApp.Selection.MoveRight(WdUnits.wdCell, WdMovementType.wdMove)

                        WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse + feedIndex), Double).ToString("N1")

                    Next
                Next

                ' Alternate white rows and remove borders
                For i = 1 To nbRows - 1

                    bookMarks.FirstContinuousProductionFormulaName.Select()
                    WordApp.Selection.MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                    WordApp.Selection.SelectRow()

                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If
                Next

                bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

                Dim ligneEnrobeTotalMasse As ArrayList = tableauProductionContinu.Item(tableauProductionContinu.Count - 3)

                bookMarks.ContinuousProductionTotalQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasse), Double).ToString("N1")
                bookMarks.ContinuousProductionTotalAsphaltQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasseBitume), Double).ToString("N1")

                Dim ligneSommairePourcentageAvecGBR As ArrayList = tableauProductionContinu.Item(tableauProductionContinu.Count - 1)
                bookMarks.ContinuousProductionMixWithRecycledPercentage.Text = CType(ligneSommairePourcentageAvecGBR.Item(EnumDailyReportTableauIndex.colonne_SommairePourcentageAvecGBR), Double).ToString("N0")

                bookMarks.ContinuousProductionTotalCellsToMerge.Cells.Merge()

            Else

                bookMarks.ContinuousProductionSummarySection.Delete()

            End If

            RaiseEvent CurrentProgress(85) ' 85 % Progress

            ' ------------------------------------
            ' DISCONTINUOUS PRODUCTION SUMMARY TABLE Discontinuous
            ' ------------------------------------


            '    ' Find non null feeder
            '    For Each _feedStat As FeedersStatistics In productionDay.Statistics.MixesTotal.DISCONTINUOUS_FEEDERS_STATS

            '        If (_feedStat.TOTAL_MASS > 0) Then

            '            nonNullFeeds.Add(_feedStat)

            '        End If
            '    Next

            '    If (nonNullFeeds.Count > 0) Then

            '        Dim columnsWidth = bookMarks.FirstDiscontinuousProductionFeederDescription.Columns.Width / nonNullFeeds.Count

            '        ' First non null feeder
            '        bookMarks.FirstDiscontinuousProductionFeederDescription.Select()

            '        ' Feeder= description
            '        If (IsNothing(nonNullFeeds.First().MATERIAL_NAME)) Then
            '            .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & "(T)"
            '        Else
            '            .Text = nonNullFeeds.First().LOCATION & Environment.NewLine & nonNullFeeds.First().MATERIAL_NAME & " (T)"
            '        End If

            '        bookMarks.FirstDiscontinuousProductionFeederTotalQuantity.Select()
            '        .Text = nonNullFeeds.First().TOTAL_MASS.ToString("N1")

            '        With .Columns.Last
            '            .Width = columnsWidth
            '            .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
            '        End With

            '        ' Other non null feeders
            '        For feedStatIndex = nonNullFeeds.Count - 1 To 1 Step -1

            '            WordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range.Select()

            '            .InsertColumnsRight()

            '            ' Feeder description
            '            If (IsNothing(nonNullFeeds(feedStatIndex).MATERIAL_NAME)) Then
            '                .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & "(T)"
            '            Else
            '                .Text = nonNullFeeds(feedStatIndex).LOCATION & Environment.NewLine & nonNullFeeds(feedStatIndex).MATERIAL_NAME & " (T)"
            '            End If

            '            With .Columns.Last
            '                .Width = columnsWidth
            '                .Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
            '            End With

            '            ' Total quantity
            '            .MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
            '            .Text = nonNullFeeds(feedStatIndex).TOTAL_MASS.ToString("N1")

            '        Next

            '        bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

            '        ' Find first non null mixStat
            '        Dim _mixStat As MixStatistics
            '        Dim _nonNullFeed As FeedersStatistics
            '        Dim nbNonNullMix As Integer = 0
            '        Dim mixWithRecycledQuantitySum As Double = 0

            '        Dim totalAsphaltQuantity As Double = 0

            '        Dim firstMixStatIndex As Integer = -1

            '        productionDay.Statistics.DiscontinuousProduction.Mixes.Sort()

            '        For _firstMixStatIndex = 0 To productionDay.Statistics.DiscontinuousProduction.Mixes.Count - 1

            '            _mixStat = productionDay.Statistics.DiscontinuousProduction.Mixes(_firstMixStatIndex)

            '            If (_mixStat.TOTAL_MASS > 0) Then

            '                firstMixStatIndex = _firstMixStatIndex

            '                bookMarks.FirstDiscontinuousProductionFormulaName.Text = _mixStat.FORMULA_NAME

            '                bookMarks.FirstDiscontinuousProductionMixName.Text = _mixStat.NAME

            '                bookMarks.FirstDiscontinuousProductionAsphaltName.Text = _mixStat.ASPHALT_STATS.NAME

            '                bookMarks.FirstDiscontinuousProductionRAP.Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

            '                bookMarks.FirstDiscontinuousProductionTotalQuantity.Text = _mixStat.TOTAL_MASS.ToString("N1")

            '                bookMarks.FirstDiscontinuousProductionAsphaltQuantity.Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

            '                totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

            '                For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

            '                    _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

            '                    bookMarks.FirstDiscontinuousProductionFeederQuantity.Select()

            '                    For Each _currentMixFeed As FeedersStatistics In _mixStat.DISCONTINUOUS_FEEDERS_STATS

            '                        If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

            '                            .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

            '                            .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

            '                            Exit For ' Corresponding Feeder was found
            '                        End If

            '                    Next

            '                Next

            '                If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
            '                    mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
            '                End If

            '                nbNonNullMix += 1

            '                Exit For ' First non null mixStat was found
            '            End If

            '        Next

            '        ' Used for alternate white rows
            '        Dim nbRows As Integer = 1

            '        ' Other non null mixstats
            '        For mixStatIndex = productionDay.Statistics.DiscontinuousProduction.Mixes.Count - 1 To firstMixStatIndex + 1 Step -1

            '            _mixStat = productionDay.Statistics.DiscontinuousProduction.Mixes(mixStatIndex)

            '            If (_mixStat.TOTAL_MASS > 0) Then

            '                bookMarks.FirstDiscontinuousProductionFormulaName.Select()

            '                .InsertRowsBelow()
            '                nbRows += 1

            '                ' Formula Name
            '                .Text = _mixStat.FORMULA_NAME

            '                ' Mix Name
            '                moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionMixName)
            '                .Text = _mixStat.NAME

            '                ' Asphalt Name
            '                moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionAsphaltName)
            '                .Text = _mixStat.ASPHALT_STATS.NAME

            '                ' target recycled percentage
            '                moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionRAP)
            '                .Text = If(Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE), "-", _mixStat.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0"))

            '                ' mix quantity
            '                moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionTotalQuantity)
            '                .Text = _mixStat.TOTAL_MASS.ToString("N1")

            '                ' Asphalt Quantity
            '                moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionAsphaltQuantity)
            '                .Text = _mixStat.ASPHALT_STATS.TOTAL_MASS.ToString("N1")

            '                totalAsphaltQuantity += _mixStat.ASPHALT_STATS.TOTAL_MASS

            '                For nonNullFeedIndex = 0 To nonNullFeeds.Count - 1

            '                    _nonNullFeed = nonNullFeeds(nonNullFeedIndex)

            '                    moveSelectionToCellBelow(bookMarks.FirstDiscontinuousProductionFeederQuantity)

            '                    For Each _currentMixFeed As FeedersStatistics In _mixStat.DISCONTINUOUS_FEEDERS_STATS

            '                        If (_nonNullFeed.INDEX = _currentMixFeed.INDEX) Then

            '                            .MoveRight(WdUnits.wdCell, nonNullFeedIndex, WdMovementType.wdMove)

            '                            .Text = _currentMixFeed.TOTAL_MASS.ToString("N1")

            '                            Exit For ' Corresponding Feeder was found
            '                        End If
            '                    Next
            '                Next

            '                If (Not Double.IsNaN(_mixStat.SET_POINT_RECYCLED_PERCENTAGE) AndAlso _mixStat.SET_POINT_RECYCLED_PERCENTAGE > 0) Then
            '                    mixWithRecycledQuantitySum += _mixStat.TOTAL_MASS
            '                End If

            '                nbNonNullMix += 1

            '            End If
            '        Next

            '        ' Alternate white rows and remove borders
            '        For i = 1 To nbRows - 1

            '            bookMarks.FirstDiscontinuousProductionFormulaName.Select()
            '            .MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
            '            .SelectRow()

            '            .Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

            '            If (i Mod 2 = 1) Then
            '                .Shading.BackgroundPatternColor = WdColor.wdColorWhite
            '            End If
            '        Next

            '        bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

            '        bookMarks.DiscontinuousProductionTotalQuantity.Text = productionDay.Statistics.DiscontinuousProduction.Quantity.ToString("N1")
            '        bookMarks.DiscontinuousProductionTotalAsphaltQuantity.Text = totalAsphaltQuantity.ToString("N1")
            '        bookMarks.DiscontinuousProductionMixWithRecycledPercentage.Text = (mixWithRecycledQuantitySum / productionDay.Statistics.DiscontinuousProduction.Quantity * 100).ToString("N0")

            '        bookMarks.DiscontinuousProductionTotalCellsToMerge.Cells.Merge()


            '    Else

            '        bookMarks.DiscontinuousProductionSummarySection.Delete()

            '    End If
            'End With



            '' Creation des entêtes des bennes chaudes

            ' Créeation de la première entête de benne chaude
            Dim tableauProductionDiscontinu As List(Of ArrayList) = dailyReport.getTableauProductionDiscontinu

            If tableauProductionDiscontinu.Count > 4 Then

                Dim ligneSommaireEntete As ArrayList = tableauProductionDiscontinu.Item(EnumDailyReportTableauIndex.ligne_SommaireEntete)
                Dim columnsWidth = bookMarks.FirstDiscontinuousProductionFeederDescription.Columns.Width / ligneSommaireEntete.Count
                If ligneSommaireEntete.Count > 0 Then


                    bookMarks.FirstDiscontinuousProductionFeederDescription.Select()

                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete.Item(0)

                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"


                    bookMarks.FirstDiscontinuousProductionFeederTotalQuantity.Select()
                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                Else

                End If

                ' Création de autres entête de benne chaude
                For feederEnteteIndex = ligneSommaireEntete.Count - 1 To 1 Step -1

                    WordDoc.Bookmarks("FirstDiscontinuousProductionFeederDesc").Range.Select()
                    WordApp.Selection.InsertColumnsRight()

                    'Dim columnsWidth = bookMarks.FirstDiscontinuousProductionFeederDescription.Columns.Width / ligneSommaireEntete.Count
                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete(feederEnteteIndex)

                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"

                    ' Total quantity
                    WordApp.Selection.MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter

                Next

                bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

                '' Céeation des lignes du tableau

                ' Création de la première ligne

                Dim ligneEnrobe As ArrayList

                If tableauProductionDiscontinu.Count > 4 Then

                    ligneEnrobe = tableauProductionDiscontinu.Item(EnumDailyReportTableauIndex.ligne_SommaireFirstLigneEnrobe)

                    bookMarks.FirstDiscontinuousProductionFormulaName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFormule)

                    bookMarks.FirstDiscontinuousProductionMixName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeName)

                    bookMarks.FirstDiscontinuousProductionAsphaltName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeGrade)

                    bookMarks.FirstDiscontinuousProductionRAP.Text = If(Double.IsNaN(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise)), "-", CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise), Double).ToString("N0"))

                    bookMarks.FirstDiscontinuousProductionTotalQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasse), Double).ToString("N1")

                    bookMarks.FirstDiscontinuousProductionAsphaltQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume), Double).ToString("N1")

                    bookMarks.FirstDiscontinuousProductionFeederQuantity.Select()

                    WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse), Double).ToString("N1")

                    For feedIndex = 1 To ligneSommaireEntete.Count - 1

                        WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                        WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse + feedIndex), Double).ToString("N1")
                        'WordApp.Selection.Columns.Last.Width = columnsWidth
                        'WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                    Next

                Else

                End If

                ' Used for alternate white rows
                Dim nbRows As Integer = 1

                ' Other non null mixstats
                For indexLigneEnrobe = tableauProductionDiscontinu.Count - 4 To 2 Step -1

                    bookMarks.FirstDiscontinuousProductionFormulaName.Select()

                    WordApp.Selection.InsertRowsBelow()
                    nbRows += 1

                    ligneEnrobe = tableauProductionDiscontinu.Item(indexLigneEnrobe)

                    WordApp.Selection.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFormule)
                    WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                    WordApp.Selection.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeName)
                    WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                    WordApp.Selection.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeGrade)
                    WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                    WordApp.Selection.Text = If(Double.IsNaN(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise)), "-", CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise), Double).ToString("N0"))
                    WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                    WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasse), Double).ToString("N1")
                    WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                    WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume), Double).ToString("N1")
                    WordApp.Selection.MoveRight(WdUnits.wdCell, 1)

                    'WordApp.Selection.Columns.Last.Width = columnsWidth
                    'WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                    WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse), Double).ToString("N1")

                    For feedIndex = 1 To ligneSommaireEntete.Count - 1

                        WordApp.Selection.MoveRight(WdUnits.wdCell, 1)
                        'WordApp.Selection.Columns.Last.Width = columnsWidth
                        'WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                        WordApp.Selection.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFirstFeederMasse + feedIndex), Double).ToString("N1")
        
                    Next
                Next

                ' Alternate white rows and remove borders
                For i = 1 To nbRows - 1

                    bookMarks.FirstDiscontinuousProductionFormulaName.Select()
                    WordApp.Selection.MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                    WordApp.Selection.SelectRow()

                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If
                Next

                bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

                Dim ligneEnrobeTotalMasse As ArrayList = tableauProductionDiscontinu.Item(tableauProductionDiscontinu.Count - 3)

                bookMarks.DiscontinuousProductionTotalQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasse), Double).ToString("N1")
                bookMarks.DiscontinuousProductionTotalAsphaltQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasseBitume), Double).ToString("N1")

                Dim ligneSommairePourcentageAvecGBR As ArrayList = tableauProductionDiscontinu.Item(tableauProductionDiscontinu.Count - 1)
                bookMarks.DiscontinuousProductionMixWithRecycledPercentage.Text = CType(ligneSommairePourcentageAvecGBR.Item(EnumDailyReportTableauIndex.colonne_SommairePourcentageAvecGBR), Double).ToString("N0")

                bookMarks.DiscontinuousProductionTotalCellsToMerge.Cells.Merge()

            Else

                bookMarks.DiscontinuousProductionSummarySection.Delete()

            End If

            RaiseEvent CurrentProgress(90) ' 90 % Progress

            ' ---------
            ' Comments
            ' ---------

            bookMarks.Comments.Text = "Test rapport a valider avec Martin"

            ' ----------
            ' Signature
            ' ----------

            If (Not dailyReport.getUsineOperator() = FactoryOperator.DEFAULT_OPERATOR) Then

                bookMarks.OperatorName.Text = dailyReport.getUsineOperator().ToString()
            End If

            bookMarks.CurrentDate1.Text = Date.Today.ToString(Me.Formater.FullDateFormat)
            bookMarks.CurrentDate2.Text = Date.Now.ToString(Me.Formater.DateTimeFormat)

            ' -----
            ' SAVE N QUIT
            ' -----
            Dim savePath = Constants.Paths.SUMMARY_REPORTS_ARCHIVES_DIRECTORY & "Rapport Journalier Sommaire - " & XmlSettings.Settings.instance.Usine.PLANT_NAME & New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day).ToString(" - (yyyy-MM-dd)")

            Me.WordDoc.SaveAs2(savePath)
            Dim writableReport As New SummaryDailyReport(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day), savePath & ".docx", False)
            'productionDay.ReportFilesInfo.addReport(writableReport)

            Me.WordDoc.SaveAs2(savePath, WdSaveFormat.wdFormatPDF)
            Dim readOnlyReport As New SummaryDailyReport(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day), savePath & ".pdf", True)
            'productionDay.ReportFilesInfo.addReport(readOnlyReport)

            ProgramController.ReportsPersistence.addDailyReports(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day), writableReport.getFileInfo.FullName, readOnlyReport.getFileInfo.FullName, Nothing)

            Me.killDocumentObjects()

            If (XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_READ_ONLY) Then
                readOnlyReport.open()
            End If

            If (XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_WRITABLE) Then
                writableReport.open()
            End If

            RaiseEvent CurrentProgress(100) ' 100 % Progress

            Return readOnlyReport

        Catch ex As Threading.ThreadAbortException

            Me.disposeOfRessources()

            RaiseEvent ProcessInterrupted(Me, ex)
            Return Nothing
        End Try

        RaiseEvent ProcessComplete(Me)

    End Function

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
