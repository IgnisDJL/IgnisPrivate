Imports MasterUsine.Constants.UserInterface.ManualDataPrompt

' #language
Public Class ManualDataPrompt
    Inherits Form

    ' UI Components. Please do not modify by hand.
    Private WithEvents startLabel As System.Windows.Forms.Label
    Private WithEvents endLabel As System.Windows.Forms.Label
    Private WithEvents startTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents endTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents siloContentAtStartUnitLabel As System.Windows.Forms.Label
    Private WithEvents siloContentAtStartLabel As System.Windows.Forms.Label
    Private WithEvents siloContentAtStartTextBox As System.Windows.Forms.TextBox
    Private WithEvents siloContentAtEndUnitLabel As System.Windows.Forms.Label
    Private WithEvents siloContentAtEndLabel As System.Windows.Forms.Label
    Private WithEvents siloContentAtEndTextBox As System.Windows.Forms.TextBox
    Private WithEvents rejectedMixQuantityUnitLabel As System.Windows.Forms.Label
    Private WithEvents rejectedMixQuantityLabel As System.Windows.Forms.Label
    Private WithEvents rejectedMixQuantityTextBox As System.Windows.Forms.TextBox
    Private WithEvents weightedQuantityUnitLabel As System.Windows.Forms.Label
    Private WithEvents weightedQuantityLabel As System.Windows.Forms.Label
    Private WithEvents weightedQuantityTextBox As System.Windows.Forms.TextBox
    Private WithEvents lastLoadingTimeTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents firstLoadingTimeTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents lastLoadingTimeLabel As System.Windows.Forms.Label
    Private WithEvents firstLoadingTimeLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityUnitLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityTextBox As System.Windows.Forms.TextBox
    Private WithEvents normalRecycledMixQuantityUnitLabel As System.Windows.Forms.Label
    Private WithEvents normalRecycledMixQuantityLabel As System.Windows.Forms.Label
    Private WithEvents normalRecycledMixQuantityTextBox As System.Windows.Forms.TextBox
    Private WithEvents specialRecycledMixQuantityUnitLabel As System.Windows.Forms.Label
    Private WithEvents specialRecycleMixQuantity As System.Windows.Forms.Label
    Private WithEvents specialRecycledMixQuantityTextBox As System.Windows.Forms.TextBox
    Private WithEvents rejectedAggregatesUnitLabel As System.Windows.Forms.Label
    Private WithEvents rejectedAggregatesLabel As System.Windows.Forms.Label
    Private WithEvents rejectedAggregatesTextBox As System.Windows.Forms.TextBox
    Private WithEvents rejectedFillerUnitLabel As System.Windows.Forms.Label
    Private WithEvents rejectedFillerLabel As System.Windows.Forms.Label
    Private WithEvents rejectedFillerTextBox As System.Windows.Forms.TextBox
    Private WithEvents fuelQuantityAtStart1UnitLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtStart1Label As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtStart1TextBox As System.Windows.Forms.TextBox
    Private WithEvents fuelQuantityAtEnd1UnitLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtEnd1Label As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtEnd1TextBox As System.Windows.Forms.TextBox
    Private WithEvents fuelQuantityAtStart2UnitLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtStart2Label As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtStart2TextBox As System.Windows.Forms.TextBox
    Private WithEvents fuelQuantityAtEnd2UnitLabel As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtEnd2Label As System.Windows.Forms.Label
    Private WithEvents fuelQuantityAtEnd2TextBox As System.Windows.Forms.TextBox
    Private WithEvents boillerQuantityAtStartUnitLabel As System.Windows.Forms.Label
    Private WithEvents boillerQuantityAtStartLabel As System.Windows.Forms.Label
    Private WithEvents boillerQuantityAtStartTextBox As System.Windows.Forms.TextBox
    Private WithEvents boillerQuantityAtEndUnitLabel As System.Windows.Forms.Label
    Private WithEvents boillerQuantityAtEndLabel As System.Windows.Forms.Label
    Private WithEvents boillerQuantityAtEndTextBox As System.Windows.Forms.TextBox
    Private WithEvents myCancelButton As System.Windows.Forms.Button
    Private WithEvents okButton As System.Windows.Forms.Button
    Private WithEvents toggleOptionnalButton As System.Windows.Forms.Button
    Private WithEvents drumhoursCounterEndLabel As System.Windows.Forms.Label
    Private WithEvents drumHoursCounterStartLabel As System.Windows.Forms.Label
    Private WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Private WithEvents separationLine As Microsoft.VisualBasic.PowerPacks.LineShape

    ' Class attributes

    Public Shared instance As New ManualDataPrompt
    Private state As WINDOW_STATE

    Private optionnalDataComponents As List(Of Control)
    Private WithEvents drumsHoursCounterEndTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents drumHoursCounterStartTimePicker As System.Windows.Forms.DateTimePicker
    Private WithEvents drumHoursCounterStartUnitLabel As System.Windows.Forms.Label
    Private WithEvents drumHoursCounterEndUnitLabel As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox

    Private manualData As ManualData

    Public Sub New()
        Me.manualData = New ManualData

        Me.optionnalDataComponents = New List(Of Control)

        Me.InitializeComponent()

        Me.startLabel.Text = START_LABEL_TEXT
        Me.endLabel.Text = END_LABEL_TEXT
        Me.siloContentAtStartLabel.Text = SILO_CONTENT_AT_START_LABEL_TEXT
        'Me.quantityLabel.Text = QUANTITY_LABEL_TEXT
        Me.siloContentAtEndLabel.Text = SILO_CONTENT_AT_END_LABEL_TEXT
        Me.rejectedMixQuantityLabel.Text = REJECTED_MIX_QUANTITY_LABEL_TEXT
        Me.weightedQuantityLabel.Text = WEIGHTED_QUANTITY_LABEL_TEXT
        Me.lastLoadingTimeLabel.Text = LAST_LOADING_TIME_LABEL_TEXT
        Me.firstLoadingTimeLabel.Text = FIRST_LOADING_TIME_LABEL_TEXT
        Me.fuelQuantityLabel.Text = FUEL_QUANTITY_LABEL_TEXT
        Me.normalRecycledMixQuantityLabel.Text = NORMAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT
        Me.specialRecycleMixQuantity.Text = SPECIAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT
        Me.rejectedAggregatesLabel.Text = REJECTED_AGGREGATES_LABEL_TEXT
        Me.rejectedFillerLabel.Text = REJECTED_FILLER_LABEL_TEXT
        Me.fuelQuantityAtStart1Label.Text = FUEL_QUANTITY_AT_START_1_LABEL_TEXT
        Me.fuelQuantityAtEnd1Label.Text = FUEL_QUANTITY_AT_END_1_LABEL_TEXT
        Me.fuelQuantityAtStart2Label.Text = FUEL_QUANTITY_AT_START_2_LABEL_TEXT
        Me.fuelQuantityAtEnd2Label.Text = FUEL_QUANTITY_AT_END_2_LABEL_TEXT
        Me.boillerQuantityAtStartLabel.Text = BOILER_QUANTITY_AT_START_LABEL_TEXT
        Me.boillerQuantityAtEndLabel.Text = BOILER_QUANTITY_AT_END_LABEL_TEXT
        Me.drumhoursCounterEndLabel.Text = DRUM_QUANTITY_AT_END_LABEL_TEXT
        Me.drumHoursCounterStartLabel.Text = DRUM_QUANTITY_AT_START_LABEL_TEXT

        Me.optionnalDataComponents.Add(fuelQuantityAtStart1UnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtStart1Label) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtStart1TextBox) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtEnd1UnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtEnd1Label) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtEnd1TextBox) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtStart2UnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtStart2Label) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtStart2TextBox) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtEnd2UnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtEnd2Label) ' (optional)
        Me.optionnalDataComponents.Add(fuelQuantityAtEnd2TextBox) ' (optional)
        Me.optionnalDataComponents.Add(boillerQuantityAtStartUnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(boillerQuantityAtStartLabel) ' (optional)
        Me.optionnalDataComponents.Add(boillerQuantityAtStartTextBox) ' (optional)
        Me.optionnalDataComponents.Add(boillerQuantityAtEndUnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(boillerQuantityAtEndLabel) ' (optional)
        Me.optionnalDataComponents.Add(boillerQuantityAtEndTextBox) ' (optional)
        'Me.optionnalDataComponents.Add(drumQuantityAtEndUnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(drumhoursCounterEndLabel) ' (optional)
        'Me.optionnalDataComponents.Add(drumQuantityEndTextBox) ' (optional)
        'Me.optionnalDataComponents.Add(drumQuantityAtStartUnitLabel) ' (optional)
        Me.optionnalDataComponents.Add(drumHoursCounterStartLabel) ' (optional)
        'Me.optionnalDataComponents.Add(drumQuantityAtStartTextBox) ' (optional)
        Me.optionnalDataComponents.Add(ShapeContainer1) ' (optional) (contains the separationLine)

        Me.state = WINDOW_STATE.EXPANDED
        Me.toggleOptionnalButton.Text = TOGGLE_OPTIONNAL_DATA_BUTTON_TEXT_EXPANDED

    End Sub

    Public Overloads Function ShowDialog(manualData As ManualData) As DialogResult

        Me.manualData = manualData

        Me.startTimePicker.Value = manualData.START
        Me.endTimePicker.Value = manualData.END_
        Me.siloContentAtStartTextBox.Text = manualData.SILO_CONTENT_AT_START
        'Me.quantityTextBox.Text = manualData.QUANTITY
        Me.siloContentAtEndTextBox.Text = manualData.SILO_CONTENT_AT_END
        Me.rejectedMixQuantityTextBox.Text = manualData.REJECTED_MIX
        Me.weightedQuantityTextBox.Text = manualData.WEIGHTED_QUANTITY
        Me.firstLoadingTimeTimePicker.Value = manualData.FIRST_LOADING_TIME
        Me.lastLoadingTimeTimePicker.Value = manualData.LAST_LOADING_TIME
        Me.fuelQuantityTextBox.Text = manualData.FUEL_QUANTITY
        Me.normalRecycledMixQuantityTextBox.Text = manualData.NORMAL_RECYCLED_MIX_QUANTITY
        Me.specialRecycledMixQuantityTextBox.Text = manualData.SPECIAL_RECYCLED_MIX_QUANTITY
        Me.rejectedAggregatesTextBox.Text = manualData.REJECTED_AGGREGATES
        Me.rejectedFillerTextBox.Text = manualData.REJECTED_FILLER

        'Me.drumQuantityAtStartTextBox.Text = manualData.DRUM_QUANTITY_AT_START
        'Me.drumQuantityEndTextBox.Text = manualData.DRUM_QUANTITY_AT_END
        Me.fuelQuantityAtStart1TextBox.Text = manualData.FUEL_QUANTITY_AT_START_1
        Me.fuelQuantityAtEnd1TextBox.Text = manualData.FUEL_QUANTITY_AT_END_1
        Me.fuelQuantityAtStart2TextBox.Text = manualData.FUEL_QUANTITY_AT_START_2
        Me.fuelQuantityAtEnd2TextBox.Text = manualData.FUEL_QUANTITY_AT_END_2
        Me.boillerQuantityAtStartTextBox.Text = manualData.BOILER_QUANTITY_AT_START
        Me.boillerQuantityAtEndTextBox.Text = manualData.BOILER_QUANTITY_AT_END

        Return Me.ShowDialog()

    End Function

    Public ReadOnly Property MANUAL_DATA As ManualData
        Get
            Return Me.manualData
        End Get
    End Property

    ' Generated by Visual Studio's Windows Form maker. Please do not modify by hand
    Private Sub InitializeComponent()
        Me.startLabel = New System.Windows.Forms.Label()
        Me.endLabel = New System.Windows.Forms.Label()
        Me.startTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.endTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.siloContentAtStartUnitLabel = New System.Windows.Forms.Label()
        Me.siloContentAtStartLabel = New System.Windows.Forms.Label()
        Me.siloContentAtStartTextBox = New System.Windows.Forms.TextBox()
        Me.siloContentAtEndUnitLabel = New System.Windows.Forms.Label()
        Me.siloContentAtEndLabel = New System.Windows.Forms.Label()
        Me.siloContentAtEndTextBox = New System.Windows.Forms.TextBox()
        Me.rejectedMixQuantityUnitLabel = New System.Windows.Forms.Label()
        Me.rejectedMixQuantityLabel = New System.Windows.Forms.Label()
        Me.rejectedMixQuantityTextBox = New System.Windows.Forms.TextBox()
        Me.weightedQuantityUnitLabel = New System.Windows.Forms.Label()
        Me.weightedQuantityLabel = New System.Windows.Forms.Label()
        Me.weightedQuantityTextBox = New System.Windows.Forms.TextBox()
        Me.lastLoadingTimeTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.firstLoadingTimeTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.lastLoadingTimeLabel = New System.Windows.Forms.Label()
        Me.firstLoadingTimeLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityUnitLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityTextBox = New System.Windows.Forms.TextBox()
        Me.normalRecycledMixQuantityUnitLabel = New System.Windows.Forms.Label()
        Me.normalRecycledMixQuantityLabel = New System.Windows.Forms.Label()
        Me.normalRecycledMixQuantityTextBox = New System.Windows.Forms.TextBox()
        Me.specialRecycledMixQuantityUnitLabel = New System.Windows.Forms.Label()
        Me.specialRecycleMixQuantity = New System.Windows.Forms.Label()
        Me.specialRecycledMixQuantityTextBox = New System.Windows.Forms.TextBox()
        Me.rejectedAggregatesUnitLabel = New System.Windows.Forms.Label()
        Me.rejectedAggregatesLabel = New System.Windows.Forms.Label()
        Me.rejectedAggregatesTextBox = New System.Windows.Forms.TextBox()
        Me.rejectedFillerUnitLabel = New System.Windows.Forms.Label()
        Me.rejectedFillerLabel = New System.Windows.Forms.Label()
        Me.rejectedFillerTextBox = New System.Windows.Forms.TextBox()
        Me.fuelQuantityAtStart1UnitLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityAtStart1Label = New System.Windows.Forms.Label()
        Me.fuelQuantityAtStart1TextBox = New System.Windows.Forms.TextBox()
        Me.fuelQuantityAtEnd1UnitLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityAtEnd1Label = New System.Windows.Forms.Label()
        Me.fuelQuantityAtEnd1TextBox = New System.Windows.Forms.TextBox()
        Me.fuelQuantityAtStart2UnitLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityAtStart2Label = New System.Windows.Forms.Label()
        Me.fuelQuantityAtStart2TextBox = New System.Windows.Forms.TextBox()
        Me.fuelQuantityAtEnd2UnitLabel = New System.Windows.Forms.Label()
        Me.fuelQuantityAtEnd2Label = New System.Windows.Forms.Label()
        Me.fuelQuantityAtEnd2TextBox = New System.Windows.Forms.TextBox()
        Me.boillerQuantityAtStartUnitLabel = New System.Windows.Forms.Label()
        Me.boillerQuantityAtStartLabel = New System.Windows.Forms.Label()
        Me.boillerQuantityAtStartTextBox = New System.Windows.Forms.TextBox()
        Me.boillerQuantityAtEndUnitLabel = New System.Windows.Forms.Label()
        Me.boillerQuantityAtEndLabel = New System.Windows.Forms.Label()
        Me.boillerQuantityAtEndTextBox = New System.Windows.Forms.TextBox()
        Me.myCancelButton = New System.Windows.Forms.Button()
        Me.okButton = New System.Windows.Forms.Button()
        Me.toggleOptionnalButton = New System.Windows.Forms.Button()
        Me.drumhoursCounterEndLabel = New System.Windows.Forms.Label()
        Me.drumHoursCounterStartLabel = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.separationLine = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.drumsHoursCounterEndTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.drumHoursCounterStartTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.drumHoursCounterStartUnitLabel = New System.Windows.Forms.Label()
        Me.drumHoursCounterEndUnitLabel = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'startLabel
        '
        Me.startLabel.AutoSize = True
        Me.startLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startLabel.Location = New System.Drawing.Point(12, 45)
        Me.startLabel.Name = "startLabel"
        Me.startLabel.Size = New System.Drawing.Size(44, 20)
        Me.startLabel.TabIndex = 11
        Me.startLabel.Text = "Début"
        '
        'endLabel
        '
        Me.endLabel.AutoSize = True
        Me.endLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endLabel.Location = New System.Drawing.Point(12, 73)
        Me.endLabel.Name = "endLabel"
        Me.endLabel.Size = New System.Drawing.Size(27, 20)
        Me.endLabel.TabIndex = 12
        Me.endLabel.Text = "Fin"
        '
        'startTimePicker
        '
        Me.startTimePicker.CalendarFont = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startTimePicker.CustomFormat = "HH : mm"
        Me.startTimePicker.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.startTimePicker.Location = New System.Drawing.Point(291, 43)
        Me.startTimePicker.Name = "startTimePicker"
        Me.startTimePicker.ShowUpDown = True
        Me.startTimePicker.Size = New System.Drawing.Size(74, 22)
        Me.startTimePicker.TabIndex = 2
        Me.startTimePicker.Value = New Date(2013, 4, 8, 0, 0, 0, 0)
        '
        'endTimePicker
        '
        Me.endTimePicker.CalendarFont = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endTimePicker.CustomFormat = "HH : mm"
        Me.endTimePicker.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.endTimePicker.Location = New System.Drawing.Point(291, 71)
        Me.endTimePicker.Name = "endTimePicker"
        Me.endTimePicker.ShowUpDown = True
        Me.endTimePicker.Size = New System.Drawing.Size(74, 22)
        Me.endTimePicker.TabIndex = 3
        Me.endTimePicker.Value = New Date(2013, 4, 8, 0, 0, 0, 0)
        '
        'siloContentAtStartUnitLabel
        '
        Me.siloContentAtStartUnitLabel.AutoSize = True
        Me.siloContentAtStartUnitLabel.Location = New System.Drawing.Point(404, 183)
        Me.siloContentAtStartUnitLabel.Name = "siloContentAtStartUnitLabel"
        Me.siloContentAtStartUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.siloContentAtStartUnitLabel.TabIndex = 28
        Me.siloContentAtStartUnitLabel.Text = "( T )"
        '
        'siloContentAtStartLabel
        '
        Me.siloContentAtStartLabel.AutoSize = True
        Me.siloContentAtStartLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.siloContentAtStartLabel.Location = New System.Drawing.Point(12, 183)
        Me.siloContentAtStartLabel.Name = "siloContentAtStartLabel"
        Me.siloContentAtStartLabel.Size = New System.Drawing.Size(253, 20)
        Me.siloContentAtStartLabel.TabIndex = 27
        Me.siloContentAtStartLabel.Text = "Compteur silo début de journée (approx)"
        '
        'siloContentAtStartTextBox
        '
        Me.siloContentAtStartTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.siloContentAtStartTextBox.Location = New System.Drawing.Point(291, 183)
        Me.siloContentAtStartTextBox.Name = "siloContentAtStartTextBox"
        Me.siloContentAtStartTextBox.Size = New System.Drawing.Size(107, 22)
        Me.siloContentAtStartTextBox.TabIndex = 26
        '
        'siloContentAtEndUnitLabel
        '
        Me.siloContentAtEndUnitLabel.AutoSize = True
        Me.siloContentAtEndUnitLabel.Location = New System.Drawing.Point(404, 211)
        Me.siloContentAtEndUnitLabel.Name = "siloContentAtEndUnitLabel"
        Me.siloContentAtEndUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.siloContentAtEndUnitLabel.TabIndex = 37
        Me.siloContentAtEndUnitLabel.Text = "( T )"
        '
        'siloContentAtEndLabel
        '
        Me.siloContentAtEndLabel.AutoSize = True
        Me.siloContentAtEndLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.siloContentAtEndLabel.Location = New System.Drawing.Point(12, 211)
        Me.siloContentAtEndLabel.Name = "siloContentAtEndLabel"
        Me.siloContentAtEndLabel.Size = New System.Drawing.Size(232, 20)
        Me.siloContentAtEndLabel.TabIndex = 36
        Me.siloContentAtEndLabel.Text = "Compteur silo fin de journée (approx)"
        '
        'siloContentAtEndTextBox
        '
        Me.siloContentAtEndTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.siloContentAtEndTextBox.Location = New System.Drawing.Point(291, 211)
        Me.siloContentAtEndTextBox.Name = "siloContentAtEndTextBox"
        Me.siloContentAtEndTextBox.Size = New System.Drawing.Size(107, 22)
        Me.siloContentAtEndTextBox.TabIndex = 35
        '
        'rejectedMixQuantityUnitLabel
        '
        Me.rejectedMixQuantityUnitLabel.AutoSize = True
        Me.rejectedMixQuantityUnitLabel.Location = New System.Drawing.Point(404, 239)
        Me.rejectedMixQuantityUnitLabel.Name = "rejectedMixQuantityUnitLabel"
        Me.rejectedMixQuantityUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.rejectedMixQuantityUnitLabel.TabIndex = 40
        Me.rejectedMixQuantityUnitLabel.Text = "( T )"
        '
        'rejectedMixQuantityLabel
        '
        Me.rejectedMixQuantityLabel.AutoSize = True
        Me.rejectedMixQuantityLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rejectedMixQuantityLabel.Location = New System.Drawing.Point(12, 239)
        Me.rejectedMixQuantityLabel.Name = "rejectedMixQuantityLabel"
        Me.rejectedMixQuantityLabel.Size = New System.Drawing.Size(163, 20)
        Me.rejectedMixQuantityLabel.TabIndex = 39
        Me.rejectedMixQuantityLabel.Text = "Compteur d'enrobé rejeté"
        '
        'rejectedMixQuantityTextBox
        '
        Me.rejectedMixQuantityTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rejectedMixQuantityTextBox.Location = New System.Drawing.Point(291, 239)
        Me.rejectedMixQuantityTextBox.Name = "rejectedMixQuantityTextBox"
        Me.rejectedMixQuantityTextBox.Size = New System.Drawing.Size(107, 22)
        Me.rejectedMixQuantityTextBox.TabIndex = 38
        '
        'weightedQuantityUnitLabel
        '
        Me.weightedQuantityUnitLabel.AutoSize = True
        Me.weightedQuantityUnitLabel.Location = New System.Drawing.Point(404, 267)
        Me.weightedQuantityUnitLabel.Name = "weightedQuantityUnitLabel"
        Me.weightedQuantityUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.weightedQuantityUnitLabel.TabIndex = 43
        Me.weightedQuantityUnitLabel.Text = "( T )"
        '
        'weightedQuantityLabel
        '
        Me.weightedQuantityLabel.AutoSize = True
        Me.weightedQuantityLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.weightedQuantityLabel.Location = New System.Drawing.Point(12, 267)
        Me.weightedQuantityLabel.Name = "weightedQuantityLabel"
        Me.weightedQuantityLabel.Size = New System.Drawing.Size(168, 20)
        Me.weightedQuantityLabel.TabIndex = 42
        Me.weightedQuantityLabel.Text = "Compteur poste de pesée"
        '
        'weightedQuantityTextBox
        '
        Me.weightedQuantityTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.weightedQuantityTextBox.Location = New System.Drawing.Point(291, 267)
        Me.weightedQuantityTextBox.Name = "weightedQuantityTextBox"
        Me.weightedQuantityTextBox.Size = New System.Drawing.Size(107, 22)
        Me.weightedQuantityTextBox.TabIndex = 41
        '
        'lastLoadingTimeTimePicker
        '
        Me.lastLoadingTimeTimePicker.CalendarFont = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lastLoadingTimeTimePicker.CustomFormat = "HH : mm"
        Me.lastLoadingTimeTimePicker.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lastLoadingTimeTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lastLoadingTimeTimePicker.Location = New System.Drawing.Point(291, 127)
        Me.lastLoadingTimeTimePicker.Name = "lastLoadingTimeTimePicker"
        Me.lastLoadingTimeTimePicker.ShowUpDown = True
        Me.lastLoadingTimeTimePicker.Size = New System.Drawing.Size(74, 22)
        Me.lastLoadingTimeTimePicker.TabIndex = 45
        Me.lastLoadingTimeTimePicker.Value = New Date(2013, 4, 8, 0, 0, 0, 0)
        '
        'firstLoadingTimeTimePicker
        '
        Me.firstLoadingTimeTimePicker.CalendarFont = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.firstLoadingTimeTimePicker.CustomFormat = "HH : mm"
        Me.firstLoadingTimeTimePicker.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.firstLoadingTimeTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.firstLoadingTimeTimePicker.Location = New System.Drawing.Point(291, 99)
        Me.firstLoadingTimeTimePicker.Name = "firstLoadingTimeTimePicker"
        Me.firstLoadingTimeTimePicker.ShowUpDown = True
        Me.firstLoadingTimeTimePicker.Size = New System.Drawing.Size(74, 22)
        Me.firstLoadingTimeTimePicker.TabIndex = 44
        Me.firstLoadingTimeTimePicker.Value = New Date(2013, 4, 8, 0, 0, 0, 0)
        '
        'lastLoadingTimeLabel
        '
        Me.lastLoadingTimeLabel.AutoSize = True
        Me.lastLoadingTimeLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lastLoadingTimeLabel.Location = New System.Drawing.Point(12, 129)
        Me.lastLoadingTimeLabel.Name = "lastLoadingTimeLabel"
        Me.lastLoadingTimeLabel.Size = New System.Drawing.Size(165, 20)
        Me.lastLoadingTimeLabel.TabIndex = 47
        Me.lastLoadingTimeLabel.Text = "Heure dernier chargement"
        '
        'firstLoadingTimeLabel
        '
        Me.firstLoadingTimeLabel.AutoSize = True
        Me.firstLoadingTimeLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.firstLoadingTimeLabel.Location = New System.Drawing.Point(12, 101)
        Me.firstLoadingTimeLabel.Name = "firstLoadingTimeLabel"
        Me.firstLoadingTimeLabel.Size = New System.Drawing.Size(169, 20)
        Me.firstLoadingTimeLabel.TabIndex = 46
        Me.firstLoadingTimeLabel.Text = "Heure premier chargement"
        '
        'fuelQuantityUnitLabel
        '
        Me.fuelQuantityUnitLabel.AutoSize = True
        Me.fuelQuantityUnitLabel.Location = New System.Drawing.Point(404, 155)
        Me.fuelQuantityUnitLabel.Name = "fuelQuantityUnitLabel"
        Me.fuelQuantityUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.fuelQuantityUnitLabel.TabIndex = 50
        Me.fuelQuantityUnitLabel.Text = "( L )"
        '
        'fuelQuantityLabel
        '
        Me.fuelQuantityLabel.AutoSize = True
        Me.fuelQuantityLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityLabel.Location = New System.Drawing.Point(12, 155)
        Me.fuelQuantityLabel.Name = "fuelQuantityLabel"
        Me.fuelQuantityLabel.Size = New System.Drawing.Size(65, 20)
        Me.fuelQuantityLabel.TabIndex = 49
        Me.fuelQuantityLabel.Text = "Carburant"
        '
        'fuelQuantityTextBox
        '
        Me.fuelQuantityTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityTextBox.Location = New System.Drawing.Point(291, 155)
        Me.fuelQuantityTextBox.Name = "fuelQuantityTextBox"
        Me.fuelQuantityTextBox.Size = New System.Drawing.Size(107, 22)
        Me.fuelQuantityTextBox.TabIndex = 48
        '
        'normalRecycledMixQuantityUnitLabel
        '
        Me.normalRecycledMixQuantityUnitLabel.AutoSize = True
        Me.normalRecycledMixQuantityUnitLabel.Location = New System.Drawing.Point(404, 295)
        Me.normalRecycledMixQuantityUnitLabel.Name = "normalRecycledMixQuantityUnitLabel"
        Me.normalRecycledMixQuantityUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.normalRecycledMixQuantityUnitLabel.TabIndex = 53
        Me.normalRecycledMixQuantityUnitLabel.Text = "( T )"
        '
        'normalRecycledMixQuantityLabel
        '
        Me.normalRecycledMixQuantityLabel.AutoSize = True
        Me.normalRecycledMixQuantityLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.normalRecycledMixQuantityLabel.Location = New System.Drawing.Point(12, 297)
        Me.normalRecycledMixQuantityLabel.Name = "normalRecycledMixQuantityLabel"
        Me.normalRecycledMixQuantityLabel.Size = New System.Drawing.Size(187, 20)
        Me.normalRecycledMixQuantityLabel.TabIndex = 52
        Me.normalRecycledMixQuantityLabel.Text = "Compteur de recyclés utilisés"
        '
        'normalRecycledMixQuantityTextBox
        '
        Me.normalRecycledMixQuantityTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.normalRecycledMixQuantityTextBox.Location = New System.Drawing.Point(291, 295)
        Me.normalRecycledMixQuantityTextBox.Name = "normalRecycledMixQuantityTextBox"
        Me.normalRecycledMixQuantityTextBox.Size = New System.Drawing.Size(107, 22)
        Me.normalRecycledMixQuantityTextBox.TabIndex = 51
        '
        'specialRecycledMixQuantityUnitLabel
        '
        Me.specialRecycledMixQuantityUnitLabel.AutoSize = True
        Me.specialRecycledMixQuantityUnitLabel.Location = New System.Drawing.Point(404, 321)
        Me.specialRecycledMixQuantityUnitLabel.Name = "specialRecycledMixQuantityUnitLabel"
        Me.specialRecycledMixQuantityUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.specialRecycledMixQuantityUnitLabel.TabIndex = 56
        Me.specialRecycledMixQuantityUnitLabel.Text = "( T )"
        '
        'specialRecycleMixQuantity
        '
        Me.specialRecycleMixQuantity.AutoSize = True
        Me.specialRecycleMixQuantity.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.specialRecycleMixQuantity.Location = New System.Drawing.Point(12, 323)
        Me.specialRecycleMixQuantity.Name = "specialRecycleMixQuantity"
        Me.specialRecycleMixQuantity.Size = New System.Drawing.Size(252, 20)
        Me.specialRecycleMixQuantity.TabIndex = 55
        Me.specialRecycleMixQuantity.Text = "Compteur de recyclé + bardeaux utilisés"
        '
        'specialRecycledMixQuantityTextBox
        '
        Me.specialRecycledMixQuantityTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.specialRecycledMixQuantityTextBox.Location = New System.Drawing.Point(291, 321)
        Me.specialRecycledMixQuantityTextBox.Name = "specialRecycledMixQuantityTextBox"
        Me.specialRecycledMixQuantityTextBox.Size = New System.Drawing.Size(107, 22)
        Me.specialRecycledMixQuantityTextBox.TabIndex = 54
        '
        'rejectedAggregatesUnitLabel
        '
        Me.rejectedAggregatesUnitLabel.AutoSize = True
        Me.rejectedAggregatesUnitLabel.Location = New System.Drawing.Point(404, 349)
        Me.rejectedAggregatesUnitLabel.Name = "rejectedAggregatesUnitLabel"
        Me.rejectedAggregatesUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.rejectedAggregatesUnitLabel.TabIndex = 59
        Me.rejectedAggregatesUnitLabel.Text = "( T )"
        '
        'rejectedAggregatesLabel
        '
        Me.rejectedAggregatesLabel.AutoSize = True
        Me.rejectedAggregatesLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rejectedAggregatesLabel.Location = New System.Drawing.Point(12, 349)
        Me.rejectedAggregatesLabel.Name = "rejectedAggregatesLabel"
        Me.rejectedAggregatesLabel.Size = New System.Drawing.Size(189, 20)
        Me.rejectedAggregatesLabel.TabIndex = 58
        Me.rejectedAggregatesLabel.Text = "Compteur de granulats rejetés"
        '
        'rejectedAggregatesTextBox
        '
        Me.rejectedAggregatesTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rejectedAggregatesTextBox.Location = New System.Drawing.Point(291, 349)
        Me.rejectedAggregatesTextBox.Name = "rejectedAggregatesTextBox"
        Me.rejectedAggregatesTextBox.Size = New System.Drawing.Size(107, 22)
        Me.rejectedAggregatesTextBox.TabIndex = 57
        '
        'rejectedFillerUnitLabel
        '
        Me.rejectedFillerUnitLabel.AutoSize = True
        Me.rejectedFillerUnitLabel.Location = New System.Drawing.Point(404, 377)
        Me.rejectedFillerUnitLabel.Name = "rejectedFillerUnitLabel"
        Me.rejectedFillerUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.rejectedFillerUnitLabel.TabIndex = 65
        Me.rejectedFillerUnitLabel.Text = "( T )"
        '
        'rejectedFillerLabel
        '
        Me.rejectedFillerLabel.AutoSize = True
        Me.rejectedFillerLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rejectedFillerLabel.Location = New System.Drawing.Point(12, 377)
        Me.rejectedFillerLabel.Name = "rejectedFillerLabel"
        Me.rejectedFillerLabel.Size = New System.Drawing.Size(153, 20)
        Me.rejectedFillerLabel.TabIndex = 64
        Me.rejectedFillerLabel.Text = "Compteur de filler rejeté"
        '
        'rejectedFillerTextBox
        '
        Me.rejectedFillerTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rejectedFillerTextBox.Location = New System.Drawing.Point(291, 377)
        Me.rejectedFillerTextBox.Name = "rejectedFillerTextBox"
        Me.rejectedFillerTextBox.Size = New System.Drawing.Size(107, 22)
        Me.rejectedFillerTextBox.TabIndex = 63
        '
        'fuelQuantityAtStart1UnitLabel
        '
        Me.fuelQuantityAtStart1UnitLabel.AutoSize = True
        Me.fuelQuantityAtStart1UnitLabel.Location = New System.Drawing.Point(404, 482)
        Me.fuelQuantityAtStart1UnitLabel.Name = "fuelQuantityAtStart1UnitLabel"
        Me.fuelQuantityAtStart1UnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.fuelQuantityAtStart1UnitLabel.TabIndex = 75
        Me.fuelQuantityAtStart1UnitLabel.Text = "( T )"
        '
        'fuelQuantityAtStart1Label
        '
        Me.fuelQuantityAtStart1Label.AutoSize = True
        Me.fuelQuantityAtStart1Label.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtStart1Label.Location = New System.Drawing.Point(12, 482)
        Me.fuelQuantityAtStart1Label.Name = "fuelQuantityAtStart1Label"
        Me.fuelQuantityAtStart1Label.Size = New System.Drawing.Size(164, 20)
        Me.fuelQuantityAtStart1Label.TabIndex = 74
        Me.fuelQuantityAtStart1Label.Text = "Quantité carburant 1 début"
        '
        'fuelQuantityAtStart1TextBox
        '
        Me.fuelQuantityAtStart1TextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtStart1TextBox.Location = New System.Drawing.Point(291, 482)
        Me.fuelQuantityAtStart1TextBox.Name = "fuelQuantityAtStart1TextBox"
        Me.fuelQuantityAtStart1TextBox.Size = New System.Drawing.Size(107, 22)
        Me.fuelQuantityAtStart1TextBox.TabIndex = 73
        '
        'fuelQuantityAtEnd1UnitLabel
        '
        Me.fuelQuantityAtEnd1UnitLabel.AutoSize = True
        Me.fuelQuantityAtEnd1UnitLabel.Location = New System.Drawing.Point(403, 510)
        Me.fuelQuantityAtEnd1UnitLabel.Name = "fuelQuantityAtEnd1UnitLabel"
        Me.fuelQuantityAtEnd1UnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.fuelQuantityAtEnd1UnitLabel.TabIndex = 78
        Me.fuelQuantityAtEnd1UnitLabel.Text = "( T )"
        '
        'fuelQuantityAtEnd1Label
        '
        Me.fuelQuantityAtEnd1Label.AutoSize = True
        Me.fuelQuantityAtEnd1Label.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtEnd1Label.Location = New System.Drawing.Point(12, 510)
        Me.fuelQuantityAtEnd1Label.Name = "fuelQuantityAtEnd1Label"
        Me.fuelQuantityAtEnd1Label.Size = New System.Drawing.Size(143, 20)
        Me.fuelQuantityAtEnd1Label.TabIndex = 77
        Me.fuelQuantityAtEnd1Label.Text = "Quantité carburant 1 fin"
        '
        'fuelQuantityAtEnd1TextBox
        '
        Me.fuelQuantityAtEnd1TextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtEnd1TextBox.Location = New System.Drawing.Point(291, 510)
        Me.fuelQuantityAtEnd1TextBox.Name = "fuelQuantityAtEnd1TextBox"
        Me.fuelQuantityAtEnd1TextBox.Size = New System.Drawing.Size(107, 22)
        Me.fuelQuantityAtEnd1TextBox.TabIndex = 76
        '
        'fuelQuantityAtStart2UnitLabel
        '
        Me.fuelQuantityAtStart2UnitLabel.AutoSize = True
        Me.fuelQuantityAtStart2UnitLabel.Location = New System.Drawing.Point(403, 538)
        Me.fuelQuantityAtStart2UnitLabel.Name = "fuelQuantityAtStart2UnitLabel"
        Me.fuelQuantityAtStart2UnitLabel.Size = New System.Drawing.Size(40, 20)
        Me.fuelQuantityAtStart2UnitLabel.TabIndex = 81
        Me.fuelQuantityAtStart2UnitLabel.Text = "( m³ )"
        '
        'fuelQuantityAtStart2Label
        '
        Me.fuelQuantityAtStart2Label.AutoSize = True
        Me.fuelQuantityAtStart2Label.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtStart2Label.Location = New System.Drawing.Point(12, 538)
        Me.fuelQuantityAtStart2Label.Name = "fuelQuantityAtStart2Label"
        Me.fuelQuantityAtStart2Label.Size = New System.Drawing.Size(164, 20)
        Me.fuelQuantityAtStart2Label.TabIndex = 80
        Me.fuelQuantityAtStart2Label.Text = "Quantité carburant 2 début"
        '
        'fuelQuantityAtStart2TextBox
        '
        Me.fuelQuantityAtStart2TextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtStart2TextBox.Location = New System.Drawing.Point(291, 538)
        Me.fuelQuantityAtStart2TextBox.Name = "fuelQuantityAtStart2TextBox"
        Me.fuelQuantityAtStart2TextBox.Size = New System.Drawing.Size(107, 22)
        Me.fuelQuantityAtStart2TextBox.TabIndex = 79
        '
        'fuelQuantityAtEnd2UnitLabel
        '
        Me.fuelQuantityAtEnd2UnitLabel.AutoSize = True
        Me.fuelQuantityAtEnd2UnitLabel.Location = New System.Drawing.Point(403, 566)
        Me.fuelQuantityAtEnd2UnitLabel.Name = "fuelQuantityAtEnd2UnitLabel"
        Me.fuelQuantityAtEnd2UnitLabel.Size = New System.Drawing.Size(40, 20)
        Me.fuelQuantityAtEnd2UnitLabel.TabIndex = 84
        Me.fuelQuantityAtEnd2UnitLabel.Text = "( m³ )"
        '
        'fuelQuantityAtEnd2Label
        '
        Me.fuelQuantityAtEnd2Label.AutoSize = True
        Me.fuelQuantityAtEnd2Label.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtEnd2Label.Location = New System.Drawing.Point(12, 566)
        Me.fuelQuantityAtEnd2Label.Name = "fuelQuantityAtEnd2Label"
        Me.fuelQuantityAtEnd2Label.Size = New System.Drawing.Size(143, 20)
        Me.fuelQuantityAtEnd2Label.TabIndex = 83
        Me.fuelQuantityAtEnd2Label.Text = "Quantité carburant 2 fin"
        '
        'fuelQuantityAtEnd2TextBox
        '
        Me.fuelQuantityAtEnd2TextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fuelQuantityAtEnd2TextBox.Location = New System.Drawing.Point(291, 566)
        Me.fuelQuantityAtEnd2TextBox.Name = "fuelQuantityAtEnd2TextBox"
        Me.fuelQuantityAtEnd2TextBox.Size = New System.Drawing.Size(107, 22)
        Me.fuelQuantityAtEnd2TextBox.TabIndex = 82
        '
        'boillerQuantityAtStartUnitLabel
        '
        Me.boillerQuantityAtStartUnitLabel.AutoSize = True
        Me.boillerQuantityAtStartUnitLabel.Location = New System.Drawing.Point(404, 594)
        Me.boillerQuantityAtStartUnitLabel.Name = "boillerQuantityAtStartUnitLabel"
        Me.boillerQuantityAtStartUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.boillerQuantityAtStartUnitLabel.TabIndex = 87
        Me.boillerQuantityAtStartUnitLabel.Text = "( T )"
        '
        'boillerQuantityAtStartLabel
        '
        Me.boillerQuantityAtStartLabel.AutoSize = True
        Me.boillerQuantityAtStartLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boillerQuantityAtStartLabel.Location = New System.Drawing.Point(12, 594)
        Me.boillerQuantityAtStartLabel.Name = "boillerQuantityAtStartLabel"
        Me.boillerQuantityAtStartLabel.Size = New System.Drawing.Size(154, 20)
        Me.boillerQuantityAtStartLabel.TabIndex = 86
        Me.boillerQuantityAtStartLabel.Text = "Quantité bouilloire début"
        '
        'boillerQuantityAtStartTextBox
        '
        Me.boillerQuantityAtStartTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boillerQuantityAtStartTextBox.Location = New System.Drawing.Point(291, 594)
        Me.boillerQuantityAtStartTextBox.Name = "boillerQuantityAtStartTextBox"
        Me.boillerQuantityAtStartTextBox.Size = New System.Drawing.Size(107, 22)
        Me.boillerQuantityAtStartTextBox.TabIndex = 85
        '
        'boillerQuantityAtEndUnitLabel
        '
        Me.boillerQuantityAtEndUnitLabel.AutoSize = True
        Me.boillerQuantityAtEndUnitLabel.Location = New System.Drawing.Point(404, 622)
        Me.boillerQuantityAtEndUnitLabel.Name = "boillerQuantityAtEndUnitLabel"
        Me.boillerQuantityAtEndUnitLabel.Size = New System.Drawing.Size(32, 20)
        Me.boillerQuantityAtEndUnitLabel.TabIndex = 90
        Me.boillerQuantityAtEndUnitLabel.Text = "( T )"
        '
        'boillerQuantityAtEndLabel
        '
        Me.boillerQuantityAtEndLabel.AutoSize = True
        Me.boillerQuantityAtEndLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boillerQuantityAtEndLabel.Location = New System.Drawing.Point(12, 622)
        Me.boillerQuantityAtEndLabel.Name = "boillerQuantityAtEndLabel"
        Me.boillerQuantityAtEndLabel.Size = New System.Drawing.Size(133, 20)
        Me.boillerQuantityAtEndLabel.TabIndex = 89
        Me.boillerQuantityAtEndLabel.Text = "Quantité bouilloire fin"
        '
        'boillerQuantityAtEndTextBox
        '
        Me.boillerQuantityAtEndTextBox.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boillerQuantityAtEndTextBox.Location = New System.Drawing.Point(291, 622)
        Me.boillerQuantityAtEndTextBox.Name = "boillerQuantityAtEndTextBox"
        Me.boillerQuantityAtEndTextBox.Size = New System.Drawing.Size(107, 22)
        Me.boillerQuantityAtEndTextBox.TabIndex = 88
        '
        'myCancelButton
        '
        Me.myCancelButton.Location = New System.Drawing.Point(321, 650)
        Me.myCancelButton.Name = "myCancelButton"
        Me.myCancelButton.Size = New System.Drawing.Size(106, 30)
        Me.myCancelButton.TabIndex = 91
        Me.myCancelButton.Text = "Annuler"
        Me.myCancelButton.UseVisualStyleBackColor = True
        '
        'okButton
        '
        Me.okButton.Location = New System.Drawing.Point(209, 650)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(106, 30)
        Me.okButton.TabIndex = 92
        Me.okButton.Text = "Ok"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'toggleOptionnalButton
        '
        Me.toggleOptionnalButton.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toggleOptionnalButton.Location = New System.Drawing.Point(16, 650)
        Me.toggleOptionnalButton.Name = "toggleOptionnalButton"
        Me.toggleOptionnalButton.Size = New System.Drawing.Size(187, 30)
        Me.toggleOptionnalButton.TabIndex = 93
        Me.toggleOptionnalButton.Text = "Données optionnelles"
        Me.toggleOptionnalButton.UseVisualStyleBackColor = True
        '
        'drumhoursCounterEndLabel
        '
        Me.drumhoursCounterEndLabel.AutoSize = True
        Me.drumhoursCounterEndLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.drumhoursCounterEndLabel.Location = New System.Drawing.Point(12, 456)
        Me.drumhoursCounterEndLabel.Name = "drumhoursCounterEndLabel"
        Me.drumhoursCounterEndLabel.Size = New System.Drawing.Size(170, 20)
        Me.drumhoursCounterEndLabel.TabIndex = 95
        Me.drumhoursCounterEndLabel.Text = "Compte-heures tambour fin"
        '
        'drumHoursCounterStartLabel
        '
        Me.drumHoursCounterStartLabel.AutoSize = True
        Me.drumHoursCounterStartLabel.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.drumHoursCounterStartLabel.Location = New System.Drawing.Point(12, 428)
        Me.drumHoursCounterStartLabel.Name = "drumHoursCounterStartLabel"
        Me.drumHoursCounterStartLabel.Size = New System.Drawing.Size(191, 20)
        Me.drumHoursCounterStartLabel.TabIndex = 98
        Me.drumHoursCounterStartLabel.Text = "Compte-heures tambour début"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.separationLine})
        Me.ShapeContainer1.Size = New System.Drawing.Size(439, 692)
        Me.ShapeContainer1.TabIndex = 100
        Me.ShapeContainer1.TabStop = False
        '
        'separationLine
        '
        Me.separationLine.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.separationLine.Name = "separationLine"
        Me.separationLine.X1 = 14
        Me.separationLine.X2 = 433
        Me.separationLine.Y1 = 411
        Me.separationLine.Y2 = 411
        '
        'drumsHoursCounterEndTimePicker
        '
        Me.drumsHoursCounterEndTimePicker.CalendarFont = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.drumsHoursCounterEndTimePicker.CustomFormat = "HH : mm"
        Me.drumsHoursCounterEndTimePicker.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.drumsHoursCounterEndTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.drumsHoursCounterEndTimePicker.Location = New System.Drawing.Point(291, 454)
        Me.drumsHoursCounterEndTimePicker.Name = "drumsHoursCounterEndTimePicker"
        Me.drumsHoursCounterEndTimePicker.ShowUpDown = True
        Me.drumsHoursCounterEndTimePicker.Size = New System.Drawing.Size(74, 22)
        Me.drumsHoursCounterEndTimePicker.TabIndex = 102
        Me.drumsHoursCounterEndTimePicker.Value = New Date(2013, 4, 8, 0, 0, 0, 0)
        '
        'drumHoursCounterStartTimePicker
        '
        Me.drumHoursCounterStartTimePicker.CalendarFont = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.drumHoursCounterStartTimePicker.CustomFormat = "HH : mm"
        Me.drumHoursCounterStartTimePicker.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.drumHoursCounterStartTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.drumHoursCounterStartTimePicker.Location = New System.Drawing.Point(291, 426)
        Me.drumHoursCounterStartTimePicker.Name = "drumHoursCounterStartTimePicker"
        Me.drumHoursCounterStartTimePicker.ShowUpDown = True
        Me.drumHoursCounterStartTimePicker.Size = New System.Drawing.Size(74, 22)
        Me.drumHoursCounterStartTimePicker.TabIndex = 101
        Me.drumHoursCounterStartTimePicker.Value = New Date(2013, 4, 8, 0, 0, 0, 0)
        '
        'drumHoursCounterStartUnitLabel
        '
        Me.drumHoursCounterStartUnitLabel.AutoSize = True
        Me.drumHoursCounterStartUnitLabel.Location = New System.Drawing.Point(371, 428)
        Me.drumHoursCounterStartUnitLabel.Name = "drumHoursCounterStartUnitLabel"
        Me.drumHoursCounterStartUnitLabel.Size = New System.Drawing.Size(65, 20)
        Me.drumHoursCounterStartUnitLabel.TabIndex = 103
        Me.drumHoursCounterStartUnitLabel.Text = "( hh:mm )"
        '
        'drumHoursCounterEndUnitLabel
        '
        Me.drumHoursCounterEndUnitLabel.AutoSize = True
        Me.drumHoursCounterEndUnitLabel.Location = New System.Drawing.Point(371, 456)
        Me.drumHoursCounterEndUnitLabel.Name = "drumHoursCounterEndUnitLabel"
        Me.drumHoursCounterEndUnitLabel.Size = New System.Drawing.Size(65, 20)
        Me.drumHoursCounterEndUnitLabel.TabIndex = 104
        Me.drumHoursCounterEndUnitLabel.Text = "( hh:mm )"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(371, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 20)
        Me.Label3.TabIndex = 105
        Me.Label3.Text = "( hh:mm )"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(371, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 20)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "( hh:mm )"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(371, 129)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 20)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "( hh:mm )"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(371, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 108
        Me.Label6.Text = "( hh:mm )"
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(263, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(22, 22)
        Me.Panel1.TabIndex = 109
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(263, 71)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(22, 22)
        Me.Panel2.TabIndex = 110
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(263, 99)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(22, 22)
        Me.Panel3.TabIndex = 110
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(263, 127)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(22, 22)
        Me.Panel4.TabIndex = 110
        '
        'Panel5
        '
        Me.Panel5.Location = New System.Drawing.Point(263, 155)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(22, 22)
        Me.Panel5.TabIndex = 110
        '
        'Panel6
        '
        Me.Panel6.Location = New System.Drawing.Point(263, 183)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(22, 22)
        Me.Panel6.TabIndex = 110
        '
        'Panel7
        '
        Me.Panel7.Location = New System.Drawing.Point(263, 211)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(22, 22)
        Me.Panel7.TabIndex = 110
        '
        'Panel8
        '
        Me.Panel8.Location = New System.Drawing.Point(263, 239)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(22, 22)
        Me.Panel8.TabIndex = 110
        '
        'Panel9
        '
        Me.Panel9.Location = New System.Drawing.Point(263, 267)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(22, 22)
        Me.Panel9.TabIndex = 110
        '
        'Panel10
        '
        Me.Panel10.Location = New System.Drawing.Point(263, 295)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(22, 22)
        Me.Panel10.TabIndex = 110
        '
        'Panel11
        '
        Me.Panel11.Location = New System.Drawing.Point(263, 321)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(22, 22)
        Me.Panel11.TabIndex = 110
        '
        'Panel12
        '
        Me.Panel12.Location = New System.Drawing.Point(263, 349)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(22, 22)
        Me.Panel12.TabIndex = 110
        '
        'Panel13
        '
        Me.Panel13.Location = New System.Drawing.Point(263, 377)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(22, 22)
        Me.Panel13.TabIndex = 110
        '
        'Panel14
        '
        Me.Panel14.Location = New System.Drawing.Point(263, 426)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(22, 22)
        Me.Panel14.TabIndex = 110
        '
        'Panel15
        '
        Me.Panel15.Location = New System.Drawing.Point(263, 454)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(22, 22)
        Me.Panel15.TabIndex = 110
        '
        'Panel16
        '
        Me.Panel16.Location = New System.Drawing.Point(263, 482)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(22, 22)
        Me.Panel16.TabIndex = 110
        '
        'Panel17
        '
        Me.Panel17.Location = New System.Drawing.Point(263, 510)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(22, 22)
        Me.Panel17.TabIndex = 110
        '
        'Panel18
        '
        Me.Panel18.Location = New System.Drawing.Point(263, 538)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(22, 22)
        Me.Panel18.TabIndex = 110
        '
        'Panel19
        '
        Me.Panel19.Location = New System.Drawing.Point(263, 566)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(22, 22)
        Me.Panel19.TabIndex = 110
        '
        'Panel20
        '
        Me.Panel20.Location = New System.Drawing.Point(263, 594)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(22, 22)
        Me.Panel20.TabIndex = 110
        '
        'Panel21
        '
        Me.Panel21.Location = New System.Drawing.Point(263, 622)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(22, 22)
        Me.Panel21.TabIndex = 110
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 19)
        Me.Label1.TabIndex = 111
        Me.Label1.Text = "Opérateur"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Roger Champagne", "Simon Boivert", "Usine de St-Bruno"})
        Me.ComboBox1.Location = New System.Drawing.Point(104, 10)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(330, 28)
        Me.ComboBox1.TabIndex = 112
        '
        'ManualDataPrompt
        '
        Me.ClientSize = New System.Drawing.Size(439, 692)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel21)
        Me.Controls.Add(Me.Panel20)
        Me.Controls.Add(Me.Panel19)
        Me.Controls.Add(Me.Panel18)
        Me.Controls.Add(Me.Panel17)
        Me.Controls.Add(Me.Panel16)
        Me.Controls.Add(Me.Panel15)
        Me.Controls.Add(Me.Panel14)
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.drumHoursCounterEndUnitLabel)
        Me.Controls.Add(Me.drumHoursCounterStartUnitLabel)
        Me.Controls.Add(Me.drumsHoursCounterEndTimePicker)
        Me.Controls.Add(Me.drumHoursCounterStartTimePicker)
        Me.Controls.Add(Me.drumHoursCounterStartLabel)
        Me.Controls.Add(Me.drumhoursCounterEndLabel)
        Me.Controls.Add(Me.toggleOptionnalButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.myCancelButton)
        Me.Controls.Add(Me.boillerQuantityAtEndUnitLabel)
        Me.Controls.Add(Me.boillerQuantityAtEndLabel)
        Me.Controls.Add(Me.boillerQuantityAtEndTextBox)
        Me.Controls.Add(Me.boillerQuantityAtStartUnitLabel)
        Me.Controls.Add(Me.boillerQuantityAtStartLabel)
        Me.Controls.Add(Me.boillerQuantityAtStartTextBox)
        Me.Controls.Add(Me.fuelQuantityAtEnd2UnitLabel)
        Me.Controls.Add(Me.fuelQuantityAtEnd2Label)
        Me.Controls.Add(Me.fuelQuantityAtEnd2TextBox)
        Me.Controls.Add(Me.fuelQuantityAtStart2UnitLabel)
        Me.Controls.Add(Me.fuelQuantityAtStart2Label)
        Me.Controls.Add(Me.fuelQuantityAtStart2TextBox)
        Me.Controls.Add(Me.fuelQuantityAtEnd1UnitLabel)
        Me.Controls.Add(Me.fuelQuantityAtEnd1Label)
        Me.Controls.Add(Me.fuelQuantityAtEnd1TextBox)
        Me.Controls.Add(Me.fuelQuantityAtStart1UnitLabel)
        Me.Controls.Add(Me.fuelQuantityAtStart1Label)
        Me.Controls.Add(Me.fuelQuantityAtStart1TextBox)
        Me.Controls.Add(Me.rejectedFillerUnitLabel)
        Me.Controls.Add(Me.rejectedFillerLabel)
        Me.Controls.Add(Me.rejectedFillerTextBox)
        Me.Controls.Add(Me.rejectedAggregatesUnitLabel)
        Me.Controls.Add(Me.rejectedAggregatesLabel)
        Me.Controls.Add(Me.rejectedAggregatesTextBox)
        Me.Controls.Add(Me.specialRecycledMixQuantityUnitLabel)
        Me.Controls.Add(Me.specialRecycleMixQuantity)
        Me.Controls.Add(Me.specialRecycledMixQuantityTextBox)
        Me.Controls.Add(Me.normalRecycledMixQuantityUnitLabel)
        Me.Controls.Add(Me.normalRecycledMixQuantityLabel)
        Me.Controls.Add(Me.normalRecycledMixQuantityTextBox)
        Me.Controls.Add(Me.fuelQuantityUnitLabel)
        Me.Controls.Add(Me.fuelQuantityLabel)
        Me.Controls.Add(Me.fuelQuantityTextBox)
        Me.Controls.Add(Me.lastLoadingTimeTimePicker)
        Me.Controls.Add(Me.firstLoadingTimeTimePicker)
        Me.Controls.Add(Me.lastLoadingTimeLabel)
        Me.Controls.Add(Me.firstLoadingTimeLabel)
        Me.Controls.Add(Me.weightedQuantityUnitLabel)
        Me.Controls.Add(Me.weightedQuantityLabel)
        Me.Controls.Add(Me.weightedQuantityTextBox)
        Me.Controls.Add(Me.rejectedMixQuantityUnitLabel)
        Me.Controls.Add(Me.rejectedMixQuantityLabel)
        Me.Controls.Add(Me.rejectedMixQuantityTextBox)
        Me.Controls.Add(Me.siloContentAtEndUnitLabel)
        Me.Controls.Add(Me.siloContentAtEndLabel)
        Me.Controls.Add(Me.siloContentAtEndTextBox)
        Me.Controls.Add(Me.siloContentAtStartUnitLabel)
        Me.Controls.Add(Me.siloContentAtStartLabel)
        Me.Controls.Add(Me.siloContentAtStartTextBox)
        Me.Controls.Add(Me.endTimePicker)
        Me.Controls.Add(Me.startTimePicker)
        Me.Controls.Add(Me.endLabel)
        Me.Controls.Add(Me.startLabel)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "ManualDataPrompt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Données supplémentaires"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Sub toggleOptionnalData() Handles toggleOptionnalButton.Click

        ' Toggle visibility for optionnal components
        For Each component As Control In Me.optionnalDataComponents

            component.Visible = Not component.Visible

        Next


        If (Me.state = WINDOW_STATE.CONDENSED) Then

            Me.state = WINDOW_STATE.EXPANDED

            Me.Size = New Size(Me.Size.Width, WINDOWS_HEIGHT_EXPANDED)
            Me.ClientSize = New Size(Me.ClientSize.Width, WINDOWS_HEIGHT_EXPANDED)

            Me.okButton.Location = New Point(okButton.Location.X, BUTTONS_Y_EXPANDED)
            Me.myCancelButton.Location = New Point(myCancelButton.Location.X, BUTTONS_Y_EXPANDED)
            Me.toggleOptionnalButton.Location = New Point(toggleOptionnalButton.Location.X, BUTTONS_Y_EXPANDED)

            toggleOptionnalButton.Text = TOGGLE_OPTIONNAL_DATA_BUTTON_TEXT_EXPANDED


        ElseIf (Me.state = Constants.UserInterface.ManualDataPrompt.WINDOW_STATE.EXPANDED) Then

            Me.state = WINDOW_STATE.CONDENSED

            Me.Size = New Size(Me.Size.Width, WINDOWS_HEIGHT_CONDENSED)
            Me.ClientSize = New Size(Me.ClientSize.Width, WINDOWS_HEIGHT_CONDENSED)

            Me.okButton.Location = New Point(okButton.Location.X, BUTTONS_Y_CONDENSED)
            Me.myCancelButton.Location = New Point(myCancelButton.Location.X, BUTTONS_Y_CONDENSED)
            Me.toggleOptionnalButton.Location = New Point(toggleOptionnalButton.Location.X, BUTTONS_Y_CONDENSED)

            toggleOptionnalButton.Text = TOGGLE_OPTIONNAL_DATA_BUTTON_TEXT_CONDENSED

        End If

        Me.Refresh()

    End Sub


    Private Sub whenPressEnter(sender As Object, e As KeyEventArgs) _
