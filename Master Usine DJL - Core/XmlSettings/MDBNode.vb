Namespace XmlSettings

    Public Class MDBNode
        Inherits DataFileNode

        Public Const NODE_NAME As String = "mdb"
        Public Const XPATH_TO_NODE = XmlSettings.DataFilesNode.XPATH_TO_NODE & "/" & XmlSettings.MDBNode.NODE_NAME

        Public Const STOP_OFFSET_ATTRIBUTE As String = "stopOffset"

        Public Const FEEDS_INFO_NODE_NAME As String = "feedsInfo"

        Private feedsInfoNode As Xml.XmlNode

        Private nbAsphaltFeeds As Integer = 0

        Public ReadOnly FEEDS_LIST As New List(Of FeedInfo) ' Useless... why is this here???

        Private _subColumns As New List(Of DataInfo)
        Public ReadOnly Property SUB_COLUMNS As List(Of DataInfo)
            Get
                Return Me._subColumns
            End Get
        End Property

        Private hotFeeds As New List(Of FeedInfoNode)
        Public ReadOnly Property HOT_FEEDS As List(Of FeedInfoNode)
            Get
                Return Me.hotFeeds
            End Get
        End Property

        Private coldFeeds As New List(Of FeedInfoNode)
        Public ReadOnly Property COLD_FEEDS As List(Of FeedInfoNode)
            Get
                Return Me.coldFeeds
            End Get
        End Property

        Public Sub New(parentNode As Xml.XmlNode, mdbNode As Xml.XmlNode)
            MyBase.New(parentNode, mdbNode)

            Me.feedsInfoNode = Me.NODE.SelectSingleNode(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME)
            If (IsNothing(Me.feedsInfoNode)) Then
                Me.feedsInfoNode = Me.NODE.OwnerDocument.CreateElement(FEEDS_INFO_NODE_NAME)
                Me.NODE.AppendChild(Me.feedsInfoNode)
            End If

            Me.DATA_LIST.AddRange(Constants.Input.MDB.AVAILABLE_DATA)

            Me.SUB_COLUMNS.AddRange(Constants.Input.MDB.AVAILABLE_SUBCOLUMNS)

            ' For each <feed> node in the <feedsInfo> node's childs
            For Each feedNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME & "/" & IGNIS.FeedInfoNode.NODE_NAME)

                Dim tagName As String = feedNode.Attributes.GetNamedItem(FeedInfoNode.TAG_ATTRIBUTE).Value

                ' If the feed info is for a hotfeed
                If (tagName.Equals(MDBCycle.HOT_FEED_TAG.TAG_NAME)) Then

                    Me.HOT_FEEDS.Add(New FeedInfoNode(feedNode, MDBCycle.HOT_FEED_TAG, Me.SUB_COLUMNS))

                    If (Me.HOT_FEEDS.Last.IS_ASPHALT) Then
                        'Me.HOT_FEEDS.Last.INDEX = Constants.Input.Common.ASPHALT_FEEDS_START_INDEX + Me.nbAsphaltFeeds
                        Me.HOT_FEEDS.Last.INDEX = Me.nbAsphaltFeeds
                        Me.nbAsphaltFeeds += 1
                    End If

                    'If the feed info is for a coldfeed
                ElseIf (tagName.Equals(MDBCycle.COLD_FEED_TAG.TAG_NAME)) Then

                    Me.COLD_FEEDS.Add(New FeedInfoNode(feedNode, MDBCycle.COLD_FEED_TAG, Me.SUB_COLUMNS))

                End If

            Next

            ' Unknown Feeds
            For Each unknownFeedNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME & "/" & IGNIS.UnknownFeedNode.NODE_NAME)

                Dim tagName As String = unknownFeedNode.Attributes.GetNamedItem(IGNIS.UnknownFeedNode.TAG_ATTRIBUTE).Value

                If (tagName.Equals(MDBCycle.HOT_FEED_TAG.TAG_NAME)) Then

                    Me.UNKNOWN_FEEDS.Add(New UnknownFeedNode(unknownFeedNode, MDBCycle.HOT_FEED_TAG))

                ElseIf (tagName.Equals(MDBCycle.COLD_FEED_TAG.TAG_NAME)) Then

                    Me.UNKNOWN_FEEDS.Add(New UnknownFeedNode(unknownFeedNode, MDBCycle.COLD_FEED_TAG))

                End If

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

        Public Function addColdFeedInfo(location As String, material As String, index As Integer, isRecycled As Boolean) As FeedInfo

            Dim xmlDoc = Me.NODE.OwnerDocument

            Dim newNode = xmlDoc.CreateElement(FeedInfoNode.NODE_NAME)
            Dim feedInformation As New FeedInfoNode(newNode, MDBCycle.COLD_FEED_TAG, Me.SUB_COLUMNS)

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

            Return addColdFeedInfo(feedInformation)

        End Function

        Public Function addColdFeedInfo(feedInfo As FeedInfoNode)

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

            Me.addUnknownFeed(coldFeedInfo.LOCATION, coldFeedInfo.MATERIAL, MDBCycle.COLD_FEED_TAG)

        End Sub

        Public Function addHotFeedInfo(location As String, material As String, index As Integer, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean) As FeedInfo

            Dim xmlDoc = Me.NODE.OwnerDocument

            Dim newNode = xmlDoc.CreateElement(FeedInfoNode.NODE_NAME)
            Dim feedInformation As New FeedInfoNode(newNode, MDBCycle.HOT_FEED_TAG, Me.SUB_COLUMNS)

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

            Dim isFillerAttr = xmlDoc.CreateAttribute(FeedInfoNode.IS_FILLER_ATTRIBUTE)
            newNode.Attributes.Append(isFillerAttr)
            feedInformation.IS_FILLER = isFiller

            Dim isAsphaltAttr = xmlDoc.CreateAttribute(FeedInfoNode.IS_ASPHALT_ATTRIBUTE)
            newNode.Attributes.Append(isAsphaltAttr)
            feedInformation.IS_ASPHALT = isAsphalt

            If (isAsphalt) Then
                ' feedInformation.INDEX = Constants.Input.Common.ASPHALT_FEEDS_START_INDEX + Me.nbAsphaltFeeds
                feedInformation.INDEX = Me.nbAsphaltFeeds
                Me.nbAsphaltFeeds += 1
            End If

            Return addHotFeedInfo(feedInformation)

        End Function

        Public Function addHotFeedInfo(feedInfo As FeedInfoNode) As FeedInfoNode

            ' Remove matching unknown feed
            For Each unknownFeed In Me.UNKNOWN_FEEDS

                If (unknownFeed.LOCATION.Equals(feedInfo.LOCATION) AndAlso unknownFeed.TAG.Equals(feedInfo.TAG)) Then

                    Me.removeUnknownFeed(unknownFeed)

                    Exit For
                End If

            Next

            Me.feedsInfoNode.AppendChild(feedInfo.NODE)
            Me.HOT_FEEDS.Add(feedInfo)

            Return feedInfo
        End Function

        Public Sub removeHotFeedInfo(hotFeedInfo As FeedInfoNode)

            If (hotFeedInfo.IS_ASPHALT) Then
                Me.nbAsphaltFeeds -= 1
                For Each asphaltFeed In Me.HOT_FEEDS
                    If (asphaltFeed.INDEX > hotFeedInfo.INDEX) Then
                        asphaltFeed.INDEX -= 1
                    End If
                Next
            End If

            Me.addUnknownFeed(hotFeedInfo.LOCATION, hotFeedInfo.MATERIAL, MDBCycle.HOT_FEED_TAG)

            Me.HOT_FEEDS.Remove(hotFeedInfo)
            Me.feedsInfoNode.RemoveChild(hotFeedInfo.NODE)
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

            If (tag.Equals(Cycle.HOT_FEED_TAG)) Then

                For Each hotFeedInfo In Me.HOT_FEEDS
                    If (hotFeedInfo.INDEX.Equals(index)) Then
                        Return hotFeedInfo
                    End If
                Next

            ElseIf (tag.Equals(Cycle.COLD_FEED_TAG)) Then

                For Each coldFeedInfo In Me.COLD_FEEDS
                    If (coldFeedInfo.INDEX.Equals(index)) Then
                        Return coldFeedInfo
                    End If
                Next

            Else

                For Each summaryFeedInfo In Constants.Input.MDB.AVAILABLE_FEEDINFO
                    If (tag.Equals(summaryFeedInfo.TAG)) Then
                        Return summaryFeedInfo
                    End If
                Next

            End If

            ' Unkown tag or unfound feed index
            Debugger.Break()

            Return Nothing
        End Function

        'Public Function getDataByName(columnName As Tag) As DataInfo

        '    For Each _data In Me.DATA_LIST

        '        If (_data.TAG.Equals(columnName)) Then
        '            Return _data
        '        End If
        '    Next

        '    Return Nothing

        'End Function

        'Public Function getColumnByName(columnName As String) As DataFilesNode.ColumnInfo

        '    For Each column In Me.COLUMN_LIST

        '        If (column.NAME.Equals(columnName)) Then
        '            Return column
        '        End If
        '    Next

        '    Return Nothing

        'End Function


        Public Property ACTIVE As Boolean
            Get
                Return Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value.Equals(Settings.IS_ACTIVE)
            End Get
            Set(value As Boolean)
                Me.NODE.Attributes.GetNamedItem(Settings.ACTIVE_ATTRIBUTE).Value = If(value, Settings.IS_ACTIVE, Settings.IS_NOT_ACTIVE)
            End Set
        End Property

        Public Property STOP_OFFSET As Integer
            Get
                Return CInt(Me.NODE.Attributes.GetNamedItem(STOP_OFFSET_ATTRIBUTE).Value)
            End Get
            Set(value As Integer)
                Me.NODE.Attributes.GetNamedItem(STOP_OFFSET_ATTRIBUTE).Value = value
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


        Public Overrides Function verifyTag(tag As String, isSubColumn As Boolean) As Tag

            '    If (isSubColumn) Then

            '        For Each tagObject In Feeder.TAGS

            '            If (tag.Equals(tagObject.tag)) Then
            '                Return tagObject
            '            End If

            '        Next

            '        For Each tagObject In MDBFeeder.TAGS

            '            If (tag.Equals(tagObject.tag)) Then
            '                Return tagObject
            '            End If

            '        Next

            '    Else

            '        For Each tagObject In Cycle.TAGS

            '            If (tag.Equals(tagObject.tag)) Then
            '                Return tagObject
            '            End If

            '        Next

            '        For Each tagObject In MDBCycle.TAGS

            '            If (tag.Equals(tagObject.tag)) Then
            '                Return tagObject
            '            End If

            '        Next

            '    End If

            '    Throw New InvalidTagException("Invalid tag in mdb -> " & tag)

            '    Return Nothing
            Return Nothing
        End Function

        Protected Overrides Function create(parentNode As Xml.XmlNode) As Xml.XmlNode
            Dim document = parentNode.OwnerDocument

            Dim node = document.CreateElement(NODE_NAME)

            node.Attributes.Append(document.CreateAttribute(Settings.ACTIVE_ATTRIBUTE))
            node.Attributes.Append(document.CreateAttribute(STOP_OFFSET_ATTRIBUTE))

            parentNode.AppendChild(node)

            Return node
        End Function

        Protected Overrides Sub setDefaultValues()
            Me.ACTIVE = False
            Me.STOP_OFFSET = 60
        End Sub
    End Class ' End MDB Node
End Namespace
