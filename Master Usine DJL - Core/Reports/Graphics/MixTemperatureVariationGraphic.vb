﻿Imports System.Windows.Forms.DataVisualization.Charting

Public Class MixTemperatureVariationGraphic
    Inherits XYScatterGraphic

    ' #settings
    Public Property UNIT As Unit = XmlSettings.Settings.instance.Reports.TEMPERATURE_UNIT

    Private SET_POINT_SERIE As New Serie
    Private MAX_PRECISION_SERIE As New Serie
    Private MIN_PRECISION_SERIE As New Serie

    Public Sub New(debutPeriode As Date, finPeriode As Date)
        MyBase.New()

        Me.Y_TITLE = "Variation (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

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


        ' settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.SET_POINT_SERIE.IsVisibleInLegend = False
        Me.SET_POINT_SERIE.ChartType = SeriesChartType.StepLine
        Me.SET_POINT_SERIE.Color = Drawing.Color.Green
        Me.SET_POINT_SERIE.BorderWidth = 2

        Me.MAX_PRECISION_SERIE.IsVisibleInLegend = False
        Me.MAX_PRECISION_SERIE.ChartType = SeriesChartType.StepLine
        Me.MAX_PRECISION_SERIE.Color = Drawing.Color.LightGreen
        Me.MAX_PRECISION_SERIE.BorderWidth = 2

        Me.MIN_PRECISION_SERIE.IsVisibleInLegend = False
        Me.MIN_PRECISION_SERIE.ChartType = SeriesChartType.StepLine
        Me.MIN_PRECISION_SERIE.Color = Drawing.Color.LightGreen
        Me.MIN_PRECISION_SERIE.BorderWidth = 2

        Me.SIZE = New Drawing.Size(MyBase.SIZE.Width, MyBase.SIZE.Height * 0.68)

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_TEMP_VAR As Double
    Public Property MINIMUM_TEMP_VAR As Double = Celsius.UNIT.convert(1000, Me.UNIT)

    Public Sub setGraphicData(cyclesDateTime As List(Of Date), cyclesProductionSpeed As List(Of Double), virginAsphaltNameList As List(Of String), recordedTemperatureList As List(Of Double), targetTemperatureList As List(Of Double))
        For indexCycle As Integer = 0 To cyclesDateTime.Count - 1 Step 1

            If (cyclesProductionSpeed(indexCycle) > 0) Then

                If Not Double.IsNaN(recordedTemperatureList(indexCycle)) And Not Double.IsNaN(targetTemperatureList(indexCycle)) Then

                    If Not (recordedTemperatureList(indexCycle) >= 200 Or recordedTemperatureList(indexCycle) <= 100) Then

                        Dim cycleDateTime = cyclesDateTime(indexCycle)
                        Dim temperatureVariation As Double = recordedTemperatureList(indexCycle) - targetTemperatureList(indexCycle)

                        Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, temperatureVariation)

                        With XYScatterGraphic.pointFormatList_asphalt.getFormatFor("", virginAsphaltNameList(indexCycle))
                            Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                            Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
                        End With

                        ' To be removed when data accessible
                        If (temperatureVariation > Me.MAXIMUM_TEMP_VAR) Then

                            Me.MAXIMUM_TEMP_VAR = temperatureVariation

                        End If
                        If (temperatureVariation < Me.MINIMUM_TEMP_VAR) Then

                            Me.MINIMUM_TEMP_VAR = temperatureVariation

                        End If
                    End If

                End If

            End If

        Next

    End Sub

    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As XmlSettings.DataFileNode)

        If (Not Double.IsNaN(cycle.TEMPERATURE_VARIATION) AndAlso cycle.SET_POINT_TEMPERATURE > 0) Then

            If (Not MixStatistics.isAbberanteTemperatureValue(cycle.TEMPERATURE_VARIATION, cycle.SET_POINT_TEMPERATURE)) Then

                Dim cycleDateTime = cycle.TIME
                Dim cycleMixTempVar = DirectCast(dataFileNode.getUnitByTag(cycle.TEMPERATURE_VARIATION_TAG), TemperatureUnit).unitConvert(cycle.TEMPERATURE_VARIATION, Me.UNIT)

                Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleMixTempVar)

                With XYScatterGraphic.pointFormatList_asphalt.getFormatFor("", cycle.ASPHALT_NAME)
                    Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                    Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
                End With

                ' To be removed when data accessible
                If (cycleMixTempVar > Me.MAXIMUM_TEMP_VAR) Then

                    Me.MAXIMUM_TEMP_VAR = cycleMixTempVar

                End If
                If (cycleMixTempVar < Me.MINIMUM_TEMP_VAR) Then

                    Me.MINIMUM_TEMP_VAR = cycleMixTempVar

                End If

            End If

        End If

    End Sub


    Private Sub buildLegend()

        ' #refactor - Place them in same order than asphalt summary table
        Me.LEGEND.Enabled = True

        Dim pointsFormats = XYScatterGraphic.pointFormatList_asphalt.getAllFormats

        Dim nbItems As Integer

        If (pointsFormats.Count = 4) Then
            nbItems = 3
        Else
            nbItems = pointsFormats.Count
        End If

        For i = 0 To nbItems - 1

            Dim item As New LegendItem
            Dim _format As PointFormatList.PointFormat = pointsFormats(i)

            item.Color = _format.COLOR
            item.BorderColor = _format.COLOR
            item.Name = _format.ASPHALT_NAME
            item.ImageStyle = Windows.Forms.DataVisualization.Charting.LegendImageStyle.Marker
            item.MarkerStyle = _format.MARKER
            item.MarkerSize = 15

            Me.LEGEND.CustomItems.Add(item)

        Next

        If (pointsFormats.Count = 4) Then

            With pointsFormats.Last

                Dim othersItem As New LegendItem

                othersItem.Color = .COLOR
                othersItem.BorderColor = .COLOR
                othersItem.Name = "Autres"
                othersItem.ImageStyle = Windows.Forms.DataVisualization.Charting.LegendImageStyle.Marker
                othersItem.MarkerStyle = .MARKER
                othersItem.MarkerSize = 15

                Me.LEGEND.CustomItems.Add(othersItem)

            End With
        End If

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            If (Me.MAXIMUM_TEMP_VAR >= Math.Abs(Me.MINIMUM_TEMP_VAR)) Then
                Me.Y_MAXIMUM = (Me.MAXIMUM_TEMP_VAR \ Celsius.UNIT.unitConvert(5, Me.UNIT) + 1) * Celsius.UNIT.unitConvert(5, Me.UNIT)
                Me.Y_MINIMUM = 0 - Me.Y_MAXIMUM
            Else
                Me.Y_MINIMUM = (Me.MINIMUM_TEMP_VAR \ Celsius.UNIT.unitConvert(5, Me.UNIT) - 1) * Celsius.UNIT.unitConvert(5, Me.UNIT)
                Me.Y_MAXIMUM = 0 - Me.Y_MINIMUM
            End If

            Y_INTERVAL = (Me.Y_MAXIMUM - Me.Y_MINIMUM) / 5


            Me.SET_POINT_SERIE.Points.AddXY(Me.X_MINIMUM, 0)
            Me.SET_POINT_SERIE.Points.AddXY(Me.X_MAXIMUM, 0)

            Me.MAX_PRECISION_SERIE.Points.AddXY(Me.X_MINIMUM, Celsius.UNIT.unitConvert(7, Me.UNIT))
            Me.MAX_PRECISION_SERIE.Points.AddXY(Me.X_MAXIMUM, Celsius.UNIT.unitConvert(7, Me.UNIT))

            Me.MIN_PRECISION_SERIE.Points.AddXY(Me.X_MINIMUM, Celsius.UNIT.unitConvert(-7, Me.UNIT))
            Me.MIN_PRECISION_SERIE.Points.AddXY(Me.X_MAXIMUM, Celsius.UNIT.unitConvert(-7, Me.UNIT))


            Me.chart.Series.Add(Me.SET_POINT_SERIE)
            Me.chart.Series.Add(Me.MAX_PRECISION_SERIE)
            Me.chart.Series.Add(Me.MIN_PRECISION_SERIE)

            Me.buildLegend()

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableMixTempVariation_FR.bmp"
        End Get
    End Property
End Class
