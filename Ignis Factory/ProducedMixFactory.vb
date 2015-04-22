Imports System.Globalization

Public Class ProducedMixFactory
    Private mixComponentUsedFactory As MixComponentUsedFactory
    Private feederFactory As FeederFactory

    Public Sub New()
        Application.CurrentCulture = New CultureInfo("EN-US")
        Me.feederFactory = New FeederFactory
        Me.mixComponentUsedFactory = New MixComponentUsedFactory
    End Sub

    Public Function createProducedMix(indexCycle As Integer, sourceFile As SourceFile) As ProducedMix
        Dim producedMix As ProducedMix

        Dim mixNumber As String = sourceFile.sourceFileAdapter.getMixNumber(indexCycle, sourceFile)
        Dim mixName As String = sourceFile.sourceFileAdapter.getMixName(indexCycle, sourceFile)
        Dim recordedTemperature As Double = sourceFile.sourceFileAdapter.getMixRecordedTemperature(indexCycle, sourceFile)
        Dim tempsDeProduction As TimeSpan = sourceFile.sourceFileAdapter.getDureeCycle(indexCycle, sourceFile)
        Dim hotFeederList As List(Of HotFeeder) = feederFactory.createHotFeederList(indexCycle, sourceFile)
        Dim virginAsphaltConcrete As VirginAsphaltConcrete = mixComponentUsedFactory.createVirginAsphaltConcrete(indexCycle, sourceFile)
        Dim rapAsphaltConcreteList As List(Of RapAsphaltConcrete) = mixComponentUsedFactory.createRapAsphaltConcreteList(hotFeederList)

        producedMix = New ProducedMix(mixNumber, mixName, recordedTemperature, hotFeederList, virginAsphaltConcrete, rapAsphaltConcreteList, tempsDeProduction)

        Return producedMix

    End Function

End Class
