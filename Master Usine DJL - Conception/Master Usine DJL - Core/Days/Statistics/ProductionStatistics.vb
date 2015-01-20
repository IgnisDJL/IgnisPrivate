Public MustInherit Class ProductionStatistics
    Implements IComparable

    Public Property NAME As String
    Public MustOverride ReadOnly Property TOTAL_MASS As Double

    Private cycleList As New List(Of Cycle)

    Public ReadOnly Property CYCLES As List(Of Cycle)
        Get
            Return Me.cycleList
        End Get
    End Property

    Public MustOverride Sub addCycle(cycle As Cycle, dataFileNode As DataFileNode)

    Public Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo

        obj = DirectCast(obj, ProductionStatistics)

        If (Me.TOTAL_MASS > obj.TOTAL_MASS) Then
            Return -1

        ElseIf (Me.TOTAL_MASS < obj.TOTAL_MASS) Then
            Return 1

        End If

        Return 0

    End Function

End Class
