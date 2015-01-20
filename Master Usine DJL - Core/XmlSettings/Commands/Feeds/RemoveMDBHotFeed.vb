Namespace Commands.Settings

    Public Class RemoveMDBHotFeed
        Inherits SettingsCommand

        Private feedToRemove As FeedInfoNode

        Public Sub New(feedToRemove As FeedInfoNode)

            Me.feedToRemove = feedToRemove
        End Sub

        Public Overrides Sub execute()

            XmlSettings.Settings.instance.Usine.DataFiles.MDB.removeHotFeedInfo(Me.feedToRemove)

        End Sub

        Public Overrides Sub undo()

            Me.feedToRemove = XmlSettings.Settings.instance.Usine.DataFiles.MDB.addHotFeedInfo(Me.feedToRemove)
        End Sub
    End Class
End Namespace

