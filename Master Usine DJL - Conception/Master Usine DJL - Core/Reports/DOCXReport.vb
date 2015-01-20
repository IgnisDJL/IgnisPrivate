Imports Microsoft.Office.Interop
Imports IGNIS.XmlSettings

Public Class DOCXReport

    Private Property MASS_UNIT As Unit = Settings.instance.Report.Word.MASS_UNIT
    Private Property PRODUCTION_SPEED_UNIT As Unit = Settings.instance.Report.Word.PRODUCTION_SPEED_UNIT
    Private Property PERCENT_UNIT As Unit = Settings.instance.Report.Word.PERCENT_UNIT
    Private Property TEMPERATURE_UNIT As Unit = Settings.instance.Report.Word.TEMPERATURE_UNIT

    Private productionDay As ProductionDay
    Private manualEntries ' As ManualDataPrompt.Result

    Public Shared WordApp As Word.Application
    Private wordDoc As Word.Document

    Private page1_mainTable As Word.Table
    Private page2_mainTable As Word.Table

    Private nbMixInSummary As Integer = 0


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="producionDay"></param>
    ''' <remarks>
    ''' ProgressBar : 5%
    ''' </remarks>
    Public Sub New(producionDay As ProductionDay)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport word (Génération du model)"
        ReportGenerationControl.instance.addStep(5)

        WordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone

        If (IGNIS.ProductionDay.generateModel OrElse Not DOCXModel.fileExists) Then
            Try

                Dim model As New DOCXModel(DOCXReport.WordApp)
                Me.wordDoc = model.generateModel()

            Catch e As Threading.ThreadAbortException
                Me.dispose()
            End Try

        Else
            Me.wordDoc = WordApp.Documents.Open(Constants.Paths.DOCX_MODEL, Nothing, True)
        End If

        Me.productionDay = producionDay

        'Me.manualEntries = ManualDataPrompt.instance.getResult(producionDay.DATE_)

        Me.page1_mainTable = wordDoc.Tables(1)

        Me.page2_mainTable = wordDoc.Tables(2)

    End Sub


    Public Sub generateReport()

        Me.loadTables()

        Me.loadGraphics()

        Me.loadFooterInfo()

        Me.consolidate()

    End Sub

    ' TABLES '

    ' Make private
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' ProgressBar : 35 %
    ''' </remarks>
    Public Sub loadTables()

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Tableau de production)"
        Me.loadProductionTables()
        ReportGenerationControl.instance.addStep(5)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Tableau de temperature)"
        Me.loadTemperatureTable()
        ReportGenerationControl.instance.addStep(5)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Tableau de pourcentage de bitume)"
        Me.loadAsphaltPercentageTable()
        ReportGenerationControl.instance.addStep(5)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Tableau de pourcentage de recycle)"
        Me.loadRecycledTable()
        ReportGenerationControl.instance.addStep(5)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Sommaire de production)"
        Me.loadProductionSummaryTable_Continuous()
        ReportGenerationControl.instance.addStep(5)

        Me.loadConsumptionTable()

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Sommaire de production)"
        Me.loadProductionSummaryTable_batch()
        ReportGenerationControl.instance.addStep(5)

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Sommaire des bitume)"
        Me.loadAsphaltSummaryTable()
        ReportGenerationControl.instance.addStep(5)

        Me.loadRejectsSummary()

        ' If we need a new page or not...
        If (Me.nbMixInSummary < 12) Then
            page2_mainTable.Select()
            WordApp.Selection.MoveDown()
            WordApp.Selection.InsertBreak(Word.WdBreakType.wdPageBreak)
        End If

        Me.loadStopsJustification()

        ReportGenerationControl.instance.PROGRESSION_LABEL_TEXT = "Génération du rapport Word (Sommaire des événements)"
        Me.loadEventsSummaryTable()

    End Sub

    Private Sub loadProductionTables()

        Dim mainCell = Me.page1_mainTable.Rows(1).Cells(1)
        mainCell.Range.Paragraphs(1).Range.Select()

        With WordApp.Selection

            .ParagraphFormat.SpaceAfter = 5

            .MoveRight()
            .MoveLeft()
            .Font.Bold = True
            .Font.Size = 11
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Title & " " & Me.productionDay.DATE_.ToString("d MMMM yyyy", XmlSettings.Settings.LANGUAGE.Culture))

            .InsertBreak(Word.WdBreakType.wdLineBreak)
            .Font.Bold = False
            .Font.Size = 9
            .Font.SmallCaps = False
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_openingHoursText & " : ")
            .Font.Underline = Word.WdUnderline.wdUnderlineSingle

            If (IsNothing(Me.manualEntries)) Then

                .TypeText("    " & "-" & "    ")
                .Font.Underline = Word.WdUnderline.wdUnderlineNone
                .TypeText(" " & Settings.LANGUAGE.General.WordFor_To & " ")
                .Font.Underline = Word.WdUnderline.wdUnderlineSingle
                .TypeText("    " & "-" & ChrW(160) & ChrW(160) & ChrW(160) & ChrW(160))

            Else

                .TypeText("    " & Me.manualEntries.openingHour.ToString("H\hmm") & "    ")
                .Font.Underline = Word.WdUnderline.wdUnderlineNone
                .TypeText(" " & Settings.LANGUAGE.General.WordFor_To & " ")
                .Font.Underline = Word.WdUnderline.wdUnderlineSingle
                .TypeText("    " & Me.manualEntries.closingHour.ToString("H\hmm") & ChrW(160) & ChrW(160) & ChrW(160) & ChrW(160))

            End If



            .InsertBreak(Word.WdBreakType.wdLineBreak)
            .Font.Underline = Word.WdUnderline.wdUnderlineNone
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_productionHoursText & " : ")
            .Font.Underline = Word.WdUnderline.wdUnderlineSingle
            .TypeText("    " & Me.productionDay.START_TIME.ToString("H\hmm") & "    ")
            .Font.Underline = Word.WdUnderline.wdUnderlineNone
            .TypeText(" " & Settings.LANGUAGE.General.WordFor_To & " ")
            .Font.Underline = Word.WdUnderline.wdUnderlineSingle
            .TypeText("    " & Me.productionDay.END_TIME.ToString("H\hmm") & ChrW(160) & ChrW(160) & ChrW(160) & ChrW(160))

            Dim table1 = mainCell.Tables(1)


            ' First row (Timespan)
            table1.Cell(2, 2).Select() ' #Constant
            .TypeText(productionDay.CONTINUOUS_TIMESPAN.ToString("h\:mm"))

            table1.Cell(2, 3).Select()
            .TypeText(productionDay.BATCH_TIMESPAN.ToString("h\:mm"))

            table1.Cell(2, 4).Select()
            .TypeText(productionDay.STOP_TIMESPAN.ToString("h\:mm"))


            ' Second row (Time percentage)
            table1.Cell(3, 2).Select()
            .TypeText(PerOne.UNIT.convert(productionDay.CONTINUOUS_TIMESPAN.TotalMilliseconds / productionDay.DAY_TOTAL_TIMESPAN.TotalMilliseconds, Me.PERCENT_UNIT).ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

            table1.Cell(3, 3).Select()
            .TypeText(PerOne.UNIT.convert(productionDay.BATCH_TIMESPAN.TotalMilliseconds / productionDay.DAY_TOTAL_TIMESPAN.TotalMilliseconds, Me.PERCENT_UNIT).ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

            table1.Cell(3, 4).Select()
            .TypeText(PerOne.UNIT.convert(productionDay.STOP_TIMESPAN.TotalMilliseconds / productionDay.DAY_TOTAL_TIMESPAN.TotalMilliseconds, Me.PERCENT_UNIT).ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)


            ' Number of mix switch ( 3rd row )
            table1.Cell(4, 2).Select()
            .TypeText(productionDay.NUMBER_OF_MIX_SWITCH_CONTINUOUS)

            table1.Cell(4, 3).Select()
            .TypeText(productionDay.NUMBER_OF_MIX_SWITCH_BATCH)

            table1.Cell(4, 4).Select()
            .TypeText(If(productionDay.NUMBER_OF_STOPS = -1, "-", productionDay.NUMBER_OF_STOPS))

            ' Quantity ( 4th row )
            table1.Cell(5, 2).Select()
            .TypeText(productionDay.TOTAL_MASS_CONTINUOUS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            table1.Cell(5, 3).Select()
            .TypeText(productionDay.TOTAL_MASS_BATCH.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            table1.Cell(5, 4).Select()
            .TypeText("-")

            ' Global production speed ( 5th row )
            table1.Cell(6, 2).Select()
            .TypeText(productionDay.TONS_PER_HOUR_CONTINUOUS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            table1.Cell(6, 3).Select()
            .TypeText(productionDay.TONS_PER_HOUR_BATCH.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            table1.Cell(6, 4).Select()
            .TypeText("-")

            ' --------------------              --------------------'
            ' -------------------- SECOND TABLE --------------------'
            ' --------------------              --------------------'

            Dim table2 = mainCell.Tables(2)
            Dim mix As MixStatistics

            ' Mix highlights ( First 3 rows )
            For i = 2 To 4

                If (productionDay.MIX_STATS.Count > i - 2) Then

                    mix = productionDay.MIX_STATS(i - 2)

                    ' Mix name
                    table2.Cell(i, 1).Select()
                    .TypeText(mix.NAME & " (" & mix.ASPHALT_STATS.NAME & ")")

                    ' Total mass
                    table2.Cell(i, 2).Select()
                    .TypeText(mix.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                    ' Production Speed
                    table2.Cell(i, 3).Select()
                    .TypeText(mix.AVERAGE_PRODUCTION_SPEED.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                    ' Production type
                    table2.Cell(i, 4).Select()
                    .TypeText(mix.PRODUCTION_TYPE)

                Else

                    table2.Cell(i, 1).Select()
                    .TypeText("-")

                    table2.Cell(i, 2).Select()
                    .TypeText("-")

                    table2.Cell(i, 3).Select()
                    .TypeText("-")

                    table2.Cell(i, 4).Select()
                    .TypeText("-")

                End If

            Next


            ' Fourth row (Other mix)
            If (productionDay.MIX_STATS.Count > 3) Then

                table2.Cell(5, 2).Select()
                .TypeText(productionDay.OTHER_MIX_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                table2.Cell(5, 3).Select()
                .TypeText(CDbl(productionDay.OTHER_MIX_STATS.TOTAL_MASS / productionDay.OTHER_MIX_STATS.TOTAL_TIME.TotalHours).ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            Else

                table2.Cell(5, 2).Select()
                .TypeText("-")

                table2.Cell(5, 3).Select()
                .TypeText("-")

            End If

            table2.Cell(5, 4).Select()
            .TypeText("-")


            ' Fifth row (Total mix)
            table2.Cell(6, 2).Select()
            .TypeText(productionDay.TOTAL_MIX_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            table2.Cell(6, 3).Select()
            .TypeText(CDbl(productionDay.TOTAL_MIX_STATS.TOTAL_MASS / productionDay.TOTAL_MIX_STATS.TOTAL_TIME.TotalHours).ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            table2.Cell(6, 4).Select()
            .TypeText("-")

            ' Sixth row (Total mix)
            table2.Cell(7, 2).Select()
            .TypeText(Me.manualEntries.mixSold)

            table2.Cell(7, 3).Select()
            .TypeText("-")

            table2.Cell(7, 4).Select()
            .TypeText("-")

            ' Seventh row (Total mix)
            table2.Cell(8, 2).Select()
            .TypeText(Me.manualEntries.mixLeft)

            table2.Cell(8, 3).Select()
            .TypeText("-")

            table2.Cell(8, 4).Select()
            .TypeText("-")

        End With

    End Sub

    Private Sub loadTemperatureTable()

        Dim mainCell = Me.page1_mainTable.Rows(2).Cells(1)
        Dim table1 = mainCell.Tables(1)
        Dim asphalt As AsphaltStatistics

        With WordApp.Selection

            For i = 2 To 4

                If (productionDay.ASPHALT_STATS.Count > i - 2 AndAlso productionDay.ASPHALT_STATS(i - 2).TOTAL_MASS > 0) Then

                    asphalt = productionDay.ASPHALT_STATS(i - 2)

                    ' Header
                    table1.Cell(1, i).Select()
                    .TypeText(asphalt.NAME)

                    ' Set point
                    table1.Cell(2, i).Select()
                    If (asphalt.SET_POINT_TEMPERATURE > Celsius.UNIT.convert(0, Settings.instance.Report.Word.TEMPERATURE_UNIT)) Then
                        .TypeText(asphalt.SET_POINT_TEMPERATURE & _
                                  " " & ChrW(177) & " " & Celsius.UNIT.unitConvert(7, Settings.instance.Report.Word.TEMPERATURE_UNIT) & Settings.instance.Report.Word.TEMPERATURE_UNIT)
                    Else
                        .TypeText("-")
                    End If

                    ' Maximum #settings 7 and C
                    table1.Cell(3, i).Select()
                    If (asphalt.SET_POINT_TEMPERATURE > Celsius.UNIT.convert(0, Settings.instance.Report.Word.TEMPERATURE_UNIT) _
                        AndAlso asphalt.MAX_TEMPERATURE > asphalt.SET_POINT_TEMPERATURE + Celsius.UNIT.unitConvert(7, Settings.instance.Report.Word.TEMPERATURE_UNIT) Or asphalt.MAX_TEMPERATURE > Celsius.UNIT.convert(170, Settings.instance.Report.Word.TEMPERATURE_UNIT)) Then
                        .Font.ColorIndex = Word.WdColorIndex.wdRed
                        .Font.Bold = True
                    End If
                    .TypeText(asphalt.MAX_TEMPERATURE & " " & Me.TEMPERATURE_UNIT)

                    ' Average
                    table1.Cell(4, i).Select()
                    If (asphalt.SET_POINT_TEMPERATURE > Celsius.UNIT.convert(0, Settings.instance.Report.Word.TEMPERATURE_UNIT) AndAlso _
                        (asphalt.AVERAGE_TEMPERATURE > asphalt.SET_POINT_TEMPERATURE + Celsius.UNIT.unitConvert(7, Settings.instance.Report.Word.TEMPERATURE_UNIT) OrElse _
                         asphalt.AVERAGE_TEMPERATURE < asphalt.SET_POINT_TEMPERATURE - Celsius.UNIT.unitConvert(7, Settings.instance.Report.Word.TEMPERATURE_UNIT))) Then

                        .Font.ColorIndex = Word.WdColorIndex.wdRed
                        .Font.Bold = True
                    End If
                    .TypeText(asphalt.AVERAGE_TEMPERATURE.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.TEMPERATURE_UNIT)

                    ' Minimum
                    table1.Cell(5, i).Select()
                    If (asphalt.SET_POINT_TEMPERATURE > Celsius.UNIT.convert(0, Settings.instance.Report.Word.TEMPERATURE_UNIT) _
                        AndAlso asphalt.MIN_TEMPERATURE < asphalt.SET_POINT_TEMPERATURE - Celsius.UNIT.unitConvert(7, Settings.instance.Report.Word.TEMPERATURE_UNIT)) Then
                        .Font.ColorIndex = Word.WdColorIndex.wdRed
                        .Font.Bold = True
                    End If
                    .TypeText(asphalt.MIN_TEMPERATURE & " " & Me.TEMPERATURE_UNIT)

                    ' Under limit
                    table1.Cell(6, i).Select()
                    .TypeText(asphalt.BELOW_TEMPERATURE_LIMIT_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

                    ' Over limit
                    table1.Cell(7, i).Select()
                    .TypeText(asphalt.ABOVE_TEMPERATURE_LIMIT_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

                    ' Out limit
                    table1.Cell(8, i).Select()
                    .TypeText(asphalt.OUT_LIMIT_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)


                    ' Mix no conform qty
                    table1.Cell(9, i).Select()
                    .TypeText(CDbl(asphalt.OUT_TEMPERATURE_LIMIT_MASS).ToString("N0", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.MASS_UNIT)

                    ' Mix qty
                    table1.Cell(10, i).Select()
                    .TypeText(asphalt.BELONGING_MIX_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.MASS_UNIT)

                Else

                    For j = 1 To 10
                        table1.Cell(j, i).Select()
                        .TypeText("-")
                    Next

                End If

            Next

        End With

    End Sub

    Private Sub loadAsphaltPercentageTable()

        Dim mainCell = Me.page1_mainTable.Rows(3).Cells(1)
        Dim table1 = mainCell.Tables(1)
        Dim mix As MixStatistics

        With WordApp.Selection

            For i = 2 To 4

                If (productionDay.MIX_STATS.Count > i - 2 AndAlso productionDay.MIX_STATS(i - 2).TOTAL_MASS > 0) Then

                    mix = productionDay.MIX_STATS(i - 2)

                    ' Header
                    table1.Cell(1, i).Select()
                    .ParagraphFormat.WordWrap = False
                    .TypeText(mix.NAME)

                    ' Set point
                    table1.Cell(2, i).Select()
                    If (Double.IsNaN(mix.ASPHALT_MAX_SET_POINT_PERCENTAGE)) Then

                        .TypeText("-")

                    Else
                        If (mix.ASPHALT_MAX_SET_POINT_PERCENTAGE.Equals(mix.ASPHALT_MIN_SET_POINT_PERCENTAGE)) Then

                            .TypeText(mix.ASPHALT_MAX_SET_POINT_PERCENTAGE.ToString("N2", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

                        Else

                            .TypeText(mix.ASPHALT_MIN_SET_POINT_PERCENTAGE.ToString("N2", XmlSettings.Settings.LANGUAGE.Culture) & _
                                      " " & Settings.LANGUAGE.General.WordFor_To & " " & mix.ASPHALT_MAX_SET_POINT_PERCENTAGE.ToString("N2", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

                        End If
                    End If

                    ' Maximum
                    table1.Cell(3, i).Select()
                    If (mix.hasSetPointPercentage AndAlso mix.ASPHALT_MAX_PERCENTAGE > mix.ASPHALT_MAX_SET_POINT_PERCENTAGE + MixStatistics.CONTROL_PRECISION) Then
                        .Font.Bold = True
                        .Font.ColorIndex = Word.WdColorIndex.wdRed
                    End If

                    .TypeText(mix.ASPHALT_MAX_PERCENTAGE.ToString("N2", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

                    ' Average
                    table1.Cell(4, i).Select()
                    If (mix.hasSetPointPercentage AndAlso mix.ASPHALT_AVERAGE_PERCENTAGE > mix.ASPHALT_MAX_SET_POINT_PERCENTAGE + MixStatistics.CONTROL_PRECISION Or _
                        mix.hasSetPointPercentage AndAlso mix.ASPHALT_AVERAGE_PERCENTAGE < mix.ASPHALT_MIN_SET_POINT_PERCENTAGE - MixStatistics.CONTROL_PRECISION) Then

                        .Font.Bold = True
                        .Font.ColorIndex = Word.WdColorIndex.wdRed
                    End If
                    .TypeText(mix.ASPHALT_AVERAGE_PERCENTAGE.ToString("N2", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)

                    ' Minimum
                    table1.Cell(5, i).Select()
                    If (mix.hasSetPointPercentage AndAlso mix.ASPHALT_MIN_PERCENTAGE < mix.ASPHALT_MIN_SET_POINT_PERCENTAGE - MixStatistics.CONTROL_PRECISION) Then
                        .Font.Bold = True
                        .Font.ColorIndex = Word.WdColorIndex.wdRed
                    End If
                    .TypeText(mix.ASPHALT_MIN_PERCENTAGE.ToString("N2", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)


                    ' out tolerence
                    table1.Cell(6, i).Select()
                    If (mix.hasSetPointPercentage) Then
                        .TypeText(mix.ASPHALT_OUT_LIMIT_TOLERENCE_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)
                    Else
                        .TypeText("-")
                    End If

                    ' out control
                    table1.Cell(7, i).Select()
                    If (mix.hasSetPointPercentage) Then
                        .TypeText(mix.ASPHALT_OUT_LIMIT_CONTROLE_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.PERCENT_UNIT)
                    Else
                        .TypeText("-")
                    End If

                    ' Out control mass
                    table1.Cell(8, i).Select()
                    If (mix.hasSetPointPercentage) Then
                        .TypeText(mix.OUT_CONTROL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.MASS_UNIT)
                    Else
                        .TypeText("-")
                    End If

                    ' Total mass
                    table1.Cell(9, i).Select()
                    .TypeText(mix.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture) & " " & Me.MASS_UNIT)

                Else

                    For j = 1 To 9
                        table1.Cell(j, i).Select()
                        .TypeText("-")
                    Next

                End If

            Next

        End With

    End Sub

    Private Sub loadRecycledTable()

        Dim mainCell = Me.page2_mainTable.Cell(1, 1)
        Dim table = mainCell.Tables(1)
        Dim mix As MixStatistics

        Dim insertTotal As Boolean = False
        With WordApp.Selection

            ' First 3 rows
            For i = 2 To 4

                If (productionDay.MIX_STATS.Count > i - 2 AndAlso productionDay.MIX_STATS(i - 2).TOTAL_MASS > 0) Then

                    mix = productionDay.MIX_STATS(i - 2)

                    ' Mix name
                    table.Cell(i, 1).Select()
                    .TypeText(mix.NAME & " (" & mix.ASPHALT_STATS.NAME & ")")

                    ' Mix mass
                    table.Cell(i, 2).Select()
                    .TypeText(mix.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                    ' Set Point recycled percentage
                    table.Cell(i, 3).Select()
                    If (Double.IsNaN(mix.SET_POINT_RECYCLED_PERCENTAGE)) Then
                        .TypeText("-")
                    Else
                        .TypeText(mix.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                    End If


                    If (Double.IsNaN(mix.AVERAGE_RECYCLED_PERCENTAGE)) Then

                        ' Average recycled percentage
                        table.Cell(i, 4).Select()
                        .TypeText("-")

                    Else

                        ' Average recycled percentage
                        table.Cell(i, 4).Select()
                        .TypeText(mix.AVERAGE_RECYCLED_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))

                        insertTotal = True

                    End If

                    ' Recycled mass
                    If (Double.IsNaN(mix.TOTAL_RECYCLED_MASS)) Then
                        table.Cell(i, 5).Select()
                        .TypeText("-")
                    Else
                        table.Cell(i, 5).Select()
                        .TypeText(mix.TOTAL_RECYCLED_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                    End If


                Else

                    table.Cell(i, 1).Select()
                    .TypeText("-")

                    table.Cell(i, 2).Select()
                    .TypeText("-")

                    table.Cell(i, 3).Select()
                    .TypeText("-")

                    table.Cell(i, 4).Select()
                    .TypeText("-")

                    table.Cell(i, 5).Select()
                    .TypeText("-")

                End If

            Next


            ' Fourth row (Other mix)
            If (productionDay.MIX_STATS.Count > 3) Then

                ' Mix mass
                table.Cell(5, 2).Select()
                .TypeText(productionDay.OTHER_MIX_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                ' Set Point recycled percentage
                table.Cell(5, 3).Select()
                .TypeText("-")

                ' Average recycled percentage
                table.Cell(5, 4).Select()
                If (Double.IsNaN(productionDay.OTHER_MIX_STATS.AVERAGE_RECYCLED_PERCENTAGE)) Then
                    .TypeText("-")
                Else
                    .TypeText(productionDay.OTHER_MIX_STATS.AVERAGE_RECYCLED_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                End If

                ' Recycled mass
                table.Cell(5, 5).Select()
                If (Double.IsNaN(productionDay.OTHER_MIX_STATS.TOTAL_RECYCLED_MASS)) Then
                    .TypeText("-")
                Else
                    .TypeText(productionDay.OTHER_MIX_STATS.TOTAL_RECYCLED_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                End If

            Else

                table.Cell(5, 2).Select()
                .TypeText("-")

                table.Cell(5, 3).Select()
                .TypeText("-")

                table.Cell(5, 4).Select()
                .TypeText("-")

                table.Cell(5, 5).Select()
                .TypeText("-")

            End If

            table.Cell(5, 4).Select()
            .TypeText("-")


            ' Fifth row (Total mix)
            ' Mix mass
            table.Cell(6, 2).Select()
            .TypeText(productionDay.TOTAL_MIX_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            ' Set Point recycled percentage
            table.Cell(6, 3).Select()
            .TypeText("-")

            ' Average recycled percentage
            table.Cell(6, 4).Select()
            If (Double.IsNaN(productionDay.TOTAL_MIX_STATS.AVERAGE_RECYCLED_PERCENTAGE)) Then
                .TypeText("-")
            Else
                .TypeText(productionDay.TOTAL_MIX_STATS.AVERAGE_RECYCLED_PERCENTAGE.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
            End If

            ' Recycled mass
            table.Cell(6, 5).Select()
            If (insertTotal) Then

                .TypeText(productionDay.TOTAL_MIX_STATS.TOTAL_RECYCLED_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            Else
                .TypeText("-")

            End If

        End With

    End Sub

    Private Sub loadConsumptionTable()

        Dim mainCell = Me.page2_mainTable.Cell(2, 1)
        Dim table = mainCell.Tables(1)

        With WordApp.Selection

            table.Cell(6, 2).Select()
            .TypeText(productionDay.TOTAL_MIX_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

            If (Not IsNothing(Me.manualEntries) AndAlso Not Double.IsNaN(Me.manualEntries.fuel)) Then
                table.Cell(6, 3).Select()
                .TypeText(Me.manualEntries.fuel)

                table.Cell(6, 4).Select()
                ' .TypeText((Me.manualEntries.fuel / productionDay.TOTAL_MIX_STATS.TOTAL_MASS).ToString("N2", XmlSettings.Settings.LANGUAGE.Culture))

            Else
                table.Cell(6, 3).Select()
                .TypeText("-")
            End If

        End With


    End Sub

    Private Sub loadProductionSummaryTable_Continuous()

        If (Settings.instance.Usine.DataFiles.LOG.ACTIVE) Then

            Dim continuousTableCell = Me.page2_mainTable.Cell(3, 1)
            Dim continuousTable = continuousTableCell.Tables(1)

            Dim nonNullFeeds As New List(Of FeedersStatistics)

            Dim feedIndex As Integer

            Dim highLightRow As Boolean = True

            With WordApp.Selection

                ' Column titles
                For i = 0 To productionDay.TOTAL_MIX_STATS.CONTINUOUS_FEEDERS_STATS.Count - 1

                    Dim feedStat = productionDay.TOTAL_MIX_STATS.CONTINUOUS_FEEDERS_STATS(i)

                    If (feedStat.TOTAL_MASS > 0) Then

                        nonNullFeeds.Add(feedStat)

                        continuousTable.Columns.Last.Previous.Select()
                        .InsertColumnsRight()

                        continuousTable.Cell(1, 6 + feedIndex + 1).Select()

                        feedIndex += 1

                        If (Not IsNothing(feedStat.LOCATION)) Then

                            .TypeText(feedStat.LOCATION)

                        End If

                        If (Not IsNothing(feedStat.MATERIAL_NAME)) Then

                            .InsertBreak(Word.WdBreakType.wdLineBreak)
                            .TypeText(feedStat.MATERIAL_NAME)

                        End If

                        If (IsNothing(feedStat.LOCATION) And IsNothing(feedStat.MATERIAL_NAME)) Then

                            .TypeText("Feeder " & i + 1 & " (" & Me.MASS_UNIT & ")")

                        Else

                            .TypeText(" (" & Me.MASS_UNIT & ")")

                        End If

                    End If

                Next

                continuousTable.Columns.Width = continuousTableCell.Width * 2 / (7 + 1.5 + nonNullFeeds.Count)
                continuousTable.Columns(1).Width = continuousTableCell.Width * 2 / (7 + 2 + nonNullFeeds.Count) * 3 / 2
                continuousTable.Columns(2).Width = continuousTableCell.Width * 2 / (7 + 2 + nonNullFeeds.Count) * 3 / 2
                continuousTable.Columns(3).Width = continuousTableCell.Width * 2 / (7 + 2 + nonNullFeeds.Count) * 3 / 2

                Dim currentRow As Word.Row = Nothing

                Dim totalAsphalt As Double

                For Each mix In productionDay.MIX_STATS

                    If (mix.PRODUCTION_TYPE.Equals(Settings.LANGUAGE.General.WordFor_Continuous) AndAlso mix.TOTAL_MASS > 0) Then

                        continuousTable.Rows.Last.Select()
                        .InsertRowsAbove()

                        currentRow = continuousTable.Rows.Last.Previous

                        ' Formula
                        If (Not IsNothing(mix.FORMULA_NAME)) Then
                            currentRow.Cells(1).Select()
                            .TypeText(mix.FORMULA_NAME)
                        End If

                        ' Mix name
                        currentRow.Cells(2).Select()
                        .Font.Bold = False
                        .TypeText(mix.NAME)

                        ' Asphalt name
                        currentRow.Cells(3).Select()
                        .TypeText(mix.ASPHALT_STATS.NAME)

                        ' Recycled percentage
                        currentRow.Cells(4).Select()
                        .TypeText(mix.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                        ' Mix qty
                        currentRow.Cells(5).Select()
                        .TypeText(mix.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                        ' Asphalt qty
                        currentRow.Cells(6).Select()
                        .TypeText(mix.ASPHALT_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                        totalAsphalt += mix.ASPHALT_STATS.TOTAL_MASS

                        ' Feeders
                        mix.CONTINUOUS_FEEDERS_STATS.Sort()

                        feedIndex = 0

                        Dim typeZero As Boolean
                        For Each totalFeed In nonNullFeeds

                            typeZero = True

                            For Each feedStats In mix.CONTINUOUS_FEEDERS_STATS
                                If (feedStats.INDEX = totalFeed.INDEX) Then
                                    currentRow.Cells(6 + 1 + feedIndex).Select()

                                    If (feedStats.TOTAL_MASS > 10 Or Not feedStats.TOTAL_MASS > 0) Then
                                        .TypeText(feedStats.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                                    Else
                                        .TypeText(feedStats.TOTAL_MASS.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                                    End If

                                    feedIndex += 1
                                    typeZero = False
                                    Exit For
                                End If
                            Next

                            If (typeZero) Then
                                currentRow.Cells(6 + 1 + feedIndex).Select()
                                .TypeText(0)
                                feedIndex += 1
                            End If
                        Next

                        ' Fuel
                        currentRow.Cells(6 + 1 + feedIndex).Select()
                        .TypeText("-")

                        currentRow.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                        If (highLightRow) Then
                            currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                        End If

                        highLightRow = Not highLightRow

                    End If

                Next

                If (nonNullFeeds.Count > 0) Then


                    ' Last row (TOTAL)
                    currentRow = continuousTable.Rows.Last

                    ' Mix qty
                    currentRow.Cells(5).Select()
                    .TypeText(productionDay.TOTAL_MASS_CONTINUOUS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                    ' Asphalt qty
                    currentRow.Cells(6).Select()
                    .TypeText(totalAsphalt.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))


                    feedIndex = 0
                    For Each feed In nonNullFeeds

                        currentRow.Cells(6 + 1 + feedIndex).Select()
                        .TypeText(feed.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                        feedIndex += 1
                    Next

                    If (highLightRow) Then
                        currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                    End If

                    ' Fuel
                    currentRow.Cells(6 + 1 + feedIndex).Select()
                    .TypeText("-")

                    continuousTable.Select()
                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                    Me.nbMixInSummary += continuousTable.Rows.Count - 2

                Else

                    continuousTable.Delete()

                    continuousTableCell.Range.Select()
                    .MoveRight()
                    .MoveLeft()
                    .Font.SmallCaps = False
                    .Font.Bold = False
                    .Font.Size = 8
                    .Text = Settings.LANGUAGE.WordReport.MixSummarySection_NoContinuousMix

                End If
            End With ' End with app.selection

        End If

    End Sub

    Private Sub loadProductionSummaryTable_batch()

        If (Settings.instance.Usine.DataFiles.MDB.ACTIVE OrElse _
            Settings.instance.Usine.DataFiles.CSV.ACTIVE) Then

            Dim batchTableCell = Me.page2_mainTable.Cell(4, 1)
            Dim batchTable = batchTableCell.Tables(1)

            ' -----------------------------------
            '-----------------------------------
            Dim nonNullFeeds As New List(Of FeedersStatistics)

            Dim feedIndex As Integer

            Dim highLightRow As Boolean = True

            With WordApp.Selection

                productionDay.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS.Sort()

                ' Column titles
                For i = 0 To productionDay.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS.Count - 1

                    Dim feedStat = productionDay.TOTAL_MIX_STATS.BATCH_FEEDERS_STATS(i)

                    If (feedStat.TOTAL_MASS > 0) Then

                        nonNullFeeds.Add(feedStat)

                        batchTable.Columns.Last.Previous.Select()
                        .InsertColumnsRight()

                        batchTable.Cell(1, 6 + feedIndex + 1).Select()

                        feedIndex += 1

                        If (Not IsNothing(feedStat.LOCATION)) Then

                            .TypeText(feedStat.LOCATION)

                        End If

                        If (Not IsNothing(feedStat.MATERIAL_NAME)) Then

                            .InsertBreak(Word.WdBreakType.wdLineBreak)
                            .TypeText(feedStat.MATERIAL_NAME)

                        End If

                        If (IsNothing(feedStat.LOCATION) And IsNothing(feedStat.MATERIAL_NAME)) Then

                            .TypeText("Feeder " & i + 1 & " (" & Me.MASS_UNIT & ")")

                        Else

                            .TypeText(" (" & Me.MASS_UNIT & ")")

                        End If

                    End If

                Next

                batchTable.Columns.Width = batchTableCell.Width * 2 / (7 + 1.5 + nonNullFeeds.Count)
                batchTable.Columns(1).Width = batchTableCell.Width * 2 / (7 + 2 + nonNullFeeds.Count) * 3 / 2
                batchTable.Columns(2).Width = batchTableCell.Width * 2 / (7 + 2 + nonNullFeeds.Count) * 3 / 2
                batchTable.Columns(3).Width = batchTableCell.Width * 2 / (7 + 2 + nonNullFeeds.Count) * 3 / 2

                Dim currentRow As Word.Row = Nothing

                Dim totalAsphalt As Double

                For Each mix In productionDay.MIX_STATS

                    If (mix.PRODUCTION_TYPE.Equals(Settings.LANGUAGE.General.WordFor_Batch)) Then

                        batchTable.Rows.Last.Select()
                        .InsertRowsAbove()

                        currentRow = batchTable.Rows.Last.Previous

                        ' Formula
                        If (Not IsNothing(mix.FORMULA_NAME)) Then
                            currentRow.Cells(1).Select()
                            .TypeText(mix.FORMULA_NAME)
                        End If

                        ' Mix name
                        currentRow.Cells(2).Select()
                        .Font.Bold = False
                        .TypeText(mix.NAME)

                        ' Asphalt name
                        currentRow.Cells(3).Select()
                        .TypeText(mix.ASPHALT_STATS.NAME)

                        ' Recycled percentage
                        currentRow.Cells(4).Select()
                        If (Double.IsNaN(mix.SET_POINT_RECYCLED_PERCENTAGE)) Then
                            .TypeText("-")
                        Else
                            .TypeText(mix.SET_POINT_RECYCLED_PERCENTAGE.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                        End If

                        ' Mix qty
                        currentRow.Cells(5).Select()
                        .TypeText(mix.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                        ' Asphalt qty
                        currentRow.Cells(6).Select()
                        If (mix.ASPHALT_STATS.TOTAL_MASS > 10) Then
                            .TypeText(mix.ASPHALT_STATS.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                        Else
                            .TypeText(mix.ASPHALT_STATS.TOTAL_MASS.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                        End If

                        totalAsphalt += mix.ASPHALT_STATS.TOTAL_MASS

                        ' Feeders
                        mix.BATCH_FEEDERS_STATS.Sort()

                        feedIndex = 0

                        Dim typeZero As Boolean
                        For Each totalFeed In nonNullFeeds

                            typeZero = True

                            For Each feedStats In mix.BATCH_FEEDERS_STATS
                                If (feedStats.INDEX = totalFeed.INDEX) Then
                                    currentRow.Cells(6 + 1 + feedIndex).Select()

                                    If (feedStats.TOTAL_MASS > 10 Or Not feedStats.TOTAL_MASS > 0) Then
                                        .TypeText(feedStats.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                                    Else
                                        .TypeText(feedStats.TOTAL_MASS.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                                    End If

                                    feedIndex += 1
                                    typeZero = False
                                    Exit For
                                End If
                            Next

                            If (typeZero) Then
                                currentRow.Cells(6 + 1 + feedIndex).Select()
                                .TypeText(0)
                                feedIndex += 1
                            End If
                        Next

                        ' Fuel
                        currentRow.Cells(6 + 1 + feedIndex).Select()
                        .TypeText("-")

                        currentRow.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                        If (highLightRow) Then
                            currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                        End If

                        highLightRow = Not highLightRow

                    End If

                Next

                If (nonNullFeeds.Count > 0) Then

                    ' Last row
                    currentRow = batchTable.Rows.Last

                    ' Mix qty
                    currentRow.Cells(5).Select()
                    .TypeText(productionDay.TOTAL_MASS_BATCH.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                    ' Asphalt qty
                    currentRow.Cells(6).Select()
                    If (totalAsphalt > 10) Then
                        .TypeText(totalAsphalt.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                    Else
                        .TypeText(totalAsphalt.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                    End If

                    feedIndex = 0
                    For Each feed In nonNullFeeds

                        currentRow.Cells(6 + 1 + feedIndex).Select()
                        If (feed.TOTAL_MASS > 10) Then
                            .TypeText(feed.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))

                        Else
                            .TypeText(feed.TOTAL_MASS.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                        End If
                        feedIndex += 1
                    Next

                    ' Fuel
                    currentRow.Cells(6 + 1 + feedIndex).Select()
                    .TypeText("-")

                    If (highLightRow) Then
                        currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                    End If

                    batchTable.Select()
                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                    Me.nbMixInSummary += batchTable.Rows.Count - 2

                Else

                    batchTable.Delete()

                    batchTableCell.Range.Select()
                    .MoveRight()
                    .MoveLeft()

                    .Font.SmallCaps = False
                    .Font.Bold = False
                    .Font.Size = 8

                    .Text = Settings.LANGUAGE.WordReport.MixSummarySection_NoBatchMix

                End If

            End With ' End with app.selection

        End If

    End Sub

    Private Sub loadAsphaltSummaryTable()

        Dim mainCell = Me.page2_mainTable.Cell(5, 1)
        Dim table = mainCell.Tables(1)

        Dim highLightRow As Boolean = True

        Dim totalAsphaltMass As Double

        With WordApp.Selection

            Dim currentRow As Word.Row
            For Each asphaltStat In productionDay.ASPHALT_STATS

                table.Rows.Last.Select()
                .InsertRowsAbove()

                currentRow = table.Rows.Last.Previous

                ' Asphalt tank
                currentRow.Cells(1).Select()
                .Font.Bold = False
                .TypeText(asphaltStat.TANK)

                ' asphalt name
                currentRow.Cells(2).Select()
                .TypeText(asphaltStat.NAME)

                ' asphalt mass
                currentRow.Cells(3).Select()
                If (asphaltStat.TOTAL_MASS > 10) Then
                    .TypeText(asphaltStat.TOTAL_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                Else
                    .TypeText(asphaltStat.TOTAL_MASS.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                End If

                totalAsphaltMass += asphaltStat.TOTAL_MASS

                currentRow.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                If (highLightRow) Then
                    currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                End If

                highLightRow = Not highLightRow

            Next

            ' Recycled Aphalt
            If (Not Double.IsNaN(productionDay.RECYCLED_ASPHALT_MASS)) Then

                table.Rows.Last.Select()
                .InsertRowsAbove()

                currentRow = table.Rows.Last.Previous

                ' Asphalt tank
                currentRow.Cells(1).Select()
                .Font.Bold = False
                .TypeText(productionDay.RECYCLED_MIX_FEED_NAME)

                ' asphalt mass
                currentRow.Cells(3).Select()
                If (productionDay.RECYCLED_ASPHALT_MASS > 10) Then
                    .TypeText(productionDay.RECYCLED_ASPHALT_MASS.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
                Else
                    .TypeText(productionDay.RECYCLED_ASPHALT_MASS.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
                End If

                totalAsphaltMass += productionDay.RECYCLED_ASPHALT_MASS

                currentRow.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                If (highLightRow) Then
                    currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                End If

                highLightRow = Not highLightRow

            End If

            ' Last row
            currentRow = table.Rows.Last
            currentRow.Cells(3).Select()
            If (totalAsphaltMass > 10) Then
                .TypeText(totalAsphaltMass.ToString("N0", XmlSettings.Settings.LANGUAGE.Culture))
            Else
                .TypeText(totalAsphaltMass.ToString("N1", XmlSettings.Settings.LANGUAGE.Culture))
            End If

            If (highLightRow) Then
                currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            End If

        End With

    End Sub

    Private Sub loadRejectsSummary()

        Dim mainCell = Me.page2_mainTable.Cell(5, 2)
        Dim table = mainCell.Tables(1)


        With WordApp.Selection

            table.Cell(2, 2).Select()
            .TypeText(Me.manualEntries.mixRejected)

            table.Cell(3, 2).Select()
            .TypeText(Me.manualEntries.aggregatesRejected)

            table.Cell(4, 2).Select()
            .TypeText(Me.manualEntries.fillerRejected)


        End With



    End Sub

    Private Sub loadStopsJustification()

        ' Select the last table
        Dim table = wordDoc.Tables(wordDoc.Tables.Count - 3)
        table.Select()

        Dim highLightRow As Boolean = True

        With WordApp.Selection

            Dim currentRow As Word.Row

            Dim stopEvents = Events.STOP_EVENTS
            stopEvents.Sort()

            ' 2 because we don't want to ask for the justification of the last stop
            For i = 0 To stopEvents.Count - 2

                Dim event_ = stopEvents(i)

                table.Rows.Last.Select()
                .InsertRowsBelow()

                currentRow = table.Rows.Last
                currentRow.Range.Font.Bold = False
                currentRow.Height = 36

                ' Start
                currentRow.Cells(1).Select()
                .TypeText(event_.TIME.ToString("HH:mm:ss"))

                If (IsNothing(event_.NEXT_START)) Then
                    currentRow.Cells(2).Select()
                    .TypeText("-")

                    currentRow.Cells(3).Select()
                    .TypeText("-")
                Else
                    currentRow.Cells(2).Select()
                    .TypeText(event_.NEXT_START.TIME.ToString("HH:mm:ss"))

                    currentRow.Cells(3).Select()
                    .TypeText(event_.DURATION.ToString("h\:mm\:ss"))
                End If


                If (highLightRow) Then
                    currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                Else
                    currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorWhite
                End If
                highLightRow = Not highLightRow

            Next

            With table.Rows.First.Borders(Word.WdBorderType.wdBorderBottom)
                .LineStyle = Word.WdLineStyle.wdLineStyleSingle
                .LineWidth = Word.WdLineWidth.wdLineWidth100pt
            End With

            table.Columns(3).Borders(Word.WdBorderType.wdBorderRight).Color = Word.WdColor.wdColorBlack
            table.Columns(3).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            table.Columns(3).Borders(Word.WdBorderType.wdBorderRight).LineWidth = Word.WdLineWidth.wdLineWidth100pt

            table.Columns(4).Borders(Word.WdBorderType.wdBorderRight).Color = Word.WdColor.wdColorBlack
            table.Columns(4).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            table.Columns(4).Borders(Word.WdBorderType.wdBorderRight).LineWidth = Word.WdLineWidth.wdLineWidth100pt

            table.Columns(5).Borders(Word.WdBorderType.wdBorderRight).Color = Word.WdColor.wdColorBlack
            table.Columns(5).Borders(Word.WdBorderType.wdBorderRight).LineStyle = Word.WdLineStyle.wdLineStyleSingle
            table.Columns(5).Borders(Word.WdBorderType.wdBorderRight).LineWidth = Word.WdLineWidth.wdLineWidth100pt

            ' If the number of rows in the table is EXACTLY 14, then we need to insert a page
            ' break before the next title or it will not be placed correctly
            If (stopEvents.Count - 1 = 14) Then
                .MoveDown(Word.WdUnits.wdLine, 9) ' 9 is the right number of 'move down' to be right above the next title
                .InsertBreak(Word.WdBreakType.wdPageBreak)
            End If

        End With

    End Sub

    Private Sub loadEventsSummaryTable()

        ' Select the before last table
        Dim table = wordDoc.Tables(wordDoc.Tables.Count - 1)
        table.Select()

        Dim highLightRow As Boolean = True

        With WordApp.Selection

            Dim currentRow As Word.Row

            Dim importantEvents = Events.IMPORTANT_EVENTS
            ' importantEvents.Add(New SingleEvent(ManualDataPrompt.instance.getResult(Me.productionDay.DATE_).openingHour, "Ouverture"))
            ' importantEvents.Add(New SingleEvent(ManualDataPrompt.instance.getResult(Me.productionDay.DATE_).closingHour, "Fermeture"))
            importantEvents.Sort()

            If (Settings.instance.Report.Word.EVENTS_ACTIVE) Then

                ' Progress bar
                Dim progressBarStep As Double = 1 / (importantEvents.Count - 1) * 10

                For i = 0 To importantEvents.Count - 1

                    ReportGenerationControl.instance.addStep(progressBarStep)

                    Dim event_ = importantEvents(i)

                    table.Rows.Last.Select()
                    .InsertRowsAbove()

                    currentRow = table.Rows.Last.Previous

                    currentRow.Cells(1).Select()
                    .Font.Bold = False
                    .TypeText(i + 1)

                    If (TypeOf event_ Is StopEvent) Then

                        Dim stopEv = DirectCast(event_, StopEvent)

                        ' Start
                        currentRow.Cells(5).Select()
                        .TypeText(stopEv.TIME.ToString("HH:mm:ss"))

                        If (IsNothing(stopEv.NEXT_START)) Then

                            currentRow.Cells(6).Select()
                            .TypeText("-")

                            currentRow.Cells(7).Select()
                            .TypeText("-")

                        Else

                            currentRow.Cells(2).Select()
                            .TypeText(ChrW(10003))

                            currentRow.Cells(6).Select()
                            .TypeText(stopEv.NEXT_START.TIME.ToString("HH:mm:ss"))

                            currentRow.Cells(7).Select()
                            .TypeText(stopEv.DURATION.ToString("h\:mm\:ss"))

                        End If


                        ' Comment
                        currentRow.Cells(8).Select()
                        .TypeText(event_.ToString)


                    ElseIf (TypeOf event_ Is MixRecipeChangeEvent) Then

                        currentRow.Cells(3).Select()
                        .TypeText(ChrW(10003))

                        ' Start
                        currentRow.Cells(5).Select()
                        .TypeText(event_.TIME.ToString("HH:mm:ss"))

                        ' Stop
                        currentRow.Cells(6).Select()
                        .TypeText("-")

                        ' Duration
                        currentRow.Cells(7).Select()
                        .TypeText("-")

                        ' Comment
                        currentRow.Cells(8).Select()
                        .TypeText(event_.ToString)

                    ElseIf (TypeOf event_ Is MixChangeEvent) Then

                        currentRow.Cells(4).Select()
                        .TypeText(ChrW(10003))

                        ' Start
                        currentRow.Cells(5).Select()
                        .TypeText(event_.TIME.ToString("HH:mm:ss"))

                        ' Stop
                        currentRow.Cells(6).Select()
                        .TypeText("-")

                        ' Duration
                        currentRow.Cells(7).Select()
                        .TypeText("-")

                        ' Comment
                        currentRow.Cells(8).Select()
                        .TypeText(event_.ToString)

                    Else

                        ' Start
                        currentRow.Cells(5).Select()
                        .TypeText(event_.TIME.ToString("HH:mm:ss"))

                        ' Stop
                        currentRow.Cells(6).Select()
                        .TypeText("-")

                        ' Duration
                        currentRow.Cells(7).Select()
                        .TypeText("-")

                        ' Comment
                        currentRow.Cells(8).Select()
                        .TypeText(event_.ToString)

                    End If

                    currentRow.Borders(Word.WdBorderType.wdBorderBottom).LineStyle = Word.WdLineStyle.wdLineStyleNone

                    If (highLightRow) Then
                        currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
                    End If
                    highLightRow = Not highLightRow

                Next

                ' Last row
                currentRow = table.Rows.Last

                currentRow.Cells(2).Select()
                .TypeText(Events.NB_STOPS)

                currentRow.Cells(3).Select()
                .TypeText(Events.NB_MIX_RECIPE_CHANGE)

                currentRow.Cells(4).Select()
                .TypeText(Events.NB_MIX_CHANGE)

                currentRow.Cells(7).Select()
                .TypeText(Events.IMPORTANT_EVENTS_DURATION.ToString("h\:mm\:ss"))

            Else ' Events not active

                ' Last row
                currentRow = table.Rows.Last

                currentRow.Cells(1).Select()
                .TypeText(Events.NB_STOPS)

                currentRow.Cells(2).Select()
                .TypeText(Events.NB_MIX_RECIPE_CHANGE)

                currentRow.Cells(3).Select()
                .TypeText(Events.NB_MIX_CHANGE)

                currentRow.Cells(4).Select()
                .TypeText(Events.STOP_EVENTS_DURATION.ToString("h\:mm\:ss"))

                ReportGenerationControl.instance.addStep(10)

            End If

            If (highLightRow) Then
                currentRow.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            End If

        End With

    End Sub

    Private Sub loadFooterInfo()

        For Each section As Word.Section In Me.wordDoc.Sections

            section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Select()

            WordApp.Selection.Tables(1).Cell(1, 3).Select()

            WordApp.Selection.Font.SmallCaps = False
            WordApp.Selection.Font.ColorIndex = Word.WdColorIndex.wdGray50
            WordApp.Selection.Font.Size = 8

            WordApp.Selection.TypeText(Settings.LANGUAGE.WordReport.Footer_Right & " " & DateTime.Now.ToString("yyyy-MM-dd HH:mm", XmlSettings.Settings.LANGUAGE.Culture))

        Next

    End Sub

    ' GRAPHICS '

    ' Make private
    Public Sub loadGraphics()

        Me.loadProductionGraphics()
        Me.loadTemperatureGraphics()
        Me.loadAsphaltPercentageGraphics()
        Me.loadRecycledPercentageGraphics()
        Me.loadFuelConsumptionGraphics()

        ReportGenerationControl.instance.addStep(5)


    End Sub

    Private Sub loadProductionGraphics()

        Dim mainCell = Me.page1_mainTable.Rows(1).Cells(2)
        mainCell.Select()

        With WordApp.Selection

            Dim graphic2 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.PRODUCTION_SPEED_GRAPHIC, False, True, .Range)
            graphic2.Width = mainCell.Width

            Dim graphic1 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ACCUMULATED_MASS_GRAPHIC, False, True, .Range)
            graphic1.Width = mainCell.Width

        End With

    End Sub

    Private Sub loadTemperatureGraphics()

        Dim mainCell = Me.page1_mainTable.Rows(2).Cells(2)
        mainCell.Select()

        With WordApp.Selection

            Dim graphic2 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_VARIATION_GRAPHIC, False, True, .Range)
            graphic2.Width = mainCell.Width

            Dim graphic1 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.MIX_TEMPERATURE_GRAPHIC, False, True, .Range)
            graphic1.Width = mainCell.Width

        End With


    End Sub

    Private Sub loadAsphaltPercentageGraphics()

        Dim mainCell = Me.page1_mainTable.Rows(3).Cells(2)
        mainCell.Select()

        With WordApp.Selection

            Dim graphic2 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ASPHALT_PERCENTAGE_VARIATION_GRAPHIC, False, True, .Range)
            graphic2.Width = mainCell.Width

            Dim graphic1 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.ASPHALT_PERCENTAGE_GRAPHIC, False, True, .Range)
            graphic1.Width = mainCell.Width

        End With

    End Sub

    Private Sub loadRecycledPercentageGraphics()

        Dim mainCell = Me.page2_mainTable.Rows(1).Cells(2)
        mainCell.Select()

        With WordApp.Selection

            Dim graphic2 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.RECYCLED_PERCENTAGE_GRAPHIC, False, True, .Range)
            graphic2.Width = mainCell.Width

        End With

    End Sub

    Private Sub loadFuelConsumptionGraphics()

        Dim mainCell = Me.page2_mainTable.Rows(2).Cells(2)
        mainCell.Select()

        With WordApp.Selection

            Dim graphic2 = .InlineShapes.AddPicture(Constants.Paths.OUTPUT_DIRECTORY & Constants.Output.Graphics.SaveAsNames.FUEL_CONSUMPTION_GRAPHIC, False, True, .Range)
            graphic2.Width = mainCell.Width

        End With

    End Sub

    Private Sub consolidate()

        wordDoc.Bookmarks.Item("\endofdoc").Select()

        wordDoc.ActiveWindow.View.Type = Word.WdViewType.wdPrintView

        WordApp.Selection.WholeStory()
        WordApp.Selection.Font.Name = "Arial"

    End Sub

    Public Sub saveAs(fullPath As String)

        Try

            Me.wordDoc.SaveAs2(fullPath)

        Catch ex As Runtime.InteropServices.COMException

            If (UIExceptionHandler.instance.handle(New OpenedFileException(OpenedFileException.FileType.Docx, ex))) Then
                saveAs(fullPath)
            End If

        End Try

    End Sub

    Public Sub saveAsPDF(fullPath As String)

        Try
            Me.wordDoc.SaveAs2(fullPath, Word.WdSaveFormat.wdFormatPDF)

            If (XmlSettings.Settings.instance.Report.Word.OPEN_WHEN_DONE) Then
                System.Diagnostics.Process.Start(fullPath)
            End If

        Catch ex As Runtime.InteropServices.COMException

            If (UIExceptionHandler.instance.handle(New OpenedFileException(OpenedFileException.FileType.PDF, ex))) Then
                saveAsPDF(fullPath)
            End If

        End Try

    End Sub

    Public Sub dispose()

        For Each document As Word.Document In DOCXReport.WordApp.Documents

            document.Close(False)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(document)
            document = Nothing

        Next

        Me.wordDoc = Nothing

    End Sub

    Public Shared Sub killApp()
        If (Not IsNothing(WordApp)) Then
            WordApp.Application.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp)
            WordApp = Nothing
        End If
    End Sub

End Class
