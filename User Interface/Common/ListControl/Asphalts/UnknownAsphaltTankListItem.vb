Namespace UI

    Public Class UnknownAsphaltTankListItem
        Inherits Common.ListItem(Of XmlSettings.AsphaltNode.UnknownTankNode)

        ' Constants

        ' Components
        Private WithEvents tankNameLabel As Label
        Private WithEvents asphaltNameLabel As Label

        ' Attributes

        ' Events

        Public Sub New(unknownTankInfo As XmlSettings.AsphaltNode.UnknownTankNode)
            MyBase.New(unknownTankInfo)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.tankNameLabel = New Label
            Me.tankNameLabel.Text = Me.ItemObject.TANK_NAME

            Me.asphaltNameLabel = New Label
            Me.asphaltNameLabel.Text = Me.ItemObject.ASPHALT_NAME

            Me.Controls.Add(Me.tankNameLabel)
            Me.Controls.Add(Me.asphaltNameLabel)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)

            Me.Size = newSize

            Me.tankNameLabel.Location = New Point(MixAndAsphaltSettingsViewLayout.SPACE_BETWEEN_CONTROLS_X, 0)
            Me.tankNameLabel.Size = New Size(Me.Width * 3 / 8, Me.Height)

            Me.asphaltNameLabel.Location = New Point(Me.tankNameLabel.Location.X + Me.tankNameLabel.Size.Width + MixAndAsphaltSettingsViewLayout.SPACE_BETWEEN_CONTROLS_X, 0)
            Me.asphaltNameLabel.Size = Me.tankNameLabel.Size

        End Sub

        Public Overrides Sub onSelect()

        End Sub

        Public Overrides Sub onUnselect()

        End Sub

        Private Sub _onClick() Handles Me.Click, tankNameLabel.Click, asphaltNameLabel.Click

            Me.raiseClickEvent()

        End Sub
    End Class
End Namespace

