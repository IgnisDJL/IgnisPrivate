Namespace UI
    Public Class FeederListView
        Inherits Panel

        Private WithEvents gridViewContainer As DataGridView
        Private WithEvents gridViewContainerItem As DataGridView

        Private catalogName As String = ""
        Private catalogContainer As CatalogContainer

        Private newCatalogContainerId As UI.Common.TextField
        Private newDescription As UI.Common.TextField
        Private newPercentangeAC As UI.Common.TextField
        Private newEffectiveDate As UI.Common.DatePickerPanel
        Friend WithEvents btn_CatalogContainerAdd As System.Windows.Forms.Button

        Public Sub New(ByRef catalogue As CatalogContainer)
            MyBase.New()

            Me.catalogContainer = catalogue

            Me.InitializeComponent()

            'fillCatalogContainerItem()

        End Sub

        Private Sub InitializeComponent()


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                                       gridViewContainer Column
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim columnId = New DataGridViewTextBoxColumn()
            columnId.HeaderText = "No benne"
            columnId.Width = 100
            columnId.Resizable = False

            Dim columnDescription = New DataGridViewTextBoxColumn()
            columnDescription.HeaderText = "Description"
            columnDescription.Width = 100

            Dim columnRap = New DataGridViewCheckBoxColumn()
            columnRap.HeaderText = "Recyclé"
            columnRap.Width = 70
            columnRap.Resizable = False

            Dim columnAsphaltPercentage = New DataGridViewTextBoxColumn()
            columnAsphaltPercentage.HeaderText = "Pourcentage A/C"
            columnAsphaltPercentage.Width = 115

            Dim columnDelete = New DataGridViewButtonColumn()
            columnDelete.HeaderText = ""
            columnDelete.Width = 70
            columnDelete.DefaultCellStyle.Padding = New Padding(30, 0, 20, 0)
            columnDelete.Resizable = False


            gridViewContainer = New DataGridView
            gridViewContainer.Columns.Add(columnId)
            gridViewContainer.Columns.Add(columnDescription)
            gridViewContainer.Columns.Add(columnRap)
            gridViewContainer.Columns.Add(columnAsphaltPercentage)
            gridViewContainer.Columns.Add(columnDelete)
            gridViewContainer.AllowUserToAddRows = False
            gridViewContainer.Location = New Point(0, 100)
            gridViewContainer.AutoSize = True
            gridViewContainer.ColumnHeadersHeight = 60
            gridViewContainer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            gridViewContainer.AllowUserToAddRows = False
            gridViewContainer.Location = New Point(0, 100)
            gridViewContainer.Size = New Size(400, 300)
            gridViewContainer.ColumnHeadersHeight = 60
            gridViewContainer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            gridViewContainer.ReadOnly = True
            gridViewContainer.MultiSelect = False
            gridViewContainer.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                                       gridViewContainerItem Column
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim columnId_containerItem = New DataGridViewTextBoxColumn()
            columnId_containerItem.HeaderText = "No benne"
            columnId_containerItem.Width = 100
            columnId_containerItem.Resizable = False

            Dim columnDescription_containerItem = New DataGridViewTextBoxColumn()
            columnDescription_containerItem.HeaderText = "Description"
            columnDescription_containerItem.Width = 100

            Dim columnRap_containerItem = New DataGridViewCheckBoxColumn()
            columnRap_containerItem.HeaderText = "Recyclé"
            columnRap_containerItem.Width = 70
            columnRap_containerItem.Resizable = False

            Dim columnAsphaltPercentage_containerItem = New DataGridViewTextBoxColumn()
            columnAsphaltPercentage_containerItem.HeaderText = "Pourcentage A/C"
            columnAsphaltPercentage_containerItem.Width = 115

            Dim columnEffectiveDate_containerItem = New DataGridViewTextBoxColumn()
            columnEffectiveDate_containerItem.HeaderText = "Date effective"
            columnEffectiveDate_containerItem.Width = 100

            Dim columnDelete_containerItem = New DataGridViewButtonColumn()
            columnDelete_containerItem.HeaderText = ""
            columnDelete_containerItem.Width = 70
            columnDelete_containerItem.DefaultCellStyle.Padding = New Padding(30, 0, 20, 0)
            columnDelete_containerItem.Resizable = False

            Dim columnEdit_containerItem = New DataGridViewButtonColumn()
            columnEdit_containerItem.HeaderText = ""
            columnEdit_containerItem.Width = 70
            columnEdit_containerItem.DefaultCellStyle.Padding = New Padding(30, 0, 20, 0)
            columnEdit_containerItem.Resizable = False

            gridViewContainerItem = New DataGridView
            gridViewContainerItem.Columns.Add(columnId_containerItem)
            gridViewContainerItem.Columns.Add(columnDescription_containerItem)
            gridViewContainerItem.Columns.Add(columnRap_containerItem)
            gridViewContainerItem.Columns.Add(columnAsphaltPercentage_containerItem)
            gridViewContainerItem.Columns.Add(columnEffectiveDate_containerItem)
            gridViewContainerItem.Columns.Add(columnEdit_containerItem)
            gridViewContainerItem.Columns.Add(columnDelete_containerItem)
            gridViewContainerItem.AllowUserToAddRows = False
            gridViewContainerItem.Location = New Point(0, 430)
            gridViewContainerItem.Size = New Size(700, 300)
            gridViewContainerItem.ColumnHeadersHeight = 60
            gridViewContainerItem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            gridViewContainerItem.ReadOnly = True
            gridViewContainerItem.MultiSelect = False
            gridViewContainerItem.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            Me.AutoSize = True
            Me.newCatalogContainerId = New UI.Common.TextField
            Me.newDescription = New UI.Common.TextField
            Me.newEffectiveDate = New UI.Common.DatePickerPanel
            Me.newPercentangeAC = New UI.Common.TextField

            Me.newCatalogContainerId.Location = New Point(0, 0)
            Me.newCatalogContainerId.Size = New Size(150, LayoutManager.FIELDS_HEIGHT)
            Me.newCatalogContainerId.PlaceHolder = "No benne"

            Me.newDescription.Location = New Point(Me.newCatalogContainerId.Location.X + Me.newCatalogContainerId.Width + 20, Me.newCatalogContainerId.Location.Y)
            Me.newDescription.Size = New Size(150, LayoutManager.FIELDS_HEIGHT)
            Me.newDescription.PlaceHolder = "Description"

            Me.newPercentangeAC.Location = New Point(Me.newDescription.Location.X + Me.newDescription.Width + 20, Me.newDescription.Location.Y)
            Me.newPercentangeAC.Size = New Size(75, LayoutManager.FIELDS_HEIGHT)
            Me.newPercentangeAC.PlaceHolder = "% A/C"

            Me.newEffectiveDate.Location = New Point(Me.newPercentangeAC.Location.X + Me.newPercentangeAC.Width + 5, Me.newPercentangeAC.Location.Y)
            Me.newEffectiveDate.Size = New Size(163, 60)

            Me.newEffectiveDate.LayoutType = UI.Common.DatePickerPanel.LayoutTypes.SingleDatePicker

            Me.btn_CatalogContainerAdd = New System.Windows.Forms.Button()
            Me.btn_CatalogContainerAdd.Location = New Point(0, 32)
            Me.btn_CatalogContainerAdd.Image = Constants.UI.Images._16x16.ADD

            Me.Controls.Add(Me.newCatalogContainerId)
            Me.Controls.Add(Me.newDescription)
            Me.Controls.Add(Me.newEffectiveDate)
            Me.Controls.Add(Me.newPercentangeAC)
            Me.Controls.Add(Me.btn_CatalogContainerAdd)

            Me.Controls.Add(Me.gridViewContainer)
            Me.Controls.Add(Me.gridViewContainerItem)

        End Sub

        Private Sub btn_CatalogContainerAdd_Click(sender As Object, e As EventArgs) Handles btn_CatalogContainerAdd.Click
            Dim listviewItem As ListViewItem = New ListViewItem()

            listviewItem.Text = newCatalogContainerId.Text
            listviewItem.SubItems.Add(newDescription.Text)
            catalogContainer.addNewContainerToCatalog(newCatalogContainerId.Text, newEffectiveDate.StartDate, newDescription.Text)
            fillCatalogContainerItem()

        End Sub



        Private Sub fillCatalogContainerItem()

            Me.gridViewContainer.Rows.Clear()

            For Each item As String In catalogContainer.getAllContainerId

                gridViewContainer.Rows.Add(item, catalogContainer.getDescriptionFromContainer(item, Date.Now), False, 0)

            Next

            Me.gridViewContainer.Refresh()
        End Sub

        Private Sub gridViewContainer_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles gridViewContainer.CellPainting
            If e.ColumnIndex = 4 AndAlso e.RowIndex >= 0 Then
                e.Paint(e.CellBounds, DataGridViewPaintParts.All)
                Dim img As Image = Constants.UI.Images._16x16.DELETE
                e.Graphics.DrawImage(img, e.CellBounds.Left + Convert.ToInt32(e.CellBounds.Width / 2), e.CellBounds.Top + 5, 10, 10)
                e.Handled = True
            End If
        End Sub

        Private Sub gridViewContainer_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gridViewContainer.SelectionChanged
            Me.gridViewContainerItem.Rows.Clear()

            If (gridViewContainer.SelectedRows.Count > 0) Then
                For Each effectiveDate As Date In catalogContainer.getContainerItemAllEffectiveDate(gridViewContainer.SelectedRows(0).Cells(0).Value)
                    gridViewContainerItem.Rows.Add(gridViewContainer.SelectedRows(0).Cells(0).Value, catalogContainer.getDescriptionFromContainer(gridViewContainer.SelectedRows(0).Cells(0).Value, effectiveDate), False, 0, effectiveDate)
                Next
            End If
        End Sub

        Private Sub gridViewContainer_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridViewContainer.CellContentClick
            If e.ColumnIndex = 4 And e.RowIndex >= 0 Then
                catalogContainer.removeContainerFromCatalog(gridViewContainer.Rows(e.RowIndex).Cells(0).Value)
                gridViewContainer.Rows.RemoveAt(e.RowIndex)
            End If
        End Sub

        Private Sub gridViewContainerItem_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles gridViewContainerItem.CellPainting
            If e.ColumnIndex = 6 AndAlso e.RowIndex >= 0 Then
                e.Paint(e.CellBounds, DataGridViewPaintParts.All)
                Dim img As Image = Constants.UI.Images._16x16.DELETE
                e.Graphics.DrawImage(img, e.CellBounds.Left + Convert.ToInt32(e.CellBounds.Width / 2), e.CellBounds.Top + 5, 10, 10)
                e.Handled = True
            End If

            If e.ColumnIndex = 5 AndAlso e.RowIndex >= 0 Then
                e.Paint(e.CellBounds, DataGridViewPaintParts.All)
                Dim img As Image = Constants.UI.Images._16x16.EDIT
                e.Graphics.DrawImage(img, e.CellBounds.Left + Convert.ToInt32(e.CellBounds.Width / 2), e.CellBounds.Top + 5, 10, 10)
                e.Handled = True
            End If
        End Sub

        Private Sub gridViewContainerItem_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridViewContainerItem.CellContentClick
            If e.ColumnIndex = 6 And e.RowIndex >= 0 Then
                catalogContainer.removeContainerFromCatalog(gridViewContainerItem.Rows(e.RowIndex).Cells(0).Value)
                gridViewContainerItem.Rows.RemoveAt(e.RowIndex)
            End If

            If e.ColumnIndex = 5 And e.RowIndex >= 0 Then
                gridViewContainerItem.ReadOnly = False
            End If

        End Sub
    End Class
End Namespace

