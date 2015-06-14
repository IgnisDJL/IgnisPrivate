Public MustInherit Class Report

    Private finPeriode As Date
    Private debutPeriode As Date
    Private dateCreationRapport As Date
    Private productionDayList As List(Of ProductionDay_1)


    Private dureeTotaleDesPauses As TimeSpan



    Public Sub New(debutPeriode As Date, finPeriode As Date)
        Me.debutPeriode = debutPeriode
        Me.finPeriode = finPeriode
        dateCreationRapport = Date.Now

        Dim continueSourceFileList As List(Of SourceFile)
        Dim discontinueSourceFileList As List(Of SourceFile)
        Dim continueSourceFileComplementList As List(Of String)
        Dim discontinueSourceFileComplementList As List(Of String)
        Dim productionDayList As List(Of ProductionDay_1)
        Dim productionDayFactory As ProductionDayFactory

        continueSourceFileList = New List(Of SourceFile)
        discontinueSourceFileList = New List(Of SourceFile)
        continueSourceFileComplementList = New List(Of String)
        discontinueSourceFileComplementList = New List(Of String)
        productionDayFactory = New ProductionDayFactory
        productionDayList = New List(Of ProductionDay_1)

        Dim productionDay As ProductionDay_1

        continueSourceFileList.Clear()
        discontinueSourceFileList.Clear()

        Dim logDirectory As IO.DirectoryInfo = New IO.DirectoryInfo(Constants.Paths.LOG_ARCHIVES_DIRECTORY)
        Dim csvDirectory As IO.DirectoryInfo = New IO.DirectoryInfo(Constants.Paths.CSV_ARCHIVES_DIRECTORY)
        Dim eventDirectory As IO.DirectoryInfo = New IO.DirectoryInfo(Constants.Paths.EVENTS_ARCHIVES_DIRECTORY)



        Dim newestSourceFile As SourceFile = Nothing

        Dim regexLogFile As New System.Text.RegularExpressions.Regex(Constants.Input.LOG.FILE_NAME_REGEX)
        Dim regexEventFile As New System.Text.RegularExpressions.Regex(Constants.Input.Events.FILE_NAME_REGEX)
        Dim regexCSVFile As New System.Text.RegularExpressions.Regex(Constants.Input.CSV.FILE_NAME_REGEX)
        Dim regexMDBFile As New System.Text.RegularExpressions.Regex(Constants.Input.MDB.FILE_NAME_REGEX)

        For Each file As IO.FileInfo In logDirectory.GetFiles

            If (regexLogFile.Match(file.Name).Success) And (PlantProduction.getPlantType = Constants.Settings.UsineType.LOG Or PlantProduction.getPlantType = Constants.Settings.UsineType.HYBRID) Then
                Dim sourceFile As New SourceFile(file.FullName, New SourceFileLogAdapter())

                If eventDirectory.Exists Then
                    For Each eventfile As IO.FileInfo In eventDirectory.GetFiles
                        If (sourceFile.Date_.Year.ToString + sourceFile.Date_.Month.ToString + sourceFile.Date_.Day.ToString + ".log").Equals(eventfile.Name) Then
                            sourceFile.setEventFilePath(eventfile.FullName)
                        End If

                    Next
                End If

                continueSourceFileList.Add(sourceFile)
            End If

        Next

        For Each file As IO.FileInfo In csvDirectory.GetFiles

            If (regexCSVFile.Match(file.Name).Success) And (PlantProduction.getPlantType = Constants.Settings.UsineType.CSV Or PlantProduction.getPlantType = Constants.Settings.UsineType.MDB Or PlantProduction.getPlantType = Constants.Settings.UsineType.HYBRID) Then
                Dim sourceFile As New SourceFile(file.FullName, New SourceFileCSVAdapter())

                discontinueSourceFileList.Add(sourceFile)
            End If
        Next

        If PlantProduction.getPlantType = Constants.Settings.UsineType.HYBRID Then

            For Each continueSourceFile As SourceFile In continueSourceFileList
                If (continueSourceFile.Date_ >= New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day) And continueSourceFile.Date_ <= New Date(finPeriode.Year, finPeriode.Month, finPeriode.Day)) Then

                    If discontinueSourceFileList.Contains(continueSourceFile) Then
                        productionDay = productionDayFactory.createProductionDayHybrid(continueSourceFile, discontinueSourceFileList.Item(discontinueSourceFileList.IndexOf(continueSourceFile)))

                        productionDay.setSourceFileComplementPathContinue(continueSourceFile.getEventFilePath)

                        productionDayList.Add(productionDay)

                    Else
                        productionDay = productionDayFactory.createProductionDayHybrid(continueSourceFile)
                        productionDay.setSourceFileComplementPathContinue(continueSourceFile.getEventFilePath)
                        productionDayList.Add(productionDay)


                    End If
                End If

            Next

        ElseIf PlantProduction.getPlantType = Constants.Settings.UsineType.LOG Then

            For Each continueSourceFile As SourceFile In continueSourceFileList
                If (continueSourceFile.Date_ >= New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day) And continueSourceFile.Date_ <= New Date(finPeriode.Year, finPeriode.Month, finPeriode.Day)) Then
                    productionDay = productionDayFactory.createProductionDayContinue(continueSourceFile)
                    productionDay.setSourceFileComplementPathContinue(continueSourceFile.getEventFilePath)
                    productionDayList.Add(productionDay)
                End If


            Next

        ElseIf PlantProduction.getPlantType = Constants.Settings.UsineType.CSV Or PlantProduction.getPlantType = Constants.Settings.UsineType.MDB Then

            For Each discontinueSourceFile As SourceFile In discontinueSourceFileList
                If (discontinueSourceFile.Date_ >= New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day) And discontinueSourceFile.Date_ <= New Date(finPeriode.Year, finPeriode.Month, finPeriode.Day)) Then
                    productionDay = productionDayFactory.createProductionDayDiscontinue(discontinueSourceFile)
                    productionDayList.Add(productionDay)
                End If
            Next

        End If

        Me.productionDayList = productionDayList

    End Sub

    Public ReadOnly Property getDebutPeriode As Date
        Get
            Return debutPeriode
        End Get
    End Property


    Public ReadOnly Property getFinPeriode As Date
        Get
            Return finPeriode
        End Get
    End Property

    Public ReadOnly Property getDateCreationRapport As Date
        Get
            Return dateCreationRapport
        End Get
    End Property


    Public Function getDureePeriode() As TimeSpan
        Dim dureePeriode As TimeSpan = finPeriode.Subtract(debutPeriode)

        Return dureePeriode
    End Function

    Public Function getProductionDayList() As List(Of ProductionDay_1)

        Return productionDayList
    End Function

    Public Function getProductionCycleContiuList() As List(Of ProductionCycle)

        Return getProductionCycleList(True)
    End Function

    Public Function getProductionCycleDiscontiuList() As List(Of ProductionCycle)

        Return getProductionCycleList(False)

    End Function

    Private Function getProductionCycleList(continu As Boolean) As List(Of ProductionCycle)
        Dim tempProductionCycleList = New List(Of ProductionCycle)
        Dim productionCycleList = New List(Of ProductionCycle)
        For Each productionDay_1 As ProductionDay_1 In getProductionDayList()

            If getDebutPeriode.Day = productionDay_1.getProductionDate.Day And getFinPeriode.Day = productionDay_1.getProductionDate.Day Then

                If continu Then
                    tempProductionCycleList = productionDay_1.getProductionCycle_Discontinue(getDebutPeriode, getFinPeriode)
                Else
                    tempProductionCycleList = productionDay_1.getProductionCycle_Continue(getDebutPeriode, getFinPeriode)
                End If

            Else
                If getDebutPeriode.Day < productionDay_1.getProductionDate.Day Then

                    If continu Then
                        tempProductionCycleList = productionDay_1.getProductionCycle_Continue(New Date(getDebutPeriode.Year, getDebutPeriode.Month, getDebutPeriode.Day + 1), getFinPeriode)

                    Else
                        tempProductionCycleList = productionDay_1.getProductionCycle_Discontinue(New Date(getDebutPeriode.Year, getDebutPeriode.Month, getDebutPeriode.Day + 1), getFinPeriode)

                    End If

                Else

                    If continu Then
                        tempProductionCycleList = productionDay_1.getProductionCycle_Continue(getDebutPeriode, getFinPeriode - TimeSpan.FromSeconds(1))

                    Else
                        tempProductionCycleList = productionDay_1.getProductionCycle_Discontinue(getDebutPeriode, getFinPeriode - TimeSpan.FromSeconds(1))

                    End If

                End If

            End If

            If tempProductionCycleList.Count > 0 Then
                If productionCycleList.Count > 0 Then
                    productionCycleList.InsertRange(productionCycleList.Count - 1, tempProductionCycleList)
                Else
                    productionCycleList.InsertRange(0, tempProductionCycleList)
                End If
            End If

        Next

        Return productionCycleList
    End Function

    Public Function getProducedMixList(productionCycleList As List(Of ProductionCycle)) As List(Of ProducedMix)
        Dim producedMixList = New List(Of ProducedMix)
        ' TODO A REPARER NE PAS UTILISER UN PRODUCTIONDAY_1 
        If productionDayList.Count > 0 Then
            Dim productionDay = New ProductionDay_1(Date.Now)
            producedMixList = productionDay.getProducedMixList(productionCycleList)
        End If

        Return producedMixList
    End Function


    Public Function getSourceFileComplementContinuList() As List(Of String)
        Dim sourceFileComplementContinuList = New List(Of String)


        For Each productionDay_1 As ProductionDay_1 In getProductionDayList()
            If Not String.IsNullOrEmpty(productionDay_1.getSourceFileComplementPathContinue) Then
                sourceFileComplementContinuList.Add(productionDay_1.getSourceFileComplementPathContinue)
            End If
        Next

        Return sourceFileComplementContinuList
    End Function

End Class
