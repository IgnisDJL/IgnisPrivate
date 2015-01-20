
Public Class AsphaltPercentageGraphic
    Inherits XYScatterGraphic

    Public Property UNIT As Unit = XmlSettings.Settings.instance.Reports.PERCENT_UNIT

    Private lastPointColor As Drawing.Color

    Public Sub New()
        MyBase.New()

        Me.Y_TITLE = "Pourcentage de bitume (" & Me.UNIT.ToString & ")"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

        ' settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.Y_LABEL_FORMAT = "0.00"

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.ASPHALT_PERCENTAGE_GRAPHIC
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
        Dim cycleAsphaltPercentage = dataFileNode.getUnitByTag(cycle.ASPHALT_PERCENTAGE_TAG).convert(cycle.ASPHALT_PERCENTAGE, Me.UNIT)


        If (cycleAsphaltPercentage > Percent.UNIT.convert(2, Me.UNIT) Or cycle.PRODUCTION_SPEED > 0) Then

            Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleAsphaltPercentage)

            With XYScatterGraphic.pointFormatList_mix.getFormatFor(cycle.MIX_NAME, cycle.ASPHALT_NAME)
                Me.MAIN_DATA_SERIE.Points.Last.Color = .COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = .MARKER
            End With

            ' To be removed when data accessible
            If (cycleAsphaltPercentage > Me.MAXIMUM_PERC) Then

                Me.MAXIMUM_PERC = cycleAsphaltPercentage

            ElseIf (cycleAsphaltPercentage < Me.MINIMUM_PERC) Then

                Me.MINIMUM_PERC = cycleAsphaltPercentage

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
    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            ' Me.DATE_ = ProductionDay.CURRENT_DATE

            Me.Y_MAXIMUM = (Me.MAXIMUM_PERC * 1000 \ Percent.UNIT.convert(0.5 * 1000, Me.UNIT) + 1) * Percent.UNIT.convert(0.5 * 1000, Me.UNIT) / 1000

            If (Me.MINIMUM_PERC < Percent.UNIT.convert(0.5, Me.UNIT)) Then
                Me.Y_MINIMUM = 0
            Else
                Me.Y_MINIMUM = (Me.MINIMUM_PERC * 1000 \ Percent.UNIT.convert(0.5 * 1000, Me.UNIT)) * Percent.UNIT.convert(0.5 * 1000, Me.UNIT) / 1000
            End If

            If (Me.Y_MAXIMUM - Me.Y_MINIMUM < Percent.UNIT.convert(2.5, Me.UNIT)) Then
                Me.Y_INTERVAL = Percent.UNIT.convert(0.25, Me.UNIT)
            Else
                Me.Y_INTERVAL = Percent.UNIT.convert(0.5, Me.UNIT)
            End If

            Me.buildLegend()

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableAsphaltPercentage_FR.bmp"
        End Get
    End Property
End Class