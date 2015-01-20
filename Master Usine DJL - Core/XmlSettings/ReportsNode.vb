
Namespace XmlSettings

    Public Class ReportsNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "reports"
        Public Const XPATH_TO_NODE = "/settings/" & NODE_NAME

        Public Const MASS_UNIT_ATTRIBUTE As String = "massUnit"
        Public Const PERCENT_UNIT_ATTRIBUTE As String = "percentUnit"
        Public Const PRODUCTION_SPEED_UNIT_ATTRIBUTE As String = "productionUnit"
        Public Const TEMPERATURE_UNIT_ATTRIBUTE As String = "temperatureUnit"

        Public SummaryReport As SummaryReportNode

        Public Sub New(parentNode As Xml.XmlNode, reportNode As Xml.XmlNode)
            MyBase.New(parentNode, reportNode)

            SummaryReport = New SummaryReportNode(Me.NODE, Me.NODE.SelectSingleNode(SummaryReportNode.XPATH_TO_NODE))

        End Sub

        Public Property MASS_UNIT As Unit
            Get
                Return Unit.parse(Me.NODE.Attributes.GetNamedItem(MASS_UNIT_ATTRIBUTE).Value)
            End Get
            Set(value As Unit)
                Me.NODE.Attributes.GetNamedItem(MASS_UNIT_ATTRIBUTE).Value = value.SYMBOL
            End Set
        End Property

        Public Property PERCENT_UNIT As Unit
            Get
                Return Unit.parse(Me.NODE.Attributes.GetNamedItem(PERCENT_UNIT_ATTRIBUTE).Value)
            End Get
            Set(value As Unit)
                Me.NODE.Attributes.GetNamedItem(PERCENT_UNIT_ATTRIBUTE).Value = value.SYMBOL
            End Set
        End Property

        Public Property PRODUCTION_SPEED_UNIT As Unit
            Get
                Return Unit.parse(Me.NODE.Attributes.GetNamedItem(PRODUCTION_SPEED_UNIT_ATTRIBUTE).Value)
            End Get
            Set(value As Unit)
                Me.NODE.Attributes.GetNamedItem(PRODUCTION_SPEED_UNIT_ATTRIBUTE).Value = value.SYMBOL
            End Set
        End Property

        Public Property TEMPERATURE_UNIT As Unit
            Get
                Return Unit.parse(Me.NODE.Attributes.GetNamedItem(TEMPERATURE_UNIT_ATTRIBUTE).Value)
            End Get
            Set(value As Unit)
                Me.NODE.Attributes.GetNamedItem(TEMPERATURE_UNIT_ATTRIBUTE).Value = value.SYMBOL
            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode

            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(MASS_UNIT_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(TEMPERATURE_UNIT_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(PRODUCTION_SPEED_UNIT_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(PERCENT_UNIT_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()

            Me.MASS_UNIT = Unit.DEFAULT_MASS_UNIT
            Me.TEMPERATURE_UNIT = Unit.DEFAULT_TEMPERATURE_UNIT
            Me.PRODUCTION_SPEED_UNIT = Unit.DEFAULT_PRODUCTION_SPEED_UNIT
            Me.PERCENT_UNIT = Unit.DEFAULT_PERCENT_UNIT
        End Sub
    End Class

End Namespace
