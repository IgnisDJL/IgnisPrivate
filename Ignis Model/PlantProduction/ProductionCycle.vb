Public Class ProductionCycle

    Private endOfCycle As Date
    Private producedMix As ProducedMix
    Private fillerUsed As FillerUsed
    Private additiveUsed As AdditiveUsed
    Private dustRemovalDebit As Double
    Private siloFillingNumber As String
    Private bagHouseDiff As Double
    Private asphaltDensity As Double
    Private coldFeederList As List(Of ColdFeeder)
    Private virginAsphaltUsed As AsphaltUsed
    Private recycledAsphaltUsed As RecycledAsphaltUsed
    Private totalAsphaltUsed As AsphaltUsed
    Private virginAggregateUsed As AggregateUsed
    Private recycledAggregateUsed As RecycledAggregateUsed
    Private asphaltTankId As String
    Private asphaltRecordedTemperature As Double



    Sub New(asphaltTankId As String, asphaltRecordedTemperature As Double, endOfCycle As Date, mixProduced As ProducedMix, coldFeederList As List(Of ColdFeeder), virginAsphaltUsed As AsphaltUsed, recycledAsphaltUsed As RecycledAsphaltUsed, totalAsphaltUsed As AsphaltUsed, virginAggregateUsed As AggregateUsed, recycledAggregateUsed As RecycledAggregateUsed, fillerUsed As FillerUsed, additiveUsed As AdditiveUsed, dustRemovalDebit As Double, siloFillingNumber As String, bagHouseDiff As Double, asphaltDensity As Double)
        Me.asphaltTankId = asphaltTankId
        Me.asphaltRecordedTemperature = asphaltRecordedTemperature
        Me.endOfCycle = endOfCycle
        Me.producedMix = producedMix
        Me.fillerUsed = fillerUsed
        Me.additiveUsed = additiveUsed
        Me.dustRemovalDebit = dustRemovalDebit
        Me.siloFillingNumber = siloFillingNumber
        Me.bagHouseDiff = bagHouseDiff
        Me.asphaltDensity = asphaltDensity
        Me.coldFeederList = coldFeederList
        Me.virginAsphaltUsed = virginAsphaltUsed
        Me.recycledAsphaltUsed = recycledAsphaltUsed
        Me.totalAsphaltUsed = totalAsphaltUsed
        Me.virginAggregateUsed = virginAggregateUsed
        Me.recycledAggregateUsed = recycledAggregateUsed

    End Sub

    Public ReadOnly Property getEndOfCycle As Date
        Get
            Return endOfCycle
        End Get
    End Property

    Public ReadOnly Property getProducedMix As ProducedMix
        Get
            Return producedMix
        End Get
    End Property

    Public ReadOnly Property getFillerUsed As FillerUsed
        Get
            Return fillerUsed
        End Get
    End Property

    Public ReadOnly Property getAdditiveUsed As AdditiveUsed
        Get
            Return additiveUsed
        End Get
    End Property

    Public ReadOnly Property getDustRemovalDebit As Double
        Get
            Return dustRemovalDebit
        End Get
    End Property


    Public ReadOnly Property getSiloFillingNumber As String
        Get
            Return siloFillingNumber
        End Get
    End Property

    Public ReadOnly Property getBagHouseDiff As Double
        Get
            Return bagHouseDiff
        End Get
    End Property

    Public ReadOnly Property getAsphaltDensity As Double
        Get
            Return asphaltDensity
        End Get
    End Property

    Public ReadOnly Property getColdFeederList As List(Of ColdFeeder)
        Get
            Return coldFeederList
        End Get
    End Property

    Public ReadOnly Property getVirginAsphaltUsed As AsphaltUsed
        Get
            Return virginAsphaltUsed
        End Get
    End Property


    Public ReadOnly Property getRecycledAsphaltUsed As RecycledAsphaltUsed
        Get
            Return recycledAsphaltUsed
        End Get
    End Property

    Public ReadOnly Property getTotalAsphaltUsed As AsphaltUsed
        Get
            Return totalAsphaltUsed
        End Get
    End Property

    Public ReadOnly Property getVirginAggregateUsed As AggregateUsed
        Get
            Return virginAggregateUsed
        End Get
    End Property

    Public ReadOnly Property getRecycledAggregateUsed As RecycledAggregateUsed
        Get
            Return recycledAggregateUsed
        End Get
    End Property

    Public Function getAsphaltName() As String
        Return Plant.asphaltCatalog.getDescriptionFromContainer(asphaltTankId, endOfCycle)
    End Function

    Public ReadOnly Property getRecordedTemperature As Double
        Get
            Return asphaltRecordedTemperature
        End Get
    End Property

    Public ReadOnly Property getAsphaltTankId As String
        Get
            Return asphaltTankId
        End Get
    End Property
End Class
