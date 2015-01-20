Namespace Commands.Settings

    Public Class RemoveCSVHotFeed
        Inherits SettingsCommand

        Private feedToRemove As FeedInfoNode

        Public Sub New(feedToRemove As FeedInfoNode)

            Me.feedToRemove = feedToRemove
        End Sub

        Public Overrides Sub execute()

            XmlSettings.Settings.instance.Usine.DataFiles.CSV.removeHotFeedInfo(Me.feedToRemove)

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.DataFiles.CSV.addHotFeedInfo(Me.feedToRemove)

        End Sub
    End Class
End Namespace

