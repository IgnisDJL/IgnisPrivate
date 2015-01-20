Namespace XmlSettings

    Public Class OperatorsNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "operators"
        Public Const XPATH_TO_NODE = XmlSettings.UsineNode.XPATH_TO_NODE & "/" & NODE_NAME

        Private _operators As New List(Of OperatorInfo)

        Public Sub New(parentNode As Xml.XmlNode, operatorsNode As Xml.XmlNode)
            MyBase.New(parentNode, operatorsNode)

            For Each operatorInfoNode As Xml.XmlNode In Me.NODE.SelectNodes(OperatorInfo.XPATH_TO_NODE)

                Me.OPERATORS.Add(New OperatorInfo(operatorInfoNode))

            Next

        End Sub

        Public ReadOnly Property OPERATORS As List(Of OperatorInfo)
            Get
                Return Me._operators
            End Get
        End Property

        Public Function removeOperator(operatorInfo As OperatorInfo) As OperatorInfo

            Me.OPERATORS.Remove(operatorInfo)
            Me.NODE.RemoveChild(operatorInfo.NODE)

            Return operatorInfo
        End Function

        Public Function addOperatorInfo(firstName As String, lastName As String) As OperatorInfo

            Dim newNode = Me.NODE.OwnerDocument.CreateElement(OperatorInfo.NODE_NAME)

            Dim firstNameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(OperatorInfo.FIRST_NAME_ATTRIBUTE)

            newNode.Attributes.Append(firstNameAttribute)

            Dim lastNameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(OperatorInfo.LAST_NAME_ATTRIBUTE)

            newNode.Attributes.Append(lastNameAttribute)

            Me.NODE.AppendChild(newNode)

            Dim operatorInformation As New OperatorInfo(newNode)

            operatorInformation.FIRST_NAME = firstName
            operatorInformation.LAST_NAME = lastName

            Me.OPERATORS.Add(operatorInformation)

            Return operatorInformation
        End Function

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
        End Sub

        Public Class OperatorInfo

            Public Const NODE_NAME As String = "operator"
            Public Const FIRST_NAME_ATTRIBUTE As String = "firstName"
            Public Const LAST_NAME_ATTRIBUTE As String = "lastName"
            Public Const XPATH_TO_NODE As String = XmlSettings.OperatorsNode.XPATH_TO_NODE & "/" & NODE_NAME

            Private _node As Xml.XmlNode

            Public Sub New(myNode As Xml.XmlNode)
                Me._node = myNode
            End Sub

            Public Property FIRST_NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(FIRST_NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(FIRST_NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property LAST_NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(LAST_NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(LAST_NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me._node
                End Get
            End Property

            Public Overrides Function ToString() As String
                Return Me.FIRST_NAME & " " & Me.LAST_NAME
            End Function

            Public Overrides Function equals(obj As Object) As Boolean

                If (TypeOf obj Is OperatorInfo) Then
                    Return DirectCast(obj, OperatorInfo).FIRST_NAME.Equals(Me.FIRST_NAME) AndAlso DirectCast(obj, OperatorInfo).LAST_NAME.Equals(Me.LAST_NAME)
                Else
                    Return False
                End If

            End Function

            Public Shared Operator =(mine As OperatorInfo, his As Object)
                Return mine.equals(his)
            End Operator

            Public Shared Operator <>(mine As OperatorInfo, his As Object)
                Return Not mine.equals(his)
            End Operator

        End Class
    End Class

End Namespace
