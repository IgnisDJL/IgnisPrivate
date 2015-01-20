Namespace Constants.Output

    Public Class Excel

        ''' <summary></summary>
        Public Class DataSheet

            ''' <summary></summary>
            Public Const SHEET_NAME = "Données"

            ''' <summary></summary>
            Public Const DATA_TABLE_TOP_LEFT_CELL_NAME = "DataTableStart"

            ''' <summary></summary>
            Public Const CSV_DATA_TABLE_TOP_LEFT_CELL = "csvDataTableStart"

            ''' <summary></summary>
            Public Const LOG_DATA_TABLE_TOP_LEFT_CELL = "logDataTableStart"

        End Class ' End Datasheet

        ''' <summary></summary>
        Public Const NEW_REPORT_NAME = "Feuille d'analyse"

        ''' <summary></summary>
        Public Const REPORT_EXTENSION = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook

        ''' <summary></summary>
        Public Const REPORT_EXTEnSION_WITH_MACRO = Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled

    End Class ' End excel

End Namespace
