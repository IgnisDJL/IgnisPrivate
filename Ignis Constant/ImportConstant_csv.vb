﻿
Public MustInherit Class ImportConstant_csv
    Implements GlobalImportConstant

    Public Const asphaltTankId_Fr_csv = "Tank Bit"
    Public Const asphaltTankId_En_csv = "Type   Bitume"

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
    Public MustOverride ReadOnly Property dureeMalaxHumide() As String Implements GlobalImportConstant.dureeMalaxHumide
    Public MustOverride ReadOnly Property dureeMalaxSec() As String Implements GlobalImportConstant.dureeMalaxSec
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
    Public MustOverride ReadOnly Property virginAsphaltConcreteGrade() As String Implements GlobalImportConstant.virginAsphaltConcreteGrade

    ''**********************************************
    ''  getter de l'enrobé produit
    ''**********************************************
    Public MustOverride ReadOnly Property mixName() As String Implements GlobalImportConstant.mixName
    Public MustOverride ReadOnly Property mixNumber() As String Implements GlobalImportConstant.mixNumber
    Public MustOverride ReadOnly Property mixRecordedTemperature() As String Implements GlobalImportConstant.mixRecordedTemperature

    ''**********************************************
    ''  getter des bennes froides
    ''**********************************************

    Public MustOverride ReadOnly Property coldFeederTargetPercentage As String Implements GlobalImportConstant.coldFeederTargetPercentage
    Public MustOverride ReadOnly Property coldFeederActualPercentage As String Implements GlobalImportConstant.coldFeederActualPercentage
    Public MustOverride ReadOnly Property coldFeederDebit As String Implements GlobalImportConstant.coldFeederDebit
    Public MustOverride ReadOnly Property coldFeederMass As String Implements GlobalImportConstant.coldFeederMass
    Public MustOverride ReadOnly Property coldFeederMoisturePercentage As String Implements GlobalImportConstant.coldFeederMoisturePercentage
    Public MustOverride ReadOnly Property coldFeederID() As String Implements GlobalImportConstant.coldFeederID
    Public MustOverride ReadOnly Property coldFeederMaterialID As String Implements GlobalImportConstant.coldFeederMaterialID

    Public MustOverride ReadOnly Property coldFeederRecycledTargetPercentage As String
    Public MustOverride ReadOnly Property coldFeederRecycledActualPercentage As String Implements GlobalImportConstant.coldFeederRecycledActualPercentage
    Public MustOverride ReadOnly Property coldFeederRecycledDebit As String
    Public MustOverride ReadOnly Property coldFeederRecycledMass As String
    Public MustOverride ReadOnly Property coldFeederRecycledMoisturePercentage As String
    Public MustOverride ReadOnly Property coldFeederRecycledID As String Implements GlobalImportConstant.coldFeederRecycledID
    Public MustOverride ReadOnly Property coldFeederRecycledMaterialID As String


    ''***************************************************************************************************
    ''  Fonction additionelles pour adapter la structure particulière des fichiers .csv produit par Minds
    ''***************************************************************************************************

    ' ID des bennes chaudes

    Public MustOverride ReadOnly Property hotFeederAggregateTargetPercentage() As String
    Public MustOverride ReadOnly Property hotFeederAggregateActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederAggregateDebit() As String
    Public MustOverride ReadOnly Property hotFeederAggregateMass() As String
    Public MustOverride ReadOnly Property hotFeederAggregateID() As String
    Public MustOverride ReadOnly Property hotFeederAggregateMaterialID() As String

    Public MustOverride ReadOnly Property hotFeederFillerTargetPercentage() As String
    Public MustOverride ReadOnly Property hotFeederFillerActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederFillerDebit() As String
    Public MustOverride ReadOnly Property hotFeederFillerMass() As String
    Public MustOverride ReadOnly Property hotFeederFillerID() As String
    Public MustOverride ReadOnly Property hotFeederFillerMaterialID() As String

    Public MustOverride ReadOnly Property hotFeederAdditiveTargetPercentage() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveDebit() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveMass() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveID() As String
    Public MustOverride ReadOnly Property hotFeederAdditiveMaterialID() As String

    Public MustOverride ReadOnly Property hotFeederChauxTargetPercentage() As String
    Public MustOverride ReadOnly Property hotFeederChauxActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederChauxDebit() As String
    Public MustOverride ReadOnly Property hotFeederChauxMass() As String
    Public MustOverride ReadOnly Property hotFeederChauxID() As String
    Public MustOverride ReadOnly Property hotFeederChauxMaterialID() As String

    Public MustOverride ReadOnly Property hotFeederDopeTargetPercentage() As String
    Public MustOverride ReadOnly Property hotFeederDopeActualPercentage() As String
    Public MustOverride ReadOnly Property hotFeederDopeDebit() As String
    Public MustOverride ReadOnly Property hotFeederDopeMass() As String
    Public MustOverride ReadOnly Property hotFeederDopeID() As String
    Public MustOverride ReadOnly Property hotFeederDopeMaterialID() As String


    ''**********************************************
    ''  getter des bennes chaudes
    ''**********************************************

    '' ID des bennes chaudes

    ''*******************************************************************************************************
    '' Cette partie de l'interface GlobalImportConstant ne s'applique pas aux fichiers .csv généré par minds
    '' Les fonction ont été remplacé par des fonctions définit dans la classe ImportConstant_csv
    ''*******************************************************************************************************

    Public ReadOnly Property hotFeederActualPercentage As String Implements GlobalImportConstant.hotFeederActualPercentage
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederDebit As String Implements GlobalImportConstant.hotFeederDebit
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederID As String Implements GlobalImportConstant.hotFeederID
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederMass As String Implements GlobalImportConstant.hotFeederMass
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederRecycledActualPercentage As String Implements GlobalImportConstant.hotFeederRecycledActualPercentage
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederRecycledID As String Implements GlobalImportConstant.hotFeederRecycledID
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederTargetPercentage As String Implements GlobalImportConstant.hotFeederTargetPercentage
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

    Public ReadOnly Property hotFeederMaterialID As String Implements GlobalImportConstant.hotFeederMaterialID
        Get
            '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
            '' la classe ImportConstant_csv
            Return "-5"
        End Get
    End Property

End Class
