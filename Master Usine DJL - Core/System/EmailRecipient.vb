Public Class EmailRecipient

    Private _address As String

    Public Sub New(address As String)
        Me._address = address
    End Sub

    Public Sub New(address As String, selected As Boolean)
        Me._address = address
        Me.Selected = selected
    End Sub

    Public ReadOnly Property Address As String
        Get
            Return Me._address
        End Get
    End Property

    Public Property Selected As Boolean = False
End Class
