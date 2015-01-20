Namespace XmlSettings

    Public Class AdminNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "admin"
        Public Const XPATH_TO_NODE = "/settings/" & NODE_NAME

        Public Const PASSWORD_ATTRIBUTE = "password"

        Private permissionNodes As List(Of PermissionNode)

        Public Sub New(parentNode As Xml.XmlNode, adminNode As Xml.XmlNode)
            MyBase.New(parentNode, adminNode)

            Me.permissionNodes = New List(Of PermissionNode)

            For Each permissionElement As Xml.XmlNode In Me.NODE.SelectNodes(PermissionNode.XPATH_TO_NODE)

                Me.permissionNodes.Add(New PermissionNode(permissionElement))
            Next

            Me.createPermissionIfNotExists(PermissionNode.CAN_OPEN_DATA_FILES)
            Me.createPermissionIfNotExists(PermissionNode.CAN_MODIFY_DELAY_CODES)
            Me.createPermissionIfNotExists(PermissionNode.CAN_CHANGE_EMAIL_SETTINGS)
            Me.createPermissionIfNotExists(PermissionNode.CAN_RESET_DATABASE)

        End Sub

        Public ReadOnly Property PERMISSION_NODES As List(Of PermissionNode)
            Get
                Return Me.permissionNodes
            End Get
        End Property

        Public ReadOnly Property PASSWORD As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(PASSWORD_ATTRIBUTE).Value
            End Get
        End Property

        Private Sub createPermissionIfNotExists(name As String)

            Dim itExists As Boolean = False
            For Each _permission As PermissionNode In Me.permissionNodes

                If (_permission.Name = name) Then
                    itExists = True
                    Exit For
                End If
            Next

            If (Not itExists) Then

                Me.permissionNodes.Add(PermissionNode.create(Me.NODE, name))
            End If
        End Sub

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode

            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(PASSWORD_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
            ' No defaults....
        End Sub
    End Class
End Namespace

