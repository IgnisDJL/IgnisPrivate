Public Class Events



    Private Shared allEvents As New List(Of SingleEvent)
    Private Shared otherImportantEvents As New List(Of SingleEvent)
    Private Shared stopEvents As New List(Of StopEvent)
    Private Shared startEvents As New List(Of StartEvent)
    Private Shared mixChangeEvents As New List(Of MixChangeEvent)
    Private Shared mixRecipeChangeEvents As New List(Of MixRecipeChangeEvent)

    Private Shared eventSettings As XmlSettings.EventsNode = XmlSettings.Settings.instance.Usine.Events

    Public Shared Sub addEventFromLog(OADate As Double, message As String)

        Dim eventDate = Date.FromOADate(OADate)

        For Each importantEvt In eventSettings.Important.IMPORTANT_EVENTS

            If (importantEvt.MESSAGE.Equals(message)) Then

                If (Not (message.Equals(otherImportantEvents.Last.MESSAGE) AndAlso eventDate.Subtract(otherImportantEvents.Last.TIME).CompareTo(TimeSpan.FromMinutes(1)) < 0)) Then

                    otherImportantEvents.Add(New SingleEvent(eventDate, message, importantEvt.ALT_MESSAGE))
                    Exit Sub
                End If

            End If

        Next

        For Each startEvt In eventSettings.Start_.START_EVENTS

            If (startEvt.MESSAGE.Equals(message)) Then

                If (stopEvents.Count > 0) Then

                    If (startEvents.Count - stopEvents.Count = 0) Then

                        Dim newStartEv = New StartEvent(eventDate, message, stopEvents.Last, startEvt.ALT_MESSAGE)
                        startEvents.Add(newStartEv)
                        stopEvents.Last.NEXT_START = newStartEv
                        Exit Sub

                    Else

                        ' To many stops or starts
                        'Debugger.Break()

                    End If

                Else

                    ' First start of the day...
                    Dim newStartEv = New StartEvent(eventDate, message, Nothing)
                    startEvents.Add(newStartEv)
                    otherImportantEvents.Add(newStartEv)
                    Exit Sub

                End If


            End If

        Next

        For Each stopEvt In eventSettings.Stop_.STOP_EVENTS

            If (stopEvt.MESSAGE.Equals(message)) Then

                If (startEvents.Count > 0) Then

                    Dim newStopEv = New StopEvent(eventDate, message, stopEvt.ALT_MESSAGE)

                    stopEvents.Add(newStopEv)
                    Exit Sub

                Else

                    ' Stop without start
                    Debugger.Break()

                End If

            End If

        Next

        allEvents.Add(New SingleEvent(eventDate, message))

    End Sub


    Public Shared Sub addStartEvent(time As Date)

        If (stopEvents.Count > 0) Then
            startEvents.Add(New StartEvent(time, eventSettings.Start_.DEFAULT_MESSAGE, stopEvents.Last))
            stopEvents.Last.NEXT_START = startEvents.Last
        Else
            Dim startEv = New StartEvent(time, "", Nothing)
            startEvents.Add(startEv)
            otherImportantEvents.Add(startEv)
        End If

    End Sub

    Public Shared Sub addStopEvent(time As Date)

        stopEvents.Add(New StopEvent(time, eventSettings.Stop_.DEFAULT_MESSAGE))

    End Sub

    Public Shared Sub addMixChangeEvent(time As Date, message As String)

        mixChangeEvents.Add(New MixChangeEvent(time, message))

    End Sub

    Public Shared Sub addMixRecipeChangeEvent(time As Date, message As String)

        mixRecipeChangeEvents.Add(New MixRecipeChangeEvent(time, message))

    End Sub

    Private Shared importantEvents As List(Of SingleEvent)
    Public Shared ReadOnly Property IMPORTANT_EVENTS As List(Of SingleEvent)
        Get
            If (IsNothing(importantEvents)) Then
                importantEvents = New List(Of SingleEvent)
            End If

            importantEvents.AddRange(otherImportantEvents)
            importantEvents.AddRange(stopEvents)
            importantEvents.AddRange(mixChangeEvents)
            importantEvents.AddRange(mixRecipeChangeEvents)

            Return importantEvents

        End Get
    End Property

    Public Shared ReadOnly Property STOP_EVENTS As List(Of StopEvent)
        Get
            Return stopEvents
        End Get
    End Property

    Public Shared ReadOnly Property START_EVENTS As List(Of StartEvent)
        Get
            Return startEvents
        End Get
    End Property

    Public Shared ReadOnly Property ALL_EVENTS As List(Of SingleEvent)
        Get
            Dim newList As New List(Of SingleEvent)
            newList.AddRange(allEvents)
            newList.AddRange(IMPORTANT_EVENTS)

            Return newList
        End Get
    End Property


    Public Shared ReadOnly Property STOP_EVENTS_DURATION As TimeSpan
        Get
            Dim stopEventsDuration As TimeSpan
            For Each stopEv In stopEvents
                stopEventsDuration = stopEventsDuration.Add(stopEv.DURATION)
            Next

            Return stopEventsDuration
        End Get
    End Property

    Private Shared importantEventsDuration As TimeSpan
    Public Shared ReadOnly Property IMPORTANT_EVENTS_DURATION As TimeSpan
        Get
            If (importantEventsDuration.Equals(TimeSpan.Zero)) Then
                For Each impEv In IMPORTANT_EVENTS
                    importantEventsDuration = importantEventsDuration.Add(impEv.DURATION)
                Next
            End If

            Return importantEventsDuration
        End Get
    End Property

    Public Shared ReadOnly Property NB_STOPS As Integer
        Get
            If (stopEvents.Count > 0) Then
                Return stopEvents.Count - 1

            Else
                Return 0
            End If
        End Get
    End Property

    Public Shared ReadOnly Property NB_STARTS As Integer
        Get
            Return startEvents.Count
        End Get
    End Property

    Public Shared ReadOnly Property NB_MIX_CHANGE As Integer
        Get
            Return mixChangeEvents.Count
        End Get
    End Property

    Public Shared ReadOnly Property NB_MIX_RECIPE_CHANGE As Integer
        Get
            If (mixRecipeChangeEvents.Count > 0) Then
                Dim count As Integer = 1

                mixRecipeChangeEvents.Sort()

                For i = 1 To mixRecipeChangeEvents.Count - 1
                    If (Not mixRecipeChangeEvents(i - 1).TIME.Equals(mixRecipeChangeEvents(i).TIME)) Then
                        count += 1
                    End If
                Next

                Return count
            Else
                Return 0
            End If

        End Get
    End Property


    ' Recheck this... dont know if it's still accurate
    Public Shared Function rectifyCycleDuration(cycle As LOGCycle) As TimeSpan

        For Each startEvent In startEvents

            If (cycle.TIME.CompareTo(startEvent.TIME) > 0 And cycle.PREVIOUS_CYCLE.TIME.CompareTo(startEvent.TIME) < 0) Then

                Return cycle.TIME.Subtract(cycle.PREVIOUS_CYCLE.TIME).Subtract(startEvent.DURATION)

            End If

        Next

        Return cycle.TIME.Subtract(cycle.PREVIOUS_CYCLE.TIME)

    End Function

    Public Shared Sub reset()

        allEvents = New List(Of SingleEvent)
        otherImportantEvents = New List(Of SingleEvent)
        stopEvents = New List(Of StopEvent)
        startEvents = New List(Of StartEvent)
        mixChangeEvents = New List(Of MixChangeEvent)
        mixRecipeChangeEvents = New List(Of MixRecipeChangeEvent)
        importantEvents = New List(Of SingleEvent)

    End Sub

End Class
