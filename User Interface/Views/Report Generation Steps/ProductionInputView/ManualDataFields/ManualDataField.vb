Namespace UI

    Public MustInherit Class ManualDataField
        Inherits Panel

        Protected dataNameLabel As Label
        Protected validationIconPanel As Panel
        Protected unitLabel As Label

        Protected showValidationIcon As Boolean = True

        Public Event ValueChangedEvent(field As ManualDataField)
        Public Event EnterKeyPressed()

        Protected Sub New(dataName As String, unit As String, showValidationIcon As Boolean)

            Me.showValidationIcon = showValidationIcon

            Me.initializeComponents()

            Me.dataNameLabel.Text = dataName
            Me.unitLabel.Text = "( " & unit & " )"
        End Sub

        Private Sub initializeComponents()

            Me.dataNameLabel = New Label
            Me.dataNameLabel.AutoSize = False
            Me.dataNameLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.unitLabel = New Label
            Me.unitLabel.AutoSize = False
            Me.unitLabel.TextAlign = ContentAlignment.MiddleRight

            Me.Controls.Add(dataNameLabel)
            Me.Controls.Add(unitLabel)
        End Sub

        Public MustOverride Sub ajustLayout(newSize As Size)

        Public WriteOnly Property IsValid As Boolean
            Set(isValid As Boolean)

                If (showValidationIcon) Then

                    If (isValid) Then
                        Me.validationIconPanel.BackgroundImage = Constants.UI.Images._24x24.GOOD
                    Else
                        Me.validationIconPanel.BackgroundImage = Constants.UI.Images._24x24.DELETE
                    End If

                End If

            End Set
        End Property

        Public MustOverride Shadows WriteOnly Property TabIndex As Integer

        Protected Sub raiseValueChangedEvent()
            RaiseEvent ValueChangedEvent(Me)
        End Sub

        Protected Sub raiseEnterKeyPressed()
            RaiseEvent EnterKeyPressed()
        End Sub

        Protected MustOverride Sub listenToEnterKey(sender As Object, e As KeyEventArgs)

    End Class
End Namespace
