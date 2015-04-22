Public Class VirginAsphaltConcrete
    Inherits MixComponentUsed
    Implements IEquatable(Of VirginAsphaltConcrete)
    Private density As Double
    Private recordedTemperature As Double
    Private tankId As String
    Private grade As String

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)

        Me.density = -3
        Me.recordedTemperature = -3
        Me.tankId = "-3"
    End Sub

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, recordedTemperature As Double, density As Double, tankId As String, grade As String)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)

        Me.density = density
        Me.recordedTemperature = recordedTemperature
        Me.tankId = tankId
        Me.grade = grade

    End Sub

    Public Sub New(virginAsphaltConcrete As VirginAsphaltConcrete)
        MyBase.New(virginAsphaltConcrete.getTargetPercentage, virginAsphaltConcrete.getActualPercentage, virginAsphaltConcrete.getDebit, virginAsphaltConcrete.getMass)
        Me.density = virginAsphaltConcrete.getDensity
        Me.recordedTemperature = virginAsphaltConcrete.getRecordedTemperature
        Me.tankId = virginAsphaltConcrete.getTankId
        Me.grade = virginAsphaltConcrete.getGrade
    End Sub

    '' TODO 
    '' Grade devrait être récupéré par le catalogue s'il n'est pas disponible (à valider)
    Public ReadOnly Property getGrade() As String
        Get
            Return grade
        End Get
    End Property

    Public ReadOnly Property getDensity() As Double
        Get
            Return density
        End Get
    End Property

    Public ReadOnly Property getRecordedTemperature() As Double
        Get
            Return recordedTemperature
        End Get
    End Property

    Public ReadOnly Property getTankId() As String
        Get
            Return tankId
        End Get
    End Property
    '' TODO 
    '' Faire une validation de l'utilisation des catalogues
    Public Function getAsphaltName(productionDate As Date) As String
        Return Plant.asphaltCatalog.getDescriptionFromContainer(tankId, productionDate)
    End Function

    Public Function isVirginAsphaltEmpty() As Boolean

        If getMass() <= 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overloads Function Equals(ByVal virginAsphaltConcrete As VirginAsphaltConcrete) As Boolean Implements IEquatable(Of VirginAsphaltConcrete).Equals
        If Me.getTankId = virginAsphaltConcrete.getTankId And Me.getGrade = virginAsphaltConcrete.getGrade Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
