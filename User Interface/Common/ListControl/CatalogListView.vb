Namespace UI
    Public Class CalalogListView
        Inherits Panel

        Private catalogName As String = ""
        Private catalogContainer As CatalogContainer

        Private newCatalogContainerId As System.Windows.Forms.TextBox
        Private newDescription As System.Windows.Forms.TextBox
        Private newEffectiveDate As UI.Common.DatePickerPanel

        Private catalogContainerListView As ListView
        Friend WithEvents btn_CatalogContainerAdd As System.Windows.Forms.Button
        Friend WithEvents btn_CatalogContainerRemove As System.Windows.Forms.Button
        Dim catalogContainerItem As ListView

        Public Sub New(ByRef catalogue As CatalogContainer)
            MyBase.New()

            Me.catalogContainer = catalogue

            Me.InitializeComponent()

            fillCatalogContainerItem()

        End Sub


        Private Sub InitializeComponent()
            Me.AutoSize = True
            Me.newCatalogContainerId = New System.Windows.Forms.TextBox
            Me.newDescription = New System.Windows.Forms.TextBox
            Me.newEffectiveDate = New UI.Common.DatePickerPanel

            Me.newCatalogContainerId.Location = New Point(0, 0)
            Me.newCatalogContainerId.Size = New Size(35, LayoutManager.FIELDS_HEIGHT)

            Me.newDescription.Location = New Point(Me.newCatalogContainerId.Location.X + Me.newCatalogContainerId.Width + 5, Me.newCatalogContainerId.Location.Y)
            Me.newDescription.Size = New Size(150, LayoutManager.FIELDS_HEIGHT)

            Me.newEffectiveDate.Location = New Point(Me.newDescription.Location.X + Me.newDescription.Width + 5, Me.newDescription.Location.Y)
            Me.newEffectiveDate.Size = New Size(150, 50)

            Me.newEffectiveDate.LayoutType = UI.Common.DatePickerPanel.LayoutTypes.SingleDatePicker

            Me.btn_CatalogContainerAdd = New System.Windows.Forms.Button()
            Me.btn_CatalogContainerRemove = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            '    'catalogContainer
            '    '
            '    Me.catalogContainerListView.LabelEdit = True
            '    Me.catalogContainerListView.FullRowSelect = True
            '    'Me.catalogContainerListView.GridLines = True
            '    'Me.catalogContainerListView.View = System.Windows.Forms.View.Details

            '    Me.catalogContainerListView.Location = New System.Drawing.Point(0, 200)
            '    Me.catalogContainerListView.Name = "catalogContainer"
            '    Me.catalogContainerListView.Size = New System.Drawing.Size(500, 250)
            '    Me.catalogContainerListView.TabIndex = 0
            '    Me.catalogContainerListView.Columns.Add(New ColumnHeader("test"))
            ''
            'btn_CatalogContainerAdd
            '
            Me.btn_CatalogContainerAdd.Location = New System.Drawing.Point(0, 50)
            Me.btn_CatalogContainerAdd.Name = "btn_CatalogContainerAdd"
            Me.btn_CatalogContainerAdd.Size = New System.Drawing.Size(90, 30)
            Me.btn_CatalogContainerAdd.TabIndex = 0
            Me.btn_CatalogContainerAdd.Text = "Ajouter"
            Me.btn_CatalogContainerAdd.UseVisualStyleBackColor = True
            '
            'btn_CatalogContainerRemove
            '
            Me.btn_CatalogContainerRemove.Location = New System.Drawing.Point(90, 50)
            Me.btn_CatalogContainerRemove.Name = "btn_CatalogContainerRemove"
            Me.btn_CatalogContainerRemove.Size = New System.Drawing.Size(110, 30)
            Me.btn_CatalogContainerRemove.TabIndex = 0
            Me.btn_CatalogContainerRemove.Text = "Supprimer"
            Me.btn_CatalogContainerRemove.UseVisualStyleBackColor = True
            '
            'CalalogListView

            ' Create a new ListView control. 
            Me.catalogContainerListView = New ListView()
            Me.catalogContainerListView.Bounds = New Rectangle(New Point(0, 100), New Size(300, 200))

            ' Set the view to show details.
            Me.catalogContainerListView.View = System.Windows.Forms.View.Details
            ' Allow the user to edit item text.
            Me.catalogContainerListView.LabelEdit = True
            ' Allow the user to rearrange columns.
            Me.catalogContainerListView.AllowColumnReorder = True
            ' Select the item and subitems when selection is made.
            Me.catalogContainerListView.FullRowSelect = True
            ' Display grid lines.
            Me.catalogContainerListView.GridLines = True
            ' Sort the items in the list in ascending order.
            Me.catalogContainerListView.Sorting = SortOrder.Ascending

            ' Create columns for the items and subitems. 
            ' Width of -2 indicates auto-size.
            Me.catalogContainerListView.Columns.Add("Container Id", -2, HorizontalAlignment.Left)
            Me.catalogContainerListView.Columns.Add("Description", -2, HorizontalAlignment.Left)


            ' Add the ListView to the control collection. 
            Me.Controls.Add(Me.catalogContainerListView)
            Me.Controls.Add(Me.newCatalogContainerId)
            Me.Controls.Add(Me.newDescription)
            Me.Controls.Add(Me.newEffectiveDate)
            Me.Controls.Add(Me.btn_CatalogContainerAdd)
            Me.Controls.Add(Me.btn_CatalogContainerRemove)


            Me.ResumeLayout(False)

        End Sub

        Private Sub btn_CatalogContainerAdd_Click(sender As Object, e As EventArgs) Handles btn_CatalogContainerAdd.Click
            Dim listviewItem As ListViewItem = New ListViewItem()

            listviewItem.Text = newCatalogContainerId.Text
            listviewItem.SubItems.Add(newDescription.Text)
            catalogContainerListView.Items.Add(listviewItem)
            catalogContainer.addNewContainerToCatalog(newCatalogContainerId.Text, newEffectiveDate.StartDate, newDescription.Text)


        End Sub

        Private Sub btn_CatalogContainerRemove_Click(sender As Object, e As EventArgs) Handles btn_CatalogContainerRemove.Click
            If catalogContainerListView.SelectedItems.Count > 0 Then
                catalogContainer.removeContainerFromCatalog(catalogContainerListView.SelectedItems(0).Text)
                catalogContainerListView.Items.Remove(catalogContainerListView.SelectedItems(0))
            End If
            
        End Sub

        Private Sub fillCatalogContainerItem()

            Me.catalogContainerListView.Clear()

            Me.catalogContainerListView.Columns.Add("Container Id", -2, HorizontalAlignment.Left)
            Me.catalogContainerListView.Columns.Add("Description", -2, HorizontalAlignment.Left)

            For Each item As String In catalogContainer.getAllContainerId
                Dim listviewItem As ListViewItem = New ListViewItem()
                listviewItem.Text = item
                listviewItem.SubItems.Add(catalogContainer.getDescriptionFromContainer(item, Date.Now))

                catalogContainerListView.Items.Add(listviewItem)
            Next
            catalogContainerListView.Update()
        End Sub

    End Class
End Namespace

