Namespace Commands.Settings

    Public Class UpdateOperator
        Inherits SettingsCommand

        Private _newFirstName As String
        Private _newLastName As String
        Private _unchangedOperator As FactoryOperator

        Public Sub New(_operatorToUpdate As FactoryOperator, newFirstName As String, newLastName As String)
            MyBase.New()

            Me._newFirstName = newFirstName
            Me._newLastName = newLastName
            Me._unchangedOperator = _operatorToUpdate


        End Sub

        Public Overrides Sub execute()

            For Each _operatorInfo As XmlSettings.OperatorsNode.OperatorInfo In Me.Settings.Usine.OperatorsInfo.OPERATORS

                If (_operatorInfo.FIRST_NAME.Equals(Me._unchangedOperator.FirstName) AndAlso _operatorInfo.LAST_NAME.Equals(Me._unchangedOperator.LastName)) Then

                    _operatorInfo.FIRST_NAME = _newFirstName
                    _operatorInfo.LAST_NAME = _newLastName

                    Exit For
                End If
            Next

        End Sub

        Public Overrides Sub undo()

            For Each _operatorInfo As XmlSettings.OperatorsNode.OperatorInfo In Me.Settings.Usine.OperatorsInfo.OPERATORS

                If (_operatorInfo.FIRST_NAME.Equals(_newFirstName) AndAlso _operatorInfo.LAST_NAME.Equals(_newLastName)) Then

                    _operatorInfo.FIRST_NAME = Me._unchangedOperator.FirstName
                    _operatorInfo.LAST_NAME = Me._unchangedOperator.LastName

                    Exit For
                End If
            Next

        End Sub

    End Class
End Namespace