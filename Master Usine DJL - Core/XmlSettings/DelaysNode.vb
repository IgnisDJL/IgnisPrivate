Namespace XmlSettings

    Public Class DelaysNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME = "delays"
        Public Const XPATH_TO_NODE = XmlSettings.EventsNode.XPATH_TO_NODE & "/" & NODE_NAME

        Public Const JUSTIFIABLE_DURATION_ATTRIBUTE As String = "justifiableDuration"

        Private _delayTypes As List(Of DelayTypeNode)

        Public Sub New(parentNode As Xml.XmlNode, myNode As Xml.XmlNode)
            MyBase.New(parentNode, myNode)

            Me._delayTypes = New List(Of DelayTypeNode)

            For Each childNode As Xml.XmlNode In Me.NODE.ChildNodes

                Me._delayTypes.Add(New DelayTypeNode(childNode))
            Next

        End Sub

        Public Function addType(name As String, color As Color) As DelayTypeNode

            Dim newNode As Xml.XmlNode = Me.NODE.OwnerDocument.CreateElement(DelayTypeNode.NODE_NAME)

            Dim nameAttr As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(DelayTypeNode.NAME_ATTRIBUTE)
            Dim colorAttr As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(DelayTypeNode.COLOR_ATTRIBUTE)

            newNode.Attributes.Append(nameAttr)
            newNode.Attributes.Append(colorAttr)

            Dim newDelayType As New DelayTypeNode(newNode)

            newDelayType.NAME = name
            newDelayType.COLOR = color

            Me._delayTypes.Add(newDelayType)
            Me.NODE.AppendChild(newNode)

            Return newDelayType
        End Function

        Public Function removeType(delayType As DelayTypeNode) As DelayTypeNode

            Me._delayTypes.Remove(delayType)
            Me.NODE.RemoveChild(delayType.NODE)

            Return delayType
        End Function

        Public ReadOnly Property DELAY_TYPES As List(Of DelayTypeNode)
            Get
                Return _delayTypes
            End Get
        End Property

        Public Property JUSTIFIABLE_DURATION As TimeSpan
            Get
                Return TimeSpan.FromSeconds(CDbl(Me.NODE.Attributes.GetNamedItem(JUSTIFIABLE_DURATION_ATTRIBUTE).Value))
            End Get
            Set(value As TimeSpan)
                Me.NODE.Attributes.GetNamedItem(JUSTIFIABLE_DURATION_ATTRIBUTE).Value = value.TotalSeconds
            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(JUSTIFIABLE_DURATION_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
            Me.JUSTIFIABLE_DURATION = TimeSpan.FromMinutes(10)
        End Sub

        Public Class DelayTypeNode

            Public Const NODE_NAME = "type"
            Public Const XPATH_TO_NODE = XmlSettings.DelaysNode.XPATH_TO_NODE & "/" & NODE_NAME

            Public Const NAME_ATTRIBUTE As String = "name"
            Public Const COLOR_ATTRIBUTE As String = "color"
            Public Const PAUSE_ATTRIBUTE As String = "pause"
            Public Const MAINTENANCE_ATTRIBUTE As String = "maintenance"
            Public Const BREAKAGE_ATTRIBUTE As String = "breakage"
            Public Const INTERN_ATTRIBUTE As String = "intern"
            Public Const EXTERN_ATTRIBUTE As String = "extern"

            Private typeNode As Xml.XmlNode

            Private _delays As List(Of DelayCodeNode)

            Public Sub New(myNode As Xml.XmlNode)
                Me.typeNode = myNode

                Me._delays = New List(Of DelayCodeNode)

                For Each childNode As Xml.XmlNode In Me.NODE.ChildNodes

                    Me._delays.Add(New DelayCodeNode(childNode))

                Next

            End Sub

            Public Function addDelay(code As Integer, description As String) As DelayCodeNode

                Dim newNode As Xml.XmlNode = Me.NODE.OwnerDocument.CreateElement(DelayCodeNode.NODE_NAME)

                Dim codeAttr As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(DelayCodeNode.CODE_ATTRIBUTE)

                Dim descAttr As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(DelayCodeNode.DESCRIPTION_ATTRIBUTE)

                newNode.Attributes.Append(codeAttr)
                newNode.Attributes.Append(descAttr)

                Dim newDelayCodeNode As New DelayCodeNode(newNode)

                newDelayCodeNode.CODE = code
                newDelayCodeNode.DESCRIPTION = description

                Return addDelay(newDelayCodeNode)
            End Function

            Public Function addDelay(delayCodeNode As DelayCodeNode) As DelayCodeNode

                Me._delays.Add(delayCodeNode)
                Me.NODE.AppendChild(delayCodeNode.NODE)

                Return delayCodeNode
            End Function

            Public Function removeDelay(delayCode As DelayCodeNode) As DelayCodeNode
                Me._delays.Remove(delayCode)
                Me.NODE.RemoveChild(delayCode.NODE)

                Return delayCode
            End Function

            Public ReadOnly Property DELAY_CODES As List(Of DelayCodeNode)
                Get
                    Return Me._delays
                End Get
            End Property

            Public Property NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property COLOR As Color
                Get
                    Dim attributeValue As String = Me.NODE.Attributes.GetNamedItem(COLOR_ATTRIBUTE).Value
                    Dim r As Integer = Convert.ToInt32(attributeValue.Substring(0, 2), 16)
                    Dim g As Int16 = Convert.ToInt32(attributeValue.Substring(2, 2), 16)
                    Dim b As Int16 = Convert.ToInt32(attributeValue.Substring(4, 2), 16)
                    Return Drawing.Color.FromArgb(r, g, b)
                End Get
                Set(value As Color)
                    Me.NODE.Attributes.GetNamedItem(COLOR_ATTRIBUTE).Value = Hex(value.R) & Hex(value.G) & Hex(value.B)
                End Set
            End Property

            Public Property IS_PAUSE As Boolean
                Get
                    Dim attr = Me.NODE.Attributes.GetNamedItem(PAUSE_ATTRIBUTE)

                    If (IsNothing(attr)) Then
                        Return False
                    Else
                        Return attr.Value = Settings.TRUE_VALUE
                    End If
                End Get
                Set(value As Boolean)

                    If (IsNothing(Me.NODE.Attributes.GetNamedItem(PAUSE_ATTRIBUTE))) Then
                        Me.NODE.Attributes.Append(Me.NODE.OwnerDocument.CreateAttribute(PAUSE_ATTRIBUTE))
                    End If

                    Me.NODE.Attributes.GetNamedItem(PAUSE_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
                End Set
            End Property

            Public Property IS_MAINTENANCE As Boolean
                Get
                    Dim attr = Me.NODE.Attributes.GetNamedItem(MAINTENANCE_ATTRIBUTE)

                    If (IsNothing(attr)) Then
                        Return False
                    Else
                        Return attr.Value = Settings.TRUE_VALUE
                    End If
                End Get
                Set(value As Boolean)

                    If (IsNothing(Me.NODE.Attributes.GetNamedItem(MAINTENANCE_ATTRIBUTE))) Then
                        Me.NODE.Attributes.Append(Me.NODE.OwnerDocument.CreateAttribute(MAINTENANCE_ATTRIBUTE))
                    End If

                    Me.NODE.Attributes.GetNamedItem(MAINTENANCE_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
                End Set
            End Property

            Public Property IS_BREAKAGE As Boolean
                Get
                    Dim attr = Me.NODE.Attributes.GetNamedItem(BREAKAGE_ATTRIBUTE)

                    If (IsNothing(attr)) Then
                        Return False
                    Else
                        Return attr.Value = Settings.TRUE_VALUE
                    End If
                End Get
                Set(value As Boolean)

                    If (IsNothing(Me.NODE.Attributes.GetNamedItem(BREAKAGE_ATTRIBUTE))) Then
                        Me.NODE.Attributes.Append(Me.NODE.OwnerDocument.CreateAttribute(BREAKAGE_ATTRIBUTE))
                    End If

                    Me.NODE.Attributes.GetNamedItem(BREAKAGE_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
                End Set
            End Property

            Public Property IS_INTERN As Boolean
                Get
                    Dim attr = Me.NODE.Attributes.GetNamedItem(INTERN_ATTRIBUTE)

                    If (IsNothing(attr)) Then
                        Return False
                    Else
                        Return attr.Value = Settings.TRUE_VALUE
                    End If
                End Get
                Set(value As Boolean)

                    If (IsNothing(Me.NODE.Attributes.GetNamedItem(INTERN_ATTRIBUTE))) Then
                        Me.NODE.Attributes.Append(Me.NODE.OwnerDocument.CreateAttribute(INTERN_ATTRIBUTE))
                    End If

                    Me.NODE.Attributes.GetNamedItem(INTERN_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
                End Set
            End Property

            Public Property IS_EXTERN As Boolean
                Get
                    Dim attr = Me.NODE.Attributes.GetNamedItem(EXTERN_ATTRIBUTE)

                    If (IsNothing(attr)) Then
                        Return False
                    Else
                        Return attr.Value = Settings.TRUE_VALUE
                    End If
                End Get
                Set(value As Boolean)

                    If (IsNothing(Me.NODE.Attributes.GetNamedItem(EXTERN_ATTRIBUTE))) Then
                        Me.NODE.Attributes.Append(Me.NODE.OwnerDocument.CreateAttribute(EXTERN_ATTRIBUTE))
                    End If

                    Me.NODE.Attributes.GetNamedItem(EXTERN_ATTRIBUTE).Value = If(value, Settings.TRUE_VALUE, Settings.FALSE_VALUE)
                End Set
            End Property

            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me.typeNode
                End Get
            End Property
        End Class

        Public Class DelayCodeNode

            Public Const NODE_NAME = "delay"

            Public Const CODE_ATTRIBUTE As String = "code"
            Public Const DESCRIPTION_ATTRIBUTE As String = "description"

            Private delayNode As Xml.XmlNode

            Public Sub New(myNode As Xml.XmlNode)
                Me.delayNode = myNode

            End Sub

            Public Property CODE As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(CODE_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(CODE_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property DESCRIPTION As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(DESCRIPTION_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(DESCRIPTION_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Overrides Function ToString() As String
                Return CODE & " - " & DESCRIPTION
            End Function

            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me.delayNode
                End Get
            End Property
        End Class

    End Class ' End DelaysNode
End Namespace
