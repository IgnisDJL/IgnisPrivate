Namespace Commands.Settings

    Public Class AddOperator
        Inherits SettingsCommand

        Private _newOperator As FactoryOperator

        Public Sub New(firstName As String, lastName As String)
            MyBase.New()

            Me._newOperator = New FactoryOperator(firstName, lastName)

        End Sub

        Public Overrides Sub execute()

            Me.Settings.Usine.OperatorsInfo.addOperatorInfo(Me._newOperator.FirstName, Me._newOperator.LastName)

        End Sub

        Public Overrides Sub undo()

            For Each _operatorInfo As XmlSettings.OperatorsNode.OperatorInfo In Me.Settings.Usine.OperatorsInfo.OPERATORS

                If (_operatorInfo.FIRST_NAME.Equals(_newOperator.FirstName) AndAlso _operatorInfo.LAST_NAME.Equals(_newOperator.LastName)) Then

                    XmlSettings.Settings.instance.Usine.OperatorsInfo.removeOperator(_operatorInfo)

                    Exit For
                End If

            Next

        End Sub

    End Class
End Namespace
