Namespace XmlSettings

    Public Class DataInfoNode
        Inherits XmlSettings.XMLSettingsNode
        Implements DataInfo

        Public Const NODE_NAME As String = "data"

        Public Const TAG_ATTRIBUTE As String = "tag"
        Public Const UNIT_ATTRIBUTE As String = "unit"
        Public Const INDEX_ATTRIBUTE As String = "index"

        Protected parentNode As DataFileNode

        Private _tag As Tag

        Private isSubColumn As Boolean = False

        Public Sub New(myNode As Xml.XmlNode, myParent As DataFileNode, myTag As Tag, isRow As Boolean)
            MyBase.New(myNode)

            Me.parentNode = myParent
            Me.isSubColumn = isRow

            Me._tag = myTag

        End Sub

        Public ReadOnly Property TAG As Tag Implements DataInfo.TAG
            Get
                Return Me._tag
            End Get
        End Property

        Public Property UNIT As Unit Implements DataInfo.UNIT
            Get
                Dim attrValue = Me.NODE.Attributes.GetNamedItem(UNIT_ATTRIBUTE)

                Return If(IsNothing(attrValue), IGNIS.Unit.NO_UNIT, IGNIS.Unit.parse(attrValue.Value))

            End Get
            Set(value As Unit)
                Me.NODE.Attributes.GetNamedItem(UNIT_ATTRIBUTE).Value = value.SYMBOL
            End Set
        End Property

        Public Property INDEX As Integer
            Get
                Return If(IsNothing(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE)), -1, CInt(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value))
            End Get
            Set(value As Integer)
                If (Not IsNothing(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE))) Then
                    Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value = value
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return If(Me.INDEX = -1, Me.TAG.ToString, Me.TAG.ToString & " " & Me.INDEX)
        End Function

    End Class

End Namespace
