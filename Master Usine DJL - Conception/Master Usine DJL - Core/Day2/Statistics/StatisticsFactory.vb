Public Class StatisticsFactory

    Public Shared Function getStatistics(datafilesSettings As XmlSettings.DataFilesNode) As Statistics

        With datafilesSettings

            If (.CSV.ACTIVE AndAlso .LOG.ACTIVE) Then
                Return New HybridStatistics()

            ElseIf (.CSV.ACTIVE) Then

            ElseIf (.LOG.ACTIVE) Then

            ElseIf (.MDB.ACTIVE) Then

            End If

        End With

    End Function

End Class
