Imports System.Windows.Forms.DataVisualization.Charting

Public Class AsphaltPercentageVariationGraphic
    Inherits Graphic

    Public Property UNIT As Unit = XmlSettings.Settings.instance.Report.Word.PERCENT_UNIT

    Private SET_POINT_SERIE As New Serie
    Private MAX_PRECISION_SERIE1 As New Serie
    Private MIN_PRECISION_SERIE1 As New Serie

    Private MAX_PRECISION_SERIE2 As New Serie
    Private MIN_PRECISION_SERIE2 As New Serie


    Public Sub New()
        MyBase.New()


        Me.Y_TITLE = "Variation (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.Y_LABEL_FORMAT = "0.0"

        Me.X_LABEL_ORENTATION = 0

        ' settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.SET_POINT_SERIE.IsVisibleInLegend = False
        Me.SET_POINT_SERIE.ChartType = SeriesChartType.StepLine
        Me.SET_POINT_SERIE.Color = Drawing.Color.Green
        Me.SET_POINT_SERIE.BorderWidth = 2


        Me.MAX_PRECISION_SERIE1.IsVisibleInLegend = False
        Me.MAX_PRECISION_SERIE1.ChartType = SeriesChartType.StepLine
        Me.MAX_PRECISION_SERIE1.Color = Drawing.Color.LightGreen
        Me.MAX_PRECISION_SERIE1.BorderWidth = 2

        Me.MIN_PRECISION_SERIE1.IsVisibleInLegend = False
        Me.MIN_PRECISION_SERIE1.ChartType = SeriesChartType.StepLine
        Me.MIN_PRECISION_SERIE1.Color = Drawing.Color.LightGreen
        Me.MIN_PRECISION_SERIE1.BorderWidth = 2


        Me.MAX_PRECISION_SERIE2.IsVisibleInLegend = False
        Me.MAX_PRECISION_SERIE2.ChartType = SeriesChartType.StepLine
        Me.MAX_PRECISION_SERIE2.Color = Drawing.Color.IndianRed
        Me.MAX_PRECISION_SERIE2.BorderWidth = 2

        Me.MIN_PRECISION_SERIE2.IsVisibleInLegend = False
        Me.MIN_PRECISION_SERIE2.ChartType = SeriesChartType.StepLine
        Me.MIN_PRECISION_SERIE2.Color = Drawing.Color.IndianRed
        Me.MIN_PRECISION_SERIE2.BorderWidth = 2

        Me.SIZE = New Drawing.Size(MyBase.SIZE.Width, MyBase.SIZE.Height * 0.705)

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.ASPHALT_PERCENTAGE_VARIATION_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_PERC_VAR As Double
    Public Property MINIMUM_PERC_VAR As Double = 1000


    Private WriteOnly Property DATE_ As Date
        Set(value As Date)

            Me.X_MINIMUM = value.ToOADate
            Me.X_MAXIMUM = value.Add(TimeSpan.FromHours(24.01)).ToOADate

        End Set
    End Property


    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As DataFileNode)

        If (IsNothing(cycle.PREVIOUS_CYCLE)) Then

            Me.DATE_ = cycle.DATE_

        End If

        ' ??? 0.1 ??? Like why would this happen?
        If (Not Double.IsNaN(cycle.ASPHALT_SET_POINT_PERCENTAGE) And cycle.ASPHALT_PERCENTAGE > 0.1 And cycle.PRODUCTION_SPEED > 0) Then

            Dim cycleDateTime = cycle.TIME
            Dim cycleAsphaltPercVar = dataFileNode.getUnitByTag(cycle.ASPHALT_PERCENTAGE_VARIATION_TAG).convert(cycle.ASPHALT_PERCENTAGE_VARIATION, Me.UNIT)

            Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleAsphaltPercVar)

            With Graphic.pointFormatList_mix.getFormatFor(cycle.MIX_NAME, cycle.ASPHALT_NAME)
                Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
            End With

            ' To be removed when data accessible
            If (cycleAsphaltPercVar > Me.MAXIMUM_PERC_VAR) Then

                Me.MAXIMUM_PERC_VAR = cycleAsphaltPercVar

            ElseIf (cycleAsphaltPercVar < Me.MINIMUM_PERC_VAR) Then

                Me.MINIMUM_PERC_VAR = cycleAsphaltPercVar

            End If

        End If

    End Sub

    Private Sub buildLegend()

        Me.LEGEND.Enabled = True

        Dim setPointItem As New LegendItem

        setPointItem.Color = Me.SET_POINT_SERIE.Color
        setPointItem.BorderColor = Me.SET_POINT_SERIE.Color
        setPointItem.Name = "Visé"
        setPointItem.ImageStyle = Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line
        setPointItem.BorderWidth = 100

        Me.LEGEND.CustomItems.Add(setPointItem)

        Dim limite1Item As New LegendItem

        limite1Item.Color = Me.MIN_PRECISION_SERIE1.Color
        limite1Item.BorderColor = Me.MIN_PRECISION_SERIE1.Color
        limite1Item.Name = "Tolerance " & "(" & ChrW(177) & MixStatistics.TOLERENCE_PRECISION & ")"
        limite1Item.ImageStyle = Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line
        limite1Item.BorderWidth = 100

        Me.LEGEND.CustomItems.Add(limite1Item)

        Dim limite2Item As New LegendItem

        limite2Item.Color = Me.MIN_PRECISION_SERIE2.Color
        limite2Item.BorderColor = Me.MIN_PRECISION_SERIE2.Color
        limite2Item.Name = "Controle " & "(" & ChrW(177) & MixStatistics.CONTROL_PRECISION & ")"
        limite2Item.ImageStyle = Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line
        limite2Item.BorderWidth = 100

        Me.LEGEND.CustomItems.Add(limite2Item)

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            Me.DATE_ = ProductionDay.CURRENT_DATE

            Me.buildLegend()

            If (Me.MAXIMUM_PERC_VAR > Math.Abs(Me.MINIMUM_PERC_VAR)) Then
                Me.Y_MAXIMUM = (Me.MAXIMUM_PERC_VAR * 1000 \ Percent.UNIT.convert(0.5 * 1000, Me.UNIT) + 1) * Percent.UNIT.convert(0.5 * 1000, Me.UNIT) / 1000
                Me.Y_MINIMUM = 0 - Me.Y_MAXIMUM
            Else
                Me.Y_MINIMUM = (Me.MINIMUM_PERC_VAR * 1000 \ Percent.UNIT.convert(0.5 * 1000, Me.UNIT) - 1) * Percent.UNIT.convert(0.5 * 1000, Me.UNIT) / 1000
                Me.Y_MAXIMUM = 0 - Me.Y_MINIMUM
            End If

            Y_INTERVAL = (Me.Y_MAXIMUM - Me.Y_MINIMUM) / 5

            Me.SET_POINT_SERIE.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMinByValue("X").XValue, 0)
            Me.SET_POINT_SERIE.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMaxByValue("X").XValue, 0)

            Me.MAX_PRECISION_SERIE1.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMinByValue("X").XValue, MixStatistics.TOLERENCE_PRECISION)
            Me.MAX_PRECISION_SERIE1.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMaxByValue("X").XValue, MixStatistics.TOLERENCE_PRECISION)

            Me.MIN_PRECISION_SERIE1.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMinByValue("X").XValue, 0 - MixStatistics.TOLERENCE_PRECISION)
            Me.MIN_PRECISION_SERIE1.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMaxByValue("X").XValue, 0 - MixStatistics.TOLERENCE_PRECISION)

            Me.MAX_PRECISION_SERIE2.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMinByValue("X").XValue, MixStatistics.CONTROL_PRECISION)
            Me.MAX_PRECISION_SERIE2.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMaxByValue("X").XValue, MixStatistics.CONTROL_PRECISION)

            Me.MIN_PRECISION_SERIE2.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMinByValue("X").XValue, 0 - MixStatistics.CONTROL_PRECISION)
            Me.MIN_PRECISION_SERIE2.Points.AddXY(Me.MAIN_DATA_SERIE.Points.FindMaxByValue("X").XValue, 0 - MixStatistics.CONTROL_PRECISION)

            Me.chart.Series.Add(Me.SET_POINT_SERIE)
            Me.chart.Series.Add(Me.MAX_PRECISION_SERIE1)
            Me.chart.Series.Add(Me.MIN_PRECISION_SERIE1)
            Me.chart.Series.Add(Me.MAX_PRECISION_SERIE2)
            Me.chart.Series.Add(Me.MIN_PRECISION_SERIE2)

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableAsphaltPercentageVariation_FR.bmp"
        End Get
    End Property
End Class
