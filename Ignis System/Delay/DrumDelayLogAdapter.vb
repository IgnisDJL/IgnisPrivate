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
            Dim averageDureeCycle As TimeSpan = getAverageDureeCycle(productionCycleList)

            dateBoundaryEventList = New List(Of List(Of Date))
            dateBoundaryList = getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
            eventLog = New EventsFile(sourceFileComplementList.Item(0))

            For Each stopEvent As StopEvent In eventLog.getEvents().STOP_EVENTS
                dateBoundary = New List(Of Date)
                dateBoundary.Add(stopEvent.TIME)

                If IsNothing(stopEvent.NEXT_START) Then
                    dateBoundary.Add(endPeriod)
                Else
                    dateBoundary.Add(stopEvent.NEXT_START.TIME)
                End If


                dateBoundaryEventList.Add(dateBoundary)
            Next

            dateBoundaryFinalList = New List(Of List(Of Date))
            dateBoundaryFinalList.InsertRange(0, dateBoundaryList)

            For Each dateBoundaryEvent As List(Of Date) In dateBoundaryEventList

                For Each dateBoundaryBrut As List(Of Date) In dateBoundaryList

                    If dateBoundaryBrut.Item(0) > dateBoundaryEvent.Item(0) And dateBoundaryBrut.Item(1) < dateBoundaryEvent.Item(1) And dateBoundaryBrut.Item(0) < dateBoundaryEvent.Item(1) Then
                        dateBoundaryFinalList.Remove(dateBoundaryBrut)
                        dateBoundaryFinalList.Add(dateBoundaryEvent)
                    End If
                Next
            Next

            Return dateBoundaryFinalList
        End If
    End Function

End Class
