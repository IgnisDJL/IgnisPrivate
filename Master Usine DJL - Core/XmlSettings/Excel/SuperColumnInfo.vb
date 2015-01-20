'Public Class SuperColumnInfo
'    Implements ExcelColumn

'    Public Const NODE_NAME As String = "superColumn"
'    Public Const TAG_ATTRIBUTE As String = "tag"
'    Public Const COLUMN_NAME_ATTRIBUTE As String = "columnName"
'    Public Const INDEX_ATTRIBUTE As String = "index"

'    Private superColumnNode As Xml.XmlNode
'    Private sheetNode As XmlSettings.ExcelNode.SheetNode

'    Public Sub New(myNode As Xml.XmlNode, sheetNode As XmlSettings.ExcelNode.SheetNode)

'        Me.superColumnNode = myNode
'        Me.sheetNode = sheetNode

'    End Sub

'    Private _tag As Tag
'    Public Property TAG As Tag Implements ExcelColumn.TAG
'        Get

'            If (IsNothing(Me._tag)) Then

'                Me._tag = Me.sheetNode.verifyTag(Me.NODE.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value)

'            End If

'            Return Me._tag
'        End Get
'        Set(value As Tag)
'            Me._tag = value
'            Me.NODE.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value = value.TAG_NAME
'        End Set
'    End Property

'    Public Property COLUMN_NAME As String Implements ExcelColumn.COLUMN_NAME
'        Get
'            Return Me.NODE.Attributes.GetNamedItem(COLUMN_NAME_ATTRIBUTE).Value
'        End Get
'        Set(value As String)
'            Me.NODE.Attributes.GetNamedItem(COLUMN_NAME_ATTRIBUTE).Value = value
'        End Set
'    End Property

'    Public Property INDEX As Integer
'        Get
'            Return CInt(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value)
'        End Get
'        Set(value As Integer)
'            Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value = value
'        End Set
'    End Property

'    ''' <summary>#comment</summary>
'    Public Property SUBCOLUMNS As New List(Of SubColumnInfo)

'    Public Sub addSubColumn(reference As DataInfo, columnName As String, unit As Unit)

'        Dim subColumnNode As Xml.XmlNode = Me.NODE.OwnerDocument.CreateElement(SubColumnInfo.NODE_NAME)
'        Dim subColumnInformation As New SubColumnInfo(subColumnNode, Me, Me.sheetNode)


'        Dim tagAttr = Me.NODE.OwnerDocument.CreateAttribute(SubColumnInfo.TAG_ATTRIBUTE)
'        subColumnNode.Attributes.Append(tagAttr)

'        Dim unitAttr = Me.NODE.OwnerDocument.CreateAttribute(SubColumnInfo.UNIT_ATTRIBUTE)
'        subColumnNode.Attributes.Append(unitAttr)

'        subColumnInformation.TAG = reference.TAG
'        subColumnInformation.COLUMN_NAME = columnName
'        subColumnInformation.UNIT = unit

'        Me.NODE.AppendChild(subColumnNode)
'        Me.SUBCOLUMNS.Add(subColumnInformation)

'        Me.sheetNode.NB_COLUMNS += 1

'    End Sub

'    Public ReadOnly Property NODE As Xml.XmlNode
'        Get
'            Return Me.superColumnNode
'        End Get
'    End Property


'End Class
