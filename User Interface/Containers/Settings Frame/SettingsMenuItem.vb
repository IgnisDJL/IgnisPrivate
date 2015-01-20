Namespace UI

    Public Class SettingsMenuItem
        Inherits Panel

        ' Constants


        ' Components
        Private WithEvents iconPanel As Panel

        Private WithEvents nameLabel As Label

        ' --- ToolTip?

        ' Attributes
        Private labelText As String
        Private icon As Image

        Private _isSelected As Boolean

        ' Events
        Public Event Clicked()


        Public Sub New(text As String, Optional icon As Image = Nothing)

            Me.labelText = text
            Me.icon = icon
            Me._isSelected = False

            Me.initializeComponents()
        End Sub

        Private Sub initializeComponents()

            Me.BackColor = Color.FromArgb(255, 0, 70, 222) ' #0046DE

            Me.nameLabel = New Label
            Me.nameLabel.Font = Constants.UI.Fonts.DEFAULT_FONT
            Me.nameLabel.Text = labelText
            Me.nameLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.nameLabel.ForeColor = Color.White
            Me.nameLabel.Cursor = Cursors.Hand

            If (Not IsNothing(Me.icon)) Then
                Me.iconPanel = New Panel
                Me.iconPanel.Cursor = Cursors.Hand
                Me.Controls.Add(iconPanel)
            End If

            Me.Controls.Add(nameLabel)

        End Sub

        Public Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.nameLabel.Location = New Point(0, 1)
            Me.nameLabel.Size = New Size(newSize.Width, newSize.Height - 2)

        End Sub

        Public Property IsSelected As Boolean
            Get
                Return Me._isSelected
            End Get
            Set(isSelected As Boolean)

                If (isSelected) Then
                    Me.nameLabel.Font = Constants.UI.Fonts.DEFAULT_FONT_BOLD
                Else
                    Me.nameLabel.Font = Constants.UI.Fonts.DEFAULT_FONT
                End If

                Me._isSelected = isSelected
            End Set
        End Property

        Private Sub _onClick() Handles iconPanel.Click, nameLabel.Click

            RaiseEvent Clicked()
        End Sub

        Private Sub _onMouseEnter() Handles nameLabel.MouseEnter, iconPanel.MouseEnter

            If (Not Me._isSelected) Then
                Me.nameLabel.Font = Constants.UI.Fonts.DEFAULT_FONT_UNDERLINED
            End If

        End Sub

        Private Sub _onMouseLeave() Handles nameLabel.MouseLeave, iconPanel.MouseLeave

            If (Not Me._isSelected) Then
                Me.nameLabel.Font = Constants.UI.Fonts.DEFAULT_FONT
            End If
        End Sub


        Private Sub paintBorders(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

            ControlPaint.DrawBorder(e.Graphics, DirectCast(sender, Panel).ClientRectangle, _
                                    Color.White, 0, ButtonBorderStyle.Solid, _
                                    Color.White, 0, ButtonBorderStyle.Solid, _
                                    Color.White, 0, ButtonBorderStyle.Solid, _
                                    Color.White, 1, ButtonBorderStyle.Solid)
        End Sub

    End Class
End Namespace
