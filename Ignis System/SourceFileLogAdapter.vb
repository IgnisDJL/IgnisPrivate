Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class SourceFileLogAdapter
    Inherits SourceFileAdapter




    Public Sub New()

    End Sub

    Private Function getLineFromLogFile(lineNumber As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Try
            Return Regex.Split(getCycle(indexCycle, sourceFile), "\r")(lineNumber)
        Catch
            Return ""
        End Try

    End Function

    Protected Overrides Function getCycleList(sourceFile As SourceFile) As List(Of String)

        If IsNothing(cycleList) Then
            Dim readingStream As System.IO.StreamReader = Nothing
            Dim stringFile As String = Nothing

            readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName)
            stringFile = readingStream.ReadToEnd
            cycleList = New List(Of String)

            For Each cycle_1 As String In Split(stringFile, "Time :").ToList
                If Not String.IsNullOrEmpty(cycle_1) Then
                    cycleList.Add(cycle_1)
                End If
            Next

            Return cycleList
        Else
            Return cycleList
        End If

    End Function

    Public Overrides Function getAdditiveActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveActualPercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")

        Try
            additiveActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.Additive).Value.Trim
            Return If(String.IsNullOrEmpty(additiveActualPercentage), "0.00", additiveActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try

    End Function

    Public Overrides Function getAdditiveDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveDebit As String

        Dim regex = New Regex("([\d]+.[\d]+)")

        Try
            additiveDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.Additive).Value.Trim
            Return If(String.IsNullOrEmpty(additiveDebit), "0.00", additiveDebit)
        Catch ex As Exception
            Return "0.00"
        End Try

    End Function

    Public Overrides Function getAdditiveMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            additiveMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.Additive).Value.Trim
            Return If(String.IsNullOrEmpty(additiveMass), "0.00", additiveMass)
        Catch ex As Exception
            Return "0.00"
        End Try


    End Function

    Public Overrides Function getAdditiveTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            additiveTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.Additive).Value.Trim
            Return If(String.IsNullOrEmpty(additiveTargetPercentage), "0.00", additiveTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltRecordedTemperature As String
        Dim regex = New Regex("Asphalt Temp :[\s]+(\-?[\d]*)")
        asphaltRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltRecordedTemperature), "0.00", asphaltRecordedTemperature)

    End Function

    Public Overrides Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String
        Dim regex = New Regex("A/C Tank:[\s]([\d]+)")
        asphaltTankId = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltTankId), "N/A", asphaltTankId)

    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim bagHouseDiff As String
        Dim regex = New Regex("Bh Diff:[\s](\-?[\d]{1,3}.[\d])")
        bagHouseDiff = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(bagHouseDiff), "0.00", bagHouseDiff)
    End Function

    Protected Overrides Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Return getCycleList(sourceFile).Item(indexCycle)
    End Function

    Public Overrides Function getCycleCount(sourceFile As SourceFile) As Integer
        Return getCycleList(sourceFile).Count()
    End Function

    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Dim regex = New Regex(Constants.Input.LOG.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(sourceFile.getFileInfo.Name)

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

    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dustRemovalDebit As String
        Dim regex = New Regex("Dust Removal:[\s]+([\d]{1,2}\.[\d]+)")
        dustRemovalDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(dustRemovalDebit), "0.00", dustRemovalDebit)
    End Function

    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederActualPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim
            Return If(String.IsNullOrEmpty(feederActualPercentage), "0.00", feederActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        Dim feederCountForCycle As New Integer
        feederCountForCycle = 0

        Try
            For Each feederId As String In Regex.Split(getLineFromLogFile(EnumLineLogFile.feederId, indexCycle, sourceFile), "[\s]+")
                If Not String.IsNullOrEmpty(feederId) Then
                    feederCountForCycle += 1
                End If
            Next

        Catch ex As Exception
            Return feederCountForCycle
        End Try

        Return feederCountForCycle
    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederDebit As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederDebit, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederDebit), "0.00", feederDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String

        Dim regex = New Regex("(\w+)")
        Try
            feederID = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederId, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederID), "N/A", feederID)
        Catch ex As Exception
            Return "N/A"
        End Try
    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMass As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederMass, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMass), "0.00", feederMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMoisturePercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMoisturePercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederMoisturePercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMoisturePercentage), "0.00", feederMoisturePercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function


    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Dim feederRecycledAsphaltPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Dim feederId As String
        feederId = getColdFeederID(indexFeeder, indexCycle, sourceFile).Trim.ToUpper
        Try
            If feederId.Contains("RAP") Then


                feederRecycledAsphaltPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederRecycledAsphaltPercentage, indexCycle, sourceFile))(Integer.Parse(Split(feederId, "RAP")(1)) - 1).Value.Trim
            Else
                feederRecycledAsphaltPercentage = "0.0"
            End If

            Return If(String.IsNullOrEmpty(feederRecycledAsphaltPercentage), "0.0", feederRecycledAsphaltPercentage)
        Catch ex As Exception
            Return "0.0"
        End Try
    End Function

    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederTargetPercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.feederTargetPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederTargetPercentage), "0.00", feederTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getFillerActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerActualPercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            fillerActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.Filler).Value.Trim

            Return If(String.IsNullOrEmpty(fillerActualPercentage), "0.00", fillerActualPercentage)

        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getFillerDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerDebit As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            fillerDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.Filler).Value.Trim

            Return If(String.IsNullOrEmpty(fillerDebit), "0.00", fillerDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getFillerMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            fillerMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.Filler).Value.Trim
            Return If(String.IsNullOrEmpty(fillerMass), "0.00", fillerMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getFillerTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            fillerTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.Filler).Value.Trim
            Return If(String.IsNullOrEmpty(fillerTargetPercentage), "0.00", fillerTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixCounter As String
        Dim regex = New Regex("Mix Tons :[\s]+([\d]+)[\s]T")
        mixCounter = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixCounter), "0.00", mixCounter)
    End Function

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixDebit As String
        Dim regex = New Regex("Mix Tph[\s]+([\d]{2,3})")
        mixDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixDebit), "0.00", mixDebit)
    End Function

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixName As String
        Dim regex = New Regex("Mix Name :[\s]([a-zA-Z0-9\s\-_%]+)([\n])")
        mixName = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixName), "N/A", mixName)
    End Function

    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String
        Dim regex = New Regex("Mix Number :[\s]+([a-zA-Z0-9\s\-_%]+)[\s]+Mix Name :[\s]([a-zA-Z0-9\s\-_%]+)")
        mixNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixNumber), "N/A", mixNumber)
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixRecordedTemperature As String
        Dim regex = New Regex("Mix Temp :[\s]+([\-]?[\d]+)")
        mixRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixRecordedTemperature), "0.00", mixRecordedTemperature)
    End Function

    Public Overrides Function getRecycledAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAggregateActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.RecycledAggregate).Value.Trim

            Return If(String.IsNullOrEmpty(recycledAggregateActualPercentage), "0.00", recycledAggregateActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateDebit As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAggregateDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.RecycledAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAggregateDebit), "0.00", recycledAggregateDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAggregateMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.RecycledAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAggregateMass), "0.00", recycledAggregateMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateMoisturePercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAggregateMoisturePercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.moisturePercentage, indexCycle, sourceFile))(EnumColumnType.RecycledAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAggregateMoisturePercentage), "0.00", recycledAggregateMoisturePercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAggregateTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.RecycledAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAggregateTargetPercentage), "0.00", recycledAggregateTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAsphaltActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.RecycledAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAsphaltActualPercentage), "0.00", recycledAsphaltActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltDebit As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAsphaltDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.RecycledAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAsphaltDebit), "0.00", recycledAsphaltDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAsphaltMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.RecycledAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAsphaltMass), "0.00", recycledAsphaltMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getRecycledAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            recycledAsphaltTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.RecycledAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(recycledAsphaltTargetPercentage), "0.00", recycledAsphaltTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltDensity As String
        Dim regex = New Regex("Ac Specific Gravity[\s]+([\d].[\d]{3})")
        asphaltDensity = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltDensity), "0.00", asphaltDensity)
    End Function


    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim siloFillingNumber As String
        Dim regex = New Regex("Silo Filling:[\s]+([\d]+)")
        siloFillingNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(siloFillingNumber), "N/A", siloFillingNumber)
    End Function

    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        'Dim time As String
        'Dim regex = New Regex("^([\d][\d]?:[\d][\d]:[\d][\d]([\s](AM|PM))?)")
        'time = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        'Return If(String.IsNullOrEmpty(time), "N/A", time)
        Return Date.Now
    End Function

    Public Overrides Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltActualPercentage), "0.00", totalAsphaltActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltDebit As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltDebit), "0.00", totalAsphaltDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltMass), "0.00", totalAsphaltMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltTargetPercentage), "0.00", totalAsphaltTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAggregateActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.VirginAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(virginAggregateActualPercentage), "0.00", virginAggregateActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateDebit As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAggregateDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.VirginAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(virginAggregateDebit), "0.00", virginAggregateDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAggregateMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.VirginAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(virginAggregateMass), "0.00", virginAggregateMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateMoisturePercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAggregateMoisturePercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.moisturePercentage, indexCycle, sourceFile))(EnumColumnType.VirginAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(virginAggregateMoisturePercentage), "0.00", virginAggregateMoisturePercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAggregateTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.VirginAggregate).Value.Trim
            Return If(String.IsNullOrEmpty(virginAggregateTargetPercentage), "0.00", virginAggregateTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")

        Try
            virginAsphaltTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.targetPercentage, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltTargetPercentage), "0.00", virginAsphaltTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.actualPercentage, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltActualPercentage), "0.00", virginAsphaltActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltDebit As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.debit, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltDebit), "0.00", virginAsphaltDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.mass, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltMass), "0.00", virginAsphaltMass)
        Catch ex As Exception
            Return "0.00"
        End Try

    End Function

    Public Overrides Function getRecycledAggregateAsphaltPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        'Aucune information a ce sujet dans le fichier de .log, la confusion est creer par le fait que l'objet AggregateUsed est utilise dans la classe Feeder et dans ProductionCycle
        Return "0.00"
    End Function

    Protected Overrides Function getColdFeederForCycle(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDopeAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDopeAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDopeTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

    End Function

    Protected Overrides Function getHotFeederForCycle(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederID(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederMass(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function
End Class
