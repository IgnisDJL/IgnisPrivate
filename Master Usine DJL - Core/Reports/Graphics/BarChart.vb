Imports System.Windows.Forms.DataVisualization.Charting

Public MustInherit Class BarChart
    Inherits ChartAdapter

    Protected Sub New()
        MyBase.New()

        Me.Y_LABEL_FORMAT = "#0\h"


        Me.MAIN_DATA_SERIE.ChartType = SeriesChartType.Bar


        Me.GRID_COLOR = Drawing.Color.FromArgb(200, 200, 200)
        Me.GRID_SIZE = 2

        Me.AXIS_SIZE = 3

        Me.SIZE = New Drawing.Size(400 * 3, 130 * 3) ' *3 = 300dpi / 96dpi (default)


        Me.FONT = New Drawing.Font("Arial", 25.0!, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MAIN_DATA_SERIE.Font = New Drawing.Font("Arial", 24.0!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.chartArea.AxisX.ScaleBreakStyle.Enabled = False

    End Sub



End Class
