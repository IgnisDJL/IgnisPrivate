Namespace UI

    Public Class EventsListView
        Inherits Common.ListControlTemplate(Of XmlSettings.EventsNode.EventInfo)

        ' Events
        Public Event DeleteEventInfo(eventInfo As XmlSettings.EventsNode.EventInfo)
        Public Event UpdateEventInfo(eventInfo As XmlSettings.EventsNode.EventInfo, newName As String, newReplace As String, newIsStart As Boolean, newIsStop As Boolean)

        Public Sub New()
            MyBase.New("Évènements")

        End Sub

        Public Overrides Sub addObject(obj As XmlSettings.EventsNode.EventInfo)

            Dim newItem As New EventsListItem(obj)

            Me.addItem(newItem)

            AddHandler newItem.DeleteEventInfo, AddressOf Me.raiseDeleteEvent
            AddHandler newItem.UpdateEventInfo, AddressOf Me.raiseUpdateEvent


        End Sub

        Private Sub raiseDeleteEvent(eventInfo As XmlSettings.EventsNode.EventInfo)

            RaiseEvent DeleteEventInfo(eventInfo)

        End Sub

        Private Sub raiseUpdateEvent(eventInfo As XmlSettings.EventsNode.EventInfo, newName As String, newReplace As String, newIsStart As Boolean, newIsStop As Boolean)

            RaiseEvent UpdateEventInfo(eventInfo, newName, newReplace, newIsStart, newIsStop)

        End Sub
    End Class
End Namespace
