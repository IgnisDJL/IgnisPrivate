Imports System.Globalization

Public Class MixComponentUsedFactory
    Public Sub New()
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub
    Public Function createVirginAsphaltConcrete(indexCycle As Integer, sourceFile As SourceFile) As VirginAsphaltConcrete
        Dim virginAsphaltConcrete As VirginAsphaltConcrete
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim recordedTemperature As Double
        Dim density As Double
        Dim asphaltTankId As String
        Dim asphaltGrade As String

        recordedTemperature = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteRecordedTemperature(indexCycle, sourceFile)
        density = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteDensity(indexCycle, sourceFile)
        asphaltTankId = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteTankId(indexCycle, sourceFile)
        asphaltGrade = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteGrade(indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteTargetPercentage(indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteActualPercentage(indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteDebit(indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getVirginAsphaltConcreteMass(indexCycle, sourceFile)

        virginAsphaltConcrete = New VirginAsphaltConcrete(targetPercentage, actualPercentage, debit, mass, recordedTemperature, density, asphaltTankId, asphaltGrade)

        Return virginAsphaltConcrete
    End Function

    Public Function createRapAsphaltConcrete(recycledHotFeeder As RecycledHotFeeder) As RapAsphaltConcrete
        Dim rapAsphaltConcrete As RapAsphaltConcrete
        Dim targetPercentage As Double = recycledHotFeeder.getTargetPercentage
        Dim actualPercentage As Double = recycledHotFeeder.getActualPercentage
        '' Dans l'état des choses, il n'y a aucun moyen de connaître la masse de chacun des bitume recyclé. Aucun des fichiers source ne fournit cette information.
        Dim mass As Double = 0
        Dim debit As Double = 0
        rapAsphaltConcrete = New RapAsphaltConcrete(targetPercentage, actualPercentage, debit, mass)

        Return rapAsphaltConcrete
    End Function

    Public Function createRapAsphaltConcreteList(hotFeederList As List(Of HotFeeder)) As List(Of RapAsphaltConcrete)
        Dim rapAsphaltConcreteList = New List(Of RapAsphaltConcrete)
        Dim rapAsphaltConcrete As RapAsphaltConcrete

        For Each recycledHotFeeder As HotFeeder In hotFeederList

            If TypeOf recycledHotFeeder Is RecycledHotFeeder Then
                rapAsphaltConcrete = createRapAsphaltConcrete(recycledHotFeeder)
                rapAsphaltConcreteList.Add(rapAsphaltConcrete)
            End If

        Next

        Return rapAsphaltConcreteList
    End Function
End Class
