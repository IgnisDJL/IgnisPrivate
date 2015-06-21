Namespace UI

    Public Class ManualDataStepView
        Inherits GenerationStepView

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "Données complémentaires"

        Private Shared ReadOnly FIELDS_HEIGHT As Integer = 30
        Private Shared ReadOnly OPERATOR_LABEL_WIDTH As Integer = 200
        Private Shared ReadOnly REJECTED_PERCENTAGE_LABELS_WIDTH As Integer = 125
        Private Shared ReadOnly PERCENTAGE_DIFF_LABEL_WIDTH As Integer = 170

        Private Shared ReadOnly SKIP_WARNING_MESSAGE_SIZE As Size = New Size(400, 145)

        ' Components
        Private dateLabel As Label
        Private dailyQuantityAndTimesLabel As Label

        ' --- Mandatory

        Private silosLabel As Label
        Private WithEvents siloQuantityAtStartField As ManualDataQuantityField
        Private WithEvents siloQuantityAtEndField As ManualDataQuantityField

        Private rejectedMaterialsLabel As Label
        Private WithEvents rejectedMixQuantityField As ManualDataQuantityField
        Private WithEvents rejectedAggregatesQuantityField As ManualDataQuantityField
        Private WithEvents rejectedFillerQuantityField As ManualDataQuantityField
        Private WithEvents rejectedRecycledQuantityField As ManualDataQuantityField
        Private rejectedMixPercentageLabel As Label
        Private rejectedAggregatesPercentageLabel As Label
        Private rejectedFillerPercentageLabel As Label
        Private rejectedRecycledPercentageLabel As Label

        ' --- Optional
        Private weightStationLabel As Label
        Private WithEvents weightedQuantityField As ManualDataQuantityField
        Private percentageDiffLabel As Label

        Private fuelLabel As Label
        Private WithEvents fuelQuantityAtStart1Field As ManualDataQuantityField
        Private WithEvents fuelQuantityAtEnd1Field As ManualDataQuantityField
        Private WithEvents fuelQuantityAtStart2Field As ManualDataQuantityField
        Private WithEvents fuelQuantityAtEnd2Field As ManualDataQuantityField


        ' --- Buttons

        Private WithEvents nextButton As Common.NextButton
        Private WithEvents skipButton As Button
        ' settings button for the operators?

        Private WithEvents skipWarningMessagePanel As Common.UserMessagePanel

        ' Attributes
        Private currentManualData As ManualData

        Private mandatoryFields As List(Of Control)
        Private optionalFields As List(Of Control)

        Private showOptionalFields As Boolean = True

        Private temporaryOperator As FactoryOperator

        Public Sub New()
            MyBase.New()

            Me.mandatoryFields = New List(Of Control)
            Me.optionalFields = New List(Of Control)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            ' Top section
            Me.dateLabel = New Label
            Me.dateLabel.AutoSize = False
            Me.dateLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.dateLabel.Font = Constants.UI.Fonts.BIGGER_DEFAULT_FONT_BOLD

            Me.dailyQuantityAndTimesLabel = New Label
            Me.dailyQuantityAndTimesLabel.AutoSize = False
            Me.dailyQuantityAndTimesLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.dailyQuantityAndTimesLabel.ForeColor = Constants.UI.Colors.DARK_GREY



            ' Silos section
            Me.silosLabel = New Label
            Me.silosLabel.AutoSize = False
            Me.silosLabel.ForeColor = Constants.UI.Colors.DARK_GREY
            Me.silosLabel.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            Me.silosLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.silosLabel.Text = "Quantité disponible en silo(s)"

            Me.siloQuantityAtStartField = New ManualDataQuantityField("Début de journée", ManualData.MASS_UNIT.SYMBOL, True)
            Me.siloQuantityAtEndField = New ManualDataQuantityField("Fin de journée", ManualData.MASS_UNIT.SYMBOL, True)

            ' Rejected materials section
            Me.rejectedMaterialsLabel = New Label
            Me.rejectedMaterialsLabel.AutoSize = False
            Me.rejectedMaterialsLabel.ForeColor = Constants.UI.Colors.DARK_GREY
            Me.rejectedMaterialsLabel.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            Me.rejectedMaterialsLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.rejectedMaterialsLabel.Text = "Rejets de matériaux"

            Me.rejectedMixQuantityField = New ManualDataQuantityField("Enrobés", ManualData.MASS_UNIT.SYMBOL, True)
            Me.rejectedAggregatesQuantityField = New ManualDataQuantityField("Granulats", ManualData.MASS_UNIT.SYMBOL, True)
            Me.rejectedFillerQuantityField = New ManualDataQuantityField("Filler", ManualData.MASS_UNIT.SYMBOL, True)
            Me.rejectedRecycledQuantityField = New ManualDataQuantityField("GBR (Recyclé)", ManualData.MASS_UNIT.SYMBOL, True)

            Me.rejectedMixPercentageLabel = New Label
            Me.rejectedMixPercentageLabel.AutoSize = False
            Me.rejectedMixPercentageLabel.TextAlign = ContentAlignment.MiddleRight
            Me.rejectedMixPercentageLabel.ForeColor = Constants.UI.Colors.LIGHT_GREY

            Me.rejectedAggregatesPercentageLabel = New Label
            Me.rejectedAggregatesPercentageLabel.AutoSize = False
            Me.rejectedAggregatesPercentageLabel.TextAlign = ContentAlignment.MiddleRight
            Me.rejectedAggregatesPercentageLabel.ForeColor = Constants.UI.Colors.LIGHT_GREY

            Me.rejectedFillerPercentageLabel = New Label
            Me.rejectedFillerPercentageLabel.AutoSize = False
            Me.rejectedFillerPercentageLabel.TextAlign = ContentAlignment.MiddleRight
            Me.rejectedFillerPercentageLabel.ForeColor = Constants.UI.Colors.LIGHT_GREY

            Me.rejectedRecycledPercentageLabel = New Label
            Me.rejectedRecycledPercentageLabel.AutoSize = False
            Me.rejectedRecycledPercentageLabel.TextAlign = ContentAlignment.MiddleRight
            Me.rejectedRecycledPercentageLabel.ForeColor = Constants.UI.Colors.LIGHT_GREY

            ' Optional
            ' Weight station section
            Me.weightStationLabel = New Label
            Me.weightStationLabel.AutoSize = False
            Me.weightStationLabel.ForeColor = Constants.UI.Colors.DARK_GREY
            Me.weightStationLabel.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            Me.weightStationLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.weightStationLabel.Text = "Poste de pesée"

            Me.weightedQuantityField = New ManualDataQuantityField("Quantité pesée", ManualData.MASS_UNIT.SYMBOL)

            Me.percentageDiffLabel = New Label
            Me.percentageDiffLabel.AutoSize = False
            Me.percentageDiffLabel.TextAlign = ContentAlignment.MiddleRight
            Me.percentageDiffLabel.ForeColor = Constants.UI.Colors.LIGHT_GREY

            ' Fuel section
            Me.fuelLabel = New Label
            Me.fuelLabel.AutoSize = False
            Me.fuelLabel.ForeColor = Constants.UI.Colors.DARK_GREY
            Me.fuelLabel.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            Me.fuelLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.fuelLabel.Text = "Carburants"

            With XmlSettings.Settings.instance.Usine.FuelsInfo

                Me.fuelQuantityAtStart1Field = New ManualDataQuantityField("Quantité " & .FUEL_1_NAME & " (Début de journée)", .FUEL_1_UNIT)
                Me.fuelQuantityAtEnd1Field = New ManualDataQuantityField("Quantité " & .FUEL_1_NAME & " (Fin de journée)", .FUEL_1_UNIT)
                Me.fuelQuantityAtStart2Field = New ManualDataQuantityField("Quantité " & .FUEL_2_NAME & " (Début de journée)", .FUEL_2_UNIT)
                Me.fuelQuantityAtEnd2Field = New ManualDataQuantityField("Quantité " & .FUEL_2_NAME & " (Fin de journée)", .FUEL_2_UNIT)
            End With

          

            Me.nextButton = New Common.NextButton

            Me.skipButton = New Button
            Me.skipButton.TextAlign = ContentAlignment.MiddleCenter
            Me.skipButton.Text = "Étape suivante"
            Me.skipButton.Font = Constants.UI.Fonts.SMALLER_DEFAULT_FONT

            Me.OtherButtons.Add(Me.nextButton)
            Me.OtherButtons.Add(Me.skipButton)

            ' The order in which the components are added is important for the z-index
            ' Top Section
            Me.Controls.Add(dateLabel)
            Me.Controls.Add(dailyQuantityAndTimesLabel)

            ' Silos Section
            Me.Controls.Add(silosLabel)
            Me.Controls.Add(siloQuantityAtStartField)
            Me.Controls.Add(siloQuantityAtEndField)

            ' Rejected Materials Section
            Me.Controls.Add(rejectedMaterialsLabel)
            Me.Controls.Add(rejectedMixPercentageLabel)
            Me.Controls.Add(rejectedAggregatesPercentageLabel)
            Me.Controls.Add(rejectedFillerPercentageLabel)
            Me.Controls.Add(rejectedRecycledPercentageLabel)
            Me.Controls.Add(rejectedMixQuantityField)
            Me.Controls.Add(rejectedAggregatesQuantityField)
            Me.Controls.Add(rejectedFillerQuantityField)
            Me.Controls.Add(rejectedRecycledQuantityField)

            ' Weight Station Section
            Me.Controls.Add(weightStationLabel)
            Me.Controls.Add(percentageDiffLabel)
            Me.Controls.Add(weightedQuantityField)


            ' Fuel Section
            Me.Controls.Add(fuelLabel)
            Me.Controls.Add(fuelQuantityAtStart1Field)
            Me.Controls.Add(fuelQuantityAtEnd1Field)
            Me.Controls.Add(fuelQuantityAtStart2Field)
            Me.Controls.Add(fuelQuantityAtEnd2Field)

            Me.mandatoryFields.Add(silosLabel)
            Me.mandatoryFields.Add(siloQuantityAtStartField)
            Me.mandatoryFields.Add(siloQuantityAtEndField)

            Me.mandatoryFields.Add(rejectedMaterialsLabel)
            Me.mandatoryFields.Add(rejectedMixQuantityField)
            Me.mandatoryFields.Add(rejectedAggregatesQuantityField)
            Me.mandatoryFields.Add(rejectedFillerQuantityField)
            Me.mandatoryFields.Add(rejectedRecycledQuantityField)

            ' Optionnal fields
            Me.optionalFields.Add(weightStationLabel)
            Me.optionalFields.Add(weightedQuantityField)

            Me.optionalFields.Add(fuelLabel)
            Me.optionalFields.Add(fuelQuantityAtStart1Field)
            Me.optionalFields.Add(fuelQuantityAtEnd1Field)
            Me.optionalFields.Add(fuelQuantityAtStart2Field)
            Me.optionalFields.Add(fuelQuantityAtEnd2Field)


            Me.siloQuantityAtStartField.TabIndex = 2
            Me.siloQuantityAtEndField.TabIndex = 3
            Me.rejectedMixQuantityField.TabIndex = 4
            Me.rejectedAggregatesQuantityField.TabIndex = 5
            Me.rejectedFillerQuantityField.TabIndex = 6
            Me.rejectedRecycledQuantityField.TabIndex = 7
            Me.weightedQuantityField.TabIndex = 8
            Me.fuelQuantityAtStart1Field.TabIndex = 11
            Me.fuelQuantityAtEnd1Field.TabIndex = 12
            Me.fuelQuantityAtStart2Field.TabIndex = 13
            Me.fuelQuantityAtEnd2Field.TabIndex = 14
            Me.nextButton.TabIndex = 19
            Me.skipButton.TabIndex = 20
            Me.cancelButton.TabIndex = 21
            Me.backButton.TabIndex = 22
        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Me.AutoScroll = False

            ' Date label and daily quantity and times label
            Me.dateLabel.Location = New Point(ReportGenerationFrameLayout.LOCATION_START_X, 0)
            Me.dateLabel.Size = New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT)

            Me.dailyQuantityAndTimesLabel.Location = New Point(ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT)
            Me.dailyQuantityAndTimesLabel.Size = New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT)


            ' Mandatory fields
            Dim nbFields As Integer = 3  ' +3 for date label, the daily quantity and time labels and the operator label
            For Each field As Control In Me.mandatoryFields

                field.Location = New Point(ReportGenerationFrameLayout.LOCATION_START_X, ReportGenerationFrameLayout.LOCATION_START_Y - 5 + nbFields * (FIELDS_HEIGHT + 5))

                If (TypeOf field Is ManualDataField) Then
                    DirectCast(field, ManualDataField).ajustLayout(New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT))
                Else
                    field.Size = New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT)
                End If

                nbFields += 1
            Next

            ' Percentage label
            Me.rejectedMixPercentageLabel.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ManualDataQuantityField.UNIT_LABEL_WIDTH - ManualDataQuantityField.QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - 2 * FIELDS_HEIGHT - REJECTED_PERCENTAGE_LABELS_WIDTH, Me.rejectedMixQuantityField.Location.Y)
            Me.rejectedMixPercentageLabel.Size = New Size(REJECTED_PERCENTAGE_LABELS_WIDTH, FIELDS_HEIGHT)

            Me.rejectedAggregatesPercentageLabel.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ManualDataQuantityField.UNIT_LABEL_WIDTH - ManualDataQuantityField.QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - 2 * FIELDS_HEIGHT - REJECTED_PERCENTAGE_LABELS_WIDTH, Me.rejectedAggregatesQuantityField.Location.Y)
            Me.rejectedAggregatesPercentageLabel.Size = New Size(REJECTED_PERCENTAGE_LABELS_WIDTH, FIELDS_HEIGHT)

            Me.rejectedFillerPercentageLabel.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ManualDataQuantityField.UNIT_LABEL_WIDTH - ManualDataQuantityField.QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - 2 * FIELDS_HEIGHT - REJECTED_PERCENTAGE_LABELS_WIDTH, Me.rejectedFillerQuantityField.Location.Y)
            Me.rejectedFillerPercentageLabel.Size = New Size(REJECTED_PERCENTAGE_LABELS_WIDTH, FIELDS_HEIGHT)

            Me.rejectedRecycledPercentageLabel.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ManualDataQuantityField.UNIT_LABEL_WIDTH - ManualDataQuantityField.QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - 2 * FIELDS_HEIGHT - REJECTED_PERCENTAGE_LABELS_WIDTH, Me.rejectedRecycledQuantityField.Location.Y)
            Me.rejectedRecycledPercentageLabel.Size = New Size(REJECTED_PERCENTAGE_LABELS_WIDTH, FIELDS_HEIGHT)



            ' Optional Fields
            Dim location As Point
            For Each field As Control In Me.optionalFields

                field.Visible = showOptionalFields

                If (showOptionalFields) Then

                    location = New Point(ReportGenerationFrameLayout.LOCATION_START_X, ReportGenerationFrameLayout.LOCATION_START_Y + nbFields * (FIELDS_HEIGHT + 5))

                    If (Me.DisplayRectangle.Contains(location)) Then
                        field.Location = New Point(location)

                        If (TypeOf field Is ManualDataField) Then
                            DirectCast(field, ManualDataField).ajustLayout(New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT))
                        Else
                            field.Size = New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT)
                        End If

                    End If

                    nbFields += 1

                End If

            Next

            If (showOptionalFields) Then
                Me.percentageDiffLabel.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ManualDataQuantityField.UNIT_LABEL_WIDTH - ManualDataQuantityField.QUANTITY_FIELD_WIDTH_WITHOUT_UNKNOWN_BUTTON - FIELDS_HEIGHT - PERCENTAGE_DIFF_LABEL_WIDTH, Me.weightedQuantityField.Location.Y)
                Me.percentageDiffLabel.Size = New Size(PERCENTAGE_DIFF_LABEL_WIDTH, FIELDS_HEIGHT)
            End If

            ' Next Button (In buttons panel)
            Me.nextButton.Location = New Point(Me.Width - ReportGenerationFrameLayout.LOCATION_START_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me.nextButton.Size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

            ' Skip Button (In buttons panel)
            Me.skipButton.Location = New Point(Me.nextButton.Location.X - ReportGenerationFrameLayout.SPACE_BETWEEN_CONTROLS_X - ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.BUTTONS_PANEL_LOCATION_START_Y)
            Me.skipButton.Size = New Size(ReportGenerationFrameLayout.CONTROL_BUTTONS_WIDTH, ReportGenerationFrameLayout.CONTROL_BUTTONS_HEIGHT)

        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim nbFields As Integer = Me.mandatoryFields.Count + 3 ' +3 for date label, the daily quantity and time labels and the operator label

            Dim location As Point

            Me.percentageDiffLabel.Visible = showOptionalFields

            For Each field As Control In Me.optionalFields

                field.Visible = showOptionalFields

                location = New Point(UI.LayoutManager.LOCATION_START_X, ReportGenerationFrameLayout.LOCATION_START_Y + nbFields * (FIELDS_HEIGHT + 5) - Me.VerticalScroll.Value)


                If (showOptionalFields) Then

                    field.Location = New Point(location)

                    If (TypeOf field Is ManualDataField) Then
                        DirectCast(field, ManualDataField).ajustLayout(New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT))
                    Else
                        field.Size = New Size(Me.Width - 2 * ReportGenerationFrameLayout.LOCATION_START_X, FIELDS_HEIGHT)
                    End If

                    nbFields += 1

                End If

            Next

            If (Not IsNothing(Me.skipWarningMessagePanel)) Then
                Me.skipWarningMessagePanel.Location = New Point(Me.Width / 2 - SKIP_WARNING_MESSAGE_SIZE.Width / 2, Me.Height / 2 - SKIP_WARNING_MESSAGE_SIZE.Height / 2)
            End If

            Me.AutoScroll = True
        End Sub

        Public Sub showManualData(data As ManualData)

            Me.currentManualData = data
            updateFields()


        End Sub

        Private Sub updateFields()

            If (currentManualData.DATE_1.Date = currentManualData.DATE_2.Date) Then
                Me.dateLabel.Text = Me.currentManualData.DATE_1.ToString("dddd d MMMM", New Globalization.CultureInfo("fr-FR"))
            Else
                Me.dateLabel.Text = Me.currentManualData.DATE_1.ToString("dddd d MMMM", New Globalization.CultureInfo("fr-FR"))
                Me.dateLabel.Text += "   au   "
                Me.dateLabel.Text += Me.currentManualData.DATE_2.ToString("dddd d MMMM", New Globalization.CultureInfo("fr-FR"))
            End If

            Me.dailyQuantityAndTimesLabel.Text = Me.currentManualData.PRODUCED_QUANTITY & " T produites entre " & _
                                                 Me.currentManualData.PRODUCTION_START_TIME.ToString("H:mm") & " et " & _
                                                 Me.currentManualData.PRODUCTION_END_TIME.ToString("H:mm")

            Me.siloQuantityAtStartField.Value = Me.currentManualData.SILO_QUANTITY_AT_START
            Me.siloQuantityAtEndField.Value = Me.currentManualData.SILO_QUANTITY_AT_END

            Me.rejectedMixQuantityField.Value = Me.currentManualData.REJECTED_MIX_QUANTITY
            Me.rejectedAggregatesQuantityField.Value = Me.currentManualData.REJECTED_AGGREGATES_QUANTITY
            Me.rejectedFillerQuantityField.Value = Me.currentManualData.REJECTED_FILLER_QUANTITY
            Me.rejectedRecycledQuantityField.Value = Me.currentManualData.REJECTED_RECYCLED_QUANTITY

            Me.weightedQuantityField.Value = Me.currentManualData.WEIGHTED_QUANTITY



            Me.fuelQuantityAtStart1Field.Value = Me.currentManualData.FUEL_QUANTITY_AT_START_1
            Me.fuelQuantityAtEnd1Field.Value = Me.currentManualData.FUEL_QUANTITY_AT_END_1
            Me.fuelQuantityAtStart2Field.Value = Me.currentManualData.FUEL_QUANTITY_AT_START_2
            Me.fuelQuantityAtEnd2Field.Value = Me.currentManualData.FUEL_QUANTITY_AT_END_2


            Me.nextButton.Enabled = Me.currentManualData.isComplete

        End Sub

        Protected Overloads Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub afterShow()

            If (Not IsNothing(Me.skipWarningMessagePanel)) Then
                Me.Controls.Remove(Me.skipWarningMessagePanel)
            End If

        End Sub

        Public Overrides Sub onHide()

            Me.currentManualData = Nothing

        End Sub

        Protected Overrides Sub goBack()

            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.showPreviousManualData()

        End Sub

        Private Sub goNext() Handles nextButton.Click

            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.showNextManualData()

        End Sub

        Protected Overrides Sub cancel()

            Me.Controls.Remove(Me.skipWarningMessagePanel)

            ProgramController.ReportGenerationController.cancelGeneration()
            Me.currentManualData = Nothing
        End Sub

        Private Sub showSkipWarning() Handles skipButton.Click

            If (IsNothing(Me.skipWarningMessagePanel)) Then
                Me.initializeSkipWarningMessage()
            End If

            Me.ajustLayoutFinal()

            Me.Controls.Add(Me.skipWarningMessagePanel)
            Me.skipWarningMessagePanel.BringToFront()

            Me.skipWarningMessagePanel.Focus()
        End Sub

        Private Sub skipStep(closeStatus As Common.PopUpMessage.ClosingStatus) Handles skipWarningMessagePanel.CloseEvent

            If (closeStatus = Common.PopUpMessage.ClosingStatus.Ok) Then

                ProgramController.ReportGenerationController.skipManualDataStep()

            End If

            Me.Controls.Remove(Me.skipWarningMessagePanel)
        End Sub

        Private Sub initializeSkipWarningMessage()

            Me.skipWarningMessagePanel = New Common.UserMessagePanel("Avertissement!", "Aucune donnée supplémentaire ne sera sauvegardée." & Environment.NewLine & "Changer d'étape quand même?", Constants.UI.Images._64x64.WARNING, True)

            Me.skipWarningMessagePanel.ajustLayout(SKIP_WARNING_MESSAGE_SIZE)
        End Sub

        Private Sub handleQuantityValuesChanged(field As ManualDataField) Handles siloQuantityAtStartField.ValueChangedEvent, _
                                                                                  siloQuantityAtEndField.ValueChangedEvent, _
                                                                                  rejectedMixQuantityField.ValueChangedEvent, _
                                                                                  rejectedAggregatesQuantityField.ValueChangedEvent, _
                                                                                  rejectedFillerQuantityField.ValueChangedEvent, _
                                                                                  rejectedRecycledQuantityField.ValueChangedEvent, _
                                                                                  weightedQuantityField.ValueChangedEvent, _
                                                                                  fuelQuantityAtStart1Field.ValueChangedEvent, _
                                                                                  fuelQuantityAtEnd1Field.ValueChangedEvent, _
                                                                                  fuelQuantityAtStart2Field.ValueChangedEvent, _
                                                                                  fuelQuantityAtEnd2Field.ValueChangedEvent
            'drumsHourCounterAtStartField.ValueChangedEvent
            'drumsHourCounterAtEndField.ValueChangedEvent, _
            'boilerHourCounterAtStartField.ValueChangedEvent, _
            'boilerHourCounterAtEndField.ValueChangedEvent
            Try

                If (field.Equals(Me.siloQuantityAtStartField)) Then ' Silo at start

                    Me.currentManualData.SILO_QUANTITY_AT_START = Me.siloQuantityAtStartField.Value

                ElseIf (field.Equals(Me.siloQuantityAtEndField)) Then ' Silo at end

                    Me.currentManualData.SILO_QUANTITY_AT_END = Me.siloQuantityAtEndField.Value

                ElseIf (field.Equals(Me.rejectedMixQuantityField)) Then ' Rejected mix

                    Me.currentManualData.REJECTED_MIX_QUANTITY = Me.rejectedMixQuantityField.Value

                ElseIf (field.Equals(Me.rejectedAggregatesQuantityField)) Then ' Rejected Aggs

                    Me.currentManualData.REJECTED_AGGREGATES_QUANTITY = Me.rejectedAggregatesQuantityField.Value

                ElseIf (field.Equals(Me.rejectedFillerQuantityField)) Then ' Rejected Filler

                    Me.currentManualData.REJECTED_FILLER_QUANTITY = Me.rejectedFillerQuantityField.Value

                ElseIf (field.Equals(Me.rejectedRecycledQuantityField)) Then ' Rejected Recycled

                    Me.currentManualData.REJECTED_RECYCLED_QUANTITY = Me.rejectedRecycledQuantityField.Value

                ElseIf (field.Equals(Me.weightedQuantityField)) Then ' Weighted qty

                    Me.currentManualData.WEIGHTED_QUANTITY = Me.weightedQuantityField.Value

                ElseIf (field.Equals(Me.fuelQuantityAtStart1Field)) Then ' Fuel 1 at start

                    Me.currentManualData.FUEL_QUANTITY_AT_START_1 = Me.fuelQuantityAtStart1Field.Value

                ElseIf (field.Equals(Me.fuelQuantityAtEnd1Field)) Then ' Fuel 1 at end

                    Me.currentManualData.FUEL_QUANTITY_AT_END_1 = Me.fuelQuantityAtEnd1Field.Value

                ElseIf (field.Equals(Me.fuelQuantityAtStart2Field)) Then ' Fuel 2 at start

                    Me.currentManualData.FUEL_QUANTITY_AT_START_2 = Me.fuelQuantityAtStart2Field.Value

                ElseIf (field.Equals(Me.fuelQuantityAtEnd2Field)) Then ' Fuel 2 at end

                    Me.currentManualData.FUEL_QUANTITY_AT_END_2 = Me.fuelQuantityAtEnd2Field.Value

                    'ElseIf (field.Equals(Me.drumsHourCounterAtStartField)) Then ' Drums at start

                    '    Me.currentManualData.DRUMS_HOURS_COUNTER_AT_START = Me.drumsHourCounterAtStartField.Value

                    'ElseIf (field.Equals(Me.drumsHourCounterAtEndField)) Then ' Drums at end

                    '    Me.currentManualData.DRUMS_HOURS_COUNTER_AT_END = Me.drumsHourCounterAtEndField.Value

                    'ElseIf (field.Equals(Me.boilerHourCounterAtStartField)) Then ' Boiler at start

                    '    Me.currentManualData.BOILERS_HOUR_COUNTER_AT_START = Me.boilerHourCounterAtStartField.Value

                    'ElseIf (field.Equals(Me.boilerHourCounterAtEndField)) Then ' Boiler at end

                    '    Me.currentManualData.BOILERS_HOUR_COUNTER_AT_END = Me.boilerHourCounterAtEndField.Value

                End If

                field.IsValid = True

            Catch dataEx As IncorrectDataException

                field.IsValid = False

            End Try

            updatePercentageLabels()

            Me.nextButton.Enabled = Me.currentManualData.isComplete

        End Sub

        Private Sub updatePercentageLabels()

            With Me.currentManualData

                ' Rejected Mix Percentage
                If (.REJECTED_MIX_QUANTITY.Equals(ManualData.INVALID_QUANTITY) OrElse _
                    .REJECTED_MIX_QUANTITY.Equals(ManualData.UNKNOWN_QUANTITY)) Then

                    Me.rejectedMixPercentageLabel.Text = ""
                Else

                    Me.rejectedMixPercentageLabel.Text = "(" & (.REJECTED_MIX_QUANTITY / .PRODUCED_QUANTITY * 100).ToString("N1") & " %)"
                End If

                ' Rejected Aggs Percentage
                If (.REJECTED_AGGREGATES_QUANTITY.Equals(ManualData.INVALID_QUANTITY) OrElse _
                    .REJECTED_AGGREGATES_QUANTITY.Equals(ManualData.UNKNOWN_QUANTITY)) Then

                    Me.rejectedAggregatesPercentageLabel.Text = ""
                Else

                    Me.rejectedAggregatesPercentageLabel.Text = "(" & (.REJECTED_AGGREGATES_QUANTITY / .PRODUCED_QUANTITY * 100).ToString("N1") & " %)"
                End If

                ' Rejected Filler Percentage
                If (.REJECTED_FILLER_QUANTITY.Equals(ManualData.INVALID_QUANTITY) OrElse _
                    .REJECTED_FILLER_QUANTITY.Equals(ManualData.UNKNOWN_QUANTITY)) Then

                    Me.rejectedFillerPercentageLabel.Text = ""
                Else

                    Me.rejectedFillerPercentageLabel.Text = "(" & (.REJECTED_FILLER_QUANTITY / .PRODUCED_QUANTITY * 100).ToString("N1") & " %)"
                End If

                ' Rejected Recycled Percentage
                If (.REJECTED_RECYCLED_QUANTITY.Equals(ManualData.INVALID_QUANTITY) OrElse _
                    .REJECTED_RECYCLED_QUANTITY.Equals(ManualData.UNKNOWN_QUANTITY)) Then

                    Me.rejectedRecycledPercentageLabel.Text = ""
                Else

                    Me.rejectedRecycledPercentageLabel.Text = "(" & (.REJECTED_RECYCLED_QUANTITY / .PRODUCED_QUANTITY * 100).ToString("N1") & " %)"
                End If

                ' Sold Quantity Diff Percentage
                If (.WEIGHTED_QUANTITY.Equals(ManualData.INVALID_QUANTITY) OrElse _
                    .WEIGHTED_QUANTITY.Equals(ManualData.UNKNOWN_QUANTITY) OrElse _
                    .WEIGHTED_QUANTITY.Equals(0)) Then

                    Me.percentageDiffLabel.Text = ""
                Else

                    Dim siloQty As Double

                    If (.SILO_QUANTITY_AT_START.Equals(ManualData.UNKNOWN_QUANTITY) OrElse _
                                                        .SILO_QUANTITY_AT_START.Equals(ManualData.INVALID_QUANTITY)) Then
                        siloQty = 0

                    Else

                        siloQty = .SILO_QUANTITY_AT_START

                        siloQty -= If(.SILO_QUANTITY_AT_END.Equals(ManualData.UNKNOWN_QUANTITY) OrElse _
                                      .SILO_QUANTITY_AT_END.Equals(ManualData.INVALID_QUANTITY), 0, .SILO_QUANTITY_AT_END)
                    End If


                    Dim rejectedMix = If(currentManualData.REJECTED_MIX_QUANTITY.Equals(ManualData.UNKNOWN_QUANTITY) OrElse _
                                         currentManualData.REJECTED_MIX_QUANTITY.Equals(ManualData.INVALID_QUANTITY), 0, currentManualData.REJECTED_MIX_QUANTITY)

                    Me.percentageDiffLabel.Text = "(Écart : " & _
                                                  ((.WEIGHTED_QUANTITY - _
                                                  (.PRODUCED_QUANTITY - rejectedMix + siloQty) _
                                                  ) / .WEIGHTED_QUANTITY * 100).ToString("N1") & _
                                                  " %)"
                End If

            End With

        End Sub

        Private Sub nextOnEnter() Handles siloQuantityAtStartField.EnterKeyPressed, _
                                            siloQuantityAtEndField.EnterKeyPressed, _
                                            rejectedMixQuantityField.EnterKeyPressed, _
                                            rejectedAggregatesQuantityField.EnterKeyPressed, _
                                            rejectedFillerQuantityField.EnterKeyPressed, _
                                            rejectedRecycledQuantityField.EnterKeyPressed, _
                                            weightedQuantityField.EnterKeyPressed, _
                                            fuelQuantityAtStart1Field.EnterKeyPressed, _
                                            fuelQuantityAtEnd1Field.EnterKeyPressed, _
                                            fuelQuantityAtStart2Field.EnterKeyPressed, _
                                            fuelQuantityAtEnd2Field.EnterKeyPressed


            If (Me.nextButton.Enabled) Then
                Me.goNext()
            End If

        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Public Overrides ReadOnly Property OverallProgressValue As Integer
            Get
                Return 30
            End Get
        End Property

    End Class
End Namespace
