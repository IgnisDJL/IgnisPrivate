
Namespace UI.Common

    Public Class CheckableRecipientListItem
        Inherits RecipientListItem

        ' Components
        Private WithEvents checkBox As CheckBox

        ' Attributes

        ' Events
        Public Event CheckedChange(item As CheckableRecipientListItem, checked As Boolean)

        Public Sub New(recipient As EmailRecipient)
            MyBase.New(recipient)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.addressLabel = New Label
            Me.addressLabel.AutoSize = False
            Me.addressLabel.Text = Me.ItemObject.Address
            Me.addressLabel.TextAlign = ContentAlignment.MiddleLeft

            Me.checkBox = New CheckBox
            Me.checkBox.CheckAlign = ContentAlignment.MiddleCenter
            Me.checkBox.Cursor = Cursors.Hand
            Me.checkBox.Checked = Me.ItemObject.Selected

            Me.Controls.Add(Me.addressLabel)
            Me.Controls.Add(Me.checkBox)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.checkBox.Location = New Point(Me.Width - Me.Height, 0)
            Me.checkBox.Size = New Size(Me.Height, Me.Height)

            Me.addressLabel.Location = New Point(SPACE_BETWEEN_CONTROLS_X, 0)
            Me.addressLabel.Size = New Size(newSize.Width - Me.checkBox.Width, Me.Height)

        End Sub

        Private Sub onChecked() Handles checkBox.CheckedChanged

            Me.ItemObject.Selected = Me.checkBox.Checked
            RaiseEvent CheckedChange(Me, Me.checkBox.Checked)

        End Sub

        Public Property IsChecked As Boolean
            Get
                Return Me.checkBox.Checked
            End Get
            Set(value As Boolean)
                Me.checkBox.Checked = value
            End Set
        End Property

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, addressLabel.Click

            raiseClickEvent()

        End Sub

    End Class
End Namespace
