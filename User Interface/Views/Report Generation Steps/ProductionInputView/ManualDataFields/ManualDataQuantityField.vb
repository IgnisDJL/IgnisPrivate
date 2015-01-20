Namespace UI

    Public Class ManualDataQuantityField
        Inherits ManualDataField

        Public Shared ReadOnly QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON As Integer = 100
        Public Shared ReadOnly QUANTITY_FIELD_WIDTH_WITHOUT_UNKNOWN_BUTTON As Integer = 130
        Public Shared ReadOnly UNIT_LABEL_WIDTH As Integer = 60

        Private Shared ReadOnly UNKNOWN_TEXT As String = "?"

        Private WithEvents quantityField As Common.TextField
        Private WithEvents unknownButton As Button

        Private canBeUnknown As Boolean

        Public Sub New(dataName As String, unit As String, Optional canBeUnknown As Boolean = False)
            MyBase.New(dataName, unit, True)

            Me.canBeUnknown = canBeUnknown

            Me.initializeComponents()
        End Sub

        Private Sub initializeComponents()

            Me.quantityField = New Common.TextField
            Me.quantityField.ValidationType = Common.TextField.ValidationTypes.Decimals
            Me.quantityField.CanBeUnknown = Me.canBeUnknown
            AddHandler quantityField.TextChanged, AddressOf raiseValueChangedEvent

            If (Me.canBeUnknown) Then
                Me.unknownButton = New Button
                Me.unknownButton.Font = Constants.UI.Fonts.DEFAULT_FONT_BOLD
                Me.unknownButton.ImageAlign = ContentAlignment.MiddleCenter
                Me.unknownButton.Image = Constants.UI.Images._24x24.UNKNOWN
                Me.unknownButton.TabStop = False
                Me.Controls.Add(Me.unknownButton)

                Dim unknowButtonTooltip As New ToolTip
                unknowButtonTooltip.ShowAlways = True
                unknowButtonTooltip.Active = True
                unknowButtonTooltip.InitialDelay = 500
                unknowButtonTooltip.AutoPopDelay = 10000
                unknowButtonTooltip.BackColor = Color.White

                unknowButtonTooltip.SetToolTip(Me.unknownButton, "Valeur inconnue (?)")
            End If

            Me.validationIconPanel = New Panel
            Me.validationIconPanel.BackgroundImageLayout = ImageLayout.Center

            Me.Controls.Add(Me.quantityField)
            Me.Controls.Add(Me.validationIconPanel)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            If (Me.canBeUnknown) Then ' With the unknown value button

                Me.dataNameLabel.Location = New Point(0, 0)
                Me.dataNameLabel.Size = New Size(newSize.Width - UNIT_LABEL_WIDTH - QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - newSize.Height - newSize.Height, newSize.Height)

                Me.validationIconPanel.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH - QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - newSize.Height - newSize.Height, 0)
                Me.validationIconPanel.Size = New Size(newSize.Height, newSize.Height)

                Me.quantityField.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH - QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON - newSize.Height, 0)
                Me.quantityField.Size = New Size(QUANTITY_FIELD_WIDTH_WITH_UNKNOWN_BUTTON, newSize.Height)

                Me.unknownButton.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH - newSize.Height, 0)
                Me.unknownButton.Size = New Size(newSize.Height, newSize.Height)

            Else ' Without the unknown value button

                Me.dataNameLabel.Location = New Point(0, 0)
                Me.dataNameLabel.Size = New Size(newSize.Width - UNIT_LABEL_WIDTH - QUANTITY_FIELD_WIDTH_WITHOUT_UNKNOWN_BUTTON - newSize.Height, newSize.Height)

                Me.validationIconPanel.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH - QUANTITY_FIELD_WIDTH_WITHOUT_UNKNOWN_BUTTON - newSize.Height, 0)
                Me.validationIconPanel.Size = New Size(newSize.Height, newSize.Height)

                Me.quantityField.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH - QUANTITY_FIELD_WIDTH_WITHOUT_UNKNOWN_BUTTON, 0)
                Me.quantityField.Size = New Size(QUANTITY_FIELD_WIDTH_WITHOUT_UNKNOWN_BUTTON, newSize.Height)
            End If

            Me.unitLabel.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH, 0)
            Me.unitLabel.Size = New Size(UNIT_LABEL_WIDTH, newSize.Height)

        End Sub

        Public Property Value As Double
            Get

                If (Me.canBeUnknown AndAlso Me.quantityField.Text = UNKNOWN_TEXT) Then
                    Return ManualData.UNKNOWN_QUANTITY

                ElseIf Me.quantityField.Text = "" Then

                    Return ManualData.INVALID_QUANTITY

                Else

                    Dim returnValue As Double

                    If (Double.TryParse(Me.quantityField.Text, returnValue)) Then
                        Return returnValue
                    Else
                        Return ManualData.INVALID_QUANTITY
                    End If

                End If
            End Get
            Set(value As Double)

                If (Me.canBeUnknown AndAlso value.Equals(ManualData.UNKNOWN_QUANTITY)) Then

                    Me.quantityField.Text = UNKNOWN_TEXT

                    Me.IsValid = True

                ElseIf (value.Equals(ManualData.UNKNOWN_QUANTITY) OrElse value.Equals(ManualData.INVALID_QUANTITY)) Then

                    Me.quantityField.Text = ""

                    Me.IsValid = False

                Else

                    Me.quantityField.Text = value.ToString
                    Me.IsValid = True

                End If
            End Set
        End Property

        Private Sub setUnknownValue() Handles unknownButton.Click
            Me.Value = ManualData.UNKNOWN_QUANTITY
        End Sub

        Public Overrides WriteOnly Property TabIndex As Integer
            Set(value As Integer)
                Me.quantityField.TabIndex = value
            End Set
        End Property

        Protected Overrides Sub listenToEnterKey(sender As Object, e As KeyEventArgs) Handles quantityField.KeyDown

            If (e.KeyCode = Keys.Enter) Then
                raiseEnterKeyPressed()
            End If

        End Sub

        Public Shadows Sub focus()
            Me.quantityField.Focus()
        End Sub


    End Class
End Namespace
