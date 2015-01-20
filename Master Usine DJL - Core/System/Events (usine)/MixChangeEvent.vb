Public Class MixChangeEvent
    Inherits SingleEvent

    Public Sub New(time As Date, message As String, Optional outputMessage As String = Nothing)
        MyBase.New(time, message, outputMessage)

    End Sub

End Class
