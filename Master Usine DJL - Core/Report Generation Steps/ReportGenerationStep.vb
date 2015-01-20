Public MustInherit Class ReportGenerationStep

    Protected progressPercentage As Integer
    Protected totalProgressionPercentage As Integer

    Public MustOverride Sub showPrevious()



    Public Overridable Sub cancelGeneration()
        ProgramController.ReportGenerationController.cancelGeneration()
    End Sub


End Class
