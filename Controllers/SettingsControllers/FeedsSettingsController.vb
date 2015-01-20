Imports IGNIS.Commands.Settings

Public Class FeedsSettingsController
    Inherits SettingsController

    Private _feedsInfo1 As List(Of FeedInfoNode)
    Private _feedsInfo2 As List(Of FeedInfoNode)
    Private _feedsInfo3 As List(Of FeedInfoNode)

    Public Sub New()
        MyBase.New()

    End Sub

    Public ReadOnly Property UsineType As Constants.Settings.UsineType
        Get
            Return XmlSettings.Settings.instance.Usine.TYPE
        End Get
    End Property

    Public Sub addNewFeed1(index As Integer, location As String, material As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

        Select Case Me.UsineType
            Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                Me.executeCommand(New AddCSVHotFeed(index, location, material, isRecycled, isFiller))

            Case Constants.Settings.UsineType.LOG
                Me.executeCommand(New AddLOGColdFeed(index, location, material, isRecycled, isFiller))

            Case Constants.Settings.UsineType.MDB
                Me.executeCommand(New AddMDBHotFeed(index, location, material, isRecycled, isFiller, isAsphalt))

            Case Constants.Settings.UsineType.UNKNOWN
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub addNewFeed2(index As Integer, location As String, material As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

        Select Case Me.UsineType
            Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                Me.executeCommand(New AddCSVColdFeed(index, location, material, isRecycled))

            Case Constants.Settings.UsineType.MDB
                Me.executeCommand(New AddMDBColdFeed(index, location, material, isRecycled))

            Case Constants.Settings.UsineType.UNKNOWN
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub addNewFeed3(index As Integer, location As String, material As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

        Select Case Me.UsineType
            Case Constants.Settings.UsineType.HYBRID
                Me.executeCommand(New AddLOGColdFeed(index, location, material, isRecycled, isFiller))

            Case Constants.Settings.UsineType.UNKNOWN
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub deleteFeed1(feedInfo As FeedInfoNode)

        Select Case Me.UsineType
            Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                Me.executeCommand(New RemoveCSVHotFeed(feedInfo))

            Case Constants.Settings.UsineType.LOG
                Me.executeCommand(New RemoveLOGColdFeed(feedInfo))

            Case Constants.Settings.UsineType.MDB
                Me.executeCommand(New RemoveMDBHotFeed(feedInfo))

            Case Constants.Settings.UsineType.UNKNOWN
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub deleteFeed2(feedInfo As FeedInfoNode)

        Select Case Me.UsineType
            Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                Me.executeCommand(New RemoveCSVColdFeed(feedInfo))

            Case Constants.Settings.UsineType.MDB
                Me.executeCommand(New RemoveMDBColdFeed(feedInfo))

            Case Constants.Settings.UsineType.UNKNOWN
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub deleteFeed3(feedInfo As FeedInfoNode)

        Select Case Me.UsineType
            Case Constants.Settings.UsineType.HYBRID
                Me.executeCommand(New RemoveLOGColdFeed(feedInfo))

            Case Constants.Settings.UsineType.UNKNOWN
                Throw New NotImplementedException

        End Select
    End Sub

    Public Sub updateFeed(feedInfo As FeedInfoNode, newIndex As Integer, newLocation As String, newMaterial As String, newIsRecycled As Boolean, newIsFiller As Boolean, newIsAsphalt As Boolean)

        Me.executeCommand(New UpdateFeedInfo(feedInfo, newIndex, newLocation, newMaterial, newIsRecycled, newIsFiller, newIsAsphalt))
    End Sub

    Public ReadOnly Property Feeds1 As List(Of FeedInfoNode)
        Get

            Select Case Me.UsineType
                Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                    Return XmlSettings.Settings.instance.Usine.DataFiles.CSV.HOT_FEEDS

                Case Constants.Settings.UsineType.LOG
                    Return XmlSettings.Settings.instance.Usine.DataFiles.LOG.COLD_FEEDS

                Case Constants.Settings.UsineType.MDB
                    Return XmlSettings.Settings.instance.Usine.DataFiles.MDB.HOT_FEEDS

                Case Constants.Settings.UsineType.UNKNOWN
                    Throw New NotImplementedException

            End Select

            Return Nothing
        End Get
    End Property

    Public ReadOnly Property UnknownsFeeds1 As List(Of UnknownFeedNode)
        Get

            Dim unknownFeeds As New List(Of UnknownFeedNode)

            Select Case Me.UsineType
                Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV

                    For Each _unknownFeedInfo As UnknownFeedNode In XmlSettings.Settings.instance.Usine.DataFiles.CSV.UNKNOWN_FEEDS

                        If (_unknownFeedInfo.TAG.Equals(Cycle.HOT_FEED_TAG)) Then
                            unknownFeeds.Add(_unknownFeedInfo)
                        End If
                    Next

                Case Constants.Settings.UsineType.LOG

                    unknownFeeds = XmlSettings.Settings.instance.Usine.DataFiles.LOG.UNKNOWN_FEEDS

                Case Constants.Settings.UsineType.MDB

                    For Each _unknownFeedInfo As UnknownFeedNode In XmlSettings.Settings.instance.Usine.DataFiles.MDB.UNKNOWN_FEEDS

                        If (_unknownFeedInfo.TAG.Equals(Cycle.HOT_FEED_TAG)) Then
                            unknownFeeds.Add(_unknownFeedInfo)
                        End If
                    Next

                Case Constants.Settings.UsineType.UNKNOWN
                    Throw New NotImplementedException

            End Select

            Return unknownFeeds
        End Get
    End Property

    Public ReadOnly Property Feeds2 As List(Of FeedInfoNode)
        Get
            ' #refactor
            Dim feeds As New List(Of FeedInfoNode)

            Select Case Me.UsineType

                Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV
                    feeds.AddRange(XmlSettings.Settings.instance.Usine.DataFiles.CSV.COLD_FEEDS)

                Case Constants.Settings.UsineType.MDB
                    feeds.AddRange(XmlSettings.Settings.instance.Usine.DataFiles.MDB.COLD_FEEDS)

                Case Constants.Settings.UsineType.UNKNOWN
                    Throw New NotImplementedException

            End Select

            Return feeds
        End Get
    End Property

    Public ReadOnly Property UnknownsFeeds2 As List(Of UnknownFeedNode)
        Get

            Dim unknownFeeds As New List(Of UnknownFeedNode)

            Select Case Me.UsineType
                Case Constants.Settings.UsineType.HYBRID, Constants.Settings.UsineType.CSV

                    For Each _unknownFeedInfo As UnknownFeedNode In XmlSettings.Settings.instance.Usine.DataFiles.CSV.UNKNOWN_FEEDS

                        If (_unknownFeedInfo.TAG.Equals(Cycle.COLD_FEED_TAG)) Then
                            unknownFeeds.Add(_unknownFeedInfo)
                        End If
                    Next

                Case Constants.Settings.UsineType.LOG
                    ' Do nothing

                Case Constants.Settings.UsineType.MDB

                    For Each _unknownFeedInfo As UnknownFeedNode In XmlSettings.Settings.instance.Usine.DataFiles.MDB.UNKNOWN_FEEDS

                        If (_unknownFeedInfo.TAG.Equals(Cycle.COLD_FEED_TAG)) Then
                            unknownFeeds.Add(_unknownFeedInfo)
                        End If
                    Next

                Case Constants.Settings.UsineType.UNKNOWN
                    Throw New NotImplementedException

            End Select

            Return unknownFeeds
        End Get
    End Property

    Public ReadOnly Property Feeds3 As List(Of FeedInfoNode)
        Get
            ' #refactor
            Dim feeds As New List(Of FeedInfoNode)

            Select Case Me.UsineType

                Case Constants.Settings.UsineType.HYBRID
                    feeds.AddRange(XmlSettings.Settings.instance.Usine.DataFiles.LOG.COLD_FEEDS)

                Case Constants.Settings.UsineType.UNKNOWN
                    Throw New NotImplementedException

            End Select

            Return feeds
        End Get
    End Property

    Public ReadOnly Property UnknownsFeeds3 As List(Of UnknownFeedNode)
        Get

            Dim unknownFeeds As New List(Of UnknownFeedNode)

            Select Case Me.UsineType
                Case Constants.Settings.UsineType.HYBRID

                    Return XmlSettings.Settings.instance.Usine.DataFiles.LOG.UNKNOWN_FEEDS

                Case Constants.Settings.UsineType.UNKNOWN
                    Throw New NotImplementedException

            End Select

            Return unknownFeeds
        End Get
    End Property
End Class
