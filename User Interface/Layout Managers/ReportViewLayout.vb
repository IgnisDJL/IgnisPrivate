Namespace UI

    Public Class ReportViewLayout
        Inherits ArchivesExplorerViewTemplateLayout

        ' Constants
        Public Shared Shadows ReadOnly MINIMUM_SIZE As Size = New Size(510, 500)
        Public Shared Shadows ReadOnly CONDENSED_SIZE As Size = New Size(705, 0)

        Public Shared ReadOnly GENERATE_BUTTONS_SIZE As Size = New Size(200, CONTROL_BUTTONS_HEIGHT)

        Public Shared ReadOnly DATE_LIST_VIEW_BUTTON_SIZE As Size = New Size(30, 30)


        ' Common Attributes


        ' Components Attributes
        Private _showOnlyReportReadyDatesCheckBox_location As Point

        Private _reportsToGenerateList_size As Size
        Private _reportsToGenerateList_location As Point

        Private _generateButton_size As Size
        Private _generateButton_location As Point
        Private _generateButton_text As String

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)
        End Sub

        Protected Overloads Overrides Sub computeLayout()
            MyBase.computeLayout()

            '
            ' Reports To Generate List
            '
            _reportsToGenerateList_location = New Point(Me.AvailableFilesListView_Location.X, Me.AvailableFilesListView_Location.Y + Me.AvailableFilesListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            _reportsToGenerateList_size = Me.AvailableFilesListView_Size
            '
            ' Generation Button
            '
            _generateButton_location = New Point(Me.Width - LOCATION_START_X - GENERATE_BUTTONS_SIZE.Width, Me.Height - GENERATE_BUTTONS_SIZE.Height - SPACE_BETWEEN_CONTROLS_Y)

        End Sub

        '
        ' Reports To Generate List
        '
        Public ReadOnly Property ReportsToGenerateList_Size As Size
            Get
                Return _reportsToGenerateList_size
            End Get
        End Property
        Public ReadOnly Property ReportsToGenerateList_Location As Point
            Get
                Return _reportsToGenerateList_location
            End Get
        End Property

        '
        ' Generate Button
        '
        Public ReadOnly Property GenerateButton_Size As Size
            Get
                Return GENERATE_BUTTONS_SIZE
            End Get
        End Property
        Public ReadOnly Property GenerateButton_Location As Point
            Get
                Return _generateButton_location
            End Get
        End Property
        Public ReadOnly Property GenerateButton_Text As String
            Get
                Return _generateButton_text
            End Get
        End Property

    End Class

End Namespace
