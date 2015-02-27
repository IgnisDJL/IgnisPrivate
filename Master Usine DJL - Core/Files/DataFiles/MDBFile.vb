Public Class MDBFile
    Inherits CyclesFile

    ' #language
    Public Shared ReadOnly GENERIC_NAME As String = "Base de données (.mdb)"

    ' Attributes
    Private _copy As IO.FileInfo
    Private _date As Date

    ' Events
    Public Event AnalysisStartedEvent(mdbFile As MDBFile)
    Public Event AnalysisProgress(current As Integer, total As Integer)
    Public Event AnalysisStopedEvent(mdbFile As MDBFile)

    Public Sub New(pathToDataBase As String)
        MyBase.New(pathToDataBase)

        Me._copy = Nothing
    End Sub

    Public Sub New(pathToDataBase As String, pathToCopy As String)
        MyBase.New(pathToDataBase)
        Me._copy = New IO.FileInfo(pathToCopy)
    End Sub

    ' #refactor - dont do the last cycle thing
    Public Overrides Function getCycles(startDate As Date, endDate As Date) As List(Of Cycle)

        RaiseEvent AnalysisStartedEvent(Me)

        Dim cycleList As New List(Of Cycle)
        Dim previousCycle As MDBCycle = Nothing

        Dim mdbNode As XmlSettings.MDBNode = XmlSettings.Settings.instance.Usine.DataFiles.MDB

        OleDBAdapter.initialize(Me.getFileInfo.FullName)

        Dim cycleCount = 0

        For _dayOffset = 0 To endDate.Subtract(startDate).Days

            cycleCount += getNumberOfCyclesFor(Me.getFileInfo, startDate.AddDays(_dayOffset).Date)
        Next

        If (cycleCount > 0) Then


            ' Select all the data and the cold feeds materials
            Dim query = Constants.Input.MDB.CycleQuery.QUERY & "BETWEEN #" & startDate.Date.ToString("yyyy/MM/dd") & "# AND #" & endDate.Date.ToString("yyyy/MM/dd") & "#" & _
                        " ORDER BY " & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.CYCLE_ID & " ASC"

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)

            Dim cyclesTableReader = dbCommand.ExecuteReader

            Dim feedsTableReader As System.Data.OleDb.OleDbDataReader

            Dim cycleID As String = "-1"

            Dim isSameCycle = False

            Dim cycle As MDBCycle = Nothing

            Dim isFirstPass = True

            Dim previousMaterialName = ""

            Dim locationName As Object

            Dim feedIsUnknown As Boolean = True

            Dim recipeIsUnknown As Boolean = True

            Dim tankIsUnknown As Boolean = True

            Dim hotFeedsBuffer As New List(Of Feeder) ' For unknown feeds (no index otherwise)

            Dim coldFeedsBuffer As New List(Of Feeder)

            Dim addToFeedsBuffer As Boolean

            While (cyclesTableReader.Read)

                isSameCycle = cycleID.Equals(CStr(cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.CYCLE_ID)))

                If (Not isSameCycle) Then

                    If (Not isFirstPass) Then

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
                            ' XmlSettings.Settings.instance.Usine.AsphaltInfo.addUnknownTank(cycle.ASPHALT_TANK, cycle.ASPHALT_NAME)
                        End If

                        RaiseEvent AnalysisProgress(cycleList.Count, cycleCount)

                        cycleList.Add(cycle.Clone)

                        previousCycle = cycle.Clone

                    End If


                    cycle = New MDBCycle(previousCycle)

                    With cycle

                        ' Make 2 try/catch block for error proofing :
                        ' 1 for the absolutely necessary info
                        ' and 1 for the less important info

                        Try

                            cycleID = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.CYCLE_ID)

                            .CYCLE_ID = cycleID
                            .COMMAND_ID = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.COMMAND_ID)
                            .SET_POINT_MASS = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.SET_POINT_MASS)
                            .TEMPERATURE = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.TEMPERATURE)
                            Dim date_time = CStr(cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.DATE_TIME)).Split({" "c}, 2)
                            .DATE_ = DateTime.Parse(date_time(0))
                            .TIME = DateTime.Parse(date_time(0) & " " & date_time(1))
                            .DRY_MALAXING_TIME = TimeSpan.FromSeconds(CDbl(cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.DRY_MALAXING_TIME)))
                            .TOTAL_MALAXING_TIME = TimeSpan.FromSeconds(CDbl(cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.TOTAL_MALAXING_TIME)))
                            .TRUCK_ID = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.TRUCK_ID)
                            .FORMULA_NAME = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.FORMULA_NAME)

                        Catch ex As InvalidCastException
                            ' #exception
                            ' Not enough info in DB
                            UIExceptionHandler.instance.handle(ex)
                        End Try

                        Try
                            .MIX_NAME = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.MIX_NAME)
                            .RECIPE_QUANTITY = cyclesTableReader.Item(Constants.Input.MDB.CycleQuery.RESULTS.RECIPE_QUANTITY)
                        Catch ex As Exception

                            ' #remove?
                            ' Do nothing for now
                            Console.WriteLine("No recipes found with the name : " & .FORMULA_NAME)
                        End Try

                    End With

                    query = Constants.Input.MDB.FeedsQuery.QUERY & cycleID

                    dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)

                    feedsTableReader = dbCommand.ExecuteReader


                    While (feedsTableReader.Read)

                        Dim hotfeed As New MDBFeeder(cycle)

                        addToFeedsBuffer = True

                        With hotfeed

                            .RECIPE_MASS = feedsTableReader.Item(Constants.Input.MDB.FeedsQuery.RESULTS.RECIPE_MASS)
                            .SET_POINT_MASS = feedsTableReader.Item(Constants.Input.MDB.FeedsQuery.RESULTS.SET_POINT_MASS)
                            .MASS = feedsTableReader.Item(Constants.Input.MDB.FeedsQuery.RESULTS.MASS)
                            .MANUAL_MODE = feedsTableReader.Item(Constants.Input.MDB.FeedsQuery.RESULTS.MANUAL_MODE)
                            .MATERIAL_NAME = feedsTableReader.Item(Constants.Input.MDB.FeedsQuery.RESULTS.MATERIAL_NAME)
                            .LOCATION = feedsTableReader.Item(Constants.Input.MDB.FeedsQuery.RESULTS.LOCATION)

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

                            ' #refactor - rename : hotFeedIndexMap? list of key value pairs | key = location, value = index
                            For Each _feedBuffer As Feeder In hotFeedsBuffer
                                If (Not IsNothing(_feedBuffer.LOCATION) AndAlso _feedBuffer.LOCATION.Equals(hotfeed.LOCATION)) Then
                                    hotfeed.INDEX = _feedBuffer.INDEX
                                    addToFeedsBuffer = False
                                End If
                            Next

                            If (addToFeedsBuffer) Then
                                hotfeed.INDEX = hotFeedsBuffer.Count
                                hotFeedsBuffer.Add(hotfeed)
                            End If

                        End If


                        cycle.addHotFeed(hotfeed)

                    End While

                    feedsTableReader.Close()

                End If

                ' Add a condition to test if the recipe name was found in the
                ' recipes table, if it wasn't found, throw an exeption. (or not if
                ' it does nothing. Maybe the condition below will be enough)

                ' #refactor - extract constants
                If (Not IsDBNull(cyclesTableReader.Item(13))) Then

                    ' If the same material comes twice
                    If (Not previousMaterialName.Equals(CStr(cyclesTableReader.Item(13)))) Then

                        previousMaterialName = CStr(cyclesTableReader.Item(13))

                        locationName = cyclesTableReader.Item(12)
                        If (IsDBNull(locationName)) Then
                            locationName = Constants.Input.MDB.DEFAULT_LOCATION
                        End If

                        Dim coldFeed As New MDBFeeder(cycle)

                        addToFeedsBuffer = True

                        With coldFeed

                            .SET_POINT_PERCENTAGE = cyclesTableReader.Item(11)
                            .LOCATION = locationName
                            .MATERIAL_NAME = cyclesTableReader.Item(13)

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

                            ' #refactor - rename : hotFeedIndexMap? list of key value pairs | key = location, value = index
                            ' #todo - persist location of unknown feeds to always have the same indexes ??? Only use I can think of is periodic reports and stuff. But I guess you could use location... 
                            For Each _feedBuffer As Feeder In coldFeedsBuffer
                                If (Not IsNothing(_feedBuffer.LOCATION) AndAlso _feedBuffer.LOCATION.Equals(coldFeed.LOCATION)) Then
                                    coldFeed.INDEX = _feedBuffer.INDEX
                                    addToFeedsBuffer = False
                                End If
                            Next

                            If (addToFeedsBuffer) Then
                                coldFeed.INDEX = coldFeedsBuffer.Count
                                coldFeedsBuffer.Add(coldFeed)
                            End If
                        End If

                        cycle.addColdFeed(coldFeed)

                    End If

                End If

                ' #remove?
                ' delete if
                If (isFirstPass) Then

                    isFirstPass = False

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

            RaiseEvent AnalysisProgress(cycleList.Count, cycleCount)

            ' Add last cycle
            cycleList.Add(cycle)

            cyclesTableReader.Close()

        End If

        RaiseEvent AnalysisStopedEvent(Me)

        Return cycleList
    End Function

    Public Overloads Function hasCycles() As Boolean

        Return (getNumberOfCyclesFor(Me.getFileInfo, Me._date) > 0)
    End Function

    Public Overloads Function hasCycles(_date As Date) As Boolean

        Return (getNumberOfCyclesFor(Me.getFileInfo, _date) > 0)
    End Function

    Private Shared Function getNumberOfCyclesFor(mdbFileInfo As IO.FileInfo, _day As Date) As Integer

        OleDBAdapter.initialize(mdbFileInfo.FullName)

        Dim cycleCountQuery = "SELECT COUNT(" & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.CYCLE_ID & ") FROM " & Constants.Input.MDB.Tables.CYCLE & " WHERE " & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.DATE_TIME & " BETWEEN #" & _day.ToString("yyyy/MM/dd") & "# AND #" & _day.Add(TimeSpan.FromDays(1)).ToString("yyyy/MM/dd") & "#"
        Dim cycleCount = CInt(New System.Data.OleDb.OleDbCommand(cycleCountQuery, OleDBAdapter.MDB_CONNECTION).ExecuteScalar)

        Return cycleCount
    End Function

    Public Function getLastCycleDate() As Date

        OleDBAdapter.initialize(Me.getFileInfo.FullName)

        Dim lastCycleDateQuery = "SELECT TOP 1 " & Constants.Input.MDB.Tables.CYCLE & "." & Constants.Input.MDB.Columns.DATE_TIME & _
                                 " FROM " & Constants.Input.MDB.Tables.CYCLE & _
                                 " ORDER BY " & Constants.Input.MDB.Columns.DATE_TIME & " DESC"

        Dim queryResultReader = New System.Data.OleDb.OleDbCommand(lastCycleDateQuery, OleDBAdapter.MDB_CONNECTION).ExecuteReader

        If (queryResultReader.Read) Then
            Return Date.Parse(queryResultReader.Item(Constants.Input.MDB.Columns.DATE_TIME)).Date
        Else
            Return Date.MaxValue
        End If

    End Function

    Public Overrides Sub open()

        If (IsNothing(Me._copy)) Then

            Diagnostics.Process.Start(Me.getFileInfo.FullName)

        Else

            Diagnostics.Process.Start(Me._copy.FullName)

        End If
    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return Me._date
        End Get
    End Property

    Public ReadOnly Property Copy As IO.FileInfo
        Get
            Return Me._copy
        End Get
    End Property

    Public Sub setDate(_date As Date)
        Me._date = _date
    End Sub



End Class
