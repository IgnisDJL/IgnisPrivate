
Public MustInherit Class ProductionDay

    Public Property XLS_REPORT As XLSReport

    Public Property DOCX_REPORT As DOCXReport

    Protected currentDataFile As DataFileNode

    Public Shared generateModel As Boolean = True

    Private Shared currentDate As Date
    Public Shared ReadOnly Property CURRENT_DATE As Date
        Get
            Return currentDate
        End Get
    End Property

    Protected Sub New(date_ As Date)
        ProductionDay.currentDate = date_
    End Sub

    ' Time related
    Protected _date As Date
    Public ReadOnly Property DATE_ As Date
        Get
            Return Me._date
        End Get
    End Property

    Protected startTime As Date
    Public ReadOnly Property START_TIME As Date
        Get
            Return Me.startTime
        End Get
    End Property

    Protected endTime As Date
    Public ReadOnly Property END_TIME As Date
        Get
            Return Me.endTime
        End Get
    End Property

    Public ReadOnly Property DAY_TOTAL_TIMESPAN As TimeSpan
        Get
            Return Me.endTime.Subtract(Me.startTime)
        End Get
    End Property

    ' Events Stats
    Public ReadOnly Property STOP_TIMESPAN As TimeSpan
        Get
            Return Events.STOP_EVENTS_DURATION
        End Get
    End Property

    Protected continuousProduction_duration As TimeSpan = TimeSpan.Zero
    Public ReadOnly Property CONTINUOUS_TIMESPAN As TimeSpan
        Get
            Return Me.continuousProduction_duration
        End Get
    End Property

    Protected batchProduction_duration As TimeSpan = TimeSpan.Zero
    Public ReadOnly Property BATCH_TIMESPAN As TimeSpan
        Get
            Return Me.batchProduction_duration
        End Get
    End Property

    ' Mix related
    Private mixStats As New List(Of MixStatistics)
    Public ReadOnly Property MIX_STATS As List(Of MixStatistics)
        Get
            Return Me.mixStats
        End Get
    End Property

    Private asphaltStats As New List(Of AsphaltStatistics)
    Public ReadOnly Property ASPHALT_STATS As List(Of AsphaltStatistics)
        Get
            Return Me.asphaltStats
        End Get
    End Property

    Private otherMix As New MixStatistics With {.NAME = "Autres"}
    Public ReadOnly Property OTHER_MIX_STATS As MixStatistics
        Get
            Return Me.otherMix
        End Get
    End Property

    Private totalMix As New MixStatistics With {.NAME = "Total"}
    Public ReadOnly Property TOTAL_MIX_STATS As MixStatistics
        Get
            Return Me.totalMix
        End Get
    End Property

    Protected continuousProduction_nbMixSwitch As Integer
    Public ReadOnly Property NUMBER_OF_MIX_SWITCH_CONTINUOUS As Integer
        Get
            Return Me.continuousProduction_nbMixSwitch
        End Get
    End Property

    Protected batchProduction_nbMixSwitch As Integer
    Public ReadOnly Property NUMBER_OF_MIX_SWITCH_BATCH As Integer
        Get
            Return Me.batchProduction_nbMixSwitch
        End Get
    End Property

    Protected continuousProduction_totalMass As Double
    Public ReadOnly Property TOTAL_MASS_CONTINUOUS As Integer
        Get
            Return Me.continuousProduction_totalMass
        End Get
    End Property

    Protected batchProduction_totalMass As Double
    Public ReadOnly Property TOTAL_MASS_BATCH As Integer
        Get
            Return Me.batchProduction_totalMass
        End Get
    End Property

    Public ReadOnly Property TONS_PER_HOUR_CONTINUOUS As Double
        Get
            Dim tph = XmlSettings.Settings.instance.Report.Word.MASS_UNIT.convert(Me.TOTAL_MASS_CONTINUOUS, Tons.UNIT) / DAY_TOTAL_TIMESPAN.TotalHours
            Return TonsPerHour.UNIT.convert(tph, XmlSettings.Settings.instance.Report.Word.PRODUCTION_SPEED_UNIT)
        End Get
    End Property

    Public ReadOnly Property TONS_PER_HOUR_BATCH As Double
        Get
            Dim tph = XmlSettings.Settings.instance.Report.Word.MASS_UNIT.convert(Me.TOTAL_MASS_BATCH, Tons.UNIT) / DAY_TOTAL_TIMESPAN.TotalHours
            Return TonsPerHour.UNIT.convert(tph, XmlSettings.Settings.instance.Report.Word.PRODUCTION_SPEED_UNIT)
        End Get
    End Property

    Protected nbStops As Integer
    Public ReadOnly Property NUMBER_OF_STOPS As Integer
        Get

            If (Events.NB_STARTS > 0) Then

                Return Events.NB_STOPS

            Else
                Return -1

            End If

        End Get
    End Property

    Public RECYCLED_MIX_FEED_NAME As String = Nothing
    Public RECYCLED_ASPHALT_MASS As Double = Double.NaN

    Public MustOverride Sub gatherData()
    Public MustOverride Sub generateGraphics()
    Public MustOverride Sub generateReports()

End Class
