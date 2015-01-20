Namespace UI

    Public Class OperatorListView
        Inherits Common.ListControlTemplate(Of FactoryOperator)

        ' Events
        Public Event deleteOperator(_operator As FactoryOperator)
        Public Event updateOperator(_operator As FactoryOperator, newFirstName As String, newLastName As String)

        Public Sub New()
            MyBase.New("Opérateurs")

        End Sub

        Public Overrides Sub addObject(obj As FactoryOperator)

            Dim newItem = New OperatorListItem(obj)

            ' #todo - when clear() is called, these should be unbound
            ' or #refactor, find a better way to pass the events
            AddHandler newItem.deleteOperator, AddressOf Me.raiseDeleteEvent
            AddHandler newItem.updateOperator, AddressOf Me.raiseUpdateEvent

            Me.addItem(newItem)

        End Sub

        Private Sub raiseDeleteEvent(operatorToDelete As FactoryOperator)

            RaiseEvent deleteOperator(operatorToDelete)

        End Sub

        Private Sub raiseUpdateEvent(operatorToUpdate As FactoryOperator, newFirstName As String, newLastName As String)

            RaiseEvent updateOperator(operatorToUpdate, newFirstName, newLastName)

        End Sub

    End Class
End Namespace
