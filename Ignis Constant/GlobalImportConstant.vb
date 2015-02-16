Public MustInherit Class GlobalImportConstant

    ''**********************************************
    ''  Constantes permettant de déterminer la 
    ''  langue d'un fichier sources
    ''**********************************************

    Public Const time_Fr_log = "Heure :"
    Public Const time_En_log = "Time :"

    Public Const asphaltTankId_Fr_csv = "Tank Bit"
    Public Const asphaltTankId_En_csv = "Type   Bitume"

    Public Const informationNonDisponible = "-3"
    Public Const erreurDansLeCode = "-2"
    Public Const valeurNonConforme = "-1"


    ''**********************************************
    ''  getter du cycle de production
    ''**********************************************
    Public MustOverride ReadOnly Property truckID() As String
    Public MustOverride ReadOnly Property contractID() As String
    Public MustOverride ReadOnly Property recycledID() As String
    Public MustOverride ReadOnly Property time() As String
    Public MustOverride ReadOnly Property siloFillingNumber() As String
    Public MustOverride ReadOnly Property bagHouseDiff() As String
    Public MustOverride ReadOnly Property dustRemovalDebit() As String


    ''**********************************************
    ''  getter pour les totaux de production
    ''**********************************************
    Public MustOverride ReadOnly Property totalAggregateMass() As String
    Public MustOverride ReadOnly Property totalAsphaltActualPercentage() As String
    Public MustOverride ReadOnly Property totalAsphaltMass() As String
    Public MustOverride ReadOnly Property totalMass() As String

    ''**********************************************
    ''  getter du bitume utilisé
    ''**********************************************
    Public MustOverride ReadOnly Property asphaltTankId() As String
    Public MustOverride ReadOnly Property asphaltRecordedTemperature() As String
    Public MustOverride ReadOnly Property asphaltDensity() As String


    ''**********************************************
    ''  getter de l'enrobé produit
    ''**********************************************
    Public MustOverride ReadOnly Property mixDebit() As String
    Public MustOverride ReadOnly Property mixName() As String
    Public MustOverride ReadOnly Property mixNumber() As String
    Public MustOverride ReadOnly Property mixRecordedTemperature() As String
    Public MustOverride ReadOnly Property mixCounter() As String

    ''**********************************************
    ''  getter des bennes froides
    ''**********************************************
    Public MustOverride ReadOnly Property coldFeederAggregateID() As String
    Public MustOverride ReadOnly Property coldFeederAggregateActualPercentage As String
    Public MustOverride ReadOnly Property coldFeederRecycledAggregateID As String
    Public MustOverride ReadOnly Property coldFeederRecycledAggregateActualPercentage As String

    ''**********************************************
    ''  getter des bennes chaudes
    ''**********************************************

    '' ID des bennes chaudes
    Public MustOverride ReadOnly Property hotFeederAggregateID() As String
    Public MustOverride ReadOnly Property hotFeederFillerID() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveID() As String
    Public MustOverride ReadOnly Property hotFeederChauxID() As String
    Public MustOverride ReadOnly Property hotFeederFillerActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederAggregateActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederChauxActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederFillerMass() As String
    Public MustOverride ReadOnly Property hotFeederAggregateMass() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveMass() As String
    Public MustOverride ReadOnly Property hotFeederChauxMass() As String
    Public MustOverride ReadOnly Property hotFeederDopeID() As String
    Public MustOverride ReadOnly Property hotFeederDopeActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederDopeMass() As String

    Public Class ImportConstantFr_log
        Inherits GlobalImportConstant

        ''**********************************************
        ''  Constantes du cycle de production
        ''**********************************************

        Public Const siloFillingNumber_Fr = "Silo Utilisé:"
        Public Const bagHouseDiff_Fr = "Dépress:"
        Public Const dustRemovalDebit_Fr = "Filler Recup:"
        Public Const recycled_Fr = "Rec"
        Public Const truckID_Fr = "N/A"
        Public Const contractID_Fr = "N/A"

        ''**********************************************
        ''  Constantes des bennes chaudes/froides
        ''**********************************************
        Public Const feederTargetPercentage_Fr = "Theo %"
        Public Const feederActualPercentage_Fr = "Act %"
        Public Const feederDebit_Fr = "T/h"
        Public Const feederMass_Fr = "Ton."
        Public Const feederMoisturePercentage_Fr = "Hum%"


        ''**********************************************
        ''  Constantes des bennes froides
        ''**********************************************
        '' Id bennes froides
        Public Const coldFeederAggregateID_Fr = "Dos"
        Public Const coldFeederRecycledAsphaltPercentage_Fr = "Bit.Rec%"

        ''**********************************************
        ''  Constantes des bennes chaudes
        ''**********************************************

        '' Id bennes chaudes
        Public Const hotFeederAggregateID_Fr = "Agr."
        Public Const hotFeederFillerID_Fr = "Filler"
        Public Const hotFeederAdditiveID_Fr = "Add"
        Public Const hotFeederChauxID_Fr = "N/A"


        ''**********************************************
        ''  Constantes du bitume utilisé
        ''**********************************************
        '' Id bitume utilisé
        Public Const virginAsphaltID_Fr = "Bitume"
        Public Const recycledAsphaltID_Fr = "Bit."
        Public Const totalAsphaltID_Fr = "Bitume"

        Public Const asphaltTankId_Fr = "Tank Bit:"
        Public Const asphaltRecordedTemperature_Fr = "Temp. Bitume :"
        Public Const asphaltDensity_Fr = "Mass. Vol. Bit:"


        ''**********************************************
        ''  Constantes de l'enrobé produit
        ''**********************************************
        Public Const mixCounter_Fr = "Ton. Frm :"
        Public Const mixDebit_Fr = "Débit"
        Public Const mixName_Fr = "Nom Frm. :"
        Public Const mixNumber_Fr = "No. Frm. :"
        Public Const mixRecordedTemperature_Fr = "Temp. Enr. :"

        Public Overrides ReadOnly Property asphaltDensity As String
            Get
                Return asphaltDensity_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property asphaltRecordedTemperature As String
            Get
                Return asphaltRecordedTemperature_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property asphaltTankId As String
            Get
                Return asphaltTankId_Fr
            End Get
        End Property

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
                Return contractID_Fr
            End Get
        End Property


        Public Overrides ReadOnly Property hotFeederAdditiveID As String
            Get
                Return hotFeederAdditiveID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAggregateID As String
            Get
                Return hotFeederAggregateID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederChauxID As String
            Get
                Return hotFeederChauxID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederFillerID As String
            Get
                Return hotFeederFillerID_Fr
            End Get
        End Property

        '' Fonction constante inutilisé pour un fichier log
        Public Overrides ReadOnly Property totalAsphaltActualPercentage As String
            Get
                Return "N/A"
            End Get
        End Property
        '' Fonction constante inutilisé pour un fichier log
        Public Overrides ReadOnly Property totalAsphaltMass As String
            Get
                Return "N/A"
            End Get
        End Property
        '' Information manquante pour un fichier log
        Public Overrides ReadOnly Property truckID As String
            Get
                Return truckID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederAggregateID As String
            Get
                Return coldFeederAggregateID_Fr
            End Get
        End Property





        Public Overrides ReadOnly Property hotFeederAggregateActualPercentage As String
            Get
                Return feederActualPercentage_Fr + hotFeederAggregateID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederFillerActualPercentage As String
            Get
                Return feederActualPercentage_Fr + hotFeederFillerID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederChauxActualPercentage As String
            Get
                Return feederActualPercentage_Fr + hotFeederChauxID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAdditiveActualPercentage As String
            Get
                Return feederActualPercentage_Fr + hotFeederAdditiveID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAggregateMass As String
            Get
                Return feederMass_Fr + hotFeederAggregateID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederFillerMass As String
            Get
                Return feederMass_Fr + hotFeederFillerID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederChauxMass As String
            Get
                Return feederMass_Fr + hotFeederChauxID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAdditiveMass As String
            Get
                Return feederMass_Fr + hotFeederAdditiveID_Fr
            End Get
        End Property
        '' Constante inutilisé pour un fichier log
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
        '' Constante inutilisé pour une production en continue
        Public Overrides ReadOnly Property hotFeederDopeActualPercentage As String
            Get
                Return Nothing
            End Get
        End Property
        '' Constante inutilisé pour une production en continue
        Public Overrides ReadOnly Property hotFeederDopeID As String
            Get
                Return Nothing
            End Get
        End Property
        '' Constante inutilisé pour une production en continue
        Public Overrides ReadOnly Property hotFeederDopeMass As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederAggregateActualPercentage As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederRecycledAggregateActualPercentage As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederRecycledAggregateID As String
            Get
                Return Nothing
            End Get
        End Property
    End Class

    Public Class ImportConstantEn_log
        Inherits GlobalImportConstant





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



        Public Overrides ReadOnly Property hotFeederAdditiveID As String
            Get
                Return hotFeederAdditiveID_En
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAggregateID As String
            Get
                Return hotFeederAggregateID_En
            End Get
        End Property


        Public Overrides ReadOnly Property hotFeederChauxID As String
            Get
                Return hotFeederChauxID_En
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederFillerID As String
            Get
                Return hotFeederFillerID_En
            End Get
        End Property
        '' Constante inutilisé pour un fichier log
        Public Overrides ReadOnly Property totalAsphaltActualPercentage As String
            Get
                Return "N/A"
            End Get
        End Property
        '' Constante inutilisé pour un fichier log
        Public Overrides ReadOnly Property totalAsphaltMass As String
            Get
                Return "N/A"
            End Get
        End Property

        Public Overrides ReadOnly Property truckID As String
            Get
                Return truckID_En
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederAggregateID As String
            Get
                Return coldFeederAggregateID_En
            End Get
        End Property




        Public Overrides ReadOnly Property hotFeederAggregateActualPercentage As String
            Get
                Return feederActualPercentage_En + hotFeederAggregateID_En
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederFillerActualPercentage As String
            Get
                Return feederActualPercentage_En + hotFeederFillerID_En
            End Get
        End Property


        Public Overrides ReadOnly Property hotFeederChauxActualPercentage As String
            Get
                Return feederActualPercentage_En + hotFeederChauxID_En
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAdditiveActualPercentage As String
            Get
                Return feederActualPercentage_En + hotFeederAdditiveID_En
            End Get
        End Property
        Public Overrides ReadOnly Property hotFeederFillerMass As String
            Get
                Return feederMass_En + hotFeederFillerID_En
            End Get
        End Property


        Public Overrides ReadOnly Property hotFeederChauxMass As String
            Get
                Return feederMass_En + hotFeederChauxID_En
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAdditiveMass As String
            Get
                Return feederMass_En + hotFeederAdditiveID_En
            End Get
        End Property

        Public Overrides ReadOnly Property hotFeederAggregateMass As String
            Get
                Return feederMass_En + hotFeederAggregateID_En
            End Get
        End Property

        '' Constante inutilisé pour un fichier log
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
        '' Constante inutilisé pour une production en continue
        Public Overrides ReadOnly Property hotFeederDopeActualPercentage As String
            Get
                Return Nothing
            End Get
        End Property
        '' Constante inutilisé pour une production en continue
        Public Overrides ReadOnly Property hotFeederDopeID As String
            Get
                Return Nothing
            End Get
        End Property
        '' Constante inutilisé pour une production en continue
        Public Overrides ReadOnly Property hotFeederDopeMass As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederAggregateActualPercentage As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederRecycledAggregateActualPercentage As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederRecycledAggregateID As String
            Get
                Return Nothing
            End Get
        End Property
    End Class

    Public Class ImportConstantFr_csv
        Inherits GlobalImportConstant

        ''**********************************************
        ''  Constantes du cycle de production
        ''**********************************************

        Public Const siloFillingNumber_Fr = "Silo     Stockage"
        Public Const recycled_Fr = "     Recycle "
        Public Const truckID_Fr = "Camion"
        Public Const contractID_Fr = "Contrat"
        Public Const time_Fr = "Heure"

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
        ''          Cycle de production
        ''**********************************************
        '' Information non disponible dans un fichier csv
        Public Overrides ReadOnly Property asphaltDensity As String
            Get
                Return "-3"
            End Get
        End Property

        Public Overrides ReadOnly Property asphaltTankId As String
            Get
                Return asphaltTankId_Fr_csv
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
        ''**********************************************

        ''Total asphalt

        Public Overrides ReadOnly Property totalAsphaltActualPercentage As String
            Get
                Return totalAsphaltActualPercentage_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property totalAsphaltMass As String
            Get
                Return totalAsphaltMass_Fr
            End Get
        End Property


        ''Total Aggregate

        Public Overrides ReadOnly Property totalAggregateMass As String
            Get
                Return totalAggregateMass_Fr
            End Get
        End Property

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
        Public Overrides ReadOnly Property coldFeederAggregateID As String
            Get
                Return coldFeederAggregateID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederAggregateActualPercentage As String
            Get
                Return coldFeederAggregateActualPercentage_Fr
            End Get
        End Property

        ''RecycledAggregate
        Public Overrides ReadOnly Property coldFeederRecycledAggregateID As String
            Get
                Return coldFeederRecycledAggregateID_Fr
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederRecycledAggregateActualPercentage As String
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
        ''          Bitume utilisé
        ''**********************************************

        Public Overrides ReadOnly Property asphaltRecordedTemperature As String
            Get
                Return asphaltRecordedTemperature_Fr
            End Get
        End Property


        ''**********************************************
        ''          Enrobé produit
        ''**********************************************

        '' Information non disponible dans ce fichier sour
        Public Overrides ReadOnly Property mixCounter As String
            Get
                Return "-3"
            End Get
        End Property

        '' Information non disponible dans ce fichier sour
        Public Overrides ReadOnly Property mixDebit As String
            Get
                Return "-3"
            End Get
        End Property

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


    End Class

    Public Class ImportConstantEn_csv
        Inherits GlobalImportConstant

        ''**********************************************
        ''  Constantes du cycle de production
        ''**********************************************
        Public Const siloFillingNumber_En = "Silo     Stockage"
        Public Const recycled_En = "     Recycle "
        Public Const truckID_En = "Camion"
        Public Const contractID_En = "Contrat"
        Public Const time_En = "Heure"

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
        Public Const asphaltRecordedTemperature_En = "Tmp. Bit"

        ''**********************************************
        ''  Constantes de l'enrobé produit
        ''**********************************************
        Public Const mixNumber_En = "Formule"
        Public Const mixRecordedTemperature_En = "Tmp. Enr"

        ''***********************************************************************************************************************************************************************************
        ''                                                                              Getter des constantes pour CSV Anglais
        ''***********************************************************************************************************************************************************************************

        ''**********************************************
        ''          Cycle de production
        ''**********************************************

        '' Information non disponible dans ce fichier source
        Public Overrides ReadOnly Property asphaltDensity As String
            Get
                Return "-3"
            End Get
        End Property

        Public Overrides ReadOnly Property asphaltTankId As String
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

        ''**********************************************
        ''          Totaux de production
        ''**********************************************

        ''Total asphalt
        Public Overrides ReadOnly Property totalAsphaltActualPercentage As String
            Get
                Return totalAsphaltActualPercentage_En
            End Get
        End Property

        Public Overrides ReadOnly Property totalAsphaltMass As String
            Get
                Return totalAsphaltMass_En
            End Get
        End Property

        ''Total Aggregate
        Public Overrides ReadOnly Property totalAggregateMass As String
            Get
                Return totalAggregateMass_En
            End Get
        End Property

        ''Total Mass
        Public Overrides ReadOnly Property totalMass As String
            Get
                Return totalMass_En
            End Get
        End Property


        ''**********************************************
        ''          Bennes froides
        ''**********************************************

        ''Aggregate
        Public Overrides ReadOnly Property coldFeederAggregateID As String
            Get
                Return coldFeederAggregateID_En
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederAggregateActualPercentage As String
            Get
                Return coldFeederAggregateActualPercentage_En
            End Get
        End Property

        ''RecycledAggregate
        Public Overrides ReadOnly Property coldFeederRecycledAggregateID As String
            Get
                Return coldFeederRecycledAggregateID_En
            End Get
        End Property

        Public Overrides ReadOnly Property coldFeederRecycledAggregateActualPercentage As String
            Get
                Return coldFeederRecycledAggregateActualPercentage_En
            End Get
        End Property


        ''**********************************************
        ''          Bennes chaudes
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
        ''          Bitume utilisé
        ''**********************************************

        Public Overrides ReadOnly Property asphaltRecordedTemperature As String
            Get
                Return asphaltRecordedTemperature_En
            End Get
        End Property


        ''**********************************************
        ''          Enrobé produit
        ''**********************************************
        '' Information non disponible dans ce fichier sour

        Public Overrides ReadOnly Property mixDebit As String
            Get
                Return "-3"
            End Get
        End Property

        '' Information non disponible dans ce fichier sour
        Public Overrides ReadOnly Property mixName As String
            Get
                Return "-3"

            End Get
        End Property

        '' Information non disponible dans ce fichier sour
        Public Overrides ReadOnly Property mixCounter As String
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
    End Class

End Class
