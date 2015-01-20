Public Class IncorrectInputException
    Inherits MasterUsineException

    Public Sub New(innerException As IncorrectDataException, inputName As String)
        MyBase.New(New Exception("La valeur '" & innerException.NEW_VALUE & "' pour le champ '" & inputName & "' est incorrecte", innerException))
        ' #refactor from settings.language
    End Sub

End Class
