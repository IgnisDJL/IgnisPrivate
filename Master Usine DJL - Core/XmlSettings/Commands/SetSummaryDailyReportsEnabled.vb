Namespace Commands.Settings

    Public Class SetSummaryDailyReportsEnabled
        Inherits SettingsCommand

        Private _newState As Boolean

        Public Sub New(enabled As Boolean)
            MyBase.New()

            Me._newState = enabled
        End Sub

        Public Overrides Sub execute()

            Me.Settings.Reports.SummaryReport.ACTIVE = _newState
        End Sub

        Public Overrides Sub undo()

            Me.Settings.Reports.SummaryReport.ACTIVE = Not _newState
        End Sub

    End Class
End Namespace

