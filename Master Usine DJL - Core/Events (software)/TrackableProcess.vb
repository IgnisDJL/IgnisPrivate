Namespace Eventing

    ' #refactor - change namespace name to IGNIS.Events
    Public Interface TrackableProcess

        Event ProcessStarting(sender As Object)
        Event ProcessComplete(sender As Object)
        Event CurrentProgress(progressPercentage)
        Event ProcessInterrupted(sender As Object, exception As Exception)

    End Interface
End Namespace
