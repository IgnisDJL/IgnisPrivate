Imports IGNIS.UI.Common

Namespace UI

    Public Class UsineSettingsView
        Inherits SettingsView

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Usine"
        Public Shared ReadOnly FUEL_UNIT_SUGGESTIONS As String() = {"L", "m³"}

        ' Components
        Private idLabel As Label
        Private idField As TextField
        Private idBuffer As String

        Private nameLabel As Label
        Private nameField As TextField
        Private nameBuffer As String

        Private typesLabel As Label
        Private typesContainer As Panel

        Private WithEvents hybridLabel As Label
        Private WithEvents hybridCheckBox As CheckBox

        Private WithEvents logLabel As Label
        Private WithEvents logCheckBox As CheckBox

        Private WithEvents csvLabel As Label
        Private WithEvents csvCheckBox As CheckBox

        Private WithEvents mdbLabel As Label
        Private WithEvents mdbCheckBox As CheckBox

        Private WithEvents newOperatorFirstNameField As TextField
        Private WithEvents newOperatorLastNameField As TextField
        Private WithEvents addNewOperatorButton As Button

        Private operatorListView As OperatorListView

        Private fuel1NameLabel As Label
        Private fuel1NameField As TextField
        Private fuel1NameBuffer As String

        Private fuel1UnitLabel As Label
        Private fuel1UnitField As ComboBox
        Private fuel1UnitBuffer As String

        Private fuel2NameLabel As Label
        Private fuel2NameField As TextField
        Private fuel2NameBuffer As String

        Private fuel2UnitLabel As Label
        Private fuel2UnitField As ComboBox
        Private fuel2UnitBuffer As String

        ' Attributes
        Private _usineSettings As UsineSettingsController

        Public Sub New()
            MyBase.New()

            Me.layout = New UsineSettingsViewLayout

            Me._usineSettings = ProgramController.SettingsControllers.UsineSettingsController

            Me.initializeComponents()

        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.idLabel = New Label
            Me.idLabel.Text = "Identifiant"

            Me.idField = New TextField
            Me.idField.TextAlign = HorizontalAlignment.Center
            Me.idField.MaxLength = 5
            Me.idField.CanBeEmpty = False
            AddHandler Me.idField.LostFocus, AddressOf Me.onIdChanged

            Me.nameLabel = New Label
            Me.nameLabel.Text = "Nom de l'usine"

            Me.nameField = New TextField
            Me.nameField.ValidationType = TextField.ValidationTypes.Text
            Me.nameField.CanBeEmpty = False
            AddHandler Me.nameField.LostFocus, AddressOf Me.onNameChanged

            Me.typesLabel = New Label
            Me.typesLabel.Text = "Type d'usine"

            Me.typesContainer = New Panel
            Me.typesContainer.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            ' #todo - tooltips for each types
            Me.hybridLabel = New Label
            Me.hybridLabel.Text = "Bimode"
            Me.hybridLabel.Cursor = Cursors.Hand

            Me.hybridCheckBox = New CheckBox
            Me.hybridCheckBox.Cursor = Cursors.Hand

            Me.logLabel = New Label
            Me.logLabel.Text = "Minds en continu"
            Me.logLabel.Cursor = Cursors.Hand

            Me.logCheckBox = New CheckBox
            Me.logCheckBox.Cursor = Cursors.Hand

            Me.csvLabel = New Label
            Me.csvLabel.Text = "Minds à batch"
            Me.csvLabel.Cursor = Cursors.Hand

            Me.csvCheckBox = New CheckBox
            Me.csvCheckBox.Cursor = Cursors.Hand

            Me.mdbLabel = New Label
            Me.mdbLabel.Text = "Marcotte"
            Me.mdbLabel.Cursor = Cursors.Hand

            Me.mdbCheckBox = New CheckBox
            Me.mdbCheckBox.Cursor = Cursors.Hand

            Me.typesContainer.Controls.Add(Me.hybridLabel)
            Me.typesContainer.Controls.Add(Me.logLabel)
            Me.typesContainer.Controls.Add(Me.csvLabel)
            Me.typesContainer.Controls.Add(Me.mdbLabel)

            Me.typesContainer.Controls.Add(Me.hybridCheckBox)
            Me.typesContainer.Controls.Add(Me.logCheckBox)
            Me.typesContainer.Controls.Add(Me.csvCheckBox)
            Me.typesContainer.Controls.Add(Me.mdbCheckBox)

            Me.newOperatorFirstNameField = New TextField
            Me.newOperatorFirstNameField.PlaceHolder = "Prénom"
            Me.newOperatorFirstNameField.ValidationType = TextField.ValidationTypes.Text
            Me.newOperatorFirstNameField.CanBeEmpty = False

            Me.newOperatorLastNameField = New TextField
            Me.newOperatorLastNameField.PlaceHolder = "Nom"
            Me.newOperatorLastNameField.ValidationType = TextField.ValidationTypes.Text
            Me.newOperatorLastNameField.CanBeEmpty = False

            Me.addNewOperatorButton = New Button
            Me.addNewOperatorButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewOperatorButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewOperatorButton.Enabled = False

            Me.operatorListView = New OperatorListView()
            AddHandler Me.operatorListView.deleteOperator, AddressOf Me.deleteOperator
            AddHandler Me.operatorListView.updateOperator, AddressOf Me.updateOperator

            Me.fuel1NameLabel = New Label
            Me.fuel1NameLabel.Text = "Nom du carburant 1"

            Me.fuel1NameField = New TextField
            AddHandler Me.fuel1NameField.LostFocus, AddressOf Me.updateFuelInfo

            Me.fuel1UnitLabel = New Label
            Me.fuel1UnitLabel.Text = "Unité"
            Me.fuel1UnitLabel.TextAlign = ContentAlignment.TopCenter

            Me.fuel1UnitField = New ComboBox
            Me.fuel1UnitField.DropDownStyle = ComboBoxStyle.DropDown
            Me.fuel1UnitField.Items.AddRange(FUEL_UNIT_SUGGESTIONS)
            AddHandler Me.fuel1UnitField.LostFocus, AddressOf Me.updateFuelInfo

            Me.fuel2NameLabel = New Label
            Me.fuel2NameLabel.Text = "Nom du carburant 2"

            Me.fuel2NameField = New TextField
            AddHandler Me.fuel2NameField.LostFocus, AddressOf Me.updateFuelInfo

            Me.fuel2UnitLabel = New Label
            Me.fuel2UnitLabel.Text = "Unité"
            Me.fuel2UnitLabel.TextAlign = ContentAlignment.TopCenter

            Me.fuel2UnitField = New ComboBox
            Me.fuel2UnitField.DropDownStyle = ComboBoxStyle.DropDown
            Me.fuel2UnitField.Items.AddRange(FUEL_UNIT_SUGGESTIONS)
            AddHandler Me.fuel2UnitField.LostFocus, AddressOf Me.updateFuelInfo

            Me.Controls.Add(Me.idLabel)
            Me.Controls.Add(Me.idField)
            Me.Controls.Add(Me.nameLabel)
            Me.Controls.Add(Me.nameField)
            Me.Controls.Add(Me.typesLabel)
            Me.Controls.Add(Me.typesContainer)
            Me.Controls.Add(Me.newOperatorFirstNameField)
            Me.Controls.Add(Me.newOperatorLastNameField)
            Me.Controls.Add(Me.addNewOperatorButton)
            Me.Controls.Add(Me.operatorListView)
            Me.Controls.Add(Me.fuel1NameLabel)
            Me.Controls.Add(Me.fuel1NameField)
            Me.Controls.Add(Me.fuel1UnitLabel)
            Me.Controls.Add(Me.fuel1UnitField)
            Me.Controls.Add(Me.fuel2NameLabel)
            Me.Controls.Add(Me.fuel2NameField)
            Me.Controls.Add(Me.fuel2UnitLabel)
            Me.Controls.Add(Me.fuel2UnitField)

            Me.nameField.TabIndex = 0
            Me.idField.TabIndex = 1
            Me.hybridCheckBox.TabIndex = 2
            Me.logCheckBox.TabIndex = 3
            Me.csvCheckBox.TabIndex = 4
            Me.mdbCheckBox.TabIndex = 5
            Me.BackButton.TabIndex = 6
            Me.newOperatorFirstNameField.TabIndex = 7
            Me.newOperatorLastNameField.TabIndex = 8
            Me.addNewOperatorButton.TabIndex = 9
            fuel1NameField.TabIndex = 10
            fuel1UnitField.TabIndex = 11
            fuel2NameField.TabIndex = 12
            fuel2UnitField.TabIndex = 13

            AddHandler Me.newOperatorFirstNameField.ValidationOccured, AddressOf Me.enableAddOperatorButton
            AddHandler Me.newOperatorLastNameField.ValidationOccured, AddressOf Me.enableAddOperatorButton

        End Sub

        Protected Overloads Overrides Sub ajustLayout()

            Dim layout = DirectCast(Me.layout, UsineSettingsViewLayout)

            Me.idLabel.Location = layout.IDLabel_Location
            Me.idLabel.Size = layout.IDLabel_Size

            Me.idField.Location = layout.IDField_Location
            Me.idField.Size = layout.IDField_Size

            Me.nameLabel.Location = layout.NameLabel_Location
            Me.nameLabel.Size = layout.NameLabel_Size

            Me.nameField.Location = layout.NameField_Location
            Me.nameField.Size = layout.NameField_Size

            Me.typesLabel.Location = layout.TypesLabel_Location
            Me.typesLabel.Size = layout.TypesLabel_Size

            Me.typesContainer.Location = layout.TypesContainer_Location
            Me.typesContainer.Size = layout.TypesContainer_Size

            Me.hybridLabel.Location = layout.HybridLabel_Location
            Me.hybridLabel.Size = layout.HybridLabel_Size
            Me.hybridCheckBox.Location = layout.HybridCheckBox_Location
            Me.hybridCheckBox.Size = UsineSettingsViewLayout.CHECK_BOX_SIZE

            Me.logLabel.Location = layout.LOGLabel_Location
            Me.logLabel.Size = layout.LOGLabel_Size
            Me.logCheckBox.Location = layout.LOGCheckBox_Location
            Me.logCheckBox.Size = UsineSettingsViewLayout.CHECK_BOX_SIZE

            Me.csvLabel.Location = layout.CSVLabel_Location
            Me.csvLabel.Size = layout.CSVLabel_Size
            Me.csvCheckBox.Location = layout.CSVCheckBox_Location
            Me.csvCheckBox.Size = UsineSettingsViewLayout.CHECK_BOX_SIZE

            Me.mdbLabel.Location = layout.MDBLabel_Location
            Me.mdbLabel.Size = layout.MDBLabel_Size
            Me.mdbCheckBox.Location = layout.MDBCheckBox_Location
            Me.mdbCheckBox.Size = UsineSettingsViewLayout.CHECK_BOX_SIZE

            Me.newOperatorFirstNameField.Location = layout.NewOperatorFirstNameField_Location
            Me.newOperatorFirstNameField.Size = layout.NewOperatorFirstNameField_Size

            Me.newOperatorLastNameField.Location = layout.NewOperatorLastNameField_Location
            Me.newOperatorLastNameField.Size = layout.NewOperatorLastNameField_Size

            Me.addNewOperatorButton.Location = layout.AddNewOperatorButton_Location
            Me.addNewOperatorButton.Size = layout.AddNewOperatorButton_Size

            Me.operatorListView.Location = layout.OperatorListView_Location
            Me.operatorListView.ajustLayout(layout.OperatorListView_Size)

            Me.fuel1NameLabel.Location = layout.Fuel1NameLabel_Location
            Me.fuel1NameLabel.Size = layout.Fuel1NameLabel_Size
            Me.fuel1NameField.Location = layout.Fuel1NameField_Location
            Me.fuel1NameField.Size = layout.Fuel1NameField_Size

            Me.fuel1UnitLabel.Location = layout.Fuel1UnitLabel_Location
            Me.fuel1UnitLabel.Size = layout.Fuel1UnitLabel_Size
            Me.fuel1UnitField.Location = layout.Fuel1UnitField_Location
            Me.fuel1UnitField.Size = layout.Fuel1UnitField_Size

            Me.fuel2NameLabel.Location = layout.Fuel2NameLabel_Location
            Me.fuel2NameLabel.Size = layout.Fuel2NameLabel_Size
            Me.fuel2NameField.Location = layout.Fuel2NameField_Location
            Me.fuel2NameField.Size = layout.Fuel2NameField_Size

            Me.fuel2UnitLabel.Location = layout.Fuel2UnitLabel_Location
            Me.fuel2UnitLabel.Size = layout.Fuel2UnitLabel_Size
            Me.fuel2UnitField.Location = layout.Fuel2UnitField_Location
            Me.fuel2UnitField.Size = layout.Fuel2UnitField_Size


        End Sub


        Protected Overloads Overrides Sub ajustLayoutFinal()

            Dim layout = DirectCast(Me.layout, UsineSettingsViewLayout)

            Me.operatorListView.ajustLayoutFinal(layout.OperatorListView_Size)

        End Sub

        Public Overrides Sub updateFields()

            Me.idField.DefaultText = Me._usineSettings.UsineID
            Me.idBuffer = Me.idField.Text

            Me.nameField.DefaultText = Me._usineSettings.UsineName
            Me.nameBuffer = nameField.Text

            RemoveHandler hybridCheckBox.CheckedChanged, AddressOf Me.onTypeChanged
            RemoveHandler csvCheckBox.CheckedChanged, AddressOf Me.onTypeChanged
            RemoveHandler logCheckBox.CheckedChanged, AddressOf Me.onTypeChanged
            RemoveHandler mdbCheckBox.CheckedChanged, AddressOf Me.onTypeChanged

            Me.hybridCheckBox.Checked = False
            Me.csvCheckBox.Checked = False
            Me.logCheckBox.Checked = False
            Me.mdbCheckBox.Checked = False

            Select Case Me._usineSettings.UsineType

                Case Constants.Settings.UsineType.HYBRID
                    Me.hybridCheckBox.Checked = True

                Case Constants.Settings.UsineType.CSV
                    Me.csvCheckBox.Checked = True

                Case Constants.Settings.UsineType.LOG
                    Me.logCheckBox.Checked = True

                Case Constants.Settings.UsineType.MDB
                    Me.mdbCheckBox.Checked = True

            End Select

            AddHandler hybridCheckBox.CheckedChanged, AddressOf Me.onTypeChanged
            AddHandler csvCheckBox.CheckedChanged, AddressOf Me.onTypeChanged
            AddHandler logCheckBox.CheckedChanged, AddressOf Me.onTypeChanged
            AddHandler mdbCheckBox.CheckedChanged, AddressOf Me.onTypeChanged

            Me.operatorListView.clear()

            For Each _operator As FactoryOperator In Me._usineSettings.getOperators

                Me.operatorListView.addObject(_operator)
            Next
            Me.operatorListView.refreshList()

            Me.fuel1NameField.DefaultText = Me._usineSettings.getFuel1Name
            Me.fuel1NameBuffer = Me.fuel1NameField.Text

            Me.fuel1UnitField.SelectedItem = Me._usineSettings.getFuel1Unit
            Me.fuel1UnitBuffer = Me.fuel1UnitField.Text

            Me.fuel2NameField.DefaultText = Me._usineSettings.getFuel2Name
            Me.fuel2NameBuffer = Me.fuel2NameField.Text

            Me.fuel2UnitField.SelectedItem = Me._usineSettings.getFuel2Unit
            Me.fuel2UnitBuffer = Me.fuel2UnitField.Text

        End Sub

        Protected Overloads Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub afterShow()

            ' #patch Bug fix - Always selected on show. Don't know why
            Me.fuel1UnitField.SelectionLength = 0
            Me.fuel2UnitField.SelectionLength = 0

            Me.Focus()

        End Sub

        Public Overrides Sub onHide()

        End Sub

        Public Sub onIdChanged()

            If (Not Me.idBuffer = Me.idField.Text AndAlso _
                Me.idField.IsValid) Then

                Me._usineSettings.UsineID = Me.idField.Text
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Public Sub onNameChanged()

            If (Not Me.nameBuffer = Me.nameField.Text AndAlso _
                Me.nameField.IsValid) Then

                Me._usineSettings.UsineName = Me.nameField.Text
                Me.raiseSettingChangedEvent()
            End If
        End Sub

        Private Sub onTypeChanged(sender As Object, args As EventArgs)

            If (DirectCast(sender, CheckBox).Checked) Then

                If (sender.Equals(Me.hybridCheckBox)) Then

                    Me._usineSettings.UsineType = Constants.Settings.UsineType.HYBRID

                ElseIf (sender.Equals(Me.csvCheckBox)) Then

                    Me._usineSettings.UsineType = Constants.Settings.UsineType.CSV

                ElseIf (sender.Equals(Me.logCheckBox)) Then

                    Me._usineSettings.UsineType = Constants.Settings.UsineType.LOG

                ElseIf (sender.Equals(Me.mdbCheckBox)) Then

                    Me._usineSettings.UsineType = Constants.Settings.UsineType.MDB

                End If

            ElseIf (Not Me.hybridCheckBox.Checked AndAlso _
                    Not Me.csvCheckBox.Checked AndAlso _
                    Not Me.logCheckBox.Checked AndAlso _
                    Not Me.mdbCheckBox.Checked) Then

                Me._usineSettings.UsineType = Constants.Settings.UsineType.UNKNOWN

            End If

            Me.raiseSettingChangedEvent()

        End Sub

        Private Sub addOperator() Handles addNewOperatorButton.Click

            Me._usineSettings.addNewOperator(Me.newOperatorFirstNameField.Text, Me.newOperatorLastNameField.Text)

            Me.newOperatorFirstNameField.Text = ""
            Me.newOperatorLastNameField.Text = ""
            Me.addNewOperatorButton.Enabled = False

            Me.raiseSettingChangedEvent()

            Me.operatorListView.selectLastItem()

        End Sub

        Private Sub addOperatorOnEnter(sender As Object, e As KeyEventArgs) Handles newOperatorFirstNameField.KeyDown, newOperatorLastNameField.KeyDown

            If (e.KeyCode = Keys.Enter) Then

                If (Me.addNewOperatorButton.Enabled) Then
                    Me.addOperator()
                Else
                    Beep()
                End If
            End If
        End Sub

        Private Sub enableAddOperatorButton()

            If (newOperatorFirstNameField.IsValid AndAlso
                newOperatorLastNameField.IsValid) Then

                Me.addNewOperatorButton.Enabled = True
            Else
                Me.addNewOperatorButton.Enabled = False
            End If
        End Sub

        Private Sub deleteOperator(operatorToDelete As FactoryOperator)

            Me._usineSettings.removeOperator(operatorToDelete)
            Me.raiseSettingChangedEvent()
        End Sub

        Private Sub updateOperator(operatorToUpdate As FactoryOperator, newFirstName As String, newLastName As String)

            Me._usineSettings.updateOperator(operatorToUpdate, newFirstName, newLastName)
            Me.raiseSettingChangedEvent()
        End Sub

        Private Sub updateFuelInfo()

            ' If one of the fields' value changed
            If (Not Me.fuel1NameBuffer = Me.fuel1NameField.Text OrElse _
                Not Me.fuel1UnitBuffer = Me.fuel1UnitField.Text OrElse _
                Not Me.fuel2NameBuffer = Me.fuel2NameField.Text OrElse _
                Not Me.fuel2UnitBuffer = Me.fuel2UnitField.Text) Then

                ' If values are valid
                If (Me.fuel1NameField.IsValid AndAlso Me.fuel2NameField.IsValid) Then

                    Me._usineSettings.updateFuelInformation(Me.fuel1NameField.Text, Me.fuel1UnitField.Text, Me.fuel2NameField.Text, Me.fuel2UnitField.Text)
                    Me.raiseSettingChangedEvent()
                End If
            End If
        End Sub

        ' Check checkbox when label is clicked
        Private Sub hybridLabelClick() Handles hybridLabel.Click
            Me.hybridCheckBox.Checked = Not Me.hybridCheckBox.Checked
        End Sub
        Private Sub mdbLabelClick() Handles mdbLabel.Click
            Me.mdbCheckBox.Checked = Not Me.mdbCheckBox.Checked
        End Sub
        Private Sub csvLabelClick() Handles csvLabel.Click
            Me.csvCheckBox.Checked = Not Me.csvCheckBox.Checked
        End Sub
        Private Sub logLabelClick() Handles logLabel.Click
            Me.logCheckBox.Checked = Not Me.logCheckBox.Checked
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Protected Overrides ReadOnly Property Controller As SettingsController
            Get
                Return Me._usineSettings
            End Get
        End Property
    End Class
End Namespace
