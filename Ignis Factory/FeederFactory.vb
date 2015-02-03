Imports System.Globalization

Public Class FeederFactory

    Private mixComponentUsedFactory As MixComponentUsedFactory

    Public Sub New()
        mixComponentUsedFactory = New MixComponentUsedFactory
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub

    Public Function createFeeder(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As Feeder_1
        Dim feeder As Feeder_1

        feeder = New Feeder_1(sourceFile.sourceFileAdapter.getFeederID(indexFeeder, indexCycle, sourceFile))
        feeder.setAggregateUsed(getAggregateUsedForFeeder(indexFeeder, indexCycle, sourceFile))

        Return feeder
    End Function

    Public Function createFeederList(indexCycle As Integer, sourceFile As SourceFile) As List(Of Feeder_1)
        Dim feederList As List(Of Feeder_1) = New List(Of Feeder_1)

        For indexFeeder As Integer = 0 To sourceFile.sourceFileAdapter.getFeederCountForCycle(indexCycle, sourceFile) - 1
            feederList.Add(createFeeder(indexFeeder, indexCycle, sourceFile))
        Next

        Return feederList
    End Function

    Private Function getAggregateUsedForFeeder(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As AggregateUsed
        Dim asphaltPercentage As Double

        asphaltPercentage = sourceFile.sourceFileAdapter.getFeederRecycledAsphaltPercentage(indexFeeder, indexCycle, sourceFile)

        If String.IsNullOrEmpty(asphaltPercentage) Then
            Return mixComponentUsedFactory.createAggregatetUsed(indexFeeder, indexCycle, sourceFile)
        ElseIf True Then

            Return mixComponentUsedFactory.createRecycledAggregateUsed(indexFeeder, indexCycle, sourceFile)
        End If

        Return Nothing
    End Function

End Class
