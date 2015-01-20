Public Class FuelConsumptionGraphic
    Inherits XYScatterGraphic

    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As XmlSettings.DataFileNode)

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.FUEL_CONSUMPTION_GRAPHIC
        End Get
    End Property

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableFuelConsumption_FR.bmp"
        End Get
    End Property
End Class
