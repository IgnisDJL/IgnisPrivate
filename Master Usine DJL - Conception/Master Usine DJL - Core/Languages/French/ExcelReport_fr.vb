Public Class ExcelReport_fr
    Implements ExcelReport

    Public ReadOnly Property DataSheetName As String Implements ExcelReport.DataSheetName
        Get
            Return "Données"
        End Get
    End Property

    Public ReadOnly Property DataSheetName_Csv As String Implements ExcelReport.DataSheetName_Csv
        Get
            Return "Données (csv)"
        End Get
    End Property

    Public ReadOnly Property DataSheetName_Log As String Implements ExcelReport.DataSheetName_Log
        Get
            Return "Données (log)"
        End Get
    End Property

    Public ReadOnly Property FileName As String Implements ExcelReport.FileName
        Get
            Return "Feuilles d'analyse"
        End Get
    End Property

    Public ReadOnly Property GraphicsSheetName As String Implements ExcelReport.GraphicsSheetName
        Get
            Return "Graphiques"
        End Get
    End Property
End Class
