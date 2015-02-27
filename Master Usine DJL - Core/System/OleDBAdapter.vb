Imports System.Data.OleDb

Public Class OleDBAdapter

    Public Shared Property sourceFilePath As String

    Public Shared Property MDB_CONNECTION As OleDbConnection


    Public Shared Sub initialize(path As String)

        If (IsNothing(MDB_CONNECTION) OrElse Not MDB_CONNECTION.State = ConnectionState.Open) Then

            sourceFilePath = path

            MDB_CONNECTION = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + sourceFilePath + ";User Id=admin; Password=;")

            MDB_CONNECTION.Open()
        End If

    End Sub

    Public Shared Sub reset(mdbFile As IO.FileInfo)

        If (Not IsNothing(MDB_CONNECTION) AndAlso MDB_CONNECTION.State = ConnectionState.Open) Then

            MDB_CONNECTION.Close()

        End If

        initialize(sourceFilePath)

    End Sub

    Public Shared Sub kill()

        If (Not IsNothing(MDB_CONNECTION) AndAlso Not MDB_CONNECTION.State = ConnectionState.Closed) Then

            MDB_CONNECTION.Close()

        End If

    End Sub

End Class
