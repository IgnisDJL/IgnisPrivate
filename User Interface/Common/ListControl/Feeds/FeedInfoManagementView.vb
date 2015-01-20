Imports IGNIS.UI.Common

Namespace UI

    Public Class FeedInfoManagementView
        Inherits Panel
        Implements FeedsLayout

        ' Constants
        Public Shared ReadOnly ADD_NEW_FEED_BUTTONS_SIZE As Size = New Size(30, LayoutManager.FIELDS_HEIGHT)
        Public Shared ReadOnly UNKNOWN_FEED_LIST_VIEW_HEIGHT As Integer = 120
        Public Shared ReadOnly SPACE_BETWEEN_FIELDS_X As Integer = 5

        ' Components
        Private newFeedIndexField As TextBox
        Private WithEvents newFeedLocationField As TextField
        Private WithEvents newFeedMaterialField As TextField
        Private newFeedRecycledCheckBox As CheckBox
        Private newFeedFillerCheckBox As CheckBox
        Private newFeedAsphaltCheckBox As CheckBox
        Private WithEvents addNewFeedButton As Button

        Private _feedListView As FeedInfoListView
        Private unknownFeedsList As UnknownFeedListView

        ' Attributes
        Private _title As String
        Private _layoutType As FeedsLayout.LayoutType
        Private Delegate Sub ajustLayoutPartialDelegate()
        Private ajustLayoutPartial As ajustLayoutPartialDelegate

        ' Events
        Public Event AddNewFeedInfo(index As Integer, location As String, material As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)

        Public Sub New(title As String, layoutType As FeedsLayout.LayoutType)

            Me._title = title
            Me._layoutType = layoutType

            Me.initializeComponents()
        End Sub

        Private Sub initializeComponents()

            Me.newFeedIndexField = New TextBox
            Me.newFeedIndexField.TextAlign = HorizontalAlignment.Center
            Me.newFeedIndexField.ReadOnly = True

            Me.newFeedLocationField = New TextField
            Me.newFeedLocationField.PlaceHolder = "Emplacement"
            Me.newFeedLocationField.CanBeEmpty = False
            Me.newFeedLocationField.ValidationType = TextField.ValidationTypes.Text

            Me.newFeedMaterialField = New TextField
            Me.newFeedMaterialField.PlaceHolder = "Matériel"
            Me.newFeedMaterialField.CanBeEmpty = True
            Me.newFeedMaterialField.ValidationType = TextField.ValidationTypes.Text

            Me.newFeedRecycledCheckBox = New CheckBox
            Me.newFeedRecycledCheckBox.Text = "Recyclé"
            Me.newFeedRecycledCheckBox.TextAlign = ContentAlignment.TopCenter
            Me.newFeedRecycledCheckBox.CheckAlign = ContentAlignment.BottomCenter
            Me.newFeedRecycledCheckBox.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.newFeedFillerCheckBox = New CheckBox
            Me.newFeedFillerCheckBox.Text = "Filler"
            Me.newFeedFillerCheckBox.TextAlign = ContentAlignment.TopCenter
            Me.newFeedFillerCheckBox.CheckAlign = ContentAlignment.BottomCenter
            Me.newFeedFillerCheckBox.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.newFeedAsphaltCheckBox = New CheckBox
            Me.newFeedAsphaltCheckBox.Text = "Bitume"
            Me.newFeedAsphaltCheckBox.TextAlign = ContentAlignment.TopCenter
            Me.newFeedAsphaltCheckBox.CheckAlign = ContentAlignment.BottomCenter
            Me.newFeedAsphaltCheckBox.Font = Constants.UI.Fonts.SMALL_DEFAULT_FONT

            Me.addNewFeedButton = New Button
            Me.addNewFeedButton.Image = Constants.UI.Images._24x24.ADD
            Me.addNewFeedButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.addNewFeedButton.Enabled = False

            Me._feedListView = New FeedInfoListView(Me._title, Me._layoutType)

            Me.unknownFeedsList = New UnknownFeedListView()
            AddHandler Me.unknownFeedsList.ItemSelectedEvent, AddressOf Me.onUnknownFeedSelected

            Me.Controls.Add(Me.newFeedIndexField)
            Me.Controls.Add(Me.newFeedLocationField)
            Me.Controls.Add(Me.newFeedMaterialField)
            Me.Controls.Add(Me.newFeedRecycledCheckBox)
            Me.Controls.Add(Me.addNewFeedButton)
            Me.Controls.Add(Me._feedListView)
            Me.Controls.Add(Me.unknownFeedsList)

            Me.newFeedIndexField.TabStop = False
            Me.newFeedLocationField.TabIndex = 1
            Me.newFeedMaterialField.TabIndex = 2
            Me.newFeedRecycledCheckBox.TabIndex = 3
            Me.newFeedFillerCheckBox.TabIndex = 4
            Me.newFeedAsphaltCheckBox.TabIndex = 5
            Me.addNewFeedButton.TabIndex = 6

            Me.refreshLayout()

            AddHandler Me.newFeedLocationField.ValidationOccured, AddressOf Me.enableAddNewFeedButton
            AddHandler Me.newFeedMaterialField.ValidationOccured, AddressOf Me.enableAddNewFeedButton


        End Sub

        Public Sub ajustLayout(newSize As Size)
            Me.Size = newSize

            Me.newFeedIndexField.Location = New Point(0, 0)
            Me.newFeedIndexField.Size = New Size(35, LayoutManager.FIELDS_HEIGHT)

            Me.newFeedLocationField.Location = New Point(Me.newFeedIndexField.Location.X + Me.newFeedIndexField.Width + SPACE_BETWEEN_FIELDS_X, Me.newFeedIndexField.Location.Y)
            Me.newFeedLocationField.Size = New Size(150, LayoutManager.FIELDS_HEIGHT)

            Me.newFeedMaterialField.Location = New Point(Me.newFeedLocationField.Location.X + Me.newFeedLocationField.Width + SPACE_BETWEEN_FIELDS_X, Me.newFeedIndexField.Location.Y)
            Me.newFeedMaterialField.Size = New Size(150, LayoutManager.FIELDS_HEIGHT)

            Me.addNewFeedButton.Location = New Point(Me.Width - ADD_NEW_FEED_BUTTONS_SIZE.Width)
            Me.addNewFeedButton.Size = ADD_NEW_FEED_BUTTONS_SIZE

            Me.ajustLayoutPartial()

            Me._feedListView.Location = New Point(0, Me.newFeedIndexField.Location.Y + Me.newFeedIndexField.Height)
            Me._feedListView.ajustLayout(New Size(Me.Width, Me.Height - LayoutManager.FIELDS_HEIGHT - UNKNOWN_FEED_LIST_VIEW_HEIGHT))

            Me.unknownFeedsList.Location = New Point(0, Me.Height - UNKNOWN_FEED_LIST_VIEW_HEIGHT)
            Me.unknownFeedsList.ajustLayout(New Size(Me.Width, UNKNOWN_FEED_LIST_VIEW_HEIGHT))
        End Sub

        Public Sub ajustLayoutFinal(newSize As Size)

            Me.Size = newSize

            Me._feedListView.ajustLayoutFinal(New Size(Me.Width, Me.Height - LayoutManager.FIELDS_HEIGHT - UNKNOWN_FEED_LIST_VIEW_HEIGHT))
            Me.unknownFeedsList.ajustLayoutFinal(New Size(Me.Width, UNKNOWN_FEED_LIST_VIEW_HEIGHT))
        End Sub

        Public Sub updateLists(feeds As List(Of FeedInfoNode), unknownFeeds As List(Of UnknownFeedNode))

            Me._feedListView.clear()

            Dim highestIndex As Integer = 0

            For Each _feedInfo As FeedInfoNode In feeds
                Me._feedListView.addObject(_feedInfo)

                If (_feedInfo.INDEX > highestIndex) Then
                    highestIndex = _feedInfo.INDEX
                End If

            Next
            Me._feedListView.refreshList()

            Me.newFeedIndexField.Text = highestIndex + 1

            Me.unknownFeedsList.clear()

            For Each _unknownFeed As UnknownFeedNode In unknownFeeds
                Me.unknownFeedsList.addObject(_unknownFeed)
            Next
            Me.unknownFeedsList.refreshList()

        End Sub

        Private Sub ajustLayoutRecycledOnly() Implements FeedsLayout.ajustLayoutRecycledOnly

            Dim checkBoxesSize As Size = New Size(Me.getCheckBoxesAvailableWidth / 1, LayoutManager.FIELDS_HEIGHT + 5)

            Me.newFeedRecycledCheckBox.Location = New Point(Me.newFeedMaterialField.Location.X + Me.newFeedMaterialField.Width, Me.newFeedIndexField.Location.Y - 5)
            Me.newFeedRecycledCheckBox.Size = checkBoxesSize
        End Sub

        Private Sub ajustLayoutRecycledAndFiller() Implements FeedsLayout.ajustLayoutRecycledAndFiller

            Dim checkBoxesSize As Size = New Size(Me.getCheckBoxesAvailableWidth / 2, LayoutManager.FIELDS_HEIGHT + 5)

            Me.newFeedRecycledCheckBox.Location = New Point(Me.newFeedMaterialField.Location.X + Me.newFeedMaterialField.Width, Me.newFeedIndexField.Location.Y - 5)
            Me.newFeedRecycledCheckBox.Size = checkBoxesSize

            Me.newFeedFillerCheckBox.Location = New Point(Me.newFeedRecycledCheckBox.Location.X + Me.newFeedRecycledCheckBox.Width, Me.newFeedIndexField.Location.Y - 5)
            Me.newFeedFillerCheckBox.Size = checkBoxesSize
        End Sub

        Private Sub ajustLayoutRecycledFillerAndAsphalt() Implements FeedsLayout.ajustLayoutRecycledFillerAndAsphalt

            Dim checkBoxesSize As Size = New Size(Me.getCheckBoxesAvailableWidth / 3, LayoutManager.FIELDS_HEIGHT + 5)

            Me.newFeedRecycledCheckBox.Location = New Point(Me.newFeedMaterialField.Location.X + Me.newFeedMaterialField.Width, Me.newFeedIndexField.Location.Y - 5)
            Me.newFeedRecycledCheckBox.Size = checkBoxesSize

            Me.newFeedFillerCheckBox.Location = New Point(Me.newFeedRecycledCheckBox.Location.X + Me.newFeedRecycledCheckBox.Width, Me.newFeedIndexField.Location.Y - 5)
            Me.newFeedFillerCheckBox.Size = checkBoxesSize

            Me.newFeedAsphaltCheckBox.Location = New Point(Me.newFeedFillerCheckBox.Location.X + Me.newFeedFillerCheckBox.Width, Me.newFeedIndexField.Location.Y - 5)
            Me.newFeedAsphaltCheckBox.Size = checkBoxesSize
        End Sub

        Private Function getCheckBoxesAvailableWidth() As Double

            Return Me.Width - 2 * SPACE_BETWEEN_FIELDS_X - Me.newFeedIndexField.Width - Me.newFeedLocationField.Width - Me.newFeedMaterialField.Width - Me.addNewFeedButton.Width
        End Function

        Public WriteOnly Property LayoutType As FeedsLayout.LayoutType Implements FeedsLayout.Layout
            Set(value As FeedsLayout.LayoutType)
                Me._layoutType = value
                Me.FeedListView.LayoutType = value
                Me.refreshLayout()
            End Set
        End Property

        Public WriteOnly Property Title As String
            Set(value As String)
                Me._title = value
                Me.FeedListView.Title = value
            End Set
        End Property

        Private Sub refreshLayout() Implements FeedsLayout.refreshLayout

            Me.Controls.Remove(Me.newFeedFillerCheckBox)
            Me.Controls.Remove(Me.newFeedAsphaltCheckBox)

            Select Case Me._layoutType

                Case FeedsLayout.LayoutType.RECYCLED_ONLY
                    Me.ajustLayoutPartial = AddressOf ajustLayoutRecycledOnly

                Case FeedsLayout.LayoutType.RECYCLED_AND_FILLER
                    Me.ajustLayoutPartial = AddressOf ajustLayoutRecycledAndFiller

                    Me.Controls.Add(Me.newFeedFillerCheckBox)

                Case FeedsLayout.LayoutType.RECYCLED_FILLER_AND_ASPHALT
                    Me.ajustLayoutPartial = AddressOf ajustLayoutRecycledFillerAndAsphalt

                    Me.Controls.Add(Me.newFeedFillerCheckBox)
                    Me.Controls.Add(Me.newFeedAsphaltCheckBox)
            End Select

        End Sub

        Private Sub addNewFeed() Handles addNewFeedButton.Click

            RaiseEvent AddNewFeedInfo(CInt(Me.newFeedIndexField.Text), Me.newFeedLocationField.Text, Me.newFeedMaterialField.Text, Me.newFeedRecycledCheckBox.Checked, Me.newFeedFillerCheckBox.Checked, Me.newFeedAsphaltCheckBox.Checked)

            Me.newFeedLocationField.DefaultText = ""
            Me.newFeedMaterialField.DefaultText = ""
            Me.newFeedRecycledCheckBox.Checked = False
            Me.newFeedFillerCheckBox.Checked = False
            Me.newFeedAsphaltCheckBox.Checked = False

        End Sub

        Private Sub enableAddNewFeedButton()

            If (Me.newFeedLocationField.IsValid AndAlso _
                Me.newFeedMaterialField.IsValid) Then

                Me.addNewFeedButton.Enabled = True
            Else
                Me.addNewFeedButton.Enabled = False
            End If
        End Sub

        Private Sub addNewFeedOnEnter(sender As Object, e As KeyEventArgs) Handles newFeedLocationField.KeyDown, newFeedMaterialField.KeyDown

            If (e.KeyCode = Keys.Enter And Me.addNewFeedButton.Enabled) Then
                Me.addNewFeed()
            End If
        End Sub

        Private Sub onUnknownFeedSelected(unknownFeed As UnknownFeedNode)
            Me.newFeedLocationField.Focus()
            Me.newFeedLocationField.DefaultText = unknownFeed.LOCATION
        End Sub

        Public ReadOnly Property FeedListView As FeedInfoListView
            Get
                Return Me._feedListView
            End Get
        End Property
    End Class
End Namespace
