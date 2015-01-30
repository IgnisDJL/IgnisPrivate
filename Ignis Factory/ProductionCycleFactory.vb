
Public Class ProductionCycleFactory

    Private feederFactory As FeederFactory
    Private producedMixFactory As ProducedMixFactory
    Private mixComponentUsedFactory As MixComponentUsedFactory

    Public Sub New()
        Me.feederFactory = New FeederFactory
        Me.producedMixFactory = New ProducedMixFactory
        Me.mixComponentUsedFactory = New MixComponentUsedFactory
    End Sub

    Public Function createProductionCycle(indexCycle As Integer, sourceFile As SourceFile) As ProductionCycle
        Dim productionCycle As ProductionCycle

        Dim asphaltTankId As String
        Dim asphaltRecordedTemperature As Double
        Dim endOfCycle As Date
        Dim mixProduced As ProducedMix
        Dim virginAsphaltUsed As AsphaltUsed
        Dim recycledAsphaltUsed As RecycledAsphaltUsed
        Dim feederList As List(Of Feeder_1)
        Dim totalAsphaltUsed As AsphaltUsed
        Dim virginAggregateUsed As AggregateUsed
        Dim recycledAggregateUsed As RecycledAggregateUsed
        Dim fillerUsed As FillerUsed
        Dim additiveUsed As AdditiveUsed
        Dim dustRemovalDebit As Double
        Dim siloFillingNumber As String
        Dim asphaltDensity As Double


        asphaltTankId = sourceFile.sourceFileAdapter.getAsphaltTankId(indexCycle, sourceFile)
        asphaltRecordedTemperature = sourceFile.sourceFileAdapter.getAsphaltRecordedTemperature(indexCycle, sourceFile)
        endOfCycle = sourceFile.sourceFileAdapter.getTime(indexCycle, sourceFile)
        mixProduced = producedMixFactory.createProducedMix(indexCycle, sourceFile)
        feederList = feederFactory.createFeederList(indexCycle, sourceFile)
        virginAsphaltUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.VirginAspahlt, indexCycle, sourceFile)
        recycledAsphaltUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.RecycledAsphalt, indexCycle, sourceFile)
        totalAsphaltUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.TotalAsphalt, indexCycle, sourceFile)
        virginAggregateUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.VirginAggregate, indexCycle, sourceFile)
        recycledAggregateUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.RecycledAggregate, indexCycle, sourceFile)
        fillerUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.Filler, indexCycle, sourceFile)
        additiveUsed = mixComponentUsedFactory.createMixComponentUsed(EnumColumnType.Additive, indexCycle, sourceFile)
        dustRemovalDebit = sourceFile.sourceFileAdapter.getDustRemovalDebit(indexCycle, sourceFile)
        siloFillingNumber = sourceFile.sourceFileAdapter.getSiloFillingNumber(indexCycle, sourceFile)
        asphaltDensity = sourceFile.sourceFileAdapter.getAsphaltDensity(indexCycle, sourceFile)

        productionCycle = New ProductionCycle(asphaltTankId, asphaltRecordedTemperature, endOfCycle, mixProduced, feederList, virginAsphaltUsed, recycledAsphaltUsed, totalAsphaltUsed,
                                              virginAggregateUsed, recycledAggregateUsed, fillerUsed, additiveUsed, dustRemovalDebit,
                                              siloFillingNumber, asphaltDensity)


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
