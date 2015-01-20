''' <summary>
''' Represents a file containing data from the factory. This type of files are generally stored in the Archives folder.
''' </summary>
Public MustInherit Class DataFile
    Inherits File

    Public Sub New(filePath As String)
        MyBase.New(filePath)
    End Sub

    Public MustOverride Overrides ReadOnly Property Date_ As Date

End Class
