Public Class Chrono

    Private Shared startTime As Date
    Private Shared lastTime As Date

    Public Shared Sub start()
        startTime = DateTime.Now
        Console.WriteLine("Chrono started : 0ms")
        lastTime = startTime
    End Sub

    Public Shared Sub log(text As String)
        Dim now = DateTime.Now
        Console.WriteLine(text & " : " & now.Subtract(lastTime).TotalMilliseconds & "ms - " & now.Subtract(startTime).TotalMilliseconds & "ms")
        lastTime = now
    End Sub

    Public Shared Sub _stop()
        Dim now = DateTime.Now
        Console.WriteLine("Chrono stopped : " & now.Subtract(startTime).TotalMilliseconds & "ms")
        lastTime = now
    End Sub

End Class
