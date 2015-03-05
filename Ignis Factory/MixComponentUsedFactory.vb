﻿Imports System.Globalization

Public Class MixComponentUsedFactory
    Public Sub New()
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub
    Public Function createAsphaltUsed(indexCycle As Integer, sourceFile As SourceFile) As AsphaltUsed
        Dim asphaltUsed As AsphaltUsed
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim asphaltRecordedTemperature As Double
        Dim asphaltDensity As Double
        Dim asphaltTankId As String
        Dim asphaltRank As String


        asphaltRecordedTemperature = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteRecordedTemperature(indexCycle, sourceFile)
        asphaltDensity = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteDensity(indexCycle, sourceFile)
        asphaltTankId = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteTankId(indexCycle, sourceFile)
        asphaltRank = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteRank(indexCycle, sourceFile)

        targetPercentage = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteTargetPercentage(indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteActualPercentage(indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteDebit(indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteMass(indexCycle, sourceFile)

        asphaltUsed = New AsphaltUsed(targetPercentage, actualPercentage, debit, mass, asphaltRecordedTemperature, asphaltDensity, asphaltTankId, asphaltRank)

        Return asphaltUsed
    End Function

    Public Function createAggregatedtUsed(indexCycle As Integer, sourceFile As SourceFile) As AggregateUsed
        Dim aggregateUsed As AggregateUsed

        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double

        targetPercentage = sourceFile.sourceFileAdapter.getCycleAggregateTargetPercentage(indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getCycleAggregateActualPercentage(indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getCycleAggregateDebit(indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getCycleAggregateMass(indexCycle, sourceFile)

        aggregateUsed = New AggregateUsed(targetPercentage, actualPercentage, debit, mass)

        Return aggregateUsed
    End Function

End Class
