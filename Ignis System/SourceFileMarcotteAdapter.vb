Imports System.Data.OleDb

Public Class SourceFileMarcotteAdapter
    Inherits SourceFileAdapter

    Private connection As System.Data.OleDb.OleDbConnection

    Public Sub New(sourceFilePath As String)
        connection = New System.Data.OleDb.OleDbConnection(Constants.Input.MDB.CONNECTION_STRING + "Data Source=" & sourceFilePath + ";")
        connection.Open()
    End Sub

    Protected Overrides Sub Finalize()
        connection.Close()
    End Sub

    Public Function getNouvellesDates(derniereDate As Date) As List(Of Date)

        Dim query = "SELECT distinct DateValue(Date)  AS NewDate FROM Cycle WHERE ( Date >= " + "#" + derniereDate.Year.ToString + "/" + derniereDate.Month.ToString + "/" + (derniereDate.Day + 1).ToString + "#)"

        Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, connection)
        Dim mdbListDate = dbCommand.ExecuteReader

        Dim nouvellesDates = New List(Of Date)

        While (mdbListDate.Read)

            nouvellesDates.Add(mdbListDate(0))
        End While
        Return nouvellesDates
    End Function


    ''***********************************************************************************************************************
    ''  Fonction private unique au type de fichier source
    ''  Fonction qui effectu une oppération de formatage ou d'affichage du fichier source
    ''
    ''***********************************************************************************************************************
    Protected Overrides Function getCycleList(sourceFile As SourceFile) As List(Of String)

        If (IsNothing(cycleList)) Then

            cycleList = New List(Of String)


            Dim query = "SELECT CycleID FROM Cycle WHERE ( Date >= " + "#" + sourceFile.Date_().Year.ToString + "/" + sourceFile.Date_().Month.ToString + "/" + (sourceFile.Date_().Day).ToString + "#)"

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, connection)
            Dim mdbListDate = dbCommand.ExecuteReader

            While (mdbListDate.Read)
                cycleList.Add(mdbListDate(0))
            End While


            Return cycleList
        Else

            Return cycleList

        End If


    End Function


    Protected Overrides Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Return getCycleList(sourceFile).ElementAt(indexCycle)
    End Function


    ''***********************************************************************************************************************
    ''  Fonction protected force l'adapteur a implémenté une fonction utile a la lecture du fichier source ou au formatage des donnée
    ''***********************************************************************************************************************

    Public Overrides Function getCycleCount(sourceFile As SourceFile) As Integer
        Return getCycleList(sourceFile).Count
    End Function


    Public Overrides Sub setImportConstantForLanguage(sourceFile As SourceFile)

    End Sub


    ''***********************************************************************************************************************
    ''  Fonction publique générique a tout les adapteurs
    ''  Fonction qui récupère une donnée du fichier source, ou qui calcule une donnée avec d'autre donnée source
    ''  Ces fonctions permettent de générer les objets du modèle du programme
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionCycle
    ''***********************************************************************************************************************

    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Return sourceFile.Date_
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionCycle
    ''***********************************************************************************************************************
    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        Dim time As Date

            Dim query = "SELECT FORMAT(date,'hh:nn:ss am/pm') FROM Cycle WHERE CycleID = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, connection)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()

            time = mdbListDate(0)

            Return time
    End Function

    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les totaux d'un cycle de production 
    ''***********************************************************************************************************************

    '' Total asphalt
    Public Overrides Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function


    ''TotalAggregate
    Public Overrides Function getTotalAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getTotalAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    ''TotalMass
    Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les données liées au bitume utilisé dans un cycle 
    ''***********************************************************************************************************************

    Public Overrides Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle
    ''***********************************************************************************************************************
    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************
    Public Overrides Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les Bennes chaudes d'un cycle
    ''***********************************************************************************************************************

    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

    End Function

    Public Overrides Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

    Public Overrides Function getHotFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

    End Function

End Class

