Namespace Commands.Settings

    Public Class AddCSVHotFeed
        Inherits SettingsCommand

        Private newIndex As Integer
        Private newLocation As String
        Private newMaterial As String
        Private newIsRecycled As Boolean
        Private newIsFiller As Boolean

        Private newFeedInfo As FeedInfoNode

        Public Sub New(index As Integer, location As String, material As String, isRecycled As Boolean, isFiller As Boolean)

            Me.newIndex = index
            Me.newLocation = location
            Me.newMaterial = material
            Me.newIsRecycled = isRecycled
            Me.newIsFiller = isFiller

        End Sub

        Public Overrides Sub execute()

            If (IsNothing(Me.newFeedInfo)) Then
                Me.newFeedInfo = XmlSettings.Settings.instance.Usine.DataFiles.CSV.addHotFeedInfo(Me.newLocation, Me.newMaterial, Me.newIndex, Me.newIsRecycled, Me.newIsFiller)
            Else
                XmlSettings.Settings.instance.Usine.DataFiles.CSV.addHotFeedInfo(Me.newFeedInfo)
            End If

        End Sub

        Public Overrides Sub undo()

            XmlSettings.Settings.instance.Usine.DataFiles.CSV.removeHotFeedInfo(Me.newFeedInfo)

        End Sub
    End Class
End Namespace

