Public Class ImportController

    Private settings As XmlSettings.Settings

    Private lastIdentifiedFiles As List(Of DataFile)

    Private _usbDirectory As IO.DirectoryInfo
    Private temporaryArchivesDirectory As IO.DirectoryInfo
    Private dataDirectory As IO.DirectoryInfo
    Private eventDirectory As IO.DirectoryInfo

    Private updateArchivesImageThread As Threading.Thread

    ''' <summary>
    ''' The files that wont be added to the archive image in case they are not complete.
    ''' Only the newestFiles can be incomplete (e.g. Importation is done in the middle of a production day).
    ''' </summary>
    Private newestImportedFiles As List(Of IO.FileInfo)


    Public Sub New(settings As XmlSettings.Settings)

        Me.settings = settings

        Me.lastIdentifiedFiles = New List(Of DataFile)
        Me.newestImportedFiles = New List(Of IO.FileInfo)

    End Sub

    Public Function importFiles() As Integer

        Dim nbImportedFiles As Integer = 0

        ProgramController.DataFilesPersistence.initializeImportation()

        For Each file As DataFile In Me.lastIdentifiedFiles

            If (TypeOf file Is CSVFile) Then
                ProgramController.DataFilesPersistence.addCSVFile(file.getFileInfo)
                nbImportedFiles += 1
            ElseIf (TypeOf file Is LOGFile) Then
                ProgramController.DataFilesPersistence.addLOGFile(file.getFileInfo)
                nbImportedFiles += 1
            ElseIf (TypeOf file Is EventsFile) Then
                ProgramController.DataFilesPersistence.addEventsFile(file.getFileInfo)
                nbImportedFiles += 1
            ElseIf (TypeOf file Is MDBFile) Then
                ProgramController.DataFilesPersistence.addMDBFile(file.getFileInfo)
                nbImportedFiles += 1
            End If

        Next

        ProgramController.DataFilesPersistence.finalizeImportation()

        Me.updateArchivesImageThread = New Threading.Thread(New Threading.ThreadStart(AddressOf updateArchivesImage))
        Me.updateArchivesImageThread.Start()

        Return nbImportedFiles
    End Function

    Public Function identifyFilesToImport() As List(Of DataFile)

        Me.lastIdentifiedFiles.Clear()

        If (IsNothing(USBDirectory)) Then
            USBDirectory = New IO.DirectoryInfo(XmlSettings.Settings.instance.Usine.USB_DIRECTORY)
        End If

        If (XmlSettings.Settings.instance.Usine.DataFiles.CSV.ACTIVE) Then
            identifyCSVFiles()
        End If

        If (XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE) Then
            identifyLOGFiles()
        End If

        If (XmlSettings.Settings.instance.Usine.Events.ACTIVE) Then
            identifyEventsFiles()
        End If

        If (XmlSettings.Settings.instance.Usine.DataFiles.MDB.ACTIVE) Then
            identifyMDBFiles()
        End If

        Return Me.lastIdentifiedFiles
    End Function

    Private Sub identifyCSVFiles()

        If (dataDirectory.Exists) Then

            Dim newestCSVFile As CSVFile = Nothing

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.CSV.FILE_NAME_REGEX)

            For Each file As IO.FileInfo In dataDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    Dim csvFile As New CSVFile(file.FullName)

                    Me.lastIdentifiedFiles.Add(csvFile)

                    If (IsNothing(newestCSVFile)) Then
                        newestCSVFile = csvFile
                    ElseIf (newestCSVFile.Date_.CompareTo(csvFile.Date_) < 0) Then
                        newestCSVFile = csvFile
                    End If

                End If
            Next

            Me.newestImportedFiles.Add(newestCSVFile.getFileInfo)
        End If
    End Sub

    Private Sub identifyLOGFiles()

        Dim fileList As New List(Of LOGFile)

        If (dataDirectory.Exists) Then

            Dim newestLOGFile As LOGFile = Nothing

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.LOG.FILE_NAME_REGEX)

            For Each file As IO.FileInfo In dataDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    Dim logFile As New LOGFile(file.FullName)

                    Me.lastIdentifiedFiles.Add(logFile)

                    If (IsNothing(newestLOGFile)) Then
                        newestLOGFile = logFile
                    ElseIf (newestLOGFile.Date_.CompareTo(logFile.Date_) < 0) Then
                        newestLOGFile = logFile
                    End If

                End If

            Next

            Me.newestImportedFiles.Add(newestLOGFile.getFileInfo)

        End If

    End Sub

    Private Sub identifyEventsFiles()

        If (eventDirectory.Exists) Then

            Dim newestEventsFile As EventsFile = Nothing

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.Events.FILE_NAME_REGEX)

            For Each file As IO.FileInfo In eventDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    Dim eventsFile As New EventsFile(file.FullName)

                    Me.lastIdentifiedFiles.Add(eventsFile)

                    If (IsNothing(newestEventsFile)) Then
                        newestEventsFile = eventsFile
                    ElseIf (newestEventsFile.Date_.CompareTo(eventsFile.Date_) < 0) Then
                        newestEventsFile = eventsFile
                    End If

                End If

            Next

            Me.newestImportedFiles.Add(newestEventsFile.getFileInfo)

        End If

    End Sub

    Public Sub identifyMDBFiles()

        If (temporaryArchivesDirectory.Exists) Then

            Dim mdbFileNameRegex As New System.Text.RegularExpressions.Regex(Constants.Input.MDB.FILE_NAME_REGEX)

            For Each file In temporaryArchivesDirectory.GetFiles

                If (mdbFileNameRegex.Match(file.Name).Success) Then

                    Dim mdbFile = New MDBFile(file.FullName)
                    mdbFile.setDate(mdbFile.getLastCycleDate)
                    Me.lastIdentifiedFiles.Add(mdbFile)

                End If
            Next
        End If
    End Sub

    ' In different thread
    Private Sub updateArchivesImage()

        Dim archivesImageFile As New IO.FileInfo(USBDirectory.FullName & "\Ressources\ArchivesImage")

        If (archivesImageFile.Exists) Then

            Dim writer As New IO.StreamWriter(archivesImageFile.FullName)

            Try

                ' csv
                For Each file As IO.FileInfo In New IO.DirectoryInfo(Constants.Paths.CSV_ARCHIVES_DIRECTORY).GetFiles

                    If (Not newestImportedFiles.Contains(file)) Then
                        writer.WriteLine(file.Name)
                    End If

                Next

                ' log
                For Each file As IO.FileInfo In New IO.DirectoryInfo(Constants.Paths.LOG_ARCHIVES_DIRECTORY).GetFiles

                    If (Not newestImportedFiles.Contains(file)) Then
                        writer.WriteLine(file.Name)
                    End If

                Next

                ' events
                For Each file As IO.FileInfo In New IO.DirectoryInfo(Constants.Paths.EVENTS_ARCHIVES_DIRECTORY).GetFiles

                    If (Not newestImportedFiles.Contains(file)) Then
                        writer.WriteLine(file.Name)
                    End If

                Next

                writer.Close()

            Catch ex As Threading.ThreadAbortException
                writer.Close()
            End Try

        End If

    End Sub

    Public Function isValidUSBDirectory(directory As IO.DirectoryInfo) As Boolean

        ' #refactor
        ' Check for things that you actually use... Like archiveimage, tempArchives, Data and Events

        If (directory.Exists) Then

            Dim usbHasRessourcesDIR As Boolean = False
            Dim usbHasCheminTXT As Boolean = False
            Dim usbHasImporterEXE As Boolean = False

            For Each innerDir In directory.GetDirectories

                If (innerDir.Name = "Ressources") Then
                    usbHasRessourcesDIR = True
                End If

            Next

            For Each file In directory.GetFiles

                If (file.Name = "Chemin.txt") Then
                    usbHasCheminTXT = True
                End If

                If (file.Name = "Master Usine - Import.exe") Then
                    usbHasImporterEXE = True
                End If

            Next

            Return usbHasCheminTXT AndAlso usbHasImporterEXE AndAlso usbHasRessourcesDIR

        Else
            Return False
        End If

    End Function

    Public Sub clear()
        Me.lastIdentifiedFiles.Clear()
    End Sub

    Public Property USBDirectory As IO.DirectoryInfo
        Get

            ' If usbDirectory was not set or is invalid
            If (IsNothing(_usbDirectory) OrElse Not isValidUSBDirectory(_usbDirectory)) Then

                ' Get from settings file
                Dim savedPath As String = Me.settings.Usine.USB_DIRECTORY

                ' If nothing in settings file
                If (savedPath.Equals("")) Then

                    ' Auto detect
                    For Each drive As IO.DriveInfo In IO.DriveInfo.GetDrives

                        If (ProgramController.ImportController.isValidUSBDirectory(New IO.DirectoryInfo(drive.Name))) Then

                            Me.settings.Usine.USB_DIRECTORY = drive.Name
                            setUSBDirectory(New IO.DirectoryInfo(drive.Name))
                            Return _usbDirectory
                        End If

                    Next

                    Return New IO.DirectoryInfo("C:\")

                Else ' There is a directory in settings file

                    Dim savedDirectory As IO.DirectoryInfo = New IO.DirectoryInfo(Me.settings.Usine.USB_DIRECTORY)

                    ' If it's invalid
                    If (Not isValidUSBDirectory(savedDirectory)) Then

                        ' Auto detect
                        For Each drive As IO.DriveInfo In IO.DriveInfo.GetDrives

                            If (ProgramController.ImportController.isValidUSBDirectory(New IO.DirectoryInfo(drive.Name))) Then
                                Me.settings.Usine.USB_DIRECTORY = drive.Name
                                setUSBDirectory(New IO.DirectoryInfo(drive.Name))
                                Return _usbDirectory
                            End If

                        Next

                    End If

                    Return savedDirectory

                End If

            End If

            Return _usbDirectory
        End Get
        Set(value As IO.DirectoryInfo)

            setUSBDirectory(value)

        End Set
    End Property

    ' Had to do that because I couldn't set in the getter... weird behavior of vb
    Private Sub setUSBDirectory(value As IO.DirectoryInfo)

        If (Not Me.settings.Usine.USB_DIRECTORY.Equals(value.FullName)) Then

            Me.settings.Usine.USB_DIRECTORY = value.FullName
            Me.settings.save()
        End If

        If (Not value.Equals(Me._usbDirectory)) Then

            _usbDirectory = value

            ' #refactor
            temporaryArchivesDirectory = New IO.DirectoryInfo(_usbDirectory.FullName & "\Ressources\Temporary archives")
            dataDirectory = New IO.DirectoryInfo(temporaryArchivesDirectory.FullName & "\Data")
            eventDirectory = New IO.DirectoryInfo(temporaryArchivesDirectory.FullName & "\Events")
        End If
    End Sub

    Public ReadOnly Property NB_FILES_TO_IMPORT As Integer
        Get
            Return If(IsNothing(Me.lastIdentifiedFiles), 0, Me.lastIdentifiedFiles.Count)
        End Get
    End Property

End Class
