Imports System.Globalization

Public Class ProductionDayFactory

    Private productionCycleFactory As ProductionCycleFactory

    Public Sub New()
        productionCycleFactory = New ProductionCycleFactory()
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub

    Public Function createProductionDayContinue(sourceFileContinue As SourceFile) As ProductionDay_1

        Dim productionDay As ProductionDay_1
        productionDay = New ProductionDay_1(sourceFileContinue.Date_)
        productionDay.productionContinue = getProductionCycleForProductionDay(sourceFileContinue)

        Return productionDay
    End Function

    Public Function createProductionDayDiscontinue(sourceFileDiscontinue As SourceFile) As ProductionDay_1

        Dim productionDay As ProductionDay_1
        productionDay = New ProductionDay_1(sourceFileDiscontinue.Date_)

        productionDay.productionDiscontinue = getProductionCycleForProductionDay(sourceFileDiscontinue)

        Return productionDay
    End Function

    Public Function createProductionDayHybrid(sourceFileContinue As SourceFile, sourceFileDiscontinue As SourceFile) As ProductionDay_1

        Dim productionDay As ProductionDay_1
        productionDay = New ProductionDay_1(sourceFileContinue.Date_)

        productionDay.productionContinue = getProductionCycleForProductionDay(sourceFileContinue)
        productionDay.productionDiscontinue = getProductionCycleForProductionDay(sourceFileDiscontinue)

        Return productionDay
    End Function

    Public Function createProductionDayHybrid(sourceFileContinue As SourceFile) As ProductionDay_1

        Dim productionDay As ProductionDay_1
        productionDay = New ProductionDay_1(sourceFileContinue.Date_)

        productionDay.productionContinue = getProductionCycleForProductionDay(sourceFileContinue)

        Return productionDay
    End Function

    Private Function getProductionCycleForProductionDay(sourceFile As SourceFile) As List(Of ProductionCycle)
        Return productionCycleFactory.createProductionCycleList(sourceFile)
    End Function


End Class
