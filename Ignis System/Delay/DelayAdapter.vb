Public MustInherit Class DelayAdapter

    Public Sub New()
    End Sub

    Public MustOverride Function getDateBoundaryList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of List(Of Date))
    Public MustOverride Function getDateBoundaryList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle)) As List(Of List(Of Date))
    Public MustOverride Function getDateBoundaryList(startPeriod As Date, endPeriod As Date) As List(Of List(Of Date))

End Class
