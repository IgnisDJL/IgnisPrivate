Namespace UI.Common

    Public MustInherit Class ListControlTemplate(Of MyType)
        Inherits Panel

        ' Constants
        Public Shared ReadOnly TITLE_LABEL_HEIGHT As Integer = 35

        Private Shared ReadOnly SELECTED_ITEM_COLOR As Color = Color.LightBlue
        Protected Shared ReadOnly ODD_ITEM_COLOR As Color = Color.White
        Protected Shared ReadOnly EVEN_ITEM_COLOR As Color = Color.LightGray

        Protected Shared ReadOnly ITEMS_HEIGHT As Integer = 27

        ' Components
        Protected listPanel As Panel
        Protected titleLabel As Label
        Protected itemCountLabel As Label
        Protected loaderPanel As PictureBox

        Protected titleBarButtons As List(Of Control)

        ' Attributes
        Protected initialItemList As List(Of ListItem(Of MyType))
        Protected displayedItemList As List(Of ListItem(Of MyType))

        Private _initialObjectList As List(Of MyType)
        Private _displayedObjectList As List(Of MyType)

        Private _selectedItem As ListItem(Of MyType)

        Protected lastInsertedItem As ListItem(Of MyType)

        Protected _title As String

        Private _itemCountIsShowing As Boolean = True

        Public Delegate Function filterMethodDelegate(obj As MyType) As Boolean
        Public Property FilterMethod As filterMethodDelegate
        Public Property SortMethod As Comparison(Of MyType)

        ' Events
        Public Event ItemSelectedEvent(itemObject As MyType)

        Public Sub New(title As String)

            Me._title = title

            Me.initialItemList = New List(Of ListItem(Of MyType))
            Me.displayedItemList = New List(Of ListItem(Of MyType))

            Me._initialObjectList = New List(Of MyType)
            Me._displayedObjectList = New List(Of MyType)

            Me.titleBarButtons = New List(Of Control)

            Me.initializeComponents()

            Me.FilterMethod = Function(obj As MyType)
                                  Return True
                              End Function

        End Sub

        Protected Overridable Sub initializeComponents()

            Me.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.titleLabel = New Label
            Me.titleLabel.AutoSize = False
            Me.titleLabel.Height = TITLE_LABEL_HEIGHT
            Me.titleLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.titleLabel.Text = _title

            Me.itemCountLabel = New Label
            Me.itemCountLabel.AutoSize = False
            Me.itemCountLabel.TextAlign = ContentAlignment.MiddleRight
            Me.itemCountLabel.Size = New Size(70, TITLE_LABEL_HEIGHT)

            Me.listPanel = New Panel
            Me.listPanel.VerticalScroll.SmallChange = Me.ItemsHeight

            Me.listPanel.AutoScroll = True

            Me.Controls.Add(titleLabel)
            Me.Controls.Add(itemCountLabel)
            Me.Controls.Add(listPanel)

        End Sub

        Private Sub initializeLoaderPanel()

            Me.loaderPanel = New PictureBox
            Me.loaderPanel.Image = Image.FromFile(Constants.Paths.IMAGES_DIRECTORY & "Gifs\Loader.gif") '#refactor
            Me.loaderPanel.Enabled = True

            Me.loaderPanel.SizeMode = PictureBoxSizeMode.CenterImage

        End Sub

        Public Overridable Sub ajustLayout(newSize As Size)

            Me.listPanel.AutoScroll = False

            Me.Size = newSize

            Dim titleBarButtonsCombinedWidth As Integer = 0

            For Each _control In Me.titleBarButtons

                titleBarButtonsCombinedWidth += _control.Width + 4
                _control.Location = New Point(Me.Width - titleBarButtonsCombinedWidth, 0)
            Next

            If (Me.Controls.Contains(Me.loaderPanel)) Then

                Me.loaderPanel.Location = Me.listPanel.Location
                Me.loaderPanel.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - Me.titleLabel.Height)

            Else

                If (Me._itemCountIsShowing) Then

                    Me.itemCountLabel.Location = New Point(Me.Width - titleBarButtonsCombinedWidth - Me.itemCountLabel.Width, 0)
                    Me.titleLabel.Width = Me.itemCountLabel.Location.X

                Else
                    Me.titleLabel.Width = Me.Width
                End If

                Me.titleLabel.Location = New Point(5, 0)

                Me.listPanel.Location = New Point(0, Me.titleLabel.Height)
                Me.listPanel.Size = New Size(Me.ClientSize.Width, Me.ClientSize.Height - Me.titleLabel.Height)

                For Each item As ListItem(Of MyType) In Me.displayedItemList
                    If (item.Location.Y < Me.listPanel.Height) Then
                        item.Width = Me.ClientSize.Width
                    End If
                Next
            End If

        End Sub

        Public Overridable Sub ajustLayoutFinal(newSize As Size)

            If (Not Me.Controls.Contains(Me.loaderPanel)) Then

                Dim itemsWidth As Integer = If(Me.listPanel.Controls.Count * ItemsHeight > listPanel.Height, Me.listPanel.Width - SystemInformation.VerticalScrollBarWidth, Me.ClientSize.Width)

                For Each item As ListItem(Of MyType) In Me.displayedItemList
                    item.ajustLayout(New Size(itemsWidth, ItemsHeight))
                Next

                Me.listPanel.AutoScroll = True

            End If

        End Sub

        Public Overridable Sub showLoader()

            If (IsNothing(Me.loaderPanel)) Then
                Me.initializeLoaderPanel()
            End If

            Me.Controls.Add(Me.loaderPanel)
            Me.Controls.Remove(Me.listPanel)
            Me.loaderPanel.BringToFront()
            Me.ajustLayout(Me.Size)
        End Sub

        Public Overridable Sub hideLoader()
            Me.Controls.Add(Me.listPanel)
            Me.Controls.Remove(Me.loaderPanel)
            Me.ajustLayout(Me.Size)
            Me.ajustLayoutFinal(Me.Size)
        End Sub

        Public Sub selectFirstItem()

            If (Me.displayedItemList.Count > 0) Then
                Me.selectItem(Me.displayedItemList.First)
            Else
                RaiseEvent ItemSelectedEvent(Nothing)
            End If
        End Sub

        Public Sub selectLastItem()

            If (Me.displayedItemList.Count > 0) Then
                Me.selectItem(Me.displayedItemList.Last)
            Else
                RaiseEvent ItemSelectedEvent(Nothing)
            End If
        End Sub

        Public Sub selectItem(objectToSelect As MyType)

            For Each item In Me.displayedItemList

                If (item.ItemObject.Equals(objectToSelect)) Then
                    Me.selectItem(item)
                End If
            Next

        End Sub

        Protected Overridable Sub selectItem(item As ListItem(Of MyType))

            If (Not item.Equals(Me._selectedItem)) Then

                If (Not IsNothing(Me._selectedItem)) Then
                    unselectItem(Me._selectedItem)
                End If

                Me._selectedItem = item

                item.onSelect()
                item.BackColor = SELECTED_ITEM_COLOR
                item.Font = New Font(item.Font.FontFamily, item.Font.Size, FontStyle.Bold)

                RaiseEvent ItemSelectedEvent(item.ItemObject)

            End If

            item.Focus()

        End Sub

        Protected Overridable Sub unselectItem(item As ListItem(Of MyType))

            item.onUnselect()
            item.BackColor = If(Me.listPanel.Controls.IndexOf(item) Mod 2 = 0, EVEN_ITEM_COLOR, ODD_ITEM_COLOR)
            item.Font = New Font(item.Font.FontFamily, item.Font.Size, FontStyle.Regular)

        End Sub

        Public MustOverride Sub addObject(obj As MyType)

        Protected Overridable Sub addItem(newItem As ListItem(Of MyType))

            Me._initialObjectList.Add(newItem.ItemObject)
            Me.initialItemList.Add(newItem)

            AddHandler newItem.ClickEvent, AddressOf selectItem
            AddHandler newItem.PreviewKeyDown, AddressOf _onKeyDown

            Me.NumberOfItems = Me.InitialObjectList.Count
            MyBase.Refresh()

        End Sub

        Public Overridable Sub removeItem(item As ListItem(Of MyType))

            ' #todo

        End Sub

        Public Sub addTitleBarButton(button As Control)
            Me.titleBarButtons.Add(button)
            Me.Controls.Add(button)
            Me.ajustLayout(Me.Size)
            button.BringToFront()
        End Sub

        ' #todo - remove

        Public Sub clear()

            Me._selectedItem = Nothing

            Me._initialObjectList.Clear()
            Me._displayedObjectList.Clear()

            Me.initialItemList.Clear()
            Me.displayedItemList.Clear()

            Me.listPanel.Controls.Clear()

            Me.NumberOfItems = 0

            MyBase.Refresh()

        End Sub

        Public Overridable Sub refreshList()

            Me.listPanel.Visible = False

            If (Not IsNothing(Me.SortMethod)) Then

                Me.initialItemList.Sort(Function(x As ListItem(Of MyType), y As ListItem(Of MyType))
                                            Return Me.SortMethod(x.ItemObject, y.ItemObject)
                                        End Function)
            End If

            Me._displayedObjectList.Clear()
            Me.displayedItemList.Clear()
            Me.listPanel.Controls.Clear()

            For Each item As ListItem(Of MyType) In Me.initialItemList

                If (Me.FilterMethod(item.ItemObject)) Then

                    item.BackColor = If(Me.displayedItemList.Count Mod 2 = 0, EVEN_ITEM_COLOR, ODD_ITEM_COLOR)
                    item.Location = New Point(0, Me.displayedItemList.Count * ItemsHeight)

                    Me._displayedObjectList.Add(item.ItemObject)
                    Me.displayedItemList.Add(item)
                    Me.listPanel.Controls.Add(item)

                End If
            Next

            If (Not IsNothing(Me._selectedItem)) Then
                Me._selectedItem.BackColor = SELECTED_ITEM_COLOR
            End If

            Me.NumberOfItems = Me.displayedItemList.Count

            ajustLayout(Me.Size)
            ajustLayoutFinal(Me.Size)

            Me.listPanel.Visible = True
            MyBase.Refresh()

        End Sub

        Public Function getSelectedObjects() As List(Of MyType)
            ' #todo
            Return Nothing
        End Function

        Protected WriteOnly Property NumberOfItems As Integer
            Set(value As Integer)

                If (value = 0) Then

                    Me.itemCountLabel.Text = "(vide)"

                Else
                    Me.itemCountLabel.Text = "(" & value & ")"
                End If
            End Set
        End Property

        Public WriteOnly Property Title As String
            Set(value As String)
                Me._title = value
                Me.titleLabel.Text = value
            End Set
        End Property

        Public Property ShowNumberOfItemsInTitle As Boolean
            Get
                Return Me._itemCountIsShowing
            End Get
            Set(value As Boolean)

                If (value) Then

                    If (Not Me._itemCountIsShowing) Then

                        Me.Controls.Add(Me.itemCountLabel)

                        Me._itemCountIsShowing = True

                        Me.ajustLayout(Me.Size)

                    End If

                Else

                    If (Me._itemCountIsShowing) Then

                        Me.Controls.Remove(Me.itemCountLabel)

                        Me._itemCountIsShowing = False

                        Me.ajustLayout(Me.Size)

                    End If

                End If

            End Set
        End Property

        Protected Overridable ReadOnly Property ItemsHeight As Integer
            Get
                Return ITEMS_HEIGHT
            End Get
        End Property

        Protected Sub _onKeyDown(sender As Object, e As PreviewKeyDownEventArgs)

            If (Not IsNothing(Me._selectedItem)) Then

                Dim selectedIndex As Integer = Me.displayedItemList.IndexOf(Me._selectedItem)

                If (e.KeyCode = Keys.Down AndAlso Not selectedIndex = Me.displayedItemList.Count - 1) Then

                    Me.selectItem(Me.displayedItemList(selectedIndex + 1))

                ElseIf (e.KeyCode = Keys.Up AndAlso Not selectedIndex = 0) Then

                    Me.selectItem(Me.displayedItemList(selectedIndex - 1))

                End If
            End If
        End Sub

        Public ReadOnly Property InitialObjectList As List(Of MyType)
            Get
                Return Me._initialObjectList
            End Get
        End Property

        Public ReadOnly Property DisplayedObjectList As List(Of MyType)
            Get
                Return Me._displayedObjectList
            End Get
        End Property

        Public ReadOnly Property SelectedObject As MyType
            Get
                Return If(IsNothing(Me._selectedItem), Nothing, Me._selectedItem.ItemObject)
            End Get
        End Property

        Private Sub _onFocus() Handles Me.GotFocus
            If (Not IsNothing(Me._selectedItem)) Then
                Me._selectedItem.Focus()
            End If
        End Sub

    End Class

End Namespace
