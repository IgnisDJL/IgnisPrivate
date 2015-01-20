Namespace UI

    Public Class ReportsToGenerateListItem
        Inherits Common.ListItem(Of ReportFile.ReportTypes)


        ' Components
        Private WithEvents iconPanel As Panel
        Private WithEvents checkBox As CheckBox
        Private WithEvents reportNameLabel As Label

        ' Attributes
        Private checked As Boolean

        ' Events
        Public Event CheckedChange(item As ReportsToGenerateListItem, checked As Boolean)

        Public Sub New(reportType As ReportFile.ReportTypes)
            MyBase.New(reportType)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            ' Checkbox
            Me.checkBox = New CheckBox
            Me.checkBox.CheckAlign = ContentAlignment.MiddleCenter

            ' Icon Panel
            Me.iconPanel = New Panel
            Me.iconPanel.BackgroundImageLayout = ImageLayout.Center

            ' Label
            Me.reportNameLabel = New Label
            Me.reportNameLabel.AutoSize = False
            Me.reportNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Select Case Me.ItemObject

                Case ReportFile.ReportTypes.SummaryDailyReport

                    Me.reportNameLabel.Text = SummaryDailyReport.GENERIC_NAME
                    Me.iconPanel.BackgroundImage = Constants.UI.Images._24x24.SUMMARY_DAILY_REPORT

                Case ReportFile.ReportTypes.SummaryNightShiftReport

                    Me.reportNameLabel.Text = "Rapport de nuit"

            End Select

            Me.Controls.Add(iconPanel)
            Me.Controls.Add(reportNameLabel)
            Me.Controls.Add(checkBox)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.checkBox.Location = New Point(2, 0)
            Me.checkBox.Size = New Size(Me.Height, Me.Height)

            Me.iconPanel.Location = New Point(checkBox.Location.X + checkBox.Width, 0)
            Me.iconPanel.Size = New Size(Me.Height, Me.Height)

            Me.reportNameLabel.Location = New Point(Me.iconPanel.Location.X + Me.iconPanel.Width, 0)
            Me.reportNameLabel.Size = New Size(Me.Width - Me.iconPanel.Width - Me.checkBox.Width - 2, Me.Height)

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Public Property IsChecked As Boolean
            Get
                Return Me.checkBox.Checked
            End Get
            Set(value As Boolean)
                Me.checkBox.Checked = value
            End Set
        End Property

        Private Sub onCheck() Handles checkBox.CheckedChanged
            RaiseEvent CheckedChange(Me, Me.checkBox.Checked)
        End Sub

        Private Sub checkOnClick() Handles reportNameLabel.Click, Me.Click, iconPanel.Click
            Me.checkBox.Checked = Not Me.checkBox.Checked
        End Sub
    End Class

End Namespace
