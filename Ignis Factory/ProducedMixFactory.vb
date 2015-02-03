Imports System.Globalization

Public Class ProducedMixFactory
    Public Sub New()
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub

    Public Function createProducedMix(indexCycle As Integer, sourceFile As SourceFile) As ProducedMix
        Dim producedMix As ProducedMix

        Dim mixNumber As String = sourceFile.sourceFileAdapter.getMixNumber(indexCycle, sourceFile)
        Dim mixName As String = sourceFile.sourceFileAdapter.getMixName(indexCycle, sourceFile)
        Dim recordedTemperature As Double = sourceFile.sourceFileAdapter.getMixRecordedTemperature(indexCycle, sourceFile)
        Dim mixDebit As Double = sourceFile.sourceFileAdapter.getMixDebit(indexCycle, sourceFile)
        Dim mixCounter As Double = sourceFile.sourceFileAdapter.getMixCounter(indexCycle, sourceFile)

        producedMix = New ProducedMix(mixNumber, mixName, recordedTemperature, mixDebit, mixCounter)

        Return producedMix

    End Function

End Class
