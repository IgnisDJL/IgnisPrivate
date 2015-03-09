Imports System.Data.OleDb

Public Class SourceFileMarcotteAdapter
    Inherits SourceFileAdapter

    Private coldFeederList As Dictionary(Of String, List(Of List(Of String)))
    Private hotFeederList As Dictionary(Of String, List(Of List(Of String)))

    Public Sub New()

    End Sub

    ''***********************************************************************************************************************
    ''  Fonction private unique au type de fichier source
    ''  Fonction qui effectu une oppération de formatage ou d'affichage du fichier source
    ''***********************************************************************************************************************

    Private Function getColdFeederList(indexCycle As Integer, sourceFile As SourceFile) As List(Of List(Of String))

        If (IsNothing(coldFeederList)) Then

            coldFeederList = New Dictionary(Of String, List(Of List(Of String)))

            OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

            Dim query = "SELECT " + ImportConstantEn_mdb.cycleCycleID + ", " +
            sourceFile.importConstant.coldFeederID + ", " + sourceFile.importConstant.coldFeederMaterialID + ", " +
            sourceFile.importConstant.coldFeederTargetPercentage + ", " + sourceFile.importConstant.coldFeederActualPercentage + ", " +
            sourceFile.importConstant.coldFeederDebit + ", " + sourceFile.importConstant.coldFeederMass + ", " + sourceFile.importConstant.coldFeederMoisturePercentage + ", " +
            ImportConstantEn_mdb.commandeCommandeID +
            " FROM ((((( " + ImportConstantEn_mdb.tableColdFeedsRecipesDetails +
            " INNER JOIN " + ImportConstantEn_mdb.tableColdFeedsRecipes + " ON " + ImportConstantEn_mdb.coldFeedsRecipesDetailsRecipeID + " = " + ImportConstantEn_mdb.coldFeedsRecipesRecipeID +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableRecette + " ON " + ImportConstantEn_mdb.coldFeedsRecipesRecipeID + " = " + ImportConstantEn_mdb.recetteColdFeedRecipeID +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableStringCache + " ON " + ImportConstantEn_mdb.recetteNom + " = " + ImportConstantEn_mdb.stringCacheStr +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableCommande + " ON " + ImportConstantEn_mdb.stringCacheStringCacheID + " = " + ImportConstantEn_mdb.commandeNomFormuleID +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.commandeCommandeID + " = " + ImportConstantEn_mdb.cycleCommandeID +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableMateriau + " ON " + ImportConstantEn_mdb.coldFeedsRecipesDetailsMateriauID + " = " + ImportConstantEn_mdb.materiauMateriauID +
            " WHERE " + ImportConstantEn_mdb.cycleDate + " BETWEEN  CDate('" + sourceFile.Date_() + "') AND CDate('" + (sourceFile.Date_().AddDays(1)) + "')" +
            " AND " + ImportConstantEn_mdb.materiauTypeID + " <> " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader
            Try

                While (mdbListDate.Read)

                    If coldFeederList.Keys.Contains(mdbListDate(0)) Then

                        Dim coldFeeder = New List(Of String)

                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederID + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederMaterialID + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederTargetPercentage + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederActualPercentage + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederDebit + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederMass + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederMoisturePercentage + 1))

                        coldFeederList.Item(mdbListDate(0)).Add(coldFeeder)

                    Else
                        Dim coldFeederListForCycle = New List(Of List(Of String))
                        Dim coldFeeder = New List(Of String)

                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederID + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederMaterialID + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederTargetPercentage + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederActualPercentage + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederDebit + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederMass + 1))
                        coldFeeder.Add(mdbListDate(EnumMDB.coldFeederMoisturePercentage + 1))

                        coldFeederListForCycle.Add(coldFeeder)

                        coldFeederList.Add(mdbListDate(0), coldFeederListForCycle)
                    End If

                End While
            Catch ex As Exception
                Return Nothing
            End Try
            dbCommand.Dispose()
            mdbListDate.Close()

            If coldFeederList.Keys.Contains(getCycle(indexCycle, sourceFile)) Then
                Return coldFeederList.Item(getCycle(indexCycle, sourceFile))
            Else
                Return New List(Of List(Of String))
            End If


            Return coldFeederList.Item(getCycle(indexCycle, sourceFile))
        Else

            If coldFeederList.Keys.Contains(getCycle(indexCycle, sourceFile)) Then
                Return coldFeederList.Item(getCycle(indexCycle, sourceFile))
            Else
                Return New List(Of List(Of String))
            End If

        End If
    End Function


    Private Function getHotFeederList(indexCycle As Integer, sourceFile As SourceFile) As List(Of List(Of String))

        If (IsNothing(hotFeederList)) Then

            hotFeederList = New Dictionary(Of String, List(Of List(Of String)))

            OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

            Dim query = "SELECT " + ImportConstantEn_mdb.cycleCycleID + ", " + sourceFile.importConstant.hotFeederID + ", " + sourceFile.importConstant.hotFeederMaterialID + ", " +
            sourceFile.importConstant.hotFeederTargetPercentage + ", " + sourceFile.importConstant.hotFeederActualPercentage + ", " +
            sourceFile.importConstant.hotFeederDebit + ", " + sourceFile.importConstant.hotFeederMass +
            " FROM (((( " + ImportConstantEn_mdb.tableCycleDetails +
            " INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.cycleCycleID + " = " + ImportConstantEn_mdb.detailsCycleID +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableEmplacement + " ON " + ImportConstantEn_mdb.detailsEmplacement + " = " + ImportConstantEn_mdb.emplacementNoEmplacment +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableStringCache1 + " ON " + ImportConstantEn_mdb.detailsNomMateriauID + " = " + ImportConstantEn_mdb.stringCacheStringCacheID1 +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableStringCache2 + " ON " + ImportConstantEn_mdb.detailsNoSerieID + " = " + ImportConstantEn_mdb.stringCacheStringCacheID2 +
            " ) INNER JOIN " + ImportConstantEn_mdb.tableMateriau + " ON " + ImportConstantEn_mdb.stringCacheStr1 + " = " + ImportConstantEn_mdb.materiauNom +
            " AND " + ImportConstantEn_mdb.stringCacheStr2 + " = " + ImportConstantEn_mdb.materiauNoSerie +
            " WHERE " + ImportConstantEn_mdb.cycleDate + " BETWEEN  CDate('" + sourceFile.Date_() + "') AND CDate('" + (sourceFile.Date_().AddDays(1)) + "')" +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " <> " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            Try
                While (mdbListDate.Read)

                    If hotFeederList.Keys.Contains(mdbListDate(0)) Then

                        Dim hotFeeder = New List(Of String)

                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederID + 1))

                        If IsDBNull(mdbListDate(EnumMDB.hotFeederMaterialID + 1)) Then
                            hotFeeder.Add("-3")
                        Else
                            hotFeeder.Add(mdbListDate(EnumMDB.hotFeederMaterialID + 1))
                        End If

                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederTargetPercentage + 1))
                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederActualPercentage + 1))
                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederDebit + 1))
                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederMass + 1))

                        hotFeederList.Item(mdbListDate(0)).Add(hotFeeder)
                    Else
                        Dim hotFeederListForCycle = New List(Of List(Of String))
                        Dim hotFeeder = New List(Of String)

                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederID + 1))

                        If IsDBNull(mdbListDate(EnumMDB.hotFeederMaterialID + 1)) Then
                            hotFeeder.Add("-3")
                        Else
                            hotFeeder.Add(mdbListDate(EnumMDB.hotFeederMaterialID + 1))
                        End If


                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederTargetPercentage + 1))
                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederActualPercentage + 1))
                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederDebit + 1))
                        hotFeeder.Add(mdbListDate(EnumMDB.hotFeederMass + 1))

                        hotFeederListForCycle.Add(hotFeeder)

                        hotFeederList.Add(mdbListDate(0), hotFeederListForCycle)
                    End If

                End While

            Catch ex As Exception
                Return Nothing
            End Try

            dbCommand.Dispose()
            mdbListDate.Close()

            If hotFeederList.Keys.Contains(getCycle(indexCycle, sourceFile)) Then
                Return hotFeederList.Item(getCycle(indexCycle, sourceFile))
            Else
                Return New List(Of List(Of String))
            End If


            Return hotFeederList.Item(getCycle(indexCycle, sourceFile))
        Else

            If hotFeederList.Keys.Contains(getCycle(indexCycle, sourceFile)) Then
                Return hotFeederList.Item(getCycle(indexCycle, sourceFile))
            Else
                Return New List(Of List(Of String))
            End If

        End If
    End Function

    Private Function getColdFeeder(indexCycle As Integer, sourceFile As SourceFile) As List(Of List(Of String))
        Return getColdFeederList(indexCycle, sourceFile)
    End Function

    Private Function getHotFeeder(indexCycle As Integer, sourceFile As SourceFile) As List(Of List(Of String))
        Return getHotFeederList(indexCycle, sourceFile)
    End Function

    Protected Overrides Function getCycleList(sourceFile As SourceFile) As List(Of String)

        If (IsNothing(cycleList)) Then

            cycleList = New List(Of String)

            OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

            Dim query = "SELECT " + ImportConstantEn_mdb.cycleCycleID + " FROM " + ImportConstantEn_mdb.tableCycle +
            " WHERE " + ImportConstantEn_mdb.cycleDate + " BETWEEN  CDate('" + sourceFile.Date_() + "') AND CDate('" + (sourceFile.Date_().AddDays(1)) + "')"

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            While (mdbListDate.Read)
                cycleList.Add(mdbListDate(0))
            End While
            dbCommand.Dispose()
            mdbListDate.Close()
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
        sourceFile.importConstant = New ImportConstantEn_mdb
    End Sub


    ''***********************************************************************************************************************
    ''  Fonction publique générique a tout les adapteurs
    ''  Fonction qui récupère une donnée du fichier source, ou qui calcule une donnée avec d'autre donnée source
    ''  Ces fonctions permettent de générer les objets du modèle du programme
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionDay
    ''***********************************************************************************************************************

    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Return sourceFile.Date_
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionCycle
    ''***********************************************************************************************************************



    Public Overrides Function getManuelle(indexCycle As Integer, sourceFile As SourceFile) As Boolean
        Dim manuelle As Boolean = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try
            Dim query = "SELECT " + sourceFile.importConstant.manuel + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
                " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile)


            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            manuelle = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(manuelle), "-1", manuelle)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getDureeMalaxHumideCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dureeMalaxHumideCycle As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Dim query = "SELECT " + sourceFile.importConstant.dureeMalaxHumide + " FROM " + ImportConstantEn_mdb.tableCycle +
            " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)
        Try

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            dureeMalaxHumideCycle = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(dureeMalaxHumideCycle), "-1", dureeMalaxHumideCycle)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getDureeMalaxSecCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dureeMalaxSecCycle As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Dim query = "SELECT " + sourceFile.importConstant.dureeMalaxSec + " FROM " + ImportConstantEn_mdb.tableCycle +
            " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)
        Try

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            dureeMalaxSecCycle = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(dureeMalaxSecCycle), "-1", dureeMalaxSecCycle)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getDureeCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dureeCycle As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Dim query = "SELECT " + sourceFile.importConstant.dureeCycle + " FROM " + ImportConstantEn_mdb.tableCycle +
            " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)
        Try

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            dureeCycle = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(dureeCycle), "-1", dureeCycle)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        Dim time As Date

        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Dim query = "SELECT " + sourceFile.importConstant.time + " FROM " + ImportConstantEn_mdb.tableCycle + " WHERE " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

        Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
        Dim mdbListDate = dbCommand.ExecuteReader

        mdbListDate.Read()
        time = mdbListDate(0)
        mdbListDate.Close()
        dbCommand.Dispose()

        Return time
    End Function

    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function

    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim truckID As String = "-4"


        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Dim query = "SELECT " + sourceFile.importConstant.truckID + " FROM " + ImportConstantEn_mdb.tableCommande +
            " INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.cycleCommandeID + " = " + ImportConstantEn_mdb.commandeCommandeID +
            " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)
        Try

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            truckID = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(truckID), "-1", truckID)

        Catch ex As Exception
            Return "-2"
        End Try


    End Function

    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim contractID As String = "-4"

        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try

            Dim query = "SELECT " + sourceFile.importConstant.contractID + " FROM " + ImportConstantEn_mdb.tableCommande +
                " INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.commandeCommandeID + " = " + ImportConstantEn_mdb.cycleCommandeID +
                " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            contractID = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(contractID), "-1", contractID)

        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim siloFillingNumber As String = "-4"

        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try

            Dim query = "SELECT " + sourceFile.importConstant.siloFillingNumber + " FROM " + ImportConstantEn_mdb.tableCommande +
                " INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.commandeCommandeID + " = " + ImportConstantEn_mdb.cycleCommandeID +
                " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            siloFillingNumber = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(siloFillingNumber), "-1", siloFillingNumber)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les totaux d'un cycle de production 
    ''***********************************************************************************************************************

    '' Total asphalt
    Public Overrides Function getCycleAsphaltConcreteActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String

        Dim virginAsphaltActualPercentage As String = "-4"

        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try
            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteActualPercentage + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " = " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            virginAsphaltActualPercentage = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(virginAsphaltActualPercentage), "-1", virginAsphaltActualPercentage)

        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getCycleAsphaltConcreteDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function

    Public Overrides Function getCycleAsphaltConcreteMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltMass As String = "-4"

        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try

            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteMass + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " = " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            virginAsphaltMass = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(virginAsphaltMass), "-1", virginAsphaltMass)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltTargetPercentage As String
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try

            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteTargetPercentage + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " = " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            virginAsphaltTargetPercentage = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(virginAsphaltTargetPercentage), "-1", virginAsphaltTargetPercentage)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''TotalAggregate

    '' Information non récupéré pour ce fichier source
    Public Overrides Function getCycleAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-6"
    End Function

    '' Information non récupéré pour ce fichier source
    Public Overrides Function getCycleAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-6"
    End Function

    '' Information non récupéré pour ce fichier source
    Public Overrides Function getCycleAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-6"
    End Function

    '' Information non récupéré pour ce fichier source
    Public Overrides Function getCycleAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-6"
    End Function

    ''TotalMass
    Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalMass As Double = -4
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try
            Dim query = "SELECT sum(" + sourceFile.importConstant.totalMass + "), " + ImportConstantEn_mdb.detailsCycleID +
            " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " Group By " + ImportConstantEn_mdb.detailsCycleID

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            totalMass = mdbListDate(0)

            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(totalMass.ToString), "-1", totalMass.ToString)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les données liées au bitume utilisé dans un cycle 
    ''***********************************************************************************************************************

    Public Overrides Function getCycleAsphaltConcreteDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteDensity As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Try
            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteDensity + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " = " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            virginAsphaltConcreteDensity = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(virginAsphaltConcreteDensity), "-1", virginAsphaltConcreteDensity)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltRecordedTemperature As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Try
            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteRecordedTemperature + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " = " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            asphaltRecordedTemperature = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(asphaltRecordedTemperature), "-1", asphaltRecordedTemperature)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try

            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteTankId + " FROM " + ImportConstantEn_mdb.tableCycleDetails +
            " Where " + ImportConstantEn_mdb.detailsCycleID + " = " + getCycle(indexCycle, sourceFile) +
            " AND " + ImportConstantEn_mdb.detailsTypeID + " = " + ImportConstantEn_mdb.typeAsphalt

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            asphaltTankId = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()
            Return If(String.IsNullOrEmpty(asphaltTankId), "-1", asphaltTankId)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getCycleAsphaltConcreteGrade(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteGrade As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)
        Try

            Dim query = "SELECT " + sourceFile.importConstant.virginAsphaltConcreteGrade +
            " FROM (" + ImportConstantEn_mdb.tableCycle +
            " INNER JOIN " + ImportConstantEn_mdb.tableCommande + " ON " + ImportConstantEn_mdb.commandeCommandeID + " = " + ImportConstantEn_mdb.cycleCommandeID +
            " )INNER JOIN " + ImportConstantEn_mdb.tableMateriau + " ON " + ImportConstantEn_mdb.materiauMateriauID + " = " + ImportConstantEn_mdb.commandeNewBitumeID +
            " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            virginAsphaltConcreteGrade = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()
            Return If(String.IsNullOrEmpty(virginAsphaltConcreteGrade), "-1", virginAsphaltConcreteGrade)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle
    ''***********************************************************************************************************************
    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Try

            Dim query = "SELECT " + sourceFile.importConstant.mixNumber + " FROM (" + ImportConstantEn_mdb.tableStringCache +
                " INNER JOIN " + ImportConstantEn_mdb.tableCommande + " ON " + ImportConstantEn_mdb.commandeNomFormuleID + " = " + ImportConstantEn_mdb.stringCacheStringCacheID +
                ") INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.commandeCommandeID + " = " + ImportConstantEn_mdb.cycleCommandeID +
                " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            mixNumber = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()
            Return If(String.IsNullOrEmpty(mixNumber), "-1", mixNumber)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixName As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Try

            Dim query = "SELECT " + sourceFile.importConstant.mixName + " FROM (" + ImportConstantEn_mdb.tableStringCache +
                " INNER JOIN " + ImportConstantEn_mdb.tableCommande + " ON " + ImportConstantEn_mdb.commandeDescriptionFormuleID + " = " + ImportConstantEn_mdb.stringCacheStringCacheID +
                ") INNER JOIN " + ImportConstantEn_mdb.tableCycle + " ON " + ImportConstantEn_mdb.commandeCommandeID + " = " + ImportConstantEn_mdb.cycleCommandeID +
                " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            mixName = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()

            Return If(String.IsNullOrEmpty(mixName), "-1", mixName)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixRecordedTemperature As String = "-4"
        OleDBAdapter.initialize(sourceFile.getFileInfo.FullName)

        Try

            Dim query = "SELECT " + sourceFile.importConstant.mixRecordedTemperature + " FROM " + ImportConstantEn_mdb.tableCycle +
            " Where " + ImportConstantEn_mdb.cycleCycleID + " = " + getCycle(indexCycle, sourceFile)

            Dim dbCommand = New System.Data.OleDb.OleDbCommand(query, OleDBAdapter.MDB_CONNECTION)
            Dim mdbListDate = dbCommand.ExecuteReader

            mdbListDate.Read()
            mixRecordedTemperature = mdbListDate(0)
            dbCommand.Dispose()
            mdbListDate.Close()
            Return If(String.IsNullOrEmpty(mixRecordedTemperature), "-1", mixRecordedTemperature)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************
    Public Overrides Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
        Return getColdFeeder(indexCycle, sourceFile).Count
    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederID As String = "-4"

        Try
            coldFeederID = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederID)

            Return If(String.IsNullOrEmpty(coldFeederID), "-1", coldFeederID)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederTargetPercentaged As String = "-4"

        Try
            coldFeederTargetPercentaged = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederTargetPercentage)
            Return If(String.IsNullOrEmpty(coldFeederTargetPercentaged), "-1", coldFeederTargetPercentaged)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederActualPercentage As String = "-4"

        Try
            coldFeederActualPercentage = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederActualPercentage)
            Return If(String.IsNullOrEmpty(coldFeederActualPercentage), "-1", coldFeederActualPercentage)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederMaterialID As String = "-4"

        Try
            coldFeederMaterialID = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederMaterialID)
            Return If(String.IsNullOrEmpty(coldFeederMaterialID), "-1", coldFeederMaterialID)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim ColdFeederDebit As String = "-4"

        Try
            ColdFeederDebit = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederDebit)
            Return If(String.IsNullOrEmpty(ColdFeederDebit), "-1", ColdFeederDebit)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederMass As String = "-4"

        Try
            coldFeederMass = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederMass)
            Return If(String.IsNullOrEmpty(coldFeederMass), "-1", coldFeederMass)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederMoisturePercentage As String = "-4"

        Try
            coldFeederMoisturePercentage = getColdFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.coldFeederMoisturePercentage)
            Return If(String.IsNullOrEmpty(coldFeederMoisturePercentage), "-1", coldFeederMoisturePercentage)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function
    '' Information non disponible dans la base de donnée Marcotte
    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les Bennes chaudes d'un cycle
    ''***********************************************************************************************************************

    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
        Dim HotFeederCountForCycle As String = "-4"
        Try
            HotFeederCountForCycle = getHotFeeder(indexCycle, sourceFile).Count
            Return If(String.IsNullOrEmpty(HotFeederCountForCycle), "-1", HotFeederCountForCycle)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederID As String = "-4"

        Try
            hotFeederID = getHotFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.hotFeederID)
            Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederTargetPercentage As String = "-4"

        Try
            hotFeederTargetPercentage = getHotFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.hotFeederTargetPercentage)
            Return If(String.IsNullOrEmpty(hotFeederTargetPercentage), "-1", hotFeederTargetPercentage)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederActualPercentage As String = "-4"

        Try
            hotFeederActualPercentage = getHotFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.hotFeederActualPercentage)
            Return If(String.IsNullOrEmpty(hotFeederActualPercentage), "-1", hotFeederActualPercentage)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederDebit As String = "-4"

        Try
            hotFeederDebit = getHotFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.hotFeederDebit)
            Return If(String.IsNullOrEmpty(hotFeederDebit), "-1", hotFeederDebit)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederMass As String = "-4"

        Try
            hotFeederMass = getHotFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.hotFeederMass)
            Return If(String.IsNullOrEmpty(hotFeederMass), "-1", hotFeederMass)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederMaterialID As String = "-4"

        Try
            hotFeederMaterialID = getHotFeeder(indexCycle, sourceFile).Item(indexFeeder).Item(EnumMDB.hotFeederMaterialID)
            Return If(String.IsNullOrEmpty(hotFeederMaterialID), "-1", hotFeederMaterialID)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

End Class

