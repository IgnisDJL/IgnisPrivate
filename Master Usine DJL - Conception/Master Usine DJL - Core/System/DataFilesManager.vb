Imports System.IO
Imports System.Text.RegularExpressions
Imports IGNIS.XmlSettings

''' <summary>
''' Extracts the data from the data files.
''' </summary>
''' <remarks> </remarks>
Public Class DataFilesManager

    Private Const CSV_DATA_SEPERATOR = ","c


    Private Shared faultyRows As New List(Of String())

    ''' <summary>
    ''' Returns the data from each files in the CSV directory.
    ''' </summary>
    ''' <returns>A linked list of String arrays, representing every lines containing an array of their values as Strings</returns>
    ''' <remarks>
    ''' Doesn't return the first and the last 2 lines of
    ''' each files.
    ''' </remarks>
    Public Shared Function getCSVData(file As FileInfo) As List(Of CSVCycle)

        Dim dataFileNode As CSVNode = XmlSettings.Settings.instance.Usine.DataFiles.CSV

        If (Not dataFileNode.ACTIVE) Then
            Throw New InactiveDataFileException("According to the program's settings, the .csv files are inacive.")
        End If

        Dim readingStream As System.IO.StreamReader

        Dim cycleList As New List(Of CSVCycle)
        Dim rows() As String
        Dim row() As String
        Dim nbCols = dataFileNode.DATA_FORMAT.Count

        Dim fileExtensionRegex = New System.Text.RegularExpressions.Regex("(\.csv)$")

        Dim fileDate = DataFilesManager.convertCSVFileNameToDate(file.Name)

        Dim addCycle As Boolean

        Dim stringFile As String = Nothing

        Dim retryOpeningFile As Boolean
        Do
            retryOpeningFile = False
            Try

                ' Open a stream to the file
                readingStream = New System.IO.StreamReader(file.FullName)

                ' Download file in memory
                stringFile = readingStream.ReadToEnd

            Catch ex As IO.IOException

                If (UIExceptionHandler.instance.handle(ex)) Then
                    retryOpeningFile = True
                Else

                    Dim del As ReportGenerationControl.CancelReportFromInsideDelegateMethod = AddressOf ReportGenerationControl.instance.cancelReportMaking
                    MainWindow.instance.Invoke(del)

                End If

            End Try

        Loop While (retryOpeningFile)

        ' Array of rows for a single file
        rows = stringFile.Split(Environment.NewLine)

        Dim incorrectValueRegex = New Regex(Constants.Input.CSV.EMPTY_VALUE_REGEX)

        Dim previousCycle As CSVCycle = Nothing

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Importation des données du .csv"
        Dim progressionBarStep As Double = 1 / (rows.Length - 4) * 5 ' The 5 means this part is about 5% of the whole process

        ' Add the rows to the list of rows
        ' Starting at 1 to skip the first line
        ' Stopping at rows.lenght - 3 because the last 2 lines are empty
        For i = 1 To rows.Length - 3

            ReportGenerationControl.instance.addStep(progressionBarStep)

            addCycle = True

            row = rows(i).Split(CSV_DATA_SEPERATOR)

            ' #comment
            If (nbCols.Equals(row.Length)) Then

                Dim cycle As New CSVCycle(previousCycle)

                cycle.DATE_ = fileDate

                ReDim cycle.AGGREGATE_MASS(dataFileNode.NB_AGGREGATE_MASS_COLUMNS - 1)
                ReDim cycle.AGGREGATE_PERCENTAGE(dataFileNode.NB_AGGREGATE_PERCENTAGE_COLUMNS - 1)
                ReDim cycle.COLDFEED_PERCENTAGE(dataFileNode.NB_COLDFEED_PERCENTAGE_COLUMNS - 1)

                For Each feedInfo In dataFileNode.HOT_FEEDS

                    cycle.HOT_FEEDS.Add(New CSVFeeder(cycle) _
                                        With {.INDEX = feedInfo.INDEX, _
                                              .LOCATION = feedInfo.LOCATION, _
                                              .MATERIAL_NAME = feedInfo.MATERIAL, _
                                              .IS_FILLER = feedInfo.IS_FILLER, _
                                              .IS_RECYCLED = feedInfo.IS_RECYCLED})

                Next

                For Each feedInfo In dataFileNode.COLD_FEEDS
                    cycle.COLD_FEEDS.Add(New CSVFeeder(cycle) _
                                         With {.INDEX = feedInfo.INDEX, _
                                               .LOCATION = feedInfo.LOCATION, _
                                               .MATERIAL_NAME = feedInfo.MATERIAL, _
                                               .IS_RECYCLED = feedInfo.IS_RECYCLED})
                Next

                ' For each column...
                For j = 0 To row.Length - 1

                    Dim dataValue As String = Nothing

                    Try
                        If (Not CStr(row(j)).Equals("""""")) Then
                            dataValue = row(j)
                        End If

                    Catch ex As InvalidCastException
                        'do something later
                        Debugger.Break()
                        Console.WriteLine(ex.Message)
                    End Try

                    Dim dataInfo As DataInfoNode = XmlSettings.Settings.instance.Usine.DataFiles.CSV.DATA_FORMAT(j)

                    Select Case dataInfo.TAG

                        Case CSVCycle.AGGREGATE_MASS_TAG

                            cycle.AGGREGATE_MASS(dataInfo.index - 1) = dataValue

                            For k = 0 To cycle.HOT_FEEDS.Count - 1

                                If (cycle.HOT_FEEDS(k).INDEX.Equals(dataInfo.index)) Then

                                    cycle.HOT_FEEDS(k).MASS = CDbl(dataValue)

                                    If (IsNothing(cycle.PREVIOUS_CYCLE)) Then
                                        cycle.HOT_FEEDS(k).ACCUMULATED_MASS = CDbl(dataValue)
                                    Else
                                        cycle.HOT_FEEDS(k).ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.HOT_FEEDS(k).ACCUMULATED_MASS + CDbl(dataValue)
                                    End If

                                    If (DirectCast(cycle.HOT_FEEDS(k), CSVFeeder).IS_RECYCLED) Then
                                        cycle.RECYCLED_MASS = cycle.HOT_FEEDS(k).MASS
                                    End If

                                End If
                            Next

                        Case CSVCycle.AGGREGATE_PERCENTAGE_TAG

                            cycle.AGGREGATE_PERCENTAGE(dataInfo.index - 1) = dataValue

                            For Each feed In cycle.HOT_FEEDS
                                If (feed.INDEX.Equals(dataInfo.index)) Then
                                    DirectCast(feed, CSVFeeder).PERCENTAGE = dataValue

                                    If (DirectCast(feed, CSVFeeder).IS_RECYCLED) Then
                                        cycle.RECYCLED_PERCENTAGE = dataValue
                                    End If


                                End If
                            Next

                        Case CSVCycle.DOPE_MASS_TAG
                            cycle.DOPE_MASS = dataValue

                        Case IGNIS.Cycle.ASPHALT_MASS_TAG
                            cycle.ASPHALT_MASS = dataValue

                        Case CSVCycle.FILLER_APPORT_TAG
                            For Each feed In cycle.HOT_FEEDS
                                If (DirectCast(feed, CSVFeeder).IS_FILLER) Then
                                    DirectCast(feed, CSVFeeder).PERCENTAGE += dataValue
                                End If
                            Next
                            cycle.FILLER_APPORT = dataValue

                        Case CSVCycle.FILLER_RECUP_TAG
                            For Each feed In cycle.HOT_FEEDS
                                If (DirectCast(feed, CSVFeeder).IS_FILLER) Then
                                    DirectCast(feed, CSVFeeder).PERCENTAGE -= dataValue
                                End If
                            Next
                            cycle.FILLER_RECUP = dataValue

                        Case CSVCycle.COLD_FEED_PERCENTAGE_TAG

                            cycle.COLDFEED_PERCENTAGE(dataInfo.index - 1) = dataValue

                            For Each feed In cycle.COLD_FEEDS
                                If (feed.INDEX.Equals(dataInfo.index)) Then
                                    DirectCast(feed, CSVFeeder).PERCENTAGE = dataValue
                                End If
                            Next

                        Case IGNIS.Cycle.ASPHALT_TANK_TAG
                            cycle.ASPHALT_TANK = dataValue
                            cycle.ASPHALT_NAME = dataValue

                        Case IGNIS.Cycle.ASPHALT_PERCENTAGE_TAG
                            cycle.ASPHALT_PERCENTAGE = dataValue

                        Case CSVCycle.DOPE_PERCENTAGE_TAG
                            cycle.DOPE_PERCENTAGE = dataValue

                        Case CSVCycle.ASPHALT_TEMPERATURE_TAG
                            cycle.ASPHALT_TEMPERATURE = dataValue

                        Case CSVCycle.SILO_TAG
                            cycle.SILO = dataValue

                        Case CSVCycle.ID_TAG
                            cycle.ID = CInt(dataValue)

                        Case CSVCycle.COMMAND_ID_TAG
                            cycle.COMMAND_ID = dataValue

                        Case CSVCycle.TRUCK_ID_TAG
                            cycle.TRUCK_ID = dataValue

                        Case IGNIS.Cycle.MIX_NAME_TAG
                            cycle.MIX_NAME = dataValue

                        Case IGNIS.Cycle.MIX_FORMULA_NAME_TAG
                            cycle.FORMULA_NAME = dataValue

                        Case IGNIS.Cycle.AGGREGATES_MASS_TAG
                            cycle.AGGREGATES_MASS = dataValue

                        Case IGNIS.Cycle.MIX_MASS_TAG
                            cycle.MIX_MASS = dataValue

                            If (IsNothing(cycle.PREVIOUS_CYCLE)) Then
                                cycle.MIX_ACCUMULATED_MASS = dataValue
                            Else
                                cycle.MIX_ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.MIX_ACCUMULATED_MASS + dataValue
                            End If

                        Case IGNIS.Cycle.TEMPERATURE_TAG
                            cycle.TEMPERATURE = dataValue

                        Case CSVCycle.WET_MALAXING_TIME_TAG
                            cycle.WET_MALAXING_TIME = TimeSpan.FromSeconds(CDbl(dataValue))

                        Case IGNIS.Cycle.DURATION_TAG
                            cycle.DURATION = TimeSpan.FromSeconds(CDbl(dataValue))

                        Case IGNIS.Cycle.TIME_TAG ' Important to set the date before the time
                            cycle.TIME = DateTime.Parse(fileDate.ToString("yyyy-MM-dd") & " " & dataValue)


                    End Select


                Next

                ' Calculations
                cycle.PRODUCTION_SPEED = cycle.MIX_MASS / cycle.DURATION.TotalHours

                ' Settings!!!
                For Each recipeInfo In XmlSettings.Settings.instance.Usine.RecipesInfo.RECIPES

                    If (recipeInfo.match(cycle.FORMULA_NAME)) Then

                        cycle.MIX_NAME = recipeInfo.MIX_NAME
                        cycle.RECYCLED_SET_POINT_PERCENTAGE = recipeInfo.RECYCLED_SET_POINT_PERCENTAGE
                        cycle.ASPHALT_SET_POINT_PERCENTAGE = recipeInfo.ASPHALT_SET_POINT_PERCENTAGE
                        Exit For

                    End If
                Next

                For Each tankInfo As XmlSettings.AsphaltNode.TankInfo In XmlSettings.Settings.instance.Usine.AsphaltInfo.TANKS

                    If (cycle.ASPHALT_TANK.Equals(tankInfo.TANK_NAME)) Then

                        cycle.ASPHALT_NAME = tankInfo.ASPHALT_NAME
                        cycle.SET_POINT_TEMPERATURE = tankInfo.SET_POINT_TEMP

                        Exit For
                    End If

                Next
                ' End Settings!!!

                If (addCycle) Then
                    cycleList.Add(cycle)
                    previousCycle = cycle
                End If

            Else

                ' Dont forget this...
                faultyRows.Add(row)

            End If

        Next

        Return cycleList

    End Function

    ''' <summary>
    ''' Converts the name of the .csv file, wich contains the date when data was gathered, into a DateTime object
    ''' </summary>
    ''' <param name="fileName">The name of the file (just the name not the full path to the file)</param>
    ''' <returns>The date time object representing the file's date</returns>
    ''' <remarks>Format of the file : dd-mm-yyyy_someText.csv</remarks>
    Private Shared Function convertCSVFileNameToDate(fileName As String) As Date

        Dim regex = New System.Text.RegularExpressions.Regex("^(([\d][\d])-([\d][\d])-([\d]{4}))")
        Dim match = regex.Match(fileName)

        If (Not match.Success) Then
            Throw New DataFileNameException(fileName, ".csv")
        End If

        Dim day = CInt(match.Groups(2).Value)
        Dim month = CInt(match.Groups(3).Value)
        Dim year = CInt(match.Groups(4).Value)

        Return New Date(year, month, day)

    End Function

    ''' <summary>
    ''' Returns the data from each files in the LOG directory.
    ''' </summary>
    ''' <returns>A linked list of String arrays, representing every lines containing an array of their values as Strings</returns>
    ''' <remarks>
    ''' This method only returns the data in the file, not the table header or anything else.
    ''' 
    ''' ** This method should go in the constants class
    ''' </remarks>
    Public Shared Function getLOGData(file As FileInfo) As List(Of LOGCycle)

        If (Not XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE) Then
            Throw New InactiveDataFileException("According to the program's settings, the .log files are inacive.")
        End If

        Dim readingStream As System.IO.StreamReader

        Dim tables() As String
        Dim cycleList As New List(Of LOGCycle)
        Dim tableLines() As String

        Dim emptyValueRegex = New Regex(Constants.Input.LOG.EMPTY_DATA_REGEX)

        Dim regex As Regex
        Dim match As Match

        Dim previousCycle As LOGCycle = Nothing

        Dim logNode = XmlSettings.Settings.instance.Usine.DataFiles.LOG

        Dim hotFeedsAccumulatedMassBuffer(Constants.Input.LOG.NUMBER_OF_HOT_FEEDS - 1) As Double
        Dim coldFeedsAccumulatedMassBuffer As Double()

        Dim stringFile As String = Nothing

        Dim retryOpeningFile As Boolean
        Do
            retryOpeningFile = False
            Try

                ' Open a stream to the file
                readingStream = New System.IO.StreamReader(file.FullName)

                ' Download file in memory
                stringFile = readingStream.ReadToEnd

            Catch ex As IO.IOException

                If (UIExceptionHandler.instance.handle(ex)) Then
                    retryOpeningFile = True
                Else

                    Dim del As ReportGenerationControl.CancelReportFromInsideDelegateMethod = AddressOf ReportGenerationControl.instance.cancelReportMaking
                    MainWindow.instance.Invoke(del)

                End If

            End Try

        Loop While (retryOpeningFile)

        ' Split into tables
        tables = stringFile.Split({"Time : "}, StringSplitOptions.None)

        ' Make regex for table2
        Dim table2RegexBuilder As New System.Text.StringBuilder()

        Dim table2Regex As String

        ' Progress bar
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Importation des données du .log"
        Dim progressionBarStep As Double = 1 / (tables.GetLength(0) - 1) * 5 ' The 5 means this part is about 5% of the whole process

        Dim feedIsUnknown As Boolean = True

        Dim tankIsUnknown As Boolean = True

        Dim recipeIsUnknown As Boolean = True

        ' tables start at 1 because of the file.split("Time :")
        For i = 1 To tables.GetLength(0) - 1

            Dim cycle As New LOGCycle(previousCycle)

            ' Progress bar
            ReportGenerationControl.instance.addStep(progressionBarStep)

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

                Dim hotFeeds(logNode.NUMBER_OF_HOT_FEEDS - 1) As LOGFeeder

                For k = 0 To logNode.NUMBER_OF_HOT_FEEDS - 1
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

                ' #askM - virginAsphalt or totalAsphalt?
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

                ' Throw some problem reading log file exception
                Debugger.Break()
                Throw ex

            End Try

        Next ' End for each table

        Return cycleList

    End Function

    ''' <summary>
    ''' #comment
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getMDBData(file As FileInfo, startDate As Date) As List(Of MDBCycle)

        Dim cycleList As New List(Of MDBCycle)
        Dim previousCycle As MDBCycle = Nothing

        Dim endDate = startDate.Add(TimeSpan.FromDays(1))

        Dim mdbNode As XmlSettings.MDBNode = XmlSettings.Settings.instance.Usine.DataFiles.MDB

        OleDBAdapter.initialize(file)

        ' For the progress bar...
        Dim cycleCountQuery = "SELECT COUNT(" & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.CYCLE_ID & ") FROM " & Constants.Input.MDB.Tables.CYCLE & " WHERE " & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.DATE_TIME & " BETWEEN #" & startDate.ToString("yyyy/MM/dd") & "# AND #" & endDate.ToString("yyyy/MM/dd") & "#"
        Dim cycleCount = CInt(New System.Data.OleDb.OleDbCommand(cycleCountQuery, OleDBAdapter.MDB_CONNECTION).ExecuteScalar)
        Dim progressBarStep As Double = 1 / cycleCount * 10 ' I mesured that this section is aproximatly 30 % of the whole thing
        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Importation de la base de donnée"


        ' Select all the data and the cold feeds materials
        Dim query = Constants.Input.MDB.CycleQuery.QUERY & "BETWEEN #" & startDate.ToString("yyyy/MM/dd") & "# AND #" & endDate.ToString("yyyy/MM/dd") & "#" & _
                    " ORDER BY " & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.CYCLE_ID & " ASC"

        Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)

        Dim reader1 = dbCommand.ExecuteReader

        Dim reader2 As System.Data.OleDb.OleDbDataReader

        Dim cycleID As String = "-1"

        Dim isSameCycle = False

        Dim cycle As MDBCycle = Nothing

        Dim isFirstpass = True

        Dim previousMaterialName = ""

        Dim locationName As Object

        Dim feedIsUnknown As Boolean = True

        Dim recipeIsUnknown As Boolean = True

        Dim tankIsUnknown As Boolean = True


        While (reader1.Read)

            isSameCycle = cycleID.Equals(CStr(reader1.Item(0)))

            If (Not isSameCycle) Then

                If (Not isFirstpass) Then


                    ' Calculations

                    If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

                        ' Duration
                        cycle.DURATION = cycle.TOTAL_MALAXING_TIME.Add(Constants.Input.MDB.STOP_OFFSET)

                        ' Accumulated mass
                        cycle.MIX_ACCUMULATED_MASS = cycle.MIX_MASS

                    Else

                        ' Duration
                        Dim timeDiff = cycle.TIME.Subtract(cycle.PREVIOUS_CYCLE.TIME)

                        If (timeDiff > cycle.TOTAL_MALAXING_TIME + Constants.Input.MDB.STOP_OFFSET) Then
                            cycle.DURATION = cycle.TOTAL_MALAXING_TIME + Constants.Input.MDB.STOP_OFFSET
                        Else
                            cycle.DURATION = timeDiff
                        End If

                        ' Accumulated mass
                        cycle.MIX_ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.MIX_ACCUMULATED_MASS + cycle.MIX_MASS

                    End If

                    ' Production speed - #unitProblem
                    cycle.PRODUCTION_SPEED = cycle.MIX_MASS / cycle.DURATION.TotalHours

                    ' Set point temperature - might be overriden by settings
                    cycle.SET_POINT_TEMPERATURE = Constants.AsphaltTemperature.getAsphaltSetPointTemperature(cycle.ASPHALT_NAME)

                    ' Asphalt percentage - #unitProblem
                    cycle.ASPHALT_PERCENTAGE = cycle.ASPHALT_SUMMARY_FEED.MASS / cycle.MIX_MASS * 100

                    ' Recycled percentage - #unitProblem
                    For Each feed In cycle.HOT_FEEDS
                        If (feed.IS_RECYCLED) Then
                            cycle.RECYCLED_PERCENTAGE = feed.MASS / cycle.AGGREGATES_MASS * 100
                        End If
                    Next

                    recipeIsUnknown = True

                    ' Settings...
                    For Each recipeInfo In XmlSettings.Settings.instance.Usine.RecipesInfo.RECIPES

                        If (recipeInfo.match(cycle.FORMULA_NAME)) Then

                            cycle.MIX_NAME = recipeInfo.MIX_NAME
                            cycle.RECYCLED_SET_POINT_PERCENTAGE = recipeInfo.RECYCLED_SET_POINT_PERCENTAGE
                            cycle.ASPHALT_SET_POINT_PERCENTAGE = recipeInfo.ASPHALT_SET_POINT_PERCENTAGE

                            recipeIsUnknown = False

                            Exit For
                        End If

                    Next

                    If (recipeIsUnknown) Then
                        XmlSettings.Settings.instance.Usine.RecipesInfo.addUnknownRecipe(cycle.FORMULA_NAME, cycle.MIX_NAME)
                    End If

                    tankIsUnknown = True

                    For Each asphaltInfo In XmlSettings.Settings.instance.Usine.AsphaltInfo.TANKS

                        If (asphaltInfo.TANK_NAME.Equals(cycle.ASPHALT_TANK)) Then

                            cycle.ASPHALT_NAME = asphaltInfo.ASPHALT_NAME
                            cycle.SET_POINT_TEMPERATURE = asphaltInfo.SET_POINT_TEMP

                            tankIsUnknown = False

                            Exit For
                        End If

                    Next

                    If (tankIsUnknown) Then
                        XmlSettings.Settings.instance.Usine.AsphaltInfo.addUnknownTank(cycle.ASPHALT_TANK, cycle.ASPHALT_NAME)
                    End If

                    cycleList.Add(cycle.Clone)

                    previousCycle = cycle.Clone

                    ' Progress bar
                    ReportGenerationControl.instance.addStep(progressBarStep)

                End If


                cycle = New MDBCycle(previousCycle)

                With cycle

                    ' Make 2 try/catch block for error proofing :
                    ' 1 for the absolutely necessary info
                    ' and 1 for the less important info

                    Try

                        cycleID = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.CYCLE_ID)

                        .CYCLE_ID = cycleID
                        .COMMAND_ID = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.COMMAND_ID)
                        .SET_POINT_MASS = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.SET_POINT_MASS)
                        .TEMPERATURE = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.TEMPERATURE)
                        Dim date_time = CStr(reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.DATE_TIME)).Split({" "c}, 2)
                        .DATE_ = DateTime.Parse(date_time(0))
                        .TIME = DateTime.Parse(date_time(0) & " " & date_time(1))
                        .DRY_MALAXING_TIME = TimeSpan.FromSeconds(CDbl(reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.DRY_MALAXING_TIME)))
                        .TOTAL_MALAXING_TIME = TimeSpan.FromSeconds(CDbl(reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.TOTAL_MALAXING_TIME)))
                        .TRUCK_ID = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.TRUCK_ID)
                        .FORMULA_NAME = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.FORMULA_NAME)

                    Catch ex As InvalidCastException
                        ' #exception
                        ' Not enough info in DB
                        UIExceptionHandler.instance.handle(ex)
                    End Try

                    Try
                        .MIX_NAME = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.MIX_NAME)
                        .RECIPE_QUANTITY = reader1.Item(Constants.Input.MDB.CycleQuery.RESULTS.RECIPE_QUANTITY)
                    Catch ex As Exception

                        ' #remove?
                        ' Do nothing for now
                        Console.WriteLine("No recipes found with the name : " & .FORMULA_NAME)
                    End Try

                End With

                query = Constants.Input.MDB.FeedsQuery.QUERY & cycleID

                dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)

                reader2 = dbCommand.ExecuteReader



                While (reader2.Read)

                    Dim hotfeed As New MDBFeeder(cycle)

                    With hotfeed

                        .RECIPE_MASS = reader2.Item(Constants.Input.MDB.FeedsQuery.RESULTS.RECIPE_MASS)
                        .SET_POINT_MASS = reader2.Item(Constants.Input.MDB.FeedsQuery.RESULTS.SET_POINT_MASS)
                        .MASS = reader2.Item(Constants.Input.MDB.FeedsQuery.RESULTS.MASS)
                        .MANUAL_MODE = reader2.Item(Constants.Input.MDB.FeedsQuery.RESULTS.MANUAL_MODE)
                        .MATERIAL_NAME = reader2.Item(Constants.Input.MDB.FeedsQuery.RESULTS.MATERIAL_NAME)
                        .LOCATION = reader2.Item(Constants.Input.MDB.FeedsQuery.RESULTS.LOCATION)
                        .INDEX = Constants.Input.MDB.getLocationIndex(.LOCATION)

                    End With

                    feedIsUnknown = True

                    ' Settings
                    For Each feedInfo In XmlSettings.Settings.instance.Usine.DataFiles.MDB.HOT_FEEDS

                        If (feedInfo.LOCATION.Equals(hotfeed.LOCATION)) Then

                            hotfeed.MATERIAL_NAME = feedInfo.MATERIAL
                            hotfeed.IS_RECYCLED = feedInfo.IS_RECYCLED
                            hotfeed.IS_FILLER = feedInfo.IS_FILLER
                            hotfeed.IS_ASPHALT = feedInfo.IS_ASPHALT
                            hotfeed.INDEX = feedInfo.INDEX

                            feedIsUnknown = False

                            Exit For
                        End If

                    Next

                    If (feedIsUnknown) Then
                        XmlSettings.Settings.instance.Usine.DataFiles.MDB.addUnknownFeed(hotfeed.LOCATION, hotfeed.MATERIAL_NAME, MDBCycle.HOT_FEED_TAG)
                    End If

                    cycle.addHotFeed(hotfeed)

                End While

                reader2.Close()

            End If

            ' Add a condition to test if the recipe name was found in the
            ' recipes table, if it wasn't found, throw an exeption. (or not if
            ' it does nothing. Maybe the condition below will be enough)

            If (Not IsDBNull(reader1.Item(13))) Then

                ' If the same material comes twice
                If (Not previousMaterialName.Equals(CStr(reader1.Item(13)))) Then

                    previousMaterialName = CStr(reader1.Item(13))

                    locationName = reader1.Item(12)
                    If (IsDBNull(locationName)) Then
                        locationName = Constants.Input.MDB.DEFAULT_LOCATION
                    End If

                    Dim coldFeed As New MDBFeeder(cycle)

                    With coldFeed

                        .SET_POINT_PERCENTAGE = reader1.Item(11)
                        .LOCATION = locationName
                        .MATERIAL_NAME = reader1.Item(13)
                        .INDEX = Constants.Input.MDB.getLocationIndex(.LOCATION)

                    End With

                    feedIsUnknown = True

                    ' Settings
                    For Each feedInfo In XmlSettings.Settings.instance.Usine.DataFiles.MDB.COLD_FEEDS

                        If (feedInfo.LOCATION.Equals(coldFeed.LOCATION)) Then

                            coldFeed.MATERIAL_NAME = feedInfo.MATERIAL
                            coldFeed.INDEX = feedInfo.INDEX
                            coldFeed.IS_RECYCLED = feedInfo.IS_RECYCLED

                            feedIsUnknown = False

                            Exit For
                        End If

                    Next

                    If (feedIsUnknown) Then
                        XmlSettings.Settings.instance.Usine.DataFiles.MDB.addUnknownFeed(coldFeed.LOCATION, coldFeed.MATERIAL_NAME, MDBCycle.COLD_FEED_TAG)
                    End If

                    cycle.addColdFeed(coldFeed)

                End If

            End If

            ' #remove?
            ' delete if
            If (isFirstpass) Then

                isFirstpass = False

            End If

        End While

        ' #refactor
        ' Last cycle

        ' Calculations

        If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

            ' Duration
            cycle.DURATION = cycle.TOTAL_MALAXING_TIME.Add(Constants.Input.MDB.STOP_OFFSET)

            ' Accumulated mass
            cycle.MIX_ACCUMULATED_MASS = cycle.MIX_MASS

        Else

            ' Duration
            Dim timeDiff = cycle.TIME.Subtract(cycle.PREVIOUS_CYCLE.TIME)

            If (timeDiff > cycle.TOTAL_MALAXING_TIME + Constants.Input.MDB.STOP_OFFSET) Then
                cycle.DURATION = cycle.TOTAL_MALAXING_TIME + Constants.Input.MDB.STOP_OFFSET
            Else
                cycle.DURATION = timeDiff
            End If

            ' Accumulated mass
            cycle.MIX_ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.MIX_ACCUMULATED_MASS + cycle.MIX_MASS

        End If

        ' Production speed - #unitProblem
        cycle.PRODUCTION_SPEED = cycle.MIX_MASS / cycle.DURATION.TotalHours

        ' Set point temperature - might be overriden by settings
        cycle.SET_POINT_TEMPERATURE = Constants.AsphaltTemperature.getAsphaltSetPointTemperature(cycle.ASPHALT_NAME)

        ' Asphalt percentage - #unitProblem
        cycle.ASPHALT_PERCENTAGE = cycle.ASPHALT_SUMMARY_FEED.MASS / cycle.MIX_MASS * 100

        ' Recycled percentage - #unitProblem
        For Each feed In cycle.HOT_FEEDS
            If (feed.IS_RECYCLED) Then
                cycle.RECYCLED_PERCENTAGE = feed.MASS / cycle.AGGREGATES_MASS * 100
            End If
        Next

        ' Settings...
        For Each recipeInfo In XmlSettings.Settings.instance.Usine.RecipesInfo.RECIPES

            If (recipeInfo.match(cycle.FORMULA_NAME)) Then

                cycle.MIX_NAME = recipeInfo.MIX_NAME
                cycle.RECYCLED_SET_POINT_PERCENTAGE = recipeInfo.RECYCLED_SET_POINT_PERCENTAGE
                cycle.ASPHALT_SET_POINT_PERCENTAGE = recipeInfo.ASPHALT_SET_POINT_PERCENTAGE

                Exit For
            End If

        Next

        For Each asphaltInfo In XmlSettings.Settings.instance.Usine.AsphaltInfo.TANKS

            If (asphaltInfo.TANK_NAME.Equals(cycle.ASPHALT_TANK)) Then

                cycle.ASPHALT_NAME = asphaltInfo.ASPHALT_NAME
                cycle.SET_POINT_TEMPERATURE = asphaltInfo.SET_POINT_TEMP

                Exit For
            End If

        Next

        ' Add last cycle
        cycleList.Add(cycle)

        reader1.Close()

        Return cycleList

    End Function

    Public Shared Function nbCyclesFor(file As FileInfo, day As Date) As Integer

        OleDBAdapter.initialize(file)

        ' For the progress bar...
        Dim cycleCountQuery = "SELECT COUNT(" & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.CYCLE_ID & ") FROM " & Constants.Input.MDB.Tables.CYCLE & " WHERE " & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.DATE_TIME & " BETWEEN #" & day.ToString("yyyy/MM/dd") & "# AND #" & day.Add(TimeSpan.FromDays(1)).ToString("yyyy/MM/dd") & "#"
        Dim cycleCount = CInt(New System.Data.OleDb.OleDbCommand(cycleCountQuery, OleDBAdapter.MDB_CONNECTION).ExecuteScalar)

        Return cycleCount

    End Function

    Public Shared Sub getEvents(file As FileInfo)

        Try

            Dim readingStream As System.IO.StreamReader

            ' Open a stream to the file
            readingStream = New System.IO.StreamReader(file.FullName)

            Dim fileLines = readingStream.ReadToEnd.Split(Environment.NewLine)

            ' -2 because last line is useless
            For i = 0 To fileLines.Length - 2

                Dim splitLine = fileLines(i).Split(";")

                Events.addEventFromLog(CDbl(splitLine(0)), splitLine(1))

            Next

        Catch ex As IO.IOException

            If (UIExceptionHandler.instance.handle(ex)) Then
                getEvents(file)
            End If

        End Try

    End Sub
End Class
