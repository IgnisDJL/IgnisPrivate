Namespace UI

    Public Class DailyReportViewLayout
        Inherits ReportViewLayout

        ' Constantes
        Private Shared ReadOnly NO_LAST_REPORT_READY_DATE_MESSAGE_PANEL_SIZE As Size = New Size(300, 100)

        ' Components Attributes
       Private _noLastReportReadyDateMessagePanel_location As Point
        Private _noLastReportReadyDateMessagePanel_size As Size

        Public Sub New()
            MyBase.New()

        End Sub

        Protected Overrides Sub computeLayout()
            MyBase.computeLayout()

            Me._noLastReportReadyDateMessagePanel_location = New Point(Me.Width / 2 - Me.NoLastReportReadyDateMessagePanel_Size.Width / 2, Me.Height / 2 - Me.NoLastReportReadyDateMessagePanel_Size.Height / 2)

        End Sub

        '
        ' No Last Report Ready Date Message Panel
        '
        Public ReadOnly Property NoLastReportReadyDateMessagePanel_Location As Point
            Get
                Return Me._noLastReportReadyDateMessagePanel_location
            End Get
        End Property
        Public ReadOnly Property NoLastReportReadyDateMessagePanel_Size As Size
            Get
                Return NO_LAST_REPORT_READY_DATE_MESSAGE_PANEL_SIZE
            End Get
        End Property

    End Class

End Namespace
