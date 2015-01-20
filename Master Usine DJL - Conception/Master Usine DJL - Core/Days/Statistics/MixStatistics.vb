Imports IGNIS.XmlSettings

Public Class MixStatistics
    Inherits ProductionStatistics

    ' #settings
    Public Shared ReadOnly TOLERENCE_PRECISION As Double = Percent.UNIT.convert(0.22, Settings.instance.Report.Word.PERCENT_UNIT)
    Public Shared ReadOnly CONTROL_PRECISION As Double = Percent.UNIT.convert(0.33, Settings.instance.Report.Word.PERCENT_UNIT)

    Private setPointIsSet As Boolean = False
    Private isOutControleLimit As Boolean = False

    Public Property PRODUCTION_TYPE As String
    Public Property FORMULA_NAME As String

    Private _asphaltStats As New AsphaltStatistics
    Public ReadOnly Property ASPHALT_STATS As AsphaltStatistics
        Get
            Return Me._asphaltStats
        End Get
    End Property

    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As DataFileNode)

        Me.CYCLES.Add(cycle)

        Me.CYCLE_TIME = cycle.DURATION
        Me.ASPHALT_SET_POINT_PERCENTAGE = dataFileNode.getUnitByTag(cycle.ASPHALT_SET_POINT_PERCENTAGE_TAG).convert(cycle.ASPHALT_SET_POINT_PERCENTAGE, Settings.instance.Report.Word.PERCENT_UNIT)
        Me.ASPHALT_STATS.CYCLE_ASPHALT_MASS = dataFileNode.getUnitByTag(cycle.ASPHALT_MASS_TAG).convert(cycle.ASPHALT_MASS, Settings.instance.Report.Word.MASS_UNIT)


        'If (NAME = "eb14re20ad5828") Then
        '    Debugger.Break()
        'End If
        If (cycle.PRODUCTION_SPEED > 0 Or cycle.ASPHALT_SET_POINT_PERCENTAGE > 0) Then
            Me.CYCLE_ASPHALT_PERCENTAGE = dataFileNode.getUnitByTag(cycle.ASPHALT_PERCENTAGE_TAG).convert(cycle.ASPHALT_PERCENTAGE, Settings.instance.Report.Word.PERCENT_UNIT)
        End If

        If (cycle.PRODUCTION_SPEED > 0) Then
            Me.CYCLE_MASS = dataFileNode.getUnitByTag(cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, Settings.instance.Report.Word.MASS_UNIT)

            If (Double.IsNaN(cycle.RECYCLED_SET_POINT_PERCENTAGE)) Then

                Me.SET_POINT_RECYCLED_PERCENTAGE = cycle.RECYCLED_SET_POINT_PERCENTAGE

            Else

                Me.CYCLE_RECYCLED_MASS = dataFileNode.getUnitByTag(cycle.RECYCLED_MASS_TAG).convert(cycle.RECYCLED_MASS, Settings.instance.Report.Word.MASS_UNIT)
                Me.SET_POINT_RECYCLED_PERCENTAGE = cycle.RECYCLED_SET_POINT_PERCENTAGE

            End If

            Me.CYCLE_RECYCLED_PERCENTAGE = dataFileNode.getUnitByTag(cycle.RECYCLED_PERCENTAGE_TAG).convert(cycle.RECYCLED_PERCENTAGE, Settings.instance.Report.Word.PERCENT_UNIT)

        Else

        End If

    End Sub

    ' Mass related
    Public WriteOnly Property CYCLE_MASS As Double
        Set(value As Double)
            Me.totalMass += value
            Me._asphaltStats.CYCLE_MIX_MASS = Me.totalMass

            If (Me.isOutControleLimit) Then
                Me.outControlMass += value
                Me.isOutControleLimit = False
            End If

        End Set
    End Property

    Private totalMass As Double
    Public Overrides ReadOnly Property TOTAL_MASS As Double
        Get
            Return Me.totalMass
        End Get
    End Property

    ' Time related
    Public WriteOnly Property CYCLE_TIME As TimeSpan
        Set(value As TimeSpan)
            Me.totalTime += value
        End Set
    End Property

    Private totalTime As TimeSpan
    Public ReadOnly Property TOTAL_TIME As TimeSpan
        Get
            Return Me.totalTime
        End Get
    End Property

    Private averageProductionSpeed As Double
    Public ReadOnly Property AVERAGE_PRODUCTION_SPEED As Double
        Get

            If (Me.TOTAL_TIME.Equals(TimeSpan.Zero)) Then

                Return 0

            Else

                If (Me.averageProductionSpeed = 0) Then
                    Me.averageProductionSpeed = Settings.instance.Report.Word.MASS_UNIT.convert(Me.TOTAL_MASS, Tons.UNIT) / Me.TOTAL_TIME.TotalHours
                End If

                Return TonsPerHour.UNIT.convert(Me.averageProductionSpeed, Settings.instance.Report.Word.PRODUCTION_SPEED_UNIT)

            End If

        End Get
    End Property


    ' Recycled Percentage related
    Public WriteOnly Property CYCLE_RECYCLED_PERCENTAGE As Double
        Set(value As Double)
            Me.recycledPercentages.Add(value)
        End Set
    End Property

    Public WriteOnly Property CYCLE_RECYCLED_MASS As Double
        Set(value As Double)
            If (Double.IsNaN(Me.recycledMassSum)) Then
                Me.recycledMassSum = value
            Else
                Me.recycledMassSum += value
            End If
        End Set
    End Property

    Public Property SET_POINT_RECYCLED_PERCENTAGE As Double = 0

    Private recycledPercentages As New List(Of Double)
    Public ReadOnly Property AVERAGE_RECYCLED_PERCENTAGE As Double
        Get
            If (Me.recycledPercentages.Count > 0) Then

                If (Me.recycledPercentages.Average >= 0) Then
                    Return Me.recycledMassSum / totalMass * 100
                Else
                    Return Double.NaN
                End If
            Else
                Return 0
            End If
        End Get
    End Property

    Private recycledMassSum As Double = Double.NaN
    Public ReadOnly Property TOTAL_RECYCLED_MASS As Double
        Get
            Return Me.recycledMassSum
        End Get
    End Property

    ' Asphalt percentage related
    Private setPointList As New List(Of Double)
    Public WriteOnly Property ASPHALT_SET_POINT_PERCENTAGE As Double
        Set(value As Double)

            If (Not value = 0 And Not Double.IsNaN(value)) Then

                Me.setPointList.Add(value)

                setPointIsSet = True

            End If

        End Set
    End Property

    Public ReadOnly Property ASPHALT_MAX_SET_POINT_PERCENTAGE As Double
        Get
            If (Me.setPointList.Count > 0) Then
                Return Math.Round(Me.setPointList.Max, 2)
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    Public ReadOnly Property ASPHALT_MIN_SET_POINT_PERCENTAGE As Double
        Get
            If (Me.setPointList.Count > 0) Then
                Return Math.Round(Me.setPointList.Min, 2)
            Else
                Return Double.NaN
            End If
        End Get
    End Property

    Private percentageSum As Double
    Private nbPercentage As Integer
    ' Has to be input before cycle mass for isoutcontrol variable
    Public WriteOnly Property CYCLE_ASPHALT_PERCENTAGE As Double
        Set(value As Double)


            If (Not Double.IsNaN(value)) Then
                Me.percentageSum += value
                Me.nbPercentage += 1

                If (value > Me.maxPercentage) Then
                    Me.maxPercentage = value
                End If

                If (value < Me.minPercentage) Then
                    Me.minPercentage = value
                End If

                If (setPointIsSet) Then

                    If (value > Me.ASPHALT_MAX_SET_POINT_PERCENTAGE + CONTROL_PRECISION) Then

                        Me.nbCycleAboveLimitControle += 1
                        Me.isOutControleLimit = True

                    ElseIf (value > Me.ASPHALT_MAX_SET_POINT_PERCENTAGE + TOLERENCE_PRECISION) Then

                        Me.nbCycleAboveLimitTolerence += 1

                    End If

                    If (value < Me.ASPHALT_MIN_SET_POINT_PERCENTAGE - CONTROL_PRECISION) Then

                        Me.nbCycleBelowLimitControle += 1
                        Me.isOutControleLimit = True

                    ElseIf (value < Me.ASPHALT_MIN_SET_POINT_PERCENTAGE - TOLERENCE_PRECISION) Then

                        Me.nbCycleBelowLimitTolerence += 1

                    End If

                End If

            End If

        End Set
    End Property


    Public ReadOnly Property ASPHALT_AVERAGE_PERCENTAGE As Double
        Get
            Return Me.percentageSum / Me.nbPercentage
        End Get
    End Property

    Private maxPercentage As Double
    Public ReadOnly Property ASPHALT_MAX_PERCENTAGE As Double
        Get
            Return Me.maxPercentage
        End Get
    End Property

    Private minPercentage As Double = 100
    Public ReadOnly Property ASPHALT_MIN_PERCENTAGE As Double
        Get
            Return Me.minPercentage
        End Get
    End Property

    Private nbCycleBelowLimitTolerence As Integer = 0
    Public ReadOnly Property ASPHALT_BELOW_LIMIT_TOLERENCE_PERCENTAGE As Double
        Get
            Return Me.nbCycleBelowLimitTolerence / Me.CYCLES.Count * 100
        End Get
    End Property

    Private nbCycleAboveLimitTolerence As Integer = 0
    Public ReadOnly Property ASPHALT_ABOVE_LIMIT_TOLERENCE_PERCENTAGE As Double
        Get
            Return Me.nbCycleAboveLimitTolerence / Me.CYCLES.Count * 100
        End Get
    End Property

    Public ReadOnly Property ASPHALT_OUT_LIMIT_TOLERENCE_PERCENTAGE As Double
        Get
            Return ASPHALT_ABOVE_LIMIT_TOLERENCE_PERCENTAGE + ASPHALT_BELOW_LIMIT_TOLERENCE_PERCENTAGE
        End Get
    End Property

    Private nbCycleBelowLimitControle As Integer = 0
    Public ReadOnly Property ASPHALT_BELOW_LIMIT_CONTROLE_PERCENTAGE As Double
        Get
            Return Me.nbCycleBelowLimitControle / Me.CYCLES.Count * 100
        End Get
    End Property

    Private nbCycleAboveLimitControle As Integer = 0
    Public ReadOnly Property ASPHALT_ABOVE_LIMIT_CONTROLE_PERCENTAGE As Double
        Get
            Return Me.nbCycleAboveLimitControle / Me.CYCLES.Count * 100
        End Get
    End Property

    Public ReadOnly Property ASPHALT_OUT_LIMIT_CONTROLE_PERCENTAGE As Double
        Get
            Return ASPHALT_ABOVE_LIMIT_CONTROLE_PERCENTAGE + ASPHALT_BELOW_LIMIT_CONTROLE_PERCENTAGE
        End Get
    End Property

    Private outControlMass As Double
    Public ReadOnly Property OUT_CONTROL_MASS As Double
        Get
            Return outControlMass
        End Get
    End Property

    Public Function hasSetPointPercentage() As Boolean
        Return setPointIsSet
    End Function

    ' Feeders related
    Private constinuousFeederStats As New List(Of FeedersStatistics)
    Public ReadOnly Property CONTINUOUS_FEEDERS_STATS As List(Of FeedersStatistics)
        Get
            Return Me.constinuousFeederStats
        End Get
    End Property

    Private batchFeederStats As New List(Of FeedersStatistics)
    Public ReadOnly Property BATCH_FEEDERS_STATS As List(Of FeedersStatistics)
        Get
            Return Me.batchFeederStats
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me.FORMULA_NAME & " - " & Me.NAME
    End Function

End Class
