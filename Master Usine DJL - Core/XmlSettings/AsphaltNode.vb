Namespace XmlSettings

    Public Class AsphaltNode
        Inherits ComplexSettingsNode

        Public Const XPATH_TO_NODE As String = XmlSettings.UsineNode.XPATH_TO_NODE & "/" & NODE_NAME
        Public Const NODE_NAME As String = "asphaltInfo"

        Private asphaltTanks As New List(Of TankInfo)

        Private unknownTanks As New List(Of UnknownTankNode)

        Public Sub New(parentNode As Xml.XmlNode, asphaltNode As Xml.XmlNode)
            MyBase.New(parentNode, asphaltNode)

            For Each tankNode As Xml.XmlNode In Me.NODE.SelectNodes(TankInfo.XPATH_TO_NODE)

                Me.TANKS.Add(New TankInfo(tankNode))

            Next

            For Each unknownTank As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & UnknownTankNode.NODE_NAME)

                Me.UNKNOWN_TANKS.Add(New UnknownTankNode(unknownTank))

            Next

        End Sub

        Public ReadOnly Property TANKS As List(Of TankInfo)
            Get
                Return asphaltTanks
            End Get
        End Property

        Public ReadOnly Property UNKNOWN_TANKS As List(Of UnknownTankNode)
            Get
                Return unknownTanks
            End Get
        End Property

        Public Sub removeTankInfo(tankInfo As TankInfo)

            Me.TANKS.Remove(tankInfo)
            Me.NODE.RemoveChild(tankInfo.NODE)

        End Sub

        Public Function addTankInfo(tankName As String, asphaltName As String, setPointTemp As String) As TankInfo

            Dim newNode = Me.NODE.OwnerDocument.CreateElement(TankInfo.NODE_NAME)

            Dim nameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(TankInfo.NAME_ATTRIBUTE)

            newNode.Attributes.Append(nameAttribute)

            Dim temperatureAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(TankInfo.SET_POINT_TEMP_ATTRIBUTE)

            newNode.Attributes.Append(temperatureAttribute)


            Dim tankInformation As New TankInfo(newNode)

            tankInformation.TANK_NAME = tankName
            tankInformation.ASPHALT_NAME = asphaltName
            tankInformation.SET_POINT_TEMP = setPointTemp


            Return addTankInfo(tankInformation)

        End Function

        Public Function addTankInfo(tankInfo As TankInfo) As TankInfo

            Me.NODE.AppendChild(tankInfo.NODE)

            For Each unknownTank In Me.UNKNOWN_TANKS

                If (unknownTank.TANK_NAME.Equals(tankInfo.TANK_NAME)) Then
                    Me.removeUnknownTank(unknownTank)

                    Exit For
                End If

            Next

            Me.TANKS.Add(tankInfo)

            Return tankInfo
        End Function

        Public Sub removeUnknownTank(unknownTank As UnknownTankNode)

            Me.UNKNOWN_TANKS.Remove(unknownTank)
            Me.NODE.RemoveChild(unknownTank.NODE)

        End Sub

        Public Function addUnknownTank(tankName As String, asphaltName As String)

            Dim addToList As Boolean = True

            For Each tankNode In Me.UNKNOWN_TANKS

                If (tankNode.TANK_NAME.Equals(tankName) AndAlso tankNode.ASPHALT_NAME.Equals(asphaltName)) Then

                    addToList = False

                    Return tankNode

                End If

            Next

            If (addToList) Then

                Dim newNode = Me.NODE.OwnerDocument.CreateElement(UnknownTankNode.NODE_NAME)

                Dim nameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(UnknownTankNode.NAME_ATTRIBUTE)

                newNode.Attributes.Append(nameAttribute)

                Me.NODE.AppendChild(newNode)

                Dim tankInformation As New UnknownTankNode(newNode)

                tankInformation.TANK_NAME = tankName
                tankInformation.ASPHALT_NAME = asphaltName

                Me.UNKNOWN_TANKS.Add(tankInformation)

                Return tankInformation

            End If

            Debugger.Break()
            Return Nothing

        End Function

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
        End Sub

        Public Class TankInfo

            Public Const NODE_NAME As String = "tank"
            Public Const NAME_ATTRIBUTE As String = "name"
            Public Const SET_POINT_TEMP_ATTRIBUTE As String = "temperature"
            Public Const XPATH_TO_NODE As String = XmlSettings.AsphaltNode.XPATH_TO_NODE & "/" & NODE_NAME

            Private _node As Xml.XmlNode

            Public Sub New(myNode As Xml.XmlNode)
                Me._node = myNode
            End Sub

            Public Property TANK_NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property ASPHALT_NAME As String
                Get
                    Return Me.NODE.InnerText
                End Get
                Set(value As String)
                    Me.NODE.InnerText = value
                End Set
            End Property

            Public Property SET_POINT_TEMP As Double
                Get
                    Return Double.Parse(Me.NODE.Attributes.GetNamedItem(SET_POINT_TEMP_ATTRIBUTE).Value, Globalization.NumberStyles.Any, XmlSettings.Settings.LANGUAGE.Culture)
                End Get
                Set(value As Double)
                    Me.NODE.Attributes.GetNamedItem(SET_POINT_TEMP_ATTRIBUTE).Value = value.ToString
                End Set
            End Property

            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me._node
                End Get
            End Property

            Public Overrides Function ToString() As String
                Return Me.ASPHALT_NAME.ToString
            End Function

            Public Overrides Function equals(obj As Object) As Boolean

                If (TypeOf obj Is TankInfo) Then
                    Return DirectCast(obj, TankInfo).TANK_NAME.Equals(Me.TANK_NAME) Or DirectCast(obj, TankInfo).ASPHALT_NAME.Equals(Me.ASPHALT_NAME)
                Else
                    Return False
                End If

            End Function

            Public Shared Operator =(mine As TankInfo, his As Object)
                Return mine.equals(his)
            End Operator

            Public Shared Operator <>(mine As TankInfo, his As Object)
                Return Not mine.equals(his)
            End Operator

        End Class ' End tankinfo

        Public Class UnknownTankNode

            Public Const NODE_NAME As String = "unknownTank"

            Public Const NAME_ATTRIBUTE As String = "name"

            Private _node As Xml.XmlNode

            Public Sub New(myNode As Xml.XmlNode)

                Me._node = myNode

            End Sub

            Public Property TANK_NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property ASPHALT_NAME As String
                Get
                    Return Me.NODE.InnerText
                End Get
                Set(value As String)
                    Me.NODE.InnerText = value
                End Set
            End Property
            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me._node
                End Get
            End Property

            Public Overrides Function ToString() As String
                Return Me.TANK_NAME
            End Function

        End Class
    End Class
End Namespace
