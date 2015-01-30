
Public Class ProductionDayFactory

    Private productionCycleFactory As ProductionCycleFactory

    Public Sub New()
        productionCycleFactory = New ProductionCycleFactory()
    End Sub

    Public Function createProductionDay(sourceFile As SourceFile) As ProductionDay_1

        Dim productionDay As ProductionDay_1
        productionDay = New ProductionDay_1(sourceFile.sourceFileAdapter.getDate(sourceFile))
        productionDay.productionCycleList = getProductionCycleForProductionDay(sourceFile)

        Return productionDay
    End Function

    Private Function getProductionCycleForProductionDay(sourceFile As SourceFile) As List(Of ProductionCycle)
        Return productionCycleFactory.createProductionCycleList(sourceFile)
    End Function


End Class
