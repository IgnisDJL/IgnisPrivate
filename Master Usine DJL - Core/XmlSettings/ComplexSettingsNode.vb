Namespace XmlSettings

    Public MustInherit Class ComplexSettingsNode
        Inherits SettingsNode

        Private _myNode As Xml.XmlNode

        Protected Sub New(parentNode As Xml.XmlNode, myNode As Xml.XmlNode)

            If (IsNothing(myNode)) Then
                Me._myNode = Me.create(parentNode)
                Me.setDefaultValues()
            Else
                Me._myNode = myNode
            End If

        End Sub

        Public Overrides ReadOnly Property NODE As Xml.XmlNode
            Get
                Return Me._myNode
            End Get
        End Property

        Protected MustOverride Function create(parentNode As Xml.XmlNode) As Xml.XmlNode

        Protected MustOverride Sub setDefaultValues()

    End Class
End Namespace
