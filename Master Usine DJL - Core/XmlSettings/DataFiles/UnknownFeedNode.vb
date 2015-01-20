Public Class UnknownFeedNode
    Inherits XmlSettings.XMLSettingsNode

    Public Shared ReadOnly NODE_NAME As String = "unknownFeed"

    Public Shared ReadOnly TAG_ATTRIBUTE As String = "tag"
    Public Shared ReadOnly MATERIAL_ATTRIBUTE As String = "material"

    Private _tag As Tag

    Public Sub New(myNode As Xml.XmlNode, tag As Tag)
        MyBase.New(myNode)

        Dim tagAttr As Xml.XmlAttribute = myNode.OwnerDocument.CreateAttribute(TAG_ATTRIBUTE)
        myNode.Attributes.Append(tagAttr)
        tagAttr.Value = tag.TAG_NAME

        Me._tag = tag

    End Sub

    Public ReadOnly Property TAG As Tag
        Get
            Return Me._tag
        End Get
    End Property

    Public Property LOCATION As String
        Get
            Return Me.NODE.InnerText
        End Get
        Set(value As String)
            Me.NODE.InnerText = value
        End Set
    End Property

    Public Property MATERIAL As String
        Get
            Return Me.NODE.Attributes.GetNamedItem(MATERIAL_ATTRIBUTE).Value
        End Get
        Set(value As String)
            Me.NODE.Attributes.GetNamedItem(MATERIAL_ATTRIBUTE).Value = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Me.LOCATION
    End Function

End Class
