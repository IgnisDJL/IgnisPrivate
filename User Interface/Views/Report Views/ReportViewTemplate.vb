Imports IGNIS.UI.Common

Namespace UI

    Public Class ReportViewTemplate
        Inherits View

        ' Components
        'Protected WithEvents reportsToGenerateListControl As ReportsToGenerateListControl
        Protected WithEvents generateButton As Button
        Protected WithEvents backButton As BackButton

        ' Attributes

        Protected Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.generateButton = New Button
            Me.generateButton.TextAlign = ContentAlignment.MiddleCenter
            Me.generateButton.ImageAlign = ContentAlignment.MiddleRight
            Me.generateButton.Image = Constants.UI.Images._32x32.GOOD
            Me.generateButton.Text = "Générer!"
            Me.generateButton.Size = ReportViewLayout.GENERATE_BUTTONS_SIZE
            Me.generateButton.Font = Constants.UI.Fonts.DEFAULT_FONT_BOLD

            Me.backButton = New BackButton
            Me.backButton.Size = New Size(Common.BackButton.BUTTON_WIDTH, UI.LayoutManager.CONTROL_BUTTONS_HEIGHT)

            Me.Controls.Add(Me.generateButton)
            Me.Controls.Add(Me.backButton)
        End Sub


        Protected Overrides Sub ajustLayout(newSize As Size)
            'MyBase.ajustLayout(newSize)

            Dim layout As ReportViewLayout = DirectCast(Me.layout, ReportViewLayout)

            '' Reports To Generate List
            'Me.reportsToGenerateListControl.Location = layout.ReportsToGenerateList_Location
            'Me.reportsToGenerateListControl.ajustLayout(layout.ReportsToGenerateList_Size)

            ' Generate Button
            Me.generateButton.Location = layout.GenerateButton_Location
            Me.backButton.Location = layout.BackButton_Location

        End Sub
        Protected Sub backToMainMenu() Handles backButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.MainMenuView)
        End Sub

        Protected Overrides Sub ajustLayoutFinal(newSize As Size)
            'MyBase.ajustLayoutFinal(newSize)
        End Sub

        Protected Overrides Sub beforeShow()
            'MyBase.beforeShow()

        End Sub

        Public Overrides Sub onHide()
            'MyBase.onHide()

        End Sub

    End Class
End Namespace
