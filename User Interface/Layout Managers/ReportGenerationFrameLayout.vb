Namespace UI

    Public Class ReportGenerationFrameLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(725, 500)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Private Shared ReadOnly PROGRESSION_PANEL_HEIGHT As Integer = 80
        Private Shared ReadOnly PROGRESSION_PANEL_PADDING_Y = 5
        Private Shared ReadOnly PROGRESSION_PANEL_SECTION_Y As Integer = (PROGRESSION_PANEL_HEIGHT - 2 * PROGRESSION_PANEL_PADDING_Y) / 3

        Private Shared ReadOnly ANALYSIS_LABEL_SIZE As Size = New Size(105, PROGRESSION_PANEL_SECTION_Y)
        Private Shared ReadOnly MANUAL_DATA_LABEL_SIZE As Size = New Size(125, PROGRESSION_PANEL_SECTION_Y)
        Private Shared ReadOnly DELAYS_LABEL_SIZE As Size = New Size(90, PROGRESSION_PANEL_SECTION_Y)
        Private Shared ReadOnly COMMENTS_LABEL_SIZE As Size = New Size(155, PROGRESSION_PANEL_SECTION_Y)
        Private Shared ReadOnly GENERATION_LABEL_SIZE As Size = New Size(130, PROGRESSION_PANEL_SECTION_Y)
        Private Shared ReadOnly TOP_LABELS_WIDTH_SUM As Integer = 105 + 125 + 90 + 155 + 130
        ' Attributes


        ' Components Attributes

        Private _progressionPanel_size As Size
        Private _progressionPanel_location As Point
        ' -- Inside Progression Panel
        Private _progressionTitleLabel_size As Size
        Private _progressionTitleLabel_location As Point
        Private _progressionBar_size As Size
        Private _progressionBar_location As Point
        Private _analysisStepLabel_size As Size
        Private _analysisStepLabel_location As Point
        Private _manualDataStepLabel_size As Size
        Private _manualDataStepLabel_location As Point
        Private _eventsJustificationStepLabel_size As Size
        Private _delaysJustificationStepLabel_location As Point
        Private _commentsStepLabel_size As Size
        Private _commentsStepLabel_location As Point
        Private _finishingGenerationStepLabel_size As Size
        Private _finishingGenerationStepLabel_location As Point

        Private _generationStepView_size As Size
        Private _generationStepView_location As Point

        Private _buttonsPanel_size As Size
        Private _buttonsPanel_location As Point
        ' -- Inside Buttons Panel
        Private _backButton_size As Size
        Private _backButton_location As Point
        Private _cancelButton_size As Size
        Private _cancelButton_location As Point


        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)


            _progressionPanel_location = New Point(0, 0)

            _generationStepView_location = New Point(0, PROGRESSION_PANEL_HEIGHT)


        End Sub

        Protected Overloads Overrides Sub computeLayout()

            '
            ' Progression Panel
            '
            _progressionPanel_size = New Size(Me.Width, PROGRESSION_PANEL_HEIGHT)

            '
            ' Progression title Label (Inside Progression Panel)
            '
            _progressionTitleLabel_location = New Point(0, PROGRESSION_PANEL_PADDING_Y)
            _progressionTitleLabel_size = New Size(Me.Width, PROGRESSION_PANEL_SECTION_Y)
            '
            ' Progression Bar (Inside Progression Panel)
            '
            _progressionBar_location = New Point(LOCATION_START_X, ProgressionTitleLabel_Location.Y + ProgressionTitleLabel_Size.Height + PROGRESSION_PANEL_PADDING_Y)
            _progressionBar_size = New Size(Me.Width - 2 * LOCATION_START_X, PROGRESSION_PANEL_SECTION_Y / 2)

            Dim stepLabels_Y As Integer = ProgressionPanel_Size.Height - PROGRESSION_PANEL_SECTION_Y - PROGRESSION_PANEL_PADDING_Y
            Dim spaceBetweenLabels_X = (ProgressionBar_Size.Width - TOP_LABELS_WIDTH_SUM) / 4
            '
            ' Analysis Step Label (Inside Progression Panel)
            '
            _analysisStepLabel_location = New Point(SPACE_BETWEEN_CONTROLS_X + 15, stepLabels_Y)
            '
            ' Manual Data Step Label (Inside Progression Panel)
            '
            _manualDataStepLabel_location = New Point(AnalysisStepLabel_Location.X + ANALYSIS_LABEL_SIZE.Width + spaceBetweenLabels_X, stepLabels_Y)
            '
            ' Delays Justification Step Label (Inside Progression Panel)
            '
            _delaysJustificationStepLabel_location = New Point(ManualDataStepLabel_Location.X + MANUAL_DATA_LABEL_SIZE.Width + spaceBetweenLabels_X, stepLabels_Y)
            '
            ' KA01_Comments Step Label (Inside Progression Panel)
            '
            _commentsStepLabel_location = New Point(DelaysJustificationStepLabel_Location.X + DELAYS_LABEL_SIZE.Width + spaceBetweenLabels_X, stepLabels_Y)
            '
            ' Finishing Generation Step Label (Inside Progression Panel)
            '
            _finishingGenerationStepLabel_location = New Point(ProgressionBar_Location.X + ProgressionBar_Size.Width - GENERATION_LABEL_SIZE.Width, stepLabels_Y)
            '
            ' Generation Step View
            '
            _generationStepView_size = New Size(Me.Width, Me.Height - PROGRESSION_PANEL_HEIGHT - BUTTONS_PANEL_HEIGHT)
            '
            ' Buttons Panel
            '
            _buttonsPanel_location = New Point(0, Me.GenerationStepView_Location.Y + Me.GenerationStepView_Size.Height)
            _buttonsPanel_size = New Size(Me.Width, BUTTONS_PANEL_HEIGHT)
            '
            ' Back Button (Inside Buttons Panel)
            '
            _backButton_location = New Point(LOCATION_START_X, BUTTONS_PANEL_LOCATION_START_Y)
            _backButton_size = New Size(CONTROL_BUTTONS_WIDTH, CONTROL_BUTTONS_HEIGHT)
            '
            ' Cancel Button (Inside Buttons Panel)
            '
            _cancelButton_location = New Point(LOCATION_START_X + BackButton_Size.Width + SPACE_BETWEEN_CONTROLS_X, BUTTONS_PANEL_LOCATION_START_Y)
            _cancelButton_size = New Size(CONTROL_BUTTONS_WIDTH, CONTROL_BUTTONS_HEIGHT)

        End Sub

        '
        ' Progression Panel
        '
        Public ReadOnly Property ProgressionPanel_Size As Size
            Get
                Return _progressionPanel_size
            End Get
        End Property
        Public ReadOnly Property ProgressionPanel_Location As Point
            Get
                Return _progressionPanel_location
            End Get
        End Property

        '
        ' Progression title Label (Inside Progression Panel)
        '
        Public ReadOnly Property ProgressionTitleLabel_Size As Size
            Get
                Return _progressionTitleLabel_size
            End Get
        End Property
        Public ReadOnly Property ProgressionTitleLabel_Location As Point
            Get
                Return _progressionTitleLabel_location
            End Get
        End Property

        '
        ' Progression Bar (Inside Progression Panel)
        '
        Public ReadOnly Property ProgressionBar_Size As Size
            Get
                Return _progressionBar_size
            End Get
        End Property
        Public ReadOnly Property ProgressionBar_Location As Point
            Get
                Return _progressionBar_location
            End Get
        End Property

        '
        ' Analysis Step Label (Inside Progression Panel)
        '
        Public ReadOnly Property AnalysisStepLabel_Size As Size
            Get
                Return ANALYSIS_LABEL_SIZE
            End Get
        End Property
        Public ReadOnly Property AnalysisStepLabel_Location As Point
            Get
                Return _analysisStepLabel_location
            End Get
        End Property

        '
        ' Manual Data Step Label (Inside Progression Panel)
        '
        Public ReadOnly Property ManualDataStepLabel_Size As Size
            Get
                Return MANUAL_DATA_LABEL_SIZE
            End Get
        End Property
        Public ReadOnly Property ManualDataStepLabel_Location As Point
            Get
                Return _manualDataStepLabel_location
            End Get
        End Property

        '
        ' Delays Justification Step Label (Inside Progression Panel)
        '
        Public ReadOnly Property DelaysJustificationStepLabel_Size As Size
            Get
                Return DELAYS_LABEL_SIZE
            End Get
        End Property
        Public ReadOnly Property DelaysJustificationStepLabel_Location As Point
            Get
                Return _delaysJustificationStepLabel_location
            End Get
        End Property

        '
        ' KA01_Comments Step Label (Inside Progression Panel)
        '
        Public ReadOnly Property CommentsStepLabel_Size As Size
            Get
                Return COMMENTS_LABEL_SIZE
            End Get
        End Property
        Public ReadOnly Property CommentsStepLabel_Location As Point
            Get
                Return _commentsStepLabel_location
            End Get
        End Property

        '
        ' Finishing Generation Step Label (Inside Progression Panel)
        '
        Public ReadOnly Property FinishingGenerationStepLabel_Size As Size
            Get
                Return GENERATION_LABEL_SIZE
            End Get
        End Property
        Public ReadOnly Property FinishingGenerationStepLabel_Location As Point
            Get
                Return _finishingGenerationStepLabel_location
            End Get
        End Property

        '
        ' Generation Step View
        '
        Public ReadOnly Property GenerationStepView_Size As Size
            Get
                Return _generationStepView_size
            End Get
        End Property
        Public ReadOnly Property GenerationStepView_Location As Point
            Get
                Return _generationStepView_location
            End Get
        End Property

        '
        ' Buttons Panel
        '
        Public ReadOnly Property buttonsPanel_Size As Size
            Get
                Return _buttonsPanel_size
            End Get
        End Property
        Public ReadOnly Property ButtonsPanel_Location As Point
            Get
                Return _buttonsPanel_location
            End Get
        End Property

        '
        ' Back Button (Inside Button Panel)
        '
        Public ReadOnly Property BackButton_Size As Size
            Get
                Return _backButton_size
            End Get
        End Property
        Public ReadOnly Property BackButton_Location As Point
            Get
                Return _backButton_location
            End Get
        End Property

        '
        ' Cancel Button (Inside Button Panel)
        '
        Public ReadOnly Property CancelButton_Size As Size
            Get
                Return _cancelButton_size
            End Get
        End Property
        Public ReadOnly Property CancelButton_Location As Point
            Get
                Return _cancelButton_location
            End Get
        End Property

    End Class
End Namespace
