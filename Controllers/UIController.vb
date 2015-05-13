Imports IGNIS.UI

Public Class UIController

    ' Controls the Mainwindow, the reportGenerationControl, the archives view, the reporthistoryview, the manual data prompt, the events prompt,
    ' the comments prompt and the settingsWindows

    Private _mainFrame As UI.MainFrame

    ' Views
    Private _mainMenuView As MainMenuView
    Private _importFilesView As ImportFilesView
    Private _dailyReportView As DailyReportView
    Private _fileExportationView As FileExportationView
    Private _reportGenerationFrame As DailyReportGenerationFrame
    Private _settingsFrame As SettingsFrame

    ' -- Generation Steps
    Private _dataFilesAnalysisStepView As DataFilesAnalysisStepView
    Private _manualDataStepView As ManualDataStepView
    Private _delaysJustificationStepView As DelayJustificationStepView
    Private _commentsStepView As CommentsStepView
    Private _finishingGenerationStepView As FinishingGenerationStepView

    ' -- Settings Views
    Private _usineSettingsView As UsineSettingsView
    Private _dataFilesSettingsView As DataFilesSettingsView
    Private _feedsSettingsView As FeedsSettingsView
    Private _catalogueSettingsView As CatalogSettingsView
    Private _mixAndAsphaltSettingsView As MixAndAsphaltSettingsView
    Private _eventsSettingsView As EventsSettingsView
    Private _reportsSettingsView As ReportsSettingsView
    Private _emailSettingsView As EmailSettingsView
    Private _adminSettingsView As AdminSettingsView

    ' -- Exportation Views
    Private _emailExportationView As EmailExportationView

    ' Shared Components

    Private currentView As View

    Public Sub New(mainContainer As UI.MainFrame)
        Me._mainFrame = mainContainer

        ' Top level views
        Me._mainMenuView = New MainMenuView
        Me._importFilesView = New ImportFilesView
        Me._dailyReportView = New DailyReportView
        Me._fileExportationView = New FileExportationView
        Me._reportGenerationFrame = New DailyReportGenerationFrame
        Me._settingsFrame = New SettingsFrame

        ' Report generation steps views
        Me._dataFilesAnalysisStepView = New DataFilesAnalysisStepView(XmlSettings.Settings.instance.Usine)
        Me._manualDataStepView = New ManualDataStepView
        Me._delaysJustificationStepView = New DelayJustificationStepView
        Me._commentsStepView = New CommentsStepView
        Me._finishingGenerationStepView = New FinishingGenerationStepView

        ' Settings views
        Me._usineSettingsView = New UsineSettingsView
        Me._dataFilesSettingsView = New DataFilesSettingsView
        Me._feedsSettingsView = New FeedsSettingsView
        Me._catalogueSettingsView = New CatalogSettingsView
        Me._mixAndAsphaltSettingsView = New MixAndAsphaltSettingsView
        Me._eventsSettingsView = New EventsSettingsView
        Me._reportsSettingsView = New ReportsSettingsView
        Me._emailSettingsView = New EmailSettingsView
        Me._adminSettingsView = New AdminSettingsView

        Me._settingsFrame.addSettingView(Me._usineSettingsView)
        Me._settingsFrame.addSettingView(Me._dataFilesSettingsView)
        Me._settingsFrame.addSettingView(Me._feedsSettingsView)
        Me._settingsFrame.addSettingView(Me._catalogueSettingsView)
        Me._settingsFrame.addSettingView(Me._mixAndAsphaltSettingsView)
        Me._settingsFrame.addSettingView(Me._eventsSettingsView)
        Me._settingsFrame.addSettingView(Me._reportsSettingsView)
        Me._settingsFrame.addSettingView(Me._emailSettingsView)
        Me._settingsFrame.addSettingView(Me._adminSettingsView)

        Me._emailExportationView = New EmailExportationView

        ' #todo With settings, decide wether to show main menu or dailyReport view
        Me.changeView(MainMenuView)

    End Sub

    Public Sub changeView(newView As View)

        If (Not IsNothing(Me.currentView)) Then
            Me.currentView.onHide()
        End If

        _mainFrame.Controls.Remove(Me.currentView)

        Me.currentView = newView

        Me.currentView.Visible = False

        Me.currentView.beforeShow(_mainFrame)

        _mainFrame.Controls.Add(Me.currentView)
        _mainFrame.Text = Me.currentView.Name

        Me.currentView.afterShow()

        _mainFrame.Refresh()

        Me.currentView.Visible = True

    End Sub

    Public Sub invokeFromUIThread(method As [Delegate])

        _mainFrame.Invoke(method)

    End Sub

    Public ReadOnly Property MainFrame As UI.MainFrame
        Get
            Return _mainFrame
        End Get
    End Property

    Public ReadOnly Property MainMenuView As MainMenuView
        Get
            Return _mainMenuView
        End Get
    End Property

    Public ReadOnly Property ImportFilesView As ImportFilesView
        Get
            Return _importFilesView
        End Get
    End Property

    Public ReadOnly Property DailyReportView As DailyReportView
        Get
            Return _dailyReportView
        End Get
    End Property

    Public ReadOnly Property FileExportationView As FileExportationView
        Get
            Return _fileExportationView
        End Get
    End Property

    Public ReadOnly Property ReportGenerationFrame As DailyReportGenerationFrame
        Get
            Return _reportGenerationFrame
        End Get
    End Property

    Public ReadOnly Property SettingsFrame As SettingsFrame
        Get
            Return _settingsFrame
        End Get
    End Property

    Public ReadOnly Property DataFilesAnalysisStepView As DataFilesAnalysisStepView
        Get
            Return _dataFilesAnalysisStepView
        End Get
    End Property

    Public ReadOnly Property ManualDataStepView As ManualDataStepView
        Get
            Return _manualDataStepView
        End Get
    End Property

    Public ReadOnly Property DelaysJustificationStepView As DelayJustificationStepView
        Get
            Return _delaysJustificationStepView
        End Get
    End Property

    Public ReadOnly Property CommentsStepView As CommentsStepView
        Get
            Return _commentsStepView
        End Get
    End Property

    Public ReadOnly Property FinishingGenerationStepView As FinishingGenerationStepView
        Get
            Return Me._finishingGenerationStepView
        End Get
    End Property

    Public ReadOnly Property UsineSettingsView As UsineSettingsView
        Get
            Return Me._usineSettingsView
        End Get
    End Property

    Public ReadOnly Property DataFilesSettingsView As DataFilesSettingsView
        Get
            Return Me._dataFilesSettingsView
        End Get
    End Property

    Public ReadOnly Property FeedsSettingsView As FeedsSettingsView
        Get
            Return Me._feedsSettingsView
        End Get
    End Property

    Public ReadOnly Property CatalogueSettingsView As CatalogSettingsView
        Get
            Return Me._catalogueSettingsView
        End Get
    End Property

    Public ReadOnly Property MixAndAsphaltSettingsView As MixAndAsphaltSettingsView
        Get
            Return Me._mixAndAsphaltSettingsView
        End Get
    End Property

    Public ReadOnly Property EventsSettingsView As EventsSettingsView
        Get
            Return Me._eventsSettingsView
        End Get
    End Property

    Public ReadOnly Property ReportsSettingsView As ReportsSettingsView
        Get
            Return Me._reportsSettingsView
        End Get
    End Property

    Public ReadOnly Property EmailSettingsView As EmailSettingsView
        Get
            Return Me._emailSettingsView
        End Get
    End Property

    Public ReadOnly Property AdminSettingsView As AdminSettingsView
        Get
            Return Me._adminSettingsView
        End Get
    End Property

    Public ReadOnly Property EmailExportationView As EmailExportationView
        Get
            Return Me._emailExportationView
        End Get
    End Property
End Class
