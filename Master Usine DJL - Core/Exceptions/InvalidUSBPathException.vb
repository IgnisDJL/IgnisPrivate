Public Class InvalidUSBPathException
    Inherits MasterUsineException

    Public Sub New(path As String)
        MyBase.New(New Exception("Le dossier '" & path & "' n'est pas la clé USB IGNIS."))

    End Sub

End Class
