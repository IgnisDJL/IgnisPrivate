Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class MindsContinueDataPlant
    Inherits DataPlant

    ' Constants
    Private LOG_FILES_CULTURE As Globalization.CultureInfo = Globalization.CultureInfo.CreateSpecificCulture("en-US")

    Public Sub New(pathToLOGFile As String)

        MyBase.New(pathToLOGFile)
        productionDate = getDate()

    End Sub

    Public Overrides Function getDate()
        Dim regex As New Regex(Constants.Input.LOG.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(Me.getFileInfo.Name)

        If (match.Success) Then

            Dim day As Integer = Integer.Parse(match.Groups(LOG.LOGFileNameRegexDateIndex.DAY).Value)
            Dim month As Integer = Integer.Parse(match.Groups(LOG.LOGFileNameRegexDateIndex.MONTH).Value)
            Dim year As Integer = Integer.Parse(match.Groups(LOG.LOGFileNameRegexDateIndex.YEAR).Value)

            Return New Date(year, month, day)

        Else

            ' #exception

            Return Nothing
        End If
    End Function


    Public Overrides Function getCycles(startTime As Date, endTime As Date) As List(Of Cycle)

        Dim defaultCulture = Threading.Thread.CurrentThread.CurrentCulture
        Threading.Thread.CurrentThread.CurrentCulture = LOG_FILES_CULTURE

        analysisStarted(Me)

        Dim readingStream As System.IO.StreamReader = Nothing

        Dim cycleList As New List(Of Cycle)

        Dim stringFile As String = Nothing

        Dim retryOpeningFile As Boolean
        Do
            retryOpeningFile = False
            Try

                ' Open a stream to the file
                readingStream = New System.IO.StreamReader(Me.getFileInfo.FullName)

                ' Download file in memory
                stringFile = readingStream.ReadToEnd

            Catch ex As IO.IOException

                ' #exception
                Debugger.Break()

            Catch ex As Threading.ThreadAbortException

                If (Not IsNothing(readingStream)) Then
                    readingStream.Close()
                End If

                Return Nothing

            End Try

        Loop While (retryOpeningFile)

        If (Not IsNothing(readingStream)) Then
            readingStream.Close()
        End If

        If (stringFile.StartsWith("Time")) Then

            cycleList = Me.getCyclesEn(stringFile)
        ElseIf (stringFile.StartsWith("Heure")) Then

            cycleList = Me.getCyclesFr(stringFile)
        End If

        analysisStoped(Me)


        Threading.Thread.CurrentThread.CurrentCulture = defaultCulture

        Return cycleList
    End Function

    Public Function getCyclesEn(stringFile As String) As List(Of Cycle)

        Dim tables() As String
        Dim cycleList As New List(Of Cycle)
        Dim tableLines() As String

        Dim emptyValueRegex = New Regex(Constants.Input.LOG.EMPTY_DATA_REGEX)

        Dim regex As Regex
        Dim match As Match

        Dim previousCycle As LOGCycle = Nothing

        Dim logNode = XmlSettings.Settings.instance.Usine.DataFiles.LOG

        Dim hotFeedsAccumulatedMassBuffer(Constants.Input.LOG.NUMBER_OF_HOT_FEEDS - 1) As Double
        Dim coldFeedsAccumulatedMassBuffer As Double() = Nothing

        ' Split into tables
        tables = stringFile.Split({"Time : "}, StringSplitOptions.None)

        ' Make regex for table2
        Dim table2RegexBuilder As New System.Text.StringBuilder()

        Dim table2Regex As String

        Dim feedIsUnknown As Boolean = True

        Dim tankIsUnknown As Boolean = True

        Dim recipeIsUnknown As Boolean = True

        Dim nbTablesToAnalyse As Integer = tables.GetLength(0) - 1

        ' tables start at 1 because of the file.split("Time :")
        For i = 1 To nbTablesToAnalyse

            analysisRunning(i, nbTablesToAnalyse)

            Dim cycle As New LOGCycle(previousCycle)

            Try

                tableLines = tables(i).Split({vbCrLf, vbLf}, StringSplitOptions.None)

                ' First line of the table
                regex = New Regex("^([\d][\d]?:[\d][\d]:[\d][\d]([\s](AM|PM))?)")
                Dim timeStr = regex.Match(tableLines(0)).Groups(1).Value

                regex = New Regex("Date : ([\d][\d]?/[\d][\d]?/[\d]{4})")
                Dim dateStr = regex.Match(tableLines(0)).Groups(1).Value
                cycle.TIME = DateTime.Parse(dateStr & " " & timeStr)
                cycle.DATE_ = DateTime.Parse(dateStr)

                regex = New Regex("Mix Tph[\s]+([\d]{2,3})")
                cycle.PRODUCTION_SPEED = regex.Match(tableLines(0)).Groups(1).Value

                regex = New Regex("Ac Specific Gravity[\s]+([\d].[\d]{3})")
                cycle.DENSITY = regex.Match(tableLines(0)).Groups(1).Value

                regex = New Regex("Bh Diff:[\s](\-?[\d]{1,3}.[\d])")
                cycle.BAG_HOUSE_DIFF = regex.Match(tableLines(0)).Groups(1).Value


                ' Skip 2nd, 3rd and 4th line

                Dim hotFeeds(XmlSettings.LOGNode.NUMBER_OF_HOT_FEEDS - 1) As LOGFeeder

                For k = 0 To XmlSettings.LOGNode.NUMBER_OF_HOT_FEEDS - 1
                    hotFeeds(k) = New LOGFeeder(cycle) With {.INDEX = k + 1}
                Next


                ' 5th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")


                match = regex.Match(tableLines(4))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).SET_POINT_PERCENTAGE = Double.NaN
                    Else
                        hotFeeds(j - 1).SET_POINT_PERCENTAGE = match.Groups(j).Value
                    End If


                Next

                regex = New Regex("Mix Tons :[\s]+([\d]+)[\s]T")
                cycle.TOTAL_ACCUMULATED_TONS = regex.Match(tableLines(4)).Groups(1).Value

                ' 6th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(5))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).PERCENTAGE = Double.NaN
                    Else
                        hotFeeds(j - 1).PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                regex = New Regex("Mix Number :[\s]+([a-zA-Z0-9\s\-_%]+)[\s]+Mix Name :[\s]([a-zA-Z0-9\s\-_%]+)")
                match = regex.Match(tableLines(5))

                cycle.MIX_NAME = match.Groups(2).Value ' Important to set the mixName first beacause setting the formula may affect the name
                cycle.FORMULA_NAME = match.Groups(1).Value

                recipeIsUnknown = True

                For Each recipeInfo In XmlSettings.Settings.instance.Usine.RecipesInfo.RECIPES

                    If (cycle.FORMULA_NAME.Equals(recipeInfo.FORMULA)) Then

                        cycle.MIX_NAME = recipeInfo.MIX_NAME
                        cycle.RECYCLED_SET_POINT_PERCENTAGE = recipeInfo.RECYCLED_SET_POINT_PERCENTAGE

                        recipeIsUnknown = False
                        Exit For
                    End If

                Next

                If (recipeIsUnknown) Then
                    XmlSettings.Settings.instance.Usine.RecipesInfo.addUnknownRecipe(cycle.FORMULA_NAME, cycle.MIX_NAME)
                End If


                ' 7th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(6))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).PRODUCTION_SPEED = Double.NaN
                    Else
                        hotFeeds(j - 1).PRODUCTION_SPEED = match.Groups(j).Value
                    End If

                Next

                regex = New Regex("Asphalt Temp :[\s]+(\-?[\d]*)")
                match = regex.Match(tableLines(6))

                cycle.ASPHALT_TEMPERATURE = match.Groups(1).Value


                regex = New Regex("A/C Tank:[\s]([\d]+)")
                match = regex.Match(tableLines(6))

                cycle.ASPHALT_TANK = match.Groups(1).Value
                cycle.ASPHALT_NAME = match.Groups(1).Value ' By default... maybe will be overwirtten in settings

                tankIsUnknown = True

                For Each tankInfo In XmlSettings.Settings.instance.Usine.AsphaltInfo.TANKS

                    If (cycle.ASPHALT_TANK.Equals(tankInfo.TANK_NAME)) Then
                        cycle.ASPHALT_NAME = tankInfo.ASPHALT_NAME
                        cycle.SET_POINT_TEMPERATURE = tankInfo.SET_POINT_TEMP

                        tankIsUnknown = False
                        Exit For
                    End If

                Next

                If (tankIsUnknown) Then
                    XmlSettings.Settings.instance.Usine.AsphaltInfo.addUnknownTank(cycle.ASPHALT_TANK, cycle.ASPHALT_NAME)
                End If

                ' 8th line
                regex = New Regex(".{12}([\s|\d|\-]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d|\-]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d|\-]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(7))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).ACCUMULATED_MASS = Double.NaN
                    Else

                        If (Not IsNothing(previousCycle) AndAlso previousCycle.HOT_FEEDS(j - 1).ACCUMULATED_MASS > CDbl(match.Groups(j).Value) + hotFeedsAccumulatedMassBuffer(j - 1)) Then
                            hotFeedsAccumulatedMassBuffer(j - 1) = previousCycle.HOT_FEEDS(j - 1).ACCUMULATED_MASS
                        End If

                        hotFeeds(j - 1).ACCUMULATED_MASS = match.Groups(j).Value + hotFeedsAccumulatedMassBuffer(j - 1)

                        ' cycle mass
                        If (IsNothing(previousCycle)) Then
                            hotFeeds(j - 1).MASS = hotFeeds(j - 1).ACCUMULATED_MASS
                        Else
                            hotFeeds(j - 1).MASS = hotFeeds(j - 1).ACCUMULATED_MASS - previousCycle.HOT_FEEDS(j - 1).ACCUMULATED_MASS
                        End If

                    End If

                Next

                regex = New Regex("Mix Temp :[\s]+([\-]?[\d]+)")

                cycle.TEMPERATURE = regex.Match(tableLines(7)).Groups(1).Value

                ' 9th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(8))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).MOISTURE_PERCENTAGE = Nothing
                    Else
                        hotFeeds(j - 1).MOISTURE_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                regex = New Regex("Silo Filling:[\s]+([\d]+)")
                cycle.SILO_FILLING = regex.Match(tableLines(8)).Groups(1).Value


                ' 10th line
                regex = New Regex("Dust Removal:[\s]+([\d]{1,2}\.[\d]+)")
                cycle.DUST_REMOVAL = regex.Match(tableLines(9)).Groups(1).Value


                ' Skip 11th

                ' 12th line

                ' The string split works fine but we have to ignore the first element.
                ' The form of the feedsLocations array is {" ", title1, title2, ... , lastTitle}
                Dim feedsLocations As String() = tableLines(11).Split({" "}, System.StringSplitOptions.RemoveEmptyEntries)

                'regex = New Regex(table2TitleRegex)
                'match = regex.Match(tableLines(11))

                ' -2 because the first element is not needed
                Dim coldFeeds(feedsLocations.Count - 2) As LOGFeeder

                ' starts at 1 because the first element is ignored
                For j = 1 To feedsLocations.Count - 1

                    Dim currentFeed As New LOGFeeder(cycle)
                    currentFeed.INDEX = j

                    currentFeed.LOCATION = feedsLocations(j)

                    feedIsUnknown = True

                    ' Settings
                    For Each feedInfo In logNode.COLD_FEEDS

                        If (currentFeed.LOCATION.Equals(feedInfo.LOCATION)) Then

                            currentFeed.MATERIAL_NAME = feedInfo.MATERIAL
                            currentFeed.IS_FILLER = feedInfo.IS_FILLER
                            currentFeed.IS_RECYCLED = feedInfo.IS_RECYCLED

                            feedIsUnknown = False
                            Exit For
                        End If

                    Next

                    If (feedIsUnknown) Then
                        logNode.addUnknownFeed(currentFeed.LOCATION, currentFeed.MATERIAL_NAME, IGNIS.Cycle.COLD_FEED_TAG)
                    End If

                    coldFeeds(j - 1) = currentFeed
                Next

                ' Build the regex for the next rows
                table2RegexBuilder.Clear()
                For j = 1 To feedsLocations.Count - 1
                    table2RegexBuilder.Append("([\s|\d]{4}[\s|.][\s|\d])[\s]{2}")
                Next

                table2Regex = table2RegexBuilder.ToString

                If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

                    ReDim coldFeedsAccumulatedMassBuffer(feedsLocations.Count - 2)

                End If

                ' Skip 13th line

                ' 14th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(13))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).SET_POINT_PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).SET_POINT_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                ' 15th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(14))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                ' 16th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(15))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).PRODUCTION_SPEED = Double.NaN
                    Else
                        coldFeeds(j - 1).PRODUCTION_SPEED = match.Groups(j).Value
                    End If

                Next

                ' 17th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(16))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).ACCUMULATED_MASS = Double.NaN
                    Else

                        If (Not IsNothing(previousCycle) AndAlso previousCycle.COLD_FEEDS(j - 1).ACCUMULATED_MASS > CDbl(match.Groups(j).Value) + coldFeedsAccumulatedMassBuffer(j - 1)) Then
                            coldFeedsAccumulatedMassBuffer(j - 1) = previousCycle.COLD_FEEDS(j - 1).ACCUMULATED_MASS
                        End If

                        coldFeeds(j - 1).ACCUMULATED_MASS = match.Groups(j).Value + coldFeedsAccumulatedMassBuffer(j - 1)

                        If (IsNothing(previousCycle)) Then
                            coldFeeds(j - 1).MASS = coldFeeds(j - 1).ACCUMULATED_MASS
                        Else
                            coldFeeds(j - 1).MASS = coldFeeds(j - 1).ACCUMULATED_MASS - previousCycle.COLD_FEEDS(j - 1).ACCUMULATED_MASS
                        End If
                    End If

                Next

                ' 18th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(17))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).MOISTURE_PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).MOISTURE_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                ' 19th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(18))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).RECYCLED_ASPHALT_PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).RECYCLED_ASPHALT_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                cycle.AGGREGATES_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_AGGREGATES).MASS + _
                                        hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).MASS + _
                                        hotFeeds(Constants.Input.LOG.HotFeedsIndexes.FILLER).MASS + _
                                        hotFeeds(Constants.Input.LOG.HotFeedsIndexes.ADDITIVE).MASS


                cycle.ASPHALT_ACCUMULATED_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT).ACCUMULATED_MASS

                cycle.ASPHALT_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT).MASS

                cycle.ASPHALT_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.TOTAL_ASPHALT).PERCENTAGE

                cycle.ASPHALT_SET_POINT_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.TOTAL_ASPHALT).SET_POINT_PERCENTAGE


                cycle.MIX_MASS = cycle.AGGREGATES_MASS + hotFeeds(Constants.Input.LOG.HotFeedsIndexes.TOTAL_ASPHALT).MASS


                cycle.RECYCLED_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).MASS

                cycle.RECYCLED_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).PERCENTAGE

                cycle.RECYCLED_SET_POINT_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).SET_POINT_PERCENTAGE


                cycle.VIRGIN_AGGREGATES_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_AGGREGATES)

                cycle.RECYCLE_SUMMARY_FEED = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES)

                cycle.RAP_ASPHALT_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_ASPHALT)

                cycle.VIRGIN_ASPHALT_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT)

                cycle.ASPHALT_SUMMARY_FEED = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT)

                cycle.FILLER_SUMMARY_FEED = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.FILLER)

                cycle.ADDITIVE_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.ADDITIVE)



                If (IsNothing(cycle.PREVIOUS_CYCLE)) Then
                    cycle.MIX_ACCUMULATED_MASS = cycle.MIX_MASS
                Else
                    cycle.MIX_ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.MIX_ACCUMULATED_MASS + cycle.MIX_MASS
                End If

                For Each feed In hotFeeds
                    cycle.HOT_FEEDS.Add(feed)
                Next

                For Each feed In coldFeeds
                    cycle.COLD_FEEDS.Add(feed)
                Next

                ' Calculations
                If (Not IsNothing(cycle.PREVIOUS_CYCLE)) Then
                    cycle.DURATION = Events.rectifyCycleDuration(cycle)
                End If

                cycleList.Add(cycle)
                previousCycle = cycle

            Catch ex As IndexOutOfRangeException

                ' Insert in some problem table object
                Debugger.Break()

            Catch ex As InvalidCastException

                Dim newEx As New Exception("Incapable de lire le fichier '" & Me.getFileInfo.Name & "'. Format non reconnu.", ex)

                Throw newEx

            End Try

        Next ' End for each table

        Return cycleList
    End Function

    Public Function getCyclesFr(stringFile As String) As List(Of Cycle)

        Dim tables() As String
        Dim cycleList As New List(Of Cycle)
        Dim tableLines() As String

        Dim emptyValueRegex = New Regex(Constants.Input.LOG.EMPTY_DATA_REGEX)

        Dim regex As Regex
        Dim match As Match

        Dim previousCycle As LOGCycle = Nothing

        Dim logNode = XmlSettings.Settings.instance.Usine.DataFiles.LOG

        Dim hotFeedsAccumulatedMassBuffer(Constants.Input.LOG.NUMBER_OF_HOT_FEEDS - 1) As Double
        Dim coldFeedsAccumulatedMassBuffer As Double() = Nothing

        ' Split into tables
        tables = stringFile.Split({"Heure : "}, StringSplitOptions.None)

        ' Make regex for table2
        Dim table2RegexBuilder As New System.Text.StringBuilder()

        Dim table2Regex As String

        Dim feedIsUnknown As Boolean = True

        Dim tankIsUnknown As Boolean = True

        Dim recipeIsUnknown As Boolean = True

        Dim nbTablesToAnalyse As Integer = tables.GetLength(0) - 1

        ' tables start at 1 because of the file.split("Time :")
        For i = 1 To nbTablesToAnalyse

            analysisRunning(i, nbTablesToAnalyse)

            Dim cycle As New LOGCycle(previousCycle)

            Try

                tableLines = tables(i).Split({vbCrLf, vbLf}, StringSplitOptions.None)

                ' First line of the table
                regex = New Regex("^([\d][\d]?:[\d][\d]:[\d][\d]([\s](AM|PM))?)")
                Dim timeStr = regex.Match(tableLines(0)).Groups(1).Value

                regex = New Regex("Date : ([\d][\d]?/[\d][\d]?/[\d]{4})")
                Dim dateStr = regex.Match(tableLines(0)).Groups(1).Value
                cycle.TIME = DateTime.Parse(dateStr & " " & timeStr)
                cycle.DATE_ = DateTime.Parse(dateStr)

                regex = New Regex("D.bit[\s]+([\d]{2,3})")
                cycle.PRODUCTION_SPEED = regex.Match(tableLines(0)).Groups(1).Value

                regex = New Regex("Mass. Vol. Bit:[\s]+([\d].[\d]{3})")
                cycle.DENSITY = regex.Match(tableLines(0)).Groups(1).Value

                regex = New Regex("D.press:[\s](\-?[\d]{1,3}.[\d]{2})")
                cycle.BAG_HOUSE_DIFF = regex.Match(tableLines(0)).Groups(1).Value


                ' Skip 2nd, 3rd and 4th line

                Dim hotFeeds(XmlSettings.LOGNode.NUMBER_OF_HOT_FEEDS - 1) As LOGFeeder

                For k = 0 To XmlSettings.LOGNode.NUMBER_OF_HOT_FEEDS - 1
                    hotFeeds(k) = New LOGFeeder(cycle) With {.INDEX = k + 1}
                Next


                ' 5th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")


                match = regex.Match(tableLines(4))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).SET_POINT_PERCENTAGE = Double.NaN
                    Else
                        hotFeeds(j - 1).SET_POINT_PERCENTAGE = match.Groups(j).Value
                    End If


                Next

                regex = New Regex("Ton. Frm :[\s]+([\d]+)[\s]T")
                cycle.TOTAL_ACCUMULATED_TONS = regex.Match(tableLines(4)).Groups(1).Value

                ' 6th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(5))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).PERCENTAGE = Double.NaN
                    Else
                        hotFeeds(j - 1).PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                regex = New Regex("No. Frm. :[\s]+([a-zA-Z0-9\s\-_%]+)[\s]+Nom Frm. :[\s]([a-zA-Z0-9\s\-_%]+)")
                match = regex.Match(tableLines(5))

                cycle.MIX_NAME = match.Groups(2).Value ' Important to set the mixName first beacause setting the formula may affect the name
                cycle.FORMULA_NAME = match.Groups(1).Value

                recipeIsUnknown = True

                For Each recipeInfo In XmlSettings.Settings.instance.Usine.RecipesInfo.RECIPES

                    If (cycle.FORMULA_NAME.Equals(recipeInfo.FORMULA)) Then

                        cycle.MIX_NAME = recipeInfo.MIX_NAME
                        cycle.RECYCLED_SET_POINT_PERCENTAGE = recipeInfo.RECYCLED_SET_POINT_PERCENTAGE

                        recipeIsUnknown = False
                        Exit For
                    End If

                Next

                If (recipeIsUnknown) Then
                    XmlSettings.Settings.instance.Usine.RecipesInfo.addUnknownRecipe(cycle.FORMULA_NAME, cycle.MIX_NAME)
                End If


                ' 7th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(6))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).PRODUCTION_SPEED = Double.NaN
                    Else
                        hotFeeds(j - 1).PRODUCTION_SPEED = match.Groups(j).Value
                    End If

                Next

                regex = New Regex("Temp. Bitume :[\s]+(\-?[\d]*)")
                match = regex.Match(tableLines(6))

                cycle.ASPHALT_TEMPERATURE = match.Groups(1).Value


                regex = New Regex("Tank Bit:[\s]([\d]+)")
                match = regex.Match(tableLines(6))

                cycle.ASPHALT_TANK = match.Groups(1).Value
                cycle.ASPHALT_NAME = match.Groups(1).Value ' By default... maybe will be overwirtten in settings

                tankIsUnknown = True

                For Each tankInfo In XmlSettings.Settings.instance.Usine.AsphaltInfo.TANKS

                    If (cycle.ASPHALT_TANK.Equals(tankInfo.TANK_NAME)) Then
                        cycle.ASPHALT_NAME = tankInfo.ASPHALT_NAME
                        cycle.SET_POINT_TEMPERATURE = tankInfo.SET_POINT_TEMP

                        tankIsUnknown = False
                        Exit For
                    End If

                Next

                If (tankIsUnknown) Then
                    XmlSettings.Settings.instance.Usine.AsphaltInfo.addUnknownTank(cycle.ASPHALT_TANK, cycle.ASPHALT_NAME)
                End If

                ' 8th line
                regex = New Regex(".{12}([\s|\d|\-]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d|\-]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d|\-]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d|\-]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(7))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).ACCUMULATED_MASS = Double.NaN
                    Else

                        If (Not IsNothing(previousCycle) AndAlso previousCycle.HOT_FEEDS(j - 1).ACCUMULATED_MASS > CDbl(match.Groups(j).Value) + hotFeedsAccumulatedMassBuffer(j - 1)) Then
                            hotFeedsAccumulatedMassBuffer(j - 1) = previousCycle.HOT_FEEDS(j - 1).ACCUMULATED_MASS
                        End If

                        hotFeeds(j - 1).ACCUMULATED_MASS = match.Groups(j).Value + hotFeedsAccumulatedMassBuffer(j - 1)

                        ' cycle mass
                        If (IsNothing(previousCycle)) Then
                            hotFeeds(j - 1).MASS = hotFeeds(j - 1).ACCUMULATED_MASS
                        Else
                            hotFeeds(j - 1).MASS = hotFeeds(j - 1).ACCUMULATED_MASS - previousCycle.HOT_FEEDS(j - 1).ACCUMULATED_MASS
                        End If

                    End If

                Next

                regex = New Regex("Temp. Enr. :[\s]+([\-]?[\d]+)")

                cycle.TEMPERATURE = regex.Match(tableLines(7)).Groups(1).Value

                ' 9th line
                regex = New Regex(".{12}([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}" _
                                  & "([\s|\d]{4}[\s|.][\s|\d])[\s]{2}" _
                                  & "([\s|\d]{3}[\s|.][\s|\d]{2})[\s]{2}")

                match = regex.Match(tableLines(8))
                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        hotFeeds(j - 1).MOISTURE_PERCENTAGE = Nothing
                    Else
                        hotFeeds(j - 1).MOISTURE_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                regex = New Regex("Silo Utilis.:[\s]+([\d]+)")
                cycle.SILO_FILLING = regex.Match(tableLines(8)).Groups(1).Value


                ' 10th line
                regex = New Regex("Filler Recup:[\s]+([\d]{1,2}\.[\d]+)")
                cycle.DUST_REMOVAL = regex.Match(tableLines(9)).Groups(1).Value


                ' Skip 11th

                ' 12th line

                ' The string split works fine but we have to ignore the first element.
                ' The form of the feedsLocations array is {" ", title1, title2, ... , lastTitle}
                Dim feedsLocations As String() = tableLines(11).Split({" "}, System.StringSplitOptions.RemoveEmptyEntries)

                'regex = New Regex(table2TitleRegex)
                'match = regex.Match(tableLines(11))

                ' -2 because the first element is not needed
                Dim coldFeeds(feedsLocations.Count - 2) As LOGFeeder

                ' starts at 1 because the first element is ignored
                For j = 1 To feedsLocations.Count - 1

                    Dim currentFeed As New LOGFeeder(cycle)
                    currentFeed.INDEX = j

                    currentFeed.LOCATION = feedsLocations(j)

                    feedIsUnknown = True

                    ' Settings
                    For Each feedInfo In logNode.COLD_FEEDS

                        If (currentFeed.LOCATION.Equals(feedInfo.LOCATION)) Then

                            currentFeed.MATERIAL_NAME = feedInfo.MATERIAL
                            currentFeed.IS_FILLER = feedInfo.IS_FILLER
                            currentFeed.IS_RECYCLED = feedInfo.IS_RECYCLED

                            feedIsUnknown = False
                            Exit For
                        End If

                    Next

                    If (feedIsUnknown) Then
                        logNode.addUnknownFeed(currentFeed.LOCATION, currentFeed.MATERIAL_NAME, IGNIS.Cycle.COLD_FEED_TAG)
                    End If

                    coldFeeds(j - 1) = currentFeed
                Next

                ' Build the regex for the next rows
                table2RegexBuilder.Clear()
                For j = 1 To feedsLocations.Count - 1
                    table2RegexBuilder.Append("([\s|\d]{4}[\s|.][\s|\d])[\s]{2}")
                Next

                table2Regex = table2RegexBuilder.ToString

                If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

                    ReDim coldFeedsAccumulatedMassBuffer(feedsLocations.Count - 2)

                End If

                ' Skip 13th line

                ' 14th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(13))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).SET_POINT_PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).SET_POINT_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                ' 15th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(14))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                ' 16th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(15))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).PRODUCTION_SPEED = Double.NaN
                    Else
                        coldFeeds(j - 1).PRODUCTION_SPEED = match.Groups(j).Value
                    End If

                Next

                ' 17th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(16))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).ACCUMULATED_MASS = Double.NaN
                    Else

                        If (Not IsNothing(previousCycle) AndAlso previousCycle.COLD_FEEDS(j - 1).ACCUMULATED_MASS > CDbl(match.Groups(j).Value) + coldFeedsAccumulatedMassBuffer(j - 1)) Then
                            coldFeedsAccumulatedMassBuffer(j - 1) = previousCycle.COLD_FEEDS(j - 1).ACCUMULATED_MASS
                        End If

                        coldFeeds(j - 1).ACCUMULATED_MASS = match.Groups(j).Value + coldFeedsAccumulatedMassBuffer(j - 1)

                        If (IsNothing(previousCycle)) Then
                            coldFeeds(j - 1).MASS = coldFeeds(j - 1).ACCUMULATED_MASS
                        Else
                            coldFeeds(j - 1).MASS = coldFeeds(j - 1).ACCUMULATED_MASS - previousCycle.COLD_FEEDS(j - 1).ACCUMULATED_MASS
                        End If
                    End If

                Next

                ' 18th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(17))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).MOISTURE_PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).MOISTURE_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                ' 19th line
                regex = New Regex(".{12}" & table2Regex)
                match = regex.Match(tableLines(18))

                For j = 1 To match.Groups.Count - 1

                    If (emptyValueRegex.Match(match.Groups(j).Value).Success) Then
                        coldFeeds(j - 1).RECYCLED_ASPHALT_PERCENTAGE = Double.NaN
                    Else
                        coldFeeds(j - 1).RECYCLED_ASPHALT_PERCENTAGE = match.Groups(j).Value
                    End If

                Next

                cycle.AGGREGATES_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_AGGREGATES).MASS + _
                                        hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).MASS + _
                                        hotFeeds(Constants.Input.LOG.HotFeedsIndexes.FILLER).MASS + _
                                        hotFeeds(Constants.Input.LOG.HotFeedsIndexes.ADDITIVE).MASS


                cycle.ASPHALT_ACCUMULATED_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT).ACCUMULATED_MASS

                cycle.ASPHALT_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT).MASS

                cycle.ASPHALT_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.TOTAL_ASPHALT).PERCENTAGE

                cycle.ASPHALT_SET_POINT_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.TOTAL_ASPHALT).SET_POINT_PERCENTAGE


                cycle.MIX_MASS = cycle.AGGREGATES_MASS + hotFeeds(Constants.Input.LOG.HotFeedsIndexes.TOTAL_ASPHALT).MASS


                cycle.RECYCLED_MASS = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).MASS

                cycle.RECYCLED_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).PERCENTAGE

                cycle.RECYCLED_SET_POINT_PERCENTAGE = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES).SET_POINT_PERCENTAGE


                cycle.VIRGIN_AGGREGATES_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_AGGREGATES)

                cycle.RECYCLE_SUMMARY_FEED = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_AGGREGATES)

                cycle.RAP_ASPHALT_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.RECYCLED_ASPHALT)

                cycle.VIRGIN_ASPHALT_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT)

                cycle.ASPHALT_SUMMARY_FEED = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.VIRGIN_ASPHALT)

                cycle.FILLER_SUMMARY_FEED = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.FILLER)

                cycle.ADDITIVE_FEEDER = hotFeeds(Constants.Input.LOG.HotFeedsIndexes.ADDITIVE)



                If (IsNothing(cycle.PREVIOUS_CYCLE)) Then
                    cycle.MIX_ACCUMULATED_MASS = cycle.MIX_MASS
                Else
                    cycle.MIX_ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.MIX_ACCUMULATED_MASS + cycle.MIX_MASS
                End If

                For Each feed In hotFeeds
                    cycle.HOT_FEEDS.Add(feed)
                Next

                For Each feed In coldFeeds
                    cycle.COLD_FEEDS.Add(feed)
                Next

                ' Calculations
                If (Not IsNothing(cycle.PREVIOUS_CYCLE)) Then
                    cycle.DURATION = Events.rectifyCycleDuration(cycle)
                End If

                cycleList.Add(cycle)
                previousCycle = cycle

            Catch ex As IndexOutOfRangeException

                ' Insert in some problem table object
                Debugger.Break()

            Catch ex As InvalidCastException

                Dim newEx As New Exception("Incapable de lire le fichier '" & Me.getFileInfo.Name & "'. Format non reconnu.", ex)

                Throw newEx

            End Try

        Next ' End for each table

        Return cycleList
    End Function
End Class
