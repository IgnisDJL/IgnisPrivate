﻿Imports System.Globalization

Public Class ProductionCycleFactory

    Private feederFactory As FeederFactory
    Private producedMixFactory As ProducedMixFactory


    Public Sub New()
        Me.feederFactory = New FeederFactory
        Me.producedMixFactory = New ProducedMixFactory

        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub

    Private Function createProductionCycle(indexCycle As Integer, sourceFile As SourceFile) As ProductionCycle
        Dim productionCycle As ProductionCycle

        Dim endOfCycle As Date
        Dim mixProduced As ProducedMix
        Dim coldFeederList As List(Of ColdFeeder)
        Dim dustRemovalDebit As Double
        Dim siloFillingNumber As String
        Dim bagHouseDiff As Double
        Dim dureeCycle As TimeSpan
        Dim dureeMalaxHumide As TimeSpan
        Dim dureeMalaxSec As TimeSpan
        Dim manuelle As Boolean
        Dim contractID As String
        Dim truckID As String

        endOfCycle = sourceFile.sourceFileAdapter.getTime(indexCycle, sourceFile)
        mixProduced = producedMixFactory.createProducedMix(indexCycle, sourceFile)
        coldFeederList = feederFactory.createColdFeederList(indexCycle, sourceFile)
        dustRemovalDebit = sourceFile.sourceFileAdapter.getDustRemovalDebit(indexCycle, sourceFile)
        siloFillingNumber = sourceFile.sourceFileAdapter.getSiloFillingNumber(indexCycle, sourceFile)
        bagHouseDiff = sourceFile.sourceFileAdapter.getBagHouseDiff(indexCycle, sourceFile)
        dureeCycle = sourceFile.sourceFileAdapter.getDureeCycle(indexCycle, sourceFile)
        dureeMalaxHumide = sourceFile.sourceFileAdapter.getDureeMalaxHumideCycle(indexCycle, sourceFile)
        dureeMalaxSec = sourceFile.sourceFileAdapter.getDureeMalaxSecCycle(indexCycle, sourceFile)
        manuelle = sourceFile.sourceFileAdapter.getManuelle(indexCycle, sourceFile)
        contractID = sourceFile.sourceFileAdapter.getContractID(indexCycle, sourceFile)
        truckID = sourceFile.sourceFileAdapter.getTruckID(indexCycle, sourceFile)

        productionCycle = New ProductionCycle(endOfCycle, mixProduced, coldFeederList,
                                                dustRemovalDebit, siloFillingNumber, bagHouseDiff,
                                              dureeCycle, dureeMalaxHumide, dureeMalaxSec, manuelle, contractID, truckID)


        Return productionCycle
    End Function


    Public Function createProductionCycleList(sourceFile As SourceFile) As List(Of ProductionCycle)
        Dim productionCycleList As List(Of ProductionCycle) = New List(Of ProductionCycle)

        For indexCycle As Integer = 0 To sourceFile.sourceFileAdapter.getCycleCount(sourceFile) - 1
            productionCycleList.Add(createProductionCycle(indexCycle, sourceFile))
        Next

        Return productionCycleList
    End Function

End Class
