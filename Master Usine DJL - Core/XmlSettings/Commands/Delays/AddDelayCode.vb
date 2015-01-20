Namespace Commands.Settings

    Public Class AddDelayCode
        Inherits SettingsCommand

        Private code As String
        Private description As String
        Private type As DelayType

        Private newDelayCodeNode As XmlSettings.DelaysNode.DelayCodeNode

        Public Sub New(code As String, description As String, type As DelayType)

            Me.code = code
            Me.description = description
            Me.type = type

        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me.newDelayCodeNode)) Then

                For Each typeNode As XmlSettings.DelaysNode.DelayTypeNode In XmlSettings.Settings.instance.Usine.Events.Delays.DELAY_TYPES

                    If (Me.type.Name = typeNode.NAME) Then
                        Me.newDelayCodeNode = typeNode.addDelay(Me.code, Me.description)
                    End If
                Next
            Else

                For Each typeNode As XmlSettings.DelaysNode.DelayTypeNode In XmlSettings.Settings.instance.Usine.Events.Delays.DELAY_TYPES

                    If (Me.type.Name = typeNode.NAME) Then
                        typeNode.addDelay(Me.newDelayCodeNode)
                    End If
                Next
            End If

        End Sub

        Public Overrides Sub undo()

            For Each typeNode As XmlSettings.DelaysNode.DelayTypeNode In XmlSettings.Settings.instance.Usine.Events.Delays.DELAY_TYPES

                If (Me.type.Name = typeNode.NAME) Then
                    typeNode.removeDelay(Me.newDelayCodeNode)
                End If
            Next

        End Sub
    End Class
End Namespace

