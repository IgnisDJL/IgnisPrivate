Imports IGNIS.Commands.Settings

Public Class ReportsSettingsController
    Inherits SettingsController

    Public Sub New()
        MyBase.New()

    End Sub

    Public Property DailyReportsEnabled As Boolean
        Get
            Return XmlSettings.Settings.instance.Reports.SummaryReport.ACTIVE
        End Get
        Set(value As Boolean)
            Me.executeCommand(New SetSummaryDailyReportsEnabled(value))
        End Set
    End Property

    Public Property DailyReportsOpenWritableWhenDone As Boolean
        Get
            Return XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_WRITABLE
        End Get
        Set(value As Boolean)
            Me.executeCommand(New SetDailySummaryReportOpenWritable(value))
        End Set
    End Property

    Public Property DailyReportsOpenReadOnlyWhenDone As Boolean
        Get
            Return XmlSettings.Settings.instance.Reports.SummaryReport.OPEN_WHEN_DONE_READ_ONLY
        End Get
        Set(value As Boolean)
            Me.executeCommand(New SetDailySummaryReportOpenReadOnly(value))
        End Set
    End Property

    Public Sub setReportsUnits(massUnit As Unit, temperatureUnit As Unit, percentUnit As Unit, productionRateUnit As Unit)
        Me.executeCommand(New SetReportsUnits(massUnit, temperatureUnit, percentUnit, productionRateUnit))
    End Sub

    Public ReadOnly Property ReportsMassUnit As Unit
        Get
            Return XmlSettings.Settings.instance.Reports.MASS_UNIT
        End Get
    End Property

    Public ReadOnly Property ReportsTemperatureUnit As Unit
        Get
            Return XmlSettings.Settings.instance.Reports.TEMPERATURE_UNIT
        End Get
    End Property

    Public ReadOnly Property ReportsPercentUnit As Unit
        Get
            Return XmlSettings.Settings.instance.Reports.PERCENT_UNIT
        End Get
    End Property

    Public ReadOnly Property ReportsProductionRateUnit As Unit
        Get
            Return XmlSettings.Settings.instance.Reports.PRODUCTION_SPEED_UNIT
        End Get
    End Property
End Class
