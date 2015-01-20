Namespace UI

    Public Class UnknownRecipesListView
        Inherits Common.ListControlTemplate(Of XmlSettings.RecipesNode.UnknownRecipeNode)

        Public Sub New()
            MyBase.New("Recettes inconnues")


        End Sub

        Public Overrides Sub addObject(obj As XmlSettings.RecipesNode.UnknownRecipeNode)

            Dim newItem As New UnknownRecipeListItem(obj)

            Me.addItem(newItem)

        End Sub
    End Class
End Namespace
