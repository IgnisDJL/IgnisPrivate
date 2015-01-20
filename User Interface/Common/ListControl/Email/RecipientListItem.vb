
Namespace UI.Common

    Public MustInherit Class RecipientListItem
        Inherits Common.ListItem(Of EmailRecipient)

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5

        ' Components
        Protected WithEvents addressLabel As Label

        Public Sub New(recipient As EmailRecipient)
            MyBase.New(recipient)

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, addressLabel.Click

            raiseClickEvent()

        End Sub

    End Class
End Namespace
