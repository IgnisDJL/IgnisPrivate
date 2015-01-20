Public Class MDBFile
    Inherits CyclesFile

    ' #language
    Public Shared ReadOnly GENERIC_NAME As String = "Base de données (.mdb)"

    Private copy As IO.FileInfo
    Private _date As Date

    Public Sub New(pathToDataBase As String)
        MyBase.New(pathToDataBase)
        Me.copy = Nothing
        Me._date = Me.getFileInfo.LastWriteTime
    End Sub

    Public Sub New(pathToDataBase As String, pathToCopy As String)
        MyBase.New(pathToDataBase)
        Me.copy = New IO.FileInfo(pathToCopy)
        Me._date = Me.getFileInfo.LastWriteTime
    End Sub

    Public Overrides Function getCycles(startTime As Date, endTime As Date) As List(Of Cycle)

        ' #todo

        Return Nothing
    End Function

    Public Overrides Sub open()

        If (IsNothing(Me.copy)) Then

            Diagnostics.Process.Start(Me.getFileInfo.FullName)

        Else

            Diagnostics.Process.Start(Me.copy.FullName)

        End If
    End Sub

    Public Overrides ReadOnly Property Date_ As Date
        Get
            Return Me._date
        End Get
    End Property
End Class
