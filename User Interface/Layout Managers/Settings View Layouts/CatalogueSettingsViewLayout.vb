
Namespace UI

    Public Class CatalogueSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly FEEDS_MANAGERS_HEIGHT As Integer = 300

        ' Components attributes
        Private _catalogueFeeder_location As Point
        Private _catalogueFeeder_size As Size

        Private _catalogueMix_location As Point
        Private _catalogueMix_size As Size

        Private _catalogueAsphaltConcrete_location As Point
        Private _catalogueAsphaltConcrete_size As Size

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            Me._catalogueFeeder_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            Me._catalogueFeeder_size = New Size(Me.Width - 2 * LOCATION_START_X, FEEDS_MANAGERS_HEIGHT)

            Me._catalogueMix_location = New Point(LOCATION_START_X, Me.CatalogueFeeder_Location.Y + Me.CatalogueFeeder_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._catalogueMix_size = New Size(Me.Width - 2 * LOCATION_START_X, FEEDS_MANAGERS_HEIGHT)

            Me._catalogueAsphaltConcrete_location = New Point(LOCATION_START_X, Me.CatalogueMix_Location.Y + Me.CatalogueMix_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._catalogueAsphaltConcrete_size = New Size(Me.Width - 2 * LOCATION_START_X, FEEDS_MANAGERS_HEIGHT)

        End Sub
        '
        ' Feed Manager 1
        '
        Public ReadOnly Property CatalogueFeeder_Location As Point
            Get
                Return _catalogueFeeder_location
            End Get
        End Property

        Public ReadOnly Property CatalogueFeeder_Size As Size
            Get
                Return _catalogueFeeder_size
            End Get
        End Property
        '
        ' Feed Manager 2
        '
        Public ReadOnly Property CatalogueMix_Location As Point
            Get
                Return _catalogueMix_location
            End Get
        End Property
        Public ReadOnly Property CatalogueMix_Size As Size
            Get
                Return _catalogueMix_size
            End Get
        End Property
        '
        ' Feed Manager 3
        '
        Public ReadOnly Property CatalogueAsphaltConcrete_Location As Point
            Get
                Return _catalogueAsphaltConcrete_location
            End Get
        End Property
        Public ReadOnly Property CatalogueAsphaltConcrete_Size As Size
            Get
                Return _catalogueAsphaltConcrete_size
            End Get
        End Property
    End Class
End Namespace
