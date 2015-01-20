Public Class TemporaryExceptionHandler
    Implements ExceptionHandler

    Private Shared instance As TemporaryExceptionHandler = New TemporaryExceptionHandler

    Private Sub New()

    End Sub

    Public Sub handle(exception As Exception) Implements ExceptionHandler.handle
        Console.Error.WriteLine("EXCEPTION THROWN : " & exception.Message.ToString)
    End Sub

    Public Sub writeErrorMsg(msg As String)
        Console.Error.WriteLine(msg)
    End Sub


    Public Shared Function getInstance() As TemporaryExceptionHandler
        Return instance
    End Function


End Class
