
Public Class ImportConstantEn_log
    Inherits ImportConstant_log

    ''**********************************************
    ''  Constantes du cycle de production
    ''**********************************************
    Public Const siloFillingNumber_En = "Silo Filling:"
    Public Const bagHouseDiff_En = "Bh Diff:"
    Public Const dustRemovalDebit_En = "Dust Removal:"
    Public Const recycled_En = "Rap"
    Public Const truckID_En = "N/A"
    Public Const contractID_En = "N/A"
    Public Const dureeCycle_En = "-3"

    ''**********************************************
    ''  Constantes des bennes chaudes/froides
    ''**********************************************
    Public Const feederTargetPercentage_En = "SP %"
    Public Const feederActualPercentage_En = "Act %"
    Public Const feederDebit_En = "Tph"
    Public Const feederMass_En = "Tons"
    Public Const feederMoisturePercentage_En = "Mst%"


    ''**********************************************
    ''  Constantes des bennes froides
    ''**********************************************
    '' Id bennes froides
    Public Const coldFeederAggregateID_En = "Fdr"
    Public Const coldFeederRecycledAsphaltPercentage_En = "Rap Ac%"


    ''**********************************************
    ''  Constantes des bennes chaudes
    ''**********************************************
    '' Id bennes chaudes
    Public Const hotFeederAggregateID_En = "Virgin"
    Public Const hotFeederFillerID_En = "Filler"
    Public Const hotFeederAdditiveID_En = "Add"
    Public Const hotFeederChauxID_En = "N/A"


    ''**********************************************
    ''  Constantes du bitume utilisé
    ''**********************************************
    '' Id bitume utilisé
    Public Const virginAsphaltID_En = "Virgin"
    Public Const recycledAsphaltID_En = "Rap"
    Public Const totalAsphaltID_En = "Total"

    Public Const asphaltTankId_En = "A/C Tank:"
    Public Const asphaltRecordedTemperature_En = "Asphalt Temp :"
    Public Const asphaltDensity_En = "Ac Specific Gravity"


    ''**********************************************
    ''  Constantes de l'enrobé produit
    ''**********************************************
    Public Const mixCounter_En = "Mix Tons :"
    Public Const mixDebit_En = "Mix Tph"
    Public Const mixName_En = "Mix Name :"
    Public Const mixNumber_En = "Mix Number :"
    Public Const mixRecordedTemperature_En = "Mix Temp :"


    ''***********************************************************************************************************************************************************
    ''                                                              Fonction des getter
    ''***********************************************************************************************************************************************************

    Public Overrides ReadOnly Property recycledID As String
        Get
            Return recycled_En
        End Get
    End Property


    ''***********************************************
    ''              Production Day
    ''***********************************************

    Public Overrides ReadOnly Property totalMass As String
        Get
            Return "-3"
        End Get
    End Property


    ''***********************************************
    ''              Production Cycle
    ''***********************************************

    '' Cette information n'est pas diponible pour les fichiers sources .log
    Public Overrides ReadOnly Property manuel As String
        Get
            Return "-3"
        End Get
    End Property

    '' Cette information n'est pas diponible pour les fichiers sources .log
    Public Overrides ReadOnly Property dureeMalaxHumide As String
        Get
            Return "-3"
        End Get
    End Property
    '' Cette information n'est pas diponible pour les fichiers sources .log
    Public Overrides ReadOnly Property dureeMalaxSec As String
        Get
            Return "-3"
        End Get
    End Property

    '' Cette information n'est pas diponible pour les fichiers sources .log
    Public Overrides ReadOnly Property dureeCycle As String
        Get
            Return dureeCycle_En
        End Get
    End Property

    Public Overrides ReadOnly Property time As String
        Get
            Return time_En_log
        End Get
    End Property


    Public Overrides ReadOnly Property truckID As String
        Get
            Return truckID_En
        End Get
    End Property

    Public Overrides ReadOnly Property bagHouseDiff As String
        Get
            Return bagHouseDiff_En
        End Get
    End Property

    Public Overrides ReadOnly Property dustRemovalDebit As String
        Get
            Return dustRemovalDebit_En
        End Get
    End Property

    Public Overrides ReadOnly Property contractID As String
        Get
            Return contractID_En
        End Get
    End Property

    Public Overrides ReadOnly Property siloFillingNumber As String
        Get
            Return siloFillingNumber_En
        End Get
    End Property

    ''***********************************************
    ''                  Mix
    ''***********************************************
    Public Overrides ReadOnly Property mixName As String
        Get
            Return mixName_En
        End Get
    End Property

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


    ''***********************************************
    ''              Aggregate
    ''***********************************************

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

    ''***********************************************
    ''              AsphaltConcrete
    ''***********************************************


    '' Constante inutilisé pour un fichier log
    Public Overrides ReadOnly Property virginAsphaltConcreteTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteActualPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteDebit As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteMass As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteRecordedTemperature As String
        Get
            Return asphaltRecordedTemperature_En
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteDensity As String
        Get
            Return asphaltDensity_En
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteTankId As String
        Get
            Return asphaltTankId_En
        End Get
    End Property

    '' Cette information n'est pas diponible pour les fichiers sources .log
    Public Overrides ReadOnly Property virginAsphaltConcreteGrade As String
        Get
            Return "-3"
        End Get
    End Property

End Class

