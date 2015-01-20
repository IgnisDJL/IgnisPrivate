Public Class Feeder

    Public Sub New(belongingCycle As Cycle)

        Me.belongingCycle = belongingCycle

    End Sub

    Public Overridable Function getData(tagName As Tag) As Object

        Select Case tagName

            Case Feeder.LOCATION_TAG
                Return Me.LOCATION

            Case Feeder.MATERIAL_NAME_TAG
                Return Me.MATERIAL_NAME

            Case Feeder.MASS_TAG
                Return Me.MASS

            Case Feeder.SET_POINT_MASS_TAG
                Return Me.SET_POINT_MASS

            Case Feeder.ACCUMULATED_MASS_TAG
                Return Me.ACCUMULATED_MASS

            Case Feeder.SET_POINT_PERCENTAGE_TAG
                Return Me.SET_POINT_PERCENTAGE

            Case Feeder.PERCENTAGE_TAG
                Return Me.PERCENTAGE

            Case Else
                Throw New InvalidTagException("Invalid tag => " & tagName.TAG_NAME)

        End Select

    End Function

    Private belongingCycle As Cycle
    Public ReadOnly Property BELONGING_CYCLE As Cycle
        Get
            Return Me.belongingCycle
        End Get
    End Property

    Public Property INDEX As Integer = 0

    Public Property LOCATION As String = Nothing

    Public Property MATERIAL_NAME As String = Nothing

    Public Property MASS As Double = Double.NaN



    Public Overridable Property IS_RECYCLED As Boolean = False

    Public Overridable Property IS_FILLER As Boolean = False

    ' --------Calculated---------- '
    Public Property ACCUMULATED_MASS As Double = Double.NaN

    Public Property SET_POINT_PERCENTAGE As Double = Double.NaN

    Private _percentage As Double = Double.NaN
    Public Property PERCENTAGE As Double
        Get
            Return If(Double.IsNaN(Me._percentage), Me.MASS / Me.BELONGING_CYCLE.MIX_MASS * 100, Me._percentage)
        End Get
        Set(value As Double)
            Me._percentage = value
        End Set
    End Property

    Private setPointMass As Double = Double.NaN
    Public Property SET_POINT_MASS As Double
        Get

            If (Double.IsNaN(Me.setPointMass) AndAlso Not Double.IsNaN(Me.SET_POINT_PERCENTAGE)) Then
                Me.setPointMass = Me.SET_POINT_PERCENTAGE / 100 * Me.BELONGING_CYCLE.MIX_MASS
            End If

            Return Me.setPointMass
        End Get
        Set(value As Double)
            Me.setPointMass = value
        End Set
    End Property

    ' --------Constants----------- '


    Public Shared ReadOnly LOCATION_TAG As Tag = New Tag("#Location", "Emplacement", Unit.NO_UNIT, False)

    Public Shared ReadOnly MATERIAL_NAME_TAG As Tag = New Tag("#MaterialName", "Matériau", Unit.NO_UNIT, False)

    Public Shared ReadOnly MASS_TAG As Tag = New Tag("#FeedMass", "Masse", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly SET_POINT_MASS_TAG As Tag = New Tag("#SetPointMass", "Masse visée", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly ACCUMULATED_MASS_TAG As Tag = New Tag("#FeedAccumulatedMass", "Masse Accumulée", Unit.DEFAULT_MASS_UNIT, False)

    Public Shared ReadOnly SET_POINT_PERCENTAGE_TAG As Tag = New Tag("#SetPointPercentage", "Pourcentage Visé", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly PERCENTAGE_TAG As Tag = New Tag("#Percentage", "Pourcentage Actuel", Unit.DEFAULT_PERCENT_UNIT, False)

    Public Shared ReadOnly TAGS As Tag() = {LOCATION_TAG, _
                                            MATERIAL_NAME_TAG, _
                                            MASS_TAG, _
                                            SET_POINT_MASS_TAG, _
                                            ACCUMULATED_MASS_TAG, _
                                            SET_POINT_PERCENTAGE_TAG, _
                                            PERCENTAGE_TAG}

    Public Overrides Function ToString() As String
        Return Me.LOCATION.ToString()
    End Function

End Class
