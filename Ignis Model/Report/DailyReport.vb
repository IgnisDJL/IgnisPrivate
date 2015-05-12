Public Class DailyReport
    Inherits Report

    Private donneeManuel As ManualData
    Private productionCycleContinuList As List(Of ProductionCycle)
    Private productionCycleDiscontinuList As List(Of ProductionCycle)
    Private producedMixContinuList As List(Of ProducedMix)
    Private producedMixDiscontinuList As List(Of ProducedMix)
    Private totalMixMass As Double = -1
    Private totalMixMassDiscontinu As Double = -1
    Private totalMixMassContinu As Double = -1

    Private delayContinuList As List(Of Delay_1)
    Private delayDiscontinuList As List(Of Delay_1)
    Private delayHybridList As List(Of Delay_1)

    Private delayFactory As DelayFactory

    Private tempsDeProductionDiscontinu As TimeSpan
    Private tempsDeProductionContinu As TimeSpan
    Private tempsDeProductionHybrid As TimeSpan

    Private commentaire As String
    ''**********************************************************
    ''*                     Constructeur
    ''**********************************************************
    Public Sub New(dateDebut As Date, dateFin As Date)

        MyBase.New(dateDebut, dateFin)
        commentaire = ""
        productionCycleContinuList = getProductionCycleContiuList()
        productionCycleDiscontinuList = getProductionCycleDiscontiuList()
        producedMixContinuList = getProducedMixList(productionCycleContinuList)
        producedMixDiscontinuList = getProducedMixList(productionCycleDiscontinuList)

        setTotalMixMass()
        delayFactory = New DelayFactory

        tempsDeProductionContinu = getMixProductionTime(producedMixContinuList)
        tempsDeProductionDiscontinu = getMixProductionTime(producedMixDiscontinuList)

        donneeManuel = New ManualData(Date.Now, getDebutPeriode, getFinPeriode, totalMixMass)
    End Sub


    ''**********************************************************
    ''*         Section des données de production 1.0
    ''**********************************************************

    ''**********************************************************
    ''*         Tableau des Horaire 1.1
    ''**********************************************************

    Public Function getTableauHoraire() As List(Of ArrayList)

        Dim tableauHoraire = New List(Of ArrayList)

        Dim ligneOperation = New ArrayList
        Dim ligneProduction = New ArrayList
        'Dim lignePostePesee = New ArrayList
        Dim ligneDelaisPauses = New ArrayList
        Dim ligneDelaisEntretiens = New ArrayList

        '' Ligne Operation
        ligneOperation.Insert(EnumDailyReportTableauIndex.colonne_OpperationDebut, getDebutPeriode)
        ligneOperation.Insert(EnumDailyReportTableauIndex.colonne_OpperationFin, getFinPeriode)
        ligneOperation.Insert(EnumDailyReportTableauIndex.colonne_OpperationDuree, getDureePeriode)

        tableauHoraire.Insert(EnumDailyReportTableauIndex.ligne_Operation, ligneOperation)

        '' Ligne Production
        ligneProduction.Insert(EnumDailyReportTableauIndex.colonne_ProductionDebut, getDebutProduction)
        ligneProduction.Insert(EnumDailyReportTableauIndex.colonne_ProductionFin, getFinProduction)
        ligneProduction.Insert(EnumDailyReportTableauIndex.colonne_ProductionDuree, calculateDureeProduction)

        tableauHoraire.Insert(EnumDailyReportTableauIndex.ligne_Production, ligneProduction)
        ''Ligne Delais Pauses
        ligneDelaisPauses.Insert(EnumDailyReportTableauIndex.colonne_PausesDuree, calculeDureeTotaleDelaisPause)

        tableauHoraire.Insert(EnumDailyReportTableauIndex.ligne_DelaisPauses, ligneDelaisPauses)

        ''Ligne Delais Entretiens
        ligneDelaisEntretiens.Insert(EnumDailyReportTableauIndex.colonne_Entretiens, calculateDureeTotaleDelaisEntretien)

        tableauHoraire.Insert(EnumDailyReportTableauIndex.ligne_DelaisEntretiens, ligneDelaisEntretiens)

        Return tableauHoraire
    End Function

    ''**********************************************************
    ''*         Tableau des Enrobés 1.2
    ''**********************************************************

    Public Function getTableauEnrobes() As List(Of ArrayList)
        Dim tableauEnrobes = New List(Of ArrayList)

        Dim ligneEnrobe1 = New ArrayList
        Dim ligneEnrobe2 = New ArrayList
        Dim ligneEnrobe3 = New ArrayList
        Dim ligneEnrobeAutres = New ArrayList
        Dim ligneQuantiteTotaleProduite = New ArrayList
        Dim ligneQuantiteEnSiloDebut = New ArrayList
        Dim ligneQuantiteEnSiloFin = New ArrayList
        Dim ligneQuantiteTotaleVendable = New ArrayList
        Dim ligneRejetsEnrobes = New ArrayList
        Dim ligneQuantiteTotalePayable = New ArrayList
        Dim ligneQuantiteTotaleVendue = New ArrayList

        Dim producedMixSortDecending As ArrayList = producedMixSortDecending_Hybrid()

        Dim mixProductionTimeEnrobe1 As TimeSpan

        If IsNothing(producedMixSortDecending.Item(0)) Then
            mixProductionTimeEnrobe1 = TimeSpan.Zero
        Else

            '' Ligne Enrobe 1
            mixProductionTimeEnrobe1 = TryCast(producedMixSortDecending.Item(0), ProducedMix).getTempsDeProduction

            ligneEnrobe1.Insert(EnumDailyReportTableauIndex.colonne_Enrobe1NoFormule, TryCast(producedMixSortDecending.Item(0), ProducedMix).getMixNumber)
            ligneEnrobe1.Insert(EnumDailyReportTableauIndex.colonne_Enrobe1NomEnrobe, TryCast(producedMixSortDecending.Item(0), ProducedMix).getMixName)
            ligneEnrobe1.Insert(EnumDailyReportTableauIndex.colonne_Enrobe1Quantite, TryCast(producedMixSortDecending.Item(0), ProducedMix).getMixMass)
            ligneEnrobe1.Insert(EnumDailyReportTableauIndex.colonne_Enrobe1Production, TryCast(producedMixSortDecending.Item(0), ProducedMix).getMixDebit)
            ligneEnrobe1.Insert(EnumDailyReportTableauIndex.colonne_Enrobe1ProductionMode, producedMixSortDecending.Item(1))

            tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_Enrobe1, ligneEnrobe1)

            '' Ligne Enrobe 2

        End If

        Dim mixProductionTimeEnrobe2 As TimeSpan

        If IsNothing(producedMixSortDecending.Item(2)) Then
            mixProductionTimeEnrobe2 = TimeSpan.Zero
        Else
            mixProductionTimeEnrobe2 = TryCast(producedMixSortDecending.Item(2), ProducedMix).getTempsDeProduction

            ligneEnrobe2.Insert(EnumDailyReportTableauIndex.colonne_Enrobe2NoFormule, TryCast(producedMixSortDecending.Item(2), ProducedMix).getMixNumber)
            ligneEnrobe2.Insert(EnumDailyReportTableauIndex.colonne_Enrobe2NomEnrobe, TryCast(producedMixSortDecending.Item(2), ProducedMix).getMixName)
            ligneEnrobe2.Insert(EnumDailyReportTableauIndex.colonne_Enrobe2Quantite, TryCast(producedMixSortDecending.Item(2), ProducedMix).getMixMass)
            ligneEnrobe2.Insert(EnumDailyReportTableauIndex.colonne_Enrobe2Production, TryCast(producedMixSortDecending.Item(2), ProducedMix).getMixDebit)
            ligneEnrobe2.Insert(EnumDailyReportTableauIndex.colonne_Enrobe2ProductionMode, producedMixSortDecending.Item(3))
        End If
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_Enrobe2, ligneEnrobe2)

        Dim mixProductionTimeEnrobe3 As TimeSpan

        If IsNothing(producedMixSortDecending.Item(4)) Then
            mixProductionTimeEnrobe3 = TimeSpan.Zero
        Else
            '' Ligne Enrobe 3
            mixProductionTimeEnrobe3 = TryCast(producedMixSortDecending.Item(4), ProducedMix).getTempsDeProduction

            ligneEnrobe3.Insert(EnumDailyReportTableauIndex.colonne_Enrobe3NoFormule, TryCast(producedMixSortDecending.Item(4), ProducedMix).getMixNumber)
            ligneEnrobe3.Insert(EnumDailyReportTableauIndex.colonne_Enrobe3NomEnrobe, TryCast(producedMixSortDecending.Item(4), ProducedMix).getMixName)
            ligneEnrobe3.Insert(EnumDailyReportTableauIndex.colonne_Enrobe3Quantite, TryCast(producedMixSortDecending.Item(4), ProducedMix).getMixMass)
            ligneEnrobe3.Insert(EnumDailyReportTableauIndex.colonne_Enrobe3Production, TryCast(producedMixSortDecending.Item(4), ProducedMix).getMixDebit)
            ligneEnrobe3.Insert(EnumDailyReportTableauIndex.colonne_Enrobe3ProductionMode, producedMixSortDecending.Item(5))
        End If

        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_Enrobe3, ligneEnrobe3)

        Dim totalMixProductionTimeAutre As TimeSpan

        If IsNothing(producedMixSortDecending.Item(4)) Then
            totalMixProductionTimeAutre = TimeSpan.Zero
        Else

            '' Ligne Enrobe Autres
            Dim totalMixMassAutre = getMixMass(TryCast(producedMixSortDecending.Item(6), List(Of ProducedMix)))
            totalMixProductionTimeAutre = getMixProductionTime(TryCast(producedMixSortDecending.Item(6), List(Of ProducedMix)))
            ligneEnrobeAutres.Insert(EnumDailyReportTableauIndex.colonne_EnrobeAutreNombre, TryCast(producedMixSortDecending.Item(6), List(Of ProducedMix)).Count)
            ligneEnrobeAutres.Insert(EnumDailyReportTableauIndex.colonne_EnrobeAutreQuantite, totalMixMassAutre)
            ligneEnrobeAutres.Insert(EnumDailyReportTableauIndex.colonne_EnrobeAutreProduction, totalMixMassAutre / totalMixProductionTimeAutre.TotalHours)
        End If

        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_EnrobeAutres, ligneEnrobeAutres)

        '' Ligne Quantitée Totale produite

        ligneQuantiteTotaleProduite.Insert(EnumDailyReportTableauIndex.colonne_QuantiteTotaleProduiteQuantite, totalMixMass)
        ligneQuantiteTotaleProduite.Insert(EnumDailyReportTableauIndex.colonne_QuantiteTotaleProduiteProduction, totalMixMass /
                                 (totalMixProductionTimeAutre.TotalHours + mixProductionTimeEnrobe3.TotalHours + mixProductionTimeEnrobe2.TotalHours + mixProductionTimeEnrobe1.TotalHours))

        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_QuantiteTotaleProduite, ligneQuantiteTotaleProduite)

        '' Ligne Quantitée silo (début de jounée)

        ligneQuantiteEnSiloDebut.Insert(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloDebutQuantite, getQuantiteEnSiloDebut)
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_QuantiteEnSiloDebut, ligneQuantiteEnSiloDebut)

        '' Ligne Quantitée silo (fin de jounée)

        ligneQuantiteEnSiloFin.Insert(EnumDailyReportTableauIndex.colonne_QuantiteEnSiloFinQuantite, getQuantiteEnSiloFin)
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_QuantiteEnSiloFin, ligneQuantiteEnSiloFin)

        '' Ligne Quantite Totale Vendable

        ligneQuantiteTotaleVendable.Insert(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendableQuantite, getQuantiteTotaleVendable)
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_QuantiteTotaleVendable, ligneQuantiteTotaleVendable)


        '' Ligne Rejet d enrobés

        ligneRejetsEnrobes.Insert(EnumDailyReportTableauIndex.colonne_RejetsEnrobesQuantite, getQuantiteEnrobeRejete)
        ligneRejetsEnrobes.Insert(EnumDailyReportTableauIndex.colonne_RejetsEnrobesPourcentageRejet, getPourcentageDeRejet)
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_RejetsEnrobes, ligneRejetsEnrobes)

        '' Ligne Quantite Totale payable

        ligneQuantiteTotalePayable.Insert(EnumDailyReportTableauIndex.colonne_QuantiteTotalePayableQuantite, getQuantiteTotalePayable)
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_QuantiteTotalePayable, ligneQuantiteTotalePayable)

        '' Ligne Quantite Totale vendable

        ligneQuantiteTotaleVendue.Insert(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVendueQuantite, getQuantiteTotalVendue)
        ligneQuantiteTotaleVendue.Insert(EnumDailyReportTableauIndex.colonne_QuantiteTotaleVenduePourcentageEcart, getPourcentageEcartQuantieVenduQuantiteRejete)
        tableauEnrobes.Insert(EnumDailyReportTableauIndex.ligne_QuantiteTotaleVendue, ligneQuantiteTotaleVendue)

        Return tableauEnrobes
    End Function

    ''**********************************************************
    ''*         Section Production et Delais 2.0
    ''**********************************************************

    ''**********************************************************
    ''*         Tableau des mode de production 2.1
    ''**********************************************************

    Public Function getTableauModeProduction() As List(Of ArrayList)
        Dim tableauModeProduction = New List(Of ArrayList)

        Dim ligneDuree = New ArrayList
        Dim lignePourcentageDuTemps = New ArrayList
        Dim ligneNombreDeChangements = New ArrayList
        Dim ligneQuantiteProduite = New ArrayList
        Dim ligneTauxDeProduction = New ArrayList

        '' Ligne Durée
        Dim totalDelaisHybrid As TimeSpan = calculateDureeTotal(getHybridDelayList)
        Dim totalDelaisContinu As TimeSpan = calculateDureeTotal(getContinuDelayList)
        ligneDuree.Insert(EnumDailyReportTableauIndex.colonne_DureeContinu, getDureePeriode() - totalDelaisContinu)
        ligneDuree.Insert(EnumDailyReportTableauIndex.colonne_DureeDiscontinu, getMixProductionTime(producedMixDiscontinuList))
        ligneDuree.Insert(EnumDailyReportTableauIndex.colonne_DureeDelais, totalDelaisHybrid)

        tableauModeProduction.Insert(EnumDailyReportTableauIndex.ligne_Duree, ligneDuree)


        '' Ligne Pourcentage du temps


        If productionCycleDiscontinuList.Count = 0 Then
            lignePourcentageDuTemps.Insert(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsContinu,
                                    Math.Round(((getDureePeriode().TotalSeconds - totalDelaisContinu.TotalSeconds) / getDureePeriode().TotalSeconds) * 100))
        Else
            lignePourcentageDuTemps.Insert(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsContinu,
                                    Math.Round((tempsDeProductionContinu.TotalSeconds / getDureePeriode().TotalSeconds) * 100))
        End If

        lignePourcentageDuTemps.Insert(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsDiscontinu,
                                    Math.Round((tempsDeProductionDiscontinu.TotalSeconds / getDureePeriode().TotalSeconds) * 100))

        lignePourcentageDuTemps.Insert(EnumDailyReportTableauIndex.colonne_PourcentageDuTempsDelais,
                                    Math.Round(((totalDelaisHybrid.TotalSeconds) / getDureePeriode().TotalSeconds) * 100))

        tableauModeProduction.Insert(EnumDailyReportTableauIndex.ligne_PourcentageDuTemps, lignePourcentageDuTemps)

        '' Ligne nombres changements de mélange / délais
        ligneNombreDeChangements.Insert(EnumDailyReportTableauIndex.colonne_NombreDeChangementsContinu, getNombreChangementMix(productionCycleContinuList))
        ligneNombreDeChangements.Insert(EnumDailyReportTableauIndex.colonne_NombreDeChangementsDiscontinu, getNombreChangementMix(productionCycleDiscontinuList))
        ligneNombreDeChangements.Insert(EnumDailyReportTableauIndex.colonne_NombreDeChangementsDelais, getHybridDelayList.Count)

        tableauModeProduction.Insert(EnumDailyReportTableauIndex.ligne_NombreDeChangements, ligneNombreDeChangements)

        '' Ligne quantité produite
        ligneQuantiteProduite.Insert(EnumDailyReportTableauIndex.colonne_QuantiteProduiteContinu, getTotalMixMassContinu)
        ligneQuantiteProduite.Insert(EnumDailyReportTableauIndex.colonne_QuantiteProduiteDiscontinu, getTotalMixMassDiscontinu)

        tableauModeProduction.Insert(EnumDailyReportTableauIndex.ligne_QuantiteProduite, ligneQuantiteProduite)

        '' Ligne Taux de production T/H
        ligneTauxDeProduction.Insert(EnumDailyReportTableauIndex.colonne_TauxDeProductionContinu, getTotalMixMassContinu() / tempsDeProductionContinu.TotalHours)
        ligneTauxDeProduction.Insert(EnumDailyReportTableauIndex.colonne_TauxDeProductionDiscontinu, getTotalMixMassDiscontinu() / tempsDeProductionDiscontinu.TotalHours)

        tableauModeProduction.Insert(EnumDailyReportTableauIndex.ligne_TauxDeProduction, ligneTauxDeProduction)

        Return tableauModeProduction
    End Function

    ''**********************************************************
    ''*         Tableau Temps de production 2.2
    ''**********************************************************

    Public Function getTableauTempsDeProduction() As List(Of ArrayList)
        Dim tableauTempsDeProduction = New List(Of ArrayList)

        Dim ligneTempsTotalOperations = New ArrayList
        Dim ligneTempsNetOperations = New ArrayList
        Dim ligneProductionNette = New ArrayList
        Dim ligneProductionEfficace = New ArrayList
        Dim ligneProductionEfficaceInterne = New ArrayList
        Dim ligneDelais = New ArrayList

        '' ligne Temps Total Operations
        ligneTempsTotalOperations.Insert(EnumDailyReportTableauIndex.colonne_TempsTotalOperationsDuree, getDureePeriode)

        tableauTempsDeProduction.Insert(EnumDailyReportTableauIndex.ligne_TempsTotalOperations, ligneTempsTotalOperations)

        '' ligne Temps nette Operations
        ligneTempsNetOperations.Insert(EnumDailyReportTableauIndex.colonne_TempsNetOperationsDuree, getDureePeriode() - calculeDureeTotaleDelaisPause())

        tableauTempsDeProduction.Insert(EnumDailyReportTableauIndex.ligne_TempsNetOperations, ligneTempsNetOperations)

        '' ligne Production nette
        ligneProductionNette.Insert(EnumDailyReportTableauIndex.colonne_ProductionNetteDuree, getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien())

        tableauTempsDeProduction.Insert(EnumDailyReportTableauIndex.ligne_ProductionNette, ligneProductionNette)

        '' ligne Production efficace
        ligneProductionEfficace.Insert(EnumDailyReportTableauIndex.colonne_ProductionEfficaceDuree, getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien() - calculateDureeTotal(getHybridDelayList()))

        tableauTempsDeProduction.Insert(EnumDailyReportTableauIndex.ligne_ProductionEfficace, ligneProductionEfficace)

        '' ligne Production efficace interne
        ligneProductionEfficaceInterne.Insert(EnumDailyReportTableauIndex.colonne_ProductionEfficaceInterneDuree, getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien() - calculateDureeTotaleDelaisInterne())

        tableauTempsDeProduction.Insert(EnumDailyReportTableauIndex.ligne_ProductionEfficaceInterne, ligneProductionEfficaceInterne)

        '' ligne Delais
        ligneDelais.Insert(EnumDailyReportTableauIndex.colonne_DelaisDuree, calculateDureeTotal(getHybridDelayList()))

        tableauTempsDeProduction.Insert(EnumDailyReportTableauIndex.ligne_Delais, ligneDelais)

        Return tableauTempsDeProduction
    End Function

    ''**********************************************************
    ''*         Tableau Delais 2.3
    ''**********************************************************

    Public Function getTableauDelais() As List(Of ArrayList)
        Dim tableauDelais = New List(Of ArrayList)

        Dim ligneNombreDeBris = New ArrayList
        Dim ligneDisponibilite = New ArrayList
        Dim ligneUtilisation = New ArrayList
        Dim ligneTempsEntrePannes = New ArrayList
        Dim ligneTempsPourReparer = New ArrayList


        '' ligne Nombre de bris
        ligneNombreDeBris.Insert(EnumDailyReportTableauIndex.colonne_NombreDeBris, calculateNombreDeBrisInterne)

        tableauDelais.Insert(EnumDailyReportTableauIndex.ligne_NombreDeBris, ligneNombreDeBris)

        '' ligne Disponibilie
        ligneDisponibilite.Insert(EnumDailyReportTableauIndex.colonne_Disponibilite, _
                                  (getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien() - calculateDureeTotaleDelaisInterne()).TotalSeconds / _
                                  (getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien()).TotalSeconds * 100)

        tableauDelais.Insert(EnumDailyReportTableauIndex.ligne_Disponibilite, ligneDisponibilite)

        '' ligne Utilisation
        ligneUtilisation.Insert(EnumDailyReportTableauIndex.colonne_Utilisation, _
                                (getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien() - calculateDureeTotal(getHybridDelayList())).TotalSeconds / _
                                 getDureePeriode().TotalSeconds * 100)

        tableauDelais.Insert(EnumDailyReportTableauIndex.ligne_Utilisation, ligneUtilisation)

        '' ligne Temps entre les pannes
        ligneTempsEntrePannes.Insert(EnumDailyReportTableauIndex.colonne_TempsEntrePannes, _
                                     (getDureePeriode() - calculeDureeTotaleDelaisPause() - calculateDureeTotaleDelaisEntretien() - calculateDureeTotal(getHybridDelayList())).TotalHours / _
                                      calculateNombreDeBrisInterne())

        tableauDelais.Insert(EnumDailyReportTableauIndex.ligne_TempsEntrePannes, ligneTempsEntrePannes)

        '' ligne Temps pour réparer les pannes
        ligneTempsPourReparer.Insert(EnumDailyReportTableauIndex.colonne_TempsPourReparer, _
                                     calculerDureeTotaleDelaisInterneAvecBris.TotalHours / _
                                     calculateNombreDeBrisInterne())

        tableauDelais.Insert(EnumDailyReportTableauIndex.ligne_TempsPourReparer, ligneTempsPourReparer)

        Return tableauDelais
    End Function

    ''****************************************************************************
    ''*         Section Bitumes et Températures de productions 3.0
    ''****************************************************************************

    ''****************************************************************************
    ''*                     Tableau Bitumes consommés 3.1
    ''****************************************************************************

    Public Function getTableauBitumeConsommes() As List(Of ArrayList)
        Dim tableauBitumeConsommes = New List(Of ArrayList)
        Dim ligneTotalBitumeConsommes = New ArrayList
        '' Dans ce tableau le nombre de lignes est dynamique donc il n'y a qu'une ligne 

        Dim index_ligne_VirginAsphaltConcrete As Integer = EnumDailyReportTableauIndex.ligne_VirginAsphaltConcrete

        Dim producedMixHybridList = New List(Of ProducedMix)
        producedMixHybridList.InsertRange(0, producedMixContinuList)
        producedMixHybridList.InsertRange(0, producedMixDiscontinuList)

        Dim virginAsphaltConcreteList As List(Of VirginAsphaltConcrete) = getBitumeConsomme(producedMixHybridList)


        For Each virginAsphaltConcrete As VirginAsphaltConcrete In virginAsphaltConcreteList

            If (virginAsphaltConcrete.getMass > 0) Then

                '' ligne (dynamique) Bitume neuf utilisé
                Dim ligneVirginAsphaltConcrete = New ArrayList

                ligneVirginAsphaltConcrete.Insert(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteReservoir, virginAsphaltConcrete.getTankId)
                ligneVirginAsphaltConcrete.Insert(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteGrade, virginAsphaltConcrete.getGrade)
                ligneVirginAsphaltConcrete.Insert(EnumDailyReportTableauIndex.colonne_VirginAsphaltConcreteQuantite, virginAsphaltConcrete.getMass)

                tableauBitumeConsommes.Insert(index_ligne_VirginAsphaltConcrete, ligneVirginAsphaltConcrete)

                index_ligne_VirginAsphaltConcrete = index_ligne_VirginAsphaltConcrete + 1
            End If

        Next

        ligneTotalBitumeConsommes.Insert(EnumDailyReportTableauIndex.colonne_TotalBitumeConsommesQuantite, getVirginAsphaltConcreteMass(virginAsphaltConcreteList))
        tableauBitumeConsommes.Insert(index_ligne_VirginAsphaltConcrete, ligneTotalBitumeConsommes)

        Return tableauBitumeConsommes
    End Function

    ''****************************************************************************
    ''*             Tableau Écart Par rapport à la valeur visée 3.2
    ''****************************************************************************

    Public Function getTableauEcartValeurVisee() As List(Of ArrayList)
        Dim tableauEcartValeurVisee = New List(Of ArrayList)
        Dim ligneBitumeEcrat = New ArrayList
        Dim ligneTemperatureEcrat = New ArrayList


        '' ligne Bitume (%)
        ligneBitumeEcrat.Insert(EnumDailyReportTableauIndex.colonne_BitumeEcartPourcentage, getMoyennePondereEcartPourcentageBitume())
        tableauEcartValeurVisee.Insert(EnumDailyReportTableauIndex.ligne_BitumeEcart, ligneBitumeEcrat)

        ' ligne Temperature (C)
        ligneTemperatureEcrat.Insert(EnumDailyReportTableauIndex.colonne_BitumeEcartPourcentage, getMoyennePondereEcartTemperature)
        tableauEcartValeurVisee.Insert(EnumDailyReportTableauIndex.ligne_TemperatureEcart, ligneTemperatureEcrat)

        Return tableauEcartValeurVisee
    End Function

    ''****************************************************************************
    ''*             Tableau Variation en production 3.3
    ''****************************************************************************

    Public Function getTableauVariationEnProduction() As List(Of ArrayList)
        Dim tableauVariationEnProduction = New List(Of ArrayList)
        Dim ligneVariationTemperature = New ArrayList

        ' ligne variation Temperature (C/T)
        ligneVariationTemperature.Insert(EnumDailyReportTableauIndex.colonne_VariationTemperature, getVariationTemperature)
        tableauVariationEnProduction.Insert(EnumDailyReportTableauIndex.ligne_VariationTemperature, ligneVariationTemperature)

        Return tableauVariationEnProduction
    End Function


    ''****************************************************************************
    ''*             Tableau Taux de valeurs aberrantes 3.4
    ''****************************************************************************

    Public Function getTableauValeursAberrantes() As List(Of ArrayList)
        Dim tableauValeursAberrantes = New List(Of ArrayList)
        Dim lignePourcentageBitume = New ArrayList
        Dim lignePourcentageTemperature = New ArrayList

        ' ligne des valeurs aberrantes pour le bitume
        lignePourcentageBitume.Insert(EnumDailyReportTableauIndex.colonne_PourcentageBitume, getValeursAberrantesBitume)
        tableauValeursAberrantes.Insert(EnumDailyReportTableauIndex.ligne_PourcentageBitume, lignePourcentageBitume)

        ' ligne des valeurs aberrantes pour la temperature des bitumes
        lignePourcentageTemperature.Insert(EnumDailyReportTableauIndex.colonne_PourcentageTemperature, getValeursAberrantesTemperature)
        tableauValeursAberrantes.Insert(EnumDailyReportTableauIndex.ligne_PourcentageTemperature, lignePourcentageTemperature)

        Return tableauValeursAberrantes
    End Function


    ''****************************************************************************
    ''*                     Section Des Carburants 4.0
    ''****************************************************************************

    ''****************************************************************************
    ''*                      Tableau Carburants 4.1
    ''****************************************************************************

    Public Function getTableauCarburants() As List(Of ArrayList)
        Dim tableauCarburants = New List(Of ArrayList)
        Dim ligneCarburantPrincipal = New ArrayList
        Dim ligneCarburantGazNaturel = New ArrayList


        ' ligne des valeurs aberrantes pour le bitume
        ligneCarburantPrincipal.Insert(EnumDailyReportTableauIndex.colonne_NomCarburant, ProgramController.SettingsControllers.UsineSettingsController.getFuel1Name)
        ligneCarburantPrincipal.Insert(EnumDailyReportTableauIndex.colonne_QuantiteConsomme, donneeManuel.FUEL_CONSUMED_QUANTITY_1)
        ligneCarburantPrincipal.Insert(EnumDailyReportTableauIndex.colonne_UniteQuantiteConsomme, ProgramController.SettingsControllers.UsineSettingsController.getFuel1Unit)
        ligneCarburantPrincipal.Insert(EnumDailyReportTableauIndex.colonne_TauxDeConsommation, donneeManuel.FUEL_CONSUMPTION_RATE_1)
        ligneCarburantPrincipal.Insert(EnumDailyReportTableauIndex.colonne_UniteTauxDeConsommation, ProgramController.SettingsControllers.UsineSettingsController.getFuel1Unit + "/T")


        tableauCarburants.Insert(EnumDailyReportTableauIndex.ligne_CarburantPrincipal, ligneCarburantPrincipal)

        ' ligne des valeurs aberrantes pour la temperature des bitumes

        ligneCarburantGazNaturel.Insert(EnumDailyReportTableauIndex.colonne_NomCarburant, ProgramController.SettingsControllers.UsineSettingsController.getFuel2Name)
        ligneCarburantGazNaturel.Insert(EnumDailyReportTableauIndex.colonne_QuantiteConsomme, donneeManuel.FUEL_CONSUMED_QUANTITY_2)
        ligneCarburantGazNaturel.Insert(EnumDailyReportTableauIndex.colonne_UniteQuantiteConsomme, ProgramController.SettingsControllers.UsineSettingsController.getFuel2Unit)
        ligneCarburantGazNaturel.Insert(EnumDailyReportTableauIndex.colonne_TauxDeConsommation, donneeManuel.FUEL_CONSUMPTION_RATE_2)
        ligneCarburantGazNaturel.Insert(EnumDailyReportTableauIndex.colonne_UniteTauxDeConsommation, ProgramController.SettingsControllers.UsineSettingsController.getFuel2Unit + "/T")


        tableauCarburants.Insert(EnumDailyReportTableauIndex.ligne_CarburantGazNatutel, ligneCarburantGazNaturel)


        Return tableauCarburants
    End Function



    ''****************************************************************************
    ''*                     Section Des Rejets 5.0
    ''****************************************************************************

    ''****************************************************************************
    ''*                         Tableau Rejet 5.1
    ''****************************************************************************

    Public Function getTableauRejets() As List(Of ArrayList)
        Dim tableauRejets = New List(Of ArrayList)
        Dim ligneQuantiteRejete = New ArrayList
        Dim ligneTauxDeRejet = New ArrayList


        ' ligne des Quantité rejetée pour un matériau
        ligneQuantiteRejete.Insert(EnumDailyReportTableauIndex.colonne_RejetGranulats, donneeManuel.REJECTED_AGGREGATES_QUANTITY)
        ligneQuantiteRejete.Insert(EnumDailyReportTableauIndex.colonne_RejetFiller, donneeManuel.REJECTED_FILLER_QUANTITY)
        ligneQuantiteRejete.Insert(EnumDailyReportTableauIndex.colonne_RejetGBR, donneeManuel.REJECTED_RECYCLED_QUANTITY)

        tableauRejets.Insert(EnumDailyReportTableauIndex.ligne_QuantiteRejete, ligneQuantiteRejete)

        ' taux de rejets pour un matériau
        ligneTauxDeRejet.Insert(EnumDailyReportTableauIndex.colonne_RejetGranulats, donneeManuel.REJECTED_AGGREGATES_PERCENTAGE)
        ligneTauxDeRejet.Insert(EnumDailyReportTableauIndex.colonne_RejetFiller, donneeManuel.REJECTED_FILLER_PERCENTAGE)
        ligneTauxDeRejet.Insert(EnumDailyReportTableauIndex.colonne_RejetGBR, donneeManuel.REJECTED_RECYCLED_PERCENTAGE)


        tableauRejets.Insert(EnumDailyReportTableauIndex.ligne_TauxDeRejet, ligneTauxDeRejet)


        Return tableauRejets
    End Function



    ''****************************************************************************
    ''*                     Section Sommaire des Délais 6.0
    ''****************************************************************************

    ''****************************************************************************
    ''*                         Tableau Délais 6.1
    ''****************************************************************************

    Public Function getTableauDelay() As List(Of ArrayList)
        Dim tableauDelay = New List(Of ArrayList)
        Dim ligneDelayNonJustifiable = New ArrayList
        Dim ligneDelayTotal = New ArrayList

        Dim indexLigneDelay As Integer = 0


        For Each delay As Delay_1 In delayFactory.removeDelayLowerThen(getHybridDelayList(), TimeSpan.FromMinutes(10))

            Dim ligneDelay = New ArrayList
            '' Ligne des délais de 10 minutes ou plus
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDebut, delay.getStartDelay)
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisJustifiableFin, delay.getEndDelay)
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDuree, delay.getDuration)
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisJustifiableName, delay.getDelayCode)
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisJustifiableDescription, delay.getDelayDescription)
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisJustifiableCommentaire, delay.getDelayJustification)
            ligneDelay.Insert(EnumDailyReportTableauIndex.colonne_DelaisColor, delay.getColor)

            tableauDelay.Insert(indexLigneDelay, ligneDelay)

            indexLigneDelay += 1
        Next

        Dim delaisNonJustifiable As List(Of Delay_1) = delayFactory.removeDelayHigherThen(getHybridDelayList(), TimeSpan.FromMinutes(10))

        '' Ligne des délais de moins de 10 minutes
        ligneDelayNonJustifiable.Insert(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableNombre, delaisNonJustifiable.Count)
        ligneDelayNonJustifiable.Insert(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableDuree, calculateDureeTotal(delaisNonJustifiable))
        ligneDelayNonJustifiable.Insert(EnumDailyReportTableauIndex.colonne_DelaisNonJustifiableLimite, XmlSettings.Settings.instance.Usine.Events.Delays.JUSTIFIABLE_DURATION.TotalMinutes.ToString("N0"))
        tableauDelay.Insert(indexLigneDelay, ligneDelayNonJustifiable)

        indexLigneDelay += 1

        '' Ligne du total des délais
        ligneDelayTotal.Insert(EnumDailyReportTableauIndex.colonne_DelaisTotalDuree, calculateDureeTotal(getHybridDelayList()))
        tableauDelay.Insert(indexLigneDelay, ligneDelayTotal)

        Return tableauDelay
    End Function

    ''****************************************************************************
    ''*             Section Sommaire de la production en continu 7.0
    ''****************************************************************************

    ''****************************************************************************
    ''*                     Tableau Production Continu 7.1
    ''****************************************************************************

    Public Function getTableauProductionContinu() As List(Of ArrayList)
        Dim tableauProductionContinu = getTableauSommaireProduction(producedMixContinuList, productionCycleContinuList)


        Return tableauProductionContinu
    End Function

    ''****************************************************************************
    ''*                     Tableau Production Continu 7.1
    ''****************************************************************************

    Public Function getTableauProductionDiscontinu() As List(Of ArrayList)

        Dim tableauProductionDiscontinu = getTableauSommaireProduction(producedMixDiscontinuList, productionCycleDiscontinuList)



        Return tableauProductionDiscontinu
    End Function

    Private Function getTableauSommaireProduction(producedMixList As List(Of ProducedMix), productionCycleList As List(Of ProductionCycle)) As List(Of ArrayList)
        Dim tableauProduction = New List(Of ArrayList)
        Dim ligneSommaireEntete = New ArrayList
        Dim ligneSommaireTotal = New ArrayList

        Dim ligneSommairePourcentageAvecGBR = New ArrayList
        Dim ligneSommairePourcentageDeGBR = New ArrayList

        Dim indexLigne As Integer = EnumDailyReportTableauIndex.ligne_SommaireFirstLigneEnrobe
        Dim indexColonneEntete As Integer = 0

        Dim totalMass As Double = 0
        Dim totalMassBitume As Double = 0
        Dim totalMassFeederList As ArrayList = New ArrayList

        producedMixList.Sort()

        '' Ligne des entêtes de chaque bennes

        If producedMixList.Count > 0 Then

            For Each feeder As Feeder_1 In producedMixList.Item(0).getHotFeederList

                '' TODO
                '' Poser la question à Martin, la date pour la sélection du nom du Feeder 
                '' doit-elle être la date de début de la période ou la date de la fin de la période ?

                Dim infosFeeder As ArrayList = New ArrayList

                infosFeeder.Insert(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederID, feeder.getFeederID)
                infosFeeder.Insert(EnumDailyReportTableauIndex.colonne_SommaireEnteteFeederName, feeder.getFeederName(getDebutPeriode))

                ligneSommaireEntete.Insert(indexColonneEntete, infosFeeder)

                indexColonneEntete += 1
            Next

            tableauProduction.Insert(EnumDailyReportTableauIndex.ligne_SommaireEntete, ligneSommaireEntete)

        End If

        For Each producedMix As ProducedMix In producedMixList

            If (producedMix.getMixMass > 0) Then

                Dim ligneEnrobe = New ArrayList

                '' TODO
                '' Le mix Name devrait être récupéré du catalogue de la meme manière que le nom des feeders
                ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeFormule, producedMix.getMixNumber)
                ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeName, producedMix.getMixName)
                '' TODO
                '' getGrade devrait être récupéré de la même manière que le nom des feeders
                ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeGrade, producedMix.getVirginAsphaltConcrete.getGrade)


                '' TODO
                '' Refactor de la gestion des RapAsphaltConcrete, il semble n'y avoir aucun interet a en avoir une liste
                If producedMix.getRapAsphaltConcreteList.Count > 0 Then
                    ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeRapVise, producedMix.getRapAsphaltConcreteList.Item(0).getTargetPercentage)
                Else
                    ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeRapVise, 0)
                End If

                ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeMasse, producedMix.getMixMass)
                totalMass += producedMix.getMixMass

                ligneEnrobe.Insert(EnumDailyReportTableauIndex.colonne_EnrobeMasseBitume, producedMix.getVirginAsphaltConcrete.getMass)
                totalMassBitume += producedMix.getVirginAsphaltConcrete.getMass

                '' TODO
                '' À Valider que ce for fonctionne correctement

                For index As Integer = 0 To ligneSommaireEntete.Count - 1 Step 1
                    ligneEnrobe.Insert(ligneEnrobe.Count, producedMix.getHotFeederList.Item(index).getMass)

                    If totalMassFeederList.Count = index Then
                        totalMassFeederList.Add(producedMix.getHotFeederList.Item(index).getMass)
                    Else
                        totalMassFeederList.Item(index) += producedMix.getHotFeederList.Item(index).getMass
                    End If

                Next
                tableauProduction.Insert(indexLigne, ligneEnrobe)

                indexLigne += 1
            End If

        Next

        ligneSommaireTotal.Insert(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasse, totalMass)
        ligneSommaireTotal.Insert(EnumDailyReportTableauIndex.colonne_EnrobeTotalMasseBitume, totalMassBitume)

        For Each totalFeederMass As Double In totalMassFeederList
            ligneSommaireTotal.Insert(ligneSommaireTotal.Count, totalFeederMass)
        Next
        tableauProduction.Insert(indexLigne, ligneSommaireTotal)

        ligneSommairePourcentageAvecGBR.Insert(EnumDailyReportTableauIndex.colonne_SommairePourcentageAvecGBR, getPourcentageAvecGBR(productionCycleList))
        tableauProduction.Insert(tableauProduction.Count, ligneSommairePourcentageAvecGBR)

        ligneSommairePourcentageDeGBR.Insert(EnumDailyReportTableauIndex.colonne_SommairePourcentageDeGBR, getPourcentageDeGBR(productionCycleList))
        tableauProduction.Insert(tableauProduction.Count, ligneSommairePourcentageDeGBR)

        Return tableauProduction
    End Function



    '' TODO
    '' Vérifier avec Martin que la valeur souhaité est bel et bien le nombre de cycle (qui ont produit de l'enrobé) avec un écart de plus ou moins 0.005 (0.5%) entre le poucentage de bitume visé et réel
    '' divisé par le nombre de cycle (qui ont produit de l'enribé) multiplié par 100
    Private Function getValeursAberrantesBitume() As Double
        Dim nombreDeCycle As Integer = 0
        Dim nombreDeCycleAvecEnrobe As Integer = 0
        Dim ecartBitume As Double

        Dim productionCycleHybridList = New List(Of ProductionCycle)
        productionCycleHybridList.InsertRange(0, productionCycleContinuList)
        productionCycleHybridList.InsertRange(0, productionCycleDiscontinuList)

        For Each productionCycle As ProductionCycle In productionCycleHybridList

            If (productionCycle.getProducedMix.getMixMass > 0 _
                And productionCycle.getProducedMix.getVirginAsphaltConcrete.getTargetPercentage > 0 _
                And productionCycle.getProducedMix.getVirginAsphaltConcrete.getActualPercentage >= 0) Then

                ecartBitume = Math.Abs(productionCycle.getProducedMix.getVirginAsphaltConcrete.getActualPercentage - productionCycle.getProducedMix.getVirginAsphaltConcrete.getTargetPercentage)

                '' TODO
                '' Vérifier si TargetPercentage et ActualPercentage sont affiché dans le format ex: 22.0 ou plutot 0.22

                '' TODO
                '' Constante à sortir (0.5%)
                If ecartBitume > 0.005 Then
                    nombreDeCycle += 1
                End If

                nombreDeCycleAvecEnrobe += 1
            End If
        Next

        Return nombreDeCycle / nombreDeCycleAvecEnrobe * 100
    End Function

    '' TODO 
    '' Vérifier avec Martin que la fonction doit seulement compter le pourcentage des cycles (ayant produit de l'enrobé) dans lesquelles la 
    '' température du bitume était plus élevé ou égale a 200 ou plus petite ou égale a 100 
    Private Function getValeursAberrantesTemperature() As Double
        Dim nombreDeCycle As Integer = 0
        Dim nombreDeCycleAvecEnrobe As Integer = 0

        Dim productionCycleHybridList = New List(Of ProductionCycle)
        productionCycleHybridList.InsertRange(0, productionCycleContinuList)
        productionCycleHybridList.InsertRange(0, productionCycleDiscontinuList)

        For Each productionCycle As ProductionCycle In productionCycleHybridList

            If (productionCycle.getProducedMix.getMixMass > 0 _
                And productionCycle.getProducedMix.getVirginAsphaltConcrete.getTargetPercentage > 0 _
                And productionCycle.getProducedMix.getVirginAsphaltConcrete.getActualPercentage >= 0) Then

                If productionCycle.getProducedMix.getRecordedTemperature >= 200 Or productionCycle.getProducedMix.getRecordedTemperature <= 100 Then
                    nombreDeCycle += 1
                End If

                nombreDeCycleAvecEnrobe += 1
            End If

        Next

        Return nombreDeCycle / nombreDeCycleAvecEnrobe * 100
    End Function

    Private Function getVariationTemperature() As Double
        Dim variationTemperature As Double = 0
        Dim absDiffContinu As Double = 0
        Dim absDiffDiscontinu As Double = 0
        Dim nombreDeCycle As Integer = 0
        Dim masseTotal As Double = 0

        Dim previousProductionCycleContinu As ProductionCycle = Nothing

        For Each productionCycleContinu As ProductionCycle In productionCycleContinuList

            If productionCycleContinu.getProducedMix.getRecordedTemperature >= 0 _
                And productionCycleContinu.getProducedMix.getMixMass > 0 Then

                If IsNothing(previousProductionCycleContinu) Then
                    previousProductionCycleContinu = productionCycleContinu
                Else

                    absDiffContinu += Math.Abs(previousProductionCycleContinu.getProducedMix.getRecordedTemperature - productionCycleContinu.getProducedMix.getRecordedTemperature) * productionCycleContinu.getProducedMix.getMixMass

                    masseTotal += productionCycleContinu.getProducedMix.getMixMass
                    nombreDeCycle += 1

                    previousProductionCycleContinu = productionCycleContinu

                End If

            End If

        Next

        Dim previousProductionCycleDiscontinu As ProductionCycle = Nothing

        For Each productionCycleDiscontinu As ProductionCycle In productionCycleDiscontinuList

            If productionCycleDiscontinu.getProducedMix.getRecordedTemperature >= 0 _
                And productionCycleDiscontinu.getProducedMix.getMixMass > 0 Then

                If IsNothing(previousProductionCycleDiscontinu) Then
                    previousProductionCycleDiscontinu = productionCycleDiscontinu
                Else

                    absDiffDiscontinu += Math.Abs(previousProductionCycleDiscontinu.getProducedMix.getRecordedTemperature - productionCycleDiscontinu.getProducedMix.getRecordedTemperature) * productionCycleDiscontinu.getProducedMix.getMixMass

                    masseTotal += productionCycleDiscontinu.getProducedMix.getMixMass
                    nombreDeCycle += 1

                    previousProductionCycleDiscontinu = productionCycleDiscontinu



                End If

            End If

        Next

        Return absDiffDiscontinu / (nombreDeCycle * masseTotal)
    End Function

    Private Function getMoyennePondereEcartPourcentageBitume() As Double
        Dim moyennePondereEcartPourcentageBitume As Double = 0
        Dim nombreDeCycle As Integer = 0
        Dim masseTotal As Double = 0

        Dim productionCycleHybridList = New List(Of ProductionCycle)
        productionCycleHybridList.InsertRange(0, productionCycleContinuList)
        productionCycleHybridList.InsertRange(0, productionCycleDiscontinuList)

        '' TODO
        '' Vérifier si les pourcentages s'affiche dans le format ex: 22.0 ou plutot 0.22 

        For Each productionCycle As ProductionCycle In productionCycleHybridList

            If (productionCycle.getProducedMix.getMixMass > 0 _
                And productionCycle.getProducedMix.getVirginAsphaltConcrete.getTargetPercentage > 0 _
                And productionCycle.getProducedMix.getVirginAsphaltConcrete.getActualPercentage >= 0) Then

                moyennePondereEcartPourcentageBitume = (productionCycle.getProducedMix.getVirginAsphaltConcrete.getActualPercentage - _
                    productionCycle.getProducedMix.getVirginAsphaltConcrete.getTargetPercentage) * _
                    productionCycle.getProducedMix.getMixMass

                nombreDeCycle += 1
                masseTotal += productionCycle.getProducedMix.getMixMass
            End If
        Next

        moyennePondereEcartPourcentageBitume = moyennePondereEcartPourcentageBitume / (nombreDeCycle * masseTotal)

        Return moyennePondereEcartPourcentageBitume
    End Function

    '' TODO
    '' Vérifier si les pourcentages s'affiche dans le format ex: 22.0 ou plutot 0.22 

    '' TODO
    '' Vérifier si les temperature sont en unitée celcius

    Private Function getMoyennePondereEcartTemperature() As Double
        Dim moyennePondereEcartTemperature As Double = 0
        Dim nombreDeCycle As Integer = 0
        Dim masseTotal As Double = 0


        Dim productionCycleHybridList = New List(Of ProductionCycle)
        productionCycleHybridList.InsertRange(0, productionCycleContinuList)
        productionCycleHybridList.InsertRange(0, productionCycleDiscontinuList)


        For Each productionCycle As ProductionCycle In productionCycleHybridList

            If (productionCycle.getProducedMix.getMixMass > 0 _
                And productionCycle.getProducedMix.getTargetTemperature > 0 _
                And productionCycle.getProducedMix.getRecordedTemperature >= 0) Then

                moyennePondereEcartTemperature = (productionCycle.getProducedMix.getRecordedTemperature - _
                    productionCycle.getProducedMix.getTargetTemperature) * _
                    productionCycle.getProducedMix.getMixMass

                nombreDeCycle += 1
                masseTotal += productionCycle.getProducedMix.getMixMass
            End If

            moyennePondereEcartTemperature = moyennePondereEcartTemperature / (nombreDeCycle * masseTotal)
        Next

        Return moyennePondereEcartTemperature
    End Function

    Private Function getBitumeConsomme(producedMixList As List(Of ProducedMix)) As List(Of VirginAsphaltConcrete)
        Dim totalVirginAsphaltConcreteList = New List(Of VirginAsphaltConcrete)

        For Each producedMix As ProducedMix In producedMixList

            If totalVirginAsphaltConcreteList.Contains(producedMix.getVirginAsphaltConcrete) Then
                totalVirginAsphaltConcreteList.ElementAt(totalVirginAsphaltConcreteList.IndexOf(producedMix.getVirginAsphaltConcrete)).addMass(producedMix.getVirginAsphaltConcrete.getMass)
            Else
                totalVirginAsphaltConcreteList.Add(New VirginAsphaltConcrete(producedMix.getVirginAsphaltConcrete))
            End If
        Next

        Return totalVirginAsphaltConcreteList
    End Function


    Public Function getDonneeManuel() As ManualData

        Return donneeManuel
    End Function

    '' Production
    Private Function getDebutProduction() As Date

        Return donneeManuel.PRODUCTION_START_TIME
    End Function


    Private Function getFinProduction() As Date

        Return donneeManuel.PRODUCTION_END_TIME
    End Function

    '' Production
    Private Function calculateDureeProduction() As TimeSpan
        Dim dureeProduction As TimeSpan = getFinProduction.Subtract(getDebutProduction)

        Return dureeProduction
    End Function



    Private Function calculateNombreDeBrisInterne() As Integer
        Dim nombreDeBris As Integer = 0

        '' TODO 
        '' À faire

        '' Boucler sur le tableau des délais hybrid du rapport, puis compter combien
        '' d'entre eux sont des délais appartenent à la catégorie: Interne (avec bris)
        '' Code de délais de 1 à 17

        Return nombreDeBris
    End Function

    Private Sub setTotalMixMass()
        totalMixMass = getTotalMixMassContinu()
        totalMixMass += getTotalMixMassDiscontinu()
    End Sub

    Private Function getTotalMixMassContinu() As Double

        If totalMixMassContinu = -1 Then
            totalMixMassContinu = getMixMass(producedMixContinuList)
            Return totalMixMassContinu
        Else
            Return totalMixMassContinu
        End If
    End Function

    Private Function getTotalMixMassDiscontinu() As Double

        If totalMixMassDiscontinu = -1 Then
            totalMixMassDiscontinu = getMixMass(producedMixDiscontinuList)
            Return totalMixMassDiscontinu
        Else
            Return totalMixMassDiscontinu
        End If
    End Function

    Private Function getMixProductionTime(producedMixList As List(Of ProducedMix)) As TimeSpan
        Dim tempsDeProduction As TimeSpan

        For Each producedMix As ProducedMix In producedMixList
            tempsDeProduction += producedMix.getTempsDeProduction
        Next

        Return tempsDeProduction
    End Function

    Private Function getMixMass(producedMixList As List(Of ProducedMix)) As Double
        Dim mixTotalMass As Double

        For Each producedMix As ProducedMix In producedMixList
            mixTotalMass += producedMix.getMixMass
        Next

        Return mixTotalMass
    End Function

    Private Function getVirginAsphaltConcreteMass(virginAsphaltConcreteList As List(Of VirginAsphaltConcrete)) As Double
        Dim virginAsphaltConcreteTotalMass As Double

        For Each virginAsphaltConcrete As VirginAsphaltConcrete In virginAsphaltConcreteList
            virginAsphaltConcreteTotalMass += virginAsphaltConcrete.getMass
        Next

        Return virginAsphaltConcreteTotalMass
    End Function


    Private Function producedMixSortDecending(previousProducedMix_1 As ProducedMix, previousProductionModeMix_1 As String, previousProducedMix_2 As ProducedMix, previousProductionModeMix_2 As String,
                                              previousProducedMix_3 As ProducedMix, previousProductionModeMix_3 As String, productionMode As String, previouspProducedMixAutresList As List(Of ProducedMix),
                                              producedMixList As List(Of ProducedMix)) As ArrayList

        Dim producedMix_1 As ProducedMix = previousProducedMix_1
        Dim productionModeMix_1 As String = previousProductionModeMix_1
        Dim producedMix_2 As ProducedMix = previousProducedMix_2
        Dim productionModeMix_2 As String = previousProductionModeMix_2
        Dim producedMix_3 As ProducedMix = previousProducedMix_3
        Dim productionModeMix_3 As String = previousProductionModeMix_3
        Dim producedMixAutresList As List(Of ProducedMix) = previouspProducedMixAutresList
        Dim resultSort = New ArrayList
        '

        For Each producedMix As ProducedMix In producedMixList

            If (producedMix.getMixMass > 0) Then


                If IsNothing(producedMix_1) Then
                    producedMix_1 = producedMix
                    productionModeMix_1 = productionMode

                ElseIf IsNothing(producedMix_2) Then

                    If (producedMix.getMixMass > producedMix_1.getMixMass) Then
                        producedMix_2 = producedMix_1
                        productionModeMix_2 = productionModeMix_1
                        producedMix_1 = producedMix
                        productionModeMix_1 = productionMode
                    Else
                        producedMix_2 = producedMix
                        productionModeMix_2 = productionMode
                    End If

                ElseIf IsNothing(producedMix_3) Then

                    If (producedMix.getMixMass > producedMix_1.getMixMass) Then
                        producedMix_3 = producedMix_2
                        productionModeMix_3 = productionModeMix_2
                        producedMix_2 = producedMix_1
                        productionModeMix_2 = productionModeMix_1
                        producedMix_1 = producedMix
                        productionModeMix_1 = productionMode

                    ElseIf (producedMix.getMixMass > producedMix_2.getMixMass) Then
                        producedMix_3 = producedMix_2
                        productionModeMix_3 = productionModeMix_2
                        producedMix_2 = producedMix
                        productionModeMix_2 = productionMode
                    Else
                        producedMix_3 = producedMix
                        productionModeMix_3 = productionMode
                    End If

                ElseIf (producedMix.getMixMass > producedMix_1.getMixMass) Then
                    producedMixAutresList.Add(producedMix_3)
                    producedMix_3 = producedMix_2
                    productionModeMix_3 = productionModeMix_2
                    producedMix_2 = producedMix_1
                    productionModeMix_2 = productionModeMix_1
                    producedMix_1 = producedMix
                    productionModeMix_1 = productionMode

                ElseIf (producedMix.getMixMass > producedMix_2.getMixMass) Then

                    producedMixAutresList.Add(producedMix_3)
                    producedMix_3 = producedMix_2
                    productionModeMix_3 = productionModeMix_2
                    producedMix_2 = producedMix
                    productionModeMix_2 = productionMode

                ElseIf (producedMix.getMixMass > producedMix_3.getMixMass) Then

                    producedMixAutresList.Add(producedMix_3)
                    producedMix_3 = producedMix
                    productionModeMix_3 = productionMode

                Else
                    producedMixAutresList.Add(producedMix)
                End If
            End If

        Next

        resultSort.Add(producedMix_1)
        resultSort.Add(productionModeMix_1)
        resultSort.Add(producedMix_2)
        resultSort.Add(productionModeMix_2)
        resultSort.Add(producedMix_3)
        resultSort.Add(productionModeMix_3)
        resultSort.Add(producedMixAutresList)

        Return resultSort
    End Function

    Private Function producedMixSortDecending_Hybrid() As ArrayList
        Dim resultSort As ArrayList
        Dim producedMix_1 As ProducedMix
        Dim producedMix_2 As ProducedMix
        Dim producedMix_3 As ProducedMix

        Dim productionModeMix_1 As String = "Continu"
        Dim productionModeMix_2 As String = "Continu"
        Dim productionModeMix_3 As String = "Continu"

        Dim producedMixAutresList = New List(Of ProducedMix)
        Dim producedMixSortList = New List(Of ProducedMix)


        resultSort = producedMixSortDecending(Nothing, productionModeMix_1, Nothing, productionModeMix_2, Nothing, productionModeMix_3, "Continu", producedMixAutresList, producedMixContinuList)

        producedMix_1 = resultSort.Item(0)
        productionModeMix_1 = resultSort.Item(1)
        producedMix_2 = resultSort.Item(2)
        productionModeMix_2 = resultSort.Item(3)
        producedMix_3 = resultSort.Item(4)
        productionModeMix_3 = resultSort.Item(5)
        producedMixAutresList = resultSort.Item(6)

        resultSort = producedMixSortDecending(producedMix_1, productionModeMix_1, producedMix_2, productionModeMix_2, producedMix_3, productionModeMix_3, "Discontinu", producedMixAutresList, producedMixDiscontinuList)

        Return resultSort
    End Function

    Private Sub setDiscontinuDelayList()
        delayDiscontinuList = New List(Of Delay_1)
        delayDiscontinuList = delayFactory.createBatchDelayList(getDebutPeriode, getFinPeriode, productionCycleDiscontinuList, New List(Of String))
    End Sub

    Private Function getDiscontinuDelayList() As List(Of Delay_1)
        If IsNothing(delayDiscontinuList) Then
            setDiscontinuDelayList()
            Return delayDiscontinuList
        Else
            Return delayDiscontinuList
        End If

    End Function

    Private Sub setContinuDelayList()
        delayContinuList = New List(Of Delay_1)
        delayContinuList = delayFactory.createDrumDelayList(getDebutPeriode, getFinPeriode, productionCycleContinuList, getSourceFileComplementContinuList())

    End Sub

    Private Function getContinuDelayList() As List(Of Delay_1)
        If IsNothing(delayContinuList) Then

            setContinuDelayList()
            Return delayContinuList
        Else
            Return delayContinuList
        End If

    End Function

    Private Sub setHybridDelayList()
        delayHybridList = New List(Of Delay_1)
        delayHybridList = delayFactory.createHybridDelayList(getDebutPeriode, getFinPeriode, productionCycleContinuList, productionCycleDiscontinuList, getSourceFileComplementContinuList(), New List(Of String))
    End Sub

    Public Function getHybridDelayList() As List(Of Delay_1)
        If IsNothing(delayHybridList) Then
            setHybridDelayList()
            Return delayHybridList
        Else
            Return delayHybridList
        End If
    End Function


    Public Function getHybridDelayListJustifiable() As List(Of Delay_1)
        If IsNothing(delayHybridList) Then
            setHybridDelayList()
            Return delayFactory.removeDelayLowerThen(getHybridDelayList(), TimeSpan.FromMinutes(10))
        Else
            Return delayFactory.removeDelayLowerThen(getHybridDelayList(), TimeSpan.FromMinutes(10))
        End If
    End Function
    Private Function getNombreChangementMix(productionCycleList As List(Of ProductionCycle)) As Integer
        Dim nombreDeChangement As Integer = 0
        Dim previousProductionCycle As ProductionCycle

        previousProductionCycle = productionCycleList.Item(0)

        For Each productionCycle As ProductionCycle In productionCycleList
            If Not productionCycle.Equals(productionCycleList.Item(0)) Then

                If Not productionCycle.getProducedMix.getMixNumber.Equals(previousProductionCycle.getProducedMix.getMixNumber) Then
                    nombreDeChangement = nombreDeChangement + 1
                End If
                previousProductionCycle = productionCycle
            End If
        Next

        Return nombreDeChangement
    End Function

    Private Function calculateDureeTotal(delayList As List(Of Delay_1)) As TimeSpan
        Dim totalDelaisDuree = TimeSpan.Zero

        For Each delay As Delay_1 In delayList
            totalDelaisDuree += delay.getDuration
        Next

        Return totalDelaisDuree
    End Function

    Private Function getQuantiteEnSiloDebut() As Double

        Dim UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
        Dim INVALID_QUANTITY As Double = Double.NaN

        If Double.IsNaN(donneeManuel.SILO_QUANTITY_AT_START) Then
            Return INVALID_QUANTITY
        ElseIf Double.IsNegativeInfinity(donneeManuel.SILO_QUANTITY_AT_START) Then
            Return UNKNOWN_QUANTITY
        Else
            Return donneeManuel.SILO_QUANTITY_AT_START
        End If

    End Function

    Private Function getQuantiteEnSiloFin() As Double

        Dim UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
        Dim INVALID_QUANTITY As Double = Double.NaN

        If Double.IsNaN(donneeManuel.SILO_QUANTITY_AT_END) Then
            Return INVALID_QUANTITY
        ElseIf Double.IsNegativeInfinity(donneeManuel.SILO_QUANTITY_AT_END) Then
            Return UNKNOWN_QUANTITY
        Else
            Return donneeManuel.SILO_QUANTITY_AT_END
        End If

    End Function

    Private Function getQuantiteTotaleVendable() As Double

        Dim UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
        Dim INVALID_QUANTITY As Double = Double.NaN

        If Not (Double.IsNaN(getQuantiteEnSiloDebut) Or Double.IsNegativeInfinity(getQuantiteEnSiloDebut)) And _
           Not (Double.IsNaN(getQuantiteEnSiloFin) Or Double.IsNegativeInfinity(getQuantiteEnSiloFin)) Then

            getQuantiteEnSiloDebut.Equals(UNKNOWN_QUANTITY)
            getQuantiteEnSiloFin.Equals(UNKNOWN_QUANTITY)

            Return totalMixMass + getQuantiteEnSiloDebut() - getQuantiteEnSiloFin()
        Else
            Return totalMixMass
        End If

    End Function

    Private Function getQuantiteEnrobeRejete() As Double
        Return donneeManuel.REJECTED_MIX_QUANTITY
    End Function

    Private Function getPourcentageDeRejet() As Double

        Dim UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
        Dim INVALID_QUANTITY As Double = Double.NaN

        If Double.IsNaN(getQuantiteEnrobeRejete()) Then
            Return INVALID_QUANTITY
        ElseIf Double.IsNegativeInfinity(getQuantiteEnrobeRejete()) Then
            Return UNKNOWN_QUANTITY
        Else
            Return getQuantiteEnrobeRejete() / totalMixMass * 100
        End If

    End Function

    Private Function getPourcentageAvecGBR(productionCyclelist As List(Of ProductionCycle)) As Double
        Dim nombreTotalDeMix As Integer = 0
        Dim nombreTotalDeMixAvecGBR As Integer = 0

        For Each productionCycle As ProductionCycle In productionCyclelist

            If productionCycle.getProducedMix.getMixMass > 0 Then
                nombreTotalDeMix += 1

                If productionCycle.getProducedMix.getRapAsphaltConcreteList.Count > 0 Then
                    nombreTotalDeMixAvecGBR += 1
                End If

            End If


        Next

        Return nombreTotalDeMixAvecGBR / nombreTotalDeMix * 100
    End Function

    Private Function getPourcentageDeGBR(productionCyclelist As List(Of ProductionCycle)) As Double
        Dim massTotalDeMix As Double = 0
        Dim massTotalDeMixAvecGBR As Integer = 0

        For Each productionCycle As ProductionCycle In productionCyclelist

            If productionCycle.getProducedMix.getMixMass > 0 Then
                massTotalDeMix += productionCycle.getProducedMix.getMixMass

                If productionCycle.getProducedMix.getRapAsphaltConcreteList.Count > 0 Then

                    For Each rapAsphaltConcrete As RapAsphaltConcrete In productionCycle.getProducedMix.getRapAsphaltConcreteList
                        massTotalDeMixAvecGBR += rapAsphaltConcrete.getMass
                    Next
                End If

            End If
        Next

        Return massTotalDeMixAvecGBR / massTotalDeMix * 100
    End Function

    Private Function getQuantiteTotalePayable() As Double

        Dim UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
        Dim INVALID_QUANTITY As Double = Double.NaN


        If Not (Double.IsNaN(getQuantiteEnrobeRejete) Or Double.IsNegativeInfinity(getQuantiteEnrobeRejete)) Then

            Return getQuantiteTotaleVendable() - getQuantiteEnrobeRejete()

        Else

            Return getQuantiteTotaleVendable()
        End If

    End Function

    Private Function getQuantiteTotalVendue() As Double
        Return donneeManuel.WEIGHTED_QUANTITY
    End Function

    Private Function getPourcentageEcartQuantieVenduQuantiteRejete() As Double

        Dim UNKNOWN_QUANTITY As Double = Double.NegativeInfinity
        Dim INVALID_QUANTITY As Double = Double.NaN

        If Double.IsNaN(getQuantiteTotalVendue()) Then
            Return INVALID_QUANTITY
        ElseIf Double.IsNegativeInfinity(getQuantiteTotalVendue()) Then
            Return UNKNOWN_QUANTITY

        Else
            Return (getQuantiteTotalVendue() - getQuantiteTotalePayable()) / getQuantiteTotalVendue() * 100
        End If

    End Function

    Public Function getUsineOperator() As FactoryOperator
        Return donneeManuel.FACTORY_OPERATOR
    End Function

    Public Sub splitDelay(delay As Delay_1, splitTime As Date)

        If (Me.getHybridDelayList.Contains(delay)) Then

            Dim newDelays As List(Of Delay_1)

            newDelays = delayFactory.splitDelay(delay, splitTime)

            If (newDelays.Count > 0) Then
                Me.getHybridDelayList.InsertRange(Me.getHybridDelayList.IndexOf(delay), newDelays)
                Me.getHybridDelayList.Remove(delay)
            End If

        End If

    End Sub

    Public Sub mergeDelays(firstDelay As Delay_1, secondDelay As Delay_1)

        If Me.getHybridDelayList.Contains(firstDelay) And Me.getHybridDelayList.Contains(secondDelay) Then
            Dim newDelay As Delay_1
            newDelay = delayFactory.mergeDelays(firstDelay, secondDelay)

            If (Not IsNothing(newDelay)) Then

                Me.getHybridDelayList.Insert(Me.getHybridDelayList.IndexOf(firstDelay), newDelay)
                Me.getHybridDelayList.Remove(firstDelay)
                Me.getHybridDelayList.Remove(secondDelay)
            End If

        End If

    End Sub

    Public Function getProductionDate() As Date
        Return New Date(Me.getDebutPeriode.Year, Me.getDebutPeriode.Month, Me.getDebutPeriode.Day)
    End Function

    Public Function getReportComment() As String
        Return Me.commentaire
    End Function

    Public Sub setReportComment(reportComment As String)
        Me.commentaire = reportComment
    End Sub



    ''******************************************************************************************************************************************************************
    ''*
    ''*                                                 Sections des fonctions consernant les données des graphiques
    ''*
    ''******************************************************************************************************************************************************************

    ''******************************************************************************************************************************************************************
    ''*                                                                 Graphique des horaire en (h)
    ''******************************************************************************************************************************************************************

    ''' <summary>
    '''
    ''' </summary>
    ''' <returns>Retourne une liste de TimeSpan contenant 4 objets: dureeProduction as TimeSpan, dureeTotaleDelaisPause as TimeSpan,
    ''' dureeTotaleDelaisEntretien as TimeSpan, DureeTotaleDelais as TimeSpan  </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe DG01_ProductionDistributionGraphic</remarks>
    Public Function getProductionDistributionGraphicData() As List(Of TimeSpan)

        Dim productionDistributionGraphicData As List(Of TimeSpan) = New List(Of TimeSpan)

        productionDistributionGraphicData.Add(calculateDureeProduction())
        productionDistributionGraphicData.Add(calculeDureeTotaleDelaisPause())
        productionDistributionGraphicData.Add(calculateDureeTotaleDelaisEntretien())
        productionDistributionGraphicData.Add(calculateDureeTotal(getHybridDelayList))

        Return productionDistributionGraphicData
    End Function


    ''******************************************************************************************************************************************************************
    ''*                                                                 Graphique des Délais en (h)
    ''******************************************************************************************************************************************************************

    ''' <summary>
    ''' Fonction qui retourne la durée totale des délais classé par catégories de délais, pour les catégories: Interne (avec bris), Interne (sans bris), Externe (chantier), Externe (autres)  
    ''' </summary>
    ''' <returns>Retourne une liste de TimeSpan contenant 4 objets: dureeTotaleDelaisInterneAvecBris as TimeSpan, DureeTotalDelaisInterneSansBris as TimeSpan,
    ''' dureeTotalDelaisExterneChantier as TimeSpan, dureeTotalDelaisExterneAutres as TimeSpan  </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe DG02_DelaysDistributionGraphic</remarks>
    Public Function getDelaysDistributionGraphicData() As List(Of TimeSpan)

        Dim delaysDistributionGraphicData As List(Of TimeSpan) = New List(Of TimeSpan)

        delaysDistributionGraphicData.Add(calculerDureeTotaleDelaisInterneAvecBris())
        delaysDistributionGraphicData.Add(calculerDureeTotalDelaisInterneSansBris())
        delaysDistributionGraphicData.Add(calculerDureeTotalDelaisExterneChantier())
        delaysDistributionGraphicData.Add(calculerDureeTotalDelaisExterneAutres)

        Return delaysDistributionGraphicData
    End Function


    ''******************************************************************************************************************************************************************
    ''*                                                                 Graphique variation en (Celcius)
    ''******************************************************************************************************************************************************************

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>Retourne un ArrayList qui contient cinq objets Liste: cyclesDateTime as List(of Date),cyclesProductionSpeed as List(of Double),
    ''' virginAsphaltNameList as List(Of String), recordedTemperatureList as List (of Double), targetTemperatureList as List( of Double)
    ''' </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe MixTemperatureVariationGraphic</remarks>

    Public Function getMixTemperatureVariationGraphicData() As ArrayList
        Dim mixTemperatureVariationGraphicData As ArrayList = New ArrayList

        Dim cyclesDateTime As List(Of Date) = New List(Of Date)
        Dim cyclesProductionSpeed As List(Of Double) = New List(Of Double)
        Dim virginAsphaltNameList As List(Of String) = New List(Of String)
        Dim recordedTemperatureList As List(Of Double) = New List(Of Double)
        Dim targetTemperatureList As List(Of Double) = New List(Of Double)

        '' TODO faire une fonction pour getProductionCycleHybridList
        Dim productionCycleHybridList = New List(Of ProductionCycle)
        productionCycleHybridList.InsertRange(0, productionCycleContinuList)
        productionCycleHybridList.InsertRange(0, productionCycleDiscontinuList)

        '' TODO code dupliqué avec la méthode getValeursAberrantesBitume
        For Each productionCycle As ProductionCycle In productionCycleHybridList

            If (productionCycle.getProducedMix.getMixMass > 0) Then

                cyclesDateTime.Add(productionCycle.getEndOfCycle)
                cyclesProductionSpeed.Add(productionCycle.getProducedMix.getMixDebit)
                virginAsphaltNameList.Add(productionCycle.getProducedMix.getVirginAsphaltConcrete.getAsphaltName(getProductionDate))
                recordedTemperatureList.Add(productionCycle.getProducedMix.getRecordedTemperature)
                targetTemperatureList.Add(productionCycle.getProducedMix.getTargetTemperature)
            End If
        Next

        mixTemperatureVariationGraphicData.Add(cyclesDateTime)
        mixTemperatureVariationGraphicData.Add(cyclesProductionSpeed)
        mixTemperatureVariationGraphicData.Add(virginAsphaltNameList)
        mixTemperatureVariationGraphicData.Add(recordedTemperatureList)
        mixTemperatureVariationGraphicData.Add(targetTemperatureList)

        Return mixTemperatureVariationGraphicData
    End Function


    ''******************************************************************************************************************************************************************
    ''*                                                                 Graphique du tonnage accumulé en (T)
    ''******************************************************************************************************************************************************************

    ''' <summary>
    ''' Fonction retourne pour chacun des cycles disontinu de la période: et extrait pour chacun: la data de fin du cycle, la masse totale du cycle, le débit de production d'enrobé bitumineux.
    ''' </summary>
    ''' <returns>Retourne un ArrayList qui contient trois objets Liste: cyclesDateTime as List(of Date), cyclesMass as List(of Double), cyclesProductionSpeed as List(of Double)  </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe AccumulatedMassGraphic pour les cycles continu de la période</remarks>
    Public Function getAccumulatedMassGraphicDataDiscontinu() As ArrayList
        Return getAccumulatedMassGraphicData(productionCycleDiscontinuList)
    End Function

    ''' <summary>
    ''' Fonction retourne pour chacun des cycles continu de la période: fin du cycle, la masse totale du cycle, le débit de production d'enrobé bitumineux.
    ''' </summary>
    ''' <returns>Retourne un ArrayList qui contient trois objets Liste: cyclesDateTime as List(of Date), cyclesMass as List(of Double), cyclesProductionSpeed as List(of Double)  </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe AccumulatedMassGraphic pour les cycles discontinu de la période</remarks>
    Public Function getAccumulatedMassGraphicDataContinu() As ArrayList
        Return getAccumulatedMassGraphicData(productionCycleContinuList)
    End Function

    ''' <summary>
    ''' La fonction permet de boucler sur une collection d'objets ProductionCycle, puis d'en extraire pour chacun: la data de fin du cycle, la masse totale du cycle, le débit de production d'enrobé bitumineux.
    ''' </summary>
    ''' <param name="productionCycleList">Collection d'objets ProductionCycle</param>
    ''' <returns>Retourne un ArrayList qui contient trois objets Liste: cyclesDateTime as List(of Date), cyclesMass as List(of Double), cyclesProductionSpeed as List(of Double)  </returns>
    ''' <remarks>Fonction pour usage interne de la classe seulement  </remarks>
    Private Function getAccumulatedMassGraphicData(productionCycleList As List(Of ProductionCycle)) As ArrayList
        Dim graphicDate As ArrayList = New ArrayList

        Dim cyclesDateTime = New List(Of Date)
        Dim cyclesProductionSpeed = New List(Of Double)
        Dim cyclesMass = New List(Of Double)

        For Each productionCycle As ProductionCycle In productionCycleList
            cyclesDateTime.Add(productionCycle.getEndOfCycle)
            cyclesMass.Add(productionCycle.getProducedMix.getHotFeederMass() + productionCycle.getProducedMix.getVirginAsphaltConcrete.getMass())
            cyclesProductionSpeed.Add(productionCycle.getProducedMix.getMixDebit)
        Next

        graphicDate.Add(cyclesDateTime)
        graphicDate.Add(cyclesMass)
        graphicDate.Add(cyclesProductionSpeed)

        Return graphicDate
    End Function
    ''******************************************************************************************************************************************************************
    ''*                                                                 Graphique de la production en (T/h)
    ''******************************************************************************************************************************************************************

    ''' <summary>
    ''' Fonction retourne pour chacun des cycles discontinu de la période: fin du cycle,  le débit de production d'enrobé bitumineux.
    ''' </summary>
    ''' <returns>Retourne un ArrayList qui contient deux objets Liste: cyclesDateTime as List(of Date), cyclesProductionSpeed as List(of Double)  </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe ProductionSpeedGraphic pour les cycles continu de la période</remarks>
    Public Function getProductionSpeedGraphicDataDiscontinu() As ArrayList
        Return getProductionSpeedGraphicData(productionCycleDiscontinuList)
    End Function

    ''' <summary>
    ''' Fonction boucle sur la liste complète des cycles de la production continu, et extrait pour chacun: la data de fin du cycle, le débit de production d'enrobé bitumineux.
    ''' </summary>
    ''' <returns>Retourne un ArrayList qui contient deux objets Liste: cyclesDateTime as List(of Date), cyclesProductionSpeed as List(of Double)  </returns>
    ''' <remarks>La fonction est principalement utilité pour fournir les donnée d'entré à la classe ProductionSpeedGraphic pour les cycles discontinu de la période</remarks>
    Public Function getProductionSpeedGraphicDataContinu() As ArrayList
        Return getProductionSpeedGraphicData(productionCycleContinuList)
    End Function

    ''' <summary>
    ''' La fonction permet de boucler sur une collection d'objets ProductionCycle, puis d'en extraire pour chacun: la data de fin du cycle, le débit de production d'enrobé bitumineux.
    ''' </summary>
    ''' <param name="productionCycleList">Collection d'objets ProductionCycle</param>
    ''' <returns>Retourne un ArrayList qui contient deux objets Liste: cyclesDateTime as List(of Date), cyclesProductionSpeed as List(of Double)  </returns>
    ''' <remarks>Fonction pour usage interne de la classe seulement  </remarks>
    Private Function getProductionSpeedGraphicData(productionCycleList As List(Of ProductionCycle)) As ArrayList
        Dim graphicDate As ArrayList = New ArrayList

        Dim cyclesDateTime = New List(Of Date)
        Dim cyclesProductionSpeed = New List(Of Double)

        For Each productionCycle As ProductionCycle In productionCycleList
            cyclesDateTime.Add(productionCycle.getEndOfCycle)
            cyclesProductionSpeed.Add(productionCycle.getProducedMix.getMixDebit)
        Next

        graphicDate.Add(cyclesDateTime)
        graphicDate.Add(cyclesProductionSpeed)

        Return graphicDate
    End Function


    ''******************************************************************************************************************************************************************
    ''*
    ''*                                                       Sections des fonctions privées concernant les délais
    ''*
    ''******************************************************************************************************************************************************************

    ''' <summary>
    ''' Fonction qui boucle sur la liste complète des délais pour la période choisi et
    ''' retourne la somme des durées de chaque délais appartenent à la catégorie: Entretien    
    ''' </summary>
    ''' <returns>Somme des durées des délais pour la catégorie: Entretien</returns>
    ''' <remarks>La facon d'identifier la catégorie n'est pas idéal</remarks>
    Private Function calculateDureeTotaleDelaisEntretien() As TimeSpan
        Dim dureeTotaleDelaisEntretien As TimeSpan = TimeSpan.Zero

        For Each delay As Delay_1 In getHybridDelayList()
            If delay.getDelayCategorieName().Equals(DelayTypeNameConstant.delayTypeEntretien) Then
                dureeTotaleDelaisEntretien += delay.getDuration
            End If
        Next

        Return dureeTotaleDelaisEntretien
    End Function


    ''' <summary>
    ''' Fonction qui boucle sur la liste complète des délais pour la période choisi et
    ''' retourne la somme des durées de chaque délais appartenent à la catégorie: Pause    
    ''' </summary>
    ''' <returns>Somme des durées des délais pour la catégorie: Pause</returns>
    ''' <remarks>La facon d'identifier la catégorie n'est pas idéal</remarks>
    Private Function calculeDureeTotaleDelaisPause() As TimeSpan
        Dim dureeTotaleDelaisPause As TimeSpan = TimeSpan.Zero

        For Each delay As Delay_1 In getHybridDelayList()
            If delay.getDelayCategorieName().Equals(DelayTypeNameConstant.delayTypePause) Then
                dureeTotaleDelaisPause += delay.getDuration
            End If
        Next

        Return dureeTotaleDelaisPause
    End Function


    ''' <summary>
    ''' Fonction qui calcule la somme totale des durée de chaque délais pour la période choisi, appartenent à la catégorie: Interne (avec bris) ou à la catégorie: Interne (sans bris) 
    ''' </summary>
    ''' <returns>Duree totale des délais pour la catégorie Interne (avec bris) et Interne (sans bris)</returns>
    ''' <remarks></remarks>
    Private Function calculateDureeTotaleDelaisInterne() As TimeSpan
        Dim dureeTotaleDelaisInterne As TimeSpan = TimeSpan.Zero

        dureeTotaleDelaisInterne = calculerDureeTotaleDelaisInterneAvecBris() + calculerDureeTotalDelaisInterneSansBris()

        Return dureeTotaleDelaisInterne
    End Function

    ''' <summary>
    ''' Fonction qui boucle sur la liste complète des délais pour la période choisi et
    ''' retourne la somme des durée de chaque délais appartenent à la catégorie: Interne (avec bris)    
    ''' </summary>
    ''' <returns>Duree totale des délais pour la catégorie Interne (avec bris)</returns>
    ''' <remarks>La facon d'identifier la catégorie n'est pas idéal</remarks>
    Private Function calculerDureeTotaleDelaisInterneAvecBris() As TimeSpan
        Dim dureeTotaleDelaisInterneAvecBris As TimeSpan = TimeSpan.Zero

        For Each delay As Delay_1 In getHybridDelayList()
            If delay.getDelayCategorieName().Equals(DelayTypeNameConstant.delayTypeInterneAvecBris) Then
                dureeTotaleDelaisInterneAvecBris += delay.getDuration
            End If
        Next

        Return dureeTotaleDelaisInterneAvecBris
    End Function

    ''' <summary>
    ''' Fonction qui boucle sur la liste complète des délais pour la période choisi et
    ''' retourne la somme des durées de chaque délais appartenent à la catégorie: Interne (sans bris)    
    ''' </summary>
    ''' <returns>Somme des durées des délais pour la catégorie: Interne (sans bris)</returns>
    ''' <remarks>La facon d'identifier la catégorie n'est pas idéal</remarks>
    Private Function calculerDureeTotalDelaisInterneSansBris() As TimeSpan
        Dim dureeTotaleDelaisInterneSansBris As TimeSpan = TimeSpan.Zero

        For Each delay As Delay_1 In getHybridDelayList()
            If delay.getDelayCategorieName().Equals(DelayTypeNameConstant.delayTypeInterneSansBris) Then
                dureeTotaleDelaisInterneSansBris += delay.getDuration
            End If
        Next

        Return dureeTotaleDelaisInterneSansBris
    End Function

    ''' <summary>
    ''' Fonction qui boucle sur la liste complète des délais pour la période choisi et
    ''' retourne la somme des durées de chaque délais appartenent à la catégorie: Externe (chantier)    
    ''' </summary>
    ''' <returns>Somme des durées des délais pour la catégorie: Externe (chantier)</returns>
    ''' <remarks>La facon d'identifier la catégorie n'est pas idéal</remarks>
    Private Function calculerDureeTotalDelaisExterneChantier() As TimeSpan
        Dim dureeTotalDelaisExterneChantier As TimeSpan = TimeSpan.Zero

        For Each delay As Delay_1 In getHybridDelayList()
            If delay.getDelayCategorieName().Equals(DelayTypeNameConstant.delayTypeExterneChantier) Then
                dureeTotalDelaisExterneChantier += delay.getDuration
            End If
        Next

        Return dureeTotalDelaisExterneChantier
    End Function

    ''' <summary>
    ''' Fonction qui boucle sur la liste complète des délais pour la période choisi et
    ''' retourne la somme des durées de chaque délais appartenent à la catégorie: Externe (autres)    
    ''' </summary>
    ''' <returns>Somme des durées des délais pour la catégorie: Externe (autres)</returns>
    ''' <remarks>La facon d'identifier la catégorie n'est pas idéal</remarks>
    Private Function calculerDureeTotalDelaisExterneAutres() As TimeSpan
        Dim dureeTotalDelaisExterneAutres As TimeSpan = TimeSpan.Zero

        For Each delay As Delay_1 In getHybridDelayList()
            If delay.getDelayCategorieName().Equals(DelayTypeNameConstant.delayTypeExterneAutres) Then
                dureeTotalDelaisExterneAutres += delay.getDuration
            End If
        Next

        Return dureeTotalDelaisExterneAutres
    End Function
End Class
