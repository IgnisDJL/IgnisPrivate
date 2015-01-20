Namespace XmlSettings

    Public Class EmailsNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "emails"
        Public Const XPATH_TO_NODE = XmlSettings.UsineNode.XPATH_TO_NODE & "/" & NODE_NAME

        Public Const CREDENTIALS_ATTRIBUTE As String = "credentials"
        Public Const PASSWORD_ATTRIBUTE As String = "password"
        Public Const PORT_ATTRIBUTE As String = "port"
        Public Const HOST_ATTRIBUTE As String = "host"
        Public Const SHOW_POPUP_AFTER_GENERATION_ATTRIBUTE As String = "showPopupAfterGeneration"

        Private Const XPATH_TO_RECIPIENTS_NODE As String = XPATH_TO_NODE & "/recipients"
        Private Const XPATH_TO_DEFAULT_RECIPIENTS_NODE As String = XPATH_TO_NODE & "/defaultRecipients"

        Private recipientsNode As Xml.XmlNode
        Private defaultRecipientsNode As Xml.XmlNode

        Private _recipients As List(Of RecipientInfo)
        Private _defaultRecipients As List(Of RecipientInfo)

        Public Sub New(parentNode As Xml.XmlNode, emailsNode As Xml.XmlNode)
            MyBase.New(parentNode, emailsNode)

            If (IsNothing(Me.NODE.Attributes.GetNamedItem(SHOW_POPUP_AFTER_GENERATION_ATTRIBUTE))) Then
                Me.NODE.Attributes.Append(Me.NODE.OwnerDocument.CreateAttribute(SHOW_POPUP_AFTER_GENERATION_ATTRIBUTE))
                Me.SHOW_POPUP_AFTER_GENERATION = True
            End If

            Me.recipientsNode = Me.NODE.SelectSingleNode(XPATH_TO_RECIPIENTS_NODE)
            If (IsNothing(Me.recipientsNode)) Then
                Me.recipientsNode = Me.NODE.OwnerDocument.CreateElement("recipients")
                Me.NODE.AppendChild(Me.recipientsNode)
            End If

            Me.defaultRecipientsNode = Me.NODE.SelectSingleNode(XPATH_TO_DEFAULT_RECIPIENTS_NODE)
            If (IsNothing(Me.defaultRecipientsNode)) Then
                Me.defaultRecipientsNode = Me.NODE.OwnerDocument.CreateElement("defaultRecipients")
                Me.NODE.AppendChild(Me.defaultRecipientsNode)
            End If

            Me._recipients = New List(Of RecipientInfo)

            For Each _recipientNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_RECIPIENTS_NODE & "/" & RecipientInfo.NODE_NAME)

                Me._recipients.Add(New RecipientInfo(_recipientNode))
            Next

            Me._defaultRecipients = New List(Of RecipientInfo)

            For Each _recipientNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_DEFAULT_RECIPIENTS_NODE & "/" & RecipientInfo.NODE_NAME)

                Me._defaultRecipients.Add(New RecipientInfo(_recipientNode))
            Next

        End Sub

        Public ReadOnly Property RECIPIENTS As List(Of RecipientInfo)
            Get
                Return Me._recipients
            End Get
        End Property

        Public ReadOnly Property DEFAULT_RECIPIENTS As List(Of RecipientInfo)
            Get
                Return Me._defaultRecipients
            End Get
        End Property

        Public Function removeRecipientInfo(recipientInfo As RecipientInfo) As RecipientInfo

            Me.RECIPIENTS.Remove(recipientInfo)
            Me.recipientsNode.RemoveChild(recipientInfo.NODE)

            Return recipientInfo
        End Function

        Public Function addRecipientInfo(address As String) As RecipientInfo

            Dim newNode = Me.NODE.OwnerDocument.CreateElement(RecipientInfo.NODE_NAME)

            Dim selectedAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(RecipientInfo.SELECTED_ATTRIBUTE)

            newNode.Attributes.Append(selectedAttribute)

            Me.recipientsNode.AppendChild(newNode)

            Dim recipientInformation = New RecipientInfo(newNode)

            recipientInformation.ADDRESS = address
            recipientInformation.SELECTED = Me.RECIPIENTS.Count = 0 ' Selected if it's the only recipient

            Me.RECIPIENTS.Add(recipientInformation)

            Return recipientInformation
        End Function

        Public Function removeDefaultRecipientInfo(recipientInfo As RecipientInfo) As RecipientInfo

            Me.DEFAULT_RECIPIENTS.Remove(recipientInfo)
            Me.defaultRecipientsNode.RemoveChild(recipientInfo.NODE)

            Return recipientInfo
        End Function

        Public Function addDefaultRecipientInfo(address As String) As RecipientInfo

            Dim newNode = Me.NODE.OwnerDocument.CreateElement(RecipientInfo.NODE_NAME)

            Me.defaultRecipientsNode.AppendChild(newNode)

            Dim recipientInformation = New RecipientInfo(newNode)

            recipientInformation.ADDRESS = address

            Me.DEFAULT_RECIPIENTS.Add(recipientInformation)

            Return recipientInformation
        End Function

        Public Property CREDENTIALS As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(CREDENTIALS_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(CREDENTIALS_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property PASSWORD As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(PASSWORD_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(PASSWORD_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property HOST As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(HOST_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(HOST_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property PORT As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(PORT_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(PORT_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property SHOW_POPUP_AFTER_GENERATION As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(SHOW_POPUP_AFTER_GENERATION_ATTRIBUTE).Value = Settings.TRUE_VALUE
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(SHOW_POPUP_AFTER_GENERATION_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(CREDENTIALS_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(PASSWORD_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(HOST_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(PORT_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()

            Me.CREDENTIALS = "ignis.djl@gmail.com"
            Me.PASSWORD = "masterusine"
            Me.HOST = "smtp.gmail.com"
            Me.PORT = 587
        End Sub

        Public Class RecipientInfo

            Public Const NODE_NAME As String = "recipient"

            Public Const SELECTED_ATTRIBUTE As String = "selected"

            Private _node As Xml.XmlNode

            Public Sub New(_node As Xml.XmlNode)

                Me._node = _node

            End Sub

            Public Property ADDRESS As String
                Get
                    Return Me._node.InnerText
                End Get
                Set(value As String)
                    Me._node.InnerText = value
                End Set
            End Property

            Public Property SELECTED As Boolean
                Get
                    Dim attr = Me.NODE.Attributes.GetNamedItem(SELECTED_ATTRIBUTE)

                    If (IsNothing(attr)) Then
                        Return False
                    Else
                        Return Me.NODE.Attributes.GetNamedItem(SELECTED_ATTRIBUTE).Value.Equals(Settings.TRUE_VALUE)
                    End If

                End Get
                Set(value As Boolean)

                    Dim attr = Me.NODE.Attributes.GetNamedItem(SELECTED_ATTRIBUTE)

                    If (Not IsNothing(attr)) Then
                        attr.Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
                    End If

                End Set
            End Property

            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me._node
                End Get
            End Property

        End Class
    End Class

End Namespace
