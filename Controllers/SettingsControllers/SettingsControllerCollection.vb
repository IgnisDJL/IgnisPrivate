Public Class SettingsControllerCollection

    Private _usineSettingsController As UsineSettingsController
    Private _dataFilesSettingsController As DataFilesSettingsController
    Private _cataloguesSettingsController As CataloguesSettingsController
    Private _feedsSettingsController As FeedsSettingsController
    Private _mixAndAsphaltSettingsController As MixAndAsphaltSettingsController
    Private _eventsSettingsController As EventsSettingsController
    Private _reportsSettingsController As ReportsSettingsController
    Private _emailSettingsController As EmailSettingsController
    Private _adminSettingsController As AdminSettingsController

    Public Sub New()

        Me._usineSettingsController = New UsineSettingsController
        Me._dataFilesSettingsController = New DataFilesSettingsController
        Me._feedsSettingsController = New FeedsSettingsController
        Me._cataloguesSettingsController = New CataloguesSettingsController
        Me._mixAndAsphaltSettingsController = New MixAndAsphaltSettingsController
        Me._eventsSettingsController = New EventsSettingsController
        Me._reportsSettingsController = New ReportsSettingsController
        Me._emailSettingsController = New EmailSettingsController
        Me._adminSettingsController = New AdminSettingsController
    End Sub

    Public ReadOnly Property UsineSettingsController As UsineSettingsController
        Get
            Return Me._usineSettingsController
        End Get
    End Property

    Public ReadOnly Property DataFilesSettingsController As DataFilesSettingsController
        Get
            Return Me._dataFilesSettingsController
        End Get
    End Property

    Public ReadOnly Property FeedsSettingsController As FeedsSettingsController
        Get
            Return Me._feedsSettingsController
        End Get
    End Property

    Public ReadOnly Property CataloguesSettingsController As CataloguesSettingsController
        Get
            Return Me._cataloguesSettingsController
        End Get
    End Property

    Public ReadOnly Property MixAndAsphaltSettingsController As MixAndAsphaltSettingsController
        Get
            Return Me._mixAndAsphaltSettingsController
        End Get
    End Property

    Public ReadOnly Property EventsSettingsController As EventsSettingsController
        Get
            Return Me._eventsSettingsController
        End Get
    End Property

    Public ReadOnly Property ReportsSettingsController As ReportsSettingsController
        Get
            Return Me._reportsSettingsController
        End Get
    End Property

    Public ReadOnly Property EmailSettingsController As EmailSettingsController
        Get
            Return Me._emailSettingsController
        End Get
    End Property

    Public ReadOnly Property AdminSettingsController As AdminSettingsController
        Get
            Return Me._adminSettingsController
        End Get
    End Property

End Class
