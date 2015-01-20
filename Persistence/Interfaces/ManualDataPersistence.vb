Public MustInherit Class ManualDataPersistence

    ''' <summary>Verifies that the format of the data storage is correct.</summary>
    Public MustOverride Function verifyFormat() As Boolean

    ''' <summary>#comment</summary>
    Public MustOverride Sub reset()

    ''' <summary>#comment</summary>
    Public MustOverride Function getData(day As DateTime) As ManualData

    ''' <summary>#comment</summary>
    Public MustOverride Function addData(data As ManualData) As ManualData

End Class
