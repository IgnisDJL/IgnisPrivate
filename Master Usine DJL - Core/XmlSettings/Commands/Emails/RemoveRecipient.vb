Namespace Commands.Settings

    Public Class RemoveRecipient
        Inherits SettingsCommand

        Private _recipientAddress As String
        Private _recipientIsSelected As Boolean = False

        Public Sub New(recipientAddress As String)
            MyBase.New()

            Me._recipientAddress = recipientAddress

        End Sub

        Public Overrides Sub execute()

            For Each _recipientInfo As XmlSettings.EmailsNode.RecipientInfo In Me.Settings.Usine.EmailsInfo.RECIPIENTS

                If (_recipientInfo.ADDRESS.Equals(Me._recipientAddress)) Then

                    Me._recipientIsSelected = Me.Settings.Usine.EmailsInfo.removeRecipientInfo(_recipientInfo).SELECTED

                    Exit For
                End If

            Next

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.addRecipientInfo(Me._recipientAddress).SELECTED = Me._recipientIsSelected

        End Sub

    End Class
End Namespace