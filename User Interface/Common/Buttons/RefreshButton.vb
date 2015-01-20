Namespace UI.Common

    Public Class RefreshButton
        Inherits Button

        Public Sub New()
            MyBase.New()

            Me.ImageAlign = ContentAlignment.MiddleLeft

        End Sub

        Public Sub changeImage() Handles Me.Resize

            If (Me.Size.Height > 64) Then
                Me.Image = Constants.UI.Images._64x64.REFRESH
            ElseIf (Me.Size.Height > 32) Then
                Me.Image = Constants.UI.Images._32x32.REFRESH
            ElseIf (Me.Size.Height > 24) Then
                Me.Image = Constants.UI.Images._24x24.REFRESH
            ElseIf (Me.Size.Height > 16) Then
                Me.Image = Constants.UI.Images._16x16.REFRESH
            End If

        End Sub

    End Class

End Namespace
