Public Class DrumDelayAdapter
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
        Dim averageDureeCycle As TimeSpan = getAverageDureeCycle(productionCycleList)

        If productionCycleList.Count = 0 Then
            Return getDateBoundaryList(startPeriod, endPeriod)
        Else

            dateBoundaryList = New List(Of List(Of Date))

            If (productionCycleList.Item(0).getEndOfCycle() - averageDureeCycle).Subtract(startPeriod) > TimeSpan.Zero Then
                dateBoundary = New List(Of Date)
                dateBoundary.Add(startPeriod)
                dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle() - averageDureeCycle)
                dateBoundaryList.Add(dateBoundary)
            End If

            If emptyProduction(productionCycleList.Item(0)) Then
                dateBoundary = New List(Of Date)
                dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle() - averageDureeCycle)
                dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle())
                dateBoundaryList.Add(dateBoundary)
            End If

            For index As Integer = 1 To productionCycleList.Count - 1


                If (productionCycleList.Item(index).getEndOfCycle() - averageDureeCycle).Subtract(productionCycleList.Item(index - 1).getEndOfCycle()) > TimeSpan.Zero Then
                    dateBoundary = New List(Of Date)
                    dateBoundary.Add(productionCycleList.Item(index - 1).getEndOfCycle())
                    dateBoundary.Add(productionCycleList.Item(index).getEndOfCycle() - averageDureeCycle)
                    dateBoundaryList.Add(dateBoundary)

                ElseIf emptyProduction(productionCycleList.Item(index)) Then
                    dateBoundary = New List(Of Date)
                    dateBoundary.Add(productionCycleList.Item(index).getEndOfCycle() - averageDureeCycle)
                    dateBoundary.Add(productionCycleList.Item(index).getEndOfCycle())
                    dateBoundaryList.Add(dateBoundary)
                End If
            Next

            If (endPeriod).Subtract(productionCycleList.Item(productionCycleList.Count - 1).getEndOfCycle()) > TimeSpan.Zero Then
                dateBoundary = New List(Of Date)
                dateBoundary.Add(productionCycleList.Item(productionCycleList.Count - 1).getEndOfCycle())
                dateBoundary.Add(endPeriod)
                dateBoundaryList.Add(dateBoundary)
            End If

            Return dateBoundaryList
        End If
    End Function

    Public Overloads Overrides Function getDateBoundaryList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of List(Of Date))
        If sourceFileComplementPathList.Count = 0 Then
            Return getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
        Else
            Return getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
        End If

    End Function

    Protected Function getAverageDureeCycle(productionCycleList As List(Of ProductionCycle)) As TimeSpan
        Dim cycleDuration As TimeSpan
        Dim averageCycleDuration = New Dictionary(Of TimeSpan, Integer)

        Dim actualDurationOccurance As Integer = 0
        Dim averageDuration As TimeSpan

        For index As Integer = 1 To Math.Floor((productionCycleList.Count * 0.25))


            cycleDuration = productionCycleList.Item(index).getEndOfCycle().Subtract(productionCycleList.Item(index - 1).getEndOfCycle())

            If (averageCycleDuration.Keys.Contains(cycleDuration)) Then
                averageCycleDuration.Item(cycleDuration) += 1
            Else
                averageCycleDuration.Add(cycleDuration, 1)
            End If

            If averageDuration = cycleDuration Then
                actualDurationOccurance = averageCycleDuration.Item(cycleDuration)

            ElseIf (actualDurationOccurance < averageCycleDuration.Item(cycleDuration)) Then
                actualDurationOccurance = averageCycleDuration.Item(cycleDuration)
                averageDuration = cycleDuration
            End If


        Next

        Return averageDuration
    End Function


    Private Function emptyProduction(productionCycle As ProductionCycle) As Boolean

        If productionCycle.isHotFeederEmpty() And productionCycle.isVirginAsphaltEmpty() And productionCycle.isColdFeederEmpty() Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
