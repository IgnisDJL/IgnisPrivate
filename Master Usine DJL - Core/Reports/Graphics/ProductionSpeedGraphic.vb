Imports System.Windows.Forms.DataVisualization.Charting

Public Class ProductionSpeedGraphic
    Inherits XYScatterGraphic

    Public Property UNIT As Unit = XmlSettings.Settings.instance.Reports.PRODUCTION_SPEED_UNIT

    Private firstPointFormat As PointFormatList.PointFormat = New PointFormatList.PointFormat("", "", Drawing.Color.Blue, MarkerStyle.Square)
    Private secondPointFormat As PointFormatList.PointFormat = New PointFormatList.PointFormat("", "", Drawing.Color.Red, MarkerStyle.Cross)

    Private lastPointFormat As PointFormatList.PointFormat = firstPointFormat

    Public Sub New(debutPeriode As Date, finPeriode As Date)
        MyBase.New()

        Dim borneDebutAxeX As Date

        If debutPeriode < New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day, 12, 0, 0) Then
            borneDebutAxeX = New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day)
            Me.X_MINIMUM = borneDebutAxeX.ToOADate
        Else
            borneDebutAxeX = New Date(debutPeriode.Year, debutPeriode.Month, debutPeriode.Day, 12, 0, 0)
            Me.X_MINIMUM = borneDebutAxeX.ToOADate
        End If

        If finPeriode < borneDebutAxeX + TimeSpan.FromHours(24) Then
            Me.X_MAXIMUM = (borneDebutAxeX + TimeSpan.FromHours(24)).ToOADate
        Else
            Me.X_MAXIMUM = (borneDebutAxeX + TimeSpan.FromHours(36)).ToOADate
        End If

        Me.Y_TITLE = "Production (" & Me.UNIT.ToString & ")"

        Me.Y_LABEL_FORMAT = "### ###"

        Me.X_LABEL_FORMAT = "HH:mm"

        Me.X_LABEL_ORENTATION = 0

        ' settings
        Me.X_INTERVAL = Constants.Output.Graphics.intervalFromHours(4)

        Me.SIZE = New Drawing.Size(MyBase.SIZE.Width, MyBase.SIZE.Height * 0.705)

    End Sub

    Protected Overrides ReadOnly Property FILE_NAME As String
        Get
            Return Constants.Output.Graphics.SaveAsNames.PRODUCTION_SPEED_GRAPHIC
        End Get
    End Property

    Public Property MAXIMUM_TPH As Integer
    Public Property MINIMUM_TPH As Integer = TonsPerHour.UNIT.convert(1000, Me.UNIT)

    Public Sub toggleMarkerColor()

        If (Me.lastPointFormat.Equals(Me.firstPointFormat)) Then
            Me.lastPointFormat = Me.secondPointFormat

        Else
            Me.lastPointFormat = Me.firstPointFormat
        End If

    End Sub

    Public Sub setGraphicData(cyclesDateTime As List(Of Date), cyclesProductionSpeed As List(Of Double))
        For indexCycle As Integer = 0 To cyclesDateTime.Count - 1 Step 1

            If (cyclesProductionSpeed(indexCycle) > 0) Then

                If (cyclesProductionSpeed(indexCycle) > Me.MAXIMUM_TPH) Then
                    Me.MAXIMUM_TPH = cyclesProductionSpeed(indexCycle)
                End If

                If (cyclesProductionSpeed(indexCycle) < Me.MINIMUM_TPH) Then
                    Me.MINIMUM_TPH = cyclesProductionSpeed(indexCycle)
                End If

                Dim valuesForAverage As New List(Of Double)

                For i = 1 To 9

                    If ((indexCycle - i) >= 0) Then

                        If (cyclesProductionSpeed(indexCycle - i) > 0) Then

                            valuesForAverage.Add(cyclesProductionSpeed(indexCycle - i))
                            If (Double.IsInfinity(valuesForAverage.Last)) Then
                                Debugger.Break()
                            End If
                        End If

                    End If
                Next

                If (valuesForAverage.Count > 0) Then

                    Dim avg = valuesForAverage.Average
                    Me.MAIN_DATA_SERIE.Points.AddXY(cyclesDateTime(indexCycle), avg)
                    Me.MAIN_DATA_SERIE.Points.Last.Color = Me.lastPointFormat.COLOR
                    Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = Me.lastPointFormat.MARKER

                    If (avg > Me.MAXIMUM_TPH) Then
                        Me.MAXIMUM_TPH = avg
                    End If

                    If (avg < Me.MINIMUM_TPH) Then
                        Me.MINIMUM_TPH = avg
                    End If

                End If

            End If

        Next
    End Sub

    Public Overrides Sub addCycle(cycle As Cycle, dataFileNode As XmlSettings.DataFileNode)

        Dim cycleDateTime = cycle.TIME
        Dim cycleTph = dataFileNode.getUnitByTag(cycle.PRODUCTION_SPEED_TAG).convert(cycle.PRODUCTION_SPEED, Me.UNIT)

        If (cycleTph > 0) Then

            ' Sans lissage
            Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, cycleTph)
            Me.MAIN_DATA_SERIE.Points.Last.Color = Me.lastPointFormat.COLOR
            Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = Me.lastPointFormat.MARKER

            If (cycleTph > Me.MAXIMUM_TPH) Then
                Me.MAXIMUM_TPH = cycleTph
            End If

            If (cycleTph < Me.MINIMUM_TPH) Then
                Me.MINIMUM_TPH = cycleTph
            End If

            ' Avec lissage
            ' Mobile average
            Dim valuesForAverage As New List(Of Double)
            Dim cycleBuffer = cycle

            For i = 0 To 9

                If (Not IsNothing(cycleBuffer.PREVIOUS_CYCLE)) Then

                    cycleBuffer = cycleBuffer.PREVIOUS_CYCLE

                    If (cycleBuffer.PRODUCTION_SPEED > 0) Then

                        valuesForAverage.Add(dataFileNode.getUnitByTag(cycle.PRODUCTION_SPEED_TAG).convert(cycleBuffer.PRODUCTION_SPEED, Me.UNIT))
                        If (Double.IsInfinity(valuesForAverage.Last)) Then
                            Debugger.Break()
                        End If
                    End If

                End If
            Next

            If (valuesForAverage.Count > 0) Then

                Dim avg = valuesForAverage.Average
                Me.MAIN_DATA_SERIE.Points.AddXY(cycleDateTime, avg)
                Me.MAIN_DATA_SERIE.Points.Last.Color = Me.lastPointFormat.COLOR
                Me.MAIN_DATA_SERIE.Points.Last.MarkerStyle = Me.lastPointFormat.MARKER

                If (avg > Me.MAXIMUM_TPH) Then
                    Me.MAXIMUM_TPH = avg
                End If

                If (avg < Me.MINIMUM_TPH) Then
                    Me.MINIMUM_TPH = avg
                End If

            End If

        End If

    End Sub

    Protected Overrides Sub consolidate()

        If (MAIN_DATA_SERIE.Points.Count > 0) Then

            Me.Y_MAXIMUM = ((Me.MAXIMUM_TPH \ TonsPerHour.UNIT.convert(50, Me.UNIT)) + 1) * TonsPerHour.UNIT.convert(50, Me.UNIT)

            If (Me.MINIMUM_TPH < TonsPerHour.UNIT.convert(50, Me.UNIT)) Then
                Me.Y_MINIMUM = 0
            Else
                Me.Y_MINIMUM = ((Me.MINIMUM_TPH \ TonsPerHour.UNIT.convert(50, Me.UNIT))) * TonsPerHour.UNIT.convert(50, Me.UNIT)
            End If

            Me.Y_INTERVAL = (Me.Y_MAXIMUM - Me.Y_MINIMUM) / 5

        End If

        MyBase.consolidate()

    End Sub

    Protected Overrides ReadOnly Property NO_DATA_IMAGE_NAME As String
        Get
            Return "unavailableProductionSpeed_FR.bmp"
        End Get
    End Property
End Class
