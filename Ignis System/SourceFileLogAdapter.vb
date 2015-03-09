Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class SourceFileLogAdapter
    Inherits SourceFileAdapter

    Private hotFeederValidIndexList As List(Of Integer)


    Public Sub New()
        hotFeederValidIndexList = New List(Of Integer)
        hotFeederValidIndexList.Add(EnumColumnType.VirginAggregate)
        hotFeederValidIndexList.Add(EnumColumnType.RecycledAggregate)
        hotFeederValidIndexList.Add(EnumColumnType.Filler)
        hotFeederValidIndexList.Add(EnumColumnType.Additive)
    End Sub

    ''***********************************************************************************************************************
    ''  Fonction private unique au type de fichier source
    ''  Fonction qui effectu une oppération de formatage ou d'affichage du fichier source
    ''
    ''***********************************************************************************************************************

    Private Function getLineFromLogFile(lineNumber As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Try
            Return Regex.Split(getCycle(indexCycle, sourceFile), "\r")(lineNumber)
        Catch
            Return ""
        End Try

    End Function

    ''***********************************************************************************************************************
    ''  Fonction protected force l'adapteur a implémenté une fonction utile a la lecture du fichier source ou au formatage des donnée
    ''***********************************************************************************************************************

    Protected Overrides Function getCycleList(sourceFile As SourceFile) As List(Of String)

        If IsNothing(cycleList) Then
            Dim readingStream As System.IO.StreamReader = Nothing
            Dim stringFile As String = Nothing

            readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName)
            stringFile = readingStream.ReadToEnd
            cycleList = New List(Of String)

            For Each cycle_1 As String In Split(stringFile, sourceFile.importConstant.time).ToList
                If Not String.IsNullOrEmpty(cycle_1) Then
                    cycleList.Add(cycle_1)
                End If
            Next

            Return cycleList
        Else
            Return cycleList
        End If

    End Function

    Protected Overrides Function getCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Return getCycleList(sourceFile).Item(indexCycle)
    End Function




    ''***********************************************************************************************************************
    ''  Fonction publique mais qui n'ont pas la responsabilié de retourner une informations directement au modèle du domaine
    ''
    ''*********************************************************************************************************************


    ''' <summary>
    ''' Cette fonction renvoi le nombre de cycle qui ont été détecté dans le fichier source
    ''' </summary>
    ''' <param name="sourceFile"></param>
    ''' <returns>Le nombre de cycle détecté dans le fichier source</returns>
    ''' <remarks>Cette donnée n'est utilisé que pour ittéré sur le bon nombre de cycle, elle n'est conservé nulle part</remarks>
    Public Overrides Function getCycleCount(sourceFile As SourceFile) As Integer
        Return getCycleList(sourceFile).Count()
    End Function


    Public Overrides Sub setImportConstantForLanguage(sourceFile As SourceFile)

        If IsNothing(sourceFile.importConstant) Then
            Dim readingStream As System.IO.StreamReader = Nothing
            Dim stringFile As String = Nothing

            readingStream = New System.IO.StreamReader(sourceFile.getFileInfo.FullName)
            stringFile = readingStream.ReadToEnd

            If (stringFile.StartsWith(ImportConstant_log.time_En_log)) Then
                sourceFile.importConstant = New ImportConstantEn_log
            Else
                sourceFile.importConstant = New ImportConstantFr_log
            End If
        End If
    End Sub


    ''***********************************************************************************************************************
    ''  Fonction publique générique a tout les adapteurs
    ''  Fonction qui récupère une donnée du fichier source, ou qui calcule une donnée avec d'autre donnée source
    ''  Ces fonctions permettent de générer les objets du modèle du programme
    ''***********************************************************************************************************************

    ''***********************************************************************************************************************
    ''  Section concernant de donnée lier a un ProductionDay
    ''***********************************************************************************************************************



    ''' <summary>
    ''' Fonction qui utilise le nom du fichier source pour déterminer la date de création du fichier source
    ''' </summary>
    ''' <param name="sourceFile"></param>
    ''' <returns>Renvoi la date de création du fichier source</returns>
    ''' <remarks>Utilisé par l'objet sourceFile pour lister les fichiers disponible a l'importation</remarks>
    Public Overrides Function getDate(sourceFile As SourceFile) As Date
        Dim regex = New Regex(Constants.Input.LOG.FILE_NAME_REGEX)
        Dim match As Match = regex.Match(sourceFile.getFileInfo.Name)

        If (match.Success) Then

            Dim day As Integer = Integer.Parse(match.Groups(LOG.LOGFileNameRegexDateIndex.DAY).Value)
            Dim month As Integer = Integer.Parse(match.Groups(LOG.LOGFileNameRegexDateIndex.MONTH).Value)
            Dim year As Integer = Integer.Parse(match.Groups(LOG.LOGFileNameRegexDateIndex.YEAR).Value)

            Return New Date(year, month, day)

        Else

            ' #exception

            Return Nothing
        End If
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les totaux d'un cycle de production 
    ''***********************************************************************************************************************

    
    ''Total Mass
    Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim realTotalMass As Double = -4
        Try
            If indexCycle > 0 Then

                Dim previousAsphaltConcreteTotalMass As Double
                Dim actualAsphaltConcreteTotalMass As Double

                previousAsphaltConcreteTotalMass = getCycleAsphaltConcreteTotalMass(indexCycle - 1, sourceFile)
                actualAsphaltConcreteTotalMass = getCycleAsphaltConcreteTotalMass(indexCycle, sourceFile)

                If actualAsphaltConcreteTotalMass > previousAsphaltConcreteTotalMass Then

                    Dim previousTotalMass As Double
                    Dim actualTotalMass As Double


                    For indexFeeder As Integer = 0 To hotFeederValidIndexList.Count - 1

                        previousTotalMass += getHotFeederMass(indexFeeder, indexCycle - 1, sourceFile)
                        actualTotalMass += getHotFeederMass(indexFeeder, indexCycle, sourceFile)

                    Next

                    previousTotalMass += previousAsphaltConcreteTotalMass
                    actualTotalMass += actualAsphaltConcreteTotalMass

                    realTotalMass = actualTotalMass - previousTotalMass

                    Return If(realTotalMass < -4, -1, realTotalMass)
                Else
                    Return If(realTotalMass < -4, -1, realTotalMass)
                End If

            Else
                Return If(realTotalMass < -4, -1, realTotalMass)
            End If

        Catch ex As Exception
            Return -2.0
        End Try
    End Function


    '' TODO 
    '' Cette information est disponible donc il faut la calculer
    Public Overrides Function getCycleAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim cycleAggregateActualPercentage As String = "-4"
        Try

            Return cycleAggregateActualPercentage
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' TODO 
    '' Cette information est disponible donc il faut la calculer
    Public Overrides Function getCycleAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim cycleAggregateDebit As String = "-4"



        Try
            Return If(String.IsNullOrEmpty(cycleAggregateDebit), "-1", cycleAggregateDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' TODO 
    '' Cette information est disponible donc il faut la calculer
    Public Overrides Function getCycleAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim cycleAggregateMass As String = "-4"

        Try
            Return If(String.IsNullOrEmpty(cycleAggregateMass), "-1", cycleAggregateMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    '' TODO 
    '' Cette information est disponible donc il faut la calculer
    Public Overrides Function getCycleAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim cycleAggregateTargetPercentage As String = "-4"
        Try
            Return If(String.IsNullOrEmpty(cycleAggregateTargetPercentage), "-1", cycleAggregateTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''***********************************************************************************************************************
    ''  Section concernant les donnée liées au bitume ou A/C d'un cycle 
    ''***********************************************************************************************************************

    Public Overrides Function getCycleAsphaltConcreteActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltActualPercentage As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederActualPercentage, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltActualPercentage), "-1", virginAsphaltActualPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltDebit As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederDebit, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltDebit), "-1", virginAsphaltDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltMass As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMass, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltMass), "-1", virginAsphaltMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Private Function getCycleAsphaltConcreteTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltConcreteTotalMass As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            asphaltConcreteTotalMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMass, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(asphaltConcreteTotalMass), "-1", asphaltConcreteTotalMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltTargetPercentage As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            virginAsphaltTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederTargetPercentage, indexCycle, sourceFile))(EnumColumnType.VirginAspahlt).Value.Trim
            Return If(String.IsNullOrEmpty(virginAsphaltTargetPercentage), "-1", virginAsphaltTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getCycleAsphaltConcreteGrade(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim virginAsphaltConcreteGrade As String = "-4"

        Try
            virginAsphaltConcreteGrade = "-3"
            Return If(String.IsNullOrEmpty(virginAsphaltConcreteGrade), "-1", virginAsphaltConcreteGrade)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les donnée liées un ProductionCycle 
    ''***********************************************************************************************************************
    Public Overrides Function getDureeMalaxHumideCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dureeMalaxHumideCycle As String = "-4"
        Try

            dureeMalaxHumideCycle = sourceFile.importConstant.dureeMalaxHumide()

            Return If(String.IsNullOrEmpty(dureeMalaxHumideCycle), "-1", dureeMalaxHumideCycle)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getDureeMalaxSecCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dureeMalaxSecCycle As String = "-4"
        Try

            dureeMalaxSecCycle = sourceFile.importConstant.dureeMalaxSec()

            Return If(String.IsNullOrEmpty(dureeMalaxSecCycle), "-1", dureeMalaxSecCycle)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getDureeCycle(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dureeCycle As String = "-4"
        Try

            dureeCycle = sourceFile.importConstant.dureeCycle()

            Return If(String.IsNullOrEmpty(dureeCycle), "-1", dureeCycle)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getManuelle(indexCycle As Integer, sourceFile As SourceFile) As Boolean
        Dim manuelle As Boolean = False

        Return manuelle
    End Function

    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        Dim time As Date
        Try
            Dim regex = New Regex("([\d]+:[\d]+:[\d]+[\s]AM)|([\d]+:[\d]+:[\d]+[\s]PM)")
            time = regex.Match(getCycle(indexCycle, sourceFile)).Groups(0).Value.Trim
            Return time
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function
    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function

    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim siloFillingNumber As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.siloFillingNumber + "[\s]+([\d]+)")
            siloFillingNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(siloFillingNumber), "-1", siloFillingNumber)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim bagHouseDiff As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.bagHouseDiff + "[\s](\-?[\d]{1,3}.[\d])")
            bagHouseDiff = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(bagHouseDiff), "-1", bagHouseDiff)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dustRemovalDebit As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.dustRemovalDebit + "[\s]+([\d]{1,2}\.[\d]+)")
            dustRemovalDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(dustRemovalDebit), "-1", dustRemovalDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function



    ''***********************************************************************************************************************
    ''  Section concernant les données liées au bitume utilisé dans un cycle 
    ''**********************************************************************************************************************

    Public Overrides Function getCycleAsphaltConcreteTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.virginAsphaltConcreteTankId + "[\s]([\d]+)")
            asphaltTankId = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(asphaltTankId), "-1", asphaltTankId)
        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getCycleAsphaltConcreteRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltRecordedTemperature As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.virginAsphaltConcreteRecordedTemperature + "[\s]+(\-?[\d]*)")
            asphaltRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(asphaltRecordedTemperature), "-1", asphaltRecordedTemperature)
        Catch ex As Exception
            Return "-2"
        End Try

    End Function

    Public Overrides Function getCycleAsphaltConcreteDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltDensity As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.virginAsphaltConcreteDensity + "[\s]+([\d].[\d]{3})")
            asphaltDensity = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(asphaltDensity), "-1", asphaltDensity)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle


    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixName As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.mixName + "[\s]([a-zA-Z0-9\s\-_%]+)([\n])")
            mixName = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(mixName), "-1", mixName)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.mixNumber + "[\s]+([a-zA-Z0-9\s\-_%]+)[\s]+" + sourceFile.importConstant.mixName + "[\s]([a-zA-Z0-9\s\-_%]+)")
            mixNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(mixNumber), "-1", mixNumber)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixRecordedTemperature As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.mixRecordedTemperature + "[\s]+([\-]?[\d]+)")
            mixRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(mixRecordedTemperature), "-1", mixRecordedTemperature)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************

    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederActualPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim
            Return If(String.IsNullOrEmpty(feederActualPercentage), "-1", feederActualPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        Dim feederCountForCycle As New Integer
        feederCountForCycle = 0

        Try
            For Each feederId As String In Regex.Split(getLineFromLogFile(EnumLineLogFile.coldFeederId, indexCycle, sourceFile), "[\s]+")
                If Not String.IsNullOrEmpty(feederId) Then
                    feederCountForCycle += 1
                End If
            Next

        Catch ex As Exception
            Return -2
        End Try

        Return feederCountForCycle
    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederDebit As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederDebit, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederDebit), "-1", feederDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String = "-4"

        Dim regex = New Regex("(\w+)")
        Try
            feederID = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederId, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederID), "-1", feederID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMass As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederMass, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMass), "-1", feederMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMoisturePercentage As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMoisturePercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederMoisturePercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMoisturePercentage), "-1", feederMoisturePercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Dim feederRecycledAsphaltPercentage As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Dim feederId As String
        feederId = getColdFeederID(indexFeeder, indexCycle, sourceFile).Trim
        Try
            If feederId.Contains(sourceFile.importConstant.recycledID) Then


                feederRecycledAsphaltPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederRecycledAsphaltPercentage, indexCycle, sourceFile))(Integer.Parse(Split(feederId, sourceFile.importConstant.recycledID)(1)) - 1).Value.Trim
            Else
                feederRecycledAsphaltPercentage = "0.0"
            End If

            Return If(String.IsNullOrEmpty(feederRecycledAsphaltPercentage), "-1", feederRecycledAsphaltPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederTargetPercentage As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederTargetPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederTargetPercentage), "-1", feederTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getColdFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim materialID As String = "-4"

        Try

            materialID = sourceFile.importConstant.coldFeederMaterialID

            Return If(String.IsNullOrEmpty(materialID), "-1", materialID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les Bennes chaudes d'un cycle
    ''  
    ''  Dans une production en continue, il n'y a pas réellement de bennes chaudes. Comme dans le cas d'une fichier .log
    ''  Toutefois, afin de concerver une uniformité d'un objet ProductionCycle, une liste de bennes chaudes est tout de même
    ''  ajouté. Se sont les totaux accumulé des composant de l'enrobé bitumineux qui y sont inséré
    ''***********************************************************************************************************************

    Public Overrides Function getHotFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederDebit As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederDebit, indexCycle, sourceFile))(hotFeederValidIndexList.Item(indexFeeder)).Value.Trim

            Return If(String.IsNullOrEmpty(feederDebit), "-1", feederDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederActualPercentage, indexCycle, sourceFile))(hotFeederValidIndexList.Item(indexFeeder)).Value.Trim
            Return If(String.IsNullOrEmpty(feederActualPercentage), "-1", feederActualPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        Return hotFeederValidIndexList.Count

    End Function


    Public Overrides Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String = "-4"

        Dim regex = New Regex("(\w+)")
        Try
            feederID = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederIdPart1, indexCycle, sourceFile))(hotFeederValidIndexList.Item(indexFeeder)).Value.Trim

            If EnumColumnType.RecycledAggregate >= indexFeeder Then
                feederID = feederID + regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederIdPart2, indexCycle, sourceFile))(hotFeederValidIndexList.Item(indexFeeder)).Value.Trim()
            End If

            Return If(String.IsNullOrEmpty(feederID), "-1", feederID)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMass As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMass, indexCycle, sourceFile))(hotFeederValidIndexList.Item(indexFeeder)).Value.Trim

            Return If(String.IsNullOrEmpty(feederMass), "-1", feederMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederTargetPercentage As String = "-4"

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederTargetPercentage, indexCycle, sourceFile))(hotFeederValidIndexList.Item(indexFeeder)).Value.Trim

            Return If(String.IsNullOrEmpty(feederTargetPercentage), "-1", feederTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim hotFeederMaterialID As String = "-4"

        Try
            hotFeederMaterialID = sourceFile.importConstant.hotFeederMaterialID
            Return If(String.IsNullOrEmpty(hotFeederMaterialID), "-1", hotFeederMaterialID)
        Catch ex As Exception
            Return "-2"
        End Try

    End Function

End Class
