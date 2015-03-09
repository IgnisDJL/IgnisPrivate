Public Class ProducedMix
    Private mixNumber As String
    Private mixName As String
    Private recordedTemperature As Double
    Private mixMass As Double

    Sub New(mixNumber As String, mixName As String, recordedTemperature As Double, mixMass As Double)

        Me.mixNumber = mixNumber
        Me.mixName = mixName
        Me.recordedTemperature = recordedTemperature
        Me.mixMass = mixMass
    End Sub


    Public ReadOnly Property getMixNumber As String
        Get
            Return mixNumber
        End Get
    End Property

    Public ReadOnly Property getMixName As String
        Get
            Return mixName
        End Get
    End Property

    Public ReadOnly Property getRecordedTemperature As Double
        Get
            Return recordedTemperature
        End Get
    End Property

    Public ReadOnly Property getMixMass As Double
        Get
            Return mixMass
        End Get
    End Property

End Class
