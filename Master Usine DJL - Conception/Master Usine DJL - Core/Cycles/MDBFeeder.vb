Public Class MDBFeeder
    Inherits Feeder

    Private belongingCycle As MDBCycle

    Public Sub New(belongingCycle As MDBCycle)
        MyBase.New(belongingCycle)

        Me.belongingCycle = belongingCycle

    End Sub

    Public Overrides Function getData(tagName As Tag) As Object

        Select Case tagName

            Case RECIPE_MASS_TAG
                Return RECIPE_MASS

            Case MANUAL_MODE_TAG
                Return MANUAL_MODE

            Case Else
                Return MyBase.getData(tagName)

        End Select

    End Function


    Public Property RECIPE_MASS As Double = Double.NaN

    Public Property MANUAL_MODE As Boolean

    ' --------Constants----------- '

    Public Shared ReadOnly RECIPE_MASS_TAG As Tag = New Tag("#RecipeMass", "Masse Recette", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly MANUAL_MODE_TAG As Tag = New Tag("#Manual", "Mode Manuel", Unit.NO_UNIT, False)

    Public Shared Shadows ReadOnly TAGS As Tag() = {RECIPE_MASS_TAG, _
                                                    MANUAL_MODE_TAG}

    Private isAggregate As Boolean = False
    Public Property IS_AGGREGATE As Boolean
        Get

            If (IsNothing(Me.LOCATION)) Then

                Return Me.isAggregate

            Else

                Dim regex As New System.Text.RegularExpressions.Regex("benne", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                Return Me.isAggregate OrElse regex.Match(Me.LOCATION).Success And Not Me.IS_RECYCLED

            End If

        End Get
        Set(value As Boolean)
            Me.isAggregate = value
        End Set
    End Property

    Private isAsphalt As Boolean = False
    Public Property IS_ASPHALT As Boolean
        Get

            If (IsNothing(Me.LOCATION) OrElse Me.isAsphalt) Then
                Return Me.isAsphalt

            Else

                Dim regex As New System.Text.RegularExpressions.Regex("bitume", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                Me.isAsphalt = regex.Match(Me.LOCATION).Success

            End If

            Return Me.isAsphalt

        End Get
        Set(value As Boolean)
            Me.isAsphalt = value
        End Set
    End Property

    Public Overrides Property IS_RECYCLED As Boolean
        Get

            If (IsNothing(Me.LOCATION)) Then
                Return MyBase.IS_RECYCLED

            Else

                Dim regex As New System.Text.RegularExpressions.Regex("recycl", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                Return MyBase.IS_RECYCLED OrElse regex.Match(Me.LOCATION).Success

            End If

        End Get
        Set(value As Boolean)
            MyBase.IS_RECYCLED = value
        End Set
    End Property

    Public Overrides Property IS_FILLER As Boolean
        Get

            If (IsNothing(Me.LOCATION)) Then
                Return MyBase.IS_FILLER

            Else

                Dim regex As New System.Text.RegularExpressions.Regex("filler", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                Return MyBase.IS_FILLER OrElse regex.Match(Me.LOCATION).Success

            End If

        End Get
        Set(value As Boolean)
            MyBase.IS_FILLER = value
        End Set
    End Property

End Class
