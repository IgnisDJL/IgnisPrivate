﻿Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input
Imports System.Globalization
Imports System.Text

Public Class SourceFileCSVAdapter
    Inherits SourceFileAdapter

    Private columnNameList As List(Of String)
    Private hotFeederCount As Integer = 0
    Public Sub New()

    End Sub

    ''***********************************************************************************************************************
    ''  Fonction private unique au type de fichier source
    ''  Fonction qui effectu une oppération de formatage ou d'affichage du fichier source
    ''
    ''***********************************************************************************************************************
    Private Function getColumnFromCSVFile(columnName As String, indexCycle As Integer, sourceFile As SourceFile) As String

        Dim index As Integer = getIndexForColumnName(columnName, sourceFile)

        Try
            If (Not index = -1) Then

                Return Regex.Split(getCycle(indexCycle, sourceFile), ","c)(index)
            Else
                Return ""

            End If
        Catch
            Return "-2"
        End Try
    End Function

    Private Function getIndexForColumnName(columnName As String, sourceFile As SourceFile) As Integer
        If (getColumnNameList(sourceFile).Contains(columnName)) Then
            Return columnNameList.IndexOf(columnName)
        Else
            Return -1
        End If
    End Function

    Private Sub setColumnNameList(sourceFile As SourceFile)

        Dim readingStream As System.IO.StreamReader = Nothing
        Dim stringFile As String = Nothing

        readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName, Encoding.GetEncoding(1252))
        stringFile = readingStream.ReadToEnd
        columnNameList = New List(Of String)

        For Each columnName As String In Split(Regex.Split(stringFile, "\r")(0), ",")
            If Not String.IsNullOrEmpty(columnName) Then
                columnNameList.Add(columnName.Trim)
            End If
        Next

    End Sub

    Private Function getColumnNameList(sourceFile As SourceFile) As List(Of String)
        If IsNothing(columnNameList) Then
            setColumnNameList(sourceFile)
            Return columnNameList
        Else
            Return columnNameList
        End If
    End Function
    ''***********************************************************************************************************************************
    ''  Fonction protected, force l'adapteur a implémenter une fonction utile a la lecture du fichier source ou au formatage des données
    ''***********************************************************************************************************************************
    Protected Overrides Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Return getCycleList(sourceFile).ElementAt(indexCycle)
    End Function

    Protected Overrides Function getCycleList(sourceFile As SourceFile) As List(Of String)

        If IsNothing(cycleList) Then
            Dim readingStream As System.IO.StreamReader = Nothing
            Dim stringFile As String = Nothing

            readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName, Encoding.GetEncoding(1252))
            stringFile = readingStream.ReadToEnd
            cycleList = New List(Of String)

            For Each cycle_1 As String In Regex.Split(stringFile, "\r")
                If Not String.IsNullOrEmpty(cycle_1) Then
                    cycleList.Add(cycle_1)
                End If
            Next

            cycleList.RemoveAt(0)
            cycleList.RemoveAt(cycleList.Count - 1)
            cycleList.RemoveAt(cycleList.Count - 1)
            Return cycleList
        Else
            Return cycleList
        End If

    End Function

    ''***********************************************************************************************************************
    ''  Fonction publique mais qui n'ont pas la responsabilié de retourner une informations directement au modèle du domaine
    ''
    ''*********************************************************************************************************************
    Public Overrides Sub setImportConstantForLanguage(sourceFile As SourceFile)

        If IsNothing(sourceFile.importConstant) Then
            Dim readingStream As System.IO.StreamReader = Nothing
            Dim stringFile As String = Nothing

            readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName)
            stringFile = readingStream.ReadToEnd

            If (getColumnNameList(sourceFile).Contains(ImportConstant_csv.asphaltTankId_En_csv)) Then
                sourceFile.importConstant = New ImportConstantEn_csv
            Else
                sourceFile.importConstant = New ImportConstantFr_csv
            End If
        End If
    End Sub

    Public Overrides Function getCycleCount(sourceFile As SourceFile) As Integer
        Return getCycleList(sourceFile).Count()
    End Function



    ''***********************************************************************************************************************
    ''  Fonction publique générique a tout les adapteurs
    ''  Fonction qui récupère une donnée du fichier source, ou qui calcule une donnée avec d'autre donnée source
    ''  Ces fonctions permettent de générer les objets du modèle du programme
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionDay
    ''***********************************************************************************************************************
    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Dim regex As New Regex(Constants.Input.CSV.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(sourceFile.getFileInfo.FullName)

        If (match.Success) Then

            Dim day As Integer = Integer.Parse(match.Groups(CSV.CSVFileNameRegexDateIndex.DAY).Value)
            Dim month As Integer = Integer.Parse(match.Groups(CSV.CSVFileNameRegexDateIndex.MONTH).Value)
            Dim year As Integer = Integer.Parse(match.Groups(CSV.CSVFileNameRegexDateIndex.YEAR).Value)

            Return New Date(year, month, day)

        Else

            ' #exception

            Return Nothing
        End If
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les totaux d'un cycle de production 
    ''***********************************************************************************************************************


    '' Aucune information dispobible sur ce paramètre dans le fichier source
    Public Overrides Function getManuelle(indexCycle As Integer, sourceFile As SourceFile) As Boolean
        Dim manuelle As Boolean = False
            Return manuelle
    End Function

    Public Overrides Function getDureeMalaxHumideCycle(indexCycle As Integer, sourceFile As SourceFile) As TimeSpan
        Dim dureeMalaxHumideCycle = New  TimeSpan 

        Try
            dureeMalaxHumideCycle = TimeSpan.FromSeconds(getColumnFromCSVFile(sourceFile.importConstant.dureeMalaxHumide, indexCycle, sourceFile))
            Return dureeMalaxHumideCycle
        Catch ex As Exception
            Return TimeSpan.Zero
        End Try
    End Function

    Public Overrides Function getDureeMalaxSecCycle(indexCycle As Integer, sourceFile As SourceFile) As TimeSpan
        Dim dureeMalaxSecCycle = New TimeSpan

        Try
            dureeMalaxSecCycle = TimeSpan.FromSeconds(getColumnFromCSVFile(sourceFile.importConstant.dureeCycle, indexCycle, sourceFile) - getColumnFromCSVFile(sourceFile.importConstant.dureeMalaxHumide, indexCycle, sourceFile))
            Return dureeMalaxSecCycle
        Catch ex As Exception
            Return TimeSpan.Zero
        End Try
    End Function

    Public Overrides Function getDureeCycle(indexCycle As Integer, sourceFile As SourceFile) As TimeSpan
        Dim dureeCycle = New TimeSpan

        Try
            dureeCycle = TimeSpan.FromSeconds(getColumnFromCSVFile(sourceFile.importConstant.dureeCycle, indexCycle, sourceFile))
            Return dureeCycle
        Catch ex As Exception
            Return TimeSpan.Zero
        End Try
    End Function


    ' ''Total Mass
    'Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String
    '    Dim totalMass As String = "-4"

    '    Try
    '        totalMass = getColumnFromCSVFile(sourceFile.importConstant.totalMass, indexCycle, sourceFile)
    '        Return If(String.IsNullOrEmpty(totalMass), "-1", totalMass)
    '    Catch ex As Exception
    '        Return "-2"
    '    End Try
    'End Function


    ''***********************************************************************************************************************
    ''  Section concernant les donnée liées un ProductionCycle 
    ''***********************************************************************************************************************

    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        Dim time As String

        Try
            time = getColumnFromCSVFile(sourceFile.importConstant.time, indexCycle, sourceFile)
            Return getDate(sourceFile) + " " + time
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim contractID As String = "-4"

        Try
            contractID = getColumnFromCSVFile(sourceFile.importConstant.contractID, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(contractID), "-1", contractID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim siloFillingNumber As String = "-4"

        Try
            siloFillingNumber = getColumnFromCSVFile(sourceFile.importConstant.siloFillingNumber, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(siloFillingNumber), "-1", siloFillingNumber)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim truckID As String = "-4"

        Try
            truckID = getColumnFromCSVFile(sourceFile.importConstant.truckID, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(truckID), "-1", truckID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' Cette information n'est pas disponible actuellement dans un csv
    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim bagHouseDiff As String = "-4"

        Try
            bagHouseDiff = sourceFile.importConstant.bagHouseDiff

            Return If(String.IsNullOrEmpty(bagHouseDiff), "-1", bagHouseDiff)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' Cette information n'est pas disponible actuellement dans un csv
    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dustRemovalDebit As String = "-4"
        Try
            dustRemovalDebit = sourceFile.importConstant.dustRemovalDebit

            Return If(String.IsNullOrEmpty(dustRemovalDebit), "-1", dustRemovalDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les données liées au bitume utilisé dans un cycle 
    ''**********************************************************************************************************************
    '' Cette information n'est pas disponible actuellement dans le fichier source des .csv
    '' Cette information est disponible dans une base de donnée en parrallele, dans une version ultérieur elle sera récupéré de la formule
    Public Overrides Function getVirginAsphaltConcreteTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteTargetPercentage As String = "-4"
        Try
            virginAsphaltConcreteTargetPercentage = sourceFile.importConstant.virginAsphaltConcreteTargetPercentage

            Return If(String.IsNullOrEmpty(virginAsphaltConcreteTargetPercentage), "-1", virginAsphaltConcreteTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltConcreteActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltActualPercentage As String = "-4"

        Try
            totalAsphaltActualPercentage = getColumnFromCSVFile(sourceFile.importConstant.virginAsphaltConcreteActualPercentage, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(totalAsphaltActualPercentage), "-1", totalAsphaltActualPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltConcreteDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteDebit As String = "-4"

        Try
            virginAsphaltConcreteDebit = sourceFile.importConstant.virginAsphaltConcreteDebit

            Return If(String.IsNullOrEmpty(virginAsphaltConcreteDebit), "-1", virginAsphaltConcreteDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltConcreteMass(indexCycle As Integer, sourceFile As SourceFile) As Double
        Dim totalAsphaltMass As Double = Double.NaN

        Try
            totalAsphaltMass = getColumnFromCSVFile(sourceFile.importConstant.virginAsphaltConcreteMass, indexCycle, sourceFile)

            'Conversion de la masse en kilograme en Tonnes
            ' TODO rendre la conversion plus propre
            totalAsphaltMass = totalAsphaltMass / 1000

            Return If(totalAsphaltMass < 0, Double.NaN, totalAsphaltMass)
        Catch ex As Exception
            Return Double.NaN
        End Try
    End Function

    Public Overrides Function getVirginAsphaltConcreteRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltRecordedTemperature As String = "-4"

        Try
            asphaltRecordedTemperature = getColumnFromCSVFile(sourceFile.importConstant.virginAsphaltConcreteRecordedTemperature, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(asphaltRecordedTemperature), "-1", asphaltRecordedTemperature)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltConcreteDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteDensity As String = "-4"
        Try
            virginAsphaltConcreteDensity = sourceFile.importConstant.virginAsphaltConcreteDensity

            Return If(String.IsNullOrEmpty(virginAsphaltConcreteDensity), "-1", virginAsphaltConcreteDensity)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getVirginAsphaltConcreteTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String = "-4"

        Try
            asphaltTankId = getColumnFromCSVFile(sourceFile.importConstant.virginAsphaltConcreteTankId, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(asphaltTankId), "-1", asphaltTankId)
        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getVirginAsphaltConcreteGrade(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteGrade As String = "-4"
        Try
            virginAsphaltConcreteGrade = sourceFile.importConstant.virginAsphaltConcreteGrade

            Return If(String.IsNullOrEmpty(virginAsphaltConcreteGrade), "-1", virginAsphaltConcreteGrade)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle
    ''***********************************************************************************************************************
    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String = "-4"

        Try
            mixNumber = getColumnFromCSVFile(sourceFile.importConstant.mixNumber, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(mixNumber), "-1", mixNumber)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' Cette information n'est pas disponible actuellement dans un csv
    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixName As String = "-4"
        Try
            mixName = sourceFile.importConstant.mixName
            Return If(String.IsNullOrEmpty(mixName), "-1", mixName)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixRecordedTemperature As String

        Try
            mixRecordedTemperature = getColumnFromCSVFile(sourceFile.importConstant.mixRecordedTemperature, indexCycle, sourceFile)
            Return If(String.IsNullOrEmpty(mixRecordedTemperature), "0", mixRecordedTemperature)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************
    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederActualPercentage As String = "-4"

        Try

            If (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederID + (indexFeeder + 1).ToString)) Then
                coldFeederActualPercentage = getColumnFromCSVFile(sourceFile.importConstant.coldFeederActualPercentage + (indexFeeder + 1).ToString, indexCycle, sourceFile)

            ElseIf (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederRecycledID + (indexFeeder + 1).ToString)) Then
                coldFeederActualPercentage = getColumnFromCSVFile(sourceFile.importConstant.coldFeederRecycledActualPercentage + (indexFeeder + 1).ToString, indexCycle, sourceFile)
            End If

            Return If(String.IsNullOrEmpty(coldFeederActualPercentage) Or coldFeederActualPercentage.Equals(""""""), "-1", coldFeederActualPercentage)

        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer
        Dim coldFeederCountForCycle As Integer

        For Each coldFeeder As String In getColumnNameList(sourceFile)
            If coldFeeder.Contains(sourceFile.importConstant.coldFeederID) Then

                coldFeederCountForCycle = coldFeederCountForCycle + 1

            ElseIf coldFeeder.Contains(sourceFile.importConstant.coldFeederRecycledID) Then

                coldFeederCountForCycle = coldFeederCountForCycle + 1

            End If
        Next

        Return coldFeederCountForCycle
    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederDebit As String = "-4"

        Try

            If (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederID + (indexFeeder + 1).ToString)) Then
                coldFeederDebit = sourceFile.importConstant.coldFeederDebit

            ElseIf (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederRecycledID + (indexFeeder + 1).ToString)) Then
                coldFeederDebit = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledDebit
            End If

            Return If(String.IsNullOrEmpty(coldFeederDebit) Or coldFeederDebit.Equals(""""""), "-1", coldFeederDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Dim coldFeederID As String = "-4"
        Try

            If (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederID + (indexFeeder + 1).ToString)))) Then
                coldFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederID + (indexFeeder + 1).ToString

            ElseIf (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledID + (getColdFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString.Trim)))) Then
                coldFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledID + (getColdFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString
            End If

            Return If(String.IsNullOrEmpty(coldFeederID) Or coldFeederID.Equals(""""""), "-1", coldFeederID.Trim)

        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As Double
        Dim coldFeederMass As Double = Double.NaN

        Try
            If (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederID + (indexFeeder + 1).ToString)) Then
                coldFeederMass = sourceFile.importConstant.coldFeederMass

            ElseIf (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederRecycledID + (indexFeeder + 1).ToString)) Then
                coldFeederMass = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledMass
            End If

            'Conversion de la masse en kilograme en Tonnes
            ' TODO rendre la conversion plus propre
            coldFeederMass = coldFeederMass / 1000

            Return If(coldFeederMass < 0, Double.NaN, coldFeederMass)
        Catch ex As Exception
            Return Double.NaN
        End Try
    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederMoisturePercentage As String = "-4"

        Try
            If (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederID + (indexFeeder + 1).ToString)) Then
                coldFeederMoisturePercentage = sourceFile.importConstant.coldFeederMoisturePercentage

            ElseIf (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederRecycledID + (indexFeeder + 1).ToString)) Then
                coldFeederMoisturePercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledMoisturePercentage
            End If

            Return If(String.IsNullOrEmpty(coldFeederMoisturePercentage) Or coldFeederMoisturePercentage.Equals(""""""), "-1", coldFeederMoisturePercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' Cette information n'est pas disponible actuellement dans un csv
    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederRecycledAsphaltPercentage As String = "-4"

        Try
            coldFeederRecycledAsphaltPercentage = "-3"

            Return If(String.IsNullOrEmpty(coldFeederRecycledAsphaltPercentage), "-1", coldFeederRecycledAsphaltPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' Cette information n'est pas disponible actuellement dans un csv
    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim coldFeederTargetPercentage As String = "-4"

        Try
            If (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederID + (indexFeeder + 1).ToString)) Then
                coldFeederTargetPercentage = sourceFile.importConstant.coldFeederTargetPercentage

            ElseIf (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederRecycledID + (indexFeeder + 1).ToString)) Then
                coldFeederTargetPercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledTargetPercentage

            End If
            Return If(String.IsNullOrEmpty(coldFeederTargetPercentage) Or coldFeederTargetPercentage.Equals(""""""), "-1", coldFeederTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim materialID As String = "-4"

        Try
            If (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederID + (indexFeeder + 1).ToString)) Then
                materialID = sourceFile.importConstant.coldFeederMaterialID

            ElseIf (getColdFeederID(indexFeeder, indexCycle, sourceFile).Equals(sourceFile.importConstant.coldFeederRecycledID + (indexFeeder + 1).ToString)) Then
                materialID = TryCast(sourceFile.importConstant, ImportConstant_csv).coldFeederRecycledMaterialID
            End If

            Return If(String.IsNullOrEmpty(materialID) Or materialID.Equals(""""""), "-1", materialID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les Bennes chaudes d'un cycle
    ''***********************************************************************************************************************

    '' Cette information n'est pas disponible actuellement dans un csv
    Public Overrides Function getHotFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederDebit As String = "-4"
        Try
            If (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString).Trim)) Then

                hotFeederDebit = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateDebit

            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then

                hotFeederDebit = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveDebit

            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then

                hotFeederDebit = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeDebit

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID).Trim)) Then

                hotFeederDebit = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxDebit

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID).Trim)) Then
                hotFeederDebit = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerDebit
            End If

            Return If(String.IsNullOrEmpty(hotFeederDebit), "-1", hotFeederDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederActualPercentage As String = "-4"
        Try
            If (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString).Trim)) Then
                hotFeederActualPercentage = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateActualPercentage + (indexFeeder + 1).ToString, indexCycle, sourceFile)

            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederActualPercentage = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveActualPercentage + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString, indexCycle, sourceFile)

            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederActualPercentage = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeActualPercentage + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString, indexCycle, sourceFile)

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID).Trim)) Then
                hotFeederActualPercentage = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxActualPercentage, indexCycle, sourceFile)

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID).Trim)) Then
                hotFeederActualPercentage = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerActualPercentage, indexCycle, sourceFile)
            End If

            Return If(String.IsNullOrEmpty(hotFeederActualPercentage), "-1", hotFeederActualPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''Pour l'instant les hotFeeder: Additive et Chaux sont exclus, jusqu'a ce que j'ai des exemples pour les constantes
    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        If hotFeederCount <= 0 Then

            Dim hotFeederCountForCycle As Integer

            Try

                For Each hotFeeder As String In getColumnNameList(sourceFile)
                    If hotFeeder.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateActualPercentage) Then
                        hotFeederCountForCycle = hotFeederCountForCycle + 1

                    ElseIf hotFeeder.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerActualPercentage) Then
                        hotFeederCountForCycle = hotFeederCountForCycle + 1

                    ElseIf hotFeeder.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveActualPercentage) Then
                        hotFeederCountForCycle = hotFeederCountForCycle + 1

                    ElseIf hotFeeder.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxActualPercentage) Then
                        hotFeederCountForCycle = hotFeederCountForCycle + 1

                    ElseIf hotFeeder.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeActualPercentage) Then
                        hotFeederCountForCycle = hotFeederCountForCycle + 1
                    End If
                Next

                Me.hotFeederCount = hotFeederCountForCycle
                Return If(String.IsNullOrEmpty(hotFeederCount), "-1", hotFeederCount)
            Catch ex As Exception
                Return "-2"
            End Try
        Else
            Return Me.hotFeederCount
        End If

    End Function

    Public Overrides Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Dim hotFeederID As String = "-4"
        Try

            If (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString)))) Then
                hotFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString
                Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID.Trim)

            ElseIf (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString.Trim)))) Then
                hotFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString
                Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID.Trim)

            ElseIf (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString.Trim)))) Then
                hotFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString
                Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID.Trim)

            ElseIf (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID)))) Then
                hotFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID
                Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID.Trim)

            ElseIf (Not IsNothing(getColumnNameList(sourceFile).Find(Function(x) x.Contains(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID)))) Then
                hotFeederID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID
                Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID.Trim)

            End If

            Return If(String.IsNullOrEmpty(hotFeederID), "-1", hotFeederID.Trim)
        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As Double
        Dim hotFeederMass As Double = Double.NaN
        Try

            If (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString).Trim)) Then
                hotFeederMass = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateMass + (indexFeeder + 1).ToString, indexCycle, sourceFile)

            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederMass = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveMass + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString, indexCycle, sourceFile)

            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederMass = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeMass + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString, indexCycle, sourceFile)

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID).Trim)) Then
                hotFeederMass = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxMass, indexCycle, sourceFile)

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID).Trim)) Then
                hotFeederMass = getColumnFromCSVFile(TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerMass, indexCycle, sourceFile)
            End If


            'Conversion de la masse en kilograme en Tonnes
            ' TODO rendre la conversion plus propre
            hotFeederMass = hotFeederMass / 1000

            Return If(hotFeederMass < 0, Double.NaN, hotFeederMass)
        Catch ex As Exception
            Return Double.NaN
        End Try
    End Function


    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederTargetPercentage As String = "-4"
        Try
            If (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString).Trim)) Then
                hotFeederTargetPercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateTargetPercentage
            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederTargetPercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveTargetPercentage 
            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederTargetPercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeTargetPercentage

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID).Trim)) Then
                hotFeederTargetPercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxTargetPercentage

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID).Trim)) Then
                hotFeederTargetPercentage = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerTargetPercentage
            End If

            Return If(String.IsNullOrEmpty(hotFeederTargetPercentage), "-1", hotFeederTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getHotFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederMaterialID As String = "-4"
        Try
            If (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateID + (indexFeeder + 1).ToString).Trim)) Then
                hotFeederMaterialID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAggregateMaterialID
            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederMaterialID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederAdditiveMaterialID 
            ElseIf getHotFeederID(indexFeeder, indexCycle, sourceFile).Trim.Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeID + (getHotFeederCountForCycle(indexCycle, sourceFile) - (indexFeeder + 1)).ToString).Trim) Then
                hotFeederMaterialID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederDopeMaterialID 
            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxID).Trim)) Then
                hotFeederMaterialID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederChauxMaterialID

            ElseIf (getHotFeederID(indexFeeder, indexCycle, sourceFile).Equals((TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerID).Trim)) Then
                hotFeederMaterialID = TryCast(sourceFile.importConstant, ImportConstant_csv).hotFeederFillerMaterialID
            End If

            Return If(String.IsNullOrEmpty(hotFeederMaterialID), "-1", hotFeederMaterialID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

End Class
