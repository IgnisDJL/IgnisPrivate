Namespace XmlSettings

    Public Class EventsNode
        Inherits ComplexSettingsNode

        Public Const XPATH_TO_NODE = XmlSettings.UsineNode.XPATH_TO_NODE & "/" & NODE_NAME
        Public Const NODE_NAME As String = "events"

        Public Important As ImportantNode
        Public Start_ As StartNode
        Public Stop_ As StopNode
        Public Delays As DelaysNode

        Public Sub New(parentNode As Xml.XmlNode, eventsNode As Xml.XmlNode)
            MyBase.New(parentNode, eventsNode)

            Me.Important = New ImportantNode(Me.NODE, Me.NODE.SelectSingleNode(ImportantNode.XPATH_TO_NODE))

            Me.Start_ = New StartNode(Me.NODE, Me.NODE.SelectSingleNode(StartNode.XPATH_TO_NODE))

            Me.Stop_ = New StopNode(Me.NODE, Me.NODE.SelectSingleNode(StopNode.XPATH_TO_NODE))

            Me.Delays = New DelaysNode(Me.NODE, Me.NODE.SelectSingleNode(DelaysNode.XPATH_TO_NODE))

        End Sub

        Public Function addEventInfo(message As String, altMessage As String, type As Constants.Input.Events.EventType) As EventInfo

            Dim newNode = Me.NODE.OwnerDocument.CreateElement(EventInfo.NODE_NAME)

            Dim replaceAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(EventInfo.ALT_NAME_ATTRIBUTE)

            newNode.Attributes.Append(replaceAttribute)

            Dim eventInformation As New EventInfo(newNode, message)
            eventInformation.ALT_MESSAGE = altMessage

            Return addEventInfo(eventInformation, type)

        End Function

        Public Function addEventInfo(eventInfo As EventInfo, type As Constants.Input.Events.EventType) As EventInfo

            Select Case type

                Case Constants.Input.Events.EventType.IMPORTANT
                    Me.Important.appendNode(eventInfo.NODE)
                    Me.Important.IMPORTANT_EVENTS.Add(eventInfo)

                Case Constants.Input.Events.EventType.START
                    Me.Start_.appendNode(eventInfo.NODE)
                    Me.Start_.START_EVENTS.Add(eventInfo)

                Case Constants.Input.Events.EventType.STOP_
                    Me.Stop_.appendNode(eventInfo.NODE)
                    Me.Stop_.STOP_EVENTS.Add(eventInfo)

            End Select

            Return eventInfo

        End Function

        Public Property ACTIVE As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value.Equals(Settings.IS_ACTIVE)
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value = If(value, Settings.IS_ACTIVE, Settings.IS_NOT_ACTIVE)
            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(Settings.ACTIVE_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
            Me.ACTIVE = False
        End Sub

        Public Class ImportantNode
            Inherits ComplexSettingsNode

            Public Const NODE_NAME = "important"
            Public Const XPATH_TO_NODE = XmlSettings.EventsNode.XPATH_TO_NODE & "/" & NODE_NAME

            Public Sub New(parentNode As Xml.XmlNode, importantNode As Xml.XmlNode)
                MyBase.New(parentNode, importantNode)

                For Each node As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & EventInfo.NODE_NAME)

                    Me.importantEvents.Add(New EventInfo(node, node.InnerText))

                Next

            End Sub

            Private importantEvents As New List(Of EventInfo)
            Public ReadOnly Property IMPORTANT_EVENTS As List(Of EventInfo)
                Get
                    Return Me.importantEvents
                End Get
            End Property

            Public Sub removeEventInfo(eventInfo As EventInfo)

                Me.IMPORTANT_EVENTS.Remove(eventInfo)
                Me.NODE.RemoveChild(eventInfo.NODE)

            End Sub

            Public Sub appendNode(node As Xml.XmlNode)
                Me.NODE.AppendChild(node)
            End Sub

            Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
                Dim document = parentNode.OwnerDocument

                Dim node = document.CreateElement(NODE_NAME)

                parentNode.AppendChild(node)

                Return node
            End Function

            Protected Overrides Sub setDefaultValues()
            End Sub
        End Class

        Public Class StartNode
            Inherits ComplexSettingsNode

            Public Const NODE_NAME As String = "start"
            Public Const XPATH_TO_NODE = XmlSettings.EventsNode.XPATH_TO_NODE & "/" & NODE_NAME

            Public Property DEFAULT_MESSAGE As String = Constants.Input.Events.DEFAULT_START_MESSAGE

            Public Sub New(parentNode As Xml.XmlNode, startNode As Xml.XmlNode)
                MyBase.New(parentNode, startNode)

                For Each node As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & EventInfo.NODE_NAME)

                    Me.startEvents.Add(New EventInfo(node, node.InnerText))
                Next

            End Sub

            Private startEvents As New List(Of EventInfo)
            Public ReadOnly Property START_EVENTS As List(Of EventInfo)
                Get
                    Return Me.startEvents
                End Get
            End Property

            Public Sub removeEventInfo(eventInfo As EventInfo)

                Me.START_EVENTS.Remove(eventInfo)
                Me.NODE.RemoveChild(eventInfo.NODE)

            End Sub

            Public Sub appendNode(node As Xml.XmlNode)
                Me.NODE.AppendChild(node)
            End Sub

            Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
                Dim document = parentNode.OwnerDocument

                Dim node = document.CreateElement(NODE_NAME)

                parentNode.AppendChild(node)

                Return node
            End Function

            Protected Overrides Sub setDefaultValues()
            End Sub
        End Class

        Public Class StopNode
            Inherits ComplexSettingsNode

            Public Const NODE_NAME As String = "stop"
            Public Const XPATH_TO_NODE = XmlSettings.EventsNode.XPATH_TO_NODE & "/" & NODE_NAME

            Public Property DEFAULT_MESSAGE As String = Constants.Input.Events.DEFAULT_STOP_MESSAGE

            Public Sub New(parentNode As Xml.XmlNode, stopNode As Xml.XmlNode)
                MyBase.New(parentNode, stopNode)

                For Each node As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & EventInfo.NODE_NAME)

                    Me.stopEvents.Add(New EventInfo(node, node.InnerText))

                Next

            End Sub

            Private stopEvents As New List(Of EventInfo)
            Public ReadOnly Property STOP_EVENTS As List(Of EventInfo)
                Get
                    Return Me.stopEvents
                End Get
            End Property

            Public Sub removeEventInfo(eventInfo As EventInfo)

                Me.STOP_EVENTS.Remove(eventInfo)
                Me.NODE.RemoveChild(eventInfo.NODE)

            End Sub

            Public Sub appendNode(node As Xml.XmlNode)
                Me.NODE.AppendChild(node)
            End Sub

            Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
                Dim document = parentNode.OwnerDocument

                Dim node = document.CreateElement(NODE_NAME)

                parentNode.AppendChild(node)

                Return node
            End Function

            Protected Overrides Sub setDefaultValues()
            End Sub
        End Class

        Public Class EventInfo
            Inherits SettingsNode

            Public Const NODE_NAME As String = "event"
            Public Const ALT_NAME_ATTRIBUTE As String = "replace"

            Private eventNode As Xml.XmlNode
            Public Overrides ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me.eventNode
                End Get
            End Property

            Public Sub New(myNode As Xml.XmlNode, message As String)

                Me.eventNode = myNode

                Me.NODE.InnerText = message

            End Sub

            Public Property MESSAGE As String
                Get
                    Return Me.NODE.InnerText
                End Get
                Set(value As String)
                    Me.NODE.InnerText = value
                End Set
            End Property

            Public Property ALT_MESSAGE As String
                Get
                    Dim altMessage = Me.NODE.Attributes.GetNamedItem(ALT_NAME_ATTRIBUTE).Value
                    Return If(altMessage = "", Me.MESSAGE, altMessage)
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(ALT_NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Overrides Function ToString() As String
                Return Me.ALT_MESSAGE
            End Function

            Public Overrides Function Equals(obj As Object) As Boolean

                If (TypeOf obj Is EventInfo) Then
                    Return DirectCast(obj, EventInfo).MESSAGE.Equals(Me.MESSAGE)
                Else
                    Return False
                End If

            End Function

            Public Shared Operator =(mine As EventInfo, his As Object)
                Return mine.Equals(his)
            End Operator

            Public Shared Operator <>(mine As EventInfo, his As Object)
                Return Not mine.Equals(his)
            End Operator
        End Class

    End Class
End Namespace