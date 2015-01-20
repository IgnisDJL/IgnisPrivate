Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class MindsDiscontinueDataPlant
    Inherits DataPlant

    Private Const CSV_DATA_SEPERATOR = ","c
    Private CSV_FILES_CULTURE As Globalization.CultureInfo = Globalization.CultureInfo.CreateSpecificCulture("en-US")

    Public Sub New(pathToCSVFile As String)

        MyBase.New(pathToCSVFile)
        productionDate = getDate()

    End Sub

    Public Overrides Function getCycles(startTime As Date, endTime As Date) As List(Of Cycle)

        Dim defaultCulture = Threading.Thread.CurrentThread.CurrentCulture
        Threading.Thread.CurrentThread.CurrentCulture = CSV_FILES_CULTURE

        analysisStarted(Me)

        Dim faultyRows As New List(Of String())

        Dim dataFileNode As XmlSettings.CSVNode = XmlSettings.Settings.instance.Usine.DataFiles.CSV

        Dim readingStream As System.IO.StreamReader = Nothing

        Dim cycleList As New List(Of Cycle)
        Dim rows() As String
        Dim row() As String
        Dim nbCols = dataFileNode.DATA_FORMAT.Count

        Dim fileExtensionRegex = New System.Text.RegularExpressions.Regex("(\.csv)$")

        Dim fileDate = getDate()

        Dim shouldAddCycle As Boolean

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
                ' Throw openedFileException (getFileInfo)
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

        ' Array of rows for a single file
        rows = stringFile.Split(Environment.NewLine)

        Dim incorrectValueRegex = New Regex(Constants.Input.CSV.EMPTY_VALUE_REGEX)

        Dim previousCycle As CSVCycle = Nothing

        Dim nbRowsToAnalyse As Integer = rows.Length - 3

        ' Add the rows to the list of rows
        ' Starting at 1 to skip the first line
        ' Stopping at rows.lenght - 3 because the last 2 lines are empty
        For i = 1 To nbRowsToAnalyse

            analysisRunning(i, nbRowsToAnalyse)

            shouldAddCycle = True

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

                    Dim dataInfo As XmlSettings.DataInfoNode = XmlSettings.Settings.instance.Usine.DataFiles.CSV.DATA_FORMAT(j)

                    Select Case dataInfo.TAG

                        Case CSVCycle.AGGREGATE_MASS_TAG

                            Dim isUnknownFeed = True

                            cycle.AGGREGATE_MASS(dataInfo.INDEX - 1) = dataValue

                            For k = 0 To cycle.HOT_FEEDS.Count - 1

                                If (cycle.HOT_FEEDS(k).INDEX.Equals(dataInfo.INDEX)) Then

                                    cycle.HOT_FEEDS(k).MASS = CDbl(dataValue)

                                    If (IsNothing(cycle.PREVIOUS_CYCLE)) Then
                                        cycle.HOT_FEEDS(k).ACCUMULATED_MASS = CDbl(dataValue)
                                    Else
                                        cycle.HOT_FEEDS(k).ACCUMULATED_MASS = cycle.PREVIOUS_CYCLE.HOT_FEEDS(k).ACCUMULATED_MASS + CDbl(dataValue)
                                    End If

                                    If (DirectCast(cycle.HOT_FEEDS(k), CSVFeeder).IS_RECYCLED) Then
                                        cycle.RECYCLED_MASS = cycle.HOT_FEEDS(k).MASS
                                    End If

                                    isUnknownFeed = False
                                    Exit For
                                End If
                            Next

                            If (isUnknownFeed) Then

                                Dim location As String = "Benne " & dataInfo.INDEX

                                Dim accumulatedMass As Double = CDbl(dataValue)

                                If (Not IsNothing(cycle.PREVIOUS_CYCLE) AndAlso Not IsNothing(cycle.PREVIOUS_CYCLE.HOT_FEEDS(dataInfo.INDEX - 1))) Then
                                    accumulatedMass += cycle.PREVIOUS_CYCLE.HOT_FEEDS(dataInfo.INDEX - 1).ACCUMULATED_MASS
                                End If

                                cycle.HOT_FEEDS.Add(New CSVFeeder(cycle) _
                                                    With {.INDEX = dataInfo.INDEX, _
                                                          .LOCATION = location, _
                                                          .MATERIAL_NAME = Nothing, _
                                                          .IS_FILLER = False, _
                                                          .IS_RECYCLED = False, _
                                                          .MASS = CDbl(dataValue), _
                                                          .ACCUMULATED_MASS = accumulatedMass})

                                XmlSettings.Settings.instance.Usine.DataFiles.CSV.addUnknownFeed(location, Nothing, IGNIS.Cycle.HOT_FEED_TAG)
                            End If

                        Case CSVCycle.AGGREGATE_PERCENTAGE_TAG

                            Dim isUnknownFeed = True

                            cycle.AGGREGATE_PERCENTAGE(dataInfo.INDEX - 1) = dataValue

                            For Each feed In cycle.HOT_FEEDS
                                If (feed.INDEX.Equals(dataInfo.INDEX)) Then
                                    DirectCast(feed, CSVFeeder).PERCENTAGE = dataValue

                                    If (DirectCast(feed, CSVFeeder).IS_RECYCLED) Then
                                        cycle.RECYCLED_PERCENTAGE = dataValue
                                    End If

                                    isUnknownFeed = False
                                    Exit For
                                End If
                            Next

                            If (isUnknownFeed) Then

                                Dim location As String = "Benne " & dataInfo.INDEX

                                cycle.HOT_FEEDS.Add(New CSVFeeder(cycle) _
                                                    With {.INDEX = dataInfo.INDEX, _
                                                          .LOCATION = location, _
                                                          .MATERIAL_NAME = Nothing, _
                                                          .IS_FILLER = False, _
                                                          .IS_RECYCLED = False, _
                                                          .PERCENTAGE = dataValue})

                                XmlSettings.Settings.instance.Usine.DataFiles.CSV.addUnknownFeed(location, Nothing, IGNIS.Cycle.HOT_FEED_TAG)
                            End If

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

                            Dim isUnknownFeed = True

                            cycle.COLDFEED_PERCENTAGE(dataInfo.INDEX - 1) = dataValue

                            For Each feed In cycle.COLD_FEEDS
                                If (feed.INDEX.Equals(dataInfo.INDEX)) Then
                                    DirectCast(feed, CSVFeeder).PERCENTAGE = dataValue

                                    isUnknownFeed = False
                                    Exit For
                                End If
                            Next

                            If (isUnknownFeed) Then

                                Dim location As String = "Benne " & dataInfo.INDEX

                                cycle.COLD_FEEDS.Add(New CSVFeeder(cycle) _
                                                    With {.INDEX = dataInfo.INDEX, _
                                                          .LOCATION = location, _
                                                          .MATERIAL_NAME = Nothing, _
                                                          .IS_FILLER = False, _
                                                          .IS_RECYCLED = False, _
                                                          .PERCENTAGE = dataValue})

                                XmlSettings.Settings.instance.Usine.DataFiles.CSV.addUnknownFeed(location, Nothing, IGNIS.Cycle.COLD_FEED_TAG)
                            End If

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

                If (shouldAddCycle) Then
                    cycleList.Add(cycle)
                    previousCycle = cycle
                End If

            Else

                ' #exception (bug report?)
                ' Dont forget this...
                faultyRows.Add(row)

            End If

        Next

        analysisStoped(Me)

        Threading.Thread.CurrentThread.CurrentCulture = defaultCulture

        Return cycleList
    End Function

    Public Overrides Function getDate() As Object
        Dim regex As New Regex(Constants.Input.CSV.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(Me.getFileInfo.Name)

        If (match.Success) Then

            Dim day As Integer = Integer.Parse(match.Groups(CSV.CSVFileNameRegexDateIndex.DAY).Value)
            Dim month As Integer = Integer.Parse(match.Groups(CSV.CSVFileNameRegexDateIndex.MONTH).Value)
            Dim year As Integer = Integer.Parse(match.Groups(CSV.CSVFileNameRegexDateIndex.YEAR).Value)

            Return New Date(year, month, day)

        Else

            ' #exception

            Return Nothing
        End If
    End Function
End Class
