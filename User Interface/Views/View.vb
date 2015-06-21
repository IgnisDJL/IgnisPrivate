Namespace UI

    Public Class View
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

        Protected Overridable Sub initializeComponents()

        End Sub

        ''' <summary>
        ''' Ajusts the location, size and other size related attributes of the view's components
        ''' </summary>
        ''' <param name="newSize">The ClientSize of the parent container</param>
        ''' <remarks>Size has been set and layout computed in the View.ajustLayout(container) method</remarks>
        Protected Overridable Sub ajustLayout(newSize As Size)

            ' Called after the resize event of the main window
        End Sub
        Protected Overridable Sub ajustLayoutFinal(newSize As Size)

        End Sub

        Protected Overridable Sub beforeShow()

        End Sub

        Public Overridable Sub afterShow()

        End Sub

        Public Overridable Sub onHide() Handles Me.Disposed

        End Sub

        Public Overridable Shadows ReadOnly Property Name As String
            Get
                Return ""
            End Get
        End Property

        Public ReadOnly Property LayoutManager As LayoutManager
            Get
                Return Me.layout
            End Get
        End Property

    End Class

End Namespace
