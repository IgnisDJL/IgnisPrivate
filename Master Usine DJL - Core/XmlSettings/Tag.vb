Public Class Tag

    Private defaultUnit As Unit
    Private isIndexed As Boolean
    Private displayName As String
    Private _tag As String

    Public Sub New(tag As String, displayName As String, defaultUnit As Unit, isIndexed As Boolean)
        Me._tag = tag
        Me.displayName = displayName
        Me.defaultUnit = defaultUnit
        Me.isIndexed = isIndexed
    End Sub

    Public ReadOnly Property TAG_NAME As String
        Get
            Return Me._tag
        End Get
    End Property

    Public ReadOnly Property DEFAULT_UNIT As Unit
        Get
            Return Me.defaultUnit
        End Get
    End Property

    Public ReadOnly Property IS_INDEXED As Boolean
        Get
            Return Me.isIndexed
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.displayName
    End Function

    Public Shared Operator =(mine As Tag, his As Tag)
        Return mine.TAG_NAME = his.TAG_NAME AndAlso mine.displayName = his.displayName
    End Operator

    Public Shared Operator <>(mine As Tag, his As Tag)
        Return Not mine = his
    End Operator

End Class
