Imports IGNIS.Constants.Settings

Public Class PlantProduction
    Private plantType As UsineType
    Private plantName As String
    Private _productionDayList As List(Of ProductionDay_1)

    Sub New(plantName As String, plantType As UsineType)
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

    Public Function getProductionDay(dateDebut As Date, dateFin As Date) As List(Of ProductionDay_1)
        Dim productionDayToReturn = New List(Of ProductionDay_1)

        For Each productionDay As ProductionDay_1 In _productionDayList
            If productionDay.getProductionDate >= New Date(dateDebut.Year, dateDebut.Month, dateDebut.Day) And productionDay.getProductionDate <= New Date(dateFin.Year, dateFin.Month, dateFin.Day) Then
                productionDayToReturn.Add(productionDay)
            End If
        Next

        Return productionDayToReturn
    End Function

    Public ReadOnly Property getPlantType As UsineType
        Get
            Return plantType
        End Get
    End Property

    Public ReadOnly Property getPlantName As String
        Get
            Return plantName
        End Get
    End Property

    Public Sub setPlantType(usineType As UsineType)
        Me.plantType = UsineType
    End Sub

End Class
