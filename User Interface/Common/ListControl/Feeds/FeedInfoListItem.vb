Imports IGNIS.UI.Common

Namespace UI

    Public Class FeedInfoListItem
        Inherits Common.ListItem(Of FeedInfoNode)
        Implements FeedsLayout

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5
        Public Shared ReadOnly BUTTONS_SIZE As Size = New Size(25, 25)

        ' Components
        Private WithEvents indexLabel As Label
        Private WithEvents locationLabel As Label
        Private WithEvents locationField As TextField
        Private WithEvents materialLabel As Label
        Private WithEvents materialField As TextField
        Private WithEvents recycledCheckBox As CheckBox
        Private WithEvents fillerCheckBox As CheckBox
        Private WithEvents asphaltCheckBox As CheckBox

        Private WithEvents deleteButton As Button
        Private WithEvents editButton As Button

        Private WithEvents confirmEditButton As Button
        Private WithEvents cancelEditButton As Button

        ' Attritbutes
        Private _layoutType As FeedsLayout.LayoutType
        Private Delegate Sub ajustLayoutPartialDelegate()
        Private ajustLayoutPartial As ajustLayoutPartialDelegate

        ' Events
        Public Event DeleteFeedInfo(feedInfo As FeedInfoNode)
        Public Event UpdateFeedInfo(feedInfo As FeedInfoNode, newIndex As Integer, newLocation As String, newMaterial As String, isRecycled As Boolean, isFiller As Boolean, isAsphalt As Boolean)


        Public Sub New(feedInfo As FeedInfoNode, layoutType As FeedsLayout.LayoutType)
            MyBase.New(feedInfo)

            Me._layoutType = layoutType

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.indexLabel = New Label
            Me.indexLabel.TextAlign = ContentAlignment.MiddleCenter
            Me.indexLabel.Text = Me.ItemObject.INDEX

            Me.locationLabel = New Label
            Me.locationLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.locationLabel.Text = Me.ItemObject.LOCATION

            Me.locationField = New TextField
            Me.locationField.AutoSize = False
            Me.locationField.TextAlign = HorizontalAlignment.Left
            Me.locationField.CanBeEmpty = False
            Me.locationField.ValidationType = TextField.ValidationTypes.Text
            Me.locationField.DefaultText = Me.ItemObject.LOCATION

            Me.materialLabel = New Label
            Me.materialLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.materialLabel.Text = Me.ItemObject.MATERIAL

            Me.materialField = New TextField
            Me.materialField.AutoSize = False
            Me.materialField.TextAlign = HorizontalAlignment.Left
            Me.materialField.CanBeEmpty = True
            Me.materialField.ValidationType = TextField.ValidationTypes.Text
            Me.materialField.DefaultText = Me.ItemObject.MATERIAL

            Me.recycledCheckBox = New CheckBox
            Me.recycledCheckBox.Enabled = False
            Me.recycledCheckBox.Checked = Me.ItemObject.IS_RECYCLED
            Me.recycledCheckBox.CheckAlign = ContentAlignment.MiddleCenter

            Me.fillerCheckBox = New CheckBox
            Me.fillerCheckBox.Enabled = False
            Me.fillerCheckBox.Checked = Me.ItemObject.IS_FILLER
            Me.fillerCheckBox.CheckAlign = ContentAlignment.MiddleCenter

            Me.asphaltCheckBox = New CheckBox
            Me.asphaltCheckBox.Enabled = False
            Me.asphaltCheckBox.Checked = Me.ItemObject.IS_ASPHALT
            Me.asphaltCheckBox.CheckAlign = ContentAlignment.MiddleCenter

            Me.deleteButton = New Button
            Me.deleteButton.Image = Constants.UI.Images._16x16.DELETE
            Me.deleteButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.deleteButton.Size = BUTTONS_SIZE
            Me.deleteButton.BackColor = Me.BackColor

            Me.editButton = New Button
            Me.editButton.Image = Constants.UI.Images._16x16.EDIT
            Me.editButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.editButton.Size = BUTTONS_SIZE
            Me.editButton.BackColor = Me.BackColor

            Me.cancelEditButton = New Button
            Me.cancelEditButton.Image = Constants.UI.Images._16x16.WRONG
            Me.cancelEditButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.cancelEditButton.Size = BUTTONS_SIZE
            Me.cancelEditButton.BackColor = Me.BackColor

            Me.confirmEditButton = New Button
            Me.confirmEditButton.Image = Constants.UI.Images._16x16.GOOD
            Me.confirmEditButton.ImageAlign = ContentAlignment.MiddleCenter
            Me.confirmEditButton.Size = BUTTONS_SIZE
            Me.confirmEditButton.BackColor = Me.BackColor
            Me.confirmEditButton.Enabled = False

            Me.Controls.Add(Me.indexLabel)
            Me.Controls.Add(Me.locationLabel)
            Me.Controls.Add(Me.materialLabel)
            Me.Controls.Add(Me.recycledCheckBox)

            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            Me.cancelEditButton.TabStop = False
            Me.confirmEditButton.TabStop = False
            Me.deleteButton.TabStop = False
            Me.editButton.TabStop = False

            Me.refreshLayout()

            AddHandler Me.locationField.ValidationOccured, AddressOf Me.enableConfirmEditButton
            AddHandler Me.materialField.ValidationOccured, AddressOf Me.enableConfirmEditButton

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.indexLabel.Location = New Point(0, 0)
            Me.indexLabel.Size = New Size(35, Me.Height)

            Me.locationLabel.Location = New Point(Me.indexLabel.Location.X + Me.indexLabel.Width + SPACE_BETWEEN_CONTROLS_X, Me.indexLabel.Location.Y)
            Me.locationLabel.Size = New Size(150, Me.Height)

            Me.locationField.Location = Me.locationLabel.Location
            Me.locationField.Size = Me.locationLabel.Size

            Me.materialLabel.Location = New Point(Me.locationLabel.Location.X + Me.locationLabel.Width + SPACE_BETWEEN_CONTROLS_X, Me.indexLabel.Location.Y)
            Me.materialLabel.Size = New Size(150, Me.Height)

            Me.materialField.Location = Me.materialLabel.Location
            Me.materialField.Size = Me.materialLabel.Size

            Me.deleteButton.Location = New Point(newSize.Width - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)
            Me.editButton.Location = New Point(Me.deleteButton.Location.X - BUTTONS_SIZE.Width - SPACE_BETWEEN_CONTROLS_X, (newSize.Height - BUTTONS_SIZE.Height) / 2)

            Me.cancelEditButton.Location = Me.deleteButton.Location
            Me.confirmEditButton.Location = Me.editButton.Location

            Me.ajustLayoutPartial()

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub ajustLayoutRecycledOnly() Implements FeedsLayout.ajustLayoutRecycledOnly

            Dim checkBoxesSize As Size = New Size(getCheckBoxesAvailableWidth() / 1, Me.Height)

            Me.recycledCheckBox.Location = New Point(Me.materialLabel.Location.X + Me.materialLabel.Width, Me.indexLabel.Location.Y)
            Me.recycledCheckBox.Size = checkBoxesSize

        End Sub

        Private Sub ajustLayoutRecycledAndFiller() Implements FeedsLayout.ajustLayoutRecycledAndFiller

            Dim checkBoxesSize As Size = New Size(getCheckBoxesAvailableWidth() / 2, Me.Height)

            Me.recycledCheckBox.Location = New Point(Me.materialLabel.Location.X + Me.materialLabel.Width, Me.indexLabel.Location.Y)
            Me.recycledCheckBox.Size = checkBoxesSize

            Me.fillerCheckBox.Location = New Point(Me.recycledCheckBox.Location.X + Me.recycledCheckBox.Width, Me.indexLabel.Location.Y)
            Me.fillerCheckBox.Size = checkBoxesSize

        End Sub

        Private Sub ajustLayoutRecycledFillerAndAsphalt() Implements FeedsLayout.ajustLayoutRecycledFillerAndAsphalt

            Dim checkBoxesSize As Size = New Size(getCheckBoxesAvailableWidth() / 3, Me.Height)

            Me.recycledCheckBox.Location = New Point(Me.materialLabel.Location.X + Me.materialLabel.Width, Me.indexLabel.Location.Y)
            Me.recycledCheckBox.Size = checkBoxesSize

            Me.fillerCheckBox.Location = New Point(Me.recycledCheckBox.Location.X + Me.recycledCheckBox.Width, Me.indexLabel.Location.Y)
            Me.fillerCheckBox.Size = checkBoxesSize

            Me.asphaltCheckBox.Location = New Point(Me.fillerCheckBox.Location.X + Me.fillerCheckBox.Width, Me.indexLabel.Location.Y)
            Me.asphaltCheckBox.Size = checkBoxesSize

        End Sub

        Private Function getCheckBoxesAvailableWidth() As Double

            Return Me.Width - 4 * SPACE_BETWEEN_CONTROLS_X - Me.indexLabel.Width - Me.locationLabel.Width - Me.materialLabel.Width - 2 * BUTTONS_SIZE.Width
        End Function

        Public Shadows WriteOnly Property Layout As FeedsLayout.LayoutType Implements FeedsLayout.Layout
            Set(value As FeedsLayout.LayoutType)

                If (Not Me._layoutType = value) Then

                    Me._layoutType = value
                    Me.refreshLayout()
                End If
            End Set
        End Property

        Private Sub refreshLayout() Implements FeedsLayout.refreshLayout

            Me.Controls.Remove(Me.fillerCheckBox)
            Me.Controls.Remove(Me.asphaltCheckBox)

            Select Case Me._layoutType

                Case FeedsLayout.LayoutType.RECYCLED_ONLY
                    Me.ajustLayoutPartial = AddressOf Me.ajustLayoutRecycledOnly

                Case FeedsLayout.LayoutType.RECYCLED_AND_FILLER
                    Me.ajustLayoutPartial = AddressOf Me.ajustLayoutRecycledAndFiller

                    Me.Controls.Add(Me.fillerCheckBox)

                Case FeedsLayout.LayoutType.RECYCLED_FILLER_AND_ASPHALT
                    Me.ajustLayoutPartial = AddressOf Me.ajustLayoutRecycledFillerAndAsphalt

                    Me.Controls.Add(Me.fillerCheckBox)
                    Me.Controls.Add(Me.asphaltCheckBox)
            End Select

        End Sub

        Private Sub _onClick() Handles Me.Click, indexLabel.Click, locationLabel.Click, materialLabel.Click

            Me.raiseClickEvent()
        End Sub

        Private Sub raiseDeleteEvent() Handles deleteButton.Click

            RaiseEvent DeleteFeedInfo(Me.ItemObject)

        End Sub

        Private Sub raiseUpdateEvent() Handles confirmEditButton.Click

            RaiseEvent UpdateFeedInfo(Me.ItemObject, CInt(Me.indexLabel.Text), Me.locationField.Text, Me.materialField.Text, Me.recycledCheckBox.Checked, Me.fillerCheckBox.Checked, Me.asphaltCheckBox.Checked)

        End Sub

        Private Sub cancelEditing() Handles cancelEditButton.Click

            enterReadMode()

        End Sub

        Private Sub enterWriteMode() Handles editButton.Click

            Me._currentMode = Mode.WRITE

            Me.Controls.Remove(Me.locationLabel)
            Me.Controls.Remove(Me.materialLabel)
            Me.Controls.Remove(Me.deleteButton)
            Me.Controls.Remove(Me.editButton)

            Me.Controls.Add(Me.locationField)
            Me.Controls.Add(Me.materialField)
            Me.Controls.Add(Me.confirmEditButton)
            Me.Controls.Add(Me.cancelEditButton)

            Me.recycledCheckBox.Enabled = True
            Me.fillerCheckBox.Enabled = True
            Me.asphaltCheckBox.Enabled = True

            Me.locationField.DefaultText = Me.locationLabel.Text
            Me.materialField.DefaultText = Me.materialLabel.Text

        End Sub

        Private Sub enableConfirmEditButton()

            If (Me.locationField.IsValid AndAlso _
                Me.materialField.IsValid) Then

                Me.confirmEditButton.Enabled = True
            Else
                Me.confirmEditButton.Enabled = False
            End If
        End Sub

        Private Sub enterReadMode()

            Me._currentMode = Mode.READ

            Me.Controls.Remove(Me.locationField)
            Me.Controls.Remove(Me.materialField)
            Me.Controls.Remove(Me.confirmEditButton)
            Me.Controls.Remove(Me.cancelEditButton)

            Me.Controls.Add(Me.locationLabel)
            Me.Controls.Add(Me.materialLabel)
            Me.Controls.Add(Me.deleteButton)
            Me.Controls.Add(Me.editButton)

            Me.recycledCheckBox.Enabled = False
            Me.fillerCheckBox.Enabled = False
            Me.asphaltCheckBox.Enabled = False

        End Sub

        Private Sub confirmOnEnter(sender As Object, e As KeyEventArgs) Handles locationField.KeyDown, materialField.KeyDown

            If (e.KeyCode = Keys.Enter) Then

                If (Me.confirmEditButton.Enabled) Then

                    raiseUpdateEvent()
                Else
                    Beep()
                End If

            ElseIf (e.KeyCode = Keys.Escape) Then

                cancelEditing()
            End If
        End Sub
    End Class
End Namespace
