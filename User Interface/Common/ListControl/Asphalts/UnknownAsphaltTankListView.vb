Namespace UI

    Public Class UnknownAsphaltTankListView
        Inherits Common.ListControlTemplate(Of XmlSettings.AsphaltNode.UnknownTankNode)

        Public Sub New()
            MyBase.New("Bennes de bitume inconnues")

            Me.SortMethod = Function(x As XmlSettings.AsphaltNode.UnknownTankNode, y As XmlSettings.AsphaltNode.UnknownTankNode)
                                Return x.TANK_NAME.CompareTo(y.TANK_NAME)
                            End Function
        End Sub

        Public Overrides Sub addObject(obj As XmlSettings.AsphaltNode.UnknownTankNode)

            Dim newItem As New UnknownAsphaltTankListItem(obj)

            Me.addItem(newItem)

        End Sub
    End Class
End Namespace
