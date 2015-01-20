Namespace Commands.Settings

    Public Class AddMDBColdFeed
        Inherits SettingsCommand

        Private newIndex As Integer
        Private newLocation As String
        Private newMaterial As String
        Private newIsRecycled As Boolean

        Private newFeedInfo As FeedInfoNode

        Public Sub New(index As Integer, location As String, material As String, isRecycled As Boolean)

            Me.newIndex = index
            Me.newLocation = location
            Me.newMaterial = material
            Me.newIsRecycled = isRecycled

        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me.newFeedInfo)) Then
                Me.newFeedInfo = XmlSettings.Settings.instance.Usine.DataFiles.MDB.addColdFeedInfo(Me.newLocation, Me.newMaterial, Me.newIndex, Me.newIsRecycled)
            Else
                XmlSettings.Settings.instance.Usine.DataFiles.MDB.addColdFeedInfo(Me.newFeedInfo)
            End If

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.DataFiles.MDB.removeColdFeedInfo(Me.newFeedInfo)

        End Sub
    End Class
End Namespace

