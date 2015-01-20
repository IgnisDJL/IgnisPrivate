Namespace Commands

    Public Interface CommandManager(Of commandType)

        Sub execute(command As commandType)

        Sub undo()

        Sub redo()

    End Interface
End Namespace
