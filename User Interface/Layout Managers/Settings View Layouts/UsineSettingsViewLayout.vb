Namespace UI
    Public Class UsineSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(315, 330)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly ID_LABEL_SIZE As Size = New Size(100, FIELDS_HEIGHT)
        Public Shared ReadOnly ID_FIELD_SIZE As Size = New Size(100, FIELDS_HEIGHT)

        Public Shared ReadOnly TYPE_LABELS_SIZE As Size = New Size(200, FIELDS_HEIGHT)
        Public Shared ReadOnly CHECK_BOX_SIZE As Size = New Size(20, 26)
        Public Shared ReadOnly CHECK_BOX_LOCATION_X As Integer = 220

        Public Shared ReadOnly SPACE_BETWEEN_CHECKBOXES_Y As Integer = 5

        Public Shared ReadOnly TYPES_CONTAINER_HEIGHT As Integer = 4 * FIELDS_HEIGHT + 5 * SPACE_BETWEEN_CHECKBOXES_Y

        Public Shared ReadOnly SPACE_BETWEEN_OPERATOR_FIELDS_X As Integer = OperatorListItem.SPACE_BETWEEN_CONTROLS_X
        Public Shared ReadOnly ADD_NEW_OPERATOR_BUTTONS_SIZE As Size = New Size(40, FIELDS_HEIGHT)

        Public Shared ReadOnly FUEL_UNIT_FIELDS_SIZE As Size = New Size(65, FIELDS_HEIGHT)

        ' Components Attributes
        Private _idLabel_size As Size
        Private _idLabel_location As Point

        Private _idField_size As Size
        Private _idField_location As Point

        Private _nameLabel_size As Size
        Private _nameLabel_location As Point

        Private _nameField_size As Size
        Private _nameField_location As Point

        Private _typesLabel_size As Size
        Private _typesLabel_location As Point

        Private _typesContainer_size As Size
        Private _typesContainer_location As Point

        Private _hybridLabel_size As Size
        Private _hybridLabel_location As Point
        Private _hybridCheckBox_location As Point

        Private _mdbLabel_size As Size
        Private _mdbLabel_location As Point
        Private _mdbCheckBox_location As Point

        Private _logLabel_size As Size
        Private _logLabel_location As Point
        Private _logCheckBox_location As Point

        Private _csvLabel_size As Size
        Private _csvLabel_location As Point
        Private _csvCheckBox_location As Point

        Private _operatorsList_size As Size
        Private _operatorsList_location As Point

        Private _newOperatorFirstNameField_size As Size
        Private _newOperatorFirstNameField_location As Point

        Private _newOperatorLastNameField_size As Size
        Private _newOperatorLastNameField_location As Point

        Private _addNewOperatorButton_size As Size
        Private _addNewOperatorButton_location As Point

        Private _operatorListView_size As Size
        Private _operatorListView_location As Point

        Private _fuel1NameLabel_size As Size
        Private _fuel1NameLabel_location As Point

        Private _fuel1NameField_size As Size
        Private _fuel1NameField_location As Point

        Private _fuel1UnitLabel_size As Size
        Private _fuel1UnitLabel_location As Point

        Private _fuel1UnitField_size As Size
        Private _fuel1UnitField_location As Point

        Private _fuel2NameLabel_size As Size
        Private _fuel2NameLabel_location As Point

        Private _fuel2NameField_size As Size
        Private _fuel2NameField_location As Point

        Private _fuel2UnitLabel_size As Size
        Private _fuel2UnitLabel_location As Point

        Private _fuel2UnitField_size As Size
        Private _fuel2UnitField_location As Point

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' At location start
            Me._nameLabel_location = New Point(LOCATION_START_X, LOCATION_START_Y)

            ' Under nameLabel
            Me._nameField_location = New Point(Me.NameLabel_Location.X, Me.NameLabel_Location.Y + FIELDS_HEIGHT)
            Me._nameField_size = New Size(Me.Width - 2 * LOCATION_START_X - Me.IDField_Size.Width - 10, FIELDS_HEIGHT)
            Me._nameLabel_size = Me.NameField_Size

            ' Next to name field but at nameLabel height
            Me._idLabel_location = New Point(Me.NameField_Location.X + Me.NameField_Size.Width + 10, Me.NameLabel_Location.Y)

            ' Under idLabel
            Me._idField_location = New Point(Me.IDLabel_Location.X, Me.IDLabel_Location.Y + Me.IDLabel_Size.Height)

            ' Under nameField with a little bit of spacing
            Me._typesLabel_location = New Point(LOCATION_START_X, Me.NameField_Location.Y + Me.NameField_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._typesLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

            ' Under typesLabel
            Me._typesContainer_location = New Point(Me.TypesLabel_Location.X, Me.TypesLabel_Location.Y + Me.TypesLabel_Size.Height)
            Me._typesContainer_size = New Size(Me.TypesLabel_Size.Width, TYPES_CONTAINER_HEIGHT)

            ' Hybrid Type - Inside type container
            Me._hybridCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, SPACE_BETWEEN_CHECKBOXES_Y)
            Me._hybridLabel_location = New Point(Me.HybridCheckBox_Location.X + CHECK_BOX_SIZE.Width, Me.HybridCheckBox_Location.Y)

            ' LOG Type - Inside type container
            Me._logCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.HybridCheckBox_Location.Y + FIELDS_HEIGHT + SPACE_BETWEEN_CHECKBOXES_Y)
            Me._logLabel_location = New Point(Me.LOGCheckBox_Location.X + CHECK_BOX_SIZE.Width, Me.LOGCheckBox_Location.Y)

            ' CSV Type - Inside type container
            Me._csvCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.LOGCheckBox_Location.Y + FIELDS_HEIGHT + SPACE_BETWEEN_CHECKBOXES_Y)
            Me._csvLabel_location = New Point(Me.CSVCheckBox_Location.X + CHECK_BOX_SIZE.Width, Me.CSVCheckBox_Location.Y)

            ' MDB Type - Inside type container
            Me._mdbCheckBox_location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.CSVCheckBox_Location.Y + FIELDS_HEIGHT + SPACE_BETWEEN_CHECKBOXES_Y)
            Me._mdbLabel_location = New Point(Me.MDBCheckBox_Location.X + CHECK_BOX_SIZE.Width, Me.MDBCheckBox_Location.Y)

            ' New Operator First Name Field
            Me._newOperatorFirstNameField_location = New Point(LOCATION_START_X + SPACE_BETWEEN_OPERATOR_FIELDS_X, Me.TypesContainer_Location.Y + Me.TypesContainer_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._newOperatorFirstNameField_size = New Size((Me.TypesContainer_Size.Width - 4 * SPACE_BETWEEN_OPERATOR_FIELDS_X - ADD_NEW_OPERATOR_BUTTONS_SIZE.Width) / 2, FIELDS_HEIGHT)

            ' New Operator Last Name Field
            Me._newOperatorLastNameField_location = New Point(Me.NewOperatorFirstNameField_Location.X + Me.NewOperatorFirstNameField_Size.Width + SPACE_BETWEEN_OPERATOR_FIELDS_X, Me._newOperatorFirstNameField_location.Y)
            Me._newOperatorLastNameField_size = New Size(Me.NewOperatorFirstNameField_Size.Width, FIELDS_HEIGHT)

            ' Add New Operator Button
            Me._addNewOperatorButton_location = New Point(Me.NewOperatorLastNameField_Location.X + Me.NewOperatorLastNameField_Size.Width + SPACE_BETWEEN_OPERATOR_FIELDS_X, Me._newOperatorFirstNameField_location.Y)
            Me._addNewOperatorButton_size = ADD_NEW_OPERATOR_BUTTONS_SIZE

            ' Operator List View
            Me._operatorListView_location = New Point(LOCATION_START_X, Me.NewOperatorFirstNameField_Location.Y + Me.NewOperatorFirstNameField_Size.Height)
            Me._operatorListView_size = New Size(Me.Width - 2 * LOCATION_START_X, Me.Height - Me.NewOperatorLastNameField_Location.Y - Me.NewOperatorFirstNameField_Size.Height - LOCATION_START_Y - SPACE_BETWEEN_CONTROLS_Y - 2 * FIELDS_HEIGHT)

            Dim fuelFieldsSize As New Size((Me.Width - 2 * LOCATION_START_X - 2 * FUEL_UNIT_FIELDS_SIZE.Width - 10 - SPACE_BETWEEN_CONTROLS_X) / 2, FIELDS_HEIGHT)

            ' Fuel 1 Name Label
            Me._fuel1NameLabel_location = New Point(LOCATION_START_X, Me.OperatorListView_Location.Y + Me.OperatorListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._fuel1NameLabel_size = fuelFieldsSize

            ' Fuel 1 Name Field
            Me._fuel1NameField_location = New Point(LOCATION_START_X, Me.Fuel1NameLabel_Location.Y + Me.Fuel1NameLabel_Size.Height)
            Me._fuel1NameField_size = fuelFieldsSize

            ' Fuel 1 Unit Label
            Me._fuel1UnitLabel_location = New Point(Me.Fuel1NameLabel_Location.X + Me.Fuel1NameLabel_Size.Width + 5, Me.Fuel1NameLabel_Location.Y)
            Me._fuel1UnitLabel_size = FUEL_UNIT_FIELDS_SIZE

            ' Fuel 1 Unit Field
            Me._fuel1UnitField_location = New Point(Me.Fuel1UnitLabel_Location.X, Me.Fuel1UnitLabel_Location.Y + Me.Fuel1UnitLabel_Size.Height)
            Me._fuel1UnitField_size = FUEL_UNIT_FIELDS_SIZE

            ' Fuel 2 Name Label
            Me._fuel2NameLabel_location = New Point(Me.Fuel1UnitLabel_Location.X + Me.Fuel1UnitLabel_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.Fuel1UnitLabel_Location.Y)
            Me._fuel2NameLabel_size = fuelFieldsSize

            ' Fuel 2 Name Field
            Me._fuel2NameField_location = New Point(Me.Fuel2NameLabel_Location.X, Me.Fuel2NameLabel_Location.Y + Me.Fuel2NameLabel_Size.Height)
            Me._fuel2NameField_size = fuelFieldsSize

            ' Fuel 2 Unit Label
            Me._fuel2UnitLabel_location = New Point(Me.Fuel2NameLabel_Location.X + Me.Fuel2NameLabel_Size.Width + 5, Me.Fuel2NameLabel_Location.Y)
            Me._fuel2UnitLabel_size = FUEL_UNIT_FIELDS_SIZE

            ' Fuel 2 Unit Field
            Me._fuel2UnitField_location = New Point(Me.Fuel2UnitLabel_Location.X, Me.Fuel2UnitLabel_Location.Y + Me.Fuel2UnitLabel_Size.Height)
            Me._fuel2UnitField_size = FUEL_UNIT_FIELDS_SIZE
        End Sub

        '
        ' ID Label
        '
        Public ReadOnly Property IDLabel_Size As Size
            Get
                Return ID_LABEL_SIZE
            End Get
        End Property
        Public ReadOnly Property IDLabel_Location As Point
            Get
                Return _idLabel_location
            End Get
        End Property
        '
        ' ID Field
        '
        Public ReadOnly Property IDField_Size As Size
            Get
                Return ID_FIELD_SIZE
            End Get
        End Property
        Public ReadOnly Property IDField_Location As Point
            Get
                Return _idField_location
            End Get
        End Property
        '
        ' Name Label
        '
        Public ReadOnly Property NameLabel_Size As Size
            Get
                Return _nameLabel_size
            End Get
        End Property
        Public ReadOnly Property NameLabel_Location As Point
            Get
                Return _nameLabel_location
            End Get
        End Property
        '
        ' Name Field
        '
        Public ReadOnly Property NameField_Size As Size
            Get
                Return _nameField_size
            End Get
        End Property
        Public ReadOnly Property NameField_Location As Point
            Get
                Return _nameField_location
            End Get
        End Property
        '
        ' Types Label
        '
        Public ReadOnly Property TypesLabel_Size As Size
            Get
                Return _typesLabel_size
            End Get
        End Property
        Public ReadOnly Property TypesLabel_Location As Point
            Get
                Return _typesLabel_location
            End Get
        End Property
        '
        ' Types Container
        '
        Public ReadOnly Property TypesContainer_Size As Size
            Get
                Return _typesContainer_size
            End Get
        End Property
        Public ReadOnly Property TypesContainer_Location As Point
            Get
                Return _typesContainer_location
            End Get
        End Property
        '
        ' Hybrid Type
        '
        Public ReadOnly Property HybridLabel_Size As Size
            Get
                Return TYPE_LABELS_SIZE
            End Get
        End Property
        Public ReadOnly Property HybridLabel_Location As Point
            Get
                Return _hybridLabel_location
            End Get
        End Property
        Public ReadOnly Property HybridCheckBox_Location As Point
            Get
                Return _hybridCheckBox_location
            End Get
        End Property
        '
        ' LOG Type
        '
        Public ReadOnly Property LOGLabel_Size As Size
            Get
                Return TYPE_LABELS_SIZE
            End Get
        End Property
        Public ReadOnly Property LOGLabel_Location As Point
            Get
                Return _logLabel_location
            End Get
        End Property
        Public ReadOnly Property LOGCheckBox_Location As Point
            Get
                Return _logCheckBox_location
            End Get
        End Property
        '
        ' CSV Type
        '
        Public ReadOnly Property CSVLabel_Size As Size
            Get
                Return TYPE_LABELS_SIZE
            End Get
        End Property
        Public ReadOnly Property CSVLabel_Location As Point
            Get
                Return _csvLabel_location
            End Get
        End Property
        Public ReadOnly Property CSVCheckBox_Location As Point
            Get
                Return _csvCheckBox_location
            End Get
        End Property
        '
        ' MDB Type
        '
        Public ReadOnly Property MDBLabel_Size As Size
            Get
                Return TYPE_LABELS_SIZE
            End Get
        End Property
        Public ReadOnly Property MDBLabel_Location As Point
            Get
                Return _mdbLabel_location
            End Get
        End Property
        Public ReadOnly Property MDBCheckBox_Location As Point
            Get
                Return _mdbCheckBox_location
            End Get
        End Property
        '
        ' New Operator First Name Field
        '
        Public ReadOnly Property NewOperatorFirstNameField_Size As Size
            Get
                Return _newOperatorFirstNameField_size
            End Get
        End Property
        Public ReadOnly Property NewOperatorFirstNameField_Location As Point
            Get
                Return _newOperatorFirstNameField_location
            End Get
        End Property
        '
        ' New Operator Last Name Field
        '
        Public ReadOnly Property NewOperatorLastNameField_Size As Size
            Get
                Return _newOperatorLastNameField_size
            End Get
        End Property
        Public ReadOnly Property NewOperatorLastNameField_Location As Point
            Get
                Return _newOperatorLastNameField_location
            End Get
        End Property
        '
        ' Add New Operator Button
        '
        Public ReadOnly Property AddNewOperatorButton_Size As Size
            Get
                Return _addNewOperatorButton_size
            End Get
        End Property
        Public ReadOnly Property AddNewOperatorButton_Location As Point
            Get
                Return _addNewOperatorButton_location
            End Get
        End Property
        '
        ' Operator List View
        '
        Public ReadOnly Property OperatorListView_Size As Size
            Get
                Return _operatorListView_size
            End Get
        End Property
        Public ReadOnly Property OperatorListView_Location As Point
            Get
                Return _operatorListView_location
            End Get
        End Property
        '
        ' Fuel 1 Name Label
        '
        Public ReadOnly Property Fuel1NameLabel_Size As Size
            Get
                Return _fuel1NameLabel_size
            End Get
        End Property
        Public ReadOnly Property Fuel1NameLabel_Location As Point
            Get
                Return _fuel1NameLabel_location
            End Get
        End Property
        '
        ' Fuel 1 Name Field
        '
        Public ReadOnly Property Fuel1NameField_Size As Size
            Get
                Return _fuel1NameField_size
            End Get
        End Property
        Public ReadOnly Property Fuel1NameField_Location As Point
            Get
                Return _fuel1NameField_location
            End Get
        End Property
        '
        ' Fuel 1 Unit Label
        '
        Public ReadOnly Property Fuel1UnitLabel_Size As Size
            Get
                Return _fuel1UnitLabel_size
            End Get
        End Property
        Public ReadOnly Property Fuel1UnitLabel_Location As Point
            Get
                Return _fuel1UnitLabel_location
            End Get
        End Property
        '
        ' Fuel 1 Unit Field
        '
        Public ReadOnly Property Fuel1UnitField_Size As Size
            Get
                Return _fuel1UnitField_size
            End Get
        End Property
        Public ReadOnly Property Fuel1UnitField_Location As Point
            Get
                Return _fuel1UnitField_location
            End Get
        End Property
        '
        ' Fuel 2 Name Label
        '
        Public ReadOnly Property Fuel2NameLabel_Size As Size
            Get
                Return _fuel2NameLabel_size
            End Get
        End Property
        Public ReadOnly Property Fuel2NameLabel_Location As Point
            Get
                Return _fuel2NameLabel_location
            End Get
        End Property
        '
        ' Fuel 2 Name Field
        '
        Public ReadOnly Property Fuel2NameField_Size As Size
            Get
                Return _fuel2NameField_size
            End Get
        End Property
        Public ReadOnly Property Fuel2NameField_Location As Point
            Get
                Return _fuel2NameField_location
            End Get
        End Property
        '
        ' Fuel 2 Unit Label
        '
        Public ReadOnly Property Fuel2UnitLabel_Size As Size
            Get
                Return _fuel2UnitLabel_size
            End Get
        End Property
        Public ReadOnly Property Fuel2UnitLabel_Location As Point
            Get
                Return _fuel2UnitLabel_location
            End Get
        End Property
        '
        ' Fuel 2 Unit Field
        '
        Public ReadOnly Property Fuel2UnitField_Size As Size
            Get
                Return _fuel2UnitField_size
            End Get
        End Property
        Public ReadOnly Property Fuel2UnitField_Location As Point
            Get
                Return _fuel2UnitField_location
            End Get
        End Property
    End Class
End Namespace