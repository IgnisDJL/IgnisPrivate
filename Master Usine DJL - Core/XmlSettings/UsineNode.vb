Namespace XmlSettings

    Public Class UsineNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "usine"
        Public Const XPATH_TO_NODE = "/settings/" & NODE_NAME

        Public Const PLANT_ID_ATTRIBUTE As String = "id"
        Public Const PLANT_NAME_ATTRIBUTE As String = "name"
        Public Const USB_ROOT_ATTRIBUTE As String = "usbRoot"

        Public DataFiles As DataFilesNode

        Public Events As EventsNode

        Public AsphaltInfo As AsphaltNode

        Public RecipesInfo As RecipesNode

        Public OperatorsInfo As OperatorsNode

        Public FuelsInfo As FuelsNode

        Public EmailsInfo As EmailsNode

        Public Sub New(parentNode As Xml.XmlNode, usineNode As Xml.XmlNode)
            MyBase.New(parentNode, usineNode)

            Me.DataFiles = New DataFilesNode(Me.NODE, Me.NODE.SelectSingleNode(DataFilesNode.XPATH_TO_NODE))

            Me.Events = New EventsNode(Me.NODE, Me.NODE.SelectSingleNode(EventsNode.XPATH_TO_NODE))

            Me.AsphaltInfo = New AsphaltNode(Me.NODE, Me.NODE.SelectSingleNode(AsphaltNode.XPATH_TO_NODE))

            Me.RecipesInfo = New RecipesNode(Me.NODE, Me.NODE.SelectSingleNode(RecipesNode.XPATH_TO_NODE))

            Me.OperatorsInfo = New OperatorsNode(Me.NODE, Me.NODE.SelectSingleNode(OperatorsNode.XPATH_TO_NODE))

            Me.FuelsInfo = New FuelsNode(Me.NODE, Me.NODE.SelectSingleNode(FuelsNode.XPATH_TO_NODE))

            Me.EmailsInfo = New EmailsNode(Me.NODE, Me.NODE.SelectSingleNode(EmailsNode.XPATH_TO_NODE))

        End Sub

        Public Property PLANT_NAME As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(PLANT_NAME_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(PLANT_NAME_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property PLANT_ID As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(PLANT_ID_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(PLANT_ID_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property USB_DIRECTORY As String
            Get
                Return Me.NODE.Attributes.GetNamedItem(USB_ROOT_ATTRIBUTE).Value
            End Get
            Set(value As String)
                Me.NODE.Attributes.GetNamedItem(USB_ROOT_ATTRIBUTE).Value = value
            End Set
        End Property

        Public Property TYPE As Constants.Settings.UsineType
            Get

                With Me.DataFiles

                    If (.CSV.ACTIVE AndAlso .LOG.ACTIVE) Then
                        Return Constants.Settings.UsineType.HYBRID
                    ElseIf (.CSV.ACTIVE) Then
                        Return Constants.Settings.UsineType.CSV
                    ElseIf (.LOG.ACTIVE) Then
                        Return Constants.Settings.UsineType.LOG
                    ElseIf (.MDB.ACTIVE) Then
                        Return Constants.Settings.UsineType.MDB
                    Else
                        Return Constants.Settings.UsineType.UNKNOWN
                    End If

                End With

            End Get
            Set(value As Constants.Settings.UsineType)

                With Me.DataFiles

                    .CSV.ACTIVE = False
                    .LOG.ACTIVE = False
                    .MDB.ACTIVE = False

                    Select Case value

                        Case Constants.Settings.UsineType.HYBRID
                            .CSV.ACTIVE = True
                            .LOG.ACTIVE = True

                        Case Constants.Settings.UsineType.CSV
                            .CSV.ACTIVE = True

                        Case Constants.Settings.UsineType.LOG
                            .LOG.ACTIVE = True

                        Case Constants.Settings.UsineType.MDB
                            .MDB.ACTIVE = True

                        Case Else

                    End Select

                End With

            End Set
        End Property

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode

            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(PLANT_ID_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(PLANT_NAME_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(USB_ROOT_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()

            Me.PLANT_ID = "000"
            Me.PLANT_NAME = "Aucun"
            Me.USB_DIRECTORY = "C:\"
        End Sub
    End Class
End Namespace
