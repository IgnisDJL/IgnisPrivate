﻿
Public Class ImportConstantEn_csv
    Inherits ImportConstant_csv

    ''**********************************************
    ''  Constantes du cycle de production
    ''**********************************************
    Public Const siloFillingNumber_En = "Silo     Stockage"
    Public Const recycled_En = "     Recycle "
    Public Const truckID_En = "Camion"
    Public Const contractID_En = "Contrat"
    Public Const time_En = "Heure"
    Public Const dureeMalaxHumide_En = "Dur. mal. hum."
    Public Const dureeCycle_En = "Dur. Cycle"

    ''**********************************************
    ''  Constantes des totaux de production
    ''**********************************************

    Public Const totalMass_En = "Poids Total"
    Public Const totalAggregateMass_En = "Poids Agg Total"

    ''**********************************************
    ''  Constantes des bennes froides
    ''**********************************************

    ''Aggregate
    Public Const coldFeederAggregateID_En = "Doseur "
    Public Const coldFeederAggregateActualPercentage_En = "%       Doseur "

    ''RecycledAggregate
    Public Const coldFeederRecycledAggregateID_En = "Recycle "
    Public Const coldFeederRecycledAggregateActualPercentage_En = "%      Recycle "

    ''**********************************************
    ''  Constantes des bennes chaudes
    ''**********************************************

    ''Aggregate
    Public Const hotFeederAggregateID_En = "Agg "
    Public Const hotFeederAggregateActualPercentage_En = "% Agrégat "
    Public Const hotFeederAggregateMass_En = "% Agrégat "


    ''Filler
    Public Const hotFeederFillerID_En = "Fil    App"
    Public Const hotFeederFillerActualPercentage_En = "% Filler    Apport"
    Public Const hotFeederFillerMass_En = "Poids Fil    App"

    ''Additive
    Public Const hotFeederAdditiveID_En = "Add "
    Public Const hotFeederAdditiveActualPercentage_En = "%        Additif "
    Public Const hotFeederAdditiveMass_En = "Poids Add "

    ''Chaux
    Public Const hotFeederChauxID_En = "Chaux"
    Public Const hotFeederChauxMass_En = "Poids Chaux"
    Public Const hotFeederChauxActualPercentage_En = "-3"

    ''Dope
    Public Const hotFeederDopeID_En = "Dope "
    Public Const hotFeederDopeActualPercentage_En = "%         Dope "
    Public Const hotFeederDopeMass_En = "Poids Dope "


    ''**********************************************
    ''  Constantes du bitume utilisé
    ''**********************************************

    ''Asphalt
    Public Const totalAsphaltID_En = "Bit"
    Public Const totalAsphaltActualPercentage_En = "%        Bit"
    Public Const totalAsphaltMass_En = "Poids Bit"
    Public Const virginAsphaltConcreteRecordedTemperature_En = "Tmp. Bit"

    ''**********************************************
    ''  Constantes de l'enrobé produit
    ''**********************************************
    Public Const mixNumber_En = "Formule"
    Public Const mixRecordedTemperature_En = "Tmp. Enr"

    ''***********************************************************************************************************************************************************************************
    ''                                                                              Getter des constantes pour CSV Anglais
    ''***********************************************************************************************************************************************************************************

    ''**********************************************
    ''              Cycle de production
    ''**********************************************

    '' Information non disponible dans un fichier csv
    Public Overrides ReadOnly Property manuel As String
        Get
            Return Nothing
        End Get
    End Property

    '' Information non disponible dans ce fichier source
    Public Overrides ReadOnly Property dureeMalaxSec As String
        Get
            Return "-3"
        End Get
    End Property

    '' Information non disponible dans ce fichier source
    Public Overrides ReadOnly Property virginAsphaltConcreteDensity As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteTankId As String
        Get
            Return asphaltTankId_En_csv
        End Get
    End Property

    '' Information non disponible dans ce fichier source
    Public Overrides ReadOnly Property bagHouseDiff As String
        Get
            Return "-3"
        End Get
    End Property

    '' Information non disponible dans ce fichier source
    Public Overrides ReadOnly Property dustRemovalDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property recycledID As String
        Get
            Return recycled_En
        End Get
    End Property

    Public Overrides ReadOnly Property siloFillingNumber As String
        Get
            Return siloFillingNumber_En
        End Get
    End Property

    Public Overrides ReadOnly Property time As String
        Get
            Return time_En
        End Get
    End Property

    Public Overrides ReadOnly Property contractID As String
        Get
            Return contractID_En
        End Get
    End Property

    Public Overrides ReadOnly Property truckID As String
        Get
            Return truckID_En
        End Get
    End Property

    Public Overrides ReadOnly Property totalMass As String
        Get
            Return totalMass_En
        End Get
    End Property


    Public Overrides ReadOnly Property dureeCycle As String
        Get
            Return dureeCycle_En
        End Get
    End Property

    Public Overrides ReadOnly Property dureeMalaxHumide As String
        Get
            Return dureeMalaxHumide_En
        End Get
    End Property


    ''**********************************************
    ''              Bennes froides
    ''**********************************************

    ''Aggregate
    Public Overrides ReadOnly Property coldFeederID As String
        Get
            Return coldFeederAggregateID_En
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederActualPercentage As String
        Get
            Return coldFeederAggregateActualPercentage_En
        End Get
    End Property

    ''RecycledAggregate
    Public Overrides ReadOnly Property coldFeederRecycledID As String
        Get
            Return coldFeederRecycledAggregateID_En
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledActualPercentage As String
        Get
            Return coldFeederRecycledAggregateActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederDebit As String
        Get
            '' Cette information n'est pas disponible dans les fichiers.csv produit par minds
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederMass As String
        Get
            '' Cette information n'est pas disponible dans les fichiers.csv produit par minds
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederMoisturePercentage As String
        Get
            '' Cette information n'est pas disponible dans les fichiers.csv produit par minds
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederTargetPercentage As String
        Get
            '' Cette information n'est pas disponible dans les fichiers.csv produit par minds
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederMaterialID As String
        Get
            '' Cette information n'est pas disponible dans les fichiers.csv produit par minds
            Return "-3"
        End Get
    End Property

    ''**********************************************
    ''              Bennes chaudes
    ''**********************************************

    '' Additive
    Public Overrides ReadOnly Property hotFeederAdditiveID As String
        Get
            Return hotFeederAdditiveID_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveActualPercentage As String
        Get
            Return hotFeederAdditiveActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveMass As String
        Get
            Return hotFeederAdditiveMass_En
        End Get
    End Property

    '' Aggregate
    Public Overrides ReadOnly Property hotFeederAggregateID As String
        Get
            Return hotFeederAggregateID_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateActualPercentage As String
        Get
            Return hotFeederAggregateActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateMass As String
        Get
            Return hotFeederAggregateMass_En
        End Get
    End Property

    ''Chaux
    Public Overrides ReadOnly Property hotFeederChauxID As String
        Get
            Return hotFeederChauxID_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxActualPercentage As String
        Get
            Return hotFeederChauxActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxMass As String
        Get
            Return hotFeederChauxMass_En
        End Get
    End Property

    ''Filler
    Public Overrides ReadOnly Property hotFeederFillerID As String
        Get
            Return hotFeederFillerID_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerActualPercentage As String
        Get
            Return hotFeederFillerActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerMass As String
        Get
            Return hotFeederFillerMass_En
        End Get
    End Property

    ''Dope
    Public Overrides ReadOnly Property hotFeederDopeID As String
        Get
            Return hotFeederDopeID_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeActualPercentage As String
        Get
            Return hotFeederDopeActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeMass As String
        Get
            Return hotFeederDopeMass_En
        End Get
    End Property

    ''**********************************************
    ''              virginAsphaltConcrete
    ''**********************************************

    Public Overrides ReadOnly Property virginAsphaltConcreteRecordedTemperature As String
        Get
            Return virginAsphaltConcreteRecordedTemperature_En
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteActualPercentage As String
        Get
            Return totalAsphaltActualPercentage_En
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteMass As String
        Get
            Return totalAsphaltMass_En
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    '' Cette information n'est pas diponible pour les fichiers sources .csv
    Public Overrides ReadOnly Property virginAsphaltConcreteDebit As String
        Get
            Return "-3"
        End Get
    End Property

    '' Cette information n'est pas diponible pour les fichiers sources .csv
    Public Overrides ReadOnly Property virginAsphaltConcreteGrade As String
        Get
            Return "-3"
        End Get
    End Property

    ''**********************************************
    ''              Enrobé produit
    ''**********************************************
    '' Information non disponible dans ce fichier sour


    '' Information non disponible dans ce fichier sour
    Public Overrides ReadOnly Property mixName As String
        Get
            Return "-3"

        End Get
    End Property


    '' Alias, numéro de formule
    Public Overrides ReadOnly Property mixNumber As String
        Get
            Return mixNumber_En
        End Get
    End Property

    Public Overrides ReadOnly Property mixRecordedTemperature As String
        Get
            Return mixRecordedTemperature_En
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveMaterialID As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateMaterialID As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxMaterialID As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeMaterialID As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerMaterialID As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledMass As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledMaterialID As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledMoisturePercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property
End Class
