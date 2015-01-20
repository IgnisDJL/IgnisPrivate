Public Class FeedInfoConstant
    Implements FeedInfo

    Private _tag As Tag
    Private subColumns As New List(Of DataInfo)

    Public Sub New(tag As Tag, subColumns As DataInfo())
        Me._tag = tag
        Me.SUB_COLUMNS.AddRange(subColumns)
    End Sub

    Public ReadOnly Property TAG As Tag Implements FeedInfo.TAG
        Get
            Return Me._tag
        End Get
    End Property

    Public Property INDEX As Integer Implements FeedInfo.INDEX

    Public Property SUB_COLUMNS As List(Of DataInfo) Implements FeedInfo.SUB_COLUMNS
        Get
            Return Me.subColumns
        End Get
        Set(value As List(Of DataInfo))
            Me.subColumns = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Me.TAG.ToString
    End Function

End Class
