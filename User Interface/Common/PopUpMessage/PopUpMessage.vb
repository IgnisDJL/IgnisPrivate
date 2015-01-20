Namespace UI.Common

    Public MustInherit Class PopUpMessage
        Inherits Panel

        ' Constantes
        Public Shared ReadOnly BACK_COLOR As Color = Color.FromArgb(0, 0, 0)
        Public Shared ReadOnly FORE_COLOR As Color = Color.White

        Private Shared ReadOnly CLOSE_BUTTON_SIZE_UP As Size = New Size(30, 20)
        Private Shared ReadOnly CLOSE_BUTTON_SIZE_DOWN As Size = New Size(28, 18)

        ' Components
        Protected WithEvents closeButton As Panel

        ' Attributes
        Private _showCloseButton As Boolean = True

        ' Events
        Public Event CloseEvent(status As ClosingStatus)

        Public Enum ClosingStatus As Integer
            Ok = 1
            Cancel = 2
        End Enum

        Protected Overridable Sub initializeComponents()

            Me.BackColor = BACK_COLOR
            Me.ForeColor = FORE_COLOR

            Me.closeButton = New Panel
            Me.closeButton.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
            Me.closeButton.BackColor = Color.White
            Me.closeButton.Size = CLOSE_BUTTON_SIZE_UP
            Me.closeButton.BackgroundImageLayout = ImageLayout.Center
            Me.closeButton.BackgroundImage = Constants.UI.Images._16x16.DELETE
            Me.Controls.Add(closeButton)

        End Sub

        Public Overridable Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.closeButton.Location = New Point(Me.Width - closeButton.Width - 1, 1)

            Me.ajustLayout()
        End Sub

        Protected MustOverride Sub ajustLayout()

        Protected Sub raiseCloseEvent(status As ClosingStatus)
            RaiseEvent CloseEvent(status)
        End Sub

        Public Sub onCloseButtonDown() Handles closeButton.MouseDown
            Me.closeButton.Size = CLOSE_BUTTON_SIZE_DOWN
            Me.closeButton.Location = New Point(Me.Width - closeButton.Width - 2, 2)
            Me.closeButton.BackColor = BACK_COLOR
        End Sub

        Public Sub onCloseButtonUp() Handles closeButton.MouseUp
            Me.closeButton.Size = CLOSE_BUTTON_SIZE_UP
            Me.closeButton.Location = New Point(Me.Width - closeButton.Width - 1, 1)
            Me.closeButton.BackColor = Color.White
        End Sub

        Public Sub onClose() Handles closeButton.Click
            Me.raiseCloseEvent(ClosingStatus.Cancel)
        End Sub

        Public Property ShowCloseButton As Boolean
            Get
                Return Me._showCloseButton
            End Get
            Set(value As Boolean)

                Me._showCloseButton = value

                If (value) Then

                    Me.Controls.Add(Me.closeButton)
                Else
                    Me.Controls.Remove(Me.closeButton)
                End If
            End Set
        End Property

        Public Property IsDraggable As Boolean = True
        Private mouseIsDown As Boolean = False
        Private mouseStart As Point = Nothing
        Private panelStart As Point = Nothing

        Protected Sub _onMouseDown(o As Object, e As MouseEventArgs) Handles Me.MouseDown

            mouseIsDown = True
            mouseStart = e.Location
            panelStart = Me.Location
        End Sub

        Protected Sub _onMouseUp() Handles Me.MouseUp

            mouseIsDown = False
            mouseStart = Nothing
            panelStart = Nothing
        End Sub

        Protected Sub dragLocation(o As Object, e As MouseEventArgs) Handles Me.MouseMove

            If (Me.IsDraggable) Then

                If (mouseIsDown AndAlso Not IsNothing(mouseStart) AndAlso Not IsNothing(panelStart)) Then

                    If (Me.Parent.ClientRectangle.Contains(e.X + Me.Location.X, e.Y + Me.Location.Y)) Then

                        panelStart.Offset(e.Location.X - mouseStart.X, e.Location.Y - mouseStart.Y)

                        Me.Location = panelStart

                    End If
                End If
            End If
        End Sub

    End Class
End Namespace
