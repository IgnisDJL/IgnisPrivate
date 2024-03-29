﻿Public MustInherit Class ImportConstant_mdb
    Implements GlobalImportConstant

    ''**********************************************
    ''  getter du cycle de production
    ''**********************************************
    Public MustOverride ReadOnly Property truckID() As String Implements GlobalImportConstant.truckID
    Public MustOverride ReadOnly Property contractID() As String Implements GlobalImportConstant.contractID
    Public MustOverride ReadOnly Property recycledID() As String Implements GlobalImportConstant.recycledID
    Public MustOverride ReadOnly Property time() As String Implements GlobalImportConstant.time
    Public MustOverride ReadOnly Property siloFillingNumber() As String Implements GlobalImportConstant.siloFillingNumber
    Public MustOverride ReadOnly Property bagHouseDiff() As String Implements GlobalImportConstant.bagHouseDiff
    Public MustOverride ReadOnly Property dustRemovalDebit() As String Implements GlobalImportConstant.dustRemovalDebit
    Public MustOverride ReadOnly Property totalMass() As String Implements GlobalImportConstant.totalMass
    Public MustOverride ReadOnly Property dureeCycle() As String Implements GlobalImportConstant.dureeCycle
    Public MustOverride ReadOnly Property dureeMalaxHumide As String Implements GlobalImportConstant.dureeMalaxHumide
    Public MustOverride ReadOnly Property dureeMalaxSec As String Implements GlobalImportConstant.dureeMalaxSec
    Public MustOverride ReadOnly Property manuel As String Implements GlobalImportConstant.manuel

    ''**********************************************
    ''  getter virginAsphaltConcrete
    ''**********************************************
    Public MustOverride ReadOnly Property virginAsphaltConcreteTargetPercentage() As String Implements GlobalImportConstant.virginAsphaltConcreteTargetPercentage
    Public MustOverride ReadOnly Property virginAsphaltConcreteActualPercentage() As String Implements GlobalImportConstant.virginAsphaltConcreteActualPercentage
    Public MustOverride ReadOnly Property virginAsphaltConcreteDebit() As String Implements GlobalImportConstant.virginAsphaltConcreteDebit
    Public MustOverride ReadOnly Property virginAsphaltConcreteMass() As String Implements GlobalImportConstant.virginAsphaltConcreteMass
    Public MustOverride ReadOnly Property virginAsphaltConcreteTankId() As String Implements GlobalImportConstant.virginAsphaltConcreteTankId
    Public MustOverride ReadOnly Property virginAsphaltConcreteRecordedTemperature() As String Implements GlobalImportConstant.virginAsphaltConcreteRecordedTemperature
    Public MustOverride ReadOnly Property virginAsphaltConcreteDensity() As String Implements GlobalImportConstant.virginAsphaltConcreteDensity
    Public MustOverride ReadOnly Property virginAsphaltConcreteRank() As String Implements GlobalImportConstant.virginAsphaltConcreteGrade

    ''**********************************************
    ''  getter de l'enrobé produit
    ''**********************************************
    Public MustOverride ReadOnly Property mixName() As String Implements GlobalImportConstant.mixName
    Public MustOverride ReadOnly Property mixNumber() As String Implements GlobalImportConstant.mixNumber
    Public MustOverride ReadOnly Property mixRecordedTemperature() As String Implements GlobalImportConstant.mixRecordedTemperature

    ''**********************************************
    ''  getter des bennes froides
    ''**********************************************

    Public MustOverride ReadOnly Property coldFeederID() As String Implements GlobalImportConstant.coldFeederID
    Public MustOverride ReadOnly Property coldFeederTargetPercentage As String Implements GlobalImportConstant.coldFeederTargetPercentage
    Public MustOverride ReadOnly Property coldFeederActualPercentage As String Implements GlobalImportConstant.coldFeederActualPercentage
    Public MustOverride ReadOnly Property coldFeederDebit As String Implements GlobalImportConstant.coldFeederDebit
    Public MustOverride ReadOnly Property coldFeederMass As String Implements GlobalImportConstant.coldFeederMass
    Public MustOverride ReadOnly Property coldFeederMoisturePercentage As String Implements GlobalImportConstant.coldFeederMoisturePercentage
    Public MustOverride ReadOnly Property coldFeederRecycledID As String Implements GlobalImportConstant.coldFeederRecycledID
    Public MustOverride ReadOnly Property coldFeederRecycledActualPercentage As String Implements GlobalImportConstant.coldFeederRecycledActualPercentage
    Public MustOverride ReadOnly Property coldFeederMaterialID As String Implements GlobalImportConstant.coldFeederMaterialID

    ''**********************************************
    ''  getter des bennes chaudes
    ''**********************************************
    '' ID des bennes chaudes
    Public MustOverride ReadOnly Property hotFeederID() As String Implements GlobalImportConstant.hotFeederID
    Public MustOverride ReadOnly Property hotFeederTargetPercentage As String Implements GlobalImportConstant.hotFeederTargetPercentage
    Public MustOverride ReadOnly Property hotFeederActualPercentage As String Implements GlobalImportConstant.hotFeederActualPercentage
    Public MustOverride ReadOnly Property hotFeederDebit As String Implements GlobalImportConstant.hotFeederDebit
    Public MustOverride ReadOnly Property hotFeederMass As String Implements GlobalImportConstant.hotFeederMass
    Public MustOverride ReadOnly Property hotFeederRecycledID As String Implements GlobalImportConstant.hotFeederRecycledID
    Public MustOverride ReadOnly Property hotFeederRecycledActualPercentage As String Implements GlobalImportConstant.hotFeederRecycledActualPercentage
    Public MustOverride ReadOnly Property hotFeederMaterialID As String Implements GlobalImportConstant.hotFeederMaterialID

End Class
