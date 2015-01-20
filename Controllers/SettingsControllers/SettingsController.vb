Imports IGNIS.Commands.Settings

Public MustInherit Class SettingsController
    Implements Commands.CommandManager(Of SettingsCommand)

    Private _executedCommands As List(Of SettingsCommand)
    Private _redoableCommands As List(Of SettingsCommand)

    Protected Sub New()

        _executedCommands = New List(Of SettingsCommand)
        _redoableCommands = New List(Of SettingsCommand)

    End Sub

    Protected Sub executeCommand(command As SettingsCommand) Implements Commands.CommandManager(Of SettingsCommand).execute

        command.execute()
        Me._executedCommands.Add(command)
        Me._redoableCommands.Clear()

        XmlSettings.Settings.instance.save()

    End Sub

    Public Sub undo() Implements Commands.CommandManager(Of SettingsCommand).undo

        If (Me.CanUndo) Then

            Me._executedCommands.Last.undo()

            Me._redoableCommands.Add(_executedCommands.Last)

            Me._executedCommands.RemoveAt(Me._executedCommands.Count - 1)

            XmlSettings.Settings.instance.save()

        End If

    End Sub

    Public Sub redo() Implements Commands.CommandManager(Of SettingsCommand).redo

        If (Me.CanRedo) Then

            Me._redoableCommands.Last.execute()

            Me._executedCommands.Add(Me._redoableCommands.Last)

            Me._redoableCommands.RemoveAt(Me._redoableCommands.Count - 1)

            XmlSettings.Settings.instance.save()

        End If

    End Sub

    Public ReadOnly Property CanUndo As Boolean
        Get
            Return Me._executedCommands.Count > 0
        End Get
    End Property

    Public ReadOnly Property CanRedo As Boolean
        Get
            Return _redoableCommands.Count > 0
        End Get
    End Property
End Class
