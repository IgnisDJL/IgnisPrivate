Public Class EventsStatistics

    Private _delays As List(Of Delay)
    Private _justifiableDelays As List(Of Delay)

    Private _pausesDuration As TimeSpan
    Private _nbPauses As Integer
    Private _maintenanceDuration As TimeSpan
    Private _nbMaintenance As Integer
    Private _internDelaysDuration As TimeSpan
    Private _delaysDuration As TimeSpan
    Private _internBreakagesDuration As TimeSpan
    Private _nbOfBreakages As Integer
    Private _justifiableDelaysDuration As TimeSpan
    Private _internWithBreakageDuration As TimeSpan
    Private _internWithoutBreakageDuration As TimeSpan
    Private _externDuration As TimeSpan
    Private _otherDelaysDuration As TimeSpan

    Public Sub New()

    End Sub

    Public Sub preCompile(operationStartTime As Date, operationEndTime As Date, startEvents As List(Of StartEvent), stopEvents As List(Of StopEvent))

        ' #shiftdenuit
        Me._delays = New List(Of Delay)
        Me._justifiableDelays = New List(Of Delay)

        Me.reset()

        ' First delay
        If (startEvents.Count > 0 AndAlso startEvents.First.TIME.Subtract(operationStartTime).TotalMinutes > 1) Then
            Me._delays.Add(New Delay(operationStartTime, startEvents.First.TIME))
        End If

        ' All delays (except first and last)
        Dim stopEvt As StopEvent

        For i = 0 To stopEvents.Count - 2

            stopEvt = stopEvents(i)

            If (Not IsNothing(stopEvt.NEXT_START)) Then
                Me._delays.Add(New Delay(stopEvt.TIME, stopEvt.NEXT_START.TIME))
            End If
        Next

        ' Last delay #refactor - sometimes last cycle is later than last event
        If (stopEvents.Count > 0 AndAlso operationEndTime.Subtract(stopEvents.Last.TIME).TotalMinutes > 1) Then
            Me._delays.Add(New Delay(stopEvents.Last.TIME, operationEndTime))
        End If

        ' Justifiable delays
        Dim minimumDurationForJustification As TimeSpan = XmlSettings.Settings.instance.Usine.Events.Delays.JUSTIFIABLE_DURATION

        For Each _delay As Delay In Me._delays

            If (_delay.Duration.CompareTo(minimumDurationForJustification) >= 1) Then

                _delay.IsJustifiable = True
                Me._justifiableDelays.Add(_delay)
            End If

        Next

    End Sub

    Public Sub finalizeCompilation()

        Me.reset()

        For Each _delay As Delay In Me._delays

            If (_delay.IsUnknown) Then

                ' Delay that was specified as unknown
                Me._delaysDuration = Me._delaysDuration.Add(_delay.Duration)

                If (_delay.IsJustifiable) Then
                    Me._justifiableDelaysDuration = Me._justifiableDelaysDuration.Add(_delay.Duration)
                End If

            ElseIf (IsNothing(_delay.Code)) Then

                ' Delay that has no code attributed to...

                Me._delaysDuration = Me._delaysDuration.Add(_delay.Duration)

            ElseIf (_delay.Type.IsPause) Then

                Me._pausesDuration = Me._pausesDuration.Add(_delay.Duration)
                Me._nbPauses += 1

            ElseIf (_delay.Type.IsMaintenance) Then

                Me._maintenanceDuration = Me._maintenanceDuration.Add(_delay.Duration)
                Me._nbMaintenance += 1

            Else

                If (_delay.Type.IsIntern AndAlso _delay.Type.IsBreakage) Then

                    Me._internDelaysDuration = Me._internDelaysDuration.Add(_delay.Duration)
                    Me._internBreakagesDuration = Me._internBreakagesDuration.Add(_delay.Duration)
                    Me._internWithBreakageDuration = Me._internWithBreakageDuration.Add(_delay.Duration)
                    Me._nbOfBreakages += 1
                End If

                If (_delay.Type.IsIntern AndAlso Not _delay.Type.IsBreakage) Then

                    Me._internDelaysDuration = Me._internDelaysDuration.Add(_delay.Duration)

                    Me._internWithoutBreakageDuration = Me._internBreakagesDuration.Add(_delay.Duration)
                End If

                If (_delay.Type.IsExtern) Then

                    Me._externDuration = Me._externDuration.Add(_delay.Duration)

                End If

                If (_delay.Type.IsOther) Then

                    Me._otherDelaysDuration = Me._otherDelaysDuration.Add(_delay.Duration)
                End If

                Me._delaysDuration = Me._delaysDuration.Add(_delay.Duration)

                If (_delay.IsJustifiable) Then
                    Me._justifiableDelaysDuration = Me._justifiableDelaysDuration.Add(_delay.Duration)
                End If

            End If
        Next
    End Sub

    Public Sub reset()

        Me._pausesDuration = TimeSpan.Zero
        Me._nbPauses = 0
        Me._maintenanceDuration = TimeSpan.Zero
        Me._nbMaintenance = 0
        Me._internDelaysDuration = TimeSpan.Zero
        Me._delaysDuration = TimeSpan.Zero
        Me._internBreakagesDuration = TimeSpan.Zero
        Me._nbOfBreakages = 0
        Me._justifiableDelaysDuration = TimeSpan.Zero
        Me._internWithBreakageDuration = TimeSpan.Zero
        Me._internWithoutBreakageDuration = TimeSpan.Zero
        Me._externDuration = TimeSpan.Zero
        Me._otherDelaysDuration = TimeSpan.Zero
    End Sub

    Public Function splitDelay(delay As Delay, splitTime As Date) As Delay()

        Dim indexOfDelay As Integer = Me.Delays.IndexOf(delay)

        Me.Delays.RemoveAt(indexOfDelay)

        Dim firstDelay As Delay = New Delay(delay.StartTime, splitTime)
        Dim secondDelay As Delay = New Delay(splitTime, delay.EndTime)

        firstDelay.IsUnknown = delay.IsUnknown
        firstDelay.Code = delay.Code
        firstDelay.Justification = delay.Justification

        Me.Delays.Insert(indexOfDelay, firstDelay)
        Me.Delays.Insert(indexOfDelay + 1, secondDelay)

        Return {firstDelay, secondDelay}
    End Function

    Public Function mergeDelays(delay As Delay) As Delay

        Dim indexOfDelay As Integer = Me.Delays.IndexOf(delay)

        Me.Delays.RemoveAt(indexOfDelay)

        Dim secondDelay As Delay = Me.Delays(indexOfDelay)

        Me.Delays.RemoveAt(indexOfDelay)

        Dim newDelay As New Delay(delay.StartTime, secondDelay.EndTime)

        Me.Delays.Insert(indexOfDelay, newDelay)

        Return newDelay
    End Function

    Public ReadOnly Property Delays As List(Of Delay)
        Get
            Return Me._delays
        End Get
    End Property

    Public ReadOnly Property JustifiableDelays As List(Of Delay)
        Get
            Return Me._justifiableDelays
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property CT01_PausesDuration As TimeSpan
        Get
            Return _pausesDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property NbPauses As Integer
        Get
            Return _nbPauses
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property CT01_MaintenanceDuration As TimeSpan
        Get
            Return _maintenanceDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property NbMaintenance As Integer
        Get
            Return _nbMaintenance
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property InternDelaysDuration As TimeSpan
        Get
            Return _internDelaysDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property DT01_DelaysDuration As TimeSpan
        Get
            Return _delaysDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property InternBreakagesDuration As TimeSpan
        Get
            Return _internBreakagesDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property DT03_BreakageNumber As Integer
        Get
            Return _nbOfBreakages
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property JustifiableDelaysDuration As TimeSpan
        Get
            Return _justifiableDelaysDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property InternWithBreakageDuration As TimeSpan
        Get
            Return _internWithBreakageDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property InternWithoutBreakageDuration As TimeSpan
        Get
            Return _internWithoutBreakageDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property ExternDuration As TimeSpan
        Get
            Return _externDuration
        End Get
    End Property

    '''<summary></summary>
    Public ReadOnly Property OtherDelaysDuration As TimeSpan
        Get
            Return _otherDelaysDuration
        End Get
    End Property
End Class
