
Public Class ImportConstantFr_csv
    Inherits ImportConstant_csv

    ''**********************************************
    ''  Constantes du cycle de production
    ''**********************************************

    Public Const siloFillingNumber_Fr = "Silo     Stockage"
    Public Const recycled_Fr = "     Recycle "
    Public Const truckID_Fr = "Camion"
    Public Const contractID_Fr = "Contrat"
    Public Const time_Fr = "Heure"
    Public Const dureeMalaxHumide_Fr = "Durée malax.     humide"
    Public Const dureeCycle_Fr = "Durée      Cycle"
    ''**********************************************
    ''  Constantes des totaux de production
    ''**********************************************

    ''Total Aggregate
    Public Const totalAggregateMass_Fr = "Poids Agg Total"

    ''Total Mass
    Public Const totalMass_Fr = "Poids       Total"



    ''**********************************************
    ''  Constantes des bennes froides
    ''**********************************************

    ''Aggregate
    Public Const coldFeederAggregateID_Fr = "Doseur "
    Public Const coldFeederRecycledAggregateID_Fr = "Recycle "

    ''RecycledAggregate
    Public Const coldFeederRecycledAggregateActualPercentage_Fr = "%      Recycle "
    Public Const coldFeederAggregateActualPercentage_Fr = "%       Doseur "

    ''**********************************************
    ''  Constantes des bennes chaudes
    ''**********************************************

    ''Aggregate
    Public Const hotFeederAggregateID_Fr = "Agrégat "
    Public Const hotFeederAggregateActualPercentage_Fr = "% Agrégat "
    Public Const hotFeederAggregateMass_Fr = "Poids Agrégat "


    ''Filler
    Public Const hotFeederFillerID_Fr = "Filler    Apport"
    Public Const hotFeederFillerMass_Fr = "Poids Filler    Apport"
    Public Const hotFeederFillerActualPercentage_Fr = "% Filler    Apport"


    ''Additive
    Public Const hotFeederAdditiveID_Fr = "Add "
    Public Const hotFeederAdditiveActualPercentage_Fr = "%        Additif "
    Public Const hotFeederAdditiveMass_Fr = "Poids Add "

    ''Chaux
    Public Const hotFeederChauxID_Fr = "Chaux"
    Public Const hotFeederChauxActualPercentage_Fr = "-3"
    Public Const hotFeederChauxMass_Fr = "Poids Chaux"

    ''Dope
    Public Const hotFeederDopeID_Fr = "Dope "
    Public Const hotFeederDopeActualPercentage_Fr = "%         Dope "
    Public Const hotFeederDopeMass_Fr = "Poids      Dope "

    ''**********************************************
    ''  Constantes du bitume utilisé
    ''**********************************************

    ''Asphalt

    Public Const totalAsphaltMass_Fr = "Poids      Bitume"
    Public Const totalAsphaltActualPercentage_Fr = "%         Bitume"
    Public Const asphaltRecordedTemperature_Fr = "Temp.     Bitume"
    'Public Const totalAsphaltID_Fr = "Bitume"


    ''**********************************************
    ''  Constantes de l'enrobé produit
    ''**********************************************
    Public Const mixNumber_Fr = "Formule"
    Public Const mixRecordedTemperature_Fr = "Temp.    Enrobés"

    ''***********************************************************************************************************************************************************************************
    ''                                                                              Getter des constantes pour CSV Anglais
    ''***********************************************************************************************************************************************************************************


    ''**********************************************
    ''              Asphalt Concrete
    ''**********************************************

    Public Overrides ReadOnly Property virginAsphaltConcreteTargetPercentage As String
        Get
            '' Cette information n'est pas disponible pour un fichier .csv produit par minds
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteActualPercentage As String
        Get
            Return totalAsphaltActualPercentage_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteMass As String
        Get
            Return totalAsphaltMass_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteRecordedTemperature As String
        Get
            Return asphaltRecordedTemperature_Fr
        End Get
    End Property

    '' TODO
    '' À vérifier ci cela est exacte

    Public Overrides ReadOnly Property virginAsphaltConcreteDensity As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteTankId As String
        Get
            Return asphaltTankId_Fr_csv
        End Get
    End Property


    ''***********************************************
    ''              Production Cycle
    ''***********************************************

    '' Information non disponible dans un fichier csv
    Public Overrides ReadOnly Property manuel As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property dureeCycle As String
        Get
            Return dureeCycle_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property dureeMalaxHumide As String
        Get
            Return dureeMalaxHumide_Fr
        End Get
    End Property

    '' Information non disponible dans un fichier csv
    Public Overrides ReadOnly Property dureeMalaxSec As String
        Get
            Return "-3"
        End Get
    End Property

    '' Information non disponible dans un fichier csv
    Public Overrides ReadOnly Property bagHouseDiff As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property contractID As String
        Get
            Return contractID_Fr
        End Get
    End Property

    '' Information non disponible dans un fichier csv
    Public Overrides ReadOnly Property dustRemovalDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property truckID As String
        Get
            Return truckID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property recycledID As String
        Get
            Return recycled_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property siloFillingNumber As String
        Get
            Return siloFillingNumber_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property time As String
        Get
            Return time_Fr
        End Get
    End Property

    ''**********************************************
    ''          Totaux de production

    ''Total Mass
    Public Overrides ReadOnly Property totalMass As String
        Get
            Return totalMass_Fr
        End Get
    End Property

    ''**********************************************
    ''          Bennes froides
    ''**********************************************
    ''Aggregate
    Public Overrides ReadOnly Property coldFeederID As String
        Get
            Return coldFeederAggregateID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederActualPercentage As String
        Get
            Return coldFeederAggregateActualPercentage_Fr
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

    ''RecycledAggregate
    Public Overrides ReadOnly Property coldFeederRecycledID As String
        Get
            Return coldFeederRecycledAggregateID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property coldFeederRecycledActualPercentage As String
        Get
            Return coldFeederRecycledAggregateActualPercentage_Fr
        End Get
    End Property


    ''**********************************************
    ''          Bennes chaudes
    ''**********************************************

    '' Additive
    Public Overrides ReadOnly Property hotFeederAdditiveID As String
        Get
            Return hotFeederAdditiveID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveActualPercentage As String
        Get
            Return hotFeederAdditiveActualPercentage_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAdditiveMass As String
        Get
            Return hotFeederAdditiveMass_Fr
        End Get
    End Property

    '' Aggregate
    Public Overrides ReadOnly Property hotFeederAggregateID As String
        Get
            Return hotFeederAggregateID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateActualPercentage As String
        Get
            Return hotFeederAggregateActualPercentage_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederAggregateMass As String
        Get
            Return hotFeederAggregateMass_Fr
        End Get
    End Property

    ''Chaux
    Public Overrides ReadOnly Property hotFeederChauxID As String
        Get
            Return hotFeederChauxID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxActualPercentage As String
        Get
            Return hotFeederChauxActualPercentage_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederChauxMass As String
        Get
            Return hotFeederChauxMass_Fr
        End Get
    End Property

    ''Filler
    Public Overrides ReadOnly Property hotFeederFillerID As String
        Get
            Return hotFeederFillerID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerActualPercentage As String
        Get
            Return hotFeederFillerActualPercentage_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederFillerMass As String
        Get
            Return hotFeederFillerMass_Fr
        End Get
    End Property

    ''Dope
    Public Overrides ReadOnly Property hotFeederDopeID As String
        Get
            Return hotFeederDopeID_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeActualPercentage As String
        Get
            Return hotFeederDopeActualPercentage_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property hotFeederDopeMass As String
        Get
            Return hotFeederDopeMass_Fr
        End Get
    End Property

    ''**********************************************
    ''          Enrobé produit
    ''**********************************************

    '' Information non disponible dans ce fichier sour
    Public Overrides ReadOnly Property mixName As String
        Get
            Return "-3"
        End Get
    End Property

    '' Alias, numéro de formule
    Public Overrides ReadOnly Property mixNumber As String
        Get
            Return mixNumber_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property mixRecordedTemperature As String
        Get
            Return mixRecordedTemperature_Fr
        End Get
    End Property

    '' Cette information n'est pas diponible pour les fichiers sources .csv
    Public Overrides ReadOnly Property virginAsphaltConcreteGrade As String
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
End Class
