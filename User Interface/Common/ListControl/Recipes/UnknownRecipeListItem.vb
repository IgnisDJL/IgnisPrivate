Namespace UI

    Public Class UnknownRecipeListItem
        Inherits Common.ListItem(Of XmlSettings.RecipesNode.UnknownRecipeNode)

        ' Constants

        ' Components
        Private WithEvents formulaLabel As Label
        Private WithEvents mixNameLabel As Label

        ' Attributes

        ' Events

        Public Sub New(unknownRecipe As XmlSettings.RecipesNode.UnknownRecipeNode)
            MyBase.New(unknownRecipe)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.formulaLabel = New Label
            Me.formulaLabel.Text = Me.ItemObject.FORMULA

            Me.mixNameLabel = New Label
            Me.mixNameLabel.Text = Me.ItemObject.MIX_NAME

            Me.Controls.Add(Me.formulaLabel)
            Me.Controls.Add(Me.mixNameLabel)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.formulaLabel.Location = New Point(MixAndAsphaltSettingsViewLayout.SPACE_BETWEEN_CONTROLS_X, 0)
            Me.formulaLabel.Size = New Size(Me.Width * 2 / 6, Me.Height)

            Me.mixNameLabel.Location = New Point(Me.formulaLabel.Location.X + Me.formulaLabel.Size.Width + MixAndAsphaltSettingsViewLayout.SPACE_BETWEEN_CONTROLS_X, 0)
            Me.mixNameLabel.Size = Me.formulaLabel.Size

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, formulaLabel.Click, mixNameLabel.Click

            Me.raiseClickEvent()

        End Sub
    End Class
End Namespace

