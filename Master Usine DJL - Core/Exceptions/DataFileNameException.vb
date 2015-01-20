Public Class DataFileNameException
    Inherits Exception

    Public Sub New(fileName As String, fileExtension As String)

        MyBase.New("'" & fileName & "' is not the correct format for a " & fileExtension & " file name.")

    End Sub

    Public Sub New(ByVal message As String, ByVal inner As Exception)
        MyBase.New(message, inner)
        ' Add other code for custom properties here.
    End Sub

End Class
