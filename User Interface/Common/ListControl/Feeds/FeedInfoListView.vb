Namespace UI

    Public Class FeedInfoListView
        Inherits Common.ListControlTemplate(Of FeedInfoNode)

        ' Constants

        ' Components

        ' Attributes
        Private _layoutType As FeedsLayout.LayoutType

        ' Events
        Public Event DeleteFeedInfo(feedInfo As FeedInfoNode)
        Public Event UpdateFeedInfo(feedInfo As FeedInfoNode, newIndex As Integer, newLocation As String, newMaterial As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

        Public Sub New(title As String, layoutType As FeedsLayout.LayoutType)
            MyBase.New(title)

            Me._layoutType = layoutType

            Me.SortMethod = Function(x As FeedInfoNode, y As FeedInfoNode)
                                Return x.INDEX.CompareTo(y.INDEX)
                            End Function
        End Sub

        Public Overrides Sub addObject(obj As FeedInfoNode)

            Dim newItem = New FeedInfoListItem(obj, Me._layoutType)

            Me.addItem(newItem)

            AddHandler newItem.DeleteFeedInfo, AddressOf Me.raiseDeleteEvent
            AddHandler newItem.UpdateFeedInfo, AddressOf Me.raiseUpdateEvent

        End Sub

        Public WriteOnly Property LayoutType As FeedsLayout.LayoutType
            Set(value As FeedsLayout.LayoutType)
                Me._layoutType = value
            End Set
        End Property

        Private Sub raiseDeleteEvent(feedInfo As FeedInfoNode)

            RaiseEvent DeleteFeedInfo(feedInfo)

        End Sub

        Private Sub raiseUpdateEvent(feedInfo As FeedInfoNode, newIndex As Integer, newLocation As String, newMaterial As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

            RaiseEvent UpdateFeedInfo(feedInfo, newIndex, newLocation, newMaterial, isRecycled, isFiller, isAsphalt)

        End Sub
    End Class
End Namespace
