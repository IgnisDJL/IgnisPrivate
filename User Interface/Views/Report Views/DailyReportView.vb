Namespace UI

    Public Class DailyReportView
        Inherits ReportViewTemplate

        ' Constants
        ' #language
        'Private Shared ReadOnly SHOW_ONLY_REPORT_READY_CHECKBOX_TOOLTIP_CHECKED As String = "Afficher toutes les dates"
        'Private Shared ReadOnly SHOW_ONLY_REPORT_READY_CHECKBOX_TOOLTIP_UNCHECKED As String = "Afficher les date prêtes pour" & Environment.NewLine & "la génération de rapport"

        'Private Shared ReadOnly TODAY_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne la date d'aujourd'hui"
        'Private Shared ReadOnly YESTERDAY_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne la date de hier"
        'Private Shared ReadOnly LAST_REPORT_READY_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne la dernière date prête" & Environment.NewLine & "pour la génération de rapports"
        'Private Shared ReadOnly THIS_WEEK_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne les dates de cette semaine"
        'Private Shared ReadOnly LAST_WEEK_SHORTCUT_BUTTON_TOOLTIP_TEXT As String = "Sélectionne les dates de la semaine dernière"

        ' Components
        'Private WithEvents showOnlyReportReadyDatesCheckbox As CheckBox
        'Private showOnlyReportsReadyDatesCheckboxToolTip As ToolTip

        '' --- Shortcut buttons
        'Private WithEvents todayShortcutButton As Button
        'Private WithEvents yesterdayShortcutButton As Button
        'Private WithEvents lastReportReadyDateShortcutButton As Button
        'Private WithEvents thisWeekShortcutButton As Button
        'Private WithEvents lastWeekShortcutButton As Button

        'Private shortcutButtonsToolTip As ToolTip

        Private WithEvents sendReportsByEmailPanel As Common.UserMessagePanel
        Private WithEvents erreurHoraireDateMessagePanel As Common.UserMessagePanel
        Friend WithEvents label_identification As System.Windows.Forms.Label
        Friend WithEvents cbox_identification As System.Windows.Forms.ComboBox
        Friend WithEvents label_horaire As System.Windows.Forms.Label
        Friend WithEvents label_debut As System.Windows.Forms.Label
        Friend WithEvents label_fin As System.Windows.Forms.Label
        Friend WithEvents dtp_dateDebut As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtp_dateFin As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtp_timeDebut As System.Windows.Forms.DateTimePicker
        Friend WithEvents dtp_timeFin As System.Windows.Forms.DateTimePicker
        Friend WithEvents panel_nouveauRapport As System.Windows.Forms.Panel
        Friend WithEvents label_presentation As System.Windows.Forms.Label

        ' Attributes
        Private generationController As ReportGenerationController_1

        Public Sub New()
            MyBase.New()

            Me.generationController = ProgramController.ReportGenerationController

            Me.layout = New DailyReportViewLayout

            Me.initializeComponents()
            Me.fillOperatorComboBox()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()
            Me.InitializeComponent()

        End Sub

        Protected Overloads Overrides Sub ajustLayout(newSize As Size)
            MyBase.ajustLayout(newSize)

            Dim layout As DailyReportViewLayout = DirectCast(Me.layout, DailyReportViewLayout)

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal(newSize As Size)
            MyBase.ajustLayoutFinal(newSize)

            Dim layout As DailyReportViewLayout = DirectCast(Me.layout, DailyReportViewLayout)

            If (Not IsNothing(Me.erreurHoraireDateMessagePanel)) Then
                Me.erreurHoraireDateMessagePanel.Location = layout.erreurHoraireDateMessagePanel_Location
                Me.erreurHoraireDateMessagePanel.ajustLayout(layout.erreurHoraireDateMessagePanel_Size)
            End If

            'Me.reportsToGenerateListControl.ajustLayoutFinal(layout.ReportsToGenerateList_Size)

        End Sub

        Protected Sub beforeUpdateDatesList()


            If (Not IsNothing(Me.erreurHoraireDateMessagePanel)) Then
                Me.removeErreurHoraireDateMessagePanel()
            End If

            Me.generateButton.Enabled = False
        End Sub

        'Private Sub onReportToGenerateChecked(reportType As ReportFile.ReportTypes, checked As Boolean) Handles reportsToGenerateListControl.ItemChecked

        '    'If (checked) Then
        '    '    Me.generationController.ReportsToGenerate.Add(reportType)
        '    'Else
        '    '    Me.generationController.ReportsToGenerate.Remove(reportType)
        '    'End If

        '    Me.enableGenerateButtons()
        'End Sub

        Private Sub setEnableGenerateButtons(enable as Boolean)
            Me.generateButton.Enabled = enable
        End Sub

        Private Sub showErreurHoraireDateMessagePanel( limiteSuperieur As Boolean)
            If (limiteSuperieur) then
                Me.erreurHoraireDateMessagePanel = New Common.UserMessagePanel("Attention!", "La période maximale est de 24 heures.", Constants.UI.Images._64x64.WARNING)
            else            
                Me.erreurHoraireDateMessagePanel = New Common.UserMessagePanel("Attention!", "La période minimale est de 1 heure.", Constants.UI.Images._64x64.WARNING)
            end if
            

            Me.ajustLayoutFinal(Me.Size)

            Me.Controls.Add(Me.erreurHoraireDateMessagePanel)
            setEnableGenerateButtons(false)
            Me.erreurHoraireDateMessagePanel.BringToFront()
        End Sub

        Private Sub removeErreurHoraireDateMessagePanel() Handles erreurHoraireDateMessagePanel.CloseEvent
            Me.Controls.Remove(Me.erreurHoraireDateMessagePanel)
            setEnableGenerateButtons(true)
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

            'Me.updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)

        End Sub

        Public Overrides Sub onHide()
            MyBase.onHide()

        End Sub


        Private Sub startGeneration() Handles generateButton.Click
            dim dateDebut as date
            dim dateFin as date

            dateDebut = dtp_dateDebut.Value.Date + TimeSpan.FromHours(dtp_timeDebut.Value.TimeOfDay.Hours) + TimeSpan.FromHours(dtp_timeDebut.Value.TimeOfDay.Minutes)
            dateFin = dtp_dateFin.Value.Date + TimeSpan.FromHours(dtp_timeFin.Value.TimeOfDay.Hours) + TimeSpan.FromHours(dtp_timeFin.Value.TimeOfDay.Minutes)

            if(dateFin.Subtract(dateDebut) > TimeSpan.FromHours(24)) then
                showErreurHoraireDateMessagePanel(True)
            else if (dateFin.Subtract(dateDebut) < TimeSpan.FromHours(1))
                showErreurHoraireDateMessagePanel(False)
            else 
                
                Me.generationController.startDailyReportGenerationSequence(dateDebut, dateFin, cbox_identification.SelectedItem.ToString)
            End If

        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Création de rapports"
            End Get 
        End Property

        Public ReadOnly Property GenerateButtonIcon As Image
            Get
                Return Constants.UI.Images._32x32.MULTIPLE_DAILY_REPORTS
            End Get
        End Property

        private Sub fillOperatorComboBox()
               cbox_identification.Items.Add(FactoryOperator.DEFAULT_OPERATOR.ToString)
            For Each _operator As FactoryOperator In ProgramController.SettingsControllers.UsineSettingsController.getOperators 
                cbox_identification.Items.Add(_operator.ToString)
            Next

            cbox_identification.SelectedIndex = 0
        End Sub


        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DailyReportView))
            Me.label_identification = New System.Windows.Forms.Label()
            Me.cbox_identification = New System.Windows.Forms.ComboBox()
            Me.label_horaire = New System.Windows.Forms.Label()
            Me.label_debut = New System.Windows.Forms.Label()
            Me.label_fin = New System.Windows.Forms.Label()
            Me.dtp_dateDebut = New System.Windows.Forms.DateTimePicker()
            Me.dtp_dateFin = New System.Windows.Forms.DateTimePicker()
            Me.dtp_timeDebut = New System.Windows.Forms.DateTimePicker()
            Me.dtp_timeFin = New System.Windows.Forms.DateTimePicker()
            Me.panel_nouveauRapport = New System.Windows.Forms.Panel()
            Me.label_presentation = New System.Windows.Forms.Label()
            Me.panel_nouveauRapport.SuspendLayout()
            Me.SuspendLayout()
            '
            'label_identification
            '
            Me.label_identification.AutoSize = True
            Me.label_identification.Location = New System.Drawing.Point(25, 50)
            Me.label_identification.Name = "label_identification"
            Me.label_identification.Size = New System.Drawing.Size(73, 13)
            Me.label_identification.TabIndex = 0
            Me.label_identification.Text = "Identification :"
            '
            'cbox_identification
            '
            Me.cbox_identification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbox_identification.FormattingEnabled = True
            Me.cbox_identification.Location = New System.Drawing.Point(150, 50)
            Me.cbox_identification.Name = "cbox_identification"
            Me.cbox_identification.Size = New System.Drawing.Size(500, 21)
            Me.cbox_identification.TabIndex = 0
            '
            'label_horaire
            '
            Me.label_horaire.AutoSize = True
            Me.label_horaire.Location = New System.Drawing.Point(65, 115)
            Me.label_horaire.Name = "label_horaire"
            Me.label_horaire.Size = New System.Drawing.Size(87, 13)
            Me.label_horaire.TabIndex = 0
            Me.label_horaire.Text = "Horaire de travail"
            '
            'label_debut
            '
            Me.label_debut.AutoSize = True
            Me.label_debut.Location = New System.Drawing.Point(65, 150)
            Me.label_debut.Name = "label_debut"
            Me.label_debut.Size = New System.Drawing.Size(42, 13)
            Me.label_debut.TabIndex = 0
            Me.label_debut.Text = "Début :"
            '
            'label_fin
            '
            Me.label_fin.AutoSize = True
            Me.label_fin.Location = New System.Drawing.Point(65, 200)
            Me.label_fin.Name = "label_fin"
            Me.label_fin.Size = New System.Drawing.Size(27, 13)
            Me.label_fin.TabIndex = 0
            Me.label_fin.Text = "Fin :"
            '
            'dtp_dateDebut
            '
            Me.dtp_dateDebut.CustomFormat = "dd/MM/yyyy"
            Me.dtp_dateDebut.Location = New System.Drawing.Point(140, 150)
            Me.dtp_dateDebut.Name = "dtp_dateDebut"
            Me.dtp_dateDebut.Size = New System.Drawing.Size(200, 20)
            Me.dtp_dateDebut.TabIndex = 0
            '
            'dtp_dateFin
            '
            Me.dtp_dateFin.CustomFormat = "dd/MM/yyyy"
            Me.dtp_dateFin.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.dtp_dateFin.Location = New System.Drawing.Point(140, 200)
            Me.dtp_dateFin.Name = "dtp_dateFin"
            Me.dtp_dateFin.Size = New System.Drawing.Size(200, 20)
            Me.dtp_dateFin.TabIndex = 0
            '
            'dtp_timeDebut
            '
            Me.dtp_timeDebut.CustomFormat = "HH'h'mm"
            Me.dtp_timeDebut.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dtp_timeDebut.Location = New System.Drawing.Point(370, 150)
            Me.dtp_timeDebut.Name = "dtp_timeDebut"
            Me.dtp_timeDebut.ShowUpDown = True
            Me.dtp_timeDebut.Size = New System.Drawing.Size(80, 20)
            Me.dtp_timeDebut.TabIndex = 0
            '
            'dtp_timeFin
            '
            Me.dtp_timeFin.CustomFormat = "HH'h'mm"
            Me.dtp_timeFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
            Me.dtp_timeFin.Location = New System.Drawing.Point(370, 200)
            Me.dtp_timeFin.Name = "dtp_timeFin"
            Me.dtp_timeFin.ShowUpDown = True
            Me.dtp_timeFin.Size = New System.Drawing.Size(80, 20)
            Me.dtp_timeFin.TabIndex = 0
            '
            'panel_nouveauRapport
            '
            Me.panel_nouveauRapport.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.panel_nouveauRapport.AutoSize = True
            Me.panel_nouveauRapport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panel_nouveauRapport.Controls.Add(Me.label_identification)
            Me.panel_nouveauRapport.Controls.Add(Me.cbox_identification)
            Me.panel_nouveauRapport.Controls.Add(Me.label_horaire)
            Me.panel_nouveauRapport.Controls.Add(Me.label_debut)
            Me.panel_nouveauRapport.Controls.Add(Me.label_fin)
            Me.panel_nouveauRapport.Controls.Add(Me.dtp_dateDebut)
            Me.panel_nouveauRapport.Controls.Add(Me.dtp_dateFin)
            Me.panel_nouveauRapport.Controls.Add(Me.dtp_timeDebut)
            Me.panel_nouveauRapport.Controls.Add(Me.dtp_timeFin)
            Me.panel_nouveauRapport.Location = New System.Drawing.Point(70, -75)
            Me.panel_nouveauRapport.Name = "panel_nouveauRapport"
            Me.panel_nouveauRapport.Padding = New System.Windows.Forms.Padding(25, 0, 25, 25)
            Me.panel_nouveauRapport.Size = New System.Drawing.Size(682, 277)
            Me.panel_nouveauRapport.TabIndex = 0
            '
            'label_presentation
            '
            Me.label_presentation.AutoSize = True
            Me.label_presentation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.label_presentation.Location = New System.Drawing.Point(70, 30)
            Me.label_presentation.MaximumSize = New System.Drawing.Size(682, 500)
            Me.label_presentation.Name = "label_presentation"
            Me.label_presentation.Size = New System.Drawing.Size(676, 41)
            Me.label_presentation.TabIndex = 0
            Me.label_presentation.Text = resources.GetString("label_presentation.Text")
            '
            'DailyReportView
            '
            Me.Controls.Add(Me.panel_nouveauRapport)
            Me.Controls.Add(Me.label_presentation)
            Me.panel_nouveauRapport.ResumeLayout(False)
            Me.panel_nouveauRapport.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
    End Class

End Namespace
