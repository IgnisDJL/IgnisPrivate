Public Class ImportConstantFr_log
    Inherits ImportConstant_log


    ''**********************************************
    '' Constantes du cycle de production
    ''**********************************************
    Public Const siloFillingNumber_Fr = "Silo Utilisé:"
    Public Const bagHouseDiff_Fr = "Dépress:"
    Public Const dustRemovalDebit_Fr = "Filler Recup:"
    Public Const recycled_Fr = "Rec"

    ''**********************************************
    '' Constantes des bennes chaudes/froides
    ''**********************************************
    Public Const feederTargetPercentage_Fr = "Theo %"
    Public Const feederActualPercentage_Fr = "Act %"
    Public Const feederDebit_Fr = "T/h"
    Public Const feederMass_Fr = "Ton."
    Public Const feederMoisturePercentage_Fr = "Hum%"

    ''**********************************************
    '' Constantes du bitume utilisé
    ''**********************************************
    '' Id bitume utilisé
    Public Const virginAsphaltID_Fr = "Bitume"
    Public Const recycledAsphaltID_Fr = "Bit."
    Public Const totalAsphaltID_Fr = "Bitume"
    Public Const asphaltTankId_Fr = "Tank Bit:"
    Public Const asphaltRecordedTemperature_Fr = "Temp. Bitume :"
    Public Const asphaltDensity_Fr = "Mass. Vol. Bit:"
    ''**********************************************
    '' Constantes de l'enrobé produit
    ''**********************************************
    Public Const mixCounter_Fr = "Ton. Frm :"
    Public Const mixDebit_Fr = "Débit"
    Public Const mixName_Fr = "Nom Frm. :"
    Public Const mixNumber_Fr = "No. Frm. :"
    Public Const mixRecordedTemperature_Fr = "Temp. Enr. :"


    ''***********************************************************************************************************************************************************
    ''                                                              Fonction des getter
    ''***********************************************************************************************************************************************************


    ''***********************************************
    ''                  Asphalt Concrete
    ''***********************************************

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

    Public Overrides ReadOnly Property virginAsphaltConcreteDensity As String
        Get
            Return asphaltDensity_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteRecordedTemperature As String
        Get
            Return asphaltRecordedTemperature_Fr
        End Get
    End Property

    Public Overrides ReadOnly Property virginAsphaltConcreteTankId As String
        Get
            Return asphaltTankId_Fr
        End Get
    End Property


    ''***********************************************
    ''                  Aggregate
    ''***********************************************

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

    Public Overrides ReadOnly Property cycleAggregateTargetPercentage As String
        Get
            Return "-3"
        End Get
    End Property

    ''***********************************************
    ''                 Production Day
    ''***********************************************

    '' TODO Cette information doit être calculé et cela est possible !
    Public Overrides ReadOnly Property totalMass As String
        Get
            Return "-3"
        End Get
    End Property

    ''***********************************************
    ''                 Production Cycle
    ''***********************************************

    Public Overrides ReadOnly Property bagHouseDiff As String
        Get
            Return bagHouseDiff_Fr
        End Get
    End Property
    Public Overrides ReadOnly Property dustRemovalDebit As String
        Get
            Return dustRemovalDebit_Fr
        End Get
    End Property
    Public Overrides ReadOnly Property mixCounter As String
        Get
            Return mixCounter_Fr
        End Get
    End Property
    Public Overrides ReadOnly Property mixDebit As String
        Get
            Return mixDebit_Fr
        End Get
    End Property
    Public Overrides ReadOnly Property mixName As String
        Get
            Return mixName_Fr
        End Get
    End Property
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
            Return time_Fr_log
        End Get
    End Property
    Public Overrides ReadOnly Property contractID As String
        Get
            '' Cette information n'est pas disponible pour un fichier .log produit par minds
            Return "-3"
        End Get
    End Property

    Public Overrides ReadOnly Property truckID As String
        Get
            '' Cette information n'est pas disponible pour un fichier .log produit par minds
            Return "-3"
        End Get
    End Property


    Public Overrides ReadOnly Property cycleAggregateMass As String
        Get
            '' Cette information n'est pas disponible pour un fichier .log produit par minds
            Return "-3"
        End Get
    End Property


End Class