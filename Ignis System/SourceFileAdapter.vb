Public MustInherit Class SourceFileAdapter
    Protected cycleList As List(Of String)

    Public Sub New()
    End Sub

    Protected MustOverride Function getCycleList(sourceFile As SourceFile) As List(Of String)

    Public MustOverride Function getDate(sourceFile As SourceFile) As Date

    Public MustOverride Function getCycleCount(sourceFile As SourceFile) As Integer

    Public MustOverride Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date

    Protected MustOverride Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

    Protected MustOverride Function getColdFeederForCycle(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederID(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederTargetPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederActualPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederDebit(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederMass(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederMoisturePercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getFillerTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAdditiveTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getFillerActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAdditiveActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getFillerDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAdditiveDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getFillerMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAdditiveMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getVirginAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getRecycledAggregateAsphaltPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTotalAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getDopeTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getDopeAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getDopeAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

    Protected MustOverride Function getHotFeederForCycle(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederID(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederTargetPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederActualPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederDebit(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederMass(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederMoisturePercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    Public MustOverride Function getHotFeederRecycledAsphaltPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String


End Class
