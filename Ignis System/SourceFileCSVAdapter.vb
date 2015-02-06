Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input
Imports System.Globalization
Imports System.Text

Public Class SourceFileCSVAdapter
    Inherits SourceFileAdapter




    Private columnNameList As List(Of String)

    Public Sub New()

    End Sub

    Private Function getColumnFromCSVFile(columnName As String, indexCycle As Integer, sourceFile As SourceFile) As String

        Try
            Return Regex.Split(getCycle(indexCycle, sourceFile), ","c)(getIndexForColumnName(columnName, sourceFile))
        Catch
            Return ""
        End Try

    End Function

    Private Function getIndexForColumnName(columnName As String, sourceFile As SourceFile) As Integer
        If (getColumnNameList(sourceFile).Contains(columnName)) Then
            Return columnNameList.IndexOf(columnName)
        Else
            Return -1
        End If
    End Function

    Private Sub setColumnNameList(sourceFile As SourceFile)

        Dim readingStream As System.IO.StreamReader = Nothing
        Dim stringFile As String = Nothing

        readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName, Encoding.GetEncoding(1252))
        stringFile = readingStream.ReadToEnd
        columnNameList = New List(Of String)

        For Each columnName As String In Split(Regex.Split(stringFile, "\r")(0), ",")
            If Not String.IsNullOrEmpty(columnName) Then
                columnNameList.Add(columnName.Trim)
            End If
        Next

    End Sub

    Private Function getColumnNameList(sourceFile As SourceFile) As List(Of String)
        If IsNothing(columnNameList) Then
            setColumnNameList(sourceFile)
            Return columnNameList
        Else
            Return columnNameList
        End If
    End Function

    Public Overrides Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String

        Try
            asphaltTankId = getColumnFromCSVFile("Tank Bit", indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(asphaltTankId), "0.00", asphaltTankId)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Protected Overrides Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Return getCycleList(sourceFile).ElementAt(indexCycle)
    End Function

    Public Overrides Function getCycleCount(sourceFile As SourceFile) As Integer
        Return getCycleList(sourceFile).Count()
    End Function

    Protected Overrides Function getCycleList(sourceFile As SourceFile) As List(Of String)
        If IsNothing(cycleList) Then
            Dim readingStream As System.IO.StreamReader = Nothing
            Dim stringFile As String = Nothing

            readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName, Encoding.GetEncoding(1252))
            stringFile = readingStream.ReadToEnd
            cycleList = New List(Of String)

            For Each cycle_1 As String In Regex.Split(stringFile, "\r")
                If Not String.IsNullOrEmpty(cycle_1) Then
                    cycleList.Add(cycle_1)
                End If
            Next

            cycleList.RemoveAt(0)
            cycleList.RemoveAt(cycleList.Count - 1)

            Return cycleList
        Else
            Return cycleList
        End If

    End Function

    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Dim regex As New Regex(Constants.Input.CSV.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(sourceFile.getFileInfo.FullName)

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


    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String

        Try
            mixNumber = getColumnFromCSVFile("Formule", indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(mixNumber), "0", mixNumber)
        Catch ex As Exception
            Return "0"
        End Try
    End Function


    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        'Dim time As String

        'Try
        '    time = getColumnFromCSVFile("Heure", indexCycle, sourceFile)
        '    Return If(String.IsNullOrEmpty(time), "0.00", time)
        'Catch ex As Exception
        '    Return "0.00"
        'End Try


        Return Date.Now
    End Function


    Public Overrides Function getAdditiveActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAdditiveDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAdditiveMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAdditiveTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Protected Overrides Function getColdFeederForCycle(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, cycleIndex As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDopeAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDopeAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDopeTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getFillerActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getFillerDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getFillerMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getFillerTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

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

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAggregateAsphaltPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getRecycledAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getVirginAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function
End Class
