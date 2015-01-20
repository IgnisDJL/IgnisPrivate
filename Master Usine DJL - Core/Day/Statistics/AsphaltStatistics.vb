Imports IGNIS.XmlSettings

Public Class AsphaltStatistics
    Inherits ProductionStatistics

    Public Shared ReadOnly ASPHALT_POURCENTAGE_ABBERANCE_THRESHOLD_PERCENTAGE As Double = 0.05

    Private isOutLimitCycle As Boolean = False

    Private _nbCyclesWithAberrantPercentage As Integer = 0

    Public Property TANK As String

    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As DataFileNode)

        If (Not TypeOf cycle Is LOGCycle Or cycle.PRODUCTION_SPEED > 0) Then

            Me.CYCLES.Add(cycle)

            Me.CYCLE_ASPHALT_MASS = dataFileNode.getUnitByTag(cycle.ASPHALT_MASS_TAG).convert(cycle.ASPHALT_MASS, Settings.instance.Reports.MASS_UNIT)
            Me.CYCLE_MIX_MASS = dataFileNode.getUnitByTag(cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, Settings.instance.Reports.MASS_UNIT)
            Me.CYCLE_TEMPERATURE = dataFileNode.getUnitByTag(cycle.TEMPERATURE_TAG).convert(cycle.TEMPERATURE, Settings.instance.Reports.TEMPERATURE_UNIT)

            If (Not Double.IsNaN(cycle.ASPHALT_PERCENTAGE_VARIATION) AndAlso _
                cycle.ASPHALT_PERCENTAGE_VARIATION > ASPHALT_POURCENTAGE_ABBERANCE_THRESHOLD_PERCENTAGE * cycle.ASPHALT_SET_POINT_PERCENTAGE) Then

                _nbCyclesWithAberrantPercentage += 1
            End If
        End If

    End Sub

    ' Mass related
    Private accumulatedMass As Double
    Public WriteOnly Property CYCLE_ASPHALT_MASS As Double
        Set(value As Double)
            Me.accumulatedMass += value

        End Set
    End Property

    Public Overrides ReadOnly Property TOTAL_MASS As Double
        Get
            Return Me.accumulatedMass
        End Get
    End Property

    ' Must be input after temp because of isOutLimitCycle
    Public WriteOnly Property CYCLE_MIX_MASS As Double
        Set(value As Double)

            Me.totalMixMass += value

            If (Me.isOutLimitCycle) Then
                Me.outLimitMixMass += value
                Me.isOutLimitCycle = False
            End If

        End Set
    End Property

    Private totalMixMass As Double
    Public ReadOnly Property BELONGING_MIX_MASS As Double
        Get
            Return Me.totalMixMass
        End Get
    End Property

    Private outLimitMixMass As Double
    Public ReadOnly Property OUT_TEMPERATURE_LIMIT_MASS As Double
        Get
            Return Me.outLimitMixMass
        End Get
    End Property

    ' Temperature related
    Public Property SET_POINT_TEMPERATURE As Double

    Private temperatureSum As Double
    Public WriteOnly Property CYCLE_TEMPERATURE As Double
        Set(value As Double)

            Me.temperatureSum += value

            If (value > Me.maxTemperature) Then
                Me.maxTemperature = value
            End If

            If (value < Me.minTemperature) Then
                Me.minTemperature = value
            End If

            ' #settings
            If (Me.SET_POINT_TEMPERATURE > Celsius.UNIT.convert(0, Settings.instance.Reports.TEMPERATURE_UNIT)) Then

                If (value > Me.SET_POINT_TEMPERATURE + Celsius.UNIT.unitConvert(7, Settings.instance.Reports.TEMPERATURE_UNIT)) Then
                    Me.nbCycleAboveTempLimit += 1
                    isOutLimitCycle = True
                End If

                If (value < Me.SET_POINT_TEMPERATURE - Celsius.UNIT.unitConvert(7, Settings.instance.Reports.TEMPERATURE_UNIT)) Then
                    Me.nbCycleBelowTempLimit += 1
                    isOutLimitCycle = True
                End If

            Else

                If (value > Celsius.UNIT.convert(170, Settings.instance.Reports.TEMPERATURE_UNIT)) Then
                    Me.nbCycleAboveTempLimit += 1
                    isOutLimitCycle = True
                End If

            End If

        End Set
    End Property

    Public ReadOnly Property AVERAGE_TEMPERATURE As Double
        Get
            Return Me.temperatureSum / Me.CYCLES.Count
        End Get
    End Property

    Private maxTemperature As Integer = 0
    Public ReadOnly Property MAX_TEMPERATURE As Double
        Get
            Return Me.maxTemperature
        End Get
    End Property

    Private minTemperature As Integer = 10000
    Public ReadOnly Property MIN_TEMPERATURE As Double
        Get
            Return Me.minTemperature
        End Get
    End Property

    Private nbCycleBelowTempLimit As Integer = 0
    Public ReadOnly Property BELOW_TEMPERATURE_LIMIT_PERCENTAGE As Double
        Get
            Return PerOne.UNIT.convert(Me.nbCycleBelowTempLimit / Me.CYCLES.Count, Settings.instance.Reports.PERCENT_UNIT)
        End Get
    End Property

    Private nbCycleAboveTempLimit As Integer = 0
    Public ReadOnly Property ABOVE_TEMPERATURE_LIMIT_PERCENTAGE As Double
        Get
            Return PerOne.UNIT.convert(Me.nbCycleAboveTempLimit / Me.CYCLES.Count, Settings.instance.Reports.PERCENT_UNIT)
        End Get
    End Property

    ' Percentage related
    Public ReadOnly Property OUT_LIMIT_PERCENTAGE As Double
        Get
            Return BELOW_TEMPERATURE_LIMIT_PERCENTAGE + ABOVE_TEMPERATURE_LIMIT_PERCENTAGE
        End Get
    End Property

    Public ReadOnly Property NB_CYCLE_WITH_ABERRANT_PERCENTAGE As Integer
        Get
            Return _nbCyclesWithAberrantPercentage
        End Get
    End Property

End Class
