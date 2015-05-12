Namespace UI

    Public Class EmailExportationViewLayout
        Inherits LayoutManager

        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(400, 300)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly OPERATORS_LABEL_SIZE As Size = New Size(100, FIELDS_HEIGHT)

        Public Shared ReadOnly SEND_BUTTON_SIZE As Size = New Size(150, CONTROL_BUTTONS_HEIGHT)

        ' Components Attributes

        ' Attachment Information Label
        Private _attachmentInformationLabel_location As Point
        Private _attachmentInformationLabel_size As Size

        ' Operators Label
        Private _operatorsLabel_location As Point
        Private _operatorsLabel_size As Size

        ' Operators Combo Box
        Private _operatorsComboBox_location As Point
        Private _operatorsComboBox_size As Size

        ' Recipients List View
        Private _recipientsListView_location As Point
        Private _recipientsListView_size As Size

        ' KA01_Comments Label
        Private _commentsLabel_location As Point
        Private _commentsLabel_size As Size

        ' KA01_Comments Text Field
        Private _commentsTextField_location As Point
        Private _commentsTextField_size As Size

        ' Buttons Panel
        Private _buttonsPanel_location As Point
        Private _buttonsPanel_size As Size

        ' Back Button
        Private _backButton_location As Point
        Private _backButton_size As Size

        ' Send Button
        Private _sendButton_location As Point
        Private _sendButton_size As Size


        ' Attributes
        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' Attachment Information Label
            Me._attachmentInformationLabel_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            Me._attachmentInformationLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

            ' Operators Label
            Me._operatorsLabel_location = New Point(LOCATION_START_X, Me.AttachmentInformationLabel_Location.Y + Me.AttachmentInformationLabel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)

            ' Operators Combo Box
            Me._operatorsComboBox_location = New Point(Me.OperatorsLabel_Location.X + Me.OperatorsLabel_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.OperatorsLabel_Location.Y)
            Me._operatorsComboBox_size = New Size(Me.Width - 2 * LOCATION_START_X - SPACE_BETWEEN_CONTROLS_X - Me.OperatorsLabel_Size.Width, FIELDS_HEIGHT)

            ' Recipients List View
            Me._recipientsListView_location = New Point(LOCATION_START_X, Me.OperatorsLabel_Location.Y + Me.OperatorsLabel_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._recipientsListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 180)

            ' KA01_Comments Label
            Me._commentsLabel_location = New Point(LOCATION_START_X, Me.RecipientsListView_Location.Y + Me.RecipientsListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._commentsLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

            ' KA01_Comments Text Field
            Me._commentsTextField_location = New Point(LOCATION_START_X, Me.CommentsLabel_Location.Y + Me.CommentsLabel_Size.Height)
            Me._commentsTextField_size = New Size(Me.Width - 2 * LOCATION_START_X, Me.Height - Me.CommentsTextField_Location.Y - BUTTONS_PANEL_HEIGHT - SPACE_BETWEEN_CONTROLS_Y)

            ' Buttons Panel
            Me._buttonsPanel_location = New Point(0, Me.Height - BUTTONS_PANEL_HEIGHT)
            Me._buttonsPanel_size = New Size(Me.Width, BUTTONS_PANEL_HEIGHT)

            ' Back Button
            Me._backButton_location = New Point(LOCATION_START_X, BUTTONS_PANEL_LOCATION_START_Y)

            ' Send Button
            Me._sendButton_location = New Point(Me.Width - LOCATION_START_X - SEND_BUTTON_SIZE.Width, BUTTONS_PANEL_LOCATION_START_Y)

        End Sub

        '
        ' Attachment Information Label
        '
        Public ReadOnly Property AttachmentInformationLabel_Location As Point
            Get
                Return Me._attachmentInformationLabel_location
            End Get
        End Property
        Public ReadOnly Property AttachmentInformationLabel_Size As Size
            Get
                Return Me._attachmentInformationLabel_size
            End Get
        End Property
        ' 
        ' Operators Label
        ' 
        Public ReadOnly Property OperatorsLabel_Location As Point
            Get
                Return Me._operatorsLabel_location
            End Get
        End Property
        Public ReadOnly Property OperatorsLabel_Size As Size
            Get
                Return OPERATORS_LABEL_SIZE
            End Get
        End Property
        ' 
        ' Operators Combo Box
        ' 
        Public ReadOnly Property OperatorsComboBox_Location As Point
            Get
                Return Me._operatorsComboBox_location
            End Get
        End Property
        Public ReadOnly Property OperatorsComboBox_Size As Size
            Get
                Return Me._operatorsComboBox_size
            End Get
        End Property
        ' 
        ' Recipients List View
        ' 
        Public ReadOnly Property RecipientsListView_Location As Point
            Get
                Return Me._recipientsListView_location
            End Get
        End Property
        Public ReadOnly Property RecipientsListView_Size As Size
            Get
                Return Me._recipientsListView_size
            End Get
        End Property
        ' 
        ' KA01_Comments Label
        ' 
        Public ReadOnly Property CommentsLabel_Location As Point
            Get
                Return Me._commentsLabel_location
            End Get
        End Property
        Public ReadOnly Property CommentsLabel_Size As Size
            Get
                Return Me._commentsLabel_size
            End Get
        End Property
        ' 
        ' KA01_Comments Text Field
        ' 
        Public ReadOnly Property CommentsTextField_Location As Point
            Get
                Return Me._commentsTextField_location
            End Get
        End Property
        Public ReadOnly Property CommentsTextField_Size As Size
            Get
                Return Me._commentsTextField_size
            End Get
        End Property
        ' 
        ' Buttons Panel
        ' 
        Public ReadOnly Property ButtonsPanel_Location As Point
            Get
                Return Me._buttonsPanel_location
            End Get
        End Property
        Public ReadOnly Property ButtonsPanel_Size As Size
            Get
                Return Me._buttonsPanel_size
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
        ' 
        ' Send Button
        ' 
        Public ReadOnly Property SendButton_Location As Point
            Get
                Return Me._sendButton_location
            End Get
        End Property
        Public ReadOnly Property SendButton_Size As Size
            Get
                Return SEND_BUTTON_SIZE
            End Get
        End Property

    End Class
End Namespace
