Public Class AsphaltUsed
    Inherits MixComponentUsed

    Private density As Double
    Private recordedTemperature As Double
    Private tankId As String
    Private grade As String

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)

        Me.density = -3
        Me.recordedTemperature = -3
        Me.tankId = "-3"
    End Sub

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, recordedTemperature As Double, density As Double, tankId As String, grade As String)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)

        Me.density = density
        Me.recordedTemperature = recordedTemperature
        Me.tankId = tankId
        Me.grade = grade
    End Sub

    Public ReadOnly Property getgrade() As String
        Get
            Return grade
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
