Public Class ProductionCycle

    Private endOfCycle As Date
    Private producedMix As ProducedMix
    Private dustRemovalDebit As Double
    Private siloFillingNumber As String
    Private bagHouseDiff As Double
    Private coldFeederList As List(Of ColdFeeder)
    Private hotFeederList As List(Of HotFeeder)
    Private recycledAsphaltUsed As RapAsphaltConcrete
    Private virginAsphaltUsed As VirginAsphaltConcrete
    Private dureeCycle As Double
    Private dureeMalaxHumide As Double
    Private dureeMalaxSec As Double
    Private manuelle As Boolean

    Private contractID As String
    Private truckID As String

    Sub New(endOfCycle As Date, producedMix As ProducedMix, coldFeederList As List(Of ColdFeeder), hotFeederList As List(Of HotFeeder), virginAsphaltUsed As VirginAsphaltConcrete, dustRemovalDebit As Double, siloFillingNumber As String, bagHouseDiff As Double,
            dureeCycle As Double, dureeMalaxHumide As Double, dureeMalaxSec As Double, manuelle As Boolean, contractID As String, truckID As String)

        Me.endOfCycle = endOfCycle
        Me.producedMix = producedMix
        Me.dustRemovalDebit = dustRemovalDebit
        Me.siloFillingNumber = siloFillingNumber
        Me.bagHouseDiff = bagHouseDiff
        Me.coldFeederList = coldFeederList
        Me.hotFeederList = hotFeederList
        Me.recycledAsphaltUsed = recycledAsphaltUsed
        Me.virginAsphaltUsed = virginAsphaltUsed
        Me.dureeCycle = dureeCycle
        Me.dureeMalaxHumide = dureeMalaxHumide
        Me.dureeMalaxSec = dureeMalaxSec
        Me.manuelle = manuelle
        Me.contractID = contractID
        Me.truckID = truckID

    End Sub


    Public ReadOnly Property getContractID As String
        Get
            Return Me.contractID
        End Get
    End Property

    Public ReadOnly Property getTruckID As String
        Get
            Return truckID
        End Get
    End Property


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

    Public ReadOnly Property getHotFeederList As List(Of HotFeeder)
        Get
            Return hotFeederList
        End Get
    End Property

    Public ReadOnly Property getRecycledAsphaltUsed As RapAsphaltConcrete
        Get
            Return recycledAsphaltUsed
        End Get
    End Property

    Public ReadOnly Property getVirginAsphaltUsed As VirginAsphaltConcrete
        Get
            Return virginAsphaltUsed
        End Get
    End Property

    Public Function getAsphaltName() As String
        Return Plant.asphaltCatalog.getDescriptionFromContainer(virginAsphaltUsed.getTankId, endOfCycle)
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

    Public Function getTime() As TimeSpan
        Return endOfCycle.TimeOfDay()
    End Function

    Public Function isHotFeederEmpty() As Boolean
        Dim totalMass As Double

        For Each hotFeeder As HotFeeder In getHotFeederList()
            totalMass += hotFeeder.getMass()
        Next
        If totalMass <= 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function isColdFeederEmpty() As Boolean
        Dim totalMass As Double

        For Each coldFeeder As ColdFeeder In getColdFeederList()
            totalMass += coldFeeder.getMass()
        Next
        If totalMass <= 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function isVirginAsphaltEmpty() As Boolean

        If getVirginAsphaltUsed().getMass() <= 0 Then
            Return True
        Else
            Return False
        End If
    End Function


End Class
