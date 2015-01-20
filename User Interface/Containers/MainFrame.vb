Namespace UI

    Public Class MainFrame
        Inherits Form

        ' Constants
        Public Shared ReadOnly ORIGINAL_SIZE As Size = New Size(850, 650)

        Public Const TIME_TO_WAIT_BEFORE_AFTER_RESIZE As Integer = 100

        ' Attributes
        Private resizeStart As Date
        Private afterResizeWasCalled As Boolean = True

        ' Threads
        Private afterResizeThread As Threading.Thread

        Public Sub New()

            ' Initialize components
            initializeComponents()

            ProgramController.initialize(Me)

        End Sub

        Private Sub initializeComponents()
            Me.SuspendLayout()

            Me.StartPosition = FormStartPosition.CenterScreen

            Me.Text = "IGNIS"
            Me.Size = ORIGINAL_SIZE

            Me.Icon = Constants.UI.Icons.PROGRAM

            Me.Font = Constants.UI.Fonts.DEFAULT_FONT

            Me.ResumeLayout()
        End Sub

        ''' <summary>
        ''' Calls methods that should be executed during a resize event
        ''' </summary>
        Public Sub _OnResize(sender As Object, eventsArgs As EventArgs) Handles Me.Resize

            For Each _control As Control In Me.Controls

                DirectCast(_control, UI.View).ajustLayout(Me)
            Next

            ' Throttle afterResize method
            Me.afterResizeWasCalled = False
            Me.resizeStart = Now

            If (IsNothing(Me.afterResizeThread) AndAlso Me.Visible) Then

                Me.afterResizeThread = New Threading.Thread(Sub() Me.throttleAfterResize())
                Me.afterResizeThread.Start()
            End If

        End Sub

        ''' <summary>
        ''' Calls methods that should be executed after a resize event
        ''' </summary>
        Public Sub _afterResize()
            Dim x = Me.Visible

            Me.afterResizeWasCalled = True

            If (Not IsNothing(Me.afterResizeThread)) Then

                Me.afterResizeThread.Abort()
                Me.afterResizeThread = Nothing
            End If

            For Each _control As Control In Me.Controls

                DirectCast(_control, UI.View).ajustLayoutFinal(Me)

            Next

        End Sub

        ' Non User Code
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)

            MyBase.Dispose(disposing)

        End Sub

        ' To call in different thread
        Private Sub throttleAfterResize()

            While (Now.Subtract(Me.resizeStart).TotalMilliseconds < TIME_TO_WAIT_BEFORE_AFTER_RESIZE)
                Threading.Thread.Sleep(50)
            End While

            If (Not Me.afterResizeWasCalled) Then

                Me.Invoke(Sub() _afterResize())
            End If
        End Sub

    End Class

End Namespace
