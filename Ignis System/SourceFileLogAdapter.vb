Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class SourceFileLogAdapter
    Inherits SourceFileAdapter

    Public Sub New()
    End Sub

    Public Overrides Function getAdditiveActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getAdditiveDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getAdditiveMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getAdditiveTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Protected Overrides Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getCycleCount(sourceFile As SourceFile) As Integer
        Return Nothing
    End Function

    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Dim regex As New Regex(Constants.Input.LOG.FILE_NAME_REGEX)
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
        Return Nothing
    End Function

    Public Overrides Function getFeederActualPercentage(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
        Return Nothing
    End Function

    Public Overrides Function getFeederDebit(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Protected Overrides Function getFeederForCycle(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFeederID(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFeederMass(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFeederMoisturePercentage(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFeederRecycledAsphaltPercentage(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFeederTargetPercentage(indexFeeder As String, cycleIndex As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFillerActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFillerDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFillerMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getFillerTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAggregateAsphaltPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getRecycledAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function


    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        Return Nothing
    End Function

    Public Overrides Function getTotalAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getVirginAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function
End Class
