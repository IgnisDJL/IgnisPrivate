﻿Public Class RecycledAsphaltUsed
    Inherits AsphaltUsed

    Public Sub New(targetPercentage As Double, actualPercentage As Double, debit As Double, mass As Double)
        MyBase.New(targetPercentage, actualPercentage, debit, mass)
    End Sub
End Class
