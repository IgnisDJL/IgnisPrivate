
Namespace UI

    Public Class ReportsSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Rapports"


        ' Components
        ' !LAYOUT!
        Private WithEvents summaryDailyReportEnableCheckBox As CheckBox
        Private WithEvents openSummaryDailyReportWhenDoneLabel As Label
        Private WithEvents openSummaryDailyReportReadOnlyOptionCheckBox As CheckBox
        Private WithEvents openSummaryDailyReportWritableOptionCheckBox As CheckBox

        Private WithEvents reportsUnitsPanel As UnitsPanel
        ' !LAYOUT!


        ' Attributes
        Private _reportsSettings As ReportsSettingsController

        Public Sub New()
            MyBase.New()

            Me.layout = New ReportsSettingsViewLayout

            Me._reportsSettings = ProgramController.SettingsControllers.ReportsSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.summaryDailyReportEnableCheckBox = New CheckBox
            Me.summaryDailyReportEnableCheckBox.AutoSize = False
            Me.summaryDailyReportEnableCheckBox.TextAlign = ContentAlignment.MiddleCenter
            Me.summaryDailyReportEnableCheckBox.CheckAlign = ContentAlignment.MiddleLeft
            Me.summaryDailyReportEnableCheckBox.Text = "Générer des rapports journaliers sommaires"
            Me.summaryDailyReportEnableCheckBox.Cursor = Cursors.Hand

            Me.openSummaryDailyReportWhenDoneLabel = New Label
            Me.openSummaryDailyReportWhenDoneLabel.Text = "Ouvrir lorsque la génération est terminée :"
            Me.openSummaryDailyReportWhenDoneLabel.TextAlign = ContentAlignment.BottomLeft

            Me.openSummaryDailyReportReadOnlyOptionCheckBox = New CheckBox
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.AutoSize = False
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.TextAlign = ContentAlignment.MiddleCenter
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.CheckAlign = ContentAlignment.MiddleRight
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.ImageAlign = ContentAlignment.MiddleLeft
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.Image = Constants.UI.Images._32x32.READONLY_SUMMARY_DAILY_REPORT
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.Text = "Format PDF"
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.Cursor = Cursors.Hand

            Me.openSummaryDailyReportWritableOptionCheckBox = New CheckBox
            Me.openSummaryDailyReportWritableOptionCheckBox.AutoSize = False
            Me.openSummaryDailyReportWritableOptionCheckBox.TextAlign = ContentAlignment.MiddleCenter
            Me.openSummaryDailyReportWritableOptionCheckBox.CheckAlign = ContentAlignment.MiddleRight
            Me.openSummaryDailyReportWritableOptionCheckBox.ImageAlign = ContentAlignment.MiddleLeft
            Me.openSummaryDailyReportWritableOptionCheckBox.Image = Constants.UI.Images._32x32.SUMMARY_DAILY_REPORT
            Me.openSummaryDailyReportWritableOptionCheckBox.Text = "Format Word"
            Me.openSummaryDailyReportWritableOptionCheckBox.Cursor = Cursors.Hand

            Me.reportsUnitsPanel = New UnitsPanel()
            Me.reportsUnitsPanel.Title = "Unités dans les rapports"

            Me.Controls.Add(Me.summaryDailyReportEnableCheckBox)
            Me.Controls.Add(Me.openSummaryDailyReportWhenDoneLabel)
            Me.Controls.Add(Me.openSummaryDailyReportReadOnlyOptionCheckBox)
            Me.Controls.Add(Me.openSummaryDailyReportWritableOptionCheckBox)
            Me.Controls.Add(Me.reportsUnitsPanel)

        End Sub


        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, ReportsSettingsViewLayout)

            Me.summaryDailyReportEnableCheckBox.Location = layout.SummaryDailyReportEnableCheckBox_Location
            Me.summaryDailyReportEnableCheckBox.Size = layout.SummaryDailyReportEnableCheckBox_Size

            Me.openSummaryDailyReportWhenDoneLabel.Location = layout.OpenSummaryDailyReportWhenDoneLabel_Location
            Me.openSummaryDailyReportWhenDoneLabel.Size = layout.OpenSummaryDailyReportWhenDoneLabel_Size

            Me.openSummaryDailyReportReadOnlyOptionCheckBox.Location = layout.OpenSummaryDailyReportReadOnlyOptionCheckBox_Location
            Me.openSummaryDailyReportReadOnlyOptionCheckBox.Size = layout.OpenSummaryDailyReportReadOnlyOptionCheckBox_Size

            Me.openSummaryDailyReportWritableOptionCheckBox.Location = layout.OpenSummaryDailyReportWritableOptionCheckBox_Location
            Me.openSummaryDailyReportWritableOptionCheckBox.Size = layout.OpenSummaryDailyReportWritableOptionCheckBox_Size

            Me.reportsUnitsPanel.Location = layout.ReportsUnitsPanel_Location
            Me.reportsUnitsPanel.ajustLayout(layout.ReportsUnitsPanel_Size)

        End Sub


        Protected Overloads Overrides Sub ajustLayoutFinal()

        End Sub

        Public Overrides Sub updateFields()
            Me.updatingFields = True

            Me.summaryDailyReportEnableCheckBox.Checked = Me._reportsSettings.DailyReportsEnabled

            Me.openSummaryDailyReportReadOnlyOptionCheckBox.Checked = _reportsSettings.DailyReportsOpenReadOnlyWhenDone
            Me.openSummaryDailyReportWritableOptionCheckBox.Checked = _reportsSettings.DailyReportsOpenWritableWhenDone

            Me.reportsUnitsPanel.updateUnits(_reportsSettings.ReportsMassUnit, _reportsSettings.ReportsTemperatureUnit, _reportsSettings.ReportsPercentUnit, _reportsSettings.ReportsProductionRateUnit)

            Me.updatingFields = False
        End Sub

        Protected Overloads Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub afterShow()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub toggleSummaryDailyReportFields() Handles summaryDailyReportEnableCheckBox.CheckedChanged

            With Me.summaryDailyReportEnableCheckBox

                Me.openSummaryDailyReportWhenDoneLabel.Enabled = .Checked
                Me.openSummaryDailyReportReadOnlyOptionCheckBox.Enabled = .Checked
                Me.openSummaryDailyReportWritableOptionCheckBox.Enabled = .Checked


                If (Not Me.updatingFields) Then

                    Me._reportsSettings.DailyReportsEnabled = .Checked

                    Me.raiseSettingChangedEvent()
                End If
            End With

        End Sub

        Private Sub onSummaryDailyOpenWhenDoneChecked(sender As Object, e As EventArgs) Handles openSummaryDailyReportReadOnlyOptionCheckBox.CheckedChanged, openSummaryDailyReportWritableOptionCheckBox.CheckedChanged

            If (Not Me.updatingFields) Then

                Me.updatingFields = True

                If (sender.Equals(openSummaryDailyReportReadOnlyOptionCheckBox)) Then

                    Me._reportsSettings.DailyReportsOpenReadOnlyWhenDone = Me.openSummaryDailyReportReadOnlyOptionCheckBox.Checked
                    Me.raiseSettingChangedEvent()

                ElseIf (sender.Equals(openSummaryDailyReportWritableOptionCheckBox)) Then

                    Me._reportsSettings.DailyReportsOpenWritableWhenDone = Me.openSummaryDailyReportWritableOptionCheckBox.Checked
                    Me.raiseSettingChangedEvent()
                End If

                Me.updatingFields = False
            End If
        End Sub

        Private Sub onUnitsChanged() Handles reportsUnitsPanel.MassUnitChanged, reportsUnitsPanel.TemperatureUnitChanged, reportsUnitsPanel.PercentageUnitChanged, reportsUnitsPanel.ProductionRateUnitChanged

            If (Not Me.updatingFields) Then

                With reportsUnitsPanel
                    Me._reportsSettings.setReportsUnits(.MassUnit, .TemperatureUnit, .PercentageUnit, .ProductionRateUnit)
                End With

                Me.raiseSettingChangedEvent()
            End If

        End Sub

        Private Sub clickThroughLabels() Handles openSummaryDailyReportWhenDoneLabel.Click
            Me.Focus()
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._reportsSettings
            End Get
        End Property
    End Class
End Namespace


