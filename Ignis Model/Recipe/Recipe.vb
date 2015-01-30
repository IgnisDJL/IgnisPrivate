Public Class Recipe
    Private recipeNumber As Integer
    Private recipeName As String
    Private recipePartList As List(Of RecipePart)
    Private mixTargetTemperature As Double

    Sub New(recipeNumber As Integer, recipeName As String, mixTargetTemperature As Double, percentageList As List(Of Double), materialList As List(Of Material))
        Me.recipeNumber = recipeNumber
        Me.recipeName = recipeName
        Me.mixTargetTemperature = mixTargetTemperature
        createRecipe(percentageList, materialList)
    End Sub

    Public Function createRecipe(percentageList As List(Of Double), materialList As List(Of Material)) As Boolean

        Dim totalPercentage As Double

        For Each percentage As Double In percentageList
            totalPercentage += percentage
        Next

        If (Not totalPercentage = 1) Then
            Return False
        Else
            For index As Integer = 0 To materialList.Count - 1
                recipePartList.Add(New RecipePart(percentageList.ElementAt(index), materialList.ElementAt(index)))
            Next

            Return True
        End If

    End Function

    Public ReadOnly Property getRecipePartList As List(Of RecipePart)
        Get
            Return recipePartList
        End Get
    End Property

End Class
