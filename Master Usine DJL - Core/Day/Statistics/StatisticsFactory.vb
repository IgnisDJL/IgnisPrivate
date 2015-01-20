Public Class StatisticsFactory

    Public Shared Function getStatistics(datafilesSettings As XmlSettings.DataFilesNode) As Statistics

        With datafilesSettings

            If (.CSV.ACTIVE AndAlso .LOG.ACTIVE) Then
                Return New HybridStatistics()

            ElseIf (.CSV.ACTIVE) Then

                Return New CSVStatistiques()

            ElseIf (.LOG.ACTIVE) Then

                Return New LOGStatistics()

            ElseIf (.MDB.ACTIVE) Then

                Return New MDBStatistiques()

            End If

        End With

        Throw New NotImplementedException

    End Function

End Class
