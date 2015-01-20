''' Guillaume Beaudry
''' 2015-01-19
''' 

Public MustInherit Class DataPlant

    'Variables de classes
    Private fileInfo As IO.FileInfo

    'Moment auxquel les informations d'une usine ont été collecté par le système de l'usine
    Private _productionDate As Date

    'Propriétés
    Public Property productionDate As Date
        Get
            Return Me._productionDate
        End Get
        Set(value As Date)
            _productionDate = value
        End Set
    End Property

    'Événements
    Event AnalysisStartedEvent(dataPlant As DataPlant)
    Event AnalysisProgress(current As Integer, total As Integer)
    Event AnalysisStopedEvent(dataPlant As DataPlant)

    Public Sub New(filePath As String)
        Me.fileInfo = New IO.FileInfo(filePath)
    End Sub

    Public Function getFileInfo() As IO.FileInfo
        Return Me.fileInfo
    End Function

    Protected Function analysisStarted(dataPlant As DataPlant)
        RaiseEvent AnalysisStartedEvent(dataPlant)
    End Function

    Protected Function analysisStoped(dataPlant As DataPlant)
        RaiseEvent AnalysisStopedEvent(dataPlant)
    End Function

    Protected Function analysisRunning(current As Integer, total As Integer)
        RaiseEvent AnalysisProgress(current, total)
    End Function

    MustOverride Function getCycles(startTime As Date, endTime As Date) As List(Of Cycle)
    MustOverride Function getDate()

End Class
