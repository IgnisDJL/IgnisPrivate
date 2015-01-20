Public Class IncorrectDataException
    Inherits MasterUsineException

    Private newValue As Object
    Private oldValue As Object

    Public Sub New(newValue As Object, oldValue As Object)
        MyBase.New(New Exception("La valeur '" & newValue & "' est incorrecte")) ' #refactor from settings.language

        Me.newValue = newValue
        Me.oldValue = oldValue

    End Sub

    Public ReadOnly Property NEW_VALUE As Object
        Get
            Return Me.newValue
        End Get
    End Property

    Public ReadOnly Property OLD_VALUE As Object
        Get
            Return oldValue
        End Get
    End Property

End Class
