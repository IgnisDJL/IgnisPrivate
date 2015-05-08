Public MustInherit Class ReportFormater

    Protected Shared ReadOnly UNKNOWN_VALUE_CHARACTER As String = "?"
    Protected Shared ReadOnly INVALID_VALUE_CHARACTER As String = "-"


    Private _timeFormat As String
    Private _durationFormat As String
    Private _shortDateFormat As String
    Private _fullDateFormat As String
    Private _dateTimeFormat As String
    Private _unknownValue As String
    Private _invalidValue As String

    Protected Sub New(timeFormat As String, durationFormat As String, shortDateFormat As String, fullDateFormat As String, dateTimeFormat As String, unknwownValueCharacter As String, invalidValueCharacter As String)
        Me._timeFormat = timeFormat
        Me._durationFormat = durationFormat
        Me._shortDateFormat = shortDateFormat
        Me._fullDateFormat = fullDateFormat
        Me._dateTimeFormat = dateTimeFormat
        Me._unknownValue = unknwownValueCharacter
        Me._invalidValue = invalidValueCharacter
    End Sub

    'Public Function getProductionTypeString(reportType As MixStatistics.ProductionTypes)

    '    Select Case reportType

    '        Case MixStatistics.ProductionTypes.Continuous
    '            Return "Continu"

    '        Case MixStatistics.ProductionTypes.Discontinuous
    '            Return "Discontinu"

    '        Case Else
    '            Return Me.InvalidValueCharacter

    '    End Select
    'End Function

    Public Function getManualDataString(value As Double, Optional stringFormat As String = Nothing, Optional canBeInvalid As Boolean = True, Optional canBeUnknown As Boolean = True) As String

        If (canBeUnknown AndAlso value.Equals(ManualData.INVALID_QUANTITY)) Then
            Return Me.InvalidValueCharacter
        ElseIf (canBeInvalid AndAlso value.Equals(ManualData.UNKNOWN_QUANTITY)) Then
            Return Me.UnknownValueCharacter
        Else

            Return If(IsNothing(stringFormat), value.ToString(), value.ToString(stringFormat))
        End If

    End Function

    ''' <summary>
    ''' Returns the format for Time strings such as the time of the day
    ''' </summary>
    Public ReadOnly Property TimeFormat As String
        Get
            Return Me._timeFormat
        End Get
    End Property

    ''' <summary>
    ''' Returns the format for duration strings such as time spans
    ''' </summary>
    Public ReadOnly Property DurationFormat As String
        Get
            Return Me._durationFormat
        End Get
    End Property

    ''' <summary>
    ''' Returns the format for short date strings
    ''' </summary>
    Public ReadOnly Property ShortDateFormat As String
        Get
            Return Me._shortDateFormat
        End Get
    End Property

    ''' <summary>
    ''' Returns the format for long date strings
    ''' </summary>
    Public ReadOnly Property FullDateFormat As String
        Get
            Return Me._fullDateFormat
        End Get
    End Property

    ''' <summary>
    ''' Returns the format for strings including a date and a time
    ''' </summary>
    Public ReadOnly Property DateTimeFormat As String
        Get
            Return Me._dateTimeFormat
        End Get
    End Property

    ''' <summary>
    ''' Returns the character representing an unknown value
    ''' </summary>
    Public ReadOnly Property UnknownValueCharacter As String
        Get
            Return Me._unknownValue
        End Get
    End Property

    ''' <summary>
    ''' Returns the character representing an invalid value
    ''' </summary>
    Public ReadOnly Property InvalidValueCharacter As String
        Get
            Return Me._invalidValue
        End Get
    End Property

    Public Shared Function FormatTimeSpan(span As TimeSpan) As String

        If (span.Minutes < 10) Then
            Return String.Format("{0}h0{1}", _
                             CInt(Math.Truncate(span.TotalHours)), _
                             span.Minutes)
        Else
            Return String.Format("{0}h{1}", _
                     CInt(Math.Truncate(span.TotalHours)), _
                     span.Minutes)
        End If



    End Function

End Class
