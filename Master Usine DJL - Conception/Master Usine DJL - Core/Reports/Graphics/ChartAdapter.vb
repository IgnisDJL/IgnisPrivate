Imports System.Windows.Forms.DataVisualization.Charting

Public MustInherit Class ChartAdapter

    Protected Sub New()

        Me.chart.ChartAreas.Clear()
        Me.chart.Series.Clear()
        Me.chart.Legends.Clear()

    End Sub

    Public chart As New Chart

    Public chartArea As New ChartArea("default")

    Private mainDataSerie As New Serie
    Protected ReadOnly Property MAIN_DATA_SERIE As Serie
        Get
            Return Me.mainDataSerie
        End Get
    End Property

    Protected MustOverride ReadOnly Property FILE_NAME As String

    Public Property X_INTERVAL As Double
        Get
            Return Me.chartArea.AxisX.Interval
        End Get
        Set(value As Double)

            Me.chartArea.AxisX.Interval = value
            Me.chartArea.AxisX.LabelStyle.Interval = value
            Me.chartArea.AxisX.MajorGrid.Interval = value
            Me.chartArea.AxisX.MinorGrid.Interval = value
            Me.chartArea.AxisX.MinorGrid.IntervalOffset = value / 2

        End Set
    End Property

    Public Property Y_INTERVAL As Double
        Get
            Return Me.chartArea.AxisY.Interval
        End Get
        Set(value As Double)

            Me.chartArea.AxisY.Interval = value
            Me.chartArea.AxisY.LabelStyle.Interval = value
            Me.chartArea.AxisY.MajorGrid.Interval = value
            Me.chartArea.AxisY.MinorGrid.Interval = value
            Me.chartArea.AxisY.MinorGrid.IntervalOffset = value / 2

        End Set
    End Property

    Public Property X_TITLE As String
        Get
            Return Me.chartArea.AxisX.Title
        End Get
        Set(value As String)
            Me.chartArea.AxisX.Title = value
        End Set
    End Property


    Public Property Y_TITLE As String
        Get
            Return Me.chartArea.AxisY.Title
        End Get
        Set(value As String)
            Me.chartArea.AxisY.Title = value
        End Set
    End Property

    Public Property X_LABEL_FORMAT As String
        Get
            Return Me.chartArea.AxisX.LabelStyle.Format
        End Get
        Set(value As String)
            Me.chartArea.AxisX.LabelStyle.Format = value
        End Set
    End Property

    Public Property Y_LABEL_FORMAT As String
        Get
            Return Me.chartArea.AxisY.LabelStyle.Format
        End Get
        Set(value As String)
            Me.chartArea.AxisY.LabelStyle.Format = value
        End Set
    End Property

    Protected Property X_MAXIMUM As Double
        Get
            Return Me.chartArea.AxisX.Maximum
        End Get
        Set(value As Double)
            Me.chartArea.AxisX.Maximum = value
        End Set
    End Property

    Protected Property X_MINIMUM As Double
        Get
            Return Me.chartArea.AxisX.Minimum
        End Get
        Set(value As Double)
            Me.chartArea.AxisX.Minimum = value
        End Set
    End Property

    Protected Property Y_MAXIMUM As Double
        Get
            Return Me.chartArea.AxisY.Maximum
        End Get
        Set(value As Double)
            Me.chartArea.AxisY.Maximum = value
        End Set
    End Property

    Protected Property Y_MINIMUM As Double
        Get
            Return Me.chartArea.AxisY.Minimum
        End Get
        Set(value As Double)
            Me.chartArea.AxisY.Minimum = value
        End Set
    End Property

    Protected WriteOnly Property FONT As System.Drawing.Font
        Set(value As System.Drawing.Font)
            Me.chartArea.AxisX.LabelAutoFitMaxFontSize = value.Size
            Me.chartArea.AxisX.LabelAutoFitMinFontSize = value.Size
            Me.chartArea.AxisX.TitleFont = value
            Me.chartArea.AxisX.LabelStyle.Font = value
            Me.chartArea.AxisY.LabelAutoFitMaxFontSize = value.Size
            Me.chartArea.AxisY.LabelAutoFitMinFontSize = value.Size
            Me.chartArea.AxisY.TitleFont = value
            Me.chartArea.AxisY.LabelStyle.Font = value
            Me.LEGEND.Font = value
        End Set
    End Property

    Protected WriteOnly Property GRID_COLOR As System.Drawing.Color
        Set(value As System.Drawing.Color)
            Me.chartArea.AxisX.MajorGrid.LineColor = value
            Me.chartArea.AxisX.MinorGrid.LineColor = value
            Me.chartArea.AxisY.MajorGrid.LineColor = value
            Me.chartArea.AxisY.MinorGrid.LineColor = value
        End Set
    End Property

    Protected WriteOnly Property GRID_SIZE As Integer
        Set(value As Integer)
            Me.chartArea.AxisX.MajorGrid.LineWidth = value
            Me.chartArea.AxisX.MinorGrid.LineWidth = value
            Me.chartArea.AxisY.MajorGrid.LineWidth = value
            Me.chartArea.AxisY.MinorGrid.LineWidth = value
        End Set
    End Property

    Protected WriteOnly Property AXIS_SIZE As Integer
        Set(value As Integer)
            Me.chartArea.AxisX.LineWidth = value
            Me.chartArea.AxisX.MajorTickMark.LineWidth = value
            Me.chartArea.AxisY.LineWidth = value
            Me.chartArea.AxisY.MajorTickMark.LineWidth = value
        End Set
    End Property

    Protected Sub enableMinorGrids()
        Me.chartArea.AxisX.MinorGrid.Enabled = True
        Me.chartArea.AxisY.MinorGrid.Enabled = True
    End Sub

    Protected WriteOnly Property X_LABEL_ORENTATION As Integer
        Set(value As Integer)

            Select Case value

                Case 0
                    Me.chartArea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None

                Case 1
                    Me.chartArea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30

                Case 2
                    Me.chartArea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep45

                Case 3
                    Me.chartArea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90

            End Select

        End Set
    End Property

    Protected WriteOnly Property Y_LABEL_ORENTATION As Integer
        Set(value As Integer)

            Select Case value

                Case 0
                    Me.chartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None

                Case 1
                    Me.chartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep30

                Case 2
                    Me.chartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep45

                Case 3
                    Me.chartArea.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.LabelsAngleStep90

            End Select

        End Set
    End Property

    Protected WriteOnly Property X_TITLE_ORENTATION As Integer
        Set(value As Integer)

            Select Case value

                Case 1
                    Me.chartArea.AxisX.TextOrientation = TextOrientation.Horizontal

                Case 2
                    Me.chartArea.AxisX.TextOrientation = TextOrientation.Rotated90

                Case 3
                    Me.chartArea.AxisX.TextOrientation = TextOrientation.Rotated270

                Case 4
                    Me.chartArea.AxisX.TextOrientation = TextOrientation.Stacked

            End Select

        End Set
    End Property

    Protected WriteOnly Property Y_TITLE_ORENTATION As Integer
        Set(value As Integer)

            Select Case value

                Case 1
                    Me.chartArea.AxisY.TextOrientation = TextOrientation.Horizontal

                Case 2
                    Me.chartArea.AxisY.TextOrientation = TextOrientation.Rotated90

                Case 3
                    Me.chartArea.AxisY.TextOrientation = TextOrientation.Rotated270

                Case 4
                    Me.chartArea.AxisY.TextOrientation = TextOrientation.Stacked

            End Select

        End Set
    End Property

    Protected WriteOnly Property MINOR_GRID_STYLE As Integer
        Set(value As Integer)

            Select Case value

                Case 1
                    chartArea.AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Solid
                    chartArea.AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Solid

                Case 2
                    chartArea.AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dash
                    chartArea.AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash

                Case 3
                    chartArea.AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot
                    chartArea.AxisY.MinorGrid.LineDashStyle = ChartDashStyle.DashDot

                Case 4
                    chartArea.AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDotDot
                    chartArea.AxisY.MinorGrid.LineDashStyle = ChartDashStyle.DashDotDot

            End Select

        End Set
    End Property

    Protected Property SIZE As System.Drawing.Size
        Get
            Return Me.chart.Size
        End Get
        Set(value As System.Drawing.Size)
            Me.chart.Size = value
        End Set
    End Property


    Private _legend As New Legend
    Public ReadOnly Property LEGEND As Legend
        Get
            Return Me._legend
        End Get
    End Property

    Protected MustOverride ReadOnly Property NO_DATA_IMAGE_NAME As String

    Protected Overridable Sub consolidate()

        If (Me.MAIN_DATA_SERIE.Points.Count > 0) Then

            For Each serie In Me.chart.Series
                serie.XValueType = ChartValueType.Date
            Next

            Me.chart.Series.Add(MAIN_DATA_SERIE)

        Else

            Me.chartArea.BackImage = Constants.Paths.IMAGES_DIRECTORY & NO_DATA_IMAGE_NAME
            Me.chartArea.BackImageWrapMode = ChartImageWrapMode.Scaled
            Me.chartArea.BackImageAlignment = ChartImageAlignmentStyle.Center

        End If


        Me.chart.ChartAreas.Add(Me.chartArea)
        Me.chart.Legends.Add(Me._legend)

    End Sub

    Public Sub save()

        Me.consolidate()

        Dim bmp As New Drawing.Bitmap(Me.SIZE.Width, Me.SIZE.Height)
        bmp.SetResolution(300, 300)
        Me.chart.DrawToBitmap(bmp, New Drawing.Rectangle(0, 0, Me.SIZE.Width, Me.SIZE.Height))
        bmp.Save(Constants.Paths.OUTPUT_DIRECTORY & FILE_NAME)

    End Sub

    Protected Class Serie
        Inherits Series

        Public Sub New()
            MyBase.New()

            Me.IsVisibleInLegend = False

        End Sub

    End Class

    Public Class LegendItem
        Inherits System.Windows.Forms.DataVisualization.Charting.LegendItem
    End Class

End Class