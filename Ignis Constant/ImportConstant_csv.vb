
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
    Public MustOverride ReadOnly Property virginAsphaltConcreteRank() As String Implements GlobalImportConstant.virginAsphaltConcreteRank

    ''**********************************************
    ''  getter cycleAggregate
    ''**********************************************
    Public MustOverride ReadOnly Property cycleAggregateTargetPercentage() As String Implements GlobalImportConstant.cycleAggregateTargetPercentage
    Public MustOverride ReadOnly Property cycleAggregateActualPercentage() As String Implements GlobalImportConstant.cycleAggregateActualPercentage
    Public MustOverride ReadOnly Property cycleAggregateDebit() As String Implements GlobalImportConstant.cycleAggregateDebit
    Public MustOverride ReadOnly Property cycleAggregateMass() As String Implements GlobalImportConstant.cycleAggregateMass

    ''**********************************************
    ''  getter de l'enrobé produit
    ''**********************************************
    Public MustOverride ReadOnly Property mixDebit() As String Implements GlobalImportConstant.mixDebit
    Public MustOverride ReadOnly Property mixName() As String Implements GlobalImportConstant.mixName
    Public MustOverride ReadOnly Property mixNumber() As String Implements GlobalImportConstant.mixNumber
    Public MustOverride ReadOnly Property mixRecordedTemperature() As String Implements GlobalImportConstant.mixRecordedTemperature
    Public MustOverride ReadOnly Property mixCounter() As String Implements GlobalImportConstant.mixCounter

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


    ''***************************************************************************************************
    ''  Fonction additionelles pour adapter la structure particulière des fichiers .csv produit par Minds
    ''***************************************************************************************************

    ' ID des bennes chaudes

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

    '' TODO 
    '' Retirer la fonction en commentaire lorsqu'on sera certain qu'elle n'est plus utile

    'Public ReadOnly Property hotFeederMoisturePercentage As String Implements GlobalImportConstant.hotFeederMoisturePercentage
    '    Get
    '        '' Cette constante est inutilisé dans le contexte des fichiers .csv produit par minds, car une fonction plus spécifique est déjà définit dans 
    '        '' la classe ImportConstant_csv
    '        Return "-5"
    '    End Get
    'End Property

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
