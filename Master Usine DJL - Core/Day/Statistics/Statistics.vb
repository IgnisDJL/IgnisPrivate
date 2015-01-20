Public MustInherit Class Statistics

    Protected _startTime As Date
    Protected _endTime As Date

    ' Prodution related
    Private _continuousProduction As ProductionTypeStatistics
    Private _discontinuousProduction As ProductionTypeStatistics

    ' Mix Related
    Private _allMixes As List(Of MixStatistics)
    Private _otherMixes As MixStatistics
    Private _mixesTotal As MixStatistics

    ' Asphalt Related
    Private _allAsphalts As List(Of AsphaltStatistics)

    ' Cycle relate
    Protected _nbProductiveCycles As Integer = 0

    ' Event related
    Private _eventsStatistics As EventsStatistics

    Public Sub New()

        Me._continuousProduction = New ProductionTypeStatistics
        Me._discontinuousProduction = New ProductionTypeStatistics

        Me._allMixes = New List(Of MixStatistics)
        Me._otherMixes = New MixStatistics
        Me._mixesTotal = New MixStatistics
        Me._allAsphalts = New List(Of AsphaltStatistics)
        Me._eventsStatistics = New EventsStatistics
    End Sub

    ''' <summary>
    ''' Computes the statistics from the given cycles and events
    ''' </summary>
    Public MustOverride Sub compute(cycles As List(Of Cycle), ByRef events As EventsCollection)

    Public ReadOnly Property ProductionStartTime As Date
        Get
            Return _startTime
        End Get
    End Property

    Public ReadOnly Property ProductionEndTime As Date
        Get
            Return _endTime
        End Get
    End Property

    ''' <summary>Production statistics for the Continuous part of the production</summary>
    Public ReadOnly Property ContinuousProduction As ProductionTypeStatistics
        Get
            Return _continuousProduction
        End Get
    End Property

    ''' <summary>Production statistics for the Discontinuous part of the production</summary>
    Public ReadOnly Property DiscontinuousProduction As ProductionTypeStatistics
        Get
            Return _discontinuousProduction
        End Get
    End Property

    ''' <summary>List of statistics for each different mixes</summary>
    Public ReadOnly Property AllMixes As List(Of MixStatistics)
        Get
            Return _allMixes
        End Get
    End Property

    ''' <summary>Mix statistics on mixes other than the top 3 mixes</summary>
    Public ReadOnly Property OtherMixes As MixStatistics
        Get
            Return _otherMixes
        End Get
    End Property

    ''' <summary>Mix statistics on all the mixes</summary>
    Public ReadOnly Property MixesTotal As MixStatistics
        Get
            Return _mixesTotal
        End Get
    End Property

    ''' <summary>List of statistics for each different asphalt</summary>
    Public ReadOnly Property AllAsphalts As List(Of AsphaltStatistics)
        Get
            Return _allAsphalts
        End Get
    End Property

    ''' <summary>Events statistics</summary>
    Public ReadOnly Property EventsStatistics As EventsStatistics
        Get
            Return _eventsStatistics
        End Get
    End Property

    ''' <summary>Gets the number of cycles with a production rate greater than zero</summary>
    Public ReadOnly Property NB_PRODUCTIVE_CYLES As Integer
        Get
            Return _nbProductiveCycles
        End Get
    End Property

End Class
