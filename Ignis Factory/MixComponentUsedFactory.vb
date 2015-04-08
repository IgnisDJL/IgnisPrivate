Imports System.Globalization

Public Class MixComponentUsedFactory
    Public Sub New()
        Application.CurrentCulture = New CultureInfo("EN-US")
    End Sub
    Public Function createAsphaltUsed(indexCycle As Integer, sourceFile As SourceFile) As VirginAsphaltConcrete
        Dim asphaltUsed As VirginAsphaltConcrete
        Dim targetPercentage As Double
        Dim actualPercentage As Double
        Dim debit As Double
        Dim mass As Double
        Dim asphaltRecordedTemperature As Double
        Dim asphaltDensity As Double
        Dim asphaltTankId As String
        Dim asphaltGrade As String

        asphaltRecordedTemperature = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteRecordedTemperature(indexCycle, sourceFile)
        asphaltDensity = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteDensity(indexCycle, sourceFile)
        asphaltTankId = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteTankId(indexCycle, sourceFile)
        asphaltGrade = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteGrade(indexCycle, sourceFile)
        targetPercentage = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteTargetPercentage(indexCycle, sourceFile)
        actualPercentage = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteActualPercentage(indexCycle, sourceFile)
        debit = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteDebit(indexCycle, sourceFile)
        mass = sourceFile.sourceFileAdapter.getCycleAsphaltConcreteMass(indexCycle, sourceFile)

        asphaltUsed = New VirginAsphaltConcrete(targetPercentage, actualPercentage, debit, mass, asphaltRecordedTemperature, asphaltDensity, asphaltTankId, asphaltGrade)

        Return asphaltUsed
    End Function
End Class
