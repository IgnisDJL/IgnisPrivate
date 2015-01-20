''' <summary>
''' Represents a data file containing the daily measures taken by the factory's software. These measures are usually
''' taken in a periodic way and grouped by "cycles" of production.
''' </summary>
Public MustInherit Class CyclesFile
    Inherits DataFile

    Public Sub New(filePath As String)
        MyBase.New(filePath)
    End Sub

    Public MustOverride Function getCycles(startTime As Date, endTime As Date) As List(Of Cycle)

    Public MustOverride Overrides ReadOnly Property Date_ As Date

End Class
