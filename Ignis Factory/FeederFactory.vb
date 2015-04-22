Imports System.Globalization

Public Class FeederFactory

    Public Sub New()
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub

    Public Function createColdFeeder(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As ColdFeeder
        Dim feeder As ColdFeeder
        Dim feederId As String
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim moisturePercentage As Double
        Dim materialID As String

        feederId = sourceFile.sourceFileAdapter.getColdFeederID(indexFeeder, indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getColdFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getColdFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getColdFeederDebit(indexFeeder, indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getColdFeederMass(indexFeeder, indexCycle, sourceFile)
        moisturePercentage = sourceFile.sourceFileAdapter.getColdFeederMoisturePercentage(indexFeeder, indexCycle, sourceFile)
        materialID = sourceFile.sourceFileAdapter.getColdFeederMaterialID(indexFeeder, indexCycle, sourceFile)

        feeder = New ColdFeeder(feederId, materialID, targetPercentage, actualPercentage, debit, mass, moisturePercentage)

        Return feeder
    End Function

    Public Function createRecycledColdFeeder(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As ColdFeeder
        Dim feeder As RecycledColdFeeder

        Dim feederId As String
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim moisturePercentage As Double
        Dim asphaltPercentage As Double
        Dim materialID As String
        Dim productionDate As Date

        feederId = sourceFile.sourceFileAdapter.getColdFeederID(indexFeeder, indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getColdFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getColdFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getColdFeederDebit(indexFeeder, indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getColdFeederMass(indexFeeder, indexCycle, sourceFile)
        moisturePercentage = sourceFile.sourceFileAdapter.getColdFeederMoisturePercentage(indexFeeder, indexCycle, sourceFile)
        asphaltPercentage = sourceFile.sourceFileAdapter.getColdFeederRecycledAsphaltPercentage(indexFeeder, indexCycle, sourceFile)
        materialID = sourceFile.sourceFileAdapter.getColdFeederMaterialID(indexFeeder, indexCycle, sourceFile)
        productionDate = sourceFile.sourceFileAdapter.getDate(sourceFile)
        feeder = New RecycledColdFeeder(feederId, materialID, targetPercentage, actualPercentage, debit, mass, moisturePercentage, asphaltPercentage, productionDate)

        Return feeder
    End Function


    Public Function createColdFeederList(indexCycle As Integer, sourceFile As SourceFile) As List(Of ColdFeeder)
        Dim feederList As List(Of ColdFeeder) = New List(Of ColdFeeder)

        For indexFeeder As Integer = 0 To sourceFile.sourceFileAdapter.getColdFeederCountForCycle(indexCycle, sourceFile) - 1

            If (sourceFile.sourceFileAdapter.getColdFeederID(indexFeeder, indexCycle, sourceFile).Contains(sourceFile.importConstant.recycledID)) Then
                feederList.Add(createRecycledColdFeeder(indexFeeder, indexCycle, sourceFile))
            Else
                feederList.Add(createColdFeeder(indexFeeder, indexCycle, sourceFile))
            End If

        Next

        Return feederList
    End Function

    Public Function createHotFeederList(indexCycle As Integer, sourceFile As SourceFile) As List(Of HotFeeder)
        Dim feederList As List(Of HotFeeder) = New List(Of HotFeeder)

        For indexFeeder As Integer = 0 To sourceFile.sourceFileAdapter.getHotFeederCountForCycle(indexCycle, sourceFile) - 1

            If (sourceFile.sourceFileAdapter.getHotFeederID(indexFeeder, indexCycle, sourceFile).Contains(sourceFile.importConstant.recycledID)) Then
                feederList.Add(createRecycledHotFeeder(indexFeeder, indexCycle, sourceFile))
            Else
                feederList.Add(createHotFeeder(indexFeeder, indexCycle, sourceFile))
            End If

        Next
        Return feederList
    End Function

    Private Function createHotFeeder(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As HotFeeder
        Dim feeder As HotFeeder

        Dim feederId As String
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim materialID As String


        feederId = sourceFile.sourceFileAdapter.getHotFeederID(indexFeeder, indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getHotFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getHotFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getHotFeederDebit(indexFeeder, indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getHotFeederMass(indexFeeder, indexCycle, sourceFile)
        materialID = sourceFile.sourceFileAdapter.getHotFeederMaterialID(indexFeeder, indexCycle, sourceFile)


        feeder = New HotFeeder(feederId, materialID, targetPercentage, actualPercentage, debit, mass)

        Return feeder
    End Function

    Private Function createRecycledHotFeeder(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As RecycledHotFeeder
        Dim feeder As RecycledHotFeeder

        Dim feederId As String
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim materialID As String


        feederId = sourceFile.sourceFileAdapter.getHotFeederID(indexFeeder, indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getHotFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getHotFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getHotFeederMass(indexFeeder, indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getHotFeederDebit(indexFeeder, indexCycle, sourceFile)
        materialID = sourceFile.sourceFileAdapter.getHotFeederMaterialID(indexFeeder, indexCycle, sourceFile)


        feeder = New RecycledHotFeeder(feederId, materialID, targetPercentage, actualPercentage, debit, mass)
        Return feeder
    End Function

End Class
