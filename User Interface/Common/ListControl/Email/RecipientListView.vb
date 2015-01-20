Namespace UI.Common

    Public Class RecipientListView
        Inherits Common.ListControlTemplate(Of EmailRecipient)

        ' Components
        Private WithEvents checkAllCheckBox As CheckBox
        Private checkAllCheckBoxToolTip As ToolTip

        ' Attributes
        Private _checkableItems As Boolean = False

        Private _updatingCheckAllCheckBoxState As Boolean = False
        Private _updatingItemsCheckState As Boolean = False
        Private _applyingCheckAll As Boolean = False

        ' Events
        Public Event deleteRecipient(recipient As EmailRecipient)
        Public Event updateRecipient(recipient As EmailRecipient, newAddress As String)
        Public Event ItemChecked(recipient As EmailRecipient, checked As Boolean)

        Public Sub New(title As String)
            MyBase.New(title)

        End Sub

        Protected Overrides Sub initializeComponents()
            MyBase.initializeComponents()

            Me.checkAllCheckBox = New CheckBox
            Me.checkAllCheckBox.CheckAlign = ContentAlignment.MiddleCenter
            Me.checkAllCheckBox.Size = New Size(Me.ItemsHeight, TITLE_LABEL_HEIGHT)
            Me.checkAllCheckBox.Cursor = Cursors.Hand
            Me.checkAllCheckBox.Enabled = False

            Me.checkAllCheckBoxToolTip = New ToolTip
        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)
            MyBase.ajustLayout(newSize)

            If (Me._checkableItems) Then

                Me.titleLabel.Width -= Me.checkAllCheckBox.Width
                Me.itemCountLabel.Location = New Point(Me.itemCountLabel.Location.X - Me.checkAllCheckBox.Width, Me.itemCountLabel.Location.Y)
                Me.checkAllCheckBox.Location = New Point(Me.ClientSize.Width - Me.checkAllCheckBox.Width, 2)
            End If
        End Sub

        Public Overrides Sub addObject(obj As EmailRecipient)

            If (Me.CheckableItems) Then

                Dim newItem = New CheckableRecipientListItem(obj)

                AddHandler newItem.CheckedChange, AddressOf Me.raiseCheckChangeEvent

                Me.addItem(newItem)

            Else ' Editable items...

                Dim newItem = New EditableRecipientListItem(obj)

                AddHandler newItem.deleteRecipient, AddressOf Me.raiseDeleteEvent
                AddHandler newItem.updateRecipient, AddressOf Me.raiseUpdateEvent

                Me.addItem(newItem)
            End If

            ' #todo - when clear() is called, these should be unbound
            ' or #refactor, find a better way to pass the events
        End Sub

        Public Overrides Sub refreshList()
            MyBase.refreshList()

            If (Me.CheckableItems) Then
                Me.updateCheckAllCheckBoxState()
            End If
        End Sub

        Private Sub raiseDeleteEvent(recipient As EmailRecipient)

            RaiseEvent deleteRecipient(recipient)

        End Sub

        Private Sub raiseUpdateEvent(recipient As EmailRecipient, newAddress As String)

            RaiseEvent updateRecipient(recipient, newAddress)

        End Sub

        Private Sub raiseCheckChangeEvent(item As CheckableRecipientListItem, checked As Boolean)

            If (Not Me._updatingItemsCheckState) Then

                RaiseEvent ItemChecked(item.ItemObject, checked)
            End If

            If (Not Me._applyingCheckAll) Then
                Me.updateCheckAllCheckBoxState()
            End If
        End Sub

        Private Sub toggleCheckAllItems() Handles checkAllCheckBox.CheckedChanged

            If (Not Me._updatingCheckAllCheckBoxState) Then

                Me._applyingCheckAll = True

                For Each item As CheckableRecipientListItem In Me.displayedItemList
                    item.IsChecked = Me.checkAllCheckBox.Checked
                Next

                Me._applyingCheckAll = False
            End If

            Me.updateCheckAllCheckBoxState()
        End Sub

        Public Property CheckableItems As Boolean
            Get
                Return Me._checkableItems
            End Get
            Set(itemsAreCheckable As Boolean)

                If (Not Me._checkableItems = itemsAreCheckable) Then

                    If (itemsAreCheckable) Then

                        Me.Controls.Add(Me.checkAllCheckBox)

                    Else

                        Me.Controls.Remove(Me.checkAllCheckBox)

                    End If

                    Me._checkableItems = itemsAreCheckable
                End If

            End Set
        End Property

        Private Sub updateCheckAllCheckBoxState()
            Me._updatingCheckAllCheckBoxState = True

            If (Me.displayedItemList.Count = 0) Then

                Me.checkAllCheckBox.Enabled = False

            Else

                Me.checkAllCheckBox.Enabled = True

                Dim allItemsAreSelected As Boolean = True

                For Each item As CheckableRecipientListItem In Me.displayedItemList

                    If (Not item.IsChecked) Then
                        allItemsAreSelected = False

                        Exit For
                    End If
                Next

                If (allItemsAreSelected) Then

                    Me.checkAllCheckBox.Checked = True
                    Me.checkAllCheckBoxToolTip.SetToolTip(Me.checkAllCheckBox, "Tout déséléctionner")
                Else
                    Me.checkAllCheckBox.Checked = False
                    Me.checkAllCheckBoxToolTip.SetToolTip(Me.checkAllCheckBox, "Tout séléctionner")
                End If
            End If

            Me._updatingCheckAllCheckBoxState = False
        End Sub

    End Class
End Namespace
