Namespace Commands.Settings

    Public Class RemoveRecipeInfo
        Inherits SettingsCommand

        Private recipeToRemove As XmlSettings.RecipesNode.RecipeInfo

        Public Sub New(recipeToRemove As XmlSettings.RecipesNode.RecipeInfo)

            Me.recipeToRemove = recipeToRemove
        End Sub

        Public Overrides Sub execute()

            XmlSettings.Settings.instance.Usine.RecipesInfo.removeRecipeInfo(Me.recipeToRemove)

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.RecipesInfo.addRecipeInfo(Me.recipeToRemove)

        End Sub
    End Class
End Namespace

