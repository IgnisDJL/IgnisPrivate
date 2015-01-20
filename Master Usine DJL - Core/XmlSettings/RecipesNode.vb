Namespace XmlSettings

    Public Class RecipesNode
        Inherits ComplexSettingsNode

        Public Const NODE_NAME As String = "recipesInfo"
        Public Const XPATH_TO_NODE = "/settings/usine/" & NODE_NAME

        Private recipes_ As New List(Of RecipeInfo)
        Public ReadOnly Property RECIPES As List(Of RecipeInfo)
            Get
                Return Me.recipes_
            End Get
        End Property

        Private unknownRecipes As New List(Of UnknownRecipeNode)
        Public ReadOnly Property UNKNOWN_RECIPES As List(Of UnknownRecipeNode)
            Get
                Return Me.unknownRecipes
            End Get
        End Property

        Public Sub New(parentNode As Xml.XmlNode, recipesNode As Xml.XmlNode)
            MyBase.New(parentNode, recipesNode)

            For Each recipeNode As Xml.XmlNode In Me.NODE.SelectNodes(RecipeInfo.XPATH_TO_NODE)

                Dim recipe As New RecipeInfo(recipeNode)

                If (Not IsNothing(recipeNode.Attributes.GetNamedItem(RecipeInfo.FORMULA_ATTRIBUTE))) Then

                    recipe.regex = False
                    Me.RECIPES.Add(recipe)

                ElseIf (Not IsNothing(recipeNode.Attributes.GetNamedItem(RecipeInfo.REGEX_ATTRIBUTE))) Then

                    recipe.regex = True
                    Me.RECIPES.Add(recipe)

                End If

            Next

            For Each unknownRecipe As Xml.XmlNode In Me.NODE.SelectNodes(UnknownRecipeNode.XPATH_TO_NODE)

                Me.UNKNOWN_RECIPES.Add(New UnknownRecipeNode(unknownRecipe))

            Next

        End Sub

        Public Sub removeRecipeInfo(recipeInfo As RecipeInfo)

            Me.RECIPES.Remove(recipeInfo)
            Me.NODE.RemoveChild(recipeInfo.NODE)

        End Sub

        Public Function addRecipeInfo(formula As String, regex As Boolean, mixName As String, RAP As String, setPointAsphalt As String) As RecipeInfo

            Dim newNode = Me.NODE.OwnerDocument.CreateElement(RecipeInfo.NODE_NAME)

            Dim nameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(If(regex, RecipeInfo.REGEX_ATTRIBUTE, RecipeInfo.FORMULA_ATTRIBUTE))

            newNode.Attributes.Append(nameAttribute)

            Dim mixNameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(RecipeInfo.MIX_NAME_ATTRIBUTE)

            newNode.Attributes.Append(mixNameAttribute)

            Dim RAPAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(RecipeInfo.RAP_ATTRIBUTE)

            newNode.Attributes.Append(RAPAttribute)

            Dim ACAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(RecipeInfo.ASPHALT_PERCENTAGE_ATTRIBUTE)

            newNode.Attributes.Append(ACAttribute)

            Dim recipeInformation As New RecipeInfo(newNode)

            If (regex) Then
                recipeInformation.FORMULA_REGEX = New System.Text.RegularExpressions.Regex(formula)
            Else
                recipeInformation.FORMULA = formula
            End If

            Me.NODE.AppendChild(newNode)

            If (regex) Then
                recipeInformation.FORMULA_REGEX = New System.Text.RegularExpressions.Regex(formula)
            Else
                recipeInformation.FORMULA = formula
            End If
            recipeInformation.regex = regex

            recipeInformation.MIX_NAME = mixName
            recipeInformation.RECYCLED_SET_POINT_PERCENTAGE = If(RAP = "", Double.NaN, RAP)
            recipeInformation.ASPHALT_SET_POINT_PERCENTAGE = If(setPointAsphalt = "", Double.NaN, setPointAsphalt)

            Return Me.addRecipeInfo(recipeInformation)
        End Function

        Public Function addRecipeInfo(newRecipeInfo As RecipeInfo) As RecipeInfo

            Me.NODE.AppendChild(newRecipeInfo.NODE)

            For Each unknownRecipe In Me.UNKNOWN_RECIPES

                If (unknownRecipe.FORMULA.Equals(newRecipeInfo.FORMULA)) Then

                    Me.removeUnknownRecipe(unknownRecipe)
                    Exit For
                End If
            Next

            Me.RECIPES.Add(newRecipeInfo)

            Return newRecipeInfo
        End Function

        Public Function addUnknownRecipe(formulaName As String, mixName As String) As UnknownRecipeNode

            Dim addToList As Boolean = True

            For Each unknownRecipe In Me.UNKNOWN_RECIPES

                If (unknownRecipe.FORMULA.Equals(formulaName) AndAlso unknownRecipe.MIX_NAME.Equals(mixName)) Then

                    addToList = False

                    Return unknownRecipe

                End If

            Next

            If (addToList) Then

                Dim newNode = Me.NODE.OwnerDocument.CreateElement(UnknownRecipeNode.NODE_NAME)

                Dim nameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(UnknownRecipeNode.FORMULA_ATTRIBUTE)

                newNode.Attributes.Append(nameAttribute)

                Dim mixNameAttribute As Xml.XmlAttribute = Me.NODE.OwnerDocument.CreateAttribute(UnknownRecipeNode.MIX_NAME_ATTRIBUTE)

                newNode.Attributes.Append(mixNameAttribute)

                Me.NODE.AppendChild(newNode)

                Dim recipeInformation As New UnknownRecipeNode(newNode)

                Me.UNKNOWN_RECIPES.Add(recipeInformation)

                recipeInformation.FORMULA = formulaName
                recipeInformation.MIX_NAME = mixName

                Return recipeInformation

            End If


            Debugger.Break()
            Return Nothing

        End Function

        Public Sub removeUnknownRecipe(unknownRecipe As UnknownRecipeNode)

            Me.UNKNOWN_RECIPES.Remove(unknownRecipe)
            Me.NODE.RemoveChild(unknownRecipe.NODE)

        End Sub

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
        End Sub

        Public Class RecipeInfo

            Public Const NODE_NAME As String = "recipe"
            Public Const XPATH_TO_NODE As String = XmlSettings.RecipesNode.XPATH_TO_NODE & "/" & NODE_NAME

            Public Const FORMULA_ATTRIBUTE As String = "formula"
            Public Const REGEX_ATTRIBUTE As String = "regex"
            Public Const MIX_NAME_ATTRIBUTE As String = "mix"
            Public Const RAP_ATTRIBUTE As String = "RAP"
            Public Const ASPHALT_PERCENTAGE_ATTRIBUTE As String = "AC"


            Public regex As Boolean

            Private recipeNode As Xml.XmlNode
            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me.recipeNode
                End Get
            End Property

            Public Sub New(myNode As Xml.XmlNode)
                Me.recipeNode = myNode
            End Sub


            Public Property FORMULA As String
                Get
                    Dim attribute = Me.NODE.Attributes.GetNamedItem(FORMULA_ATTRIBUTE)
                    Return If(IsNothing(attribute), "", Me.NODE.Attributes.GetNamedItem(FORMULA_ATTRIBUTE).Value)
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(FORMULA_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property FORMULA_REGEX As System.Text.RegularExpressions.Regex
                Get
                    If (IsNothing(Me.NODE.Attributes.GetNamedItem(REGEX_ATTRIBUTE))) Then
                        Return New System.Text.RegularExpressions.Regex("")
                    Else
                        Return New System.Text.RegularExpressions.Regex(Me.NODE.Attributes.GetNamedItem(REGEX_ATTRIBUTE).Value)
                    End If
                End Get
                Set(value As System.Text.RegularExpressions.Regex)
                    Me.NODE.Attributes.GetNamedItem(REGEX_ATTRIBUTE).Value = value.ToString
                End Set
            End Property

            Public Property MIX_NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(MIX_NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(MIX_NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property RECYCLED_SET_POINT_PERCENTAGE As Double
                Get
                    If (Me.NODE.Attributes.GetNamedItem(RAP_ATTRIBUTE).Value = "") Then
                        Return Double.NaN
                    Else
                        Return Double.Parse(Me.NODE.Attributes.GetNamedItem(RAP_ATTRIBUTE).Value, Globalization.NumberStyles.Any, XmlSettings.Settings.LANGUAGE.Culture)
                    End If
                End Get
                Set(value As Double)
                    Me.NODE.Attributes.GetNamedItem(RAP_ATTRIBUTE).Value = value.ToString
                End Set
            End Property

            Public Property ASPHALT_SET_POINT_PERCENTAGE As Double
                Get
                    If (Me.NODE.Attributes.GetNamedItem(ASPHALT_PERCENTAGE_ATTRIBUTE).Value = "") Then
                        Return Double.NaN
                    Else
                        Return Double.Parse(Me.NODE.Attributes.GetNamedItem(ASPHALT_PERCENTAGE_ATTRIBUTE).Value, Globalization.NumberStyles.Any, XmlSettings.Settings.LANGUAGE.Culture)
                    End If
                End Get
                Set(value As Double)
                    Me.NODE.Attributes.GetNamedItem(ASPHALT_PERCENTAGE_ATTRIBUTE).Value = value.ToString
                End Set
            End Property


            Public Function match(formula As String) As Boolean

                If (Me.regex) Then

                    Return Me.FORMULA_REGEX.Match(formula).Success

                Else

                    Return Me.FORMULA.Equals(formula)

                End If

                Return False
            End Function

            Public Overrides Function ToString() As String
                Return Me.MIX_NAME.ToString
            End Function

            Public Overrides Function Equals(obj As Object) As Boolean

                If (TypeOf obj Is RecipeInfo) Then
                    Return Me.FORMULA.Equals(DirectCast(obj, RecipeInfo).FORMULA) AndAlso Me.FORMULA_REGEX.ToString.Equals(DirectCast(obj, RecipeInfo).FORMULA_REGEX.ToString)
                End If

                Return False
            End Function

        End Class

        Public Class UnknownRecipeNode

            Public Const NODE_NAME As String = "unknownRecipe"
            Public Const XPATH_TO_NODE As String = XmlSettings.RecipesNode.XPATH_TO_NODE & "/" & NODE_NAME

            Public Const FORMULA_ATTRIBUTE As String = "formula"
            Public Const MIX_NAME_ATTRIBUTE As String = "mix"


            Private _node As Xml.XmlNode


            Public Sub New(myNode As Xml.XmlNode)
                Me._node = myNode
            End Sub

            Public ReadOnly Property NODE As Xml.XmlNode
                Get
                    Return Me._node
                End Get
            End Property

            Public Property FORMULA As String
                Get
                    Dim attribute = Me.NODE.Attributes.GetNamedItem(FORMULA_ATTRIBUTE)
                    Return If(IsNothing(attribute), "", Me.NODE.Attributes.GetNamedItem(FORMULA_ATTRIBUTE).Value)
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(FORMULA_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Property MIX_NAME As String
                Get
                    Return Me.NODE.Attributes.GetNamedItem(MIX_NAME_ATTRIBUTE).Value
                End Get
                Set(value As String)
                    Me.NODE.Attributes.GetNamedItem(MIX_NAME_ATTRIBUTE).Value = value
                End Set
            End Property

            Public Overrides Function ToString() As String
                Return Me.FORMULA
            End Function

        End Class
    End Class

End Namespace