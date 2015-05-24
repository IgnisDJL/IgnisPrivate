Imports IGNIS.UI.Common

Public Class CatalogListItem
    Inherits ListViewItem

    Private itemId_label As Label
    Private customName_label As Label
    Private effectiveDate_label As Label

    Private itemId_textField As TextField
    Private customName_textField As TextField

    Private WithEvents deleteButton As Button
    Private WithEvents editButton As Button

    Private WithEvents confirmEditButton As Button
    Private WithEvents cancelEditButton As Button

    Public Sub New(itemId As String, effectiveDate As Date, actualCustomName As String)
        MyBase.New()
        InitializeComponent()

        itemId_label.Text = itemId
        effectiveDate_label.Text = effectiveDate.ToString
        customName_label.Text = actualCustomName

    End Sub

    Private Sub InitializeComponent()

        itemId_label = New Label
        customName_label = New Label
        effectiveDate_label = New Label

        itemId_textField = New TextField
        customName_textField = New TextField

        Me.deleteButton = New Button
        Me.deleteButton.Image = Constants.UI.Images._16x16.DELETE
        Me.deleteButton.ImageAlign = ContentAlignment.MiddleCenter
        Me.deleteButton.Size = New Size(25, 25)
        Me.deleteButton.BackColor = Me.BackColor

        Me.editButton = New Button
        Me.editButton.Image = Constants.UI.Images._16x16.EDIT
        Me.editButton.ImageAlign = ContentAlignment.MiddleCenter
        Me.editButton.Size = New Size(25, 25)
        Me.editButton.BackColor = Me.BackColor

        Me.cancelEditButton = New Button
        Me.cancelEditButton.Image = Constants.UI.Images._16x16.WRONG
        Me.cancelEditButton.ImageAlign = ContentAlignment.MiddleCenter
        Me.cancelEditButton.Size = New Size(25, 25)
        Me.cancelEditButton.BackColor = Me.BackColor
        Me.cancelEditButton.Enabled = False
        Me.cancelEditButton.Visible = False

        Me.confirmEditButton = New Button
        Me.confirmEditButton.Image = Constants.UI.Images._16x16.GOOD
        Me.confirmEditButton.ImageAlign = ContentAlignment.MiddleCenter
        Me.confirmEditButton.Size = New Size(25, 25)
        Me.confirmEditButton.BackColor = Me.BackColor
        Me.confirmEditButton.Enabled = False
        Me.confirmEditButton.Visible = False

    End Sub


End Class
