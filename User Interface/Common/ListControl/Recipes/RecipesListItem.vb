Imports IGNIS.UI.Common

Namespace UI

    Public Class RecipesListItem
        Inherits Common.ListItem(Of XmlSettings.RecipesNode.RecipeInfo)


        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25) ' #Refactor - extract

        ' Components
        Private WithEvents formulaLabel As Label
        Private WithEvents mixNameLabel As Label
        Private WithEvents rapLabel As Label
        Private WithEvents acPercentageLabel As Label

        Private WithEvents formulaField As TextField
        Private WithEvents mixNameField As TextField
        Private WithEvents rapField As TextField
        Private WithEvents acPercentageField As TextField

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        ' Attributes

        ' Events
        Public Event deleteRecipe(_recipe As XmlSettings.RecipesNode.RecipeInfo)
        Public Event updateRecipe(_recipe As XmlSettings.RecipesNode.RecipeInfo, newFormula As String, newMixName As String, newRecycledTargetPercentage As Double, newAsphaltTargetPercentage As Double)


        Public Sub New(_recipe As XmlSettings.RecipesNode.RecipeInfo)
            MyBase.New(_recipe)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.formulaLabel = New Label
            Me.formulaLabel.Text = Me.ItemObject.FORMULA
            Me.formulaLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.mixNameLabel = New Label
            Me.mixNameLabel.Text = Me.ItemObject.MIX_NAME
            Me.mixNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.rapLabel = New Label
            Me.rapLabel.Text = Me.ItemObject.RECYCLED_SET_POINT_PERCENTAGE & "%"
            Me.rapLabel.TextAlign = ContentAlignment.MiddleCenter

            Me.acPercentageLabel = New Label
            Me.acPercentageLabel.Text = Me.ItemObject.ASPHALT_SET_POINT_PERCENTAGE & "%"
            Me.acPercentageLabel.TextAlign = ContentAlignment.MiddleCenter

            Me.formulaField = New TextField
            Me.formulaField.AutoSize = False
            Me.formulaField.ValidationType = TextField.ValidationTypes.Text
            Me.formulaField.CanBeEmpty = False

            Me.mixNameField = New TextField
            Me.mixNameField.AutoSize = False
            Me.mixNameField.ValidationType = TextField.ValidationTypes.Text

            Me.rapField = New TextField
            Me.rapField.AutoSize = False
            Me.rapField.TextAlign = HorizontalAlignment.Center
            Me.rapField.ValidationType = TextField.ValidationTypes.Decimals
            Me.rapField.AcceptsPercentSign = True
            Me.rapField.CanBeEmpty = False

            Me.acPercentageField = New TextField
            Me.acPercentageField.AutoSize = False
            Me.acPercentageField.TextAlign = HorizontalAlignment.Center
            Me.acPercentageField.ValidationType = TextField.ValidationTypes.Decimals
            Me.acPercentageField.AcceptsPercentSign = True
            Me.acPercentageField.CanBeEmpty = False

            Me.deleteButton = New Button
            Me.deleteButton.Image = Constants.UI.Images._16x16.DELETE
            Me.deleteButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.deleteButton.Size = BUTTONS_SIZE
            Me.deleteButton.BackColor = Me.BackColor

            Me.editButton = New Button
            Me.editButton.Image = Constants.UI.Images._16x16.EDIT
            Me.editButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.editButton.Size = BUTTONS_SIZE
            Me.editButton.BackColor = Me.BackColor

            Me.cancelEditButton = New Button
            Me.cancelEditButton.Image = Constants.UI.Images._16x16.WRONG
            Me.cancelEditButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.cancelEditButton.Size = BUTTONS_SIZE
            Me.cancelEditButton.BackColor = Me.BackColor

            Me.confirmEditButton = New Button
            Me.confirmEditButton.Image = Constants.UI.Images._16x16.GOOD
            Me.confirmEditButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.confirmEditButton.Size = BUTTONS_SIZE
            Me.confirmEditButton.BackColor = Me.BackColor
            Me.confirmEditButton.Enabled = False

            Me.Controls.Add(Me.formulaLabel)
            Me.Controls.Add(Me.mixNameLabel)
            Me.Controls.Add(Me.rapLabel)
            Me.Controls.Add(Me.acPercentageLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            AddHandler Me.formulaField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.mixNameField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.rapField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.acPercentageField.ValidationOccured, AddressOf Me.enableConfirmEditButton

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.formulaLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            Me.formulaLabel.Size = New Size((newSize.Width - 2 * BUTTONS_SIZE.Width - 7 * SPACE_BETWEEN_CONTROLS_X) * 2 / 6, newSize.Height)

            Me.formulaField.Location = Me.formulaLabel.Location
            Me.formulaField.Size = Me.formulaLabel.Size

            Me.mixNameLabel.Location = New Point(Me.formulaLabel.Location.X + Me.formulaLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.mixNameLabel.Size = Me.formulaLabel.Size

            Me.mixNameField.Location = Me.mixNameLabel.Location
            Me.mixNameField.Size = Me.mixNameLabel.Size

            Me.rapLabel.Location = New Point(Me.mixNameLabel.Location.X + Me.mixNameLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.rapLabel.Size = New Size((newSize.Width - 2 * BUTTONS_SIZE.Width - 7 * SPACE_BETWEEN_CONTROLS_X) * 1 / 6, newSize.Height)

            Me.rapField.Location = Me.rapLabel.Location
            Me.rapField.Size = Me.rapLabel.Size

            Me.acPercentageLabel.Location = New Point(Me.rapLabel.Location.X + Me.rapLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.acPercentageLabel.Size = Me.rapLabel.Size

            Me.acPercentageField.Location = Me.acPercentageLabel.Location
            Me.acPercentageField.Size = Me.acPercentageLabel.Size

            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, formulaLabel.Click, mixNameLabel.Click, rapLabel.Click, acPercentageLabel.Click

            raiseClickEvent()

        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent deleteRecipe(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent updateRecipe(Me.ItemObject, Me.formulaField.Text, Me.mixNameField.Text, CDbl(Me.rapField.Text.Trim("%")), CDbl(Me.acPercentageField.Text.Trim("%")))

        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.formulaField.IsValid AndAlso _
                Me.mixNameField.IsValid AndAlso _
                Me.rapField.IsValid AndAlso _
                Me.acPercentageField.IsValid) Then

                Me.confirmEditButton.Enabled = True
            Else
                Me.confirmEditButton.Enabled = False
            End If
        End Sub

        Private Sub cancelEditing() Handles cancelEditButton.Click

            enterReadMode()

        End Sub

        Private Sub enterWriteMode() Handles editButton.Click

            Me._currentMode = Mode.WRITE

            Me.Controls.Remove(Me.formulaLabel)
            Me.Controls.Remove(Me.mixNameLabel)
            Me.Controls.Remove(Me.rapLabel)
            Me.Controls.Remove(Me.acPercentageLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.formulaField)
            Me.Controls.Add(Me.mixNameField)
            Me.Controls.Add(Me.rapField)
            Me.Controls.Add(Me.acPercentageField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.formulaField.DefaultText = Me.formulaLabel.Text
            Me.mixNameField.DefaultText = Me.mixNameLabel.Text
            Me.rapField.DefaultText = Me.rapLabel.Text
            Me.acPercentageField.DefaultText = Me.acPercentageLabel.Text

        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.formulaField)
            Me.Controls.Remove(Me.mixNameField)
            Me.Controls.Remove(Me.rapField)
            Me.Controls.Remove(Me.acPercentageField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.formulaLabel)
            Me.Controls.Add(Me.mixNameLabel)
            Me.Controls.Add(Me.rapLabel)
            Me.Controls.Add(Me.acPercentageLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles formulaField.KeyDown, mixNameField.KeyDown, rapField.KeyDown, acPercentageField.KeyDown

            If (e.KeyCode = Keys.Enter) Then

                If (Me.confirmEditButton.Enabled) Then
                    raiseUpdateEvent()
                Else
                    Beep()
                End If
                raiseUpdateEvent()

            ElseIf (e.KeyCode = Keys.Escape) Then

                cancelEditing()
            End If
        End Sub

    End Class
End Namespace

