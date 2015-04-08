Public Class DailyReport
    Inherits Report

    Private dateDebut As Date
    Private dateFin As Date
    Private mixSummaryInfo As Dictionary(Of String, ArrayList)



    Public Sub New(dateDebut As Date, dateFin As Date)
        mixSummaryInfo = New Dictionary(Of String, ArrayList)

        Me.dateDebut = dateDebut
        Me.dateFin = dateFin
    End Sub

    Public ReadOnly Property getDateDebut As Date
        Get
            Return dateDebut
        End Get
    End Property


    Public ReadOnly Property getDateFin As Date
        Get
            Return dateFin
        End Get
    End Property

    'Public Function getTotalMixProduced() As Double

    '    Dim debut As Date
    '    Dim fin As Date
    '    Dim totalMixMass As Double = 0

    '    For Each productionDay As ProductionDay_1 In productionDayList

    '        If productionDay.getProductionDate.Day <> dateDebut.Day Then
    '            debut = New Date(dateDebut.Year, dateDebut.Month, dateDebut.Day + 1)
    '        End If

    '        If productionDay.getProductionDate.Day <> dateFin.Day Then
    '            debut = New Date(dateDebut.Year, dateDebut.Month, dateDebut.Day - 1)
    '        End If

    '        For Each producedMix As ProducedMix In productionDay.getProducedMixList_Hybrid(debut, fin)
    '            totalMixMass += producedMix.getMixMass()
    '        Next

    '    Next

    '    Return totalMixMass

    'End Function

    'Public Sub getMixSummaryInfo()
    '    Dim debut As Date
    '    Dim fin As Date
    '    Dim totalMixMass As Double = 0

    '    For Each productionDay As ProductionDay_1 In productionDayList

    '        If productionDay.getProductionDate.Day <> dateDebut.Day Then
    '            debut = New Date(dateDebut.Year, dateDebut.Month, dateDebut.Day + 1)
    '        End If

    '        If productionDay.getProductionDate.Day <> dateFin.Day Then
    '            debut = New Date(dateDebut.Year, dateDebut.Month, dateDebut.Day - 1)
    '        End If

    '        Dim mixProducedList = New List(Of ProducedMix)

    '        Dim summaryMix As ArrayList


    '        For Each productionCycle As ProductionCycle In productionDay.getProductionCycle_Continue(debut, fin)

    '            If mixProducedList.Contains(productionCycle.getProducedMix) Then



    '                mixProducedList.Item(mixProducedList.IndexOf(productionCycle.getProducedMix)).addMass(productionCycle.getProducedMix.getMixMass)

    '            Else
    '                mixProducedList.Add(New ProducedMix(productionCycle.getProducedMix))
    '                summaryMix = New ArrayList
    '                summaryMix.Add(productionCycle.getProducedMix.getMixName)
    '                summaryMix.Add(productionCycle.getProducedMix.getMixMass)
    '                summaryMix.Add(productionCycle.getDureeCycle)
    '            End If
    '        Next

    '    Next

    'End Sub

End Class
