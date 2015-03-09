Public Class ProductionCycle

    Private endOfCycle As Date
    Private producedMix As ProducedMix
    Private dustRemovalDebit As Double
    Private siloFillingNumber As String
    Private bagHouseDiff As Double
    Private coldFeederList As List(Of ColdFeeder)
    Private hotFeederList As List(Of HotFeeder)
    Private recycledAsphaltUsed As RecycledAsphaltUsed
    Private totalAsphaltUsed As AsphaltUsed
    Private dureeCycle As Double
    Private dureeMalaxHumide As Double
    Private dureeMalaxSec As Double
    Private manuelle As Boolean

    Sub New(endOfCycle As Date, producedMix As ProducedMix, coldFeederList As List(Of ColdFeeder), hotFeederList As List(Of HotFeeder), totalAsphaltUsed As AsphaltUsed, dustRemovalDebit As Double, siloFillingNumber As String, bagHouseDiff As Double,
            dureeCycle As Double, dureeMalaxHumide As Double, dureeMalaxSec As Double, manuelle As Boolean)

        Me.endOfCycle = endOfCycle
        Me.producedMix = producedMix
        Me.dustRemovalDebit = dustRemovalDebit
        Me.siloFillingNumber = siloFillingNumber
        Me.bagHouseDiff = bagHouseDiff
        Me.coldFeederList = coldFeederList
        Me.hotFeederList = hotFeederList
        Me.recycledAsphaltUsed = recycledAsphaltUsed
        Me.totalAsphaltUsed = totalAsphaltUsed
        Me.dureeCycle = dureeCycle
        Me.dureeMalaxHumide = dureeMalaxHumide
        Me.dureeMalaxSec = dureeMalaxSec
        Me.manuelle = manuelle

    End Sub

    Public ReadOnly Property getManuelle As Boolean
        Get
            Return Me.manuelle
        End Get
    End Property

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


    Public ReadOnly Property getColdFeederList As List(Of ColdFeeder)
        Get
            Return coldFeederList
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

    Public Function getAsphaltName() As String
        Return Plant.asphaltCatalog.getDescriptionFromContainer(totalAsphaltUsed.getTankId, endOfCycle)
    End Function

    Public ReadOnly Property getDureeCycle As Double
        Get
            Return Me.dureeCycle
        End Get
    End Property

    Public ReadOnly Property getDureeMalaxHumide As Double
        Get
            Return Me.dureeMalaxHumide
        End Get
    End Property

    Public ReadOnly Property getDureeMalaxSec As Double
        Get
            Return Me.dureeMalaxSec
        End Get
    End Property
End Class
