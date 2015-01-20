Imports IGNIS.Commands.Settings

Public Class UsineSettingsController
    Inherits SettingsController

    Private usineSettings As XmlSettings.UsineNode

    Public Sub New()
        MyBase.New()

        Me.usineSettings = XmlSettings.Settings.instance.Usine

    End Sub

    Public Property UsineName As String
        Get
            Return Me.usineSettings.PLANT_NAME
        End Get
        Set(value As String)
            Me.executeCommand(New SetUsineName(value))
        End Set
    End Property

    Public Property UsineID As String
        Get
            Return Me.usineSettings.PLANT_ID
        End Get
        Set(value As String)
            Me.executeCommand(New SetUsineID(value))
        End Set
    End Property

    Public Property UsineType As Constants.Settings.UsineType
        Get
            Return Me.usineSettings.TYPE
        End Get
        Set(value As Constants.Settings.UsineType)
            Me.executeCommand(New SetUsineType(value))
        End Set
    End Property

    Public Sub addNewOperator(firstName As String, lastName As String)
        Me.executeCommand(New AddOperator(firstName, lastName))
    End Sub

    Public Sub removeOperator(_operator As FactoryOperator)
        Me.executeCommand(New RemoveOperator(_operator))
    End Sub

    Public Sub updateOperator(_operator As FactoryOperator, newFirstName As String, newLastName As String)
        Me.executeCommand(New UpdateOperator(_operator, newFirstName, newLastName))
    End Sub

    Public Function getOperators() As List(Of FactoryOperator)

        Dim operatorList As New List(Of FactoryOperator)

        For Each _operatorNode As XmlSettings.OperatorsNode.OperatorInfo In Me.usineSettings.OperatorsInfo.OPERATORS

            operatorList.Add(New FactoryOperator(_operatorNode.FIRST_NAME, _operatorNode.LAST_NAME))

        Next

        Return operatorList
    End Function

    Public Sub updateFuelInformation(newFuel1Name As String, newFuel1Unit As String, newFuel2Name As String, newFuel2Unit As String)
        Me.executeCommand(New UpdateFuelInformation(newFuel1Name, newFuel1Unit, newFuel2Name, newFuel2Unit))
    End Sub

    Public Function getFuel1Name() As String
        Return Me.usineSettings.FuelsInfo.FUEL_1_NAME
    End Function

    Public Function getFuel1Unit() As String
        Return Me.usineSettings.FuelsInfo.FUEL_1_UNIT
    End Function

    Public Function getFuel2Name() As String
        Return Me.usineSettings.FuelsInfo.FUEL_2_NAME
    End Function

    Public Function getFuel2Unit() As String
        Return Me.usineSettings.FuelsInfo.FUEL_2_UNIT
    End Function

End Class
