Imports System.Text.RegularExpressions
Imports IGNIS.Constants.Input

Public Class SourceFileLogAdapter
    Inherits SourceFileAdapter

    Public Sub New()

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
    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTotalMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function

    ''Total aggregate

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getCycleAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateActualPercentage As String = "-4"
        Try

            Return totalAggregateActualPercentage
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getCycleAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateDebit As String = "-4"

        Try
            Return If(String.IsNullOrEmpty(totalAggregateDebit), "-1", totalAggregateDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getCycleAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateMass As String = "-4"

        Try
            Return If(String.IsNullOrEmpty(totalAggregateMass), "-1", totalAggregateMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getCycleAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateTargetPercentage As String = "-4"
        Try
            Return If(String.IsNullOrEmpty(totalAggregateTargetPercentage), "-1", totalAggregateTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    ''Cycle asphalt
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


    ''***********************************************************************************************************************
    ''  Section concernant les donnée liées un ProductionCycle 
    ''***********************************************************************************************************************


    '' #TODO À réparer la fonction ! 

    Public Overrides Function getTime(indexCycle As Integer, sourceFile As SourceFile) As Date
        'Dim time As String
        'Dim regex = New Regex("^([\d][\d]?:[\d][\d]:[\d][\d]([\s](AM|PM))?)")
        'time = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        'Return If(String.IsNullOrEmpty(time), "-1", time)
        Return Date.Now
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

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixCounter As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.mixCounter + "[\s]+([\d]+)[\s]T")
            mixCounter = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(mixCounter), "-1", mixCounter)
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
    ''**********************************************************************************************************************

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixDebit As String = "-4"
        Try
            Dim regex = New Regex(sourceFile.importConstant.mixDebit + "[\s]+([\d]{2,3})")
            mixDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
            Return If(String.IsNullOrEmpty(mixDebit), "-1", mixDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

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
    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getColdFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
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
            feederDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederDebit, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederDebit), "-1", feederDebit)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function


    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String = "-4"
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederActualPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim
            Return If(String.IsNullOrEmpty(feederActualPercentage), "-1", feederActualPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        Return EnumLineLogFile.hotFeederCount

    End Function


    Public Overrides Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String = "-4"

        Dim regex = New Regex("(\w+)")
        Try
            feederID = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederIdPart1, indexCycle, sourceFile))(indexFeeder).Value.Trim

            If regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederIdPart2, indexCycle, sourceFile)).Count > indexFeeder Then
                feederID = feederID + regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederIdPart2, indexCycle, sourceFile))(indexFeeder).Value.Trim()
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
            feederMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMass, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMass), "-1", feederMass)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function

    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederTargetPercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederTargetPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederTargetPercentage), "-1", feederTargetPercentage)
        Catch ex As Exception
            Return "-2"
        End Try
    End Function
    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getHotFeederMaterialID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Return "-3"
    End Function
End Class
