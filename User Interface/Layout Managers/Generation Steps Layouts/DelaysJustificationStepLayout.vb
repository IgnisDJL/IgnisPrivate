Namespace UI

    Public Class DelaysJustificationStepLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(300, 300)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly COMPONENTS_HEIGHTS As Integer = 30

        Public Shared ReadOnly TOP_LABELS_SIZE As Size = New Size(120, COMPONENTS_HEIGHTS)

        Public Shared ReadOnly UNDO_LABEL_SIZE As Size = New Size(190, 25)

        ' Components
        Private _dateLabel_size As Size
        Private _dateLabel_location As Point

        Private _startTextLabel_size As Size
        Private _startTextLabel_location As Point
        Private _startTimeLabel_size As Size
        Private _startTimeLabel_location As Point

        Private _endTextLabel_size As Size
        Private _endTextLabel_location As Point
        Private _endTimeLabel_size As Size
        Private _endTimeLabel_location As Point

        Private _delayNumberTextLabel_size As Size
        Private _delayNumberTextLabel_location As Point
        Private _delayNumberValueLabel_size As Size
        Private _delayNumberValueLabel_location As Point

        Private _durationTextLabel_size As Size
        Private _durationTextLabel_location As Point
        Private _durationValueLabel_size As Size
        Private _durationValueLabel_location As Point

        Private _delayTypeLabel_size As Size
        Private _delayTypeLabel_location As Point
        Private _delayTypeCombobox_size As Size
        Private _delayTypeCombobox_location As Point

        Private _delayCodeLabel_size As Size
        Private _delayCodeLabel_location As Point
        Private _delayCodeCombobox_size As Size
        Private _delayCodeCombobox_location As Point

        Private _delayJustificationLabel_size As Size
        Private _delayJustificationLabel_location As Point
        Private _delayJustificationTextbox_size As Size
        Private _delayJustificationTextbox_location As Point

        Private _undoLabel_size As Size
        Private _undoLabel_location As Point

        Private _nextButton_size As Size
        Private _nextButton_location As Point

        Private _skipButton_size As Size
        Private _skipButton_location As Point

        Private _splitButton_size As Size
        Private _splitButton_location As Point

        ' Attributes

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)


        End Sub

        Protected Overloads Overrides Sub computeLayout()

            Me._dateLabel_location = New Point(LOCATION_START_X, 0)
            Me._dateLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, COMPONENTS_HEIGHTS)

            Dim topLabels_X As Integer = Me.Width / 2 - (TOP_LABELS_SIZE.Width * 3 + SPACE_BETWEEN_CONTROLS_X) / 2

            Me._startTextLabel_location = New Point(topLabels_X, Me.DateLabel_Location.Y + Me.DateLabel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._startTextLabel_size = TOP_LABELS_SIZE
            Me._startTimeLabel_location = New Point(StartTextLabel_Location.X + StartTextLabel_Size.Width, StartTextLabel_Location.Y)
            Me._startTimeLabel_size = TOP_LABELS_SIZE

            Me._delayNumberTextLabel_location = New Point(topLabels_X + 2 * TOP_LABELS_SIZE.Width + SPACE_BETWEEN_CONTROLS_X, Me.StartTextLabel_Location.Y + 1 * COMPONENTS_HEIGHTS / 2)
            Me._delayNumberTextLabel_size = TOP_LABELS_SIZE
            Me._delayNumberValueLabel_location = New Point(DelayNumberTextLabel_Location.X, DelayNumberTextLabel_Location.Y + COMPONENTS_HEIGHTS)
            Me._delayNumberValueLabel_size = TOP_LABELS_SIZE

            Me._endTextLabel_location = New Point(topLabels_X, StartTextLabel_Location.Y + StartTextLabel_Size.Height)
            Me._endTextLabel_size = TOP_LABELS_SIZE
            Me._endTimeLabel_location = New Point(EndTextLabel_Location.X + EndTextLabel_Size.Width, EndTextLabel_Location.Y)
            Me._endTimeLabel_size = TOP_LABELS_SIZE

            Me._durationTextLabel_location = New Point(topLabels_X, EndTextLabel_Location.Y + COMPONENTS_HEIGHTS)
            Me._durationTextLabel_size = TOP_LABELS_SIZE
            Me._durationValueLabel_location = New Point(DurationTextLabel_Location.X + DurationTextLabel_Size.Width, DurationTextLabel_Location.Y)
            Me._durationValueLabel_size = TOP_LABELS_SIZE

            Me._delayTypeLabel_location = New Point(LOCATION_START_X, DurationTextLabel_Location.Y + DurationTextLabel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._delayTypeLabel_size = New Size((Me.Width - 2 * LOCATION_START_X - SPACE_BETWEEN_CONTROLS_X) / 3, COMPONENTS_HEIGHTS)
            Me._delayTypeCombobox_location = New Point(LOCATION_START_X, DelayTypeLabel_Location.Y + DelayTypeLabel_Size.Height)
            Me._delayTypeCombobox_size = New Size(DelayTypeLabel_Size.Width, COMPONENTS_HEIGHTS)

            Me._delayCodeLabel_location = New Point(DelayTypeLabel_Location.X + DelayTypeLabel_Size.Width + SPACE_BETWEEN_CONTROLS_X, DelayTypeLabel_Location.Y)
            Me._delayCodeLabel_size = New Size(DelayTypeLabel_Size.Width * 2, COMPONENTS_HEIGHTS)
            Me._delayCodeCombobox_location = New Point(DelayCodeLabel_Location.X, DelayCodeLabel_Location.Y + DelayCodeLabel_Size.Height)
            Me._delayCodeCombobox_size = New Size(DelayCodeLabel_Size.Width, COMPONENTS_HEIGHTS)

            Me._delayJustificationLabel_location = New Point(LOCATION_START_X, DelayTypeCombobox_Location.Y + COMPONENTS_HEIGHTS + SPACE_BETWEEN_CONTROLS_Y)
            Me._delayJustificationLabel_size = New Size(Me.Width / 2, COMPONENTS_HEIGHTS)
            Me._delayJustificationTextbox_location = New Point(LOCATION_START_X, DelayJustificationLabel_Location.Y + DelayJustificationLabel_Size.Height)
            Me._delayJustificationTextbox_size = New Size(Me.Width - 2 * LOCATION_START_X, COMPONENTS_HEIGHTS)

            Me._undoLabel_location = New Point(Me.Width - UNDO_LABEL_SIZE.Width - 5, Me.Height - UNDO_LABEL_SIZE.Height - 5)

            ' Next Button (In buttons panel)
            Me._nextButton_location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me._nextButton_size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

            ' Skip Button (In buttons panel)
            Me._skipButton_location = New Point(NextButton_Location.X - ReportGenerationFrameLayout.SPACE_BETWEEN_CONTROLS_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me._skipButton_size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

            ' Split Button (In buttons Panel)
            Me._splitButton_location = New Point(SkipButton_Location.X - ReportGenerationFrameLayout.SPACE_BETWEEN_CONTROLS_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me._splitButton_size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

        End Sub

        '
        ' Date Label
        '
        Public ReadOnly Property DateLabel_Size As Size
            Get
                Return _dateLabel_size
            End Get
        End Property
        Public ReadOnly Property DateLabel_Location As Point
            Get
                Return _dateLabel_location
            End Get
        End Property
        '
        ' Start Time Label
        '
        Public ReadOnly Property StartTextLabel_Size As Size
            Get
                Return _startTextLabel_size
            End Get
        End Property
        Public ReadOnly Property StartTextLabel_Location As Point
            Get
                Return _startTextLabel_location
            End Get
        End Property
        Public ReadOnly Property StartTimeLabel_Size As Size
            Get
                Return _startTimeLabel_size
            End Get
        End Property
        Public ReadOnly Property StartTimeLabel_Location As Point
            Get
                Return _startTimeLabel_location
            End Get
        End Property
        '
        ' End Time Label
        '
        Public ReadOnly Property EndTextLabel_Size As Size
            Get
                Return _endTextLabel_size
            End Get
        End Property
        Public ReadOnly Property EndTextLabel_Location As Point
            Get
                Return _endTextLabel_location
            End Get
        End Property
        Public ReadOnly Property EndTimeLabel_Size As Size
            Get
                Return _endTimeLabel_size
            End Get
        End Property
        Public ReadOnly Property EndTimeLabel_Location As Point
            Get
                Return _endTimeLabel_location
            End Get
        End Property
        '
        ' Delay Number Label
        '
        Public ReadOnly Property DelayNumberTextLabel_Size As Size
            Get
                Return _delayNumberTextLabel_size
            End Get
        End Property
        Public ReadOnly Property DelayNumberTextLabel_Location As Point
            Get
                Return _delayNumberTextLabel_location
            End Get
        End Property
        Public ReadOnly Property DelayNumberValueLabel_Size As Size
            Get
                Return _delayNumberValueLabel_size
            End Get
        End Property
        Public ReadOnly Property DelayNumberValueLabel_Location As Point
            Get
                Return _delayNumberValueLabel_location
            End Get
        End Property
        '
        ' Duration Label
        '
        Public ReadOnly Property DurationTextLabel_Size As Size
            Get
                Return _durationTextLabel_size
            End Get
        End Property
        Public ReadOnly Property DurationTextLabel_Location As Point
            Get
                Return _durationTextLabel_location
            End Get
        End Property
        Public ReadOnly Property DurationValueLabel_Size As Size
            Get
                Return _durationValueLabel_size
            End Get
        End Property
        Public ReadOnly Property DurationValueLabel_Location As Point
            Get
                Return _durationValueLabel_location
            End Get
        End Property
        '
        ' Delay Type
        '
        Public ReadOnly Property DelayTypeLabel_Size As Size
            Get
                Return _delayTypeLabel_size
            End Get
        End Property
        Public ReadOnly Property DelayTypeLabel_Location As Point
            Get
                Return _delayTypeLabel_location
            End Get
        End Property
        Public ReadOnly Property DelayTypeCombobox_Size As Size
            Get
                Return _delayTypeCombobox_size
            End Get
        End Property
        Public ReadOnly Property DelayTypeCombobox_Location As Point
            Get
                Return _delayTypeCombobox_location
            End Get
        End Property
        '
        ' Delay Code
        '
        Public ReadOnly Property DelayCodeLabel_Size As Size
            Get
                Return _delayCodeLabel_size
            End Get
        End Property
        Public ReadOnly Property DelayCodeLabel_Location As Point
            Get
                Return _delayCodeLabel_location
            End Get
        End Property
        Public ReadOnly Property DelayCodeCombobox_Size As Size
            Get
                Return _delayCodeCombobox_size
            End Get
        End Property
        Public ReadOnly Property DelayCodeCombobox_Location As Point
            Get
                Return _delayCodeCombobox_location
            End Get
        End Property
        '
        ' Delay Justification
        '
        Public ReadOnly Property DelayJustificationLabel_Size As Size
            Get
                Return _delayJustificationLabel_size
            End Get
        End Property
        Public ReadOnly Property DelayJustificationLabel_Location As Point
            Get
                Return _delayJustificationLabel_location
            End Get
        End Property
        Public ReadOnly Property DelayJustificationTextbox_Size As Size
            Get
                Return _delayJustificationTextbox_size
            End Get
        End Property
        Public ReadOnly Property DelayJustificationTextbox_Location As Point
            Get
                Return _delayJustificationTextbox_location
            End Get
        End Property
        '
        ' Undo label
        '
        Public ReadOnly Property UndoLabel_Location As Point
            Get
                Return _undoLabel_location
            End Get
        End Property
        '
        ' Next Button (Inside Buttons Panel)
        '
        Public ReadOnly Property NextButton_Size As Size
            Get
                Return _nextButton_size
            End Get
        End Property
        Public ReadOnly Property NextButton_Location As Point
            Get
                Return _nextButton_location
            End Get
        End Property
        '
        ' Skip Button (Inside Buttons Panel)
        '
        Public ReadOnly Property SkipButton_Size As Size
            Get
                Return _skipButton_size
            End Get
        End Property
        Public ReadOnly Property SkipButton_Location As Point
            Get
                Return _skipButton_location
            End Get
        End Property
        '
        ' Split Button (Inside Buttons Panel)
        '
        Public ReadOnly Property SplitButton_Size As Size
            Get
                Return _splitButton_size
            End Get
        End Property
        Public ReadOnly Property SplitButton_Location As Point
            Get
                Return _splitButton_location
            End Get
        End Property
    End Class
End Namespace
