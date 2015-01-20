Namespace UI

    Public MustInherit Class SettingsView
        Inherits Panel

        ' Components
        Private WithEvents _backButton As Common.BackButton
        Private WithEvents _undoButton As Button
        Private WithEvents _redoButton As Button

        ' Attributes
        Private verticalScrollBuffer As Integer = 0
        Protected Shadows layout As LayoutManager

        Protected updatingFields As Boolean = False

        ' Events        
        Public Event SettingChangedEvent()

        Protected Sub New()
            MyBase.New()

        End Sub

        Protected Overridable Sub initializeComponents()

            Me.AutoScroll = True

            Me._backButton = New Common.BackButton
            AddHandler _backButton.Click, AddressOf Me.defaultBackBehavior

            Me._undoButton = New Button
            Me._undoButton.Text = "Défaire"
            Me._undoButton.ImageAlign = ContentAlignment.MiddleLeft
            Me._undoButton.TextAlign = ContentAlignment.MiddleRight
            Me._undoButton.Image = Constants.UI.Images._24x24.UNDO
            Me._undoButton.Enabled = False

            ' The order in which the event handlers are added is important.
            AddHandler _undoButton.Click, AddressOf Me.Controller.undo
            AddHandler _undoButton.Click, AddressOf Me.raiseSettingChangedEvent

            Me._redoButton = New Button
            Me._redoButton.Text = "Refaire"
            Me._redoButton.ImageAlign = ContentAlignment.MiddleRight
            Me._redoButton.TextAlign = ContentAlignment.MiddleLeft
            Me._redoButton.Image = Constants.UI.Images._24x24.REDO
            Me._redoButton.Enabled = False

            ' The order in which the event handlers are added is important.
            AddHandler _redoButton.Click, AddressOf Me.Controller.redo
            AddHandler _redoButton.Click, AddressOf Me.raiseSettingChangedEvent

            Me.focusOnClick()
        End Sub

        Public Sub ajustLayout(container As Control)

            Me.Size = container.ClientSize

            If (Me.VerticalScroll.Visible) Then
                Me.verticalScrollBuffer = Me.VerticalScroll.Value
            End If

            Me.AutoScroll = False

            Me.layout.computeLayout(container.ClientSize)

            ajustLayout()
        End Sub

        Public Sub ajustLayoutFinal(container As Control)

            Me.Size = container.ClientSize

            ajustLayoutFinal()

            Me.AutoScroll = True

            ' #Patch - Bug fix. If only one call to .value, the scrollbar doesn't move. Weird...
            Me.VerticalScroll.Value = Me.verticalScrollBuffer
            Me.VerticalScroll.Value = Me.verticalScrollBuffer
        End Sub

        Public Sub beforeShow(container As Control)
            container.MinimumSize = layout.MinimumSize
            beforeShow()
            ajustLayout(container)
            ajustLayoutFinal(container)
            updateFields()
        End Sub


        Protected MustOverride Sub ajustLayout()

        ' Called after the resize event of the main window
        Protected MustOverride Sub ajustLayoutFinal()

        Public MustOverride Sub updateFields()

        Protected MustOverride Sub beforeShow()

        Public MustOverride Sub afterShow()

        Public MustOverride Sub onHide() Handles Me.Disposed

        Private Sub updateUndoRedoButtonsState()

            Me._undoButton.Enabled = Me.Controller.CanUndo
            Me._redoButton.Enabled = Me.Controller.CanRedo

        End Sub

        Protected Overridable Sub defaultBackBehavior()

            ProgramController.UIController.changeView(ProgramController.UIController.MainMenuView)

        End Sub

        Public MustOverride Shadows ReadOnly Property Name As String

        Public ReadOnly Property LayoutManager As LayoutManager
            Get
                Return Me.layout
            End Get
        End Property

        Protected Sub raiseSettingChangedEvent()

            RaiseEvent SettingChangedEvent()

            updateFields()
            updateUndoRedoButtonsState()
        End Sub

        Protected MustOverride ReadOnly Property Controller As SettingsController

        Public ReadOnly Property BackButton As Common.BackButton
            Get
                Return Me._backButton
            End Get
        End Property

        Public ReadOnly Property RedoButton As Button
            Get
                Return Me._redoButton
            End Get
        End Property

        Public ReadOnly Property UndoButton As Button
            Get
                Return Me._undoButton
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function

        Protected Overridable Sub focusOnClick()

            AddHandler Me.Click, AddressOf Me.Focus

            AddHandler Me.UndoButton.Click, AddressOf Me.Focus
            AddHandler Me.RedoButton.Click, AddressOf Me.Focus
        End Sub

    End Class
End Namespace