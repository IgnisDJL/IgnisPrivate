Namespace Commands.Settings

    Public Class AddDefaultRecipient
        Inherits SettingsCommand

        Private _newRecipient As String

        Private _recipientInfo As XmlSettings.EmailsNode.RecipientInfo

        Public Sub New(address As String)
            MyBase.New()

            Me._newRecipient = address

        End Sub

        Public Overrides Sub execute()

            Me._recipientInfo = Me.Settings.Usine.EmailsInfo.addDefaultRecipientInfo(Me._newRecipient)

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.EmailsInfo.removeDefaultRecipientInfo(Me._recipientInfo)

        End Sub

    End Class
End Namespace

