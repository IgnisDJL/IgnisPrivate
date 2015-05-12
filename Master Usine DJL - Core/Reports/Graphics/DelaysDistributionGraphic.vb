Public Class DG02_DelaysDistributionGraphic
    Inherits BarChart

    Public Sub New(internDelaysWithBeakageDuration As TimeSpan, internDelaysWithoutBreakageDuration As TimeSpan, externDelaysDuration As TimeSpan, otherDelaysDuration As TimeSpan)
        MyBase.New()

        Me.X_TITLE = "Délais (h)"

        Me.Y_INTERVAL = 1

        ' Temporary solution
        Dim otherDelaysName As String = "Externe (autres)"
        Dim otherDelaysColor As Color = Color.LightBlue

        Dim externDelaysName As String = "Externe (chantier)"
        Dim externDelaysColor As Color = Color.Salmon

        Dim internDelaysWithoutBreakageName As String = "Interne (sans bris)"
        Dim internDelaysWithoutBreakageColor As Color = Color.Yellow

        Dim internDelaysWithBreakageName As String = "Interne (avec bris)"
        Dim internDelaysWithBreakageColor As Color = Color.CadetBlue

        For Each _delayType As DelayType In ProgramController.SettingsControllers.EventsSettingsController.DelayTypes

            If (_delayType.IsOther) Then

                otherDelaysName = _delayType.Name
                otherDelaysColor = _delayType.Color

            ElseIf (_delayType.IsExtern) Then

                externDelaysName = _delayType.Name
                externDelaysColor = _delayType.Color

            ElseIf (_delayType.IsIntern AndAlso Not _delayType.IsBreakage) Then

                internDelaysWithoutBreakageName = _delayType.Name
                internDelaysWithoutBreakageColor = _delayType.Color

            ElseIf (_delayType.IsIntern AndAlso _delayType.IsBreakage) Then

                internDelaysWithBreakageName = _delayType.Name
                internDelaysWithBreakageColor = _delayType.Color

            End If
        Next

        Me.MAIN_DATA_SERIE.Points.AddXY(otherDelaysName, otherDelaysDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = otherDelaysColor
        Me.MAIN_DATA_SERIE.Points.Last.Label = otherDelaysDuration.ToString("h\hmm")

        Me.MAIN_DATA_SERIE.Points.AddXY(externDelaysName, externDelaysDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = externDelaysColor
        Me.MAIN_DATA_SERIE.Points.Last.Label = externDelaysDuration.ToString("h\hmm")

        Me.MAIN_DATA_SERIE.Points.AddXY(internDelaysWithoutBreakageName, internDelaysWithoutBreakageDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = internDelaysWithoutBreakageColor
        Me.MAIN_DATA_SERIE.Points.Last.Label = internDelaysWithoutBreakageDuration.ToString("h\hmm")

        Me.MAIN_DATA_SERIE.Points.AddXY(internDelaysWithBreakageName, internDelaysWithBeakageDuration.TotalHours)
        Me.MAIN_DATA_SERIE.Points.Last.Color = internDelaysWithBreakageColor
        Me.MAIN_DATA_SERIE.Points.Last.Label = internDelaysWithBeakageDuration.ToString("h\hmm")

    End Sub

        Protected Overrides ReadOnly Property FILE_NAME As String
            Get
            Return Constants.Output.Graphics.SaveAsNames.DELAYS_DISTRIBUTION_GRAPHIC
            End Get
        End Property

        Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
            Get
                Return "UnavailableDelaysDistributionGraphic_FR.bmp"
            End Get
        End Property

End Class
