Namespace UI

    Public Class UnknownFeedListView
        Inherits Common.ListControlTemplate(Of UnknownFeedNode)

        ' Constants

        ' Components

        ' Attributes

        Public Sub New()
            MyBase.New("Bennes inconnues")

            Me.SortMethod = Function(x As UnknownFeedNode, y As UnknownFeedNode)
                                Return x.LOCATION.CompareTo(y.LOCATION)
                            End Function
        End Sub

        Public Overrides Sub addObject(obj As UnknownFeedNode)

            Dim newItem As New UnknownFeedListItem(obj)

            Me.addItem(newItem)

        End Sub
    End Class
End Namespace
