Namespace Commands.Settings

    Public Class UpdateRecipeInfo
        Inherits SettingsCommand

        Private newFormula As String
        Private newMixName As String
        Private newRAPTargetPercentage As Double
        Private newACTargetPercentage As Double

        Private oldFormula As String
        Private oldMixName As String
        Private oldRAPTargetPercentage As Double
        Private oldACTargetPercentage As Double

        Private recipeToUpdate As XmlSettings.RecipesNode.RecipeInfo

        Public Sub New(recipeToUpdate As XmlSettings.RecipesNode.RecipeInfo, newFormula As String, newMixName As String, newRAPTargetPercentage As Double, newACTargetPercentage As Double)

            Me.newFormula = newFormula
            Me.newMixName = newMixName
            Me.newRAPTargetPercentage = newRAPTargetPercentage
            Me.newACTargetPercentage = newACTargetPercentage

            Me.recipeToUpdate = recipeToUpdate

            Me.oldFormula = recipeToUpdate.FORMULA
            Me.oldMixName = recipeToUpdate.MIX_NAME
            Me.oldRAPTargetPercentage = recipeToUpdate.RECYCLED_SET_POINT_PERCENTAGE
            Me.oldACTargetPercentage = recipeToUpdate.ASPHALT_SET_POINT_PERCENTAGE
        End Sub

        Public Overrides Sub execute()

            With Me.recipeToUpdate
                .FORMULA = newFormula
                .MIX_NAME = newMixName
                .RECYCLED_SET_POINT_PERCENTAGE = newRAPTargetPercentage
                .ASPHALT_SET_POINT_PERCENTAGE = newACTargetPercentage
            End With

        End Sub

        Public Overrides Sub undo()

            With Me.recipeToUpdate
                .FORMULA = oldFormula
                .MIX_NAME = oldMixName
                .RECYCLED_SET_POINT_PERCENTAGE = oldRAPTargetPercentage
                .ASPHALT_SET_POINT_PERCENTAGE = oldACTargetPercentage
            End With
        End Sub
    End Class
End Namespace

