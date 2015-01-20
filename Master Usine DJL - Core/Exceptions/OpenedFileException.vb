''' <summary>
''' Usually thrown when a opened file is being deleted or overwritten (saved over)
''' </summary>
''' <remarks></remarks>
Public Class OpenedFileException
    Inherits MasterUsineException

    Public Enum FileType
        Model_Docx = 1
        Model_XLS = 2
        PDF = 3
        Docx = 4
        XLS = 5
    End Enum

    Public Sub New(fileType As FileType, exeption As Exception)
        MyBase.New(exeption)

        Select Case fileType

            Case OpenedFileException.FileType.Model_Docx
                Me.uiMessage = "Le document model du rapport journalier est présentement ouvert. Fermez-le si possible et appuyez sur OK. Sinon appuyez sur Annuler."

            Case OpenedFileException.FileType.Model_XLS
                Me.uiMessage = "Le document model de la feuille d'analyse est présentement ouvert. Fermez-le si possible et appuyez sur OK. Sinon appuyez sur Annuler."

            Case OpenedFileException.FileType.Docx
                Me.uiMessage = "Le document WORD du rapport journalier est présentement ouvert. Fermez-le ou sauvegardez-le sous un autre nom si possible et appuyez sur OK. Sinon appuyez sur Annuler."

            Case OpenedFileException.FileType.PDF
                Me.uiMessage = "Le document PDF du rapport journalier est présentement ouvert. Fermez-le ou sauvegardez-le sous un autre nom si possible et appuyez sur OK. Sinon appuyez sur Annuler."

            Case OpenedFileException.FileType.XLS
                Me.uiMessage = "Le document de la feuille d'analyse est présentement ouvert. Fermez-le ou sauvegardez-le sous un autre nom si possible et appuyez sur OK. Sinon appuyez sur Annuler."

        End Select

    End Sub

End Class
