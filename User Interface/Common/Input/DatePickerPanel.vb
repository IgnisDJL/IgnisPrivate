Namespace UI.Common

    Public Class DatePickerPanel
        Inherits Panel

        ' Constants
        Public Shared Shadows ReadOnly HEIGHT As Integer = 105
        Public Shared ReadOnly LABELS_HEIGHT As Integer = 20
        Public Shared ReadOnly DATE_PICKER_HEIGHT As Integer = 30
        Public Shared ReadOnly DATE_PICKER_MAXIMUM_WIDTH As Integer = 350
        Public Shared ReadOnly SHORTCUT_BUTTONS_HEIGHT As Integer = 40
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5

        Private Shared ReadOnly DATE_PICKER_CONDENSED_WIDTH As Integer = 210
        Private Shared ReadOnly DATE_PICKER_FORMAT_CONDENSED As String = " d MMM  yyyy HH mm ss"
        Private Shared ReadOnly DATE_PICKER_FORMAT_FULL As String = "    dd MMMM  yyyy HH mm ss"

        Private Const SHOW_END_DATE_TIME_PICKER_TOOL_TIP_TEXT As String = "Sélectionner plusieur dates"
        Private Const HIDE_END_DATE_TIME_PICKER_TOOL_TIP_TEXT As String = "Sélectionner une seule date"

        ' Components
        Private startDateLabel As Label
        Private endDateLabel As Label

        Private WithEvents startDatePicker As DateTimePicker
        Private WithEvents endDatePicker As DateTimePicker

        Private WithEvents changeLayoutButton As Button
        Private changeLayoutButtonToolTip As ToolTip

        ' Attributes
        Private _startDate As Date
        Private _endDate As Date

        Private _changeLayoutButtonIsShowing As Boolean = False
        Private _layoutType As LayoutTypes

        Private _shortCutButtons As List(Of Control)

        '' Note pour moi même, revoir la disposition des fonction d'ici la première release

        ' Events
        Public Event DatesChanged(startDate As Date, endDate As Date)
        Public Event LayoutChanged(newLayoutType As LayoutTypes)

        Public Sub New()
            MyBase.New()

            Me._shortCutButtons = New List(Of Control)

            Me.initializeComponents()
        End Sub

        Private Sub initializeComponents()

            Me.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.startDateLabel = New Label
            Me.startDateLabel.AutoSize = False

            Me.endDateLabel = New Label
            Me.endDateLabel.AutoSize = False
            Me.endDateLabel.Text = "Date de fin"

            Me.startDatePicker = New DateTimePicker
            Me.startDatePicker.Format = DateTimePickerFormat.Custom

            Me.endDatePicker = New DateTimePicker
            Me.endDatePicker.Format = DateTimePickerFormat.Custom

            Me.changeLayoutButton = New Button
            Me.changeLayoutButton.Size = New Size(DATE_PICKER_HEIGHT, DATE_PICKER_HEIGHT)
            Me.changeLayoutButton.ImageAlign = ContentAlignment.MiddleCenter

            Me.changeLayoutButtonToolTip = New ToolTip

            Me.Controls.Add(Me.startDateLabel)
            Me.Controls.Add(Me.startDatePicker)

            Me.StartDate = Today
            Me.EndDate = Today
            Me.LayoutType = LayoutTypes.DoubleDatePicker
        End Sub

        Public Sub ajustLayout(mySize As Size)
            Me.Size = mySize

            Dim datePickersAvailableWidth As Integer ' Includes the space between them if there are 2
            If (Me._changeLayoutButtonIsShowing) Then

                datePickersAvailableWidth = Me.Width - 3 * SPACE_BETWEEN_CONTROLS_X - Me.changeLayoutButton.Width
            Else
                datePickersAvailableWidth = Me.Width - 2 * SPACE_BETWEEN_CONTROLS_X
            End If

            Select Case Me._layoutType

                Case LayoutTypes.SingleDatePicker

                    If (datePickersAvailableWidth > DATE_PICKER_MAXIMUM_WIDTH) Then

                        If (Me._changeLayoutButtonIsShowing) Then

                            Me.startDateLabel.Location = New Point(Me.Width / 2 - (DATE_PICKER_MAXIMUM_WIDTH + Me.changeLayoutButton.Width + SPACE_BETWEEN_CONTROLS_X) / 2, 0)
                        Else

                            Me.startDateLabel.Location = New Point(Me.Width / 2 - DATE_PICKER_MAXIMUM_WIDTH / 2, 0)
                        End If

                        Me.startDateLabel.Size = New Size(DATE_PICKER_MAXIMUM_WIDTH, LABELS_HEIGHT)
                    Else
                        Me.startDateLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
                        Me.startDateLabel.Size = New Size(datePickersAvailableWidth, LABELS_HEIGHT)
                    End If

                    Me.startDatePicker.Location = New Point(Me.startDateLabel.Location.X, Me.startDateLabel.Height)
                    Me.startDatePicker.Size = Me.startDateLabel.Size

                    Me.changeLayoutButton.Location = New Point(Me.startDatePicker.Location.X + Me.startDatePicker.Width + SPACE_BETWEEN_CONTROLS_X, Me.startDatePicker.Location.Y)

                Case LayoutTypes.DoubleDatePicker

                    If (datePickersAvailableWidth > DATE_PICKER_MAXIMUM_WIDTH * 2 + SPACE_BETWEEN_CONTROLS_X) Then

                        If (Me._changeLayoutButtonIsShowing) Then

                            Me.startDateLabel.Location = New Point(Me.Width / 2 - (2 * DATE_PICKER_MAXIMUM_WIDTH + Me.changeLayoutButton.Width + 2 * SPACE_BETWEEN_CONTROLS_X) / 2, 0)
                        Else

                            Me.startDateLabel.Location = New Point(Me.Width / 2 - (2 * DATE_PICKER_MAXIMUM_WIDTH + SPACE_BETWEEN_CONTROLS_X) / 2, 0)
                        End If

                        Me.startDateLabel.Size = New Size(DATE_PICKER_MAXIMUM_WIDTH, LABELS_HEIGHT)
                        Me.endDateLabel.Size = New Size(DATE_PICKER_MAXIMUM_WIDTH, LABELS_HEIGHT)
                    Else
                        Me.startDateLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
                        Me.startDateLabel.Size = New Size((datePickersAvailableWidth - SPACE_BETWEEN_CONTROLS_X) / 2, LABELS_HEIGHT)
                    End If

                    Me.startDatePicker.Location = New Point(Me.startDateLabel.Location.X, Me.startDateLabel.Height)
                    Me.startDatePicker.Size = Me.startDateLabel.Size

                    Me.endDateLabel.Location = New Point(Me.startDateLabel.Location.X + Me.startDateLabel.Width + SPACE_BETWEEN_CONTROLS_X, Me.startDateLabel.Location.Y)
                    Me.endDateLabel.Size = Me.startDateLabel.Size

                    Me.endDatePicker.Location = New Point(Me.endDateLabel.Location.X, Me.endDateLabel.Height)
                    Me.endDatePicker.Size = Me.endDateLabel.Size

                    Me.changeLayoutButton.Location = New Point(Me.endDatePicker.Location.X + Me.endDatePicker.Width + SPACE_BETWEEN_CONTROLS_X, Me.endDatePicker.Location.Y)

            End Select

            ' Shortcut buttons
            If (Me._shortCutButtons.Count > 0) Then

                Dim shorcutButtonsWidth As Integer = (Me.Width - (Me._shortCutButtons.Count + 1) * SPACE_BETWEEN_CONTROLS_X) / Me._shortCutButtons.Count

                For i = 0 To Me._shortCutButtons.Count - 1
                    With Me._shortCutButtons(i)
                        .Location = New Point(i * (shorcutButtonsWidth + SPACE_BETWEEN_CONTROLS_X) + SPACE_BETWEEN_CONTROLS_X, Me.startDatePicker.Location.Y + DATE_PICKER_HEIGHT + 6)
                        .Size = New Size(shorcutButtonsWidth, SHORTCUT_BUTTONS_HEIGHT)
                    End With
                Next
            End If

            ' Date picker formats
            If (Me.startDatePicker.Width < DATE_PICKER_CONDENSED_WIDTH) Then
                Me.startDatePicker.CustomFormat = DATE_PICKER_FORMAT_CONDENSED
                Me.endDatePicker.CustomFormat = DATE_PICKER_FORMAT_CONDENSED
            Else
                Me.startDatePicker.CustomFormat = DATE_PICKER_FORMAT_FULL
                Me.endDatePicker.CustomFormat = DATE_PICKER_FORMAT_FULL
            End If
        End Sub

        Public Sub ajustLayoutFinal(mySize As Size)

        End Sub

        Private Sub listenToCalendarsCloseUp() Handles startDatePicker.CloseUp, endDatePicker.CloseUp
            onDatesChanged()
        End Sub

        Private Sub listenToEnterKey(o As Object, e As KeyEventArgs) Handles startDatePicker.KeyDown, endDatePicker.KeyDown
            If (e.KeyCode = Keys.Enter) Then
                e.SuppressKeyPress = True
                Me.onDatesChanged()
            End If
        End Sub

        Public Property StartDate As Date
            Get
                Return Me._startDate
            End Get
            Set(value As Date)
                Me.startDatePicker.Value = value
                Me._startDate = value
            End Set
        End Property

        Public Property EndDate As Date
            Get
                Return Me._endDate
            End Get
            Set(value As Date)
                Me.endDatePicker.Value = value
                Me._endDate = value
            End Set
        End Property

        Private Sub onDatesChanged() Handles startDatePicker.LostFocus, endDatePicker.LostFocus

            Dim startDateChanged As Boolean = False
            Dim endDateChanged As Boolean = False

            If (Not Me.startDatePicker.Value.Equals(Me._startDate)) Then

                Me._startDate = Me.startDatePicker.Value
                startDateChanged = True
            End If

            If (startDateChanged AndAlso Me._layoutType = LayoutTypes.SingleDatePicker) Then

                Me._endDate = Me._startDate
                Me.endDatePicker.Value = Me._startDate

            ElseIf (Me._layoutType = LayoutTypes.DoubleDatePicker AndAlso Not Me.endDatePicker.Value.Equals(Me._endDate)) Then

                Me._endDate = Me.endDatePicker.Value
                endDateChanged = True

            End If

            If (startDateChanged OrElse endDateChanged) Then
                RaiseEvent DatesChanged(Me._startDate, Me._endDate)
            End If

        End Sub

        Public Property LayoutType As LayoutTypes
            Get
                Return Me._layoutType
            End Get
            Set(type As LayoutTypes)

                If (Not Me._layoutType = type) Then

                    Select Case type

                        Case LayoutTypes.SingleDatePicker

                            Me.Controls.Remove(Me.endDateLabel)
                            Me.Controls.Remove(Me.endDatePicker)

                            Me.startDateLabel.Text = "Date sélectionnée"

                            Me.changeLayoutButton.Image = Constants.UI.Images._16x16.RIGHT_GREY_ARROW
                            Me.changeLayoutButtonToolTip.SetToolTip(Me.changeLayoutButton, SHOW_END_DATE_TIME_PICKER_TOOL_TIP_TEXT)

                        Case LayoutTypes.DoubleDatePicker

                            Me.Controls.Add(Me.endDateLabel)
                            Me.Controls.Add(Me.endDatePicker)

                            Me.startDateLabel.Text = "Date de début"

                            Me.changeLayoutButton.Image = Constants.UI.Images._16x16.LEFT_GREY_ARROW
                            Me.changeLayoutButtonToolTip.SetToolTip(Me.changeLayoutButton, HIDE_END_DATE_TIME_PICKER_TOOL_TIP_TEXT)

                    End Select

                    Me._layoutType = type
                    RaiseEvent LayoutChanged(type)

                    Me.ajustLayout(Me.Size)
                    Me.ajustLayoutFinal(Me.Size)
                End If

            End Set
        End Property

        Private Sub changeLayoutWithButton() Handles changeLayoutButton.Click

            Select Case Me._layoutType

                Case LayoutTypes.SingleDatePicker
                    Me.LayoutType = LayoutTypes.DoubleDatePicker

                Case LayoutTypes.DoubleDatePicker
                    Me.LayoutType = LayoutTypes.SingleDatePicker

            End Select
        End Sub

        Public Property ShowChangeLayoutButton As Boolean
            Get
                Return Me._changeLayoutButtonIsShowing
            End Get
            Set(showButton As Boolean)

                If (Not showButton = Me._changeLayoutButtonIsShowing) Then

                    If (showButton) Then

                        Me.Controls.Add(Me.changeLayoutButton)
                    Else

                        Me.Controls.Remove(Me.changeLayoutButton)
                    End If

                    Me._changeLayoutButtonIsShowing = showButton

                    Me.ajustLayout(Me.Size)
                    Me.ajustLayoutFinal(Me.Size)
                End If
            End Set
        End Property

        Public Sub addShortcutButton(button As Control)

            Me.Controls.Add(button)
            Me._shortCutButtons.Add(button)

            Me.ajustLayout(Me.Size)
            Me.ajustLayoutFinal(Me.Size)
        End Sub

        Public Sub removeShortcutButton(button As Control)

            Me.Controls.Remove(button)
            Me._shortCutButtons.Remove(button)

            Me.ajustLayout(Me.Size)
            Me.ajustLayoutFinal(Me.Size)
        End Sub

        Public Enum LayoutTypes
            SingleDatePicker = 1
            DoubleDatePicker = 2
        End Enum

    End Class
End Namespace
