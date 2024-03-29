﻿Public Class RecycledColdFeeder
    Inherits ColdFeeder

    Private asphaltPercentage As Double

    Public Sub New(feederId As String, materialID As String, targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double, moisturePercentage As Double, asphaltPercentage As Double, productionDate As Date)
        MyBase.New(feederId, materialID, targetPercentage, actualPercentage, debit, mass, moisturePercentage)
        Me.asphaltPercentage = asphaltPercentage
    End Sub

    Public ReadOnly Property getAsphaltPercentage() As Double
        Get
            Return asphaltPercentage
        End Get
    End Property


    Public Overrides Function isRecycled() As Boolean
        Return True
    End Function
End Class
