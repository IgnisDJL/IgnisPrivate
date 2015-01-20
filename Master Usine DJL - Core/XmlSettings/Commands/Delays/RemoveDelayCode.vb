Namespace Commands.Settings

    Public Class RemoveDelayCode
        Inherits SettingsCommand

        Private codeToRemove As DelayCode

        Private typeNode As XmlSettings.DelaysNode.DelayTypeNode
        Private codeNodeToRemove As XmlSettings.DelaysNode.DelayCodeNode

        Public Sub New(codeToRemove As DelayCode)

            Me.codeToRemove = codeToRemove

            For Each typeNode As XmlSettings.DelaysNode.DelayTypeNode In XmlSettings.Settings.instance.Usine.Events.Delays.DELAY_TYPES

                If (Me.codeToRemove.Type.Name = typeNode.NAME) Then

                    Me.typeNode = typeNode

                    For Each codeNode As XmlSettings.DelaysNode.DelayCodeNode In typeNode.DELAY_CODES

                        If (Me.codeToRemove.Code.Equals(codeNode.CODE)) Then

                            Me.codeNodeToRemove = codeNode
                        End If

                    Next

                End If
            Next
        End Sub

        Public Overrides Sub execute()

            Me.typeNode.removeDelay(codeNodeToRemove)

        End Sub

        Public Overrides Sub undo()

            Me.typeNode.addDelay(codeNodeToRemove)

        End Sub
    End Class
End Namespace

