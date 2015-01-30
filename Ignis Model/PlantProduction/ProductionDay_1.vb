Public Class ProductionDay_1
    Private _productionCycleList As List(Of ProductionCycle)
    Private productionDate As Date

    Sub New(productionDate As Date)
        Me.productionDate = productionDate
        Me._productionCycleList = New List(Of ProductionCycle)
    End Sub

    Public Property productionCycleList As List(Of ProductionCycle)
        Get
            Return _productionCycleList
        End Get
        Set(value As List(Of ProductionCycle))
            _productionCycleList = value
        End Set
    End Property

    Public Sub addProductionCycle(productionCycle As ProductionCycle)
        _productionCycleList.Add(productionCycle)
    End Sub

    Public Function getMixProducedList() As List(Of ProducedMix)

        Dim mixProducedList As List(Of ProducedMix) = New List(Of ProducedMix)

        For Each productionCycle As ProductionCycle In productionCycleList
            mixProducedList.Add(productionCycle.getProducedMix)
        Next

        Return mixProducedList
    End Function

End Class
