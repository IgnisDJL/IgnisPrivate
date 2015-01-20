Namespace UI.Common

    Public Class FileListItem
        Inherits ListItem(Of File)

        ' Constants


        ' Components
        Private WithEvents iconPanel As Panel
        Private iconToolTip As ToolTip

        Private WithEvents fileNameLabel As Label
        Private userCantOpenDataFilesTooltip As ToolTip

        Private WithEvents checkBox As CheckBox

        ' Attributes
        Private Shadows icon As Image
        Private iconCaption As String

        Private _showCheckBox As Boolean = False

        Public Property DisplayDateForMDB As Boolean = True

        ' Events
        Public Event CheckedChange(item As FileListItem, checked As Boolean)

        Public Sub New(file As File, icon As Image, iconCaption As String)
            MyBase.New(file)

            Me.icon = icon
            Me.iconCaption = iconCaption

            Me.initializeComponents()
        End Sub

        Protected Overrides Sub initializeComponents()

            Me.iconToolTip = New ToolTip
            Me.iconToolTip.ShowAlways = True
            Me.iconToolTip.Active = True
            Me.iconToolTip.InitialDelay = 500
            Me.iconToolTip.BackColor = Color.White

            Me.iconPanel = New Panel
            Me.iconPanel.BackgroundImage = Me.icon
            Me.iconPanel.BackgroundImageLayout = ImageLayout.Center
            Me.iconToolTip.SetToolTip(Me.iconPanel, iconCaption)

            Me.fileNameLabel = New Label
            Me.fileNameLabel.AutoSize = False

            If (TypeOf Me.ItemObject Is MDBFile AndAlso Not Me.DisplayDateForMDB) Then
                Me.fileNameLabel.Text = Me.ItemObject.ToString()
            Else
                Me.fileNameLabel.Text = Me.ItemObject.ToString() & " - " & Me.ItemObject.Date_.ToString("d MMM yyyy")
            End If

            Me.userCantOpenDataFilesTooltip = New ToolTip
            Me.userCantOpenDataFilesTooltip.Active = False
            Me.userCantOpenDataFilesTooltip.InitialDelay = 35
            Me.userCantOpenDataFilesTooltip.AutoPopDelay = 5000
            Me.userCantOpenDataFilesTooltip.SetToolTip(Me.fileNameLabel, "Vous n'avez pas la permission d'ouvrir les fichiers de données.")

            Me.checkBox = New CheckBox
            Me.checkBox.CheckAlign = ContentAlignment.MiddleCenter
            Me.checkBox.Cursor = Cursors.Hand

            Me.Controls.Add(iconPanel)
            Me.Controls.Add(fileNameLabel)

        End Sub

        Public Overrides Sub ajustLayout(newSize As Size)
            Me.Size = newSize

            Me.iconPanel.Location = New Point(0, 0)
            Me.iconPanel.Size = New Size(newSize.Height, newSize.Height)

            Me.checkBox.Location = New Point(Me.ClientSize.Width - Me.Height, 0)
            Me.checkBox.Size = New Size(Me.Height, Me.Height)

            Me.fileNameLabel.Location = New Point(Me.iconPanel.Width, 0)

            If (Me._showCheckBox) Then

                Me.fileNameLabel.Size = New Size(Me.ClientSize.Width - Me.iconPanel.Width - checkBox.Width, Me.Height)
            Else

                Me.fileNameLabel.Size = New Size(Me.ClientSize.Width - Me.iconPanel.Width, Me.Height)
            End If

        End Sub

        Private Sub _onClick() Handles Me.Click, fileNameLabel.Click, iconPanel.Click
            raiseClickEvent()
        End Sub

        Private toolTipThreads As New List(Of Threading.Thread)
        Private Sub _onDoubleClick() Handles Me.DoubleClick, fileNameLabel.DoubleClick, iconPanel.DoubleClick

            If (TypeOf Me.ItemObject Is DataFile) Then

                If (ProgramController.SettingsControllers.AdminSettingsController.UserIsAdmin OrElse _
                    ProgramController.SettingsControllers.AdminSettingsController.UserCanOpenDataFiles) Then

                    Me.ItemObject.open()

                Else
                    Beep()
                    Me.userCantOpenDataFilesTooltip.Active = True

                    For Each _thr As Threading.Thread In toolTipThreads
                        _thr.Abort()
                    Next
                    toolTipThreads.Clear()

                    ' Remove tooltip after the lenght of an autopop delay
                    Dim toolTipThread As New Threading.Thread(Sub()
                                                                  Dim start = Now
                                                                  Dim tt = Me.userCantOpenDataFilesTooltip

                                                                  While (Now.Subtract(start).TotalMilliseconds < tt.AutoPopDelay)

                                                                      Threading.Thread.Sleep(1000)
                                                                  End While

                                                                  tt.Active = False
                                                              End Sub)
                    toolTipThread.Start()
                    toolTipThreads.Add(toolTipThread)
                End If

            Else

                Me.ItemObject.open()
            End If

        End Sub

        Public Property ShowCheckBox As Boolean
            Get
                Return Me._showCheckBox
            End Get
            Set(value As Boolean)

                If (Not value = Me._showCheckBox) Then

                    If (value) Then
                        Me.Controls.Add(Me.checkBox)
                    Else
                        Me.Controls.Remove(Me.checkBox)
                    End If

                    Me._showCheckBox = value
                End If
            End Set
        End Property

        Private Sub onChecked() Handles checkBox.CheckedChanged

            RaiseEvent CheckedChange(Me, Me.IsChecked)

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
            ' Do nothing here...
        End Sub

        Public Overrides Sub onUnselect()
            ' Do nothing here...
        End Sub

    End Class
End Namespace
