Public Class ProductionCycle
    Implements IComparable(Of ProductionCycle)
    Implements IEquatable(Of ProductionCycle)
    Private endOfCycle As Date
    Private producedMix As ProducedMix
    Private dustRemovalDebit As Double
    Private siloFillingNumber As String
    Private bagHouseDiff As Double
    Private coldFeederList As List(Of ColdFeeder)

    Private dureeCycle As TimeSpan
    Private dureeMalaxHumide As TimeSpan
    Private dureeMalaxSec As TimeSpan
    Private manuelle As Boolean

    Private contractID As String
    Private truckID As String

    Sub New(endOfCycle As Date, producedMix As ProducedMix, coldFeederList As List(Of ColdFeeder), dustRemovalDebit As Double, siloFillingNumber As String, bagHouseDiff As Double,
            dureeCycle As TimeSpan, dureeMalaxHumide As TimeSpan, dureeMalaxSec As TimeSpan, manuelle As Boolean, contractID As String, truckID As String)

        Me.endOfCycle = endOfCycle
        Me.producedMix = producedMix
        Me.dustRemovalDebit = dustRemovalDebit
        Me.siloFillingNumber = siloFillingNumber
        Me.bagHouseDiff = bagHouseDiff
        Me.coldFeederList = coldFeederList

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




    Public ReadOnly Property getDureeCycle As TimeSpan
        Get
            Return Me.dureeCycle
        End Get
    End Property

    Public ReadOnly Property getDureeMalaxHumide As TimeSpan
        Get
            Return Me.dureeMalaxHumide
        End Get
    End Property

    Public ReadOnly Property getDureeMalaxSec As TimeSpan
        Get
            Return Me.dureeMalaxSec
        End Get
    End Property

    Public Function getTime() As TimeSpan
        Return endOfCycle.TimeOfDay()
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

    Public Function CompareTo(compareProductionCycle As ProductionCycle) As Integer Implements IComparable(Of ProductionCycle).CompareTo
        ' A null value means that this object is greater. 
        If compareProductionCycle Is Nothing Then
            Return 1
        Else
            Return Me.endOfCycle.CompareTo(compareProductionCycle.getEndOfCycle)
        End If
    End Function

    Public Overloads Function Equals(other As ProductionCycle) As Boolean Implements IEquatable(Of ProductionCycle).Equals
        If other Is Nothing Then
            Return False
        End If
        Return (Me.endOfCycle.Equals(other.getEndOfCycle))
    End Function

End Class
