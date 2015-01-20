Namespace UI.Common

    Public Class FileListControl
        Inherits ListControlTemplate(Of File)

        ' Constants
        Public Shared ReadOnly FILE_TYPE_PRIORITY As Type() = {Type.GetType("IGNIS.MDBFile"), _
                                                               Type.GetType("IGNIS.CSVFile"), _
                                                               Type.GetType("IGNIS.LOGFile"), _
                                                               Type.GetType("IGNIS.EventsFile"), _
                                                               Type.GetType("IGNIS.SummaryDailyReport"), _
                                                               Type.GetType("IGNIS.SummaryPeriodicReport")}

        ' Components
        Private WithEvents checkAllCheckBox As CheckBox
        Private checkAllCheckBoxToolTip As ToolTip

        ' Attributes
        Private _checkableItems As Boolean = False
        Private _checkedFiles As List(Of File)

        Private _updatingCheckAllCheckBoxState As Boolean = False
        Private _updatingItemsCheckState As Boolean = False
        Private _applyingCheckAll As Boolean = False

        Public Property DisplayReportsFirst As Boolean = False

        Public Property DisplayDatesForMDBFiles As Boolean = True

        ' Events
        Public Event ItemChecked(file As File, checked As Boolean)

        Public Sub New(title As String)
            MyBase.New(title)

            Me.SortMethod = AddressOf Me.compareTo
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

        Private Function compareTo(x As File, y As File) As Integer

            Dim xType As Type = x.GetType
            Dim yType As Type = y.GetType

            If (xType.Equals(yType)) Then

                If (TypeOf x Is ReportFile) Then

                    Dim xVal = If(DirectCast(x, ReportFile).IS_READ_ONLY, 1, 0)
                    Dim yVal = If(DirectCast(y, ReportFile).IS_READ_ONLY, 1, 0)

                    Return y.Date_.CompareTo(x.Date_) - xVal.CompareTo(yVal) * 10

                Else
                    Return y.Date_.CompareTo(x.Date_)
                End If

            Else

                Dim xTypeValue = 0
                Dim yTypeValue = 0

                For i = 0 To FILE_TYPE_PRIORITY.Count - 1

                    If (xType.Equals(FILE_TYPE_PRIORITY(i))) Then
                        xTypeValue = i
                    End If

                    If (yType.Equals(FILE_TYPE_PRIORITY(i))) Then
                        yTypeValue = i
                    End If
                Next

                If (Me.DisplayReportsFirst) Then

                    If (TypeOf x Is ReportFile) Then
                        xTypeValue -= 10

                    End If

                    If (TypeOf y Is ReportFile) Then
                        yTypeValue -= 10

                    End If
                End If

                Return xTypeValue.CompareTo(yTypeValue)
            End If

        End Function

        Public Overrides Sub addObject(obj As File)

            Dim iconInfo = Constants.UI.Images.IconFactory.getIconFor(obj)

            Dim newItem = New FileListItem(obj, iconInfo._24x24, iconInfo.Caption)
            newItem.ShowCheckBox = Me.CheckableItems
            newItem.DisplayDateForMDB = Me.DisplayDatesForMDBFiles

            AddHandler newItem.CheckedChange, AddressOf Me.raiseCheckChangeEvent

            Me.addItem(newItem)

            Me.lastInsertedItem = newItem
        End Sub

        Public Overrides Sub refreshList()
            MyBase.refreshList()

            If (Me.CheckableItems) Then

                Me._updatingItemsCheckState = True

                For Each item As FileListItem In Me.displayedItemList

                    item.IsChecked = Me._checkedFiles.Contains(item.ItemObject)
                Next

                Me._updatingItemsCheckState = False

                Me.updateCheckAllCheckBoxState()
            End If
        End Sub

        Private Sub raiseCheckChangeEvent(item As FileListItem, checked As Boolean)

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

                For Each item As FileListItem In Me.displayedItemList
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

                For Each item As FileListItem In Me.displayedItemList

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

        Public Sub setCheckFilesList(ByRef list As List(Of File))
            Me._checkedFiles = list
        End Sub
    End Class
End Namespace
