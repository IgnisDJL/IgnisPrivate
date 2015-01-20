Public Class WordReport_fr
    Implements WordReport

    Private FrenchLanguage As French

    Public Sub New(french As French)
        Me.FrenchLanguage = french
    End Sub

    Public ReadOnly Property FileName As String Implements WordReport.FileName
        Get
            Return "Rapport journalier"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageSection_Note As String Implements WordReport.AsphaltPercentageSection_Note
        Get
            Return "Ce tableau indique les données obtenues pour les trois enrobés les plus fréquemment produits."
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageSection_Table_OutControleMass As String Implements WordReport.AsphaltPercentageSection_Table_OutControleMass
        Get
            Return "Quantité hors contrôle"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageSection_Table_OutControlePercentage As String Implements WordReport.AsphaltPercentageSection_Table_OutControlePercentage
        Get
            Return "% hors contrôle"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageSection_Table_OutTolerancePercentage As String Implements WordReport.AsphaltPercentageSection_Table_OutTolerancePercentage
        Get
            Return "% hors tolérance"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageSection_Table_SetPointPercentage As String Implements WordReport.AsphaltPercentageSection_Table_SetPointPercentage
        Get
            Return "% bitume visé"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageSection_Title As String Implements WordReport.AsphaltPercentageSection_Title
        Get
            Return "Pourcentage de bitume"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_Comments As String Implements WordReport.EventsSummarySection_Table_Comments
        Get
            Return "Commentaires"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_Duration As String Implements WordReport.EventsSummarySection_Table_Duration
        Get
            Return "Durée"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_End As String Implements WordReport.EventsSummarySection_Table_End
        Get
            Return "Fin"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_EventNumber As String Implements WordReport.EventsSummarySection_Table_EventNumber
        Get
            Return "No"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_RecipeChange As String Implements WordReport.EventsSummarySection_Table_MixChange
        Get
            Return "Changement de formule"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_MixRecipeChange As String Implements WordReport.EventsSummarySection_Table_MixRecipeChange
        Get
            Return "Changement au mélange"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_Start As String Implements WordReport.EventsSummarySection_Table_Start
        Get
            Return "Début"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Title As String Implements WordReport.EventsSummarySection_Title
        Get
            Return "Sommaire des événements"
        End Get
    End Property

    Public ReadOnly Property EventsSummarySection_Table_StopsDuration As String Implements WordReport.EventsSummarySection_Table_StopsDuration
        Get
            Return "Durée des arrêts"
        End Get
    End Property

    Public ReadOnly Property FuelConsumptionSection_AverageConsumption As String Implements WordReport.FuelConsumptionSection_AverageConsumption
        Get
            Return "Consommation moyenne"
        End Get
    End Property

    Public ReadOnly Property FuelConsumptionSection_FuelConsumption As String Implements WordReport.FuelConsumptionSection_FuelConsumption
        Get
            Return "Consommation de carburant"
        End Get
    End Property

    Public ReadOnly Property FuelConsumptionSection_Title As String Implements WordReport.FuelConsumptionSection_Title
        Get
            Return "Consommation de carburant"
        End Get
    End Property

    Public ReadOnly Property MixSummarySection_BatchTitle As String Implements WordReport.MixSummarySection_BatchTitle
        Get
            Return "Sommaire de la production en discontinu"
        End Get
    End Property

    Public ReadOnly Property MixSummarySection_ContinuousTitle As String Implements WordReport.MixSummarySection_ContinuousTitle
        Get
            Return "Sommaire de la production en continu"
        End Get
    End Property

    Public ReadOnly Property MixSummarySection_SetPointRAP As String Implements WordReport.MixSummarySection_SetPointRAP
        Get
            Return "RAP"
        End Get
    End Property


    Public ReadOnly Property MixSummarySection_NoBatchMix As String Implements WordReport.MixSummarySection_NoBatchMix
        Get
            Return "Aucun enrobé produit en dicontinu"
        End Get
    End Property

    Public ReadOnly Property MixSummarySection_NoContinuousMix As String Implements WordReport.MixSummarySection_NoContinuousMix
        Get
            Return "Aucun enrobé produit en continu"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_openingHoursText As String Implements WordReport.ProductionSection_openingHoursText
        Get
            Return "Heures d'ouverture de l'usine"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_productionHoursText As String Implements WordReport.ProductionSection_productionHoursText
        Get
            Return "Heures de production de l'usine"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table1_Duration As String Implements WordReport.ProductionSection_Table1_Duration
        Get
            Return "Durée"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table1_MixSwitchAndStops As String Implements WordReport.ProductionSection_Table1_MixSwitchAndStops
        Get
            Return "Nbre chg. mélange / arrêts"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table1_ProductionMode As String Implements WordReport.ProductionSection_Table1_ProductionMode
        Get
            Return "Mode de production"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table1_ProductionSpeed As String Implements WordReport.ProductionSection_Table1_ProductionSpeed
        Get
            Return "Taux de production"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table1_TimePercentage As String Implements WordReport.ProductionSection_Table1_TimePercentage
        Get
            Return "Pourcentage du temps"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table2_MassLeft As String Implements WordReport.ProductionSection_Table2_MassLeft
        Get
            Return "Quantité restante silo"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table2_MassSold As String Implements WordReport.ProductionSection_Table2_MassSold
        Get
            Return "Quantité vendue"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table2_TotalProduction As String Implements WordReport.ProductionSection_Table2_TotalProduction
        Get
            Return "Total production"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Title As String Implements WordReport.ProductionSection_Title
        Get
            Return "Données de production"
        End Get
    End Property

    Public ReadOnly Property RecyclingSection_Table_AverageRAP As String Implements WordReport.RecyclingSection_Table_AverageRAP
        Get
            Return "RAP réel moyen"
        End Get
    End Property

    Public ReadOnly Property RecyclingSection_Table_RAPMass As String Implements WordReport.RecyclingSection_Table_RAPMass
        Get
            Return "Quantité RAP"
        End Get
    End Property

    Public ReadOnly Property RecyclingSection_Table_SetPointRAP As String Implements WordReport.RecyclingSection_Table_SetPointRAP
        Get
            Return "RAP visé"
        End Get
    End Property

    Public ReadOnly Property RecyclingSection_Title As String Implements WordReport.RecyclingSection_Title
        Get
            Return "Recyclé"
        End Get
    End Property

    Public ReadOnly Property ReportFooter_Right As String Implements WordReport.Footer_Right
        Get
            Return "Généré le"
        End Get
    End Property

    Public ReadOnly Property ReportFooter_Middle As String Implements WordReport.Footer_Middle
        Get
            Return "Rapport journalier de production - page"
        End Get
    End Property

    Public ReadOnly Property ReportHeader As String Implements WordReport.Header
        Get
            Return "Rapport journalier de production de la centrale"
        End Get
    End Property

    Public ReadOnly Property SignatureSection_Date As String Implements WordReport.SignatureSection_Date
        Get
            Return "Date"
        End Get
    End Property

    Public ReadOnly Property SignatureSection_Operator As String Implements WordReport.SignatureSection_Operator
        Get
            Return "Opérateur"
        End Get
    End Property

    Public ReadOnly Property SignatureSection_Signature As String Implements WordReport.SignatureSection_Signature
        Get
            Return "Signature"
        End Get
    End Property

    Public ReadOnly Property SignatureSection_Supervisor As String Implements WordReport.SignatureSection_Supervisor
        Get
            Return "Superviseur"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Title As String Implements WordReport.StopsSummarySection_Title
        Get
            Return "Sommaire des arrêts"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Table_Cause As String Implements WordReport.StopsSummarySection_Table_Cause
        Get
            Return "Cause / Action prise"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Table_Code As String Implements WordReport.StopsSummarySection_Table_Code
        Get
            Return "Code"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Table_Description As String Implements WordReport.StopsSummarySection_Table_Description
        Get
            Return "Description"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Table_Duration As String Implements WordReport.StopsSummarySection_Table_Duration
        Get
            Return "Durée"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Table_End As String Implements WordReport.StopsSummarySection_Table_End
        Get
            Return "Fin"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Table_Start As String Implements WordReport.StopsSummarySection_Table_Start
        Get
            Return "Début"
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Note As String Implements WordReport.TemperatureSection_Note
        Get
            Return "Ce tableau indique les données obtenues pour les trois bitumes les plus fréquemment utilisés."
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Table_OutLimitsMass As String Implements WordReport.TemperatureSection_Table_OutLimitsMass
        Get
            Return "Quantité d'enrobé hors limites"
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Table_OutLimitsPercentage As String Implements WordReport.TemperatureSection_Table_OutLimitsPercentage
        Get
            Return "% hors limites"
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Table_OverLimitPercentage As String Implements WordReport.TemperatureSection_Table_OverLimitPercentage
        Get
            Return "% supérieur à la limite"
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Table_SetPointTemperature As String Implements WordReport.TemperatureSection_Table_SetPointTemperature
        Get
            Return "Température visée"
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Table_UnderLimitPercentage As String Implements WordReport.TemperatureSection_Table_UnderLimitPercentage
        Get
            Return "% inférieur à la limite"
        End Get
    End Property

    Public ReadOnly Property TemperatureSection_Title As String Implements WordReport.TemperatureSection_Title
        Get
            Return "Température de production"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table2_ProductionMode As String Implements WordReport.ProductionSection_Table2_ProductionMode
        Get
            Return "Mode de production"
        End Get
    End Property

    Public ReadOnly Property ProductionSection_Table2_ProductionSpeed As String Implements WordReport.ProductionSection_Table2_ProductionSpeed
        Get
            Return "Production"
        End Get
    End Property

    Public ReadOnly Property AsphaltSummarySection_Table_AsphaltName As String Implements WordReport.AsphaltSummarySection_Table_AsphaltName
        Get
            Return "Grade de bitume"
        End Get
    End Property

    Public ReadOnly Property AsphaltSummarySection_Table_Tanks As String Implements WordReport.AsphaltSummarySection_Table_Tanks
        Get
            Return "Réservoirs"
        End Get
    End Property

    Public ReadOnly Property AsphaltSummarySection_Title As String Implements WordReport.AsphaltSummarySection_Title
        Get
            Return "Sommaires des bitumes"
        End Get
    End Property

    Public ReadOnly Property RejectsSummarySection_Table_Materials As String Implements WordReport.RejectsSummarySection_Table_Materials
        Get
            Return "Matériaux"
        End Get
    End Property

    Public ReadOnly Property RejectsSummarySection_Table_RejectedQuantity As String Implements WordReport.RejectsSummarySection_Table_RejectedQuantity
        Get
            Return "Quantité rejetée"
        End Get
    End Property

    Public ReadOnly Property RejectsSummarySection_Title As String Implements WordReport.RejectsSummarySection_Title
        Get
            Return "Sommaire des rejets"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_1 As String Implements WordReport.StopsSummarySection_Codes_1
        Get
            Return "Bennes froides"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_10 As String Implements WordReport.StopsSummarySection_Codes_10
        Get
            Return "Malaxeur"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_11 As String Implements WordReport.StopsSummarySection_Codes_11
        Get
            Return "Élévateur au silo"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_12 As String Implements WordReport.StopsSummarySection_Codes_12
        Get
            Return "Silo"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_13 As String Implements WordReport.StopsSummarySection_Codes_13
        Get
            Return "Contrôle informatique"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_14 As String Implements WordReport.StopsSummarySection_Codes_14
        Get
            Return "NA (Annulé)"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_15 As String Implements WordReport.StopsSummarySection_Codes_15
        Get
            Return "Produit hors tolérance"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_16 As String Implements WordReport.StopsSummarySection_Codes_16
        Get
            Return "Panne éléctrique H-Q"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_17 As String Implements WordReport.StopsSummarySection_Codes_17
        Get
            Return "Autres"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_18 As String Implements WordReport.StopsSummarySection_Codes_18
        Get
            Return "Syst. Recyclé"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_19 As String Implements WordReport.StopsSummarySection_Codes_19
        Get
            Return "Syst. Injection additifs"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_2 As String Implements WordReport.StopsSummarySection_Codes_2
        Get
            Return "Convoyeurs"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_20 As String Implements WordReport.StopsSummarySection_Codes_20
        Get
            Return "Balances"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_21 As String Implements WordReport.StopsSummarySection_Codes_21
        Get
            Return "Fourn. de matières premières"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_22 As String Implements WordReport.StopsSummarySection_Codes_22
        Get
            Return "Bouilloire"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_23 As String Implements WordReport.StopsSummarySection_Codes_23
        Get
            Return "Carburant"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_24 As String Implements WordReport.StopsSummarySection_Codes_24
        Get
            Return "Syst. hydrauliques"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_25 As String Implements WordReport.StopsSummarySection_Codes_25
        Get
            Return "Compresseur et syst. air"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_3 As String Implements WordReport.StopsSummarySection_Codes_3
        Get
            Return "Séchage des granulats"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_4 As String Implements WordReport.StopsSummarySection_Codes_4
        Get
            Return "Dépoussiéreur"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_5 As String Implements WordReport.StopsSummarySection_Codes_5
        Get
            Return "Élévateur"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_6 As String Implements WordReport.StopsSummarySection_Codes_6
        Get
            Return "Tamiseur"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_7 As String Implements WordReport.StopsSummarySection_Codes_7
        Get
            Return "Bennes chaudes"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_8 As String Implements WordReport.StopsSummarySection_Codes_8
        Get
            Return "Alimentation du filler"
        End Get
    End Property

    Public ReadOnly Property StopsSummarySection_Codes_9 As String Implements WordReport.StopsSummarySection_Codes_9
        Get
            Return "Alim. +chauff. du bitume"
        End Get
    End Property

End Class
