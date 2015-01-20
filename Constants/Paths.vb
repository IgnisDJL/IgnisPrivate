Namespace Constants

    ''' <summary>Provides access to the important paths (url)</summary>
    Public Class Paths

        ' =============== Private =============== '


        ''' <summary>Absolute path to the directory of the program's executable file (.exe)</summary>
        Public Shared ReadOnly PROGRAM_ROOT As String = My.Application.Info.DirectoryPath & "\"

        ''' <summary>Path to the Ressources folder</summary>
        Private Shared ReadOnly RESSOURCES_DIRECTORY As String = PROGRAM_ROOT & "Ressources\"

        ''' <summary>Path to the Archives folder</summary>
        Private Shared ReadOnly ARCHIVES_DIRECTORY As String = RESSOURCES_DIRECTORY & "Archives\"

        ''' <summary>Path to the Data files archives folder</summary>
        Private Shared ReadOnly DATA_ARCHIVES_DIRECTORY As String = ARCHIVES_DIRECTORY & "Data\"

        ''' <summary>Path to the Reports archives folder</summary>
        Private Shared ReadOnly REPORTS_ARCHIVES_DIRECTORY As String = ARCHIVES_DIRECTORY & "Reports\"

        ''' <summary>Path to the Models folder</summary>
        Private Shared ReadOnly MODELS_DIRECTORY As String = RESSOURCES_DIRECTORY & "Models\"

        ''' <summary>Path to the Report Templates folder</summary>
        Private Shared ReadOnly REPORT_TEMPLATES_DIRECTORY As String = RESSOURCES_DIRECTORY & "Report Templates\"

        ''' <summary>Path to the Settings folder</summary>
        Private Shared ReadOnly SETTINGS_DIRECTORY As String = RESSOURCES_DIRECTORY & "Settings\"


        ' =============== Public =============== '


        ' ----- Datafiles ----- '

        ''' <summary>Path to the CSV files archives folder</summary>
        Public Shared ReadOnly CSV_ARCHIVES_DIRECTORY As String = DATA_ARCHIVES_DIRECTORY & "CSV\"

        ''' <summary>Path to the LOG files archives folder</summary>
        Public Shared ReadOnly LOG_ARCHIVES_DIRECTORY As String = DATA_ARCHIVES_DIRECTORY & "LOG\"

        ''' <summary>Path to the MDB files archives folder</summary>
        Public Shared ReadOnly MDB_ARCHIVES_DIRECTORY As String = DATA_ARCHIVES_DIRECTORY & "MDB\"

        ''' <summary>Path to the Events files archives folder</summary>
        Public Shared ReadOnly EVENTS_ARCHIVES_DIRECTORY As String = DATA_ARCHIVES_DIRECTORY & "Events\"


        ' ----- Reports ----- '

        ''' <summary>Path to the extended reports archives folder</summary>
        Public Shared ReadOnly EXTENDED_REPORTS_ARCHIVES_DIRECTORY As String = REPORTS_ARCHIVES_DIRECTORY & "Extended\"

        ''' <summary>Path to the summary reports archives folder</summary>
        Public Shared ReadOnly SUMMARY_REPORTS_ARCHIVES_DIRECTORY As String = REPORTS_ARCHIVES_DIRECTORY & "Summary\"

        ''' <summary>Path to the summary daily report template file</summary>
        Public Shared ReadOnly SUMMARY_DAILY_REPORT_TEMPLATE As String = REPORT_TEMPLATES_DIRECTORY & "DailySummaryReport.tpl.docx"


        ' ----- Images ----- '

        ''' <summary>Path to the Images folder</summary>
        Public Shared ReadOnly IMAGES_DIRECTORY As String = RESSOURCES_DIRECTORY & "Images\"

        ''' <summary>Path to the Icons folder</summary>
        Public Shared ReadOnly ICONS_DIRECTORY As String = IMAGES_DIRECTORY & "Icons\"


        ' ----- Settings ----- '

        ''' <summary>Path to the settings.xml file</summary>
        Public Shared ReadOnly SETTINGS_FILE As String = SETTINGS_DIRECTORY & "settings.xml"


        ' ----- Models ----- '

        ''' <summary>Path to the ModelCSV.xlsx file</summary>
        Public Shared ReadOnly CSV_MODEL As String = MODELS_DIRECTORY & "ModelCSV.xlsx"

        ''' <summary>Path to the ModelLOG.xlsx file</summary>
        Public Shared ReadOnly LOG_MODEL As String = MODELS_DIRECTORY & "ModelLOG.xlsx"

        ''' <summary>Path to the ModelHybrid.xlsx file</summary>
        Public Shared ReadOnly HYBRID_MODEL As String = MODELS_DIRECTORY & "ModelHybrid.xlsx"

        ''' <summary>Path to the ModelMDB.xlsx file</summary>
        Public Shared ReadOnly MDB_MODEL As String = MODELS_DIRECTORY & "ModelMDB.xlsx"

        ''' <summary>Path to the ModelWord.docx file</summary>
        Public Shared ReadOnly DOCX_MODEL As String = MODELS_DIRECTORY & "ModelWord.docx"


        ' ----- Output ----- '

        ''' <summary>Path to the Output folder</summary>
        Public Shared ReadOnly OUTPUT_DIRECTORY As String = RESSOURCES_DIRECTORY & "Output\"


        ' ----- Database ----- '

        ''' <summary>Path to the database</summary>
        Public Shared ReadOnly DATABASE_FILE As String = RESSOURCES_DIRECTORY & "Database\db.s3db"

    End Class

End Namespace