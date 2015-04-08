Public Class ProductionDay_1
    Private _productionContinue As List(Of ProductionCycle)
    Private _productionDiscontinue As List(Of ProductionCycle)
    Private sourceFileComplementPathDiscontinue As List(Of String)
    Private sourceFileComplementPathContinue As List(Of String)
    Private productionDate As Date

    Sub New(productionDate As Date)
        Me.productionDate = productionDate
        Me.productionContinue = New List(Of ProductionCycle)
        Me.productionDiscontinue = New List(Of ProductionCycle)
        Me.sourceFileComplementPathDiscontinue = New List(Of String)
        Me.sourceFileComplementPathContinue = New List(Of String)
    End Sub

    Public ReadOnly Property getProductionDate As Date
        Get
            Return productionDate
        End Get
    End Property

    Public Property productionContinue As List(Of ProductionCycle)
        Get
            Return _productionContinue
        End Get
        Set(value As List(Of ProductionCycle))
            _productionContinue = value
        End Set
    End Property

    Public Property productionDiscontinue As List(Of ProductionCycle)
        Get
            Return _productionDiscontinue
        End Get
        Set(value As List(Of ProductionCycle))
            _productionDiscontinue = value
        End Set
    End Property

    Public Sub addProductionCycleContinue(productionCycle As ProductionCycle)
        _productionContinue.Add(productionCycle)
    End Sub

    Public Sub setSourceFileComplementPathDiscontinue(pathList As List(Of String))
        sourceFileComplementPathDiscontinue = pathList
    End Sub

    Public Sub setSourceFileComplementPathContinue(pathList As List(Of String))
        sourceFileComplementPathContinue = pathList
    End Sub

    Public Function getSourceFileComplementPathContinue() As List(Of String)
        Return sourceFileComplementPathContinue
    End Function

    Public Function getsourceFileComplementPathDiscontinue() As List(Of String)

        Return sourceFileComplementPathDiscontinue
    End Function

    Public Sub addProductionCycleDiscontinue(productionCycle As ProductionCycle)
        _productionDiscontinue.Add(productionCycle)
    End Sub

    Public Function getProducedMixList_Hybrid(startPeriod As Date, endPeriod As Date) As List(Of ProducedMix)
        Return getProducedMixList(getProductionCycle_Hybrid(startPeriod, endPeriod))
    End Function

    Public Function getProducedMixList_Continue(startPeriod As Date, endPeriod As Date) As List(Of ProducedMix)

        Return getProducedMixList(getProductionCycle_Continue(startPeriod, endPeriod))
    End Function

    Public Function getProducedMixList_Discontinue(startPeriod As Date, endPeriod As Date) As List(Of ProducedMix)
        Return getProducedMixList(getProductionCycle_Discontinue(startPeriod, endPeriod))
    End Function

    Public Function getProductionCycle_Hybrid(startPeriod As Date, endPeriod As Date) As List(Of ProductionCycle)
        Dim listOfCycle = getProductionCycleForPeriod(startPeriod, endPeriod, productionContinue)
        listOfCycle.InsertRange(0, getProductionCycleForPeriod(startPeriod, endPeriod, productionDiscontinue))

        Return listOfCycle
    End Function

    Public Function getProductionCycle_Continue(startPeriod As Date, endPeriod As Date) As List(Of ProductionCycle)
        Return getProductionCycleForPeriod(startPeriod, endPeriod, productionContinue)
    End Function

    Public Function getProductionCycle_Discontinue(startPeriod As Date, endPeriod As Date) As List(Of ProductionCycle)
        Return getProductionCycleForPeriod(startPeriod, endPeriod, productionDiscontinue)
    End Function

    Private Function getProducedMixList(productionCycleList As List(Of ProductionCycle)) As List(Of ProducedMix)

        Dim mixProducedList = New List(Of ProducedMix)

        For Each productionCycle As ProductionCycle In productionCycleList

            If mixProducedList.Contains(productionCycle.getProducedMix) Then
                mixProducedList.Item(mixProducedList.IndexOf(productionCycle.getProducedMix)).addMass(productionCycle.getProducedMix.getMixMass)
            Else
                mixProducedList.Add(New ProducedMix(productionCycle.getProducedMix))
            End If
        Next

        Return mixProducedList
    End Function

    Private Function getProductionCycleForPeriod(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle)) As List(Of ProductionCycle)

        Dim productionCycleListForPeriod = New List(Of ProductionCycle)


        For Each productionCycle As ProductionCycle In productionCycleList
            Dim time = New TimeSpan(productionCycle.getEndOfCycle.Hour, productionCycle.getEndOfCycle.Minute, productionCycle.getEndOfCycle.Second)
            Dim completeDate = Me.productionDate.Add(time)

            If Date.Compare(completeDate, endPeriod) <= 0 And Date.Compare(completeDate, startPeriod) >= 0 Then
                productionCycleListForPeriod.Add(productionCycle)
            End If
        Next
        Return productionCycleListForPeriod
    End Function


    
End Class
