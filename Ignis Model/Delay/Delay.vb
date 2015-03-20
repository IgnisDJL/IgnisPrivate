Public Class Delay

    Private numDelay As Integer
    Private justification As String
    Private startDelay As Date
    Private endDelay As Date

    Sub New(startDelay As Date, endDelay As Date)

    End Sub

    Public Function getDuration() As TimeSpan
        Return endDelay.Subtract(startDelay)
    End Function


End Class
