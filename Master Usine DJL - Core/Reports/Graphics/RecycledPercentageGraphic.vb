Public Class RecycledPercentageGraphic
    Inherits XYScatterGraphic

    Public Property UNIT As Unit = XmlSettings.Settings.instance.Reports.PERCENT_UNIT

    Private setPointPercentages As New List(Of Double)

    Public Sub New()
        MyBase.New()

        Me.Y_TITLE = "Pourcentage de recyclé (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

        ' settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.Y_LABEL_FORMAT = "0"

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.RECYCLED_PERCENTAGE_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_PERC As Double
    Public Property MINIMUM_PERC As Double = 1000

    Private WriteOnly Property DATE_ As Date
        Set(value As Date)

            Me.X_MINIMUM = value.ToOADate
            Me.X_MAXIMUM = value.Add(TimeSpan.FromHours(24.01)).ToOADate

        End Set
    End Property


    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As XmlSettings.DataFileNode)

        Dim cycleDateTime = cycle.TIME
        Dim cycleRealPercentage = dataFileNode.getUnitByTag(cycle.RECYCLED_PERCENTAGE_TAG).convert(cycle.RECYCLED_PERCENTAGE, Me.UNIT)
        Dim cycleSetPointPercentage = dataFileNode.getUnitByTag(cycle.RECYCLED_SET_POINT_PERCENTAGE_TAG).convert(cycle.RECYCLED_SET_POINT_PERCENTAGE, Me.UNIT)

        Dim lastPointFormat = XYScatterGraphic.pointFormatList_mix.getFormatFor(cycle.MIX_NAME, cycle.ASPHALT_NAME)

        If (Not Double.IsNaN(cycleRealPercentage) And cycle.PRODUCTION_SPEED > 0) Then
            'If (Not Double.IsNaN(cycleRealPercentage) And cycleSetPointPercentage > 0 And cycle.PRODUCTION_SPEED > 0) Then

            Me.setPointPercentages.Add(cycleSetPointPercentage)
            Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleRealPercentage)

            With lastPointFormat
                Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
            End With

            ' To be removed when data accessible
            If (cycleRealPercentage > Me.MAXIMUM_PERC) Then

                Me.MAXIMUM_PERC = cycleRealPercentage

            ElseIf (cycleRealPercentage < Me.MINIMUM_PERC) Then

                Me.MINIMUM_PERC = cycleRealPercentage

            End If

            ' To be removed when data accessible
            If (cycleSetPointPercentage > Me.MAXIMUM_PERC) Then

                Me.MAXIMUM_PERC = cycleSetPointPercentage

            ElseIf (cycleSetPointPercentage < Me.MINIMUM_PERC) Then

                Me.MINIMUM_PERC = cycleSetPointPercentage

            End If

        End If

    End Sub

    Private Sub buildLegend()

        Me.LEGEND.Enabled = True

        Dim pointsFormats = XYScatterGraphic.pointFormatList_mix.getAllFormats

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
            item.Name = _format.MIX_NAME & " (" & _format.ASPHALT_NAME & ")"
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

        Dim setPointItem As New LegendItem

        setPointItem.Color = Drawing.Color.Green
        setPointItem.BorderColor = Drawing.Color.Green
        setPointItem.Name = "% visé"
        setPointItem.ImageStyle = Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line
        setPointItem.BorderWidth = 100

        Me.LEGEND.CustomItems.Add(setPointItem)

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            ' Me.DATE_ = ProductionDay.CURRENT_DATE

            For Each setPoint In Me.setPointPercentages.Distinct
                Dim setPointSerie As New Serie()
                setPointSerie.Color = Drawing.Color.Green
                setPointSerie.ChartType = Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
                setPointSerie.BorderWidth = 2
                setPointSerie.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMinByValue("X").XValue, setPoint)
                setPointSerie.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMaxByValue("X").XValue, setPoint)
                Me.chart.Series.Add(setPointSerie)
            Next

            Me.Y_MAXIMUM = (Me.MAXIMUM_PERC * 1000 \ Percent.UNIT.convert(2 * 1000, Me.UNIT) + 1) * Percent.UNIT.convert(2 * 1000, Me.UNIT) / 1000

            Me.Y_MINIMUM = 0

            Me.Y_INTERVAL = Percent.UNIT.convert(2, Me.UNIT)

            Me.buildLegend()

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableRecycledPercentage_FR.bmp"
        End Get
    End Property
End Class
