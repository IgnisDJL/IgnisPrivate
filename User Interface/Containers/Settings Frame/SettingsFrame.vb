Namespace UI

    Public Class SettingsFrame
        Inherits View

        ' Constants


        ' Components
        Private settingsMenuPanel As Panel

        Private settingsFormPanel As Panel

        Private buttonsPanel As Panel

        ' Attributes
        Private selectedView As SettingsView
        Private views As List(Of SettingsView)

        Private menuItems As List(Of SettingsMenuItem)

        Public Sub New()
            MyBase.New()

            Me.layout = New SettingsFrameLayout

            Me.views = New List(Of SettingsView)

            Me.menuItems = New List(Of SettingsMenuItem)

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.settingsMenuPanel = New Panel
            Me.settingsMenuPanel.BackColor = Color.Blue

            Me.settingsFormPanel = New Panel
            Me.settingsFormPanel.BorderStyle = Windows.Forms.BorderStyle.Fixed3D

            Me.buttonsPanel = New Panel

            Me.Controls.Add(Me.settingsMenuPanel)
            Me.Controls.Add(Me.settingsFormPanel)
            Me.Controls.Add(Me.buttonsPanel)

        End Sub

        Protected Overloads Overrides Sub ajustLayout(newSize As Size)

            Dim layout As SettingsFrameLayout = DirectCast(Me.layout, SettingsFrameLayout)

            Me.settingsMenuPanel.Location = layout.SettingsMenuPanel_Location
            Me.settingsMenuPanel.Size = layout.SettingsMenuPanel_Size

            Me.settingsFormPanel.Location = layout.SettingsFormPanel_Location
            Me.settingsFormPanel.Size = layout.SettingsFormPanel_Size

            Me.buttonsPanel.Location = layout.ButtonsPanel_Location
            Me.buttonsPanel.Size = layout.ButtonsPanel_Size

            Dim settingsMenuItemsSize As Size = New Size(Me.settingsMenuPanel.Width, Me.computeMenuItemsHeight())

            Dim _item As SettingsMenuItem
            For itemIndex = 0 To Me.settingsMenuPanel.Controls.Count - 1

                _item = DirectCast(Me.settingsMenuPanel.Controls(itemIndex), SettingsMenuItem)
                _item.Location = New Point(0, itemIndex * settingsMenuItemsSize.Height)

                If (itemIndex = Me.settingsMenuPanel.Controls.Count - 1) Then
                    ' Ajust last item's height to fill the whole height
                    _item.ajustLayout(New Size(Me.settingsMenuPanel.Width, Me.settingsMenuPanel.Height - (Me.settingsMenuPanel.Controls.Count - 1) * settingsMenuItemsSize.Height))
                Else

                    _item.ajustLayout(settingsMenuItemsSize)
                End If
            Next

            If (Not IsNothing(Me.selectedView)) Then

                Me.selectedView.ajustLayout(Me.settingsFormPanel)

                Me.selectedView.BackButton.Location = layout.BackButton_Location
                Me.selectedView.BackButton.Size = layout.BackButton_Size

                Me.selectedView.UndoButton.Location = layout.UndoButton_Location
                Me.selectedView.UndoButton.Size = layout.UndoButton_Size

                Me.selectedView.RedoButton.Location = layout.RedoButton_Location
                Me.selectedView.RedoButton.Size = layout.RedoButton_Size
            End If
        End Sub

        Protected Overloads Overrides Sub ajustLayoutFinal(newSize As Size)

            Me.selectedView.ajustLayoutFinal(Me.settingsFormPanel)

        End Sub

        Public Sub selectView(settingsView As SettingsView)

            If (Not settingsView.Equals(Me.selectedView)) Then

                If (Not IsNothing(Me.selectedView)) Then

                    Me.menuItems(Me.views.IndexOf(Me.selectedView)).IsSelected = False

                    Me.buttonsPanel.Controls.Remove(Me.selectedView.BackButton)
                    Me.buttonsPanel.Controls.Remove(Me.selectedView.UndoButton)
                    Me.buttonsPanel.Controls.Remove(Me.selectedView.RedoButton)

                    Me.selectedView.onHide()

                    Me.settingsFormPanel.Controls.Remove(Me.selectedView)

                    RemoveHandler Me.selectedView.SettingChangedEvent, AddressOf Me.onSettingsChanged

                End If

                Me.selectedView = settingsView

                Me.menuItems(Me.views.IndexOf(Me.selectedView)).IsSelected = True

                Me.selectedView.beforeShow(Me.settingsFormPanel)

                Me.buttonsPanel.Controls.Add(Me.selectedView.BackButton)
                Me.buttonsPanel.Controls.Add(Me.selectedView.UndoButton)
                Me.buttonsPanel.Controls.Add(Me.selectedView.RedoButton)

                Me.settingsFormPanel.Controls.Add(Me.selectedView)

                AddHandler Me.selectedView.SettingChangedEvent, AddressOf Me.onSettingsChanged

                Me.selectedView.afterShow()

                Me.ajustLayout(Me.Size)
                Me.ajustLayoutFinal(Me.Size)
            End If

        End Sub

        Public Sub addSettingView(view As SettingsView)

            Me.views.Add(view)

            Dim menuItem As SettingsMenuItem = Me.addMenuItem(view.Name)

            AddHandler menuItem.Clicked, Sub() Me.selectView(view)

        End Sub

        Private Function addMenuItem(name As String) As SettingsMenuItem

            Dim newItem = New SettingsMenuItem(name)

            Me.settingsMenuPanel.Controls.Add(newItem)

            Me.menuItems.Add(newItem)

            Return newItem

        End Function

        Protected Overloads Overrides Sub beforeShow()

            If (IsNothing(Me.selectedView)) Then

                Me.selectView(Me.views.First)

            Else

                Me.selectedView.beforeShow(Me.settingsFormPanel)

            End If

        End Sub

        Public Overrides Sub afterShow()

            Me.selectedView.afterShow()

            Me.ajustLayout(Me.Size)
            Me.ajustLayoutFinal(Me.Size)
        End Sub

        Public Overrides Sub onHide()

            Me.selectedView.onHide()
        End Sub

        Private Sub onSettingsChanged()

        End Sub

        Private Function computeMenuItemsHeight() As Integer

            Return CInt(Me.settingsMenuPanel.Size.Height / Me.settingsMenuPanel.Controls.Count)
        End Function

        Public Overrides ReadOnly Property Name As String
            Get
                Return "Paramètres"
            End Get
        End Property
    End Class
End Namespace
