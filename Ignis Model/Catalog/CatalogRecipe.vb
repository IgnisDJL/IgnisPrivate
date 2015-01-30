Public Class CatalogRecipe

    Private _catalogRecipe As Dictionary(Of String, String)

    Public Sub New()
        _catalogRecipe = New Dictionary(Of String, String)
    End Sub

    Public Sub addNewRecipeToCatalog(recipeId As String, recipeDescription As String)
        _catalogRecipe.Add(recipeId, recipeDescription)
    End Sub

    Public Sub removeRecipeFromCatalog(recipeId As String)
        _catalogRecipe.Remove(recipeId)
    End Sub

    Public Function getDescriptionFromRecipe(recipeId As String) As String
        Return _catalogRecipe.Item(recipeId)
    End Function

    Public Sub updateDescriptionFromRecipe(recipeId As String, newDescription As String)
        _catalogRecipe.Item(recipeId) = newDescription
    End Sub
End Class
