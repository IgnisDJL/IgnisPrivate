Imports IGNIS.UI.Common

Namespace UI

    Public MustInherit Class ReportViewTemplate
        Inherits ArchivesExplorerViewTemplate

        ' Components
        Protected WithEvents reportsToGenerateListControl As ReportsToGenerateListControl
        Protected WithEvents generateButton As Button

        ' Attributes

        Protected Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.reportsToGenerateListControl = New ReportsToGenerateListControl

            Me.generateButton = New Button
            Me.generateButton.TextAlign = ContentAlignment.MiddleCenter
            Me.generateButton.ImageAlign = ContentAlignment.MiddleRight
            Me.generateButton.Image = Constants.UI.Images._32x32.GOOD
            Me.generateButton.Text = "Générer!"
            Me.generateButton.Size = ReportViewLayout.GENERATE_BUTTONS_SIZE
            Me.generateButton.Font = Constants.UI.Fonts.DEFAULT_FONT_BOLD

            Me.Controls.Add(Me.reportsToGenerateListControl)
            Me.Controls.Add(Me.generateButton)

        End Sub


        Protected Overrides Sub ajustLayout(newSize As Size)
            MyBase.ajustLayout(newSize)

            Dim layout As ReportViewLayout = DirectCast(Me.layout, ReportViewLayout)

            ' Reports To Generate List
            Me.reportsToGenerateListControl.Location = layout.ReportsToGenerateList_Location
            Me.reportsToGenerateListControl.ajustLayout(layout.ReportsToGenerateList_Size)

            ' Generate Button
            Me.generateButton.Location = layout.GenerateButton_Location

        End Sub

        Protected Overrides Sub ajustLayoutFinal(newSize As Size)
            MyBase.ajustLayoutFinal(newSize)
        End Sub

        Protected Overrides Sub beforeShow()
            MyBase.beforeShow()

        End Sub

        Public MustOverride Overrides Sub afterShow()

        Public MustOverride Overloads Overrides ReadOnly Property Name As String

        Public MustOverride ReadOnly Property GenerateButtonIcon As Image

        Public Overrides Sub onHide()
            MyBase.onHide()

        End Sub

    End Class
End Namespace
