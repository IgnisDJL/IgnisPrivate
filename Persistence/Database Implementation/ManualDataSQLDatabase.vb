Imports IGNIS.Constants.Database.ManualDataDB

Public Class ManualDataSQLDatabase
    Inherits ManualDataPersistence

    ' Constants
    Private Shared ReadOnly INVALID_VALUE As String = "!invalid!"
    Private Shared ReadOnly UNKNOWN_VALUE As String = "?unknown?"

    Private dataBase As SQLiteAdapter

    Public Sub New(dataBase As SQLiteAdapter)

        Me.dataBase = dataBase

    End Sub

    Public Overrides Function addData(data As ManualData) As ManualData

        Dim row As New Dictionary(Of String, String)
        row.Add(Columns.DATE_, data.DATE_.ToString(Constants.Database.SQL.DATE_FORMAT))
        row.Add(Columns.PRODUCTION_START_TIME, data.PRODUCTION_START_TIME.ToString(Constants.Database.SQL.TIME_FORMAT))
        row.Add(Columns.PRODUCTION_END_TIME, data.PRODUCTION_END_TIME.ToString(Constants.Database.SQL.TIME_FORMAT))
        row.Add(Columns.PRODUCED_QUANTITY, getQuantityForDB(data.PRODUCED_QUANTITY))

        row.Add(Columns.OPERATORS_FIRST_NAME, dataBase.preventSQLInjection(data.FACTORY_OPERATOR.FirstName))
        row.Add(Columns.OPERATORS_LAST_NAME, dataBase.preventSQLInjection(data.FACTORY_OPERATOR.LastName))

        row.Add(Columns.OPERATION_START_TIME, data.OPERATION_START_TIME.ToString(Constants.Database.SQL.TIME_FORMAT))
        row.Add(Columns.OPERATION_END_TIME, data.OPERATION_END_TIME.ToString(Constants.Database.SQL.TIME_FORMAT))

        row.Add(Columns.SILO_QUANTITY_AT_START, getQuantityForDB(data.SILO_QUANTITY_AT_START))
        row.Add(Columns.SILO_QUANTITY_AT_END, getQuantityForDB(data.SILO_QUANTITY_AT_END))

        row.Add(Columns.REJECTED_MIX_QUANTITY, getQuantityForDB(data.REJECTED_MIX_QUANTITY))
        row.Add(Columns.REJECTED_AGGREGATES_QUANTITY, getQuantityForDB(data.REJECTED_AGGREGATES_QUANTITY))
        row.Add(Columns.REJECTED_FILLER_QUANTITY, getQuantityForDB(data.REJECTED_FILLER_QUANTITY))
        row.Add(Columns.REJECTED_RECYCLED_QUANTITY, getQuantityForDB(data.REJECTED_RECYCLED_QUANTITY))

        row.Add(Columns.WEIGHTED_QUANTITY, getQuantityForDB(data.WEIGHTED_QUANTITY))
        row.Add(Columns.FIRST_LOADING_TIME, data.FIRST_LOADING_TIME.ToString(Constants.Database.SQL.TIME_FORMAT))
        row.Add(Columns.LAST_LOADING_TIME, data.LAST_LOADING_TIME.ToString(Constants.Database.SQL.TIME_FORMAT))

        row.Add(Columns.FUEL_QUANTITY_AT_START_1, getQuantityForDB(data.FUEL_QUANTITY_AT_START_1))
        row.Add(Columns.FUEL_QUANTITY_AT_END_1, getQuantityForDB(data.FUEL_QUANTITY_AT_END_1))
        row.Add(Columns.FUEL_QUANTITY_AT_START_2, getQuantityForDB(data.FUEL_QUANTITY_AT_START_2))
        row.Add(Columns.FUEL_QUANTITY_AT_END_2, getQuantityForDB(data.FUEL_QUANTITY_AT_END_2))

        row.Add(Columns.DRUMS_HOURS_COUNTER_AT_START, getQuantityForDB(data.DRUMS_HOURS_COUNTER_AT_START))
        row.Add(Columns.DRUMS_HOURS_COUNTER_AT_END, getQuantityForDB(data.DRUMS_HOURS_COUNTER_AT_END))
        row.Add(Columns.BOILERS_HOUR_COUNTER_AT_START, getQuantityForDB(data.BOILERS_HOUR_COUNTER_AT_START))
        row.Add(Columns.BOILERS_HOUR_COUNTER_AT_END, getQuantityForDB(data.BOILERS_HOUR_COUNTER_AT_END))

        If (CInt(dataBase.ExecuteScalar("SELECT COUNT(*) FROM " & TableNames.MANUAL_DATA & " WHERE " & Columns.DATE_ & "='" & data.DATE_.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")) > 0) Then
            dataBase.Update(TableNames.MANUAL_DATA, row, Columns.DATE_ & "='" & data.DATE_.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")
        Else
            dataBase.Insert(TableNames.MANUAL_DATA, row)
        End If

        Return data
    End Function

    Public Overrides Function getData(day As Date) As ManualData

        Dim dataTable As DataTable = dataBase.GetDataTable("SELECT * FROM " & TableNames.MANUAL_DATA & " WHERE " & Columns.DATE_ & "='" & day.ToString(Constants.Database.SQL.DATE_FORMAT) & "'")

        If (dataTable.Rows.Count > 0) Then

            Return New ManualData(Date.Parse(dataTable(0)(Columns.DATE_)), _
                                  Date.Parse(dataTable(0)(Columns.PRODUCTION_START_TIME)), _
                                  Date.Parse(dataTable(0)(Columns.PRODUCTION_END_TIME)), _
                                  Double.Parse(dataTable(0)(Columns.PRODUCED_QUANTITY)), _
                                  New FactoryOperator(dataTable(0)(Columns.OPERATORS_FIRST_NAME), dataTable(0)(Columns.OPERATORS_LAST_NAME)), _
                                  Date.Parse(dataTable(0)(Columns.OPERATION_START_TIME)), _
                                  Date.Parse(dataTable(0)(Columns.OPERATION_END_TIME)), _
                                  getQuantityFromDB(dataTable(0)(Columns.SILO_QUANTITY_AT_START)), _
                                  getQuantityFromDB(dataTable(0)(Columns.SILO_QUANTITY_AT_END)), _
                                  getQuantityFromDB(dataTable(0)(Columns.REJECTED_MIX_QUANTITY)), _
                                  getQuantityFromDB(dataTable(0)(Columns.REJECTED_AGGREGATES_QUANTITY)), _
                                  getQuantityFromDB(dataTable(0)(Columns.REJECTED_FILLER_QUANTITY)), _
                                  getQuantityFromDB(dataTable(0)(Columns.REJECTED_RECYCLED_QUANTITY)), _
                                  getQuantityFromDB(dataTable(0)(Columns.WEIGHTED_QUANTITY)), _
                                  Date.Parse(dataTable(0)(Columns.FIRST_LOADING_TIME)), _
                                  Date.Parse(dataTable(0)(Columns.LAST_LOADING_TIME)), _
                                  getQuantityFromDB(dataTable(0)(Columns.FUEL_QUANTITY_AT_START_1)), _
                                  getQuantityFromDB(dataTable(0)(Columns.FUEL_QUANTITY_AT_END_1)), _
                                  getQuantityFromDB(dataTable(0)(Columns.FUEL_QUANTITY_AT_START_2)), _
                                  getQuantityFromDB(dataTable(0)(Columns.FUEL_QUANTITY_AT_END_2)), _
                                  getQuantityFromDB(dataTable(0)(Columns.DRUMS_HOURS_COUNTER_AT_START)), _
                                  getQuantityFromDB(dataTable(0)(Columns.DRUMS_HOURS_COUNTER_AT_END)), _
                                  getQuantityFromDB(dataTable(0)(Columns.BOILERS_HOUR_COUNTER_AT_START)), _
                                  getQuantityFromDB(dataTable(0)(Columns.BOILERS_HOUR_COUNTER_AT_END)))

        Else

            ' #exception - no manual data for that day
            Return Nothing
        End If

    End Function

    Public Overrides Sub reset()

        Me.dataBase.ClearTable(TableNames.MANUAL_DATA)

        Me.verifyFormat()

    End Sub

    Public Overrides Function verifyFormat() As Boolean

        Try

            ' #refactor - Extract method
            Dim tableColumns As Dictionary(Of String, String)
            ' Check database format for csv files table
            If (Not dataBase.tableExists(TableNames.MANUAL_DATA)) Then

                tableColumns = New Dictionary(Of String, String)
                tableColumns.Add(Columns.DATE_, "DATE") ' #refactor - Extract constant
                tableColumns.Add(Columns.PRODUCTION_START_TIME, "DATE")
                tableColumns.Add(Columns.PRODUCTION_END_TIME, "DATE")
                tableColumns.Add(Columns.PRODUCED_QUANTITY, "VARCHAR(10)")
                tableColumns.Add(Columns.OPERATORS_FIRST_NAME, "VARCHAR(10)")
                tableColumns.Add(Columns.OPERATORS_LAST_NAME, "VARCHAR(10)")
                tableColumns.Add(Columns.OPERATION_START_TIME, "DATE")
                tableColumns.Add(Columns.OPERATION_END_TIME, "DATE")
                tableColumns.Add(Columns.SILO_QUANTITY_AT_START, "VARCHAR(10)")
                tableColumns.Add(Columns.SILO_QUANTITY_AT_END, "VARCHAR(10)")
                tableColumns.Add(Columns.REJECTED_MIX_QUANTITY, "VARCHAR(10)")
                tableColumns.Add(Columns.REJECTED_AGGREGATES_QUANTITY, "VARCHAR(10)")
                tableColumns.Add(Columns.REJECTED_FILLER_QUANTITY, "VARCHAR(10)")
                tableColumns.Add(Columns.REJECTED_RECYCLED_QUANTITY, "VARCHAR(10)")
                tableColumns.Add(Columns.WEIGHTED_QUANTITY, "VARCHAR(10)")
                tableColumns.Add(Columns.FIRST_LOADING_TIME, "DATE")
                tableColumns.Add(Columns.LAST_LOADING_TIME, "DATE")
                tableColumns.Add(Columns.FUEL_QUANTITY_AT_START_1, "VARCHAR(10)")
                tableColumns.Add(Columns.FUEL_QUANTITY_AT_END_1, "VARCHAR(10)")
                tableColumns.Add(Columns.FUEL_QUANTITY_AT_START_2, "VARCHAR(10)")
                tableColumns.Add(Columns.FUEL_QUANTITY_AT_END_2, "VARCHAR(10)")
                tableColumns.Add(Columns.DRUMS_HOURS_COUNTER_AT_START, "VARCHAR(10)")
                tableColumns.Add(Columns.DRUMS_HOURS_COUNTER_AT_END, "VARCHAR(10)")
                tableColumns.Add(Columns.BOILERS_HOUR_COUNTER_AT_START, "VARCHAR(10)")
                tableColumns.Add(Columns.BOILERS_HOUR_COUNTER_AT_END, "VARCHAR(10)")

                dataBase.createTable(TableNames.MANUAL_DATA, tableColumns)

                Console.WriteLine(TableNames.MANUAL_DATA & " table was created : ")
                Me.dataBase.printTable(TableNames.MANUAL_DATA)
            End If


        Catch ex As Exception

            ' #exception
            Console.WriteLine(ex.Message)

            Return False

        End Try

        Return True

    End Function

    Private Shared Function getQuantityFromDB(valueFromDB As String) As Double

        If (valueFromDB.Equals(UNKNOWN_VALUE)) Then
            Return ManualData.UNKNOWN_QUANTITY
        ElseIf (valueFromDB.Equals(INVALID_VALUE)) Then
            Return ManualData.INVALID_QUANTITY
        Else
            Return Double.Parse(valueFromDB)
        End If
    End Function

    Private Shared Function getQuantityForDB(valueFromData As Double) As String

        If (valueFromData.Equals(ManualData.UNKNOWN_QUANTITY)) Then
            Return UNKNOWN_VALUE
        ElseIf (valueFromData.Equals(ManualData.INVALID_QUANTITY)) Then
            Return INVALID_VALUE
        Else
            Return valueFromData.ToString
        End If
    End Function

End Class
