Namespace UI

    Public Class FileExportationView
        Inherits ArchivesExplorerViewTemplate

        ' Constants
        Public Shared ReadOnly VIEW_NAME As String = "Exportation de fichiers"

        ' Components
        ' !LAYOUT!
        Private WithEvents checkedFilesListView As Common.FileListControl

        Private WithEvents sendAsEmailButton As Button
        Private WithEvents saveAsFileButton As Button

        Private WithEvents filesToOverwriteMessagePanel As Common.UserMessagePanel
        Private WithEvents filesToOverwriteMessagePanelContainer As Form
        Private WithEvents filesWereSavedMessagePanel As Common.UserMessagePanel
        ' !LAYOUT!

        ' Attributes
        Private saveAsFolderDialog As FolderBrowserDialog


        Private exportController As FileExportationController

        Public Sub New()
            MyBase.New()

            Me.layout = New FileExportationViewLayout

            Me.exportController = ProgramController.FileExportationController

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.availableFilesListView.DisplayReportsFirst = True
            Me.availableFilesListView.CheckableItems = True
            Me.availableFilesListView.setCheckFilesList(Me.exportController.FilesToExport)

            Me.checkedFilesListView = New Common.FileListControl("Fichiers sélectionnés")
            Me.checkedFilesListView.DisplayReportsFirst = True
            Me.checkedFilesListView.DisplayDatesForMDBFiles = False
            Me.checkedFilesListView.FilterMethod = Function(obj As File)
                                                       Return Me.exportController.FilesToExport.Contains(obj)
                                                   End Function

            Me.sendAsEmailButton = New Button
            Me.sendAsEmailButton.TextAlign = ContentAlignment.MiddleRight
            Me.sendAsEmailButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.sendAsEmailButton.Image = Constants.UI.Images._32x32.MAIL
            Me.sendAsEmailButton.Text = "Courriel  "
            Me.sendAsEmailButton.Size = FileExportationViewLayout.SEND_AS_EMAIL_BUTTON_SIZE

            Me.saveAsFileButton = New Button
            Me.saveAsFileButton.TextAlign = ContentAlignment.MiddleRight
            Me.saveAsFileButton.ImageAlign = ContentAlignment.MiddleLeft
            Me.saveAsFileButton.Image = Constants.UI.Images._24x24.SAVE
            Me.saveAsFileButton.Text = "Sauvegarder"
            Me.saveAsFileButton.Size = FileExportationViewLayout.SAVE_AS_FILE_BUTTON_SIZE

            Me.Controls.Add(checkedFilesListView)
            Me.Controls.Add(sendAsEmailButton)
            Me.Controls.Add(saveAsFileButton)

        End Sub

        Protected Overrides Sub ajustLayout(newSize As Size)
            MyBase.ajustLayout(newSize)

            Dim layout = DirectCast(Me.layout, FileExportationViewLayout)

            Me.checkedFilesListView.Location = layout.SelectedFilesListView_Location
            Me.checkedFilesListView.ajustLayout(layout.SelectedFilesListView_Size)

            Me.sendAsEmailButton.Location = layout.SendAsEmailButton_Location

            Me.saveAsFileButton.Location = layout.SaveAsFileButton_Location

        End Sub

        Protected Overrides Sub ajustLayoutFinal(newSize As Size)
            MyBase.ajustLayoutFinal(newSize)

            Dim layout = DirectCast(Me.layout, FileExportationViewLayout)

            Me.checkedFilesListView.ajustLayoutFinal(layout.SelectedFilesListView_Size)

        End Sub

        Private Sub onFileChecked(file As File, checked As Boolean) Handles availableFilesListView.ItemChecked

            If (checked) Then
                Me.exportController.FilesToExport.Add(file)
            Else
                Me.exportController.FilesToExport.Remove(file)
            End If

            If (Not Me.checkedFilesListView.InitialObjectList.Contains(file)) Then
                Me.checkedFilesListView.addObject(file)
            End If

            Me.checkedFilesListView.refreshList()

            Me.checkedFilesListView.selectItem(file)

            Me.enableExportButtons()
        End Sub

        Private Sub onCheckedFileSelected(file As File) Handles checkedFilesListView.ItemSelectedEvent

            ' File is in currently displayed production days
            If (Me.datePickerPanel.StartDate.CompareTo(file.Date_) <= 0 AndAlso _
                Me.datePickerPanel.EndDate.CompareTo(file.Date_) >= 0) Then

                For Each _day As ProductionDay In Me.availableDatesListView.DisplayedObjectList
                    If (_day.Date_.Equals(file.Date_)) Then

                        Me.availableDatesListView.selectItem(_day)
                        Me.availableFilesListView.selectItem(file)

                        Exit For
                    End If
                Next

                Me.checkedFilesListView.Focus()
            Else

                Me.datePickerPanel.StartDate = file.Date_
                Me.datePickerPanel.EndDate = file.Date_

                Me.updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)
            End If


        End Sub

        Protected Overrides Sub afterUpdateDatesList()
            MyBase.afterUpdateDatesList()

            If (Not IsNothing(Me.checkedFilesListView.SelectedObject)) Then
                Me.availableFilesListView.selectItem(Me.checkedFilesListView.SelectedObject)
                Me.checkedFilesListView.Focus()
            End If
        End Sub

        Protected Overloads Overrides Sub beforeShow()

            Me.hideFilesWereSavedMessagePanel()
            Me.enableExportButtons()

            For Each _fileToExport As File In Me.exportController.FilesToExport

                If (Not Me.checkedFilesListView.InitialObjectList.Contains(_fileToExport)) Then
                    Me.checkedFilesListView.addObject(_fileToExport)
                End If
            Next

            Me.checkedFilesListView.refreshList()

        End Sub

        Public Overrides Sub afterShow()

            If (Me.exportController.FilesToExport.Count > 0) Then

                Me.exportController.FilesToExport.Sort(Function(x As File, y As File)
                                                           Return y.Date_.CompareTo(x.Date_)
                                                       End Function)

                Me.datePickerPanel.StartDate = Me.exportController.FilesToExport.Last.Date_
                Me.datePickerPanel.EndDate = Me.exportController.FilesToExport.First.Date_

            End If

            Me.updateDatesList(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)
        End Sub

        Public Overrides Sub onHide()
            MyBase.onHide()

        End Sub

        Private Sub sendAsEmail() Handles sendAsEmailButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.EmailExportationView)
        End Sub

        Private Sub saveAsFile() Handles saveAsFileButton.Click

            If (IsNothing(Me.saveAsFolderDialog)) Then
                Me.saveAsFolderDialog = New FolderBrowserDialog()
                Me.saveAsFolderDialog.RootFolder = Environment.SpecialFolder.Desktop
                Me.saveAsFolderDialog.ShowNewFolderButton = True
                Me.saveAsFolderDialog.Description = "Où voulez-vous savegarder les fichiers?"
            End If

            Dim result = Me.saveAsFolderDialog.ShowDialog()

            Dim filesToOverwrite As New List(Of IO.FileInfo)

            If (result = DialogResult.OK) Then

                For Each _fileAtDestination As IO.FileInfo In New IO.DirectoryInfo(Me.saveAsFolderDialog.SelectedPath).GetFiles

                    For Each _fileToCopy As File In Me.exportController.FilesToExport

                        If (_fileAtDestination.Name = _fileToCopy.getFileInfo.Name) Then
                            filesToOverwrite.Add(_fileAtDestination)
                        End If

                    Next

                Next

                If (filesToOverwrite.Count > 0) Then

                    Me.showFilesToOverwriteMessagePanel()

                Else

                    Me.exportController.saveFiles(Me.saveAsFolderDialog.SelectedPath)

                    Me.showFilesWereSavedMessagePanel()
                End If

            End If

        End Sub

        Private Sub showFilesToOverwriteMessagePanel()

            If (IsNothing(Me.filesToOverwriteMessagePanel)) Then

                ' #refactor - Make new classe or something modal you know for message panels
                Me.filesToOverwriteMessagePanelContainer = New Form
                Me.filesToOverwriteMessagePanelContainer.FormBorderStyle = FormBorderStyle.None
                Me.filesToOverwriteMessagePanelContainer.StartPosition = FormStartPosition.CenterParent
                Me.filesToOverwriteMessagePanelContainer.Size = New Size(550, 200)

                Me.filesToOverwriteMessagePanel = New Common.UserMessagePanel("Attention", _
                                                                              "Certains fichiers portent le même nom que ceux que vous voulez sauvegarder." & _
                                                                              Environment.NewLine & _
                                                                              Environment.NewLine & _
                                                                              "Appuyez sur 'Ok' pour sauvegarder quand même." & _
                                                                              Environment.NewLine & _
                                                                              "Appuyez sur 'Annuler' pour annuler la sauvegarde.", _
                                                                              Constants.UI.Images._64x64.WARNING, True)
                Me.filesToOverwriteMessagePanel.IsDraggable = False
                Me.filesToOverwriteMessagePanel.ajustLayout(New Size(550, 200))
                Me.filesToOverwriteMessagePanelContainer.Controls.Add(Me.filesToOverwriteMessagePanel)
            End If


            Me.filesToOverwriteMessagePanelContainer.ShowDialog()
            Me.filesToOverwriteMessagePanel.Focus()
        End Sub

        Private Sub hideFilesToOverwriteMessagePanel(status As Common.PopUpMessage.ClosingStatus) Handles filesToOverwriteMessagePanel.CloseEvent

            Me.filesToOverwriteMessagePanelContainer.Close()

            If (status = Common.PopUpMessage.ClosingStatus.Ok) Then
                Me.exportController.saveFiles(Me.saveAsFolderDialog.SelectedPath)
                Me.showFilesWereSavedMessagePanel()
            End If
        End Sub

        Private Sub showFilesWereSavedMessagePanel()

            If (IsNothing(Me.filesWereSavedMessagePanel)) Then
                Me.filesWereSavedMessagePanel = New Common.UserMessagePanel("Succès", "Les fichiers ont été sauvegardés avec succès!", Constants.UI.Images._32x32.GOOD)
                Me.filesWereSavedMessagePanel.ajustLayout(New Size(300, 100))
            End If

            Me.filesWereSavedMessagePanel.Location = New Point((Me.ClientSize.Width - Me.filesWereSavedMessagePanel.Width) / 2, (Me.ClientSize.Height - Me.filesWereSavedMessagePanel.Height) / 2)
            Me.Controls.Add(Me.filesWereSavedMessagePanel)
            Me.filesWereSavedMessagePanel.BringToFront()
        End Sub

        Private Sub hideFilesWereSavedMessagePanel() Handles filesWereSavedMessagePanel.CloseEvent
            Me.Controls.Remove(Me.filesWereSavedMessagePanel)
        End Sub

        Private Sub enableExportButtons()

            If (Me.exportController.FilesToExport.Count > 0) Then

                Me.sendAsEmailButton.Enabled = True
                Me.saveAsFileButton.Enabled = True

            Else

                Me.sendAsEmailButton.Enabled = False
                Me.saveAsFileButton.Enabled = False

            End If

        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property
    End Class
End Namespace
