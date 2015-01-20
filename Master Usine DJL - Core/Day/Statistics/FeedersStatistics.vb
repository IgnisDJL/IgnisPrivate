Public Class FeedersStatistics
    Implements IComparable(Of FeedersStatistics)

    Public Property INDEX As Integer

    Public Property LOCATION As String

    Public Property MATERIAL_NAME As String

    Public Property TOTAL_MASS As Double


    Public Function CompareTo(other As FeedersStatistics) As Integer Implements IComparable(Of FeedersStatistics).CompareTo

        If (IsNothing(Me.INDEX) OrElse IsNothing(other.INDEX)) Then
            Return 0
        End If

        Return Me.INDEX.CompareTo(other.INDEX)
    End Function
End Class
