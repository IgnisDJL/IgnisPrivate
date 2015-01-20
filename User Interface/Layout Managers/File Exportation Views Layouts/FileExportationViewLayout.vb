Namespace UI

    Public Class FileExportationViewLayout
        Inherits ArchivesExplorerViewTemplateLayout

        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(600, 400)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly SEND_AS_EMAIL_BUTTON_SIZE As Size = New Size(135, CONTROL_BUTTONS_HEIGHT)
        Public Shared ReadOnly SAVE_AS_FILE_BUTTON_SIZE As Size = New Size(140, CONTROL_BUTTONS_HEIGHT)

        ' Components Attributes

        ' Selected Files List View
        Private _selectedFilesListView_location As Point
        Private _selectedFilesListView_size As Size

        ' Send As Email Button
        Private _sendAsEmailButton_location As Point

        ' Save As File Button
        Private _saveAsFileButton_location As Point


        ' Attributes
        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)


        End Sub

        Protected Overloads Overrides Sub computeLayout()
            MyBase.computeLayout()

            ' Selected Files List View
            Me._selectedFilesListView_location = New Point(Me.AvailableFilesListView_Location.X, Me.AvailableFilesListView_Location.Y + Me.AvailableFilesListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._selectedFilesListView_size = Me.AvailableFilesListView_Size

            ' Send As Email Button
            Me._sendAsEmailButton_location = New Point(Me.Width - LOCATION_START_X - SEND_AS_EMAIL_BUTTON_SIZE.Width, Me.BackButton_Location.Y)

            ' Save As File Button
            Me._saveAsFileButton_location = New Point(Me.SendAsEmailButton_Location.X - SPACE_BETWEEN_CONTROLS_X - SAVE_AS_FILE_BUTTON_SIZE.Width, Me.BackButton_Location.Y)

        End Sub

        ' 
        ' Selected Files List View
        ' 
        Public ReadOnly Property SelectedFilesListView_Location As Point
            Get
                Return Me._selectedFilesListView_location
            End Get
        End Property
        Public ReadOnly Property SelectedFilesListView_Size As Size
            Get
                Return Me._selectedFilesListView_size
            End Get
        End Property
        ' 
        ' Send As Email Button
        ' 
        Public ReadOnly Property SendAsEmailButton_Location As Point
            Get
                Return Me._sendAsEmailButton_location
            End Get
        End Property
        ' 
        ' Save As File Button
        ' 
        Public ReadOnly Property SaveAsFileButton_Location As Point
            Get
                Return Me._saveAsFileButton_location
            End Get
        End Property

    End Class
End Namespace
