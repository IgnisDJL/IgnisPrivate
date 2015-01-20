Namespace Commands.Settings

    Public Class RemoveDefaultRecipient
        Inherits SettingsCommand

        Private _recipientAddress As String

        Public Sub New(recipientAddress As String)
            MyBase.New()

            Me._recipientAddress = recipientAddress

        End Sub

        Public Overrides Sub execute()

            For Each _recipientInfo As XmlSettings.EmailsNode.RecipientInfo In Me.Settings.Usine.EmailsInfo.DEFAULT_RECIPIENTS

                If (_recipientInfo.ADDRESS.Equals(Me._recipientAddress)) Then

                    Me.Settings.Usine.EmailsInfo.removeDefaultRecipientInfo(_recipientInfo)

                    Exit For
                End If

            Next

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.addDefaultRecipientInfo(Me._recipientAddress)

        End Sub

    End Class
End Namespace