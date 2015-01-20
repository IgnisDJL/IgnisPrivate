Namespace UI.Common

    Public MustInherit Class ListItem(Of MyType)
        Inherits Panel

        ' Constants

        ' Components

        ' Attributes
        Private _itemObject As MyType

        Protected _currentMode As Mode

        ' Events
        Public Event ClickEvent(_me As ListItem(Of MyType))
        Public Event DeleteEvent(_me As List(Of MyType))

        Protected Sub New(itemObject As MyType)

            Me._currentMode = ListItem(Of MyType).Mode.READ

            Me._itemObject = itemObject

        End Sub

        Protected MustOverride Sub initializeComponents()

        Public MustOverride Sub ajustLayout(newSize As Size)

        Public MustOverride Sub onSelect()

        Public MustOverride Sub onUnselect()

        Public ReadOnly Property ItemObject As MyType
            Get
                Return Me._itemObject
            End Get
        End Property

        Protected Sub raiseClickEvent()
            RaiseEvent ClickEvent(Me)
        End Sub

        Public ReadOnly Property CurrentMode As Mode
            Get
                Return _currentMode
            End Get
        End Property

        Public Enum Mode
            READ = 0
            WRITE = 1
        End Enum

    End Class
End Namespace
