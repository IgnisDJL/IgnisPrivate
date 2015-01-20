Namespace UI

    Public Class SplitDelayPanel
        Inherits Common.PopUpMessage

        ' Constants
        Private Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 20
        Private Shared ReadOnly SPACE_BETWEEN_CONTROLS_Y As Integer = 15

        Private Shared ReadOnly BUTTONS_SIZE As Size = New Size(90, 30)

        Private Shared ReadOnly LABELS_HEIGHT As Integer = 30
        Private Shared ReadOnly TIME_LABELS_SIZE As Size = New Size(60, LABELS_HEIGHT)

        Private Shared ReadOnly SPLITTER_CURSOR_SIZE As Size = New Size(30, 40)
        Private Shared ReadOnly CURSOR_POSITION_LABEL_SIZE As Size = New Size(50, 25)
        Private Shared ReadOnly CURSOR_POSITION_LABEL_OFFSET As Point = New Point(-10, -25)

        ' Components
        Private delayTextLabel1 As Label

        Private delayStartTextLabel1 As Label
        Private delayStartValueLabel1 As Label

        Private delayEndTextLabel1 As Label
        Private delayEndValueLabel1 As Label

        Private delayDurationTextLabel1 As Label
        Private delayDurationValueLabel1 As Label


        Private delayTextLabel2 As Label

        Private delayStartTextLabel2 As Label
        Private delayStartValueLabel2 As Label

        Private delayEndTextLabel2 As Label
        Private delayEndValueLabel2 As Label

        Private delayDurationTextLabel2 As Label
        Private delayDurationValueLabel2 As Label

        Private spliterPanel As Panel
        Private WithEvents spliterCursor As Panel

        Private cursorPositionLabel As Label

        Private WithEvents okButton As Button
        Private WithEvents cancelButton As Common.CancelButton

        ' Attributes
        Private startTime As Date
        Private endTime As Date
        Private durationInMinutes As Integer

        Private _splitTime As Date

        Public Sub New()

            Me.initializeComponents()

            AddHandler delayTextLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayStartTextLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayStartValueLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayEndTextLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayEndValueLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayDurationTextLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayDurationValueLabel1.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayTextLabel2.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayStartTextLabel2.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayStartValueLabel2.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayEndTextLabel2.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayEndValueLabel2.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayDurationTextLabel2.MouseDown, AddressOf Me._onMouseDown
            AddHandler delayDurationValueLabel2.MouseDown, AddressOf Me._onMouseDown

            AddHandler delayTextLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayStartTextLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayStartValueLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayEndTextLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayEndValueLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayDurationTextLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayDurationValueLabel1.MouseMove, AddressOf Me.dragLocation
            AddHandler delayTextLabel2.MouseMove, AddressOf Me.dragLocation
            AddHandler delayStartTextLabel2.MouseMove, AddressOf Me.dragLocation
            AddHandler delayStartValueLabel2.MouseMove, AddressOf Me.dragLocation
            AddHandler delayEndTextLabel2.MouseMove, AddressOf Me.dragLocation
            AddHandler delayEndValueLabel2.MouseMove, AddressOf Me.dragLocation
            AddHandler delayDurationTextLabel2.MouseMove, AddressOf Me.dragLocation
            AddHandler delayDurationValueLabel2.MouseMove, AddressOf Me.dragLocation

            AddHandler delayTextLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayStartTextLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayStartValueLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayEndTextLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayEndValueLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayDurationTextLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayDurationValueLabel1.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayTextLabel2.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayStartTextLabel2.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayStartValueLabel2.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayEndTextLabel2.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayEndValueLabel2.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayDurationTextLabel2.MouseUp, AddressOf Me._onMouseUp
            AddHandler delayDurationValueLabel2.MouseUp, AddressOf Me._onMouseUp

        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            ' Title 1
            Me.delayTextLabel1 = New Label
            Me.delayTextLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayTextLabel1.Text = "Délai 1"
            Me.delayTextLabel1.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            Me.delayTextLabel1.Size = New Size(TIME_LABELS_SIZE.Width * 2, LABELS_HEIGHT)

            ' Start 1
            Me.delayStartTextLabel1 = New Label
            Me.delayStartTextLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayStartTextLabel1.Text = "Début :"
            Me.delayStartTextLabel1.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.delayStartTextLabel1.Size = TIME_LABELS_SIZE
            Me.delayStartValueLabel1 = New Label
            Me.delayStartValueLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayStartValueLabel1.Size = TIME_LABELS_SIZE

            ' End 1
            Me.delayEndTextLabel1 = New Label
            Me.delayEndTextLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayEndTextLabel1.Text = "Fin :"
            Me.delayEndTextLabel1.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.delayEndTextLabel1.Size = TIME_LABELS_SIZE
            Me.delayEndValueLabel1 = New Label
            Me.delayEndValueLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayEndValueLabel1.Size = TIME_LABELS_SIZE

            ' Duration 1
            Me.delayDurationTextLabel1 = New Label
            Me.delayDurationTextLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayDurationTextLabel1.Text = "Durée :"
            Me.delayDurationTextLabel1.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.delayDurationTextLabel1.Size = TIME_LABELS_SIZE
            Me.delayDurationValueLabel1 = New Label
            Me.delayDurationValueLabel1.TextAlign = ContentAlignment.MiddleCenter
            Me.delayDurationValueLabel1.Size = TIME_LABELS_SIZE

            ' Title 2
            Me.delayTextLabel2 = New Label
            Me.delayTextLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayTextLabel2.Text = "Délai 2"
            Me.delayTextLabel2.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            Me.delayTextLabel2.Size = New Size(TIME_LABELS_SIZE.Width * 2, LABELS_HEIGHT)

            ' Start 2
            Me.delayStartTextLabel2 = New Label
            Me.delayStartTextLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayStartTextLabel2.Text = "Début :"
            Me.delayStartTextLabel2.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.delayStartTextLabel2.Size = TIME_LABELS_SIZE
            Me.delayStartValueLabel2 = New Label
            Me.delayStartValueLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayStartValueLabel2.Size = TIME_LABELS_SIZE

            ' End 2
            Me.delayEndTextLabel2 = New Label
            Me.delayEndTextLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayEndTextLabel2.Text = "Fin :"
            Me.delayEndTextLabel2.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.delayEndTextLabel2.Size = TIME_LABELS_SIZE
            Me.delayEndValueLabel2 = New Label
            Me.delayEndValueLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayEndValueLabel2.Size = TIME_LABELS_SIZE

            ' Duration 2
            Me.delayDurationTextLabel2 = New Label
            Me.delayDurationTextLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayDurationTextLabel2.Text = "Durée :"
            Me.delayDurationTextLabel2.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.delayDurationTextLabel2.Size = TIME_LABELS_SIZE
            Me.delayDurationValueLabel2 = New Label
            Me.delayDurationValueLabel2.TextAlign = ContentAlignment.MiddleCenter
            Me.delayDurationValueLabel2.Size = TIME_LABELS_SIZE

            ' Splitter
            Me.spliterPanel = New Panel
            Me.spliterPanel.BackgroundImageLayout = ImageLayout.Stretch
            Me.spliterPanel.BackgroundImage = Constants.UI.Images.DelaySpliter.BACKGROUND

            Me.spliterCursor = New Panel
            Me.spliterCursor.BackgroundImageLayout = ImageLayout.Stretch
            Me.spliterCursor.BackgroundImage = Constants.UI.Images.DelaySpliter.CURSOR
            Me.spliterCursor.Size = SPLITTER_CURSOR_SIZE
            Me.spliterCursor.Cursor = Constants.UI.Images.Cursors.OPEN_HAND

            Me.cursorPositionLabel = New Label
            Me.cursorPositionLabel.AutoSize = False
            Me.cursorPositionLabel.Size = CURSOR_POSITION_LABEL_SIZE
            Me.cursorPositionLabel.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.cursorPositionLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.cursorPositionLabel.ForeColor = FORE_COLOR

            Me.okButton = New Button
            Me.okButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.okButton.Image = Constants.UI.Images._24x24.GOOD
            Me.okButton.FlatStyle = FlatStyle.Flat
            Me.okButton.Size = BUTTONS_SIZE
            Me.okButton.BackColor = FORE_COLOR
            Me.okButton.ForeColor = BACK_COLOR
            Me.okButton.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.okButton.TextAlign = ContentAlignment.MiddleRight
            Me.okButton.Text = "Diviser"

            Me.cancelButton = New Common.CancelButton
            Me.cancelButton.FlatStyle = FlatStyle.Flat
            Me.cancelButton.Size = BUTTONS_SIZE
            Me.cancelButton.BackColor = FORE_COLOR
            Me.cancelButton.ForeColor = BACK_COLOR
            Me.cancelButton.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT
            Me.cancelButton.Text = "Annuler"
            Me.cancelButton.TextAlign = ContentAlignment.MiddleRight
            Me.cancelButton.UseVisualStyleBackColor = False

            Me.Controls.Add(delayTextLabel1)
            Me.Controls.Add(delayStartTextLabel1)
            Me.Controls.Add(delayStartValueLabel1)
            Me.Controls.Add(delayEndTextLabel1)
            Me.Controls.Add(delayEndValueLabel1)
            Me.Controls.Add(delayDurationTextLabel1)
            Me.Controls.Add(delayDurationValueLabel1)
            Me.Controls.Add(delayTextLabel2)
            Me.Controls.Add(delayStartTextLabel2)
            Me.Controls.Add(delayStartValueLabel2)
            Me.Controls.Add(delayEndTextLabel2)
            Me.Controls.Add(delayEndValueLabel2)
            Me.Controls.Add(delayDurationTextLabel2)
            Me.Controls.Add(delayDurationValueLabel2)

            Me.Controls.Add(cursorPositionLabel)
            Me.Controls.Add(spliterCursor)
            Me.Controls.Add(spliterPanel)
            Me.Controls.Add(okButton)
            Me.Controls.Add(cancelButton)



        End Sub

        Protected Overrides Sub ajustLayout()

            Me.delayTextLabel1.Location = New Point(Me.Width / 2 - delayTextLabel1.Width - 25, SPACE_BETWEEN_CONTROLS_Y)
            Me.delayTextLabel2.Location = New Point(Me.Width / 2 + 25, SPACE_BETWEEN_CONTROLS_Y)

            Me.delayStartTextLabel1.Location = New Point(Me.delayTextLabel1.Location.X, Me.delayTextLabel1.Location.Y + LABELS_HEIGHT)
            Me.delayStartValueLabel1.Location = New Point(Me.delayStartTextLabel1.Location.X + TIME_LABELS_SIZE.Width, delayStartTextLabel1.Location.Y)
            Me.delayStartTextLabel2.Location = New Point(Me.delayTextLabel2.Location.X, Me.delayTextLabel2.Location.Y + LABELS_HEIGHT)
            Me.delayStartValueLabel2.Location = New Point(Me.delayStartTextLabel2.Location.X + TIME_LABELS_SIZE.Width, delayStartTextLabel2.Location.Y)

            Me.delayEndTextLabel1.Location = New Point(delayTextLabel1.Location.X, delayStartTextLabel1.Location.Y + LABELS_HEIGHT)
            Me.delayEndValueLabel1.Location = New Point(delayEndTextLabel1.Location.X + TIME_LABELS_SIZE.Width, delayEndTextLabel1.Location.Y)
            Me.delayEndTextLabel2.Location = New Point(delayTextLabel2.Location.X, delayStartTextLabel2.Location.Y + LABELS_HEIGHT)
            Me.delayEndValueLabel2.Location = New Point(delayEndTextLabel2.Location.X + TIME_LABELS_SIZE.Width, delayEndTextLabel2.Location.Y)

            Me.delayDurationTextLabel1.Location = New Point(delayTextLabel1.Location.X, delayEndTextLabel1.Location.Y + LABELS_HEIGHT)
            Me.delayDurationValueLabel1.Location = New Point(delayDurationTextLabel1.Location.X + TIME_LABELS_SIZE.Width, delayDurationTextLabel1.Location.Y)
            Me.delayDurationTextLabel2.Location = New Point(delayTextLabel2.Location.X, delayEndTextLabel2.Location.Y + LABELS_HEIGHT)
            Me.delayDurationValueLabel2.Location = New Point(delayDurationTextLabel2.Location.X + TIME_LABELS_SIZE.Width, delayDurationTextLabel2.Location.Y)


            Me.spliterPanel.Size = New Size(Me.Width - 2 * SPACE_BETWEEN_CONTROLS_X, 50)
            Me.spliterPanel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, Me.Height - BUTTONS_SIZE.Height - spliterPanel.Height - cursorPositionLabel.Height)

            Me.spliterCursor.Location = New Point(Me.spliterPanel.Location.X + Me.spliterPanel.Width / 2 - Me.spliterCursor.Width / 2, Me.spliterPanel.Location.Y + 5)

            Me.okButton.Location = New Point(Me.Width - okButton.Width - 1, Me.Height - okButton.Height - 1)
            Me.cancelButton.Location = New Point(Me.okButton.Location.X - cancelButton.Width - 5, okButton.Location.Y)

            Me.Refresh()
        End Sub

        Public Sub beforeShow(startTime As Date, endTime As Date)

            Me.startTime = startTime
            Me.endTime = endTime

            Me.durationInMinutes = endTime.Subtract(startTime).TotalMinutes

            Me._splitTime = startTime.Add(TimeSpan.FromMinutes(endTime.Subtract(startTime).TotalMinutes / 2)) ' Start at 50 %
            Me.cursorPositionLabel.Location = New Point(spliterCursor.Location.X + CURSOR_POSITION_LABEL_OFFSET.X, spliterCursor.Location.Y + CURSOR_POSITION_LABEL_OFFSET.Y)
            Me.okButton.Enabled = True

            updateLabels()
        End Sub

        Private Sub updateLabels()

            Me.cursorPositionLabel.Text = Me._splitTime.ToString("HH:mm")

            Me.delayStartValueLabel1.Text = Me.startTime.ToString("HH:mm")
            Me.delayStartValueLabel2.Text = Me._splitTime.ToString("HH:mm")

            Me.delayEndValueLabel1.Text = Me._splitTime.ToString("HH:mm")
            Me.delayEndValueLabel2.Text = Me.endTime.ToString("HH:mm")

            Me.delayDurationValueLabel1.Text = Me._splitTime.Subtract(Me.startTime).ToString("h\hmm")
            Me.delayDurationValueLabel2.Text = Me.endTime.Subtract(Me._splitTime).ToString("h\hmm")
        End Sub

        Public ReadOnly Property SplitTime As Date
            Get
                Return Me._splitTime
            End Get
        End Property

        Private mouseIsDown As Boolean
        Private mouseStart As Point
        Private cursorStart As Point

        Private Sub _onMouseDownOnCursor(o As Object, e As MouseEventArgs) Handles spliterCursor.MouseDown

            mouseIsDown = True
            mouseStart = e.Location
            cursorStart = Me.spliterCursor.Location

            Me.spliterCursor.Cursor = Constants.UI.Images.Cursors.CLOSE_HAND
        End Sub

        Private Sub _onMouseUpOnCursor() Handles spliterCursor.MouseUp

            mouseIsDown = False
            mouseStart = Nothing
            cursorStart = Nothing

            Me.okButton.Enabled = (Me.SplitTime.Subtract(Me.startTime).TotalMinutes >= 1 AndAlso Me.endTime.Subtract(Me.SplitTime).TotalMinutes >= 1)

            Me.spliterCursor.Cursor = Constants.UI.Images.Cursors.OPEN_HAND
        End Sub

        Private Sub dragCursorLocation(o As Object, e As MouseEventArgs) Handles spliterCursor.MouseMove

            If (mouseIsDown AndAlso Not IsNothing(mouseStart) AndAlso Not IsNothing(cursorStart)) Then

                Dim xOffset As Integer = e.Location.X - mouseStart.X

                If (spliterPanel.Location.X <= spliterCursor.Location.X + xOffset AndAlso spliterPanel.Location.X + spliterPanel.Width >= spliterCursor.Location.X + spliterCursor.Width + xOffset) Then

                    cursorStart.Offset(xOffset, 0)

                    Me.spliterCursor.Location = cursorStart
                    Me.cursorPositionLabel.Location = New Point(spliterCursor.Location.X + CURSOR_POSITION_LABEL_OFFSET.X, spliterCursor.Location.Y + CURSOR_POSITION_LABEL_OFFSET.Y)

                    Dim ratio As Double = (spliterCursor.Location.X - spliterPanel.Location.X) / (spliterPanel.Width - spliterCursor.Width)

                    Me._splitTime = startTime.Add(TimeSpan.FromMinutes(durationInMinutes * ratio))

                    Me.updateLabels()
                End If
            End If
        End Sub

        Private Sub _onEnterEscape(sender As Object, e As PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown

            If (e.KeyCode = Keys.Enter AndAlso okButton.Enabled) Then

                Me.raiseCloseEvent(ClosingStatus.Ok)

            ElseIf (e.KeyCode = Keys.Escape) Then

                Me.raiseCloseEvent(ClosingStatus.Cancel)
            End If
        End Sub

        Private Sub ok() Handles okButton.Click
            Me.raiseCloseEvent(ClosingStatus.Ok)
        End Sub

        Private Sub cancel() Handles cancelButton.Click
            Me.raiseCloseEvent(ClosingStatus.Cancel)
        End Sub

    End Class
End Namespace
