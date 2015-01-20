Imports System.Windows.Forms.DataVisualization.Charting
Public Class PointFormatList

    Private colorArray() As Drawing.Color = {Drawing.Color.Blue, _
                                             Drawing.Color.Red, _
                                             Drawing.Color.Orange, _
                                             Drawing.Color.Cyan}

    Private markerArray() As MarkerStyle = {MarkerStyle.Square, _
                                            MarkerStyle.Cross, _
                                            MarkerStyle.Circle, _
                                            MarkerStyle.Diamond}

    Private pointFormats As New List(Of PointFormat)

    Public Function getFormatFor(mixName As String, asphaltName As String) As PointFormat

        Dim colorIndex = 0

        For Each _format In pointFormats
            If (_format.MIX_NAME.Equals(mixName) AndAlso _format.ASPHALT_NAME.Equals(asphaltName)) Then
                Return _format
            End If
        Next

        If (pointFormats.Count > 2) Then

            If (pointFormats.Count = 3) Then

                Me.pointFormats.Add(New PointFormat("Autres", "", colorArray(3), markerArray(3)))

            End If

        Else

            Me.pointFormats.Add(New PointFormat(mixName, asphaltName, colorArray(pointFormats.Count), markerArray(pointFormats.Count)))

        End If

        Return pointFormats.Last

    End Function

    Public Function getAllFormats() As List(Of PointFormat)
        Return Me.pointFormats
    End Function

    Public Class PointFormat

        Public Sub New(mixName As String, asphaltName As String, color As Drawing.Color, marker As MarkerStyle)
            Me.MIX_NAME = mixName
            Me.ASPHALT_NAME = asphaltName
            Me.COLOR = color
            Me.MARKER = marker
        End Sub

        Public Property MIX_NAME As String
        Public Property ASPHALT_NAME As String
        Public Property COLOR As Drawing.Color
        Public Property MARKER As MarkerStyle

    End Class

End Class
