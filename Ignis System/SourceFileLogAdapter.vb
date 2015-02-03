Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class SourceFileLogAdapter
    Inherits SourceFileAdapter



    Private feederCycleDictionary As Dictionary(Of Integer, String)
    Private regex As Regex


    Public Sub New()

    End Sub
    Private Function getFeederCycleDictionary(sourceFile As SourceFile) As Dictionary(Of Integer, String)

        If IsNothing(feederCycleDictionary) Then

            feederCycleDictionary = New Dictionary(Of Integer, String)

            For Each cycle As String In getCycleList(sourceFile)
                feederCycleDictionary.Add(getCycleList(sourceFile).IndexOf(cycle), Split(cycle, "Time :")(1))
            Next
            Return feederCycleDictionary
        Else
            Return feederCycleDictionary
        End If

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
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        additiveActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Additive).Value.Trim
        Return If(String.IsNullOrEmpty(additiveActualPercentage), "0.00", additiveActualPercentage)
    End Function

    Public Overrides Function getAdditiveDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        additiveDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Additive).Value.Trim
        Return If(String.IsNullOrEmpty(additiveDebit), "0.00", additiveDebit)
    End Function

    Public Overrides Function getAdditiveMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        additiveMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Additive).Value.Trim
        Return If(String.IsNullOrEmpty(additiveMass), "0.00", additiveMass)
    End Function

    Public Overrides Function getAdditiveTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim additiveTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        additiveTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Additive).Value.Trim
        Return If(String.IsNullOrEmpty(additiveTargetPercentage), "0.00", additiveTargetPercentage)
    End Function

    Public Overrides Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

        Dim asphaltRecordedTemperature As String

        regex = New Regex("Asphalt Temp :[\s]+(\-?[\d]*)")
        asphaltRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim

        If String.IsNullOrEmpty(asphaltRecordedTemperature) Then
            Return "0.00"
        Else
            Return asphaltRecordedTemperature
        End If

    End Function

    Public Overrides Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String
        regex = New Regex("A/C Tank:[\s]([\d]+)")
        asphaltTankId = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltTankId), "N/A", asphaltTankId)

    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim bagHouseDiff As String
        regex = New Regex("Bh Diff:[\s](\-?[\d]{1,3}.[\d])")
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
        regex = New Regex(Constants.Input.LOG.FILE_NAME_REGEX)
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
        regex = New Regex("Dust Removal:[\s]+([\d]{1,2}\.[\d]+)")
        dustRemovalDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(dustRemovalDebit), "0.00", dustRemovalDebit)
    End Function

    Public Overrides Function getFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String
        regex = New Regex("([\d]+.[\d]+)")
        feederActualPercentage = regex.Match(regex.Split(getCycle(indexCycle, sourceFile), "\r")(24)).Groups(indexFeeder).Value.Trim
        Return If(String.IsNullOrEmpty(feederActualPercentage), "0.00", feederActualPercentage)
    End Function

    Public Overrides Function getFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        Dim feederCountForCycle As New Integer
        feederCountForCycle = 0

        Try
            For Each feederId As String In regex.Split(regex.Split(getCycle(indexCycle, sourceFile), "\r")(20), "[\s]+")
                If Not String.IsNullOrEmpty(feederId) Then
                    feederCountForCycle += 1
                End If
            Next

        Catch ex As Exception
            Return feederCountForCycle
        End Try

        Return feederCountForCycle
    End Function

    Public Overrides Function getFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederDebit As String
        Return If(String.IsNullOrEmpty(feederDebit), "0.00", feederDebit)
    End Function

    Protected Overrides Function getFeederListForCycle(indexCycle As Integer, sourceFile As SourceFile) As String

        Return getFeederCycleDictionary(sourceFile).Item(indexCycle)
    End Function

    Public Overrides Function getFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String
        Return If(String.IsNullOrEmpty(feederID), "N/A", feederID)
    End Function

    Public Overrides Function getFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMass As String
        Return If(String.IsNullOrEmpty(feederMass), "0.00", feederMass)
    End Function

    Public Overrides Function getFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMoisturePercentage As String
        Return If(String.IsNullOrEmpty(feederMoisturePercentage), "0.00", feederMoisturePercentage)
    End Function

    Public Overrides Function getFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederRecycledAsphaltPercentage As String
        Return If(String.IsNullOrEmpty(feederRecycledAsphaltPercentage), "0.00", feederRecycledAsphaltPercentage)
    End Function

    Public Overrides Function getFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim FeederTargetPercentage As String
        Return If(String.IsNullOrEmpty(FeederTargetPercentage), "0.00", FeederTargetPercentage)
    End Function

    Public Overrides Function getFillerActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerActualPercentage As String
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        fillerActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Filler).Value.Trim
        Return If(String.IsNullOrEmpty(fillerActualPercentage), "0.00", fillerActualPercentage)
    End Function

    Public Overrides Function getFillerDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        fillerDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Filler).Value.Trim
        Return If(String.IsNullOrEmpty(fillerDebit), "0.00", fillerDebit)
    End Function

    Public Overrides Function getFillerMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        fillerMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Filler).Value.Trim
        Return If(String.IsNullOrEmpty(fillerMass), "0.00", fillerMass)
    End Function

    Public Overrides Function getFillerTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim fillerTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        fillerTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.Filler).Value.Trim
        Return If(String.IsNullOrEmpty(fillerTargetPercentage), "0.00", fillerTargetPercentage)
    End Function

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixCounter As String
        regex = New Regex("Mix Tons :[\s]+([\d]+)[\s]T")
        mixCounter = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixCounter), "0.00", mixCounter)
    End Function

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixDebit As String
        regex = New Regex("Mix Tph[\s]+([\d]{2,3})")
        mixDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixDebit), "0.00", mixDebit)
    End Function

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixName As String
        regex = New Regex("Mix Name :[\s]([a-zA-Z0-9\s\-_%]+)([\n])")
        mixName = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixName), "N/A", mixName)
    End Function

    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String
        regex = New Regex("Mix Number :[\s]+([a-zA-Z0-9\s\-_%]+)[\s]+Mix Name :[\s]([a-zA-Z0-9\s\-_%]+)")
        mixNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixNumber), "N/A", mixNumber)
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixRecordedTemperature As String
        regex = New Regex("Mix Temp :[\s]+([\-]?[\d]+)")
        mixRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixRecordedTemperature), "0.00", mixRecordedTemperature)
    End Function

    Public Overrides Function getRecycledAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateActualPercentage As String
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAggregateActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAggregateActualPercentage), "0.00", recycledAggregateActualPercentage)
    End Function

    Public Overrides Function getRecycledAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAggregateDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAggregateDebit), "0.00", recycledAggregateDebit)
    End Function

    Public Overrides Function getRecycledAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAggregateMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAggregateMass), "0.00", recycledAggregateMass)
    End Function

    Public Overrides Function getRecycledAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateMoisturePercentage As String
        regex = New Regex("(Mst%)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAggregateMoisturePercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAggregateMoisturePercentage), "0.00", recycledAggregateMoisturePercentage)
    End Function

    Public Overrides Function getRecycledAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAggregateTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAggregateTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAggregateTargetPercentage), "0.00", recycledAggregateTargetPercentage)
    End Function

    Public Overrides Function getRecycledAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltActualPercentage As String
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAsphaltActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAsphaltActualPercentage), "0.00", recycledAsphaltActualPercentage)
    End Function

    Public Overrides Function getRecycledAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAsphaltDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAsphaltDebit), "0.00", recycledAsphaltDebit)
    End Function

    Public Overrides Function getRecycledAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAsphaltMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAsphaltMass), "0.00", recycledAsphaltMass)
    End Function

    Public Overrides Function getRecycledAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim recycledAsphaltTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        recycledAsphaltTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.RecycledAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(recycledAsphaltTargetPercentage), "0.00", recycledAsphaltTargetPercentage)
    End Function

    Public Overrides Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltDensity As String
        regex = New Regex("Ac Specific Gravity[\s]+([\d].[\d]{3})")
        asphaltDensity = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltDensity), "0.00", asphaltDensity)
    End Function


    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim siloFillingNumber As String
        regex = New Regex("Silo Filling:[\s]+([\d]+)")
        siloFillingNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(siloFillingNumber), "N/A", siloFillingNumber)
    End Function

    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        'Dim time As String
        'regex = New Regex("^([\d][\d]?:[\d][\d]:[\d][\d]([\s](AM|PM))?)")
        'time = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        'Return If(String.IsNullOrEmpty(time), "N/A", time)
        Return Date.Now
    End Function

    Public Overrides Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltActualPercentage As String
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        totalAsphaltActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.TotalAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(totalAsphaltActualPercentage), "0.00", totalAsphaltActualPercentage)
    End Function

    Public Overrides Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        totalAsphaltDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.TotalAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(totalAsphaltDebit), "0.00", totalAsphaltDebit)
    End Function

    Public Overrides Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        totalAsphaltMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.TotalAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(totalAsphaltMass), "0.00", totalAsphaltMass)
    End Function

    Public Overrides Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        totalAsphaltTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.TotalAsphalt).Value.Trim
        Return If(String.IsNullOrEmpty(totalAsphaltTargetPercentage), "0.00", totalAsphaltTargetPercentage)
    End Function

    Public Overrides Function getVirginAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateActualPercentage As String
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAggregateActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(virginAggregateActualPercentage), "0.00", virginAggregateActualPercentage)
    End Function

    Public Overrides Function getVirginAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAggregateDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(virginAggregateDebit), "0.00", virginAggregateDebit)
    End Function

    Public Overrides Function getVirginAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAggregateMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(virginAggregateMass), "0.00", virginAggregateMass)
    End Function

    Public Overrides Function getVirginAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateMoisturePercentage As String
        regex = New Regex("(Mst%)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAggregateMoisturePercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(virginAggregateMoisturePercentage), "0.00", virginAggregateMoisturePercentage)
    End Function

    Public Overrides Function getVirginAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAggregateTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAggregateTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAggregate).Value.Trim
        Return If(String.IsNullOrEmpty(virginAggregateTargetPercentage), "0.00", virginAggregateTargetPercentage)
    End Function

    Public Overrides Function getVirginAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltTargetPercentage As String
        regex = New Regex("(SP %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAsphaltTargetPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAspahlt).Value.Trim
        Return If(String.IsNullOrEmpty(virginAsphaltTargetPercentage), "0.00", virginAsphaltTargetPercentage)
    End Function

    Public Overrides Function getVirginAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltActualPercentage As String
        regex = New Regex("(Act %)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAsphaltActualPercentage = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAspahlt).Value.Trim
        Return If(String.IsNullOrEmpty(virginAsphaltActualPercentage), "0.00", virginAsphaltActualPercentage)
    End Function

    Public Overrides Function getVirginAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltDebit As String
        regex = New Regex("(Tph)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAsphaltDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAspahlt).Value.Trim
        Return If(String.IsNullOrEmpty(virginAsphaltDebit), "0.00", virginAsphaltDebit)
    End Function

    Public Overrides Function getVirginAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltMass As String
        regex = New Regex("(Tons)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)([\d]+.[\d]+)([\s]+)")
        virginAsphaltMass = regex.Match(getCycle(indexCycle, sourceFile)).Groups(EnumColumnType.VirginAspahlt).Value.Trim
        Return If(String.IsNullOrEmpty(virginAsphaltMass), "0.00", virginAsphaltMass)
    End Function

    Public Overrides Function getRecycledAggregateAsphaltPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        'Aucune information a ce sujet dans le fichier de .log, la confusion est creer par le fait que l'objet AggregateUsed est utilise dans la classe Feeder et dans ProductionCycle
        Return "0.00"
    End Function

End Class
