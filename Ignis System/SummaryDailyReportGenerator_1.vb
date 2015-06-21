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
            bookMarks.AA01_HeaderPlantName.Text = XmlSettings.Settings.instance.Usine.PLANT_NAME
            bookMarks.AA02_HeaderPlantID.Text = XmlSettings.Settings.instance.Usine.PLANT_ID


            '*****************************************************************************************************************************************
            '*                                          Section des informations extraites du rapport 
            '*****************************************************************************************************************************************

            ' Date
            bookMarks.CA01_ProductionDayDate.Text = New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day).ToString(Me.Formater.ShortDateFormat)

            ' Add er when first of month

            ajustDateString(New Date(dailyReport.getDebutPeriode.Year, dailyReport.getDebutPeriode.Month, dailyReport.getDebutPeriode.Day), bookMarks.CA01_ProductionDayDate)

            ' --------
            ' TABLE 1
            ' --------

            ' Operation
            Dim tableauHoraire As List(Of ArrayList) = dailyReport.getTableauHoraire

            Dim ligneOpperation As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_Operation)
            bookMarks.CT01_OperationStartTime.Text = CType(ligneOpperation.Item(EnumDailyReportTableauIndex.colonne_OpperationDebut), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.CT01_OperationEndTime.Text = CType(ligneOpperation.Item(EnumDailyReportTableauIndex.colonne_OpperationFin), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.CT01_OperationDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneOpperation.Item(EnumDailyReportTableauIndex.colonne_OpperationDuree), TimeSpan))

            ' Production

            Dim ligneProduction As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_Production)

            bookMarks.CT01_ProductionStartTime.Text = CType(ligneProduction.Item(EnumDailyReportTableauIndex.colonne_ProductionDebut), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.CT01_ProductionEndTime.Text = CType(ligneProduction.Item(EnumDailyReportTableauIndex.colonne_ProductionFin), Date).ToString(Me.Formater.TimeFormat)
            bookMarks.CT01_ProductionDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneProduction.Item(EnumDailyReportTableauIndex.colonne_ProductionDuree), TimeSpan))

            Dim ligneDelaisPauses As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_DelaisPauses)

            bookMarks.CT01_PausesDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDelaisPauses.Item(EnumDailyReportTableauIndex.colonne_PausesDuree), TimeSpan))

            Dim ligneDelaisEntretiens As ArrayList = tableauHoraire.Item(EnumDailyReportTableauIndex.ligne_DelaisEntretiens)

            bookMarks.CT01_MaintenanceDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDelaisEntretiens.Item(EnumDailyReportTableauIndex.colonne_Entretiens), TimeSpan))

            RaiseEvent CurrentProgress(12) ' 12 % Progress

            ' --------
            ' TABLE 2 
            ' --------

            Dim tableauEnrobes As List(Of ArrayList) = dailyReport.getTableauEnrobes

            Dim ligneEnrobe1 As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_Enrobe1)

            If (ligneEnrobe1.Count > 0) Then
                bookMarks.CT02_FirstMixName.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1NoFormule)
                bookMarks.CT02_FirstMixVirginAsphaltConcreteGrade.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1NomEnrobe)
                bookMarks.CT02_FirstMixQuantity.Text = CType(ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1Quantite), Double).ToString("N0")
                bookMarks.CT02_FirstMixProductionRate.Text = CType(ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1Production), Double).ToString("N0")
                bookMarks.CT02_FirstMixProductionMode.Text = ligneEnrobe1.Item(EnumDailyReportTableauIndex.colonne_Enrobe1ProductionMode)
            Else
                bookMarks.CT02_FirstMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_FirstMixVirginAsphaltConcreteGrade.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_FirstMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_FirstMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_FirstMixProductionMode.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneEnrobe2 As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_Enrobe2)

            If (ligneEnrobe2.Count > 0) Then

                bookMarks.CT02_SecondMixName.Text = ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2NoFormule)
                bookMarks.CT02_SecondMixVirginAsphaltConcreteGrade.Text = ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2NomEnrobe)
                bookMarks.CT02_SecondMixQuantity.Text = CType(ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2Quantite), Double).ToString("N0")
                bookMarks.CT02_SecondMixProductionRate.Text = CType(ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2Production), Double).ToString("N0")
                bookMarks.CT02_SecondMixProductionMode.Text = ligneEnrobe2.Item(EnumDailyReportTableauIndex.colonne_Enrobe2ProductionMode)

            Else
                bookMarks.CT02_SecondMixName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_SecondMixVirginAsphaltConcreteGrade.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_SecondMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_SecondMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_SecondMixProductionMode.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneEnrobe3 As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_Enrobe3)

            If (ligneEnrobe3.Count > 0) Then

                bookMarks.CT02_ThirdMixName.Text = ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3NoFormule)
                bookMarks.CT02_ThirdMixVirginAsphaltConcreteGrade.Text = ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3NomEnrobe)
                bookMarks.CT02_ThirdMixQuantity.Text = CType(ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3Quantite), Double).ToString("N0")
                bookMarks.CT02_ThirdMixProductionRate.Text = CType(ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3Production), Double).ToString("N0")
                bookMarks.CT02_ThirdMixProductionMode.Text = ligneEnrobe3.Item(EnumDailyReportTableauIndex.colonne_Enrobe3ProductionMode)

            Else
                bookMarks.CT02_ThirdMixName.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_ThirdMixVirginAsphaltConcreteGrade.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_ThirdMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_ThirdMixProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_ThirdMixProductionMode.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneEnrobeAutres As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_EnrobeAutres)

            If (ligneEnrobeAutres.Count > 0) Then

                bookMarks.CT02_OtherMixesNumberOfMixes.Text = ligneEnrobeAutres.Item(EnumDailyReportTableauIndex.colonne_EnrobeAutreNombre)
                bookMarks.CT02_OtherMixesQuantity.Text = CType(ligneEnrobeAutres.Item(EnumDailyReportTableauIndex.colonne_EnrobeAutreQuantite), Double).ToString("N0")
                bookMarks.CT02_OtherMixesProductionRate.Text = CType(ligneEnrobeAutres.Item(EnumDailyReportTableauIndex.colonne_EnrobeAutreProduction), Double).ToString("N0")
                bookMarks.CT02_OtherMixesProductionMode.Text = Me.Formater.InvalidValueCharacter
            Else
                bookMarks.CT02_OtherMixesNumberOfMixes.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_OtherMixesQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_OtherMixesProductionRate.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_OtherMixesProductionMode.Text = Me.Formater.InvalidValueCharacter
            End If


            Dim ligneQuantiteTotaleProduite As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotaleProduite)

            bookMarks.CT02_TotalMixQuantity.Text = CType(ligneQuantiteTotaleProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleProduiteQuantite), Double).ToString("N0")
            bookMarks.CT02_TotalMixProductionRate.Text = CType(ligneQuantiteTotaleProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleProduiteProduction), Double).ToString("N0")


            Dim ligneQuantiteEnSiloDebut As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteEnSiloDebut)


            If (Double.IsNaN(ligneQuantiteEnSiloDebut.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloDebutQuantite)) Or Double.IsNegativeInfinity(ligneQuantiteEnSiloDebut.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloDebutQuantite))) Then

                bookMarks.CT02_SiloStartQuantity.Text = Me.Formater.InvalidValueCharacter
            Else
                bookMarks.CT02_SiloStartQuantity.Text = ligneQuantiteEnSiloDebut.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloDebutQuantite)

            End If


            ' Silo at start


            RaiseEvent CurrentProgress(24) ' 24 % Progress

            Dim ligneQuantiteEnSiloFin As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteEnSiloFin)

            ' Silo at end

            If (Double.IsNaN(ligneQuantiteEnSiloFin.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloFinQuantite)) Or Double.IsNegativeInfinity(ligneQuantiteEnSiloFin.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloFinQuantite))) Then

                bookMarks.CT02_SiloEndQuantity.Text = Me.Formater.InvalidValueCharacter
            Else
                bookMarks.CT02_SiloEndQuantity.Text = ligneQuantiteEnSiloFin.Item(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloFinQuantite)

            End If

            Dim ligneQuantiteTotaleVendable As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotaleVendable)
            ' Salable qty
            bookMarks.CT02_SaleableQuantity.Text = CType(ligneQuantiteTotaleVendable.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendableQuantite), Double).ToString("N0")




            Dim ligneRejetsEnrobes As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_RejetsEnrobes)
            ' Rejected mix

            If (Double.IsNaN(CType(ligneRejetsEnrobes.Item(EnumDailyReportTableauIndex.colonne_RejetsEnrobesQuantite), Double)) Or Double.IsNegativeInfinity(CType(ligneRejetsEnrobes.Item(EnumDailyReportTableauIndex.colonne_RejetsEnrobesQuantite), Double))) Then

                bookMarks.CT02_RejectedMixQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_RejectedMixPercentage.Text = Me.Formater.InvalidValueCharacter
            Else
                bookMarks.CT02_RejectedMixQuantity.Text = CType(ligneRejetsEnrobes.Item(EnumDailyReportTableauIndex.colonne_RejetsEnrobesQuantite), Double).ToString("N0")
                bookMarks.CT02_RejectedMixPercentage.Text = CType(ligneRejetsEnrobes.Item(EnumDailyReportTableauIndex.colonne_RejetsEnrobesPourcentageRejet), Double).ToString("N1")
            End If

            Dim ligneQuantiteTotalePayable As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotalePayable)

            ' Payable qty
            bookMarks.CT02_PayableQuantity.Text = CType(ligneQuantiteTotalePayable.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotalePayableQuantite), Double).ToString("N0")

            Dim ligneQuantiteTotaleVendue As ArrayList = tableauEnrobes.Item(EnumDailyReportTableauIndex.ligne_QuantiteTotaleVendue)

            ' Sold (weighted) qty

            If (Double.IsNaN(CType(ligneQuantiteTotaleVendue.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendueQuantite), Double)) Or Double.IsNegativeInfinity(CType(ligneQuantiteTotaleVendue.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendueQuantite), Double))) Then

                bookMarks.CT02_SoldQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.CT02_SoldQuantityDifferencePercentage.Text = Me.Formater.InvalidValueCharacter
            Else
                bookMarks.CT02_SoldQuantity.Text = CType(ligneQuantiteTotaleVendue.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendueQuantite), Double).ToString("N0")
                bookMarks.CT02_SoldQuantityDifferencePercentage.Text = CType(ligneQuantiteTotaleVendue.Item(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVenduePourcentageEcart), Double).ToString("N1")
            End If




            ' --------------------
            ' Graphic 1 et 2 #refactor generate before graphs and store them
            ' --------------------
            XYScatterGraphic.pointFormatList_asphalt = New PointFormatList
            XYScatterGraphic.pointFormatList_mix = New PointFormatList

            '' Graphic 1

            Dim accumulateGraphicData As ArrayList
            Dim isHybrid As Boolean = XmlSettings.Settings.instance.Usine.DataFiles.CSV.ACTIVE AndAlso XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE

            Dim accumulatedMass As New AccumulatedMassGraphic(dailyReport.getDebutPeriode, dailyReport.getFinPeriode, isHybrid)

            accumulateGraphicData = dailyReport.getAccumulatedMassGraphicDataDiscontinu()

            Dim cycleListDiscontinuDateTime As List(Of Date) = accumulateGraphicData.Item(0)
            Dim cycleListDiscontinuMass As List(Of Double) = accumulateGraphicData.Item(1)
            Dim cycleListDiscontinuProductionSpeed As List(Of Double) = accumulateGraphicData.Item(2)

            accumulatedMass.setGraphicData(cycleListDiscontinuDateTime, cycleListDiscontinuMass, cycleListDiscontinuProductionSpeed)
            accumulatedMass.toggleMarkerColor()
            accumulateGraphicData = dailyReport.getAccumulatedMassGraphicDataContinu()

            Dim cycleListContinuDateTime As List(Of Date) = accumulateGraphicData.Item(0)
            Dim cycleListContinuMass As List(Of Double) = accumulateGraphicData.Item(1)
            Dim cycleListContinuProductionSpeed As List(Of Double) = accumulateGraphicData.Item(2)

            accumulatedMass.setGraphicData(cycleListContinuDateTime, cycleListContinuMass, cycleListContinuProductionSpeed)
            accumulatedMass.save()

            '' Graphic 2

            Dim productionSpeedData As ArrayList
            Dim productionSpeed As New ProductionSpeedGraphic(dailyReport.getDebutPeriode, dailyReport.getFinPeriode)

            productionSpeedData = dailyReport.getProductionSpeedGraphicDataDiscontinu()

            cycleListDiscontinuDateTime = productionSpeedData.Item(0)
            cycleListDiscontinuProductionSpeed = productionSpeedData.Item(1)

            productionSpeed.setGraphicData(cycleListDiscontinuDateTime, cycleListDiscontinuProductionSpeed)
            productionSpeed.toggleMarkerColor()

            productionSpeedData = dailyReport.getProductionSpeedGraphicDataContinu()

            cycleListDiscontinuDateTime = productionSpeedData.Item(0)
            cycleListDiscontinuProductionSpeed = productionSpeedData.Item(1)

            productionSpeed.setGraphicData(cycleListContinuDateTime, cycleListContinuProductionSpeed)
            productionSpeed.save()

            Dim g1 = bookMarks.CG01_ProductionQuantityGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC, False, True)
            Dim g2 = bookMarks.CG02_ProductionRateGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_SPEED_GRAPHIC, False, True)

            g1.Width = bookMarks.CG01_ProductionQuantityGraphic.Cells(1).Width
            g2.Width = bookMarks.CG01_ProductionQuantityGraphic.Cells(1).Width
            ' --------------------
            ' PRODUCTION ET DELAIS
            ' --------------------

            Dim tableauModeDeProduction As List(Of ArrayList) = dailyReport.getTableauModeProduction

            ' Durée
            Dim ligneDuree As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_Duree)
            bookMarks.DT01_ContinuousDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDuree.Item(EnumDailyReportTableauIndex.colonne_DureeContinu), TimeSpan))
            bookMarks.DT01_DiscontinuousDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDuree.Item(EnumDailyReportTableauIndex.colonne_DureeDiscontinu), TimeSpan))
            bookMarks.DT01_DelaysDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDuree.Item(EnumDailyReportTableauIndex.colonne_DureeDelais), TimeSpan))

            'Pourcentage du temps
            Dim lignePourcentageDuTemps As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_PourcentageDuTemps)
            bookMarks.DT01_ContinuousPercentage.Text = CType(lignePourcentageDuTemps.Item(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsContinu), Double).ToString("N0")
            bookMarks.DT01_DiscontinuousPercentage.Text = CType(lignePourcentageDuTemps.Item(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsDiscontinu), Double).ToString("N0")
            bookMarks.DT01_DelaysPercentage.Text = CType(lignePourcentageDuTemps.Item(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsDelais), Double).ToString("N0")

            'Nombre de changement de mélanges / délais
            Dim ligneNombreDeChangements As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_NombreDeChangements)
            bookMarks.DT01_ContinuousMixChange.Text = ligneNombreDeChangements.Item(EnumDailyReportTableauIndex.colonne_NombreDeChangementsContinu)
            bookMarks.DT01_DisontinuousMixChange.Text = ligneNombreDeChangements.Item(EnumDailyReportTableauIndex.colonne_NombreDeChangementsDiscontinu)
            bookMarks.DT01_DelaysNumber.Text = ligneNombreDeChangements.Item(EnumDailyReportTableauIndex.colonne_NombreDeChangementsDelais)

            'Quantite produite
            Dim ligneQuantiteProduite As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_QuantiteProduite)
            bookMarks.DT01_ContinuousQuantity.Text = CType(ligneQuantiteProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteProduiteContinu), Double).ToString("N0")
            bookMarks.DT01_DiscontinuousQuantity.Text = CType(ligneQuantiteProduite.Item(EnumDailyReportTableauIndex.colonne_QuantiteProduiteDiscontinu), Double).ToString("N0")

            'Taux de production
            Dim ligneTauxDeProduction As ArrayList = tableauModeDeProduction.Item(EnumDailyReportTableauIndex.ligne_TauxDeProduction)
            bookMarks.DT01_ContinuousProductionRate.Text = CType(ligneTauxDeProduction.Item(EnumDailyReportTableauIndex.colonne_TauxDeProductionContinu), Double).ToString("N0")
            bookMarks.DT01_DiscontinuousProductionRate.Text = CType(ligneTauxDeProduction.Item(EnumDailyReportTableauIndex.colonne_TauxDeProductionDiscontinu), Double).ToString("N0")

            RaiseEvent CurrentProgress(36) ' 36 % Progress

            ' -------------------
            ' TEMPS DE PRODUCTION
            ' -------------------

            Dim tableauTempsDeProduction As List(Of ArrayList) = dailyReport.getTableauTempsDeProduction

            ' Temps total d’opération 
            Dim ligneTempsTotalOperations As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_TempsTotalOperations)

            bookMarks.DT02_TotalOperationDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneTempsTotalOperations.Item(EnumDailyReportTableauIndex.colonne_TempsTotalOperationsDuree), TimeSpan))

            ' Temps net d’opération 
            Dim ligneTempsNetOperations As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_TempsNetOperations)
            bookMarks.DT02_NetOperationDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneTempsNetOperations.Item(EnumDailyReportTableauIndex.colonne_TempsNetOperationsDuree), TimeSpan))

            ' Production nette 
            Dim ligneProductionNette As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_ProductionNette)
            bookMarks.DT02_NetProductionDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneProductionNette.Item(EnumDailyReportTableauIndex.colonne_ProductionNetteDuree), TimeSpan))

            'Production efficace 
            Dim ligneProductionEfficace As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_ProductionEfficace)
            bookMarks.DT02_EffectiveProductionDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneProductionEfficace.Item(EnumDailyReportTableauIndex.colonne_ProductionEfficaceDuree), TimeSpan))

            ' Production efficace interne 
            Dim ligneProductionEfficaceInterne As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_ProductionEfficaceInterne)
            bookMarks.DT02_EffectiveInternalDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneProductionEfficaceInterne.Item(EnumDailyReportTableauIndex.colonne_ProductionEfficaceInterneDuree), TimeSpan))

            Dim ligneDelais As ArrayList = tableauTempsDeProduction.Item(EnumDailyReportTableauIndex.ligne_Delais)
            bookMarks.DT02_DelaysDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDelais.Item(EnumDailyReportTableauIndex.colonne_DelaisDuree), TimeSpan))

            ' -------
            ' DELAIS
            ' -------

            Dim tableauDelais As List(Of ArrayList) = dailyReport.getTableauDelais

            Dim ligneNombreDeBris As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_NombreDeBris)
            bookMarks.DT03_BreakageNumber.Text = ligneNombreDeBris.Item(EnumDailyReportTableauIndex.colonne_NombreDeBris)

            ' Disponibilité (%) = Production efficace interne / Production nette * 100
            Dim ligneDisponibilite As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_Disponibilite)
            bookMarks.DT03_DisponibilityPercentage.Text = CType(ligneDisponibilite.Item(EnumDailyReportTableauIndex.colonne_Disponibilite), Double).ToString("N0")

            ' Utilisation (%) = Production efficace / Temps total d’opération
            Dim ligneUtilisation As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_Utilisation)
            bookMarks.DT03_UtilisationPercentage.Text = CType(ligneUtilisation.Item(EnumDailyReportTableauIndex.colonne_Utilisation), Double).ToString("N0")

            If ligneNombreDeBris.Item(EnumDailyReportTableauIndex.colonne_NombreDeBris) > 0 Then
                Dim ligneTempsEntrePannes As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_TempsEntrePannes)
                bookMarks.DT03_TempsEntrePannes.Text = ReportFormater.FormatTimeSpan(CType(ligneTempsEntrePannes.Item(EnumDailyReportTableauIndex.colonne_TempsEntrePannes), TimeSpan))

                Dim ligneTempsPourReparer As ArrayList = tableauDelais.Item(EnumDailyReportTableauIndex.ligne_TempsPourReparer)
                bookMarks.DT03_TempsPourReparer.Text = ReportFormater.FormatTimeSpan(CType(ligneTempsPourReparer.Item(EnumDailyReportTableauIndex.colonne_TempsPourReparer), TimeSpan))
            Else

                bookMarks.DT03_TempsEntrePannes.Text = Me.Formater.InvalidValueCharacter
                bookMarks.DT03_TempsPourReparer.Text = Me.Formater.InvalidValueCharacter
            End If

            ' ----------------------
            ' DISTRIBUTION GRAPHICS
            ' ----------------------

            Dim productionDistributionGraphicData As List(Of TimeSpan) = dailyReport.getProductionDistributionGraphicData()

            Dim pdg = New DG01_ProductionDistributionGraphic(productionDistributionGraphicData(0), productionDistributionGraphicData(1), productionDistributionGraphicData(2), productionDistributionGraphicData(3))

            pdg.save()

            Dim delaysDistributionGraphicData As List(Of TimeSpan) = dailyReport.getDelaysDistributionGraphicData()

            Dim ddg = New DG02_DelaysDistributionGraphic(delaysDistributionGraphicData(0), delaysDistributionGraphicData(1), delaysDistributionGraphicData(2), delaysDistributionGraphicData(3))

            ddg.save()

            Dim g3 = bookMarks.DG01_ProductionDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_DISTRIBUTION_GRAPHIC, False, True)

            g3.Width = bookMarks.DG01_ProductionDistributionGraphic.Cells(1).Width

            Dim g4 = bookMarks.DG02_DelaysDistributionGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.DELAYS_DISTRIBUTION_GRAPHIC, False, True)

            g4.Width = bookMarks.DG02_DelaysDistributionGraphic.Cells(1).Width

            ' -----------------
            ' BITUMES CONSOMMÉ
            ' -----------------

            Dim tableauBitumeConsommes As List(Of ArrayList) = dailyReport.getTableauBitumeConsommes

            If tableauBitumeConsommes.Count > 1 Then

                Dim ligneVirginAsphaltConcrete As ArrayList

                ligneVirginAsphaltConcrete = tableauBitumeConsommes.Item(EnumDailyReportTableauIndex.ligne_VirginAsphaltConcrete)

                bookMarks.ET01_FirstAsphaltVirginConcreteTankId.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteReservoir)
                bookMarks.ET01_FirstAsphaltVirginConcreteGrade.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteGrade)
                bookMarks.ET01_FirstAsphaltVirginConcreteQuantity.Text = CType(ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteQuantite), Double).ToString("N1")

                For i = tableauBitumeConsommes.Count - 2 To 1 Step -1

                    bookMarks.ET01_FirstAsphaltVirginConcreteTankId.Select()
                    WordApp.Selection.InsertRowsBelow(1)

                    ligneVirginAsphaltConcrete = tableauBitumeConsommes.Item(i)

                    ' Number (cursor already in position
                    WordApp.Selection.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteReservoir)

                    ' Name
                    moveSelectionToCellBelow(bookMarks.ET01_FirstAsphaltVirginConcreteGrade)
                    WordApp.Selection.Text = ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteGrade)

                    ' Quantity
                    moveSelectionToCellBelow(bookMarks.ET01_FirstAsphaltVirginConcreteQuantity)
                    WordApp.Selection.Text = CType(ligneVirginAsphaltConcrete.Item(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteQuantite), Double).ToString("N1")

                    WordApp.Selection.SelectRow()
                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone
                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If

                Next

                Dim ligneTotalBitumeConsommes As ArrayList = tableauBitumeConsommes.Item(tableauBitumeConsommes.Count - 1)
                bookMarks.ET01_TotalAsphaltVirginConcreteQuantity.Text = CType(ligneTotalBitumeConsommes.Item(EnumDailyReportTableauIndex.colonne_TotalBitumeConsommesQuantite), Double).ToString("N1")
            Else

                bookMarks.ET01_FirstAsphaltVirginConcreteTankId.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ET01_FirstAsphaltVirginConcreteGrade.Text = Me.Formater.InvalidValueCharacter
                bookMarks.ET01_FirstAsphaltVirginConcreteQuantity.Text = Me.Formater.InvalidValueCharacter

            End If


            RaiseEvent CurrentProgress(48) ' 48 % Progress

            ' -----------------------------------
            ' ECART PAR RAPPORT A LA VALEUR VISÉE
            ' -----------------------------------

            Dim tableauEcartValeurVisee As List(Of ArrayList) = dailyReport.getTableauEcartValeurVisee

            Dim ligneBitumeEcart As ArrayList = tableauEcartValeurVisee.Item(EnumDailyReportTableauIndex.ligne_BitumeEcart)
            Dim ligneTemperatureEcart As ArrayList = tableauEcartValeurVisee.Item(EnumDailyReportTableauIndex.ligne_TemperatureEcart)

            If Double.IsNaN(ligneBitumeEcart.Item(EnumDailyReportTableauIndex.colonne_BitumeEcartPourcentage)) Then

                bookMarks.ET02_AverageTemperatureDifference.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.ET02_AverageTemperatureDifference.Text = CType(ligneBitumeEcart.Item(EnumDailyReportTableauIndex.colonne_BitumeEcartPourcentage), Double).ToString("N1")
            End If

            If Double.IsNaN(ligneTemperatureEcart.Item(EnumDailyReportTableauIndex.colonne_TemperatureEcart)) Then

                bookMarks.ET02_VirginAsphaltConcreteDifferencePerc.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.ET02_VirginAsphaltConcreteDifferencePerc.Text = CType(ligneTemperatureEcart.Item(EnumDailyReportTableauIndex.colonne_TemperatureEcart), Double).ToString("N3")
            End If


            ' -----------------------------------
            ' VARIATION EN PRODUCTION
            ' -----------------------------------
            Dim tableauVariationEnProduction As List(Of ArrayList) = dailyReport.getTableauVariationEnProduction

            Dim ligneVariationTemperature As ArrayList = tableauVariationEnProduction.Item(EnumDailyReportTableauIndex.ligne_VariationTemperature)

            If Double.IsNaN(ligneVariationTemperature.Item(EnumDailyReportTableauIndex.colonne_VariationTemperature)) Then

                bookMarks.ET03_TemperatureVariation.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.ET03_TemperatureVariation.Text = CType(ligneVariationTemperature.Item(EnumDailyReportTableauIndex.colonne_VariationTemperature), Double).ToString("N1")
            End If
           
            ' -----------------------------------
            ' TAUX DE VALEURS ABERRANTES
            ' -----------------------------------
            Dim tableauValeursAberrantes As List(Of ArrayList) = dailyReport.getTableauValeursAberrantes

            Dim lignePourcentageBitume As ArrayList = tableauValeursAberrantes.Item(EnumDailyReportTableauIndex.ligne_PourcentageBitume)
            Dim lignePourcentageTemperature As ArrayList = tableauValeursAberrantes.Item(EnumDailyReportTableauIndex.ligne_PourcentageTemperature)

            If Double.IsNaN(lignePourcentageBitume.Item(EnumDailyReportTableauIndex.colonne_PourcentageBitume)) Then

                bookMarks.ET04_TemperatureAberrancePercentage.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.ET04_TemperatureAberrancePercentage.Text = CType(lignePourcentageBitume.Item(EnumDailyReportTableauIndex.colonne_PourcentageBitume), Double).ToString("N1")
            End If

            If Double.IsNaN(lignePourcentageTemperature.Item(EnumDailyReportTableauIndex.colonne_PourcentageTemperature)) Then

                bookMarks.ET04_VirginAsphaltConcreteAberrancePerc.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.ET04_VirginAsphaltConcreteAberrancePerc.Text = CType(lignePourcentageTemperature.Item(EnumDailyReportTableauIndex.colonne_PourcentageTemperature), Double).ToString("N1")
            End If

            ' -------------------------------
            ' Temperature difference graphic
            ' -------------------------------

            Dim mixTemperatureVariationGraphicData As ArrayList = dailyReport.getMixTemperatureVariationGraphicData

            Dim mixTemperatureVariation As New MixTemperatureVariationGraphic(dailyReport.getDebutPeriode, dailyReport.getFinPeriode)

            mixTemperatureVariation.setGraphicData(mixTemperatureVariationGraphicData(0), mixTemperatureVariationGraphicData(1), mixTemperatureVariationGraphicData(2), mixTemperatureVariationGraphicData(3), mixTemperatureVariationGraphicData(4))

            mixTemperatureVariation.save()

            Dim g5 = bookMarks.EG01_TemperatureVariationGraphic.InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC, False, True)

            g5.Width = bookMarks.CG01_ProductionQuantityGraphic.Cells(1).Width


            ' -----------------------------------
            ' CARBURANTS
            ' -----------------------------------

            Dim tableauCarburants As List(Of ArrayList) = dailyReport.getTableauCarburants

            Dim ligneCarburantPrincipal As ArrayList = tableauCarburants.Item(EnumDailyReportTableauIndex.ligne_CarburantPrincipal)
            Dim ligneCarburantGazNatutel As ArrayList = tableauCarburants.Item(EnumDailyReportTableauIndex.ligne_CarburantGazNatutel)

            bookMarks.FT01_FirstFuelName.Text = ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_NomCarburant)
            bookMarks.FT01_SecondFuelName.Text = ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_NomCarburant)

            If Double.IsNaN(ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_QuantiteConsomme)) Then

                bookMarks.FT01_FirstFuelQuantity.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.FT01_FirstFuelQuantity.Text = ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_QuantiteConsomme)
            End If

            If Double.IsNaN(ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_TauxDeConsommation)) Then

                bookMarks.FT01_FirstFuelConsumptionRate.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.FT01_FirstFuelConsumptionRate.Text = CType(ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_TauxDeConsommation), Double).ToString("N1") & " " & ligneCarburantPrincipal.Item(EnumDailyReportTableauIndex.colonne_UniteTauxDeConsommation)
            End If


            If Double.IsNaN(ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_QuantiteConsomme)) Then

                bookMarks.FT01_SecondFuelQuantity.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.FT01_SecondFuelQuantity.Text = ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_QuantiteConsomme)
            End If

            If Double.IsNaN(ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_TauxDeConsommation)) Then

                bookMarks.FT01_SecondFuelConsumptionRate.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.FT01_SecondFuelConsumptionRate.Text = CType(ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_TauxDeConsommation), Double).ToString("N1") & " " & ligneCarburantGazNatutel.Item(EnumDailyReportTableauIndex.colonne_UniteTauxDeConsommation)
            End If


            ' -------
            ' REJETS
            ' -------
            Dim tableauRejets As List(Of ArrayList) = dailyReport.getTableauRejets

            Dim ligneQuantiteRejete As ArrayList = tableauRejets.Item(EnumDailyReportTableauIndex.ligne_QuantiteRejete)
            Dim ligneTauxDeRejet As ArrayList = tableauRejets.Item(EnumDailyReportTableauIndex.ligne_TauxDeRejet)

            If Double.IsNaN(ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetGranulats)) Then

                bookMarks.GT01_RejectedAggregatesQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.GT01_RejectedAggregatesPercentage.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.GT01_RejectedAggregatesQuantity.Text = ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetGranulats)
                bookMarks.GT01_RejectedAggregatesPercentage.Text = CType(ligneTauxDeRejet.Item(EnumDailyReportTableauIndex.colonne_RejetGranulats), Double).ToString("N1")
            End If

            If Double.IsNaN(ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetFiller)) Then

                bookMarks.GT01_RejectedFillerQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.GT01_RejectedFillerPercentage.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.GT01_RejectedFillerQuantity.Text = ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetFiller)
                bookMarks.GT01_RejectedFillerPercentage.Text = CType(ligneTauxDeRejet.Item(EnumDailyReportTableauIndex.colonne_RejetFiller), Double).ToString("N1")
            End If

            If Double.IsNaN(ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetGBR)) Then

                bookMarks.GT01_RejectedRecycledQuantity.Text = Me.Formater.InvalidValueCharacter
                bookMarks.GT01_RejectedRecycledPercentage.Text = Me.Formater.InvalidValueCharacter
            Else

                bookMarks.GT01_RejectedRecycledQuantity.Text = ligneQuantiteRejete.Item(EnumDailyReportTableauIndex.colonne_RejetGBR)
                bookMarks.GT01_RejectedRecycledPercentage.Text = CType(ligneTauxDeRejet.Item(EnumDailyReportTableauIndex.colonne_RejetGBR), Double).ToString("N1")
            End If

            RaiseEvent CurrentProgress(72) ' 72 % Progress

            ' Using the with statement is faster by a couple seconds with the way I fill the next tables

            ' ---------------------
            ' DELAYS SUMMARY TABLE
            ' ---------------------

            Dim tableauDelay As List(Of ArrayList) = dailyReport.getTableauDelay



            Dim ligneDelay As ArrayList
            Dim color As Color
            If (tableauDelay.Count > 2) Then

                ' All delays except first, starting from last
                For i = tableauDelay.Count - 3 To 1 Step -1

                    ligneDelay = tableauDelay.Item(i)

                    ' Add new row
                    bookMarks.HT01_FirstDelayStartTime.Select()
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

                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayEndTime)
                    WordApp.Selection.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableFin), Date).ToString(Me.Formater.TimeFormat)

                    ' Duration
                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayDuration)
                    WordApp.Selection.Text = ReportFormater.FormatTimeSpan(CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDuree), TimeSpan))

                    ' Select cell for delay code
                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayCode)

                    ' Delay Code
                    WordApp.Selection.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableName)

                    color = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisColor)

                    WordApp.Selection.Shading.BackgroundPatternColor = RGB(color.R, color.G, color.B)

                    ' Delay code description
                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayDescription)
                    WordApp.Selection.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDescription)

                    ' Delay justification
                    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayComments)
                    WordApp.Selection.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableCommentaire)


                    'If (_delay.IsUnknown) Then

                    '    ' Delay Code (unknown)
                    '    WordApp.Selection.Text = Me.Formater.UnknownValueCharacter
                    '    WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite

                    '    ' Delay justification
                    '    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayComments)
                    '    WordApp.Selection.Text = _delay.Justification

                    'ElseIf (IsNothing(_delay.Code)) Then

                    '    ' Delay Code
                    '    WordApp.Selection.Text = "-"
                    '    WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite

                    '    ' Delay code description
                    '    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayDescription)
                    '    WordApp.Selection.Text = "-"

                    '    ' Delay justification
                    '    moveSelectionToCellBelow(bookMarks.HT01_FirstDelayComments)
                    '    WordApp.Selection.Text = "-"

                    'Else

                    'End If

                Next

                ligneDelay = tableauDelay.Item(0)

                ' First delay
                bookMarks.HT01_FirstDelayStartTime.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDebut), Date).ToString(Me.Formater.TimeFormat)
                bookMarks.HT01_FirstDelayEndTime.Text = CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableFin), Date).ToString(Me.Formater.TimeFormat)
                bookMarks.HT01_FirstDelayDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDuree), TimeSpan))

                color = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisColor)

                bookMarks.HT01_FirstDelayCode.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableName)
                bookMarks.HT01_FirstDelayCode.Shading.BackgroundPatternColor = RGB(color.R, color.G, color.B)
                bookMarks.HT01_FirstDelayDescription.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDescription)
                bookMarks.HT01_FirstDelayComments.Text = ligneDelay.Item(EnumDailyReportTableauIndex.colonne_DelaisJustifiableCommentaire)

                'End If

            Else

                bookMarks.HT01_FirstDelayStartTime.Select()
                WordApp.Selection.Rows.Delete()

            End If

            Dim ligneDelayNonJustifiable As ArrayList = tableauDelay.Item(tableauDelay.Count - 2)

            bookMarks.HT01_MinimalDurationForJustification.Text = ligneDelayNonJustifiable.Item(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableLimite)
            bookMarks.HT01_DelaysNumberUnderMinimalDuration.Text = ligneDelayNonJustifiable.Item(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableNombre)
            bookMarks.HT01_DelaysUnderMinimalTimeDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDelayNonJustifiable.Item(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableDuree), TimeSpan))


            Dim ligneDelayTotal As ArrayList = tableauDelay.Item(tableauDelay.Count - 1)
            bookMarks.HT01_DelaysDuration.Text = ReportFormater.FormatTimeSpan(CType(ligneDelayTotal.Item(EnumDailyReportTableauIndex.colonne_DelaisTotalDuree), TimeSpan))

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

                Dim columnsWidth = bookMarks.JT01_FirstContinuousFeederDescription.Columns.Width / ligneSommaireEntete.Count
                If ligneSommaireEntete.Count > 0 Then

                    bookMarks.JT01_FirstContinuousFeederDescription.Select()

                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete.Item(0)

                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"


                    bookMarks.JT01_ContinuousFeederTotalQuantity.Select()
                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                Else

                End If

                ' Création de autres entête de benne chaude
                For feederEnteteIndex = ligneSommaireEntete.Count - 1 To 1 Step -1

                    WordDoc.Bookmarks("JT01_FirstContinuousFeederDescription").Range.Select()
                    WordApp.Selection.InsertColumnsRight()

                    'Dim columnsWidth = bookMarks.JT01_FirstContinuousFeederDescription.Columns.Width / ligneSommaireEntete.Count
                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete(feederEnteteIndex)

                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"

                    ' Total quantity
                    WordApp.Selection.MoveDown(WdUnits.wdLine, 2, WdMovementType.wdMove)
                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter

                Next

                bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

                '' Céeation des lignes du tableau

                ' Création de la première ligne

                Dim ligneEnrobe As ArrayList

                If tableauProductionContinu.Count > 4 Then

                    ligneEnrobe = tableauProductionContinu.Item(EnumDailyReportTableauIndex.ligne_SommaireFirstLigneEnrobe)

                    bookMarks.JT01_FirstContinuousMixNumber.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFormule)

                    bookMarks.JT01_FirstContinuousMixName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeName)

                    bookMarks.JT01_FirstContinuousVirginACGrade.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeGrade)

                    bookMarks.JT01_FirstContinuousRAPPercentage.Text = If(Double.IsNaN(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise)), "-", CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise), Double).ToString("N0"))

                    bookMarks.JT01_FirstContinuousQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasse), Double).ToString("N1")

                    bookMarks.JT01_FirstContinuousVirginACQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume), Double).ToString("N1")

                    bookMarks.JT01_FirstContinuousFeederQuantity.Select()

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
                For indexLigneEnrobe = tableauProductionContinu.Count - 4 To 2 Step -1

                    bookMarks.JT01_FirstContinuousMixNumber.Select()

                    WordApp.Selection.InsertRowsBelow()
                    nbRows += 1

                    ligneEnrobe = tableauProductionContinu.Item(indexLigneEnrobe)

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

                    bookMarks.JT01_FirstContinuousMixNumber.Select()
                    WordApp.Selection.MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                    WordApp.Selection.SelectRow()

                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If
                Next

                bookMarks.reinitializeContinuousProductionSummaryBookMarks(WordDoc)

                Dim ligneEnrobeTotalMasse As ArrayList = tableauProductionContinu.Item(tableauProductionContinu.Count - 3)

                bookMarks.JT01_ContinuousTotalQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasse), Double).ToString("N1")
                bookMarks.JT01_ContinuousTotalVirginACQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasseBitume), Double).ToString("N1")

                Dim ligneSommairePourcentageAvecGBR As ArrayList = tableauProductionContinu.Item(tableauProductionContinu.Count - 2)
                bookMarks.JT01_ContinuousWithRAPPercentage.Text = CType(ligneSommairePourcentageAvecGBR.Item(EnumDailyReportTableauIndex.colonne_SommairePourcentageAvecGBR), Double).ToString("N0")

                Dim ligneSommairePourcentageDeGBR As ArrayList = tableauProductionContinu.Item(tableauProductionContinu.Count - 1)
                bookMarks.JT01_ContinuousOfRAPPercentage.Text = CType(ligneSommairePourcentageDeGBR.Item(EnumDailyReportTableauIndex.colonne_SommairePourcentageDeGBR), Double).ToString("N0")

                bookMarks.JT01_ContinuousTotalCellsToMerge.Cells.Merge()

            Else

                bookMarks.JA01_ContinuousProductionSummarySection.Delete()

            End If

            RaiseEvent CurrentProgress(85) ' 85 % Progress


            '' Creation des entêtes des bennes chaudes

            ' Créeation de la première entête de benne chaude
            Dim tableauProductionDiscontinu As List(Of ArrayList) = dailyReport.getTableauProductionDiscontinu

            If tableauProductionDiscontinu.Count > 4 Then

                Dim ligneSommaireEntete As ArrayList = tableauProductionDiscontinu.Item(EnumDailyReportTableauIndex.ligne_SommaireEntete)
                Dim columnsWidth = bookMarks.JT02_FirstDiscontinuousFeederDescription.Columns.Width / ligneSommaireEntete.Count
                If ligneSommaireEntete.Count > 0 Then


                    bookMarks.JT02_FirstDiscontinuousFeederDescription.Select()

                    Dim ligneFedderInfo As ArrayList = ligneSommaireEntete.Item(0)

                    WordApp.Selection.Text = ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID) & Environment.NewLine & ligneFedderInfo.Item(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName) & " (T)"


                    bookMarks.JT02_DiscontinuousFeederTotalQuantity.Select()
                    WordApp.Selection.Columns.Last.Width = columnsWidth
                    WordApp.Selection.Columns.Last.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter
                Else

                End If

                ' Création de autres entête de benne chaude
                For feederEnteteIndex = ligneSommaireEntete.Count - 1 To 1 Step -1

                    WordDoc.Bookmarks("JT02_FirstDiscontinuousFeederDescription").Range.Select()
                    WordApp.Selection.InsertColumnsRight()

                    'Dim columnsWidth = bookMarks.JT02_FirstDiscontinuousFeederDescription.Columns.Width / ligneSommaireEntete.Count
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

                    bookMarks.JT02_FirstDiscontinuousMixNumber.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeFormule)

                    bookMarks.JT02_FirstDiscontinuousMixName.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeName)

                    bookMarks.JT02_FirstDiscontinuousVirginACGrade.Text = ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeGrade)

                    bookMarks.JT02_FirstDiscontinuousRAPPercentage.Text = If(Double.IsNaN(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise)), "-", CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeRapVise), Double).ToString("N0"))

                    bookMarks.JT02_FirstDiscontinuousQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasse), Double).ToString("N1")

                    bookMarks.JT02_FirstDiscontinuousVirginACQuantity.Text = CType(ligneEnrobe.Item(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume), Double).ToString("N1")

                    bookMarks.JT02_FirstDiscontinuousFeederQuantity.Select()

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

                    bookMarks.JT02_FirstDiscontinuousMixNumber.Select()

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

                    bookMarks.JT02_FirstDiscontinuousMixNumber.Select()
                    WordApp.Selection.MoveDown(WdUnits.wdLine, i, WdMovementType.wdMove)
                    WordApp.Selection.SelectRow()

                    WordApp.Selection.Borders(WdBorderType.wdBorderTop).LineStyle = WdLineStyle.wdLineStyleNone

                    If (i Mod 2 = 1) Then
                        WordApp.Selection.Shading.BackgroundPatternColor = WdColor.wdColorWhite
                    End If
                Next

                bookMarks.reinitializeDiscontinuousProductionSummaryBookMarks(WordDoc)

                Dim ligneEnrobeTotalMasse As ArrayList = tableauProductionDiscontinu.Item(tableauProductionDiscontinu.Count - 3)

                bookMarks.JT02_DiscontinuousTotalQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasse), Double).ToString("N1")
                bookMarks.JT02_DiscontinuousTotalVirginACQuantity.Text = CType(ligneEnrobeTotalMasse.Item(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasseBitume), Double).ToString("N1")

                Dim ligneSommairePourcentageAvecGBR As ArrayList = tableauProductionDiscontinu.Item(tableauProductionDiscontinu.Count - 2)
                bookMarks.JT02_DiscontinuousWithRAPPercentage.Text = CType(ligneSommairePourcentageAvecGBR.Item(EnumDailyReportTableauIndex.colonne_SommairePourcentageAvecGBR), Double).ToString("N0")

                Dim ligneSommairePourcentageDeGBR As ArrayList = tableauProductionDiscontinu.Item(tableauProductionDiscontinu.Count - 1)
                bookMarks.JT02_DiscontinuousOfRAPPercentage.Text = CType(ligneSommairePourcentageDeGBR.Item(EnumDailyReportTableauIndex.colonne_SommairePourcentageDeGBR), Double).ToString("N0")

                bookMarks.JT02_DiscontinuousTotalCellsToMerge.Cells.Merge()

            Else

                bookMarks.JA02_DiscontinuousProductionSummarySect.Delete()

            End If

            RaiseEvent CurrentProgress(90) ' 90 % Progress

            ' ---------
            ' KA01_Comments
            ' ---------

            bookMarks.KA01_Comments.Text = dailyReport.getReportComment

            ' ----------
            ' Signature
            ' ----------

            If (Not dailyReport.getUsineOperator() = FactoryOperator.DEFAULT_OPERATOR.ToString) Then

                bookMarks.LA01_OperatorName.Text = dailyReport.getUsineOperator().ToString()
            End If

            bookMarks.BA01_FooterDate.Text = Date.Today.ToString(Me.Formater.FullDateFormat)
            bookMarks.LA02_SignatureDate.Text = Date.Now.ToString(Me.Formater.DateTimeFormat)

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
