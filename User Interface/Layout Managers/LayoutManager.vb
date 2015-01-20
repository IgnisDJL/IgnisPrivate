Namespace UI

    Public MustInherit Class LayoutManager

        ' Constants
        Public Shared ReadOnly LOCATION_START_X As Integer = 25
        Public Shared ReadOnly LOCATION_START_Y As Integer = 25

        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X = 15
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_Y = 15

        Public Shared ReadOnly CONTROL_BUTTONS_WIDTH As Integer = 120
        Public Shared ReadOnly CONTROL_BUTTONS_HEIGHT As Integer = 40

        Public Shared ReadOnly BUTTONS_PANEL_HEIGHT As Integer = 60
        Public Shared ReadOnly BUTTONS_PANEL_LOCATION_START_Y As Integer = 5

        Public Shared ReadOnly FIELDS_HEIGHT As Integer = 30

        ' Attributes
        Private containerSize As Size

        Private _minimumSize As Size
        Private _condensedSize As Size

        Private currentHeightState As SizeState
        Private currentWidthState As SizeState

        Public Enum SizeState As Integer
            MINIMUM = 0
            CONDENSED = 1
            FULL = 2
        End Enum

        Public Sub New(minimumSize As Size, condensedSize As Size)

            Me._minimumSize = minimumSize
            Me._condensedSize = condensedSize

        End Sub

        Public Sub computeLayout(containerSize As Size)

            Me.containerSize = containerSize

            ' Height state
            If (containerSize.Height < _minimumSize.Height) Then
                Me.currentHeightState = SizeState.MINIMUM
            ElseIf (containerSize.Height < _condensedSize.Height) Then
                Me.currentHeightState = SizeState.CONDENSED
            Else
                Me.currentHeightState = SizeState.FULL
            End If

            ' Width state
            If (containerSize.Width < _minimumSize.Width) Then
                Me.currentWidthState = SizeState.MINIMUM
            ElseIf (containerSize.Width < _condensedSize.Width) Then
                Me.currentWidthState = SizeState.CONDENSED
            Else
                Me.currentWidthState = SizeState.FULL
            End If

            Me.computeLayout()
        End Sub

        ''' <summary>
        ''' Computes the size and location of view components based on the size state and size.
        ''' </summary>
        ''' <remarks>The size state and the size have already been computed.</remarks>
        Protected MustOverride Sub computeLayout()

        Protected ReadOnly Property Width As Integer
            Get
                Return Me.containerSize.Width
            End Get
        End Property

        Protected ReadOnly Property Height As Integer
            Get
                Return Me.containerSize.Height
            End Get
        End Property

        Public ReadOnly Property HeightState As SizeState
            Get
                Return Me.currentHeightState
            End Get
        End Property

        Public ReadOnly Property WidthState As SizeState
            Get
                Return Me.currentWidthState
            End Get
        End Property

        ''' <summary>
        ''' The minimum size of the container of the view.
        ''' </summary>
        ''' <remarks>Will be used to set the view's container.Size (not ClientSize) attribute.</remarks>
        Public ReadOnly Property MinimumSize As Size
            Get
                Return _minimumSize
            End Get
        End Property

        Public ReadOnly Property CondensedSize As Size
            Get
                Return _condensedSize
            End Get
        End Property

    End Class

End Namespace
