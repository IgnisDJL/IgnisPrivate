Imports System.Net.Mail

Public Class FileExportationController

    ' Constants

    ' Attributes
    Private _sender As FactoryOperator = FactoryOperator.DEFAULT_OPERATOR

    Private _filesToExport As List(Of File)

    Private _emailSettings As EmailSettingsController

    ' Threads
    Private _emailSendingThread As Threading.Thread

    Public Sub New(emailSettingsController As EmailSettingsController)

        Me._filesToExport = New List(Of File)

        Me._emailSettings = emailSettingsController

    End Sub

    Public Sub sendFiles(comments As String)

        Try

            Dim subject As String
            Dim body As String
            Dim commentsSection As String = ""

            If (Not comments = "") Then
                commentsSection = "<br /><br />" & _
                                  "<div>********** Commentaires **********</div>" & _
                                  "<br />" & _
                                  "<div>" & System.Web.HttpUtility.HtmlEncode(comments).Replace(Environment.NewLine, "<br />") & "</div>"
            End If

            With XmlSettings.Settings.instance

                subject = "Sujet / Titre du message"

                body = _
                    "<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">" & _
                    "<html xmlns=""http://www.w3.org/1999/xhtml"">" & _
                    "<head>" & _
                        "<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />" & _
                        "<title>" & subject & "</title>" & _
                        "<meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>" & _
                    "</head>" & _
                    "<body>" & _
                        "<span>Usine : </span><span style=""font-weight: bold"">" & .Usine.PLANT_NAME & " (" & .Usine.PLANT_ID & ")</span>" & _
                        "<br /><br />" & _
                        "<span>Opérateur : </span><span style=""font-weight: bold"">" & Sender.ToString & "</span>" & _
                        commentsSection & _
                        "<br /><br /><br />" & _
                        "<div>Envoyé via le logiciel IGNIS</div>" & _
                    "</body>" & _
                    "</html>"

            End With

            With Me._emailSettings

                Dim smtpClient As New SmtpClient
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = New Net.NetworkCredential(.Credentials, .Password)
                smtpClient.Port = .Port
                smtpClient.Host = .Host
                smtpClient.EnableSsl = True

                Dim email As New MailMessage()

                email.From = New MailAddress("anyaddress@mail.com")

                For Each _recipient In .Recipients

                    If (_recipient.Selected) Then
                        email.To.Add(_recipient.Address)
                    End If
                Next

                For Each _defaultRecipient In .DefaultRecipients

                    email.Bcc.Add(_defaultRecipient.Address)
                Next

                For Each _file As File In Me.FilesToExport

                    If (TypeOf _file Is MDBFile) Then

                        email.Attachments.Add(New Attachment(DirectCast(_file, MDBFile).Copy.FullName))
                    Else

                        email.Attachments.Add(New Attachment(_file.getFileInfo.FullName))
                    End If
                Next

                email.Subject = subject
                email.IsBodyHtml = True

                email.Body = body

                Me._emailSendingThread = New Threading.Thread(Sub()
                                                                  Me.sendEmail(smtpClient, email)
                                                              End Sub)

                Me._emailSendingThread.Start()

            End With

        Catch ex As Exception

            Me.cancelEmailSending()

            ProgramController.UIController.EmailExportationView.showErrorSendingEmailPanel()
        End Try
    End Sub

    ' In different thread
    Private Sub sendEmail(smtpClient As SmtpClient, email As MailMessage)

        Try

            smtpClient.Send(email)

            ProgramController.UIController.invokeFromUIThread(Sub()
                                                                  Me.afterEmailSent()
                                                              End Sub)


        Catch error_t As Exception

            Dim isThreadAbordException As Boolean = False
            Dim innerException As Exception = error_t

            While (Not IsNothing(innerException.InnerException))

                If (TypeOf innerException.InnerException Is Threading.ThreadAbortException) Then

                    isThreadAbordException = True

                    Exit While
                End If

                innerException = innerException.InnerException

            End While

            If (Not isThreadAbordException) Then


                ProgramController.UIController.invokeFromUIThread(Sub()
                                                                      Me.cancelEmailSending()
                                                                      ProgramController.UIController.EmailExportationView.showErrorSendingEmailPanel()
                                                                  End Sub)
            End If

        End Try

    End Sub

    ' In same thread
    Private Sub afterEmailSent()

        ProgramController.UIController.EmailExportationView.showEmailSentSuccessfullyPanel()

    End Sub

    Public Sub cancelEmailSending()

        If (Not IsNothing(Me._emailSendingThread) AndAlso Me._emailSendingThread.IsAlive) Then

            Me._emailSendingThread.Abort()
            Me._emailSendingThread = Nothing
        End If

    End Sub

    Public Property Sender As FactoryOperator
        Get
            Return Me._sender
        End Get
        Set(value As FactoryOperator)
            Me._sender = value
        End Set
    End Property

    Public Sub saveFiles(newDirectory)

        For Each _file In Me.FilesToExport

            _file.getFileInfo.CopyTo(newDirectory & "/" & _file.getFileInfo.Name, True)

        Next

    End Sub

    Public ReadOnly Property FilesToExport As List(Of File)
        Get
            Return Me._filesToExport
        End Get
    End Property

    Public ReadOnly Property SendingInProgress As Boolean
        Get
            Return (Not IsNothing(Me._emailSendingThread) AndAlso Me._emailSendingThread.IsAlive)
        End Get
    End Property

End Class
