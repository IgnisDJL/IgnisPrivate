
Public Class MixComponentUsedFactory
    Public Sub New()

    End Sub

    Public Function createMixComponentUsed(columnType As EnumColumnType, indexCycle As Integer, sourceFile As SourceFile) As MixComponentUsed

        Select Case columnType

            Case EnumColumnType.VirginAggregate

                Return createAggregateUsed(indexCycle, sourceFile)

            Case EnumColumnType.RecycledAggregate

                Return createRecycledAggregateUsed(indexCycle, sourceFile)

            Case EnumColumnType.VirginAspahlt

                Return createAsphaltUsed(columnType, indexCycle, sourceFile)

            Case EnumColumnType.RecycledAsphalt

                Return createRecycledAsphaltUsed(indexCycle, sourceFile)

            Case EnumColumnType.TotalAsphalt

                Return createAsphaltUsed(columnType, indexCycle, sourceFile)

            Case EnumColumnType.Filler

                Return createFillerUsed(indexCycle, sourceFile)

            Case EnumColumnType.Additive

                Return createAdditiveUsed(indexCycle, sourceFile)

            Case Else
                Return Nothing

        End Select

        Return Nothing
    End Function

    Private Function createAggregateUsed(indexCycle As Integer, sourceFile As SourceFile) As AggregateUsed
        Dim aggregateUsed As AggregateUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getVirginAggregateTargetPercentage(indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getVirginAggregateActualPercentage(indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getVirginAggregateDebit(indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getVirginAggregateMass(indexCycle, sourceFile)
        Dim moisturePercentage As Double = sourceFile.sourceFileAdapter.getVirginAggregateMoisturePercentage(indexCycle, sourceFile)

        aggregateUsed = New AggregateUsed(targetPercentage, actualPercentage, debit, mass, moisturePercentage)

        Return aggregateUsed
    End Function

    Private Function createRecycledAggregateUsed(indexCycle As Integer, sourceFile As SourceFile) As RecycledAggregateUsed
        Dim recycledAggregateUsed As RecycledAggregateUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getRecycledAggregateTargetPercentage(indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getRecycledAggregateActualPercentage(indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getRecycledAggregateDebit(indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getRecycledAggregateMass(indexCycle, sourceFile)
        Dim moisturePercentage As Double = sourceFile.sourceFileAdapter.getRecycledAggregateMoisturePercentage(indexCycle, sourceFile)
        Dim asphaltPercentage As Double = sourceFile.sourceFileAdapter.getRecycledAggregateAsphaltPercentage(indexCycle, sourceFile)

        recycledAggregateUsed = New RecycledAggregateUsed(targetPercentage, actualPercentage, debit, mass, moisturePercentage, asphaltPercentage)

        Return recycledAggregateUsed
    End Function

    Private Function createAsphaltUsed(columnType As EnumColumnType, indexCycle As Integer, sourceFile As SourceFile) As AsphaltUsed
        Dim asphaltUsed As AsphaltUsed
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double

        Select Case columnType

            Case EnumColumnType.VirginAspahlt
                targetPercentage = sourceFile.sourceFileAdapter.getVirginAsphaltTargetPercentage(indexCycle, sourceFile)
                actualPercentage = sourceFile.sourceFileAdapter.getVirginAsphaltActualPercentage(indexCycle, sourceFile)
                debit = sourceFile.sourceFileAdapter.getVirginAsphaltDebit(indexCycle, sourceFile)
                mass = sourceFile.sourceFileAdapter.getVirginAsphaltMass(indexCycle, sourceFile)

            Case EnumColumnType.TotalAsphalt
                targetPercentage = sourceFile.sourceFileAdapter.getTotalAsphaltTargetPercentage(indexCycle, sourceFile)
                actualPercentage = sourceFile.sourceFileAdapter.getTotalAsphaltActualPercentage(indexCycle, sourceFile)
                debit = sourceFile.sourceFileAdapter.getTotalAsphaltDebit(indexCycle, sourceFile)
                mass = sourceFile.sourceFileAdapter.getTotalAsphaltMass(indexCycle, sourceFile)
            Case Else

        End Select

        asphaltUsed = New AsphaltUsed(targetPercentage, actualPercentage, debit, mass)

        Return asphaltUsed
    End Function

    Private Function createRecycledAsphaltUsed(indexCycle As Integer, sourceFile As SourceFile) As RecycledAsphaltUsed
        Dim recycledAsphaltUsed As RecycledAsphaltUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getRecycledAsphaltTargetPercentage(indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getRecycledAsphaltActualPercentage(indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getRecycledAsphaltDebit(indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getRecycledAsphaltMass(indexCycle, sourceFile)

        recycledAsphaltUsed = New RecycledAsphaltUsed(targetPercentage, actualPercentage, debit, mass)

        Return recycledAsphaltUsed
    End Function

    Private Function createFillerUsed(indexCycle As Integer, sourceFile As SourceFile) As FillerUsed
        Dim fillerUsed As FillerUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getFillerTargetPercentage(indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getFillerActualPercentage(indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getFillerDebit(indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getFillerMass(indexCycle, sourceFile)

        fillerUsed = New FillerUsed(targetPercentage, actualPercentage, debit, mass)

        Return fillerUsed
    End Function

    Private Function createAdditiveUsed(indexCycle As Integer, sourceFile As SourceFile) As AdditiveUsed
        Dim additiveUsed As AdditiveUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getAdditiveTargetPercentage(indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getAdditiveActualPercentage(indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getAdditiveDebit(indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getAdditiveMass(indexCycle, sourceFile)

        additiveUsed = New AdditiveUsed(targetPercentage, actualPercentage, debit, mass)

        Return additiveUsed
    End Function

    Public Function createAggregatetUsed(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As AggregateUsed
        Dim aggregateUsed As AggregateUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getFeederDebit(indexFeeder, indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getFeederMass(indexFeeder, indexCycle, sourceFile)
        Dim moisturePercentage As Double = sourceFile.sourceFileAdapter.getFeederMoisturePercentage(indexFeeder, indexCycle, sourceFile)

        aggregateUsed = New AggregateUsed(targetPercentage, actualPercentage, debit, mass, moisturePercentage)

        Return aggregateUsed
    End Function

    Public Function createRecycledAggregateUsed(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As RecycledAggregateUsed
        Dim recycledAggregateUsed As RecycledAggregateUsed

        Dim targetPercentage As Double = sourceFile.sourceFileAdapter.getFeederTargetPercentage(indexFeeder, indexCycle, sourceFile)
        Dim actualPercentage As Double = sourceFile.sourceFileAdapter.getFeederActualPercentage(indexFeeder, indexCycle, sourceFile)
        Dim debit As Double = sourceFile.sourceFileAdapter.getFeederDebit(indexFeeder, indexCycle, sourceFile)
        Dim mass As Double = sourceFile.sourceFileAdapter.getFeederMass(indexFeeder, indexCycle, sourceFile)
        Dim moisturePercentage As Double = sourceFile.sourceFileAdapter.getFeederMoisturePercentage(indexFeeder, indexCycle, sourceFile)
        Dim asphaltPercentage As Double = sourceFile.sourceFileAdapter.getFeederRecycledAsphaltPercentage(indexFeeder, indexCycle, sourceFile)

        recycledAggregateUsed = New RecycledAggregateUsed(targetPercentage, actualPercentage, debit, mass, moisturePercentage, asphaltPercentage)

        Return recycledAggregateUsed
    End Function

End Class
