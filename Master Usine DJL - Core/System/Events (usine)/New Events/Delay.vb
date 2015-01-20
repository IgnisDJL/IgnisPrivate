Public Class Delay

    Private _startTime As Date
    Private _endTime As Date

    Private _code As DelayCode

    Public Sub New(startTime As Date, endTime As Date)
        Me._startTime = startTime
        Me._endTime = endTime
    End Sub

    Public ReadOnly Property Type As DelayType
        Get
            Return If(IsNothing(_code), Nothing, _code.Type)
        End Get
    End Property

    Public Property Code As DelayCode
        Get
            Return _code
        End Get
        Set(value As DelayCode)

            _code = value

            If (Not IsNothing(value)) Then
                IsUnknown = False
            End If

        End Set
    End Property

    Public Property IsJustifiable As Boolean = False

    Public Property Justification As String

    Public Property IsUnknown As Boolean

    Public ReadOnly Property StartTime As Date
        Get
            Return Me._startTime
        End Get
    End Property

    Public ReadOnly Property EndTime As Date
        Get
            Return _endTime
        End Get
    End Property

    Public ReadOnly Property Duration As TimeSpan
        Get
            Return _endTime.Subtract(_startTime)
        End Get
    End Property

End Class
