Public Class AsphaltUsed
    Inherits MixComponentUsed

    Private density As Double
    Private recordedTemperature As Double
    Private tankId As String
    Private rank As String

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)

        Me.density = -3
        Me.recordedTemperature = -3
        Me.tankId = "-3"
    End Sub

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, recordedTemperature As Double, density As Double, tankId As String, rank As String)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)

        Me.density = density
        Me.recordedTemperature = recordedTemperature
        Me.tankId = tankId
        Me.rank = rank
    End Sub

    Public ReadOnly Property getRank() As Double
        Get
            Return rank
        End Get
    End Property

    Public ReadOnly Property getDensity() As Double
        Get
            Return density
        End Get
    End Property

    Public ReadOnly Property getRecordedTemperature() As Double
        Get
            Return recordedTemperature
        End Get
    End Property

    Public ReadOnly Property getTankId() As Double
        Get
            Return tankId
        End Get
    End Property
End Class
