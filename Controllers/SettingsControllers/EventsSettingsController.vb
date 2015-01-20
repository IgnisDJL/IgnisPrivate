Imports IGNIS.Commands.Settings

Public Class EventsSettingsController
    Inherits SettingsController

    Public Sub New()
        MyBase.New()

    End Sub

    Public Property EventsEnabled As Boolean
        Get
            Return XmlSettings.Settings.instance.Usine.Events.ACTIVE
        End Get
        Set(enabled As Boolean)
            Me.executeCommand(New SetSettingsFileEnabled(enabled))
        End Set
    End Property

    Public Sub addNewEventInfo(name As String, replace As String, isStart As Boolean, isStop As Boolean)
        Me.executeCommand(New AddEventInfo(name, Replace, isStart, isStop))
    End Sub

    Public Sub removeEventInfo(eventInfoToRemove As XmlSettings.EventsNode.EventInfo)
        Me.executeCommand(New RemoveEventInfo(eventInfoToRemove))
    End Sub

    Public Sub updateEventInfo(eventInfoToUpdate As XmlSettings.EventsNode.EventInfo, newName As String, newReplace As String, newType As Constants.Input.Events.EventType)
        Me.executeCommand(New UpdateEventInfo(eventInfoToUpdate, newName, newReplace, newType))
    End Sub

    Public ReadOnly Property Events As List(Of XmlSettings.EventsNode.EventInfo)
        Get
            Dim _events As New List(Of XmlSettings.EventsNode.EventInfo)

            With XmlSettings.Settings.instance.Usine.Events

                _events.AddRange(.Start_.START_EVENTS)
                _events.AddRange(.Stop_.STOP_EVENTS)
                _events.AddRange(.Important.IMPORTANT_EVENTS)

            End With

            Return _events
        End Get
    End Property

    Public Property JustifiableDelaysDuration As TimeSpan
        Get
            Return XmlSettings.Settings.instance.Usine.Events.Delays.JUSTIFIABLE_DURATION
        End Get
        Set(value As TimeSpan)
            Me.executeCommand(New SetJustifiableDelayDuration(value))
        End Set
    End Property

    Public Sub addDelayCode(code As String, description As String, delayType As DelayType)
        Me.executeCommand(New AddDelayCode(code, description, delayType))
    End Sub

    Public Sub removeDelayCode(delayCodeToRemove As DelayCode)
        Me.executeCommand(New RemoveDelayCode(delayCodeToRemove))
    End Sub

    Public Sub updateDelayCode(delayCodeToUpdate As DelayCode, newCode As String, newDescription As String, newDelayType As DelayType)
        Me.executeCommand(New UpdateDelayCode(delayCodeToUpdate, newCode, newDescription, newDelayType))
    End Sub

    Public ReadOnly Property DelayTypes As List(Of DelayType)
        Get

            Dim delayTypeList As New List(Of DelayType)

            For Each delayTypeInfo As XmlSettings.DelaysNode.DelayTypeNode In XmlSettings.Settings.instance.Usine.Events.Delays.DELAY_TYPES

                With delayTypeInfo

                    Dim type As New DelayType(.NAME, .COLOR, .IS_PAUSE, .IS_MAINTENANCE, .IS_BREAKAGE, .IS_INTERN, .IS_EXTERN)

                    For Each code As XmlSettings.DelaysNode.DelayCodeNode In .DELAY_CODES

                        type.addCode(New DelayCode(code.CODE, code.DESCRIPTION))

                    Next

                    delayTypeList.Add(type)

                End With
            Next

            Return delayTypeList
        End Get
    End Property
End Class
