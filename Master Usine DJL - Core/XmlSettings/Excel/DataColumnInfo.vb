'Public Class DataColumnInfo
'    Implements ExcelColumn

'    Public Const NODE_NAME As String = "column"
'    Public Const TAG_ATTRIBUTE As String = "tag"
'    Public Const UNIT_ATTRIBUTE As String = "unit"
'    Public Const INDEX_ATTRIBUTE As String = "index"


'    ''' <summary>The actual xml node from the xmlDocument</summary>
'    Private columnNode As Xml.XmlNode

'    Private sheetNode As XmlSettings.ExcelNode.SheetNode

'    Public Sub New(myNode As Xml.XmlNode, sheetNode As XmlSettings.ExcelNode.SheetNode)
'        Me.columnNode = myNode
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

'    Public Property INDEX As Integer
'        Get
'            Return If(IsNothing(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE)), -1, CInt(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value))
'        End Get
'        Set(value As Integer)
'            If (Not IsNothing(Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE))) Then
'                Me.NODE.Attributes.GetNamedItem(INDEX_ATTRIBUTE).Value = value
'            End If
'        End Set
'    End Property

'    Public ReadOnly Property NODE As Xml.XmlNode
'        Get
'            Return Me.columnNode
'        End Get
'    End Property

'End Class
