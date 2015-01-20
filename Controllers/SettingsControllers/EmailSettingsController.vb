Imports IGNIS.Commands.Settings

Public Class EmailSettingsController
    Inherits SettingsController

    Private emailsSettings As XmlSettings.EmailsNode

    Public Sub New()
        MyBase.New()

        Me.emailsSettings = XmlSettings.Settings.instance.Usine.EmailsInfo

    End Sub

    Public Sub setRecipientSelection(address As String, isSelected As Boolean)

        For Each _recipientInfo As XmlSettings.EmailsNode.RecipientInfo In Me.emailsSettings.RECIPIENTS

            If (_recipientInfo.ADDRESS.Equals(address)) Then

                _recipientInfo.SELECTED = isSelected

                Exit For
            End If
        Next

        XmlSettings.Settings.instance.save()

    End Sub

    Public ReadOnly Property Recipients As List(Of EmailRecipient)
        Get

            Dim _recipients As New List(Of EmailRecipient)

            For Each _recipientInfo In Me.emailsSettings.RECIPIENTS
                _recipients.Add(New EmailRecipient(_recipientInfo.ADDRESS, _recipientInfo.SELECTED))
            Next

            Return _recipients
        End Get
    End Property

    Public ReadOnly Property DefaultRecipients As List(Of EmailRecipient)
        Get

            Dim _recipients As New List(Of EmailRecipient)

            For Each _recipientInfo In Me.emailsSettings.DEFAULT_RECIPIENTS
                _recipients.Add(New EmailRecipient(_recipientInfo.ADDRESS))
            Next

            Return _recipients
        End Get
    End Property

    Public Property Credentials As String
        Get
            Return Me.emailsSettings.CREDENTIALS
        End Get
        Set(value As String)

            Me.executeCommand(New SetCredentials(value))

        End Set
    End Property

    Public Property Password As String
        Get
            Return Me.emailsSettings.PASSWORD
        End Get
        Set(value As String)

            Me.executeCommand(New SetPassword(value))

        End Set
    End Property

    Public Property Host As String
        Get
            Return Me.emailsSettings.HOST
        End Get
        Set(value As String)

            Me.executeCommand(New SetHost(value))

        End Set
    End Property

    Public Property Port As String
        Get
            Return Me.emailsSettings.PORT
        End Get
        Set(value As String)

            Me.executeCommand(New SetPort(value))

        End Set
    End Property

    Public Sub addRecipient(newAddress As String)

        Me.executeCommand(New AddRecipient(newAddress))

    End Sub

    Public Sub removeRecipient(recipient As EmailRecipient)

        Me.executeCommand(New RemoveRecipient(recipient.Address))

    End Sub

    Public Sub updateRecipient(recipient As EmailRecipient, newAddress As String)

        For Each _recipientInfo As XmlSettings.EmailsNode.RecipientInfo In Me.emailsSettings.RECIPIENTS

            If (_recipientInfo.ADDRESS.Equals(recipient.Address)) Then

                Me.executeCommand(New UpdateRecipient(_recipientInfo, newAddress))

                Exit For
            End If
        Next
    End Sub

    Public Sub addDefaultRecipient(newAddress As String)

        Me.executeCommand(New AddDefaultRecipient(newAddress))

    End Sub

    Public Sub removeDefaultRecipient(recipient As EmailRecipient)

        Me.executeCommand(New RemoveDefaultRecipient(recipient.Address))

    End Sub

    Public Sub updateDefaultRecipient(recipient As EmailRecipient, newAddress As String)

        For Each _recipientInfo As XmlSettings.EmailsNode.RecipientInfo In Me.emailsSettings.DEFAULT_RECIPIENTS

            If (_recipientInfo.ADDRESS.Equals(recipient.Address)) Then

                Me.executeCommand(New UpdateRecipient(_recipientInfo, newAddress))

                Exit For
            End If
        Next
    End Sub

End Class
