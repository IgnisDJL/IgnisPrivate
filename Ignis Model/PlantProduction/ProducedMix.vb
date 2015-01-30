Public Class ProducedMix
    Private mixNumber As String
    Private mixName As String
    Private recordedTemperature As Double
    Private mixDebit As Double
    Private mixCounter As Double

    Sub New(mixNumber As String, mixName As String, recordedTemperature As Double, mixDebit As Double, mixCounter As Double)

        Me.mixNumber = mixNumber
        Me.mixName = mixName
        Me.recordedTemperature = recordedTemperature
        Me.mixDebit = mixDebit
        Me.mixCounter = mixCounter

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

    Public ReadOnly Property getMixCounter As Double
        Get
            Return mixCounter
        End Get
    End Property

    Public ReadOnly Property getMixDebit As Double
        Get
            Return mixDebit
        End Get
    End Property

End Class
