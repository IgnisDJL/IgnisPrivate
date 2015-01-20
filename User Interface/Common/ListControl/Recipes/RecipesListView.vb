Namespace UI

    Public Class RecipesListView
        Inherits Common.ListControlTemplate(Of XmlSettings.RecipesNode.RecipeInfo)

        ' Events
        Public Event deleteRecipe(_recipe As XmlSettings.RecipesNode.RecipeInfo)
        Public Event updateRecipe(_recipe As XmlSettings.RecipesNode.RecipeInfo, newFormula As String, newMixName As String, newRecycledTargetPercentage As Double, newAsphaltTargetPercentage As Double)

        Public Sub New()
            MyBase.New("Recettes")

        End Sub

        Public Overrides Sub addObject(obj As XmlSettings.RecipesNode.RecipeInfo)

            Dim newItem = New RecipesListItem(obj)

            ' #todo - when clear() is called, these should be unbound
            ' or #refactor, find a better way to pass the events
            AddHandler newItem.deleteRecipe, AddressOf Me.raiseDeleteEvent
            AddHandler newItem.updateRecipe, AddressOf Me.raiseUpdateEvent

            Me.addItem(newItem)

        End Sub

        Private Sub raiseDeleteEvent(_recipe As XmlSettings.RecipesNode.RecipeInfo)

            RaiseEvent deleteRecipe(_recipe)

        End Sub

        Private Sub raiseUpdateEvent(_recipe As XmlSettings.RecipesNode.RecipeInfo, newFormula As String, newMixName As String, newTargetRecycledPercentage As Double, newTargetAsphaltPercentage As Double)

            RaiseEvent updateRecipe(_recipe, newFormula, newMixName, newTargetRecycledPercentage, newTargetAsphaltPercentage)

        End Sub

    End Class
End Namespace
