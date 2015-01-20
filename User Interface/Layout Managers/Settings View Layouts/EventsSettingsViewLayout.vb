Namespace UI

    Public Class EventsSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared Shadows ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly ADD_NEW_ITEM_BUTTON_SIZE As Size = New Size(50, FIELDS_HEIGHT)


        ' Components Attributes

        ' Events Enabled Check Box
        Private _eventsEnabledCheckBox_location As Point
        Private _eventsEnabledCheckBox_size As Size

        ' New Event Name Field
        Private _newEventNameField_location As Point
        Private _newEventNameField_size As Size

        ' New Event Replace Field
        Private _newEventReplaceField_location As Point
        Private _newEventReplaceField_size As Size

        ' New Event Start Check Box
        Private _newEventStartCheckBox_location As Point
        Private _newEventStartCheckBox_size As Size

        ' New Event Stop Check Box
        Private _newEventStopCheckBox_location As Point
        Private _newEventStopCheckBox_size As Size

        ' Add New Event Button
        Private _addNewEventButton_location As Point
        Private _addNewEventButton_size As Size

        ' Events List View
        Private _eventsListView_location As Point
        Private _eventsListView_size As Size

        ' Delays Label
        Private _delaysLabel_location As Point
        Private _delaysLabel_size As Size

        ' Delays Justification Time Label
        Private _delaysJustificationTimeLabel_location As Point
        Private _delaysJustificationTimeLabel_size As Size

        ' Delays Justification Time Field
        Private _delaysJustificationTimeField_location As Point
        Private _delaysJustificationTimeField_size As Size

        ' New Delay Code Field
        Private _newDelayCodeField_location As Point
        Private _newDelayCodeField_size As Size

        ' New Delay Description Field
        Private _newDelayDescriptionField_location As Point
        Private _newDelayDescriptionField_size As Size

        ' New Delay Type Field
        Private _newDelayTypeField_location As Point
        Private _newDelayTypeField_size As Size

        ' Add New Delay Button
        Private _addNewDelayButton_location As Point
        Private _addNewDelayButton_size As Size

        ' Delays List View
        Private _delaysListView_location As Point
        Private _delaysListView_size As Size

        'Can't See Delay Codes Management Controls Label
        Private _cantSeeDelayCodesManagementControlsLabel_location As Point
        Private _cantSeeDelayCodesManagementControlsLabel_size As Size

        ' Attributes
        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' Events Enabled Check Box
            Me._eventsEnabledCheckBox_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            Me._eventsEnabledCheckBox_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)


            ' New Event Name Field
            Me._newEventNameField_location = New Point(LOCATION_START_X + SPACE_BETWEEN_CONTROLS_X, Me.EventsEnabledCheckBox_Location.Y + Me.EventsEnabledCheckBox_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._newEventNameField_size = New Size(200, FIELDS_HEIGHT)

            ' New Event Replace Field
            Me._newEventReplaceField_location = New Point(Me.NewEventNameField_Location.X + Me.NewEventNameField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewEventNameField_Location.Y)
            Me._newEventReplaceField_size = New Size(200, FIELDS_HEIGHT)

            ' New Event Start Check Box
            Me._newEventStartCheckBox_location = New Point(Me.NewEventReplaceField_Location.X + Me.NewEventReplaceField_Size.Width, Me.NewEventNameField_Location.Y - 10)
            Me._newEventStartCheckBox_size = New Size((Me.Width - 2 * LOCATION_START_X - NewEventNameField_Size.Width - NewEventReplaceField_Size.Width - ADD_NEW_ITEM_BUTTON_SIZE.Width - 2 * SPACE_BETWEEN_CONTROLS_X) / 2, FIELDS_HEIGHT + 10)

            ' New Event Stop Check Box
            Me._newEventStopCheckBox_location = New Point(Me.NewEventStartCheckBox_Location.X + Me.NewEventStartCheckBox_Size.Width, Me.NewEventNameField_Location.Y - 10)
            Me._newEventStopCheckBox_size = Me.NewEventStartCheckBox_Size

            ' Add New Event Button
            Me._addNewEventButton_location = New Point(Me.Width - ADD_NEW_ITEM_BUTTON_SIZE.Width - LOCATION_START_X, Me.NewEventNameField_Location.Y)
            Me._addNewEventButton_size = ADD_NEW_ITEM_BUTTON_SIZE

            ' Events List View
            Me._eventsListView_location = New Point(LOCATION_START_X, Me.NewEventNameField_Location.Y + Me.NewEventNameField_Size.Height)
            Me._eventsListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 180)


            ' Delays Justification Time Field
            Me._delaysJustificationTimeField_location = New Point(LOCATION_START_X, Me.EventsListView_Location.Y + Me.EventsListView_Size.Height + 2 * SPACE_BETWEEN_CONTROLS_Y)
            Me._delaysJustificationTimeField_size = New Size(100, FIELDS_HEIGHT)

            ' Delays Justification Time Label
            Me._delaysJustificationTimeLabel_location = New Point(Me.DelaysJustificationTimeField_Location.X + Me.DelaysJustificationTimeField_Size.Width + LayoutManager.SPACE_BETWEEN_CONTROLS_X, Me.DelaysJustificationTimeField_Location.Y)
            Me._delaysJustificationTimeLabel_size = New Size(Me.Width - 2 * LOCATION_START_X - DelaysJustificationTimeField_Size.Width - LayoutManager.SPACE_BETWEEN_CONTROLS_X, FIELDS_HEIGHT)


            ' New Delay Code Field
            Me._newDelayCodeField_location = New Point(LOCATION_START_X + SPACE_BETWEEN_CONTROLS_X, Me.DelaysJustificationTimeField_Location.Y + Me.DelaysJustificationTimeField_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._newDelayCodeField_size = New Size(70, FIELDS_HEIGHT)

            ' New Delay Description Field
            Me._newDelayDescriptionField_location = New Point(Me.NewDelayCodeField_Location.X + Me.NewDelayCodeField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewDelayCodeField_Location.Y)
            Me._newDelayDescriptionField_size = New Size(Me.Width - 2 * LOCATION_START_X - 70 - 180 - ADD_NEW_ITEM_BUTTON_SIZE.Width - 4 * SPACE_BETWEEN_CONTROLS_X, FIELDS_HEIGHT)

            ' New Delay Type Field
            Me._newDelayTypeField_location = New Point(Me.NewDelayDescriptionField_Location.X + Me.NewDelayDescriptionField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewDelayCodeField_Location.Y)
            Me._newDelayTypeField_size = New Size(180, FIELDS_HEIGHT)

            ' Add New Delay Button
            Me._addNewDelayButton_location = New Point(Me.Width - LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width, Me.NewDelayCodeField_Location.Y)
            Me._addNewDelayButton_size = ADD_NEW_ITEM_BUTTON_SIZE

            ' Delays List View
            Me._delaysListView_location = New Point(LOCATION_START_X, Me.NewDelayCodeField_Location.Y + Me.NewDelayCodeField_Size.Height)
            Me._delaysListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 320)

            'Can't See Delay Codes Management Controls Label
            Me._cantSeeDelayCodesManagementControlsLabel_location = New Point(LOCATION_START_X, Me.Height - FIELDS_HEIGHT)
            Me._cantSeeDelayCodesManagementControlsLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

        End Sub

        ' 
        ' Events Enabled Check Box
        ' 
        Public ReadOnly Property EventsEnabledCheckBox_Location As Point
            Get
                Return Me._eventsEnabledCheckBox_location
            End Get
        End Property
        Public ReadOnly Property EventsEnabledCheckBox_Size As Size
            Get
                Return Me._eventsEnabledCheckBox_size
            End Get
        End Property
        ' 
        ' New Event Name Field
        ' 
        Public ReadOnly Property NewEventNameField_Location As Point
            Get
                Return Me._newEventNameField_location
            End Get
        End Property
        Public ReadOnly Property NewEventNameField_Size As Size
            Get
                Return Me._newEventNameField_size
            End Get
        End Property
        ' 
        ' New Event Replace Field
        ' 
        Public ReadOnly Property NewEventReplaceField_Location As Point
            Get
                Return Me._newEventReplaceField_location
            End Get
        End Property
        Public ReadOnly Property NewEventReplaceField_Size As Size
            Get
                Return Me._newEventReplaceField_size
            End Get
        End Property
        ' 
        ' New Event Start Check Box
        ' 
        Public ReadOnly Property NewEventStartCheckBox_Location As Point
            Get
                Return Me._newEventStartCheckBox_location
            End Get
        End Property
        Public ReadOnly Property NewEventStartCheckBox_Size As Size
            Get
                Return Me._newEventStartCheckBox_size
            End Get
        End Property
        ' 
        ' New Event Stop Check Box
        ' 
        Public ReadOnly Property NewEventStopCheckBox_Location As Point
            Get
                Return Me._newEventStopCheckBox_location
            End Get
        End Property
        Public ReadOnly Property NewEventStopCheckBox_Size As Size
            Get
                Return Me._newEventStopCheckBox_size
            End Get
        End Property
        ' 
        ' Add New Event Button
        ' 
        Public ReadOnly Property AddNewEventButton_Location As Point
            Get
                Return Me._addNewEventButton_location
            End Get
        End Property
        Public ReadOnly Property AddNewEventButton_Size As Size
            Get
                Return Me._addNewEventButton_size
            End Get
        End Property
        ' 
        ' Events List View
        ' 
        Public ReadOnly Property EventsListView_Location As Point
            Get
                Return Me._eventsListView_location
            End Get
        End Property
        Public ReadOnly Property EventsListView_Size As Size
            Get
                Return Me._eventsListView_size
            End Get
        End Property
        ' 
        ' Delays Label
        ' 
        Public ReadOnly Property DelaysLabel_Location As Point
            Get
                Return Me._delaysLabel_location
            End Get
        End Property
        Public ReadOnly Property DelaysLabel_Size As Size
            Get
                Return Me._delaysLabel_size
            End Get
        End Property
        ' 
        ' Delays Justification Time Label
        ' 
        Public ReadOnly Property DelaysJustificationTimeLabel_Location As Point
            Get
                Return Me._delaysJustificationTimeLabel_location
            End Get
        End Property
        Public ReadOnly Property DelaysJustificationTimeLabel_Size As Size
            Get
                Return Me._delaysJustificationTimeLabel_size
            End Get
        End Property
        ' 
        ' Delays Justification Time Field
        ' 
        Public ReadOnly Property DelaysJustificationTimeField_Location As Point
            Get
                Return Me._delaysJustificationTimeField_location
            End Get
        End Property
        Public ReadOnly Property DelaysJustificationTimeField_Size As Size
            Get
                Return Me._delaysJustificationTimeField_size
            End Get
        End Property
        ' 
        ' New Delay Code Field
        ' 
        Public ReadOnly Property NewDelayCodeField_Location As Point
            Get
                Return Me._newDelayCodeField_location
            End Get
        End Property
        Public ReadOnly Property NewDelayCodeField_Size As Size
            Get
                Return Me._newDelayCodeField_size
            End Get
        End Property
        ' 
        ' New Delay Description Field
        ' 
        Public ReadOnly Property NewDelayDescriptionField_Location As Point
            Get
                Return Me._newDelayDescriptionField_location
            End Get
        End Property
        Public ReadOnly Property NewDelayDescriptionField_Size As Size
            Get
                Return Me._newDelayDescriptionField_size
            End Get
        End Property
        ' 
        ' New Delay Type Field
        ' 
        Public ReadOnly Property NewDelayTypeField_Location As Point
            Get
                Return Me._newDelayTypeField_location
            End Get
        End Property
        Public ReadOnly Property NewDelayTypeField_Size As Size
            Get
                Return Me._newDelayTypeField_size
            End Get
        End Property
        ' 
        ' Add New Delay Button
        ' 
        Public ReadOnly Property AddNewDelayButton_Location As Point
            Get
                Return Me._addNewDelayButton_location
            End Get
        End Property
        Public ReadOnly Property AddNewDelayButton_Size As Size
            Get
                Return Me._addNewDelayButton_size
            End Get
        End Property
        ' 
        ' Delays List View
        ' 
        Public ReadOnly Property DelaysListView_Location As Point
            Get
                Return Me._delaysListView_location
            End Get
        End Property
        Public ReadOnly Property DelaysListView_Size As Size
            Get
                Return Me._delaysListView_size
            End Get
        End Property

        'Can't See Delay Codes Management Controls Label
        Public ReadOnly Property CantSeeDelayCodesManagementControlsLabel_Location As Point
            Get
                Return Me._cantSeeDelayCodesManagementControlsLabel_location
            End Get
        End Property
        Public ReadOnly Property CantSeeDelayCodesManagementControlsLabel_Size As Size
            Get
                Return Me._cantSeeDelayCodesManagementControlsLabel_size
            End Get
        End Property
    End Class
End Namespace
