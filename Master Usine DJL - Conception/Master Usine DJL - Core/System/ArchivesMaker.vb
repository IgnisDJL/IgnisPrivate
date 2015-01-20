Imports System.IO

Public Class ArchivesMaker

    Public Shared Sub verifyArchives(year As Integer)

        'Dim archivesDir As New DirectoryInfo(Constants.Paths.ARCHIVES_DIRECTORY)

        'If (archivesDir.Exists) Then

        '    Dim yearDir As New DirectoryInfo(archivesDir.FullName & "\" & year.ToString)

        '    If (yearDir.Exists) Then

        '        For m = 1 To 12

        '            Dim monthDir As New DirectoryInfo(yearDir.FullName & "\" & m.ToString("00"))

        '            If (monthDir.Exists()) Then

        '                For d = 1 To DateTime.DaysInMonth(year, m)

        '                    Dim dayDir As New DirectoryInfo(monthDir.FullName & "\" & d.ToString("00"))

        '                    If (dayDir.Exists()) Then

        '                        Dim dataDir As New DirectoryInfo(dayDir.FullName & "\" & "Data")
        '                        If (Not dataDir.Exists) Then
        '                            dataDir.Create()
        '                        End If

        '                        Dim eventsDir As New DirectoryInfo(dayDir.FullName & "\" & "Events")
        '                        If (Not eventsDir.Exists) Then
        '                            eventsDir.Create()
        '                        End If

        '                        Dim reportsDir As New DirectoryInfo(dayDir.FullName & "\" & "Reports")
        '                        If (Not reportsDir.Exists) Then
        '                            reportsDir.Create()
        '                        End If

        '                    Else
        '                        dayDir.Create()
        '                        d -= 1
        '                        Continue For
        '                    End If

        '                Next

        '            Else
        '                monthDir.Create()
        '                m -= 1
        '                Continue For
        '            End If


        '        Next

        '    Else
        '        yearDir.Create()
        '        ArchivesMaker.verifyArchives(year)
        '    End If

        'Else
        '    archivesDir.Create()
        '    ArchivesMaker.verifyArchives(year)
        'End If

    End Sub

    Public Shared Sub verifyDBDirectory()

        'Dim dbDir As New DirectoryInfo(Constants.Paths.MDB_FILES_DIRECTORY)

        'If (Not dbDir.Exists) Then
        '    dbDir.Create()
        'End If

    End Sub

End Class
