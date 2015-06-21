Imports IGNIS.UI.Common

Namespace UI

    Public Class ImportFilesView
        Inherits View

        ' Constants
        Private Shared ReadOnly VIEW_NAME As String = "Imports"

        Private Shared ReadOnly INCORRECT_PATH_MESSAGE_PANEL_SIZE As Size = New Size(400, 100)
        Private Shared ReadOnly IMPORT_SUCCESSFUL_MESSAGE_PANEL_SIZE As Size = New Size(400, 95)

        ' Components
        Private usbPathPanel As Panel
        Private usbPathLabel As Label
        Private usbPathTextBox As TextBox
        Private WithEvents modifyPathButton As EditButton

        Private WithEvents refreshButton As RefreshButton

        Private fileListControl As FileListControl

        Private WithEvents backButton As BackButton
        Private WithEvents importButton As Button

        Private WithEvents incorrectUSBPathMessagePanel As UserMessagePanel
        Private WithEvents importSuccessfulMessagePanel As UserMessagePanel

        Private usbPathToolTip As ToolTip

        ' Attributes
        Private importController As ImportController_1
        Private directoryExplorer As FolderBrowserDialog

        Private updateFileListThread As Threading.Thread

        Public Sub New()

            Me.layout = New ImportFilesLayout

            Me.importController = ProgramController.ImportController

            initializeComponents()

        End Sub

        Protected Overrides Sub initializeComponents()

            usbPathPanel = New Panel
            usbPathPanel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            usbPathLabel = New Label
            usbPathLabel.Text = "Emplacement de la clé USB :"
            usbPathLabel.AutoSize = False
            usbPathLabel.TextAlign = ContentAlignment.MiddleLeft

            usbPathTextBox = New TextBox
            usbPathTextBox.ReadOnly = True
            usbPathTextBox.Text = importController.USBDirectory.FullName
            usbPathTextBox.AutoSize = False

            usbPathToolTip = New ToolTip
            usbPathToolTip.ShowAlways = True
            usbPathToolTip.Active = True
            usbPathToolTip.InitialDelay = 100
            usbPathToolTip.BackColor = Color.White

            modifyPathButton = New EditButton
            modifyPathButton.TextAlign = ContentAlignment.MiddleCenter
            modifyPathButton.Text = ImportFilesLayout.MODIFY_PATH_BUTTON_TEXT

            usbPathPanel.Controls.Add(usbPathLabel)
            usbPathPanel.Controls.Add(usbPathTextBox)
            usbPathPanel.Controls.Add(modifyPathButton)

            refreshButton = New RefreshButton
            refreshButton.TextAlign = ContentAlignment.MiddleCenter
            refreshButton.Text = ImportFilesLayout.REFRESH_BUTTON_TEXT

            fileListControl = New FileListControl("Fichiers à importer")

            backButton = New BackButton

            importButton = New Button
            importButton.Text = ImportFilesLayout.IMPORT_BUTTON_TEXT
            importButton.ImageAlign = ContentAlignment.MiddleLeft
            importButton.Image = Constants.UI.Images._32x32.IMPORT
            importButton.TextAlign = ContentAlignment.MiddleCenter

            Me.Controls.Add(usbPathPanel)
            Me.Controls.Add(fileListControl)
            Me.Controls.Add(backButton)
            Me.Controls.Add(importButton)
            Me.fileListControl.addTitleBarButton(Me.refreshButton)

            importButton.TabIndex = 1
            backButton.TabIndex = 2
            refreshButton.TabIndex = 3

        End Sub

        Private Sub initializeIncorrectUSBPathMessagePanel()
            ' #language
            Me.incorrectUSBPathMessagePanel = New UserMessagePanel("Attention!", "L'emplacement n'est pas celui de la clé USB IGNIS", Constants.UI.Images._32x32.WARNING)

            Me.incorrectUSBPathMessagePanel.Location = DirectCast(Me.layout, ImportFilesLayout).IncorrectUSBPathMessagePanel_Location
            Me.incorrectUSBPathMessagePanel.ajustLayout(IMPORT_SUCCESSFUL_MESSAGE_PANEL_SIZE)
        End Sub

        Private Sub initializeImportSuccessfulMessagePanel(nbImportedFiles As Integer)
            ' #language
            Me.importSuccessfulMessagePanel = New UserMessagePanel("Importation réussie!", nbImportedFiles & " fichiers ont été importés.", Constants.UI.Images._32x32.GOOD)

            Me.importSuccessfulMessagePanel.Location = New Point(Me.Width / 2 - IMPORT_SUCCESSFUL_MESSAGE_PANEL_SIZE.Width / 2, Me.Height / 2 - IMPORT_SUCCESSFUL_MESSAGE_PANEL_SIZE.Height / 2)
            Me.importSuccessfulMessagePanel.ajustLayout(IMPORT_SUCCESSFUL_MESSAGE_PANEL_SIZE)
            Me.importButton.Enabled = False
            Me.refreshButton.Enabled = False
        End Sub

        Protected Overrides Sub ajustLayout(newSize As Size)

            Dim layout As ImportFilesLayout = DirectCast(Me.LayoutManager, ImportFilesLayout)

            ' USB Path Panel
            usbPathPanel.Location = layout.USBPathPanel_Location
            usbPathPanel.Size = layout.USBPathPanel_Size

            ' USB Path Label
            usbPathLabel.Location = layout.USBPathLabel_Location
            usbPathLabel.Size = layout.USBPathLabel_Size

            ' USB Path TextBox
            usbPathTextBox.Location = layout.USBPathTextBox_Location
            usbPathTextBox.Size = layout.USBPathTextBox_Size
            usbPathTextBox.Font = New Font(usbPathTextBox.Font.FontFamily, layout.USBPathTextBox_FontSize)

            ' Modify Path Button
            modifyPathButton.Location = layout.ModifyPathButton_Location
            modifyPathButton.Size = layout.ModifyPathButton_Size

            ' Refresh Button
            refreshButton.Size = layout.RefreshButton_Size

            ' File List
            fileListControl.Location = layout.FileList_Location
            fileListControl.ajustLayout(layout.FileList_Size)

            ' Back Button
            backButton.Location = layout.BackButton_Location
            backButton.Size = layout.BackButton_Size

            ' Import Button
            importButton.Location = layout.ImportButton_Location
            importButton.Size = layout.ImportButton_Size

        End Sub

        Protected Overrides Sub ajustLayoutFinal(newSize As Size)

            fileListControl.ajustLayoutFinal(fileListControl.Size)

            If (Not IsNothing(Me.incorrectUSBPathMessagePanel)) Then
                Me.incorrectUSBPathMessagePanel.ajustLayoutFinal(INCORRECT_PATH_MESSAGE_PANEL_SIZE)
            End If

            If (Not IsNothing(Me.incorrectUSBPathMessagePanel) AndAlso Not Me.ClientRectangle.Contains(Me.incorrectUSBPathMessagePanel.Location)) Then
                Me.incorrectUSBPathMessagePanel.Location = DirectCast(Me.layout, ImportFilesLayout).IncorrectUSBPathMessagePanel_Location
            End If
        End Sub

        Private Sub importFiles() Handles importButton.Click

            Me.backButton.Enabled = False
            Me.modifyPathButton.Enabled = False
            Me.importButton.Enabled = False
            Me.refreshButton.Enabled = False

            Me.Cursor = Cursors.AppStarting

            ' Import files
            Dim nbFilesImported As Integer = Me.importController.importFiles()

            Me.backButton.Enabled = True
            Me.modifyPathButton.Enabled = True
            Me.importButton.Enabled = True
            Me.refreshButton.Enabled = True

            Me.Cursor = Cursors.Default

            backButton.Focus()

            If (nbFilesImported = importController.NB_FILES_TO_IMPORT) Then

                Me.initializeImportSuccessfulMessagePanel(nbFilesImported)
                Me.Controls.Add(Me.importSuccessfulMessagePanel)
                Me.importSuccessfulMessagePanel.BringToFront()

                Me.importSuccessfulMessagePanel.Focus()
            Else

                'Debugger.Break()
            End If
        End Sub

        Private Sub modifyUSBPath() Handles modifyPathButton.Click

            If (IsNothing(directoryExplorer)) Then
                directoryExplorer = New FolderBrowserDialog
                directoryExplorer.ShowNewFolderButton = False
                directoryExplorer.RootFolder = Environment.SpecialFolder.MyComputer
                ' #language
                directoryExplorer.Description = "Selectionnez la clé USB IGNIS et appuyez sur OK"
            End If

            If (directoryExplorer.ShowDialog = DialogResult.OK) Then

                updateFileList(New IO.DirectoryInfo(directoryExplorer.SelectedPath))

            End If

        End Sub

        Public Sub updateFileList(usbDirectory As IO.DirectoryInfo)

            Me.killUpdateFileListThread()

            ' #refactor - put dirinfo in method and just ask for string in param
            If (Me.importController.isValidUSBDirectory(usbDirectory)) Then

                Me.usbPathTextBox.Text = usbDirectory.FullName
                Me.usbPathToolTip.SetToolTip(Me.usbPathTextBox, usbPathTextBox.Text)

                Me.importController.USBDirectory = usbDirectory

                Me.Controls.Remove(Me.incorrectUSBPathMessagePanel)
                Me.importButton.Enabled = False
                Me.updateFileListThread = New Threading.Thread(New Threading.ThreadStart(AddressOf fillFileList))
                Me.fileListControl.showLoader()
                Me.updateFileListThread.Start()
                Me.Cursor = Cursors.AppStarting

            Else

                If (IsNothing(Me.incorrectUSBPathMessagePanel)) Then
                    initializeIncorrectUSBPathMessagePanel()
                End If

                Me.Controls.Add(Me.incorrectUSBPathMessagePanel)
                Me.incorrectUSBPathMessagePanel.BringToFront()

                Me.importButton.Enabled = False

                Me.incorrectUSBPathMessagePanel.Focus()

                Me.fileListControl.clear()

            End If

        End Sub

        ' In different thread
        Private Sub fillFileList()

            Dim shouldFillList As Boolean = False

            Dim filesOnUSB As List(Of DataFile) = Me.importController.identifyFilesToImport()

            ' Check if files in usb are the same as the ones in the list
            If (filesOnUSB.Count = Me.fileListControl.InitialObjectList.Count) Then

                For i = 0 To filesOnUSB.Count - 1

                    If (Not filesOnUSB(i).Equals(Me.fileListControl.InitialObjectList(i))) Then

                        shouldFillList = True
                        Exit For
                    End If

                Next

            Else
                shouldFillList = True
            End If

            If (shouldFillList) Then

                Me.Invoke(Sub() Me.fileListControl.clear())

                For Each file As DataFile In filesOnUSB
                    Me.Invoke(Sub() Me.fileListControl.addObject(file))
                Next

                Me.Invoke(Sub() Me.fileListControl.refreshList())

            End If

            Me.Invoke(Sub() afterFillFileList())


        End Sub

        ' In same thread
        Private Sub afterFillFileList()
            Me.Cursor = Cursors.Default
            Me.fileListControl.hideLoader()
            Me.fileListControl.ajustLayoutFinal(fileListControl.Size)
            Me.importButton.Enabled = True
            Me.importButton.Focus()
        End Sub

        Protected Overrides Sub beforeShow()

        End Sub

        Public Overrides Sub afterShow()
            Me.updateFileList(importController.USBDirectory)
        End Sub

        Public Overrides Sub onHide()
            Me.Controls.Remove(Me.importSuccessfulMessagePanel)
            Me.Controls.Remove(Me.incorrectUSBPathMessagePanel)
            Me.killUpdateFileListThread()
        End Sub

        Private Sub killUpdateFileListThread()
            If (Not IsNothing(Me.updateFileListThread) AndAlso Me.updateFileListThread.IsAlive) Then
                Me.updateFileListThread.Abort()
                Me.fileListControl.clear()
                Me.importController.clear()
                Me.afterFillFileList()
            End If
        End Sub

        Public Overrides ReadOnly Property Name As String
            Get
                Return VIEW_NAME
            End Get
        End Property

        Private Sub refreshFileList() Handles refreshButton.Click
            updateFileList(importController.USBDirectory)
        End Sub

        Private Sub backToMainMenu() Handles backButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.MainMenuView)
        End Sub

        Private Sub closeIncorrectUSBPathPanel() Handles incorrectUSBPathMessagePanel.CloseEvent
            Me.Controls.Remove(Me.incorrectUSBPathMessagePanel)
        End Sub

        Private Sub closeImportSuccessfulMessagePanel() Handles importSuccessfulMessagePanel.CloseEvent
            Me.Controls.Remove(Me.importSuccessfulMessagePanel)
            Me.importButton.Enabled = True
            Me.refreshButton.Enabled = True
        End Sub

    End Class
End Namespace
