Public Class UIExceptionHandler
    Implements ExceptionHandler

    Public Shared instance As UIExceptionHandler = New UIExceptionHandler

    Public Overloads Sub handle(ex As Exception) Implements ExceptionHandler.handle

        If (IsNothing(ex.InnerException)) Then

            MessageBox.Show(ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine & Environment.NewLine & "Le programme va maintenant se fermer.", ex.GetType.ToString, MessageBoxButtons.OK, MessageBoxIcon.Error)

            'Debugger.Break()

            XLSReport.killApp()
            DOCXReport.killApp()
            OleDBAdapter.kill()

            ' Not sure if safe... oh well
            Diagnostics.Process.GetCurrentProcess.Kill()

        Else

            handle(ex.InnerException)

        End If

    End Sub

    Public Overloads Function handle(ex As Runtime.InteropServices.COMException) As Boolean

        Dim answer = MainWindow.instance.Invoke(MainWindow.instance.showMessageBoxDelegate, "Un des documents Excel, Word ou PDF est ouvert. S'il vous plait, fermez le et appuyez sur Ok. Sinon appuyez sur annuler.", "", MessageBoxButtons.OKCancel)

        Return answer = DialogResult.OK

    End Function

    Public Overloads Function handle(ex As IO.IOException) As Boolean

        Dim answer = MainWindow.instance.Invoke(MainWindow.instance.showMessageBoxDelegate, "Un des documents de données (.csv ou .log) est ouvert! S'il vous plait, fermez le et appuyez sur Ok. Sinon appuyez sur annuler.", "", MessageBoxButtons.OKCancel)

        Return answer = DialogResult.OK

    End Function

    Public Overloads Function handle(ex As MasterUsineException) As Boolean

        Console.WriteLine(ex.Message)

        'Dim answer = MainWindow.instance.Invoke(MainWindow.instance.showMessageBoxDelegate, ex.uiMessage, ex.uiCaption, ex.uiMsgBoxButtons)

        Return True 'answer = DialogResult.OK

    End Function

End Class
