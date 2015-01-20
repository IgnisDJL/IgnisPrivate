Namespace Commands.Settings

    Public Class AddEventInfo
        Inherits SettingsCommand

        Private name As String
        Private replace As String
        Private isStart As Boolean
        Private isStop As Boolean

        Private newEventInfoNode As XmlSettings.EventsNode.EventInfo

        Public Sub New(name As String, replace As String, isStart As Boolean, isStop As Boolean)

            Me.name = name
            Me.replace = replace
            Me.isStop = isStop
            Me.isStart = isStart

        End Sub

        Public Overrides Sub execute()

            Dim type As Constants.Input.Events.EventType

            If (isStart) Then
                type = Constants.Input.Events.EventType.START
            ElseIf (isStop) Then
                type = Constants.Input.Events.EventType.STOP_
            Else
                type = Constants.Input.Events.EventType.IMPORTANT
            End If

            If (IsNothing(Me.newEventInfoNode)) Then
                Me.newEventInfoNode = XmlSettings.Settings.instance.Usine.Events.addEventInfo(name, replace, type)
            Else
                XmlSettings.Settings.instance.Usine.Events.addEventInfo(Me.newEventInfoNode, type)
            End If

        End Sub

        Public Overrides Sub undo()

            If (isStart) Then
                XmlSettings.Settings.instance.Usine.Events.Start_.removeEventInfo(Me.newEventInfoNode)
            ElseIf (isStop) Then
                XmlSettings.Settings.instance.Usine.Events.Stop_.removeEventInfo(Me.newEventInfoNode)
            Else
                XmlSettings.Settings.instance.Usine.Events.Important.removeEventInfo(Me.newEventInfoNode)
            End If

        End Sub
    End Class
End Namespace

