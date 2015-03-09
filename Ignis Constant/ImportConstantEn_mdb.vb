
Public Class ImportConstantEn_mdb
    Inherits ImportConstant_mdb

    ''Constante des types
    Public Const typeAsphalt = "7"
    Public Const typeAggregate = "1"
    Public Const typeRecycled = "8"

    ''*****************************************
    ''          Table Cycle
    ''*****************************************
    Public Const tableCycle = "Cycle"
    Public Const cycleCycleID = tableCycle + ".CycleID"
    Public Const cycleCommandeID = tableCycle + ".CommandeID"
    Public Const cycleDate = tableCycle + ".Date"
    Public Const cycleMixTemp = tableCycle + ".TemperatureBeton"
    Public Const cycleTempsMelangeFinalReel = tableCycle + ".TempsMelangeFinalReel"
    Public Const cycleTempsMelangeSecReel = tableCycle + ".TempsMelangeSecReel"

    ''*****************************************
    ''          Table Cycle Details
    ''*****************************************
    Public Const tableCycleDetails = "[Details Cycle]"
    Public Const detailsCycleQuantiteFormule = tableCycleDetails + ".QuantiteFormule"
    Public Const detailsCycleQuantiteDosage = tableCycleDetails + ".QuantiteDosage"
    Public Const detailsCycleQuantiteReel = tableCycleDetails + ".QuantiteReel"
    Public Const detailsCycleID = tableCycleDetails + ".CycleID"
    Public Const detailsTypeID = tableCycleDetails + ".TypeID"
    Public Const detailsTemperature = tableCycleDetails + ".Temperature"
    Public Const detailsEmplacement = tableCycleDetails + ".Emplacement"
    Public Const detailsDensite = tableCycleDetails + ".Densite"
    Public Const detailsHumidite = tableCycleDetails + ".Humidite"
    Public Const detailsNomMateriauID = tableCycleDetails + ".NomMateriauID"
    Public Const detailsNoSerieID = tableCycleDetails + ".NoSerieID"
    Public Const detailsManuelle = tableCycleDetails + ".Manuelle"

    ''*****************************************
    ''          Table Commande
    ''*****************************************
    Public Const tableCommande = "Commande"
    Public Const commandeCommandeID = tableCommande + ".CommandeID"
    Public Const commandeTruckID = tableCommande + ".SiloExpedition"
    Public Const commandeSiloFillingNumber = tableCommande + ".SiloExpedition"
    Public Const commandeNomJob = tableCommande + ".NomJob"
    Public Const commandeNomFormuleID = tableCommande + ".NomFormuleID"
    Public Const commandeDescriptionFormuleID = tableCommande + ".DescriptionFormuleID"
    Public Const commandeNewBitumeID = tableCommande + ".NewBitumeID"


    ''*****************************************
    ''          Table StringCache
    ''*****************************************

    Public Const tableStringCache = "StringCache"
    Public Const stringCacheStringCacheID = tableStringCache + ".StringCacheID"
    Public Const stringCacheStr = tableStringCache + ".Str"

    ''*****************************************
    ''          Table StringCache1
    ''*****************************************
    Public Const stringCache1 = "StringCache1"
    Public Const tableStringCache1 = tableStringCache + " as " + stringCache1
    Public Const stringCacheStringCacheID1 = stringCache1 + ".StringCacheID"
    Public Const stringCacheStr1 = stringCache1 + ".Str"

    ''*****************************************
    ''          Table StringCache2
    ''*****************************************
    Public Const stringCache2 = "StringCache2"
    Public Const tableStringCache2 = tableStringCache + " as " + stringCache2
    Public Const stringCacheStringCacheID2 = stringCache2 + ".StringCacheID"
    Public Const stringCacheStr2 = stringCache2 + ".Str"

    ''*****************************************
    ''          Table Recettes
    ''*****************************************
    Public Const tableRecette = "Recettes"
    Public Const recetteQuantite = "1000"
    Public Const recetteRecetteID = tableRecette + ".RecetteID"
    Public Const recetteNom = tableRecette + ".Nom"
    'Public Const RECIPE_DESC = "Description"
    Public Const recetteColdFeedRecipeID = tableRecette + ".ColdFeedRecipeID"

    ''*****************************************
    ''          Table ColdFeedsRecipesDetails
    ''*****************************************
    Public Const tableColdFeedsRecipesDetails = "ColdFeedsRecipesDetails"
    Public Const coldFeedsRecipesDetailsRecipeDetailsID = tableColdFeedsRecipesDetails + ".RecipeDetailsID"
    Public Const coldFeedsRecipesDetailsRecipeID = tableColdFeedsRecipesDetails + ".RecipeID"
    Public Const coldFeedsRecipesDetailsPercentage = tableColdFeedsRecipesDetails + ".Percentage"
    Public Const coldFeedsRecipesDetailsMateriauID = tableColdFeedsRecipesDetails + ".MateriauID"

    ''*****************************************
    ''          Table ColdFeedsRecipes
    ''*****************************************
    Public Const tableColdFeedsRecipes = "ColdFeedsRecipes"
    Public Const coldFeedsRecipesRecipeID = tableColdFeedsRecipes + ".RecipeID"
    Public Const coldFeedsRecipesName = tableColdFeedsRecipes + ".Name"

    ''*****************************************
    ''          Table Emplacement
    ''*****************************************
    Public Const tableEmplacement = "Emplacement"
    Public Const emplacementNoEmplacment = tableEmplacement + ".NoEmplacement"
    Public Const emplacementNom = tableEmplacement + ".Nom"

    ''*****************************************
    ''          Table Materiau
    ''*****************************************
    Public Const tableMateriau = "Materiau"
    Public Const materiauMateriauID = tableMateriau + ".MateriauID"
    Public Const materiauNom = tableMateriau + ".Nom"
    Public Const materiauTypeID = tableMateriau + ".TypeID"
    Public Const materiauNoSerie = tableMateriau + ".NoSerie"
    ''***********************************************************************************************************************************************************************************
    ''                                                                              Getter des constantes pour CSV Anglais
    ''***********************************************************************************************************************************************************************************


    ''**********************************************
    ''          Asphalt Concrete
    ''**********************************************

    Public Overrides ReadOnly Property virginAsphaltConcreteTargetPercentage As String
        Get
            Return detailsCycleQuantiteFormule + "/" + recetteQuantite
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteActualPercentage As String
        Get
            Return detailsCycleQuantiteFormule + "*(" + detailsCycleQuantiteReel + "+ 0.0000000000001)/(" + detailsCycleQuantiteDosage + "+ 0.0000000000001)/" + recetteQuantite
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteMass As String
        Get
            Return detailsCycleQuantiteReel
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteRecordedTemperature As String
        Get
            Return detailsTemperature
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteDensity As String
        Get
            Return detailsDensite
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteTankId As String
        Get
            Return detailsEmplacement
        End Get
    End Property



    ''***********************************************
    ''              Production Day
    ''***********************************************

    ''**********************************************
    ''          Cycle de production
    ''**********************************************

    Public Overrides ReadOnly Property manuel As String
        Get
            Return detailsManuelle
        End Get
    End Property

    Public Overrides ReadOnly Property dureeMalaxHumide As String
        Get
            Return cycleTempsMelangeFinalReel + " - " + cycleTempsMelangeSecReel
        End Get
    End Property

    Public Overrides ReadOnly Property dureeMalaxSec As String
        Get
            Return cycleTempsMelangeSecReel
        End Get
    End Property

    Public Overrides ReadOnly Property dureeCycle As String
        Get
            Return cycleTempsMelangeFinalReel
        End Get
    End Property

    ''Total Mass
    Public Overrides ReadOnly Property totalMass As String
        Get
            Return detailsCycleQuantiteReel
        End Get
    End Property


    '' Information non disponible dans ce fichier source
    Public Overrides ReadOnly Property bagHouseDiff As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    '' Information non disponible dans ce fichier source
    Public Overrides ReadOnly Property dustRemovalDebit As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property recycledID As String
        Get
            Return "RECYCLAGE"
        End Get
    End Property

    Public Overrides ReadOnly Property siloFillingNumber As String
        Get
            Return commandeSiloFillingNumber
        End Get
    End Property

    Public Overrides ReadOnly Property time As String
        Get
            Return "FORMAT(" + cycleDate + ",'hh:nn:ss am/pm')"
        End Get
    End Property

    Public Overrides ReadOnly Property contractID As String
        Get
            Return commandeNomJob
        End Get
    End Property

    Public Overrides ReadOnly Property truckID As String
        Get
            Return commandeTruckID
        End Get
    End Property

    ''**********************************************
    ''          Bennes froides
    ''**********************************************

    ''******************************ColdFeeder********************************

    '' Dans la base de donnée marcotte, il n'y a pas d'identifiant pour les bennes froides. L'information qui s'en rapproche le plus est le 
    '' materialID disponible dans la table coldFeedsRecipesDetails

    Public Overrides ReadOnly Property coldFeederID As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederMaterialID As String
        Get
            '' Retourne le id du matériau dans la benne froide
            Return coldFeedsRecipesDetailsMateriauID
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederTargetPercentage As String
        Get
            Return coldFeedsRecipesDetailsPercentage
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederActualPercentage As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederDebit As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederMoisturePercentage As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederMass As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    ''***************************ColdFeederRecycled*****************************

    Public Overrides ReadOnly Property coldFeederRecycledID As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    ''ColdFeederRecycled
    Public Overrides ReadOnly Property coldFeederRecycledActualPercentage As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property


    ''**********************************************
    ''          Bennes chaudes
    ''**********************************************

    Public Overrides ReadOnly Property hotFeederID As String
        Get
            Return emplacementNom
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederActualPercentage As String
        Get
            Return detailsCycleQuantiteFormule + "*(" + detailsCycleQuantiteReel + "+ 0.0000000000001)/(" + detailsCycleQuantiteDosage + "+ 0.0000000000001)/" + recetteQuantite
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDebit As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederMass As String
        Get
            Return detailsCycleQuantiteReel
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederMaterialID As String
        Get
            Return materiauMateriauID
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederTargetPercentage As String
        Get
            Return detailsCycleQuantiteFormule + "/" + recetteQuantite
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederRecycledActualPercentage As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederRecycledID As String
        Get
            '' Cette information n'est pas disponible dans les fichiers .mdb produit par marcotte
            Return "-3"
        End Get
    End Property




    ''**********************************************
    ''          Enrobé produit
    ''**********************************************
    '' Information non disponible dans ce fichier source

    Public Overrides ReadOnly Property mixName As String
        Get
            Return stringCacheStr

        End Get
    End Property


    '' Alias, numéro de formule
    Public Overrides ReadOnly Property mixNumber As String
        Get
            Return stringCacheStr
        End Get
    End Property

    Public Overrides ReadOnly Property mixRecordedTemperature As String
        Get
            Return cycleMixTemp
        End Get
    End Property


    ''**********************************************
    ''                  Aggregate
    ''**********************************************

    Public Overrides ReadOnly Property cycleAggregateTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property cycleAggregateActualPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property cycleAggregateDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property cycleAggregateMass As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteRank As String
        Get
            Return materiauNom
        End Get
    End Property
End Class