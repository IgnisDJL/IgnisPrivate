Namespace XmlSettings

    Public Class DataFilesNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "dataFiles"
        Public Const XPATH_TO_NODE = XmlSettings.UsineNode.XPATH_TO_NODE & "/" & XmlSettings.DataFilesNode.NODE_NAME

        ''' <summary>Provides access to the csv node's content and attributes</summary>
        Public CSV As CSVNode

        ''' <summary>Provides access to the log node's content and attributes</summary>
        Public LOG As LOGNode

        ''' <summary>Provides access to the mdb node's content and attributes</summary>
        Public MDB As MDBNode

        Public Sub New(parentNode As Xml.XmlNode, dataFileNode As Xml.XmlNode)
            MyBase.New(parentNode, dataFileNode)

            Me.CSV = New CSVNode(Me.NODE, Me.NODE.SelectSingleNode(CSVNode.XPATH_TO_NODE))

            Me.LOG = New LOGNode(Me.NODE, Me.NODE.SelectSingleNode(LOGNode.XPATH_TO_NODE))

            Me.MDB = New MDBNode(Me.NODE, Me.NODE.SelectSingleNode(MDBNode.XPATH_TO_NODE))

        End Sub

        ''' <summary>
        ''' ADD COMMENT!!!
        ''' </summary>
        ''' <remarks></remarks>
        Public Class DataInfo

            Public Const NODE_NAME As String = "data"

            Public Const NAME_ATTRIBUTE As String = "name"
            Public Const TAG_ATTRIBUTE As String = "tag"
            Public Const UNIT_ATTRIBUTE As String = "unit"

            Protected parentNode As DataFileNode
            Protected dataNode As Xml.XmlNode

            Private isSubColumn As Boolean = False

            Public Sub New(myNode As Xml.XmlNode, myParent As DataFileNode, isRow As Boolean)

                Me.dataNode = myNode
                Me.parentNode = myParent
                Me.isSubColumn = isRow

            End Sub

            Public Property NAME As String
                Get
                    Return Me.dataNode.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.dataNode.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property TAG As Tag
                Get
                    Return Me.parentNode.verifyTag(Me.dataNode.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value, Me.isSubColumn)
                End Get
                Set(value As Tag)
                    Me.dataNode.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value = value.TAG_NAME
                End Set
            End Property

            Public Property UNIT As Unit
                Get
                    Dim attrValue = Me.dataNode.Attributes.GetNamedItem(UNIT_ATTRIBUTE)

                    If (IsNothing(attrValue)) Then
                        Return IGNIS.Unit.NO_UNIT
                    Else
                        Return IGNIS.Unit.parse(attrValue.Value)
                    End If

                End Get
                Set(value As Unit)
                    Me.dataNode.Attributes.GetNamedItem(UNIT_ATTRIBUTE).Value = value.SYMBOL
                End Set
            End Property

            Public Overrides Function ToString() As String
                Return Me.NAME
            End Function

        End Class


        Public Class ColumnInfo

            Public Const NODE_NAME As String = "column"

            Public Const NAME_ATTRIBUTE As String = "name"
            Public Const TAG_ATTRIBUTE As String = "tag"
            Public Const INDEX_ATTRIBUTE As String = "index"

            Private columnNode As Xml.XmlNode
            Private parentNode As DataFileNode

            Public Sub New(myNode As Xml.XmlNode, myParent As DataFileNode)

                Me.columnNode = myNode
                Me.parentNode = myParent

            End Sub

            Public Property NAME As String
                Get
                    Return Me.columnNode.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.columnNode.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property TAG As Tag
                Get
                    Return Me.parentNode.verifyTag(Me.columnNode.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value, False)
                End Get
                Set(value As Tag)
                    Me.columnNode.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value = value.TAG_NAME
                End Set
            End Property

            Public Property INDEX As Integer
                Get
                    Return Integer.Parse(Me.columnNode.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value)
                End Get
                Set(value As Integer)
                    Me.columnNode.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property ROWS As List(Of DataInfo)

            Public Overrides Function ToString() As String
                Return Me.NAME
            End Function

        End Class

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
        End Sub
    End Class
End Namespace