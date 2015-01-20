Namespace UI

    Public Class EmailSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly RECIPIENT_ADDRESS_FIELD_SIZE As Size = New Size(300, FIELDS_HEIGHT)
        Public Shared ReadOnly ADD_NEW_ITEM_BUTTON_SIZE As Size = New Size(50, FIELDS_HEIGHT)

        ' Components Attributes

        ' New Recipient Field
        Private _newRecipientField_location As Point
        Private _newRecipientField_size As Size

        ' Add New Recipient Button
        Private _addNewRecipientButton_location As Point
        Private _addNewRecipientButton_size As Size

        ' Recipients List View
        Private _recipientsListView_location As Point
        Private _recipientsListView_size As Size

        ' New Default Recipient Field
        Private _newDefaultRecipientField_location As Point
        Private _newDefaultRecipientField_size As Size

        ' Add New Default Recipient Button
        Private _addNewDefaultRecipientButton_location As Point
        Private _addNewDefaultRecipientButton_size As Size

        ' Default Recipients List View
        Private _defaultRecipientsListView_location As Point
        Private _defaultRecipientsListView_size As Size

        ' Credentials Label
        Private _credentialsLabel_location As Point
        Private _credentialsLabel_size As Size

        ' Credentials Field
        Private _credentialsField_location As Point
        Private _credentialsField_size As Size

        ' Password Label
        Private _passwordLabel_location As Point
        Private _passwordLabel_size As Size

        ' Password Field
        Private _passwordField_location As Point
        Private _passwordField_size As Size

        ' Host Label
        Private _hostLabel_location As Point
        Private _hostLabel_size As Size

        ' Host Field
        Private _hostField_location As Point
        Private _hostField_size As Size

        ' Port Label
        Private _portLabel_location As Point
        Private _portLabel_size As Size

        ' Port Field
        Private _portField_location As Point
        Private _portField_size As Size

        ' Cant See Protected Email Settings Label
        Private _cantSeeProtectedEmailSettingsLabel_location As Point
        Private _cantSeeProtectedEmailSettingsLabel_size As Size

        ' Attributes
        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' New Recipient Field
            Me._newRecipientField_location = New Point(LOCATION_START_X, LOCATION_START_Y)

            ' Add New Recipient Button
            Me._addNewRecipientButton_location = New Point(Me.Width - LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width, LOCATION_START_Y)

            ' Recipients List View
            Me._recipientsListView_location = New Point(LOCATION_START_X, Me.NewRecipientField_Location.Y + Me.NewRecipientField_Size.Height)
            Me._recipientsListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 180)

            ' New Default Recipient Field
            Me._newDefaultRecipientField_location = New Point(LOCATION_START_X, Me.RecipientsListView_Location.Y + Me.RecipientsListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)

            ' Add New Default Recipient Button
            Me._addNewDefaultRecipientButton_location = New Point(Me.Width - LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width, Me.RecipientsListView_Location.Y + Me.RecipientsListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)

            ' Default Recipients List View
            Me._defaultRecipientsListView_location = New Point(LOCATION_START_X, Me.NewDefaultRecipientField_Location.Y + Me.NewDefaultRecipientField_Size.Height)
            Me._defaultRecipientsListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 150)

            ' Credentials Label
            Me._credentialsLabel_location = New Point(LOCATION_START_X, Me.DefaultRecipientsListView_Location.Y + Me.DefaultRecipientsListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._credentialsLabel_size = New Size((Me.Width - 2 * LOCATION_START_X - SPACE_BETWEEN_CONTROLS_X) / 2, FIELDS_HEIGHT)

            ' Credentials Field
            Me._credentialsField_location = New Point(LOCATION_START_X, Me.CredentialsLabel_Location.Y + Me.CredentialsLabel_Size.Height)
            Me._credentialsField_size = Me.CredentialsLabel_Size
           
            ' Password Label
            Me._passwordLabel_location = New Point(LOCATION_START_X, Me.CredentialsField_Location.Y + Me.CredentialsField_Size.Height)
            Me._passwordLabel_size = New Size((Me.Width - 2 * LOCATION_START_X - SPACE_BETWEEN_CONTROLS_X) / 2, FIELDS_HEIGHT)

            ' Password Field
            Me._passwordField_location = New Point(LOCATION_START_X, Me.PasswordLabel_Location.Y + Me.PasswordLabel_Size.Height)
            Me._passwordField_size = Me.PasswordLabel_Size
            
            ' Host Label
            Me._hostLabel_location = New Point(Me.CredentialsLabel_Location.X + Me.CredentialsLabel_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.CredentialsLabel_Location.Y)
            Me._hostLabel_size = New Size((Me.Width - 2 * LOCATION_START_X - SPACE_BETWEEN_CONTROLS_X) / 2, FIELDS_HEIGHT)

            ' Host Field
            Me._hostField_location = New Point(Me.HostLabel_Location.X, Me.HostLabel_Location.Y + Me.HostLabel_Size.Height)
            Me._hostField_size = Me.HostLabel_Size
            
            ' Port Label
            Me._portLabel_location = New Point(Me.PasswordLabel_Location.X + Me.PasswordLabel_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.PasswordLabel_Location.Y)
            Me._portLabel_size = New Size(120, FIELDS_HEIGHT)

            ' Port Field
            Me._portField_location = New Point(Me.PortLabel_Location.X, Me.PortLabel_Location.Y + Me.PortLabel_Size.Height)
            Me._portField_size = Me.PortLabel_Size
            
            ' Cant See Protected Email Settings Label
            Me._cantSeeProtectedEmailSettingsLabel_location = New Point(LOCATION_START_X, Me.Height - FIELDS_HEIGHT)
            Me._cantSeeProtectedEmailSettingsLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

        End Sub

        ' 
        ' New Recipient Field
        ' 
        Public ReadOnly Property NewRecipientField_Location As Point
            Get
                Return Me._newRecipientField_location
            End Get
        End Property
        Public ReadOnly Property NewRecipientField_Size As Size
            Get
                Return RECIPIENT_ADDRESS_FIELD_SIZE
            End Get
        End Property
        ' 
        ' Add New Recipient Button
        ' 
        Public ReadOnly Property AddNewRecipientButton_Location As Point
            Get
                Return Me._addNewRecipientButton_location
            End Get
        End Property
        Public ReadOnly Property AddNewRecipientButton_Size As Size
            Get
                Return ADD_NEW_ITEM_BUTTON_SIZE
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
        ' New Default Recipient Field
        ' 
        Public ReadOnly Property NewDefaultRecipientField_Location As Point
            Get
                Return Me._newDefaultRecipientField_location
            End Get
        End Property
        Public ReadOnly Property NewDefaultRecipientField_Size As Size
            Get
                Return RECIPIENT_ADDRESS_FIELD_SIZE
            End Get
        End Property
        ' 
        ' Add New Default Recipient Button
        ' 
        Public ReadOnly Property AddNewDefaultRecipientButton_Location As Point
            Get
                Return Me._addNewDefaultRecipientButton_location
            End Get
        End Property
        Public ReadOnly Property AddNewDefaultRecipientButton_Size As Size
            Get
                Return ADD_NEW_ITEM_BUTTON_SIZE
            End Get
        End Property
        ' 
        ' Default Recipients List View
        ' 
        Public ReadOnly Property DefaultRecipientsListView_Location As Point
            Get
                Return Me._defaultRecipientsListView_location
            End Get
        End Property
        Public ReadOnly Property DefaultRecipientsListView_Size As Size
            Get
                Return Me._defaultRecipientsListView_size
            End Get
        End Property
        ' 
        ' Credentials Label
        ' 
        Public ReadOnly Property CredentialsLabel_Location As Point
            Get
                Return Me._credentialsLabel_location
            End Get
        End Property
        Public ReadOnly Property CredentialsLabel_Size As Size
            Get
                Return Me._credentialsLabel_size
            End Get
        End Property
        ' 
        ' Credentials Field
        ' 
        Public ReadOnly Property CredentialsField_Location As Point
            Get
                Return Me._credentialsField_location
            End Get
        End Property
        Public ReadOnly Property CredentialsField_Size As Size
            Get
                Return Me._credentialsField_size
            End Get
        End Property
        ' 
        ' Password Label
        ' 
        Public ReadOnly Property PasswordLabel_Location As Point
            Get
                Return Me._passwordLabel_location
            End Get
        End Property
        Public ReadOnly Property PasswordLabel_Size As Size
            Get
                Return Me._passwordLabel_size
            End Get
        End Property
        ' 
        ' Password Field
        ' 
        Public ReadOnly Property PasswordField_Location As Point
            Get
                Return Me._passwordField_location
            End Get
        End Property
        Public ReadOnly Property PasswordField_Size As Size
            Get
                Return Me._passwordField_size
            End Get
        End Property
        ' 
        ' Host Label
        ' 
        Public ReadOnly Property HostLabel_Location As Point
            Get
                Return Me._hostLabel_location
            End Get
        End Property
        Public ReadOnly Property HostLabel_Size As Size
            Get
                Return Me._hostLabel_size
            End Get
        End Property
        ' 
        ' Host Field
        ' 
        Public ReadOnly Property HostField_Location As Point
            Get
                Return Me._hostField_location
            End Get
        End Property
        Public ReadOnly Property HostField_Size As Size
            Get
                Return Me._hostField_size
            End Get
        End Property
        ' 
        ' Port Label
        ' 
        Public ReadOnly Property PortLabel_Location As Point
            Get
                Return Me._portLabel_location
            End Get
        End Property
        Public ReadOnly Property PortLabel_Size As Size
            Get
                Return Me._portLabel_size
            End Get
        End Property
        ' 
        ' Port Field
        ' 
        Public ReadOnly Property PortField_Location As Point
            Get
                Return Me._portField_location
            End Get
        End Property
        Public ReadOnly Property PortField_Size As Size
            Get
                Return Me._portField_size
            End Get
        End Property
        ' 
        ' Cant See Protected Email Settings Label
        ' 
        Public ReadOnly Property CantSeeProtectedEmailSettingsLabel_Location As Point
            Get
                Return Me._cantSeeProtectedEmailSettingsLabel_location
            End Get
        End Property
        Public ReadOnly Property CantSeeProtectedEmailSettingsLabel_Size As Size
            Get
                Return Me._cantSeeProtectedEmailSettingsLabel_size
            End Get
        End Property
        
    End Class
End Namespace
