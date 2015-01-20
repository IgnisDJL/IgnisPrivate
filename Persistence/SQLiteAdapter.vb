Imports System.Data.SQLite
Imports System.Data.Common

''' <summary>
''' Adapter to the SQLite database
''' </summary>
Public Class SQLiteAdapter

    Private Shared ReadOnly CONNECTION_STRING As String = "Data Source=" & Constants.Paths.DATABASE_FILE & "; Version=3"

    Public Sub New()

    End Sub

    Public Function GetDataTable(sql As String) As DataTable

        Dim dt As DataTable = New DataTable()

        Dim cnn As SQLiteConnection = Nothing

        Try

            cnn = New SQLiteConnection(CONNECTION_STRING)
            cnn.Open()
            Dim mycommand As SQLiteCommand = New SQLiteCommand(cnn)
            mycommand.CommandText = sql
            Dim reader As SQLiteDataReader = mycommand.ExecuteReader()
            dt.Load(reader)
            reader.Close()
            cnn.Close()

        Catch te As Threading.ThreadAbortException

            If (Not IsNothing(cnn)) Then
                cnn.Close()
            End If

        Catch e As Exception

            Throw New Exception(e.Message)

        End Try

        Return dt

    End Function


    Public Function ExecuteNonQuery(sql As String) As Integer

        Dim cnn As SQLiteConnection = New SQLiteConnection(CONNECTION_STRING)

        cnn.Open()

        Dim mycommand As SQLiteCommand = New SQLiteCommand(cnn)

        mycommand.CommandText = sql

        Dim rowsUpdated As Integer = CInt(mycommand.ExecuteNonQuery())

        cnn.Close()

        Return rowsUpdated

    End Function

    Public Function ExecuteScalar(sql As String) As Object

        Dim cnn As SQLiteConnection = New SQLiteConnection(CONNECTION_STRING)

        cnn.Open()

        Dim mycommand As SQLiteCommand = New SQLiteCommand(cnn)

        mycommand.CommandText = sql

        Dim ret = mycommand.ExecuteScalar()

        cnn.Close()

        Return ret

    End Function


    Public Function Update(tableName As String, data As Dictionary(Of String, String), where As String) As Boolean

        Dim vals As String = ""
        Dim returnCode As Boolean = True

        If (data.Count >= 1) Then

            For Each val As KeyValuePair(Of String, String) In data

                vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString())

            Next

            vals = vals.Substring(0, vals.Length - 1)

        End If

        Try

            Me.ExecuteNonQuery(String.Format("update {0} set {1} where {2}", tableName, vals, where))

        Catch

            returnCode = False

        End Try

        Return returnCode

    End Function


    Public Function Delete(tableName As String, where As String) As Boolean

        Dim returnCode As Boolean = True
        Try

            Me.ExecuteNonQuery(String.Format("delete from {0} where {1}", tableName, where))

        Catch fail As Exception

            Console.WriteLine(fail.Message)

            returnCode = False

        End Try

        Return returnCode

    End Function


    Public Function Insert(tableName As String, data As Dictionary(Of String, String)) As Boolean

        Dim columns As String = ""
        Dim values As String = ""
        Dim returnCode As Boolean = True

        For Each val As KeyValuePair(Of String, String) In data

            columns += String.Format(" {0},", val.Key.ToString())
            values += String.Format(" '{0}',", val.Value)

        Next

        columns = columns.Substring(0, columns.Length - 1)
        values = values.Substring(0, values.Length - 1)

        Try

            Me.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2})", tableName, columns, values))

        Catch fail As Exception

            Console.WriteLine(fail.Message)

            returnCode = False

        End Try

        Return returnCode

    End Function

    Public Function tableExists(tableName As String) As Boolean


        Return (GetDataTable("SELECT name FROM sqlite_master WHERE type='table' AND name='" & tableName & "';").Rows.Count = 1)


    End Function

    ''' <summary>
    ''' Creates a table in the database.
    ''' </summary>
    ''' <param name="tableName">The name of the table</param>
    ''' <param name="columnsAndTypes">The key is the column name and the value is the column data type</param>
    ''' <remarks></remarks>
    Public Sub createTable(tableName As String, columnsAndTypes As Dictionary(Of String, String))

        Dim query As String = "CREATE TABLE " & tableName & "("

        For Each column As KeyValuePair(Of String, String) In columnsAndTypes

            query += column.Key & " " & column.Value & ","

        Next

        ' Remove last comma (,) in the query
        query = query.Substring(0, query.Length - 1) & ");"

        ExecuteNonQuery(query)

    End Sub


    Public Function ClearDB() As Boolean

        Dim tables As DataTable

        Try

            tables = Me.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME")

            For Each table As DataRow In tables.Rows

                Me.ExecuteNonQuery("DROP TABLE " & table("NAME"))

            Next

            Return True
        Catch

            Return False
        End Try

    End Function

    Public Function ClearTable(table As String) As Boolean

        Try

            Me.ExecuteNonQuery(String.Format("delete from {0}", table))

            Return True

        Catch

            Return False

        End Try

    End Function

    Public Sub printTable(tableName As String)

        Console.WriteLine()

        Dim tableData = GetDataTable("SELECT * FROM " & tableName)
        Dim columns = tableData.Columns
        Dim rows = tableData.Rows

        Console.Write(" -- ")

        For Each column As DataColumn In columns

            Console.Write(column.ColumnName & " -- ")

        Next

        Console.WriteLine()

        For Each row As DataRow In rows

            For Each column As DataColumn In columns

                Console.Write(row(column.ColumnName) & "  ")

            Next

            Console.WriteLine()
        Next

        Console.WriteLine()

    End Sub


    ''' <summary>
    ''' Prevents SQL injection
    ''' </summary>
    ''' <param name="sqlValue">The SQL value to escape</param>
    ''' <returns>The injection safe SQL value</returns>
    ''' <remarks>
    ''' Solution to Bug Iq-4 (in the zoho project) 
    ''' </remarks>
    Public Function preventSQLInjection(sqlValue As String) As String
        Return sqlValue.Replace("'", "''")
    End Function

End Class
