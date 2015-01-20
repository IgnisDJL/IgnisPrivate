Public Class FeedInfoNode
    Inherits XmlSettings.XMLSettingsNode
    Implements FeedInfo

    Public Shared ReadOnly NODE_NAME As String = "feed"

    Public Shared ReadOnly TAG_ATTRIBUTE As String = "tag"
    Public Shared ReadOnly MATERIAL_ATTRIBUTE As String = "material"
    Public Shared ReadOnly INDEX_ATTRIBUTE As String = "index"
    Public Shared ReadOnly IS_RECYCLED_ATTRIBUTE As String = "isRecycled"
    Public Shared ReadOnly IS_FILLER_ATTRIBUTE As String = "isFiller"
    Public Shared ReadOnly IS_ASPHALT_ATTRIBUTE As String = "isAsphalt"

    Private Shared ReadOnly YES As String = "yes"
    Private Shared ReadOnly NO As String = "no"


    Private _tag As Tag

    Public Sub New(myNode As Xml.XmlNode, tag As Tag, mySubColumns As List(Of DataInfo))
        MyBase.New(myNode)

        Dim tagAttr As Xml.XmlAttribute = myNode.OwnerDocument.CreateAttribute(TAG_ATTRIBUTE)
        myNode.Attributes.Append(tagAttr)
        tagAttr.Value = tag.TAG_NAME

        Me._tag = tag
        Me.SUB_COLUMNS = mySubColumns

    End Sub

    Public Property INDEX As Integer Implements FeedInfo.INDEX
        Get
            Return CInt(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value)
        End Get
        Set(value As Integer)
            Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value = value
        End Set
    End Property

    Public ReadOnly Property TAG As Tag Implements FeedInfo.TAG
        Get
            Return Me._tag
        End Get
    End Property

    Public Property SUB_COLUMNS As List(Of DataInfo) Implements FeedInfo.SUB_COLUMNS

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

    Public Property IS_RECYCLED As Boolean
        Get
            Return If(IsNothing(Me.NODE.Attributes.GetNamedItem(IS_RECYCLED_ATTRIBUTE)), False, Me.NODE.Attributes.GetNamedItem(IS_RECYCLED_ATTRIBUTE).Value.Equals(YES))
        End Get
        Set(value As Boolean)
            Me.NODE.Attributes.GetNamedItem(IS_RECYCLED_ATTRIBUTE).Value = If(value, YES, NO)
        End Set
    End Property

    Public Property IS_FILLER As Boolean
        Get
            Return If(IsNothing(Me.NODE.Attributes.GetNamedItem(IS_FILLER_ATTRIBUTE)), False, Me.NODE.Attributes.GetNamedItem(IS_FILLER_ATTRIBUTE).Value.Equals(YES))
        End Get
        Set(value As Boolean)
            If (Not IsNothing(Me.NODE.Attributes.GetNamedItem(IS_FILLER_ATTRIBUTE))) Then
                Me.NODE.Attributes.GetNamedItem(IS_FILLER_ATTRIBUTE).Value = If(value, YES, NO)
            End If
        End Set
    End Property

    Public Property IS_ASPHALT As Boolean
        Get
            Return If(IsNothing(Me.NODE.Attributes.GetNamedItem(IS_ASPHALT_ATTRIBUTE)), False, Me.NODE.Attributes.GetNamedItem(IS_ASPHALT_ATTRIBUTE).Value.Equals(YES))
        End Get
        Set(value As Boolean)
            If (Not IsNothing(Me.NODE.Attributes.GetNamedItem(IS_ASPHALT_ATTRIBUTE))) Then
                Me.NODE.Attributes.GetNamedItem(IS_ASPHALT_ATTRIBUTE).Value = If(value, YES, NO)
            End If
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Me.TAG.ToString & " " & Me.INDEX & " (" & Me.MATERIAL & ")"
    End Function

End Class
