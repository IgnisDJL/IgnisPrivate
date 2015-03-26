Public Class DelayFactory

    Private batchDelayAdapter As BatchDelayAdapter
    Private drumDelayAdapter As DrumDelayAdapter
    Private drumDelayLogAdapter As DrumDelayLogAdapter

    '' *************************************************************************************************
    ''                                          Constructeur 
    '' *************************************************************************************************
    Public Sub New()
        batchDelayAdapter = New BatchDelayAdapter()
        drumDelayAdapter = New DrumDelayAdapter()
        drumDelayLogAdapter = New DrumDelayLogAdapter()
    End Sub

    '' *************************************************************************************************
    ''                                          Delay 
    '' *************************************************************************************************

    Private Function createDelay(startDelay As Date, endDelay As Date, idDailyReport As Guid) As Delay_1

        Dim delay As Delay_1
        delay = New Delay_1(startDelay, endDelay)
        delay.setIdDailyReport(idDailyReport)

        Return delay
    End Function

    Private Function createDelay(startDelay As Date, endDelay As Date) As Delay_1
        Return createDelay(startDelay, endDelay, Nothing)
    End Function

    '' *************************************************************************************************
    ''                                          Batch Delay 
    '' *************************************************************************************************
    Public Function createBatchDelayList(startPeriod As Date, endPeriod As Date, idDailyReport As Guid, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of Delay_1)

        Dim delay As Delay_1
        Dim delayList As List(Of Delay_1)
        Dim dateBoundaryList As List(Of List(Of Date))

        delayList = New List(Of Delay_1)

        If (sourceFileComplementPathList.Count = 0) Then
            dateBoundaryList = batchDelayAdapter.getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
        Else
            dateBoundaryList = batchDelayAdapter.getDateBoundaryList(startPeriod, endPeriod, productionCycleList, sourceFileComplementPathList)
        End If

        For Each dateBoundary As List(Of Date) In dateBoundaryList
            delay = createDelay(dateBoundary(0), dateBoundary(1), idDailyReport)
            delayList.Add(delay)
        Next

        Return filterDelayList(delayList)
    End Function

    Public Function createBatchDelayList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of Delay_1)
        Return createBatchDelayList(startPeriod, endPeriod, Nothing, productionCycleList, sourceFileComplementPathList)
    End Function

    '' *************************************************************************************************
    ''                                          Drum Delay 
    '' *************************************************************************************************
    Public Function createDrumDelayList(startPeriod As Date, endPeriod As Date, idDailyReport As Guid, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of Delay_1)
        Dim delay As Delay_1
        Dim delayList As List(Of Delay_1)
        Dim dateBoundaryList As List(Of List(Of Date))

        delayList = New List(Of Delay_1)

        If (sourceFileComplementPathList.Count = 0) Then
            dateBoundaryList = drumDelayAdapter.getDateBoundaryList(startPeriod, endPeriod, productionCycleList)
        Else
            dateBoundaryList = drumDelayLogAdapter.getDateBoundaryList(startPeriod, endPeriod, productionCycleList, sourceFileComplementPathList)
        End If

        For Each dateBoundary As List(Of Date) In dateBoundaryList
            delay = createDelay(dateBoundary(0), dateBoundary(1), idDailyReport)
            delayList.Add(delay)
        Next

        Return filterDelayList(delayList)
    End Function

    Public Function createDrumDelayList(startPeriod As Date, endPeriod As Date, productionCycleList As List(Of ProductionCycle), sourceFileComplementPathList As List(Of String)) As List(Of Delay_1)
        Return createDrumDelayList(startPeriod, endPeriod, Nothing, productionCycleList, sourceFileComplementPathList)
    End Function


    '' *************************************************************************************************
    ''                                          Hybrid Delay 
    '' *************************************************************************************************
    Public Function createHybridDelayList(startPeriod As Date, endPeriod As Date, idDailyReport As Guid, drumProductionCycleList As List(Of ProductionCycle), batchProductionCycleList As List(Of ProductionCycle), drumStringPathList As List(Of String), batchStringPathList As List(Of String)) As List(Of Delay_1)
        Dim drumDelayList As List(Of Delay_1)
        Dim batchDelayList As List(Of Delay_1)
        Dim hybridDelayList As List(Of Delay_1)
        hybridDelayList = New List(Of Delay_1)
        drumDelayList = createDrumDelayList(startPeriod, endPeriod, idDailyReport, drumProductionCycleList, drumStringPathList)
        batchDelayList = createBatchDelayList(startPeriod, endPeriod, idDailyReport, batchProductionCycleList, batchStringPathList)

        If drumDelayList.Count >= batchDelayList.Count Then

            For Each drumDelay As Delay_1 In drumDelayList

                For Each batchDelay As Delay_1 In batchDelayList

                    If batchDelay.getEndDelay > drumDelay.getStartDelay And batchDelay.getStartDelay < drumDelay.getEndDelay Then

                        If drumDelay.getStartDelay > batchDelay.getStartDelay Then

                            If drumDelay.getEndDelay < batchDelay.getEndDelay Then
                                hybridDelayList.Add(New Delay_1(drumDelay.getStartDelay, drumDelay.getEndDelay))
                            Else
                                hybridDelayList.Add(New Delay_1(drumDelay.getStartDelay, batchDelay.getEndDelay))
                            End If

                        Else
                            If drumDelay.getEndDelay < batchDelay.getEndDelay Then
                                hybridDelayList.Add(New Delay_1(batchDelay.getStartDelay, drumDelay.getEndDelay))
                            Else
                                hybridDelayList.Add(New Delay_1(batchDelay.getStartDelay, batchDelay.getEndDelay))
                            End If
                        End If

                    End If
                Next
            Next
        Else
            For Each batchDelay As Delay_1 In batchDelayList

                For Each drumDelay As Delay_1 In drumDelayList

                    If drumDelay.getEndDelay > batchDelay.getStartDelay And drumDelay.getStartDelay < batchDelay.getEndDelay Then

                        If drumDelay.getStartDelay > batchDelay.getStartDelay Then

                            If drumDelay.getEndDelay < batchDelay.getEndDelay Then
                                hybridDelayList.Add(New Delay_1(drumDelay.getStartDelay, drumDelay.getEndDelay))
                            Else
                                hybridDelayList.Add(New Delay_1(drumDelay.getStartDelay, batchDelay.getEndDelay))
                            End If

                        Else
                            If drumDelay.getEndDelay < batchDelay.getEndDelay Then
                                hybridDelayList.Add(New Delay_1(batchDelay.getStartDelay, drumDelay.getEndDelay))
                            Else
                                hybridDelayList.Add(New Delay_1(batchDelay.getStartDelay, batchDelay.getEndDelay))
                            End If
                        End If

                    End If
                Next
            Next
        End If

        Return filterDelayList(hybridDelayList)
    End Function

    Public Function createHybridDelayList(startPeriod As Date, endPeriod As Date, drumProductionCycleList As List(Of ProductionCycle), batchProductionCycleList As List(Of ProductionCycle), drumStringPathList As List(Of String), batchStringPathList As List(Of String)) As List(Of Delay_1)
        Return createHybridDelayList(startPeriod, endPeriod, Nothing, drumProductionCycleList, batchProductionCycleList, drumStringPathList, batchStringPathList)
    End Function



    '' *************************************************************************************************
    ''                                          Fonction Private 
    '' *************************************************************************************************
    Private Function filterDelayList(delayList As List(Of Delay_1)) As List(Of Delay_1)

        For Each delay As Delay_1 In delayList
            If delay.getEndDelay.Subtract(delay.getStartDelay) < TimeSpan.FromSeconds(60) Then
                delayList.Remove(delay)
            End If
        Next

        Return delayList
    End Function

End Class
