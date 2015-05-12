Public Class DG01_ProductionDistributionGraphic
    Inherits BarChart

    Public Sub New(productionDuration As TimeSpan, pausesDuration As TimeSpan, maintenanceDuration As TimeSpan, delaysDuration As TimeSpan)
        MyBase.New()

        Me.X_TITLE = "Horaire (h)"

        Me.Y_INTERVAL = 2

        Me.MAIN_DATA_SERIE.Points.AddXY("Délais (tous types)", delaysDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = Color.Orange
        Me.MAIN_DATA_SERIE.Points.Last.Label = delaysDuration.ToString("h\hmm")

        Me.MAIN_DATA_SERIE.Points.AddXY("Entretien(s)", maintenanceDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = Color.CadetBlue
        Me.MAIN_DATA_SERIE.Points.Last.Label = maintenanceDuration.ToString("h\hmm")

        Me.MAIN_DATA_SERIE.Points.AddXY("Pause(s)", pausesDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = Color.Yellow
        Me.MAIN_DATA_SERIE.Points.Last.Label = pausesDuration.ToString("h\hmm")

        Me.MAIN_DATA_SERIE.Points.AddXY("Production", productionDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = Color.ForestGreen
        Me.MAIN_DATA_SERIE.Points.Last.Label = productionDuration.ToString("h\hmm")

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.PRODUCTION_DISTRIBUTION_GRAPHIC
        End Get
    End Property

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "UnavailableProductionDistributionGraphic_FR.bmp"
        End Get
    End Property
End Class
