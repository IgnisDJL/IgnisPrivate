Namespace Commands.Settings

    Public Class SetDailySummaryReportOpenReadOnly
        Inherits SettingsCommand

        Private _newState As Boolean

        Public Sub New(enabled As Boolean)
            MyBase.New()

            Me._newState = enabled
        End Sub

        Public Overrides Sub execute()

            Me.Settings.Reports.SummaryReport.OPEN_WHEN_DONE_READ_ONLY = _newState
        End Sub

        Public Overrides Sub undo()

            Me.Settings.Reports.SummaryReport.OPEN_WHEN_DONE_READ_ONLY = Not _newState
        End Sub

    End Class
End Namespace

