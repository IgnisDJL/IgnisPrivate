Namespace UI

    Public Class DataFilesAnalysisStepLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(300, 300)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Private Shared ReadOnly LABELS_HEIGHT As Integer = 30
        Private Shared ReadOnly PROGRESS_BARS_HEIGHT As Integer = 20

        ' Components Attibutes
        Private _firstLabel_size As Size
        Private _firstLabel_location As Point
        Private _firstProgressBar_size As Size
        Private _firstProgressBar_location As Point

        Private _secondLabel_size As Size
        Private _secondLabel_location As Point
        Private _secondProgressBar_size As Size
        Private _secondProgressBar_location As Point

        Private _thirdLabel_size As Size
        Private _thirdLabel_location As Point
        Private _thirdProgressBar_size As Size
        Private _thirdProgressBar_location As Point

        ' Attributes
        Private Delegate Sub computeLayoutDelegate
        Private computeLayoutMethod As computeLayoutDelegate

        Public Sub New(nbProgressBars As Integer)
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

            Select Case nbProgressBars

                Case 1
                    Me.computeLayoutMethod = AddressOf computeLayout_1
                Case 2
                    Me.computeLayoutMethod = AddressOf computeLayout_2
                Case 3
                    Me.computeLayoutMethod = AddressOf computeLayout_3

            End Select

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            Me.computeLayoutMethod()

        End Sub

        Private Sub computeLayout_1()

            Me._firstLabel_location = New Point(LOCATION_START_X, (Me.Height - PROGRESS_BARS_HEIGHT - LABELS_HEIGHT) / 2)
            Me._firstLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, LABELS_HEIGHT)

            Me._firstProgressBar_location = New Point(LOCATION_START_X, Me.FirstLabel_Location.Y + LABELS_HEIGHT)
            Me._firstProgressBar_size = New Size(Me.Width - 2 * LOCATION_START_X, PROGRESS_BARS_HEIGHT)

        End Sub

        Private Sub computeLayout_2()

            Dim labelsSize As New Size(Me.Width - 2 * LOCATION_START_X, LABELS_HEIGHT)
            Dim progressBarsSize As New Size(Me.Width - 2 * LOCATION_START_X, PROGRESS_BARS_HEIGHT)

            Me._firstLabel_location = New Point(LOCATION_START_X, (Me.Height - 2 * PROGRESS_BARS_HEIGHT - 2 * LABELS_HEIGHT - 3 * SPACE_BETWEEN_CONTROLS_Y) / 2)
            Me._firstLabel_size = labelsSize

            Me._firstProgressBar_location = New Point(LOCATION_START_X, Me.FirstLabel_Location.Y + LABELS_HEIGHT)
            Me._firstProgressBar_size = progressBarsSize

            Me._secondLabel_location = New Point(LOCATION_START_X, Me.FirstProgressBar_Location.Y + PROGRESS_BARS_HEIGHT + 3 * SPACE_BETWEEN_CONTROLS_Y)
            Me._secondLabel_size = labelsSize

            Me._secondProgressBar_location = New Point(LOCATION_START_X, Me.SecondLabel_Location.Y + LABELS_HEIGHT)
            Me._secondProgressBar_size = progressBarsSize

        End Sub

        Private Sub computeLayout_3()

            Dim labelsSize As New Size(Me.Width - 2 * LOCATION_START_X, LABELS_HEIGHT)
            Dim progressBarsSize As New Size(Me.Width - 2 * LOCATION_START_X, PROGRESS_BARS_HEIGHT)

            Me._firstLabel_location = New Point(LOCATION_START_X, (Me.Height - 3 * PROGRESS_BARS_HEIGHT - 3 * LABELS_HEIGHT - 4 * SPACE_BETWEEN_CONTROLS_Y) / 2)
            Me._firstLabel_size = labelsSize

            Me._firstProgressBar_location = New Point(LOCATION_START_X, Me.FirstLabel_Location.Y + LABELS_HEIGHT)
            Me._firstProgressBar_size = progressBarsSize

            Me._secondLabel_location = New Point(LOCATION_START_X, Me.FirstProgressBar_Location.Y + PROGRESS_BARS_HEIGHT + 2 * SPACE_BETWEEN_CONTROLS_Y)
            Me._secondLabel_size = labelsSize

            Me._secondProgressBar_location = New Point(LOCATION_START_X, Me.SecondLabel_Location.Y + LABELS_HEIGHT)
            Me._secondProgressBar_size = progressBarsSize

            Me._thirdLabel_location = New Point(LOCATION_START_X, Me.SecondProgressBar_Location.Y + PROGRESS_BARS_HEIGHT + 2 * SPACE_BETWEEN_CONTROLS_Y)
            Me._thirdLabel_size = labelsSize

            Me._thirdProgressBar_location = New Point(LOCATION_START_X, Me.ThirdLabel_Location.Y + LABELS_HEIGHT)
            Me._thirdProgressBar_size = progressBarsSize

        End Sub

        '
        ' First
        '
        ' Label
        Public ReadOnly Property FirstLabel_Size As Size
            Get
                Return Me._firstLabel_size
            End Get
        End Property
        Public ReadOnly Property FirstLabel_Location As Point
            Get
                Return Me._firstLabel_location
            End Get
        End Property
        ' Progress Bar
        Public ReadOnly Property FirstProgressBar_Size As Size
            Get
                Return Me._firstProgressBar_size
            End Get
        End Property
        Public ReadOnly Property FirstProgressBar_Location As Point
            Get
                Return Me._firstProgressBar_location
            End Get
        End Property

        '
        ' Second
        '
        ' Label
        Public ReadOnly Property SecondLabel_Size As Size
            Get
                Return Me._secondLabel_size
            End Get
        End Property
        Public ReadOnly Property SecondLabel_Location As Point
            Get
                Return Me._secondLabel_location
            End Get
        End Property
        ' Progress Bar
        Public ReadOnly Property SecondProgressBar_Size As Size
            Get
                Return Me._secondProgressBar_size
            End Get
        End Property
        Public ReadOnly Property SecondProgressBar_Location As Point
            Get
                Return Me._secondProgressBar_location
            End Get
        End Property

        '
        ' Third
        '
        ' Label
        Public ReadOnly Property ThirdLabel_Size As Size
            Get
                Return Me._thirdLabel_size
            End Get
        End Property
        Public ReadOnly Property ThirdLabel_Location As Point
            Get
                Return Me._thirdLabel_location
            End Get
        End Property
        ' Progress Bar
        Public ReadOnly Property ThirdProgressBar_Size As Size
            Get
                Return Me._thirdProgressBar_size
            End Get
        End Property
        Public ReadOnly Property ThirdProgressBar_Location As Point
            Get
                Return Me._thirdProgressBar_location
            End Get
        End Property

    End Class
End Namespace