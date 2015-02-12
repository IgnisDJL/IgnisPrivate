Imports System.Globalization

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

        targetPercentage = sourceFile.sourceFileAdapter.getTotalAsphaltTargetPercentage(indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getTotalAsphaltActualPercentage(indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getTotalAsphaltDebit(indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getTotalAsphaltMass(indexCycle, sourceFile)

        asphaltUsed = New AsphaltUsed(targetPercentage, actualPercentage, debit, mass)

        Return asphaltUsed
    End Function

    Public Function createAggregatedtUsed(indexCycle As Integer, sourceFile As SourceFile) As AggregateUsed
        Dim aggregateUsed As AggregateUsed

        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim moisturePercentage As Double

        targetPercentage = sourceFile.sourceFileAdapter.getTotalAggregateTargetPercentage(indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getTotalAggregateActualPercentage(indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getTotalAggregateDebit(indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getTotalAggregateMass(indexCycle, sourceFile)
        moisturePercentage = sourceFile.sourceFileAdapter.getTotalAggregateMoisturePercentage(indexCycle, sourceFile)

        aggregateUsed = New AggregateUsed(targetPercentage, actualPercentage, debit, mass, moisturePercentage)

        Return aggregateUsed
    End Function

End Class
