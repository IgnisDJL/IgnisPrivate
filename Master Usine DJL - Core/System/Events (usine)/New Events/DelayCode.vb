Public Class DelayCode

    Private _code As String
    Private _description As String

    Public Sub New(code As String, descripton As String)
        Me._code = code
        Me._description = descripton
    End Sub

    Public Property Type As DelayType

    Public ReadOnly Property Code As String
        Get
            Return _code
        End Get
    End Property

    Public ReadOnly Property Description As String
        Get
            Return _description
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.Code & " - " & Me.Description
    End Function
End Class
