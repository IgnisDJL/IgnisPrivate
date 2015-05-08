Public Class DrumDelayLogAdapter
    Inherits DrumDelayAdapter

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

    Public Overloads Overrides Function getDateBoundaryList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle), sourceFileComplementList As List(Of String)) As List(Of List(Of Date))
        If sourceFileComplementList.Count = 0 Then
            Return getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
        Else
            Dim eventLog As EventsFile
            Dim dateBoundary As List(Of Date)
            Dim dateBoundaryList As List(Of List(Of Date))
            Dim dateBoundaryFinalList As List(Of List(Of Date))
            Dim dateBoundaryEventList As List(Of List(Of Date))

            dateBoundaryEventList = New List(Of List(Of Date))

            Dim date1 As Date = New Date(endPeriod.Year, endPeriod.Month, endPeriod.Day)
            Dim date2 As Date = New Date(startPeriod.Year, startPeriod.Month, startPeriod.Day)

            Dim nomberOfProductionDay As Integer = date1.Subtract(date2).TotalDays

            If (sourceFileComplementList.Count = nomberOfProductionDay + 1) Then

                If productionCycleList.Count = 0 Then
                    Return getDateBoundaryList(startPeriod, endPeriod)
                Else
                    dateBoundaryList = New List(Of List(Of Date))

                    If (productionCycleList.Item(0).getEndOfCycle() - productionCycleList.Item(0).getDureeCycle).Subtract(startPeriod) > TimeSpan.Zero Then
                        dateBoundary = New List(Of Date)
                        dateBoundary.Add(startPeriod)
                        dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle() - productionCycleList.Item(0).getDureeCycle)
                        dateBoundaryList.Add(dateBoundary)
                    End If

                    If emptyProduction(productionCycleList.Item(0)) Then
                        dateBoundary = New List(Of Date)
                        dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle() - productionCycleList.Item(0).getDureeCycle)
                        dateBoundary.Add(productionCycleList.Item(0).getEndOfCycle())
                        dateBoundaryList.Add(dateBoundary)
                    End If

                End If


                For Each sourceFileComplement As String In sourceFileComplementList

                    eventLog = New EventsFile(sourceFileComplement)

                    For Each stopEvent As StopEvent In eventLog.getEvents().STOP_EVENTS
                        dateBoundary = New List(Of Date)

                        If (stopEvent.TIME < endPeriod) Then

                            If stopEvent.TIME < startPeriod Then
                                dateBoundary.Add(startPeriod)
                            Else
                                dateBoundary.Add(stopEvent.TIME)
                            End If

                        End If

                        If Not IsNothing(stopEvent.NEXT_START) Then

                            If (stopEvent.NEXT_START.TIME > startPeriod) Then

                                If (stopEvent.NEXT_START.TIME > endPeriod) Then
                                    dateBoundary.Add(endPeriod)
                                Else
                                    dateBoundary.Add(stopEvent.NEXT_START.TIME)
                                End If
                            
                            End If
                        Else
                            dateBoundary.Add(getEndPeriod(stopEvent.TIME, endPeriod))
                        End If

                        dateBoundaryEventList.Add(dateBoundary)
                    Next

                Next

                If (endPeriod).Subtract(productionCycleList.Item(productionCycleList.Count - 1).getEndOfCycle()) > TimeSpan.Zero Then
                    dateBoundary = New List(Of Date)
                    dateBoundary.Add(productionCycleList.Item(productionCycleList.Count - 1).getEndOfCycle())
                    dateBoundary.Add(endPeriod)
                    dateBoundaryList.Add(dateBoundary)
                End If

            Else
                dateBoundaryList = getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
            End If

            dateBoundaryFinalList = New List(Of List(Of Date))
            dateBoundaryFinalList.InsertRange(0, dateBoundaryList)

            'For Each dateBoundaryEvent As List(Of Date) In dateBoundaryEventList

            '    For Each dateBoundaryBrut As List(Of Date) In dateBoundaryList

            '        If dateBoundaryBrut.Item(0) > dateBoundaryEvent.Item(0) And dateBoundaryBrut.Item(1) < dateBoundaryEvent.Item(1) And dateBoundaryBrut.Item(0) < dateBoundaryEvent.Item(1) Then
            '            dateBoundaryFinalList.Remove(dateBoundaryBrut)
            '            dateBoundaryFinalList.Add(dateBoundaryEvent)
            '        End If
            '    Next
            'Next

            Return dateBoundaryFinalList
        End If
    End Function

    Private Function getEndPeriod(stopEventTime As Date, endPeriod As Date) As Date

        If stopEventTime.Day < endPeriod.Day Then
            Return New Date(stopEventTime.Year, stopEventTime.Month, stopEventTime.Day + 1) - TimeSpan.FromSeconds(1)
        End If

        Return endPeriod
    End Function

End Class
