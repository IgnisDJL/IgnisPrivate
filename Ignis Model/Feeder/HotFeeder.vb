Public Class HotFeeder
    Inherits Feeder_1
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

    Public Overrides Function isRecycled() As Boolean
        Return False
    End Function
End Class
