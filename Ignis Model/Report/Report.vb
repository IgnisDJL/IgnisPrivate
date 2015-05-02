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
        productionDayList = PlantProduction.getProductionDay(debutPeriode, finPeriode)


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
