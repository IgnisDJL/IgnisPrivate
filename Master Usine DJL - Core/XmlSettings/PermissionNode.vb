Namespace XmlSettings

    Public Class PermissionNode

        Public Const NODE_NAME As String = "permission"
        Public Const XPATH_TO_NODE = AdminNode.XPATH_TO_NODE & "/" & NODE_NAME

        Public Const NAME_ATTRIBUTE = "name"
        Public Const VALUE_ATTRIBUTE = "value"
        Public Const TIME_SET_ATTRIBUTE = "timeSet"

        ' Permission names
        Public Const CAN_OPEN_DATA_FILES = "canOpenDataFiles"
        Public Const CAN_MODIFY_DELAY_CODES = "canModifyDelayCodes"
        Public Const CAN_CHANGE_EMAIL_SETTINGS = "canChangeEmailSettings"
        Public Const CAN_RESET_DATABASE = "canResetDatabase"

        Private permissionName As String

        Private permissionNode As Xml.XmlNode

        Public Sub New(permissionNode As Xml.XmlNode)

            If (IsNothing(permissionNode)) Then
                Throw New NullReferenceException
            End If

            Me.permissionNode = permissionNode

            Me.permissionName = Me.permissionNode.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value
        End Sub

        Public ReadOnly Property Name As String
            Get
                Return Me.permissionName
            End Get
        End Property

        Public Property Value As Boolean
            Get
                Return Me.permissionNode.Attributes.GetNamedItem(VALUE_ATTRIBUTE).Value.Equals(Settings.TRUE_VALUE)
            End Get
            Set(value As Boolean)

                Dim setTime As String = Now.ToString("dd/MM/yyyy hh:mm:ss")

                Me.permissionNode.Attributes.GetNamedItem(VALUE_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)

                Me.permissionNode.Attributes.GetNamedItem(TIME_SET_ATTRIBUTE).Value = setTime
            End Set
        End Property

        Public Shared Function create(parentNode As Xml.XmlNode, name As String) As PermissionNode

            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            Dim nameAttr = document.CreateAttribute(NAME_ATTRIBUTE)
            nameAttr.Value = name

            node.Attributes.Append(nameAttr)
            node.Attributes.Append(document.CreateAttribute(VALUE_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(TIME_SET_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return New PermissionNode(node)
        End Function

    End Class
End Namespace
