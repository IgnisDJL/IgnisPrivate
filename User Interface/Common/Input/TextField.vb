Imports System.Text.RegularExpressions

Namespace UI.Common

    Public Class TextField
        Inherits TextBox

        ' Add method add validation regex
        ' Add tooltip with it (mandatory breh)
        ' Display tool tip that corresponds the regex that failed
        ' Check if field is valid before saving (in settings)
        ' Do the rest of the fields in settings and manual data

        ' Constants
        Public Shared ReadOnly INVALID_COLOR As Color = Color.Red
        Public Shared ReadOnly VALID_COLOR As Color = Color.Green

        Public Shared ReadOnly NO_VALIDATION_TYPE_REGEX As Regex = New Regex("^.*$")
        Public Shared ReadOnly NUMBER_VALIDATION_TYPE_REGEX As Regex = New Regex("^[\d]*$")
        Public Shared ReadOnly DECIMAL_VALIDATION_TYPE_REGEX As Regex = New Regex("^([\d]+(\" & Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator & "[\d]+)?)?$")
        Public Shared ReadOnly TEXT_VALIDATION_TYPE_REGEX As Regex = New Regex("^[\w_\-\s'\,\.]*$")
        Public Shared ReadOnly EMAIL_VALIDATION_TYPE_REGEX As Regex = New Regex("^([\w\d_\-\.]+@[\w\d_\-\.]+\.[\w]{2,4})?$")

        ' #language
        Public Shared ReadOnly NUMBER_VALIDATION_TYPE_WARNING_MESSAGE As String = "Ce champs ne peux contenir que des nombre entiers (e.g. 45)"
        Public Shared ReadOnly DECIMAL_VALIDATION_TYPE_WARNING_MESSAGE As String = "Ce champs ne peux contenir que des nombres entiers et décimaux (e.g. 61 ou 23,47)"
        Public Shared ReadOnly TEXT_VALIDATION_TYPE_WARNING_MESSAGE As String = "Ce champs ne peux contenir que des caractères alphanumériques (i.e. 0-9, A-Z, a-z) en plus des caractères suivants  - _ ' , ."
        Public Shared ReadOnly EMAIL_VALIDATION_TYPE_WARNING_MESSAGE As String = "Le contenu de ce champs doit correspondre à une adresse courriel (e.g. nom.prenom@djl.ca)"
        Public Shared ReadOnly CANNOT_BE_EMPTY_WARNING_MESSAGE As String = "Ce champs ne peux pas être vide"

        ' Components
        Private warningTooltip As ToolTip

        ' Attributes
        Private _placeHolderIsShowing As Boolean = False

        ' -- validation
        Private _validationRegex As Regex = Nothing
        Private _validationMessage As String = Nothing
        Private _validationType As ValidationTypes = ValidationTypes.None
        Private _isValid As Boolean = True

        Private _validateOnKeyPress As Boolean = True
        Private _validateOnFocusOut As Boolean = True
        Private _validateOnEnterKey As Boolean = False

        Private _canBeEmpty As Boolean = True
        Private _acceptPercentSign As Boolean = False
        Private _acceptTemperature As Boolean = False
        Private _canBeUnknown As Boolean = False

        ' -- appearance
        Private _defaultFont As Font
        Private _defaultForeColor As Color
        Private _placeHolder As String = Nothing
        Private placeHolderFont = Constants.UI.Fonts.DEFAULT_FONT_ITALIC
        Private placeHolderForeColor = Color.Gray

        ' Events
        Private validateOnTextChangedCounter As Integer = 0
        Public Event ValidationOccured(ByVal isValid As Boolean)

        Public Sub New()
            MyBase.New()

            Me.Font = Constants.UI.Fonts.DEFAULT_FONT
            Me.Multiline = True

            Me.warningTooltip = New ToolTip
            Me.warningTooltip.Active = False
            Me.warningTooltip.ShowAlways = True
            Me.warningTooltip.InitialDelay = 35
            Me.warningTooltip.AutoPopDelay = 100000

        End Sub

        Private Sub validate(doBeep As Boolean)

            Dim wasValid As Boolean = Me._isValid
            Dim skipRestOfValidation As Boolean = False

            Dim isEmpty As Boolean = Me.Text.Length = 0

            Dim fieldContent As String = Me.Text

            ' Ignore accpeted characters
            If (Me._acceptPercentSign AndAlso fieldContent.EndsWith("%")) Then
                fieldContent = fieldContent.Substring(0, fieldContent.Length - 1)

                If (fieldContent.Length = 0) Then
                    Me.Text = ""
                    isEmpty = True
                End If

            ElseIf (Me._acceptTemperature AndAlso fieldContent.EndsWith(Constants.Units.StringRepresentation.CELSIUS)) Then
                ' #refactor for current temp unit
                fieldContent = fieldContent.Substring(0, fieldContent.Length - Constants.Units.StringRepresentation.CELSIUS.Length)

                If (fieldContent.Length = 0) Then
                    Me.Text = ""
                    isEmpty = True
                End If
            End If

            ' Validate emptyness
            If (Not Me._canBeEmpty AndAlso isEmpty) Then

                Me._isValid = False
                skipRestOfValidation = True

                Me.warningTooltip.SetToolTip(Me, CANNOT_BE_EMPTY_WARNING_MESSAGE)

            Else

                Me._isValid = True
            End If

            ' Validate unknown value
            If (Not skipRestOfValidation AndAlso Me._canBeUnknown AndAlso Me.Text = "?") Then

                skipRestOfValidation = True
                Me._isValid = True
            End If

            ' Validate with regex
            If (Not skipRestOfValidation AndAlso Not IsNothing(Me._validationRegex)) Then

                Me._isValid = Me._validationRegex.Match(fieldContent).Success
                skipRestOfValidation = True

                If (Not Me.IsValid) Then

                    If (Not IsNothing(Me._validationMessage)) Then
                        Me.warningTooltip.SetToolTip(Me, Me._validationMessage)
                    End If
                End If
            End If

            If (Me._isValid) Then

                If (Not Me._placeHolderIsShowing) Then
                    Me.ForeColor = DefaultForeColor
                End If

                Me.warningTooltip.Active = False

            Else

                If (Not Me._placeHolderIsShowing) Then
                    Me.ForeColor = INVALID_COLOR
                End If

                If (wasValid AndAlso doBeep) Then
                    Beep()
                End If

                Me.warningTooltip.Active = True
            End If

            RaiseEvent ValidationOccured(Me._isValid)

        End Sub

        ' #refactor if needed to addValidationRegex(regex, warningMessage)
        Public Property ValidationRegex As Regex
            Get
                Return Me._validationRegex
            End Get
            Set(value As Regex)
                If (Me._validationType = ValidationTypes.Custom) Then
                    Me._validationRegex = value
                Else
                    Throw New Exception("Can't set validation regex if the validation type is not set to Custom")
                End If
            End Set
        End Property

        Public ReadOnly Property IsValid As Boolean
            Get
                Return Me._isValid
            End Get
        End Property

        Public Property ValidationType As ValidationTypes
            Get
                Return Me._validationType
            End Get
            Set(value As ValidationTypes)

                Me._validationType = value

                Select Case Me._validationType

                    Case ValidationTypes.None
                        Me._validationRegex = NO_VALIDATION_TYPE_REGEX
                        Me._validationMessage = Nothing

                    Case ValidationTypes.Numbers
                        Me._validationRegex = NUMBER_VALIDATION_TYPE_REGEX
                        Me._validationMessage = NUMBER_VALIDATION_TYPE_WARNING_MESSAGE

                    Case ValidationTypes.Decimals
                        Me._validationRegex = DECIMAL_VALIDATION_TYPE_REGEX
                        Me._validationMessage = DECIMAL_VALIDATION_TYPE_WARNING_MESSAGE

                    Case ValidationTypes.Text
                        Me._validationRegex = TEXT_VALIDATION_TYPE_REGEX
                        Me._validationMessage = TEXT_VALIDATION_TYPE_WARNING_MESSAGE

                    Case ValidationTypes.Email
                        Me._validationRegex = EMAIL_VALIDATION_TYPE_REGEX
                        Me._validationMessage = EMAIL_VALIDATION_TYPE_WARNING_MESSAGE

                    Case ValidationTypes.Custom
                        ' do nothing
                        Me._validationMessage = Nothing

                End Select

                Me.validate(False)
            End Set
        End Property

        Public WriteOnly Property ValidateOnKeyPress As Boolean
            Set(value As Boolean)
                Me._validateOnKeyPress = value
            End Set
        End Property

        Public Property PlaceHolder As String
            Get
                Return Me._placeHolder
            End Get
            Set(value As String)
                Me._placeHolder = value
                Me.showPlaceHolder()
            End Set
        End Property

        Private Sub showPlaceHolder()

            If (Not IsNothing(Me._placeHolder) AndAlso Me.Text = "") Then

                MyBase.Font = Me.placeHolderFont
                MyBase.ForeColor = placeHolderForeColor
                Me._placeHolderIsShowing = True
                Me.Text = Me._placeHolder

            End If
        End Sub

        Private Sub hidePlaceHolder()

            If (Me._placeHolderIsShowing) Then

                Me._placeHolderIsShowing = False
                Me.Text = ""
            End If

            MyBase.Font = Me._defaultFont
            MyBase.ForeColor = Me._defaultForeColor
        End Sub

        Public Property CanBeEmpty As Boolean
            Get
                Return Me._canBeEmpty
            End Get
            Set(value As Boolean)
                Me._canBeEmpty = value
                Me.validate(False)
            End Set
        End Property

        Public Property CanBeUnknown As Boolean
            Get
                Return Me._canBeUnknown
            End Get
            Set(value As Boolean)
                Me._canBeUnknown = value
                Me.validate(False)
            End Set
        End Property

        Public Property AcceptsPercentSign As Boolean
            Get
                Return Me._acceptPercentSign
            End Get
            Set(value As Boolean)
                Me._acceptPercentSign = value
                Me.validate(False)
            End Set
        End Property

        Public Property AcceptsTemperature As Boolean
            Get
                Return Me._acceptTemperature
            End Get
            Set(value As Boolean)
                Me._acceptTemperature = value
                Me.validate(False)
            End Set
        End Property

        Private Sub _onKeyPress(sender As Object, e As KeyEventArgs) Handles Me.KeyUp

            If (e.KeyCode = Keys.Enter AndAlso Me._validateOnEnterKey) Then
                validate(True)
            End If
        End Sub

        Private Sub onDecimalSeperator(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

            If (e.KeyCode = Keys.Decimal AndAlso Me._validationType = ValidationTypes.Decimals) Then

                Me.ValidateOnTextChanged = False
                Me.SelectedText = Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator

                If (Me.SelectionStart = Me.Text.Length) Then
                    Me.Text = Me.Text & "0"
                    Me.SelectionStart = Me.Text.Length - 1
                    Me.SelectionLength = 1
                End If

                Me.ValidateOnTextChanged = True
                Me.validate(True)

                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub _onTextChanged() Handles Me.TextChanged

            If (Not Me.Focused) Then
                Me.showPlaceHolder()
            End If

            ' Remove new lines (because the Multiline property is set to true so it doesn't beep when user presses Enter)
            ' The enter key is already suppressed but this is in case of a copy paste or something
            Me.Text = Me.Text.Replace(Environment.NewLine, " ")

            If (Me.ValidateOnTextChanged AndAlso Me._validateOnKeyPress) Then

                validate(True)
            End If
        End Sub

        Private Sub _onBlur() Handles Me.LostFocus

            Me.showPlaceHolder()

            If (Me._validateOnFocusOut) Then
                validate(False)
            End If
        End Sub

        Private Sub suppressEnterKey(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

            If (e.KeyCode = Keys.Enter) Then
                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub _onFocus() Handles Me.GotFocus

            Me.hidePlaceHolder()

        End Sub

        ''' <summary>
        ''' Set's the text of the field and triggers a silent validation
        ''' </summary>
        Public WriteOnly Property DefaultText As String
            Set(value As String)
                Me.Text = value
                Me.validate(False)
            End Set
        End Property

        Public Overrides Property Font As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
                Me._defaultFont = value
                Me.placeHolderFont = New Font(value, FontStyle.Italic)
            End Set
        End Property

        Public Overrides Property ForeColor As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)

                If (value = Me.placeHolderForeColor) Then
                    Throw New Exception("This color is used for the place holder. Pick another one please.")
                Else
                    MyBase.ForeColor = value
                    Me._defaultForeColor = value
                End If
            End Set
        End Property

        Public Overrides Property Text As String
            Get
                If (Me._placeHolderIsShowing) Then
                    Return ""
                Else
                    Return MyBase.Text
                End If
            End Get
            Set(value As String)
                Me.ValidateOnTextChanged = False
                MyBase.Text = value
                Me.ValidateOnTextChanged = True
            End Set
        End Property

        Private Property ValidateOnTextChanged As Boolean
            Get
                Return Me.validateOnTextChangedCounter = 0
            End Get
            Set(value As Boolean)

                If (value) Then

                    If (Me.validateOnTextChangedCounter > 0) Then
                        Me.validateOnTextChangedCounter -= 1
                    End If

                Else
                    Me.validateOnTextChangedCounter += 1
                End If
            End Set
        End Property

        Public Enum ValidationTypes
            None = 0
            Numbers = 1
            Decimals = 2
            Text = 3
            Email = 4
            Custom = 5
        End Enum

    End Class
End Namespace
