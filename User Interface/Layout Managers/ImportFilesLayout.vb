Imports IGNIS.UI.Common

Namespace UI

    Public Class ImportFilesLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(315, 330)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly USB_PATH_PANEL_HEIGHT = 80

        Public Shared ReadOnly BUTTONS_WIDTH As Integer = 120
        Public Shared ReadOnly BUTTONS_HEIGHT As Integer = 40

        Public Shared ReadOnly BOTTOM_PADDING As Integer = 10

        Public Shared ReadOnly MODIFY_PATH_BUTTON_TEXT As String = "    Parcourir"
        Public Shared ReadOnly IMPORT_BUTTON_TEXT As String = "     Importer"
        Public Shared ReadOnly REFRESH_BUTTON_TEXT As String = "    Actualiser"


        ' Common Attributes

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
        Private _modifyPathButton_text As String = MODIFY_PATH_BUTTON_TEXT

        Private _refreshButton_size As Size
        Private _refreshButton_location As Point
        Private _refreshButton_text As String = REFRESH_BUTTON_TEXT

        Private _fileList_size As Size
        Private _fileList_location As Point

        Private _backButton_size As Size
        Private _backButton_location As Point
        Private _backButton_text As String

        Private _importButton_size As Size
        Private _importButton_location As Point
        Private _importButton_text As String = IMPORT_BUTTON_TEXT

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
            _usbPathLabel_location = New Point(5, 5)
            _usbPathLabel_size = New Size(USBPathPanel_Size.Width - 2 * 5, BUTTONS_HEIGHT - 5)
            '
            ' USB Path TextBox (in USB Path Panel)
            '
            _usbPathTextBox_location = New Point(5, USBPathLabel_Location.Y + USBPathLabel_Size.Height)
            _usbPathTextBox_size = New Size(USBPathPanel_Size.Width - BUTTONS_WIDTH - 10 - 2 * 5, USBPathLabel_Size.Height)
            _usbPathTextBox_fontSize = _usbPathTextBox_size.Height / 2
            '
            ' ModifyPathButton (in USB Path Panel)
            '
            _modifyPathButton_location = New Point(USBPathTextBox_Location.X + USBPathTextBox_Size.Width + 10, USBPathTextBox_Location.Y)
            _modifyPathButton_size = New Size(BUTTONS_WIDTH, USBPathTextBox_Size.Height)
            '
            ' File List
            '
            _fileList_location = New Point(USBPathPanel_Location.X, USBPathPanel_Location.Y + USBPathPanel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            _fileList_size = New Size(Me.Width - 2 * LOCATION_START_X, Me.Height - 2 * SPACE_BETWEEN_CONTROLS_Y - BUTTONS_HEIGHT - USB_PATH_PANEL_HEIGHT - LOCATION_START_Y - BOTTOM_PADDING)
            '
            ' RefreshButton
            '
            _refreshButton_size = New Size(BUTTONS_WIDTH, FileListControl.TITLE_LABEL_HEIGHT)
            _refreshButton_location = New Point(FileList_Location.X + FileList_Size.Width - BUTTONS_WIDTH - 2, FileList_Location.Y + 2)
            '
            ' Back Button
            '
            _backButton_location = New Point(USBPathPanel_Location.X, FileList_Location.Y + FileList_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            _backButton_size = New Size(CONTROL_BUTTONS_WIDTH, BUTTONS_HEIGHT)
            '
            ' Import Button
            '
            _importButton_location = New Point(Me.Width - LOCATION_START_X - BUTTONS_WIDTH, BackButton_Location.Y)
            _importButton_size = New Size(BUTTONS_WIDTH, BUTTONS_HEIGHT)

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
        Public ReadOnly Property USBPathTextBox_FontSize As Integer
            Get
                Return _usbPathTextBox_fontSize
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
        Public ReadOnly Property ModifyPathButton_Text As String
            Get
                Return _modifyPathButton_text
            End Get
        End Property

        '
        ' Refresh Button
        '
        Public ReadOnly Property RefreshButton_Size As Size
            Get
                Return _refreshButton_size
            End Get
        End Property
        Public ReadOnly Property RefreshButton_Location As Point
            Get
                Return _refreshButton_location
            End Get
        End Property
        Public ReadOnly Property RefreshButton_Text As String
            Get
                Return _refreshButton_text
            End Get
        End Property

        '
        ' File List
        '
        Public ReadOnly Property FileList_Size As Size
            Get
                Return _fileList_size
            End Get
        End Property
        Public ReadOnly Property FileList_Location As Point
            Get
                Return _fileList_location
            End Get
        End Property

        '
        ' Back Button
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
        Public ReadOnly Property BackButton_Text As String
            Get
                Return _backButton_text
            End Get
        End Property

        '
        ' Import Button
        '
        Public ReadOnly Property ImportButton_Size As Size
            Get
                Return _importButton_size
            End Get
        End Property
        Public ReadOnly Property ImportButton_Location As Point
            Get
                Return _importButton_location
            End Get
        End Property
        Public ReadOnly Property ImportButton_Text As String
            Get
                Return _importButton_text
            End Get
        End Property

        '
        ' Incorrect USB Path Message Panel
        Public ReadOnly Property IncorrectUSBPathMessagePanel_Location() As Point
            Get
                Dim location = New Point(Me.USBPathTextBox_Location.X, 0)
                location.Offset(Me.USBPathPanel_Location)
                location.Offset(New Point(-5, USBPathPanel_Size.Height + 1))

                Return location
            End Get
        End Property

    End Class
End Namespace
