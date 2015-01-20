Namespace UI.Common

    Public Class NextButton
        Inherits Button

        Public Shared ReadOnly BUTTON_TEXT As String = "Suivant   "


        Public Sub New()
            MyBase.New()

            Me.ImageAlign = ContentAlignment.MiddleRight
            Me.Text = BUTTON_TEXT

        End Sub

        Public Sub changeImage() Handles Me.Resize

            If (Me.Size.Height > 64) Then
                Me.Image = Constants.UI.Images._64x64.NEXT_
            ElseIf (Me.Size.Height > 32) Then
                Me.Image = Constants.UI.Images._32x32.NEXT_
            ElseIf (Me.Size.Height > 24) Then
                Me.Image = Constants.UI.Images._24x24.NEXT_
            ElseIf (Me.Size.Height > 16) Then
                Me.Image = Constants.UI.Images._16x16.NEXT_
            End If

        End Sub

    End Class

End Namespace
