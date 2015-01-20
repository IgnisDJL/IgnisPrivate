''' <summary>
''' Interface for the model files generators. Each model generator has the task
''' to generate their own model file if necessary.
''' </summary>
''' <remarks>By generate, I mean make a new file or update an old model</remarks>
Public MustInherit Class XLSModel

    ''' <summary>
    ''' Method in wich the model generator will build and save the model file.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Function generateModel() As Microsoft.Office.Interop.Excel.Workbook


    Protected Sub saveAs(workBook As Microsoft.Office.Interop.Excel.Workbook, fullPath As String, extension As Integer)

        Try

            workBook.SaveAs(fullPath, extension)

        Catch ex As Exception

            If (UIExceptionHandler.instance.handle(New OpenedFileException(OpenedFileException.FileType.Model_XLS, ex))) Then
                Me.saveAs(workBook, fullPath, extension)
            End If

        End Try

    End Sub

End Class
