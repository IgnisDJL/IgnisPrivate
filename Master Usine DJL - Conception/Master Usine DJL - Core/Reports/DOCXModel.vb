Imports Microsoft.Office.Interop
Imports IGNIS.XmlSettings

Public Class DOCXModel

    Private wordApp As Word.Application
    Private wordDoc As Word.Document

    Private headerParg As Word.Paragraph
    Private usineTitleParg As Word.Paragraph

    Private page1_mainTable As Word.Table
    Private page2_mainTable As Word.Table

    Private reportSettings As ReportNode = Settings.instance.Report

    Private Shared ReadOnly logoLoc = Constants.Paths.IMAGES_DIRECTORY & "Logo-DJL.jpg"

    Public Sub New(app As Word.Application)
        Me.wordApp = app
    End Sub

    Public Function generateModel() As Word.Document

        wordDoc = wordApp.Documents.Add

        wordDoc.PageSetup.TopMargin = wordApp.InchesToPoints(0.25)
        wordDoc.PageSetup.BottomMargin = wordApp.InchesToPoints(0.25)
        wordDoc.PageSetup.LeftMargin = wordApp.InchesToPoints(0.25)
        wordDoc.PageSetup.RightMargin = wordApp.InchesToPoints(0.25)

        Me.insertPageHeader()

        Me.page1_mainTable = wordDoc.Tables.Add(wordDoc.Bookmarks.Item("\endofdoc").Range, 3, 2)
        page1_mainTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
        page1_mainTable.Columns.Width = wordApp.InchesToPoints(4)
        page1_mainTable.Rows.Height = wordApp.InchesToPoints(3.25)
        page1_mainTable.Select()
        wordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        Me.insertProduction()
        Me.insertTemperature()
        Me.insertAsphaltPrecentage()

        wordDoc.Paragraphs.Last.Range.ParagraphFormat.LineSpacing = 0.7

        wordDoc.Bookmarks.Item("\endofdoc").Select()
        wordApp.Selection.InsertParagraphAfter()
        wordDoc.Paragraphs.Last.Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle

        Me.page2_mainTable = wordDoc.Tables.Add(wordDoc.Bookmarks.Item("\endofdoc").Range, 2, 2)
        Me.page2_mainTable.Select()
        wordDoc.Tables.Add(wordDoc.Bookmarks.Item("\endofdoc").Range, 2, 1)
        wordDoc.Tables.Add(wordDoc.Bookmarks.Item("\endofdoc").Range, 1, 2)

        page2_mainTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly

        page2_mainTable.Rows(1).Height = wordApp.InchesToPoints(2)
        page2_mainTable.Rows(1).Cells.Width = wordApp.InchesToPoints(4)

        page2_mainTable.Rows(2).Height = wordApp.InchesToPoints(2)
        page2_mainTable.Rows(2).Cells.Width = wordApp.InchesToPoints(4)

        page2_mainTable.Rows(3).HeightRule = Word.WdRowHeightRule.wdRowHeightAuto
        page2_mainTable.Rows(3).Cells.Width = wordApp.InchesToPoints(8)

        page2_mainTable.Rows(4).HeightRule = Word.WdRowHeightRule.wdRowHeightAuto
        page2_mainTable.Rows(4).Cells.Width = wordApp.InchesToPoints(8)

        page2_mainTable.Rows(5).HeightRule = Word.WdRowHeightRule.wdRowHeightAuto
        page2_mainTable.Rows(5).Cells.Width = wordApp.InchesToPoints(4)


        page2_mainTable.Select()
        wordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

        Me.insertRecycling()
        Me.insertConsumption()
        Me.insertProductionSummary_continuous()
        Me.insertProductionSummary_batch()
        Me.insertAsphaltSummary()
        Me.insertRejectsSummary()

        wordDoc.Bookmarks.Item("\endofdoc").Range.Select()
        wordApp.Selection.InsertParagraphAfter()

        Me.insertStopsJustification()

        wordDoc.Bookmarks.Item("\endofdoc").Range.Select()
        wordApp.Selection.InsertParagraphAfter()

        Me.insertEventsSummary()

        wordDoc.Bookmarks.Item("\endofdoc").Range.Select()
        wordApp.Selection.InsertParagraphAfter()

        Me.insertSignatures()

        Me.insertPageFooter()

        wordDoc.Bookmarks.Item("\endofdoc").Select()
        wordDoc.ActiveWindow.View.Type = Word.WdViewType.wdPrintView

        Me.save()

        Return Me.wordDoc

    End Function

    Private Sub insertPageHeader()

        wordDoc.PageSetup.HeaderDistance = 54 - 18

        For Each section As Word.Section In Me.wordDoc.Sections

            section.Headers(Word.WdHeaderFooterIndex.wdHeaderFooterEvenPages).Range.Select()
            wordDoc.ActiveWindow.View.Type = Word.WdViewType.wdPrintView

            With wordApp.Selection

                Dim inlinelogo = .InlineShapes.AddPicture(logoLoc)
                Dim logo = inlinelogo.ConvertToShape()
                logo.Height = wordApp.InchesToPoints(0.5)
                logo.Width = wordApp.InchesToPoints(1.18)
                logo.Top = -18
                logo.Left = 0

                .Font.Size = 11
                .Font.Bold = True
                .ParagraphFormat.SpaceAfter = 0
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                .InsertParagraphAfter()

                .Font.SmallCaps = True
                .Font.ColorIndex = Word.WdColorIndex.wdBlack
                .Font.Size = 12
                .Font.Name = "Arial"
                .ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter

                .TypeText(Settings.LANGUAGE.WordReport.Header & " " & Settings.instance.Usine.PLANT_NAME & " (" & Settings.instance.Usine.PLANT_ID & ")")

            End With


        Next


    End Sub

    Private Sub insertPageFooter()

        wordDoc.PageSetup.FooterDistance = wordApp.LinesToPoints(1)

        For Each section As Word.Section In Me.wordDoc.Sections

            section.Footers(Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range.Select()

            With wordApp.Selection

                Dim footerTable = .Tables.Add(.Range, 1, 3)
                footerTable.Rows.WrapAroundText = True

                With footerTable.Range

                    .Font.SmallCaps = True
                    .Font.ColorIndex = Word.WdColorIndex.wdBlack
                    .Font.Size = 10
                    .Font.Name = "Arial"
                    .ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle
                    .ParagraphFormat.LineUnitBefore = 0
                    .ParagraphFormat.SpaceBefore = 0
                    .ParagraphFormat.SpaceAfter = 0
                    .ParagraphFormat.LineUnitAfter = 0
                    .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom
                End With

                footerTable.Rows.Height = 5
                footerTable.Columns(1).Width = 1 / 4 * wordApp.InchesToPoints(4 * 2)
                footerTable.Columns(2).Width = 2 / 4 * wordApp.InchesToPoints(4 * 2)
                footerTable.Columns(3).Width = 1 / 4 * wordApp.InchesToPoints(4 * 2)

                footerTable.Cell(1, 2).Select()
                .TypeText(Settings.LANGUAGE.WordReport.Footer_Middle & " ")
                .Fields.Add(.Range, Word.WdFieldType.wdFieldPage)

            End With

        Next

        wordDoc.PageSetup.FooterDistance = wordApp.LinesToPoints(1)


    End Sub

    Private Sub insertProduction()

        Dim mainCell = Me.page1_mainTable.Rows(1).Cells(1)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = False
            .Font.Bold = False
            .Font.Underline = False
            .TypeParagraph()

            Dim table1 = mainCell.Tables.Add(.Range, 6, 4)
            table1.Columns.Width = mainCell.Width * 0.2
            table1.Columns(1).Width = mainCell.Width * 0.4
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 12

            table1.Range.Font.Size = 8

            With table1.Range
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End With

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Columns.First.Cells.Borders(Word.WdBorderType.wdBorderRight))
            table1.Rows.First.Select()
            .Font.Bold = True
            table1.Columns.First.Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            ' Table header
            table1.Rows(1).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table1_ProductionMode)

            table1.Rows(1).Cells(2).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Continuous)

            table1.Rows(1).Cells(3).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Batch)

            table1.Rows(1).Cells(4).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Stops)


            ' First row
            table1.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(2).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table1_Duration)


            ' Second row
            table1.Rows(3).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table1_TimePercentage)


            ' Third row
            table1.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(4).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table1_MixSwitchAndStops)


            ' Fourth row
            table1.Rows(5).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .TypeText(Settings.LANGUAGE.General.WordFor_Quantity & " (" & Settings.instance.Report.Word.MASS_UNIT & ")")


            ' Fifth row
            table1.Rows(6).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(6).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table1_ProductionSpeed & " (" & Settings.instance.Report.Word.PRODUCTION_SPEED_UNIT & ")") ' <- settings for units


            ' Interval between tables
            mainCell.Range.InsertParagraphAfter()
            .MoveDown()
            .ParagraphFormat.LineSpacing = 1
            .ParagraphFormat.LineUnitBefore = 0
            .ParagraphFormat.SpaceBeforeAuto = False
            .ParagraphFormat.SpaceBefore = 0
            .ParagraphFormat.SpaceAfter = 3
            .MoveDown()


            Dim table2 As Word.Table = mainCell.Tables.Add(.Range, 8, 4)
            table2.Columns.Width = mainCell.Width * 0.2
            table2.Columns(1).Width = mainCell.Width * 0.4
            table2.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table2.Rows.Height = 12
            table2.Range.Font.Size = 8

            With table2.Range
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End With

            Me.setBorder(table2.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table2.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table2.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table2.Columns.First.Cells.Borders(Word.WdBorderType.wdBorderRight))
            table2.Rows.First.Select()
            .Font.Bold = True
            table2.Columns.First.Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            ' Table header
            table2.Rows(1).Height = table2.Rows.Height * 2
            table2.Rows(1).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .TypeText(Settings.LANGUAGE.General.WordFor_Mixes)

            table2.Rows(1).Cells(2).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Quantity & " (" & Settings.instance.Report.Word.MASS_UNIT & ")")

            table2.Rows(1).Cells(3).Select()
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table2_ProductionSpeed & " (" & Settings.instance.Report.Word.PRODUCTION_SPEED_UNIT & ")")

            table2.Rows(1).Cells(4).Select()
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table2_ProductionMode)


            ' First row
            table2.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15


            ' Second row


            ' Third row
            table2.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15


            ' Fourth row
            table2.Rows(5).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Others)


            ' Fifth row
            table2.Rows(6).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table2.Rows(6).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.PhraseFor_TotalQuantity)

            ' Sixth row
            table2.Rows(7).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table2_MassSold)

            ' Seventh row
            table2.Rows(8).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table2.Rows(8).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.ProductionSection_Table2_MassLeft)

        End With

    End Sub

    Private Sub insertTemperature()

        Dim mainCell = Me.page1_mainTable.Rows(2).Cells.Item(1)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Title)

            Dim table1 = mainCell.Tables.Add(.Range, 10, 4)
            table1.Columns.Width = mainCell.Width * 0.2
            table1.Columns(1).Width = mainCell.Width * 0.4
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 12

            table1.Range.Font.Size = 8
            table1.Range.Font.SmallCaps = False
            table1.Range.Font.Bold = False

            With table1.Range
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End With

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Columns.First.Cells.Borders(Word.WdBorderType.wdBorderRight))
            table1.Rows.First.Select()
            .Font.Bold = True
            table1.Columns.First.Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            ' Table header
            table1.Rows(1).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .TypeText(Settings.LANGUAGE.General.WordFor_Indicators)

            table1.Rows(1).Cells(2).Select()
            .Font.Bold = True

            table1.Rows(1).Cells(3).Select()
            .Font.Bold = True

            table1.Rows(1).Cells(4).Select()
            .Font.Bold = True


            ' First row
            table1.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(2).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Table_SetPointTemperature)


            ' Second row
            table1.Rows(3).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Maximum)


            ' Third row
            table1.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(4).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Average)


            ' Fourth row
            table1.Rows(5).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Minimum)


            ' Fifth row
            table1.Rows(6).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(6).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Table_UnderLimitPercentage)


            ' Sixth row
            table1.Rows(7).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Table_OverLimitPercentage)


            ' Seventh row
            table1.Rows(8).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(8).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Table_OutLimitsPercentage)

            ' heighth row
            table1.Rows(9).Height = table1.Rows.Height * 2
            table1.Rows(9).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Table_OutLimitsMass)

            ' nineth row
            table1.Rows(10).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(10).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.PhraseFor_TotalQuantity)

            ' Table note
            mainCell.Range.InsertParagraphAfter()
            mainCell.Range.Select()
            .MoveRight()
            .MoveLeft()
            .MoveLeft()

            .Font.Size = 8
            .Font.Italic = True
            .Font.Bold = False
            .Font.SmallCaps = False
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .ParagraphFormat.LineUnitBefore = 0
            .ParagraphFormat.SpaceBefore = 0
            .ParagraphFormat.SpaceAfter = 0
            .TypeText(Settings.LANGUAGE.WordReport.TemperatureSection_Note)

            ' Notes field
            .InsertParagraphAfter()
            .MoveDown()
            Dim notesTable As Word.Table = mainCell.Tables.Add(.Range, 2, 1)
            notesTable.Columns.Width = mainCell.Width
            notesTable.Rows.Height = wordApp.LinesToPoints(1.6)
            notesTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly

            For Each row As Word.Row In notesTable.Rows
                With row.Borders(Word.WdBorderType.wdBorderBottom)
                    .Color = Word.WdColor.wdColorBlack
                    .LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    .LineWidth = Word.WdLineWidth.wdLineWidth075pt
                End With
            Next

            notesTable.Rows(1).Cells(1).Select()
            notesTable.Rows(1).Cells(1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            With wordApp.Selection
                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                .Font.Bold = True
                .ParagraphFormat.SpaceAfter = 0
                .TypeText(Settings.LANGUAGE.General.WordFor_Notes & " : ")
            End With

        End With

    End Sub

    Private Sub insertAsphaltPrecentage()

        Dim mainCell = Me.page1_mainTable.Rows(3).Cells.Item(1)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltPercentageSection_Title)

            Dim table1 = mainCell.Tables.Add(.Range, 9, 4)
            table1.Columns.Width = mainCell.Width * 0.2
            table1.Columns(1).Width = mainCell.Width * 0.4
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 12

            table1.Range.Font.Size = 8
            table1.Range.Font.SmallCaps = False
            table1.Range.Font.Bold = False

            With table1.Range
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End With

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Columns.First.Cells.Borders(Word.WdBorderType.wdBorderRight))
            table1.Rows.First.Select()
            .Font.Bold = True

            table1.Columns.First.Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            ' Table header
            table1.Rows(1).Height = 19
            table1.Rows(1).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .TypeText(Settings.LANGUAGE.General.WordFor_Indicators)

            table1.Rows(1).Cells(2).Select()
            .Font.Bold = True

            table1.Rows(1).Cells(3).Select()
            .Font.Bold = True

            table1.Rows(1).Cells(4).Select()
            .Font.Bold = True

            ' First row
            table1.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(2).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltPercentageSection_Table_SetPointPercentage)


            ' Second row
            table1.Rows(3).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Maximum)


            ' Third row
            table1.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(4).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Average)


            ' Fourth row
            table1.Rows(5).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Minimum)


            ' Fifth row
            table1.Rows(6).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(6).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltPercentageSection_Table_OutTolerancePercentage)


            ' Sixth row
            table1.Rows(7).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltPercentageSection_Table_OutControlePercentage)


            ' Eighth row
            table1.Rows(8).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(8).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltPercentageSection_Table_OutControleMass)


            ' Nineth row
            table1.Rows(9).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.PhraseFor_TotalQuantity)

            ' Table notes
            mainCell.Range.InsertParagraphAfter()
            mainCell.Range.Select()
            .MoveRight()
            .MoveLeft()
            .MoveLeft()

            .Font.Size = 8
            .Font.Italic = True
            .Font.Bold = False
            .Font.SmallCaps = False
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
            .ParagraphFormat.LineUnitBefore = 0
            .ParagraphFormat.LineUnitAfter = 0.5
            .ParagraphFormat.SpaceBeforeAuto = 0
            .ParagraphFormat.SpaceBefore = 0
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltPercentageSection_Note)

            ' Notes field
            .InsertParagraphAfter()
            .MoveDown()
            Dim notesTable As Word.Table = mainCell.Tables.Add(.Range, 2, 1)
            notesTable.Columns.Width = mainCell.Width
            notesTable.Rows.Height = wordApp.LinesToPoints(1.6)
            notesTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly

            For Each row As Word.Row In notesTable.Rows
                With row.Borders(Word.WdBorderType.wdBorderBottom)
                    .Color = Word.WdColor.wdColorBlack
                    .LineStyle = Word.WdLineStyle.wdLineStyleSingle
                    .LineWidth = Word.WdLineWidth.wdLineWidth075pt
                End With
            Next

            notesTable.Rows(1).Cells(1).Select()
            notesTable.Rows(1).Cells(1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            With wordApp.Selection
                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                .Font.Bold = True
                .ParagraphFormat.SpaceAfter = 0
                .TypeText(Settings.LANGUAGE.General.WordFor_Notes & " : ")
            End With

        End With

    End Sub

    Private Sub insertRecycling()

        Dim mainCell = Me.page2_mainTable.Rows(1).Cells.Item(1)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.RecyclingSection_Title)

            Dim table1 = mainCell.Tables.Add(.Range, 6, 5)
            table1.Columns.Width = mainCell.Width / 6
            table1.Columns(1).Width = mainCell.Width / 6 * 2
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 12
            ' Normalize table sub???
            table1.Range.Font.Size = 8
            table1.Range.Font.SmallCaps = False
            table1.Range.Font.Bold = False

            With table1.Range
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End With

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Columns.First.Cells.Borders(Word.WdBorderType.wdBorderRight))
            table1.Rows.First.Select()
            .Font.Bold = True

            table1.Columns.First.Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft


            For Each cell As Word.Cell In table1.Rows(1).Cells
                cell.LeftPadding = 0
                cell.RightPadding = 0
            Next


            ' Table header
            table1.Rows(1).Height = table1.Rows.Height * 2
            table1.Rows(1).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .TypeText(Settings.LANGUAGE.General.WordFor_Mixes)

            table1.Rows(1).Cells(2).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Quantity & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

            table1.Rows(1).Cells(3).Select()
            .TypeText(Settings.LANGUAGE.WordReport.RecyclingSection_Table_SetPointRAP & " (" & Me.reportSettings.Word.PERCENT_UNIT & ")")

            table1.Rows(1).Cells(4).Select()
            .TypeText(Settings.LANGUAGE.WordReport.RecyclingSection_Table_AverageRAP & " (" & Me.reportSettings.Word.PERCENT_UNIT & ")")

            table1.Rows(1).Cells(5).Select()
            .TypeText(Settings.LANGUAGE.WordReport.RecyclingSection_Table_RAPMass & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

            ' First row
            table1.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15

            ' Second row

            ' Third row
            table1.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15

            ' Fourth row
            table1.Rows(5).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Others)


            ' Fifth row
            table1.Rows(6).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(6).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.PhraseFor_TotalQuantity)
        End With

    End Sub

    Private Sub insertConsumption()

        Dim mainCell = Me.page2_mainTable.Rows(2).Cells.Item(1)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.FuelConsumptionSection_Title)

            Dim table1 = mainCell.Tables.Add(.Range, 6, 4)
            table1.Columns.Width = mainCell.Width / 5 + 10
            table1.Columns(1).Width = mainCell.Width / 5 * 2 - 30
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 12
            ' Normalize table sub???
            table1.Range.Font.Size = 8
            table1.Range.Font.SmallCaps = False
            table1.Range.Font.Bold = False

            With table1.Range
                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            End With

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Columns.First.Cells.Borders(Word.WdBorderType.wdBorderRight))
            table1.Rows.First.Select()
            .Font.Bold = True

            table1.Columns.First.Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

            For Each cell As Word.Cell In table1.Rows(1).Cells
                cell.LeftPadding = 0
                cell.RightPadding = 0
            Next

            ' Table header
            table1.Rows(1).Height = table1.Rows.Height * 2
            table1.Rows(1).Cells(1).Select()
            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .TypeText(Settings.LANGUAGE.General.WordFor_Mixes)

            table1.Rows(1).Cells(2).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Quantity & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

            table1.Rows(1).Cells(3).Select()
            .TypeText(Settings.LANGUAGE.WordReport.FuelConsumptionSection_FuelConsumption)

            table1.Rows(1).Cells(4).Select()
            .TypeText(Settings.LANGUAGE.WordReport.FuelConsumptionSection_AverageConsumption & " (L/" & Me.reportSettings.Word.MASS_UNIT & ")")


            ' First row
            table1.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15


            ' Second row


            ' Third row
            table1.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15


            ' Fourth row
            table1.Rows(5).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Others)


            ' Fifth row
            table1.Rows(6).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(6).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.PhraseFor_TotalQuantity)

        End With

    End Sub

    Private Sub insertProductionSummary_continuous()

        Dim mainCell = Me.page2_mainTable.Rows(3).Cells.Item(1)
        mainCell.Select()

        If (Settings.instance.Usine.DataFiles.LOG.ACTIVE) Then

            With wordApp.Selection

                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                .Font.SmallCaps = True
                .Font.Size = 11
                .Font.Bold = True
                .TypeText(Settings.LANGUAGE.WordReport.MixSummarySection_ContinuousTitle)

                Dim table1 = mainCell.Tables.Add(.Range, 2, 7)
                table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
                table1.Rows.Height = 12
                ' Normalize table sub???
                table1.Range.Font.Size = 7.5
                table1.Range.Font.SmallCaps = False
                table1.Range.Font.Bold = False

                With table1.Range
                    .ParagraphFormat.LineUnitBefore = 0
                    .ParagraphFormat.LineUnitAfter = 0
                    .ParagraphFormat.SpaceAfterAuto = False
                    .ParagraphFormat.SpaceBeforeAuto = False
                    .ParagraphFormat.SpaceBefore = 0
                    .ParagraphFormat.SpaceAfter = 0
                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                End With

                Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
                Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
                Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
                Me.setBorder(table1.Columns(4).Cells.Borders(Word.WdBorderType.wdBorderRight))

                table1.Rows.First.Select()
                .Font.Bold = True

                For Each cell As Word.Cell In table1.Rows(1).Cells
                    cell.LeftPadding = 0
                    cell.RightPadding = 0
                Next

                ' Table header
                table1.Rows(1).Height = table1.Rows.Height * 3
                table1.Rows(1).Cells(1).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Recipe)

                table1.Rows(1).Cells(2).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Mix)

                table1.Rows(1).Cells(3).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Asphalt)

                table1.Rows(1).Cells(4).Select()
                .TypeText(Settings.LANGUAGE.WordReport.MixSummarySection_SetPointRAP & " (" & Me.reportSettings.Word.PERCENT_UNIT & ")")

                table1.Rows(1).Cells(5).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Quantity & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

                table1.Rows(1).Cells(6).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Asphalt & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

                table1.Rows(1).Cells(7).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Fuel & " (L)")

                table1.Rows.Last.Cells(2).Select()
                .Font.Bold = True
                .TypeText(Settings.LANGUAGE.General.WordFor_Totals)

            End With
        Else

            mainCell.Row.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            mainCell.Height = 1

        End If

    End Sub

    Private Sub insertProductionSummary_batch()

        Dim mainCell = Me.page2_mainTable.Rows(4).Cells.Item(1)
        mainCell.Select()


        If (Settings.instance.Usine.DataFiles.MDB.ACTIVE OrElse _
            Settings.instance.Usine.DataFiles.CSV.ACTIVE) Then

            With wordApp.Selection

                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
                .ParagraphFormat.SpaceBefore = 5
                .Font.SmallCaps = True
                .Font.Size = 11
                .Font.Bold = True
                .TypeText(Settings.LANGUAGE.WordReport.MixSummarySection_BatchTitle)

                Dim table1 = mainCell.Tables.Add(.Range, 2, 7)
                table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
                table1.Rows.Height = 12
                ' Normalize table sub???
                table1.Range.Font.Size = 7.5
                table1.Range.Font.SmallCaps = False
                table1.Range.Font.Bold = False

                With table1.Range
                    .ParagraphFormat.LineUnitBefore = 0
                    .ParagraphFormat.LineUnitAfter = 0
                    .ParagraphFormat.SpaceAfterAuto = False
                    .ParagraphFormat.SpaceBeforeAuto = False
                    .ParagraphFormat.SpaceBefore = 0
                    .ParagraphFormat.SpaceAfter = 0
                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
                End With

                For Each cell As Word.Cell In table1.Rows(1).Cells
                    cell.LeftPadding = 0
                    cell.RightPadding = 0
                Next

                Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
                Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
                Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
                Me.setBorder(table1.Columns(4).Cells.Borders(Word.WdBorderType.wdBorderRight))

                table1.Rows.First.Select()
                .Font.Bold = True

                ' Table header
                table1.Rows(1).Height = table1.Rows.Height * 3
                table1.Rows(1).Cells(1).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Recipe)

                table1.Rows(1).Cells(2).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Mix)

                table1.Rows(1).Cells(3).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Asphalt)

                table1.Rows(1).Cells(4).Select()
                .TypeText(Settings.LANGUAGE.WordReport.MixSummarySection_SetPointRAP & " (" & Me.reportSettings.Word.PERCENT_UNIT & ")")

                table1.Rows(1).Cells(5).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Totals & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

                table1.Rows(1).Cells(6).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Asphalt & " (" & Me.reportSettings.Word.MASS_UNIT & ")")

                table1.Rows(1).Cells(7).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Fuel & " (L)")

                table1.Rows.Last.Cells(2).Select()
                .Font.Bold = True
                .TypeText(Settings.LANGUAGE.General.WordFor_Totals)

            End With

        Else
            mainCell.Row.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            mainCell.Height = 1
        End If

    End Sub

    Private Sub insertAsphaltSummary()

        Dim mainCell = Me.page2_mainTable.Rows(5).Cells.Item(1)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .ParagraphFormat.SpaceBefore = 10
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltSummarySection_Title)

            Dim table1 = mainCell.Tables.Add(.Range, 2, 3)
            table1.Columns.Width = mainCell.Width / 3 - 15
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 12
            ' Normalize table sub???
            table1.Range.Font.Size = 8
            table1.Range.Font.SmallCaps = False
            table1.Range.Font.Bold = False


            table1.Select()
            .ParagraphFormat.LineUnitBefore = 0
            .ParagraphFormat.LineUnitAfter = 0
            .ParagraphFormat.SpaceAfterAuto = False
            .ParagraphFormat.SpaceBeforeAuto = False
            .ParagraphFormat.SpaceBefore = 0
            .ParagraphFormat.SpaceAfter = 0
            .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            .Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            table1.Rows.First.Select()
            .Font.Bold = True


            ' Table header
            table1.Rows(1).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltSummarySection_Table_Tanks)

            table1.Rows(1).Cells(2).Select()
            .TypeText(Settings.LANGUAGE.WordReport.AsphaltSummarySection_Table_AsphaltName)

            table1.Rows(1).Cells(3).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Quantity & " (" & Me.reportSettings.Word.MASS_UNIT & ")")


            ' total's row
            table1.Rows(2).Cells(1).Select()
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.General.WordFor_Totals)

        End With


    End Sub

    Private Sub insertRejectsSummary()

        Dim mainCell = Me.page2_mainTable.Rows(5).Cells(2)
        mainCell.Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .ParagraphFormat.SpaceBefore = 10
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.RejectsSummarySection_Title)

            Dim table1 = mainCell.Tables.Add(.Range, 4, 2)
            table1.Columns.Width = mainCell.Width / 3 - 15
            table1.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table1.Rows.Height = 16
            ' Normalize table sub???
            table1.Range.Font.Size = 8
            table1.Range.Font.SmallCaps = False
            table1.Range.Font.Bold = False

            table1.Select()
            .ParagraphFormat.LineUnitBefore = 0
            .ParagraphFormat.LineUnitAfter = 0
            .ParagraphFormat.SpaceAfterAuto = False
            .ParagraphFormat.SpaceBeforeAuto = False
            .ParagraphFormat.SpaceBefore = 0
            .ParagraphFormat.SpaceAfter = 1
            .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            .Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter

            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table1.Rows.First.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table1.Rows.Last.Cells.Borders(Word.WdBorderType.wdBorderBottom))
            table1.Rows.First.Select()
            .Font.Bold = True


            ' Table header
            table1.Rows(1).Height = 12
            table1.Rows(1).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.RejectsSummarySection_Table_Materials)

            table1.Rows(1).Cells(2).Select()
            .TypeText(Settings.LANGUAGE.WordReport.RejectsSummarySection_Table_RejectedQuantity & " (" & reportSettings.Word.MASS_UNIT.ToString() & ")")


            ' First row
            table1.Rows(2).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(2).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Mix)

            ' Second row
            table1.Rows(3).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Aggregates)

            ' Third row
            table1.Rows(4).Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15
            table1.Rows(4).Cells(1).Select()
            .TypeText(Settings.LANGUAGE.General.WordFor_Filler)


        End With

    End Sub

    Private Sub insertStopsJustification()


        wordDoc.Bookmarks.Item("\endofdoc").Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Title)

            Dim table = .Tables.Add(.Range, 1, 6)
            table.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table.Rows.Height = 36
            table.Rows(1).Height = 20

            With table.Range

                .Font.Size = 8
                .Font.SmallCaps = False
                .Font.Bold = False

                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0

                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

            End With

            table.Rows.First.HeadingFormat = True

            Me.setBorder(table.Rows.First.Borders(Word.WdBorderType.wdBorderTop))
            Me.setBorder(table.Rows.First.Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table.Rows.Last.Borders(Word.WdBorderType.wdBorderBottom))

            table.Rows.First.Select()
            .Font.Bold = True

            table.Columns(1).Width = (1 / 12) * wordApp.InchesToPoints(8)
            table.Cell(1, 1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Table_Start)

            table.Columns(2).Width = (1 / 12) * wordApp.InchesToPoints(8)
            table.Cell(1, 2).Select()
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Table_End)

            table.Columns(3).Width = (1 / 12) * wordApp.InchesToPoints(8)
            table.Cell(1, 3).Select()
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Table_Duration)

            table.Columns(4).Width = (1 / 12) * wordApp.InchesToPoints(8)
            table.Cell(1, 4).Select()
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Table_Code)

            table.Columns(5).Width = (4 / 12) * wordApp.InchesToPoints(8)
            table.Cell(1, 5).Select()
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Table_Description)

            table.Columns(6).Width = (4 / 12) * wordApp.InchesToPoints(8)
            table.Cell(1, 6).Select()
            .TypeText(Settings.LANGUAGE.WordReport.StopsSummarySection_Table_Cause)


            ' Codes table            
            wordDoc.Bookmarks.Item("\endofdoc").Select()
            .MoveDown()
            .InsertParagraphAfter()

            With .ParagraphFormat
                .LineSpacing = 4
                .LineUnitBefore = 0
                .LineUnitAfter = 0
                .SpaceAfterAuto = False
                .SpaceBeforeAuto = False
                .SpaceBefore = 0
                .SpaceAfter = 0
            End With

            .MoveDown()
            .InsertParagraphAfter()

            Dim table2 = .Tables.Add(.Range, 7, 4)
            table2.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            table2.Rows.Height = 10

            With table2.Range

                .Font.Size = 8
                .Font.SmallCaps = False
                .Font.Bold = False

                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0

                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

            End With

            table2.Cell(1, 1).Select()
            .TypeText("1- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_1)

            table2.Cell(2, 1).Select()
            .TypeText("2- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_2)

            table2.Cell(3, 1).Select()
            .TypeText("3- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_3)

            table2.Cell(4, 1).Select()
            .TypeText("4- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_4)

            table2.Cell(5, 1).Select()
            .TypeText("5- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_5)

            table2.Cell(6, 1).Select()
            .TypeText("6- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_6)

            table2.Cell(1, 2).Select()
            .TypeText("7- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_7)

            table2.Cell(2, 2).Select()
            .TypeText("8- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_8)

            table2.Cell(3, 2).Select()
            .TypeText("9- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_9)

            table2.Cell(4, 2).Select()
            .TypeText("10- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_10)

            table2.Cell(5, 2).Select()
            .TypeText("11- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_11)

            table2.Cell(6, 2).Select()
            .TypeText("12- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_12)

            table2.Cell(1, 3).Select()
            .TypeText("13- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_13)

            table2.Cell(2, 3).Select()
            .TypeText("14- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_14)

            table2.Cell(3, 3).Select()
            .TypeText("15- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_15)

            table2.Cell(4, 3).Select()
            .TypeText("16- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_16)

            table2.Cell(5, 3).Select()
            .TypeText("17- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_17)

            table2.Cell(6, 3).Select()
            .TypeText("18- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_18)

            table2.Cell(1, 4).Select()
            .TypeText("19- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_19)

            table2.Cell(2, 4).Select()
            .TypeText("20- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_20)

            table2.Cell(3, 4).Select()
            .TypeText("21- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_21)

            table2.Cell(4, 4).Select()
            .TypeText("22- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_22)

            table2.Cell(5, 4).Select()
            .TypeText("23- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_23)

            table2.Cell(6, 4).Select()
            .TypeText("24- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_24)

            table2.Cell(7, 4).Select()
            .TypeText("25- " & Settings.LANGUAGE.WordReport.StopsSummarySection_Codes_25)

        End With

    End Sub

    Private Sub insertEventsSummary()

        wordDoc.Bookmarks.Item("\endofdoc").Select()

        With wordApp.Selection

            .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter
            .Font.SmallCaps = True
            .Font.Size = 11
            .Font.Bold = True
            .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Title)

            If (Settings.instance.Report.Word.EVENTS_ACTIVE) Then

                Dim table = .Tables.Add(.Range, 2, 8)
                table.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
                table.Rows.Height = 12
                table.Rows(1).Height = table.Rows.Height * 2

                With table.Range

                    .Font.Size = 8
                    .Font.SmallCaps = False
                    .Font.Bold = False

                    .ParagraphFormat.LineUnitBefore = 0
                    .ParagraphFormat.LineUnitAfter = 0
                    .ParagraphFormat.SpaceAfterAuto = False
                    .ParagraphFormat.SpaceBeforeAuto = False
                    .ParagraphFormat.SpaceBefore = 0
                    .ParagraphFormat.SpaceAfter = 0

                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                End With

                table.Rows.First.HeadingFormat = True

                Me.setBorder(table.Rows.First.Borders(Word.WdBorderType.wdBorderTop))
                Me.setBorder(table.Rows.First.Borders(Word.WdBorderType.wdBorderBottom))
                Me.setBorder(table.Rows.Last.Borders(Word.WdBorderType.wdBorderBottom))

                table.Rows.First.Select()
                .Font.Bold = True

                table.Columns(1).Width = (1 / 15) * wordApp.InchesToPoints(8)
                table.Cell(1, 1).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_EventNumber)

                table.Columns(2).Width = (1 / 13.5) * wordApp.InchesToPoints(8)
                table.Cell(1, 2).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Stops)

                table.Columns(3).Width = (1 / 9.5) * wordApp.InchesToPoints(8)
                table.Cell(1, 3).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_MixRecipeChange)

                table.Columns(4).Width = (1 / 9.5) * wordApp.InchesToPoints(8)
                table.Cell(1, 4).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_MixChange)

                table.Columns(5).Width = (1 / 12) * wordApp.InchesToPoints(8)
                table.Cell(1, 5).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_Start)

                table.Columns(6).Width = (1 / 12) * wordApp.InchesToPoints(8)
                table.Cell(1, 6).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_End)

                table.Columns(7).Width = (1 / 12) * wordApp.InchesToPoints(8)
                table.Cell(1, 7).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_Duration)

                table.Columns(8).Width = (1 / 2.5) * wordApp.InchesToPoints(8)
                table.Cell(1, 8).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_Comments)

                table.Cell(2, 1).Select()
                .Font.Bold = True
                .TypeText(Settings.LANGUAGE.General.WordFor_Totals)

            Else

                Dim table = .Tables.Add(.Range, 2, 4)
                table.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
                table.Rows(1).Height = 24
                table.Rows(2).Height = 12

                With table.Range

                    .Font.Size = 8
                    .Font.SmallCaps = False
                    .Font.Bold = False

                    .ParagraphFormat.LineUnitBefore = 0
                    .ParagraphFormat.LineUnitAfter = 0
                    .ParagraphFormat.SpaceAfterAuto = False
                    .ParagraphFormat.SpaceBeforeAuto = False
                    .ParagraphFormat.SpaceBefore = 0
                    .ParagraphFormat.SpaceAfter = 0

                    .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter

                End With

                table.Rows.First.HeadingFormat = True

                Me.setBorder(table.Rows.First.Borders(Word.WdBorderType.wdBorderTop))
                Me.setBorder(table.Rows.First.Borders(Word.WdBorderType.wdBorderBottom))
                Me.setBorder(table.Rows.Last.Borders(Word.WdBorderType.wdBorderBottom))

                table.Rows.First.Select()
                .Font.Bold = True

                table.Rows.Last.Cells.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15

                table.Columns(1).Width = (1 / 4) * wordApp.InchesToPoints(8)
                table.Cell(1, 1).Select()
                .TypeText(Settings.LANGUAGE.General.WordFor_Stops)

                table.Columns(2).Width = (1 / 4) * wordApp.InchesToPoints(8)
                table.Cell(1, 2).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_MixRecipeChange)

                table.Columns(3).Width = (1 / 4) * wordApp.InchesToPoints(8)
                table.Cell(1, 3).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_MixChange)

                table.Columns(4).Width = (1 / 4) * wordApp.InchesToPoints(8)
                table.Cell(1, 4).Select()
                .TypeText(Settings.LANGUAGE.WordReport.EventsSummarySection_Table_StopsDuration)


            End If

        End With

    End Sub

    Private Sub insertSignatures()

        wordDoc.Bookmarks.Item("\endofdoc").Select()

        With wordApp.Selection

            Dim containerTable = .Tables.Add(.Range, 1, 1)
            containerTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly
            containerTable.Rows.Height = 85
            containerTable.Rows.AllowBreakAcrossPages = False

            Dim table = containerTable.Tables.Add(.Range, 4, 4)
            table.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightExactly

            table.Rows(1).Height = 30
            table.Rows(3).Height = 30

            table.Rows(2).Height = 12
            table.Rows(4).Height = 12

            With table.Range

                .Font.Size = 12
                .Font.SmallCaps = False
                .Font.Bold = False

                .ParagraphFormat.LineUnitBefore = 0
                .ParagraphFormat.LineUnitAfter = 0
                .ParagraphFormat.SpaceAfterAuto = False
                .ParagraphFormat.SpaceBeforeAuto = False
                .ParagraphFormat.SpaceBefore = 0
                .ParagraphFormat.SpaceAfter = 0

                .ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalBottom

            End With

            Me.setBorder(table.Rows(1).Cells(2).Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table.Rows(1).Cells(4).Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table.Rows(3).Cells(2).Borders(Word.WdBorderType.wdBorderBottom))
            Me.setBorder(table.Rows(3).Cells(4).Borders(Word.WdBorderType.wdBorderBottom))


            table.Columns(1).Width = 75
            table.Cell(1, 1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.SignatureSection_Signature & " : ")

            table.Columns(2).Width = 220

            table.Columns(3).Width = 50
            table.Cell(1, 3).Select()
            .TypeText(" " & Settings.LANGUAGE.WordReport.SignatureSection_Date & " : ")

            table.Columns(4).Width = 220

            table.Cell(2, 2).Select()
            .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            .Font.Size = 9
            .TypeText(Settings.LANGUAGE.WordReport.SignatureSection_Operator)

            table.Columns(1).Width = 75
            table.Cell(3, 1).Select()
            .TypeText(Settings.LANGUAGE.WordReport.SignatureSection_Signature & " : ")

            table.Columns(3).Width = 50
            table.Cell(3, 3).Select()
            .TypeText(" " & Settings.LANGUAGE.WordReport.SignatureSection_Date & " : ")

            table.Cell(4, 2).Select()
            .Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter
            .Font.Size = 9
            .TypeText(Settings.LANGUAGE.WordReport.SignatureSection_Supervisor)

        End With

    End Sub

    Private Sub setBorder(ByRef border As Word.Border)
        With border
            .Color = Word.WdColor.wdColorBlack
            .LineStyle = Word.WdLineStyle.wdLineStyleSingle
            .LineWidth = Word.WdLineWidth.wdLineWidth100pt
        End With
    End Sub

    Public Sub save()

        Try
            If (System.IO.File.Exists(Constants.Paths.DOCX_MODEL)) Then
                System.IO.File.Delete(Constants.Paths.DOCX_MODEL)
            End If

            wordDoc.SaveAs2(Constants.Paths.DOCX_MODEL)

        Catch ex As System.IO.IOException

            ' Throw model opened exception
            If (UIExceptionHandler.instance.handle(New OpenedFileException(OpenedFileException.FileType.Model_Docx, ex))) Then

                Me.save()

                ' Else do nothing... maybe we could stop the production here... or in the handle() method
            End If

        End Try

    End Sub

    Public Shared Function fileExists() As Boolean
        Return New IO.FileInfo(Constants.Paths.DOCX_MODEL).Exists
    End Function

End Class
