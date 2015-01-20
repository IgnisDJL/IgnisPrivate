Namespace UI

    Public MustInherit Class View
        Inherits Panel

        Protected Shadows layout As LayoutManager

        Public Sub ajustLayout(container As Control)

            Me.Size = container.ClientSize

            Me.layout.computeLayout(container.ClientSize)

            ajustLayout(container.ClientSize)

        End Sub

        Public Sub ajustLayoutFinal(container As Control)
            ajustLayoutFinal(container.ClientSize)
        End Sub

        Public Sub beforeShow(container As MainFrame)
            container.MinimumSize = layout.MinimumSize
            beforeShow()
            ajustLayout(container)
            ajustLayoutFinal(container)
        End Sub

        Protected MustOverride Sub initializeComponents()

        ''' <summary>
        ''' Ajusts the location, size and other size related attributes of the view's components
        ''' </summary>
        ''' <param name="newSize">The ClientSize of the parent container</param>
        ''' <remarks>Size has been set and layout computed in the View.ajustLayout(container) method</remarks>
        Protected MustOverride Sub ajustLayout(newSize As Size)

        ' Called after the resize event of the main window
        Protected MustOverride Sub ajustLayoutFinal(newSize As Size)

        Protected MustOverride Sub beforeShow()

        Public MustOverride Sub afterShow()

        Public MustOverride Sub onHide() Handles Me.Disposed

        Public MustOverride Shadows ReadOnly Property Name As String

        Public ReadOnly Property LayoutManager As LayoutManager
            Get
                Return Me.layout
            End Get
        End Property

    End Class

End Namespace
