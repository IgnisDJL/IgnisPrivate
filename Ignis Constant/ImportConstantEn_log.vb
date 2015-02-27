
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


    Public Overrides ReadOnly Property asphaltDensity As String
        Get
            Return asphaltDensity_En
        End Get
    End Property

    Public Overrides ReadOnly Property asphaltRecordedTemperature As String
        Get
            Return asphaltRecordedTemperature_En
        End Get
    End Property

    Public Overrides ReadOnly Property asphaltTankId As String
        Get
            Return asphaltTankId_En
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

    Public Overrides ReadOnly Property mixCounter As String
        Get
            Return mixCounter_En
        End Get
    End Property

    Public Overrides ReadOnly Property mixDebit As String
        Get
            Return mixDebit_En
        End Get
    End Property

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
            Return time_En_log
        End Get
    End Property

    Public Overrides ReadOnly Property contractID As String
        Get
            Return contractID_En
        End Get
    End Property

    '' Constante inutilisé pour un fichier log
    Public Overrides ReadOnly Property totalAsphaltActualPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    '' Constante inutilisé pour un fichier log
    Public Overrides ReadOnly Property totalAsphaltMass As String
        Get
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property truckID As String
        Get
            Return truckID_En
        End Get
    End Property

    Public Overrides ReadOnly Property totalAsphaltTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    ' Constante inutilisé pour un fichier log
    Public Overrides ReadOnly Property totalAggregateMass As String
        Get
            Return Nothing
        End Get
    End Property
    '' Constante inutilisé pour un fichier log
    Public Overrides ReadOnly Property totalMass As String
        Get
            Return Nothing
        End Get
    End Property

    'Public Overrides ReadOnly Property coldFeederActualPercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederMaterialID As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederDebit As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederID As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederMass As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederMoisturePercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederRecycledActualPercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederRecycledID As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property coldFeederTargetPercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederActualPercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederDebit As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederID As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederMass As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederMoisturePercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederRecycledActualPercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederRecycledID As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederTargetPercentage As String
    '    Get

    '    End Get
    'End Property

    'Public Overrides ReadOnly Property hotFeederMaterialID As String
    '    Get

    '    End Get
    'End Property
End Class

