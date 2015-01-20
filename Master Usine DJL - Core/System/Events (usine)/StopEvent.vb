Public Class StopEvent
    Inherits SingleEvent

    Public Sub New(time As Date, message As String, Optional outputMessage As String = Nothing)
        MyBase.New(time, message, outputMessage)

    End Sub

    Public Property NEXT_START As StartEvent = Nothing

    Public Overrides ReadOnly Property DURATION As TimeSpan
        Get
            If (IsNothing(Me.NEXT_START)) Then

                Return TimeSpan.Zero

            Else

                Return Me.NEXT_START.TIME.Subtract(Me.TIME)

            End If
        End Get
    End Property

    Public Overrides Function ToString() As String

        If (IsNothing(Me.NEXT_START)) Then

            If (Events.STOP_EVENTS.Last.Equals(Me)) Then
                Return "Arrêt production"
            Else
                Return MyBase.ToString
            End If
        Else
            Return MyBase.ToString & " / " & Me.NEXT_START.ToString
        End If

    End Function

End Class
