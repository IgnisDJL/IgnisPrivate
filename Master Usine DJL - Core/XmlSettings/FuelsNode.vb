Namespace XmlSettings

    Public Class FuelsNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "fuels"
        Public Const XPATH_TO_NODE = XmlSettings.UsineNode.XPATH_TO_NODE & "/" & NODE_NAME

        Private Const FUEL_1_NAME_ATTRIBUTE As String = "fuel1Name"
        Private Const FUEL_2_NAME_ATTRIBUTE As String = "fuel2Name"
        Private Const FUEL_1_UNIT_ATTRIBUTE As String = "fuel1Unit"
        Private Const FUEL_2_UNIT_ATTRIBUTE As String = "fuel2Unit"

        Public Sub New(parentNode As Xml.XmlNode, fuelsNode As Xml.XmlNode)
            MyBase.New(parentNode, fuelsNode)

        End Sub

        Public Property FUEL_1_NAME As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(FUEL_1_NAME_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(FUEL_1_NAME_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property FUEL_2_NAME As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(FUEL_2_NAME_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(FUEL_2_NAME_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property FUEL_1_UNIT As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(FUEL_1_UNIT_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(FUEL_1_UNIT_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property FUEL_2_UNIT As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(FUEL_2_UNIT_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(FUEL_2_UNIT_ATTRIBUTE).Value = value
            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(FUEL_1_NAME_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(FUEL_2_NAME_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(FUEL_1_UNIT_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(FUEL_2_UNIT_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()

            Me.FUEL_1_NAME = "Carburant principal"
            Me.FUEL_2_NAME = "Carburant secondaire"
            Me.FUEL_1_UNIT = "L"
            Me.FUEL_2_UNIT = "m³"
        End Sub
    End Class

End Namespace
