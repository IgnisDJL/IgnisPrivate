Public Class HotFeeder
    Inherits Feeder_1
    Implements IEquatable(Of HotFeeder)

    Private targetPercentage As Double
    Private actualPercentage As Double
    Private debit As Double
    Private mass As Double
    Private materialID As String

    Public Sub New(feederId As String, materialID As String, targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(feederId)
        Me.targetPercentage = targetPercentage
        Me.debit = debit
        Me.actualPercentage = actualPercentage
        Me.mass = mass
        Me.materialID = materialID
    End Sub

    Public Sub New(ByVal hotFeeder As HotFeeder)
        MyBase.New(hotFeeder.getFeederID)
        Me.targetPercentage = hotFeeder.getTargetPercentage
        Me.debit = hotFeeder.getDebit
        Me.actualPercentage = hotFeeder.getActualPercentage
        Me.mass = hotFeeder.getMass
        Me.materialID = hotFeeder.getMaterialID
    End Sub

    Public ReadOnly Property getTargetPercentage() As Double
        Get
            Return targetPercentage
        End Get
    End Property

    Public ReadOnly Property getActualPercentage() As Double
        Get
            Return actualPercentage
        End Get
    End Property

    Public ReadOnly Property getMass() As Double
        Get
            Return mass
        End Get
    End Property

    Public ReadOnly Property getMaterialID() As Double
        Get
            Return materialID
        End Get
    End Property

    Public ReadOnly Property getDebit() As Double
        Get
            Return debit
        End Get
    End Property

    Public Overrides Function isRecycled() As Boolean
        Return False
    End Function

    Public Sub addMass(mass As Double)
        Me.mass += mass
    End Sub

    Public Overloads Function Equals(ByVal hotFeeder As HotFeeder) As Boolean Implements IEquatable(Of HotFeeder).Equals
        If Me.getFeederID = hotFeeder.getFeederID Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
