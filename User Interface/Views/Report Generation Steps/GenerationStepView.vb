Namespace UI

    Public MustInherit Class GenerationStepView
        Inherits Panel

        ' Components
        Protected WithEvents backButton As Common.BackButton
        Protected WithEvents cancelButton As Common.CancelButton

        ' Attributes
        Protected Shadows layout As LayoutManager
        Protected _otherButtons As List(Of Button)

        ' Events
        ' #refactor - MustInherit
        Public Event progressEvent(currentProgressPercentage As Integer)

        Protected Sub New()

            Me._otherButtons = New List(Of Button)

            ' Initialize buttons
            Me.backButton = New Common.BackButton
            Me.cancelButton = New Common.CancelButton

        End Sub

        Public Sub ajustLayout(mySize As Size)

            Me.Size = mySize

            If (Not IsNothing(Me.layout)) Then
                Me.layout.computeLayout(mySize)
            End If

            ajustLayout()

        End Sub

        Public Sub ajustLayoutFinal(mySize As Size)
            Me.Size = mySize
            ajustLayoutFinal()
        End Sub

        Public Sub beforeShow(mySize As Size)
            beforeShow()
            ajustLayout(mySize)
            ajustLayoutFinal(mySize)
        End Sub

        Protected MustOverride Sub initializeComponents()

        Protected MustOverride Sub ajustLayout()

        ' Called after the resize event of the main window
        Protected MustOverride Sub ajustLayoutFinal()

        Protected MustOverride Sub beforeShow()

        Public MustOverride Sub afterShow()

        Public MustOverride Sub onHide() Handles Me.Disposed

        Public Function getBackButton() As Common.BackButton
            Return Me.backButton
        End Function

        Public Function getCancelButton() As Common.CancelButton
            Return Me.cancelButton
        End Function

        Public ReadOnly Property OtherButtons() As List(Of Button)
            Get
                Return Me._otherButtons
            End Get
        End Property

        Protected MustOverride Sub goBack() Handles backButton.Click

        Protected MustOverride Sub cancel() Handles cancelButton.Click

        Protected Sub raiseProgressEvent(currentProgressPercentage As Integer)
            RaiseEvent progressEvent(currentProgressPercentage)
        End Sub

        Public MustOverride Shadows ReadOnly Property Name As String

        Public MustOverride Shadows ReadOnly Property OverallProgressValue As Integer

        Public ReadOnly Property LayoutManager As LayoutManager
            Get
                Return Me.layout
            End Get
        End Property

    End Class
End Namespace
