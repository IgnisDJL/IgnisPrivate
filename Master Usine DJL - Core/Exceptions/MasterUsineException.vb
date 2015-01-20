Public Class MasterUsineException
    Inherits Exception

    Public uiMessage As String
    Public uiCaption As String = ""
    Public uiMsgBoxButtons As MessageBoxButtons = MessageBoxButtons.OKCancel


    Public Sub New(ex As Exception)
        MyBase.New(ex.Message, ex.InnerException)

    End Sub

End Class
