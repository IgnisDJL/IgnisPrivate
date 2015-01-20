
Imports IGNIS.UI.Common

Namespace UI

    Public Class MixAndAsphaltSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Enrobés et bitumes"


        ' Components
        Private WithEvents newRecipeFormulaField As TextField
        Private WithEvents newRecipeMixField As TextField
        Private WithEvents newRecipeRAPField As TextField
        Private WithEvents newRecipeACPercentageField As TextField
        Private WithEvents addNewRecipeButton As Button

        Private recipesListView As RecipesListView
        Private unknownRecipesListView As UnknownRecipesListView


        Private WithEvents newAsphaltTankField As TextField
        Private WithEvents newAsphaltNameField As TextField
        Private WithEvents newMixTargetTemperature As TextField
        Private WithEvents addNewTankInfoButton As Button

        Private tankInfoListView As AsphaltTankListView
        Private unknownTankInfoListView As UnknownAsphaltTankListView

        ' Attributes
        Private _mixAndACSettings As MixAndAsphaltSettingsController


        Public Sub New()
            MyBase.New()

            Me.layout = New MixAndAsphaltSettingsViewLayout

            Me._mixAndACSettings = ProgramController.SettingsControllers.MixAndAsphaltSettingsController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.newRecipeFormulaField = New TextField
            Me.newRecipeFormulaField.PlaceHolder = "Formule"
            Me.newRecipeFormulaField.ValidationType = TextField.ValidationTypes.Text
            Me.newRecipeFormulaField.CanBeEmpty = False

            Me.newRecipeMixField = New TextField
            Me.newRecipeMixField.PlaceHolder = "Enrobé"
            Me.newRecipeMixField.ValidationType = TextField.ValidationTypes.Text

            Me.newRecipeRAPField = New TextField
            Me.newRecipeRAPField.PlaceHolder = "RAP"
            Me.newRecipeRAPField.ValidationType = TextField.ValidationTypes.Decimals
            Me.newRecipeRAPField.CanBeEmpty = False

            Me.newRecipeACPercentageField = New TextField
            Me.newRecipeACPercentageField.PlaceHolder = "Bitume"
            Me.newRecipeACPercentageField.ValidationType = TextField.ValidationTypes.Decimals
            Me.newRecipeACPercentageField.CanBeEmpty = False

            Me.addNewRecipeButton = New Button
            Me.addNewRecipeButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewRecipeButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewRecipeButton.Enabled = False

            Me.recipesListView = New RecipesListView
            AddHandler Me.recipesListView.deleteRecipe, AddressOf Me._mixAndACSettings.removeRecipe
            AddHandler Me.recipesListView.deleteRecipe, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.recipesListView.updateRecipe, AddressOf Me._mixAndACSettings.updateRecipe
            AddHandler Me.recipesListView.updateRecipe, AddressOf Me.raiseSettingChangedEvent

            Me.unknownRecipesListView = New UnknownRecipesListView
            AddHandler Me.unknownRecipesListView.ItemSelectedEvent, AddressOf Me.onUnknownRecipeSelected

            Me.Controls.Add(Me.newRecipeFormulaField)
            Me.Controls.Add(Me.newRecipeMixField)
            Me.Controls.Add(Me.newRecipeRAPField)
            Me.Controls.Add(Me.newRecipeACPercentageField)
            Me.Controls.Add(Me.addNewRecipeButton)
            Me.Controls.Add(Me.recipesListView)
            Me.Controls.Add(Me.unknownRecipesListView)


            Me.newAsphaltTankField = New TextField
            Me.newAsphaltTankField.PlaceHolder = "Benne"
            Me.newAsphaltTankField.ValidationType = TextField.ValidationTypes.Text
            Me.newAsphaltTankField.CanBeEmpty = False

            Me.newAsphaltNameField = New TextField
            Me.newAsphaltNameField.PlaceHolder = "Bitume"
            Me.newAsphaltNameField.ValidationType = TextField.ValidationTypes.Text
            Me.newAsphaltNameField.CanBeEmpty = False

            Me.newMixTargetTemperature = New TextField
            Me.newMixTargetTemperature.PlaceHolder = "Température"
            Me.newMixTargetTemperature.ValidationType = TextField.ValidationTypes.Numbers
            Me.newMixTargetTemperature.CanBeEmpty = False

            Me.addNewTankInfoButton = New Button
            Me.addNewTankInfoButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewTankInfoButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewTankInfoButton.Enabled = False

            Me.tankInfoListView = New AsphaltTankListView
            AddHandler Me.tankInfoListView.deleteTankInfo, AddressOf Me._mixAndACSettings.removeTankInfo
            AddHandler Me.tankInfoListView.deleteTankInfo, AddressOf Me.raiseSettingChangedEvent
            AddHandler Me.tankInfoListView.updateTankInfo, AddressOf Me._mixAndACSettings.updateTankInfo
            AddHandler Me.tankInfoListView.updateTankInfo, AddressOf Me.raiseSettingChangedEvent

            Me.unknownTankInfoListView = New UnknownAsphaltTankListView
            AddHandler Me.unknownTankInfoListView.ItemSelectedEvent, AddressOf Me.onUnknownTankInfoSelected

            Me.Controls.Add(Me.newAsphaltTankField)
            Me.Controls.Add(Me.newAsphaltNameField)
            Me.Controls.Add(Me.newMixTargetTemperature)
            Me.Controls.Add(Me.addNewTankInfoButton)
            Me.Controls.Add(Me.tankInfoListView)
            Me.Controls.Add(Me.unknownTankInfoListView)


            Me.newRecipeFormulaField.TabIndex = 0
            Me.newRecipeMixField.TabIndex = 1
            Me.newRecipeRAPField.TabIndex = 2
            Me.newRecipeACPercentageField.TabIndex = 3
            Me.addNewRecipeButton.TabIndex = 4
            Me.newAsphaltTankField.TabIndex = 5
            Me.newAsphaltNameField.TabIndex = 6
            Me.addNewTankInfoButton.TabIndex = 7

            AddHandler Me.newRecipeFormulaField.ValidationOccured, AddressOf Me.enableAddNewRecipeButton
            AddHandler Me.newRecipeMixField.ValidationOccured, AddressOf Me.enableAddNewRecipeButton
            AddHandler Me.newRecipeRAPField.ValidationOccured, AddressOf Me.enableAddNewRecipeButton
            AddHandler Me.newRecipeACPercentageField.ValidationOccured, AddressOf Me.enableAddNewRecipeButton


            AddHandler Me.newAsphaltTankField.ValidationOccured, AddressOf Me.enableNewTankInfoButton
            AddHandler Me.newAsphaltNameField.ValidationOccured, AddressOf Me.enableNewTankInfoButton
            AddHandler Me.newMixTargetTemperature.ValidationOccured, AddressOf Me.enableNewTankInfoButton
        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, MixAndAsphaltSettingsViewLayout)

            Me.newRecipeFormulaField.Location = layout.NewRecipeFormulaField_Location
            Me.newRecipeFormulaField.Size = layout.NewRecipeFormulaField_Size

            Me.newRecipeMixField.Location = layout.NewRecipeMixField_Location
            Me.newRecipeMixField.Size = layout.NewRecipeMixField_Size

            Me.newRecipeRAPField.Location = layout.NewRecipeRAPField_Location
            Me.newRecipeRAPField.Size = layout.NewRecipeRAPField_Size

            Me.newRecipeACPercentageField.Location = layout.NewRecipeACPercentageField_Location
            Me.newRecipeACPercentageField.Size = layout.NewRecipeACPercentageField_Size

            Me.addNewRecipeButton.Location = layout.AddNewRecipeButton_Location
            Me.addNewRecipeButton.Size = layout.AddNewRecipeButton_Size

            Me.recipesListView.Location = layout.RecipesListView_Location
            Me.recipesListView.ajustLayout(layout.RecipesListView_Size)

            Me.unknownRecipesListView.Location = layout.UnknownRecipesListView_Location
            Me.unknownRecipesListView.ajustLayout(layout.UnknownRecipesListView_Size)

            Me.newAsphaltTankField.Location = layout.NewAsphaltTankNameField_Location
            Me.newAsphaltTankField.Size = layout.NewAsphaltTankNameField_Size

            Me.newAsphaltNameField.Location = layout.NewAsphaltNameField_Location
            Me.newAsphaltNameField.Size = layout.NewAsphaltNameField_Size

            Me.newMixTargetTemperature.Location = layout.NewMixTargetTemperatureField_Location
            Me.newMixTargetTemperature.Size = layout.NewMixTargetTemperatureField_Size

            Me.addNewTankInfoButton.Location = layout.AddNewTankInfoButton_Location
            Me.addNewTankInfoButton.Size = layout.AddNewTankInfoButton_Size

            Me.tankInfoListView.Location = layout.TankInfoListView_Location
            Me.tankInfoListView.ajustLayout(layout.TankInfoListView_Size)

            Me.unknownTankInfoListView.Location = layout.UnknownTankInfoListView_Location
            Me.unknownTankInfoListView.ajustLayout(layout.UnknownTankInfoListView_Size)

        End Sub


        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim layout = DirectCast(Me.layout, MixAndAsphaltSettingsViewLayout)

            Me.recipesListView.ajustLayoutFinal(layout.RecipesListView_Size)

            Me.unknownRecipesListView.ajustLayoutFinal(layout.UnknownRecipesListView_Size)

            Me.tankInfoListView.ajustLayoutFinal(layout.TankInfoListView_Size)

            Me.unknownTankInfoListView.ajustLayoutFinal(layout.UnknownTankInfoListView_Size)

        End Sub

        Public Overrides Sub updateFields()

            Me.recipesListView.clear()

            For Each _recipeInfo As XmlSettings.RecipesNode.RecipeInfo In Me._mixAndACSettings.RecipeInfoList

                If (Not _recipeInfo.FORMULA.Equals("")) Then
                    Me.recipesListView.addObject(_recipeInfo)
                End If
            Next
            Me.recipesListView.refreshList()

            Me.unknownRecipesListView.clear()

            For Each _unknownRecipeInfo As XmlSettings.RecipesNode.UnknownRecipeNode In Me._mixAndACSettings.UnknownRecipesList

                Me.unknownRecipesListView.addObject(_unknownRecipeInfo)
            Next
            Me.unknownRecipesListView.refreshList()

            Me.tankInfoListView.clear()

            For Each _tankInfo As XmlSettings.AsphaltNode.TankInfo In Me._mixAndACSettings.AsphaltTanks

                Me.tankInfoListView.addObject(_tankInfo)
            Next
            Me.tankInfoListView.refreshList()

            Me.unknownTankInfoListView.clear()

            For Each _unknownTankInfo As XmlSettings.AsphaltNode.UnknownTankNode In Me._mixAndACSettings.UnknownAsphaltTanks

                Me.unknownTankInfoListView.addObject(_unknownTankInfo)
            Next
            Me.unknownTankInfoListView.refreshList()

            Me.Focus()
        End Sub

        Protected Overloads Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub afterShow()

            Me.Focus()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Private Sub addRecipe() Handles addNewRecipeButton.Click

            Me._mixAndACSettings.addRecipe(Me.newRecipeFormulaField.Text, Me.newRecipeMixField.Text, CDbl(Me.newRecipeRAPField.Text), CDbl(Me.newRecipeACPercentageField.Text))

            Me.raiseSettingChangedEvent()

            Me.newRecipeFormulaField.DefaultText = ""
            Me.newRecipeMixField.DefaultText = ""
            Me.newRecipeRAPField.DefaultText = ""
            Me.newRecipeACPercentageField.DefaultText = ""

            Me.recipesListView.selectLastItem()

        End Sub

        Private Sub enableAddNewRecipeButton()

            If (Me.newRecipeFormulaField.IsValid AndAlso _
                Me.newRecipeMixField.IsValid AndAlso _
                Me.newRecipeRAPField.IsValid AndAlso _
                Me.newRecipeACPercentageField.IsValid) Then

                Me.addNewRecipeButton.Enabled = True
            Else
                Me.addNewRecipeButton.Enabled = False
            End If
        End Sub

        Private Sub onUnknownRecipeSelected(unknownRecipe As XmlSettings.RecipesNode.UnknownRecipeNode)
            Me.newRecipeFormulaField.Focus()
            Me.newRecipeFormulaField.Text = unknownRecipe.FORMULA.Trim()
        End Sub

        Private Sub addTankInfo() Handles addNewTankInfoButton.Click

            Me._mixAndACSettings.addTankInfo(Me.newAsphaltTankField.Text, Me.newAsphaltNameField.Text, CDbl(Me.newMixTargetTemperature.Text))

            Me.raiseSettingChangedEvent()

            Me.newAsphaltTankField.DefaultText = ""
            Me.newAsphaltNameField.DefaultText = ""
            Me.newMixTargetTemperature.DefaultText = ""

            Me.tankInfoListView.selectLastItem()
        End Sub

        Private Sub enableNewTankInfoButton()

            If (Me.newAsphaltTankField.IsValid AndAlso _
                Me.newAsphaltNameField.IsValid AndAlso _
                Me.newMixTargetTemperature.IsValid) Then

                Me.addNewTankInfoButton.Enabled = True
            Else
                Me.addNewTankInfoButton.Enabled = False
            End If
        End Sub

        Private Sub onUnknownTankInfoSelected(unnknownTankInfo As XmlSettings.AsphaltNode.UnknownTankNode)
            Me.newAsphaltTankField.Focus()
            Me.newAsphaltTankField.Text = unnknownTankInfo.TANK_NAME
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._mixAndACSettings
            End Get
        End Property
    End Class
End Namespace

