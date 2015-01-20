Public Class ExcelProcessException
    Inherits Exception

    Private _xlsProcess As New List(Of Diagnostics.Process)
    Public ReadOnly Property XLS_PROCESS As List(Of Diagnostics.Process)
        Get
            Return Me._xlsProcess
        End Get
    End Property


    Public Sub New(xlsProcess() As Diagnostics.Process)

        MyBase.New("Excel is already open")

        For Each singleXlsProcess In xlsProcess

            Me.XLS_PROCESS.Add(singleXlsProcess)

        Next

        ' For now...

        For Each _process In Me.XLS_PROCESS
            _process.Kill()
        Next


    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
        ' Add other code for custom properties here.
    End Sub

    Public Sub New(ByVal message As String, ByVal inner As Exception)
        MyBase.New(message, inner)
        ' Add other code for custom properties here.
    End Sub

End Class
