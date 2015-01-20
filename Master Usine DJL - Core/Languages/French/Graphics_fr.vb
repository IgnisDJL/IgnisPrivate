Public Class Graphics_fr
    Implements Graphics

    Public ReadOnly Property AccumulatedMass_Title As String Implements Graphics.AccumulatedMass_Title
        Get
            Return "Tonnage Cumulé"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentage_Title As String Implements Graphics.AsphaltPercentage_Title
        Get
            Return "Pourcentage de bitume"
        End Get
    End Property

    Public ReadOnly Property AsphaltPercentageVariation_Title As String Implements Graphics.AsphaltPercentageVariation_Title
        Get
            Return "Variation"
        End Get
    End Property

    Public ReadOnly Property FuelConsumption_Title As String Implements Graphics.FuelConsumption_Title
        Get
            Return "Consommation"
        End Get
    End Property

    Public ReadOnly Property MixTemperature_Title As String Implements Graphics.MixTemperature_Title
        Get
            Return "Température de l'enrobé"
        End Get
    End Property

    Public ReadOnly Property MixTemperatureVariation_Title As String Implements Graphics.MixTemperatureVariation_Title
        Get
            Return "Variation"
        End Get
    End Property

    Public ReadOnly Property ProductionSpeed_Title As String Implements Graphics.ProductionSpeed_Title
        Get
            Return "Production"
        End Get
    End Property

    Public ReadOnly Property RecycledPercentage_Title As String Implements Graphics.RecycledPercentage_Title
        Get
            Return "Pourcentage de recyclé"
        End Get
    End Property

End Class
