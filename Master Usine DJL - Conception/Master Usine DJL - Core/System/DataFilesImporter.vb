' #refactor
Public Class DataFilesImporter

    Private Shared directoryExplorer As New FolderBrowserDialog

    Private Shared newestImportedFiles(2) As String

    Private Shared nbFilesImported As Integer


    Public Shared Sub Import()

        directoryExplorer.ShowNewFolderButton = False
        directoryExplorer.RootFolder = Environment.SpecialFolder.MyComputer
        directoryExplorer.Description = "Selectionnez la clé USB IGNIS et appuyez sur OK"

        Dim supposedUSBDir As IO.DirectoryInfo

        supposedUSBDir = New IO.DirectoryInfo(XmlSettings.Settings.instance.Usine.USB_DIRECTORY)

        If (isValidUSBDirectory(supposedUSBDir)) Then

            USBDirectory = supposedUSBDir
            makeImports()

        Else

            If (directoryExplorer.ShowDialog(MainWindow.instance) = Windows.Forms.DialogResult.OK) Then

                supposedUSBDir = New IO.DirectoryInfo(directoryExplorer.SelectedPath)

                If (isValidUSBDirectory(supposedUSBDir)) Then

                    USBDirectory = supposedUSBDir

                    XmlSettings.Settings.instance.Usine.USB_DIRECTORY = USBDirectory.FullName
                    XmlSettings.Settings.instance.save()

                    makeImports()

                Else

                    MessageBox.Show("Le répertoire sélectionné est incorrect.", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If


            End If

        End If

    End Sub

    Private Shared Sub makeImports()

        Windows.Forms.Cursor.Current = Cursors.AppStarting

        nbFilesImported = 0

        CSVImport()
        LOGImport()
        EventsImport()
        MDBImport()

        ' Update archive image in another thread for usability and convenience
        Dim updateThrd As New Threading.Thread(AddressOf updateArchivesImage)
        updateThrd.Start()

        Windows.Forms.Cursor.Current = Cursors.Default

        MessageBox.Show("Importation des fichiers sources terminée!" & Environment.NewLine & nbFilesImported & " fichiers importés.", "Succès!", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Public Shared Function isValidUSBDirectory(directory As IO.DirectoryInfo) As Boolean

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

            Return usbHasCheminTXT And usbHasImporterEXE And usbHasRessourcesDIR

        Else
            Return False
        End If

    End Function

    Private Shared Sub CSVImport()

        Dim dataDir As New IO.DirectoryInfo(USBDirectory.FullName & "\Ressources\Temporary archives\Data")

        If (dataDir.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.CSV.FILE_NAME_REGEX)

            For Each file In dataDir.GetFiles

                Dim match = regex.Match(file.Name)

                If (match.Success) Then

                    ProgramController.DataFilesPersistence.addCSVFile(file)

                    nbFilesImported += 1

                    ' #refactor - maybe keep the date in the file object

                    If (newestImportedFiles(NEWEST_IMPORTED_FILE.CSV) Is Nothing OrElse _
                        CSVFile.getDateFromFileName(file.Name) > CSVFile.getDateFromFileName(newestImportedFiles(NEWEST_IMPORTED_FILE.CSV))) Then

                        newestImportedFiles(NEWEST_IMPORTED_FILE.CSV) = file.Name
                    End If

                End If

            Next

        End If

    End Sub

    Private Shared Sub LOGImport()

        Dim dataDir As New IO.DirectoryInfo(USBDirectory.FullName & "\Ressources\Temporary archives\Data")

        If (dataDir.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.LOG.FILE_NAME_REGEX)

            For Each file In dataDir.GetFiles

                Dim match = regex.Match(file.Name)

                If (match.Success) Then

                    ProgramController.DataFilesPersistence.addLOGFile(file)

                    nbFilesImported += 1

                    ' #refactor - maybe keep the date in the file object

                    If (newestImportedFiles(NEWEST_IMPORTED_FILE.LOG) Is Nothing OrElse _
                        LOGFile.getDateFromFileName(file.Name) > LOGFile.getDateFromFileName(newestImportedFiles(NEWEST_IMPORTED_FILE.LOG))) Then

                        newestImportedFiles(NEWEST_IMPORTED_FILE.LOG) = file.Name
                    End If

                End If

            Next

        End If

    End Sub

    Private Shared Sub MDBImport()

        Dim archivesDir As New IO.DirectoryInfo(USBDirectory.FullName & "\Ressources\Temporary archives")

        If (archivesDir.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.MDB.FILE_NAME_REGEX)

            For Each file In archivesDir.GetFiles

                Dim match = regex.Match(file.Name)

                If (match.Success) Then

                    Dim copiedFile = ProgramController.DataFilesPersistence.addMDBFile(file)

                    OleDBAdapter.reset(copiedFile)

                    nbFilesImported += 1

                End If

            Next

        End If

    End Sub

    Private Shared Sub EventsImport()

        Dim eventsDir As New IO.DirectoryInfo(USBDirectory.FullName & "\Ressources\Temporary archives\Events")

        If (eventsDir.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.Events.FILE_NAME_REGEX)

            For Each file In eventsDir.GetFiles

                Dim match = regex.Match(file.Name)

                If (match.Success) Then

                    ProgramController.DataFilesPersistence.addEventsFile(file)

                    nbFilesImported += 1

                    ' #refactor - maybe keep the date in the file object

                    If (newestImportedFiles(NEWEST_IMPORTED_FILE.EVENTS) Is Nothing OrElse _
                        EventsFile.getDateFromFileName(file.Name) > EventsFile.getDateFromFileName(newestImportedFiles(NEWEST_IMPORTED_FILE.EVENTS))) Then

                        newestImportedFiles(NEWEST_IMPORTED_FILE.EVENTS) = file.Name
                    End If

                End If

            Next

        End If

    End Sub

    Public Shared Sub updateArchivesImage()

        Dim archivesImageFile As New IO.FileInfo(USBDirectory.FullName & "\Ressources\ArchivesImage")

        If (archivesImageFile.Exists) Then

            Chrono.start()

            Dim writer As New IO.StreamWriter(archivesImageFile.FullName)

            ' csv
            For Each file As IO.FileInfo In New IO.DirectoryInfo(Constants.Paths.CSV_ARCHIVES_DIRECTORY).GetFiles

                writer.WriteLine(file.Name)

            Next

            ' log
            For Each file As IO.FileInfo In New IO.DirectoryInfo(Constants.Paths.LOG_ARCHIVES_DIRECTORY).GetFiles

                writer.WriteLine(file.Name)

            Next

            ' events
            For Each file As IO.FileInfo In New IO.DirectoryInfo(Constants.Paths.EVENTS_ARCHIVES_DIRECTORY).GetFiles

                writer.WriteLine(file.Name)

            Next

            writer.Close()
            Chrono._stop()


        End If

    End Sub

    Private Enum NEWEST_IMPORTED_FILE
        CSV = 0
        LOG = 1
        EVENTS = 2
    End Enum


    ' NEW FUNCTIONNALITIES

    Public Shared Function identifyFilesToImport() As List(Of DataFile)

        If (IsNothing(USBDirectory)) Then
            USBDirectory = New IO.DirectoryInfo(XmlSettings.Settings.instance.Usine.USB_DIRECTORY)
        End If

        Dim fileList As New List(Of DataFile)

        If (XmlSettings.Settings.instance.Usine.DataFiles.CSV.ACTIVE) Then
            fileList.AddRange(identifyCSVFiles)
        End If

        If (XmlSettings.Settings.instance.Usine.DataFiles.LOG.ACTIVE) Then
            fileList.AddRange(identifyLOGFiles)
        End If

        If (XmlSettings.Settings.instance.Usine.Events.ACTIVE) Then
            fileList.AddRange(identifyEventsFiles)
        End If

        If (XmlSettings.Settings.instance.Usine.DataFiles.MDB.ACTIVE) Then
            fileList.AddRange(identifyMDBFiles)
        End If

        Return fileList
    End Function

    Private Shared Function identifyCSVFiles() As List(Of CSVFile)

        Dim fileList As New List(Of CSVFile)

        If (dataDirectory.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.CSV.FILE_NAME_REGEX)

            For Each file As IO.FileInfo In dataDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    fileList.Add(New CSVFile(file.FullName))

                End If

            Next

        End If

        Return fileList
    End Function

    Private Shared Function identifyLOGFiles() As List(Of LOGFile)

        Dim fileList As New List(Of LOGFile)

        If (dataDirectory.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.LOG.FILE_NAME_REGEX)

            For Each file As IO.FileInfo In dataDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    fileList.Add(New LOGFile(file.FullName))

                End If

            Next

        End If

        Return fileList
    End Function

    Private Shared Function identifyEventsFiles() As List(Of EventsFile)

        Dim fileList As New List(Of EventsFile)

        If (dataDirectory.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.Events.FILE_NAME_REGEX)

            For Each file As IO.FileInfo In dataDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    fileList.Add(New EventsFile(file.FullName))

                End If

            Next

        End If

        Return fileList
    End Function

    Public Shared Function identifyMDBFiles() As List(Of MDBFile)

        Dim fileList As New List(Of MDBFile)

        If (temporaryArchivesDirectory.Exists) Then

            Dim regex As New System.Text.RegularExpressions.Regex(Constants.Input.MDB.FILE_NAME_REGEX)

            For Each file In temporaryArchivesDirectory.GetFiles

                If (regex.Match(file.Name).Success) Then

                    fileList.Add(New MDBFile(file.FullName))

                End If

            Next

        End If

        Return fileList
    End Function



    Private Shared _usbDirectory As IO.DirectoryInfo
    Private Shared temporaryArchivesDirectory As IO.DirectoryInfo
    Private Shared dataDirectory As IO.DirectoryInfo
    Private Shared eventDirectory As IO.DirectoryInfo

    Public Shared Property USBDirectory As IO.DirectoryInfo
        Get
            Return _usbDirectory
        End Get
        Set(value As IO.DirectoryInfo)

            _usbDirectory = value

            temporaryArchivesDirectory = New IO.DirectoryInfo(_usbDirectory.FullName & "\Ressources\Temporary archives")
            dataDirectory = New IO.DirectoryInfo(temporaryArchivesDirectory.FullName & "\Data")
            eventDirectory = New IO.DirectoryInfo(temporaryArchivesDirectory.FullName & "\Events")

        End Set
    End Property

End Class
