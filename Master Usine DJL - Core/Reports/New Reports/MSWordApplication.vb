Imports Microsoft.Office.Interop

Public Class MSWordApplication

    Private _wordApp As Word.Application
    Private _bookMarks As Constants.Reports.BookMarks.DailySummaryReport

    Public Sub New()
        Me._bookMarks = New Constants.Reports.BookMarks.DailySummaryReport()
    End Sub

    Public Sub initialize()

        If (IsNothing(_wordApp)) Then

            _wordApp = New Word.Application()


        End If

        _wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone

        Dim url = "C:\Users\owner\DJL\Nouveau rapports\Models\DailySummaryReport_Template - Copy.docx"

        Dim doc = WordApp.Documents.Open(url, False, True) ', False, "", "", False, "", "", Word.WdOpenFormat.wdOpenFormatAuto)
        Me._bookMarks.initialize(_wordApp)
        WordApp.Visible = True

    End Sub

    Public Sub doTest()



        BookMarks.ProductionDayDate.Text = "SIMON EST BEAU!"

    End Sub

    Public ReadOnly Property WordApp As Word.Application
        Get
            Return Me._wordApp
        End Get
    End Property

    Public ReadOnly Property BookMarks As Constants.Reports.BookMarks.DailySummaryReport
        Get
            Return Me._bookMarks
        End Get
    End Property

End Class
