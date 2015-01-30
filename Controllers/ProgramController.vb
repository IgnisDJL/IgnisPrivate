Public Class ProgramController

    Private Shared _settingsControllers As SettingsControllerCollection ' #refactor - All ui calls to settings should come through this
    Private Shared _persistenceController As PersistenceController
    Private Shared _reportGenerationController As ReportGenerationController
    Private Shared _dataFilesPersistence As DataFilesPersistence
    Private Shared _reportsPersistence As ReportsPersistence
    Private Shared _manualDataPersistence As ManualDataPersistence
    Private Shared _importController As ImportController_1
    Private Shared _fileExportationController As FileExportationController
    Private Shared _uiController As UIController

    Public Shared Sub initialize(mainFrame As UI.MainFrame)

        ' Only works when debugger is on??? To verify
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UIExceptionHandler.instance.handle

        ' Set the culture of the program
        Threading.Thread.CurrentThread.CurrentCulture = XmlSettings.Settings.LANGUAGE.Culture
        Threading.Thread.CurrentThread.CurrentUICulture = XmlSettings.Settings.LANGUAGE.Culture

        ' Show Splash screen
        UI.SplashScreen.Show(mainFrame)

        UI.SplashScreen.instance.Message = "Initialisation des paramètres"

        ' Settings Controller
        _settingsControllers = New SettingsControllerCollection

        UI.SplashScreen.instance.Message = "Initialisation de la base de données"

        ' #refactor - move all that to persistence controller
        'Databases
        Dim db = New SQLiteAdapter

        _dataFilesPersistence = New DataFilesSQLDatabase(db)
        _dataFilesPersistence.verifyFormat()

        _reportsPersistence = New ReportsSQLDatabase(db)
        _reportsPersistence.verifyFormat()

        _manualDataPersistence = New ManualDataSQLDatabase(db)
        _manualDataPersistence.verifyFormat()

        ' Persistence Controller
        _persistenceController = New PersistenceController(DataFilesPersistence, _reportsPersistence, ManualDataPersistence, XmlSettings.Settings.instance)

        UI.SplashScreen.instance.Message = "Initialisation des controleurs"

        ' Report Generation Controller
        _reportGenerationController = New ReportGenerationController()

        ' Files Import controller
        _importController = New ImportController_1(XmlSettings.Settings.instance)

        ' Files Exportation Controller
        _fileExportationController = New FileExportationController(SettingsControllers.EmailSettingsController)

        UI.SplashScreen.instance.Message = "Initialisation des fenêtres"

        ' UI Controller
        _uiController = New UIController(mainFrame)

        UI.SplashScreen.instance.Dispose()

    End Sub

    Public Shared ReadOnly Property PersistenceController As PersistenceController
        Get
            Return _persistenceController
        End Get
    End Property

    Public Shared ReadOnly Property ReportGenerationController As ReportGenerationController
        Get
            Return _reportGenerationController
        End Get
    End Property

    Public Shared ReadOnly Property UIController As UIController
        Get
            Return _uiController
        End Get
    End Property

    Public Shared ReadOnly Property DataFilesPersistence As DataFilesPersistence
        Get
            Return _dataFilesPersistence
        End Get
    End Property

    Public Shared ReadOnly Property ReportsPersistence As ReportsPersistence
        Get
            Return _reportsPersistence
        End Get
    End Property

    Public Shared ReadOnly Property ManualDataPersistence As ManualDataPersistence
        Get
            Return _manualDataPersistence
        End Get
    End Property

    Public Shared ReadOnly Property ImportController As ImportController_1
        Get
            Return _importController
        End Get
    End Property

    Public Shared ReadOnly Property FileExportationController As FileExportationController
        Get
            Return _fileExportationController
        End Get
    End Property

    Public Shared ReadOnly Property SettingsControllers As SettingsControllerCollection
        Get
            Return _settingsControllers
        End Get
    End Property

End Class
