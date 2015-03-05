Public MustInherit Class SourceFileAdapter
    Protected cycleList As List(Of String)

    Public Sub New()
    End Sub


    ''***********************************************************************************************************************
    ''  Fonction private unique au type de fichier source
    ''  Fonction qui effectu une oppération de formatage ou d'affichage du fichier source
    ''
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Fonction protected force l'adapteur a implémenté une fonction utile a la lecture du fichier source ou au formatage des donnée
    ''***********************************************************************************************************************
    Protected MustOverride Function getCycleList(sourceFile As SourceFile) As List(Of String)
    Protected MustOverride Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String


    ''***********************************************************************************************************************
    ''  Fonction publique mais qui n'ont pas la responsabilié de retourner une informations directement au modèle du domaine
    ''
    ''*********************************************************************************************************************
    Public MustOverride Function getCycleCount(sourceFile As SourceFile) As Integer
    Public MustOverride Sub setImportConstantForLanguage(sourceFile As SourceFile)


    ''***********************************************************************************************************************
    ''  Fonction publique générique a tout les adapteurs
    ''  Fonction qui récupère une donnée du fichier source, ou qui calcule une donnée avec d'autre donnée source
    ''  Ces fonctions permettent de générer les objets du modèle du programme
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionCycle
    ''***********************************************************************************************************************
    Public MustOverride Function getDate(sourceFile As SourceFile) As Date

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionCycle
    ''***********************************************************************************************************************

    Public MustOverride Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
    Public MustOverride Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les totaux d'un cycle de production 
    ''***********************************************************************************************************************
    ''TotalAggregate
    Public MustOverride Function getCycleAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    '' TODO
    '' Code mort à retirer

    '' Public MustOverride Function getTotalAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    ''TotalMass
    Public MustOverride Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les données liées au bitume utilisé dans un cycle 
    ''***********************************************************************************************************************
    Public MustOverride Function getCycleAsphaltConcreteActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteDebit(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteMass(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteTankId(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteDensity(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getCycleAsphaltConcreteRank(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle
    ''***********************************************************************************************************************
    Public MustOverride Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************
    Public MustOverride Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
    Public MustOverride Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les Bennes chaudes d'un cycle
    ''***********************************************************************************************************************
    Public MustOverride Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
    Public MustOverride Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getHotFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getHotFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    ''TODO
    '' reitirer le commentaire lorsque je me serais assurer que la fonction est inutile
    'Public MustOverride Function getHotFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

End Class
