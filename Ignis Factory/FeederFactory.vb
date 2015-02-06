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

        feederId = sourceFile.sourceFileAdapter.getColdFeederID(indexFeeder, indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getColdFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getColdFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getColdFeederDebit(indexFeeder, indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getColdFeederMass(indexFeeder, indexCycle, sourceFile)
        moisturePercentage = sourceFile.sourceFileAdapter.getColdFeederMoisturePercentage(indexFeeder, indexCycle, sourceFile)
        feeder = New ColdFeeder(feederId, targetPercentage, actualPercentage, debit, mass, moisturePercentage)

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

        feederId = sourceFile.sourceFileAdapter.getColdFeederID(indexFeeder, indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getColdFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getColdFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getColdFeederDebit(indexFeeder, indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getColdFeederMass(indexFeeder, indexCycle, sourceFile)
        moisturePercentage = sourceFile.sourceFileAdapter.getColdFeederMoisturePercentage(indexFeeder, indexCycle, sourceFile)
        asphaltPercentage = sourceFile.sourceFileAdapter.getColdFeederRecycledAsphaltPercentage(indexFeeder, indexCycle, sourceFile)
        feeder = New RecycledColdFeeder(feederId, targetPercentage, actualPercentage, debit, mass, moisturePercentage, asphaltPercentage)

        Return feeder
    End Function


    Public Function createColdFeederList(indexCycle As Integer, sourceFile As SourceFile) As List(Of ColdFeeder)
        Dim feederList As List(Of ColdFeeder) = New List(Of ColdFeeder)

        For indexFeeder As Integer = 0 To sourceFile.sourceFileAdapter.getColdFeederCountForCycle(indexCycle, sourceFile) - 1

            If (sourceFile.sourceFileAdapter.getColdFeederID(indexFeeder, indexCycle, sourceFile).Contains("Rap")) Then
                feederList.Add(createRecycledColdFeeder(indexFeeder, indexCycle, sourceFile))
            Else
                feederList.Add(createColdFeeder(indexFeeder, indexCycle, sourceFile))
            End If

        Next

        Return feederList
    End Function


End Class
