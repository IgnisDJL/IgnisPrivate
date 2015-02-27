﻿Public Interface GlobalImportConstant

    ''**********************************************
    ''  getter du cycle de production
    ''**********************************************
    ReadOnly Property truckID() As String
    ReadOnly Property contractID() As String
    ReadOnly Property recycledID() As String
    ReadOnly Property time() As String
    ReadOnly Property siloFillingNumber() As String
    ReadOnly Property bagHouseDiff() As String
    ReadOnly Property dustRemovalDebit() As String


    ''**********************************************
    ''  getter pour les totaux de production
    ''**********************************************
    ReadOnly Property totalAggregateMass() As String
    ReadOnly Property totalAsphaltActualPercentage() As String
    ReadOnly Property totalAsphaltTargetPercentage() As String
    ReadOnly Property totalAsphaltMass() As String
    ReadOnly Property totalMass() As String

    ''**********************************************
    ''  getter du bitume utilisé
    ''**********************************************
    ReadOnly Property asphaltTankId() As String
    ReadOnly Property asphaltRecordedTemperature() As String
    ReadOnly Property asphaltDensity() As String


    ''**********************************************
    ''  getter de l'enrobé produit
    ''**********************************************
    ReadOnly Property mixDebit() As String
    ReadOnly Property mixName() As String
    ReadOnly Property mixNumber() As String
    ReadOnly Property mixRecordedTemperature() As String
    ReadOnly Property mixCounter() As String

    ''**********************************************
    ''  getter des bennes froides
    ''**********************************************
    ReadOnly Property coldFeederID() As String
    ReadOnly Property coldFeederTargetPercentage As String
    ReadOnly Property coldFeederActualPercentage As String
    '' TODO
    '' il est à voir si le débit est réellement un paramètre important à récûpérer, car il n'y a que les .log qui possaide cette information
    ReadOnly Property coldFeederDebit As String


    ReadOnly Property coldFeederMass As String
    ReadOnly Property coldFeederMaterialID As String
    ReadOnly Property coldFeederMoisturePercentage As String
    ReadOnly Property coldFeederRecycledID As String
    ReadOnly Property coldFeederRecycledActualPercentage As String

    ''**********************************************
    ''  getter des bennes chaudes
    ''**********************************************
    ReadOnly Property hotFeederID() As String
    ReadOnly Property hotFeederMaterialID As String
    ReadOnly Property hotFeederTargetPercentage As String
    ReadOnly Property hotFeederActualPercentage As String
    '' TODO
    '' il est à voir si le débit est réellement un paramètre important à récûpérer, car il n'y a que les .log qui possaide cette information
    ReadOnly Property hotFeederDebit As String

    ReadOnly Property hotFeederMass As String

    '' Les bennes chaudes sont justement chauffé pour éliminer l'humidité, donc elle n'ont pas de propriété humidité
    ''ReadOnly Property hotFeederMoisturePercentage As String

    ReadOnly Property hotFeederRecycledID As String
    ReadOnly Property hotFeederRecycledActualPercentage As String


End Interface
