Namespace UI

    Public Class MainMenuLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(465, 420)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(705, 0)

        Public Shared ReadOnly SPACE_BETWEEN_BUTTONS_X = 10
        Public Shared ReadOnly SPACE_BETWEEN_BUTTONS_Y = 10

        Public Shared ReadOnly IMPORT_FILES_BUTTON_TEXT_FULL As String = "Importer Des Fichiers De La Clé USB"
        Public Shared ReadOnly IMPORT_FILES_BUTTON_TEXT_CONDENSED As String = "Importer"

        Public Shared ReadOnly CREATE_DAILY_REPORTS_BUTTON_TEXT_FULL As String = "Créer Des Rapports Journaliers"
        Public Shared ReadOnly CREATE_DAILY_REPORTS_BUTTON_TEXT_CONDENSED As String = "Journaliers"

        Public Shared ReadOnly CREATE_PERIODIC_REPORTS_BUTTON_TEXT_FULL As String = "Créer Des Rapports Périodiques"
        Public Shared ReadOnly CREATE_PERIODIC_REPORTS_BUTTON_TEXT_CONDENSED As String = "Périodiques"

        Public Shared ReadOnly EXPORT_FILES_BUTTON_TEXT_FULL As String = "Exporter Des Fichiers Et Des Rapports"
        Public Shared ReadOnly EXPORT_FILES_BUTTON_TEXT_CONDENSED As String = "Exporter"

        Public Shared ReadOnly CONFIGURE_SETTINGS_BUTTON_TEXT As String = "    Paramètres"
        Public Shared ReadOnly CONFIGURE_SETTINGS_BUTTON_SIZE As Size = New Size(150, 40)


        ' Common attributes
        Private bigButtonsWidth As Integer
        Private smallButtonsWidth As Integer

        Private buttonsHeight As Integer

        ' Components attributes
        Private _importFilesButton_size As Size
        Private _importFilesButton_location As Point
        Private _importFilesButton_text As String

        Private _createDailyReportsButton_size As Size
        Private _createDailyReportsButton_location As Point
        Private _createDailyReportsButton_text As String

        Private _createPeriodicReportsButton_size As Size
        Private _createPeriodicReportsButton_location As Point
        Private _createPeriodicReportsButton_text As String

        Private _exportFilesButton_size As Size
        Private _exportFilesButton_location As Point
        Private _exportFilesButton_text As String

        Private _configureSettingsButton_size As Size
        Private _configureSettingsButton_location As Point
        Private _configureSettingsButton_text As String

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)
        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' Common attributes
            buttonsHeight = (Me.Height - 2 * LOCATION_START_Y - 3 * SPACE_BETWEEN_BUTTONS_Y) / 4
            bigButtonsWidth = Me.Width - LOCATION_START_X * 2
            smallButtonsWidth = (bigButtonsWidth - SPACE_BETWEEN_BUTTONS_X) / 2

            '
            ' Import Files Button
            '
            _importFilesButton_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            _importFilesButton_size = New Size(bigButtonsWidth, buttonsHeight)
            _importFilesButton_text = If(Me.WidthState = SizeState.FULL, IMPORT_FILES_BUTTON_TEXT_FULL, IMPORT_FILES_BUTTON_TEXT_CONDENSED)
            '
            ' Create Daily Reports Button
            '
            _createDailyReportsButton_location = New Point(ImportFilesButton_Location.X, ImportFilesButton_Location.Y + ImportFilesButton_Size.Height + SPACE_BETWEEN_BUTTONS_Y)
            _createDailyReportsButton_size = New Size(smallButtonsWidth, buttonsHeight)
            _createDailyReportsButton_text = If(Me.WidthState = SizeState.FULL, CREATE_DAILY_REPORTS_BUTTON_TEXT_FULL, CREATE_DAILY_REPORTS_BUTTON_TEXT_CONDENSED)
            '
            ' Create Periodic Reports Button
            '
            _createPeriodicReportsButton_location = New Point(CreateDailyReportsButton_Location.X + CreateDailyReportsButton_Size.Width + SPACE_BETWEEN_BUTTONS_X, CreateDailyReportsButton_Location.Y)
            _createPeriodicReportsButton_size = New Size(smallButtonsWidth, buttonsHeight)
            _createPeriodicReportsButton_text = If(Me.WidthState = SizeState.FULL, CREATE_PERIODIC_REPORTS_BUTTON_TEXT_FULL, CREATE_PERIODIC_REPORTS_BUTTON_TEXT_CONDENSED)
            '
            ' Export Files Button
            '
            _exportFilesButton_location = New Point(ImportFilesButton_Location.X, CreateDailyReportsButton_Location.Y + CreateDailyReportsButton_Size.Height + SPACE_BETWEEN_BUTTONS_Y)
            _exportFilesButton_size = New Size(bigButtonsWidth, buttonsHeight)
            _exportFilesButton_text = If(Me.WidthState = SizeState.FULL, EXPORT_FILES_BUTTON_TEXT_FULL, EXPORT_FILES_BUTTON_TEXT_CONDENSED)
            '
            ' Configure Settings Button
            '
            _configureSettingsButton_location = New Point(LOCATION_START_X, Me.Height - SPACE_BETWEEN_BUTTONS_Y - CONFIGURE_SETTINGS_BUTTON_SIZE.Height)
            _configureSettingsButton_size = CONFIGURE_SETTINGS_BUTTON_SIZE

        End Sub

        '
        ' Import Files Button
        '
        Public ReadOnly Property ImportFilesButton_Size As Size
            Get
                Return Me._importFilesButton_size
            End Get
        End Property
        Public ReadOnly Property ImportFilesButton_Location As Point
            Get
                Return Me._importFilesButton_location
            End Get
        End Property
        Public ReadOnly Property ImportFilesButton_Text As String
            Get
                Return Me._importFilesButton_text
            End Get
        End Property

        '
        ' Create Daily Reports Button
        '
        Public ReadOnly Property CreateDailyReportsButton_Size As Size
            Get
                Return Me._createDailyReportsButton_size
            End Get
        End Property
        Public ReadOnly Property CreateDailyReportsButton_Location As Point
            Get
                Return Me._createDailyReportsButton_location
            End Get
        End Property
        Public ReadOnly Property CreateDailyReportsButton_Text As String
            Get
                Return Me._createDailyReportsButton_text
            End Get
        End Property

        '
        ' Create Periodic Reports Button
        '
        Public ReadOnly Property CreatePeriodicReportsButton_Size As Size
            Get
                Return Me._createPeriodicReportsButton_size
            End Get
        End Property
        Public ReadOnly Property CreatePeriodicReportsButton_Location As Point
            Get
                Return Me._createPeriodicReportsButton_location
            End Get
        End Property
        Public ReadOnly Property CreatePeriodicReportsButton_Text As String
            Get
                Return Me._createPeriodicReportsButton_text
            End Get
        End Property

        '
        ' Export Files Button
        '
        Public ReadOnly Property ExportFilesButton_Size As Size
            Get
                Return Me._exportFilesButton_size
            End Get
        End Property
        Public ReadOnly Property ExportFilesButton_Location As Point
            Get
                Return Me._exportFilesButton_location
            End Get
        End Property
        Public ReadOnly Property ExportFilesButton_Text As String
            Get
                Return Me._exportFilesButton_text
            End Get
        End Property

        '
        ' Configure Settings Button
        '
        Public ReadOnly Property ConfigureSettingsButton_Size As Size
            Get
                Return Me._configureSettingsButton_size
            End Get
        End Property
        Public ReadOnly Property ConfigureSettingsButton_Location As Point
            Get
                Return Me._configureSettingsButton_location
            End Get
        End Property
        Public ReadOnly Property ConfigureSettingsButton_Text As String
            Get
                Return CONFIGURE_SETTINGS_BUTTON_TEXT
            End Get
        End Property
    End Class
End Namespace
