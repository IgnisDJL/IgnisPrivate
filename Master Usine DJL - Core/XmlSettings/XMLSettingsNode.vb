Namespace XmlSettings

    Public Class XMLSettingsNode

        Private myNode As Xml.XmlNode

        Public Sub New(myNode As Xml.XmlNode)
            Me.myNode = myNode
        End Sub

        Public ReadOnly Property NODE As Xml.XmlNode
            Get
                Return Me.myNode
            End Get
        End Property

    End Class

End Namespace
