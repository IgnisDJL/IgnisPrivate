Namespace Commands.Settings

    Public Class AddMDBHotFeed
        Inherits SettingsCommand

        Private newIndex As Integer
        Private newLocation As String
        Private newMaterial As String
        Private newIsRecycled As Boolean
        Private newIsFiller As Boolean
        Private newIsAsphalt As Boolean

        Private newFeedInfo As FeedInfoNode

        Public Sub New(index As Integer, location As String, material As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

            Me.newIndex = index
            Me.newLocation = location
            Me.newMaterial = material
            Me.newIsRecycled = isRecycled
            Me.newIsFiller = isFiller
            Me.newIsAsphalt = isAsphalt

        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me.newFeedInfo)) Then
                Me.newFeedInfo = XmlSettings.Settings.instance.Usine.DataFiles.MDB.addHotFeedInfo(Me.newLocation, Me.newMaterial, Me.newIndex, Me.newIsRecycled, Me.newIsFiller, Me.newIsAsphalt)
            Else
                XmlSettings.Settings.instance.Usine.DataFiles.MDB.addHotFeedInfo(Me.newFeedInfo)
            End If

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.DataFiles.MDB.removeHotFeedInfo(Me.newFeedInfo)

        End Sub
    End Class
End Namespace

