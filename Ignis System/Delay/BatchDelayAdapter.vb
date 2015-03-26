Public Class BatchDelayAdapter
    Inherits DelayAdapter

    '' *************************************************************************************************
    ''                                      Constructeur 
    '' *************************************************************************************************
    Public Sub New()

    End Sub

    '' *************************************************************************************************
    ''                              Fonctions en provenance du parent
    '' *************************************************************************************************
    Public Overloads Overrides Function getDateBoundaryList(startPeriod As Date, endPeriod As Date) As List(Of List(Of Date))
        Dim dateBoundary As List(Of Date)
        Dim dateBoundaryList As List(Of List(Of Date))

        dateBoundaryList = New List(Of List(Of Date))
        dateBoundary = New List(Of Date)

        dateBoundary.Add(startPeriod)
        dateBoundary.Add(endPeriod)
        dateBoundaryList.Add(dateBoundary)

        Return dateBoundaryList
    End Function

    Public Overloads Overrides Function getDateBoundaryList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle)) As List(Of List(Of Date))
        Dim dateBoundary As List(Of Date)
        Dim dateBoundaryList As List(Of List(Of Date))

        dateBoundaryList = New List(Of List(Of Date))


        If productionCycleList.Count = 0 Then
            Return getDateBoundaryList(startPeriod, endPeriod)
        Else

            If (productionCycleList.Item(0).getEndOfCycle() - TimeSpan.FromSeconds(productionCycleList.Item(0).getDureeCycle)).Subtract(startPeriod) > TimeSpan.Zero Then
                dateBoundary = New List(Of Date)
                dateBoundary.Add(startPeriod)
                dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle() - TimeSpan.FromSeconds(productionCycleList.Item(0).getDureeCycle))
                dateBoundaryList.Add(dateBoundary)
            End If

            For index As Integer = 1 To productionCycleList.Count - 1
                If (productionCycleList.Item(index).getEndOfCycle() - TimeSpan.FromSeconds(productionCycleList.Item(index).getDureeCycle)).Subtract(productionCycleList.Item(index - 1).getEndOfCycle) > TimeSpan.Zero Then
                    dateBoundary = New List(Of Date)
                    dateBoundary.Add(productionCycleList.Item(index - 1).getEndOfCycle)
                    dateBoundary.Add(productionCycleList.Item(index).getEndOfCycle() - TimeSpan.FromSeconds(productionCycleList.Item(index).getDureeCycle))
                    dateBoundaryList.Add(dateBoundary)
                End If
            Next

            If (endPeriod).Subtract(productionCycleList.Item(productionCycleList.Count - 1).getEndOfCycle()) > TimeSpan.Zero Then
                dateBoundary = New List(Of Date)
                dateBoundary.Add(productionCycleList.Item(productionCycleList.Count - 1).getEndOfCycle() - TimeSpan.FromSeconds(productionCycleList.Item(productionCycleList.Count - 1).getDureeCycle))
                dateBoundary.Add(endPeriod)
                dateBoundaryList.Add(dateBoundary)

            End If
            Return dateBoundaryList
        End If

    End Function

    Public Overloads Overrides Function getDateBoundaryList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of List(Of Date))
        Return getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
    End Function

End Class
