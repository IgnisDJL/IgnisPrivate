Imports IGNIS.Constants.Settings

Module PlantProduction
    Private plantType = New UsineType
    Private plantName As String
    Private _productionDayList = New List(Of ProductionDay_1)


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

    Public Sub setPlantName(usineName As String)
        plantName = usineName
    End Sub


    Public Sub setPlantType(usineType As UsineType)
        plantType = usineType
    End Sub

End Module
