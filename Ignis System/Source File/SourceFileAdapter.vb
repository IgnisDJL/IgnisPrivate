﻿Public MustInherit Class SourceFileAdapter
    Protected cycleList As List(Of String)

    Public Sub New()
    End Sub


    ''***********************************************************************************************************************
    ''  Fonction private unique au type de fichier source
    ''  Fonction qui effectu une oppération de formatage ou d'affichage du fichier source
    ''
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Fonction protected, force l'adapteur a implémenté une fonction utile a la lecture du fichier source ou au formatage des donnée
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
    ''  Section concernant de donnée lier a un ProductionDay
    ''***********************************************************************************************************************
    Public MustOverride Function getDate(sourceFile As SourceFile) As Date
    'Public MustOverride Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionCycle
    ''***********************************************************************************************************************
    Public MustOverride Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
    Public MustOverride Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getDureeCycle(indexCycle As Integer, sourceFile As SourceFile) As TimeSpan
    Public MustOverride Function getDureeMalaxHumideCycle(indexCycle As Integer, sourceFile As SourceFile) As TimeSpan
    Public MustOverride Function getDureeMalaxSecCycle(indexCycle As Integer, sourceFile As SourceFile) As TimeSpan
    Public MustOverride Function getManuelle(indexCycle As Integer, sourceFile As SourceFile) As Boolean

    ''***********************************************************************************************************************
    ''                                      Asphalt Concrete utilisé pour un cycle (A/C) 
    ''***********************************************************************************************************************
    Public MustOverride Function getVirginAsphaltConcreteTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getVirginAsphaltConcreteActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getVirginAsphaltConcreteDebit(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getVirginAsphaltConcreteMass(indexCycle As Integer, sourceFile As SourceFile) As Double
    Public MustOverride Function getVirginAsphaltConcreteRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getVirginAsphaltConcreteDensity(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getVirginAsphaltConcreteTankId(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getVirginAsphaltConcreteGrade(indexCycle As Integer, sourceFile As SourceFile) As String

    'Public MustOverride Function getRapAsphaltConcreteTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteDebit(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteMass(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteDensity(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteTankId(indexCycle As Integer, sourceFile As SourceFile) As String
    'Public MustOverride Function getRapAsphaltConcreteGrade(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle
    ''***********************************************************************************************************************
    Public MustOverride Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************
    Public MustOverride Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
    Public MustOverride Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
    Public MustOverride Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As Double
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
    Public MustOverride Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As Double
    Public MustOverride Function getHotFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

End Class
