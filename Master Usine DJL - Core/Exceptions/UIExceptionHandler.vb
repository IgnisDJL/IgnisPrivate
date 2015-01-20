Public Class UIExceptionHandler
    Implements ExceptionHandler

    Public Shared instance As UIExceptionHandler = New UIExceptionHandler

    Public Overloads Sub handle(sende As Object, exceptionArgs As UnhandledExceptionEventArgs) Implements ExceptionHandler.handle

        Me.handle(exceptionArgs.ExceptionObject)

    End Sub

    Public Overloads Sub handle(ex As Exception)

        If (IsNothing(ex.InnerException)) Then

            If (MessageBox.Show(ex.Message & _
                Environment.NewLine & _
                Environment.NewLine & _
                ex.StackTrace & _
                Environment.NewLine & _
                Environment.NewLine & _
                "Appuyez sur OK pour redémarer le logiciel.", _
                ex.GetType.ToString, _
                MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Error) = DialogResult.OK) Then

                Diagnostics.Process.Start("IGNIS.exe")

            End If

            'Debugger.Break()

            OleDBAdapter.kill()

            ' Not sure if safe... oh well
            Diagnostics.Process.GetCurrentProcess.Kill()
        Else

            handle(ex.InnerException)

        End If

    End Sub

    Public Overloads Function handle(ex As Runtime.InteropServices.COMException) As Boolean

        Throw New NotImplementedException

    End Function

    Public Overloads Function handle(ex As IO.IOException) As Boolean

        Throw New NotImplementedException

    End Function

    Public Overloads Function handle(ex As MasterUsineException) As Boolean

        Console.WriteLine(ex.Message)

        'Dim answer = MainWindow.instance.Invoke(MainWindow.instance.showMessageBoxDelegate, ex.uiMessage, ex.uiCaption, ex.uiMsgBoxButtons)

        Return True 'answer = DialogResult.OK

    End Function

End Class
