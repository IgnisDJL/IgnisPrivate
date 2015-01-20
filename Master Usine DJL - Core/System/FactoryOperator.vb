Public Class FactoryOperator

    Public Shared ReadOnly DEFAULT_OPERATOR As FactoryOperator = New FactoryOperator("Anonyme", Nothing)

    Private _firstName As String
    Private _lastName As String

    Public Sub New(firstName As String, lastName As String)

        Me._firstName = firstName
        Me._lastName = lastName

    End Sub

    Public ReadOnly Property FirstName As String
        Get
            Return Me._firstName
        End Get
    End Property

    Public ReadOnly Property LastName As String
        Get
            Return If(IsNothing(Me._lastName), "", Me._lastName)
        End Get
    End Property

    Public ReadOnly Property EmailAddress As String
        Get
            Return FirstName & If(IsNothing(Me._lastName), "", "." & Me.LastName) & "@IGNIS_" & XmlSettings.Settings.instance.Usine.PLANT_NAME.Replace(" ", "") & ".ca"
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.FirstName & If(IsNothing(Me._lastName), "", " " & Me.LastName)
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        If (TypeOf obj Is FactoryOperator) Then
            Return DirectCast(obj, FactoryOperator).FirstName.Equals(Me.FirstName) AndAlso DirectCast(obj, FactoryOperator).LastName.Equals(Me.LastName)
        Else
            Return False
        End If
    End Function

    Public Shared Operator =(mine As FactoryOperator, his As Object)
        Return mine.Equals(his)
    End Operator

    Public Shared Operator <>(mine As FactoryOperator, his As Object)
        Return Not mine.Equals(his)
    End Operator

End Class
