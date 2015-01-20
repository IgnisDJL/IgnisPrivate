Public Class DataFilesInformation

    Private _csvFile As CSVFile
    Private _logFile As LOGFile
    Private _mdbFile As MDBFile
    Private _eventsFile As EventsFile

    Public Sub New(csvFile As CSVFile, _
                   logFile As LOGFile, _
                   mdbFile As MDBFile, _
                   eventsFile As EventsFile)

        Me._csvFile = csvFile
        Me._logFile = logFile
        Me._mdbFile = mdbFile
        Me._eventsFile = eventsFile

    End Sub

    Public ReadOnly Property CSVFile As CSVFile
        Get
            Return _csvFile
        End Get
    End Property

    Public ReadOnly Property LOGFile As LOGFile
        Get
            Return _logFile
        End Get
    End Property

    Public ReadOnly Property MDBFile As MDBFile
        Get
            Return _mdbFile
        End Get
    End Property

    Public ReadOnly Property EventsFile As EventsFile
        Get
            Return _eventsFile
        End Get
    End Property

    Public ReadOnly Property HasCSVFile As Boolean
        Get
            Return Not IsNothing(Me._csvFile)
        End Get
    End Property

    Public ReadOnly Property HasLOGFile As Boolean
        Get
            Return Not IsNothing(Me._logFile)
        End Get
    End Property

    Public ReadOnly Property HasMDBFile As Boolean
        Get
            ' #Todo
            Return Not IsNothing(Me._mdbFile) AndAlso Me.MDBFile.hasCycles()
        End Get
    End Property

    Public ReadOnly Property HasEventsFile As Boolean
        Get
            Return Not IsNothing(Me._eventsFile)
        End Get
    End Property

End Class
