﻿Imports System.Windows.Forms.DataVisualization.Charting

Public Class AccumulatedMassGraphic
    Inherits XYScatterGraphic

    Public Property UNIT As Unit = XmlSettings.Settings.instance.Reports.MASS_UNIT

    Private firstPointFormat As PointFormatList.PointFormat = New PointFormatList.PointFormat("", "", Drawing.Color.Blue, MarkerStyle.Square)
    Private secondPointFormat As PointFormatList.PointFormat = New PointFormatList.PointFormat("", "", Drawing.Color.Red, MarkerStyle.Cross)
    Private totalPointFormat As PointFormatList.PointFormat = New PointFormatList.PointFormat("", "", Drawing.Color.Green, MarkerStyle.Diamond)

    Private lastPointFormat As PointFormatList.PointFormat = firstPointFormat

    ' For hybrid
    Private TOTAL_MASS_SERIE As New Serie
    Private isHybrid As Boolean
    Private notContinuousMass As Double
    Private cycleMassList As New List(Of CycleMass)

    Public Sub New(debutPeriode As Date, finPeriode As Date, Optional isHybrid As Boolean = False)
        MyBase.New()

        Dim borneDebutAxeX As Date

        If debutPeriode < New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day, 12, 0, 0) Then
            borneDebutAxeX = New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day)
            Me.X_MINIMUM = borneDebutAxeX.ToOADate
        Else
            borneDebutAxeX = New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day, 12, 0, 0)
            Me.X_MINIMUM = borneDebutAxeX.ToOADate
        End If

        If finPeriode < borneDebutAxeX + TimeSpan.FromHours(24) Then
            Me.X_MAXIMUM = (borneDebutAxeX + TimeSpan.FromHours(24)).ToOADate
        Else
            Me.X_MAXIMUM = (borneDebutAxeX + TimeSpan.FromHours(36)).ToOADate
        End If

        Me.isHybrid = isHybrid

        Me.Y_TITLE = "Tonnage cumulé (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

        ' #settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.Y_LABEL_FORMAT = "### ###"

        Me.Y_MINIMUM = 0

        Me.MAIN_DATA_SERIE.Color = Drawing.Color.Blue

        Me.TOTAL_MASS_SERIE.Color = Drawing.Color.Green
        Me.TOTAL_MASS_SERIE.ChartType = Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Me.TOTAL_MASS_SERIE.MarkerSize = 10
        Me.TOTAL_MASS_SERIE.MarkerStyle = totalPointFormat.MARKER

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_TONS As Integer


    Public Sub toggleMarkerColor()

        If (Me.lastPointFormat.Equals(Me.firstPointFormat)) Then
            Me.lastPointFormat = Me.secondPointFormat

        Else
            Me.lastPointFormat = Me.firstPointFormat
        End If

    End Sub

    Public Sub setGraphicData(cyclesDateTime As List(Of Date), cyclesMass As List(Of Double), cyclesProductionSpeed As List(Of Double))

        'Dim cycleDateTime = Cycle.TIME
        'Dim cycleTons = dataFileNode.getUnitByTag(Cycle.MIX_MASS_TAG).convert(Cycle.MIX_ACCUMULATED_MASS, Me.UNIT)

        'If (Cycle.PRODUCTION_SPEED > 0) Then

        'Me.cycleMassList.Add(New CycleMass(cycleDateTime, dataFileNode.getUnitByTag(cycle.MIX_MASS_TAG).convert(cycle.MIX_MASS, Me.UNIT)))

        '    If (TypeOf Cycle Is LOGCycle) Then
        '        cycleTons = cycleTons - Me.notContinuousMass
        '    End If

        '    Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleTons)
        '    Me.MAIN_DATA_SERIE.Points.Last.Color = Me.lastPointFormat.COLOR
        '    Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = Me.lastPointFormat.MARKER

        '    ' To be removed when data accessible
        '    If (cycleTons > Me.MAXIMUM_TONS) Then
        '        Me.MAXIMUM_TONS = cycleTons
        '    End If

        'ElseIf (isHybrid And TypeOf Cycle Is LOGCycle) Then

        '    Me.notContinuousMass += dataFileNode.getUnitByTag(Cycle.MIX_MASS_TAG).convert(Cycle.MIX_MASS, Me.UNIT)

        'End If

        Dim massAccumulee As Double = 0

        For indexCycle As Integer = 0 To cyclesDateTime.Count - 1 Step 1

            If (cyclesProductionSpeed(indexCycle) > 0) Then
                Me.cycleMassList.Add(New CycleMass(cyclesDateTime(indexCycle), cyclesMass(indexCycle)))

                massAccumulee += (cyclesMass(indexCycle))

                Me.MAIN_DATA_SERIE.Points.AddXY(cyclesDateTime(indexCycle), massAccumulee)
                Me.MAIN_DATA_SERIE.Points.Last.Color = Me.lastPointFormat.COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = Me.lastPointFormat.MARKER

                If (massAccumulee > Me.MAXIMUM_TONS) Then
                    Me.MAXIMUM_TONS = massAccumulee
                End If

            End If

        Next

    End Sub

    Private Sub buildLegend()

        Me.LEGEND.Enabled = True

        ' Maybe find a way around this item name...
        Dim batchItem As New LegendItem

        batchItem.Color = Me.firstPointFormat.COLOR
        batchItem.BorderColor = Me.firstPointFormat.COLOR
        batchItem.Name = "Discontinu"
        batchItem.ImageStyle = LegendImageStyle.Marker
        batchItem.MarkerSize = 15
        batchItem.MarkerStyle = Me.firstPointFormat.MARKER

        Me.LEGEND.CustomItems.Add(batchItem)

        Dim continuousItem As New LegendItem

        continuousItem.Color = Me.secondPointFormat.COLOR
        continuousItem.BorderColor = Me.secondPointFormat.COLOR
        continuousItem.Name = "Continu"
        continuousItem.ImageStyle = LegendImageStyle.Marker
        continuousItem.MarkerSize = 15
        continuousItem.MarkerStyle = Me.secondPointFormat.MARKER

        Me.LEGEND.CustomItems.Add(continuousItem)

        Dim totalMassItem As New LegendItem

        totalMassItem.Color = Me.totalPointFormat.COLOR
        totalMassItem.BorderColor = Me.totalPointFormat.COLOR
        totalMassItem.Name = "Total"
        totalMassItem.ImageStyle = LegendImageStyle.Marker
        totalMassItem.MarkerSize = 15
        totalMassItem.MarkerStyle = Me.totalPointFormat.MARKER

        Me.LEGEND.CustomItems.Add(totalMassItem)

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            If (Me.isHybrid) Then

                buildLegend()

                Dim totalMass As Double = 0

                Me.cycleMassList.Sort()
                For Each cycle In Me.cycleMassList
                    totalMass += cycle.CYCLE_MASS
                    Me.TOTAL_MASS_SERIE.Points.AddXY(cycle.TIME, totalMass)
                Next

                Me.chart.Series.Add(Me.TOTAL_MASS_SERIE)

                ' #Settings
                Me.Y_MAXIMUM = ((totalMass \ Tons.UNIT.convert(200, Me.UNIT)) + 1) * Tons.UNIT.convert(200, Me.UNIT)

            Else

                Me.Y_MAXIMUM = ((Me.MAXIMUM_TONS \ Tons.UNIT.convert(200, Me.UNIT)) + 1) * Tons.UNIT.convert(200, Me.UNIT)

            End If

            Me.Y_INTERVAL = (MyBase.Y_MAXIMUM - MyBase.Y_MINIMUM) / 10

        End If

        MyBase.consolidate()

    End Sub

    ' Good luck explaining that...
    Private Class CycleMass
        Implements IComparable(Of CycleMass)

        Public Property TIME As Date
        Public Property CYCLE_MASS As Double

        Public Sub New(cycleTime As Date, cycleMass As Double)
            Me.TIME = cycleTime
            Me.CYCLE_MASS = cycleMass
        End Sub

        Public Function CompareTo(other As CycleMass) As Integer Implements IComparable(Of CycleMass).CompareTo

            Return Me.TIME.CompareTo(other.TIME)

        End Function

    End Class

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableProduction_FR.bmp"
        End Get
    End Property

    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As XmlSettings.DataFileNode)

    End Sub
End Class
