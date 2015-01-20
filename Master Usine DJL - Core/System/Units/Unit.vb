Public MustInherit Class Unit

    Public Shared ReadOnly DEFAULT_MASS_UNIT = Tons.UNIT
    Public Shared ReadOnly DEFAULT_TEMPERATURE_UNIT = Celsius.UNIT
    Public Shared ReadOnly DEFAULT_PRODUCTION_SPEED_UNIT = TonsPerHour.UNIT
    Public Shared ReadOnly DEFAULT_PERCENT_UNIT = Percent.UNIT

    Public Shared ReadOnly NO_UNIT = OtherUnit.UNIT

    Public Shared ReadOnly ALL_UNITS As Unit() = {KiloGrams.UNIT, _
                                                  OtherUnit.UNIT, _
                                                  Tons.UNIT, _
                                                  Celsius.UNIT, _
                                                  Fahrenheits.UNIT, _
                                                  KgPerHour.UNIT, _
                                                  KgPerMinute.UNIT, _
                                                  TonsPerHour.UNIT, _
                                                  TonsPerMinute.UNIT, _
                                                  PerOne.UNIT, _
                                                  Percent.UNIT, _
                                                  PerMille.UNIT}

    Public Shared ReadOnly MASS_UNITS As Unit() = {KiloGrams.UNIT, _
                                                   Tons.UNIT}

    Public Shared ReadOnly PRODUCTION_SPEED_UNITS As Unit() = {KgPerHour.UNIT, _
                                                               KgPerMinute.UNIT, _
                                                               TonsPerHour.UNIT, _
                                                               TonsPerMinute.UNIT}

    Public Shared ReadOnly TEMPERATURE_UNITS As Unit() = {Celsius.UNIT, _
                                                          Fahrenheits.UNIT}

    Public Shared ReadOnly PERCENT_UNITS As Unit() = {Percent.UNIT, _
                                                      PerMille.UNIT, _
                                                      PerOne.UNIT}


    Public Shared Function parse(unitSymbol As String) As Unit

        For Each _unit As Unit In ALL_UNITS
            If (_unit.SYMBOL = unitSymbol) Then
                Return _unit
            End If
        Next

        Return NO_UNIT

    End Function

    Public Overloads Function convert(value As Object, toUnit As Unit) As Object

        If TypeOf value Is Double Then
            Return Me.convert(DirectCast(value, Double), toUnit)
        End If

        Return value
    End Function

    Public Overridable Overloads Function convert(value As Double, toUnit As Unit) As Double
        Return value
    End Function

    Public Shared Operator &(str As String, unit As Unit)

        Return str & unit.ToString
    End Operator

    Public MustOverride ReadOnly Property SYMBOL As String

End Class
