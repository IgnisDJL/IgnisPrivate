Namespace UI

    Public Class MainMenuView
        Inherits View

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "IGNIS"

        ' Components
        Private WithEvents importFilesButton As Button
        Private WithEvents createDailyReportsButton As Button
        Private WithEvents createPeriodicReportsButton As Button
        Private WithEvents exportFilesButton As Button
        Private WithEvents configureSettingsButton As Button

        Private companyLogoPanel As Panel
        ' Attributes

        Public Sub New()

            Me.layout = New MainMenuLayout()

            Me.initializeComponents()

        End Sub

        Protected Overrides Sub initializeComponents()

            Me.importFilesButton = New Button
            Me.importFilesButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.importFilesButton.Image = Constants.UI.Images._32x32.IMPORT

            Me.createDailyReportsButton = New Button
            Me.createDailyReportsButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.createDailyReportsButton.Image = Constants.UI.Images._32x32.MULTIPLE_DAILY_REPORTS

            Me.createPeriodicReportsButton = New Button
            Me.createPeriodicReportsButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.createPeriodicReportsButton.Image = Constants.UI.Images._32x32.MULTIPLE_PERIODIC_REPORTS
            Me.createPeriodicReportsButton.Enabled = False

            Me.exportFilesButton = New Button
            Me.exportFilesButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.exportFilesButton.Image = Constants.UI.Images._32x32.EXPORT

            Me.configureSettingsButton = New Button
            Me.configureSettingsButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.configureSettingsButton.Image = Constants.UI.Images._32x32.SETTINGS

            Me.companyLogoPanel = New Panel
            Me.companyLogoPanel.BackgroundImage = Constants.UI.Images.Logo.LOGO
            Me.companyLogoPanel.Size = New Size(Constants.UI.Images.Logo.LOGO.Width, Constants.UI.Images.Logo.LOGO.Height)

            Me.Controls.Add(importFilesButton)
            Me.Controls.Add(createDailyReportsButton)
            Me.Controls.Add(createPeriodicReportsButton)
            Me.Controls.Add(exportFilesButton)
            Me.Controls.Add(configureSettingsButton)
            Me.Controls.Add(companyLogoPanel)

        End Sub

        Protected Overrides Sub ajustLayout(newSize As Size)

            Dim layout As MainMenuLayout = DirectCast(Me.LayoutManager, MainMenuLayout)

            ' Imported Files Button
            Me.importFilesButton.Location = layout.ImportFilesButton_Location
            Me.importFilesButton.Size = layout.ImportFilesButton_Size
            Me.importFilesButton.Text = layout.ImportFilesButton_Text

            ' Create Daily Reports Button
            Me.createDailyReportsButton.Location = layout.CreateDailyReportsButton_Location
            Me.createDailyReportsButton.Size = layout.CreateDailyReportsButton_Size
            Me.createDailyReportsButton.Text = layout.CreateDailyReportsButton_Text

            ' Create Periodic Reports Button
            Me.createPeriodicReportsButton.Location = layout.CreatePeriodicReportsButton_Location
            Me.createPeriodicReportsButton.Size = layout.CreatePeriodicReportsButton_Size
            Me.createPeriodicReportsButton.Text = layout.CreatePeriodicReportsButton_Text

            ' Send Files By Email Button
            Me.exportFilesButton.Location = layout.ExportFilesButton_Location
            Me.exportFilesButton.Size = layout.ExportFilesButton_Size
            Me.exportFilesButton.Text = layout.ExportFilesButton_Text

            ' Configure Settings Button
            Me.configureSettingsButton.Location = layout.ConfigureSettingsButton_Location
            Me.configureSettingsButton.Size = layout.ConfigureSettingsButton_Size
            Me.configureSettingsButton.Text = layout.ConfigureSettingsButton_Text

            ' Company logo panel
            Me.companyLogoPanel.Location = New Point(Me.Width - MainMenuLayout.LOCATION_START_X - Me.companyLogoPanel.Size.Width, Me.Height - MainMenuLayout.SPACE_BETWEEN_BUTTONS_Y - Me.companyLogoPanel.Size.Height)
        End Sub
        Private Sub x() Handles Me.Click
            Console.WriteLine(Me.Size)
        End Sub
        Protected Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub goToImportFilesView() Handles importFilesButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.ImportFilesView)
        End Sub

        Private Sub goToDailyReportView() Handles createDailyReportsButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.DailyReportView)
        End Sub

        Private Sub goToFileExportationView() Handles exportFilesButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.FileExportationView)
        End Sub

        Private Sub goToSettings() Handles configureSettingsButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.SettingsFrame)
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides Sub ajustLayoutFinal(newSize As Size)
            ' Do nothing
        End Sub

        Public Overrides Sub afterShow()

        End Sub
    End Class

End Namespace
