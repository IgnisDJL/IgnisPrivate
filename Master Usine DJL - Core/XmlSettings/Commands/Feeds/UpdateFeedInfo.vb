Namespace Commands.Settings

    Public Class UpdateFeedInfo
        Inherits SettingsCommand

        Private newIndex As Integer
        Private newLocation As String
        Private newMaterial As String
        Private newIsRecycled As Boolean
        Private newIsFiller As Boolean
        Private newIsAsphalt As Boolean

        Private oldIndex As Integer
        Private oldLocation As String
        Private oldMaterial As String
        Private oldIsRecycled As Boolean
        Private oldIsFiller As Boolean
        Private oldIsAsphalt As Boolean

        Private feedToUpdate As FeedInfoNode

        Public Sub New(feedToUpdate As FeedInfoNode, newIndex As Integer, newLocation As String, newMaterial As String, newIsRecycled As Boolean, Optional newIsFiller As Boolean = False, Optional newIsAsphalt As Boolean = False)

            Me.newIndex = newIndex
            Me.newLocation = newLocation
            Me.newMaterial = newMaterial
            Me.newIsRecycled = newIsRecycled
            Me.newIsFiller = newIsFiller
            Me.newIsAsphalt = newIsAsphalt

            Me.feedToUpdate = feedToUpdate

            Me.oldIndex = feedToUpdate.INDEX
            Me.oldLocation = feedToUpdate.LOCATION
            Me.oldMaterial = feedToUpdate.MATERIAL
            Me.oldIsRecycled = feedToUpdate.IS_RECYCLED
            Me.oldIsFiller = feedToUpdate.IS_FILLER
            Me.oldIsAsphalt = feedToUpdate.IS_ASPHALT
        End Sub

        Public Overrides Sub execute()

            With Me.feedToUpdate
                .INDEX = newIndex
                .LOCATION = newLocation
                .MATERIAL = newMaterial
                .IS_RECYCLED = newIsRecycled
                .IS_FILLER = newIsFiller
                .IS_ASPHALT = newIsAsphalt
            End With

        End Sub

        Public Overrides Sub undo()

            With Me.feedToUpdate
                .INDEX = oldIndex
                .LOCATION = oldLocation
                .MATERIAL = oldMaterial
                .IS_RECYCLED = oldIsRecycled
                .IS_FILLER = oldIsFiller
                .IS_ASPHALT = oldIsAsphalt
            End With
        End Sub
    End Class
End Namespace

