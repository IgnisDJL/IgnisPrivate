Namespace Commands.Settings

    Public Class AddRecipeInfo
        Inherits SettingsCommand

        Private formula As String
        Private mixName As String
        Private rapTargetPercentage As Double
        Private acTargetPercentage As Double

        Private newRecipeNode As XmlSettings.RecipesNode.RecipeInfo

        Public Sub New(formula As String, mixName As String, rapTargetPercentage As Double, acTargetPercentage As Double)

            Me.formula = formula
            Me.mixName = mixName
            Me.rapTargetPercentage = rapTargetPercentage
            Me.acTargetPercentage = acTargetPercentage

        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me.newRecipeNode)) Then
                Me.newRecipeNode = XmlSettings.Settings.instance.Usine.RecipesInfo.addRecipeInfo(Me.formula, False, Me.mixName, Me.rapTargetPercentage, Me.acTargetPercentage)
            Else
                XmlSettings.Settings.instance.Usine.RecipesInfo.addRecipeInfo(Me.newRecipeNode)
            End If

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.RecipesInfo.removeRecipeInfo(Me.newRecipeNode)

        End Sub
    End Class
End Namespace

