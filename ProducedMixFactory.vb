
Public Class ProducedMixFactory
    Public Sub New()

    End Sub

    Public Function createProducedMix(indexCycle As Integer, sourceFile As SourceFile) As ProducedMix
        Dim producedMix As ProducedMix

        Dim mixNumber As Integer
        Dim mixName As String
        Dim recordedTemperature As Double
        Dim mixDebit As Double
        Dim mixCounter As Double

        producedMix = New ProducedMix(mixNumber, mixName, recordedTemperature, mixDebit, mixCounter)

        Return producedMix

    End Function

End Class
