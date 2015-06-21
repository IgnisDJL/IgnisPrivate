
Public Class ImportController_1

    Private settings As XmlSettings.Settings


    Private continueSourceFileList As List(Of SourceFile)
    Private discontinueSourceFileList As List(Of SourceFile)
    Private _usbDirectory As IO.DirectoryInfo
    Private temporaryArchivesDirectory As IO.DirectoryInfo
    Private dataDirectory As IO.DirectoryInfo
    Private eventDirectory As IO.DirectoryInfo
    Private updateArchivesImageThread As Threading.Thread

    Private tobe_ImportedFiles As List(Of IO.FileInfo)



    Public Sub New(settings As XmlSettings.Settings)

        Me.settings = settings
        Me.continueSourceFileList = New List(Of SourceFile)
        Me.discontinueSourceFileList = New List(Of SourceFile)
        Me.tobe_ImportedFiles = New List(Of IO.FileInfo)
        PlantProduction.setPlantType(Me.settings.Usine.TYPE)
        PlantProduction.setPlantName(Me.settings.Usine.PLANT_NAME)

    End Sub

    Public Function importFiles() As Integer

        Dim newestSourceFileContinu As SourceFile = Nothing
        Dim newestSourceFileDiscontinu As SourceFile = Nothing
        Dim count = 0

        For Each continueSourceFile As SourceFile In continueSourceFileList
            Dim copiedFile = continueSourceFile.getFileInfo.CopyTo(Constants.Paths.LOG_ARCHIVES_DIRECTORY & continueSourceFile.getFileInfo.Name, True)
            count += 1
            If IsNothing(newestSourceFileContinu) Then

                newestSourceFileContinu = continueSourceFile

            ElseIf (continueSourceFile.Date_ > newestSourceFileContinu.Date_) Then

                tobe_ImportedFiles.Add(newestSourceFileContinu.getFileInfo)

                newestSourceFileContinu = continueSourceFile
            Else
                tobe_ImportedFiles.Add(continueSourceFile.getFileInfo)
            End If

            If Not String.IsNullOrEmpty(continueSourceFile.getEventFilePath()) Then
                Dim eventFile As IO.FileInfo = New IO.FileInfo(continueSourceFile.getEventFilePath())
                Dim copiedEventFile = eventFile.CopyTo(Constants.Paths.EVENTS_ARCHIVES_DIRECTORY & eventFile.Name, True)

            End If

        Next

        For Each discontinueSourceFile As SourceFile In Me.discontinueSourceFileList
            Dim copiedFile = discontinueSourceFile.getFileInfo.CopyTo(Constants.Paths.CSV_ARCHIVES_DIRECTORY & discontinueSourceFile.getFileInfo.Name, True)
            count += 1
            If IsNothing(newestSourceFileDiscontinu) Then
                newestSourceFileDiscontinu = discontinueSourceFile

            ElseIf discontinueSourceFile.Date_ > newestSourceFileDiscontinu.Date_ Then

                tobe_ImportedFiles.Add(newestSourceFileDiscontinu.getFileInfo)
                newestSourceFileDiscontinu = discontinueSourceFile
            Else
                tobe_ImportedFiles.Add(discontinueSourceFile.getFileInfo)
            End If
        Next

        Me.updateArchivesImage()

        Return count

    End Function


    Public Function identifyFilesToImport() As List(Of DataFile)
        PlantProduction.setPlantType(Me.settings.Usine.TYPE)

        Me.continueSourceFileList.Clear()
        Me.discontinueSourceFileList.Clear()

        If (IsNothing(USBDirectory)) Then
            USBDirectory = New IO.DirectoryInfo(XmlSettings.Settings.instance.Usine.USB_DIRECTORY)
        End If

        If (dataDirectory.Exists) Then

            Dim newestSourceFile As SourceFile = Nothing
            Dim regexLogFile As New System.Text.RegularExpressions.Regex(Constants.Input.LOG.FILE_NAME_REGEX)
            Dim regexEventFile As New System.Text.RegularExpressions.Regex(Constants.Input.Events.FILE_NAME_REGEX)
            Dim regexCSVFile As New System.Text.RegularExpressions.Regex(Constants.Input.CSV.FILE_NAME_REGEX)
            Dim regexMDBFile As New System.Text.RegularExpressions.Regex(Constants.Input.MDB.FILE_NAME_REGEX)
            Dim alreadyImportedFile As String = String.Empty

            Dim archivesImageFile As New IO.FileInfo(USBDirectory.FullName & "\Ressources\ArchivesImage")

            If (archivesImageFile.Exists) Then

                Dim reader As New IO.StreamReader(archivesImageFile.FullName)

                Try
                    alreadyImportedFile = reader.ReadToEnd()
                    reader.Close()
                Catch ex As Exception
                    reader.Close()
                End Try
            End If

            For Each file As IO.FileInfo In dataDirectory.GetFiles
                If (Not alreadyImportedFile.Contains(file.Name)) Then

                    If (regexLogFile.Match(file.Name).Success) And (PlantProduction.getPlantType = Constants.Settings.UsineType.LOG Or PlantProduction.getPlantType = Constants.Settings.UsineType.HYBRID) Then
                        Dim sourceFile As New SourceFile(file.FullName, New SourceFileLogAdapter())

                        If eventDirectory.Exists Then
                            For Each eventfile As IO.FileInfo In eventDirectory.GetFiles
                                If (sourceFile.Date_.Year.ToString + sourceFile.Date_.Month.ToString + sourceFile.Date_.Day.ToString + ".log").Equals(eventfile.Name) Then
                                    sourceFile.setEventFilePath(eventfile.FullName)
                                End If

                            Next
                        End If

                        Me.continueSourceFileList.Add(sourceFile)


                    ElseIf (regexCSVFile.Match(file.Name).Success) And (PlantProduction.getPlantType = Constants.Settings.UsineType.CSV Or PlantProduction.getPlantType = Constants.Settings.UsineType.MDB Or PlantProduction.getPlantType = Constants.Settings.UsineType.HYBRID) Then
                        Dim sourceFile As New SourceFile(file.FullName, New SourceFileCSVAdapter())

                        Me.discontinueSourceFileList.Add(sourceFile)

                    ElseIf (regexMDBFile.Match(file.Name).Success) And (PlantProduction.getPlantType = Constants.Settings.UsineType.CSV Or PlantProduction.getPlantType = Constants.Settings.UsineType.MDB) Then

                        For Each nouvelleDate As Date In getNouvellesDates(getLastDate(), file.FullName)

                            Dim sourceFile As New SourceFile(file.FullName, New SourceFileMarcotteAdapter(), nouvelleDate)

                            Me.discontinueSourceFileList.Add(sourceFile)

                        Next

                    End If
                End If
            Next
        End If


        Dim allFileToImport As List(Of DataFile) = New List(Of DataFile)

        allFileToImport.InsertRange(0, continueSourceFileList)
        allFileToImport.InsertRange(0, discontinueSourceFileList)
        Return allFileToImport
    End Function

    Private Function getLastDate() As Date
        Dim readingStream As System.IO.StreamReader = Nothing
        Dim indexMDB As String = Nothing
        readingStream = New System.IO.StreamReader(USBDirectory.FullName & "\Ressources\indexMDB")
        indexMDB = readingStream.ReadToEnd
        Return indexMDB
    End Function


    Public Function getNouvellesDates(derniereDate As Date, sourceFilePath As String) As List(Of Date)

        OleDBAdapter.initialize(sourceFilePath)
        Dim query = "SELECT distinct DateValue(Date) FROM Cycle WHERE ( Date >= " + "#" + derniereDate.Year.ToString + "/" + derniereDate.Month.ToString + "/" + (derniereDate.Day + 1).ToString + "#)"
        Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
        Dim mdbListDate = dbCommand.ExecuteReader

        Dim nouvellesDates = New List(Of Date)

        While (mdbListDate.Read)

            nouvellesDates.Add(mdbListDate(0))
        End While
        dbCommand.Dispose()
        mdbListDate.Close()
        Return nouvellesDates
    End Function


    Private Sub updateArchivesImage()

        Dim archivesImageFile As New IO.FileInfo(USBDirectory.FullName & "\Ressources\ArchivesImage")

        If (archivesImageFile.Exists) Then

            Dim writer As New IO.StreamWriter(archivesImageFile.FullName, True)

            Try

                For Each file As IO.FileInfo In tobe_ImportedFiles
                    writer.WriteLine(file.Name)
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
        Me.continueSourceFileList.Clear()
        Me.discontinueSourceFileList.Clear()
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
            Return If(IsNothing(Me.continueSourceFileList.Concat(Me.discontinueSourceFileList)), 0, Me.continueSourceFileList.Concat(Me.discontinueSourceFileList).Count)
        End Get
    End Property

End Class
