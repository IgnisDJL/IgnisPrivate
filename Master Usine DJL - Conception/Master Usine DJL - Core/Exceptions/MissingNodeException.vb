
Public Class MissingNodeException
    Inherits Exception

    Public nodeName As String
    Public xPath As String

    Public Sub New(nodeName As String, xPath As String)

        Me.nodeName = nodeName
        Me.xPath = xPath

    End Sub

End Class
