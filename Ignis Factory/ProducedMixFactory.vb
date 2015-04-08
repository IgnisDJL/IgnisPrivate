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
        Dim mixMass As Double = sourceFile.sourceFileAdapter.getTotalMass(indexCycle, sourceFile)
        ''Dim mixDebit As Double = sourceFile.sourceFile
        producedMix = New ProducedMix(mixNumber, mixName, recordedTemperature, mixMass)

        Return producedMix

    End Function

End Class
