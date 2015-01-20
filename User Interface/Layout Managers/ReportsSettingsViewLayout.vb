Namespace UI

    Public Class ReportsSettingsViewLayout
        Inherits LayoutManager

        Public Shared ReadOnly MINIMUM_SIZE As Size = New Size(0, 0)
        Public Shared ReadOnly CONDENSED_SIZE As Size = New Size(0, 0)

        Public Shared Shadows ReadOnly FIELDS_HEIGHT As Integer = 45
        Public Shared ReadOnly SUMMARY_DAILY_REPORT_ENABLE_CHECK_BOX_SIZE As Size = New Size(400, FIELDS_HEIGHT)

        Public Shared ReadOnly OPEN_SUMMARY_DAILY_REPORT_OPTIONS_CHECKBOX_SIZE As Size = New Size(185, FIELDS_HEIGHT)
        Public Shared ReadOnly UNITS_PANEL_HEIGHT = 200

        ' Components Attributes

        ' Summary Daily Report Enable Check Box
        Private _summaryDailyReportEnableCheckBox_location As Point
        Private _summaryDailyReportEnableCheckBox_size As Size

        ' Open Summary Daily Report When Done Label
        Private _openSummaryDailyReportWhenDoneLabel_location As Point
        Private _openSummaryDailyReportWhenDoneLabel_size As Size

        ' Open Summary Daily Report Read Only Option Check Box
        Private _openSummaryDailyReportReadOnlyOptionCheckBox_location As Point
        Private _openSummaryDailyReportReadOnlyOptionCheckBox_size As Size

        ' Open Summary Daily Report Writable Option Check Box
        Private _openSummaryDailyReportWritableOptionCheckBox_location As Point
        Private _openSummaryDailyReportWritableOptionCheckBox_size As Size

        ' Reports Units Panel
        Private _reportsUnitsPanel_location As Point
        Private _reportsUnitsPanel_size As Size


        ' Attributes
        Public Sub New()
            MyBase.New(MINIMUM_SIZE, CONDENSED_SIZE)

        End Sub

        Protected Overloads Overrides Sub computeLayout()

            ' Summary Daily Report Enable Check Box
            Me._summaryDailyReportEnableCheckBox_location = New Point(LOCATION_START_X, LOCATION_START_Y)
            Me._summaryDailyReportEnableCheckBox_size = SUMMARY_DAILY_REPORT_ENABLE_CHECK_BOX_SIZE

            ' Open Summary Daily Report When Done Label
            Me._openSummaryDailyReportWhenDoneLabel_location = New Point(LOCATION_START_X, Me.SummaryDailyReportEnableCheckBox_Location.Y + Me.SummaryDailyReportEnableCheckBox_Size.Height)
            Me._openSummaryDailyReportWhenDoneLabel_size = New Size(Me.Width - 2 * LOCATION_START_X, FIELDS_HEIGHT)

            ' Open Summary Daily Report Read Only Option Check Box
            Me._openSummaryDailyReportReadOnlyOptionCheckBox_location = New Point(2 * LOCATION_START_X, Me.OpenSummaryDailyReportWhenDoneLabel_Location.Y + Me.OpenSummaryDailyReportWhenDoneLabel_Size.Height)
            Me._openSummaryDailyReportReadOnlyOptionCheckBox_size = OPEN_SUMMARY_DAILY_REPORT_OPTIONS_CHECKBOX_SIZE

            ' Open Summary Daily Report Writable Option Check Box
            Me._openSummaryDailyReportWritableOptionCheckBox_location = New Point(Me.OpenSummaryDailyReportReadOnlyOptionCheckBox_Location.X + Me.OpenSummaryDailyReportReadOnlyOptionCheckBox_Size.Width + 2 * SPACE_BETWEEN_CONTROLS_X, Me.OpenSummaryDailyReportReadOnlyOptionCheckBox_Location.Y)
            Me._openSummaryDailyReportWritableOptionCheckBox_size = OPEN_SUMMARY_DAILY_REPORT_OPTIONS_CHECKBOX_SIZE

            ' Reports Units Panel
            Me._reportsUnitsPanel_location = New Point(LOCATION_START_X, Me.OpenSummaryDailyReportWritableOptionCheckBox_Location.Y + Me.OpenSummaryDailyReportWritableOptionCheckBox_Size.Height + 2 * SPACE_BETWEEN_CONTROLS_Y)
            Me._reportsUnitsPanel_size = New Size(Me.Width - 2 * LOCATION_START_X, UNITS_PANEL_HEIGHT)

        End Sub

        ' 
        ' Summary Daily Report Enable Check Box
        ' 
        Public ReadOnly Property SummaryDailyReportEnableCheckBox_Location As Point
            Get
                Return Me._summaryDailyReportEnableCheckBox_location
            End Get
        End Property
        Public ReadOnly Property SummaryDailyReportEnableCheckBox_Size As Size
            Get
                Return Me._summaryDailyReportEnableCheckBox_size
            End Get
        End Property
        ' 
        ' Open Summary Daily Report When Done Label
        ' 
        Public ReadOnly Property OpenSummaryDailyReportWhenDoneLabel_Location As Point
            Get
                Return Me._openSummaryDailyReportWhenDoneLabel_location
            End Get
        End Property
        Public ReadOnly Property OpenSummaryDailyReportWhenDoneLabel_Size As Size
            Get
                Return Me._openSummaryDailyReportWhenDoneLabel_size
            End Get
        End Property
        ' 
        ' Open Summary Daily Report Read Only Option Check Box
        ' 
        Public ReadOnly Property OpenSummaryDailyReportReadOnlyOptionCheckBox_Location As Point
            Get
                Return Me._openSummaryDailyReportReadOnlyOptionCheckBox_location
            End Get
        End Property
        Public ReadOnly Property OpenSummaryDailyReportReadOnlyOptionCheckBox_Size As Size
            Get
                Return Me._openSummaryDailyReportReadOnlyOptionCheckBox_size
            End Get
        End Property
        ' 
        ' Open Summary Daily Report Writable Option Check Box
        ' 
        Public ReadOnly Property OpenSummaryDailyReportWritableOptionCheckBox_Location As Point
            Get
                Return Me._openSummaryDailyReportWritableOptionCheckBox_location
            End Get
        End Property
        Public ReadOnly Property OpenSummaryDailyReportWritableOptionCheckBox_Size As Size
            Get
                Return Me._openSummaryDailyReportWritableOptionCheckBox_size
            End Get
        End Property
        ' 
        ' Reports Units Panel
        ' 
        Public ReadOnly Property ReportsUnitsPanel_Location As Point
            Get
                Return Me._reportsUnitsPanel_location
            End Get
        End Property
        Public ReadOnly Property ReportsUnitsPanel_Size As Size
            Get
                Return Me._reportsUnitsPanel_size
            End Get
        End Property

    End Class
End Namespace
