Namespace UI

    Public Class UnknownFeedListItem
        Inherits Common.ListItem(Of UnknownFeedNode)

        ' Constants
        Public Shared ReadOnly SPACE_BETWEEN_CONTROLS_X As Integer = 5

        ' Components
        Private WithEvents locationLabel As Label
        Private WithEvents materialLabel As Label

        ' Attributes


        Public Sub New(unknownFeed As UnknownFeedNode)
            MyBase.New(unknownFeed)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.locationLabel = New Label
            Me.locationLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.locationLabel.Text = Me.ItemObject.LOCATION

            Me.materialLabel = New Label
            Me.materialLabel.TextAlign = ContentAlignment.MiddleLeft
            Me.materialLabel.Text = Me.ItemObject.MATERIAL

            Me.Controls.Add(Me.locationLabel)
            Me.Controls.Add(Me.materialLabel)

            AddHandler Me.Click, AddressOf Me.raiseClickEvent
            AddHandler Me.locationLabel.Click, AddressOf Me.raiseClickEvent
            AddHandler Me.materialLabel.Click, AddressOf Me.raiseClickEvent

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.locationLabel.Location = New Point(35 + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.locationLabel.Size = New Size(150, Me.Height)

            Me.materialLabel.Location = New Point(Me.locationLabel.Location.X + Me.locationLabel.Width + SPACE_BETWEEN_CONTROLS_X, 0)
            Me.materialLabel.Size = New Size(150, Me.Height)

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub
    End Class
End Namespace
