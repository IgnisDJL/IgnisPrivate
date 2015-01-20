Namespace Commands.Settings

    Public Class UpdateEventInfo
        Inherits SettingsCommand

        Private newName As String
        Private newReplace As String
        Private newType As Constants.Input.Events.EventType

        Private oldName As String
        Private oldReplace As String
        Private oldType As Constants.Input.Events.EventType

        Private eventInfo As XmlSettings.EventsNode.EventInfo

        Public Sub New(eventInfo As XmlSettings.EventsNode.EventInfo, newName As String, newReplace As String, newType As Constants.Input.Events.EventType)

            Me.newName = newName
            Me.newReplace = newReplace
            Me.newType = newType

            Me.eventInfo = eventInfo

            Me.oldName = eventInfo.MESSAGE
            Me.oldReplace = eventInfo.ALT_MESSAGE

            With XmlSettings.Settings.instance.Usine.Events

                If (.Start_.START_EVENTS.Contains(Me.eventInfo)) Then

                    Me.oldType = Constants.Input.Events.EventType.START

                ElseIf (.Stop_.STOP_EVENTS.Contains(Me.eventInfo)) Then

                    Me.oldType = Constants.Input.Events.EventType.STOP_
                Else

                    Me.oldType = Constants.Input.Events.EventType.IMPORTANT
                End If

            End With

        End Sub

        Public Overrides Sub execute()

            Me.eventInfo.MESSAGE = Me.newName
            Me.eventInfo.ALT_MESSAGE = Me.newReplace

            If (Not Me.newType = Me.oldType) Then

                With XmlSettings.Settings.instance.Usine.Events

                    If (Me.oldType = Constants.Input.Events.EventType.START) Then

                        .Start_.removeEventInfo(Me.eventInfo)

                    ElseIf (Me.oldType = Constants.Input.Events.EventType.STOP_) Then

                        .Stop_.removeEventInfo(Me.eventInfo)
                    Else

                        .Important.removeEventInfo(Me.eventInfo)
                    End If

                    .addEventInfo(Me.eventInfo, Me.newType)
                End With

            End If

        End Sub

        Public Overrides Sub undo()

            Me.eventInfo.MESSAGE = Me.oldName
            Me.eventInfo.ALT_MESSAGE = Me.oldReplace

            If (Not Me.newType = Me.oldType) Then

                With XmlSettings.Settings.instance.Usine.Events

                    If (Me.newType = Constants.Input.Events.EventType.START) Then

                        .Start_.removeEventInfo(Me.eventInfo)

                    ElseIf (Me.newType = Constants.Input.Events.EventType.STOP_) Then

                        .Stop_.removeEventInfo(Me.eventInfo)
                    Else

                        .Important.removeEventInfo(Me.eventInfo)
                    End If

                    .addEventInfo(Me.eventInfo, Me.oldType)
                End With

            End If
        End Sub
    End Class
End Namespace

