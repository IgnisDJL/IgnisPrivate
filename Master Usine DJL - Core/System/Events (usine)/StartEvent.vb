Public Class StartEvent
    Inherits SingleEvent

    Public Sub New(time As Date, message As String, previousStop As StopEvent, Optional outputMessage As String = Nothing)
        MyBase.New(time, message, outputMessage)

        Me.previousStop = previousStop

    End Sub

    Private previousStop As StopEvent
    Public ReadOnly Property PREVIOUS_STOP As StopEvent
        Get
            Return Me.previousStop
        End Get
    End Property

    Public Overrides ReadOnly Property DURATION As TimeSpan
        Get
            If (IsNothing(Me.previousStop)) Then
                Return TimeSpan.Zero
            Else
                Return Me.TIME.Subtract(PREVIOUS_STOP.TIME)
            End If
        End Get
    End Property

    Public Overrides Function ToString() As String

        If (IsNothing(Me.previousStop)) Then
            Return "Démmarage production"
        Else
            Return MyBase.ToString
        End If

    End Function

End Class

