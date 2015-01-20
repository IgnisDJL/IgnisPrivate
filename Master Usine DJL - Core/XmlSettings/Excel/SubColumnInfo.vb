'Public Class SubColumnInfo
'    Implements ExcelColumn

'    Public Shadows Const NODE_NAME As String = "subColumn"
'    Public Const TAG_ATTRIBUTE As String = "tag"
'    Public Const UNIT_ATTRIBUTE As String = "unit"

'    Private subColumnNode As Xml.XmlNode
'    Private superColumnNode As SuperColumnInfo
'    Private sheetNode As XmlSettings.ExcelNode.SheetNode

'    Public Sub New(mynode As Xml.XmlNode, superColumn As SuperColumnInfo, sheetNode As XmlSettings.ExcelNode.SheetNode)

'        Me.subColumnNode = mynode
'        Me.superColumnNode = superColumn
'        Me.sheetNode = sheetNode

'    End Sub

'    Private _tag As Tag
'    Public Property TAG As Tag Implements ExcelColumn.TAG
'        Get

'            If (IsNothing(Me._tag)) Then
'                Me._tag = Me.sheetNode.verifyTag(Me.NODE.Attributes.GetNamedItem(TAG_ATTRIBUTE).Value, True)
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
'            If (Not TypeOf Me.UNIT Is OtherUnit) Then
'                Return Me.NODE.InnerText & " (" & Me.UNIT.ToString & ")"
'            Else
'                Return Me.NODE.InnerText
'            End If
'        End Get
'        Set(value As String)
'            Me.NODE.InnerText = value
'        End Set
'    End Property

'    Public Property UNIT As Unit
'        Get
'            Return If(IsNothing(Me.NODE.Attributes.GetNamedItem(UNIT_ATTRIBUTE)), IGNIS.Unit.NO_UNIT, IGNIS.Unit.parse(Me.NODE.Attributes.GetNamedItem(UNIT_ATTRIBUTE).Value))
'        End Get
'        Set(value As Unit)
'            Me.NODE.Attributes.GetNamedItem(UNIT_ATTRIBUTE).Value = value.SYMBOL
'        End Set
'    End Property

'    ''' <summary>#comment</summary>
'    Public ReadOnly Property SUPER_COLUMN As SuperColumnInfo
'        Get
'            Return Me.superColumnNode
'        End Get
'    End Property

'    Public ReadOnly Property NODE As Xml.XmlNode
'        Get
'            Return Me.subColumnNode
'        End Get
'    End Property

'End Class
