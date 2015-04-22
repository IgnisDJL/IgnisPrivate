
Public Enum EnumDailyReportTableauIndex

    '' Tableau Horaire 1.1
    ligne_Operation = 0
    ligne_Production = 1
    ligne_PostePesee = 2
    ligne_DelaisPauses = 3
    ligne_DelaisEntretiens = 4

    '' Colonne Tableau Horaire
    colonne_OpperationDebut = 0
    colonne_OpperationFin = 1
    colonne_OpperationDuree = 2

    colonne_ProductionDebut = 0
    colonne_ProductionFin = 1
    colonne_ProductionDuree = 2

    colonne_PostePeseeDebut = 0
    colonne_PostePeseeFin = 1
    colonne_PostePeseeDuree = 2

    colonne_PausesDuree = 0

    colonne_Entretiens = 0

    '' Tableau Enrobés 1.2
    ligne_Enrobe1 = 0
    ligne_Enrobe2 = 1
    ligne_Enrobe3 = 2
    ligne_EnrobeAutres = 3
    ligne_QuantiteTotaleProduite = 4
    ligne_QuantiteEnSiloDebut = 5
    ligne_QuantiteEnSiloFin = 6
    ligne_QuantiteTotaleVendable = 7
    ligne_RejetsEnrobes = 8
    ligne_QuantiteTotalePayable = 9
    ligne_QuantiteTotaleVendue = 10

    '' Colonne Tableau Enrobés
    colonne_Enrobe1NoFormule = 0
    colonne_Enrobe1NomEnrobe = 1
    colonne_Enrobe1Quantite = 2
    colonne_Enrobe1Production = 3
    colonne_Enrobe1ProductionMode = 4

    colonne_Enrobe2NoFormule = 0
    colonne_Enrobe2NomEnrobe = 1
    colonne_Enrobe2Quantite = 2
    colonne_Enrobe2Production = 3
    colonne_Enrobe2ProductionMode = 4

    colonne_Enrobe3NoFormule = 0
    colonne_Enrobe3NomEnrobe = 1
    colonne_Enrobe3Quantite = 2
    colonne_Enrobe3Production = 3
    colonne_Enrobe3ProductionMode = 4

    colonne_EnrobeAutreNombre = 0
    colonne_EnrobeAutreQuantite = 1
    colonne_EnrobeAutreProduction = 2

    colonne_QuantiteTotaleProduiteQuantite = 0
    colonne_QuantiteTotaleProduiteProduction = 1

    colonne_QuantiteEnSiloDebutQuantite = 0

    colonne_QuantiteEnSiloFinQuantite = 0

    colonne_QuantiteTotaleVendableQuantite = 0

    colonne_RejetsEnrobesQuantite = 0
    colonne_RejetsEnrobesPourcentageRejet = 1

    colonne_QuantiteTotalePayableQuantite = 0

    colonne_QuantiteTotaleVendueQuantite = 0
    colonne_QuantiteTotaleVenduePourcentageEcart = 1

    '' Tableau des modes de production 2.1
    ligne_Duree = 0
    ligne_PourcentageDuTemps = 1
    ligne_NombreDeChangements = 2
    ligne_QuantiteProduite = 3
    ligne_TauxDeProduction = 4

    '' Colonne Tableau des modes de production
    colonne_DureeContinu = 0
    colonne_DureeDiscontinu = 1
    colonne_DureeDelais = 2
  
    colonne_PourcentageDuTempsContinu = 0
    colonne_PourcentageDuTempsDiscontinu = 1
    colonne_PourcentageDuTempsDelais = 2

    colonne_NombreDeChangementsContinu = 0
    colonne_NombreDeChangementsDiscontinu = 1
    colonne_NombreDeChangementsDelais = 2

    colonne_QuantiteProduiteContinu = 0
    colonne_QuantiteProduiteDiscontinu = 1

    colonne_TauxDeProductionContinu = 0
    colonne_TauxDeProductionDiscontinu = 1


    '' Tableau des modes de production 2.2
    ligne_TempsTotalOperations = 0
    ligne_TempsNetOperations = 1
    ligne_ProductionNette = 2
    ligne_ProductionEfficace = 3
    ligne_ProductionEfficaceInterne = 4
    ligne_Delais = 5

    '' Colonne Tableau temps de production
    colonne_TempsTotalOperationsDuree = 0
    colonne_TempsNetOperationsDuree = 1
    colonne_ProductionNetteDuree = 2
    colonne_ProductionEfficaceDuree = 3
    colonne_ProductionEfficaceInterneDuree = 4
    colonne_DelaisDuree = 5


    '' Tableau des Delais 2.3

    ligne_NombreDeBris = 0
    ligne_Disponibilite = 1
    ligne_Utilisation = 2
    ligne_TempsEntrePannes = 3
    ligne_TempsPourReparer = 4

    '' Colonne Tableau des Delais

    colonne_NombreDeBris = 0
    colonne_Disponibilite = 0
    colonne_Utilisation = 0
    colonne_TempsEntrePannes = 0
    colonne_TempsPourReparer = 0

    '' Tableau des Bitumes consommés 3.1
    ligne_VirginAsphaltConcrete = 0


    '' Colonne des Bitumes consommés
    colonne_VirginAsphaltConcreteReservoir = 0
    colonne_VirginAsphaltConcreteGrade = 1
    colonne_VirginAsphaltConcreteQuantite = 2

    colonne_TotalBitumeConsommesQuantite = 0

    '' Tableau Écart Par rapport à la valeur visée 3.2
    ligne_BitumeEcart = 0
    ligne_TemperatureEcart = 1

    '' Colonne Écart Par rapport à la valeur visée
    colonne_BitumeEcartPourcentage = 0
    colonne_TemperatureEcart

    '' Tableau Écart Par rapport à la valeur visée 3.3
    ligne_VariationTemperature = 0

    '' Colonne Écart Par rapport à la valeur visée
    colonne_VariationTemperature = 0

    '' Tableau Taux de valeurs aberrantes 3.4
    ligne_PourcentageBitume = 0
    ligne_PourcentageTemperature = 1

    '' Colonne Taux de valeurs aberrantes
    colonne_PourcentageBitume = 0
    colonne_PourcentageTemperature = 0


    '' Tableau Carburants 4.1
    ligne_CarburantPrincipal = 0
    ligne_CarburantGazNatutel = 1

    '' Colonne Carburants
    colonne_NomCarburant = 0
    colonne_QuantiteConsomme = 1
    colonne_UniteQuantiteConsomme = 2
    colonne_TauxDeConsommation = 3
    colonne_UniteTauxDeConsommation = 4

    '' Tableau Carburants 4.1
    ligne_QuantiteRejete = 0
    ligne_TauxDeRejet = 1

    '' Colonne Carburants
    colonne_RejetGranulats = 0
    colonne_RejetFiller = 1
    colonne_RejetGBR = 2

    '' Tableau Delais 6.1


    '' Colonne Delais
    colonne_DelaisJustifiableDebut = 0
    colonne_DelaisJustifiableFin = 1
    colonne_DelaisJustifiableDuree = 2
    colonne_DelaisJustifiableName = 3
    colonne_DelaisJustifiableDescription = 4
    colonne_DelaisJustifiableCommentaire = 5

    colonne_DelaisNonJustifiableNombre = 0
    colonne_DelaisNonJustifiableDuree = 1

    colonne_DelaisTotalDuree = 0

    '' Tableau Sommaire Production Enrobe 7.1
    ligne_SommaireContinuEntete = 0

    '' Colonne Sommaire Production Enrobe
    ''colonne_SommaireContinuEnteteFeederName = 0

    colonne_EnrobeContinuFormule = 0
    colonne_EnrobeContinuName = 1
    colonne_EnrobeContinuGrade = 2
    colonne_EnrobeContinuRapVise = 3
    colonne_EnrobeContinuMasse = 4
    colonne_EnrobeContinuMasseBitume = 5
    ''colonne_EnrobeContinuMassFeeder = 6

    colonne_EnrobeContinuTotalMasse = 0
    colonne_EnrobeContinuTotalMasseBitume = 1


End Enum
