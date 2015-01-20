Public Class ProductionTypeStatistics

    Public Property StartTime As Date
    Public Property EndTime As Date
    Public Property Duration As TimeSpan = TimeSpan.Zero
    Public Property Quantity As Double = 0
    Public Property NbMixSwitch As Integer = 0

    Public Property Cycles As New List(Of Cycle)

    Public Property Mixes As New List(Of MixStatistics)


End Class
