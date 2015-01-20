Namespace UI.Common

    Public Class BackButton
        Inherits Button

        Public Shared ReadOnly BUTTON_TEXT As String = "  Retour"
        Public Shared ReadOnly BUTTON_WIDTH As Integer = 120

        Public Sub New()
            MyBase.New()

            Me.ImageAlign = ContentAlignment.MiddleLeft
            Me.Text = BUTTON_TEXT
        End Sub

        Public Sub changeImage() Handles Me.Resize

            If (Me.Size.Height > 64) Then
                Me.Image = Constants.UI.Images._64x64.PREVIOUS
            ElseIf (Me.Size.Height > 32) Then
                Me.Image = Constants.UI.Images._32x32.PREVIOUS
            ElseIf (Me.Size.Height > 24) Then
                Me.Image = Constants.UI.Images._24x24.PREVIOUS
            ElseIf (Me.Size.Height > 16) Then
                Me.Image = Constants.UI.Images._16x16.PREVIOUS
            End If

        End Sub

    End Class

End Namespace
