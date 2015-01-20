Public Class DelayType

    Private _name As String
    Private _color As Color
    Private _codes As List(Of DelayCode)

    Private _isPause As Boolean
    Private _isMaintenance As Boolean
    Private _isBreakage As Boolean
    Private _isIntern As Boolean
    Private _isExtern As Boolean

    Public Sub New(name As String, color As Color, isPause As Boolean, isMaintenance As Boolean, isBreakage As Boolean, isIntern As Boolean, isExtern As Boolean)

        Me._codes = New List(Of DelayCode)

        Me._name = name
        Me._color = color
        Me._isPause = isPause
        Me._isMaintenance = isMaintenance
        Me._isBreakage = isBreakage
        Me._isIntern = isIntern
        Me._isExtern = isExtern
    End Sub

    Public Sub addCode(code As DelayCode)
        code.Type = Me
        Me.Codes.Add(code)
    End Sub

    Public ReadOnly Property Codes As List(Of DelayCode)
        Get
            Return Me._codes
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Color As Color
        Get
            Return _color
        End Get
    End Property

    Public ReadOnly Property IsOther As Boolean
        Get
            Return Not (IsBreakage OrElse _
                        IsPause OrElse _
                        IsIntern OrElse _
                        IsMaintenance OrElse _
                        IsExtern)
        End Get
    End Property

    Public ReadOnly Property IsPause As Boolean
        Get
            Return _isPause
        End Get
    End Property

    Public ReadOnly Property IsMaintenance As Boolean
        Get
            Return _isMaintenance
        End Get
    End Property

    Public ReadOnly Property IsBreakage As Boolean
        Get
            Return _isBreakage
        End Get
    End Property

    Public ReadOnly Property IsIntern As Boolean
        Get
            Return _isIntern
        End Get
    End Property

    Public ReadOnly Property IsExtern As Boolean
        Get
            Return _isExtern
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Me._name
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean

        If (TypeOf obj Is DelayType) Then
            Return Me.Name.Equals(DirectCast(obj, DelayType).Name)
        Else
            Return False
        End If
        End

    End Function

    Public Shared Operator =(mine As DelayType, his As Object)
        Return mine.Equals(his)
    End Operator

    Public Shared Operator <>(mine As DelayType, his As Object)
        Return Not mine.Equals(his)
    End Operator

End Class
