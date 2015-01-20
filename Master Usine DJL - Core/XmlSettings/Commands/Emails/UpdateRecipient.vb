Namespace Commands.Settings

    Public Class UpdateRecipient
        Inherits SettingsCommand

        Private _newAddress As String
        Private _newIsSelected As Boolean
        Private isSelectedWasSet As Boolean = False

        Private _oldAddress As String
        Private _oldIsSelected As Boolean

        Private _recipientInfo As XmlSettings.EmailsNode.RecipientInfo

        Public Sub New(recipientInfo As XmlSettings.EmailsNode.RecipientInfo, newAddress As String)
            MyBase.New()

            Me._recipientInfo = recipientInfo

            Me._oldAddress = recipientInfo.ADDRESS

            Me._newAddress = newAddress

            Me.isSelectedWasSet = False
        End Sub

        Public Sub New(recipientInfo As XmlSettings.EmailsNode.RecipientInfo, newAddress As String, newIsSelected As Boolean)
            MyBase.New()

            Me._recipientInfo = recipientInfo

            Me._oldAddress = recipientInfo.ADDRESS
            Me._oldIsSelected = recipientInfo.SELECTED

            Me._newAddress = newAddress
            Me._newIsSelected = newIsSelected

            Me.isSelectedWasSet = True

        End Sub

        Public Overrides Sub execute()

            Me._recipientInfo.ADDRESS = Me._newAddress

            If (isSelectedWasSet) Then
                Me._recipientInfo.SELECTED = Me._newIsSelected
            End If

        End Sub

        Public Overrides Sub undo()

            Me._recipientInfo.ADDRESS = Me._oldAddress

            If (isSelectedWasSet) Then
                Me._recipientInfo.SELECTED = Me._oldIsSelected
            End If

        End Sub

    End Class
End Namespace