Public MustInherit Class File

    Private fileInfo As IO.FileInfo

    ' #refactor - All the files should have the date passed in their constructor.
    ' This would remove the task of computing the date from the file object (more coherence)
    ' So when you pull the files out of the database you already have a date and when you're not, you can compute the date with the name and a regex or
    ' with the last write time of the file
    Public Sub New(filePath As String)

        Me.fileInfo = New IO.FileInfo(filePath)

        If (Not fileInfo.Exists) Then

            ' #exception

        End If

    End Sub

    Public Overridable Sub open()
        Diagnostics.Process.Start(Me.fileInfo.FullName)
    End Sub

    Public Overridable Function getFileInfo() As IO.FileInfo
        Return Me.fileInfo
    End Function

    Public Overrides Function ToString() As String
        Return Me.fileInfo.Name
    End Function

    Public MustOverride ReadOnly Property Date_ As Date

    Public Overrides Function Equals(obj As Object) As Boolean

        If (TypeOf obj Is File) Then

            Return (Me.getFileInfo.FullName.Equals(DirectCast(obj, File).getFileInfo.FullName))

        Else
            Return False
        End If

    End Function

End Class
