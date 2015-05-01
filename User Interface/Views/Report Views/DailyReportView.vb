Namespace UI

    Public Class DailyReportView
        Inherits ReportViewTemplate

        ' Constants
        ' #language
        Private Shared ReadOnly SHOW_ONLY_REPORT_READY_CHECKBOX_TOOLTIP_CHECKED As String = "Afficher toutes les dates"
        Private Shared ReadOnly SHOW_ONLY_REPORT_READY_CHECKBOX_TOOLTIP_UNCHECKED As String = "Afficher les date prêtes pour" & Environment.NewLine & "la génération de rapport"

        Private Shared ReadOnly TODAY_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne la date d'aujourd'hui"
        Private Shared ReadOnly YESTERDAY_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne la date de hier"
        Private Shared ReadOnly LAST_REPORT_READY_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne la dernière date prête" & Environment.NewLine & "pour la génération de rapports"
        Private Shared ReadOnly THIS_WEEK_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne les dates de cette semaine"
        Private Shared ReadOnly LAST_WEEK_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne les dates de la semaine dernière"

        ' Components
        Private WithEvents showOnlyReportReadyDatesCheckbox As CheckBox
        Private showOnlyReportsReadyDatesCheckboxToolTip As ToolTip

        ' --- Shortcut buttons
        Private WithEvents todayShortcutButton As Button
        Private WithEvents yesterdayShortcutButton As Button
        Private WithEvents lastReportReadyDateShortcutButton As Button
        Private WithEvents thisWeekShortcutButton As Button
        Private WithEvents lastWeekShortcutButton As Button

        Private shortcutButtonsToolTip As ToolTip

        Private WithEvents sendReportsByEmailPanel As Common.UserMessagePanel
        Private WithEvents noLastReportReadyDateMessagePanel As Common.UserMessagePanel

        ' Attributes
        Private generationController As ReportGenerationController_1

        Public Sub New()
            MyBase.New()

            Me.generationController = ProgramController.ReportGenerationController

            Me.layout = New DailyReportViewLayout

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            ' Shortcut buttons
            Me.shortcutButtonsToolTip = New ToolTip
            Me.shortcutButtonsToolTip.ShowAlways = True
            Me.shortcutButtonsToolTip.Active = True
            Me.shortcutButtonsToolTip.InitialDelay = 500
            Me.shortcutButtonsToolTip.AutoPopDelay = 10000
            Me.shortcutButtonsToolTip.BackColor = Color.White

            Me.todayShortcutButton = New Button
            Me.todayShortcutButton.Text = "Aujourd'hui"
            Me.todayShortcutButton.TextAlign = ContentAlignment.MiddleCenter
            Me.todayShortcutButton.Font = Constants.UI.Fonts.SMALLER_DEFAULT_FONT
            Me.shortcutButtonsToolTip.SetToolTip(Me.todayShortcutButton, TODAY_SHORTCUT_BUTTON_TOOLTIP_TEXT)

            Me.yesterdayShortcutButton = New Button
            Me.yesterdayShortcutButton.Text = "Hier"
            Me.yesterdayShortcutButton.TextAlign = ContentAlignment.MiddleCenter
            Me.yesterdayShortcutButton.Font = Constants.UI.Fonts.SMALLER_DEFAULT_FONT
            Me.shortcutButtonsToolTip.SetToolTip(Me.yesterdayShortcutButton, YESTERDAY_SHORTCUT_BUTTON_TOOLTIP_TEXT)

            Me.lastReportReadyDateShortcutButton = New Button
            Me.lastReportReadyDateShortcutButton.Text = "Dernière date disponible"
            Me.lastReportReadyDateShortcutButton.TextAlign = ContentAlignment.MiddleCenter
            Me.lastReportReadyDateShortcutButton.Font = Constants.UI.Fonts.SMALLEST_DEFAULT_FONT
            Me.shortcutButtonsToolTip.SetToolTip(Me.lastReportReadyDateShortcutButton, LAST_REPORT_READY_SHORTCUT_BUTTON_TOOLTIP_TEXT)

            Me.thisWeekShortcutButton = New Button
            Me.thisWeekShortcutButton.Text = "Cette semaine"
            Me.thisWeekShortcutButton.TextAlign = ContentAlignment.MiddleCenter
            Me.thisWeekShortcutButton.Font = Constants.UI.Fonts.SMALLEST_DEFAULT_FONT
            Me.shortcutButtonsToolTip.SetToolTip(Me.thisWeekShortcutButton, THIS_WEEK_SHORTCUT_BUTTON_TOOLTIP_TEXT)

            Me.lastWeekShortcutButton = New Button
            Me.lastWeekShortcutButton.Text = "Semaine dernière"
            Me.lastWeekShortcutButton.TextAlign = ContentAlignment.MiddleCenter
            Me.lastWeekShortcutButton.Font = Constants.UI.Fonts.SMALLEST_DEFAULT_FONT
            Me.shortcutButtonsToolTip.SetToolTip(Me.lastWeekShortcutButton, LAST_WEEK_SHORTCUT_BUTTON_TOOLTIP_TEXT)

            ' Add controls to datePicker panel
            Me.datePickerPanel.addShortcutButton(todayShortcutButton)
            Me.datePickerPanel.addShortcutButton(yesterdayShortcutButton)
            Me.datePickerPanel.addShortcutButton(lastReportReadyDateShortcutButton)
            Me.datePickerPanel.addShortcutButton(thisWeekShortcutButton)
            Me.datePickerPanel.addShortcutButton(lastWeekShortcutButton)

            ' Show only report ready dates checkbox tooltip
            Me.showOnlyReportsReadyDatesCheckboxToolTip = New ToolTip
            Me.showOnlyReportsReadyDatesCheckboxToolTip.ShowAlways = True
            Me.showOnlyReportsReadyDatesCheckboxToolTip.Active = True
            Me.showOnlyReportsReadyDatesCheckboxToolTip.InitialDelay = 500
            Me.showOnlyReportsReadyDatesCheckboxToolTip.AutoPopDelay = 10000
            Me.showOnlyReportsReadyDatesCheckboxToolTip.BackColor = Color.White

            ' Show only report ready dates checkbox
            Me.showOnlyReportReadyDatesCheckbox = New CheckBox
            Me.showOnlyReportReadyDatesCheckbox.BackgroundImageLayout = ImageLayout.Center
            Me.showOnlyReportReadyDatesCheckbox.BackgroundImage = Constants.UI.Images._24x24.GOOD
            Me.showOnlyReportReadyDatesCheckbox.Appearance = Appearance.Button
            Me.showOnlyReportReadyDatesCheckbox.Size = DailyReportViewLayout.DATE_LIST_VIEW_BUTTON_SIZE
            Me.showOnlyReportReadyDatesCheckbox.Checked = True

            Me.availableDatesListView.addTitleBarButton(Me.showOnlyReportReadyDatesCheckbox)

            Me.reportsToGenerateListControl.ItemIsCheckedMethod = Function(item As ReportFile.ReportTypes) As Boolean
                                                                      Return True
                                                                  End Function

            Me.reportsToGenerateListControl.ItemIsEnabledMethod = Function(item As ReportFile.ReportTypes) As Boolean
                                                                      Return True
                                                                  End Function

        End Sub

        Protected Overloads Overrides Sub ajustLayout(newSize As Size)
            MyBase.ajustLayout(newSize)

            Dim layout As DailyReportViewLayout = DirectCast(Me.layout, DailyReportViewLayout)

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal(newSize As Size)
            MyBase.ajustLayoutFinal(newSize)

            Dim layout As DailyReportViewLayout = DirectCast(Me.layout, DailyReportViewLayout)

            If (Not IsNothing(Me.noLastReportReadyDateMessagePanel)) Then
                Me.noLastReportReadyDateMessagePanel.Location = layout.NoLastReportReadyDateMessagePanel_Location
                Me.noLastReportReadyDateMessagePanel.ajustLayout(layout.NoLastReportReadyDateMessagePanel_Size)
            End If

            Me.reportsToGenerateListControl.ajustLayoutFinal(layout.ReportsToGenerateList_Size)

        End Sub

        Protected Overrides Sub beforeUpdateDatesList()
            MyBase.beforeUpdateDatesList()

            If (Not IsNothing(Me.noLastReportReadyDateMessagePanel)) Then
                Me.removeNoLastReportReadyDateMessagePanel()
            End If

            Me.generateButton.Enabled = False
        End Sub

        Protected Overrides Sub afterUpdateDatesList()
            MyBase.afterUpdateDatesList()

            If (Me.showOnlyReportReadyDatesCheckbox.Checked) Then
                Me.availableDatesListView.showOnlyReportReadyDates()
            End If

            Me.reportsToGenerateListControl.refreshList()

            Me.enableGenerateButtons()
        End Sub

        Private Sub initializeNoLastReportReadyDateMessagePanel()

            Me.noLastReportReadyDateMessagePanel = New Common.UserMessagePanel("Attention!", "Aucune date disponible trouvé.", Constants.UI.Images._64x64.WARNING)

        End Sub

        Private Sub onReportToGenerateChecked(reportType As ReportFile.ReportTypes, checked As Boolean) Handles reportsToGenerateListControl.ItemChecked

            'If (checked) Then
            '    Me.generationController.ReportsToGenerate.Add(reportType)
            'Else
            '    Me.generationController.ReportsToGenerate.Remove(reportType)
            'End If

            Me.enableGenerateButtons()
        End Sub

        Private Sub enableGenerateButtons()

            'If (Me.generationController.ReportsToGenerate.Count > 0) Then

            '    Me.generateButton.Enabled = True
            '    Me.generateButton.Focus()

            'Else
            Me.generateButton.Enabled = True
            'End If

        End Sub

        Private Sub showNoLastReportReadyDateMessagePanel()

            If (IsNothing(Me.noLastReportReadyDateMessagePanel)) Then
                Me.initializeNoLastReportReadyDateMessagePanel()
            End If

            Me.ajustLayoutFinal(Me.Size)

            Me.Controls.Add(Me.noLastReportReadyDateMessagePanel)
            Me.noLastReportReadyDateMessagePanel.BringToFront()
        End Sub

        Private Sub removeNoLastReportReadyDateMessagePanel() Handles noLastReportReadyDateMessagePanel.CloseEvent
            Me.Controls.Remove(Me.noLastReportReadyDateMessagePanel)
        End Sub

        Public Sub showSendReportsByEmailPanel(nbSelectedReportReadyProductionDays As Integer)

            If (nbSelectedReportReadyProductionDays = 1) Then

                Me.sendReportsByEmailPanel = New Common.UserMessagePanel("Rapport généré", "Voulez-vous envoyer une copie de ce rapport par courriel?", Constants.UI.Images._64x64.MAIL, True)
            Else ' nbSelectedReportReadyProductionDays > 1

                Me.sendReportsByEmailPanel = New Common.UserMessagePanel("Rapports générés", "Voulez-vous envoyer une copie de ces rapports par courriel?", Constants.UI.Images._64x64.MAIL, True)
            End If
            Me.sendReportsByEmailPanel.ajustLayout(New Size(350, 120))

            Me.sendReportsByEmailPanel.Location = New Point((Me.Width - Me.sendReportsByEmailPanel.Width) / 2, (Me.Height - Me.sendReportsByEmailPanel.Height) / 2)
            Me.Controls.Add(Me.sendReportsByEmailPanel)
            Me.sendReportsByEmailPanel.BringToFront()
        End Sub

        Private Sub hideSendReportsByEmailPanel() Handles sendReportsByEmailPanel.CloseEvent
            Me.Controls.Remove(Me.sendReportsByEmailPanel)
        End Sub

        Private Sub emailLastGeneratedReport(closeStatus As Common.PopUpMessage.ClosingStatus) Handles sendReportsByEmailPanel.CloseEvent

            If (closeStatus = Common.PopUpMessage.ClosingStatus.Ok) Then ' Andalso if readonly was generated

                Me.generationController.emailLastGeneratedReports()

            End If

        End Sub

        Protected Overloads Overrides Sub beforeShow()
            MyBase.beforeShow()

            Me.hideSendReportsByEmailPanel()
        End Sub

        Public Overrides Sub afterShow()

            Me.updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)

        End Sub

        Public Overrides Sub onHide()
            MyBase.onHide()

        End Sub

        Private Sub toggleShowOnlyReportReadyDates() Handles showOnlyReportReadyDatesCheckbox.CheckedChanged

            If (showOnlyReportReadyDatesCheckbox.Checked) Then

                Me.availableDatesListView.showOnlyReportReadyDates()
                Me.showOnlyReportsReadyDatesCheckboxToolTip.SetToolTip(showOnlyReportReadyDatesCheckbox, SHOW_ONLY_REPORT_READY_CHECKBOX_TOOLTIP_CHECKED)

            Else

                Me.availableDatesListView.showAllDates()
                Me.showOnlyReportsReadyDatesCheckboxToolTip.SetToolTip(showOnlyReportReadyDatesCheckbox, SHOW_ONLY_REPORT_READY_CHECKBOX_TOOLTIP_UNCHECKED)

            End If

            Me.availableDatesListView.selectFirstItem()
            Me.generateButton.Focus()

        End Sub

        Private Sub onDatePickerLayoutChanged(layout As Common.DatePickerPanel.LayoutTypes) Handles datePickerPanel.LayoutChanged

            Select Case layout

                Case Common.DatePickerPanel.LayoutTypes.SingleDatePicker

                    Me.datePickerPanel.removeShortcutButton(Me.thisWeekShortcutButton)
                    Me.datePickerPanel.removeShortcutButton(Me.lastWeekShortcutButton)

                Case Common.DatePickerPanel.LayoutTypes.DoubleDatePicker

                    Me.datePickerPanel.addShortcutButton(Me.thisWeekShortcutButton)
                    Me.datePickerPanel.addShortcutButton(Me.lastWeekShortcutButton)
            End Select

        End Sub

        Private Sub selectTodaysDate() Handles todayShortcutButton.Click

            Me.datePickerPanel.StartDate = Today
            Me.datePickerPanel.EndDate = Today

            updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)

        End Sub

        Private Sub selectYesterdaysDate() Handles yesterdayShortcutButton.Click

            Dim yesterDay = Today.Subtract(TimeSpan.FromDays(1))

            Me.datePickerPanel.StartDate = yesterDay
            Me.datePickerPanel.EndDate = yesterDay

            updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)

        End Sub

        Private Sub selectLastReportReadyDate() Handles lastReportReadyDateShortcutButton.Click

            Me.Cursor = Cursors.AppStarting

            Dim lastReportReadyDate As Date = ProgramController.PersistenceController.findLastReportReadyDate()

            If (lastReportReadyDate.Equals(Date.MaxValue)) Then

                Me.showNoLastReportReadyDateMessagePanel()

                Me.Cursor = Cursors.Default

            Else

                Me.datePickerPanel.StartDate = lastReportReadyDate
                Me.datePickerPanel.EndDate = lastReportReadyDate

                Me.Cursor = Cursors.Default

                updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)
            End If

        End Sub

        Private Sub selectThisWeeksDates() Handles thisWeekShortcutButton.Click

            Me.datePickerPanel.StartDate = Date.Today.Subtract(TimeSpan.FromDays(Date.Today.DayOfWeek))
            Me.datePickerPanel.EndDate = Me.datePickerPanel.StartDate.Add(TimeSpan.FromDays(6))

            updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)

        End Sub

        Private Sub selectLastWeeksDates() Handles lastWeekShortcutButton.Click

            Me.datePickerPanel.StartDate = Date.Today.Subtract(TimeSpan.FromDays(Date.Today.DayOfWeek + 7))
            Me.datePickerPanel.EndDate = Me.datePickerPanel.StartDate.Add(TimeSpan.FromDays(6))

            updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)

        End Sub

        Private Sub startGeneration() Handles generateButton.Click
            Me.generationController.startDailyReportGenerationSequence(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Création de rapports"
            End Get
        End Property

        Public Overrides ReadOnly Property GenerateButtonIcon As Image
            Get
                Return Constants.UI.Images._32x32.MULTIPLE_DAILY_REPORTS
            End Get
        End Property
    End Class

End Namespace
