Namespace UI

    Public MustInherit Class ArchivesExplorerViewTemplateLayout
        Inherits LayoutManager

        ' Components Attributes

        ' Date Picker Panel
        Private _datePickerPanel_location As Point
        Private _datePickerPanel_size As Size

        ' Available Dates List View
        Private _availableDatesListView_location As Point
        Private _availableDatesListView_size As Size

        ' Available Files List View
        Private _availableFilesListView_location As Point
        Private _availableFilesListView_size As Size

        ' Back Button
        Private _backButton_location As Point

        ' Attributes
        Protected Sub New(childMinimumSize As Size, childCondensedSize As Size)
            MyBase.New(childMinimumSize, childCondensedSize)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' Date Picker Panel
            Me._datePickerPanel_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            _datePickerPanel_size = New Size(Me.Width - 2 * LOCATION_START_X, Common.DatePickerPanel.HEIGHT)

            ' Available Dates List View
            Me._availableDatesListView_location = New Point(LOCATION_START_X, LOCATION_START_Y + DatePickerPanel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._availableDatesListView_size = New Size((Me.Width - 2 * LOCATION_START_X - SPACE_BETWEEN_CONTROLS_X) / 2, Me.Height - Me.DatePickerPanel_Size.Height - CONTROL_BUTTONS_HEIGHT - LOCATION_START_Y - 3 * SPACE_BETWEEN_CONTROLS_Y)

            ' Available Files List View
            Me._availableFilesListView_location = New Point(AvailableDatesListView_Location.X + AvailableDatesListView_Size.Width + SPACE_BETWEEN_CONTROLS_X, AvailableDatesListView_Location.Y)
            Me._availableFilesListView_size = New Size(AvailableDatesListView_Size.Width, (AvailableDatesListView_Size.Height - SPACE_BETWEEN_CONTROLS_Y) / 2)

            ' Back Button
            Me._backButton_location = New Point(LOCATION_START_X, Me.Height - SPACE_BETWEEN_CONTROLS_Y - CONTROL_BUTTONS_HEIGHT)

        End Sub

        ' 
        ' Date Picker Panel
        ' 
        Public ReadOnly Property DatePickerPanel_Location As Point
            Get
                Return Me._datePickerPanel_location
            End Get
        End Property
        Public ReadOnly Property DatePickerPanel_Size As Size
            Get
                Return Me._datePickerPanel_size
            End Get
        End Property
        ' 
        ' Available Dates List View
        ' 
        Public ReadOnly Property AvailableDatesListView_Location As Point
            Get
                Return Me._availableDatesListView_location
            End Get
        End Property
        Public ReadOnly Property AvailableDatesListView_Size As Size
            Get
                Return Me._availableDatesListView_size
            End Get
        End Property
        ' 
        ' Available Files List View
        ' 
        Public ReadOnly Property AvailableFilesListView_Location As Point
            Get
                Return Me._availableFilesListView_location
            End Get
        End Property
        Public ReadOnly Property AvailableFilesListView_Size As Size
            Get
                Return Me._availableFilesListView_size
            End Get
        End Property
        ' 
        ' Back Button
        ' 
        Public ReadOnly Property BackButton_Location As Point
            Get
                Return Me._backButton_location
            End Get
        End Property

    End Class
End Namespace
