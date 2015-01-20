Namespace XmlSettings

    Public Class SummaryReportNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "summaryReport"
        Public Const XPATH_TO_NODE = ReportsNode.XPATH_TO_NODE & "/" & NODE_NAME

        Public Const OPEN_WHEN_DONE_READ_ONLY_ATTRIBUTE As String = "openReadOnlyWhenDone"
        Public Const OPEN_WHEN_DONE_WRITABLE_ATTRIBUTE As String = "openWritableWhenDone"

        Public Sub New(parentNode As Xml.XmlNode, wordNode As Xml.XmlNode)
            MyBase.New(parentNode, wordNode)

        End Sub

        Public Property ACTIVE As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value.Equals(Settings.IS_ACTIVE)
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value = If(value, Settings.IS_ACTIVE, Settings.IS_NOT_ACTIVE)
            End Set
        End Property

        Public Property OPEN_WHEN_DONE_WRITABLE As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(OPEN_WHEN_DONE_WRITABLE_ATTRIBUTE).Value.Equals(Settings.TRUE_VALUE)
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(OPEN_WHEN_DONE_WRITABLE_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
            End Set
        End Property

        Public Property OPEN_WHEN_DONE_READ_ONLY As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(OPEN_WHEN_DONE_READ_ONLY_ATTRIBUTE).Value.Equals(Settings.TRUE_VALUE)
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(OPEN_WHEN_DONE_READ_ONLY_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode

            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(Settings.ACTIVE_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(OPEN_WHEN_DONE_READ_ONLY_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(OPEN_WHEN_DONE_WRITABLE_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node

        End Function

        Protected Overrides Sub setDefaultValues()

            Me.ACTIVE = True
            Me.OPEN_WHEN_DONE_READ_ONLY = True
            Me.OPEN_WHEN_DONE_WRITABLE = False

        End Sub
    End Class

End Namespace