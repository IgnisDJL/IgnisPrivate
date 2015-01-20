Imports System.IO

Public Class FileRetreiver

    Public Const CSV_EXTENSION = ".csv"
    Public Const LOG_EXTENSION = ".log"
    Public Const MDB_EXTENSION = ".mdb"
    Public Const EVENTS_EXTENSION = ".log"

    Public Const XLSX_EXTENSION = ".xlsx"
    Public Const DOCX_EXTENSION = ".docx"

    Public Const DATA_DIR = "Data\"
    Public Const EVENTS_DIR = "Events\"
    Public Const REPORTS_DIR = "Reports\"

    Private dataDirectory As DirectoryInfo
    Private eventsDirectory As DirectoryInfo
    Private reportsDirectory As DirectoryInfo


    Public Sub New(year As Integer, month As Integer, day As Integer)

        'Dim dateDirectoryPath = Constants.Paths.ARCHIVES_DIRECTORY & year.ToString & "\" & month.ToString("00") & "\" & day.ToString("00") & "\"

        'Me.dataDirectory = New DirectoryInfo(dateDirectoryPath & DATA_DIR)

        'Me.eventsDirectory = New DirectoryInfo(dateDirectoryPath & EVENTS_DIR)

        'Me.reportsDirectory = New DirectoryInfo(dateDirectoryPath & REPORTS_DIR)

        'If (Not Me.dataDirectory.Exists Or _
        '   Not Me.eventsDirectory.Exists Or _
        '   Not Me.reportsDirectory.Exists) Then

        '    Throw New InvalidDateException("Invalid date")
        'End If

    End Sub

    Public Function getDataFile(fileExtension As String) As FileInfo

        'If (fileExtension = MDB_EXTENSION) Then

        '    If (IsNothing(OleDBAdapter.MDB_FILE)) Then

        '        For Each fileInfo In New DirectoryInfo(Constants.Paths.MDB_FILES_DIRECTORY).GetFiles()

        '            If (fileInfo.Name.EndsWith(".mdb")) Then ' In case the .ldb is still there...
        '                OleDBAdapter.MDB_FILE = fileInfo

        '                Exit For
        '            End If

        '        Next

        '    End If

        '    Return OleDBAdapter.MDB_FILE

        'Else

        '    Dim files = Me.dataDirectory.GetFiles("*" & fileExtension)

        '    If (files.Count > 0) Then
        '        Return files.ElementAt(0)
        '    Else
        '        Return Nothing
        '    End If

        'End If

    End Function

    Public Function getEventsFile() As FileInfo
        'Handle exception that the file is not there
        
        Dim files = Me.eventsDirectory.GetFiles("*" & EVENTS_EXTENSION)

        If (files.Count > 0) Then
            Return files.ElementAt(0)
        Else
            Return Nothing
        End If


    End Function

    Public Function getReportFile(fileExtension As String) As FileInfo

        Dim files = Me.reportsDirectory.GetFiles("*" & fileExtension)

        If (files.Count > 0) Then
            Return files.ElementAt(0)
        Else
            Return Nothing
        End If


    End Function

End Class
