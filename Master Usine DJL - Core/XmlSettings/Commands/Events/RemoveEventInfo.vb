Namespace Commands.Settings

    Public Class RemoveEventInfo
        Inherits SettingsCommand

        Private type As Constants.Input.Events.EventType
        Private eventInfo As XmlSettings.EventsNode.EventInfo

        Public Sub New(eventInfo As XmlSettings.EventsNode.EventInfo)

            Me.eventInfo = eventInfo
        End Sub

        Public Overrides Sub execute()

            With XmlSettings.Settings.instance.Usine.Events


                If (.Start_.START_EVENTS.Contains(Me.eventInfo)) Then

                    Me.type = Constants.Input.Events.EventType.START
                    .Start_.removeEventInfo(Me.eventInfo)

                ElseIf (.Stop_.STOP_EVENTS.Contains(Me.eventInfo)) Then

                    Me.type = Constants.Input.Events.EventType.STOP_
                    .Stop_.removeEventInfo(Me.eventInfo)
                Else

                    Me.type = Constants.Input.Events.EventType.IMPORTANT
                    .Important.removeEventInfo(Me.eventInfo)
                End If

            End With

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.Events.addEventInfo(Me.eventInfo, Me.type)

        End Sub
    End Class
End Namespace

