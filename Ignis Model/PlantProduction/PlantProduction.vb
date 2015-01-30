Public Class PlantProduction
    Private plantType As String
    Private plantName As String
    Private _productionDayList As List(Of ProductionDay_1)

    Sub New(plantName As String, plantType As String)
        Me.plantName = plantName
        Me.plantType = plantType
        _productionDayList = New List(Of ProductionDay_1)
    End Sub

    Public Property productionDayList As List(Of ProductionDay_1)
        Set(value As List(Of ProductionDay_1))
            _productionDayList = value
        End Set
        Get
            Return _productionDayList
        End Get
    End Property

    Public ReadOnly Property getPlantType As String
        Get
            Return plantType
        End Get
    End Property

    Public ReadOnly Property getPlantName As String
        Get
            Return plantName
        End Get
    End Property

End Class
