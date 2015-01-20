Imports System.IO
Imports IGNIS.Constants.Database.DataFilesDB

Public Class DataFilesSQLDatabase
    Inherits DataFilesPersistence

    Private database As SQLiteAdapter

    Public Sub New(databaseAdapter As SQLiteAdapter)

        Me.database = databaseAdapter

    End Sub

    Public Overrides Function addCSVFile(fileToImport As FileInfo) As IO.FileInfo

        Dim daySQL As String = CSVFile.getDateFromFileName(fileToImport.Name).ToString(Constants.Database.SQL.DATE_FORMAT)

        Dim copiedFile = fileToImport.CopyTo(Constants.Paths.CSV_ARCHIVES_DIRECTORY & fileToImport.Name, True)

        Dim row As New Dictionary(Of String, String)
        row.Add(Columns.DATE_, daySQL)
        row.Add(Columns.PATH, database.preventSQLInjection(copiedFile.FullName.Replace(Constants.Paths.PROGRAM_ROOT, "")))
        row.Add(Columns.NAME, database.preventSQLInjection(copiedFile.Name))

        If (CInt(database.ExecuteScalar("SELECT COUNT(*) FROM " & TableNames.CSV & " WHERE " & Columns.DATE_ & "='" & daySQL & "'")) > 0) Then
            database.Update(TableNames.CSV, row, Columns.DATE_ & "='" & daySQL & "'")
        Else
            database.Insert(TableNames.CSV, row)
        End If

        Return copiedFile
    End Function

    Public Overrides Function addEventsFile(fileToImport As FileInfo) As IO.FileInfo

        Dim daySQL As String = EventsFile.getDateFromFileName(fileToImport.Name).ToString(Constants.Database.SQL.DATE_FORMAT)

        Dim copiedFile = fileToImport.CopyTo(Constants.Paths.EVENTS_ARCHIVES_DIRECTORY & fileToImport.Name, True)

        Dim row As New Dictionary(Of String, String)
        row.Add(Columns.DATE_, daySQL)
        row.Add(Columns.PATH, database.preventSQLInjection(copiedFile.FullName.Replace(Constants.Paths.PROGRAM_ROOT, "")))
        row.Add(Columns.NAME, database.preventSQLInjection(copiedFile.Name))

        If (CInt(database.ExecuteScalar("SELECT COUNT(*) FROM " & TableNames.EVENTS & " WHERE " & Columns.DATE_ & "='" & daySQL & "'")) > 0) Then
            database.Update(TableNames.EVENTS, row, Columns.DATE_ & "='" & daySQL & "'")
        Else
            database.Insert(TableNames.EVENTS, row)
        End If

        Return copiedFile
    End Function

    Public Overrides Function addLOGFile(fileToImport As FileInfo) As IO.FileInfo

        Dim daySQL As String = LOGFile.getDateFromFileName(fileToImport.Name).ToString(Constants.Database.SQL.DATE_FORMAT)

        Dim copiedFile = fileToImport.CopyTo(Constants.Paths.LOG_ARCHIVES_DIRECTORY & fileToImport.Name, True)

        Dim row As New Dictionary(Of String, String)
        row.Add(Columns.DATE_, daySQL)
        row.Add(Columns.PATH, database.preventSQLInjection(copiedFile.FullName.Replace(Constants.Paths.PROGRAM_ROOT, "")))
        row.Add(Columns.NAME, database.preventSQLInjection(copiedFile.Name))

        If (CInt(database.ExecuteScalar("SELECT COUNT(*) FROM " & TableNames.LOG & " WHERE " & Columns.DATE_ & "='" & daySQL & "'")) > 0) Then
            database.Update(TableNames.LOG, row, Columns.DATE_ & "='" & daySQL & "'")
        Else
            database.Insert(TableNames.LOG, row)
        End If

        Return copiedFile
    End Function

    Public Overrides Function addMDBFile(fileToImport As FileInfo) As IO.FileInfo

        database.ClearTable(TableNames.MDB)

        Dim copiedFile = fileToImport.CopyTo(Constants.Paths.MDB_ARCHIVES_DIRECTORY & fileToImport.Name, True)

        ' Make copy
        fileToImport.CopyTo(Constants.Paths.MDB_ARCHIVES_DIRECTORY & fileToImport.Name.Substring(0, fileToImport.Name.Length - 4) & "-copy.mdb", True)

        Dim row As New Dictionary(Of String, String)
        row.Add(Columns.PATH, database.preventSQLInjection(copiedFile.FullName.Replace(Constants.Paths.PROGRAM_ROOT, "")))

        database.Insert(TableNames.MDB, row)

        Return copiedFile
    End Function

    Public Overrides Function getCSVFile(day As Date) As CSVFile

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.CSV & " WHERE " & Columns.DATE_ & "='" & day.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")

        If (dataTable.Rows.Count > 0) Then

            Return New CSVFile(Constants.Paths.PROGRAM_ROOT & dataTable.Rows(0)(Columns.PATH))

        Else

            ' #exception - no csv for that day
            Return Nothing
        End If
    End Function

    Public Overrides Function getAllCSVFiles() As List(Of CSVFile)

        Dim allCSVFiles As New List(Of CSVFile)

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.CSV & " ORDER BY " & Constants.Database.DataFilesDB.Columns.DATE_ & " DESC")

        For Each row As Data.DataRow In dataTable.Rows

            allCSVFiles.Add(New CSVFile(Constants.Paths.PROGRAM_ROOT & row(Constants.Database.DataFilesDB.Columns.PATH)))

        Next

        Return allCSVFiles
    End Function

    Public Overrides Function getEventsFile(day As Date) As EventsFile

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.EVENTS & " WHERE " & Columns.DATE_ & "='" & day.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")

        If (dataTable.Rows.Count > 0) Then

            Return New EventsFile(Constants.Paths.PROGRAM_ROOT & dataTable.Rows(0)(Columns.PATH))

        Else

            ' #exception - no events for that day
            Return Nothing
        End If
    End Function

    Public Overrides Function getAllEventsFiles() As List(Of EventsFile)

        Dim allEventFiles As New List(Of EventsFile)

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.EVENTS & " ORDER BY " & Constants.Database.DataFilesDB.Columns.DATE_ & " DESC")

        For Each row As Data.DataRow In dataTable.Rows

            allEventFiles.Add(New EventsFile(Constants.Paths.PROGRAM_ROOT & row(Constants.Database.DataFilesDB.Columns.PATH)))

        Next

        Return allEventFiles

    End Function

    Public Overrides Function getLOGFile(day As Date) As LOGFile

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.LOG & " WHERE " & Columns.DATE_ & "='" & day.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")

        If (dataTable.Rows.Count > 0) Then

            Return New LOGFile(Constants.Paths.PROGRAM_ROOT & dataTable.Rows(0)(Columns.PATH))

        Else

            ' #exception - no log for that day
            Return Nothing
        End If
    End Function

    Public Overrides Function getAllLOGFiles() As List(Of LOGFile)

        Dim allLOGFiles As New List(Of LOGFile)

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.LOG & " ORDER BY " & Constants.Database.DataFilesDB.Columns.DATE_ & " DESC")

        For Each row As Data.DataRow In dataTable.Rows

            allLOGFiles.Add(New LOGFile(Constants.Paths.PROGRAM_ROOT & row(Constants.Database.DataFilesDB.Columns.PATH)))

        Next

        Return allLOGFiles

    End Function

    Public Overrides Function getMDBFile() As MDBFile

        Dim dataTable As DataTable = database.GetDataTable("SELECT * FROM " & TableNames.MDB)

        If (dataTable.Rows.Count > 0) Then

            Dim filePath As String = Constants.Paths.PROGRAM_ROOT & dataTable.Rows(0)(Columns.PATH)
            Dim copyPath As String = filePath.Substring(0, filePath.Length - 4) & "-copy.mdb" '#refactor - extract constant

            Return New MDBFile(filePath, copyPath)

        Else

            ' #exception - no csv for that day
            Return Nothing
        End If
    End Function

    Public Overrides Sub reset()

        database.ClearTable(TableNames.CSV)
        database.ClearTable(TableNames.LOG)
        database.ClearTable(TableNames.MDB)
        database.ClearTable(TableNames.EVENTS)

        verifyFormat()

    End Sub

    Public Overrides Sub initializeImportation()

    End Sub

    Public Overrides Sub finalizeImportation()

    End Sub

    Public Overrides Function verifyFormat() As Boolean

        Try

            ' #refactor - Extract method
            ' Check the CSV data files archive directory
            Dim csvDirectory As New DirectoryInfo(Constants.Paths.CSV_ARCHIVES_DIRECTORY)

            If (Not csvDirectory.Exists) Then
                csvDirectory.Create()
            End If

            ' Check the LOG data files archive directory
            Dim logDirectory As New DirectoryInfo(Constants.Paths.LOG_ARCHIVES_DIRECTORY)

            If (Not logDirectory.Exists) Then
                logDirectory.Create()
            End If

            ' Check the MDB data files archive directory
            Dim mdbDirectory As New DirectoryInfo(Constants.Paths.MDB_ARCHIVES_DIRECTORY)

            If (Not mdbDirectory.Exists) Then
                mdbDirectory.Create()
            End If

            ' Check the Events data files archive directory
            Dim eventsDirectory As New DirectoryInfo(Constants.Paths.EVENTS_ARCHIVES_DIRECTORY)

            If (Not eventsDirectory.Exists) Then
                eventsDirectory.Create()
            End If

            ' #refactor - Extract method
            Dim tableColumns As Dictionary(Of String, String)
            ' Check database format for csv files table
            If (Not database.tableExists(TableNames.CSV)) Then

                tableColumns = New Dictionary(Of String, String)
                tableColumns.Add(Columns.DATE_, "DATE") ' #refactor - Extract constant
                tableColumns.Add(Columns.PATH, "VARCHAR(63)") ' Doesn't really matter since sqlite doesn't trunkate if bigger
                tableColumns.Add(Columns.NAME, "VARCHAR(31)")

                database.createTable(TableNames.CSV, tableColumns)

                Console.WriteLine(TableNames.CSV & " table was created : ")
                Me.database.printTable(TableNames.CSV)
            End If

            ' Check database format for log files table
            If (Not database.tableExists(TableNames.LOG)) Then

                tableColumns = New Dictionary(Of String, String)
                tableColumns.Add(Columns.DATE_, "DATE")
                tableColumns.Add(Columns.PATH, "VARCHAR(63)")
                tableColumns.Add(Columns.NAME, "VARCHAR(31)")

                database.createTable(TableNames.LOG, tableColumns)

                Console.WriteLine(TableNames.LOG & " table was created : ")
                Me.database.printTable(TableNames.LOG)
            End If

            ' Check database format for events files table
            If (Not database.tableExists(TableNames.EVENTS)) Then

                tableColumns = New Dictionary(Of String, String)
                tableColumns.Add(Columns.DATE_, "DATE")
                tableColumns.Add(Columns.PATH, "VARCHAR(63)")
                tableColumns.Add(Columns.NAME, "VARCHAR(31)")

                database.createTable(TableNames.EVENTS, tableColumns)

                Console.WriteLine(TableNames.EVENTS & " table was created : ")
                Me.database.printTable(TableNames.EVENTS)
            End If

            ' Check database format for events files table
            If (Not database.tableExists(TableNames.MDB)) Then

                tableColumns = New Dictionary(Of String, String)
                tableColumns.Add(Columns.PATH, "VARCHAR(63)")

                database.createTable(TableNames.MDB, tableColumns)

                Console.WriteLine(TableNames.MDB & " table was created : ")
                Me.database.printTable(TableNames.MDB)
            End If

            ' #refactor - Extract method
            Dim table As DataTable
            ' Check references for csv files
            table = database.GetDataTable("SELECT * FROM " & TableNames.CSV)

            For Each row As DataRow In table.Rows

                Dim path As String = row(Columns.PATH)

                If (Not New FileInfo(path).Exists) Then
                    database.Delete(TableNames.CSV, Columns.PATH & "='" & path & "'")
                End If

            Next

            ' Check references for log files
            table = database.GetDataTable("SELECT * FROM " & TableNames.LOG)

            For Each row As DataRow In table.Rows

                Dim path As String = row(Columns.PATH)

                If (Not New FileInfo(path).Exists) Then
                    database.Delete(TableNames.LOG, Columns.PATH & "='" & path & "'")
                End If

            Next

            ' Check references for events files
            table = database.GetDataTable("SELECT * FROM " & TableNames.EVENTS)

            For Each row As DataRow In table.Rows

                Dim path As String = row(Columns.PATH)

                If (Not New FileInfo(path).Exists) Then
                    database.Delete(TableNames.EVENTS, Columns.PATH & "='" & path & "'")
                End If

            Next

            ' Check references for events files
            table = database.GetDataTable("SELECT * FROM " & TableNames.MDB)

            For Each row As DataRow In table.Rows

                Dim path As String = row(Columns.PATH)

                If (Not New FileInfo(path).Exists) Then
                    database.Delete(TableNames.MDB, Columns.PATH & "='" & path & "'")
                End If

            Next


        Catch ex As Exception

            UIExceptionHandler.instance.handle(ex)

            Return False
        End Try

        Return True

    End Function

    ''' <remarks>This doesn't actually returns all the csv, log and events files (only the log and events files that have the same date than the csv files are joined)</remarks>
    Public Overrides Function getAllCSVLOGAndEventsFiles() As List(Of DataFile())

        Dim returnList = New List(Of DataFile())

        Dim dataTable As DataTable = database.GetDataTable("SELECT " & TableNames.CSV & "." & Columns.DATE_ & " ," & _
                                                           TableNames.CSV & "." & Columns.PATH & " ," & _
                                                           TableNames.LOG & "." & Columns.PATH & " ," & _
                                                           TableNames.EVENTS & "." & Columns.PATH & _
                                                           " FROM " & TableNames.CSV & _
                                                           " LEFT JOIN " & TableNames.LOG & " ON " & TableNames.LOG & "." & Columns.DATE_ & "=" & TableNames.CSV & "." & Columns.DATE_ & _
                                                           " LEFT JOIN " & TableNames.EVENTS & " ON " & TableNames.EVENTS & "." & Columns.DATE_ & "=" & TableNames.CSV & "." & Columns.DATE_ & _
                                                           " ORDER BY " & TableNames.CSV & "." & Columns.DATE_ & " DESC")

        For Each _row As Data.DataRow In dataTable.Rows

            Dim dataFiles(2) As DataFile

            dataFiles(0) = If(TypeOf _row(1) Is System.DBNull, Nothing, New CSVFile(_row(1)))
            dataFiles(1) = If(TypeOf _row(2) Is System.DBNull, Nothing, New CSVFile(_row(2)))
            dataFiles(2) = If(TypeOf _row(3) Is System.DBNull, Nothing, New CSVFile(_row(3)))

            returnList.Add(dataFiles)
        Next

        Return returnList

    End Function

End Class
