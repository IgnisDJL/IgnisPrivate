Namespace UI.Common

    Public Class DatesListItem
        Inherits ListItem(Of ProductionDay_1)

        Private WithEvents dateLabel As Label
        Private WithEvents readyForReportPanel As Panel

        Public Sub New(productionDay As ProductionDay_1)
            MyBase.New(productionDay)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.dateLabel = New Label
            Me.dateLabel.AutoSize = False
            Me.dateLabel.Text = Me.ItemObject.getProductionDate.ToString("dd MMMM yyyy")
            Me.dateLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.readyForReportPanel = New Panel
            Me.readyForReportPanel.BackgroundImageLayout = ImageLayout.Center
            'Me.readyForReportPanel.BackgroundImage = If(Me.ItemObject.IsReportReady, Constants.UI.Images._24x24.GOOD, Constants.UI.Images._24x24.WRONG)

            Me.Controls.Add(dateLabel)
            Me.Controls.Add(readyForReportPanel)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.readyForReportPanel.Location = New Point(0, 0)
            Me.readyForReportPanel.Size = New Size(Me.Height, Me.Height)

            Me.dateLabel.Location = New Point(readyForReportPanel.Size.Width, 0)
            Me.dateLabel.Size = New Size(Me.Width - readyForReportPanel.Width, Me.Height)

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, dateLabel.Click, readyForReportPanel.Click
            Me.raiseClickEvent()
        End Sub

    End Class
End Namespace
