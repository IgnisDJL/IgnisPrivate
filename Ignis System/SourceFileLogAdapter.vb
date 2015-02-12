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

            If (stringFile.StartsWith(GlobalImportConstant.time_En_log)) Then
                sourceFile.importConstant = New GlobalImportConstant.ImportConstantEn_log
            Else
                sourceFile.importConstant = New GlobalImportConstant.ImportConstantFr_log
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
        Return "-3"
    End Function

    ''Total aggregate

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTotalAggregateActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateActualPercentage As String = "0"

        Try

            Return totalAggregateActualPercentage
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTotalAggregateDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateDebit As String = "0"

        Try
            Return If(String.IsNullOrEmpty(totalAggregateDebit), "0.00", totalAggregateDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTotalAggregateMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateMass As String = "0"

        Try
            Return If(String.IsNullOrEmpty(totalAggregateMass), "0.00", totalAggregateMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTotalAggregateMoisturePercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateTargetPercentage As String = "0"
        Try
            Return If(String.IsNullOrEmpty(totalAggregateTargetPercentage), "0.00", totalAggregateTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    ''Cette information n'est pas disponible dans un fichier log
    Public Overrides Function getTotalAggregateTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAggregateTargetPercentage As String = "0"
        Try
            Return If(String.IsNullOrEmpty(totalAggregateTargetPercentage), "0.00", totalAggregateTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    ''Total asphalt
    Public Overrides Function getTotalAsphaltActualPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederActualPercentage, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltActualPercentage), "0.00", totalAsphaltActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getTotalAsphaltDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltDebit As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederDebit, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltDebit), "0.00", totalAsphaltDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getTotalAsphaltMass(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltMass As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMass, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltMass), "0.00", totalAsphaltMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getTotalAsphaltTargetPercentage(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim totalAsphaltTargetPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            totalAsphaltTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederTargetPercentage, indexCycle, sourceFile))(EnumColumnType.TotalAsphalt).Value.Trim
            Return If(String.IsNullOrEmpty(totalAsphaltTargetPercentage), "0.00", totalAsphaltTargetPercentage)
        Catch ex As Exception
            Return "0.00"
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
        'Return If(String.IsNullOrEmpty(time), "N/A", time)
        Return Date.Now
    End Function


    Public Overrides Function getContractID(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getTruckID(indexCycle As Integer, sourceFile As SourceFile) As String
        Return Nothing
    End Function

    Public Overrides Function getSiloFillingNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim siloFillingNumber As String
        Dim regex = New Regex(sourceFile.importConstant.siloFillingNumber + "[\s]+([\d]+)")
        siloFillingNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(siloFillingNumber), "N/A", siloFillingNumber)
    End Function

    Public Overrides Function getBagHouseDiff(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim bagHouseDiff As String
        Dim regex = New Regex(sourceFile.importConstant.bagHouseDiff + "[\s](\-?[\d]{1,3}.[\d])")
        bagHouseDiff = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(bagHouseDiff), "0.00", bagHouseDiff)
    End Function

    Public Overrides Function getDustRemovalDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim dustRemovalDebit As String
        Dim regex = New Regex(sourceFile.importConstant.dustRemovalDebit + "[\s]+([\d]{1,2}\.[\d]+)")
        dustRemovalDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(dustRemovalDebit), "0.00", dustRemovalDebit)
    End Function

    Public Overrides Function getMixCounter(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixCounter As String
        Dim regex = New Regex(sourceFile.importConstant.mixCounter + "[\s]+([\d]+)[\s]T")
        mixCounter = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixCounter), "0.00", mixCounter)
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les données liées au bitume utilisé dans un cycle 
    ''**********************************************************************************************************************

    Public Overrides Function getAsphaltTankId(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltTankId As String
        Dim regex = New Regex(sourceFile.importConstant.asphaltTankId + "[\s]([\d]+)")
        asphaltTankId = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltTankId), "N/A", asphaltTankId)

    End Function

    Public Overrides Function getAsphaltRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltRecordedTemperature As String
        Dim regex = New Regex(sourceFile.importConstant.asphaltRecordedTemperature + "[\s]+(\-?[\d]*)")
        asphaltRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltRecordedTemperature), "0.00", asphaltRecordedTemperature)

    End Function

    Public Overrides Function getAsphaltDensity(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim asphaltDensity As String
        Dim regex = New Regex(sourceFile.importConstant.asphaltDensity + "[\s]+([\d].[\d]{3})")
        asphaltDensity = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(asphaltDensity), "0.00", asphaltDensity)
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les données liées a l'enrobé bitumineux produit dans un cycle
    ''**********************************************************************************************************************

    Public Overrides Function getMixDebit(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixDebit As String
        Dim regex = New Regex(sourceFile.importConstant.mixDebit + "[\s]+([\d]{2,3})")
        mixDebit = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixDebit), "0.00", mixDebit)
    End Function

    Public Overrides Function getMixName(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixName As String
        Dim regex = New Regex(sourceFile.importConstant.mixName + "[\s]([a-zA-Z0-9\s\-_%]+)([\n])")
        mixName = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixName), "N/A", mixName)
    End Function

    Public Overrides Function getMixNumber(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixNumber As String
        Dim regex = New Regex(sourceFile.importConstant.mixNumber + "[\s]+([a-zA-Z0-9\s\-_%]+)[\s]+" + sourceFile.importConstant.mixName + "[\s]([a-zA-Z0-9\s\-_%]+)")
        mixNumber = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixNumber), "N/A", mixNumber)
    End Function

    Public Overrides Function getMixRecordedTemperature(indexCycle As Integer, sourceFile As SourceFile) As String
        Dim mixRecordedTemperature As String
        Dim regex = New Regex(sourceFile.importConstant.mixRecordedTemperature + "[\s]+([\-]?[\d]+)")
        mixRecordedTemperature = regex.Match(getCycle(indexCycle, sourceFile)).Groups(1).Value.Trim
        Return If(String.IsNullOrEmpty(mixRecordedTemperature), "0.00", mixRecordedTemperature)
    End Function


    ''***********************************************************************************************************************
    ''  Section concernant les Bennes froides d'un cycle
    ''***********************************************************************************************************************

    Public Overrides Function getColdFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederActualPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim
            Return If(String.IsNullOrEmpty(feederActualPercentage), "0.00", feederActualPercentage)
        Catch ex As Exception
            Return "0.00"
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
            Return feederCountForCycle
        End Try

        Return feederCountForCycle
    End Function

    Public Overrides Function getColdFeederDebit(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederDebit As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederDebit, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederDebit), "0.00", feederDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getColdFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String

        Dim regex = New Regex("(\w+)")
        Try
            feederID = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederId, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederID), "N/A", feederID)
        Catch ex As Exception
            Return "N/A"
        End Try
    End Function

    Public Overrides Function getColdFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMass As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederMass, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMass), "0.00", feederMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getColdFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMoisturePercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMoisturePercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederMoisturePercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMoisturePercentage), "0.00", feederMoisturePercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function


    Public Overrides Function getColdFeederRecycledAsphaltPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String

        Dim feederRecycledAsphaltPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Dim feederId As String
        feederId = getColdFeederID(indexFeeder, indexCycle, sourceFile).Trim
        Try
            If feederId.Contains(sourceFile.importConstant.recycledID) Then


                feederRecycledAsphaltPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederRecycledAsphaltPercentage, indexCycle, sourceFile))(Integer.Parse(Split(feederId, sourceFile.importConstant.recycledID)(1)) - 1).Value.Trim
            Else
                feederRecycledAsphaltPercentage = "0.0"
            End If

            Return If(String.IsNullOrEmpty(feederRecycledAsphaltPercentage), "0.0", feederRecycledAsphaltPercentage)
        Catch ex As Exception
            Return "0.0"
        End Try
    End Function

    Public Overrides Function getColdFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederTargetPercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.coldFeederTargetPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederTargetPercentage), "0.00", feederTargetPercentage)
        Catch ex As Exception
            Return "0.00"
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
        Dim feederDebit As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederDebit = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederDebit, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederDebit), "0.00", feederDebit)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getHotFeederMoisturePercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMoisturePercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMoisturePercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMoisturePercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMoisturePercentage), "0.00", feederMoisturePercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getHotFeederActualPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederActualPercentage As String
        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederActualPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederActualPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim
            Return If(String.IsNullOrEmpty(feederActualPercentage), "0.00", feederActualPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getHotFeederCountForCycle(indexCycle As Integer, sourceFile As SourceFile) As Integer

        Return EnumLineLogFile.hotFeederCount

    End Function


    Public Overrides Function getHotFeederID(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederID As String

        Dim regex = New Regex("(\w+)")
        Try
            feederID = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederId, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederID), "N/A", feederID)
        Catch ex As Exception
            Return "N/A"
        End Try
    End Function

    Public Overrides Function getHotFeederMass(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederMass As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederMass = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederMass, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederMass), "0.00", feederMass)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

    Public Overrides Function getHotFeederTargetPercentage(indexFeeder As Integer, indexCycle As Integer, sourceFile As SourceFile) As String
        Dim feederTargetPercentage As String

        Dim regex = New Regex("([\d]+.[\d]+)")
        Try
            feederTargetPercentage = regex.Matches(getLineFromLogFile(EnumLineLogFile.hotFeederTargetPercentage, indexCycle, sourceFile))(indexFeeder).Value.Trim

            Return If(String.IsNullOrEmpty(feederTargetPercentage), "0.00", feederTargetPercentage)
        Catch ex As Exception
            Return "0.00"
        End Try
    End Function

End Class
