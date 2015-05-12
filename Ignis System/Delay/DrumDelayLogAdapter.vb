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

        Dim date1 As Date = New Date(endPeriod.Year, endPeriod.Month, endPeriod.Day)
        Dim date2 As Date = New Date(startPeriod.Year, startPeriod.Month, startPeriod.Day)

        Dim nomberOfProductionDay As Integer = date1.Subtract(date2).TotalDays

        If Not sourceFileComplementList.Count = nomberOfProductionDay + 1 Then
            Return getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
        Else
            Dim eventLog As EventsFile
            Dim dateBoundary As List(Of Date)
            Dim dateBoundaryList As List(Of List(Of Date))
            Dim dateBoundaryFinalList As List(Of List(Of Date))
            Dim dateBoundaryEventList As List(Of List(Of Date))

            dateBoundaryEventList = New List(Of List(Of Date))
            dateBoundaryList = getDateBoundaryList(startPeriod, endPeriod, productionCycleList)

            For Each sourceFileComplement As String In sourceFileComplementList

                eventLog = New EventsFile(sourceFileComplement)

                For Each stopEvent As StopEvent In eventLog.getEvents().STOP_EVENTS
                    dateBoundary = New List(Of Date)
                    dateBoundary.Add(stopEvent.TIME)

                    If IsNothing(stopEvent.NEXT_START) Then
                        dateBoundary.Add(getEndPeriod(stopEvent.TIME, endPeriod))
                    Else
                        dateBoundary.Add(stopEvent.NEXT_START.TIME)
                    End If


                    dateBoundaryEventList.Add(dateBoundary)
                Next

            Next

            dateBoundaryFinalList = New List(Of List(Of Date))
            dateBoundaryFinalList.InsertRange(0, dateBoundaryList)
            Dim dateBoundaryBrutChange As Boolean = False

            For Each dateBoundaryBrut As List(Of Date) In dateBoundaryList

                For Each dateBoundaryEvent As List(Of Date) In dateBoundaryEventList

                    If dateBoundaryEvent.Item(0) <= dateBoundaryBrut.Item(0) Then

                        If dateBoundaryEvent.Item(1) >= dateBoundaryBrut.Item(1) Or (Not dateBoundaryBrut.Item(0).Day = dateBoundaryBrut.Item(1).Day And dateBoundaryBrut.Item(0).Day = dateBoundaryEvent.Item(0).Day And dateBoundaryBrut.Item(1).Day = dateBoundaryEvent.Item(1).Day) Then

                            If ((Not dateBoundaryBrut.Item(0).Day = dateBoundaryBrut.Item(1).Day And dateBoundaryBrut.Item(0).Day = dateBoundaryEvent.Item(0).Day And dateBoundaryBrut.Item(1).Day = dateBoundaryEvent.Item(1).Day)) Then
                                Dim dateBoundaryEventEnd = New List(Of Date)
                                dateBoundaryEventEnd.Add(dateBoundaryEvent.Item(0))
                                dateBoundaryEventEnd.Add(dateBoundaryBrut.Item(1))

                                dateBoundaryFinalList.Add(dateBoundaryEventEnd)
                                dateBoundaryBrutChange = True
                            Else
                                dateBoundaryFinalList.Add(dateBoundaryEvent)
                                dateBoundaryBrutChange = True
                            End If

                        End If

                    End If
                Next

                If dateBoundaryBrutChange = True Then
                    dateBoundaryFinalList.Remove(dateBoundaryBrut)
                    dateBoundaryBrutChange = False
                End If
            Next

            Return dateBoundaryFinalList
        End If
    End Function

    Private Function getEndPeriod(stopEventTime As Date, endPeriod As Date) As Date

        If stopEventTime.Day < endPeriod.Day Then
            Return New Date(stopEventTime.Year, stopEventTime.Month, stopEventTime.Day + 1)
        End If

        Return endPeriod
    End Function

End Class
