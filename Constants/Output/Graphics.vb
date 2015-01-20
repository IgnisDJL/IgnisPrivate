Namespace Constants.Output

    Public Class Graphics

        Public Shared Function intervalFromDays(nbOfDays As Double) As Double
            Return nbOfDays
        End Function

        Public Shared Function intervalFromHours(nbOfHours As Double) As Double
            Return nbOfHours / 24
        End Function

        Public Shared Function intervalFromSeconds(nbOfSeconds As Double) As Double
            Return nbOfSeconds / 24 / 3600
        End Function

        Public Class SaveAsNames

            Public Const ACCUMULATED_MASS_GRAPHIC = "Accumulated Mass Graphic.bmp"
            Public Const ASPHALT_PERCENTAGE_GRAPHIC = "Asphalt Percentage Graphic.bmp"
            Public Const ASPHALT_PERCENTAGE_VARIATION_GRAPHIC = "Asphalt Percentage Variation Graphic.bmp"
            Public Const MIX_TEMPERATURE_GRAPHIC = "Mix Temperature Graphic.bmp"
            Public Const MIX_TEMPERATURE_VARIATION_GRAPHIC = "Mix Temperature Variation Graphic.bmp"
            Public Const PRODUCTION_SPEED_GRAPHIC = "Production Speed Graphic.bmp"
            Public Const RECYCLED_PERCENTAGE_GRAPHIC = "Pourcentage de recyclé.bmp"
            Public Const FUEL_CONSUMPTION_GRAPHIC = "Consommation de carburant.bmp"
            Public Const PRODUCTION_DISTRIBUTION_GRAPHIC As String = "Distribution du temps de production.bmp"
            Public Const DELAYS_DISTRIBUTION_GRAPHIC As String = "Distribution des délais.bmp"

        End Class

    End Class

End Namespace
