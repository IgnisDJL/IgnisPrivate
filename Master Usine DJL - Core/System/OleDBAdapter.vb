Imports System.Data.OleDb

Public Class OleDBAdapter

    Public Shared Property MDB_FILE As IO.FileInfo

    Public Shared Property MDB_CONNECTION As OleDbConnection


    Public Shared Sub initialize(mdbFile As IO.FileInfo)

        If (IsNothing(MDB_CONNECTION) OrElse Not MDB_CONNECTION.State = ConnectionState.Open) Then

            MDB_FILE = mdbFile

            Dim connectionStr = Constants.Input.MDB.CONNECTION_STRING & _
                       "Data Source=" & mdbFile.FullName & ";"

            MDB_CONNECTION = New System.Data.OleDb.OleDbConnection(connectionStr)

            MDB_CONNECTION.Open()

        End If

    End Sub

    Public Shared Sub reset(mdbFile As IO.FileInfo)

        If (Not IsNothing(MDB_CONNECTION) AndAlso MDB_CONNECTION.State = ConnectionState.Open) Then

            MDB_CONNECTION.Close()

        End If

        initialize(mdbFile)

    End Sub

    Public Shared Sub kill()

        If (Not IsNothing(MDB_CONNECTION) AndAlso Not MDB_CONNECTION.State = ConnectionState.Closed) Then

            MDB_CONNECTION.Close()

        End If

    End Sub

End Class