Handles boillerQuantityAtEndTextBox.KeyDown, boillerQuantityAtStartTextBox.KeyDown, fuelQuantityAtEnd1TextBox.KeyDown, fuelQuantityAtEnd2TextBox.KeyDown, fuelQuantityAtStart1TextBox.KeyDown, fuelQuantityAtStart2TextBox.KeyDown, fuelQuantityTextBox.KeyDown, normalRecycledMixQuantityTextBox.KeyDown, rejectedAggregatesTextBox.KeyDown, rejectedFillerTextBox.KeyDown, rejectedMixQuantityTextBox.KeyDown, siloContentAtEndTextBox.KeyDown, siloContentAtStartTextBox.KeyDown, specialRecycledMixQuantityTextBox.KeyDown, weightedQuantityTextBox.KeyDown, endTimePicker.KeyDown, firstLoadingTimeTimePicker.KeyDown, lastLoadingTimeTimePicker.KeyDown, startTimePicker.KeyDown


        If (e.KeyCode = Keys.Enter) Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub okButtonClick() Handles okButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cancelButtonClick() Handles myCancelButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ' Value changed listeners
    Private Sub startTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles startTimePicker.ValueChanged

        Try

            Me.manualData.START = startTimePicker.Value

        Catch dataEx As IncorrectDataException

            startTimePicker.Value = dataEx.OLD_VALUE

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, START_LABEL_TEXT))

        Catch ex As Exception

            Debugger.Break()

        End Try
    End Sub

    Private Sub endTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles endTimePicker.ValueChanged

        Try

            Me.manualData.END_ = endTimePicker.Value

        Catch dataEx As IncorrectDataException

            endTimePicker.Value = dataEx.OLD_VALUE

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, END_LABEL_TEXT))

        Catch ex As Exception

            Debugger.Break()

        End Try
    End Sub

    Private Sub pauseTimePicker_ValueChanged(sender As Object, e As EventArgs)

        Try

            'Me.manualData.PAUSE = TimeSpan.FromHours(pauseTimePicker.Value.Hour).Add(TimeSpan.FromMinutes(pauseTimePicker.Value.Minute))

        Catch dataEx As IncorrectDataException

            'pauseTimePicker.Value = dataEx.OLD_VALUE

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, PAUSE_LABEL_TEXT))

        Catch ex As Exception

            Debugger.Break()

        End Try
    End Sub

    Private Sub plannedMaintenanceTimePicker_ValueChanged(sender As Object, e As EventArgs)

        Try

            'Me.manualData.PLANNED_MAINTENANCE = TimeSpan.FromHours(plannedMaintenanceTimePicker.Value.Hour).Add(TimeSpan.FromMinutes(plannedMaintenanceTimePicker.Value.Minute))

        Catch dataEx As IncorrectDataException

            'plannedMaintenanceTimePicker.Value = dataEx.OLD_VALUE

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, PLANNED_MAINTENANCE_LABEL_TEXT))

        Catch ex As Exception

            Debugger.Break()

        End Try
    End Sub

    Private Sub siloContentAtStartTextBox_TextChanged(sender As Object, e As EventArgs) Handles siloContentAtStartTextBox.TextChanged

        Try

            Me.manualData.SILO_CONTENT_AT_START = Double.Parse(siloContentAtStartTextBox.Text)

            Me.siloContentAtStartTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, SILO_CONTENT_AT_START_LABEL_TEXT))
            Me.siloContentAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(siloContentAtStartTextBox.Text, Nothing), SILO_CONTENT_AT_START_LABEL_TEXT))
            Me.siloContentAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.siloContentAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub quantityTextBox_TextChanged(sender As Object, e As EventArgs)

        'Try

        '    Me.manualData.QUANTITY = Double.Parse(quantityTextBox.Text)

        '    Me.quantityTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        'Catch dataEx As IncorrectDataException

        '    UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, QUANTITY_LABEL_TEXT))
        '    Me.quantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        'Catch parsEx As FormatException

        '    UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(quantityTextBox.Text, Nothing), QUANTITY_LABEL_TEXT))
        '    Me.quantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        'Catch ex As Exception

        '    Debugger.Break()
        '    Me.quantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        'End Try
    End Sub

    Private Sub siloContentAtEndTextBox_TextChanged(sender As Object, e As EventArgs) Handles siloContentAtEndTextBox.TextChanged

        Try

            Me.manualData.SILO_CONTENT_AT_END = Double.Parse(siloContentAtEndTextBox.Text)

            Me.siloContentAtEndTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, SILO_CONTENT_AT_END_LABEL_TEXT))
            Me.siloContentAtEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(siloContentAtEndTextBox.Text, Nothing), SILO_CONTENT_AT_END_LABEL_TEXT))
            Me.siloContentAtEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.siloContentAtEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub rejectedMixQuantityTextBox_TextChanged(sender As Object, e As EventArgs) Handles rejectedMixQuantityTextBox.TextChanged

        Try

            Me.manualData.REJECTED_MIX = Double.Parse(rejectedMixQuantityTextBox.Text)

            Me.rejectedMixQuantityTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, REJECTED_MIX_QUANTITY_LABEL_TEXT))
            Me.rejectedMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(rejectedMixQuantityTextBox.Text, Nothing), REJECTED_MIX_QUANTITY_LABEL_TEXT))
            Me.rejectedMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.rejectedMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub weightedQuantityTextBox_TextChanged(sender As Object, e As EventArgs) Handles weightedQuantityTextBox.TextChanged

        Try

            Me.manualData.WEIGHTED_QUANTITY = Double.Parse(weightedQuantityTextBox.Text)

            Me.weightedQuantityTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, WEIGHTED_QUANTITY_LABEL_TEXT))
            Me.weightedQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(weightedQuantityTextBox.Text, Nothing), WEIGHTED_QUANTITY_LABEL_TEXT))
            Me.weightedQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.weightedQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub firstLoadingTimeTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles firstLoadingTimeTimePicker.ValueChanged

        Try

            Me.manualData.START = firstLoadingTimeTimePicker.Value

        Catch dataEx As IncorrectDataException

            firstLoadingTimeTimePicker.Value = dataEx.OLD_VALUE

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, FIRST_LOADING_TIME_LABEL_TEXT))

        Catch ex As Exception

            Debugger.Break()

        End Try
    End Sub

    Private Sub lastLoadingTimeTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles lastLoadingTimeTimePicker.ValueChanged

        Try

            Me.manualData.LAST_LOADING_TIME = lastLoadingTimeTimePicker.Value

        Catch dataEx As IncorrectDataException

            lastLoadingTimeTimePicker.Value = dataEx.OLD_VALUE

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, LAST_LOADING_TIME_LABEL_TEXT))

        Catch ex As Exception

            Debugger.Break()

        End Try
    End Sub

    Private Sub fuelQuantityTextBox_TextChanged(sender As Object, e As EventArgs) Handles fuelQuantityTextBox.TextChanged

        Try

            Me.manualData.FUEL_QUANTITY = Double.Parse(fuelQuantityTextBox.Text)

            Me.fuelQuantityTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, FUEL_QUANTITY_LABEL_TEXT))
            Me.fuelQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(fuelQuantityTextBox.Text, Nothing), FUEL_QUANTITY_LABEL_TEXT))
            Me.fuelQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.fuelQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub normalRecycledMixQuantityTextBox_TextChanged(sender As Object, e As EventArgs) Handles normalRecycledMixQuantityTextBox.TextChanged

        Try

            Me.manualData.NORMAL_RECYCLED_MIX_QUANTITY = Double.Parse(normalRecycledMixQuantityTextBox.Text)

            Me.normalRecycledMixQuantityTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, NORMAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT))
            Me.normalRecycledMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(normalRecycledMixQuantityTextBox.Text, Nothing), NORMAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT))
            Me.normalRecycledMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.normalRecycledMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub specialRecycledMixQuantityTextBox_TextChanged(sender As Object, e As EventArgs) Handles specialRecycledMixQuantityTextBox.TextChanged

        Try

            Me.manualData.SPECIAL_RECYCLED_MIX_QUANTITY = Double.Parse(specialRecycledMixQuantityTextBox.Text)

            Me.specialRecycledMixQuantityTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, SPECIAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT))
            Me.specialRecycledMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(specialRecycledMixQuantityTextBox.Text, Nothing), SPECIAL_RECYCLED_MIX_QUANTITY_LABEL_TEXT))
            Me.specialRecycledMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.specialRecycledMixQuantityTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub rejectedAggregatesTextBox_TextChanged(sender As Object, e As EventArgs) Handles rejectedAggregatesTextBox.TextChanged

        Try

            Me.manualData.REJECTED_AGGREGATES = Double.Parse(rejectedAggregatesTextBox.Text)

            Me.rejectedAggregatesTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, REJECTED_AGGREGATES_LABEL_TEXT))
            Me.rejectedAggregatesTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(rejectedAggregatesTextBox.Text, Nothing), REJECTED_AGGREGATES_LABEL_TEXT))
            Me.rejectedAggregatesTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.rejectedAggregatesTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub rejectedFillerTextBox_TextChanged(sender As Object, e As EventArgs) Handles rejectedFillerTextBox.TextChanged

        Try

            Me.manualData.REJECTED_FILLER = Double.Parse(rejectedFillerTextBox.Text)

            Me.rejectedFillerTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, REJECTED_FILLER_LABEL_TEXT))
            Me.rejectedFillerTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(rejectedFillerTextBox.Text, Nothing), REJECTED_FILLER_LABEL_TEXT))
            Me.rejectedFillerTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.rejectedFillerTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub drumQuantityAtStartTextBox_TextChanged(sender As Object, e As EventArgs)

        Try

            'Me.manualData.DRUM_QUANTITY_AT_START = Double.Parse(drumQuantityAtStartTextBox.Text)

            'Me.drumQuantityAtStartTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, DRUM_QUANTITY_AT_START_LABEL_TEXT))
            'Me.drumQuantityAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            'UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(drumQuantityAtStartTextBox.Text, Nothing), DRUM_QUANTITY_AT_START_LABEL_TEXT))
            'Me.drumQuantityAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            'Me.drumQuantityAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub drumQuantityEndTextBox_TextChanged(sender As Object, e As EventArgs)

        Try

            'Me.manualData.DRUM_QUANTITY_AT_END = Double.Parse(drumQuantityEndTextBox.Text)

            'Me.drumQuantityEndTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, DRUM_QUANTITY_AT_END_LABEL_TEXT))
            'Me.drumQuantityEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            'UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(drumQuantityEndTextBox.Text, Nothing), DRUM_QUANTITY_AT_END_LABEL_TEXT))
            'Me.drumQuantityEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            'Me.drumQuantityEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub fuelQuantityAtStart1TextBox_TextChanged(sender As Object, e As EventArgs) Handles fuelQuantityAtStart1TextBox.TextChanged

        Try

            Me.manualData.FUEL_QUANTITY_AT_START_1 = Double.Parse(fuelQuantityAtStart1TextBox.Text)

            Me.fuelQuantityAtStart1TextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, FUEL_QUANTITY_AT_START_1_LABEL_TEXT))
            Me.fuelQuantityAtStart1TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(fuelQuantityAtStart1TextBox.Text, Nothing), FUEL_QUANTITY_AT_START_1_LABEL_TEXT))
            Me.fuelQuantityAtStart1TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.fuelQuantityAtStart1TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub fuelQuantityAtEnd1TextBox_TextChanged(sender As Object, e As EventArgs) Handles fuelQuantityAtEnd1TextBox.TextChanged

        Try

            Me.manualData.FUEL_QUANTITY_AT_END_1 = Double.Parse(fuelQuantityAtEnd1TextBox.Text)

            Me.fuelQuantityAtEnd1TextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, FUEL_QUANTITY_AT_END_1_LABEL_TEXT))
            Me.fuelQuantityAtEnd1TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(fuelQuantityAtEnd1TextBox.Text, Nothing), FUEL_QUANTITY_AT_END_1_LABEL_TEXT))
            Me.fuelQuantityAtEnd1TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.fuelQuantityAtEnd1TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub fuelQuantityAtStart2TextBox_TextChanged(sender As Object, e As EventArgs) Handles fuelQuantityAtStart2TextBox.TextChanged

        Try

            Me.manualData.FUEL_QUANTITY_AT_START_2 = Double.Parse(fuelQuantityAtStart2TextBox.Text)

            Me.fuelQuantityAtStart2TextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, FUEL_QUANTITY_AT_START_2_LABEL_TEXT))
            Me.fuelQuantityAtStart2TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(fuelQuantityAtStart2TextBox.Text, Nothing), FUEL_QUANTITY_AT_START_2_LABEL_TEXT))
            Me.fuelQuantityAtStart2TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.fuelQuantityAtStart2TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub fuelQuantityAtEnd2TextBox_TextChanged(sender As Object, e As EventArgs) Handles fuelQuantityAtEnd2TextBox.TextChanged

        Try

            Me.manualData.FUEL_QUANTITY_AT_END_2 = Double.Parse(fuelQuantityAtEnd2TextBox.Text)

            Me.fuelQuantityAtEnd2TextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, FUEL_QUANTITY_AT_END_2_LABEL_TEXT))
            Me.fuelQuantityAtEnd2TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(fuelQuantityAtEnd2TextBox.Text, Nothing), FUEL_QUANTITY_AT_END_2_LABEL_TEXT))
            Me.fuelQuantityAtEnd2TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.fuelQuantityAtEnd2TextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub boillerQuantityAtStartTextBox_TextChanged(sender As Object, e As EventArgs) Handles boillerQuantityAtStartTextBox.TextChanged

        Try

            Me.manualData.BOILER_QUANTITY_AT_START = Double.Parse(boillerQuantityAtStartTextBox.Text)

            Me.boillerQuantityAtStartTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, BOILER_QUANTITY_AT_START_LABEL_TEXT))
            Me.boillerQuantityAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(boillerQuantityAtStartTextBox.Text, Nothing), BOILER_QUANTITY_AT_START_LABEL_TEXT))
            Me.boillerQuantityAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.boillerQuantityAtStartTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

    Private Sub boillerQuantityAtEndTextBox_TextChanged(sender As Object, e As EventArgs) Handles boillerQuantityAtEndTextBox.TextChanged

        Try

            Me.manualData.BOILER_QUANTITY_AT_END = Double.Parse(boillerQuantityAtEndTextBox.Text)

            Me.boillerQuantityAtEndTextBox.BackColor = CORRECT_TEXT_FIELD_COLOR

        Catch dataEx As IncorrectDataException

            UIExceptionHandler.instance.handle(New IncorrectInputException(dataEx, BOILER_QUANTITY_AT_END_LABEL_TEXT))
            Me.boillerQuantityAtEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch parsEx As FormatException

            UIExceptionHandler.instance.handle(New IncorrectInputException(New IncorrectDataException(boillerQuantityAtEndTextBox.Text, Nothing), BOILER_QUANTITY_AT_END_LABEL_TEXT))
            Me.boillerQuantityAtEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR

        Catch ex As Exception

            Debugger.Break()
            Me.boillerQuantityAtEndTextBox.BackColor = INCORRECT_TEXT_FIELD_COLOR
        End Try
    End Sub

End Class
