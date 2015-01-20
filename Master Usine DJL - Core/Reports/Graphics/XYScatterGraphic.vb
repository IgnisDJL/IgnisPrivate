
Public MustInherit Class XYScatterGraphic
    Inherits ChartAdapter

    Public Shared pointFormatList_asphalt As PointFormatList
    Public Shared pointFormatList_mix As PointFormatList

    Protected Sub New()
        MyBase.New()

        Me.enableMinorGrids()
        Me.MINOR_GRID_STYLE = 2
        Me.GRID_COLOR = Drawing.Color.FromArgb(200, 200, 200)
        Me.GRID_SIZE = 2

        Me.AXIS_SIZE = 3

        Me.FONT = New Drawing.Font("Arial", 25.0!, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.MAIN_DATA_SERIE.ChartType = Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Me.MAIN_DATA_SERIE.MarkerSize = 10
        Me.MAIN_DATA_SERIE.MarkerStyle = Windows.Forms.DataVisualization.Charting.MarkerStyle.Square

        Me.SIZE = New Drawing.Size(400 * 3, 200 * 3) ' *3 = 300dpi / 96dpi (default)

        Me.LEGEND.Enabled = False
        Me.LEGEND.IsDockedInsideChartArea = True
        Me.LEGEND.DockedToChartArea = Me.chartArea.Name
        Me.LEGEND.BackColor = Drawing.Color.FromArgb(1, 255, 255, 255)
        Me.LEGEND.Docking = Windows.Forms.DataVisualization.Charting.Docking.Left
        Me.LEGEND.LegendStyle = Windows.Forms.DataVisualization.Charting.LegendStyle.Column
        Me.LEGEND.Font = New Drawing.Font("Arial", 20.0!, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))


    End Sub

    Public MustOverride Sub addCycle(cycle As Cycle, dataFileNode As XmlSettings.DataFileNode)


End Class
