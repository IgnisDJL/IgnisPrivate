Namespace UI

    Public Class SplashScreen
        Inherits Form

        Public Shared instance As SplashScreen

        Private messageLabel As Label

        Public Sub New()

            instance = Me

            Me.initializeComponents()
        End Sub

        Public Sub initializeComponents()

            Me.StartPosition = FormStartPosition.CenterScreen

            Me.Visible = False
            Me.SuspendLayout()

            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None

            Me.Size = New Size(Constants.UI.Images.SplashScreen.SPLASH_SCREEN.Size.Width, _
                               Constants.UI.Images.SplashScreen.SPLASH_SCREEN.Size.Height + _
                               Constants.UI.Fonts.DEFAULT_FONT.Height + 4)

            ' Background
            Me.BackgroundImage = Constants.UI.Images.SplashScreen.SPLASH_SCREEN

            ' Label
            Me.messageLabel = New Label
            Me.messageLabel.ForeColor = Color.Black
            Me.messageLabel.Font = Constants.UI.Fonts.DEFAULT_FONT
            Me.messageLabel.AutoSize = False
            Me.messageLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.messageLabel.Size = New Size(Me.Size.Width, Constants.UI.Fonts.DEFAULT_FONT.Height + 4)
            Me.messageLabel.Location = New Point(0, Me.Size.Height - Me.messageLabel.Height)
            Me.messageLabel.BackColor = Color.LightGray

            Me.Controls.Add(messageLabel)

            Me.ResumeLayout(False)
        End Sub

        Public WriteOnly Property Message As String
            Set(value As String)
                Me.messageLabel.Text = value
                Me.messageLabel.Refresh()
            End Set
        End Property


    End Class
End Namespace
