Namespace Constants.UI

    Public Class Icons

        Public Shared ReadOnly PROGRAM As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "ProgramIcon.ico")
        Public Shared ReadOnly REMOVE As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "RemoveIcon.ico")
        Public Shared ReadOnly ADD As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "AddIcon.ico")
        Public Shared ReadOnly WORD As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "WordIcon.ico")
        Public Shared ReadOnly EXCEL As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "ExcelIcon.ico")
        Public Shared ReadOnly PDF As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "PdfIcon.ico")
        Public Shared ReadOnly DATA As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "DataIcon.ico")
        Public Shared ReadOnly FOLDER As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "FolderIcon.ico")
        Public Shared ReadOnly X As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "XIcon.ico")
        Public Shared ReadOnly SETTINGS As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "SettingsIcon.ico")
        Public Shared ReadOnly HELP As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "HelpIcon.ico")
        Public Shared ReadOnly REFRESH As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "RefreshIcon.ico")
        Public Shared ReadOnly IMPORT As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "ImportIcon.ico")
        Public Shared ReadOnly DAILY_REPORT As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "DailyReportIcon.ico")
        Public Shared ReadOnly PERIODIC_REPORT As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "PeriodicReportIcon.ico")
        Public Shared ReadOnly EMAIL As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "EmailIcon.ico")
        Public Shared ReadOnly SAVE_AS As Drawing.Icon = New Drawing.Icon(Constants.Paths.IMAGES_DIRECTORY & "SaveAsIcon.ico")

        Private Shared _list As New ImageList
        Public Shared ReadOnly Property LIST As ImageList
            Get
                If (_list.Images.Count = 0) Then
                    Try
                        _list.Images.Add(PROGRAM) ' 0
                        _list.Images.Add(REMOVE) ' 1
                        _list.Images.Add(ADD) ' 2
                        _list.Images.Add(WORD) ' 3
                        _list.Images.Add(EXCEL) ' 4
                        _list.Images.Add(PDF) ' 5
                        _list.Images.Add(DATA) ' 6
                        _list.Images.Add(FOLDER) ' 7
                        _list.Images.Add(X) ' 8
                        _list.Images.Add(SETTINGS) ' 9
                        _list.Images.Add(HELP) ' 10
                        _list.Images.Add(REFRESH) ' 11
                        _list.Images.Add(IMPORT) ' 12
                    Catch e As Exception
                        Debugger.Break()
                    End Try

                End If

                Return _list
            End Get
        End Property

        Public Enum INDEX

            PROGRAM = 0
            REMOVE = 1
            ADD = 2
            WORD = 3
            EXCEL = 4
            PDF = 5
            DATA = 6
            FOLDER = 7
            X = 8
            SETTINGS = 9
            HELP = 10
            REFRESH = 11
            IMPORT = 12

        End Enum

    End Class

End Namespace
