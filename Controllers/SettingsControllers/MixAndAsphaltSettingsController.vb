Imports IGNIS.Commands.Settings

Public Class MixAndAsphaltSettingsController
    Inherits SettingsController

    Public Sub New()
        MyBase.New()

    End Sub

    Public Sub addRecipe(formula As String, mixName As String, rapTargetPercentage As Double, acTargetPercentage As Double)
        Me.executeCommand(New AddRecipeInfo(formula, mixName, rapTargetPercentage, acTargetPercentage))
    End Sub

    Public Sub removeRecipe(recipe As XmlSettings.RecipesNode.RecipeInfo)
        Me.executeCommand(New RemoveRecipeInfo(recipe))
    End Sub

    Public Sub updateRecipe(recipe As XmlSettings.RecipesNode.RecipeInfo, newFormula As String, newMixName As String, newRAPTargetPercentage As Double, newACTargetPercentage As Double)
        Me.executeCommand(New UpdateRecipeInfo(recipe, newFormula, newMixName, newRAPTargetPercentage, newACTargetPercentage))
    End Sub

    Public ReadOnly Property RecipeInfoList As List(Of XmlSettings.RecipesNode.RecipeInfo)
        Get
            Return XmlSettings.Settings.instance.Usine.RecipesInfo.RECIPES
        End Get
    End Property

    Public ReadOnly Property UnknownRecipesList As List(Of XmlSettings.RecipesNode.UnknownRecipeNode)
        Get
            Return XmlSettings.Settings.instance.Usine.RecipesInfo.UNKNOWN_RECIPES
        End Get
    End Property


    Public Sub addTankInfo(tankName As String, asphaltName As String, mixTargetTemperature As Double)
        Me.executeCommand(New AddAsphaltTankInfo(tankName, asphaltName, mixTargetTemperature))
    End Sub

    Public Sub removeTankInfo(tankInfo As XmlSettings.AsphaltNode.TankInfo)
        Me.executeCommand(New RemoveTankInfo(tankInfo))
    End Sub

    Public Sub updateTankInfo(tankInfo As XmlSettings.AsphaltNode.TankInfo, newTankName As String, newAsphaltName As String, newMixTargetTemperature As Double)
        Me.executeCommand(New UpdateTankInfo(tankInfo, newTankName, newAsphaltName, newMixTargetTemperature))
    End Sub

    Public ReadOnly Property AsphaltTanks As List(Of XmlSettings.AsphaltNode.TankInfo)
        Get
            Return XmlSettings.Settings.instance.Usine.AsphaltInfo.TANKS
        End Get
    End Property

    Public ReadOnly Property UnknownAsphaltTanks As List(Of XmlSettings.AsphaltNode.UnknownTankNode)
        Get
            Return XmlSettings.Settings.instance.Usine.AsphaltInfo.UNKNOWN_TANKS
        End Get
    End Property

End Class
