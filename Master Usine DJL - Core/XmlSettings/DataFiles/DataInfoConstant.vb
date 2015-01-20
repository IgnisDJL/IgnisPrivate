Namespace Constants

    Public Class DataInfoConstant
        Implements DataInfo

        Private _tag As Tag

        Private _unit As Unit

        Public Sub New(tag As Tag, unit As Unit)
            Me._tag = tag
            Me._unit = unit
        End Sub

        Public ReadOnly Property TAG As Tag Implements DataInfo.TAG
            Get
                Return Me._tag
            End Get
        End Property

        Public Property UNIT As Unit Implements DataInfo.UNIT
            Get
                Return Me._unit
            End Get
            Set(value As Unit)
                Me._unit = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Me.TAG.ToString
        End Function

    End Class

End Namespace
