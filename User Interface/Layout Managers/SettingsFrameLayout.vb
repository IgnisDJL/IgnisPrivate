Namespace UI

    Public Class SettingsFrameLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(800, 600)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly SETTINGS_MENU_PANEL_WIDTH As Integer = 220

        Public Shared ReadOnly UNDO_REDO_BUTTONS_SIZE As Size = New Size(CONTROL_BUTTONS_WIDTH - 20, CONTROL_BUTTONS_HEIGHT)

        ' Components Attributes
        Private _settingsMenuPanel_size As Size
        Private _settingsMenuPanel_location As Point

        Private _settingsFormPanel_size As Size
        Private _settingsFormPanel_location As Point

        Private _buttonsPanel_size As Size
        Private _buttonsPanel_location As Point

        ' -- Inside Buttons Panel
        Private _backButton_size As Size
        Private _backButton_location As Point

        Private _undoButton_size As Size
        Private _undoButton_location As Point

        Private _redoButton_size As Size
        Private _redoButton_location As Point

        ' Attributes



        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

            Me._settingsMenuPanel_location = New Point(0, 0)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            '
            ' Settings Menu Panel
            '
            Me._settingsMenuPanel_size = New Size(SETTINGS_MENU_PANEL_WIDTH, Me.Height - BUTTONS_PANEL_HEIGHT)
            '
            ' Buttons Panel
            '
            Me._buttonsPanel_location = New Point(0, Me._settingsMenuPanel_location.Y + Me._settingsMenuPanel_size.Height)
            Me._buttonsPanel_size = New Size(Me.Width, BUTTONS_PANEL_HEIGHT)
            '
            ' Settings Form Panel
            '
            Me._settingsFormPanel_location = New Point(Me._settingsMenuPanel_location.X + Me._settingsMenuPanel_size.Width, 0)
            Me._settingsFormPanel_size = New Size(Me.Width - SETTINGS_MENU_PANEL_WIDTH, Me.Height - BUTTONS_PANEL_HEIGHT + 1)
            '
            ' Back Button (Inside Buttons Panel)
            '
            _backButton_location = New Point(LOCATION_START_X, BUTTONS_PANEL_LOCATION_START_Y)
            _backButton_size = New Size(CONTROL_BUTTONS_WIDTH, CONTROL_BUTTONS_HEIGHT)
            '
            ' Redo Button (Inside Buttons Panel)
            '
            _redoButton_location = New Point(Me.ButtonsPanel_Size.Width - LOCATION_START_X - UNDO_REDO_BUTTONS_SIZE.Width, BUTTONS_PANEL_LOCATION_START_Y)
            _redoButton_size = UNDO_REDO_BUTTONS_SIZE
            '
            ' Undo Button (Inside Buttons Panel)
            '
            _undoButton_location = New Point(Me.RedoButton_Location.X - SPACE_BETWEEN_CONTROLS_X - UNDO_REDO_BUTTONS_SIZE.Width, BUTTONS_PANEL_LOCATION_START_Y)
            _undoButton_size = UNDO_REDO_BUTTONS_SIZE

        End Sub

        '
        ' Settings Menu Panel
        '
        Public ReadOnly Property SettingsMenuPanel_Size As Size
            Get
                Return _settingsMenuPanel_size
            End Get
        End Property
        Public ReadOnly Property SettingsMenuPanel_Location As Point
            Get
                Return _settingsMenuPanel_location
            End Get
        End Property
        '
        ' Settings Form Panel
        '
        Public ReadOnly Property SettingsFormPanel_Size As Size
            Get
                Return _settingsFormPanel_size
            End Get
        End Property
        Public ReadOnly Property SettingsFormPanel_Location As Point
            Get
                Return _settingsFormPanel_location
            End Get
        End Property
        '
        ' Buttons Panel
        '
        Public ReadOnly Property ButtonsPanel_Size As Size
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
        ' Undo Button (Inside Button Panel)
        '
        Public ReadOnly Property UndoButton_Size As Size
            Get
                Return _undoButton_size
            End Get
        End Property
        Public ReadOnly Property UndoButton_Location As Point
            Get
                Return _undoButton_location
            End Get
        End Property
        '
        ' Redo Button (Inside Button Panel)
        '
        Public ReadOnly Property RedoButton_Size As Size
            Get
                Return _redoButton_size
            End Get
        End Property
        Public ReadOnly Property RedoButton_Location As Point
            Get
                Return _redoButton_location
            End Get
        End Property

    End Class
End Namespace
