Namespace UI

    Public Class UnitsPanel
        Inherits Panel

        ' Constants


        ' Components
        Private titleLabel As Label

        Private massUnitLabel As Label
        Private WithEvents massUnitField As ComboBox

        Private temperatureUnitLabel As Label
        Private WithEvents temperatureUnitField As ComboBox

        Private percentageUnitLabel As Label
        Private WithEvents percentageUnitField As ComboBox

        Private productionRateUnitLabel As Label
        Private WithEvents productionRateUnitField As ComboBox


        ' Attributes
        Private shouldRaiseEvents As Boolean

        ' Events
        Public Event MassUnitChanged(unit As Unit)
        Public Event TemperatureUnitChanged(unit As Unit)
        Public Event PercentageUnitChanged(unit As Unit)
        Public Event ProductionRateUnitChanged(unit As Unit)


        Public Sub New()
            MyBase.New()

            Me.initializeComponents()
        End Sub

        Private Sub initializeComponents()

            Me.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.titleLabel = New Label
            Me.titleLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.massUnitLabel = New Label
            Me.massUnitLabel.Text = "Masse"

            Me.massUnitField = New ComboBox
            Me.massUnitField.Items.AddRange(Unit.MASS_UNITS)
            Me.massUnitField.DropDownStyle = ComboBoxStyle.DropDownList

            Me.temperatureUnitLabel = New Label
            Me.temperatureUnitLabel.Text = "Température"

            Me.temperatureUnitField = New ComboBox
            Me.temperatureUnitField.Items.AddRange(Unit.TEMPERATURE_UNITS)
            Me.temperatureUnitField.DropDownStyle = ComboBoxStyle.DropDownList

            Me.percentageUnitLabel = New Label
            Me.percentageUnitLabel.Text = "Taux (pourcentages)"

            Me.percentageUnitField = New ComboBox
            Me.percentageUnitField.Items.AddRange(Unit.PERCENT_UNITS)
            Me.percentageUnitField.DropDownStyle = ComboBoxStyle.DropDownList

            Me.productionRateUnitLabel = New Label
            Me.productionRateUnitLabel.Text = "Taux de production"

            Me.productionRateUnitField = New ComboBox
            Me.productionRateUnitField.Items.AddRange(Unit.PRODUCTION_SPEED_UNITS)
            Me.productionRateUnitField.DropDownStyle = ComboBoxStyle.DropDownList

            Me.Controls.Add(Me.titleLabel)
            Me.Controls.Add(Me.massUnitLabel)
            Me.Controls.Add(Me.massUnitField)
            Me.Controls.Add(Me.temperatureUnitLabel)
            Me.Controls.Add(Me.temperatureUnitField)
            Me.Controls.Add(Me.percentageUnitLabel)
            Me.Controls.Add(Me.percentageUnitField)
            Me.Controls.Add(Me.productionRateUnitLabel)
            Me.Controls.Add(Me.productionRateUnitField)

        End Sub

        Public Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.titleLabel.Location = New Point(LayoutManager.SPACE_BETWEEN_CONTROLS_X, 0)
            Me.titleLabel.Size = New Size(Me.Width, LayoutManager.FIELDS_HEIGHT)

            Dim componentsSize As New Size((Me.Width - 3 * LayoutManager.SPACE_BETWEEN_CONTROLS_X) / 2, LayoutManager.FIELDS_HEIGHT)

            Me.massUnitLabel.Location = New Point(LayoutManager.SPACE_BETWEEN_CONTROLS_X, Me.titleLabel.Location.Y + Me.titleLabel.Height + LayoutManager.SPACE_BETWEEN_CONTROLS_Y)
            Me.massUnitLabel.Size = componentsSize

            Me.massUnitField.Location = New Point(Me.massUnitLabel.Location.X, Me.massUnitLabel.Location.Y + Me.massUnitLabel.Height)
            Me.massUnitField.Size = componentsSize

            Me.temperatureUnitLabel.Location = New Point(Me.massUnitLabel.Location.X + Me.massUnitLabel.Width + LayoutManager.SPACE_BETWEEN_CONTROLS_X, Me.massUnitLabel.Location.Y)
            Me.temperatureUnitLabel.Size = componentsSize

            Me.temperatureUnitField.Location = New Point(Me.temperatureUnitLabel.Location.X, Me.temperatureUnitLabel.Location.Y + Me.temperatureUnitLabel.Height)
            Me.temperatureUnitField.Size = componentsSize

            Me.percentageUnitLabel.Location = New Point(LayoutManager.SPACE_BETWEEN_CONTROLS_X, Me.massUnitField.Location.Y + Me.massUnitField.Height + LayoutManager.SPACE_BETWEEN_CONTROLS_Y)
            Me.percentageUnitLabel.Size = componentsSize

            Me.percentageUnitField.Location = New Point(Me.percentageUnitLabel.Location.X, Me.percentageUnitLabel.Location.Y + Me.percentageUnitLabel.Height)
            Me.percentageUnitField.Size = componentsSize

            Me.productionRateUnitLabel.Location = New Point(Me.percentageUnitLabel.Location.X + Me.percentageUnitLabel.Width + LayoutManager.SPACE_BETWEEN_CONTROLS_X, Me.percentageUnitLabel.Location.Y)
            Me.productionRateUnitLabel.Size = componentsSize

            Me.productionRateUnitField.Location = New Point(Me.productionRateUnitLabel.Location.X, Me.productionRateUnitLabel.Location.Y + Me.productionRateUnitLabel.Height)
            Me.productionRateUnitField.Size = componentsSize

        End Sub
        Public Sub updateUnits(massUnit As Unit, temperatureUnit As Unit, percentageUnit As Unit, prodRateUnit As Unit, Optional throwEvents As Boolean = False)

            Me.shouldRaiseEvents = throwEvents

            Me.massUnitField.SelectedItem = massUnit
            Me.temperatureUnitField.SelectedItem = temperatureUnit
            Me.percentageUnitField.SelectedItem = percentageUnit
            Me.productionRateUnitField.SelectedItem = prodRateUnit

            Me.shouldRaiseEvents = True
        End Sub

        Private Sub onMassChanged() Handles massUnitField.SelectedValueChanged

            If (shouldRaiseEvents) Then

                RaiseEvent MassUnitChanged(massUnitField.SelectedItem)
            End If
        End Sub

        Private Sub onTemperatureChanged() Handles temperatureUnitField.SelectedValueChanged

            If (shouldRaiseEvents) Then

                RaiseEvent TemperatureUnitChanged(temperatureUnitField.SelectedItem)
            End If
        End Sub

        Private Sub onPercentageChanged() Handles percentageUnitField.SelectedValueChanged

            If (shouldRaiseEvents) Then

                RaiseEvent PercentageUnitChanged(percentageUnitField.SelectedItem)
            End If
        End Sub

        Private Sub onProductionRateChanged() Handles productionRateUnitField.SelectedValueChanged

            If (shouldRaiseEvents) Then

                RaiseEvent ProductionRateUnitChanged(productionRateUnitField.SelectedItem)
            End If
        End Sub

        Public WriteOnly Property Title As String
            Set(value As String)
                Me.titleLabel.Text = value
            End Set
        End Property

        Public Property MassUnit As Unit
            Get
                Return Me.massUnitField.SelectedItem
            End Get
            Set(value As Unit)
                Me.massUnitField.SelectedItem = value
            End Set
        End Property

        Public Property TemperatureUnit As Unit
            Get
                Return Me.temperatureUnitField.SelectedItem
            End Get
            Set(value As Unit)
                Me.temperatureUnitField.SelectedItem = value
            End Set
        End Property

        Public Property PercentageUnit As Unit
            Get
                Return Me.percentageUnitField.SelectedItem
            End Get
            Set(value As Unit)
                Me.percentageUnitField.SelectedItem = value
            End Set
        End Property

        Public Property ProductionRateUnit As Unit
            Get
                Return Me.productionRateUnitField.SelectedItem
            End Get
            Set(value As Unit)
                Me.productionRateUnitField.SelectedItem = value
            End Set
        End Property

    End Class
End Namespace
