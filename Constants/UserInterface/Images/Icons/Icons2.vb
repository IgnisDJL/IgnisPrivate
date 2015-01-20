Imports System.Drawing

Namespace Constants.UI.Images

    Public Class Icons2

        Private Shared ReadOnly ICONS_DIRECTORY As String = Constants.Paths.IMAGES_DIRECTORY & "Icons\"

        Public Shared ReadOnly ADD As Icon = New Icon(ICONS_DIRECTORY & "Add.ico")
        Public Shared ReadOnly CSV_FILE As Icon = New Icon(ICONS_DIRECTORY & "CSV_File.ico")
        Public Shared ReadOnly DELETE As Icon = New Icon(ICONS_DIRECTORY & "Delete.ico")
        Public Shared ReadOnly SUMMARY_DAILY_REPORT As Icon = New Icon(ICONS_DIRECTORY & "DOC_Daily_Report.ico")
        Public Shared ReadOnly SUMMARY_PERIODIC_REPORT As Icon = New Icon(ICONS_DIRECTORY & "DOC_Periodic_Report.ico")
        Public Shared ReadOnly EDIT As Icon = New Icon(ICONS_DIRECTORY & "Edit.ico")
        Public Shared ReadOnly EVENT_FILE As Icon = New Icon(ICONS_DIRECTORY & "Event_File.ico")
        Public Shared ReadOnly GOOD As Icon = New Icon(ICONS_DIRECTORY & "Good.ico")
        Public Shared ReadOnly IMPORT As Icon = New Icon(ICONS_DIRECTORY & "Import.ico")
        Public Shared ReadOnly LOG_FILE As Icon = New Icon(ICONS_DIRECTORY & "LOG_File.ico")
        Public Shared ReadOnly MAIL As Icon = New Icon(ICONS_DIRECTORY & "Mail.ico")
        Public Shared ReadOnly MDB_FILE As Icon = New Icon(ICONS_DIRECTORY & "MDB_File.ico")
        Public Shared ReadOnly MULTIPLE_DAILY_REPORTS As Icon = New Icon(ICONS_DIRECTORY & "Multiple_Daily_Reports.ico")
        Public Shared ReadOnly MULTIPLE_PERIODIC_REPORTS As Icon = New Icon(ICONS_DIRECTORY & "Multiple_Periodic_Reports.ico")
        Public Shared ReadOnly NEXT_ As Icon = New Icon(ICONS_DIRECTORY & "Next.ico")
        Public Shared ReadOnly READONLY_SUMMARY_DAILY_REPORT As Icon = New Icon(ICONS_DIRECTORY & "PDF_Daily_Report.ico")
        Public Shared ReadOnly READONLY_SUMMARY_PERIODIC_REPORT As Icon = New Icon(ICONS_DIRECTORY & "PDF_Periodic_Report.ico")
        Public Shared ReadOnly PREVIOUS As Icon = New Icon(ICONS_DIRECTORY & "Previous.ico")
        Public Shared ReadOnly REFRESH As Icon = New Icon(ICONS_DIRECTORY & "Refresh.ico")
        Public Shared ReadOnly SAVE As Icon = New Icon(ICONS_DIRECTORY & "Save.ico")
        Public Shared ReadOnly SETTINGS As Icon = New Icon(ICONS_DIRECTORY & "Settings.ico")
        Public Shared ReadOnly WARNING As Image = Image.FromFile(ICONS_DIRECTORY & "Warning.ico")
        Public Shared ReadOnly WRONG As Icon = New Icon(ICONS_DIRECTORY & "Wrong.ico")
        Public Shared ReadOnly COMPLETE_DAILY_REPORT As Icon = New Icon(ICONS_DIRECTORY & "XLS_Daily_Report.ico")
        Public Shared ReadOnly COMPLETE_PERIODIC_REPORT As Icon = New Icon(ICONS_DIRECTORY & "XLS_Periodic_Report.ico")
        Public Shared ReadOnly UNKNOWN As Icon = New Icon(ICONS_DIRECTORY & "Unknown.ico")

    End Class

End Namespace