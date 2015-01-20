Namespace Commands.Settings

    Public Class UpdateDelayCode
        Inherits SettingsCommand

        Private newCode As String
        Private newDescription As String
        Private newType As DelayType

        Private oldCode As String
        Private oldDescription As String
        Private oldType As DelayType

        Private delayCode As DelayCode

        Private delayCodeNode As XmlSettings.DelaysNode.DelayCodeNode
        Private newDelayTypeNode As XmlSettings.DelaysNode.DelayTypeNode
        Private oldDelayTypeNode As XmlSettings.DelaysNode.DelayTypeNode

        Public Sub New(delayCode As DelayCode, newCode As String, newDescription As String, newType As DelayType)

            Me.newCode = newCode
            Me.newDescription = newDescription
            Me.newType = newType

            Me.delayCode = delayCode

            Me.oldCode = delayCode.Code
            Me.oldDescription = delayCode.Description
            Me.oldType = delayCode.Type

            For Each delayTypeNode As XmlSettings.DelaysNode.DelayTypeNode In XmlSettings.Settings.instance.Usine.Events.Delays.DELAY_TYPES

                If (newType.Name = delayTypeNode.NAME) Then
                    Me.newDelayTypeNode = delayTypeNode
                End If

                If (oldType.Name = delayTypeNode.NAME) Then

                    Me.oldDelayTypeNode = delayTypeNode

                    For Each _delayCodeNode As XmlSettings.DelaysNode.DelayCodeNode In delayTypeNode.DELAY_CODES

                        If (_delayCodeNode.CODE = delayCode.Code) Then
                            Me.delayCodeNode = _delayCodeNode
                        End If
                    Next
                End If
            Next
        End Sub

        Public Overrides Sub execute()

            Me.delayCode = New DelayCode(newCode, newDescription)

            Me.delayCodeNode.CODE = newCode
            Me.delayCodeNode.DESCRIPTION = newDescription

            If (Not Me.newType = Me.oldType) Then

                Me.oldDelayTypeNode.removeDelay(Me.delayCodeNode)
                Me.newDelayTypeNode.addDelay(Me.delayCodeNode)

            End If

        End Sub

        Public Overrides Sub undo()

            Me.delayCode = New DelayCode(oldCode, oldDescription)

            Me.delayCodeNode.CODE = oldCode
            Me.delayCodeNode.DESCRIPTION = oldDescription

            If (Not Me.newType = Me.oldType) Then

                Me.newDelayTypeNode.removeDelay(Me.delayCodeNode)
                Me.oldDelayTypeNode.addDelay(Me.delayCodeNode)

            End If
        End Sub
    End Class
End Namespace

