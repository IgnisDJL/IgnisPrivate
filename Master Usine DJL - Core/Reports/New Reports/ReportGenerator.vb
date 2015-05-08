Imports Microsoft.Office.Interop

Public MustInherit Class ReportGenerator

    ' --- Word Application --- '
    Private _wordApp As Word.Application
    Private _wordDoc As Word.Document

    'Private currentReportType As ReportType
    Private _reportFormater As ReportFormater

    Protected Property CurrentWordView As WordView = WordView.None

    Protected Sub New(reportFormater As ReportFormater)

        'Me.currentReportType = reportType
        Me._reportFormater = reportFormater

        Threading.Thread.CurrentThread.CurrentCulture = XmlSettings.Settings.LANGUAGE.Culture
    End Sub

    Protected Sub initializeWordApplication()

        Me._wordApp = New Word.Application()
        _wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsAll
        Me.WordApp.Visible = True

    End Sub

    Public Sub disposeOfRessources()

        killDocumentObjects()

        killApplicationObject()

    End Sub

    Public ReadOnly Property Formater As ReportFormater
        Get
            Return Me._reportFormater
        End Get
    End Property

    Protected Sub killDocumentObjects()

        For Each document As Word.Document In WordApp.Documents

            document.Close(False)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(document)
            document = Nothing

        Next

        Me._wordDoc = Nothing

    End Sub

    Private Sub killApplicationObject()

        If (Not IsNothing(WordApp)) Then
            WordApp.Application.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp)
            _wordApp = Nothing
        End If

    End Sub

    Public Enum WordView
        None = 0
        Normal = 1
        HeaderFooter = 2
    End Enum

    '' #refactor - move to constants
    'Public Enum ReportType
    '    SummaryDailyReport = 1
    '    CompleteDailyReport = 2
    '    SummaryPeriodicRepord = 3
    '    CompletePeriodicReport = 4
    '    ManualDataReport = 5
    'End Enum

    Protected ReadOnly Property WordApp As Word.Application
        Get
            Return _wordApp
        End Get
    End Property

    Protected Property WordDoc As Word.Document
        Get
            Return _wordDoc
        End Get
        Set(document As Word.Document)
            Me._wordDoc = document
        End Set
    End Property


End Class
