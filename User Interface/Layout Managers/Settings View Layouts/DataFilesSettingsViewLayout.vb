Namespace UI

    Public Class DataFilesSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly USB_PATH_PANEL_HEIGHT = 80

        Public Shared ReadOnly UNITS_PANEL_HEIGHT = 200

        ' Components Attributes
        Private _usbPathPanel_size As Size
        Private _usbPathPanel_location As Point

        Private _usbPathLabel_size As Size
        Private _usbPathLabel_location As Point
        Private _usbPathLabel_fontSize As Integer

        Private _usbPathTextBox_size As Size
        Private _usbPathTextBox_location As Point
        Private _usbPathTextBox_fontSize As Integer

        Private _modifyPathButton_size As Size
        Private _modifyPathButton_location As Point

        Private _unitsPanel1_size As Size
        Private _unitsPanel1_location As Point

        Private _unitsPanel2_size As Size
        Private _unitsPanel2_location As Point

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            '
            ' USB Path Panel
            '
            _usbPathPanel_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            _usbPathPanel_size = New Size(Me.Width - 2 * LOCATION_START_X, USB_PATH_PANEL_HEIGHT)
            '
            ' USB Path Label (in USB Path Panel)
            '
            _usbPathLabel_location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            _usbPathLabel_size = New Size(USBPathPanel_Size.Width - 2 * SPACE_BETWEEN_CONTROLS_X, CONTROL_BUTTONS_HEIGHT - 5)
            '
            ' USB Path TextBox (in USB Path Panel)
            '
            _usbPathTextBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, USBPathLabel_Location.Y + USBPathLabel_Size.Height)
            _usbPathTextBox_size = New Size(USBPathPanel_Size.Width - CONTROL_BUTTONS_WIDTH - 10 - 2 * SPACE_BETWEEN_CONTROLS_X, USBPathLabel_Size.Height)
            _usbPathTextBox_fontSize = _usbPathTextBox_size.Height / 2
            '
            ' ModifyPathButton (in USB Path Panel)
            '
            _modifyPathButton_location = New Point(USBPathTextBox_Location.X + USBPathTextBox_Size.Width + 10, USBPathTextBox_Location.Y)
            _modifyPathButton_size = New Size(CONTROL_BUTTONS_WIDTH, USBPathTextBox_Size.Height)
            '
            ' Units Panel 1
            '
            Me._unitsPanel1_location = New Point(LOCATION_START_X, Me.USBPathPanel_Location.Y + Me.USBPathPanel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._unitsPanel1_size = New Size(Me.Width - 2 * LOCATION_START_X, UNITS_PANEL_HEIGHT)
            '
            ' Units Panel 2
            '
            Me._unitsPanel2_location = New Point(LOCATION_START_X, Me.UnitsPanel1_Location.Y + Me.UnitsPanel1_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._unitsPanel2_size = New Size(Me.Width - 2 * LOCATION_START_X, UNITS_PANEL_HEIGHT)

        End Sub

        '
        ' USB Path Panel
        '
        Public ReadOnly Property USBPathPanel_Size As Size
            Get
                Return _usbPathPanel_size
            End Get
        End Property
        Public ReadOnly Property USBPathPanel_Location As Point
            Get
                Return _usbPathPanel_location
            End Get
        End Property
        '
        ' USB Path Label
        '
        Public ReadOnly Property USBPathLabel_Size As Size
            Get
                Return _usbPathLabel_size
            End Get
        End Property
        Public ReadOnly Property USBPathLabel_Location As Point
            Get
                Return _usbPathLabel_location
            End Get
        End Property
        '
        ' USB Path TextBox
        '
        Public ReadOnly Property USBPathTextBox_Size As Size
            Get
                Return _usbPathTextBox_size
            End Get
        End Property
        Public ReadOnly Property USBPathTextBox_Location As Point
            Get
                Return _usbPathTextBox_location
            End Get
        End Property
        '
        ' Modify Path Button
        '
        Public ReadOnly Property ModifyPathButton_Size As Size
            Get
                Return _modifyPathButton_size
            End Get
        End Property
        Public ReadOnly Property ModifyPathButton_Location As Point
            Get
                Return _modifyPathButton_location
            End Get
        End Property
        '
        ' Units Panel 1
        '
        Public ReadOnly Property UnitsPanel1_Size As Size
            Get
                Return _unitsPanel1_size
            End Get
        End Property
        Public ReadOnly Property UnitsPanel1_Location As Point
            Get
                Return _unitsPanel1_location
            End Get
        End Property
        '
        ' Units Panel 2
        '
        Public ReadOnly Property UnitsPanel2_Size As Size
            Get
                Return _unitsPanel2_size
            End Get
        End Property
        Public ReadOnly Property UnitsPanel2_Location As Point
            Get
                Return _unitsPanel2_location
            End Get
        End Property

    End Class
End Namespace
