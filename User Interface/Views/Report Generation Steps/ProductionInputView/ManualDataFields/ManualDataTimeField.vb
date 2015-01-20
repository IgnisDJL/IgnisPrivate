Namespace UI

    Public Class ManualDataTimeField
        Inherits ManualDataField

        Private Shared ReadOnly TIME_PICKER_WIDTH As Integer = 100
        Private Shared ReadOnly UNIT_LABEL_WIDTH As Integer = 90

        Private WithEvents timePicker As DateTimePicker

        Public Sub New(dataName As String)
            MyBase.New(dataName, "hh:mm", False)

            Me.initializeComponents()
        End Sub

        Private Sub initializeComponents()

            Me.timePicker = New DateTimePicker
            Me.timePicker.ShowUpDown = True
            Me.timePicker.Format = DateTimePickerFormat.Custom
            Me.timePicker.CustomFormat = "HH : mm"

            AddHandler timePicker.ValueChanged, AddressOf raiseValueChangedEvent

            Me.Controls.Add(timePicker)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.dataNameLabel.Location = New Point(0, 0)
            Me.dataNameLabel.Size = New Size(newSize.Width - TIME_PICKER_WIDTH - UNIT_LABEL_WIDTH, newSize.Height)

            Me.timePicker.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH - TIME_PICKER_WIDTH, 0)
            Me.timePicker.Size = New Size(TIME_PICKER_WIDTH, newSize.Height)

            Me.unitLabel.Location = New Point(newSize.Width - UNIT_LABEL_WIDTH, 0)
            Me.unitLabel.Size = New Size(UNIT_LABEL_WIDTH, newSize.Height)
        End Sub

        Public Property Value As Date
            Get
                Return Me.timePicker.Value
            End Get
            Set(value As Date)
                Me.timePicker.Value = value
            End Set
        End Property

        Public Overrides WriteOnly Property TabIndex As Integer
            Set(value As Integer)
                Me.timePicker.TabIndex = value
            End Set
        End Property

        Protected Overrides Sub listenToEnterKey(sender As Object, e As KeyEventArgs) Handles timePicker.KeyDown

            If (e.KeyCode = Keys.Enter) Then
                raiseEnterKeyPressed()
            End If

        End Sub

        Public Shadows Sub focus()
            Me.timePicker.Focus()
        End Sub

    End Class
End Namespace
