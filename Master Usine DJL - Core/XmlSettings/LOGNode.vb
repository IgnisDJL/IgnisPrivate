Namespace XmlSettings

    Public Class LOGNode
        Inherits DataFileNode

        Public Const NODE_NAME As String = "log"

        Private currentXPath As String
        Public Const XPATH_TO_NODE = DataFilesNode.XPATH_TO_NODE & "/" & NODE_NAME

        Public Const FEEDS_INFO_NODE_NAME As String = "feedsInfo"

        Private formatNode As Xml.XmlNode
        Private feedsInfoNode As Xml.XmlNode

        Public Const NUMBER_OF_HOT_FEEDS = Constants.Input.LOG.NUMBER_OF_HOT_FEEDS
        Public Property NUMBER_OF_COLD_FEEDS As Integer = 0

        Public Property COLUMN_LIST As New List(Of FeedInfo)

        Private coldFeeds As New List(Of FeedInfoNode)
        Public ReadOnly Property COLD_FEEDS As List(Of FeedInfoNode)
            Get
                Return Me.coldFeeds
            End Get
        End Property

        Private _subColumns As New List(Of DataInfo)
        Public ReadOnly Property SUB_COLUMNS As List(Of DataInfo)
            Get
                Return Me._subColumns
            End Get
        End Property

        Public Sub New(parentNode As Xml.XmlNode, logNode As Xml.XmlNode)
            MyBase.New(parentNode, logNode)

            Me.currentXPath = XPATH_TO_NODE

            Me.feedsInfoNode = Me.NODE.SelectSingleNode(Me.currentXPath & "/" & FEEDS_INFO_NODE_NAME)
            If (IsNothing(Me.feedsInfoNode)) Then
                Me.feedsInfoNode = Me.NODE.OwnerDocument.CreateElement(FEEDS_INFO_NODE_NAME)
                Me.NODE.AppendChild(Me.feedsInfoNode)
            End If

            Me.DATA_LIST.AddRange(Constants.Input.LOG.AVAILABLE_DATA)
            Me.SUB_COLUMNS.AddRange(Constants.Input.LOG.AVAILABLE_SUBCOLUMNS)

            ' For each <feed> node in the <feedsInfo> node's childs
            For Each feedNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME & "/" & IGNIS.FeedInfoNode.NODE_NAME)

                Me.COLD_FEEDS.Add(New FeedInfoNode(feedNode, Cycle.COLD_FEED_TAG, Me.SUB_COLUMNS))

            Next

            ' Unknown Feeds
            For Each unknownFeedNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME & "/" & IGNIS.UnknownFeedNode.NODE_NAME)

                Me.UNKNOWN_FEEDS.Add(New UnknownFeedNode(unknownFeedNode, Cycle.COLD_FEED_TAG))

            Next

        End Sub ' End constructor

        Public Function addUnknownFeed(location As String, material As String, tag As Tag) As UnknownFeedNode

            Dim addToList As Boolean = True

            For Each unknownFeed In Me.UNKNOWN_FEEDS

                If (unknownFeed.LOCATION.Equals(location)) Then

                    addToList = False
                    Return unknownFeed

                End If

            Next

            If (addToList) Then

                Dim xmlDoc = Me.NODE.OwnerDocument

                Dim newNode = xmlDoc.CreateElement(UnknownFeedNode.NODE_NAME)
                Dim feedInformation As New UnknownFeedNode(newNode, tag)

                feedInformation.LOCATION = location

                Dim materialAttr = xmlDoc.CreateAttribute(UnknownFeedNode.MATERIAL_ATTRIBUTE)
                newNode.Attributes.Append(materialAttr)
                feedInformation.MATERIAL = material

                Me.feedsInfoNode.AppendChild(newNode)
                Me.UNKNOWN_FEEDS.Add(feedInformation)

                Return feedInformation

            End If

            Return Nothing

        End Function

        Public Sub removeUnknownFeed(unknownFeed As UnknownFeedNode)
            Me.UNKNOWN_FEEDS.Remove(unknownFeed)
            Me.feedsInfoNode.RemoveChild(unknownFeed.NODE)
        End Sub

        Public Function addColdFeedInfo(location As String, material As String, index As Integer, isRecycled As Boolean, isFiller As Boolean) As FeedInfoNode

            Dim xmlDoc = Me.NODE.OwnerDocument

            Dim newNode = xmlDoc.CreateElement(FeedInfoNode.NODE_NAME)
            Dim feedInformation As New FeedInfoNode(newNode, Cycle.COLD_FEED_TAG, Me.SUB_COLUMNS)

            feedInformation.LOCATION = location

            Dim materialAttr = xmlDoc.CreateAttribute(FeedInfoNode.MATERIAL_ATTRIBUTE)
            newNode.Attributes.Append(materialAttr)
            feedInformation.MATERIAL = material

            Dim indexAttr = xmlDoc.CreateAttribute(FeedInfoNode.INDEX_ATTRIBUTE)
            newNode.Attributes.Append(indexAttr)
            feedInformation.INDEX = index

            Dim isRecycledAttr = xmlDoc.CreateAttribute(FeedInfoNode.IS_RECYCLED_ATTRIBUTE)
            newNode.Attributes.Append(isRecycledAttr)
            feedInformation.IS_RECYCLED = isRecycled

            Dim isFillerdAttr = xmlDoc.CreateAttribute(FeedInfoNode.IS_FILLER_ATTRIBUTE)
            newNode.Attributes.Append(isFillerdAttr)
            feedInformation.IS_FILLER = isFiller

            Return addColdFeedInfo(feedInformation)

        End Function

        Public Function addColdFeedInfo(feedInfo As FeedInfoNode) As FeedInfoNode

            ' Remove matching unknown feed
            For Each unknownFeed In Me.UNKNOWN_FEEDS

                If (unknownFeed.LOCATION.Equals(feedInfo.LOCATION) AndAlso unknownFeed.TAG.Equals(feedInfo.TAG)) Then

                    Me.removeUnknownFeed(unknownFeed)

                    Exit For
                End If
            Next

            Me.feedsInfoNode.AppendChild(feedInfo.NODE)
            Me.COLD_FEEDS.Add(feedInfo)

            Return feedInfo
        End Function

        Public Sub removeColdFeedInfo(coldFeedInfo As FeedInfoNode)

            Me.COLD_FEEDS.Remove(coldFeedInfo)
            Me.feedsInfoNode.RemoveChild(coldFeedInfo.NODE)

            Me.addUnknownFeed(coldFeedInfo.LOCATION, coldFeedInfo.MATERIAL, Cycle.COLD_FEED_TAG)

        End Sub


        Public Function getDataInfoByTag(tag As Tag) As DataInfo

            For Each dataInfo As DataInfo In Me.DATA_LIST

                If (dataInfo.TAG = tag) Then
                    Return dataInfo
                End If

            Next

            'Unknown tag
            Debugger.Break()

            Return Nothing

        End Function


        Public Function getFeedInfoByIndex(tag As Tag, index As Integer) As IGNIS.FeedInfo

            If (tag.Equals(Cycle.COLD_FEED_TAG)) Then

                For Each coldFeedInfo In Me.COLD_FEEDS
                    If (coldFeedInfo.INDEX.Equals(index)) Then
                        Return coldFeedInfo
                    End If
                Next

            Else

                For Each summaryFeedInfo In Constants.Input.LOG.AVAILABLE_FEEDINFO
                    If (tag.Equals(summaryFeedInfo.TAG)) Then
                        Return summaryFeedInfo
                    End If
                Next

            End If

            ' Unkown tag or unfound feed index
            Debugger.Break()

            Return Nothing
        End Function

        Public Property ACTIVE As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value.Equals(Settings.IS_ACTIVE)
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value = If(value, Settings.IS_ACTIVE, Settings.IS_NOT_ACTIVE)
            End Set
        End Property

        Public Overrides Function getUnitByTag(tag As Tag) As Unit

            For Each _data In Me.DATA_LIST
                If (_data.TAG.Equals(tag)) Then
                    Return _data.UNIT
                End If
            Next

            For Each _data In Me.SUB_COLUMNS
                If (_data.TAG.Equals(tag)) Then
                    Return _data.UNIT
                End If
            Next

            ' Not supposed to happen...
            Debugger.Break()

            Return Nothing

        End Function

        Public Overrides Function verifyTag(tagName As String, isSubColumn As Boolean) As Tag

            'If (isSubColumn) Then

            '    For Each tagObject In Feeder.TAGS

            '        If (tagName.Equals(tagObject.TAG_NAME)) Then
            '            Return tagObject
            '        End If

            '    Next

            '    For Each tagObject In LOGFeeder.TAGS

            '        If (tagName.Equals(tagObject.TAG_NAME)) Then
            '            Return tagObject
            '        End If

            '    Next

            'Else

            '    For Each tagObject In Cycle.TAGS

            '        If (tagName.Equals(tagObject.TAG_NAME)) Then
            '            Return tagObject
            '        End If

            '    Next

            '    For Each tagObject In LOGCycle.TAGS

            '        If (tagName.Equals(tagObject.TAG_NAME)) Then
            '            Return tagObject
            '        End If

            '    Next

            'End If

            'Throw New InvalidTagException("Invalid tag in log -> " & tagName)

            Debugger.Break()
            Return Nothing

        End Function ' End verifyTags

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(Settings.ACTIVE_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
            Me.ACTIVE = False
        End Sub
    End Class

End Namespace