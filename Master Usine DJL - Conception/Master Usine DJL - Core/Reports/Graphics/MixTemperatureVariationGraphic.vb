Imports System.Windows.Forms.DataVisualization.Charting

Public Class MixTemperatureVariationGraphic
    Inherits Graphic

    ' #settings
    Public Property UNIT As Unit = XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT

    Private SET_POINT_SERIE As New Serie
    Private MAX_PRECISION_SERIE As New Serie
    Private MIN_PRECISION_SERIE As New Serie

    Public Sub New()
        MyBase.New()

        Me.Y_TITLE = "Variation (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

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

        Me.SIZE = New Drawing.Size(MyBase.SIZE.Width, MyBase.SIZE.Height * 0.705)

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_TEMP_VAR As Double
    Public Property MINIMUM_TEMP_VAR As Double = Celsius.UNIT.convert(1000, Me.UNIT)


    Private WriteOnly Property DATE_ As Date
        Set(value As Date)

            Me.X_MINIMUM = value.ToOADate
            Me.X_MAXIMUM = value.Add(TimeSpan.FromHours(24.01)).ToOADate

        End Set
    End Property


    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As DataFileNode)

        If (cycle.SET_POINT_TEMPERATURE > 0) Then

            Dim cycleDateTime = cycle.TIME
            Dim cycleMixTempVar = DirectCast(dataFileNode.getUnitByTag(cycle.TEMPERATURE_VARIATION_TAG), TemperatureUnit).unitConvert(cycle.TEMPERATURE_VARIATION, Me.UNIT)

            Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleMixTempVar)

            With Graphic.pointFormatList_asphalt.getFormatFor("", cycle.ASPHALT_NAME)
                Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
            End With

            ' To be removed when data accessible
            If (cycleMixTempVar > Me.MAXIMUM_TEMP_VAR) Then

                Me.MAXIMUM_TEMP_VAR = cycleMixTempVar

            ElseIf (cycleMixTempVar < Me.MINIMUM_TEMP_VAR) Then

                Me.MINIMUM_TEMP_VAR = cycleMixTempVar

            End If

        End If

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then


            Me.DATE_ = ProductionDay.CURRENT_DATE

            If (Me.MAXIMUM_TEMP_VAR > Math.Abs(Me.MINIMUM_TEMP_VAR)) Then
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

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableMixTempVariation_FR.bmp"
        End Get
    End Property
End Class
