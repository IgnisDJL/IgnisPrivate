Imports IGNIS.UI.Common

Namespace UI

    Public MustInherit Class ArchivesExplorerViewTemplate
        Inherits View

        ' Constants
        Private Shared ReadOnly INCORRECT_DATES_MESSAGE_SIZE As Size = New Size(450, 110)

        ' Components
        Protected WithEvents datePickerPanel As DatePickerPanel

        Protected WithEvents availableDatesListView As Common.DatesListControl
        Protected WithEvents availableFilesListView As Common.FileListControl

        Protected WithEvents backButton As BackButton

        Private WithEvents incorrectDatesMessagePanel As Common.UserMessagePanel

        ' Attributes

        ' Threads
        Private updateDatesListThread As Threading.Thread

        Protected Sub New()
            MyBase.New()

        End Sub

        Protected Overrides Sub initializeComponents()

            Me.datePickerPanel = New DatePickerPanel
            Me.datePickerPanel.ShowChangeLayoutButton = True

            Me.datePickerPanel.StartDate = New Date(2014, 12, 4)
            Me.datePickerPanel.EndDate = New Date(2014, 12, 4)

            Me.availableDatesListView = New DatesListControl("Dates disponibles")

            Me.availableFilesListView = New FileListControl("Fichiers disponibles")

            Me.backButton = New BackButton
            Me.backButton.Size = New Size(Common.BackButton.BUTTON_WIDTH, UI.LayoutManager.CONTROL_BUTTONS_HEIGHT)

            Me.Controls.Add(Me.datePickerPanel)
            Me.Controls.Add(Me.availableDatesListView)
            Me.Controls.Add(Me.availableFilesListView)
            Me.Controls.Add(Me.backButton)
        End Sub

        Protected Overrides Sub ajustLayout(newSize As Size)

            Dim layout = DirectCast(Me.layout, ArchivesExplorerViewTemplateLayout)

            Me.datePickerPanel.Location = layout.DatePickerPanel_Location
            Me.datePickerPanel.ajustLayout(layout.DatePickerPanel_Size)

            Me.availableDatesListView.Location = layout.AvailableDatesListView_Location
            Me.availableDatesListView.ajustLayout(layout.AvailableDatesListView_Size)

            Me.availableFilesListView.Location = layout.AvailableFilesListView_Location
            Me.availableFilesListView.ajustLayout(layout.AvailableFilesListView_Size)

            Me.backButton.Location = layout.BackButton_Location

        End Sub

        Protected Overrides Sub ajustLayoutFinal(newSize As Size)

            Dim layout = DirectCast(Me.layout, ArchivesExplorerViewTemplateLayout)

            Me.datePickerPanel.ajustLayoutFinal(layout.DatePickerPanel_Size)

            Me.availableDatesListView.ajustLayoutFinal(layout.AvailableDatesListView_Size)

            Me.availableFilesListView.ajustLayoutFinal(layout.AvailableFilesListView_Size)

        End Sub

        Protected Sub backToMainMenu() Handles backButton.Click
            ProgramController.UIController.changeView(ProgramController.UIController.MainMenuView)
        End Sub

        Protected Overridable Sub beforeUpdateDatesList()
            Me.killUpdateDateListThread()

            If (Not IsNothing(Me.incorrectDatesMessagePanel)) Then
                Me.removeIncorrectDatesMessagePanel()
            End If
        End Sub

        Public Sub updateDatesList(startDate As Date, endDate As Date) Handles datePickerPanel.DatesChanged

            Me.beforeUpdateDatesList()

            ' If dates are valid
            If Me.datePickerPanel.EndDate.Subtract(Me.datePickerPanel.StartDate) > TimeSpan.FromHours(24) Then

                Me.incorrectDatesMessagePanel = New Common.UserMessagePanel("Attention!", "La période doit être de maximum 24 heures.", Constants.UI.Images._64x64.WARNING)
                Me.incorrectDatesMessagePanel.Location = New Point(Me.datePickerPanel.Location.X, Me.datePickerPanel.Location.Y + Me.datePickerPanel.Size.Height + 5)
                Me.incorrectDatesMessagePanel.ajustLayout(INCORRECT_DATES_MESSAGE_SIZE)
                Me.Controls.Add(Me.incorrectDatesMessagePanel)
                Me.incorrectDatesMessagePanel.BringToFront()
                Me.availableDatesListView.clear()

            ElseIf (endDate.Subtract(startDate).TotalDays >= 0) Then

                Me.updateDatesListThread = New Threading.Thread(New Threading.ThreadStart(AddressOf fillDatesList))

                Me.availableDatesListView.showLoader()

                Me.updateDatesListThread.Start()

                Me.Cursor = Cursors.AppStarting

            Else

                If (IsNothing(Me.incorrectDatesMessagePanel)) Then
                    Me.initializeIncorrectDatesMessagePanel()
                End If

                Me.Controls.Add(Me.incorrectDatesMessagePanel)

                Me.incorrectDatesMessagePanel.BringToFront()

                Me.availableDatesListView.clear()

            End If

        End Sub

        ' In different thread
        Private Sub fillDatesList()

            Dim shouldFillList As Boolean = False
            Dim datesList As List(Of ProductionDay_1)

            datesList = ProgramController.ImportController.plantProduction.getProductionDay(Me.datePickerPanel.StartDate, Me.datePickerPanel.EndDate)
            ' Check if new dates are the same as the ones in the list
            If (datesList.Count = Me.availableDatesListView.DisplayedObjectList.Count) Then

                For i = 0 To datesList.Count - 1

                    If (Not datesList(i).Equals(Me.availableDatesListView.DisplayedObjectList(i))) Then

                        shouldFillList = True
                        Exit For
                    End If

                Next

            Else
                shouldFillList = True
            End If

            If (shouldFillList) Then

                Me.Invoke(Sub() Me.availableDatesListView.clear())

                For Each productionDay As ProductionDay_1 In datesList
                    Me.Invoke(Sub() Me.availableDatesListView.addObject(productionDay)) ' Filters are applyed here...
                Next

                Me.Invoke(Sub() Me.availableDatesListView.refreshList())
            End If

            Me.Invoke(Sub() afterUpdateDatesList())

        End Sub

        ' In UI thread
        Protected Overridable Sub afterUpdateDatesList()
            Me.Cursor = Cursors.Default
            Me.availableDatesListView.hideLoader()
            Me.availableDatesListView.ajustLayoutFinal(availableDatesListView.Size)

            Me.availableDatesListView.selectFirstItem()

        End Sub

        Private Sub killUpdateDateListThread()
            If (Not IsNothing(Me.updateDatesListThread) AndAlso Me.updateDatesListThread.IsAlive) Then
                Me.updateDatesListThread.Abort()
                Me.availableDatesListView.clear()
                Me.afterUpdateDatesList()
            End If
        End Sub

        Private Sub onDateSelected(itemObject As ProductionDay_1) Handles availableDatesListView.ItemSelectedEvent

            Me.availableFilesListView.clear()

            If Not IsNothing(itemObject) Then

                'If (itemObject.DataFilesInfo.HasCSVFile) Then
                '    Me.availableFilesListView.addObject(itemObject.DataFilesInfo.CSVFile)
                'End If

                'If (itemObject.DataFilesInfo.HasLOGFile) Then
                '    Me.availableFilesListView.addObject(itemObject.DataFilesInfo.LOGFile)
                'End If

                'If (itemObject.DataFilesInfo.HasMDBFile) Then
                '    Me.availableFilesListView.addObject(itemObject.DataFilesInfo.MDBFile)
                'End If

                'If (itemObject.DataFilesInfo.HasEventsFile) Then
                '    Me.availableFilesListView.addObject(itemObject.DataFilesInfo.EventsFile)
                'End If

                'If (Not IsNothing(itemObject.ReportFilesInfo.SummaryDailyReport)) Then
                '    Me.availableFilesListView.addObject(itemObject.ReportFilesInfo.SummaryDailyReport)
                'End If

                'If (Not IsNothing(itemObject.ReportFilesInfo.SummaryReadOnlyDailyReport)) Then
                '    Me.availableFilesListView.addObject(itemObject.ReportFilesInfo.SummaryReadOnlyDailyReport)
                'End If

                Me.availableFilesListView.refreshList()

                ' Todo, manual data
            End If

        End Sub

        Private Sub initializeIncorrectDatesMessagePanel()

            Me.incorrectDatesMessagePanel = New Common.UserMessagePanel("Attention!", "La date de début doit être avant la date de fin ou égale à celle-ci.", Constants.UI.Images._64x64.WARNING)

            Me.incorrectDatesMessagePanel.Location = New Point(Me.datePickerPanel.Location.X, Me.datePickerPanel.Location.Y + Me.datePickerPanel.Size.Height + 5)
            Me.incorrectDatesMessagePanel.ajustLayout(INCORRECT_DATES_MESSAGE_SIZE)
        End Sub

        Private Sub removeIncorrectDatesMessagePanel() Handles incorrectDatesMessagePanel.CloseEvent
            Me.Controls.Remove(Me.incorrectDatesMessagePanel)
        End Sub

        Protected Overrides Sub beforeShow()

        End Sub

        Public MustOverride Overrides Sub afterShow()

        Public Overrides Sub onHide()
            Me.killUpdateDateListThread()
        End Sub

        Public MustOverride Overloads Overrides ReadOnly Property Name As String

    End Class
End Namespace