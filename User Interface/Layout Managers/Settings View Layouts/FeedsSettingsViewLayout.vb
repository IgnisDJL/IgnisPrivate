
Namespace UI

    Public Class FeedsSettingsViewLayout
        Inherits LayoutManager

        ' Constants
        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared ReadOnly FEED_MANAGERS_HEIGHT As Integer = 300

        ' Components attributes
        Private _feedManager1_location As Point
        Private _feedManager1_size As Size

        Private _feedManager2_location As Point
        Private _feedManager2_size As Size

        Private _feedManager3_location As Point
        Private _feedManager3_size As Size

        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            Me._feedManager1_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            Me._feedManager1_size = New Size(Me.Width - 2 * LOCATION_START_X, FEED_MANAGERS_HEIGHT)

            Me._feedManager2_location = New Point(LOCATION_START_X, Me.FeedManager1_Location.Y + Me.FeedManager1_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._feedManager2_size = New Size(Me.Width - 2 * LOCATION_START_X, FEED_MANAGERS_HEIGHT)

            Me._feedManager3_location = New Point(LOCATION_START_X, Me.FeedManager2_Location.Y + Me.FeedManager2_Size.Height + SPACE_BETWEEN_CONTROLS_Y)
            Me._feedManager3_size = New Size(Me.Width - 2 * LOCATION_START_X, FEED_MANAGERS_HEIGHT)

        End Sub
        '
        ' Feed Manager 1
        '
        Public ReadOnly Property FeedManager1_Location As Point
            Get
                Return _feedManager1_location
            End Get
        End Property
        Public ReadOnly Property FeedManager1_Size As Size
            Get
                Return _feedManager1_size
            End Get
        End Property
        '
        ' Feed Manager 2
        '
        Public ReadOnly Property FeedManager2_Location As Point
            Get
                Return _feedManager2_location
            End Get
        End Property
        Public ReadOnly Property FeedManager2_Size As Size
            Get
                Return _feedManager2_size
            End Get
        End Property
        '
        ' Feed Manager 3
        '
        Public ReadOnly Property FeedManager3_Location As Point
            Get
                Return _feedManager3_location
            End Get
        End Property
        Public ReadOnly Property FeedManager3_Size As Size
            Get
                Return _feedManager3_size
            End Get
        End Property
    End Class
End Namespace
