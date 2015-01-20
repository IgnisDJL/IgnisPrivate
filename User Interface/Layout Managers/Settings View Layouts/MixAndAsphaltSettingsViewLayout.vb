
Namespace UI

    Public Class MixAndAsphaltSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared Shadows ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly ADD_NEW_ITEM_BUTTON_SIZE As Size = New Size(50, FIELDS_HEIGHT)

        ' Components Attributes
        Private _newRecipeFormulaField_location As Point
        Private _newRecipeFormulaField_size As Size

        Private _newRecipeMixField_location As Point
        Private _newRecipeMixField_size As Size

        Private _newRecipeRAPField_location As Point
        Private _newRecipeRAPField_size As Size

        Private _newRecipeACPercentageField_location As Point
        Private _newRecipeACPercentageField_size As Size

        Private _addNewRecipeButton_location As Point
        Private _addNewRecipeButton_size As Size

        Private _recipesListView_location As Point
        Private _recipesListView_size As Size

        Private _unknownRecipesListView_location As Point
        Private _unknownRecipesListView_size As Point


        Private _newAsphaltTankNameField_location As Point
        Private _newAsphaltTankNameField_size As Size

        Private _newAsphaltNameField_location As Point
        Private _newAsphaltNameField_size As Size

        Private _newMixTargetTemperatureField_location As Point
        Private _newMixTargetTemperatureField_size As Size

        Private _addNewTankInfoButton_location As Point
        Private _addNewTankInfoButton_size As Size

        Private _tankInfoListView_location As Point
        Private _tankInfoListView_size As Size

        Private _unknownTankInfoListView_location As Point
        Private _unknownTankInfoListView_size As Point


        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' New Recipe Formula Field
            Me._newRecipeFormulaField_location = New Point(LOCATION_START_X + SPACE_BETWEEN_CONTROLS_X, LOCATION_START_Y)
            Me._newRecipeFormulaField_size = New Size((Me.Width - 2 * LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) * 2 / 6, FIELDS_HEIGHT)

            ' New Recipe Mix Field
            Me._newRecipeMixField_location = New Point(Me.NewRecipeFormulaField_Location.X + Me.NewRecipeFormulaField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewRecipeFormulaField_Location.Y)
            Me._newRecipeMixField_size = Me.NewRecipeFormulaField_Size

            ' New Recipe RAP Field
            Me._newRecipeRAPField_location = New Point(Me.NewRecipeMixField_Location.X + Me.NewRecipeMixField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewRecipeFormulaField_Location.Y)
            Me._newRecipeRAPField_size = New Size((Me.Width - 2 * LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) * 1 / 6, FIELDS_HEIGHT)

            ' New Recipe AC Percentage Field
            Me._newRecipeACPercentageField_location = New Point(Me.NewRecipeRAPField_Location.X + Me.NewRecipeRAPField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewRecipeFormulaField_Location.Y)
            Me._newRecipeACPercentageField_size = Me.NewRecipeRAPField_Size

            ' Add New Recipe Button
            Me._addNewRecipeButton_location = New Point(Me.Width - LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width, Me.NewRecipeFormulaField_Location.Y)
            Me._addNewRecipeButton_size = ADD_NEW_ITEM_BUTTON_SIZE

            ' Recipes List View
            Me._recipesListView_location = New Point(LOCATION_START_X, Me.NewRecipeFormulaField_Location.Y + Me.NewRecipeFormulaField_Size.Height)
            Me._recipesListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 300)

            ' Unknown Recipes List View
            Me._unknownRecipesListView_location = New Point(LOCATION_START_X, Me.RecipesListView_Location.Y + Me.RecipesListView_Size.Height)
            Me._unknownRecipesListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 150)



            ' New Asphalt Tank Name Field
            Me._newAsphaltTankNameField_location = New Point(LOCATION_START_X + SPACE_BETWEEN_CONTROLS_X, UnknownRecipesListView_Location.Y + UnknownRecipesListView_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._newAsphaltTankNameField_size = New Size((Me.Width - 2 * LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) * 3 / 8, FIELDS_HEIGHT)

            ' New Asphalt Name Field
            Me._newAsphaltNameField_location = New Point(Me.NewAsphaltTankNameField_Location.X + Me.NewAsphaltTankNameField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewAsphaltTankNameField_Location.Y)
            Me._newAsphaltNameField_size = Me.NewAsphaltTankNameField_Size

            ' New Mix Target Temperature Field
            Me._newMixTargetTemperatureField_location = New Point(Me.NewAsphaltNameField_Location.X + Me.NewAsphaltNameField_Size.Width + SPACE_BETWEEN_CONTROLS_X, Me.NewAsphaltTankNameField_Location.Y)
            Me._newMixTargetTemperatureField_size = New Size((Me.Width - 2 * LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width - 5 * SPACE_BETWEEN_CONTROLS_X) * 2 / 8, FIELDS_HEIGHT)

            ' Add New Tank Info Button
            Me._addNewTankInfoButton_location = New Point(Me.Width - LOCATION_START_X - ADD_NEW_ITEM_BUTTON_SIZE.Width, Me.NewAsphaltTankNameField_Location.Y)
            Me._addNewTankInfoButton_size = ADD_NEW_ITEM_BUTTON_SIZE

            ' Tank Info List View
            Me._tankInfoListView_location = New Point(LOCATION_START_X, Me.NewAsphaltTankNameField_Location.Y + Me.NewAsphaltTankNameField_Size.Height)
            Me._tankInfoListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 200)

            ' Unknown Tank Info List View
            Me._unknownTankInfoListView_location = New Point(LOCATION_START_X, Me.TankInfoListView_Location.Y + Me.TankInfoListView_Size.Height)
            Me._unknownTankInfoListView_size = New Size(Me.Width - 2 * LOCATION_START_X, 125)

        End Sub

        '
        ' New Recipe Formula Field
        '
        Public ReadOnly Property NewRecipeFormulaField_Location As Point
            Get
                Return Me._newRecipeFormulaField_location
            End Get
        End Property
        Public ReadOnly Property NewRecipeFormulaField_Size As Size
            Get
                Return Me._newRecipeFormulaField_size
            End Get
        End Property
        '
        ' New Recipe Mix Field
        '
        Public ReadOnly Property NewRecipeMixField_Location As Point
            Get
                Return Me._newRecipeMixField_location
            End Get
        End Property
        Public ReadOnly Property NewRecipeMixField_Size As Size
            Get
                Return Me._newRecipeMixField_size
            End Get
        End Property
        '
        ' New Recipe RAP Field
        '
        Public ReadOnly Property NewRecipeRAPField_Location As Point
            Get
                Return Me._newRecipeRAPField_location
            End Get
        End Property
        Public ReadOnly Property NewRecipeRAPField_Size As Size
            Get
                Return Me._newRecipeRAPField_size
            End Get
        End Property
        '
        ' New Recipe AC Percentage Field
        '
        Public ReadOnly Property NewRecipeACPercentageField_Location As Point
            Get
                Return Me._newRecipeACPercentageField_location
            End Get
        End Property
        Public ReadOnly Property NewRecipeACPercentageField_Size As Size
            Get
                Return Me._newRecipeACPercentageField_size
            End Get
        End Property
        '
        ' Add New Recipe Button
        '
        Public ReadOnly Property AddNewRecipeButton_Location As Point
            Get
                Return Me._addNewRecipeButton_location
            End Get
        End Property
        Public ReadOnly Property AddNewRecipeButton_Size As Size
            Get
                Return Me._addNewRecipeButton_size
            End Get
        End Property
        '
        ' Recipes List View
        '
        Public ReadOnly Property RecipesListView_Location As Point
            Get
                Return Me._recipesListView_location
            End Get
        End Property
        Public ReadOnly Property RecipesListView_Size As Size
            Get
                Return Me._recipesListView_size
            End Get
        End Property
        '
        ' Unknown Recipes List View
        '
        Public ReadOnly Property UnknownRecipesListView_Location As Point
            Get
                Return Me._unknownRecipesListView_location
            End Get
        End Property
        Public ReadOnly Property UnknownRecipesListView_Size As Size
            Get
                Return Me._unknownRecipesListView_size
            End Get
        End Property
        '
        ' New Asphalt Tank Name Field
        '
        Public ReadOnly Property NewAsphaltTankNameField_Location As Point
            Get
                Return Me._newAsphaltTankNameField_location
            End Get
        End Property
        Public ReadOnly Property NewAsphaltTankNameField_Size As Size
            Get
                Return Me._newAsphaltTankNameField_size
            End Get
        End Property
        '
        ' New Asphalt Name Field
        '
        Public ReadOnly Property NewAsphaltNameField_Location As Point
            Get
                Return Me._newAsphaltNameField_location
            End Get
        End Property
        Public ReadOnly Property NewAsphaltNameField_Size As Size
            Get
                Return Me._newAsphaltNameField_size
            End Get
        End Property
        '
        ' New Mix Target Temperature Field
        '
        Public ReadOnly Property NewMixTargetTemperatureField_Location As Point
            Get
                Return Me._newMixTargetTemperatureField_location
            End Get
        End Property
        Public ReadOnly Property NewMixTargetTemperatureField_Size As Size
            Get
                Return Me._newMixTargetTemperatureField_size
            End Get
        End Property
        '
        ' Add New Tank Info Button
        '
        Public ReadOnly Property AddNewTankInfoButton_Location As Point
            Get
                Return Me._addNewTankInfoButton_location
            End Get
        End Property
        Public ReadOnly Property AddNewTankInfoButton_Size As Size
            Get
                Return Me._addNewTankInfoButton_size
            End Get
        End Property
        '
        ' Tank Info List View
        '
        Public ReadOnly Property TankInfoListView_Location As Point
            Get
                Return Me._tankInfoListView_location
            End Get
        End Property
        Public ReadOnly Property TankInfoListView_Size As Size
            Get
                Return Me._tankInfoListView_size
            End Get
        End Property
        '
        ' Unknown Tank Info List View
        '
        Public ReadOnly Property UnknownTankInfoListView_Location As Point
            Get
                Return Me._unknownTankInfoListView_location
            End Get
        End Property
        Public ReadOnly Property UnknownTankInfoListView_Size As Size
            Get
                Return Me._unknownTankInfoListView_size
            End Get
        End Property
    End Class
End Namespace