Namespace Commands.Settings

    Public Class RemoveOperator
        Inherits SettingsCommand

        Private _operatorToRemove As FactoryOperator

        Public Sub New(operatorToRemove As FactoryOperator)
            MyBase.New()

            Me._operatorToRemove = operatorToRemove

        End Sub

        Public Overrides Sub execute()

            For Each _operatorInfo As XmlSettings.OperatorsNode.OperatorInfo In Me.Settings.Usine.OperatorsInfo.OPERATORS

                If (_operatorInfo.FIRST_NAME.Equals(Me._operatorToRemove.FirstName) AndAlso _operatorInfo.LAST_NAME.Equals(Me._operatorToRemove.LastName)) Then

                    XmlSettings.Settings.instance.Usine.OperatorsInfo.removeOperator(_operatorInfo)

                    Exit For
                End If

            Next

        End Sub

        Public Overrides Sub undo()

            Me.Settings.Usine.OperatorsInfo.addOperatorInfo(Me._operatorToRemove.FirstName, Me._operatorToRemove.LastName)

        End Sub

    End Class
End Namespace