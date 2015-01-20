Imports System.Windows.Forms.DataVisualization.Charting

Public Class MixTemperatureGraphic
    Inherits Graphic

    Public Property UNIT As Unit = XmlSettings.Settings.instance.Report.Word.TEMPERATURE_UNIT

    Private ABSOLUTE_LIMIT_SERIE As New Serie

    Public Sub New()
        MyBase.New()

        ' settings
        Me.Y_TITLE = "Température de l'enrobé (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

        ' settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.ABSOLUTE_LIMIT_SERIE.IsVisibleInLegend = False
        Me.ABSOLUTE_LIMIT_SERIE.ChartType = SeriesChartType.Line
        Me.ABSOLUTE_LIMIT_SERIE.Color = Drawing.Color.IndianRed
        Me.ABSOLUTE_LIMIT_SERIE.BorderWidth = 2

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_TEMP As Integer
    Public Property MINIMUM_TEMP As Integer = 10000

    Private WriteOnly Property DATE_ As Date
        Set(value As Date)

            Me.X_MINIMUM = value.ToOADate
            Me.X_MAXIMUM = value.Add(TimeSpan.FromHours(24.01)).ToOADate

        End Set
    End Property


    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As DataFileNode)

        Dim cycleDateTime = cycle.TIME
        Dim cycleMixTemp = dataFileNode.getUnitByTag(cycle.TEMPERATURE_TAG).convert(cycle.TEMPERATURE, Me.UNIT)

        If (cycle.PRODUCTION_SPEED > 0) Then

            Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleMixTemp)

            With Graphic.pointFormatList_asphalt.getFormatFor("", cycle.ASPHALT_NAME)
                Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
            End With

            ' To be removed when data accessible
            If (cycleMixTemp > Me.MAXIMUM_TEMP) Then

                Me.MAXIMUM_TEMP = cycleMixTemp

            End If

            If (cycleMixTemp < Me.MINIMUM_TEMP) Then

                Me.MINIMUM_TEMP = cycleMixTemp

            End If

        End If

    End Sub

    Private Sub buildLegend()

        Me.LEGEND.Enabled = True

        Dim pointsFormats = Graphic.pointFormatList_asphalt.getAllFormats

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

        Dim absoluteLimitItem As New LegendItem

        absoluteLimitItem.Color = Me.ABSOLUTE_LIMIT_SERIE.Color
        absoluteLimitItem.BorderColor = Me.ABSOLUTE_LIMIT_SERIE.Color
        absoluteLimitItem.Name = "Limite " & "(" & 170 & ")"
        absoluteLimitItem.ImageStyle = LegendImageStyle.Line
        absoluteLimitItem.BorderWidth = 100

        Me.LEGEND.CustomItems.Add(absoluteLimitItem)

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            Me.DATE_ = ProductionDay.CURRENT_DATE

            Me.buildLegend()

            Me.Y_MAXIMUM = ((Me.MAXIMUM_TEMP \ 20) + 1) * 20

            If (Me.MINIMUM_TEMP < 20) Then
                Me.Y_MINIMUM = 0
            Else
                Me.Y_MINIMUM = (Me.MINIMUM_TEMP \ 20) * 20
            End If

            If (Y_MAXIMUM - Y_MINIMUM < 100) Then
                Y_INTERVAL = 10
            Else
                Y_INTERVAL = 20
            End If

            Me.ABSOLUTE_LIMIT_SERIE.Points.AddXY(Me.X_MINIMUM, Celsius.UNIT.convert(170, Me.UNIT))
            Me.ABSOLUTE_LIMIT_SERIE.Points.AddXY(Me.X_MAXIMUM, Celsius.UNIT.convert(170, Me.UNIT))

            Me.chart.Series.Add(Me.ABSOLUTE_LIMIT_SERIE)

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableMixTemp_FR.bmp"
        End Get
    End Property
End Class
