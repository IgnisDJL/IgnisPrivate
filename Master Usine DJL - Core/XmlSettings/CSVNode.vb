
Namespace XmlSettings

    Public Class CSVNode
        Inherits DataFileNode

        Public Const NODE_NAME As String = "csv"

        Public currentXPath As String
        Public Const XPATH_TO_NODE As String = XmlSettings.DataFilesNode.XPATH_TO_NODE & "/" & XmlSettings.CSVNode.NODE_NAME

        Public Const STOP_OFFSET_ATTRIBUTE As String = "stopOffset"

        Public Const FORMAT_NODE_NAME As String = "format"
        Public Const FEEDS_INFO_NODE_NAME As String = "feedsInfo"

        Private formatNode As Xml.XmlNode
        Private feedsInfoNode As Xml.XmlNode

        ''' <summary>The list of all the available data in the .csv file in order.</summary>
        Public Property DATA_FORMAT As New List(Of DataInfoNode)

        Private _subColumns As New List(Of DataInfo)
        Public ReadOnly Property SUB_COLUMNS As List(Of DataInfo)
            Get
                Return Me._subColumns
            End Get
        End Property

        Private aggregateMassIndex As Integer = 1
        Private aggregatePercentageIndex As Integer = 1
        Private coldFeedPercentageIndex As Integer = 1

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

        Public ReadOnly Property NB_AGGREGATE_MASS_COLUMNS As Integer
            Get
                Return Me.aggregateMassIndex - 1
            End Get
        End Property

        Public ReadOnly Property NB_AGGREGATE_PERCENTAGE_COLUMNS As Integer
            Get
                Return Me.aggregatePercentageIndex - 1
            End Get
        End Property

        Public ReadOnly Property NB_COLDFEED_PERCENTAGE_COLUMNS As Integer
            Get
                Return Me.coldFeedPercentageIndex - 1
            End Get
        End Property


        Public Sub New(parentNode As Xml.XmlNode, csvNode As Xml.XmlNode)
            MyBase.New(parentNode, csvNode)

            Me.currentXPath = XPATH_TO_NODE

            Me.formatNode = Me.NODE.SelectSingleNode(Me.currentXPath & "/" & FORMAT_NODE_NAME)
            If (IsNothing(Me.formatNode)) Then
                Me.formatNode = Me.NODE.OwnerDocument.CreateElement(FORMAT_NODE_NAME)
                Me.NODE.AppendChild(Me.formatNode)
            End If

            Me.feedsInfoNode = Me.NODE.SelectSingleNode(Me.currentXPath & "/" & FEEDS_INFO_NODE_NAME)
            If (IsNothing(Me.feedsInfoNode)) Then
                Me.feedsInfoNode = Me.NODE.OwnerDocument.CreateElement(FEEDS_INFO_NODE_NAME)
                Me.NODE.AppendChild(Me.feedsInfoNode)
            End If

            Me.DATA_LIST.AddRange(Constants.Input.CSV.AVAILABLE_DATA)
            Me.SUB_COLUMNS.AddRange(Constants.Input.CSV.AVAILABLE_SUBCOLUMNS)

            For Each dataNode As Xml.XmlNode In Me.NODE.SelectNodes(Me.currentXPath & "/" & FORMAT_NODE_NAME & "/" & DataInfoNode.NODE_NAME)

                Dim nodeTag As Tag = Me.getTagByName(dataNode.Attributes.GetNamedItem(DataInfoNode.TAG_ATTRIBUTE).Value)

                Dim dataInfo As New DataInfoNode(dataNode, Me, nodeTag, False)

                Me.DATA_FORMAT.Add(dataInfo)
                Me.DATA_LIST.Add(dataInfo)

                If (nodeTag.Equals(CSVCycle.AGGREGATE_MASS_TAG)) Then

                    dataInfo.INDEX = aggregateMassIndex
                    aggregateMassIndex += 1

                ElseIf (nodeTag.Equals(CSVCycle.AGGREGATE_PERCENTAGE_TAG)) Then

                    dataInfo.INDEX = aggregatePercentageIndex
                    aggregatePercentageIndex += 1

                ElseIf (nodeTag.Equals(CSVCycle.COLD_FEED_PERCENTAGE_TAG)) Then

                    dataInfo.INDEX = coldFeedPercentageIndex
                    coldFeedPercentageIndex += 1

                End If

            Next

            ' For each <feed> node in the <feedsInfo> node's childs
            For Each feedNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME & "/" & IGNIS.FeedInfoNode.NODE_NAME)

                Dim tagName As String = feedNode.Attributes.GetNamedItem(FeedInfoNode.TAG_ATTRIBUTE).Value

                ' If the feed info is for a hotfeed
                If (tagName.Equals(Cycle.HOT_FEED_TAG.TAG_NAME)) Then

                    Me.HOT_FEEDS.Add(New FeedInfoNode(feedNode, Cycle.HOT_FEED_TAG, Me.SUB_COLUMNS))

                    'If the feed info is for a coldfeed
                ElseIf (tagName.Equals(Cycle.COLD_FEED_TAG.TAG_NAME)) Then

                    Me.COLD_FEEDS.Add(New FeedInfoNode(feedNode, MDBCycle.COLD_FEED_TAG, Me.SUB_COLUMNS))

                End If

            Next

            ' Unknown Feeds
            For Each unknownFeedNode As Xml.XmlNode In Me.NODE.SelectNodes(XPATH_TO_NODE & "/" & FEEDS_INFO_NODE_NAME & "/" & IGNIS.UnknownFeedNode.NODE_NAME)

                Dim tagName As String = unknownFeedNode.Attributes.GetNamedItem(IGNIS.UnknownFeedNode.TAG_ATTRIBUTE).Value

                If (tagName.Equals(Cycle.HOT_FEED_TAG.TAG_NAME)) Then

                    Me.UNKNOWN_FEEDS.Add(New UnknownFeedNode(unknownFeedNode, Cycle.HOT_FEED_TAG))

                ElseIf (tagName.Equals(Cycle.COLD_FEED_TAG.TAG_NAME)) Then

                    Me.UNKNOWN_FEEDS.Add(New UnknownFeedNode(unknownFeedNode, Cycle.COLD_FEED_TAG))

                End If

            Next

        End Sub ' End constructor

        Public Function addFormatInfo(name As String, tag As Tag, index As Integer, unit As Unit) As DataInfo

            Dim xmlDoc = Me.NODE.OwnerDocument

            Dim newNode = xmlDoc.CreateElement(DataInfoNode.NODE_NAME)
            Dim dataInformation As New DataInfoNode(newNode, Me, tag, False)

            Dim tagAttr = xmlDoc.CreateAttribute(DataInfoNode.TAG_ATTRIBUTE)
            newNode.Attributes.Append(tagAttr)

            Dim unitAttr = xmlDoc.CreateAttribute(DataInfoNode.UNIT_ATTRIBUTE)
            newNode.Attributes.Append(unitAttr)
            dataInformation.UNIT = unit

            Me.formatNode.AppendChild(newNode)
            Me.DATA_LIST.Add(dataInformation)

            Return dataInformation

        End Function

        Public Sub removeFormatInfo(formatInfo As DataInfoNode)
            Me.DATA_LIST.Remove(formatInfo)
            Me.formatNode.RemoveChild(formatInfo.NODE)
        End Sub

        Public Function addHotFeedInfo(location As String, material As String, index As Integer, isRecycled As Boolean, isFiller As Boolean) As FeedInfo

            Dim xmlDoc = Me.NODE.OwnerDocument

            Dim newNode = xmlDoc.CreateElement(FeedInfoNode.NODE_NAME)
            Dim feedInformation As New FeedInfoNode(newNode, Cycle.HOT_FEED_TAG, Me.SUB_COLUMNS)

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

            Return addHotFeedInfo(feedInformation)

        End Function

        Public Function addHotFeedInfo(feedInfo As FeedInfoNode)

            ' Remove corresponding unknownfeed
            For Each _unknownFeed As UnknownFeedNode In Me.UNKNOWN_FEEDS

                If (_unknownFeed.TAG.Equals(Cycle.HOT_FEED_TAG) AndAlso _unknownFeed.LOCATION.Equals(feedInfo.LOCATION)) Then
                    removeUnknownFeed(_unknownFeed)
                    Exit For
                End If
            Next

            Me.feedsInfoNode.AppendChild(feedInfo.NODE)
            Me.HOT_FEEDS.Add(feedInfo)

            Return feedInfo
        End Function

        Public Sub removeHotFeedInfo(hotFeedInfo As FeedInfoNode)
            Me.HOT_FEEDS.Remove(hotFeedInfo)
            Me.feedsInfoNode.RemoveChild(hotFeedInfo.NODE)
        End Sub

        Public Function addColdFeedInfo(location As String, material As String, index As Integer, isRecycled As Boolean) As FeedInfo

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

            Return addColdFeedInfo(feedInformation)

        End Function

        Public Function addColdFeedInfo(feedInfo As FeedInfoNode) As FeedInfoNode

            ' Remove corresponding unknownfeed
            For Each _unknownFeed As UnknownFeedNode In Me.UNKNOWN_FEEDS

                If (_unknownFeed.TAG.Equals(Cycle.COLD_FEED_TAG) AndAlso _unknownFeed.LOCATION.Equals(feedInfo.LOCATION)) Then
                    removeUnknownFeed(_unknownFeed)
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
        End Sub

        Public Function addUnknownFeed(location As String, material As String, tag As Tag) As UnknownFeedNode

            Dim addToList As Boolean = True

            For Each unknownFeed In Me.UNKNOWN_FEEDS

                If (unknownFeed.LOCATION.Equals(location) AndAlso unknownFeed.TAG.Equals(tag)) Then

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

        Public Overrides Function verifyTag(tagName As String, isSubColumn As Boolean) As Tag

            Debugger.Break()

            'If (isSubColumn) Then

            '    For Each tagObject In Feeder.TAGS

            '        If (tagName.Equals(tagObject.TAG_NAME)) Then
            '            Return tagObject
            '        End If

            '    Next

            '    For Each tagObject In CSVFeeder.TAGS

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

            '    For Each tagObject In CSVCycle.TAGS

            '        If (tagName.Equals(tagObject.TAG_NAME)) Then
            '            Return tagObject
            '        End If

            '    Next

            'End If

            'Throw New InvalidTagException("Invalid tag in csv -> " & tagName)

            Return Nothing

        End Function

        Public Overrides Function getUnitByTag(tag As Tag) As Unit

            For Each _data In DATA_LIST

                If (_data.TAG.Equals(tag)) Then
                    Return _data.UNIT
                End If

            Next

            For Each _data In Me.SUB_COLUMNS
                If (_data.TAG.Equals(tag)) Then
                    Return _data.UNIT
                End If
            Next

            Return OtherUnit.UNIT

        End Function

        Public Function getDataInfoByTag(tag As Tag, index As Integer) As DataInfo

            For Each dataInfo As DataInfo In Me.DATA_LIST

                If (dataInfo.TAG.Equals(tag)) Then

                    If (index = -1) Then

                        Return dataInfo

                    ElseIf (index = DirectCast(dataInfo, DataInfoNode).INDEX) Then

                        Return dataInfo

                    End If

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

                'For Each summaryFeedInfo In Constants.Input.MDB.AVAILABLE_FEEDINFO
                '    If (tag.Equals(summaryFeedInfo.TAG)) Then
                '        Return summaryFeedInfo
                '    End If
                'Next

            End If

            ' Unkown tag or unfound feed index
            Debugger.Break()
            Return Nothing

        End Function

        Public Function getTagByName(tagName As String) As Tag

            For Each tag In Cycle.TAGS

                If (tagName.Equals(tag.TAG_NAME)) Then
                    Return tag
                End If

            Next

            For Each tag In CSVCycle.TAGS

                If (tagName.Equals(tag.TAG_NAME)) Then
                    Return tag
                End If

            Next

            Debugger.Break()
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
    End Class ' Ends csvNode

End Namespace ' Ends XmlSettings 