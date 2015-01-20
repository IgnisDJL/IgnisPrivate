Public Class SingleEvent
    Implements IComparable(Of SingleEvent)

    Public Sub New(time As Date, message As String, Optional outputMessage As String = Nothing)
        Me._time = time
        Me._message = message
        Me.outputMessage = outputMessage
    End Sub

    Protected _time As Date
    Public ReadOnly Property TIME As Date
        Get
            Return Me._time
        End Get
    End Property

    Protected _message As String
    Public ReadOnly Property MESSAGE As String
        Get
            Return Me._message
        End Get
    End Property

    Protected _duration As TimeSpan = TimeSpan.Zero
    Public Overridable ReadOnly Property DURATION As TimeSpan
        Get
            Return Me._duration
        End Get
    End Property

    Private outputMessage As String
    Public Overrides Function ToString() As String

        If (IsNothing(Me.outputMessage)) Then

            Return Me._message

        Else

            Return Me.outputMessage

        End If

    End Function

    Public Function CompareTo(other As SingleEvent) As Integer Implements IComparable(Of SingleEvent).CompareTo

        Return Me.TIME.CompareTo(other.TIME)

    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        If (TypeOf obj Is SingleEvent) Then
            Return Me.MESSAGE.Equals(DirectCast(obj, SingleEvent).MESSAGE)
        End If
        Return False
    End Function

    Public Shared Operator =(mine As SingleEvent, other As Object)
        Return mine.Equals(other)
    End Operator

    Public Shared Operator <>(mine As SingleEvent, other As Object)
        Return Not mine.Equals(other)
    End Operator

End Class